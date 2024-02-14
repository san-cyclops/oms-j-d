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
    public partial class FrmExpiry : Form
    {
        List<InvProductBatchNoTemp> invProductBatchNoTempList;
        InvProductBatchNoTemp invProductBatchNo;

        List<LgsProductBatchNoTemp> lgsProductBatchNoTempList;
        LgsProductBatchNoTemp lgsProductBatchNo; 

        decimal tmpQty;
        string documentNo;
        int locationID;
        bool isInv;
        int documentType;

        public enum transactionType
        {
            Invoice, //documentType=1
            StockAdjustment, //documentType=2
            TransferOfGoods, //documentType=3
            SalesReturnNote, //documentType=4
            LogisticStockAdjustment, //documentType=5
            LogisticTransferOfGoods, //documentType=6
            PurchaseReturn, //documentType=7
            SalesReturn, //documentType=8
            LogisticPurchaseReturn, //documentType=9
        }

        public FrmExpiry()
        {
            InitializeComponent();
        }

        public FrmExpiry(List<InvProductBatchNoTemp> invProductBatchNoList, InvProductBatchNoTemp invProductBatchNumber, string caption, decimal qty, string documentNumber, int locationId, bool isInvProduct, transactionType transactiontype) 
        {
            try
            {
                InitializeComponent();
                isInv = isInvProduct;
                invProductBatchNoTempList = invProductBatchNoList;
                this.dgvSearchDetails.AutoGenerateColumns = false;

                if (transactiontype.Equals(transactionType.Invoice))
                    documentType = 1;
                else if (transactiontype.Equals(transactionType.StockAdjustment))
                    documentType = 2;
                else if (transactiontype.Equals(transactionType.TransferOfGoods))
                    documentType = 3;
                else if (transactiontype.Equals(transactionType.SalesReturnNote))
                    documentType = 4;
                else if (transactiontype.Equals(transactionType.LogisticStockAdjustment))
                    documentType = 5;
                else if (transactiontype.Equals(transactionType.LogisticTransferOfGoods))
                    documentType = 6;
                else if (transactiontype.Equals(transactionType.PurchaseReturn))
                    documentType = 7;
                else if (transactiontype.Equals(transactionType.SalesReturn))
                    documentType = 8;
                else if (transactiontype.Equals(transactionType.LogisticPurchaseReturn))
                    documentType = 9;

                lblSerial.Text = caption;
                invProductBatchNo = invProductBatchNumber;
                dgvSearchDetails.DataSource = getFillterdBatch(invProductBatchNoTempList);
                tmpQty = qty;
                locationID = locationId;
                documentNo = documentNumber;
                txtTotal.Text = Common.ConvertDecimalToStringQty((getFillterdBatch(invProductBatchNoTempList).Count));
                ActiveControl = dtpExpiary;
                dtpExpiary.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public FrmExpiry(List<LgsProductBatchNoTemp> lgsProductBatchNoList, LgsProductBatchNoTemp lgsProductBatchNumber, string caption, decimal qty, string documentNumber, int locationId, bool isInvProduct, transactionType transactiontype)
        {
            try
            {
                InitializeComponent();
                isInv = isInvProduct;
                lgsProductBatchNoTempList = lgsProductBatchNoList;
                this.dgvSearchDetails.AutoGenerateColumns = false;

                if (transactiontype.Equals(transactionType.Invoice))
                    documentType = 1;
                else if (transactiontype.Equals(transactionType.StockAdjustment))
                    documentType = 2;
                else if (transactiontype.Equals(transactionType.TransferOfGoods))
                    documentType = 3;
                else if (transactiontype.Equals(transactionType.SalesReturnNote))
                    documentType = 4;
                else if (transactiontype.Equals(transactionType.LogisticStockAdjustment))
                    documentType = 5;
                else if (transactiontype.Equals(transactionType.LogisticTransferOfGoods))
                    documentType = 6;
                else if (transactiontype.Equals(transactionType.PurchaseReturn))
                    documentType = 7;
                else if (transactiontype.Equals(transactionType.SalesReturn))
                    documentType = 8;
                else if (transactiontype.Equals(transactionType.LogisticPurchaseReturn))
                    documentType = 9;

                lblSerial.Text = caption;
                lgsProductBatchNo = lgsProductBatchNumber;
                dgvSearchDetails.DataSource = getFillterdBatchLgs(lgsProductBatchNoTempList);
                tmpQty = qty;
                locationID = locationId;
                documentNo = documentNumber;
                txtTotal.Text = Common.ConvertDecimalToStringQty((getFillterdBatchLgs(lgsProductBatchNoTempList).Count));
                ActiveControl = dtpExpiary;
                dtpExpiary.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void FrmExpiary_Load(object sender, EventArgs e)
        {

        }


        private List<InvProductBatchNoTemp> getFillterdBatch(List<InvProductBatchNoTemp> fillterList)
        {
            fillterList = fillterList.Where(s => s.ProductID.Equals(invProductBatchNo.ProductID) && s.UnitOfMeasureID.Equals(invProductBatchNo.UnitOfMeasureID)).ToList();
            return fillterList;
        }

        private List<LgsProductBatchNoTemp> getFillterdBatchLgs(List<LgsProductBatchNoTemp> fillterList)
        {
            fillterList = fillterList.Where(s => s.ProductID.Equals(lgsProductBatchNo.ProductID) && s.UnitOfMeasureID.Equals(lgsProductBatchNo.UnitOfMeasureID)).ToList();
            return fillterList;
        }

        private void CloseForm()
        {
            //if (Toast.Show("Do you want to Close this form?", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.Yes))
            //{
                this.Close();
                this.Dispose();

            //}
        }

        private void FrmExpiary_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                    CloseForm();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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

        private void dgvSearchDetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvSearchDetails.CurrentCell.RowIndex >= 0)
                {
                    dtpExpiary.Value = Convert.ToDateTime(dgvSearchDetails["Expiary", dgvSearchDetails.CurrentCell.RowIndex].Value.ToString().Trim());
                    dtpExpiary.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpExpiary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                try
                {
                    
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                }
            }
        }

        private void dgvSearchDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if ((dgvSearchDetails.CurrentCell.RowIndex >= 0) && dgvSearchDetails.SelectedRows != null)
                    {
                        dtpExpiary.Value = Convert.ToDateTime(dgvSearchDetails["Expiary", dgvSearchDetails.CurrentCell.RowIndex].Value.ToString().Trim());
                        dtpExpiary.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void FrmExpiary_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (dtpExpiary.Value != null)
                {
                    if (isInv)
                    {
                        if (documentType.Equals(1))
                        {
                            FrmInvoice frmInvoice = new FrmInvoice();
               
                        }
                        else if (documentType.Equals(2))
                        {
                            FrmStockAdjustment frmStockAdjustment = new FrmStockAdjustment();
                            frmStockAdjustment.SetExpiryDate(dtpExpiary.Value);
                        }
                        else if (documentType.Equals(3))
                        {
                            FrmTransferOfGoodsNote frmTransferOfGoodsNote = new FrmTransferOfGoodsNote();
                            frmTransferOfGoodsNote.SetExpiryDate(dtpExpiary.Value);
                        }
                        else if (documentType.Equals(4))
                        {

                        }
                        else if (documentType.Equals(5))
                        {

                        }
                        else if (documentType.Equals(6))
                        {

                        }
                        else if (documentType.Equals(7))
                        {
                            FrmPurchaseReturnNote frmPurchaseReturnNote = new FrmPurchaseReturnNote();
                            frmPurchaseReturnNote.SetExpiryDate(dtpExpiary.Value);
                        }
                        else if (documentType.Equals(8))
                        {

                        }
                        else if (documentType.Equals(9))
                        {

                        }
                    }
                    else
                    {
                        if (documentType.Equals(1))
                        {

                        }
                        else if (documentType.Equals(2))
                        {
                            FrmStockAdjustment frmStockAdjustment = new FrmStockAdjustment();
                            frmStockAdjustment.SetExpiryDate(dtpExpiary.Value);
                        }
                        else if (documentType.Equals(3))
                        {

                        }
                        else if (documentType.Equals(4))
                        {

                        }
                        else if (documentType.Equals(5))
                        {
                            FrmLogisticStockAdjustment frmLogisticStockAdjustment = new FrmLogisticStockAdjustment();
                            frmLogisticStockAdjustment.SetExpiryDate(dtpExpiary.Value);
                        }
                        else if (documentType.Equals(6))
                        {
                            FrmLogisticTransferOfGoodsNote frmLogisticTransferOfGoodsNote = new FrmLogisticTransferOfGoodsNote();
                            frmLogisticTransferOfGoodsNote.SetExpiryDate(dtpExpiary.Value);
                        }
                        else if (documentType.Equals(7))
                        {

                        }
                        else if (documentType.Equals(8))
                        {

                        }
                        else if (documentType.Equals(9))
                        {
                            FrmLogisticPurchaseReturnNote frmLogisticPurchaseReturnNote = new FrmLogisticPurchaseReturnNote();
                            frmLogisticPurchaseReturnNote.SetExpiryDate(dtpExpiary.Value);
                        }
                    }
                }
                else
                {
                    Toast.Show("Please enter exriery date", Toast.messageType.Information, Toast.messageAction.General).Equals(DialogResult.OK);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
    }
}
