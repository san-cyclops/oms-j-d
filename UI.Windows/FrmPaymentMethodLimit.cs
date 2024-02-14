using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using System.Transactions;
using Domain;
using Utility;
using Service;

namespace UI.Windows
{
    public partial class FrmPaymentMethodLimit : Form
    {
        private ReferenceType existingReferenceType;

        public FrmPaymentMethodLimit()
        {
        }

        public enum transactionType
        {
            GiftVoucherPurchaseOrder, //documentType=1
            GiftVoucherPurchase, //documentType=2
            PurchaseOrder, //documentType=3
            Purchase, //documentType=4
        }

        int documentType;

        public FrmPaymentMethodLimit(bool isEditable, string paymentTerm, decimal creditLimit, int creditPeriod, decimal chequeLimit, int chequePeriod, transactionType transactiontype)
        {
            try
            {
                InitializeComponent();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();

                txtCreditLimit.Text = Common.ConvertDecimalToStringCurrency(creditLimit);
                txtCreditPeriod.Text = creditPeriod.ToString();
                txtChequeLimit.Text = Common.ConvertDecimalToStringCurrency(chequeLimit);
                txtChequePeriod.Text = chequePeriod.ToString();

                if (transactiontype.Equals(transactionType.GiftVoucherPurchaseOrder))
                    documentType = 1;
                else if (transactiontype.Equals(transactionType.GiftVoucherPurchase))
                    documentType = 2;
                else if (transactiontype.Equals(transactionType.PurchaseOrder))
                    documentType = 3;
                else if (transactiontype.Equals(transactionType.Purchase))
                    documentType = 4;

                Common.EnableTextBox(false, txtCreditLimit, txtCreditPeriod, txtChequeLimit, txtChequePeriod);
                
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.EditablePaymentTerm).ToString(), paymentTerm);
                if (existingReferenceType != null)
                {
                    if (paymentTerm.ToUpper() == "CREDIT" && isEditable)
                    {
                        Common.EnableTextBox(true, txtCreditLimit, txtCreditPeriod);
                        if (txtCreditLimit.Enabled)
                        {
                            ActiveControl = txtCreditLimit;
                            txtCreditLimit.Focus();
                        }
                    }
                    else if (paymentTerm.ToUpper() == "CHEQUE" && isEditable)
                    {
                        Common.EnableTextBox(true, txtChequeLimit, txtChequePeriod);
                        if (txtChequeLimit.Enabled)
                        {
                            ActiveControl = txtChequeLimit;
                            txtChequeLimit.Focus();
                        }
                    }
                }
                else
                {
                    Common.EnableTextBox(false, txtCreditLimit, txtCreditPeriod, txtChequeLimit, txtChequePeriod);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void CloseForm()
        {
            if (Toast.Show("Do you want to Close this form?", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.Yes))
            {
                this.Close();
                this.Dispose();

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                CloseForm();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void FormLoad()
        {
            
        }

        private void FrmPaymentMethodLimit_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
               if (documentType.Equals(1))
                {
                    FrmGiftVoucherPurchaseOrder frmGiftVoucherPurchaseOrder = new FrmGiftVoucherPurchaseOrder();
                    frmGiftVoucherPurchaseOrder.SetPaymentTermLimit(Common.ConvertStringToDecimalCurrency(txtCreditLimit.Text), Common.ConvertStringToInt(txtCreditPeriod.Text), Common.ConvertStringToDecimalCurrency(txtChequeLimit.Text), Common.ConvertStringToInt(txtChequePeriod.Text));
                }
                else if (documentType.Equals(2))
                {
                    FrmGiftVoucherGoodsReceivedNote frmGiftVoucherGoodsReceivedNote = new FrmGiftVoucherGoodsReceivedNote();
                    frmGiftVoucherGoodsReceivedNote.SetPaymentTermLimit(Common.ConvertStringToDecimalCurrency(txtCreditLimit.Text), Common.ConvertStringToInt(txtCreditPeriod.Text), Common.ConvertStringToDecimalCurrency(txtChequeLimit.Text), Common.ConvertStringToInt(txtChequePeriod.Text));
                }
                else if (documentType.Equals(3))
                {
                    FrmPurchaseOrder frmPurchaseOrder = new FrmPurchaseOrder();
                    //frmPurchaseOrder.SetPaymentTermLimit(txtCreditLimit, txtCreditPeriod, txtChequeLimit, txtChequePeriod);
                }
                else if (documentType.Equals(4))
                {
                    FrmGoodsReceivedNote frmGoodsReceivedNote = new FrmGoodsReceivedNote();
                    //frmGoodsReceivedNote.SetPaymentTermLimit(txtCreditLimit, txtCreditPeriod, txtChequeLimit, txtChequePeriod);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void FrmPaymentMethodLimit_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

    }
}
