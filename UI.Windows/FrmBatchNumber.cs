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
    public partial class FrmBatchNumber : Form
    {
        List<InvProductBatchNoTemp> invProductBatchNoTempList;
        InvProductBatchNoTemp invProductBatchNo;

        List<LgsProductBatchNoTemp> lgsProductBatchNoTempList;
        LgsProductBatchNoTemp lgsProductBatchNo; 

        public enum transactionType
        {
            Invoice, //documentType=1
            StockAdjustment, //documentType=2
            TransferOfGoods, //documentType=3
            SampleOut, //documentType=4
            ServiceOut, //documentType=5
            LogisticStockAdjustment, //documentType=6
            LogisticTransferOfGoods, //documentType=7
            PurchaseReturn, //documentType=8
            SalesReturn, //documentType=9
            LogisticPurchaseReturn, //documentType=10   
            Dispatch, //documentType=11 
            BarcodePrint, //documentType=12 
        }

        int documentType;
        decimal tmpQty;
        string documentNo;
        int locationID;
        string batchNumber;
        long productID;

        private bool isInv;

        public bool IsInv
        {
            get { return isInv; }
            set { isInv = value; }
        }

        private void FrmBatchNumber_Load(object sender, EventArgs e)
        {
            dgvSearchDetails.FirstDisplayedCell = null;
            //dgvSearchDetails.ClearSelection();
            
        }

        public FrmBatchNumber(List<InvProductBatchNoTemp> invProductBatchNoList, InvProductBatchNoTemp invProductBatchNumber, string caption, decimal qty, string documentNumber, int locationId, bool isInvProduct, transactionType transactiontype, long productID)
        {
            try
            {
                InitializeComponent();
                isInv = isInvProduct;
                invProductBatchNoTempList = invProductBatchNoList;
                this.dgvSearchDetails.AutoGenerateColumns = false;
                this.productID = productID;

                if (transactiontype.Equals(transactionType.Invoice))
                    documentType = 1;
                else if (transactiontype.Equals(transactionType.StockAdjustment))
                    documentType = 2;
                else if (transactiontype.Equals(transactionType.TransferOfGoods))
                    documentType = 3;
                else if (transactiontype.Equals(transactionType.SampleOut))
                    documentType = 4;
                else if (transactiontype.Equals(transactionType.ServiceOut))
                    documentType = 5;
                else if (transactiontype.Equals(transactionType.LogisticStockAdjustment))
                    documentType = 6;
                else if (transactiontype.Equals(transactionType.LogisticTransferOfGoods))
                    documentType = 7;
                else if (transactiontype.Equals(transactionType.PurchaseReturn))
                    documentType = 8;
                else if (transactiontype.Equals(transactionType.SalesReturn))
                    documentType = 9;
                else if (transactiontype.Equals(transactionType.LogisticPurchaseReturn))
                    documentType = 10;
                else if (transactiontype.Equals(transactionType.Dispatch))
                    documentType = 11;
                else if (transactiontype.Equals(transactionType.BarcodePrint))
                    documentType = 12;

                lblSerial.Text = caption;
                invProductBatchNo = invProductBatchNumber;
                dgvSearchDetails.DataSource = getFillterdBatch(invProductBatchNoTempList);
                tmpQty = qty;
                locationID = locationId;
                documentNo = documentNumber;
                txtTotal.Text = Common.ConvertDecimalToStringQty((getFillterdBatch(invProductBatchNoTempList).Count));
                ActiveControl = txtBatchNo;
                txtBatchNo.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public FrmBatchNumber(List<LgsProductBatchNoTemp> lgsProductBatchNoList, LgsProductBatchNoTemp lgsProductBatchNumber, string caption, decimal qty, string documentNumber, int locationId, bool isInvProduct, transactionType transactiontype, long productID)
        {
            try
            {
                InitializeComponent();
                isInv = isInvProduct;
                lgsProductBatchNoTempList = lgsProductBatchNoList;
                this.dgvSearchDetails.AutoGenerateColumns = false;
                this.productID = productID;

                if (transactiontype.Equals(transactionType.Invoice))
                    documentType = 1;
                else if (transactiontype.Equals(transactionType.StockAdjustment))
                    documentType = 2;
                else if (transactiontype.Equals(transactionType.TransferOfGoods))
                    documentType = 3;
                else if (transactiontype.Equals(transactionType.SampleOut))
                    documentType = 4;
                else if (transactiontype.Equals(transactionType.ServiceOut))
                    documentType = 5;
                else if (transactiontype.Equals(transactionType.LogisticStockAdjustment))
                    documentType = 6;
                else if (transactiontype.Equals(transactionType.LogisticTransferOfGoods))
                    documentType = 7;
                else if (transactiontype.Equals(transactionType.PurchaseReturn))
                    documentType = 8;
                else if (transactiontype.Equals(transactionType.SalesReturn))
                    documentType = 9;
                else if (transactiontype.Equals(transactionType.LogisticPurchaseReturn))
                    documentType = 10;
                else if (transactiontype.Equals(transactionType.Dispatch))
                    documentType = 11;
                else if (transactiontype.Equals(transactionType.BarcodePrint))
                    documentType = 12;

                lblSerial.Text = caption;
                lgsProductBatchNo = lgsProductBatchNumber;
                dgvSearchDetails.DataSource = getFillterdBatchLgs(lgsProductBatchNoTempList);
                tmpQty = qty;
                locationID = locationId;
                documentNo = documentNumber;
                txtTotal.Text = Common.ConvertDecimalToStringQty((getFillterdBatchLgs(lgsProductBatchNoTempList).Count));
                ActiveControl = txtBatchNo;
                txtBatchNo.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private List<InvProductBatchNoTemp> getFillterdBatch(List<InvProductBatchNoTemp> fillterList)
        {
            //fillterList = fillterList.Where(s => s.ProductID.Equals(invProductBatchNo.ProductID) && s.UnitOfMeasureID.Equals(invProductBatchNo.UnitOfMeasureID)).ToList();
            fillterList = fillterList.Where(s => s.ProductID.Equals(invProductBatchNo.ProductID)).ToList();
            return fillterList;
        }

        private List<LgsProductBatchNoTemp> getFillterdBatchLgs(List<LgsProductBatchNoTemp> fillterList)
        {
            //fillterList = fillterList.Where(s => s.ProductID.Equals(lgsProductBatchNo.ProductID) && s.UnitOfMeasureID.Equals(lgsProductBatchNo.UnitOfMeasureID)).ToList();
            fillterList = fillterList.Where(s => s.ProductID.Equals(lgsProductBatchNo.ProductID)).ToList();

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

        private void FrmBatchNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                CloseForm();
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
                    txtBatchNo.Text = dgvSearchDetails["BatchNo", dgvSearchDetails.CurrentCell.RowIndex].Value.ToString().Trim(); ;
                    txtBatchNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void FrmBatchNumber_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!txtBatchNo.Text.Trim().Equals(string.Empty))
                {
                    if (isInv)
                    {
                        
                        if (documentType.Equals(1))
                        {
                            FrmInvoice frmInvoice = new FrmInvoice();
                            frmInvoice.SetBatchNumber(txtBatchNo.Text.Trim());
                        }
                        else if (documentType.Equals(2))
                        {
                            FrmStockAdjustment frmStockAdjustment = new FrmStockAdjustment();
                            frmStockAdjustment.SetBatchNumber(txtBatchNo.Text.Trim());
                        }
                        else if (documentType.Equals(3))
                        {
                            FrmTransferOfGoodsNote frmTransferOfGoodsNote = new FrmTransferOfGoodsNote();
                            frmTransferOfGoodsNote.SetBatchNumber(txtBatchNo.Text.Trim());
                        }
                        else if (documentType.Equals(4))
                        {
                            FrmSampleOut frmSampleOut = new FrmSampleOut();
                            frmSampleOut.SetBatchNumber(txtBatchNo.Text.Trim());
                        }
                        else if (documentType.Equals(5))
                        {
                                
                        }
                        else if (documentType.Equals(6))
                        {
                                
                        }
                        else if (documentType.Equals(7))
                        {
                                
                        }
                        else if (documentType.Equals(8))
                        {
                            FrmPurchaseReturnNote frmPurchaseReturnNote = new FrmPurchaseReturnNote();
                            frmPurchaseReturnNote.SetBatchNumber(txtBatchNo.Text.Trim());
                        }
                        else if (documentType.Equals(9))
                        {
                            FrmSalesReturnNote frmSalesReturnNote = new FrmSalesReturnNote();
                            frmSalesReturnNote.SetBatchNumber(txtBatchNo.Text.Trim());
                        }
                        else if (documentType.Equals(10))
                        {
                                
                        }
                        else if (documentType.Equals(11))
                        {
                            FrmDispatchNote frmDispatchNote = new FrmDispatchNote();
                            frmDispatchNote.SetBatchNumber(txtBatchNo.Text.Trim());
                        }
                        else if (documentType.Equals(12))
                        {
                            FrmBarcode frmBarcode = new FrmBarcode();
                            frmBarcode.SetBatchNumber(txtBatchNo.Text.Trim());
                        }
                    }
                    else
                    {
                       
                        if (documentType.Equals(1))
                        {
                                
                        }
                        else if (documentType.Equals(2))
                        {

                        }
                        else if (documentType.Equals(3))
                        {

                        }
                        else if (documentType.Equals(4))
                        {
                            FrmSampleOut frmSampleOut = new FrmSampleOut();
                            frmSampleOut.SetBatchNumber(txtBatchNo.Text.Trim());
                        }
                        else if (documentType.Equals(5))
                        {
                            FrmServiceOut frmServiceOut = new FrmServiceOut();
                            frmServiceOut.SetBatchNumber(txtBatchNo.Text.Trim());
                        }
                        else if (documentType.Equals(6))
                        {
                            FrmLogisticStockAdjustment frmLogisticStockAdjustment = new FrmLogisticStockAdjustment();
                            frmLogisticStockAdjustment.SetBatchNumber(txtBatchNo.Text.Trim());
                        }
                        else if (documentType.Equals(7))
                        {
                            FrmLogisticTransferOfGoodsNote frmLogisticTransferOfGoodsNote = new FrmLogisticTransferOfGoodsNote();
                            frmLogisticTransferOfGoodsNote.SetBatchNumber(txtBatchNo.Text.Trim());
                        }
                        else if (documentType.Equals(8))
                        {

                        }
                        else if (documentType.Equals(9))
                        {
                                
                        }
                        else if (documentType.Equals(10))
                        {
                            FrmLogisticPurchaseReturnNote frmLogisticPurchaseReturnNote = new FrmLogisticPurchaseReturnNote();
                            frmLogisticPurchaseReturnNote.SetBatchNumber(txtBatchNo.Text.Trim());
                        }
                        
                    }
                }
                else
                {
                    //Toast.Show("Please enter batch number", Toast.messageType.Information, Toast.messageAction.General).Equals(DialogResult.OK);
                    //return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if  (e.KeyCode.Equals(Keys.Enter))
                {
                    if (txtBatchNo.Text.Trim().Equals(string.Empty))

                    {
                        if ((Toast.Show("This Batch No", Toast.messageType.Error, Toast.messageAction.NotExists).Equals(DialogResult.OK)))
                            return;
                    }

                    this.Close();
                }
                else if (e.KeyCode.Equals(Keys.F3))
                {
                    dgvSearchDetails.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvSearchDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if  (e.KeyCode.Equals(Keys.Enter))
                {
                    if ((dgvSearchDetails.CurrentCell.RowIndex >= 0) && dgvSearchDetails.SelectedRows != null)
                    {
                        txtBatchNo.Text = dgvSearchDetails["BatchNo", dgvSearchDetails.CurrentCell.RowIndex].Value.ToString().Trim(); ;
                        txtBatchNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

      
    }
}
