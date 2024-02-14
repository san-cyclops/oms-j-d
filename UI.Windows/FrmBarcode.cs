using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using System.Transactions;

using Domain;
using Utility;
using Service;
using System.Threading;
using System.IO;

namespace UI.Windows
{
    public partial class FrmBarcode : FrmBaseTransactionForm
    {
        /// <summary>
        /// By - Pravin
        /// 30/08/2013
        /// Barcode
        /// </summary>
        /// 
        UserPrivileges accessRights = new UserPrivileges();
        List<InvBarcodeDetailTemp> invBarcodeDetailTempList = new List<InvBarcodeDetailTemp>();
        List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();
        InvProductMaster existingInvProductMaster;
        InvBarcodeDetailTemp existingInvBarcodeDetailTemp = new InvBarcodeDetailTemp();        
        int documentID = 2;
        int referenceDocumentID = 0;
        static string batchNumber;
        static long barcode;
        bool isInvProduct;
        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;

        private DateTime referenceDocumentDate;
        public Dictionary<string, Process> OpenedProcesses = new Dictionary<string, Process>(StringComparer.OrdinalIgnoreCase);


        public FrmBarcode()
        {
            InitializeComponent();
        }

        public override void InitializeForm()
        {
            try
            {
                // Disable product details controls
                dgvItemDetails.DataSource = null;
                
                EnableLine(false);

                Common.EnableComboBox(true, cmbLocation);
                Common.EnableButton(false, btnSave);
                grpBody.Enabled = false;
                grpLeftFooter.Enabled = false;
                referenceDocumentID = 0;
                referenceDocumentDate = Common.GetSystemDateWithTime();
                invBarcodeDetailTempList = null;
                existingInvProductMaster = null;
                existingInvBarcodeDetailTemp = null;

                selectedRowIndex = 0;
                isUpdateGrid = false;
                rowCount = 0;

                dtpExpiry.Value = DateTime.Now;
               
                cmbLocation.SelectedValue = Common.LoggedLocationID;
                dtpReferenceDocumentDate.Value = Common.GetSystemDateWithTime();

                existingInvProductMaster = null;
                batchNumber = null;

                invProductBatchNoTempList = null;

                txtDocumentNo.Text = GetDocumentNo(true);
                this.ActiveControl = txtReferenceDocumentNo;
                txtReferenceDocumentNo.Focus();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void EnableLine(bool enable)
        {
            Common.EnableTextBox(enable, txtQty, txtCostPrice, txtSellingPrice, txtBatchNo);
            Common.EnableComboBox(enable, cmbUnit);
            dtpExpiry.Enabled = enable;
        }

        private string GetDocumentNo(bool isTemporytNo)
        {
            try
            {
                InvBarCodeService invBarCodeService = new InvBarCodeService();
                LocationService locationService = new LocationService();
                return invBarCodeService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }
        }

        private void LoadProducts()
        {
            try
            {
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                
                Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                Common.SetAutoComplete(txtProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
            }
            catch (Exception ex)
            {Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);}
        }

        public override void FormLoad()
        {
            try
            {
                dgvItemDetails.AutoGenerateColumns = false;
               
                // Load Locations
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());

                // Load Unit of measures
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                Common.LoadUnitOfMeasures(cmbUnit, unitOfMeasureService.GetAllUnitOfMeasures());

                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                Common.SetAutoBindRecords(cmbTag, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.TagType).ToString()));
                
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                documentID = autoGenerateInfo.DocumentID;

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);


                dgvItemDetails.AutoGenerateColumns = false;
                LoadProducts();
                btnSave.Text = "P&rint";
                btnPause.Enabled = false;
                dtpReferenceDocumentDate.Enabled = false;

                GetPrintingDetails();

                dtpReferenceDocumentDate.Value = Common.GetSystemDateWithTime();
                isInvProduct = true;
                base.FormLoad();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void SetBatchNumber(string batchNo)
        {
            batchNumber = batchNo;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkAutoCompleationProduct_CheckedChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }


