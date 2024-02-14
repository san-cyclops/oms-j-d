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
    public partial class FrmSerial : Form
    {
        List<InvProductSerialNoTemp> invProductSerialNoTempList;
        InvProductSerialNoTemp invProductSerialNo;
        decimal tmpQty;
        string documentNo;
        int locationID;      
        int transactionSwitchType;

        public FrmSerial()
        {
            InitializeComponent();
        }

        public enum transactionType
        {
            GoodReceivedNote,
            LogisticGoodReceivedNote,
            Invoice,
            OpeningStock
        }

        public FrmSerial(List<InvProductSerialNoTemp> invProductSerialNoList, InvProductSerialNoTemp invProductSerialNumber, string caption, decimal qty, string documentNumber, int locationId, transactionType tranType)
        {
            try
            {
                InitializeComponent();
                invProductSerialNoTempList = invProductSerialNoList;
                this.dgvSerialNo.AutoGenerateColumns = false;
                lblSerial.Text = caption;
                invProductSerialNo = invProductSerialNumber;
                dgvSerialNo.DataSource = getFillterd(invProductSerialNoTempList);
                tmpQty = qty;
                locationID = locationId;
                documentNo = documentNumber;
                txtTotal.Text = Common.ConvertDecimalToStringQty((getFillterd(invProductSerialNoTempList).Count));
                if (tranType == transactionType.GoodReceivedNote)
                    transactionSwitchType = 1;
                else if (tranType == transactionType.LogisticGoodReceivedNote)
                    transactionSwitchType = 2;
                else if (tranType == transactionType.GoodReceivedNote)
                    transactionSwitchType = 3;
                else if (tranType == transactionType.OpeningStock)
                    transactionSwitchType = 4;

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
            if (e.KeyCode.Equals(Keys.Enter))
            {
                try
                {
                    if (tmpQty.Equals(getFillterd(invProductSerialNoTempList).Count))
                    {
                        //Toast.Show(this.Text, Toast.messageType.Information, Toast.messageAction.Exceed);
                        this.Close();
                        this.Dispose();
                        return;
                    }

                    if (!txtSerialNo.Text.Trim().Equals(string.Empty))
                    {

                        InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();
                        InvPurchaseService invPurchaseServices = new InvPurchaseService();
                        LgsPurchaseService lgsPurchaseServices = new LgsPurchaseService();
                        OpeningStockService openingStockService = new OpeningStockService();

                        //invProductSerialNoTemp = invProductSerialNo;
                        invProductSerialNoTemp.SerialNo = txtSerialNo.Text.Trim();
                        invProductSerialNoTemp.DocumentID = invProductSerialNo.DocumentID;
                        invProductSerialNoTemp.ProductID = invProductSerialNo.ProductID;
                        invProductSerialNoTemp.UnitOfMeasureID = invProductSerialNo.UnitOfMeasureID;
                        invProductSerialNoTemp.ExpiryDate = invProductSerialNo.ExpiryDate;

                        if (transactionSwitchType == 4) { invProductSerialNoTemp.BatchNo = invProductSerialNo.BatchNo; }
                        else { invProductSerialNoTemp.BatchNo = string.Empty; }

                        string pausedDocumentNo = string.Empty;

                        InvProductSerialNoDetail invPausedProductSerialNoTemp = new InvProductSerialNoDetail();
                        
                        if (transactionSwitchType == 1)
                        {
                            pausedDocumentNo = invPurchaseServices.getPausedSerialNoDetailByDocumentNo(txtSerialNo.Text.Trim(), documentNo, invProductSerialNoTemp, locationID);

                        }
                        else if (transactionSwitchType == 2)
                        {
                            pausedDocumentNo = lgsPurchaseServices.getPausedSerialNoDetailByDocumentNo(txtSerialNo.Text.Trim(), documentNo, invProductSerialNoTemp, locationID);
                        }
                        else if (transactionSwitchType == 3)
                        {

                        }
                        else if (transactionSwitchType == 4)
                        {
                            pausedDocumentNo = openingStockService.GetPausedSerialNoDetailByDocumentNo(txtSerialNo.Text.Trim(), documentNo, invProductSerialNoTemp, locationID);
                        }


                        if (pausedDocumentNo != string.Empty)
                        {
                            Toast.Show("This serial No already exists!\nDocument " + pausedDocumentNo + "\nPlease Save or Cancel the document and try again.", Toast.messageType.Warning, Toast.messageAction.General);
                            return;
                        }

                        if (transactionSwitchType == 1)
                        {
                            if (invPurchaseServices.IsExistsSavedSerialNoDetailByDocumentNo(txtSerialNo.Text.Trim(), invProductSerialNoTemp,locationID))
                            {
                                if ((Toast.Show("This serial No already exists!\nDo you want to continue?", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.No)))
                                    return;
                            }
                        }
                        else if (transactionSwitchType == 2)
                        {
                            if (lgsPurchaseServices.IsExistsSavedSerialNoDetailByDocumentNo(txtSerialNo.Text.Trim(), invProductSerialNoTemp,locationID))
                            {
                                if ((Toast.Show("This serial No already exists!\nDo you want to continue?", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.No)))
                                    return;
                            }
                        }
                        else if (transactionSwitchType == 3)
                        {

                        }
                        else if (transactionSwitchType == 4)
                        {
                            if (openingStockService.IsExistsSavedSerialNoDetailByDocumentNo(txtSerialNo.Text.Trim(), invProductSerialNoTemp, locationID))
                            {
                                if ((Toast.Show("This serial No already exists!\nDo you want to continue?", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.No)))
                                    return;
                            }
                        }

                        dgvSerialNo.DataSource = null;
                        //   invProductSerialNoTempList=invPurchaseServices.getUpdateSerialNoTemp(invProductSerialNoTempList, invProductSerialNoTemp);

                        if (transactionSwitchType == 1)
                        {
                            dgvSerialNo.DataSource = getFillterd(invPurchaseServices.getUpdateSerialNoTemp(invProductSerialNoTempList, invProductSerialNoTemp));
                        }
                        else if (transactionSwitchType == 2)
                        {
                            dgvSerialNo.DataSource = getFillterd(lgsPurchaseServices.getUpdateSerialNoTemp(invProductSerialNoTempList, invProductSerialNoTemp));
                        }
                        else if (transactionSwitchType == 3)
                        {
                            
                        }
                        else if (transactionSwitchType == 4)
                        {
                            dgvSerialNo.DataSource = getFillterd(openingStockService.GetUpdateSerialNoTemp(invProductSerialNoTempList, invProductSerialNoTemp));
                        }

                        dgvSerialNo.Refresh();
                        txtTotal.Text = Common.ConvertDecimalToStringQty((getFillterd(invProductSerialNoTempList).Count));
                        txtSerialNo.Text = string.Empty;
                        ActiveControl = txtSerialNo;
                        txtSerialNo.Focus();

                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                }
            }
        }

        private void FrmSerial_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (transactionSwitchType == 1)
                {
                    FrmGoodsReceivedNote frmGoodsReceivedNote = new FrmGoodsReceivedNote();
                    if (tmpQty.Equals(invProductSerialNoTempList.Count))
                        frmGoodsReceivedNote.SetSerialNoList(invProductSerialNoTempList, true);
                    else
                        frmGoodsReceivedNote.SetSerialNoList(invProductSerialNoTempList, false);
                }
                else if (transactionSwitchType == 2)
                {
                    FrmLogisticGoodsReceivedNote frmLogisticGoodsReceivedNote = new FrmLogisticGoodsReceivedNote();
                    if (tmpQty.Equals(invProductSerialNoTempList.Count))
                        frmLogisticGoodsReceivedNote.SetSerialNoList(invProductSerialNoTempList, true);
                    else
                        frmLogisticGoodsReceivedNote.SetSerialNoList(invProductSerialNoTempList, false);
                }
                else if (transactionSwitchType == 3)
                {
                    
                }
                else if (transactionSwitchType == 4)
                {
                    FrmOpeningStock frmOpeningStock = new FrmOpeningStock();
                    if (tmpQty.Equals(invProductSerialNoTempList.Count))
                        frmOpeningStock.SetSerialNoList(invProductSerialNoTempList, true);
                    else
                        frmOpeningStock.SetSerialNoList(invProductSerialNoTempList, false);
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

        private void FrmSerial_KeyDown(object sender, KeyEventArgs e)
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

            fillterList = fillterList.Where(s => s.ProductID == invProductSerialNo.ProductID && s.UnitOfMeasureID == invProductSerialNo.UnitOfMeasureID && s.ExpiryDate == invProductSerialNo.ExpiryDate).ToList();
            return fillterList;
        }

        private void dgvSerialNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F2))
            {
                try
                {
                    if (dgvSerialNo.CurrentCell.RowIndex >= 0)
                    {
                        if (Toast.Show("Serial No " + dgvSerialNo["SerialNo", dgvSerialNo.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                            return;

                        InvProductSerialNoTemp invProductSerialNoTempDelete = new InvProductSerialNoTemp();
                        invProductSerialNoTempDelete = invProductSerialNoTempList.Where(s => s.ProductID.Equals(invProductSerialNo.ProductID) && s.UnitOfMeasureID.Equals(invProductSerialNo.UnitOfMeasureID) && s.ExpiryDate.Equals(invProductSerialNo.ExpiryDate) && s.SerialNo.Equals(dgvSerialNo["SerialNo", dgvSerialNo.CurrentCell.RowIndex].Value.ToString())).FirstOrDefault();
                        if (invProductSerialNoTempDelete != null)
                        {
                            invProductSerialNoTempList.Remove(invProductSerialNoTempDelete);
                            dgvSerialNo.DataSource = null;
                            dgvSerialNo.DataSource = getFillterd(invProductSerialNoTempList);
                            dgvSerialNo.Refresh();
                            txtTotal.Text = Common.ConvertDecimalToStringQty((getFillterd(invProductSerialNoTempList).Count));

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
}
