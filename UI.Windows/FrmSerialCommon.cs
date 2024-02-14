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
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public partial class FrmSerialCommon : Form
    {
        List<InvProductSerialNoTemp> invProductSerialNoTempList;
        InvProductSerialNoTemp invProductSerialNo;

        bool isInv;
        decimal tmpQty;
        string documentNo;
        int locationID;
        int documentID;
        int productID;
        int documentType; 

        public enum transactionType
        {
            Invoice, //documentType=1
            StockAdjustment, //documentType=2
            TransferOfGoods, //documentType=3
            OpeningStock, //documentType=4
            SampleOut, //documentType=5
            ServiceOut, //documentType=6
            LogisticStockAdjustment, //documentType=7
            PurchaseReturn, //documentType=8
            LogisticPurchaseReturn, //documentType=9
            LogisticTransferOfGoods, //documentType=10 
            Dispatch //documentType=11 
        }

        public FrmSerialCommon()
        {
            InitializeComponent();  
        }

        public FrmSerialCommon(List<InvProductSerialNoTemp> invProductSerialNoList, InvProductSerialNoTemp invProductSerialNumber, string caption, decimal qty, string documentNumber, int locationId, bool isInvProduct, int docID, transactionType transactiontype)
        {
            try
            {
                InitializeComponent();
                isInv = isInvProduct;
                documentID = docID;
                invProductSerialNoTempList = invProductSerialNoList;

                if (transactiontype.Equals(transactionType.Invoice))
                    documentType = 1;
                else if (transactiontype.Equals(transactionType.StockAdjustment))
                    documentType = 2;
                else if (transactiontype.Equals(transactionType.TransferOfGoods))
                    documentType = 3;
                else if (transactiontype.Equals(transactionType.OpeningStock))
                    documentType = 4;
                else if (transactiontype.Equals(transactionType.SampleOut))
                    documentType = 5;
                else if (transactiontype.Equals(transactionType.ServiceOut))
                    documentType = 6;
                else if (transactiontype.Equals(transactionType.LogisticStockAdjustment))
                    documentType = 7;
                else if (transactiontype.Equals(transactionType.PurchaseReturn))
                    documentType = 8;
                else if (transactiontype.Equals(transactionType.LogisticPurchaseReturn))
                    documentType = 9;
                else if (transactiontype.Equals(transactionType.LogisticTransferOfGoods))
                    documentType = 10;
                else if (transactiontype.Equals(transactionType.Dispatch))
                    documentType = 11;

                this.dgvSearchDetails.AutoGenerateColumns = false;
                lblSerial.Text = caption;
                invProductSerialNo = invProductSerialNumber;
                productID = (int)invProductSerialNo.ProductID;
                dgvSearchDetails.DataSource = getFillterd(invProductSerialNoTempList);
                tmpQty = qty;
                locationID = locationId;
                documentNo = documentNumber;
                txtTotal.Text = Common.ConvertDecimalToStringQty((getFillterd(invProductSerialNoTempList).Count));
                ActiveControl = txtSerialNo;
                txtSerialNo.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void txtSerialNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    string serial = txtSerialNo.Text.Trim();
                    
                    for (int i = 0; i < dgvSearchDetails.RowCount; i++)
                    {
                        if (serial.Equals(dgvSearchDetails["SerialNo", i].Value.ToString()))
                        {
                            DataGridViewCheckBoxCell chkBox = new DataGridViewCheckBoxCell();
                            chkBox = (DataGridViewCheckBoxCell)dgvSearchDetails.Rows[i].Cells[0];
                            chkBox.Value = true;
                            txtSerialNo.Text = string.Empty;
                            txtSerialNo.Focus();
                        }
                    }
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
            CloseForm();
        }

        private void FrmSerialCommon_KeyDown(object sender, KeyEventArgs e)
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

        private List<InvProductSerialNoTemp> getFillterd(List<InvProductSerialNoTemp> fillterList)
        {
            fillterList = fillterList.Where(s => s.ProductID.Equals(invProductSerialNo.ProductID) && s.UnitOfMeasureID.Equals(invProductSerialNo.UnitOfMeasureID)).ToList();
            return fillterList;
        }

        private void FrmSerialCommon_Load(object sender, EventArgs e)
        {

        }

        private void FrmSerialCommon_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                List<InvProductSerialNoTemp> invSerialNoList = new List<InvProductSerialNoTemp>();

                for (int i = 0; i < dgvSearchDetails.RowCount; i++)
                {
                    DataGridViewCheckBoxCell chkbox = new DataGridViewCheckBoxCell();
                    chkbox = (DataGridViewCheckBoxCell)dgvSearchDetails.Rows[i].Cells[0];

                    if (chkbox.Value != null)
                    {
                        InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();
                        CommonService commonService = new CommonService();

                        invProductSerialNoTemp.SerialNo = dgvSearchDetails["SerialNo", i].Value.ToString();
                        invProductSerialNoTemp.DocumentID = documentID;
                        invProductSerialNoTemp.ProductID = invProductSerialNo.ProductID;
                        invProductSerialNoTemp.UnitOfMeasureID = invProductSerialNo.UnitOfMeasureID;
                        invProductSerialNoTemp.ExpiryDate = invProductSerialNo.ExpiryDate;

                        invSerialNoList = commonService.UpdateInvSerialNoTemp(invSerialNoList, invProductSerialNoTemp);
                    }
                }

                if (documentType.Equals(1))
                {
                    //FrmInvoice frmInvoice = new FrmInvoice();
                    //frmInvoice.SetInvSerialNoList(invSerialNoList);
                }
                else if (documentType.Equals(2))
                {
                    FrmStockAdjustment frmStockAdjustment = new FrmStockAdjustment();
                    frmStockAdjustment.SetInvSerialNoList(invSerialNoList);
                }
                else if (documentType.Equals(3))
                {
                    FrmTransferOfGoodsNote frmTransferOfGoodsNote = new FrmTransferOfGoodsNote();
                    frmTransferOfGoodsNote.SetInvSerialNoList(invSerialNoList);
                }
                else if (documentType.Equals(4))
                {
                    
                }
                else if (documentType.Equals(5))
                {
                    FrmSampleOut frmSampleOut = new FrmSampleOut();
                    frmSampleOut.SetInvSerialNoList(invSerialNoList);
                }
                else if (documentType.Equals(6))
                {
                    FrmServiceOut frmServiceOut = new FrmServiceOut();
                    frmServiceOut.SetSerialNoList(invSerialNoList);
                }
                else if (documentType.Equals(7))
                {
                    FrmLogisticStockAdjustment frmLogisticStockAdjustment = new FrmLogisticStockAdjustment();
                    frmLogisticStockAdjustment.SetLgsSerialNoList(invSerialNoList); 
                }
                else if (documentType.Equals(8))
                {
                    FrmPurchaseReturnNote frmPurchaseReturnNote = new FrmPurchaseReturnNote();
                    frmPurchaseReturnNote.SetSerialNoList(invSerialNoList);
                }
                else if (documentType.Equals(9))
                {
                    FrmLogisticPurchaseReturnNote frmLogisticPurchaseReturnNote = new FrmLogisticPurchaseReturnNote();
                    frmLogisticPurchaseReturnNote.SetSerialNoList(invSerialNoList);
                }
                else if (documentType.Equals(10))
                {
                    FrmLogisticTransferOfGoodsNote frmLogisticTransferOfGoodsNote = new FrmLogisticTransferOfGoodsNote();
                    frmLogisticTransferOfGoodsNote.SetSerialNoList(invSerialNoList);
                }
                else if (documentType.Equals(11))
                {
                   
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


    }
}
