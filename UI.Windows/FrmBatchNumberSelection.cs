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
    /// Developed by sanjeeewa
    /// </summary>
    public partial class FrmBatchNumberSelection : Form
    {
        List<PriceChangeTemp> PriceChangeTempList;
        PriceChangeTemp priceChangeTemp;
        List<InvProductBatchNoTemp> invProductBatchNoTempList;
        InvProductBatchNoTemp invProductBatchNo;
         
        private long[] arrLocationIds;


        bool isInv;
        decimal tmpQty;
        string documentNo;
        int locationID;
        int documentID;
        long productID;
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
            PriceChange,//documentType=11
        }

        public FrmBatchNumberSelection()
        {
            InitializeComponent();  
        }

        public FrmBatchNumberSelection(List<InvProductBatchNoTemp> invProductBatchNoList, InvProductBatchNoTemp invProductBatchNumber, string caption,bool isInvProduct, transactionType transactiontype, long productID)
        {
            try
            {
                InitializeComponent();
                isInv = isInvProduct;
                invProductBatchNoTempList = invProductBatchNoList;
                this.dgvSearchDetails.AutoGenerateColumns = false;
                this.productID = productID;

                if  (transactiontype.Equals(transactionType.PriceChange))
                    documentType = 11;


                lblSerial.Text = caption;
                invProductBatchNo = invProductBatchNumber;
                dgvSearchDetails.DataSource = invProductBatchNoTempList;   //getFillterdBatch(invProductBatchNoTempList);
                tmpQty = 0;
                locationID = 0;
                documentNo ="";

                dgvSearchDetails.Focus();

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
                        if (serial.Equals(dgvSearchDetails["BatchNo", i].Value.ToString()))
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
            try
            {
                //if (Toast.Show("Do you want to Close this form?", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.Yes))
                //{
                    this.Close();
                    this.Dispose();
                //}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void FrmBatchNumberSelection_KeyDown(object sender, KeyEventArgs e)
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

        private List<PriceChangeTemp> getFillterdBatch(List<PriceChangeTemp> fillterList)
        {
           
                fillterList = fillterList.Where(s => s.ProductID.Equals(invProductBatchNo.ProductID) && s.UnitOfMeasureID.Equals(invProductBatchNo.UnitOfMeasureID)).ToList();
                return fillterList;
        }

        private void FrmBatchNumberSelection_Load(object sender, EventArgs e)
        {

        }

        private void FrmBatchNumberSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                List<PriceChangeTemp> PriceChangeTempList = new List<PriceChangeTemp>();

                for (int i = 0; i < dgvSearchDetails.RowCount; i++)
                {
                    //DataGridViewCheckBoxCell chkbox = new DataGridViewCheckBoxCell();
                    //chkbox = (DataGridViewCheckBoxCell)dgvSearchDetails.Rows[i].Cells[0];

                    if (Convert.ToBoolean(dgvSearchDetails["SelectSerial", i].Value) == true)
                    {
                        PriceChangeTemp priceChangeTemp = new PriceChangeTemp();
                        CommonService commonService = new CommonService();

                        priceChangeTemp.BatchNo = dgvSearchDetails["BatchNo", i].Value.ToString();
                        priceChangeTemp.ProductID = Common.ConvertStringToLong(dgvSearchDetails["ProductIdBatch", i].Value.ToString());
                        priceChangeTemp.UnitOfMeasureID = Common.ConvertStringToLong(dgvSearchDetails["UOM", i].Value.ToString());
                        priceChangeTemp.LocationCode = (dgvSearchDetails["LocationIDBatch", i].Value.ToString());
                        Location location = new Location();
                        LocationService locationService = new LocationService();
                        location = locationService.GetLocationsByCode(priceChangeTemp.LocationCode);

                        priceChangeTemp.LocationName = location.LocationName;
                        priceChangeTemp.LocationId = location.LocationID;
                        priceChangeTemp.Qty = Common.ConvertStringToLong(dgvSearchDetails["Qty", i].Value.ToString());
                        PriceChangeTempList = commonService.UpdatePriceChangeTemp(PriceChangeTempList, priceChangeTemp);
                    }
                }

                if (documentType.Equals(11))
                {

                    FrmProductPriceChange frmProductPriceChange = new FrmProductPriceChange();
                    frmProductPriceChange.SetBatchNoList(PriceChangeTempList);
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvSearchDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    btnClose.Focus();
                }
              
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvSearchDetails_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
 
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelectAll.Checked) { CheckedAllLocations(); } else { UnCheckedAllLocations(); }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void CheckedAllLocations()
        {
            for (int i = 0; i < dgvSearchDetails.RowCount; i++)
            {
                dgvSearchDetails.Rows[i].Cells["SelectSerial"].Value = true;
            }

        }

        private void UnCheckedAllLocations()
        {
            for (int i = 0; i < dgvSearchDetails.RowCount; i++)
            {
                dgvSearchDetails.Rows[i].Cells["SelectSerial"].Value = false;
            }

        }

    }
}