        private void loadProductDetails(bool isCode, string strProduct, long unitofMeasureID, DateTime expiryDate)
        {
            try
            {
                existingInvProductMaster = new InvProductMaster();

                if (strProduct.Equals(string.Empty))
                {return;}

                InvProductMasterService InvProductMasterService = new InvProductMasterService();

                if (isCode)
                {
                    existingInvProductMaster = InvProductMasterService.GetProductsByRefCodesForBarcodePrint(strProduct);
                    if (isCode && strProduct.Equals(string.Empty))
                    {
                        txtProductName.Focus();
                        return;
                    }
                }
                else
                {existingInvProductMaster = InvProductMasterService.GetProductsByName(strProduct);}

                if (existingInvProductMaster != null)
                {
                    InvBarCodeService invBarCodeService = new InvBarCodeService();
                    if (invBarcodeDetailTempList == null)
                    {invBarcodeDetailTempList = new List<InvBarcodeDetailTemp>();}
                    if (!existingInvProductMaster.IsBatch)
                    {
                        txtBatchNo.Text = string.Empty;
                        txtBatchNo.Enabled = false;
                    }
                    else
                    {
                        txtBatchNo.Enabled = true;
                    }

                    existingInvBarcodeDetailTemp = invBarCodeService.getBarCodeDetailTemp(invBarcodeDetailTempList, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID, txtBatchNo.Text.Trim());
                    
                    if (existingInvBarcodeDetailTemp != null)
                    {
                        txtProductCode.Text = existingInvBarcodeDetailTemp.ProductCode;
                        txtProductName.Text = existingInvBarcodeDetailTemp.ProductName;
                        cmbUnit.SelectedValue = existingInvBarcodeDetailTemp.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingInvBarcodeDetailTemp.CostPrice);
                        txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingInvBarcodeDetailTemp.SellingPrice);
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingInvBarcodeDetailTemp.Qty);
                        txtStock.Text = existingInvBarcodeDetailTemp.Stock.ToString();

                        if (existingInvProductMaster.IsExpiry)
                        {
                            dtpExpiry.Value = Common.ConvertStringToDate((existingInvBarcodeDetailTemp.ExpiryDate == null ? dtpExpiry.Value.ToString() : existingInvBarcodeDetailTemp.ExpiryDate.ToString()));
                            dtpExpiry.Enabled = true;
                        }
                        else
                        {
                            dtpExpiry.Value = Common.ConvertDateTimeToDate(DateTime.Now);
                            dtpExpiry.Enabled = false;
                        }
                        Common.EnableComboBox(true, cmbUnit);
                        if (unitofMeasureID.Equals(0))
                            cmbUnit.Focus();

                        #region Deleted Code
                        //if (isbatch == true && existingInvBarcodeDetailTemp.BatchNo == null)
                        //{
                        //    Common.ClearTextBox(txtBatchNo);
                        //    Toast.Show("BatchNo - " + txtBatchNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                        //    txtBatchNo.Focus();                            
                        //    return;
                        //}

                        //if (existingInvProductMaster.IsBatch)
                        //{
                        //    txtProductCode.Text = existingInvBarcodeDetailTemp.ProductCode;
                        //    txtProductName.Text = existingInvBarcodeDetailTemp.ProductName;
                        //    //txtBatchNo.Text = existingInvBarcodeDetailTemp.BatchNo;
                        //    //txtStock.Text = existingInvBarcodeDetailTemp.Stock.ToString();
                        //    //txtQty.Text = existingInvBarcodeDetailTemp.Qty.ToString();
                        //    //txtCostPrice.Text = existingInvBarcodeDetailTemp.CostPrice.ToString();
                        //    //txtSellingPrice.Text = existingInvBarcodeDetailTemp.SellingPrice.ToString();
                        //    if (!isbatch)
                        //    {
                        //        txtBatchNo.Focus();
                        //        return;
                        //    }
                        //    else
                        //    {
                        //        txtProductCode.Text = existingInvBarcodeDetailTemp.ProductCode;
                        //        txtProductName.Text = existingInvBarcodeDetailTemp.ProductName;
                        //        txtBatchNo.Text = existingInvBarcodeDetailTemp.BatchNo;
                        //        txtStock.Text = existingInvBarcodeDetailTemp.Stock.ToString();
                        //        txtQty.Text = existingInvBarcodeDetailTemp.Qty.ToString();
                        //        txtCostPrice.Text = existingInvBarcodeDetailTemp.CostPrice.ToString();
                        //        txtSellingPrice.Text = existingInvBarcodeDetailTemp.SellingPrice.ToString();
                        //        txtUOM.Text = existingInvBarcodeDetailTemp.UnitOfMeasureName.ToString();
                        //        txtQty.Focus();
                        //    }
                        //}
                        //else
                        //{
                        //    txtProductCode.Text = existingInvBarcodeDetailTemp.ProductCode;
                        //    txtProductName.Text = existingInvBarcodeDetailTemp.ProductName;
                        //    txtBatchNo.Text = existingInvBarcodeDetailTemp.BatchNo;
                        //    txtStock.Text = existingInvBarcodeDetailTemp.Stock.ToString();
                        //    txtQty.Text = existingInvBarcodeDetailTemp.Qty.ToString();
                        //    txtCostPrice.Text = existingInvBarcodeDetailTemp.CostPrice.ToString();
                        //    txtSellingPrice.Text = existingInvBarcodeDetailTemp.SellingPrice.ToString();
                        //    //if (!isbatch)
                        //   // {
                        //    //    txtBatchNo.Focus();
                        //    //    return;
                        //    //}
                        //    txtQty.Focus();
                        //}
                        #endregion
                    }
                }
                else
                {
                    Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                    //if (!isbatch)
                    //{
                    //    Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    //    return;
                    //}
                    //else
                    //{
                    //    Toast.Show("BatchNo - " + txtBatchNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    //    txtBatchNo.Focus();
                    //    return;
                    //}
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void loadProductDetails(bool isCode, string strProduct, long unitofMeasureID, DateTime expiryDate, string batchNo)
        {
            try
            {
                existingInvProductMaster = new InvProductMaster();

                if (strProduct.Equals(string.Empty))
                { return; }

                InvProductMasterService InvProductMasterService = new InvProductMasterService();

                if (isCode)
                {
                    existingInvProductMaster = InvProductMasterService.GetProductsByRefCodesForBarcodePrint(strProduct);
                    if (isCode && strProduct.Equals(string.Empty))
                    {
                        txtProductName.Focus();
                        return;
                    }
                }
                else
                { existingInvProductMaster = InvProductMasterService.GetProductsByName(strProduct); }

                if (existingInvProductMaster != null)
                {
                    InvBarCodeService invBarCodeService = new InvBarCodeService();
                    if (invBarcodeDetailTempList == null)
                    { invBarcodeDetailTempList = new List<InvBarcodeDetailTemp>(); }
                    //if (!existingInvProductMaster.IsBatch)
                    //{
                    //    txtBatchNo.Text = string.Empty;
                    //    txtBatchNo.Enabled = false;
                    //}
                    //else
                    //{
                    //    txtBatchNo.Enabled = true;
                    //}

                    existingInvBarcodeDetailTemp = invBarCodeService.getBarCodeDetailTemp(invBarcodeDetailTempList, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID, batchNo);

                    if (existingInvBarcodeDetailTemp != null)
                    {
                        txtProductCode.Text = existingInvBarcodeDetailTemp.ProductCode;
                        txtProductName.Text = existingInvBarcodeDetailTemp.ProductName;
                        cmbUnit.SelectedValue = existingInvBarcodeDetailTemp.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingInvBarcodeDetailTemp.CostPrice);
                        txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingInvBarcodeDetailTemp.SellingPrice);
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingInvBarcodeDetailTemp.Qty);
                        txtStock.Text = existingInvBarcodeDetailTemp.Stock.ToString();
                        txtBatchNo.Text = existingInvBarcodeDetailTemp.BatchNo;

                        if (existingInvProductMaster.IsExpiry)
                        {
                            dtpExpiry.Value = Common.ConvertStringToDate((existingInvBarcodeDetailTemp.ExpiryDate == null ? dtpExpiry.Value.ToString() : existingInvBarcodeDetailTemp.ExpiryDate.ToString()));
                            dtpExpiry.Enabled = true;
                        }
                        else
                        {
                            dtpExpiry.Value = Common.ConvertDateTimeToDate(DateTime.Now);
                            dtpExpiry.Enabled = false;
                        }

                        Common.EnableComboBox(true, cmbUnit);
                        if (unitofMeasureID.Equals(0))
                            cmbUnit.Focus();

                    }
                }
                else
                {
                    Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                    //if (!isbatch)
                    //{
                    //    Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    //    return;
                    //}
                    //else
                    //{
                    //    Toast.Show("BatchNo - " + txtBatchNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    //    txtBatchNo.Focus();
                    //    return;
                    //}
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbLocation_Validated(object sender, EventArgs e)
        {
            try
            {
                LocationService locationService = new LocationService();
                if (cmbLocation.SelectedValue == null || locationService.IsExistsLocation(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim())).Equals(false))
                {
                    Toast.Show("Location - " + cmbLocation.Text.Trim().Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    cmbLocation.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
                
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode.Equals(Keys.Enter))
                {cmbTransaction.Focus();}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbTransaction_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    cmbTransaction_Validated(this, e); //txtReferenceDocumentNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbTransaction_Validated(object sender, EventArgs e)
        {
            try
            {
                referenceDocumentDate = dtpReferenceDocumentDate.Value;
                txtReferenceDocumentNo.Enabled = true;
                grpBody.Enabled = false;
                if (cmbTransaction.Text.Trim() == "Good Received Note")
                {
                    referenceDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote").DocumentID;
                    InvPurchaseService invPurchaseService = new InvPurchaseService();
                    Common.SetAutoComplete(txtReferenceDocumentNo, invPurchaseService.GetAllSavedDocumentNumbers(referenceDocumentID, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim())), chkAutoCompleationReferenceDocumentNo.Checked);
                    txtReferenceDocumentNo.Focus();
                }
                else if (cmbTransaction.Text.Trim() == "Purchase Order")
                {
                    referenceDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseOrder").DocumentID;
                    InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                    Common.SetAutoComplete(txtReferenceDocumentNo, invPurchaseOrderService.GetAllSavedDocumentNumbers(referenceDocumentID, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim())), chkAutoCompleationReferenceDocumentNo.Checked);
                    txtReferenceDocumentNo.Focus();
                }
                else if (cmbTransaction.Text.Trim() == "Price Change")
                {
                    InvProductPriceChangeService invProductPriceChangeService = new InvProductPriceChangeService();
                    Common.SetAutoComplete(txtReferenceDocumentNo, invProductPriceChangeService.GetAllSavedDocumentNumbers(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim())), chkAutoCompleationReferenceDocumentNo.Checked);
                    txtReferenceDocumentNo.Focus();
                }
                else if (cmbTransaction.Text.Trim() == "Temp Location - Reduce")
                {
                    InvProductPriceChangeDamageService invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
                    Common.SetAutoComplete(txtReferenceDocumentNo, invProductPriceChangeDamageService.GetAllSavedDocumentNumbers(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim())), chkAutoCompleationReferenceDocumentNo.Checked);
                    txtReferenceDocumentNo.Focus();
                }
                else if (cmbTransaction.Text.Trim() == "Stock Adjustment - Add")
                {
                    referenceDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmStockAdjustment").DocumentID;
                    InvStockAdjustmentService invStockAdjustmentService = new InvStockAdjustmentService();
                    Common.SetAutoComplete(txtReferenceDocumentNo, invStockAdjustmentService.GetSavedAddDocumentNumbers(referenceDocumentID, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim())), chkAutoCompleationReferenceDocumentNo.Checked);
                    txtReferenceDocumentNo.Focus();
                }
                else if (cmbTransaction.Text.Trim() == "Manualy Enter Item")
                {
                    dtpReferenceDocumentDate.Value = Common.GetSystemDateWithTime();
                    txtReferenceDocumentNo.Enabled = false;
                    grpBody.Enabled = true;
                    grpLeftFooter.Enabled = true;
                    txtProductCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void txtProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtProductCode.Text.Trim().Equals(string.Empty))
                    {
                        txtProductName.Enabled = true;
                        txtProductName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductCode_Leave(object sender, EventArgs e)
        {
            try
            {
                loadProductDetails(true, txtProductCode.Text.Trim(), 0, dtpExpiry.Value);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {

                    if (!txtProductName.Text.Trim().Equals(string.Empty))
                    {
                        cmbUnit.Enabled = true;
                        cmbUnit.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductName_Leave(object sender, EventArgs e)
        {
            try
            {
                loadProductDetails(false, txtProductName.Text.Trim(), 0, dtpExpiry.Value);
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
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtProductName.Text.Trim().Equals(string.Empty))
                    {
                        InvProductBatchNoTemp invProductBatchNoTemp = new InvProductBatchNoTemp();
                        //invProductBatchNoTemp.DocumentID = documentID;
                        invProductBatchNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                        invProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        LocationService locationService = new LocationService();

                        if (txtBatchNo.Text.Trim() != "")
                        {
                            InvProductBatchNoExpiaryDetail invProductBatchNo = new InvProductBatchNoExpiaryDetail();

                            CommonService commonService = new CommonService();
                            invProductBatchNo = commonService.CheckInvBatchNumber(txtBatchNo.Text.Trim(), existingInvProductMaster.InvProductMasterID, locationService.GetLocationsByName(cmbLocation.Text).LocationID, existingInvProductMaster.UnitOfMeasureID);

                            if (invProductBatchNo == null)
                            {
                                if ((Toast.Show("This Batch No Not exists!", Toast.messageType.Error, Toast.messageAction.General).Equals(DialogResult.OK)))
                                    return;
                            }
                            else
                            {
                                txtCostPrice.Text = invProductBatchNo.CostPrice.ToString();
                                txtSellingPrice.Text = invProductBatchNo.SellingPrice.ToString();
                                barcode = invProductBatchNo.BarCode;

                                if (existingInvProductMaster.IsExpiry)
                                {
                                    dtpExpiry.Enabled = true;
                                    dtpExpiry.Value = Common.ConvertStringToDateTime(invProductBatchNo.ExpiryDate.ToString());
                                    dtpExpiry.Focus();

                                }
                                else
                                {
                                    txtQty.Enabled = true;
                                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                                    {txtQty.Text = "1";}
                                    txtQty.Focus();
                                    dtpExpiry.Enabled = true;
                                }
                                return;
                            }
                        }
                        else
                        {

                            InvBarCodeService invBarCodeService = new InvBarCodeService();
                            invProductBatchNoTempList = invBarCodeService.GetBatchNoDetail(existingInvProductMaster, locationService.GetLocationsByName(cmbLocation.Text).LocationID);

                            if (invProductBatchNoTempList == null)
                                invProductBatchNoTempList = new List<InvProductBatchNoTemp>();

                            FrmBatchNumber frmBatchNumber = new FrmBatchNumber(invProductBatchNoTempList, invProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, FrmBatchNumber.transactionType.BarcodePrint, existingInvProductMaster.InvProductMasterID);
                            frmBatchNumber.ShowDialog();

                            InvProductBatchNoExpiaryDetail invProductBatchNo = new InvProductBatchNoExpiaryDetail();
                            txtBatchNo.Text = batchNumber;

                            CommonService commonService = new CommonService();

                            invProductBatchNo = commonService.CheckInvBatchNumber(txtBatchNo.Text.Trim(), existingInvProductMaster.InvProductMasterID, locationService.GetLocationsByName(cmbLocation.Text).LocationID, existingInvProductMaster.UnitOfMeasureID);

                            if (invProductBatchNo == null)
                            {
                                if ((Toast.Show("This Batch No Not exists!", Toast.messageType.Error, Toast.messageAction.General).Equals(DialogResult.OK)))
                                    return;
                            }
                            else
                            {
                                txtCostPrice.Text = invProductBatchNo.CostPrice.ToString();
                                txtSellingPrice.Text = invProductBatchNo.SellingPrice.ToString();
                                barcode = invProductBatchNo.BarCode;

                                if (existingInvProductMaster.IsExpiry)
                                {
                                    dtpExpiry.Enabled = true;
                                    dtpExpiry.Focus();
                                }
                                else
                                {
                                    txtQty.Enabled = true;
                                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                                        txtQty.Text = "1";
                                    txtQty.Focus();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            //try
            //{
            //    if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
            //    {

            //        if (!txtBatchNo.Text.Trim().Equals(string.Empty))
            //        {
            //            txtQty.Enabled = true;
            //            txtQty.Focus();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            //}
        }

        private void txtBatchNo_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    loadProductDetails(false, txtProductName.Text.Trim(), true);
            //}
            //catch (Exception ex)
            //{
            //    Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            //}
        }

        private void UpdateGrid()
        {
            try
            {
                if (Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) > 0)
                {
                    InvBarcodeDetailTemp invBarcodeDetail = new InvBarcodeDetailTemp();

                    invBarcodeDetail.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                    if (isInvProduct) { invBarcodeDetail.BaseUnitID = existingInvProductMaster.UnitOfMeasureID; }

                    invBarcodeDetail.UnitOfMeasure = cmbUnit.Text.ToString().Trim();

                    invBarcodeDetail.BarCode = existingInvBarcodeDetailTemp.BarCode;
                    invBarcodeDetail.BatchNo = txtBatchNo.Text.Trim();
                    invBarcodeDetail.ExpiryDate = dtpExpiry.Value;
                    invBarcodeDetail.CostPrice = existingInvBarcodeDetailTemp.CostPrice;
                    invBarcodeDetail.LineNo = existingInvBarcodeDetailTemp.LineNo;
                    invBarcodeDetail.ProductCode = existingInvBarcodeDetailTemp.ProductCode;
                    invBarcodeDetail.ProductID = existingInvBarcodeDetailTemp.ProductID;
                    invBarcodeDetail.ProductName = existingInvBarcodeDetailTemp.ProductName;
                    invBarcodeDetail.Qty = Common.ConvertStringToDecimalQty(txtQty.Text);
                    invBarcodeDetail.SellingPrice = existingInvBarcodeDetailTemp.SellingPrice;
                    invBarcodeDetail.Stock = existingInvBarcodeDetailTemp.Stock;
                    invBarcodeDetail.DocumentDate = dtpReferenceDocumentDate.Value;
                    invBarcodeDetail.SupplierCode = existingInvBarcodeDetailTemp.SupplierCode;
                    invBarcodeDetail.BarCode = barcode;

                    InvBarCodeService invBarCodeService = new InvBarCodeService();

                    dgvItemDetails.DataSource = null;
                    invBarcodeDetailTempList = invBarCodeService.getUpdateBarCodeDetailTemp(invBarcodeDetailTempList, invBarcodeDetail);
                    dgvItemDetails.DataSource = invBarcodeDetailTempList;
                    dgvItemDetails.Refresh();
                    txtTotQty.Text = Common.ConvertDecimalToStringQty(invBarcodeDetailTempList.GetSummaryAmount(x => x.Qty));

                    foreach (DataGridViewRow row in dgvItemDetails.Rows)
                    {
                        if (string.Equals(txtProductCode.Text.Trim(), dgvItemDetails["ProductCode", row.Index].Value.ToString()) && string.Equals(txtBatchNo.Text.Trim(), dgvItemDetails["BatchNo", row.Index].Value.ToString()))
                        {
                            isUpdateGrid = true;
                            selectedRowIndex = row.Index;
                            break;
                        }
                    }

                    if (isUpdateGrid)
                    {
                        dgvItemDetails.CurrentCell = dgvItemDetails.Rows[selectedRowIndex].Cells[0];
                        isUpdateGrid = false;
                    }
                    else
                    {
                        rowCount = dgvItemDetails.Rows.Count;
                        dgvItemDetails.CurrentCell = dgvItemDetails.Rows[rowCount - 1].Cells[0];
                    }
                    Common.EnableComboBox(false, cmbLocation);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    Common.EnableButton(false, btnDocumentDetails);

                    ClearLine();
                    EnableLine(false);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void ClearLine()
        {
            try
            {
                Common.ClearTextBox(txtProductCode, txtProductName, txtBatchNo, txtQty, txtStock, txtCostPrice, txtSellingPrice);
                txtProductCode.Focus();
                Common.ClearComboBox(cmbUnit);
                dtpExpiry.Value = DateTime.Now;
                barcode = 0;
                txtProductCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void GetPrintingDetails()
        {

            foreach (string printname in PrinterSettings.InstalledPrinters)
            {
                cmbPrinter.Items.Add(printname);
            }


            //PrinterSettings settings = new PrinterSettings();
            //foreach (PaperSize size in settings.PaperSizes)
            //{
            //    cmbPaperSize.Items.Add(size.PaperName);
            //}

            //rdoPortrait.Checked = true;
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {UpdateGrid();}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void ClearForm()
        {
            try
            {
                dgvItemDetails.DataSource = null;
                dgvItemDetails.Refresh();
                barcode = 0;
                base.ClearForm();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtReferenceDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
 
            try
            {
                if (e.KeyCode.Equals(Keys.Return) || e.KeyCode.Equals(Keys.Tab))
                {txtProductCode.Focus();}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
         }

        private void txtReferenceDocumentNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (cmbTransaction.Text.Trim() == "Good Received Note")
                {
                    referenceDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote").DocumentID;
                    //referenceDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseOrder").DocumentID;
                    InvPurchaseService invPurchaseService = new InvPurchaseService();
                    InvPurchaseHeader invPurchaseHeader = new InvPurchaseHeader();
                    
                    invPurchaseHeader = invPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtReferenceDocumentNo.Text.Trim());
                    if (invPurchaseHeader != null)
                    {
                        referenceDocumentDate = invPurchaseHeader.DocumentDate;

                        invBarcodeDetailTempList = invPurchaseService.getSavedDocumentDetailsForBarCodePrint(txtReferenceDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim()), referenceDocumentID);
                        if (invBarcodeDetailTempList == null)
                        {
                            Toast.Show("Document No - " + txtReferenceDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                            txtReferenceDocumentNo.Focus();
                            return;
                        }
                        else
                        {
                            RefreshGrid();
                            txtProductCode.Focus();
                            return;
                        }
                    }
                }
                else if (cmbTransaction.Text.Trim() == "Purchase Order")
                {
                    referenceDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseOrder").DocumentID;
                    InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                    InvPurchaseOrderHeader invPurchaseOrderHeader = new InvPurchaseOrderHeader();
                    invPurchaseOrderHeader = invPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtReferenceDocumentNo.Text.Trim());
                    if (invPurchaseOrderHeader != null)
                    {
                        referenceDocumentDate = invPurchaseOrderHeader.DocumentDate;
                        invBarcodeDetailTempList = invPurchaseOrderService.getSavedDocumentDetailsForBarCodePrint(txtReferenceDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim()), referenceDocumentID);
                        if (invBarcodeDetailTempList == null)
                        {
                            Toast.Show("Document No - " + txtReferenceDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                            txtReferenceDocumentNo.Focus();
                            return;
                        }
                        else
                        {
                            RefreshGrid();
                            txtProductCode.Focus();
                            return;
                        }
                    }
                    
                }
                else if (cmbTransaction.Text.Trim() == "Price Change")
                {
                    referenceDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmProductPriceChange").DocumentID;
                    InvProductPriceChangeService invProductPriceChangeService = new InvProductPriceChangeService();
                    InvProductPriceChangeDetail invProductPriceChange = new InvProductPriceChangeDetail();
                    invProductPriceChange = invProductPriceChangeService.GetSavedDocumentDetailsByDocumentNumber(referenceDocumentID,txtReferenceDocumentNo.Text.Trim(),Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                    if (invProductPriceChange != null)
                    {
                        referenceDocumentDate = invProductPriceChange.DocumentDate;

                        invBarcodeDetailTempList = invProductPriceChangeService.getSavedDocumentDetailsForBarCodePrint(txtReferenceDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim()), referenceDocumentID);
                        if (invBarcodeDetailTempList == null)
                        {
                            Toast.Show("Document No - " + txtReferenceDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                            txtReferenceDocumentNo.Focus();
                            return;
                        }
                        else
                        {
                            RefreshGrid();
                            txtProductCode.Focus();
                            return;
                        }
                    }
                }
                else if (cmbTransaction.Text.Trim() == "Temp Location - Reduce")
                {
                    referenceDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmProductPriceChangeDamage").DocumentID;
                    InvProductPriceChangeDamageService invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
                    InvProductPriceChangeDetailDamage invProductPriceChangeDetailDamage = new InvProductPriceChangeDetailDamage();
                    invProductPriceChangeDetailDamage = invProductPriceChangeDamageService.GetSavedDocumentDetailsByDocumentNumber(referenceDocumentID, txtReferenceDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                    if (invProductPriceChangeDetailDamage != null)
                    {
                        referenceDocumentDate = invProductPriceChangeDetailDamage.DocumentDate;

                        invBarcodeDetailTempList = invProductPriceChangeDamageService.getSavedDocumentDetailsForBarCodePrint(txtReferenceDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim()), referenceDocumentID);
                        if (invBarcodeDetailTempList == null)
                        {
                            Toast.Show("Document No - " + txtReferenceDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                            txtReferenceDocumentNo.Focus();
                            return;
                        }
                        else
                        {
                            RefreshGrid();
                            txtProductCode.Focus();
                            return;
                        }
                    }
                }
                else if (cmbTransaction.Text.Trim() == "Stock Adjustment - Add")
                {
                    referenceDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmStockAdjustment").DocumentID;
                    InvStockAdjustmentService invStockAdjustmentService = new InvStockAdjustmentService();
                    InvStockAdjustmentHeader invStockAdjustmentHeader = new InvStockAdjustmentHeader();
                    invStockAdjustmentHeader = invStockAdjustmentService.GetSavedDocumentDetailsByDocumentNumber(txtReferenceDocumentNo.Text.Trim());
                    if (invStockAdjustmentHeader != null)
                    {
                        referenceDocumentDate = invStockAdjustmentHeader.DocumentDate;
                        invBarcodeDetailTempList = invStockAdjustmentService.getSavedDocumentDetailsForBarCodePrint(txtReferenceDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim()), referenceDocumentID);
                        if (invBarcodeDetailTempList == null)
                        {
                            Toast.Show("Document No - " + txtReferenceDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                            txtReferenceDocumentNo.Focus();
                            return;
                        }
                        else
                        {
                            RefreshGrid();
                            txtProductCode.Focus();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
        
        private void RefreshGrid()
        {
            try
            {
                dgvItemDetails.DataSource = null;
                dgvItemDetails.DataSource = invBarcodeDetailTempList;
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                grpBody.Enabled = true;
                grpLeftFooter.Enabled = true;
                txtTotQty.Text = Common.ConvertDecimalToStringQty(invBarcodeDetailTempList.GetSummaryAmount(x => x.Qty));
                dgvItemDetails.Refresh();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvItemDetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                isUpdateGrid = true;
                if (dgvItemDetails.CurrentCell != null && dgvItemDetails.CurrentCell.RowIndex >= 0)
                {
                    if (dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value == null)
                    {
                        Toast.Show("No data available to display", Toast.messageType.Information, Toast.messageAction.General, "");
                        return;
                    }
                    else
                    {
                        selectedRowIndex = dgvItemDetails.CurrentCell.RowIndex;
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        loadProductDetails(true, dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim(), unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID, Common.ConvertStringToDate((dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value == null ? DateTime.Now.ToString() : dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value.ToString())), dgvItemDetails["BatchNo", dgvItemDetails.CurrentCell.RowIndex].Value.ToString());

                        txtQty.Enabled = true;
                        this.ActiveControl = txtQty;
                        txtQty.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvItemDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F2))
            {
                try
                {
                    if (dgvItemDetails.CurrentCell != null && dgvItemDetails.CurrentCell.RowIndex >= 0)
                    {
                        if (Toast.Show("Product " + dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + " - " + dgvItemDetails["ProductName", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                        {return;}

                        InvBarCodeService invBarCodeService = new Service.InvBarCodeService();
                        InvBarcodeDetailTemp invBarcodeDetailTemp = new Domain.InvBarcodeDetailTemp();

                        invBarcodeDetailTemp.ProductCode = dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim();
                        invBarcodeDetailTemp.BatchNo = dgvItemDetails["BatchNo", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim();

                        invBarcodeDetailTempList = invBarCodeService.getDeleteBarCodeDetailTemp(invBarcodeDetailTempList, invBarcodeDetailTemp);
                        RefreshGrid();
                        this.ActiveControl = txtProductCode;
                        txtProductCode.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                }
            }
        }

        public override void Save()
        {
            try
            {
                if ((Toast.Show("Barcode Printing  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.Print).Equals(DialogResult.Yes)))
                {
                    this.Cursor = Cursors.WaitCursor;

                    bool printBarCode = PrintBarCode();
                    this.Cursor = Cursors.Default;
                    if (printBarCode.Equals(true))
                    {
                        Toast.Show("Barcode Printing  - " + txtDocumentNo.Text + "", Toast.messageType.Information, Toast.messageAction.Print);
                        ClearForm();
                    }
                    else
                    {
                        Toast.Show("Barcode Printing  - " + txtDocumentNo.Text + "", Toast.messageType.Error, Toast.messageAction.Print);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool PrintBarCode()
        {
            try
            {
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                SystemConfig systemConfig = new SystemConfig();
                CommonService commonDetails = new CommonService();

                ReferenceType referenceType = new ReferenceType();
                
                StreamWriter m_streamWriter;

                string @barcodeTextPath, @appPath, @destinationPath;
                bool blnLocalCopy = false, folderExists = false;
                string txtFileName = "", exeFileName = "", tagFileName = "", sourceFile = "", destFile = "";

                txtFileName = "bar.txt";
                exeFileName = "Barcode.exe";

                referenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.TagType).ToString(), cmbTag.Text.Trim());
                if (referenceType != null)
                {
                    tagFileName = string.Concat(referenceType.LookupValue, ".lbx");
                }

                @appPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location),"BarCode");
               
                systemConfig = commonDetails.GetSystemInfo(1);

                if (systemConfig != null)
                {
                    @barcodeTextPath = @systemConfig.BarcodeTextPath; //@"C:\Barcode";
                }
                else
                {
                    return false;
                }
                 
                @destinationPath = @barcodeTextPath;

                folderExists = Directory.Exists(@destinationPath);

                if (!folderExists)
                {
                    folderExists = true;
                    blnLocalCopy = Common.CopyDirectory(@appPath, @destinationPath, true);
                }
                
                if (folderExists)
                {
                    FileStream fileStream = new FileStream(@destinationPath + @"\\" + txtFileName, FileMode.Create);
                    m_streamWriter = new StreamWriter(fileStream);

                    foreach (InvBarcodeDetailTemp invBarcodeDetailTemp in invBarcodeDetailTempList)
                    {
                        for (int count = 0; count < invBarcodeDetailTemp.Qty; count = count + 1)
                        {
                            string strSellingPrice = (string.Format("{0:#0.##}", invBarcodeDetailTemp.SellingPrice));                            

                            m_streamWriter.WriteLine(invBarcodeDetailTemp.ProductID + "," +
                                                        invBarcodeDetailTemp.ProductCode + "," +
                                                        invBarcodeDetailTemp.ProductName + ", " +
                                                        invBarcodeDetailTemp.BarCode + ", " +
                                                        invBarcodeDetailTemp.BatchNo + "," +
                                                        invBarcodeDetailTemp.SupplierCode + "," + 
                                                        invBarcodeDetailTemp.DocumentDate.ToString("ddMMyy") + "," +
                                                        (!strSellingPrice.Contains('.') ? string.Concat(strSellingPrice, "/-") : string.Format("{0:#0.00}", invBarcodeDetailTemp.SellingPrice)) + "," +
                                                        invBarcodeDetailTemp.OldSellingPrice);
                        }
                    }
                    m_streamWriter.Close();
                    fileStream.Close();
                    
                    if (File.Exists(@destinationPath + @"\\" + txtFileName))
                    {
                        Process.Start(@destinationPath + @"\\" + tagFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    cmbUnit_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbUnit_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbUnit.SelectedValue == null)
                    return;

                if (!existingInvProductMaster.UnitOfMeasureID.Equals(Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())))
                {
                    InvProductUnitConversionService invProductUnitConversionService = new InvProductUnitConversionService();
                    if (invProductUnitConversionService.GetProductUnitByProductCode(existingInvProductMaster.InvProductMasterID, Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())) == null)
                    {
                        Toast.Show("Unit - " + cmbUnit.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "Product - " + txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "");
                        dtpExpiry.Enabled = false;
                        cmbUnit.SelectedValue = existingInvProductMaster.UnitOfMeasureID;
                        cmbUnit.Focus();
                        return;
                    }
                }
                loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()), dtpExpiry.Value);

                if (existingInvProductMaster.IsBatch)
                {
                    txtBatchNo.Enabled = true;
                    txtBatchNo.Focus();
                }
                else if (existingInvProductMaster.IsExpiry)
                {
                    dtpExpiry.Enabled = true;
                    dtpExpiry.Focus();
                }
                else if (existingInvProductMaster.IsSerial)
                {
                    txtQty.Enabled = true;
                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                        txtQty.Text = "1";
                    txtQty.Focus();
                }
                else
                {
                    txtQty.Enabled = true;
                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                        txtQty.Text = "1";
                    txtQty.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtProductName.Text.Trim().Equals(string.Empty))
                    {
                        InvProductBatchNoTemp invProductBatchNoTemp = new InvProductBatchNoTemp();
                        invProductBatchNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                        invProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        InvBarCodeService invBarCodeService = new InvBarCodeService();
                        invProductBatchNoTempList = invBarCodeService.GetExpiryDetail(existingInvProductMaster, txtBatchNo.Text.Trim());

                        txtQty.Enabled = true;
                        if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                        {txtQty.Text = "1";}
                        txtQty.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpReferenceDocumentDate_Leave(object sender, EventArgs e)
        {
            
            try
            {
                if (cmbTransaction.Text.Trim() == "Manualy Enter Item")
                {
                    
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbPrinter_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                //LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                //List<ReferenceType> referenceTypeList = new List<ReferenceType>();
                //if(cmbPrinter.Text.Contains("Zebra 110"))

                //    string[] fileArray = Directory.GetFiles(@"c:\Dir\", "*.jpg");

                //    referenceTypeList = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.TitleType).ToString(), cmbTag.Text.Trim());
                //if (referenceType != null)
                //{
                //    tagFileName = string.Concat(referenceType.Remark, ".lbx");
                //}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbTransaction_Validated(this, e); //txtReferenceDocumentNo.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvItemDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
