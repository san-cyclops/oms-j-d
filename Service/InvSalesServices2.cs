using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using MoreLinq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EntityFramework.Extensions;
using System.Reflection;

using Domain;
using Data;
using System.Data;
using Utility;
using System.Collections;
using System.Data.Common;
using System.Data.Linq;
using System.Data.Entity.Validation;
using MoreLinq;
using System.Collections;
using System.Data.Entity.Core.Objects;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.ComponentModel;
using System.Configuration;

namespace Service
{
    public class InvSalesServices
    {        
        string sql;
        //ERPDbContext context = new ERPDbContext();
        
        /// <summary>
        ///  SaleTypes - DocumentID in InvSales table
        /// </summary>
        public enum SaleTypes
        {
            Sales = 1,
            Returns = 2,
            DepartmentSales = 3,
            DepartmentReturns = 4
        }

        public enum CustomerTypes
        {
            Normal = 1,
            Staff = 2,
            Loyalty = 3
        }

        #region Sales
        public InvSalesDetailTemp getSalesDetailTemp(List<InvSalesDetailTemp> InvPurchaseDetailTemp, string ProductCode, int locationID, DateTime expiryDate, long unitOfMeasureID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                InvSalesDetailTemp invSalesDetailTemp = new InvSalesDetailTemp();

                invSalesDetailTemp = InvPurchaseDetailTemp.Where(p => p.ProductCode == ProductCode).FirstOrDefault();
                return invSalesDetailTemp;
            }
        }

