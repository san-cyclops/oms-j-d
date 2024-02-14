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
    public partial class FrmMinusProducts : Form
    {
        List<InvMinusProductDetailsTemp> invMinusProductDetailsTempList;
        InvMinusProductDetailsTemp invMinusProductDetailsTemp;

        List<LgsMinusProductDetailsTemp> lgsMinusProductDetailsTempList; 
        LgsMinusProductDetailsTemp lgsMinusProductDetailsTemp; 
     
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

        public FrmMinusProducts(List<InvMinusProductDetailsTemp> invMinusProductDetailsList, InvMinusProductDetailsTemp invMinusProductDetails, int locationId, bool isInvProduct, transactionType transactiontype)
        {
            try
            {
                InitializeComponent();
                isInv = isInvProduct;
                invMinusProductDetailsTempList = invMinusProductDetailsList;
                this.dgvSearchDetails.AutoGenerateColumns = false;

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


                dgvSearchDetails.DataSource = getFillterdMinusProducts(invMinusProductDetailsTempList);
                //tmpQty = qty;
                //locationID = locationId;
                //documentNo = documentNumber;
                //txtTotal.Text = Common.ConvertDecimalToStringQty((getFillterdMinusProducts(invProductBatchNoTempList).Count));
                //ActiveControl = txtBatchNo;
                //txtBatchNo.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public FrmMinusProducts(List<LgsMinusProductDetailsTemp> lgsMinusProductDetailsList, LgsMinusProductDetailsTemp lgsMinusProductDetails, int locationId, bool isInvProduct, transactionType transactiontype)
        {
            try
            {
                InitializeComponent();
                isInv = isInvProduct;
                lgsMinusProductDetailsTempList = lgsMinusProductDetailsList;
                this.dgvSearchDetails.AutoGenerateColumns = false;

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


                dgvSearchDetails.DataSource = getFillterdLgsMinusProducts(lgsMinusProductDetailsTempList);
                //tmpQty = qty;
                //locationID = locationId;
                //documentNo = documentNumber;
                //txtTotal.Text = Common.ConvertDecimalToStringQty((getFillterdMinusProducts(invProductBatchNoTempList).Count));
                //ActiveControl = txtBatchNo;
                //txtBatchNo.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private List<InvMinusProductDetailsTemp> getFillterdMinusProducts(List<InvMinusProductDetailsTemp> fillterList)
        {
            fillterList = fillterList.Where(s => s.IsDelete.Equals(false)).ToList();
            return fillterList;
        }

        private List<LgsMinusProductDetailsTemp> getFillterdLgsMinusProducts(List<LgsMinusProductDetailsTemp> fillterList) 
        {
            fillterList = fillterList.Where(s => s.IsDelete.Equals(false)).ToList();
            return fillterList;
        }

        private void FrmMinusProducts_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void FrmMinusProducts_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Escape))
                {
                    dgvSearchDetails.DataSource = null;
                    dgvSearchDetails.Refresh();
                    this.Close();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        
    }
}