        public List<OtherExpenseTemp> getPausedExpence(InvSalesHeader invSalesHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<OtherExpenseTemp> otherExpenceTempList = new List<OtherExpenseTemp>();
                List<AccGlTransactionDetail> accGlTransactionDetails = new List<AccGlTransactionDetail>();
                AccLedgerAccountService AccLedgerAccountService = new AccLedgerAccountService();

                accGlTransactionDetails = context.AccGlTransactionDetails.Where(ps => ps.ReferenceDocumentID.Equals(invSalesHeader.SalesHeaderID) && ps.ReferenceDocumentDocumentID.Equals(invSalesHeader.DocumentID)).OrderBy(ps => ps.AccGlTransactionDetailID).ToList();
                foreach (AccGlTransactionDetail accGlTransactionDetailTemp in accGlTransactionDetails)
                {
                    OtherExpenseTemp otherExpenceTemp = new OtherExpenseTemp();
                    AccLedgerAccount accLedgerAccount = new Domain.AccLedgerAccount();

                    accLedgerAccount = AccLedgerAccountService.GetAccLedgerExpenseAccountByID(accGlTransactionDetailTemp.LedgerID);

                    otherExpenceTemp.AccLedgerAccountID = accGlTransactionDetailTemp.LedgerID;
                    otherExpenceTemp.ExpenseAmount = accGlTransactionDetailTemp.Amount;
                    otherExpenceTemp.LedgerCode = accLedgerAccount.LedgerCode;
                    otherExpenceTemp.LedgerName = accLedgerAccount.LedgerName;

                    otherExpenceTempList.Add(otherExpenceTemp);
                }

                return otherExpenceTempList.OrderBy(pd => pd.OtherExpenseTempID).ToList();
            }
        }
        
        public List<InvProductSerialNoTemp> getPausedSalesSerialNoDetail(InvSalesHeader invSalesHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();
                List<InvProductSerialNoDetail> invProductSerialNoDetail = new List<InvProductSerialNoDetail>();

                invProductSerialNoDetail = context.InvProductSerialNoDetails.Where(ps => ps.ReferenceDocumentID.Equals(invSalesHeader.SalesHeaderID) && ps.ReferenceDocumentDocumentID.Equals(invSalesHeader.DocumentID)).ToList();
                foreach (InvProductSerialNoDetail invProductSerialNoDetailTemp in invProductSerialNoDetail)
                {
                    InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();

                    invProductSerialNoTemp.ExpiryDate = invProductSerialNoDetailTemp.ExpiryDate;
                    invProductSerialNoTemp.LineNo = invProductSerialNoDetailTemp.LineNo;
                    invProductSerialNoTemp.ProductID = invProductSerialNoDetailTemp.ProductID;
                    invProductSerialNoTemp.SerialNo = invProductSerialNoDetailTemp.SerialNo;
                    invProductSerialNoTemp.UnitOfMeasureID = invProductSerialNoDetailTemp.UnitOfMeasureID;


                    invProductSerialNoTempList.Add(invProductSerialNoTemp);

                }

                return invProductSerialNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public List<PaymentTemp> getPausedPayment(InvSalesHeader invSalesHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<PaymentTemp> paymentTempList = new List<PaymentTemp>();
                List<AccPaymentDetail> accPaymentDetail = new List<AccPaymentDetail>();
                PaymentMethodService paymentMethodService = new PaymentMethodService();

                accPaymentDetail = context.AccPaymentDetails.Where(ps => ps.ReferenceDocumentID.Equals(invSalesHeader.SalesHeaderID) && ps.ReferenceDocumentDocumentID.Equals(invSalesHeader.DocumentID)).OrderBy(ps => ps.AccPaymentDetailID).ToList();
                foreach (AccPaymentDetail accPaymentDetailTemp in accPaymentDetail)
                {
                    PaymentTemp paymentTemp = new PaymentTemp();
                    AccLedgerAccount accLedgerAccount = new Domain.AccLedgerAccount();
                    BankService bankService = new BankService();

                    Bank bank = new Bank();
                    bank = bankService.GetBankByID(accPaymentDetailTemp.BankID);

                    paymentTemp.AccLedgerAccountID = 0;
                    paymentTemp.BankID = accPaymentDetailTemp.BankID;

                    if (bank != null)
                    {
                        paymentTemp.BankCode = bank.BankCode;
                        paymentTemp.BankName = bank.BankName;
                    }

                    paymentTemp.CardCheqNo = accPaymentDetailTemp.CardNo;
                    paymentTemp.ChequeDate = accPaymentDetailTemp.ChequeDate;
                    paymentTemp.PayAmount = accPaymentDetailTemp.Amount;
                    paymentTemp.PaymentMethod = paymentMethodService.GetPaymentMethodsByTypeID(accPaymentDetailTemp.paymentModeID).PaymentMethodName;
                    paymentTemp.PaymentMethodID = accPaymentDetailTemp.paymentModeID;
                    paymentTempList.Add(paymentTemp);
                }

                return paymentTempList.OrderBy(pd => pd.AccLedgerAccountID).ToList();
            }
        }

        public List<InvSalesDetailTemp> getPausedSalesDetail(InvSalesHeader invSalesHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                InvSalesDetailTemp invSalesDetailTemp;

                List<InvSalesDetailTemp> invSalesDetailTempList = new List<InvSalesDetailTemp>();

                var salesDetailTemp = (from pm in context.InvProductMasters
                                       join pd in context.InvSalesDetails on pm.InvProductMasterID equals pd.ProductID
                                       join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                       join lc in context.Locations on pd.DispatchLocationID equals lc.LocationID
                                       where pd.LocationID.Equals(invSalesHeader.LocationID) && pd.CompanyID.Equals(invSalesHeader.CompanyID)
                                       && pd.DocumentID.Equals(invSalesHeader.DocumentID) && pd.InvSalesHeaderID.Equals(invSalesHeader.InvSalesHeaderID)
                                       select new
                                       {
                                           pd.AvgCost,
                                           pd.ConvertFactor,
                                           pd.CostPrice,
                                           pd.CurrentQty,
                                           pd.DiscountAmount,
                                           pd.DiscountPercentage,
                                           pd.BatchNo,
                                           pd.ExpiryDate,
                                           pd.FreeQty,
                                           pd.GrossAmount,
                                           pd.LineNo,
                                           pd.LocationID,
                                           pd.NetAmount,
                                           pd.OrderQty,
                                           pm.ProductCode,
                                           pd.ProductID,
                                           pm.ProductName,
                                           lc.LocationCode,
                                           pd.Qty,
                                           pd.SellingPrice,
                                           pd.SubTotalDiscount,
                                           pd.TaxAmount1,
                                           pd.TaxAmount2,
                                           pd.TaxAmount3,
                                           pd.TaxAmount4,
                                           pd.TaxAmount5,
                                           pd.UnitOfMeasureID,
                                           uc.UnitOfMeasureName,
                                           pd.DispatchLocationID,
                                       }).ToArray();

                foreach (var tempProduct in salesDetailTemp)
                {
                    invSalesDetailTemp = new InvSalesDetailTemp();

                    invSalesDetailTemp.AverageCost = tempProduct.AvgCost;
                    invSalesDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    invSalesDetailTemp.Rate = tempProduct.SellingPrice;
                    invSalesDetailTemp.CurrentQty = tempProduct.CurrentQty;
                    invSalesDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                    invSalesDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                    invSalesDetailTemp.BatchNo = tempProduct.BatchNo;
                    invSalesDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                    invSalesDetailTemp.FreeQty = tempProduct.FreeQty;
                    invSalesDetailTemp.GrossAmount = tempProduct.GrossAmount;
                    invSalesDetailTemp.LineNo = tempProduct.LineNo;
                    invSalesDetailTemp.LocationID = tempProduct.LocationID;
                    invSalesDetailTemp.NetAmount = tempProduct.NetAmount;
                    invSalesDetailTemp.ProductCode = tempProduct.ProductCode;
                    invSalesDetailTemp.ProductID = tempProduct.ProductID;
                    invSalesDetailTemp.ProductName = tempProduct.ProductName;
                    invSalesDetailTemp.DispatchLocation = tempProduct.LocationCode;
                    invSalesDetailTemp.DispatchLocationID = tempProduct.DispatchLocationID;
                    invSalesDetailTemp.Qty = tempProduct.Qty;
                    invSalesDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                    invSalesDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                    invSalesDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                    invSalesDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                    invSalesDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                    invSalesDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                    invSalesDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invSalesDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    //  invSalesDetailTemp.DispatchLocation = tempProduct.LocationCode;

                    invSalesDetailTempList.Add(invSalesDetailTemp);
                }

                return invSalesDetailTempList.OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public InvSalesHeader getSalesHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {return context.InvSalesHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();}
        }
        
        public InvSalesHeader getPausedSalesHeaderByDocumentID(int documentDocumentID, long documentID, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            { return context.InvSalesHeaders.Where(ph => ph.DocumentID.Equals(documentDocumentID) && ph.InvSalesHeaderID.Equals(documentID) && ph.LocationID.Equals(locationID) && ph.DocumentStatus.Equals(0)).FirstOrDefault(); }
        }

        public InvSalesHeader GetSavedSalesHeaderByDocumentID(int documentDocumentID, long documentID)
        {
            using (ERPDbContext context = new ERPDbContext())
            { return context.InvSalesHeaders.Where(ph => ph.DocumentID.Equals(documentDocumentID) && ph.InvSalesHeaderID.Equals(documentID) && ph.DocumentStatus.Equals(1)).FirstOrDefault(); }
        }
        
        public List<InvSalesDetailTemp> getInvoiceDetail(InvSalesHeader invSalesrHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                InvSalesDetailTemp invSalesDetailTemp;

                List<InvSalesDetailTemp> invSalesDetailTempList = new List<InvSalesDetailTemp>();

                var salesDetailTemp = (from pd in context.InvSalesDetails 
                                       join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                       join lc in context.Locations on pd.DispatchLocationID equals lc.LocationID
                                       where pd.LocationID.Equals(invSalesrHeader.LocationID) 
                                       && pd.DocumentID.Equals(invSalesrHeader.DocumentID) && pd.InvSalesHeaderID.Equals(invSalesrHeader.InvSalesHeaderID)
                                       select new
                                       {
                                           pd.ConvertFactor,
                                           pd.BatchNo,
                                           pd.ExpiryDate,
                                           pd.CostPrice,
                                           pd.CurrentQty,
                                           pd.DiscountAmount,
                                           pd.DiscountPercentage,
                                           pd.FreeQty,
                                           pd.GrossAmount,
                                           pd.LineNo,
                                           pd.LocationID,
                                           pd.NetAmount,
                                           pd.OrderQty,
                                           pd.ProductCode,
                                           pd.ProductID,
                                           pd.ProductName,
                                           lc.LocationCode,
                                           IsBatch =0,
                                           pd.Qty,
                                           pd.IssuedQty,
                                           pd.BalanceQty,
                                           pd.SellingPrice,
                                           pd.SubTotalDiscount,
                                           pd.TaxAmount1,
                                           pd.TaxAmount2,
                                           pd.TaxAmount3,
                                           pd.TaxAmount4,
                                           pd.TaxAmount5,
                                           pd.UnitOfMeasureID,
                                           uc.UnitOfMeasureName

                                       }).ToArray();

                foreach (var tempProduct in salesDetailTemp)
                {
                    invSalesDetailTemp = new InvSalesDetailTemp();

                    invSalesDetailTemp.AverageCost = 0;
                    invSalesDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    invSalesDetailTemp.Rate = tempProduct.SellingPrice;
                    invSalesDetailTemp.CurrentQty = tempProduct.CurrentQty;
                    invSalesDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                    invSalesDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                    invSalesDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                    invSalesDetailTemp.BatchNo = tempProduct.BatchNo;
                    invSalesDetailTemp.FreeQty = tempProduct.FreeQty;
                    invSalesDetailTemp.GrossAmount = tempProduct.GrossAmount;
                    invSalesDetailTemp.LineNo = tempProduct.LineNo;
                    invSalesDetailTemp.LocationID = tempProduct.LocationID;
                    invSalesDetailTemp.NetAmount = tempProduct.NetAmount;
                    //invSalesDetailTemp.OrderQty = tempProduct.OrderQty;
                    invSalesDetailTemp.ProductCode = tempProduct.ProductCode;
                    invSalesDetailTemp.ProductID = tempProduct.ProductID;
                    invSalesDetailTemp.ProductName = tempProduct.ProductName;
                    invSalesDetailTemp.DispatchLocation = tempProduct.LocationCode;
                    invSalesDetailTemp.IsBatch = false;
                    invSalesDetailTemp.Qty = tempProduct.Qty;
                    invSalesDetailTemp.BalanceQty = tempProduct.BalanceQty;
                    invSalesDetailTemp.DispatchQty = tempProduct.BalanceQty;
                    invSalesDetailTemp.IssuedQty = (tempProduct.Qty - tempProduct.BalanceQty);
                    invSalesDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                    invSalesDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                    invSalesDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                    invSalesDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                    invSalesDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                    invSalesDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                    invSalesDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invSalesDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                    invSalesDetailTempList.Add(invSalesDetailTemp);
                }

                return invSalesDetailTempList.OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public string[] GetAllInvoiceNumbers()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> documentNoList = context.InvSalesHeaders.Where(q => q.DocumentStatus != 3).Select(q => q.DocumentNo).ToList();
                return documentNoList.ToArray();
            }
        }

        public InvQuotationHeader GetQuotationHeaderToInvoice(string documentNo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {return context.InvQuotationHeaders.Where(ph => ph.DocumentNo.Equals(documentNo)).FirstOrDefault();}
        }

        public InvSalesHeader GetDispatchHeaderToInvoice(string documentNo, int documentID)
        {
            using (ERPDbContext context = new ERPDbContext())
            { return context.InvSalesHeaders.Where(ph => ph.DocumentNo.Equals(documentNo) && ph.DocumentID == documentID).FirstOrDefault(); }
        }
        
        public List<InvSalesDetailTemp> GetPausedQuotationDetail(InvQuotationHeader invQuotationHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                InvSalesDetailTemp invSalesDetailTemp;

                List<InvSalesDetailTemp> invSalesDetailTempList = new List<InvSalesDetailTemp>();

                var quotationDetailTemp = (from pm in context.InvProductMasters
                                           join qd in context.InvQuotationDetails on pm.InvProductMasterID equals qd.ProductID
                                           join uc in context.UnitOfMeasures on qd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                           where qd.LocationID.Equals(invQuotationHeader.LocationID) && qd.CompanyID.Equals(invQuotationHeader.CompanyID) && qd.BalanceQty > 0
                                           && qd.DocumentID.Equals(invQuotationHeader.DocumentID) && qd.InvQuotationHeaderID.Equals(invQuotationHeader.InvQuotationHeaderID)
                                           select new
                                           {
                                               qd.ConvertFactor,
                                               qd.CostPrice,
                                               pm.PackSize,
                                               qd.DiscountAmount,
                                               qd.DiscountPercentage,
                                               qd.GrossAmount,
                                               qd.LineNo,
                                               qd.LocationID,
                                               qd.NetAmount,
                                               qd.OrderQty,
                                               pm.ProductCode,
                                               qd.ProductID,
                                               pm.ProductName,
                                               qd.SellingPrice,
                                               qd.SubTotalDiscount,
                                               qd.TaxAmount1,
                                               qd.TaxAmount2,
                                               qd.TaxAmount3,
                                               qd.TaxAmount4,
                                               qd.TaxAmount5,
                                               qd.UnitOfMeasureID,
                                               uc.UnitOfMeasureName

                                           }).ToArray();

                foreach (var tempProduct in quotationDetailTemp)
                {

                    invSalesDetailTemp = new InvSalesDetailTemp();

                    invSalesDetailTemp.Rate = tempProduct.CostPrice;
                    //invSalesDetailTemp.PackSize = tempProduct.PackSize;
                    //invSalesDetailTemp.CurrentQty = tempProduct.CurrentQty;
                    invSalesDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                    invSalesDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                    invSalesDetailTemp.GrossAmount = tempProduct.GrossAmount;
                    invSalesDetailTemp.LineNo = tempProduct.LineNo;
                    invSalesDetailTemp.LocationID = tempProduct.LocationID;
                    invSalesDetailTemp.NetAmount = tempProduct.NetAmount;
                    invSalesDetailTemp.Qty = tempProduct.OrderQty;
                    invSalesDetailTemp.BalanceQty = tempProduct.OrderQty;
                    invSalesDetailTemp.ProductCode = tempProduct.ProductCode;
                    invSalesDetailTemp.ProductID = tempProduct.ProductID;
                    invSalesDetailTemp.ProductName = tempProduct.ProductName;
                    //invSalesDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    invSalesDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                    invSalesDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                    invSalesDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                    invSalesDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                    invSalesDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                    invSalesDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                    invSalesDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invSalesDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                    invSalesDetailTempList.Add(invSalesDetailTemp);
                }
                return invSalesDetailTempList.OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public List<InvSalesDetailTemp> GetDispatchDetail(InvSalesHeader invDispatchHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                InvSalesDetailTemp invSalesDetailTemp;

                List<InvSalesDetailTemp> invSalesDetailTempList = new List<InvSalesDetailTemp>();

                var quotationDetailTemp = (from pm in context.InvProductMasters
                                           join qd in context.InvSalesDetails on pm.InvProductMasterID equals qd.ProductID
                                           join uc in context.UnitOfMeasures on qd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                           join lc in context.Locations on qd.DispatchLocationID equals lc.LocationID
                                           where qd.LocationID.Equals(invDispatchHeader.LocationID) && qd.CompanyID.Equals(invDispatchHeader.CompanyID)
                                           && qd.DocumentID.Equals(invDispatchHeader.DocumentID) && qd.InvSalesHeaderID.Equals(invDispatchHeader.InvSalesHeaderID)
                                           select new
                                           {
                                               qd.ConvertFactor,
                                               qd.CostPrice,
                                               pm.PackSize,
                                               qd.DiscountAmount,
                                               qd.DiscountPercentage,
                                               qd.GrossAmount,
                                               qd.LineNo,
                                               qd.LocationID,
                                               qd.NetAmount,
                                               qd.OrderQty,
                                               qd.Qty,
                                               qd.IssuedQty,
                                               qd.BalanceQty,
                                               pm.ProductCode,
                                               qd.ProductID,
                                               pm.ProductName,
                                               lc.LocationCode,
                                               pm.IsBatch,
                                               qd.BatchNo,
                                               qd.ExpiryDate,
                                               qd.SellingPrice,
                                               qd.SubTotalDiscount,
                                               qd.TaxAmount1,
                                               qd.TaxAmount2,
                                               qd.TaxAmount3,
                                               qd.TaxAmount4,
                                               qd.TaxAmount5,
                                               qd.UnitOfMeasureID,
                                               uc.UnitOfMeasureName

                                           }).ToArray();

                foreach (var tempProduct in quotationDetailTemp)
                {

                    invSalesDetailTemp = new InvSalesDetailTemp();

                    invSalesDetailTemp.Rate = tempProduct.CostPrice;
                    //invSalesDetailTemp.PackSize = tempProduct.PackSize;
                    //invSalesDetailTemp.CurrentQty = tempProduct.CurrentQty;
                    invSalesDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                    invSalesDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                    invSalesDetailTemp.GrossAmount = tempProduct.GrossAmount;
                    invSalesDetailTemp.LineNo = tempProduct.LineNo;
                    invSalesDetailTemp.LocationID = tempProduct.LocationID;
                    invSalesDetailTemp.DispatchLocationID = tempProduct.LocationID;
                    invSalesDetailTemp.NetAmount = tempProduct.NetAmount;
                    //invSalesDetailTemp.Qty = tempProduct.OrderQty;

                    //invSalesDetailTemp.BalanceQty = tempProduct.OrderQty;
                    invSalesDetailTemp.ProductCode = tempProduct.ProductCode;
                    invSalesDetailTemp.ProductID = tempProduct.ProductID;
                    invSalesDetailTemp.ProductName = tempProduct.ProductName;
                    invSalesDetailTemp.DispatchLocation = tempProduct.LocationCode;
                    invSalesDetailTemp.IsBatch = tempProduct.IsBatch;
                    //invSalesDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    invSalesDetailTemp.BatchNo = tempProduct.BatchNo;
                    invSalesDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                    invSalesDetailTemp.Qty = tempProduct.Qty;
                    invSalesDetailTemp.BalanceQty = tempProduct.BalanceQty;
                    invSalesDetailTemp.DispatchQty = tempProduct.BalanceQty;
                    invSalesDetailTemp.IssuedQty = (tempProduct.Qty - tempProduct.BalanceQty);

                    invSalesDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                    invSalesDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                    invSalesDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                    invSalesDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                    invSalesDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                    invSalesDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                    invSalesDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invSalesDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                    invSalesDetailTempList.Add(invSalesDetailTemp);
                }
                return invSalesDetailTempList.OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public string[] GetAllDocumentNumbersToInvoice(int documentID, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> documentNoList = new List<string>();

                var sql = (from ph in context.InvSalesHeaders
                           join pd in context.InvSalesDetails on ph.InvSalesHeaderID equals pd.InvSalesHeaderID
                           where ph.DocumentID == documentID && ph.LocationID == locationID && pd.BalanceQty != 0
                           group pd by new
                           {
                               ph.DocumentNo,
                               pd.BalanceQty,
                           } into poGroup

                           select new
                           {
                               DocumentNo = poGroup.Key.DocumentNo,
                               BalanceQty = poGroup.Sum(x => x.BalanceQty),
                           }).ToArray();

                foreach (var temp in sql)
                {
                    documentNoList = sql.Where(a => a.BalanceQty > 0).Select(a => a.DocumentNo).ToList();
                }

                return documentNoList.ToArray();
            }
        }

        
        public bool Save(InvSalesHeader invSalesHeader, List<InvSalesDetailTemp> invSalesDetailTemp, List<InvProductSerialNoTemp> invProductSerialNoTemp, out string newDocumentNo, List<OtherExpenseTemp> otherExpenceTempList, List<PaymentTemp> paymentTempList, decimal paidAmount,decimal exchangeRate , string formName = "", bool isTStatus = false)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    AccTransactionTypeService accTransactionTypeDetailService = new Service.AccTransactionTypeService();

                    #region Sales Header Save

                    //if (invSalesHeader.DocumentStatus.Equals(1))
                    //{
                    //    InvSalesServices invSalesServices = new InvSalesServices();
                    //    LocationService locationService = new LocationService();
                    //    invSalesHeader.DocumentNo = invSalesServices.GetDocumentNo(formName, invSalesHeader.LocationID, locationService.GetLocationsByID(invSalesHeader.LocationID).LocationCode, invSalesHeader.DocumentID, false).Trim();
                    //}

                    newDocumentNo = invSalesHeader.DocumentNo;

                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;

                    if (invSalesHeader.InvSalesHeaderID.Equals(0))
                        context.InvSalesHeaders.Add(invSalesHeader);
                    else
                        context.Entry(invSalesHeader).State = EntityState.Modified;

                    context.SaveChanges();

                    ////context.InvPurchaseHeaders.Update(ph => ph.InvPurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && ph.LocationID.Equals(invPurchaseHeader.LocationID) && ph.DocumentID.Equals(invPurchaseHeader.DocumentID), phNew => new InvPurchaseHeader { PurchaseHeaderID = invPurchaseHeader.InvPurchaseHeaderID });

                    #endregion

                    #region Deleted
                    //List<InvPurchaseDetail> invPurchaseDetailDelete = new List<InvPurchaseDetail>();
                    //invPurchaseDetailDelete = context.InvPurchaseDetails.Where(p => p.PurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && p.LocationID.Equals(invPurchaseHeader.LocationID) && p.DocumentID.Equals(invPurchaseHeader.DocumentID)).ToList();

                    //foreach (InvPurchaseDetail invPurchaseDetailDeleteTemp in invPurchaseDetailDelete)
                    //{
                    //    context.InvPurchaseDetails.Remove(invPurchaseDetailDeleteTemp);
                    //}
                    #endregion

                   

                    //#region Sales Detail Save

                    //context.Set<InvSalesDetail>().Delete(context.InvSalesDetails.Where(p => p.InvSalesHeaderID.Equals(invSalesHeader.InvSalesHeaderID) && p.LocationID.Equals(invSalesHeader.LocationID) && p.DocumentID.Equals(invSalesHeader.DocumentID)).AsQueryable());
                    //context.SaveChanges();


                    //var invSalesDetailSaveQuery = (from pd in invSalesDetailTemp
                    //                               select new
                    //                               {
                    //                                   InvSalesDetailID = 0,
                    //                                   SalesDetailID = 0,
                    //                                   InvSalesHeaderID = invSalesHeader.InvSalesHeaderID,
                    //                                   CompanyID = invSalesHeader.CompanyID,
                    //                                   LocationID = invSalesHeader.LocationID,
                    //                                   CostCentreID = invSalesHeader.CostCentreID,
                    //                                   DocumentID = invSalesHeader.DocumentID,
                    //                                   LineNo = pd.LineNo,
                    //                                   ProductID = pd.ProductID,
                    //                                   Descrip = string.Empty,
                    //                                   BarCodeFull = 0,
                    //                                   UnitOfMeasureID = pd.UnitOfMeasureID,
                    //                                   BaseUnitID = pd.BaseUnitID,
                    //                                   ConvertFactor = pd.ConvertFactor,
                    //                                   BatchNo = pd.BatchNo,
                    //                                   ExpiryDate = pd.ExpiryDate,
                    //                                   SerialNo = 0,
                    //                                   OrderQty = 0,
                    //                                   Qty = pd.Qty,
                    //                                   FreeQty = pd.FreeQty,
                    //                                   CurrentQty = pd.CurrentQty,
                    //                                   IssuedQty = 0, //pd.DispatchQty,
                    //                                   BalanceQty = pd.Qty,
                    //                                   BalanceFreeQty = pd.FreeQty,
                    //                                   CostPrice = 0,
                    //                                   SellingPrice = pd.Rate,
                    //                                   AvgCost = pd.AverageCost,
                    //                                   IDI1 = 0,
                    //                                   IDis1 = 0,
                    //                                   IDiscount1 = 0,
                    //                                   IDI1CashierID = 0,
                    //                                   IDI2 = 0,
                    //                                   IDis2 = 0,
                    //                                   IDiscount2 = 0,
                    //                                   IDI2CashierID = 0,
                    //                                   IDI3 = 0,
                    //                                   IDis3 = 0,
                    //                                   IDiscount3 = 0,
                    //                                   IDI3CashierID = 0,
                    //                                   IDI4 = 0,
                    //                                   IDis4 = 0,
                    //                                   IDiscount4 = 0,
                    //                                   IDI4CashierID = 0,
                    //                                   IDI5 = 0,
                    //                                   IDis5 = 0,
                    //                                   IDiscount5 = 0,
                    //                                   IDI5CashierID = 0,
                    //                                   GrossAmount = pd.GrossAmount,
                    //                                   IsSDis = false,
                    //                                   SDNo = 0,
                    //                                   SDID = 0,
                    //                                   SDIs = 0,
                    //                                   SDiscount = 0,
                    //                                   DDisCashierID = 0,
                    //                                   BillTypeID = 0,
                    //                                   SaleTypeID = 0,
                    //                                   SalesmanID = 0,
                    //                                   CashierID = 0,
                    //                                   StartTime = invSalesHeader.StartTime,
                    //                                   EndTime = invSalesHeader.EndTime,
                    //                                   UnitNo = 0,
                    //                                   ShiftNo = 0,
                    //                                   IsRecall = false,
                    //                                   RecallNo = 0,
                    //                                   RecallAdv = false,
                    //                                   TaxAmount = 0,
                    //                                   IsTax = (invSalesHeader.TaxAmount > 0 ? true : false),
                    //                                   TaxPercentage = 0,
                    //                                   IsStock = false,
                    //                                   UpdateBy = 0,
                    //                                   Status = 1,
                    //                                   ZNo = 0,
                    //                                   CustomerType = 1,
                    //                                   DiscountPercentage = pd.DiscountPercentage,
                    //                                   DiscountAmount = pd.DiscountAmount,
                    //                                   SubTotalDiscount = pd.SubTotalDiscount,
                    //                                   TaxAmount1 = pd.TaxAmount1,
                    //                                   TaxAmount2 = pd.TaxAmount2,
                    //                                   TaxAmount3 = pd.TaxAmount3,
                    //                                   TaxAmount4 = pd.TaxAmount4,
                    //                                   TaxAmount5 = pd.TaxAmount5,
                    //                                   NetAmount = pd.NetAmount,
                    //                                   DispatchLocationID = pd.DispatchLocationID,
                    //                                   CurrencyID = 0,
                    //                                   CurrencyAmount = 0,
                    //                                   CurrencyRate = 0,
                    //                                   IsUpLoad = invSalesHeader.IsUpLoad,
                    //                                   DocumentStatus = invSalesHeader.DocumentStatus,
                    //                                   GroupOfCompanyID = invSalesHeader.GroupOfCompanyID,
                    //                                   CreatedUser = invSalesHeader.CreatedUser,
                    //                                   CreatedDate = invSalesHeader.CreatedDate,
                    //                                   ModifiedUser = invSalesHeader.ModifiedUser,
                    //                                   ModifiedDate = invSalesHeader.ModifiedDate,
                    //                                   DataTransfer = invSalesHeader.DataTransfer
                    //                               }).ToList();

                    //CommonService.BulkInsert(Common.LINQToDataTable(invSalesDetailSaveQuery), "InvSalesDetail");

                    ////var entityConn = context.Database.Connection as System.Data.EntityClient.EntityConnection;
                    ////var dbConn = context.Database.Connection as SqlConnection;

                    ////SqlBulkCopy bulkInsert = new SqlBulkCopy(dbConn);
                    ////bulkInsert.DestinationTableName = "InvPurchaseDetail";
                    ////dbConn.Open();
                    ////bulkInsert.WriteToServer(Common.LINQToDataTable(invPurchaseDetailSaveQuery));
                    ////dbConn.Close();


                    //// context.InvPurchaseDetails.Update(pd => pd.PurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && pd.LocationID.Equals(invPurchaseHeader.LocationID) && pd.DocumentID.Equals(invPurchaseHeader.DocumentID), pdNew => new InvPurchaseDetail { PurchaseDetailID = pdNew.InvPurchaseDetailID });

                    //// context.InvPurchaseDetails.Where(pd => pd.PurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && pd.LocationID.Equals(invPurchaseHeader.LocationID) && pd.DocumentID.Equals(invPurchaseHeader.DocumentID)).Update(pdNew => new InvPurchaseDetail() { PurchaseDetailID = pdNew.InvPurchaseDetailID+0 });


                    ////sql = "Update InvPurchaseDetail Set PurchaseDetailID = pdNew.InvPurchaseDetailID Where PurchaseHeaderID=@PurchaseHeaderID And LocationID=@LocationID And DocumentID=@DocumentID ";
                    ////var argsPurchaseDetail = new DbParameter[] 
                    ////{ 
                    ////    new System.Data.SqlClient.SqlParameter { ParameterName ="@PurchaseHeaderID", Value=invPurchaseHeader.InvPurchaseHeaderID},
                    ////    new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=invPurchaseHeader.LocationID},
                    ////    new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=invPurchaseHeader.DocumentID}
                    ////};

                    ////CommonService.ExecuteSql(sql, argsPurchaseDetail);


                    //if (invSalesHeader.DocumentStatus.Equals(1))
                    //{
                    //    //context.InvProductStockMasters.Update(ps => context.InvPurchaseDetails.Any(pd=> ps.LocationID.Equals(invPurchaseHeader.LocationID) && ps.ProductID.Equals(pd.ProductID), ps => new InvProductStockMaster { Stock = ps.Stock + (pd.Qty * ps.ConvertFactor) });


                    //    // context.InvProductStockMasters.Update(context.InvProductStockMasters.Join(context.InvPurchaseDetails, ps => ps.ProductID, pd => pd.ProductID,(ps,pd) => new (InvProductStockMaster=ps,InvPurchaseDetail=pd) {ps.Stock=(pd.Qty*pd.ConvertFactor)}));
                    //    //if (invPurchaseDetail.UnitOfMeasureID.Equals(invPurchaseDetail.BaseUnitID))
                    //    //    context.InvProductMasters.Update(pm => pm.InvProductMasterID.Equals(invPurchaseDetail.ProductID), pmNew => new InvProductMaster { CostPrice = invPurchaseDetail.CostPrice, SellingPrice = invPurchaseDetail.SellingPrice });
                    //    //else
                    //    //    context.InvProductUnitConversions.Update(uc => uc.ProductID.Equals(invPurchaseDetail.ProductID) && uc.UnitOfMeasureID.Equals(invPurchaseDetail.UnitOfMeasureID), ucNew => new InvProductUnitConversion { CostPrice = invPurchaseDetail.CostPrice, SellingPrice = invPurchaseDetail.SellingPrice });

                    //}

                    //#endregion

           
                    #region Payment Entries

                    AccPaymentService accPaymentService = new Service.AccPaymentService();

                    AccPaymentHeader accPaymentHeader = new AccPaymentHeader();
                    accPaymentHeader = accPaymentService.getAccPaymentHeader(invSalesHeader.InvSalesHeaderID, invSalesHeader.DocumentID, invSalesHeader.LocationID);
                    if (accPaymentHeader == null)
                        accPaymentHeader = new AccPaymentHeader();

                    accPaymentHeader.Amount = invSalesHeader.NetAmount;
                    accPaymentHeader.BalanceAmount = invSalesHeader.NetAmount - paidAmount;
                    accPaymentHeader.CompanyID = invSalesHeader.CompanyID;
                    accPaymentHeader.CostCentreID = invSalesHeader.CostCentreID;
                    accPaymentHeader.CreatedDate = invSalesHeader.CreatedDate;
                    accPaymentHeader.CreatedUser = invSalesHeader.CreatedUser;
                    accPaymentHeader.DocumentDate = invSalesHeader.DocumentDate;
                    accPaymentHeader.DocumentID = invSalesHeader.CompanyID;
                    accPaymentHeader.DocumentStatus = invSalesHeader.DocumentStatus;
                    accPaymentHeader.DocumentNo = invSalesHeader.DocumentNo;
                    accPaymentHeader.GroupOfCompanyID = invSalesHeader.GroupOfCompanyID;
                    accPaymentHeader.DataTransfer = invSalesHeader.DataTransfer;
                    accPaymentHeader.IsUpLoad = false;
                    accPaymentHeader.LocationID = invSalesHeader.LocationID;
                    accPaymentHeader.ModifiedDate = invSalesHeader.ModifiedDate;
                    accPaymentHeader.ModifiedUser = invSalesHeader.ModifiedUser;
                    accPaymentHeader.PaymentDate = invSalesHeader.DocumentDate;
                    accPaymentHeader.Reference = "INVOICE";
                    accPaymentHeader.ReferenceDocumentDocumentID = invSalesHeader.DocumentID;
                    accPaymentHeader.ReferenceDocumentID = invSalesHeader.InvSalesHeaderID;
                    accPaymentHeader.ReferenceID = invSalesHeader.DocumentID;
                    accPaymentHeader.ReferenceLocationID = invSalesHeader.LocationID;
                    accPaymentHeader.Remark = "INVOICE";

                    if (accPaymentHeader.AccPaymentHeaderID.Equals(0))
                        context.AccPaymentHeaders.Add(accPaymentHeader);
                    else
                        context.Entry(accPaymentHeader).State = EntityState.Modified;

                    context.SaveChanges();


                    context.Set<AccPaymentDetail>().Delete(context.AccPaymentDetails.Where(p => p.ReferenceDocumentID.Equals(invSalesHeader.InvSalesHeaderID) && p.LocationID.Equals(invSalesHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(invSalesHeader.DocumentID) && p.DocumentStatus.Equals(0)).AsQueryable());


                    if (paymentTempList != null)
                    {
                        var paymentDetailsSaveQuery = (from pt in paymentTempList
                                                       select new
                                                       {
                                                           AccPaymentDetailID = 0,
                                                           PaymentHeaderID = accPaymentHeader.AccPaymentHeaderID,
                                                           CompanyID = accPaymentHeader.CompanyID,
                                                           LocationID = accPaymentHeader.LocationID,
                                                           CostCentreID = accPaymentHeader.CostCentreID,
                                                           DocumentNo = accPaymentHeader.DocumentNo,
                                                           DocumentID = accPaymentHeader.DocumentID,
                                                           DocumentDate = accPaymentHeader.DocumentDate,
                                                           PaymentDate = accPaymentHeader.PaymentDate,
                                                           ReferenceDocumentID = accPaymentHeader.ReferenceDocumentID,
                                                           ReferenceDocumentDocumentID = accPaymentHeader.ReferenceDocumentDocumentID,
                                                           ReferenceLocationID = accPaymentHeader.ReferenceLocationID,
                                                           SetoffDocumentID = 0,
                                                           SetoffDocumentDocumentID = 0,
                                                           SetoffLocationID = 0,
                                                           ReferenceID = accPaymentHeader.ReferenceID,
                                                           Amount = pt.PayAmount,
                                                           paymentModeID = pt.PaymentMethodID,
                                                           BankID = pt.BankID,
                                                           BankBranchID = 0,
                                                           CardNo = pt.CardCheqNo,
                                                           ChequeDate = pt.ChequeDate,
                                                           DocumentStatus = accPaymentHeader.DocumentStatus,
                                                           IsTStatus = accPaymentHeader.IsUpLoad,
                                                           GroupOfCompanyID = accPaymentHeader.GroupOfCompanyID,
                                                           CreatedUser = accPaymentHeader.CreatedUser,
                                                           CreatedDate = accPaymentHeader.CreatedDate,
                                                           ModifiedUser = accPaymentHeader.ModifiedUser,
                                                           ModifiedDate = accPaymentHeader.ModifiedDate,
                                                           DataTransfer = accPaymentHeader.DataTransfer
                                                       }).ToList();


                        CommonService.BulkInsert(Common.LINQToDataTable(paymentDetailsSaveQuery), "AccPaymentDetail");
                    }

                    #endregion


                    context.AutoGenerateInfos.Update(phnew => new AutoGenerateInfo { ExchangeRate = exchangeRate });
                    #region Process

                    var parameter = new DbParameter[] 
                        { 
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@InvSalesHeaderID", Value=invSalesHeader.InvSalesHeaderID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=invSalesHeader.LocationID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=invSalesHeader.DocumentID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@InvReferenceDocumentHeaderID", Value=invSalesHeader.ReferenceDocumentID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@InvReferenceDocumentDocumentID", Value=invSalesHeader.ReferenceDocumentDocumentID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@RecallDocumentStatus", Value=invSalesHeader.RecallDocumentStatus},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@IsDispatch", Value=invSalesHeader.IsDispatch},

                            new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentStatus", Value=invSalesHeader.DocumentStatus}
                        };


                    if (CommonService.ExecuteStoredProcedure("spInvInvoiceSave", parameter))
                    {
                        transaction.Complete();
                        //CommonService.ExecuteSqlQuery("UPDATE dbo.AutoGenerateInfo SET ExchangeRate= " + exchangeRate + " ");
                        return true;
                    }
                    else
                    { return false; }

                    #endregion

                    //context.Acc();

                }

                
            }
           
        }


        #endregion


 

        #region Dispatch
        public InvSalesHeader getDispatchHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            { return context.InvSalesHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.IsDispatch.Equals(true) && ph.LocationID.Equals(locationID)).FirstOrDefault(); }
        }

        public InvSalesHeader GetPausedDispatchHeaderByDocumentNo(int documentID, string documentNo)
        {
            using (ERPDbContext context = new ERPDbContext())
            { return context.InvSalesHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo)).FirstOrDefault(); }
        }

        public InvSalesHeader GetPausedDispatchHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            { return context.InvSalesHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault(); }
        }
        
        public SalesOrderHeader GetSalesOrderHeaderToDispatch(string documentNo, int documentID)
        {
            using (ERPDbContext context = new ERPDbContext())
            { return context.SalesOrderHeaders.Where(ph => ph.DocumentNo.Equals(documentNo) && ph.DocumentID == documentID).FirstOrDefault(); }
        }

        public List<InvSalesDetailTemp> GetSalesOrderDetail(SalesOrderHeader salesOrderHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                InvSalesDetailTemp invSalesDetailTemp;

                List<InvSalesDetailTemp> invSalesDetailTempList = new List<InvSalesDetailTemp>();

                var salesOrderDetailTemp = (from qd in context.SalesOrderDetails 
                                            where qd.LocationID.Equals(salesOrderHeader.LocationID) && qd.CompanyID.Equals(salesOrderHeader.CompanyID) && qd.DocumentID.Equals(salesOrderHeader.DocumentID) && qd.SalesOrderHeaderID.Equals(salesOrderHeader.SalesOrderHeaderID)
                                            select new
                                            {
                                                qd.SalesOrderDetailID,
                                                qd.ConvertFactor,
                                                qd.CostPrice,
                                                PackSize=1,
                                                qd.DiscountAmount,
                                                qd.DiscountPercentage,
                                                qd.GrossAmount,
                                                qd.LineNo,
                                                qd.LocationID,
                                                qd.NetAmount,
                                                qd.OrderQty,
                                                qd.ProductCode,
                                                qd.ProductID,
                                                qd.ProductName,
                                                IsBatch=0,
                                                qd.SellingPrice,
                                                qd.SubTotalDiscount,
                                                qd.TaxAmount1,
                                                qd.TaxAmount2,
                                                qd.TaxAmount3,
                                                qd.TaxAmount4,
                                                qd.TaxAmount5,
                                                qd.UnitOfMeasureID,
                                                UnitOfMeasureName="",
                                                qd.BalanceQty

                                            }).ToArray();

                foreach (var tempProduct in salesOrderDetailTemp)
                {

                    invSalesDetailTemp = new InvSalesDetailTemp();

                    //invSalesDetailTemp.Rate = tempProduct.CostPrice;
                    //invSalesDetailTemp.PackSize = tempProduct.PackSize;
                    //invSalesDetailTemp.CurrentQty = tempProduct.CurrentQty;
                    invSalesDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                    invSalesDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                    invSalesDetailTemp.GrossAmount = tempProduct.GrossAmount;
                    invSalesDetailTemp.LineNo = tempProduct.LineNo;
                    invSalesDetailTemp.LocationID = tempProduct.LocationID;
                    invSalesDetailTemp.NetAmount = tempProduct.SellingPrice * tempProduct.BalanceQty;  // tempProduct.NetAmount;
                    invSalesDetailTemp.Qty = tempProduct.OrderQty;
                    invSalesDetailTemp.BalanceQty = tempProduct.BalanceQty;
                    invSalesDetailTemp.DispatchQty = 0; // tempProduct.BalanceQty; - modify 31-01-215
                    invSalesDetailTemp.IssuedQty = tempProduct.OrderQty - tempProduct.BalanceQty;
                    invSalesDetailTemp.ProductCode = tempProduct.ProductCode;
                    invSalesDetailTemp.ProductID = tempProduct.SalesOrderDetailID;
                    invSalesDetailTemp.ProductName = tempProduct.ProductName;
                    invSalesDetailTemp.IsBatch = false;
                    invSalesDetailTemp.Rate = tempProduct.SellingPrice;
                    invSalesDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                    invSalesDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                    invSalesDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                    invSalesDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                    invSalesDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                    invSalesDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                    invSalesDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invSalesDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                    invSalesDetailTempList.Add(invSalesDetailTemp);
                }
                return invSalesDetailTempList.OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public List<InvSalesDetailTemp> GetInvoceDetail(InvSalesHeader invSalesHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                InvSalesDetailTemp invSalesDetailTemp;


                List<InvSalesDetailTemp> invSalesDetailTempList = new List<InvSalesDetailTemp>();



                var salesOrderDetailTemp = (from qd in context.InvSalesDetails
                                            where qd.LocationID.Equals(invSalesHeader.LocationID) && qd.CompanyID.Equals(invSalesHeader.CompanyID) &&
                                            qd.DocumentID.Equals(invSalesHeader.DocumentID) && qd.InvSalesHeaderID.Equals(invSalesHeader.InvSalesHeaderID)
                                            select new
                                            {
                                                qd.ConvertFactor,
                                                qd.CostPrice,
                                                PackSize = 1,
                                                qd.DiscountAmount,
                                                qd.DiscountPercentage,
                                                qd.GrossAmount,
                                                qd.LineNo,
                                                qd.LocationID,
                                                qd.NetAmount,
                                                qd.OrderQty,
                                                qd.ProductCode,
                                                qd.ProductID,
                                                qd.ProductName,
                                                IsBatch = 0,
                                                qd.SellingPrice,
                                                qd.SubTotalDiscount,
                                                qd.TaxAmount1,
                                                qd.TaxAmount2,
                                                qd.TaxAmount3,
                                                qd.TaxAmount4,
                                                qd.TaxAmount5,
                                                qd.UnitOfMeasureID,
                                                UnitOfMeasureName = "",
                                                qd.BalanceQty

                                            }).ToArray();

                foreach (var tempProduct in salesOrderDetailTemp)
                {

                    invSalesDetailTemp = new InvSalesDetailTemp();

                    invSalesDetailTemp.Rate = tempProduct.CostPrice;
                    //invSalesDetailTemp.PackSize = tempProduct.PackSize;
                    //invSalesDetailTemp.CurrentQty = tempProduct.CurrentQty;
                    invSalesDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                    invSalesDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                    invSalesDetailTemp.GrossAmount = tempProduct.GrossAmount;
                    invSalesDetailTemp.LineNo = tempProduct.LineNo;
                    invSalesDetailTemp.LocationID = tempProduct.LocationID;
                    invSalesDetailTemp.NetAmount = tempProduct.NetAmount;
                    invSalesDetailTemp.Qty = tempProduct.OrderQty;
                    invSalesDetailTemp.BalanceQty = tempProduct.BalanceQty;
                    invSalesDetailTemp.DispatchQty = tempProduct.OrderQty - tempProduct.BalanceQty;
                    invSalesDetailTemp.IssuedQty =  tempProduct.BalanceQty;
                    invSalesDetailTemp.ProductCode = tempProduct.ProductCode;
                    invSalesDetailTemp.ProductID = tempProduct.ProductID;
                    invSalesDetailTemp.ProductName = tempProduct.ProductName;
                    invSalesDetailTemp.IsBatch = false;
                    invSalesDetailTemp.Rate = tempProduct.SellingPrice;
                    invSalesDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                    invSalesDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                    invSalesDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                    invSalesDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                    invSalesDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                    invSalesDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                    invSalesDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invSalesDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                    invSalesDetailTempList.Add(invSalesDetailTemp);
                }
                return invSalesDetailTempList.OrderBy(pd => pd.LineNo).ToList();
            }
        }



        public List<InvSalesDetailTemp> GetInvoceDetailEdit(InvSalesHeader invSalesHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                InvSalesDetailTemp invSalesDetailTemp;


                List<InvSalesDetailTemp> invSalesDetailTempList = new List<InvSalesDetailTemp>();



                var salesOrderDetailTemp = (from qd in context.InvSalesDetails
                                            where qd.LocationID.Equals(invSalesHeader.LocationID) && qd.CompanyID.Equals(invSalesHeader.CompanyID) &&
                                            qd.DocumentID.Equals(invSalesHeader.DocumentID) && qd.InvSalesHeaderID.Equals(invSalesHeader.InvSalesHeaderID)
                                            select new
                                            {
                                                qd.InvSalesDetailID,
                                                qd.ProductCode,
                                                qd.ProductName,
                                                qd.SellingPrice,
                                                qd.Qty,
                                                qd.Waight,
                                                qd.Height,
                                                qd.IsDeliver

                                            }).ToArray();

                foreach (var tempProduct in salesOrderDetailTemp)
                {

                    invSalesDetailTemp = new InvSalesDetailTemp();

                    //invSalesDetailTemp.PackSize = tempProduct.PackSize;
                    //invSalesDetailTemp.CurrentQty = tempProduct.CurrentQty;
                    invSalesDetailTemp.InvSalesDetailID = tempProduct.InvSalesDetailID;
                    invSalesDetailTemp.ProductCode = tempProduct.ProductCode;
                    invSalesDetailTemp.ProductName = tempProduct.ProductName;
                    invSalesDetailTemp.Rate = tempProduct.SellingPrice;
                    invSalesDetailTemp.Qty = tempProduct.Qty;
                    invSalesDetailTemp.Waight = tempProduct.Waight;
                    invSalesDetailTemp.Height = tempProduct.Height;
                    invSalesDetailTemp.IsDeliver = tempProduct.IsDeliver;
 

                    invSalesDetailTempList.Add(invSalesDetailTemp);
                }
                return invSalesDetailTempList.OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public string[] GetAllDocumentNumbersToDispatch(int documentID, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> documentNoList = new List<string>();

                var sql = (from ph in context.InvSalesHeaders
                           join pd in context.InvSalesDetails on ph.InvSalesHeaderID equals pd.InvSalesHeaderID
                           where ph.DocumentID == documentID && ph.LocationID == locationID && pd.BalanceQty != 0
                           group pd by new
                           {
                               ph.DocumentNo,
                               pd.BalanceQty,
                           } into poGroup

                           select new
                           {
                               DocumentNo = poGroup.Key.DocumentNo,
                               BalanceQty = poGroup.Sum(x => x.BalanceQty),
                           }).ToArray();

                foreach (var temp in sql)
                {
                    documentNoList = sql.Where(a => a.BalanceQty > 0).Select(a => a.DocumentNo).ToList();
                }

                return documentNoList.ToArray();
            }
        }

        public bool SaveDispatch(InvSalesHeader invSalesHeader, List<InvSalesDetailTemp> invSalesDetailTemp, List<InvProductSerialNoTemp> invProductSerialNoTemp, string OrderNo, out string newDocumentNo, string invoiceNo, int invoiceID, string formName = "", bool isTStatus = false)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    // AccTransactionTypeDetailService accTransactionTypeDetailService = new Service.AccTransactionTypeDetailService();
                    #region Sales Header Save

                    if (invSalesHeader.DocumentStatus.Equals(1))
                    {
                        InvSalesServices invSalesServices = new InvSalesServices();
                        LocationService locationService = new LocationService();
                        //invSalesHeader.DocumentNo = invSalesServices.GetDocumentNo(formName, invSalesHeader.LocationID, locationService.GetLocationsByID(invSalesHeader.LocationID).LocationCode, invSalesHeader.DocumentID, false).Trim();

                        if (OrderNo.ToString().Substring(0, 1) == "P")
                        {
                            invSalesHeader.DocumentNo = invSalesServices.GetDocumentNo(formName, invSalesHeader.LocationID, locationService.GetLocationsByID(invSalesHeader.LocationID).LocationCode, invSalesHeader.DocumentID, false).Trim();
                        }
                        else
                        {
                            invSalesHeader.DocumentNo = invSalesServices.GetDocumentNo(formName.ToString() + "V", invSalesHeader.LocationID, locationService.GetLocationsByID(invSalesHeader.LocationID).LocationCode, invSalesHeader.DocumentID, false).Trim();
                        }
                    }

                    newDocumentNo = invSalesHeader.DocumentNo;

                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;

                    if (invSalesHeader.InvSalesHeaderID.Equals(0))
                        context.InvSalesHeaders.Add(invSalesHeader);
                    else
                        context.Entry(invSalesHeader).State = EntityState.Modified;

                    context.SaveChanges();

                    ////context.InvPurchaseHeaders.Update(ph => ph.InvPurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && ph.LocationID.Equals(invPurchaseHeader.LocationID) && ph.DocumentID.Equals(invPurchaseHeader.DocumentID), phNew => new InvPurchaseHeader { PurchaseHeaderID = invPurchaseHeader.InvPurchaseHeaderID });

                    #endregion

                    #region Sales Detail Save

                    context.Set<InvSalesDetail>().Delete(context.InvSalesDetails.Where(p => p.InvSalesHeaderID.Equals(invSalesHeader.InvSalesHeaderID) && p.LocationID.Equals(invSalesHeader.LocationID) && p.DocumentID.Equals(invSalesHeader.DocumentID)).AsQueryable());
                    context.SaveChanges();

                    var invSalesDetailSaveQuery = (from pd in invSalesDetailTemp
                                                   select new
                                                   {
                                                       InvSalesDetailID = 0,
                                                       SalesDetailID = 0,
                                                       InvSalesHeaderID = invSalesHeader.InvSalesHeaderID,
                                                       CompanyID = invSalesHeader.CompanyID,
                                                       LocationID = invSalesHeader.LocationID,
                                                       CostCentreID = invSalesHeader.CostCentreID,
                                                       DocumentID = invSalesHeader.DocumentID,
                                                       LineNo = pd.LineNo,
                                                       ProductID = pd.ProductID,
                                                       Descrip = string.Empty,
                                                       BarCodeFull = 0,
                                                       UnitOfMeasureID = pd.UnitOfMeasureID,
                                                       BaseUnitID = pd.BaseUnitID,
                                                       ConvertFactor = pd.ConvertFactor,
                                                       BatchNo = pd.BatchNo,
                                                       ExpiryDate = pd.ExpiryDate,
                                                       SerialNo = 0,
                                                       OrderQty = pd.Qty,
                                                       Qty = pd.DispatchQty,
                                                       FreeQty = pd.FreeQty,
                                                       CurrentQty = pd.CurrentQty,
                                                       IssuedQty = pd.IssuedQty,
                                                       BalanceQty = pd.DispatchQty,
                                                       BalanceFreeQty = 0,
                                                       CostPrice = 0,
                                                       SellingPrice = pd.Rate,
                                                       AvgCost = pd.AverageCost,
                                                       IDI1 = 0,
                                                       IDis1 = 0,
                                                       IDiscount1 = 0,
                                                       IDI1CashierID = 0,
                                                       IDI2 = 0,
                                                       IDis2 = 0,
                                                       IDiscount2 = 0,
                                                       IDI2CashierID = 0,
                                                       IDI3 = 0,
                                                       IDis3 = 0,
                                                       IDiscount3 = 0,
                                                       IDI3CashierID = 0,
                                                       IDI4 = 0,
                                                       IDis4 = 0,
                                                       IDiscount4 = 0,
                                                       IDI4CashierID = 0,
                                                       IDI5 = 0,
                                                       IDis5 = 0,
                                                       IDiscount5 = 0,
                                                       IDI5CashierID = 0,
                                                       GrossAmount = pd.GrossAmount,
                                                       IsSDis = false,
                                                       SDNo = 0,
                                                       SDID = 0,
                                                       SDIs = 0,
                                                       SDiscount = 0,
                                                       DDisCashierID = 0,
                                                       BillTypeID = 0,
                                                       SaleTypeID = 0,
                                                       SalesmanID = 0,
                                                       CashierID = 0,
                                                       StartTime = invSalesHeader.StartTime,
                                                       EndTime = invSalesHeader.EndTime,
                                                       UnitNo = 0,
                                                       ShiftNo = 0,
                                                       IsRecall = false,
                                                       RecallNo = 0,
                                                       RecallAdv = false,
                                                       TaxAmount = 0,
                                                       IsTax = (invSalesHeader.TaxAmount > 0 ? true : false),
                                                       TaxPercentage = 0,
                                                       IsStock = false,
                                                       UpdateBy = 0,
                                                       Status = 1,
                                                       ZNo = 0,
                                                       CustomerType = 1,
                                                       DiscountPercentage = pd.DiscountPercentage,
                                                       DiscountAmount = pd.DiscountAmount,
                                                       SubTotalDiscount = pd.SubTotalDiscount,
                                                       TaxAmount1 = pd.TaxAmount1,
                                                       TaxAmount2 = pd.TaxAmount2,
                                                       TaxAmount3 = pd.TaxAmount3,
                                                       TaxAmount4 = pd.TaxAmount4,
                                                       TaxAmount5 = pd.TaxAmount5,
                                                       NetAmount = pd.NetAmount,
                                                       DispatchLocationID = pd.DispatchLocationID,
                                                       CurrencyID = 0,
                                                       CurrencyAmount = 0,
                                                       CurrencyRate = 0,
                                                       IsUpLoad = invSalesHeader.IsUpLoad,
                                                       DocumentStatus = invSalesHeader.DocumentStatus,
                                                       GroupOfCompanyID = invSalesHeader.GroupOfCompanyID,
                                                       CreatedUser = invSalesHeader.CreatedUser,
                                                       CreatedDate = invSalesHeader.CreatedDate,
                                                       ModifiedUser = invSalesHeader.ModifiedUser,
                                                       ModifiedDate = invSalesHeader.ModifiedDate,
                                                       DataTransfer = invSalesHeader.DataTransfer,
                                                       pd.ProductCode,
                                                       pd.ProductName,
                                                       pd.Size,
                                                       pd.Gauge                                                       
                                                   }).ToList();

                    //CommonService.BulkInsert(Common.LINQToDataTable(invSalesDetailSaveQuery), "InvSalesDetail");

                    DataTable dtx = new DataTable();
                    dtx = invSalesDetailSaveQuery.ToDataTable();

                    string mergeSql1 = CommonService.MergeSqlToForTrans(invSalesDetailSaveQuery.ToDataTable(), "InvSalesDetail");
                    CommonService.BulkInsert(invSalesDetailSaveQuery.ToDataTable(), "InvSalesDetail", mergeSql1);


                    #endregion


                    #region Serial No Detail Save


                    //context.Set<InvProductSerialNoDetail>().Delete(context.InvProductSerialNoDetails.Where(p => p.ReferenceDocumentID.Equals(invSalesHeader.InvSalesHeaderID) && p.LocationID.Equals(invSalesHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(invSalesHeader.DocumentID)).AsQueryable());
                    //context.SaveChanges();


                    //if (invSalesHeader.DocumentStatus.Equals(1))
                    //{
                    //    var invProductSerialNoSaveQuery = (from pd in invProductSerialNoTemp
                    //                                       select new
                    //                                       {
                    //                                           InvProductSerialNoID = 0,
                    //                                           ProductSerialNoID = 0,
                    //                                           GroupOfCompanyID = invSalesHeader.GroupOfCompanyID,
                    //                                           CompanyID = invSalesHeader.CompanyID,
                    //                                           LocationID = invSalesHeader.LocationID,
                    //                                           CostCentreID = invSalesHeader.CostCentreID,

                    //                                           ProductID = pd.ProductID,
                    //                                           BatchNo = invSalesHeader.DocumentNo,
                    //                                           UnitOfMeasureID = pd.UnitOfMeasureID,
                    //                                           ExpiryDate = pd.ExpiryDate,
                    //                                           SerialNo = pd.SerialNo,
                    //                                           SerialNoStatus = invSalesHeader.DocumentID,
                    //                                           DocumentStatus = invSalesHeader.DocumentStatus,
                    //                                           CreatedUser = invSalesHeader.CreatedUser,
                    //                                           CreatedDate = invSalesHeader.CreatedDate,
                    //                                           ModifiedUser = invSalesHeader.ModifiedUser,
                    //                                           ModifiedDate = invSalesHeader.ModifiedDate,
                    //                                           DataTransfer = invSalesHeader.DataTransfer
                    //                                       }).ToList();




                    //    CommonService.BulkInsert(Common.LINQToDataTable(invProductSerialNoSaveQuery), "InvProductSerialNo");

                    //}
                    //else
                    //{

                    //    var invProductSerialNoDetailSaveQuery = (from pd in invProductSerialNoTemp
                    //                                             select new
                    //                                             {
                    //                                                 InvProductSerialNoDetailID = 0,
                    //                                                 ProductSerialNoDetailID = 0,
                    //                                                 GroupOfCompanyID = invSalesHeader.GroupOfCompanyID,
                    //                                                 CompanyID = invSalesHeader.CompanyID,
                    //                                                 LocationID = invSalesHeader.LocationID,
                    //                                                 CostCentreID = invSalesHeader.CostCentreID,
                    //                                                 LineNo = pd.LineNo,
                    //                                                 ProductID = pd.ProductID,
                    //                                                 UnitOfMeasureID = pd.UnitOfMeasureID,
                    //                                                 ExpiryDate = pd.ExpiryDate,
                    //                                                 ReferenceDocumentDocumentID = invSalesHeader.DocumentID,
                    //                                                 ReferenceDocumentID = invSalesHeader.InvSalesHeaderID,

                    //                                                 SerialNo = pd.SerialNo,

                    //                                                 DocumentStatus = invSalesHeader.DocumentStatus,

                    //                                                 CreatedUser = invSalesHeader.CreatedUser,
                    //                                                 CreatedDate = invSalesHeader.CreatedDate,
                    //                                                 ModifiedUser = invSalesHeader.ModifiedUser,
                    //                                                 ModifiedDate = invSalesHeader.ModifiedDate,
                    //                                                 DataTransfer = invSalesHeader.DataTransfer
                    //                                             }).ToList();


                    //    CommonService.BulkInsert(Common.LINQToDataTable(invProductSerialNoDetailSaveQuery), "InvProductSerialNoDetail");
                    //}


                    #endregion

                    #region Process

                    var parameter = new DbParameter[] 
                        { 
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@InvDispatchNoteHeaderID", Value=invSalesHeader.InvSalesHeaderID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=invSalesHeader.LocationID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=invSalesHeader.DocumentID},
                            //new System.Data.SqlClient.SqlParameter { ParameterName ="@InvoiceNo", Value=invoiceNo},
                            //new System.Data.SqlClient.SqlParameter { ParameterName ="@InvoiceId", Value=invoiceID},

                            new System.Data.SqlClient.SqlParameter { ParameterName ="@InvReferenceDocumentHeaderID", Value=invSalesHeader.ReferenceDocumentID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@InvReferenceDocumentDocumentID", Value=invSalesHeader.ReferenceDocumentDocumentID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@RecallDocumentStatus", Value=invSalesHeader.RecallDocumentStatus},

                            new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentStatus", Value=invSalesHeader.DocumentStatus}
                        };

                    if (CommonService.ExecuteStoredProcedure("[spInvDispatchNoteSave]", parameter))
                    {
                        transaction.Complete();

                        return true;
                    }
                    else
                    { return false; }

                    #endregion



                    //context.Acc();

                }
            }
        }
        
        #endregion

        #region Sales Return
        public bool SaveSalesReturn(InvSalesHeader invSalesHeader, List<InvSalesDetailTemp> invSalesDetailTemp, List<InvProductSerialNoTemp> invProductSerialNoTemp, out string newDocumentNo, List<OtherExpenseTemp> otherExpenceTempList, List<PaymentTemp> paymentTempList, decimal paidAmount, string formName = "", bool isTStatus = false)
        {
            try
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    using (TransactionScope transaction = new TransactionScope())
                    {
                        AccTransactionTypeService accTransactionTypeDetailService = new Service.AccTransactionTypeService();


                        #region Sales Header Save

                        if (invSalesHeader.DocumentStatus.Equals(1))
                        {
                            InvSalesServices invSalesServices = new InvSalesServices();
                            LocationService locationService = new LocationService();
                            invSalesHeader.DocumentNo = invSalesServices.GetDocumentNo(formName, invSalesHeader.LocationID, locationService.GetLocationsByID(invSalesHeader.LocationID).LocationCode, invSalesHeader.DocumentID, false).Trim();

                        }

                        newDocumentNo = invSalesHeader.DocumentNo;

                        context.Configuration.AutoDetectChangesEnabled = false;
                        context.Configuration.ValidateOnSaveEnabled = false;

                        if (invSalesHeader.InvSalesHeaderID.Equals(0))
                            context.InvSalesHeaders.Add(invSalesHeader);
                        else
                            context.Entry(invSalesHeader).State = EntityState.Modified;

                        context.SaveChanges();

                        ////context.InvPurchaseHeaders.Update(ph => ph.InvPurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && ph.LocationID.Equals(invPurchaseHeader.LocationID) && ph.DocumentID.Equals(invPurchaseHeader.DocumentID), phNew => new InvPurchaseHeader { PurchaseHeaderID = invPurchaseHeader.InvPurchaseHeaderID });

                        #endregion

                        #region Deleted
                        //List<InvPurchaseDetail> invPurchaseDetailDelete = new List<InvPurchaseDetail>();
                        //invPurchaseDetailDelete = context.InvPurchaseDetails.Where(p => p.PurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && p.LocationID.Equals(invPurchaseHeader.LocationID) && p.DocumentID.Equals(invPurchaseHeader.DocumentID)).ToList();

                        //foreach (InvPurchaseDetail invPurchaseDetailDeleteTemp in invPurchaseDetailDelete)
                        //{
                        //    context.InvPurchaseDetails.Remove(invPurchaseDetailDeleteTemp);
                        //}
                        #endregion


                        #region Sales Detail Save

                        context.Set<InvSalesDetail>().Delete(context.InvSalesDetails.Where(p => p.InvSalesHeaderID.Equals(invSalesHeader.InvSalesHeaderID) && p.LocationID.Equals(invSalesHeader.LocationID) && p.DocumentID.Equals(invSalesHeader.DocumentID)).AsQueryable());
                        context.SaveChanges();

                        #region UsingSQL


                        //string sql = "DELETE FROM InvPurchaseDetail WHERE PurchaseHeaderID=@PurchaseHeaderID AND LocationID=@LocationID AND DocumentID=@DocumentID AND DocumentStatus=0";
                        //var args = new DbParameter[] 
                        //{ 
                        //    new SqlParameter { ParameterName ="@PurchaseHeaderID", Value=invPurchaseHeader.InvPurchaseHeaderID},
                        //    new SqlParameter { ParameterName ="@LocationID", Value=invPurchaseHeader.LocationID},
                        //    new SqlParameter { ParameterName ="@DocumentID", Value=invPurchaseHeader.DocumentID}
                        //};

                        //CommonService.ExecuteSql(sql, args);

                        #endregion

                        #region Deleted

                        //List<InvProductSerialNoDetail> invProductSerialNoDetailDelete = new List<InvProductSerialNoDetail>();
                        //invProductSerialNoDetailDelete = context.InvProductSerialNoDetails.Where(p => p.ReferenceDocumentID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && p.LocationID.Equals(invPurchaseHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(invPurchaseHeader.DocumentID)).ToList();



                        //foreach (InvProductSerialNoDetail invProductSerialNoDetailDeleteTemp in invProductSerialNoDetailDelete)
                        //{
                        //    context.InvProductSerialNoDetails.Remove(invProductSerialNoDetailDeleteTemp);
                        //}

                        //context.SaveChanges();

                        #endregion


                        var invSalesDetailSaveQuery = (from pd in invSalesDetailTemp
                                                       select new
                                                       {
                                                           InvSalesDetailID = 0,
                                                           SalesDetailID = 0,
                                                           InvSalesHeaderID = invSalesHeader.InvSalesHeaderID,
                                                           CompanyID = invSalesHeader.CompanyID,
                                                           LocationID = invSalesHeader.LocationID,
                                                           CostCentreID = invSalesHeader.CostCentreID,
                                                           DocumentID = invSalesHeader.DocumentID,
                                                           LineNo = pd.LineNo,
                                                           ProductID = pd.ProductID,
                                                           Descrip = string.Empty,
                                                           BarCodeFull = 0,
                                                           UnitOfMeasureID = pd.UnitOfMeasureID,
                                                           BaseUnitID = pd.BaseUnitID,
                                                           ConvertFactor = pd.ConvertFactor,
                                                           BatchNo = pd.BatchNo,
                                                           ExpiryDate = pd.ExpiryDate,
                                                           SerialNo = 0,
                                                           OrderQty = 0,
                                                           Qty = pd.Qty,
                                                           FreeQty = pd.FreeQty,
                                                           CurrentQty = pd.CurrentQty,
                                                           IssuedQty = 0,
                                                           BalanceQty = pd.Qty,
                                                           BalanceFreeQty = pd.FreeQty,
                                                           CostPrice = 0,
                                                           SellingPrice = pd.Rate,
                                                           AvgCost = pd.AverageCost,
                                                           IDI1 = 0,
                                                           IDis1 = 0,
                                                           IDiscount1 = 0,
                                                           IDI1CashierID = 0,
                                                           IDI2 = 0,
                                                           IDis2 = 0,
                                                           IDiscount2 = 0,
                                                           IDI2CashierID = 0,
                                                           IDI3 = 0,
                                                           IDis3 = 0,
                                                           IDiscount3 = 0,
                                                           IDI3CashierID = 0,
                                                           IDI4 = 0,
                                                           IDis4 = 0,
                                                           IDiscount4 = 0,
                                                           IDI4CashierID = 0,
                                                           IDI5 = 0,
                                                           IDis5 = 0,
                                                           IDiscount5 = 0,
                                                           IDI5CashierID = 0,
                                                           GrossAmount = pd.GrossAmount,
                                                           IsSDis = false,
                                                           SDNo = 0,
                                                           SDID = 0,
                                                           SDIs = 0,
                                                           SDiscount = 0,
                                                           DDisCashierID = 0,
                                                           BillTypeID = 0,
                                                           SaleTypeID = 0,
                                                           SalesmanID = 0,
                                                           CashierID = 0,
                                                           StartTime = invSalesHeader.StartTime,
                                                           EndTime = invSalesHeader.EndTime,
                                                           UnitNo = 0,
                                                           ShiftNo = 0,
                                                           IsRecall = false,
                                                           RecallNo = 0,
                                                           RecallAdv = false,
                                                           TaxAmount = 0,
                                                           IsTax = (invSalesHeader.TaxAmount > 0 ? true : false),
                                                           TaxPercentage = 0,
                                                           IsStock = false,
                                                           UpdateBy = 0,
                                                           Status = 1,
                                                           ZNo = 0,
                                                           CustomerType = 1,
                                                           DiscountPercentage = pd.DiscountPercentage,
                                                           DiscountAmount = pd.DiscountAmount,
                                                           SubTotalDiscount = pd.SubTotalDiscount,
                                                           TaxAmount1 = pd.TaxAmount1,
                                                           TaxAmount2 = pd.TaxAmount2,
                                                           TaxAmount3 = pd.TaxAmount3,
                                                           TaxAmount4 = pd.TaxAmount4,
                                                           TaxAmount5 = pd.TaxAmount5,
                                                           NetAmount = pd.NetAmount,
                                                           DispatchLocationID = pd.DispatchLocationID,
                                                           CurrencyID = 0,
                                                           CurrencyAmount = 0,
                                                           CurrencyRate = 0,
                                                           IsUpLoad = invSalesHeader.IsUpLoad,
                                                           DocumentStatus = invSalesHeader.DocumentStatus,
                                                           GroupOfCompanyID = invSalesHeader.GroupOfCompanyID,
                                                           CreatedUser = invSalesHeader.CreatedUser,
                                                           CreatedDate = invSalesHeader.CreatedDate,
                                                           ModifiedUser = invSalesHeader.ModifiedUser,
                                                           ModifiedDate = invSalesHeader.ModifiedDate,
                                                           DataTransfer = invSalesHeader.DataTransfer
                                                       }).ToList();

                        //CommonService.BulkInsert(Common.LINQToDataTable(invSalesDetailSaveQuery), "InvSalesDetail");
                        CommonService.BulkInsert(invSalesDetailSaveQuery.ToDataTable(), "InvSalesDetail");

                        //var entityConn = context.Database.Connection as System.Data.EntityClient.EntityConnection;
                        //var dbConn = context.Database.Connection as SqlConnection;

                        //SqlBulkCopy bulkInsert = new SqlBulkCopy(dbConn);
                        //bulkInsert.DestinationTableName = "InvPurchaseDetail";
                        //dbConn.Open();
                        //bulkInsert.WriteToServer(Common.LINQToDataTable(invPurchaseDetailSaveQuery));
                        //dbConn.Close();


                        // context.InvPurchaseDetails.Update(pd => pd.PurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && pd.LocationID.Equals(invPurchaseHeader.LocationID) && pd.DocumentID.Equals(invPurchaseHeader.DocumentID), pdNew => new InvPurchaseDetail { PurchaseDetailID = pdNew.InvPurchaseDetailID });

                        // context.InvPurchaseDetails.Where(pd => pd.PurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && pd.LocationID.Equals(invPurchaseHeader.LocationID) && pd.DocumentID.Equals(invPurchaseHeader.DocumentID)).Update(pdNew => new InvPurchaseDetail() { PurchaseDetailID = pdNew.InvPurchaseDetailID+0 });


                        //sql = "Update InvPurchaseDetail Set PurchaseDetailID = pdNew.InvPurchaseDetailID Where PurchaseHeaderID=@PurchaseHeaderID And LocationID=@LocationID And DocumentID=@DocumentID ";
                        //var argsPurchaseDetail = new DbParameter[] 
                        //{ 
                        //    new System.Data.SqlClient.SqlParameter { ParameterName ="@PurchaseHeaderID", Value=invPurchaseHeader.InvPurchaseHeaderID},
                        //    new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=invPurchaseHeader.LocationID},
                        //    new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=invPurchaseHeader.DocumentID}
                        //};

                        //CommonService.ExecuteSql(sql, argsPurchaseDetail);


                        if (invSalesHeader.DocumentStatus.Equals(1))
                        {
                            //context.InvProductStockMasters.Update(ps => context.InvPurchaseDetails.Any(pd=> ps.LocationID.Equals(invPurchaseHeader.LocationID) && ps.ProductID.Equals(pd.ProductID), ps => new InvProductStockMaster { Stock = ps.Stock + (pd.Qty * ps.ConvertFactor) });


                            // context.InvProductStockMasters.Update(context.InvProductStockMasters.Join(context.InvPurchaseDetails, ps => ps.ProductID, pd => pd.ProductID,(ps,pd) => new (InvProductStockMaster=ps,InvPurchaseDetail=pd) {ps.Stock=(pd.Qty*pd.ConvertFactor)}));
                            //if (invPurchaseDetail.UnitOfMeasureID.Equals(invPurchaseDetail.BaseUnitID))
                            //    context.InvProductMasters.Update(pm => pm.InvProductMasterID.Equals(invPurchaseDetail.ProductID), pmNew => new InvProductMaster { CostPrice = invPurchaseDetail.CostPrice, SellingPrice = invPurchaseDetail.SellingPrice });
                            //else
                            //    context.InvProductUnitConversions.Update(uc => uc.ProductID.Equals(invPurchaseDetail.ProductID) && uc.UnitOfMeasureID.Equals(invPurchaseDetail.UnitOfMeasureID), ucNew => new InvProductUnitConversion { CostPrice = invPurchaseDetail.CostPrice, SellingPrice = invPurchaseDetail.SellingPrice });

                        }

                        #endregion

                        #region Deleted

                        //foreach (InvPurchaseDetailTemp invPurchaseDetail in invPurchaseDetailTemp)
                        //{
                        //    InvPurchaseDetail invPurchaseDetailSave = new InvPurchaseDetail();


                        //    invPurchaseDetailSave.AvgCost = invPurchaseDetail.AverageCost;
                        //    invPurchaseDetailSave.CompanyID = invPurchaseHeader.CompanyID;
                        //    invPurchaseDetailSave.ConvertFactor = invPurchaseDetail.ConvertFactor;
                        //    invPurchaseDetailSave.CostCentreID = invPurchaseHeader.CostCentreID;
                        //    invPurchaseDetailSave.CostPrice = invPurchaseDetail.CostPrice;
                        //    invPurchaseDetailSave.CurrentQty = invPurchaseDetail.CurrentQty;
                        //    invPurchaseDetailSave.DiscountAmount = invPurchaseDetail.DiscountAmount;
                        //    invPurchaseDetailSave.DiscountPercentage = invPurchaseDetail.DiscountPercentage;
                        //    invPurchaseDetailSave.DocumentID = invPurchaseHeader.DocumentID;
                        //    invPurchaseDetailSave.DocumentStatus = invPurchaseHeader.DocumentStatus;
                        //    invPurchaseDetailSave.ExpiryDate = invPurchaseDetail.ExpiryDate;
                        //    invPurchaseDetailSave.FreeQty = invPurchaseDetail.FreeQty;
                        //    invPurchaseDetailSave.GrossAmount = invPurchaseDetail.GrossAmount;
                        //    invPurchaseDetailSave.GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID;
                        //    invPurchaseDetailSave.LineNo = invPurchaseDetail.LineNo;
                        //    invPurchaseDetailSave.LocationID = invPurchaseHeader.LocationID;
                        //    invPurchaseDetailSave.NetAmount = invPurchaseDetail.NetAmount;
                        //    invPurchaseDetailSave.OrderQty = invPurchaseDetail.OrderQty;
                        //    invPurchaseDetailSave.ProductID = invPurchaseDetail.ProductID;
                        //    invPurchaseDetailSave.PurchaseHeaderID = invPurchaseHeader.InvPurchaseHeaderID;
                        //    invPurchaseDetailSave.Qty = invPurchaseDetail.Qty;
                        //    invPurchaseDetailSave.BalanceQty = invPurchaseDetail.Qty;
                        //    invPurchaseDetailSave.SellingPrice = invPurchaseDetail.SellingPrice;
                        //    invPurchaseDetailSave.SubTotalDiscount = invPurchaseDetail.SubTotalDiscount;
                        //    invPurchaseDetailSave.TaxAmount1 = invPurchaseDetail.TaxAmount1;
                        //    invPurchaseDetailSave.TaxAmount2 = invPurchaseDetail.TaxAmount2;
                        //    invPurchaseDetailSave.TaxAmount3 = invPurchaseDetail.TaxAmount3;
                        //    invPurchaseDetailSave.TaxAmount4 = invPurchaseDetail.TaxAmount4;
                        //    invPurchaseDetailSave.TaxAmount5 = invPurchaseDetail.TaxAmount5;
                        //    invPurchaseDetailSave.UnitOfMeasureID = invPurchaseDetail.UnitOfMeasureID;
                        //    invPurchaseDetailSave.BaseUnitID = invPurchaseDetail.BaseUnitID;


                        //    //if (invPurchaseHeader.DocumentStatus.Equals(1))
                        //    //{
                        //    //    context.InvProductStockMasters.Update(ps => ps.LocationID.Equals(invPurchaseHeader.LocationID) && ps.ProductID.Equals(invPurchaseDetail.ProductID), ps => new InvProductStockMaster { Stock = ps.Stock + (invPurchaseDetail.Qty * invPurchaseDetail.ConvertFactor) });

                        //    //    if (invPurchaseDetail.UnitOfMeasureID.Equals(invPurchaseDetail.BaseUnitID))
                        //    //        context.InvProductMasters.Update(pm => pm.InvProductMasterID.Equals(invPurchaseDetail.ProductID), pmNew => new InvProductMaster { CostPrice = invPurchaseDetail.CostPrice, SellingPrice = invPurchaseDetail.SellingPrice });
                        //    //    else
                        //    //        context.InvProductUnitConversions.Update(uc => uc.ProductID.Equals(invPurchaseDetail.ProductID) && uc.UnitOfMeasureID.Equals(invPurchaseDetail.UnitOfMeasureID), ucNew => new InvProductUnitConversion { CostPrice = invPurchaseDetail.CostPrice, SellingPrice = invPurchaseDetail.SellingPrice });

                        //    //}
                        //    context.InvPurchaseDetails.Add(invPurchaseDetailSave);

                        //    //context.SaveChanges();
                        //    //context.InvPurchaseDetails.Update(pd => pd.PurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && pd.LocationID.Equals(invPurchaseHeader.LocationID) && pd.DocumentID.Equals(invPurchaseHeader.DocumentID) && pd.InvPurchaseDetailID.Equals(invPurchaseDetailSave.InvPurchaseDetailID), pdNew => new InvPurchaseDetail { PurchaseDetailID = invPurchaseDetailSave.InvPurchaseDetailID });


                        //}
                        #endregion


                        #region Serial No Detail Save


                        context.Set<InvProductSerialNoDetail>().Delete(context.InvProductSerialNoDetails.Where(p => p.ReferenceDocumentID.Equals(invSalesHeader.InvSalesHeaderID) && p.LocationID.Equals(invSalesHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(invSalesHeader.DocumentID)).AsQueryable());
                        context.SaveChanges();


                        if (invSalesHeader.DocumentStatus.Equals(1))
                        {
                            var invProductSerialNoSaveQuery = (from pd in invProductSerialNoTemp
                                                               select new
                                                               {
                                                                   InvProductSerialNoID = 0,
                                                                   ProductSerialNoID = 0,
                                                                   GroupOfCompanyID = invSalesHeader.GroupOfCompanyID,
                                                                   CompanyID = invSalesHeader.CompanyID,
                                                                   LocationID = invSalesHeader.LocationID,
                                                                   CostCentreID = invSalesHeader.CostCentreID,
                                                                   ProductID = pd.ProductID,
                                                                   BatchNo = invSalesHeader.DocumentNo,
                                                                   UnitOfMeasureID = pd.UnitOfMeasureID,
                                                                   ExpiryDate = pd.ExpiryDate,
                                                                   SerialNo = pd.SerialNo,
                                                                   SerialNoStatus = invSalesHeader.DocumentID,
                                                                   DocumentStatus = invSalesHeader.DocumentStatus,
                                                                   CreatedUser = invSalesHeader.CreatedUser,
                                                                   CreatedDate = invSalesHeader.CreatedDate,
                                                                   ModifiedUser = invSalesHeader.ModifiedUser,
                                                                   ModifiedDate = invSalesHeader.ModifiedDate,
                                                                   DataTransfer = invSalesHeader.DataTransfer
                                                               }).ToList();




                            CommonService.BulkInsert(Common.LINQToDataTable(invProductSerialNoSaveQuery), "InvProductSerialNo");

                        }
                        else
                        {

                            var invProductSerialNoDetailSaveQuery = (from pd in invProductSerialNoTemp
                                                                     select new
                                                                     {
                                                                         InvProductSerialNoDetailID = 0,
                                                                         ProductSerialNoDetailID = 0,
                                                                         GroupOfCompanyID = invSalesHeader.GroupOfCompanyID,
                                                                         CompanyID = invSalesHeader.CompanyID,
                                                                         LocationID = invSalesHeader.LocationID,
                                                                         CostCentreID = invSalesHeader.CostCentreID,
                                                                         LineNo = pd.LineNo,
                                                                         ProductID = pd.ProductID,
                                                                         UnitOfMeasureID = pd.UnitOfMeasureID,
                                                                         ExpiryDate = pd.ExpiryDate,
                                                                         ReferenceDocumentDocumentID = invSalesHeader.DocumentID,
                                                                         ReferenceDocumentID = invSalesHeader.InvSalesHeaderID,

                                                                         SerialNo = pd.SerialNo,

                                                                         DocumentStatus = invSalesHeader.DocumentStatus,

                                                                         CreatedUser = invSalesHeader.CreatedUser,
                                                                         CreatedDate = invSalesHeader.CreatedDate,
                                                                         ModifiedUser = invSalesHeader.ModifiedUser,
                                                                         ModifiedDate = invSalesHeader.ModifiedDate,
                                                                         DataTransfer = invSalesHeader.DataTransfer
                                                                     }).ToList();


                            CommonService.BulkInsert(Common.LINQToDataTable(invProductSerialNoDetailSaveQuery), "InvProductSerialNoDetail");
                        }
                        #region Deleted


                        // foreach (InvProductSerialNoTemp invPurchaseDetailSerialNo in invProductSerialNoTemp)
                        // {
                        //InvProductSerialNoDetail invProductSerialNoDetailSave = new InvProductSerialNoDetail();

                        //invProductSerialNoDetailSave.ReferenceDocumentID = invPurchaseHeader.InvPurchaseHeaderID;
                        //invProductSerialNoDetailSave.ReferenceDocumentDocumentID = invPurchaseHeader.DocumentID;
                        //if (invPurchaseHeader.DocumentStatus.Equals(1))
                        //{
                        //    InvProductSerialNo invProductSerialNoSave = new InvProductSerialNo();

                        //    invProductSerialNoSave.CompanyID = invPurchaseHeader.CompanyID;
                        //    invProductSerialNoSave.CostCentreID = invPurchaseHeader.CostCentreID;
                        //    invProductSerialNoSave.DocumentStatus = invPurchaseHeader.DocumentStatus;
                        //    invProductSerialNoSave.GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID;
                        //    invProductSerialNoSave.LocationID = invPurchaseHeader.LocationID;
                        //    invProductSerialNoSave.ProductID = invPurchaseDetailSerialNo.ProductID;
                        //    invProductSerialNoSave.UnitOfMeasureID = invPurchaseDetailSerialNo.UnitOfMeasureID;
                        //    invProductSerialNoSave.ExpiryDate = invPurchaseDetailSerialNo.ExpiryDate;
                        //    invProductSerialNoSave.SerialNo = invPurchaseDetailSerialNo.SerialNo;
                        //    invProductSerialNoSave.SerialNoStatus = 1;


                        //    context.InvProductSerialNos.Add(invProductSerialNoSave);
                        //}


                        //invProductSerialNoDetailSave.CompanyID = invPurchaseHeader.CompanyID;
                        //invProductSerialNoDetailSave.CostCentreID = invPurchaseHeader.CostCentreID;
                        //invProductSerialNoDetailSave.DocumentStatus = invPurchaseHeader.DocumentStatus;
                        //invProductSerialNoDetailSave.GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID;
                        //invProductSerialNoDetailSave.LocationID = invPurchaseHeader.LocationID;
                        //invProductSerialNoDetailSave.ProductID = invPurchaseDetailSerialNo.ProductID;
                        //invProductSerialNoDetailSave.LineNo = invPurchaseDetailSerialNo.LineNo;
                        //invProductSerialNoDetailSave.UnitOfMeasureID = invPurchaseDetailSerialNo.UnitOfMeasureID;
                        //invProductSerialNoDetailSave.ExpiryDate = invPurchaseDetailSerialNo.ExpiryDate;
                        //invProductSerialNoDetailSave.SerialNo = invPurchaseDetailSerialNo.SerialNo;
                        //invProductSerialNoDetailSave.ReferenceDocumentID = invPurchaseHeader.InvPurchaseHeaderID;
                        //invProductSerialNoDetailSave.ReferenceDocumentDocumentID = invPurchaseHeader.DocumentID;

                        //context.InvProductSerialNoDetails.Add(invProductSerialNoDetailSave);
                        //context.SaveChanges();

                        //context.InvProductSerialNoDetails.Update(pd => pd.ReferenceDocumentID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && pd.LocationID.Equals(invPurchaseHeader.LocationID) && pd.ReferenceDocumentDocumentID.Equals(invPurchaseHeader.DocumentID) && pd.InvProductSerialNoDetailID.Equals(invProductSerialNoDetailSave.InvProductSerialNoDetailID), pdNew => new InvProductSerialNoDetail { ProductSerialNoDetailID = invProductSerialNoDetailSave.InvProductSerialNoDetailID });

                        // }
                        #endregion




                        #endregion


                        #region Account Entries




                        AccGlTransactionService accGlTransactionService = new AccGlTransactionService();



                        AccGlTransactionHeader accGlTransactionHeader = new AccGlTransactionHeader();
                        accGlTransactionHeader = accGlTransactionService.getAccGlTransactionHeader(invSalesHeader.InvSalesHeaderID, invSalesHeader.DocumentID, invSalesHeader.LocationID);
                        if (accGlTransactionHeader == null)
                            accGlTransactionHeader = new AccGlTransactionHeader();

                        accGlTransactionHeader.Amount = invSalesHeader.NetAmount;
                        accGlTransactionHeader.CompanyID = invSalesHeader.CompanyID;
                        accGlTransactionHeader.CostCentreID = invSalesHeader.CostCentreID;
                        accGlTransactionHeader.CreatedDate = invSalesHeader.CreatedDate;
                        accGlTransactionHeader.CreatedUser = invSalesHeader.CreatedUser;
                        accGlTransactionHeader.DocumentDate = invSalesHeader.DocumentDate;
                        accGlTransactionHeader.DocumentID = invSalesHeader.DocumentID;
                        accGlTransactionHeader.DocumentStatus = invSalesHeader.DocumentStatus;
                        accGlTransactionHeader.DocumentNo = string.Empty;
                        accGlTransactionHeader.GroupOfCompanyID = invSalesHeader.GroupOfCompanyID;
                        accGlTransactionHeader.DataTransfer = invSalesHeader.DataTransfer;
                        accGlTransactionHeader.IsUpLoad = false;
                        accGlTransactionHeader.LocationID = invSalesHeader.LocationID;
                        accGlTransactionHeader.ModifiedDate = invSalesHeader.ModifiedDate;
                        accGlTransactionHeader.ModifiedUser = invSalesHeader.ModifiedUser;
                        accGlTransactionHeader.ReferenceDocumentDocumentID = invSalesHeader.DocumentID;
                        accGlTransactionHeader.ReferenceDocumentID = invSalesHeader.InvSalesHeaderID;
                        accGlTransactionHeader.ReferenceDocumentNo = invSalesHeader.DocumentNo;
                        accGlTransactionHeader.ReferenceID = invSalesHeader.CustomerID;


                        if (accGlTransactionHeader.AccGlTransactionHeaderID.Equals(0))
                            accGlTransactionService.AddAccGlTransactionHeader(accGlTransactionHeader);
                        else
                            accGlTransactionService.UpdateAccGlTransactionHeader(accGlTransactionHeader);

                        context.SaveChanges();


                        context.Set<AccGlTransactionDetail>().Delete(context.AccGlTransactionDetails.Where(p => p.ReferenceDocumentID.Equals(invSalesHeader.InvSalesHeaderID) && p.LocationID.Equals(invSalesHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(invSalesHeader.DocumentID) && p.DocumentStatus.Equals(0)).AsQueryable());

                        if (otherExpenceTempList != null)
                        {

                            var otherExpenceSaveQuery = (from oe in otherExpenceTempList
                                                         select new
                                                         {
                                                             AccGlTransactionDetailID = 0,
                                                             AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                                             CompanyID = accGlTransactionHeader.CompanyID,
                                                             LocationID = accGlTransactionHeader.LocationID,
                                                             CostCentreID = accGlTransactionHeader.CostCentreID,
                                                             DocumentNo = accGlTransactionHeader.DocumentNo,
                                                             LedgerID = oe.AccLedgerAccountID,
                                                             LedgerSerial = "",
                                                             DocumentID = accGlTransactionHeader.DocumentID,
                                                             DocumentDate = accGlTransactionHeader.DocumentDate,
                                                             PaymentDate = accGlTransactionHeader.DocumentDate,
                                                             ReferenceID = accGlTransactionHeader.ReferenceID,
                                                             DrCr = 2,
                                                             Amount = oe.ExpenseAmount,
                                                             ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                             ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                             ReferenceLocationID = 0,
                                                             ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                             TransactionDefinitionId = 1,
                                                             ChequeNo = string.Empty,
                                                             Reference = string.Empty,
                                                             Remark = "SALES RETURN",
                                                             DocumentStatus = invSalesHeader.DocumentStatus,
                                                             IsUpLoad = isTStatus,
                                                             GroupOfCompanyID = invSalesHeader.GroupOfCompanyID,
                                                             CreatedUser = invSalesHeader.CreatedUser,
                                                             CreatedDate = invSalesHeader.CreatedDate,
                                                             ModifiedUser = invSalesHeader.ModifiedUser,
                                                             ModifiedDate = invSalesHeader.ModifiedDate,
                                                             DataTransfer = invSalesHeader.DataTransfer
                                                         }).ToList();


                            CommonService.BulkInsert(Common.LINQToDataTable(otherExpenceSaveQuery), "AccGlTransactionDetail");
                        }


                        if (paymentTempList != null)
                        {

                            var paymentAccountsSaveQuery = (from oe in paymentTempList
                                                            select new
                                                            {
                                                                AccGlTransactionDetailID = 0,
                                                                AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                                                CompanyID = accGlTransactionHeader.CompanyID,
                                                                LocationID = accGlTransactionHeader.LocationID,
                                                                CostCentreID = accGlTransactionHeader.CostCentreID,
                                                                DocumentNo = accGlTransactionHeader.DocumentNo,
                                                                LedgerID = oe.AccLedgerAccountID,
                                                                LedgerSerial = "",
                                                                DocumentID = accGlTransactionHeader.DocumentID,
                                                                DocumentDate = accGlTransactionHeader.DocumentDate,
                                                                PaymentDate = accGlTransactionHeader.DocumentDate,
                                                                ReferenceID = accGlTransactionHeader.ReferenceID,
                                                                DrCr = 2,
                                                                Amount = oe.PayAmount,
                                                                TransactionTypeId = 1,
                                                                ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                                ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                                ReferenceLocationID = 0,
                                                                ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                                TransactionDefinitionId = 1,
                                                                ChequeNo = string.Empty,
                                                                Reference = string.Empty,
                                                                Remark = "SALES RETURN",
                                                                DocumentStatus = invSalesHeader.DocumentStatus,
                                                                IsUpLoad = isTStatus,
                                                                GroupOfCompanyID = invSalesHeader.GroupOfCompanyID,
                                                                CreatedUser = invSalesHeader.CreatedUser,
                                                                CreatedDate = invSalesHeader.CreatedDate,
                                                                ModifiedUser = invSalesHeader.ModifiedUser,
                                                                ModifiedDate = invSalesHeader.ModifiedDate,
                                                                DataTransfer = invSalesHeader.DataTransfer
                                                            }).ToList();


                            CommonService.BulkInsert(Common.LINQToDataTable(paymentAccountsSaveQuery), "AccGlTransactionDetail");
                        }

                        AccTransactionTypeDetail accTransactionTypeDetail = new AccTransactionTypeDetail();

                        accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionID(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCustomer").DocumentID);

                        if (accTransactionTypeDetail != null)
                        {
                            var customerAccountEntrySaveQuery = (from oe in otherExpenceTempList
                                                                 select new
                                                                 {
                                                                     AccGlTransactionDetailID = 0,
                                                                     AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                                                     CompanyID = accGlTransactionHeader.CompanyID,
                                                                     LocationID = accGlTransactionHeader.LocationID,
                                                                     CostCentreID = accGlTransactionHeader.CostCentreID,
                                                                     DocumentNo = accGlTransactionHeader.DocumentNo,
                                                                     LedgerID = accTransactionTypeDetail.AccLedgerAccountID,
                                                                     LedgerSerial = "",
                                                                     DocumentID = accGlTransactionHeader.DocumentID,
                                                                     DocumentDate = accGlTransactionHeader.DocumentDate,
                                                                     PaymentDate = accGlTransactionHeader.DocumentDate,
                                                                     ReferenceID = accGlTransactionHeader.ReferenceID,
                                                                     DrCr = 1,
                                                                     Amount = invSalesHeader.NetAmount,
                                                                     TransactionTypeId = 1,
                                                                     ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                                     ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                                     ReferenceLocationID = 0,
                                                                     ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                                     TransactionDefinitionId = 1,
                                                                     ChequeNo = string.Empty,
                                                                     Reference = string.Empty,
                                                                     Remark = "SALES RETURN",
                                                                     DocumentStatus = invSalesHeader.DocumentStatus,
                                                                     IsUpLoad = isTStatus,
                                                                     GroupOfCompanyID = invSalesHeader.GroupOfCompanyID,
                                                                     CreatedUser = invSalesHeader.CreatedUser,
                                                                     CreatedDate = invSalesHeader.CreatedDate,
                                                                     ModifiedUser = invSalesHeader.ModifiedUser,
                                                                     ModifiedDate = invSalesHeader.ModifiedDate,
                                                                     DataTransfer = invSalesHeader.DataTransfer
                                                                 }).ToList();


                            CommonService.BulkInsert(Common.LINQToDataTable(customerAccountEntrySaveQuery), "AccGlTransactionDetail");
                        }

                        accTransactionTypeDetail = new AccTransactionTypeDetail();

                        accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionIDDefinition(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmInvoice").DocumentID, 2);

                        if (accTransactionTypeDetail != null)
                        {
                            var discountAccountEntrySaveQuery = (from oe in otherExpenceTempList
                                                                 select new
                                                                 {
                                                                     AccGlTransactionDetailID = 0,
                                                                     AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                                                     CompanyID = accGlTransactionHeader.CompanyID,
                                                                     LocationID = accGlTransactionHeader.LocationID,
                                                                     CostCentreID = accGlTransactionHeader.CostCentreID,
                                                                     DocumentNo = accGlTransactionHeader.DocumentNo,
                                                                     LedgerID = accTransactionTypeDetail.AccLedgerAccountID,
                                                                     LedgerSerial = "",
                                                                     DocumentID = accGlTransactionHeader.DocumentID,
                                                                     DocumentDate = accGlTransactionHeader.DocumentDate,
                                                                     PaymentDate = accGlTransactionHeader.DocumentDate,
                                                                     ReferenceID = accGlTransactionHeader.ReferenceID,
                                                                     DrCr = 1,
                                                                     Amount = invSalesHeader.DiscountAmount,
                                                                     TransactionTypeId = 1,
                                                                     ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                                     ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                                     ReferenceLocationID = 0,
                                                                     ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                                     TransactionDefinitionId = 1,
                                                                     ChequeNo = string.Empty,
                                                                     Reference = string.Empty,
                                                                     Remark = "SALES RETURN",
                                                                     DocumentStatus = invSalesHeader.DocumentStatus,
                                                                     IsUpLoad = isTStatus,
                                                                     GroupOfCompanyID = invSalesHeader.GroupOfCompanyID,
                                                                     CreatedUser = invSalesHeader.CreatedUser,
                                                                     CreatedDate = invSalesHeader.CreatedDate,
                                                                     ModifiedUser = invSalesHeader.ModifiedUser,
                                                                     ModifiedDate = invSalesHeader.ModifiedDate,
                                                                     DataTransfer = invSalesHeader.DataTransfer
                                                                 }).ToList();


                            CommonService.BulkInsert(Common.LINQToDataTable(discountAccountEntrySaveQuery), "AccGlTransactionDetail");
                        }

                        #region Tax1

                        //DataTable dt = new DataTable();
                        //CommonService commonService=new CommonService();
                        //dt = commonService.GetAllTaxesRelatedToSupplier(invPurchaseHeader.SupplierID);

                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    if (i == 0) // Tax1
                        //    {
                        //        tax1Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        //    }
                        //    else if (i == 1) // Tax2
                        //    {
                        //        tax2Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        //    }
                        //    else if (i == 2) // Tax3
                        //    {
                        //        tax3Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        //    }
                        //    else if (i == 3) // Tax4
                        //    {
                        //        tax4Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        //    }
                        //    else if (i == 4) // Tax5
                        //    {
                        //        tax5Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        //    }
                        //    else // More than 5
                        //    {

                        //    }

                        //}


                        #endregion


                        accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionID(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmInvoice").DocumentID);

                        if (accTransactionTypeDetail != null)
                        {
                            var purchaseAccountEntrySaveQuery = (from oe in otherExpenceTempList
                                                                 select new
                                                                 {
                                                                     AccGlTransactionDetailID = 0,
                                                                     AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                                                     CompanyID = accGlTransactionHeader.CompanyID,
                                                                     LocationID = accGlTransactionHeader.LocationID,
                                                                     CostCentreID = accGlTransactionHeader.CostCentreID,
                                                                     DocumentNo = accGlTransactionHeader.DocumentNo,
                                                                     LedgerID = accTransactionTypeDetail.AccLedgerAccountID,
                                                                     LedgerSerial = "",
                                                                     DocumentID = accGlTransactionHeader.DocumentID,
                                                                     DocumentDate = accGlTransactionHeader.DocumentDate,
                                                                     PaymentDate = accGlTransactionHeader.DocumentDate,
                                                                     ReferenceID = accGlTransactionHeader.ReferenceID,
                                                                     DrCr = 2,
                                                                     Amount = invSalesHeader.GrossAmount,
                                                                     TransactionTypeId = 1,
                                                                     ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                                     ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                                     ReferenceLocationID = 0,
                                                                     ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                                     TransactionDefinitionId = 1,
                                                                     ChequeNo = string.Empty,
                                                                     Reference = string.Empty,
                                                                     Remark = "SALES RETURN",
                                                                     DocumentStatus = invSalesHeader.DocumentStatus,
                                                                     IsUpLoad = isTStatus,
                                                                     GroupOfCompanyID = invSalesHeader.GroupOfCompanyID,
                                                                     CreatedUser = invSalesHeader.CreatedUser,
                                                                     CreatedDate = invSalesHeader.CreatedDate,
                                                                     ModifiedUser = invSalesHeader.ModifiedUser,
                                                                     ModifiedDate = invSalesHeader.ModifiedDate,
                                                                     DataTransfer = invSalesHeader.DataTransfer
                                                                 }).ToList();


                            CommonService.BulkInsert(Common.LINQToDataTable(purchaseAccountEntrySaveQuery), "AccGlTransactionDetail");
                        }


                        #endregion


                        #region Payment Entries

                        AccPaymentService accPaymentService = new Service.AccPaymentService();

                        AccPaymentHeader accPaymentHeader = new AccPaymentHeader();
                        accPaymentHeader = accPaymentService.getAccPaymentHeader(invSalesHeader.InvSalesHeaderID, invSalesHeader.DocumentID, invSalesHeader.LocationID);
                        if (accPaymentHeader == null)
                            accPaymentHeader = new AccPaymentHeader();

                        accPaymentHeader.Amount = invSalesHeader.NetAmount;
                        accPaymentHeader.BalanceAmount = invSalesHeader.NetAmount - paidAmount;
                        accPaymentHeader.CompanyID = invSalesHeader.CompanyID;
                        accPaymentHeader.CostCentreID = invSalesHeader.CostCentreID;
                        accPaymentHeader.CreatedDate = invSalesHeader.CreatedDate;
                        accPaymentHeader.CreatedUser = invSalesHeader.CreatedUser;
                        accPaymentHeader.DocumentDate = invSalesHeader.DocumentDate;
                        accPaymentHeader.DocumentID = invSalesHeader.CompanyID;
                        accPaymentHeader.DocumentStatus = invSalesHeader.DocumentStatus;
                        accPaymentHeader.DocumentNo = invSalesHeader.DocumentNo;
                        accPaymentHeader.GroupOfCompanyID = invSalesHeader.GroupOfCompanyID;
                        accPaymentHeader.DataTransfer = invSalesHeader.DataTransfer;
                        accPaymentHeader.IsUpLoad = false;
                        accPaymentHeader.LocationID = invSalesHeader.LocationID;
                        accPaymentHeader.ModifiedDate = invSalesHeader.ModifiedDate;
                        accPaymentHeader.ModifiedUser = invSalesHeader.ModifiedUser;
                        accPaymentHeader.PaymentDate = invSalesHeader.DocumentDate;
                        accPaymentHeader.Reference = "SALES RETURN";
                        accPaymentHeader.ReferenceDocumentDocumentID = invSalesHeader.DocumentID;
                        accPaymentHeader.ReferenceDocumentID = invSalesHeader.InvSalesHeaderID;
                        accPaymentHeader.ReferenceID = invSalesHeader.DocumentID;
                        accPaymentHeader.ReferenceLocationID = invSalesHeader.LocationID;
                        accPaymentHeader.Remark = "SALES RETURN";


                        if (accPaymentHeader.AccPaymentHeaderID.Equals(0))
                            context.AccPaymentHeaders.Add(accPaymentHeader);
                        else
                            context.Entry(accPaymentHeader).State = EntityState.Modified;

                        context.SaveChanges();


                        context.Set<AccPaymentDetail>().Delete(context.AccPaymentDetails.Where(p => p.ReferenceDocumentID.Equals(invSalesHeader.InvSalesHeaderID) && p.LocationID.Equals(invSalesHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(invSalesHeader.DocumentID) && p.DocumentStatus.Equals(0)).AsQueryable());


                        if (paymentTempList != null)
                        {
                            var paymentDetailsSaveQuery = (from pt in paymentTempList
                                                           select new
                                                           {
                                                               AccPaymentDetailID = 0,
                                                               PaymentHeaderID = accPaymentHeader.AccPaymentHeaderID,
                                                               CompanyID = accPaymentHeader.CompanyID,
                                                               LocationID = accPaymentHeader.LocationID,
                                                               CostCentreID = accPaymentHeader.CostCentreID,
                                                               DocumentNo = accPaymentHeader.DocumentNo,
                                                               DocumentID = accPaymentHeader.DocumentID,
                                                               DocumentDate = accPaymentHeader.DocumentDate,
                                                               PaymentDate = accPaymentHeader.PaymentDate,
                                                               ReferenceDocumentID = accPaymentHeader.ReferenceDocumentID,
                                                               ReferenceDocumentDocumentID = accPaymentHeader.ReferenceDocumentDocumentID,
                                                               ReferenceLocationID = accPaymentHeader.ReferenceLocationID,
                                                               SetoffDocumentID = 0,
                                                               SetoffDocumentDocumentID = 0,
                                                               SetoffLocationID = 0,
                                                               ReferenceID = accPaymentHeader.ReferenceID,
                                                               Amount = pt.PayAmount,
                                                               paymentModeID = pt.PaymentMethodID,
                                                               BankID = pt.BankID,
                                                               BankBranchID = 0,
                                                               CardNo = pt.CardCheqNo,
                                                               ChequeDate = pt.ChequeDate,
                                                               DocumentStatus = accPaymentHeader.DocumentStatus,
                                                               IsTStatus = accPaymentHeader.IsUpLoad,
                                                               GroupOfCompanyID = accPaymentHeader.GroupOfCompanyID,
                                                               CreatedUser = accPaymentHeader.CreatedUser,
                                                               CreatedDate = accPaymentHeader.CreatedDate,
                                                               ModifiedUser = accPaymentHeader.ModifiedUser,
                                                               ModifiedDate = accPaymentHeader.ModifiedDate,
                                                               DataTransfer = accPaymentHeader.DataTransfer
                                                           }).ToList();


                            CommonService.BulkInsert(Common.LINQToDataTable(paymentDetailsSaveQuery), "AccPaymentDetail");
                        }



                        #endregion


                        #region Process

                        var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@InvSalesHeaderID", Value=invSalesHeader.InvSalesHeaderID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=invSalesHeader.LocationID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=invSalesHeader.DocumentID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentStatus", Value=invSalesHeader.DocumentStatus}
                    };


                        if (CommonService.ExecuteStoredProcedure("spInvSalesReturnSave", parameter))
                        {
                            transaction.Complete();

                            return true;
                        }
                        else
                            return false;

                        #endregion



                        //context.Acc();

                    }
                }
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

        public List<InvSalesDetailTemp> GetPausedSalesReturnDetail(InvSalesHeader invSalesHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                InvSalesDetailTemp invSalesDetailTemp;

                List<InvSalesDetailTemp> invSalesDetailTempList = new List<InvSalesDetailTemp>();

                var salesDetailTemp = (from pm in context.InvProductMasters
                                       join pd in context.InvSalesDetails on pm.InvProductMasterID equals pd.ProductID
                                       join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                       where pd.LocationID.Equals(invSalesHeader.LocationID) && pd.CompanyID.Equals(invSalesHeader.CompanyID)
                                       && pd.DocumentID.Equals(invSalesHeader.DocumentID) && pd.InvSalesHeaderID.Equals(invSalesHeader.InvSalesHeaderID)
                                       select new
                                       {
                                           pd.AvgCost,
                                           pd.ConvertFactor,
                                           pd.CostPrice,
                                           pd.CurrentQty,
                                           pd.DiscountAmount,
                                           pd.DiscountPercentage,
                                           pd.BatchNo,
                                           pd.ExpiryDate,
                                           pd.FreeQty,
                                           pd.GrossAmount,
                                           pd.LineNo,
                                           pd.LocationID,
                                           pd.NetAmount,
                                           pd.OrderQty,
                                           pm.ProductCode,
                                           pd.ProductID,
                                           pm.ProductName,
                                           pd.Qty,
                                           pd.SellingPrice,
                                           pd.SubTotalDiscount,
                                           pd.TaxAmount1,
                                           pd.TaxAmount2,
                                           pd.TaxAmount3,
                                           pd.TaxAmount4,
                                           pd.TaxAmount5,
                                           pd.UnitOfMeasureID,
                                           uc.UnitOfMeasureName,
                                           pd.DispatchLocationID,
                                       }).ToArray();

                foreach (var tempProduct in salesDetailTemp)
                {
                    invSalesDetailTemp = new InvSalesDetailTemp();

                    invSalesDetailTemp.AverageCost = tempProduct.AvgCost;
                    invSalesDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    invSalesDetailTemp.Rate = tempProduct.SellingPrice;
                    invSalesDetailTemp.CurrentQty = tempProduct.CurrentQty;
                    invSalesDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                    invSalesDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                    invSalesDetailTemp.BatchNo = tempProduct.BatchNo;
                    invSalesDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                    invSalesDetailTemp.FreeQty = tempProduct.FreeQty;
                    invSalesDetailTemp.GrossAmount = tempProduct.GrossAmount;
                    invSalesDetailTemp.LineNo = tempProduct.LineNo;
                    invSalesDetailTemp.LocationID = tempProduct.LocationID;
                    invSalesDetailTemp.NetAmount = tempProduct.NetAmount;
                    invSalesDetailTemp.ProductCode = tempProduct.ProductCode;
                    invSalesDetailTemp.ProductID = tempProduct.ProductID;
                    invSalesDetailTemp.ProductName = tempProduct.ProductName;
                    invSalesDetailTemp.Qty = tempProduct.Qty;
                    invSalesDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                    invSalesDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                    invSalesDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                    invSalesDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                    invSalesDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                    invSalesDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                    invSalesDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invSalesDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    //  invSalesDetailTemp.DispatchLocation = tempProduct.LocationCode;

                    invSalesDetailTempList.Add(invSalesDetailTemp);
                }

                return invSalesDetailTempList.OrderBy(pd => pd.LineNo).ToList();
            }
        }
        #endregion

        public List<InvSalesDetailTemp> getUpdateSalesDetailTemp(List<InvSalesDetailTemp> invSalesTempDetail, InvSalesDetailTemp invSalesDetail, InvProductMaster invProductMaster)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                InvSalesDetailTemp invSalesDetailTemp = new InvSalesDetailTemp();

                //if (invProductMaster.IsExpiry)
                //    invSalesDetailTemp = invSalesTempDetail.Where(p => p == invSalesDetail.ProductID && p.UnitOfMeasureID == invSalesDetail.UnitOfMeasureID && (p.ExpiryDate != null ? p.ExpiryDate == invSalesDetail.ExpiryDate : true)).FirstOrDefault();
                //else
                    invSalesDetailTemp = invSalesTempDetail.Where(p => p.ProductCode == invSalesDetail.ProductCode).FirstOrDefault();

                if (invSalesDetailTemp == null || invSalesDetailTemp.LineNo.Equals(0))
                {
                    if (invSalesTempDetail.Count.Equals(0))
                        invSalesDetail.LineNo = 1;
                    else
                        invSalesDetail.LineNo = invSalesTempDetail.Max(s => s.LineNo) + 1;
                }
                else
                {
                    invSalesTempDetail.Remove(invSalesDetailTemp);
                    invSalesDetail.LineNo = invSalesDetailTemp.LineNo;

                }

                invSalesTempDetail.Add(invSalesDetail);

                return invSalesTempDetail.OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public void DeleteSales(string DocumentNo)
        {
            CommonService.ExecuteSqlQuery("update sd set sd.DocumentStatus = 4 from InvSalesHeader sd inner join InvSalesHeader sh on sh.InvSalesHeaderID=sd.InvSalesHeaderID   where sh.DocumentNo = '" + DocumentNo + "'");
            CommonService.ExecuteSqlQuery("update InvSalesHeader set DocumentStatus = 4 where DocumentNo = '" + DocumentNo + "'");
        }

        public List<InvSalesDetailTemp> getDeleteSalesDetailTemp(List<InvSalesDetailTemp> invSalesTempDetail, InvSalesDetailTemp invSalesDetail, List<InvProductSerialNoTemp> invProductSerialNoListToDelete, out List<InvProductSerialNoTemp> invProductSerialNoList)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                InvSalesDetailTemp invSalesDetailTemp = new InvSalesDetailTemp();
                List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                invSalesDetailTemp = invSalesTempDetail.Where(p => p.ProductID == invSalesDetail.ProductID && p.UnitOfMeasureID == invSalesDetail.UnitOfMeasureID && p.ExpiryDate == invSalesDetail.ExpiryDate).FirstOrDefault();

                if (invSalesDetailTemp != null)
                {
                    invSalesTempDetail.Remove(invSalesDetailTemp);
                }

                invProductSerialNoTempList = invProductSerialNoListToDelete.Where(p => p.ProductID == invSalesDetail.ProductID && p.UnitOfMeasureID == invSalesDetail.UnitOfMeasureID && p.ExpiryDate == invSalesDetail.ExpiryDate).ToList();

                foreach (InvProductSerialNoTemp InvProductSerialNoTempDelete in invProductSerialNoTempList)
                {
                    invProductSerialNoListToDelete.Remove(InvProductSerialNoTempDelete);
                }

                invProductSerialNoList = invProductSerialNoListToDelete;

                return invSalesTempDetail.OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public List<InvSalesDetailTemp> GetDeleteSalesDetailTemp(List<InvSalesDetailTemp> invSalesTempDetail, InvSalesDetailTemp invSalesDetail)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                InvSalesDetailTemp invSalesDetailTemp = new InvSalesDetailTemp();

                invSalesDetailTemp = invSalesTempDetail.Where(p => p.ProductID == invSalesDetail.ProductID && p.UnitOfMeasureID == invSalesDetail.UnitOfMeasureID).FirstOrDefault();

                long removedLineNo = 0;
                if (invSalesDetailTemp != null)
                {
                    invSalesTempDetail.Remove(invSalesDetailTemp);
                    removedLineNo = invSalesDetailTemp.LineNo;

                }
                invSalesTempDetail.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);

                return invSalesTempDetail.OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public List<InvProductSerialNoTemp> getUpdateSerialNoTemp(List<InvProductSerialNoTemp> invProductSerialNoList, InvProductSerialNoTemp invProductSerialNumber)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();

                invProductSerialNoTemp = invProductSerialNoList.Where(s => s.SerialNo.Equals(invProductSerialNumber.SerialNo) && s.ProductID.Equals(invProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(invProductSerialNumber.UnitOfMeasureID) && s.ExpiryDate.Equals(invProductSerialNumber.ExpiryDate)).FirstOrDefault();

                if (invProductSerialNoTemp == null || invProductSerialNoTemp.LineNo.Equals(0))
                {
                    if (invProductSerialNoList.Count.Equals(0))
                        invProductSerialNumber.LineNo = 1;
                    else
                    {
                        if (!invProductSerialNoList.Where(s => s.ProductID.Equals(invProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(invProductSerialNumber.UnitOfMeasureID) && s.ExpiryDate.Equals(invProductSerialNumber.ExpiryDate)).Count().Equals(0))
                            invProductSerialNumber.LineNo = invProductSerialNoList.Where(s => s.ProductID.Equals(invProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(invProductSerialNumber.UnitOfMeasureID) && s.ExpiryDate.Equals(invProductSerialNumber.ExpiryDate)).Max(s => s.LineNo) + 1;
                        else
                            invProductSerialNumber.LineNo = 1;
                    }
                    invProductSerialNoList.Add(invProductSerialNumber);
                }

                return invProductSerialNoList.OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public List<InvProductSerialNoTemp> getSerialNoDetail(InvProductMaster invProductMaster)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                List<InvProductSerialNoDetail> invProductSerialNoDetail = new List<InvProductSerialNoDetail>();

                invProductSerialNoDetail = context.InvProductSerialNoDetails.Where(ps => ps.ProductID.Equals(invProductMaster.InvProductMasterID)).ToList();
                foreach (InvProductSerialNoDetail invProductSerialNoDetailTemp in invProductSerialNoDetail)
                {
                    InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();

                    invProductSerialNoTemp.ExpiryDate = invProductSerialNoDetailTemp.ExpiryDate;
                    invProductSerialNoTemp.LineNo = invProductSerialNoDetailTemp.LineNo;
                    invProductSerialNoTemp.ProductID = invProductSerialNoDetailTemp.ProductID;
                    invProductSerialNoTemp.SerialNo = invProductSerialNoDetailTemp.SerialNo;
                    invProductSerialNoTemp.UnitOfMeasureID = invProductSerialNoDetailTemp.UnitOfMeasureID;

                    invProductSerialNoTempList.Add(invProductSerialNoTemp);
                }

                return invProductSerialNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public List<InvProductBatchNoTemp> GetBatchNoDetail(InvProductMaster invProductMaster, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();

                List<InvProductBatchNoExpiaryDetail> invProductBatchNoDetail = new List<InvProductBatchNoExpiaryDetail>();

                invProductBatchNoDetail = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(invProductMaster.InvProductMasterID) && ps.LocationID.Equals(locationID) && ps.BalanceQty > 0).ToList();
                foreach (InvProductBatchNoExpiaryDetail invProductBatchNoTemp in invProductBatchNoDetail)
                {
                    InvProductBatchNoTemp invProductBatchNoDetailTemp = new InvProductBatchNoTemp();

                    invProductBatchNoDetailTemp.LineNo = invProductBatchNoTemp.LineNo;
                    invProductBatchNoDetailTemp.ProductID = invProductBatchNoTemp.ProductID;
                    invProductBatchNoDetailTemp.BatchNo = invProductBatchNoTemp.BatchNo;
                    invProductBatchNoDetailTemp.UnitOfMeasureID = invProductBatchNoTemp.UnitOfMeasureID;

                    invProductBatchNoTempList.Add(invProductBatchNoDetailTemp);
                }

                return invProductBatchNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public List<InvProductBatchNoTemp> getBatchNoDetail(InvProductMaster invProductMaster)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();

                List<InvProductBatchNoExpiaryDetail> invProductBatchNoDetail = new List<InvProductBatchNoExpiaryDetail>();

                invProductBatchNoDetail = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(invProductMaster.InvProductMasterID)).ToList();
                foreach (InvProductBatchNoExpiaryDetail invProductBatchNoTemp in invProductBatchNoDetail)
                {
                    InvProductBatchNoTemp invProductBatchNoDetailTemp = new InvProductBatchNoTemp();

                    invProductBatchNoDetailTemp.LineNo = invProductBatchNoTemp.LineNo;
                    invProductBatchNoDetailTemp.ProductID = invProductBatchNoTemp.ProductID;
                    invProductBatchNoDetailTemp.BatchNo = invProductBatchNoTemp.BatchNo;
                    invProductBatchNoDetailTemp.UnitOfMeasureID = invProductBatchNoTemp.UnitOfMeasureID;

                    invProductBatchNoTempList.Add(invProductBatchNoDetailTemp);
                }

                return invProductBatchNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public List<InvProductBatchNoTemp> getExpiryDetail(InvProductMaster invProductMaster)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();

                List<InvProductBatchNoExpiaryDetail> invProductBatchNoDetail = new List<InvProductBatchNoExpiaryDetail>();

                invProductBatchNoDetail = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(invProductMaster.InvProductMasterID)).ToList();
                foreach (InvProductBatchNoExpiaryDetail invProductBatchNoTemp in invProductBatchNoDetail)
                {
                    InvProductBatchNoTemp invProductBatchNoDetailTemp = new InvProductBatchNoTemp();

                    invProductBatchNoDetailTemp.LineNo = invProductBatchNoTemp.LineNo;
                    invProductBatchNoDetailTemp.ProductID = invProductBatchNoTemp.ProductID;
                    invProductBatchNoDetailTemp.BatchNo = invProductBatchNoTemp.BatchNo;
                    invProductBatchNoDetailTemp.ExpiryDate = invProductBatchNoTemp.ExpiryDate;
                    invProductBatchNoDetailTemp.UnitOfMeasureID = invProductBatchNoTemp.UnitOfMeasureID;

                    invProductBatchNoTempList.Add(invProductBatchNoDetailTemp);
                }

                return invProductBatchNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public InvProductBatchNoExpiaryDetail GetInvProductBatchNoByBatchNo(string batchNo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {return context.InvProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo)).FirstOrDefault();}
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> documentNoList = context.InvSalesHeaders.Where(q => q.DocumentID.Equals(documentID) && q.LocationID.Equals(locationID) && q.DocumentStatus.Equals(1) ).Select(q => q.DocumentNo).ToList();
                return documentNoList.ToArray();
            }
        }

        public bool IsValidNoOfSerialNo(List<InvProductSerialNoTemp> invProductSerialNoList, InvProductSerialNoTemp invProductSerialNumber, decimal Qty)
        {
            if (Qty.Equals(invProductSerialNoList.Where(s => s.ProductID.Equals(invProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(invProductSerialNumber.UnitOfMeasureID) && s.ExpiryDate.Equals(invProductSerialNumber.ExpiryDate)).Count()))
                return true;
            else
                return false;
        }

        public string getPausedSerialNoDetailByDocumentNo(string serialNo, string documentNo, InvProductSerialNoTemp invProductSerialNoTemp, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                InvPurchaseHeader invPurchaseHeader = new InvPurchaseHeader();
                invPurchaseHeader = getPurchaseHeaderByDocumentNo(invProductSerialNoTemp.DocumentID, documentNo, locationID);
                if (invPurchaseHeader == null)
                    invPurchaseHeader = new InvPurchaseHeader();

                InvProductSerialNoDetail InvProductSerialNoDetailtmp = new InvProductSerialNoDetail();
                InvProductSerialNoDetailtmp = context.InvProductSerialNoDetails.Where(sd => sd.SerialNo.Equals(serialNo) && sd.ReferenceDocumentDocumentID.Equals(invProductSerialNoTemp.DocumentID) && !sd.ReferenceDocumentID.Equals(invPurchaseHeader.PurchaseHeaderID) && sd.ProductID.Equals(invProductSerialNoTemp.ProductID) && sd.UnitOfMeasureID.Equals(invProductSerialNoTemp.UnitOfMeasureID) && sd.ExpiryDate.Equals(invProductSerialNoTemp.ExpiryDate) && sd.DocumentStatus.Equals(0)).FirstOrDefault();
                if (InvProductSerialNoDetailtmp != null)
                {
                    invPurchaseHeader = getPausedPurchaseHeaderByDocumentID(invProductSerialNoTemp.DocumentID, InvProductSerialNoDetailtmp.ReferenceDocumentID, locationID);
                    if (invPurchaseHeader != null)
                        return invPurchaseHeader.DocumentNo;
                    else
                        return string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public bool IsExistsSavedSerialNoDetailByDocumentNo(string serialNo, InvProductSerialNoTemp invProductSerialNoTemp)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                if (context.InvProductSerialNos.Where(sd => sd.SerialNo.Equals(serialNo) && sd.ProductID.Equals(invProductSerialNoTemp.ProductID) && sd.UnitOfMeasureID.Equals(invProductSerialNoTemp.UnitOfMeasureID) && sd.ExpiryDate.Equals(invProductSerialNoTemp.ExpiryDate) && sd.DocumentStatus.Equals(1)).FirstOrDefault() == null)
                    return false;
                else
                    return true;
            }
        }
        
        public List<InvPurchaseDetailTemp> getPausedPurchaseDetail(InvPurchaseHeader invPurchaseHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                InvPurchaseDetailTemp invPurchaseDetailTemp;

                List<InvPurchaseDetailTemp> invPurchaseDetailTempList = new List<InvPurchaseDetailTemp>();

                var purchaseDetailTemp = (from pm in context.InvProductMasters
                                          join pd in context.InvPurchaseDetails on pm.InvProductMasterID equals pd.ProductID
                                          join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                          where pd.LocationID.Equals(invPurchaseHeader.LocationID) && pd.CompanyID.Equals(invPurchaseHeader.CompanyID)
                                          && pd.DocumentID.Equals(invPurchaseHeader.DocumentID) && pd.InvPurchaseHeaderID.Equals(invPurchaseHeader.PurchaseHeaderID)
                                          select new
                                          {
                                              pd.AvgCost,
                                              pd.ConvertFactor,
                                              pd.CostPrice,
                                              pd.CurrentQty,
                                              pd.DiscountAmount,
                                              pd.DiscountPercentage,
                                              ExpiryDate = pd.ExpiryDate,
                                              pd.FreeQty,
                                              pd.GrossAmount,
                                              pd.LineNo,
                                              pd.LocationID,
                                              pd.NetAmount,
                                              pd.OrderQty,
                                              pm.ProductCode,
                                              pd.ProductID,
                                              pm.ProductName,
                                              pd.Qty,
                                              pd.SellingPrice,
                                              pd.SubTotalDiscount,
                                              pd.TaxAmount1,
                                              pd.TaxAmount2,
                                              pd.TaxAmount3,
                                              pd.TaxAmount4,
                                              pd.TaxAmount5,
                                              pd.UnitOfMeasureID,
                                              uc.UnitOfMeasureName

                                          }).ToArray();

                foreach (var tempProduct in purchaseDetailTemp)
                {

                    invPurchaseDetailTemp = new InvPurchaseDetailTemp();

                    invPurchaseDetailTemp.AverageCost = tempProduct.AvgCost;
                    invPurchaseDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    invPurchaseDetailTemp.CostPrice = tempProduct.CostPrice;
                    invPurchaseDetailTemp.CurrentQty = tempProduct.CurrentQty;
                    invPurchaseDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                    invPurchaseDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                    invPurchaseDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                    invPurchaseDetailTemp.FreeQty = tempProduct.FreeQty;
                    invPurchaseDetailTemp.GrossAmount = tempProduct.GrossAmount;
                    invPurchaseDetailTemp.LineNo = tempProduct.LineNo;
                    invPurchaseDetailTemp.LocationID = tempProduct.LocationID;
                    invPurchaseDetailTemp.NetAmount = tempProduct.NetAmount;
                    invPurchaseDetailTemp.OrderQty = tempProduct.OrderQty;
                    invPurchaseDetailTemp.ProductCode = tempProduct.ProductCode;
                    invPurchaseDetailTemp.ProductID = tempProduct.ProductID;
                    invPurchaseDetailTemp.ProductName = tempProduct.ProductName;
                    invPurchaseDetailTemp.Qty = tempProduct.Qty;
                    invPurchaseDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    invPurchaseDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                    invPurchaseDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                    invPurchaseDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                    invPurchaseDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                    invPurchaseDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                    invPurchaseDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                    invPurchaseDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invPurchaseDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                    invPurchaseDetailTempList.Add(invPurchaseDetailTemp);
                }

                return invPurchaseDetailTempList.OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public List<InvProductSerialNoTemp> getPausedPurchaseSerialNoDetail(InvPurchaseHeader invPurchaseHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();
                List<InvProductSerialNoDetail> invProductSerialNoDetail = new List<InvProductSerialNoDetail>();

                invProductSerialNoDetail = context.InvProductSerialNoDetails.Where(ps => ps.ReferenceDocumentID.Equals(invPurchaseHeader.PurchaseHeaderID) && ps.ReferenceDocumentDocumentID.Equals(invPurchaseHeader.DocumentID)).ToList();
                foreach (InvProductSerialNoDetail invProductSerialNoDetailTemp in invProductSerialNoDetail)
                {
                    InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();

                    invProductSerialNoTemp.ExpiryDate = invProductSerialNoDetailTemp.ExpiryDate;
                    invProductSerialNoTemp.LineNo = invProductSerialNoDetailTemp.LineNo;
                    invProductSerialNoTemp.ProductID = invProductSerialNoDetailTemp.ProductID;
                    invProductSerialNoTemp.SerialNo = invProductSerialNoDetailTemp.SerialNo;
                    invProductSerialNoTemp.UnitOfMeasureID = invProductSerialNoDetailTemp.UnitOfMeasureID;

                    invProductSerialNoTempList.Add(invProductSerialNoTemp);
                }

                return invProductSerialNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public InvPurchaseHeader getPurchaseHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            { return context.InvPurchaseHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault(); }
        }

        public InvPurchaseHeader getPausedPurchaseHeaderByDocumentID(int documentDocumentID, long documentID, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            { return context.InvPurchaseHeaders.Where(ph => ph.DocumentID.Equals(documentDocumentID) && ph.PurchaseHeaderID.Equals(documentID) && ph.LocationID.Equals(locationID) && ph.DocumentStatus.Equals(0)).FirstOrDefault(); }
        }
        
        #region  - Deleted - Save
        //public bool Save(InvPurchaseHeader invPurchaseHeader, List<InvPurchaseDetail> invPurchaseDetail)
        //{

        //        context.InvPurchaseHeaders.Add(invPurchaseHeader);


        //    context.SaveChanges();

        //    foreach (InvPurchaseDetail invPurchaseDetailtmp in invPurchaseDetail)
        //    {

        //        context.InvPurchaseDetails.Add(invPurchaseDetailtmp);
        //        context.SaveChanges();
        //    }

        //    return true;
        //}

        //public bool Save(InvSalesHeader invSalesHeader, List<InvSalesDetailTemp> invSalesDetailTemp, List<InvProductSerialNoTemp> invProductSerialNoTemp)
        //{
        //    using (TransactionScope transaction = new TransactionScope())
        //    {

        //        if (invSalesHeader.InvSalesHeaderID.Equals(0))
        //            context.InvSalesHeaders.Add(invSalesHeader);
        //        else
        //            context.Entry(invSalesHeader).State = EntityState.Modified;

        //        context.SaveChanges();

        //        List<InvPurchaseDetail> invPurchaseDetailDelete = new List<InvPurchaseDetail>();
        //        invPurchaseDetailDelete = context.InvPurchaseDetails.Where(p => p.PurchaseHeaderID.Equals(invSalesHeader.InvSalesHeaderID) && p.LocationID.Equals(invSalesHeader.LocationID) && p.DocumentID.Equals(invSalesHeader.DocumentID)).ToList();



        //        foreach (InvPurchaseDetail invPurchaseDetailDeleteTemp in invPurchaseDetailDelete)
        //        {
        //            context.InvPurchaseDetails.Remove(invPurchaseDetailDeleteTemp);
        //        }

        //        List<InvProductSerialNoDetail> invProductSerialNoDetailDelete = new List<InvProductSerialNoDetail>();
        //        invProductSerialNoDetailDelete = context.InvProductSerialNoDetails.Where(p => p.ReferenceDocumentID.Equals(invSalesHeader.InvSalesHeaderID) && p.LocationID.Equals(invSalesHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(invSalesHeader.DocumentID)).ToList();



        //        foreach (InvProductSerialNoDetail invProductSerialNoDetailDeleteTemp in invProductSerialNoDetailDelete)
        //        {
        //            context.InvProductSerialNoDetails.Remove(invProductSerialNoDetailDeleteTemp);
        //        }

        //        context.SaveChanges();


        //        foreach (InvSalesDetailTemp invSalesDetail in invSalesDetailTemp)
        //        {
        //            InvSalesDetail invSalesDetailSave = new InvSalesDetail();


        //            invSalesDetailSave.AvgCost = invSalesDetail.AverageCost;
        //            invSalesDetailSave.CompanyID = invSalesHeader.CompanyID;
        //            invSalesDetailSave.ConvertFactor = invSalesDetail.ConvertFactor;
        //            invSalesDetailSave.CostCentreID = invSalesHeader.CostCentreID;
        //            invSalesDetailSave.CurrentQty = invSalesDetail.CurrentQty;
        //            invSalesDetailSave.DiscountAmount = invSalesDetail.DiscountAmount;
        //            invSalesDetailSave.DiscountPercentage = invSalesDetail.DiscountPercentage;
        //            invSalesDetailSave.DocumentID = invSalesHeader.DocumentID;
        //            invSalesDetailSave.DocumentStatus = invSalesHeader.DocumentStatus;
        //            invSalesDetailSave.ExpiryDate = invSalesDetail.ExpiryDate;
        //            invSalesDetailSave.FreeQty = invSalesDetail.FreeQty;
        //            invSalesDetailSave.GrossAmount = invSalesDetail.GrossAmount;
        //            invSalesDetailSave.LineNo = invSalesDetail.LineNo;
        //            invSalesDetailSave.LocationID = invSalesHeader.LocationID;
        //            invSalesDetailSave.NetAmount = invSalesDetail.NetAmount;
        //            invSalesDetailSave.ProductID = invSalesDetail.ProductID;
        //            invSalesDetailSave.SalesHeaderID = invSalesHeader.InvSalesHeaderID;
        //            invSalesDetailSave.Qty = invSalesDetail.Qty;
        //            invSalesDetailSave.BalanceQty = invSalesDetail.Qty;
        //            invSalesDetailSave.SellingPrice = invSalesDetail.Rate;
        //            invSalesDetailSave.SubTotalDiscount = invSalesDetail.SubTotalDiscount;
        //            invSalesDetailSave.TaxAmount1 = invSalesDetail.TaxAmount1;
        //            invSalesDetailSave.TaxAmount2 = invSalesDetail.TaxAmount2;
        //            invSalesDetailSave.TaxAmount3 = invSalesDetail.TaxAmount3;
        //            invSalesDetailSave.TaxAmount4 = invSalesDetail.TaxAmount4;
        //            invSalesDetailSave.TaxAmount5 = invSalesDetail.TaxAmount5;
        //            invSalesDetailSave.UnitOfMeasureID = invSalesDetail.UnitOfMeasureID;
        //            invSalesDetailSave.BaseUnitID = invSalesDetail.BaseUnitID;

        //            if (invSalesHeader.DocumentStatus.Equals(1))
        //            {
        //                context.InvProductStockMasters.Update(ps => ps.LocationID.Equals(invSalesHeader.LocationID) && ps.ProductID.Equals(invSalesDetail.ProductID), ps => new InvProductStockMaster { Stock = ps.Stock + (invSalesDetail.Qty * invSalesDetail.ConvertFactor) });

        //                //if (invSalesDetail.UnitOfMeasureID.Equals(invSalesDetail.BaseUnitID))
        //                //    context.InvProductMasters.Update(pm => pm.InvProductMasterID.Equals(invSalesDetail.ProductID), pmNew => new InvProductMaster { CostPrice = invSalesDetail.CostPrice, SellingPrice = invSalesDetail.SellingPrice });
        //                //else
        //                //    context.InvProductUnitConversions.Update(uc => uc.ProductID.Equals(invSalesDetail.ProductID) && uc.UnitOfMeasureID.Equals(invSalesDetail.UnitOfMeasureID), ucNew => new InvProductUnitConversion { CostPrice = invSalesDetail.CostPrice, SellingPrice = invSalesDetail.SellingPrice });

        //            }
        //            context.InvSalesDetails.Add(invSalesDetailSave);

        //            context.SaveChanges();
        //            ///context.InvSalesDetails.Update(pd => pd.SalesHeaderID.Equals(invSalesHeader.InvSalesHeaderID) && pd.LocationID.Equals(invSalesHeader.LocationID) && pd.DocumentID.Equals(invSalesHeader.DocumentID) && pd.InvSalesDetailID.Equals(invSalesDetailSave.InvSalesDetailID), pdNew => new InvPurchaseDetail { SalesDetailID = invSalesDetailSave.InvSalesDetailID });


        //        }

        //        foreach (InvProductSerialNoTemp invPurchaseDetailSerialNo in invProductSerialNoTemp)
        //        {
        //            InvProductSerialNoDetail invProductSerialNoDetailSave = new InvProductSerialNoDetail();


        //            if (invSalesHeader.DocumentStatus.Equals(1))
        //            {
        //                InvProductSerialNo invProductSerialNoSave = new InvProductSerialNo();

        //                invProductSerialNoSave.CompanyID = invSalesHeader.CompanyID;
        //                invProductSerialNoSave.CostCentreID = invSalesHeader.CostCentreID;
        //                invProductSerialNoSave.DocumentStatus = invSalesHeader.DocumentStatus;
        //                //invProductSerialNoSave.GroupOfCompanyID = invSalesHeader.GroupOfCompanyID;
        //                invProductSerialNoSave.LocationID = invSalesHeader.LocationID;
        //                invProductSerialNoSave.ProductID = invPurchaseDetailSerialNo.ProductID;
        //                invProductSerialNoSave.UnitOfMeasureID = invPurchaseDetailSerialNo.UnitOfMeasureID;
        //                invProductSerialNoSave.ExpiryDate = invPurchaseDetailSerialNo.ExpiryDate;
        //                invProductSerialNoSave.SerialNo = invPurchaseDetailSerialNo.SerialNo;
        //                invProductSerialNoSave.SerialNoStatus = 1;


        //                context.InvProductSerialNos.Add(invProductSerialNoSave);
        //            }


        //            invProductSerialNoDetailSave.CompanyID = invSalesHeader.CompanyID;
        //            invProductSerialNoDetailSave.CostCentreID = invSalesHeader.CostCentreID;
        //            invProductSerialNoDetailSave.DocumentStatus = invSalesHeader.DocumentStatus;
        //           // invProductSerialNoDetailSave.GroupOfCompanyID = invSalesHeader.GroupOfCompanyID;
        //            invProductSerialNoDetailSave.LocationID = invSalesHeader.LocationID;
        //            invProductSerialNoDetailSave.ProductID = invPurchaseDetailSerialNo.ProductID;
        //            invProductSerialNoDetailSave.LineNo = invPurchaseDetailSerialNo.LineNo;
        //            invProductSerialNoDetailSave.UnitOfMeasureID = invPurchaseDetailSerialNo.UnitOfMeasureID;
        //            invProductSerialNoDetailSave.ExpiryDate = invPurchaseDetailSerialNo.ExpiryDate;
        //            invProductSerialNoDetailSave.SerialNo = invPurchaseDetailSerialNo.SerialNo;
        //            invProductSerialNoDetailSave.ReferenceDocumentID = invSalesHeader.InvSalesHeaderID;
        //            invProductSerialNoDetailSave.ReferenceDocumentDocumentID = invSalesHeader.DocumentID;

        //            context.InvProductSerialNoDetails.Add(invProductSerialNoDetailSave);
        //            context.SaveChanges();

        //            context.InvProductSerialNoDetails.Update(pd => pd.ReferenceDocumentID.Equals(invSalesHeader.InvSalesHeaderID) && pd.LocationID.Equals(invSalesHeader.LocationID) && pd.ReferenceDocumentDocumentID.Equals(invSalesHeader.DocumentID) && pd.InvProductSerialNoDetailID.Equals(invProductSerialNoDetailSave.InvProductSerialNoDetailID), pdNew => new InvProductSerialNoDetail { ProductSerialNoDetailID = invProductSerialNoDetailSave.InvProductSerialNoDetailID });

        //        }

        //        ///context.InvSalesHeaders.Update(ph => ph.InvSalesHeaderID.Equals(invSalesHeader.InvSalesHeaderID) && ph.LocationID.Equals(invSalesHeader.LocationID) && ph.DocumentID.Equals(invSalesHeader.DocumentID), phNew => new InvPurchaseHeader { SalesHeaderID = invSalesHeader.InvSalesHeaderID });
        //        context.SaveChanges();

        //        transaction.Complete();

        //        //context.Acc();
        //        return true;
                
        //    }
        //}
        #endregion

        
        #region GetNewCode

        public string GetDocumentNo(string formName, int locationID, string locationCode, int documentId, bool isTemporytNo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                AutoGenerateInfo autoGenerateInfo;
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
                string getNewCode = string.Empty;
                long tempCode = 0;

                string preFix = string.Empty;

                bool isLocationCode = autoGenerateInfo.IsLocationCode;
                int codeLength = autoGenerateInfo.CodeLength;
                preFix = autoGenerateInfo.Prefix.Trim();

                if (isTemporytNo)
                {

                    if ((preFix.StartsWith("T")))
                        preFix = "X";
                    else
                        preFix = "T";

                }

                if (isLocationCode)
                    preFix = preFix + locationCode.Trim();


                if (isTemporytNo)
                {
                    context.DocumentNumbers.Update(d => d.LocationID.Equals(locationID) && d.DocumentName.Equals(autoGenerateInfo.FormText)   && d.DocumentID.Equals(documentId), d => new DocumentNumber { TempDocumentNo = d.TempDocumentNo + 1 });

                    tempCode = context.DocumentNumbers.Where(d => d.LocationID.Equals(locationID) && d.DocumentName.Equals(autoGenerateInfo.FormText) && d.DocumentID.Equals(documentId)).Max(d => d.TempDocumentNo);
                }
                else
                {
                    context.DocumentNumbers.Update(d => d.LocationID.Equals(locationID) && d.DocumentName.Equals(autoGenerateInfo.FormText) && d.DocumentID.Equals(documentId), d => new DocumentNumber { DocumentNo = d.DocumentNo + 1 });

                    tempCode = context.DocumentNumbers.Where(d => d.LocationID.Equals(locationID) && d.DocumentName.Equals(autoGenerateInfo.FormText) && d.DocumentID.Equals(documentId)).Max(d => d.DocumentNo);
                }

                getNewCode = (tempCode).ToString();
                getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
                return getNewCode.ToUpper();
            }
        }

        #endregion

        public ArrayList GetSelectionDataSales(Common.ReportDataStruct reportDataStruct)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                IQueryable qryResulData = context.InvSalesHeaders
                                .Where("DocumentStatus == @0 ", 1)
                                .OrderBy("" + reportDataStruct.DbColumnName.Trim() + "") //.Select("new(DocumentNo)")
                                .Select(reportDataStruct.DbColumnName.Trim());

                //var x = (from qryResulData

                // change this code to get rid of the foreach loop
                ArrayList selectionDataList = new ArrayList();
                if (reportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                {
                    foreach (var item in qryResulData)
                    {
                        DateTime tdnew = new DateTime();
                        tdnew = Convert.ToDateTime(item.ToString());
                        selectionDataList.Add(tdnew.ToShortDateString());
                    }
                }
                else
                {
                    foreach (var item in qryResulData)
                    { selectionDataList.Add(item.ToString()); }
                }
                return selectionDataList;
            }
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                // Create query 
                IQueryable qryResult = context.InvSalesHeaders.Where("DocumentStatus == @0 AND DocumentID == @1", 1, autoGenerateInfo.DocumentID);

                switch (reportDataStruct.DbColumnName.Trim())
                {
                    case "CustomerID":
                        qryResult = qryResult.Join(context.Customers, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.CustomerCode)").
                                    OrderBy("CustomerCode").Select("CustomerCode");
                        break;
                    case "SalesPersonID":
                        qryResult = qryResult.Join(context.InvSalesPersons, reportDataStruct.DbColumnName.Trim(), "InvSalesPersonID", "new(inner.SalesPersonCode)").
                                        OrderBy("SalesPersonCode").Select("SalesPersonCode");
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
        }       

        public ArrayList GetSelectionSalesData(Common.ReportDataStruct reportDataStruct)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                IQueryable qryResulData = context.InvSalesHeaders
                                .Where("DocumentStatus == @0 ", 1)
                                .OrderBy("" + reportDataStruct.DbColumnName.Trim() + "") //.Select("new(DocumentNo)")
                                .Select(reportDataStruct.DbColumnName.Trim());

                //var x = (from qryResulData

                // change this code to get rid of the foreach loop
                ArrayList selectionDataList = new ArrayList();
                if (reportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                {
                    foreach (var item in qryResulData)
                    {
                        DateTime tdnew = new DateTime();
                        tdnew = Convert.ToDateTime(item.ToString());
                        selectionDataList.Add(tdnew.ToShortDateString());
                    }
                }
                else
                {
                    foreach (var item in qryResulData)
                    { selectionDataList.Add(item.ToString()); }
                }
                return selectionDataList;
            }
        }

        public DataTable GetProductSalesDetailsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {
            using (ERPDbContext context = new ERPDbContext())
            {

                StringBuilder querrySelect = new StringBuilder();
                var query = context.InvSalesHeaders.Where("DocumentStatus=1");

                foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
                {
                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
                }

                //(this IQueryable outer, IEnumerable inner, string outerSelector, string innerSelector, string resultsSelector, params object[] values)
                //var newq = query.Join("c", querrSupplier  ,"g","c.key.grp","g.key","new(g.key as grp,c.key.cat,new(c.count()*100/g.count() as percent) as agg)");

                // Set fields to be selected
                foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
                { querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", "); }

                // Remove last two charators (", ")
                querrySelect.Remove(querrySelect.Length - 2, 2);
                //string st = querrySelect.ToString();
                //var v = query.AsQueryable().Select("new(" + querrySelect.ToString() + ")");


                //join bn in context.InvProductBatchNoExpiaryDetails on bn.ProductID equals pd.ProductID


                var queryResult = (from ph in query
                                   join pd in context.InvSalesDetails on ph.DocumentID equals pd.DocumentID
                                   join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                                   join um in context.UnitOfMeasures on pm.UnitOfMeasureID equals um.UnitOfMeasureID
                                   join d in context.InvDepartments on pm.DepartmentID equals d.InvDepartmentID
                                   join c in context.InvCategories on pm.CategoryID equals c.InvCategoryID
                                   join s in context.Suppliers on pm.SupplierID equals s.SupplierID
                                   join l in context.Locations on ph.LocationID equals l.LocationID
                                   where ph.DocumentStatus == 1
                                   group new { ph, pd, pm, um, s } by new
                                   {
                                       pd.ProductID,
                                       ph.DocumentNo,
                                       pm.ProductCode,
                                       pm.BarCode,
                                       pm.ProductName,
                                       um.UnitOfMeasureName,
                                       s.SupplierCode,
                                       s.SupplierName,
                                       pd.CostPrice
                                   }
                                       into g
                                       select new
                                       {
                                           FieldString1 = g.Key.DocumentNo,
                                           FieldString2 = g.Key.ProductCode,
                                           FieldString3 = g.Key.BarCode,
                                           FieldString4 = g.Key.ProductName,
                                           FieldString5 = g.Key.UnitOfMeasureName,
                                           FieldString6 = g.Key.SupplierCode,
                                           FieldString7 = g.Key.SupplierName,
                                           FieldDecimal1 = g.Sum(x => x.pd.Qty),
                                           FieldDecimal2 = g.Key.CostPrice,
                                           //FieldDecimal3 = g.Sum(x => x.pd.DiscountPercentage),
                                           FieldDecimal3 = g.Sum(x => x.pd.DiscountAmount),
                                           FieldDecimal4 = g.Sum(x => x.pd.NetAmount),

                                       }).ToArray();

                return queryResult.ToDataTable();
            }
        }

        public DataTable GetSupplierSalesDetailsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                StringBuilder querrySelect = new StringBuilder();
                var query = context.InvPurchaseHeaders.Where("DocumentStatus=1");

                foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
                {
                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
                }

                //(this IQueryable outer, IEnumerable inner, string outerSelector, string innerSelector, string resultsSelector, params object[] values)
                //var newq = query.Join("c", querrSupplier  ,"g","c.key.grp","g.key","new(g.key as grp,c.key.cat,new(c.count()*100/g.count() as percent) as agg)");

                // Set fields to be selected
                foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
                { querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", "); }

                // Remove last two charators (", ")
                querrySelect.Remove(querrySelect.Length - 2, 2);
                //string st = querrySelect.ToString();
                //var v = query.AsQueryable().Select("new(" + querrySelect.ToString() + ")");


                var queryResult = (from ph in query
                                   join pd in context.InvPurchaseDetails on ph.DocumentID equals pd.DocumentID
                                   join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                                   //join bn in context.InvProductBatchNoExpiaryDetails on pd.ProductID equals bn.ProductID
                                   join um in context.UnitOfMeasures on pm.UnitOfMeasureID equals um.UnitOfMeasureID
                                   join d in context.InvDepartments on pm.DepartmentID equals d.InvDepartmentID
                                   join c in context.InvCategories on pm.CategoryID equals c.InvCategoryID
                                   join s in context.Suppliers on pm.SupplierID equals s.SupplierID
                                   join l in context.Locations on ph.LocationID equals l.LocationID
                                   where ph.DocumentStatus == 1
                                   group new { ph, pd, pm, um, s } by new
                                   {
                                       pd.ProductID,
                                       ph.DocumentNo,
                                       pm.ProductCode,
                                       pm.BarCode,
                                       pm.ProductName,
                                       um.UnitOfMeasureName,
                                       s.SupplierCode,
                                       s.SupplierName,
                                       pd.CostPrice
                                   }
                                       into g
                                       select new
                                       {
                                           FieldString1 = g.Key.DocumentNo,
                                           FieldString2 = g.Key.ProductCode,
                                           FieldString3 = g.Key.BarCode,
                                           FieldString4 = g.Key.ProductName,
                                           FieldString5 = g.Key.UnitOfMeasureName,
                                           FieldString6 = g.Key.SupplierCode,
                                           FieldString7 = g.Key.SupplierName,
                                           FieldDecimal1 = g.Sum(x => x.pd.Qty),
                                           FieldDecimal2 = g.Key.CostPrice,
                                           //FieldDecimal3 = g.Sum(x => x.pd.DiscountPercentage),
                                           FieldDecimal3 = g.Sum(x => x.pd.DiscountAmount),
                                           FieldDecimal4 = g.Sum(x => x.pd.NetAmount),

                                       }).ToArray();

                return queryResult.ToDataTable();
            }
        }
    
        public DataTable GetInvoicesDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                StringBuilder selectFields = new StringBuilder();
                var query = context.InvSalesHeaders.Where("DocumentStatus = @0 AND DocumentID =@1", 1, autoGenerateInfo.DocumentID);

                // Insert Conditions to query
                foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
                {
                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
                }


                var queryResult = (from h in query
                                   join c in context.Customers on h.CustomerID equals c.CustomerID
                                   join l in context.Locations on h.LocationID equals l.LocationID
                                   join sp in context.InvSalesPersons on h.SalesPersonID equals sp.InvSalesPersonID
                                   select
                                       new
                                       {
                                           FieldString1 = h.DocumentNo,
                                           FieldString2 = EntityFunctions.TruncateTime(h.DocumentDate),
                                           FieldString3 = c.CustomerName,
                                           FieldString4 = sp.SalesPersonName,
                                           FieldString5 = h.ReferenceNo,
                                           FieldString6 = h.Remark,
                                           //FieldString6 = l.LocationName,
                                           FieldDecimal1 = h.NetAmount,
                                           FieldDecimal2 = h.GrossAmount,
                                           FieldDecimal3 = h.DiscountPercentage,
                                           FieldDecimal4 = h.DiscountAmount,
                                           FieldDecimal5 = h.TaxAmount
                                       }).ToArray();

                return queryResult.ToDataTable();
            }
        }

        public ArrayList GetSelectionSalesDataSummary(Common.ReportDataStruct reportDataStruct)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                #region old code (Dynamic linq)
                //IQueryable qryResult = context.InvSales.AsNoTracking().Where("DocumentStatus == @0", 1);
                //switch (reportDataStruct.DbColumnName.Trim())
                //{
                //    case "LocationID":
                //        qryResult = qryResult.Join(context.Locations, reportDataStruct.DbColumnName.Trim(),
                //                                   reportDataStruct.DbColumnName.Trim(),
                //                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                //                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                //                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                //                             .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                //                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                //        break;

                //    default:
                //        qryResult = qryResult.GroupBy("new(" + reportDataStruct.DbColumnName + ")", "new(" + reportDataStruct.DbColumnName + ")").OrderBy("key." + reportDataStruct.DbColumnName + "").Select("key." + reportDataStruct.DbColumnName + "");
                //        break;
                //} 
                #endregion

                var qryResult = (dynamic)null;// context.InvSales.AsNoTracking().Where("DocumentStatus == @0", 1);
                switch (reportDataStruct.DbColumnName)
                {
                    case "LocationName":
                        LocationService locationService = new LocationService();
                        qryResult = locationService.GetAllInventoryLocationNames();
                        break;
                    case "DepartmentName":
                        InvDepartmentService invDepartmentService = new InvDepartmentService();
                        qryResult = invDepartmentService.GetAllDepartmentNames();
                        break;
                    case "CategoryName":
                        InvCategoryService invCategoryService = new InvCategoryService();
                        qryResult = invCategoryService.GetAllCategoryNames();
                        break;
                    case "SubCategoryName":
                        InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                        qryResult = invSubCategoryService.GetAllSubCategoryNames();
                        break;
                    case "SubCategory2Name":
                        InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                        qryResult = invSubCategory2Service.GetInvSubCategory2Names();
                        break;
                    case "ProductCode":
                        InvProductMasterService invProductMasterService = new InvProductMasterService();
                        qryResult = invProductMasterService.GetAllProductCodes();
                        break;
                    case "ProductName":
                        InvProductMasterService invProductMasterService2 = new InvProductMasterService();
                        qryResult = invProductMasterService2.GetAllProductNames();
                        break;
                    case "SupplierName":
                        SupplierService supplierService = new SupplierService();
                        qryResult = supplierService.GetAllSupplierNames();
                        break;
                    case "DocumentID":
                    case "DocumentNo":
                    case "CustomerType":
                    case "CustomerCode":
                    case "CreatedUser":
                        IQueryable qryResult2 = context.InvSales.AsNoTracking().Where("DocumentStatus == @0", 1);   
                        qryResult = qryResult2.GroupBy("new(" + reportDataStruct.DbColumnName + ")", "new(" + reportDataStruct.DbColumnName + ")").OrderBy("key." + reportDataStruct.DbColumnName + "").Select("key." + reportDataStruct.DbColumnName + "");
                        break;

                    default:
                        break;
                }

                // change this code to get rid of the foreach loop
                ArrayList selectionDataList = new ArrayList();
                //string st = qryResult.ToString();
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
                    if (reportDataStruct.DbColumnName.Trim() == "DocumentID")
                    {
                        foreach (var item in qryResult)
                        {
                            if (item.Equals((int)SaleTypes.Sales))
                            { selectionDataList.Add(SaleTypes.Sales.ToString()); }
                            else if (item.Equals((int)SaleTypes.Returns))
                            { selectionDataList.Add(SaleTypes.Returns.ToString()); }
                            else if (item.Equals((int)SaleTypes.DepartmentSales))
                            { selectionDataList.Add("Department Sales"); }
                            else if (item.Equals((int)SaleTypes.DepartmentReturns))
                            { selectionDataList.Add("Department Returns"); }
                        }
                    }
                    else if (reportDataStruct.DbColumnName.Trim() == "CustomerType")
                    {
                        foreach (var item in qryResult)
                        {
                            if (item.Equals((int)CustomerTypes.Normal))
                            { selectionDataList.Add(CustomerTypes.Normal.ToString()); }
                            else if (item.Equals((int)CustomerTypes.Staff))
                            { selectionDataList.Add(CustomerTypes.Staff.ToString()); }
                            else if (item.Equals((int)CustomerTypes.Loyalty))
                            { selectionDataList.Add(CustomerTypes.Loyalty.ToString()); }
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
                else
                {
                    foreach (var item in qryResult)
                    {
                        if (!string.IsNullOrEmpty(item.ToString().Trim()))
                        { selectionDataList.Add(item.ToString().Trim()); }
                    }
                }
                return selectionDataList;
            }
        }



        public DataTable GetSalesDetailsSummaryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {
            using (ERPDbContext context = new ERPDbContext())
            {

                StringBuilder querrySelect = new StringBuilder();
                var query = context.InvSales.Where("DocumentStatus=1");

                foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
                {
                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
                }


                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();

                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmInvoice");
                var purchaseDocumentID = autoGenerateInfo.DocumentID;

                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSalesReturnNote");
                var purchaseReturnDocumentID = autoGenerateInfo.DocumentID;

                // Set fields to be selected
                foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
                { querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", "); }

                // Remove last two charators (", ")
                querrySelect.Remove(querrySelect.Length - 2, 2);

                var queryResult = (from ph in query
                                   join pd in context.InvProductExtendedPropertyValues on ph.ProductID equals pd.ProductID
                                   where ph.DocumentStatus == 1 && ph.IsDelete == false
                                   group new { ph, pd } by new
                                   {
                                       ph.DocumentDate,
                                       ph.ProductCode,
                                       ph.BarCode,
                                       ph.ProductName,
                                       ph.UnitOfMeasureName,
                                       ph.CostPrice,
                                       ph.DocumentID,
                                       ph.DepartmentCode,
                                       ph.DepartmentName,
                                       ph.CategoryCode,
                                       ph.CategoryName,
                                       ph.SubCategoryCode,
                                       ph.SubCategoryName,
                                       ph.SubCategory2Code,
                                       ph.SubCategory2Name,
                                       ph.LocationCode,
                                       ph.LocationName,
                                       ph.DocumentNo,
                                       ph.SupplierCode,
                                       ph.SupplierName,
                                   }
                                       into g
                                       select new
                                       {
                                           FieldString1 = g.Key.DocumentNo,
                                           FieldString2 = g.Key.DocumentDate,
                                           FieldString3 = g.Key.ProductCode,
                                           FieldString4 = g.Key.BarCode,
                                           FieldString5 = g.Key.ProductName,
                                           FieldString6 = g.Key.UnitOfMeasureName,
                                           FieldString7 = g.Key.DepartmentCode,
                                           FieldString8 = g.Key.DepartmentName,
                                           FieldString9 = g.Key.CategoryCode,
                                           FieldString10 = g.Key.CategoryName,
                                           FieldString11 = g.Key.SubCategoryCode,
                                           FieldString12 = g.Key.SubCategoryName,
                                           FieldString13 = g.Key.LocationCode,
                                           FieldString14 = g.Key.LocationName,
                                           FieldString15 = g.Key.SupplierCode,
                                           FieldString16 = g.Key.SupplierName,
                                           //FieldDecimal1 = g.Sum(x => x.ph.Qty),
                                           //FieldDecimal2 = g.Sum(x => x.ph.Qty),
                                           FieldDecimal1 = (g.Key.DocumentID == purchaseDocumentID ? g.Sum(x => x.ph.Qty) : 0),
                                           FieldDecimal2 = (g.Key.DocumentID == purchaseReturnDocumentID ? g.Sum(x => x.ph.Qty) : 0),
                                           FieldDecimal3 = g.Key.CostPrice,
                                           FieldDecimal4 = g.Sum(x => x.ph.DiscountAmount),
                                           FieldDecimal5 = g.Sum(x => x.ph.NetAmount),

                                       }).ToArray();

                return queryResult.ToDataTable();
            }
        }        


        public DataTable GetSalesRegisterDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            try
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    var query = context.InvSales.AsNoTracking().Where("DocumentStatus=@0 AND (DocumentID=@1 OR DocumentID=@2 OR DocumentID=@3 OR DocumentID=@4)", 1, 1, 2, 3, 4);// AND DocumentID=@1 AND DocumentID=@1", 1,1,3);
                    
                    foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
                    {
                        if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                        { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                        if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                        { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()).AddDays(1).AddSeconds(-1)); }

                        if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                        { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                        if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                        { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                        if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                        { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }

                        if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                        {
                            switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                            {
                                case "LocationID":
                                    query = (from qr in query
                                             join jt in context.Locations.Where(
                                                 "" +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " >= " + "@0 AND " +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " <= @1",
                                                 (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                 (reportConditionsDataStruct.ConditionTo.Trim())
                                                 ) on qr.LocationID equals jt.LocationID
                                             select qr
                                        );
                                    break;
                                case "DocumentID":
                                    int selectedFromStatus = 0, selectedToStatus = 0;

                                    if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.Sales.ToString()))
                                    { selectedFromStatus = (int)SaleTypes.Sales; }
                                    if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.Returns.ToString()))
                                    { selectedFromStatus = (int)SaleTypes.Returns; }
                                    if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.DepartmentSales.ToString()))
                                    { selectedFromStatus = (int)SaleTypes.DepartmentSales; }
                                    if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.DepartmentReturns.ToString()))
                                    { selectedFromStatus = (int)SaleTypes.DepartmentReturns; }

                                    if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.Sales.ToString()))
                                    { selectedToStatus = (int)SaleTypes.Sales; }
                                    if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.Returns.ToString()))
                                    { selectedToStatus = (int)SaleTypes.Returns; }
                                    if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.DepartmentSales.ToString()))
                                    { selectedToStatus = (int)SaleTypes.DepartmentSales; }
                                    if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.DepartmentReturns.ToString()))
                                    { selectedToStatus = (int)SaleTypes.DepartmentReturns; }

                                    query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", (selectedFromStatus), (selectedToStatus));

                                    break;
                                case "CustomerType":
                                    int selectedFromCustomerTypeStatus = 0, selectedToCustomerTypeStatus = 0;

                                    if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Normal.ToString()))
                                    { selectedFromCustomerTypeStatus = (int)CustomerTypes.Normal; }
                                    if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Staff.ToString()))
                                    { selectedFromCustomerTypeStatus = (int)CustomerTypes.Staff; }
                                    if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Loyalty.ToString()))
                                    { selectedFromCustomerTypeStatus = (int)CustomerTypes.Loyalty; }

                                    if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Normal.ToString()))
                                    { selectedToCustomerTypeStatus = (int)CustomerTypes.Normal; }
                                    if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Staff.ToString()))
                                    { selectedToCustomerTypeStatus = (int)CustomerTypes.Staff; }
                                    if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Loyalty.ToString()))
                                    { selectedToCustomerTypeStatus = (int)CustomerTypes.Loyalty; }

                                    query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", (selectedFromCustomerTypeStatus), (selectedToCustomerTypeStatus));

                                    break;
                                default:
                                    break;
                            }

                        }
                    }

                    var result = query.Select(ph =>
                                           new
                                           {
                                               FieldString1 = ph.ProductCode,
                                               FieldString2 = ph.ProductName,
                                               FieldString3 = ph.LocationCode + " " + ph.LocationName,
                                               FieldString4 = ph.DepartmentCode + " " + ph.DepartmentName,
                                               FieldString5 = ph.CategoryCode + " " + ph.CategoryName,
                                               FieldString6 = ph.SubCategoryCode + " " + ph.SubCategoryName,
                                               FieldString7 = ph.SubCategory2Code + " " + ph.SubCategory2Name,
                                               FieldString8 = (ph.DocumentID == 1 ? "SL" : (ph.DocumentID == 2 ? "RT" : (ph.DocumentID == 3 ? "DSL" : (ph.DocumentID == 4 ? "DRT" : "")))), // SL-Sales, RT-Sales Returns, DSL-Department Sales, DRT-Department Returns
                                               
                                               FieldString9 = ph.DocumentNo,
                                               FieldString10 = ph.DocumentDate,//.ToShortDateString(),
                                               //FieldString9 = ph.TerminalNo,
                                               FieldString11 = (ph.CustomerType == 1 ? "Normal" : (ph.CustomerType == 2 ? "Staff" : (ph.CustomerType == 3 ? "Loyalty" : ""))),
                                               FieldString12 = (ph.CustomerType == 2 ? (from em in context.Employees
                                                                                        where em.EmployeeID == ph.CustomerID
                                                                                        select em.EmployeeCode + " " + em.EmployeeName).FirstOrDefault() :
                                                                                   (ph.CustomerType == 3 ? (from lc in context.LoyaltyCustomers
                                                                                                            where lc.LoyaltyCustomerID == ph.CustomerID
                                                                                                            select lc.CustomerCode + " " + lc.CustomerName).FirstOrDefault() : "")),
                                               FieldString13 = ph.CreatedUser,
                                               FieldString14 = ph.SupplierCode + " " + ph.SupplierName,

                                               FieldDecimal1 = ph.Qty, //(ph.DocumentID == 1 ? ph.Qty : (ph.DocumentID == 2 ? -(ph.Qty) : (ph.DocumentID == 3 ? ph.Qty : (ph.DocumentID == 4 ? -(ph.Qty) : 0)))),
                                               FieldDecimal2 = ph.GrossAmount, //(ph.DocumentID == 1 ? ph.GrossAmount : (ph.DocumentID == 2 ? -(ph.GrossAmount) : (ph.DocumentID == 3 ? ph.GrossAmount : (ph.DocumentID == 4 ? -(ph.GrossAmount) : 0)))),
                                               FieldDecimal3 = ph.DiscountAmount, //(ph.DocumentID == 1 ? ph.DiscountAmount : (ph.DocumentID == 2 ? -(ph.DiscountAmount) : (ph.DocumentID == 3 ? ph.DiscountAmount : (ph.DocumentID == 4 ? -(ph.DiscountAmount) : 0)))),
                                               FieldDecimal4 = ph.SubTotalDiscountAmount,//(ph.DocumentID == 1 ? ph.SubTotalDiscountAmount : (ph.DocumentID == 2 ? -(ph.SubTotalDiscountAmount) : (ph.DocumentID == 3 ? ph.SubTotalDiscountAmount : (ph.DocumentID == 4 ? -(ph.SubTotalDiscountAmount) : 0)))),
                                               FieldDecimal5 = ph.NetAmount, // - ph.SubTotalDiscountAmount) //(ph.DocumentID == 1 ? (ph.NetAmount - ph.SubTotalDiscountAmount) : (ph.DocumentID == 2 ? (-(ph.NetAmount) - ph.SubTotalDiscountAmount) : (ph.DocumentID == 3 ? (ph.NetAmount - ph.SubTotalDiscountAmount) : (ph.DocumentID == 4 ? (-(ph.NetAmount) - ph.SubTotalDiscountAmount) : 0)))),
                                               //FieldDecimal5 = ph.SellingPrice,
                                               //FieldDecimal6 = ph.CostPrice
                                           });

                    DataTable dtQueryResult = new DataTable();

                    if (!reportGroupDataStructList.Any(g=> g.IsResultGroupBy)) // if no any groups
                    {
                        StringBuilder sbOrderByColumns = new StringBuilder();

                        if (reportOrderByDataStructList.Any(g => g.IsResultOrderBy))
                        {
                            foreach (var item in reportOrderByDataStructList.Where(g => g.IsResultOrderBy.Equals(true)))
                            { sbOrderByColumns.Append("" + item.ReportField + " , "); }

                            sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                            dtQueryResult = result.OrderBy(sbOrderByColumns.ToString())
                                                       .ToDataTable();
                        }
                        else
                        { dtQueryResult = result.ToDataTable(); }

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

                        StringBuilder sbGroupByColumns = new StringBuilder(); // Group by columns
                        StringBuilder sbOrderByColumns = new StringBuilder(); // Order by Columns
                        StringBuilder sbSelectColumns = new StringBuilder(); // Selected Columns
                        
                        sbGroupByColumns.Append("new(");
                        sbSelectColumns.Append("new(");

                        foreach (var item in reportGroupDataStructList.Where(g=> g.IsResultGroupBy.Equals(true)))
                        {
                            sbGroupByColumns.Append("it." + item.ReportField + " , ");
                            sbSelectColumns.Append("Key." + item.ReportField + " as " + item.ReportField + " , ");

                            sbOrderByColumns.Append("Key." + item.ReportField + " , ");
                        }

                        foreach (var item in reportDataStructList.Where(d => d.ReportDataType.Equals(typeof(decimal)) && d.IsColumnTotal.Equals(true)))
                        {
                            sbSelectColumns.Append("SUM(" + item.ReportField + ") as " + item.ReportField + " , ");
                        }

                        sbGroupByColumns.Remove(sbGroupByColumns.Length - 2, 2); // remove last ','
                        sbGroupByColumns.Append(")");

                        sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                        sbSelectColumns.Remove(sbSelectColumns.Length - 2, 2); // remove last ','
                        sbSelectColumns.Append(")");

                        var groupByResult = result.GroupBy(sbGroupByColumns.ToString(), "it")
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

                        foreach (var item in reportDataStructList.Where(d => d.ReportDataType.Equals(typeof(decimal)) && d.IsColumnTotal.Equals(true)))
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

                    #region sample group
                    //var xx = (from p in result
                    //          let g = new
                    //          {
                    //              FieldString1 = p.FieldString1,
                    //          }
                    //          group p by g into t
                    //          select new
                    //          {
                    //              FieldString1 = t.Key.FieldString1,
                    //              FieldDecimal1 = t.Sum(p => p.FieldDecimal1),
                    //              FieldDecimal2 = t.Sum(p => p.FieldDecimal2),
                    //              FieldDecimal3 = t.Sum(p => p.FieldDecimal3),
                    //              FieldDecimal4 = t.Sum(p => p.FieldDecimal4)
                    //          });//.ToString(); 
                    #endregion

                    #region ParameterExpression
                    //string columnToGroupBy = "LocationCode";

                    //// generate the dynamic Expression<Func<Product, string>>
                    //ParameterExpression pp = Expression.Parameter(typeof(InvSales), "pp");

                    //var selector = Expression.Lambda<Func<InvSales, string>>(Expression.Property(pp, columnToGroupBy), pp);

                    //var lll = context.InvSales
                    //                       .GroupBy(selector)
                    //                       .Select((group) => new
                    //                       {
                    //                           Key = group.Key,
                    //                           Count = group.Count()
                    //                       });

                    #endregion

                    return dtQueryResult;
                    
                }
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

        //public DataTable GetSalesRegisterDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        //{
        //    try
        //    {

        //        using (ERPDbContext context = new ERPDbContext())
        //        {
        //            var query = context.InvSales.AsNoTracking().Where("DocumentStatus=@0 AND (DocumentID=@1 OR DocumentID=@2 OR DocumentID=@3 OR DocumentID=@4)", 1, 1, 2, 3, 4);// AND DocumentID=@1 AND DocumentID=@1", 1,1,3);
                    
        //            StringBuilder sbWhere = new StringBuilder();

        //            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
        //            {
        //                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
        //                {
        //                    sbWhere.Append( string.IsNullOrEmpty(sbWhere.ToString().Trim()) ? "WHERE " : " AND ");
        //                    sbWhere.Append(reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= N'" + reportConditionsDataStruct.ConditionFrom.Trim() + "' AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= N'" + reportConditionsDataStruct.ConditionTo.Trim() + "'");
        //                    query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); 
        //                }

        //                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
        //                {
        //                    DateTime dttt = DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim());
        //                    sbWhere.Append(string.IsNullOrEmpty(sbWhere.ToString().Trim()) ? "WHERE " : " AND ");
        //                    //sbWhere.Append(reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= convert(datetime2, '" + DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()) + "', 121) AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= convert(datetime2, '" + DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()).AddDays(1).AddSeconds(-1) + "', 121)");
        //                    sbWhere.Append(reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " BETWEEN '" + DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()) + "' AND '" + DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
        //                    query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()).AddDays(1).AddSeconds(-1)); 
        //                }

        //                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
        //                {
        //                    sbWhere.Append(string.IsNullOrEmpty(sbWhere.ToString().Trim()) ? "WHERE " : " AND ");
        //                    sbWhere.Append(reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= cast(" + decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()) + " as decimal(18)) AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= cast(" + decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim()) + " as decimal(18)) ");
        //                    query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); 
        //                }

        //                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
        //                {
        //                    query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); 
        //                }

        //                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
        //                {
        //                    query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); 
        //                }

        //                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
        //                {
        //                    switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
        //                    {
        //                        case "LocationID":
        //                            query = (from qr in query
        //                                     join jt in context.Locations.Where(
        //                                         "" +
        //                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
        //                                         " >= " + "@0 AND " +
        //                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
        //                                         " <= @1",
        //                                         (reportConditionsDataStruct.ConditionFrom.Trim()),
        //                                         (reportConditionsDataStruct.ConditionTo.Trim())
        //                                         ) on qr.LocationID equals jt.LocationID
        //                                     select qr
        //                                );
        //                            break;
        //                        case "DocumentID":
        //                            int selectedFromStatus = 0, selectedToStatus = 0;

        //                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.Sales.ToString()))
        //                            { selectedFromStatus = (int)SaleTypes.Sales; }
        //                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.Returns.ToString()))
        //                            { selectedFromStatus = (int)SaleTypes.Returns; }
        //                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.DepartmentSales.ToString()))
        //                            { selectedFromStatus = (int)SaleTypes.DepartmentSales; }
        //                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.DepartmentReturns.ToString()))
        //                            { selectedFromStatus = (int)SaleTypes.DepartmentReturns; }

        //                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.Sales.ToString()))
        //                            { selectedToStatus = (int)SaleTypes.Sales; }
        //                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.Returns.ToString()))
        //                            { selectedToStatus = (int)SaleTypes.Returns; }
        //                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.DepartmentSales.ToString()))
        //                            { selectedToStatus = (int)SaleTypes.DepartmentSales; }
        //                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.DepartmentReturns.ToString()))
        //                            { selectedToStatus = (int)SaleTypes.DepartmentReturns; }

        //                            query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", (selectedFromStatus), (selectedToStatus));

        //                            break;
        //                        case "CustomerType":
        //                            int selectedFromCustomerTypeStatus = 0, selectedToCustomerTypeStatus = 0;

        //                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Normal.ToString()))
        //                            { selectedFromCustomerTypeStatus = (int)CustomerTypes.Normal; }
        //                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Staff.ToString()))
        //                            { selectedFromCustomerTypeStatus = (int)CustomerTypes.Staff; }
        //                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Loyalty.ToString()))
        //                            { selectedFromCustomerTypeStatus = (int)CustomerTypes.Loyalty; }

        //                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Normal.ToString()))
        //                            { selectedToCustomerTypeStatus = (int)CustomerTypes.Normal; }
        //                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Staff.ToString()))
        //                            { selectedToCustomerTypeStatus = (int)CustomerTypes.Staff; }
        //                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Loyalty.ToString()))
        //                            { selectedToCustomerTypeStatus = (int)CustomerTypes.Loyalty; }

        //                            query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", (selectedFromCustomerTypeStatus), (selectedToCustomerTypeStatus));

        //                            break;
        //                        default:
        //                            break;
        //                    }

        //                }
        //            }

        //            string st = query.ToString();
        //            StringBuilder querrySelect = new StringBuilder();
        //            // Set fields to be selected
        //            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
        //            {
        //                if (reportDataStruct.IsSelectionField)
        //                {
        //                    querrySelect.Append((reportDataStruct.IsJoinField ? reportDataStruct.DbJoinColumnName.Trim(): reportDataStruct.DbColumnName.Trim()) + " AS " + reportDataStruct.ReportField.Trim() + ", ");
        //                }
        //            }

        //            // Remove last two charators (", ")
        //            querrySelect.Remove(querrySelect.Length - 2, 2);
        //            var xx = query.Select("new(" + querrySelect.ToString() + ")");
        //            //var xxx = query.Select("(new (" + querrySelect.ToString() + "))");
        //            string ccc = xx.ToString();
        //            //string ccc = xxx.ToString();

        //            //#region dynamic select
        //            ////var selectedqry = query.Select("new(" + selectFields.ToString() + ")");
        //            ////var selectedqry2 = query.Select("(new " + selectFields.ToString() + ")");
        //            //#endregion

        //            var queryResult = query;//.AsEnumerable();
        //            //string st = queryResult.ToString();
        //            var st2 = queryResult.Select(ph =>
        //                              new
        //                              {
        //                                  ph.LocationCode,
        //                                  ph.LocationName,
        //                                  ph.DepartmentCode,
        //                                  ph.DepartmentName,
        //                                  ph.CategoryCode,
        //                                  ph.CategoryName,
        //                                  ph.SubCategoryCode,
        //                                  ph.SubCategoryName,
        //                                  ph.SubCategory2Code,
        //                                  ph.SubCategory2Name,
        //                                  SaleType = (ph.DocumentID == 1 ? "SL" : (ph.DocumentID == 2 ? "RT" : (ph.DocumentID == 3 ? "DSL" : (ph.DocumentID == 4 ? "DRT" : "")))), // SL-Sales, RT-Sales Returns, DSL-Department Sales, DRT-Department Returns
        //                                  ph.ProductCode,
        //                                  ph.ProductName,
        //                                  ph.DocumentNo,
        //                                  ph.DocumentDate,
        //                                  //FieldString9 = reportDataStructList[8].IsSelectionField == true ? ph.TerminalNo : "",
        //                                  CustomerType = (ph.CustomerType == 1 ? "Normal" : (ph.CustomerType == 2 ? "Staff" : (ph.CustomerType == 3 ? "Loyalty" : ""))),
        //                                  Customer = (ph.CustomerType == 2 ? (from em in context.Employees
        //                                                                      where em.EmployeeID == ph.CustomerID
        //                                                                      select em.EmployeeCode + " " + em.EmployeeName).FirstOrDefault() :
        //                                                                            (ph.CustomerType == 3 ? (from lc in context.LoyaltyCustomers
        //                                                                                                     where lc.LoyaltyCustomerID == ph.CustomerID
        //                                                                                                     select lc.CustomerCode + " " + lc.CustomerName).FirstOrDefault() : "")),
        //                                  ph.CreatedUser,

        //                                  Qty = (ph.DocumentID == 1 ? ph.Qty : (ph.DocumentID == 2 ? -(ph.Qty) : (ph.DocumentID == 3 ? ph.Qty : (ph.DocumentID == 4 ? -(ph.Qty) : 0)))),
        //                                  GrossAmount = (ph.DocumentID == 1 ? ph.GrossAmount : (ph.DocumentID == 2 ? -(ph.GrossAmount) : (ph.DocumentID == 3 ? ph.GrossAmount : (ph.DocumentID == 4 ? -(ph.GrossAmount) : 0)))),
        //                                  DiscountAmount = (ph.DocumentID == 1 ? ph.DiscountAmount : (ph.DocumentID == 2 ? -(ph.DiscountAmount) : (ph.DocumentID == 3 ? ph.DiscountAmount : (ph.DocumentID == 4 ? -(ph.DiscountAmount) : 0)))),
        //                                  SubTotalDiscountAmount = (ph.DocumentID == 1 ? ph.SubTotalDiscountAmount : (ph.DocumentID == 2 ? -(ph.SubTotalDiscountAmount) : (ph.DocumentID == 3 ? ph.SubTotalDiscountAmount : (ph.DocumentID == 4 ? -(ph.SubTotalDiscountAmount) : 0)))),
        //                                  NetAmount = (ph.DocumentID == 1 ? (ph.NetAmount - ph.SubTotalDiscountAmount) : (ph.DocumentID == 2 ? (-(ph.NetAmount) - ph.SubTotalDiscountAmount) : (ph.DocumentID == 3 ? (ph.NetAmount - ph.SubTotalDiscountAmount) : (ph.DocumentID == 4 ? (-(ph.NetAmount) - ph.SubTotalDiscountAmount) : 0)))),
        //                                  //ph.SellingPrice
        //                                  //ph.CostPrice
        //                              }
        //                              ).ToDataTable();// ToString();

        //            int x = st2.Rows.Count;
        //            DataTable dt = new DataTable();
        //            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SysConn"].ConnectionString);
        //            sqlConn.Open();
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.Connection = sqlConn;
        //            cmd.CommandText = xx.ToString();
        //            cmd.CommandType = CommandType.Text;

        //            cmd.CommandTimeout = 300;
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);

        //            //da.Fill(dt);
        //            //dt.TableName = "";

        //            var erer = queryResult.Select(ph => new { ph });
        //            string uuuu = erer.ToString();

        //            return null;
        //        }
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                              eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //                                  ve.PropertyName, ve.ErrorMessage);
        //            }
        //        }
        //        throw;
        //    }
        //} 

        public DataTable GetExtendedPropertySalesRegisterDataTable(AutoGenerateInfo autoGenerateInfo, DateTime dateFrom, DateTime dateTo, ArrayList productExtendedPropertyList, string productProperty, string[] productPropertyValuesList, string[] locationList, string[] suppliersList)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = context.InvSales.AsNoTracking().Where("DocumentStatus=@0 AND (DocumentID=@1 OR DocumentID=@2 OR DocumentID=@3 OR DocumentID=@4) AND DocumentDate >= @5 AND DocumentDate <= @6", 1, 1, 2, 3, 4, dateFrom, dateTo.AddDays(1).AddSeconds(-1));

                if (locationList.Length > 0)
                {
                    StringBuilder conditionFields = new StringBuilder();
                    for (int i = 0; i < locationList.Length; i++)
                    {
                        if (i < locationList.Length - 1)
                        { conditionFields.Append("LocationName=@" + i.ToString() + " OR "); }
                        else
                        { conditionFields.Append("LocationName=@" + i.ToString()); }
                    }

                    query = (from qr in query
                             join l in context.Locations.Where(
                                 conditionFields.ToString(), locationList)
                             on qr.LocationID equals l.LocationID
                             orderby qr.LocationName
                             select qr);
                }

                if (suppliersList.Length > 0)
                {
                    StringBuilder conditionFields = new StringBuilder();
                    for (int i = 0; i < suppliersList.Length; i++)
                    {
                        if (i < suppliersList.Length - 1)
                        { conditionFields.Append("SupplierName=@" + i.ToString() + " OR "); }
                        else
                        { conditionFields.Append("SupplierName=@" + i.ToString()); }
                    }

                    query = (from qr in query
                             join s in context.Suppliers.Where(
                                 conditionFields.ToString(), suppliersList)
                             on qr.SupplierID equals s.SupplierID
                             orderby qr.SupplierName
                             select qr);
                }

                string departmentText, categoryText, subCategoryText, subCategory2Text;
                departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
                categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
                subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
                subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;

                if (string.Equals(productProperty.Trim(), departmentText.Trim()))
                {
                    StringBuilder conditionFields = new StringBuilder();
                    // Create where condition
                    for (int i = 0; i < productPropertyValuesList.Length; i++)
                    {
                        if (i < productPropertyValuesList.Length - 1)
                        { conditionFields.Append("DepartmentName=@" + i.ToString() + " OR "); }
                        else
                        { conditionFields.Append("DepartmentName=@" + i.ToString()); }
                    }
                    query = query.Where(conditionFields.ToString(), productPropertyValuesList).OrderBy("DepartmentName");
                }
                else if (string.Equals(productProperty.Trim(), categoryText.Trim()))
                {
                    StringBuilder conditionFields = new StringBuilder();
                    // Create where condition
                    for (int i = 0; i < productPropertyValuesList.Length; i++)
                    {
                        if (i < productPropertyValuesList.Length - 1)
                        { conditionFields.Append("CategoryName=@" + i.ToString() + " OR "); }
                        else
                        { conditionFields.Append("CategoryName=@" + i.ToString()); }
                    }
                    query = query.Where(conditionFields.ToString(), productPropertyValuesList).OrderBy("CategoryName");
                }
                else if (string.Equals(productProperty.Trim(), subCategoryText.Trim()))
                {
                    StringBuilder conditionFields = new StringBuilder();
                    // Create where condition
                    for (int i = 0; i < productPropertyValuesList.Length; i++)
                    {
                        if (i < productPropertyValuesList.Length - 1)
                        { conditionFields.Append("SubCategoryName=@" + i.ToString() + " OR "); }
                        else
                        { conditionFields.Append("SubCategoryName=@" + i.ToString()); }
                    }
                    query = query.Where(conditionFields.ToString(), productPropertyValuesList).OrderBy("SubCategoryName");
                }
                else if (string.Equals(productProperty.Trim(), subCategory2Text.Trim()))
                {
                    StringBuilder conditionFields = new StringBuilder();
                    // Create where condition
                    for (int i = 0; i < productPropertyValuesList.Length; i++)
                    {
                        if (i < productPropertyValuesList.Length - 1)
                        { conditionFields.Append("SubCategory2Name=@" + i.ToString() + " OR "); }
                        else
                        { conditionFields.Append("SubCategory2Name=@" + i.ToString()); }
                    }
                    query = query.Where(conditionFields.ToString(), productPropertyValuesList).OrderBy("SubCategory2Name");
                }
                else
                {
                  
                }

                var resultQuery = query.AsEnumerable().Select(qr =>
                                    new {
                                        qr.CompanyCode,
                                        qr.CompanyName,
                                        qr.LocationCode,
                                        qr.LocationName,
                                        qr.CostCentreID,
                                        qr.DocumentID,
                                        qr.DocumentNo,
                                        qr.ReferenceNo,
                                        qr.DocumentDate,
                                        qr.TransactionTime,
                                        qr.CustomerCode,
                                        qr.CustomerName,
                                        qr.SupplierCode,
                                        qr.SupplierName,
                                        qr.SalesPersonCode,
                                        qr.SalesPersonName,
                                        GrossAmount = (qr.DocumentID == 1 ? qr.GrossAmount : (qr.DocumentID == 2 ? -qr.GrossAmount : (qr.DocumentID == 3 ? qr.GrossAmount : (qr.DocumentID == 4 ? -qr.GrossAmount : 0)))), // 1-Sales, 2-Sales Returns, 3-Department Sales, 3-Department Returns
                                        qr.DiscountPercentage,
                                        DiscountAmount = (qr.DocumentID == 1 ? qr.DiscountAmount : (qr.DocumentID == 2 ? -qr.DiscountAmount : (qr.DocumentID == 3 ? qr.DiscountAmount : (qr.DocumentID == 4 ? -qr.DiscountAmount : 0)))), // 1-Sales, 2-Sales Returns, 3-Department Sales, 3-Department Returns
                                        NetAmount = (qr.DocumentID == 1 ? qr.NetAmount : (qr.DocumentID == 2 ? -qr.NetAmount : (qr.DocumentID == 3 ? qr.NetAmount : (qr.DocumentID == 4 ? -qr.NetAmount : 0)))), // 1-Sales, 2-Sales Returns, 3-Department Sales, 3-Department Returns
                                        qr.SubTotalDiscountPercentage,
                                        qr.SubTotalDiscountAmount,
                                        qr.DepartmentCode,
                                        qr.DepartmentName,
                                        qr.CategoryCode,
                                        qr.CategoryName,
                                        qr.SubCategoryCode,
                                        qr.SubCategoryName,
                                        qr.SubCategory2Code,
                                        qr.SubCategory2Name,
                                        qr.ProductID,
                                        qr.ProductCode,
                                        qr.ProductName,
                                        qr.BarCode,
                                        qr.BatchNo,
                                        qr.ExpiryDate,
                                        Qty = (qr.DocumentID == 1 ? qr.Qty : (qr.DocumentID == 2 ? -qr.Qty : (qr.DocumentID == 3 ? qr.Qty : (qr.DocumentID == 4 ? -qr.Qty : 0)))), // 1-Sales, 2-Sales Returns, 3-Department Sales, 3-Department Returns
                                        qr.UnitOfMeasureName,
                                        qr.PackSize,
                                        qr.SellingPrice,
                                        qr.WholeSalePrice,
                                        qr.CostPrice,
                                        qr.AverageCost,
                                        qr.IsFreeIssue,
                                        qr.TerminalNo,
                                        qr.UnitNo,
                                        qr.CreatedUser,
                                        qr.CreatedDate,
                                        qr.CustomerType
                                    });
                DataTable dtResultQuery = resultQuery.ToDataTable();

                InvProductExtendedPropertyValueService invProductExtendedPropertyValueService = new InvProductExtendedPropertyValueService();
                List<InvProductExtendedPropertyValue> invProductExtendedPropertyValues = new List<InvProductExtendedPropertyValue>();
                invProductExtendedPropertyValues = invProductExtendedPropertyValueService.GetAllProductExtendedPropertyValues();
        
                // Insert Extended Property Columns
                for (int i = 0; i < productExtendedPropertyList.Count; i++)
                {
                    dtResultQuery.Columns.Add(productExtendedPropertyList[i].ToString(), typeof(string));
                }

                foreach (DataRow drProduct in dtResultQuery.Rows)
                {
                    List<InvProductExtendedPropertyValue> productExPropList = new List<InvProductExtendedPropertyValue>();
                    productExPropList = invProductExtendedPropertyValues.Where(p => p.ProductID.Equals(long.Parse(drProduct["ProductID"].ToString()))).ToList();

                    #region old code
                    //InvProductExtendedPropertyValueService invProductExtendedPropertyValueService = new InvProductExtendedPropertyValueService();
                    //List<InvProductExtendedPropertyValue> productExPropList = new List<InvProductExtendedPropertyValue>();
                    //productExPropList = invProductExtendedPropertyValueService.GetProductExtendedPropertyValuesByProductID(long.Parse(drProduct["ProductID"].ToString()));
                    #endregion

                    for (int i = 0; i < productExPropList.Count; i++)
                    {
                        if (dtResultQuery.Columns.Contains(productExPropList[i].ExtendedPropertyName.ToString().Trim()))
                        {
                            drProduct[productExPropList[i].ExtendedPropertyName.ToString().Trim()] = productExPropList[i].ValueData.ToString().Trim();
                        }
                    }
                   
                }

                // Reset Column Headers
                for (int i = 0; i < productExtendedPropertyList.Count; i++)
                {
                    dtResultQuery.Columns[(dtResultQuery.Columns.Count - productExtendedPropertyList.Count) + i].ColumnName = "Expr" + (i + 1);
                }
                return dtResultQuery;

                #region old code
                //foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
                //{
                //    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                //    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                //    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                //    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()).AddDays(1).AddSeconds(-1)); }

                //    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                //    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                //    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                //    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                //    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                //    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }

                //    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                //    {
                //        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                //        {
                //            case "LocationID":
                //                query = (from qr in query
                //                         join jt in context.Locations.Where(
                //                             "" +
                //                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                //                             " >= " + "@0 AND " +
                //                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                //                             " <= @1",
                //                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                //                             (reportConditionsDataStruct.ConditionTo.Trim())
                //                             ) on qr.LocationID equals jt.LocationID
                //                         select qr
                //                    );
                //                break;
                //            case "DocumentID":
                //                int selectedFromStatus = 0, selectedToStatus = 0;

                //                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.Sales.ToString()))
                //                { selectedFromStatus = (int)SaleTypes.Sales; }
                //                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.Returns.ToString()))
                //                { selectedFromStatus = (int)SaleTypes.Returns; }
                //                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.DepartmentSales.ToString()))
                //                { selectedFromStatus = (int)SaleTypes.DepartmentSales; }
                //                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.DepartmentReturns.ToString()))
                //                { selectedFromStatus = (int)SaleTypes.DepartmentReturns; }

                //                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.Sales.ToString()))
                //                { selectedToStatus = (int)SaleTypes.Sales; }
                //                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.Returns.ToString()))
                //                { selectedToStatus = (int)SaleTypes.Returns; }
                //                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.DepartmentSales.ToString()))
                //                { selectedToStatus = (int)SaleTypes.DepartmentSales; }
                //                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.DepartmentReturns.ToString()))
                //                { selectedToStatus = (int)SaleTypes.DepartmentReturns; }

                //                query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", (selectedFromStatus), (selectedToStatus));

                //                break;
                //            default:
                //                break;
                //        }

                //    }
                //}


                //var queryResult = query.AsEnumerable();
                //DataTable salesProducts = queryResult.Select(ph =>
                //                   new
                //                   {
                //                       FieldString1 = reportDataStructList[0].IsSelectionField == true ? ph.LocationCode + " " + ph.LocationName : "",
                //                       FieldString2 = reportDataStructList[1].IsSelectionField == true ? ph.DepartmentCode + " " + ph.DepartmentName : "",
                //                       FieldString3 = reportDataStructList[2].IsSelectionField == true ? ph.CategoryCode + " " + ph.CategoryName : "",
                //                       FieldString4 = reportDataStructList[3].IsSelectionField == true ? ph.SubCategoryCode + " " + ph.SubCategoryName : "",
                //                       FieldString5 = reportDataStructList[4].IsSelectionField == true ? ph.SubCategory2Code + " " + ph.SubCategory2Name : "",
                //                       FieldString6 = reportDataStructList[5].IsSelectionField == true ? (ph.DocumentID == 1 ? "SL" : (ph.DocumentID == 2 ? "RT" : (ph.DocumentID == 3 ? "DSL" : (ph.DocumentID == 4 ? "DRT" : "")))) : "", // SL-Sales, RT-Sales Returns, DSL-Department Sales, DRT-Department Returns
                //                       FieldString7 = reportDataStructList[6].IsSelectionField == true ? ph.ProductCode : "",
                //                       FieldString8 = reportDataStructList[7].IsSelectionField == true ? ph.ProductName : "",
                //                       FieldString9 = reportDataStructList[8].IsSelectionField == true ? ph.DocumentNo : "",
                //                       FieldString10 = reportDataStructList[9].IsSelectionField == true ? ph.DocumentDate.ToShortDateString() : "",
                //                       FieldString11 = ph.ProductID,
                //                       FieldString12 = "",
                //                       FieldString13 = "",
                //                       FieldString14 = "",
                //                       FieldString15 = "",
                //                       FieldString16 = "",
                //                       FieldString17 = "",
                //                       FieldString18 = "",
                //                       FieldString19 = "",
                //                       FieldString20 = "",
                //                       FieldString21 = "",
                //                       FieldString22 = "",
                //                       FieldString23 = "",
                //                       FieldString24 = "",
                //                       FieldString25 = "",
                //                       //FieldString26 = "",
                //                       //FieldString27 = "",
                //                       //FieldString28 = "",
                //                       //FieldString29 = "",
                //                       //FieldString30 = "",
                //                       FieldDecimal1 = reportDataStructList[10].IsSelectionField == true ? (ph.DocumentID == 1 ? ph.Qty : (ph.DocumentID == 2 ? -(ph.Qty) : (ph.DocumentID == 3 ? ph.Qty : (ph.DocumentID == 4 ? -(ph.Qty) : 0)))) : 0,
                //                       FieldDecimal2 = reportDataStructList[11].IsSelectionField == true ? (ph.DocumentID == 1 ? ph.GrossAmount : (ph.DocumentID == 2 ? -(ph.GrossAmount) : (ph.DocumentID == 3 ? ph.GrossAmount : (ph.DocumentID == 4 ? -(ph.GrossAmount) : 0)))) : 0,
                //                       FieldDecimal3 = reportDataStructList[12].IsSelectionField == true ? (ph.DocumentID == 1 ? ph.DiscountAmount : (ph.DocumentID == 2 ? -(ph.DiscountAmount) : (ph.DocumentID == 3 ? ph.DiscountAmount : (ph.DocumentID == 4 ? -(ph.DiscountAmount) : 0)))) : 0,
                //                       FieldDecimal4 = reportDataStructList[13].IsSelectionField == true ? (ph.DocumentID == 1 ? ph.NetAmount : (ph.DocumentID == 2 ? -(ph.NetAmount) : (ph.DocumentID == 3 ? ph.NetAmount : (ph.DocumentID == 4 ? -(ph.NetAmount) : 0)))) : 0,
                //                   }
                //                       ).ToArray().ToDataTable();


                //InvProductExtendedPropertyValueService invProductExtendedPropertyValueService = new InvProductExtendedPropertyValueService();
                //DataTable dtProdcutsExtendedPropsColumns = new DataTable();
                //dtProdcutsExtendedPropsColumns.Columns.Add("ColumnName", typeof(string));

                //int lastUpdatedColumnHeader = 11;
                //foreach (DataRow drProduct in salesProducts.Rows)
                //{
                //    List<InvProductExtendedPropertyValue> productExPropList = new List<InvProductExtendedPropertyValue>();
                //    productExPropList = invProductExtendedPropertyValueService.GetProductExtendedPropertyValuesByProductID(long.Parse(drProduct["FieldString11"].ToString()));

                //    // Set Column header
                //    for (int i = 0; i < productExPropList.Count; i++)
                //    {
                //        if (!salesProducts.Columns.Contains(productExPropList[i].ExtendedPropertyName.ToString().Trim()))
                //        {

                //            if (lastUpdatedColumnHeader < 25) // Because report has limited number(14) of fields for product extended properties
                //            {
                //                try
                //                {
                //                    salesProducts.Columns[lastUpdatedColumnHeader].ColumnName = productExPropList[i].ExtendedPropertyName.ToString().Trim();
                //                    dtProdcutsExtendedPropsColumns.Rows.Add(productExPropList[i].ExtendedPropertyName.ToString().Trim());
                //                    lastUpdatedColumnHeader++;
                //                }
                //                catch (Exception ex)
                //                {

                //                }
                //            }
                //            else
                //            {
                //            }
                //        }
                //        if (lastUpdatedColumnHeader < 25) // Because report has limited number(14) of fields for product extended properties
                //        {
                //            try
                //            {
                //                drProduct[productExPropList[i].ExtendedPropertyName.ToString().Trim()] = productExPropList[i].ValueData.ToString().Trim();
                //            }
                //            catch (Exception ex)
                //            {

                //            }
                //        }
                //    }
                //}

                //// Reset Column Headers
                //for (int i = 0; i < salesProducts.Columns.Count; i++)
                //{
                //    salesProducts.Columns[i].ColumnName = "FieldString" + (i + 1);
                //}

                //return salesProducts;
                #endregion

                //return null;
            }
        }


        public DataTable GetLocationSalesDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = context.InvSales.AsNoTracking().Where("DocumentStatus=@0 AND (DocumentID=@1 OR DocumentID=@2 OR DocumentID=@3 OR DocumentID=@4)", 1, 1, 2, 3, 4);

                foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
                {
                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
                }

                //var x = query.GroupBy("new(DbColumnName)", "new(DbColumnName)")
                //    .OrderBy("key.DbColumnName")
                //    .Select("key.DbColumnName"); 


                #region dynamic select

                //DataTable dt1 = new DataTable();
                //StringBuilder selectFields = new StringBuilder();
                ////// Set fields to be selected
                //foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
                //{
                //    selectFields.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", ");
                //    dt1.Columns.Add(reportDataStruct.DbColumnName.Trim());
                //}

                //// Remove last two charators (", ")
                //selectFields.Remove(selectFields.Length - 2, 2);
                //var selectedqry = query.Select("new(" + selectFields.ToString() + ")");
                //var selectedqry2 = query.Select("(new " + selectFields.ToString() + ")");

                //foreach (var item in selectedqry)
                //{
                //    string[] columnList = item.ToString().Split(',');
                //    dt1.Rows.Add(columnList);
                //}

                #endregion

                var queryResult = query.AsEnumerable();

                //var x = queryResult.Count();

                //return (from sl in queryResult
                //        join l in context.Locations on sl.LocationID equals l.LocationID
                //        select new
                //        {
                //            FieldString1 = reportDataStructList[0].IsSelectionField == true ? sl.LocationCode : "",
                //            FieldString2 = reportDataStructList[1].IsSelectionField == true ? l.LocationPrefixCode : "",
                //            FieldString3 = reportDataStructList[2].IsSelectionField == true ? sl.DepartmentCode + " " + sl.DepartmentName : "",
                //            FieldString4 = reportDataStructList[3].IsSelectionField == true ? sl.CategoryCode + " " + sl.CategoryName : "",
                //            FieldString5 = reportDataStructList[4].IsSelectionField == true ? sl.SubCategoryCode + " " + sl.SubCategoryName : "",
                //            FieldString6 = reportDataStructList[5].IsSelectionField == true ? sl.SubCategory2Code + " " + sl.SubCategory2Name : "",
                //            FieldString7 = reportDataStructList[6].IsSelectionField == true ? sl.ProductCode : "",
                //            FieldString8 = reportDataStructList[7].IsSelectionField == true ? sl.ProductName : "",
                //            FieldDecimal1 = reportDataStructList[8].IsSelectionField == true ? (sl.DocumentID == 1 ? sl.Qty : (sl.DocumentID == 2 ? -(sl.Qty) : (sl.DocumentID == 3 ? sl.Qty : (sl.DocumentID == 4 ? -(sl.Qty) : 0)))) : 0,
                //            //FieldDecimal2 = reportDataStructList[9].IsSelectionField == true ? sl.GrossAmount : 0,
                //            //FieldDecimal3 = reportDataStructList[10].IsSelectionField == true ? sl.DiscountAmount : 0,
                //            FieldDecimal2 = reportDataStructList[9].IsSelectionField == true ? (sl.DocumentID == 1 ? sl.NetAmount : (sl.DocumentID == 2 ? -(sl.NetAmount) : (sl.DocumentID == 3 ? sl.NetAmount : (sl.DocumentID == 4 ? -(sl.NetAmount) : 0)))) : 0
                //            //FieldDecimal5 = 0,
                //            //FieldDecimal6 = 0
                //        }
                //            );


                return (from sl in queryResult.AsEnumerable()
                        join l in context.Locations on sl.LocationID equals l.LocationID
                        select new
                        {
                            FieldString1 = reportDataStructList[0].IsSelectionField == true ? sl.LocationCode : "",
                            FieldString2 = reportDataStructList[1].IsSelectionField == true ? l.LocationPrefixCode : "",
                            FieldString3 = reportDataStructList[2].IsSelectionField == true ? sl.DepartmentCode + " " + sl.DepartmentName : "",
                            FieldString4 = reportDataStructList[3].IsSelectionField == true ? sl.CategoryCode + " " + sl.CategoryName : "",
                            FieldString5 = reportDataStructList[4].IsSelectionField == true ? sl.SubCategoryCode + " " + sl.SubCategoryName : "",
                            FieldString6 = reportDataStructList[5].IsSelectionField == true ? sl.SubCategory2Code + " " + sl.SubCategory2Name : "",
                            FieldString7 = reportDataStructList[6].IsSelectionField == true ? sl.ProductCode : "",
                            FieldString8 = reportDataStructList[7].IsSelectionField == true ? sl.ProductName : "",
                            FieldDecimal1 = reportDataStructList[8].IsSelectionField == true ? (sl.DocumentID == 1 ? sl.Qty : (sl.DocumentID == 2 ? -(sl.Qty) : (sl.DocumentID == 3 ? sl.Qty : (sl.DocumentID == 4 ? -(sl.Qty) : 0)))) : 0,
                            //FieldDecimal2 = reportDataStructList[9].IsSelectionField == true ? sl.GrossAmount : 0,
                            //FieldDecimal3 = reportDataStructList[10].IsSelectionField == true ? sl.DiscountAmount : 0,
                            FieldDecimal2 = reportDataStructList[9].IsSelectionField == true ? (sl.DocumentID == 1 ? sl.NetAmount : (sl.DocumentID == 2 ? -(sl.NetAmount) : (sl.DocumentID == 3 ? sl.NetAmount : (sl.DocumentID == 4 ? -(sl.NetAmount) : 0)))) : 0
                            //FieldDecimal5 = 0,
                            //FieldDecimal6 = 0
                        }).ToDataTable();



                //.ToArray().ToDataTable();
            }
        }

        public DataTable GetSalesTransactionDataTable(string documentNo, int documentStatus, int documentid,decimal exchangeRate)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = (
                                from ph in context.InvSalesHeaders
                                join pd in context.InvSalesDetails on ph.InvSalesHeaderID equals pd.InvSalesHeaderID
                                join oh in context.SalesOrderHeaders on ph.ReferenceDocumentID equals oh.SalesOrderHeaderID
                                join c in context.Customers on ph.CustomerID equals c.CustomerID
   
                                where
                                    ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) &&
                                    ph.DocumentID.Equals(documentid)
                                select
                                    new
                                    {
                                        FieldString1 = "I"+ ph.DocumentNo,
                                        FieldString2 = ph.ReferenceNo,
                                        FieldString3 = c.CustomerCode + "  " + c.CustomerName,
                                        FieldString4 = exchangeRate,
                                        FieldString5 = ph.Remark, // deliver address
                                        FieldString6 = ph.DocumentNo, // po no 
                                        FieldString7 = oh.Currency,
                                        FieldString8 = oh.Company,
                                        FieldString9 = EntityFunctions.TruncateTime(ph.DocumentDate),
                                        FieldString10 = EntityFunctions.TruncateTime(oh.DeliverDate),
                                        FieldString11 = "", //oh.Colour,
                                        FieldString12 = ph.CreatedUser, //l.LocationCode + "  " + l.LocationName,
                                        FieldString13 = pd.ProductCode,
                                        FieldString14 = pd.ProductName,
                                        FieldString15 = pd.SellingPrice,
                                        FieldString16 = pd.OrderQty,
                                        FieldString17 = pd.NetAmount,
                                        FieldString18 = ph.NetAmount,
                                        FieldString19 = (c.TaxID1 * ph.NetAmount) / 100, //dep.DepartmentName,
                                        FieldString20 = ((c.TaxID1 * ph.NetAmount) / 100)*exchangeRate,//ct.CategoryName,
                                        FieldString21 = c.TaxNo1, //sct.SubCategoryName,
                                        FieldString22 = c.TaxNo3, //sct2.SubCategory2Name,



                                         
                                    });
                DataTable dtc = new DataTable();
                dtc = query.ToDataTable();
                return query.AsEnumerable().ToDataTable();
            }
        }

       

        


        public DataTable GetSalesReturnTransactionDataTable(string documentNo, int documentStatus, int documentid)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = (
                                from ph in context.InvSalesHeaders
                                join pd in context.InvSalesDetails on ph.InvSalesHeaderID equals pd.InvSalesHeaderID
                                join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                                join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                                join s in context.Customers on ph.CustomerID equals s.CustomerID
                                join l in context.Locations on ph.LocationID equals l.LocationID
                                where
                                    ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) &&
                                    ph.DocumentID.Equals(documentid)
                                select
                                    new
                                    {
                                        FieldString1 = ph.DocumentNo,
                                        FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                        FieldString3 = s.CustomerCode + "  " + s.CustomerName,
                                        FieldString4 = ph.Remark,
                                        FieldString5 = "",
                                        FieldString6 = "",
                                        FieldString7 = "",
                                        FieldString8 = "",
                                        FieldString9 = ph.ReferenceNo,
                                        FieldString10 = "",
                                        FieldString11 = "",
                                        FieldString12 = l.LocationCode + "  " + l.LocationName,
                                        FieldString13 = pm.ProductCode,
                                        FieldString14 = pm.ProductName,
                                        FieldString15 = um.UnitOfMeasureName,
                                        FieldString16 = "",
                                        FieldString17 = pd.OrderQty,
                                        FieldString18 = "0",
                                        FieldString19 = pd.CostPrice,
                                        FieldString20 = pd.DiscountPercentage,
                                        FieldString21 = pd.DiscountAmount,
                                        FieldString22 = pd.NetAmount,
                                        FieldString23 = ph.GrossAmount,
                                        FieldString24 = ph.DiscountPercentage,
                                        FieldString25 = ph.DiscountAmount,
                                        FieldString26 = ph.TaxAmount,
                                        FieldString27 = ph.OtherChargers,
                                        FieldString28 = ph.NetAmount,
                                        FieldString29 = "",
                                        FieldString30 = ph.CreatedUser
                                    });
                return query.AsEnumerable().ToDataTable();
            }
        }

        public string[] GetAllProductCodes()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<String> productCodeList = new List<string>();
                var qry = (from bd in context.InvProductBatchNoExpiaryDetails
                           join pm in context.InvProductMasters on bd.ProductID equals pm.InvProductMasterID
                           where pm.IsDelete == false && bd.BalanceQty > 0
                           select new
                           {
                               pm.ProductCode,
                           }).ToArray();
                foreach (var temp in qry)
                {
                    productCodeList.Add(temp.ProductCode);
                }
                return productCodeList.ToArray();
            }
        }
        
        public string[] GetAllProductNames() 
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<String> productCodeList = new List<string>();
                var qry = (from bd in context.InvProductBatchNoExpiaryDetails
                           join pm in context.InvProductMasters on bd.ProductID equals pm.InvProductMasterID
                           where pm.IsDelete == false && bd.BalanceQty > 0
                           select new
                           {
                               pm.ProductName,
                           }).ToArray();
                foreach (var temp in qry)
                {
                    productCodeList.Add(temp.ProductName);
                }
                return productCodeList.ToArray();
            }
        }


        /// <summary>
        /// Test report - Delete this function after testing
        /// </summary>
        /// <returns></returns>
        public DataTable TestDataCrossTabReport()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = context.InvSales.Where("DocumentStatus=@0", 1);
                var queryResult = (from sl in query
                                   join l in context.Locations on sl.LocationID equals l.LocationID
                                   select new
                                   {
                                       FieldString1 = sl.LocationCode,
                                       FieldString2 = l.LocationPrefixCode,
                                       FieldString3 = sl.DepartmentCode,
                                       FieldString4 = sl.DepartmentName,
                                       FieldString5 = sl.CategoryCode,
                                       FieldString6 = sl.CategoryName,
                                       FieldString7 = sl.SubCategoryCode,
                                       FieldString8 = sl.SubCategoryName,
                                       FieldString9 = sl.SubCategory2Code,
                                       FieldString10 = sl.SubCategory2Name,
                                       FieldString11 = sl.ProductCode,
                                       FieldString12 = sl.ProductName,
                                       FieldString13 = sl.DocumentNo,
                                       FieldString14 = sl.DocumentDate,
                                       FieldString15 = sl.CustomerCode,
                                       FieldString16 = sl.SalesPersonCode,
                                       FieldString17 = "", // Company
                                       FieldString18 = "",
                                       FieldString19 = "",
                                       FieldString20 = "",
                                       FieldDecimal1 = sl.Qty,
                                       FieldDecimal2 = sl.GrossAmount,
                                       FieldDecimal3 = sl.DiscountAmount,
                                       FieldDecimal4 = sl.NetAmount,
                                       FieldDecimal5 = 0,
                                       FieldDecimal6 = 0
                                   }
                                  );

                //var queryResult = query.Select(ph =>
                //                  new
                //                  {
                //                      FieldString1 = ph.LocationCode,
                //                      FieldString2 = ph.DepartmentCode,
                //                      FieldString3 = ph.CategoryCode,
                //                      FieldString4 = ph.SubCategoryCode,
                //                      FieldString5 = ph.SubCategory2Code,
                //                      FieldString6 = ph.ProductCode,
                //                      FieldString7 = ph.DocumentNo,
                //                      FieldString8 = ph.DocumentDate,
                //                      FieldString9 = ph.CustomerCode,
                //                      FieldString10 = ph.SalesPersonCode,
                //                      FieldDecimal1 = ph.Qty,
                //                      FieldDecimal2 = ph.GrossAmount,
                //                      FieldDecimal3 = ph.DiscountAmount,
                //                      FieldDecimal4 = ph.NetAmount
                //                  }
                //                      ).ToArray();
                DataTable dt = queryResult.ToDataTable();
                return queryResult.ToDataTable();
            }
        }


    }
}
