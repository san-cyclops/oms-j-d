using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using Report.Logistic; 

namespace UI.Windows
{
    public partial class FrmLogisticTransferOfGoodsNote : UI.Windows.FrmBaseTransactionForm
    {
        /// <summary>
        /// Transfer Of Goods Note (TOG)
        /// Design By - C.S.Malluwawadu
        /// Developed By - C.S.Malluwawadu
        /// Date - 15/08/2013
        /// </summary>
        /// 

        int documentState;
        int documentID = 6;
        bool isInvProduct;
        static string batchNumber;
        static DateTime expiryDate;
        bool recallGRN;
        bool isValidControls = true;
        decimal convertFactor = 1;
        bool isMinusStock = false;
        bool isBackDated;
        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;

        bool recallMAN = false; 

        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        LgsProductMaster existingLgsProductMaster;

        LgsTransferDetailsTemp existingLgsTransferDetailTemp = new LgsTransferDetailsTemp();
        List<LgsTransferDetailsTemp> lgsTransferDetailsTempList = new List<LgsTransferDetailsTemp>();
        LgsTransferDetailsTemp existingLgsTransferDetailsTemp = new LgsTransferDetailsTemp();
        List<LgsProductBatchNoTemp> lgsProductBatchNoTempList = new List<LgsProductBatchNoTemp>();
        List<InvProductSerialNoTemp> lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();
        List<LgsPurchaseDetailTemp> lgsPurchaseDetailTempList = new List<LgsPurchaseDetailTemp>();
        List<LgsMinusProductDetailsTemp> lgsMinusProductDetailsTempList = new List<LgsMinusProductDetailsTemp>();

        public FrmLogisticTransferOfGoodsNote()
        {
            InitializeComponent();
        }      

        public override void FormLoad()
        {
            try
            {
                // Load Unit of measures
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                Common.LoadUnitOfMeasures(cmbUnit, unitOfMeasureService.GetAllUnitOfMeasures());

                // Load Locations
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbFromLocation, locationService.GetAllLocations());

                // Load TransferTypes
                LgsTransferTypeService lgsTransferTypeService = new LgsTransferTypeService();
                Common.LoadLgsTransferTypes(cmbTransferType, lgsTransferTypeService.GetAllTransferTypes());

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                chkOverwrite.Checked = autoGenerateInfo.IsOverWriteQty;
                documentID = autoGenerateInfo.DocumentID;
                isBackDated = autoGenerateInfo.IsBackDated;

                if (isBackDated)
                {
                    dtpTogDate.Enabled = true;
                }
                else
                {
                    dtpTogDate.Enabled = false;
                }

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }
                
                isInvProduct = false;

                base.FormLoad();

                //////Load Document Numbers
                LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();
                Common.SetAutoComplete(txtDocumentNo, lgsTransferOfGoodsService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbFromLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

                ////Load GRN Document Numbers 
                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                Common.SetAutoComplete(txtGrnNo, lgsPurchaseService.GetAllDocumentNumbersAsReference(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticGoodsReceivedNote").DocumentID, Convert.ToInt32(cmbFromLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

                chkAutoCompleationDocumentNo.Checked = true;
                chkAutoCompleationGrnNo.Checked = true;
                chkAutoCompleationManNo.Checked = true;

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
        }

        public override void InitializeForm()
        {
            try
            {
                // Disable product details controls
                dgvItemDetails.DataSource = null;
                grpFooter.Enabled = false;
                
                EnableLine(false);
                Common.EnableTextBox(true, txtDocumentNo, txtGrnNo, txtRemark, txtReference, txtManNo);
                Common.EnableTextBox(false, txtProductCode, txtProductName);
                Common.EnableButton(true, btnDocumentDetails, btnGrnDetails, btnManDetails);
                Common.EnableComboBox(true, cmbFromLocation, cmbToLocation, cmbTransferType);
                Common.EnableButton(false, btnSave, btnPause);
                dtpExpiry.Value = DateTime.Now;
                dtpTogDate.Value = DateTime.Now;

                //ChkBarCodeScan.Enabled = true;

                cmbFromLocation.SelectedValue = Common.LoggedLocationID;
                cmbToLocation.SelectedValue = -1;
                cmbTransferType.SelectedValue = -1;

                cmbTransferType.SelectedIndex = 0;

                //txtDocumentNo.Text = GetDocumentNo(true);
                this.ActiveControl = cmbFromLocation;

                existingLgsProductMaster = null;
                existingLgsTransferDetailsTemp = null;
                existingLgsTransferDetailTemp = null;
                batchNumber = null;

                lgsProductBatchNoTempList = null;
                lgsProductSerialNoTempList = null;
                lgsTransferDetailsTempList = null;

                selectedRowIndex = 0;
                isUpdateGrid = false;
                rowCount = 0;

                recallGRN = false;
                recallMAN = false;

                isInvProduct = false;

                ShowDocumentNo();
                LoadbalancedAllocationDocuments();

                cmbFromLocation.Focus();

                dgvBatchStock.DataSource = null;

                if (isBackDated)
                {
                    dtpTogDate.Enabled = true;
                }
                else
                {
                    dtpTogDate.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private string GetDocumentNo(bool isTemporytNo)
        {
            try
            {
                LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();
                LocationService locationService = new LocationService();
                return lgsTransferOfGoodsService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }

        }

        private void EnableLine(bool enable)
        {
            Common.EnableTextBox(enable, txtQty, txtCostPrice, txtSellingPrice, txtBatchNo, txtAmount, txtBatchNo);
            Common.EnableComboBox(enable, cmbUnit);
            dtpExpiry.Enabled = enable;
        }

        public void LoadLoacationsExceptingCurrentLocation()
        {
            LocationService locationService = new LocationService();
            Common.LoadLocations(cmbToLocation, locationService.GetLocationsExceptingCurrentLocation(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString())));
        }

        private void cmbFromLocation_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                // Load Loacations Excepting Current Location
                if (cmbFromLocation.SelectedValue!=null && Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()) != -1)
                LoadLoacationsExceptingCurrentLocation();
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
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtProductName.Enabled = true;
                    txtProductName.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateControls()
        {
            if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo))
            { return false; }
            else if (!Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbFromLocation, cmbToLocation, cmbTransferType))
            { return false; }

            else
            {
                isValidControls = true;
                this.ValidateChildren();

                return isValidControls;
            }
        }

        private bool ValidateTextBoxes() 
        {
            if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo))
            { return false; }
            else
            { return true; }
        }

        private bool ValidateComboBoxes()
        {
            if (!Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbFromLocation, cmbToLocation, cmbTransferType))
            { return false; }
            else
            { return true; }
        }

        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    //if (ValidateControls() == false) return;

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

        private void txtProductCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProductCode.Text.Trim())) { return; }

                if (recallMAN)
                {
                    LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();
                    LgsMaterialAllocationHeader lgsMaterialAllocationHeader = new LgsMaterialAllocationHeader();
                    LgsProductMaster lgsProductMaster = new LgsProductMaster();
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                    lgsProductMaster = lgsProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());
                    lgsMaterialAllocationHeader=lgsMaterialAllocationService.GetPausedMaterialAllocationHeaderByDocumentNo(txtManNo.Text.Trim());

                    if (lgsProductMaster != null)
                    {
                        if (!lgsMaterialAllocationService.CheckIsProductExisInMAN(lgsMaterialAllocationHeader, lgsProductMaster.LgsProductMasterID))
                        {
                            Toast.Show("This product not exists for above MA Note", Toast.messageType.Information, Toast.messageAction.General);
                            txtProductCode.SelectAll();
                            txtProductCode.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (lgsMaterialAllocationService.CheckIsProductExisInMAN(lgsMaterialAllocationHeader, 0))
                        {

                        }
                    }
                }

                if (ChkBarCodeScan.Checked)
                {
                    if (loadBarcodeDetails(Common.ConvertStringToLong(txtProductCode.Text.Trim())))
                    {
                        CalculateLine();
                        if (existingLgsTransferDetailsTemp != null)
                        {
                            UpdateBatchGrid(existingLgsTransferDetailsTemp);
                        }
                    }
                }
                else
                {
                    loadProductDetails(true, txtProductCode.Text.Trim(), 0, dtpExpiry.Value);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void loadProductDetails(bool isCode, string strProduct, long unitofMeasureID, DateTime expiryDate)
        {
            try
            {
                existingLgsProductMaster = new LgsProductMaster();

                if (strProduct.Equals(string.Empty))
                    return;

                LgsProductMasterService LgsProductMasterService = new LgsProductMasterService();

                if (isCode)
                {

                    existingLgsProductMaster = LgsProductMasterService.GetProductsByRefCodes(strProduct);
                    if (isCode && strProduct.Equals(string.Empty))
                    {
                        txtProductName.Focus();
                        return;
                    }
                }
                else
                    existingLgsProductMaster = LgsProductMasterService.GetProductsByName(strProduct); ;

                if (existingLgsProductMaster != null)
                {
                    LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();
                    if (lgsTransferDetailsTempList == null)
                        lgsTransferDetailsTempList = new List<LgsTransferDetailsTemp>();
                    existingLgsTransferDetailsTemp = lgsTransferOfGoodsService.getTransferDetailTemp(lgsTransferDetailsTempList, existingLgsProductMaster, Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID);
                    if (existingLgsTransferDetailsTemp != null)
                    {
                        txtProductCode.Text = existingLgsTransferDetailsTemp.ProductCode;
                        txtProductName.Text = existingLgsTransferDetailsTemp.ProductName;
                        cmbUnit.SelectedValue = existingLgsTransferDetailsTemp.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingLgsTransferDetailsTemp.CostPrice);
                        txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingLgsTransferDetailsTemp.SellingPrice);
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingLgsTransferDetailsTemp.Qty);
                        txtBatchNo.Text = existingLgsTransferDetailsTemp.BatchNo;
                        
                        if (existingLgsProductMaster.IsExpiry)
                        {
                            dtpExpiry.Value = Common.ConvertStringToDate((existingLgsTransferDetailsTemp.ExpiryDate == null ? dtpExpiry.Value.ToString() : existingLgsTransferDetailsTemp.ExpiryDate.ToString()));
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

                        if (chkViewStokDetails.Checked) { ShowStockDetails(existingLgsTransferDetailsTemp.ProductID); }                        
                    }
                }
                else
                {
                    Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    if (isCode) { txtProductCode.Focus(); txtProductCode.SelectAll(); }
                    else { txtProductName.Focus(); txtProductName.SelectAll(); }
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool loadBarcodeDetails(long barcode) 
        {
            try
            {
                existingLgsProductMaster = new LgsProductMaster();
                LgsProductMasterService LgsProductMasterService = new LgsProductMasterService();

                LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();
                if (lgsTransferDetailsTempList == null) { lgsTransferDetailsTempList = new List<LgsTransferDetailsTemp>(); }

                LgsBatchNoExpiaryDetailService lgsBatchNoExpiaryDetailService = new LgsBatchNoExpiaryDetailService();
                LgsProductBatchNoExpiaryDetail lgsProductBatchNoExpiaryDetail = new LgsProductBatchNoExpiaryDetail();
                lgsProductBatchNoExpiaryDetail = lgsBatchNoExpiaryDetailService.GetBatchNoExpiaryDetailByBarcode(barcode);

                if (lgsProductBatchNoExpiaryDetail != null)
                {
                    existingLgsTransferDetailsTemp = lgsTransferOfGoodsService.GetTransferDetailTempForBarcodeScan(barcode);

                    if (existingLgsTransferDetailsTemp != null)
                    {
                        txtProductCode.Text = existingLgsTransferDetailsTemp.ProductCode;
                        txtProductName.Text = existingLgsTransferDetailsTemp.ProductName;
                        cmbUnit.Text = existingLgsTransferDetailsTemp.UnitOfMeasure;
                        txtBatchNo.Text = existingLgsTransferDetailsTemp.BatchNo;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingLgsTransferDetailsTemp.CostPrice);
                        txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingLgsTransferDetailsTemp.SellingPrice);
                        txtQty.Text = existingLgsTransferDetailsTemp.Qty.ToString();

                        if (existingLgsTransferDetailsTemp.ExpiryDate == null)
                        {
                            dtpExpiry.Value = Common.ConvertDateTimeToDate(DateTime.Now);
                            dtpExpiry.Enabled = false;
                        }
                        else
                        {
                            dtpExpiry.Value = Common.ConvertStringToDate((existingLgsTransferDetailsTemp.ExpiryDate.ToString()));
                            dtpExpiry.Enabled = true;
                        }

                        Common.EnableComboBox(true, cmbUnit);

                        if (chkViewStokDetails.Checked) { ShowStockDetails(existingLgsTransferDetailsTemp.ProductID); }

                        return true;
                    }
                    else
                    {
                        Toast.Show("Invalid barcode", Toast.messageType.Information, Toast.messageAction.General);
                        txtProductCode.Focus();
                        return false;
                    }
                }
                else
                {
                    Toast.Show("Invalid barcode", Toast.messageType.Information, Toast.messageAction.General);
                    txtProductCode.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return false;
            }
        }

        private void ShowStockDetails(long productID = 0)
        {
            CommonService CommonService = new CommonService();

            dgvStockDetails.DataSource = null;
            dgvStockDetails.AutoGenerateColumns = false;
            List<LgsProductStockMaster> lgsProductStockMasterList = new List<LgsProductStockMaster>();
            lgsProductStockMasterList = CommonService.GetLgsStockDetailsToStockGrid(existingLgsProductMaster);
            dgvStockDetails.DataSource = lgsProductStockMasterList;
            dgvStockDetails.Refresh();

            // Batch wise stock

            dgvBatchStock.DataSource = null;
            dgvBatchStock.AutoGenerateColumns = false;
            List<LgsProductBatchNoExpiaryDetail> lgsProductBatchNoList = new List<LgsProductBatchNoExpiaryDetail>();
            lgsProductBatchNoList = CommonService.GetLgsBatchStockDetailsToStockGrid(productID, Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));
            dgvBatchStock.DataSource = lgsProductBatchNoList;
            dgvBatchStock.Refresh();
        }

        private void LoadProductsAccordingToMAN(LgsMaterialAllocationHeader lgsMaterialAllocationHeader) 
        {
            try
            {
                LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();

                Common.SetAutoComplete(txtProductCode, lgsMaterialAllocationService.GetAllProductCodesAccordingToMAN(lgsMaterialAllocationHeader), chkAutoCompleationProduct.Checked);
                Common.SetAutoComplete(txtProductName, lgsMaterialAllocationService.GetAllProductNamesAccordingToMAN(lgsMaterialAllocationHeader), chkAutoCompleationProduct.Checked);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

         
        private void LoadProducts()
        {
            try
            {
                //InvProductMasterService invProductMasterService = new InvProductMasterService();

                //Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                //Common.SetAutoComplete(txtProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);


                LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();

                Common.SetAutoComplete(txtProductCode, lgsTransferOfGoodsService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                Common.SetAutoComplete(txtProductName, lgsTransferOfGoodsService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationProduct_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadProducts();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbFromLocation_Validated(object sender, EventArgs e)
        {
            try
            {
                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbFromLocation.Text.Trim()).LocationID, documentID);
                if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }

                LoadProducts();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void SetSerialNoList(List<InvProductSerialNoTemp> setLgsProductSerialNoTemp) 
        {
            lgsProductSerialNoTempList = setLgsProductSerialNoTemp;
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) > 0)
                    {

                        CommonService commonService = new CommonService();
                        //if (commonService.ValidateCurrentStock(Convert.ToInt32(txtQty.Text.Trim()), existingInvProductMaster, Convert.ToInt32(cmbFromLocation.SelectedValue)))

                        if (!existingLgsProductMaster.UnitOfMeasureID.Equals(Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())))
                        {
                            LgsProductUnitConversionService lgsProductUnitConversionService = new LgsProductUnitConversionService();
                            
                            convertFactor = lgsProductUnitConversionService.GetProductUnitByProductCode(existingLgsProductMaster.LgsProductMasterID, Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())).ConvertFactor;
                            
                        }
                        else
                        {
                            convertFactor = 1;
                        }
                        if (commonService.ValidateBatchStockForLgsTOG(Common.ConvertStringToDecimalQty(txtQty.Text.Trim()), existingLgsProductMaster, Convert.ToInt32(cmbFromLocation.SelectedValue), txtBatchNo.Text.Trim(), convertFactor, existingLgsTransferDetailsTemp.LgsProductBatchNoExpiaryDetailID))
                        {
                            if (existingLgsProductMaster.IsSerial)
                            {
                                InvProductSerialNoTemp lgsProductSerialNoTemp = new InvProductSerialNoTemp();
                                lgsProductSerialNoTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                                lgsProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                                LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();
                                lgsProductSerialNoTempList = lgsTransferOfGoodsService.GetSerialNoDetail(existingLgsProductMaster);

                                if (lgsProductSerialNoTempList == null)
                                    lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                                //FrmSerialCommon frmSerialCommon = new FrmSerialCommon(lgsProductSerialNoTempList, lgsProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), isInvProduct, documentID, FrmSerialCommon.transactionType.TransferOfGoods);
                                FrmSerialCommon frmSerialCommon = new FrmSerialCommon(lgsProductSerialNoTempList, lgsProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), isInvProduct, documentID,FrmSerialCommon.transactionType.LogisticTransferOfGoods);
                                frmSerialCommon.ShowDialog();

                                txtAmount.Enabled = true;
                                txtAmount.Focus();
                                CalculateLine();
                            }
                            else
                            {
                                txtAmount.Enabled = true;
                                txtAmount.Focus();
                                CalculateLine();
                            }
                        }
                        else
                        {
                            Toast.Show("Transfer", Toast.messageType.Information, Toast.messageAction.QtyExceed);
                            txtAmount.Enabled = false;
                            this.ActiveControl = txtQty;
                            txtQty.Focus();
                            txtQty.SelectAll();
                            return;
                        }
                        CalculateLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (recallGRN)
                {
                    if (string.IsNullOrEmpty(txtQty.Text.Trim())) { txtQty.Text = "0"; }

                    decimal qty = Convert.ToDecimal(txtQty.Text.Trim());

                    LgsProductMaster lgsProductMaster = new LgsProductMaster();
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                    lgsProductMaster = lgsProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());
                    if (lgsProductMaster != null)
                    {
                        LgsPurchaseHeader lgsPurchaseHeader = new LgsPurchaseHeader();
                        LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                        lgsPurchaseHeader = lgsPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());

                        if (!lgsPurchaseService.IsValidNoOfQty(qty, lgsProductMaster.LgsProductMasterID, Convert.ToInt32(cmbUnit.SelectedValue), lgsPurchaseHeader.LgsPurchaseHeaderID))
                        {
                            Toast.Show("Invalid Qty.\nQty cannot grater then GRN Qty", Toast.messageType.Information, Toast.messageAction.General, "");
                            txtQty.Focus();
                            txtQty.SelectAll();
                        }
                    }
                    else
                    {
                        Toast.Show("Product", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }
                }
                else if (recallMAN)
                {
                    if (string.IsNullOrEmpty(txtQty.Text.Trim())) { txtQty.Text = "0"; }

                    decimal qty = Convert.ToDecimal(txtQty.Text.Trim());

                    LgsProductMaster lgsProductMaster = new LgsProductMaster();
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                    lgsProductMaster = lgsProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());
                    if (lgsProductMaster != null)
                    {
                        LgsMaterialAllocationHeader lgsMaterialAllocationHeader = new LgsMaterialAllocationHeader();
                        LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();
                        lgsMaterialAllocationHeader = lgsMaterialAllocationService.GetSavedDocumentDetailsByDocumentNumber(txtManNo.Text.Trim());

                        if (!lgsMaterialAllocationService.IsValidNoOfQty(qty, lgsProductMaster.LgsProductMasterID, Convert.ToInt32(cmbUnit.SelectedValue), lgsMaterialAllocationHeader.LgsMaterialAllocationHeaderID))
                        {
                            Toast.Show("Invalid Qty.\nQty cannot grater then MA Note qty", Toast.messageType.Information, Toast.messageAction.General, "");
                            txtQty.Focus();
                            txtQty.SelectAll();
                        }
                    }
                    else
                    {
                        Toast.Show("Product", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }
                }
                else
                {
                    if (Common.ConvertStringToDecimalQty(txtQty.Text) > 0)
                    {
                        CalculateLine();

                        CommonService commonService = new CommonService();
                        if (!commonService.ValidateBatchStockForLgsTOG(Common.ConvertStringToDecimalQty(txtQty.Text), existingLgsProductMaster, Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), txtBatchNo.Text.Trim(), 1, existingLgsTransferDetailsTemp.LgsProductBatchNoExpiaryDetailID))
                        {
                            Toast.Show("Invalid qty.\nQty cannot grater then batch qty", Toast.messageType.Information, Toast.messageAction.General, "");
                            txtQty.Focus();
                            txtQty.SelectAll();
                        }

                    }
                    else
                    {
                        Toast.Show("Qty", Toast.messageType.Information, Toast.messageAction.ZeroQty);

                        txtQty.Focus();
                        txtQty.SelectAll();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) > 0)
                    {
                        UpdateGrid(existingLgsTransferDetailsTemp);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void UpdateGrid(LgsTransferDetailsTemp lgsTransferDetailTemp)
        {
            try
            {
                decimal qty = 0;

                //if ((Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) > 0) && (Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text.Trim()) > 0))
                if ((Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) > 0))
                {

                    if (chkOverwrite.Checked)
                    {
                        string productCode = txtProductCode.Text.Trim();
                        string unit = cmbUnit.Text.Trim();

                        if (dgvItemDetails["ProductCode", 0].Value == null) { }
                        else
                        {
                            for (int i = 0; i < dgvItemDetails.RowCount; i++)
                            {
                                if (productCode.Equals(dgvItemDetails["ProductCode", i].Value.ToString()) && unit.Equals(dgvItemDetails["Unit", i].Value.ToString()))
                                {
                                    if (Toast.Show("", Toast.messageType.Question, Toast.messageAction.OverwriteQty).Equals(DialogResult.Yes)) { }
                                    else { return; }
                                }
                            }
                        }
                    }
                    else
                    {
                        qty = (lgsTransferDetailTemp.Qty + Common.ConvertStringToDecimalQty(txtQty.Text.Trim()));
                    }

                    if (!chkOverwrite.Checked)
                    {
                        if ((!txtGrnNo.Text.Trim().Equals(string.Empty)))
                        {
                            if ((existingLgsProductMaster != null))
                            {
                                LgsPurchaseHeader lgsPurchaseHeader = new LgsPurchaseHeader();
                                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                                lgsPurchaseHeader = lgsPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());

                                if (!lgsPurchaseService.IsValidNoOfQty(qty, existingLgsProductMaster.LgsProductMasterID, Convert.ToInt32(cmbUnit.SelectedValue), lgsPurchaseHeader.LgsPurchaseHeaderID))
                                {
                                    Toast.Show("Invalid Qty.\nQty cannot grater then GRN Qty", Toast.messageType.Information, Toast.messageAction.General, "");
                                    txtQty.Focus();
                                    txtQty.SelectAll();
                                    return;
                                }
                            }
                            else
                            {
                                Toast.Show("Product", Toast.messageType.Information, Toast.messageAction.NotExists);
                            }
                        }

                        CalculateLine(qty);
                        lgsTransferDetailTemp.Qty = Common.ConvertDecimalToDecimalQty(qty);
                    }
                    else
                    {
                        CalculateLine();
                        lgsTransferDetailTemp.Qty = Common.ConvertStringToDecimalQty(txtQty.Text);
                    }

                    lgsTransferDetailTemp.DocumentNo = txtDocumentNo.Text.Trim();
                    lgsTransferDetailTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                    lgsTransferDetailTemp.ProductCode = txtProductCode.Text.Trim();
                    lgsTransferDetailTemp.ProductName = txtProductName.Text.Trim();
                    lgsTransferDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                    lgsTransferDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();
                    lgsTransferDetailTemp.BaseUnitID = existingLgsProductMaster.UnitOfMeasureID;
                    lgsTransferDetailTemp.Qty = Common.ConvertStringToDecimalQty(txtQty.Text);

                    CalculateLine();


                    lgsTransferDetailTemp.BatchNo = txtBatchNo.Text.Trim();
                    lgsTransferDetailTemp.ExpiryDate = Common.FormatDate(dtpExpiry.Value);

                    lgsTransferDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text);
                    lgsTransferDetailTemp.SellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text);
                    lgsTransferDetailTemp.NetAmount = Common.ConvertStringToDecimalCurrency(txtAmount.Text);

                    LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();

                    dgvItemDetails.DataSource = null;
                    lgsTransferDetailsTempList = lgsTransferOfGoodsService.getUpdateLgsTransferDetailTemp(lgsTransferDetailsTempList, lgsTransferDetailTemp, existingLgsProductMaster);
                    dgvItemDetails.AutoGenerateColumns = false;
                    dgvItemDetails.DataSource = lgsTransferDetailsTempList;
                    dgvItemDetails.Refresh();

                    foreach (DataGridViewRow row in dgvItemDetails.Rows)
                    {
                        if (string.Equals(txtProductCode.Text.Trim(), dgvItemDetails["ProductCode", row.Index].Value.ToString()))
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

                    GetSummarizeFigures(lgsTransferDetailsTempList);
                    EnableLine(false);
                    Common.EnableTextBox(false, txtDocumentNo);
                    Common.EnableComboBox(false, cmbFromLocation);
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    ClearLine();

                    if (lgsTransferDetailsTempList.Count > 0)
                        grpFooter.Enabled = true;

                    txtProductCode.Enabled = true;
                    txtProductCode.Focus();
                }
                else
                {
                    Toast.Show("Operation", Toast.messageType.Information, Toast.messageAction.Invalid);
                    txtProductCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void UpdateBatchGrid(LgsTransferDetailsTemp lgsTransferDetailTemp) 
        {
            try
            {
                string productCode = txtProductCode.Text.Trim();
                string unit = cmbUnit.Text.Trim();
                string batchNo = txtBatchNo.Text.Trim();

                if (dgvItemDetails["ProductCode", 0].Value == null) { }
                else
                {
                    for (int i = 0; i < dgvItemDetails.RowCount; i++)
                    {
                        if (productCode.Equals(dgvItemDetails["ProductCode", i].Value.ToString()) && unit.Equals(dgvItemDetails["Unit", i].Value.ToString()) && batchNo.Equals((dgvItemDetails["BatchNo", i].Value.ToString())))
                        {
                            decimal tot, qty;
                            qty = Convert.ToDecimal(dgvItemDetails["Qty", i].Value.ToString());
                            tot = qty + Convert.ToDecimal(txtQty.Text.Trim());
                            txtQty.Text = tot.ToString();
                        }
                    }
                }

                
                lgsTransferDetailTemp.DocumentNo = txtDocumentNo.Text.Trim();
                lgsTransferDetailTemp.ProductID = lgsTransferDetailTemp.ProductID;
                lgsTransferDetailTemp.ProductCode = txtProductCode.Text.Trim();
                lgsTransferDetailTemp.ProductName = txtProductName.Text.Trim();

                if (cmbUnit.SelectedValue != null) { lgsTransferDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()); }
                else { lgsTransferDetailTemp.UnitOfMeasureID = 0; }
                
                lgsTransferDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();
                lgsTransferDetailTemp.BaseUnitID = existingLgsProductMaster.UnitOfMeasureID;
                lgsTransferDetailTemp.Qty = Common.ConvertStringToDecimalQty(txtQty.Text);

                CalculateLine();

                lgsTransferDetailTemp.BatchNo = txtBatchNo.Text.Trim();
                lgsTransferDetailTemp.ExpiryDate = Common.FormatDate(dtpExpiry.Value);
                lgsTransferDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text);
                lgsTransferDetailTemp.SellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text);
                lgsTransferDetailTemp.NetAmount = Common.ConvertStringToDecimalCurrency(txtAmount.Text);

                LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();

                dgvItemDetails.DataSource = null;
                lgsTransferDetailsTempList = lgsTransferOfGoodsService.GetUpdateLgsTransferDetailTempForBarcodeScan(lgsTransferDetailsTempList, lgsTransferDetailTemp);
                dgvItemDetails.AutoGenerateColumns = false;
                dgvItemDetails.DataSource = lgsTransferDetailsTempList;
                dgvItemDetails.Refresh();

                foreach (DataGridViewRow row in dgvItemDetails.Rows)
                {
                    if (string.Equals(txtProductCode.Text.Trim(), dgvItemDetails["ProductCode", row.Index].Value.ToString()))
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

                GetSummarizeFigures(lgsTransferDetailsTempList);
                EnableLine(false);
                Common.EnableTextBox(false, txtDocumentNo);
                Common.EnableComboBox(false, cmbFromLocation, cmbToLocation, cmbTransferType);
                if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                ClearLine();

                if (lgsTransferDetailsTempList.Count > 0)
                    grpFooter.Enabled = true;

                txtProductCode.Enabled = true;
                txtProductCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode, txtProductName, txtBatchNo, txtQty, txtCostPrice, txtSellingPrice, txtAmount);
            Common.ClearComboBox(cmbUnit);
            dtpExpiry.Value = DateTime.Now;
            txtProductCode.Focus();
        }

        private void CalculateLine(decimal qty = 0)
        {
            try
            {
                if (qty == 0)
                {

                    txtAmount.Text = ((Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim())) * (Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text.Trim()))).ToString();
                }
                else
                {

                    txtAmount.Text = ((Common.ConvertDecimalToDecimalQty(qty)) * (Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text.Trim()))).ToString();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void GetSummarizeFigures(List<LgsTransferDetailsTemp> listItem)
        {
            decimal totAmount = listItem.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.NetAmount);
            decimal totQty = listItem.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.Qty);
            
            txtTotalAmount.Text = Common.ConvertDecimalToStringCurrency(totAmount);
            txtTotalQty.Text = Common.ConvertDecimalToStringQty(totQty);
        }

        private void cmbFromLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (cmbFromLocation.SelectedIndex != -1)
                    {
                        cmbToLocation.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbToLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (cmbFromLocation.SelectedIndex != -1)
                    {
                        if (dtpTogDate.Enabled)
                        {
                            dtpTogDate.Focus();
                        }
                        else
                        {
                            txtReference.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpTogDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (cmbFromLocation.SelectedIndex != -1)
                    {
                        txtReference.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtReference_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtRemark.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    cmbTransferType.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbTransferType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    cmbTransferType_Validated(this, e);
                    //txtProductCode.Focus();
                
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

                if (existingLgsProductMaster != null)
                {
                    if (!existingLgsProductMaster.UnitOfMeasureID.Equals(Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())))
                    {
                        LgsProductUnitConversionService lgsProductUnitConversionService = new LgsProductUnitConversionService();
                        if (lgsProductUnitConversionService.GetProductUnitByProductCode(existingLgsProductMaster.LgsProductMasterID, Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())) == null)
                        {
                            Toast.Show("Unit - " + cmbUnit.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "Product - " + txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "");
                            dtpExpiry.Enabled = false;
                            cmbUnit.SelectedValue = existingLgsProductMaster.UnitOfMeasureID;
                            cmbUnit.Focus();
                            return;
                        }
                    }
                }

                loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()), dtpExpiry.Value);

                if (existingLgsProductMaster.IsBatch)
                {
                    txtBatchNo.Enabled = true;
                    txtBatchNo.Focus();
                }
                else if (existingLgsProductMaster.IsExpiry)
                {
                    dtpExpiry.Enabled = true;
                    dtpExpiry.Focus();
                }
                else
                {
                    txtQty.Enabled = true;
                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                    {
                        txtQty.Text = "1";
                        txtQty.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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

        public void SetBatchNumber(string batchNo)
        {
            batchNumber = batchNo;
        }

        private void txtBatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    batchNumber = null; 

                    if (!txtProductName.Text.Trim().Equals(string.Empty))
                    {
                        LgsProductBatchNoTemp lgsProductBatchNoTemp = new LgsProductBatchNoTemp();
                        LocationService locationService = new LocationService();
                        //invProductBatchNoTemp.DocumentID = documentID;
                        lgsProductBatchNoTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                        lgsProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        if (txtBatchNo.Text.Trim() != "")
                        {
                            LgsProductBatchNoExpiaryDetail lgsProductBatchNo = new LgsProductBatchNoExpiaryDetail();

                            CommonService commonService = new CommonService();
                            //LocationService locationService = new LocationService();
                            lgsProductBatchNo = commonService.CheckLgsBatchNumber(txtBatchNo.Text.Trim(), existingLgsProductMaster.LgsProductMasterID, locationService.GetLocationsByName(cmbFromLocation.Text).LocationID, existingLgsProductMaster.UnitOfMeasureID);

                            if (lgsProductBatchNo == null)
                            {
                                if ((Toast.Show("This Batch No Not exists!", Toast.messageType.Error, Toast.messageAction.General).Equals(DialogResult.OK)))
                                    return;
                            }
                            else
                            {
                                txtCostPrice.Text = lgsProductBatchNo.CostPrice.ToString();
                                txtSellingPrice.Text = lgsProductBatchNo.SellingPrice.ToString();

                                if (existingLgsProductMaster.IsExpiry)
                                {
                                    dtpExpiry.Enabled = true;
                                    dtpExpiry.Value = Common.ConvertStringToDateTime(lgsProductBatchNo.ExpiryDate.ToString());
                                    dtpExpiry.Focus();
                                
                                }
                                else
                                {
                                    txtQty.Enabled = true;
                                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                                        txtQty.Text = "1";
                                    txtQty.Focus();
                                    dtpExpiry.Enabled = true;
                                }
                                return;
                            }
                        }
                        else
                        {
                            LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();
                            lgsProductBatchNoTempList = lgsTransferOfGoodsService.getBatchNoDetail(existingLgsProductMaster, locationService.GetLocationsByName(cmbFromLocation.Text).LocationID);

                            if (lgsProductSerialNoTempList == null)
                                lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                            FrmBatchNumber frmBatchNumber = new FrmBatchNumber(lgsProductBatchNoTempList, lgsProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), isInvProduct, FrmBatchNumber.transactionType.LogisticTransferOfGoods, existingLgsProductMaster.LgsProductMasterID);
                            frmBatchNumber.ShowDialog();
                            LgsProductBatchNoExpiaryDetail lgsProductBatchNo = new LgsProductBatchNoExpiaryDetail();
                            txtBatchNo.Text = batchNumber;

                            CommonService commonService = new CommonService();
                        
                            lgsProductBatchNo = commonService.CheckLgsBatchNumber(txtBatchNo.Text.Trim(), existingLgsProductMaster.LgsProductMasterID, locationService.GetLocationsByName(cmbFromLocation.Text).LocationID, existingLgsProductMaster.UnitOfMeasureID);

                            if (lgsProductBatchNo == null)
                            {
                                if ((Toast.Show("This Batch No Not exists!", Toast.messageType.Error, Toast.messageAction.General).Equals(DialogResult.OK)))
                                    return;
                            }
                            else
                            {
                                txtCostPrice.Text = lgsProductBatchNo.CostPrice.ToString();
                                txtSellingPrice.Text = lgsProductBatchNo.SellingPrice.ToString();

                                if (existingLgsProductMaster.IsExpiry)
                                {
                                    dtpExpiry.Enabled = true;
                                    dtpExpiry.Value = Common.ConvertStringToDateTime(lgsProductBatchNo.ExpiryDate.ToString());
                                    dtpExpiry.Focus();

                                }
                                else
                                {
                                    txtQty.Enabled = true;
                                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                                        txtQty.Text = "1";
                                    txtQty.Focus();

                                }
                                return;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void SetExpiryDate(DateTime expiary)
        {
            expiryDate = expiary;
        }

        private void dtpExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtProductName.Text.Trim().Equals(string.Empty))
                    {
                        LgsProductBatchNoTemp lgsProductBatchNoTemp = new LgsProductBatchNoTemp();
                        //invProductBatchNoTemp.DocumentID = documentID;
                        lgsProductBatchNoTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                        lgsProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();
                        lgsProductBatchNoTempList = lgsTransferOfGoodsService.getExpiryDetail(existingLgsProductMaster, txtBatchNo.Text.Trim());

                        if (lgsProductSerialNoTempList == null)
                            lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                        if (lgsProductBatchNoTempList.Count > 1)
                        {
                            //FrmExpiary frmExpiry = new FrmExpiary(invProductBatchNoTempList, invProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), isInvProduct, FrmExpiary.transactionType.TransferOfGoods);
                            //frmExpiry.ShowDialog();

                            //dtpExpiry.Value = Common.FormatDate(expiryDate);
                        }

                        txtQty.Enabled = true;
                        if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                            txtQty.Text = "1";
                        txtQty.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void FrmLogisticTransferOfGoodsNote_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private string GetDocumentNo(bool isTemporytNo, bool isInv)
        {
            try
            {
                LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();
                LocationService locationService = new LocationService();
                return lgsTransferOfGoodsService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo, isInv).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }
        }

        private void RefreshDocumentNumbers()
        {
            ////Load PO Document Numbers
            LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();
            Common.SetAutoComplete(txtDocumentNo, lgsTransferOfGoodsService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbFromLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
        }

        public override void Pause()
        {
            if ((Toast.Show("TOG  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                this.Cursor = Cursors.WaitCursor;
                string NewDocumentNo;
                bool saveDocument = SaveDocument(0, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                this.Cursor = Cursors.Default;
                if (saveDocument.Equals(true))
                {
                    Toast.Show("TOG  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 0);
                    ClearForm();
                    RefreshDocumentNumbers();
                }
                else
                {
                    Toast.Show("TOG  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        public override void ClearForm()
        {
            dgvItemDetails.DataSource = null;
            dgvItemDetails.Refresh();
            base.ClearForm();
        }

        public override void Save()
        {
            if (ValidateTextBoxes() == false) return;
            if (ValidateComboBoxes() == false) return;

            ShowMinusStockProducts();

            if (!CheckBatchNumbers()) 
            {
                Toast.Show("Batch numbers cant be empty", Toast.messageType.Information, Toast.messageAction.General);
                Common.EnableTextBox(false, txtProductName, txtProductCode);
                dgvItemDetails.Focus();
                return; 
            }

            if (isMinusStock) { return; }

            if ((Toast.Show("TOG  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                this.Cursor = Cursors.WaitCursor;
                string NewDocumentNo;
                bool saveDocument = SaveDocument (1, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                this.Cursor = Cursors.Default;
                if (saveDocument.Equals(true))
                {
                    Toast.Show("TOG  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 1);
                    ClearForm();
                    RefreshDocumentNumbers();
                }
                else
                {
                    Toast.Show("TOG  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        private void ShowMinusStockProducts()
        {
            LocationService locationService = new LocationService();
            LgsMinusProductDetailsTemp lgsMinusProductDetailsTemp = new LgsMinusProductDetailsTemp();

            LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();

            if (lgsTransferDetailsTempList == null)
                lgsTransferDetailsTempList = new List<LgsTransferDetailsTemp>();

            lgsMinusProductDetailsTempList = lgsTransferOfGoodsService.getMinusProductDetails(lgsTransferDetailsTempList, locationService.GetLocationsByName(cmbFromLocation.Text).LocationID);

            if (lgsMinusProductDetailsTempList.Count > 0)
            {
                FrmMinusProducts frmMinusProducts = new FrmMinusProducts(lgsMinusProductDetailsTempList, lgsMinusProductDetailsTemp, Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), isInvProduct, FrmMinusProducts.transactionType.LogisticTransferOfGoods);
                frmMinusProducts.ShowDialog();
                isMinusStock = true;
                return;
            }
            else
            {
                isMinusStock = false;
            }
        }

        private void ShowDocumentNo()
        {
            txtDocumentNo.Text = GetDocumentNo(true);
        }

        private bool SaveDocument(int documentStatus, string documentNo, out string newDocumentNo)
        {
            try
            {
                LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();
                LgsTransferNoteHeader lgsTransferNoteHeader = new LgsTransferNoteHeader();
                LocationService locationService = new LocationService();
                Location FromLocation = new Location();
                Location ToLocation = new Location();

                FromLocation = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));
                ToLocation = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbToLocation.SelectedValue.ToString()));

                lgsTransferNoteHeader = lgsTransferOfGoodsService.GetPausedTogHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbFromLocation.SelectedValue));
                if (lgsTransferNoteHeader == null)
                    lgsTransferNoteHeader = new LgsTransferNoteHeader();

                //if (documentStatus.Equals(1)) // update paused document
                //{
                //    documentNo = GetDocumentNo(false, isInvProduct);
                //    txtDocumentNo.Text = documentNo;
                //}

                
                lgsTransferNoteHeader.CompanyID = FromLocation.CompanyID;
                lgsTransferNoteHeader.DocumentDate = Common.FormatDate(dtpTogDate.Value);

                LgsTransferTypeService lgsTransferTypeService = new LgsTransferTypeService();
                lgsTransferNoteHeader.TransferTypeID = lgsTransferTypeService.GetTransferTypesByName(cmbTransferType.Text.Trim()).LgsTransferTypeID;

                lgsTransferNoteHeader.DocumentID = documentID;
                lgsTransferNoteHeader.DocumentStatus = documentStatus;
                lgsTransferNoteHeader.DocumentNo = documentNo.Trim();
                lgsTransferNoteHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                lgsTransferNoteHeader.LocationID = FromLocation.LocationID;
                lgsTransferNoteHeader.ToLocationID = ToLocation.LocationID;
                lgsTransferNoteHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtTotalAmount.Text.ToString());
                lgsTransferNoteHeader.GrossAmount = Common.ConvertStringToDecimalCurrency(txtTotalAmount.Text.ToString());
                lgsTransferNoteHeader.ReferenceNo = txtReference.Text.Trim();
                lgsTransferNoteHeader.Remark = txtRemark.Text.Trim();


                if (!txtGrnNo.Text.Trim().Equals(string.Empty))
                {
                    LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                    LgsPurchaseHeader lgsPurchaseHeader = new LgsPurchaseHeader();

                    lgsPurchaseHeader = lgsPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());
                    lgsTransferNoteHeader.ReferenceDocumentDocumentID = lgsPurchaseHeader.DocumentID;
                    lgsTransferNoteHeader.ReferenceDocumentID = lgsPurchaseHeader.LgsPurchaseHeaderID;
                }
                else if (!txtManNo.Text.Trim().Equals(string.Empty))
                {                    
                    LgsMaterialAllocationHeader lgsMaterialAllocationHeader = new LgsMaterialAllocationHeader();
                    LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();

                    lgsMaterialAllocationHeader = lgsMaterialAllocationService.GetSavedDocumentDetailsByDocumentNumber(txtManNo.Text.Trim());
                    lgsTransferNoteHeader.ReferenceDocumentDocumentID = lgsMaterialAllocationHeader.DocumentID;
                    lgsTransferNoteHeader.ReferenceDocumentID = lgsMaterialAllocationHeader.LgsMaterialAllocationHeaderID;
                }
                else
                {
                    lgsTransferNoteHeader.ReferenceDocumentDocumentID = 0;
                    lgsTransferNoteHeader.ReferenceDocumentID = 0;
                }

                if (lgsTransferDetailsTempList == null)
                    lgsTransferDetailsTempList = new List<LgsTransferDetailsTemp>();
                

                if (lgsProductSerialNoTempList == null)
                    lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                LgsTransferOfGoodsService.isRecallMAN = recallMAN;
                return lgsTransferOfGoodsService.Save(lgsTransferNoteHeader, lgsTransferDetailsTempList, lgsProductSerialNoTempList, out newDocumentNo, this.Name);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                newDocumentNo = string.Empty;
                this.Cursor = Cursors.Default;

                return false;
            }

        }

        private bool RecallDocument(string documentNo)
        {
            try
            {
                LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();
                LgsTransferNoteHeader lgsTransferNoteHeader = new LgsTransferNoteHeader();

                lgsTransferNoteHeader = lgsTransferOfGoodsService.GetPausedTogHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbFromLocation.SelectedValue));
                if (lgsTransferNoteHeader != null)
                {
                    cmbFromLocation.SelectedValue = lgsTransferNoteHeader.LocationID;
                    cmbFromLocation.Refresh();

                    cmbToLocation.SelectedValue = lgsTransferNoteHeader.ToLocationID;
                    cmbToLocation.Refresh();

                    documentState = lgsTransferNoteHeader.DocumentStatus;

                    dtpTogDate.Value = Common.FormatDate(lgsTransferNoteHeader.DocumentDate);

                    txtDocumentNo.Text = lgsTransferNoteHeader.DocumentNo;
                    txtTotalAmount.Text = Common.ConvertDecimalToStringCurrency(lgsTransferNoteHeader.NetAmount);
                    
                    txtReference.Text = lgsTransferNoteHeader.ReferenceNo;
                    txtRemark.Text = lgsTransferNoteHeader.Remark;

                    //txtGrnNo.Text = invTransferNoteHeader.

                    LgsTransferTypeService lgsTransferTypeService = new LgsTransferTypeService();
                    cmbTransferType.Text = lgsTransferTypeService.GetTransferTypesByID(lgsTransferNoteHeader.TransferTypeID).TransferType;

                    dgvItemDetails.AutoGenerateColumns = false;

                    dgvItemDetails.DataSource = null;
                    lgsTransferDetailsTempList = lgsTransferOfGoodsService.getPausedTOGDetail(lgsTransferNoteHeader);
                    dgvItemDetails.DataSource = lgsTransferDetailsTempList;
                    dgvItemDetails.Refresh();


                    LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                    //invProductSerialNoTempList = invPurchaseService.getSerialNoDetailForTOG(invTransferNoteHeader);
                    

                    Common.EnableTextBox(false, txtDocumentNo, txtReference, txtRemark);
                    Common.EnableComboBox(false, cmbFromLocation, cmbToLocation, cmbTransferType);

                    if (lgsTransferNoteHeader.DocumentStatus.Equals(0))
                    {
                        grpFooter.Enabled = true;
                        EnableLine(false);
                        txtProductCode.Enabled = true;
                        txtProductName.Enabled = true;
                        if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    }
                    else
                    {
                        //grpFooter.Enabled = false;
                        //EnableLine(false);
                        //txtProductCode.Enabled = false;
                        //txtProductName.Enabled = false;
                        //Common.EnableButton(false, btnSave, btnPause);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnPause_Click(object sender, EventArgs e)
        {

        }

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtDocumentNo_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDocumentNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                    RecallDocument(txtDocumentNo.Text.Trim());
                else
                {
                    txtDocumentNo.Text = GetDocumentNo(true);
                    cmbToLocation.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationDocumentNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();
                Common.SetAutoComplete(txtDocumentNo, lgsTransferOfGoodsService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbFromLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
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
                        loadProductDetails(true, dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim(), unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID, Common.ConvertStringToDate((dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value == null ? DateTime.Now.ToString() : dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value.ToString())));
                        CalculateLine();


                        if (documentState.Equals(1))
                        {
                            EnableLine(false);
                            txtProductCode.Enabled = false;
                            txtProductName.Enabled = false;
                        }
                        else
                        {
                            if (dgvItemDetails["BatchNo", dgvItemDetails.CurrentCell.RowIndex].Value == null || dgvItemDetails["BatchNo", dgvItemDetails.CurrentCell.RowIndex].Value == string.Empty)
                            {
                                txtBatchNo.Enabled = true;
                                this.ActiveControl = txtBatchNo;
                                txtBatchNo.Focus();
                            }
                            else
                            {
                                txtQty.Enabled = true;
                                this.ActiveControl = txtQty;
                                txtQty.Focus();
                            }
                        }
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
                    int currentRow = dgvItemDetails.CurrentCell.RowIndex;
                    if (dgvItemDetails.CurrentCell != null && dgvItemDetails.CurrentCell.RowIndex >= 0)
                    {
                        if (Toast.Show("Product " + dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + " - " + dgvItemDetails["ProductName", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                            return;

                        LgsTransferDetailsTemp lgsTransferDetailsTemp = new LgsTransferDetailsTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                        lgsTransferDetailsTemp.ProductID = lgsProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).LgsProductMasterID;
                        existingLgsProductMaster = lgsProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim());
                        if (existingLgsProductMaster.IsExpiry.Equals(true))
                            lgsTransferDetailsTemp.ExpiryDate = Common.ConvertStringToDate(dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value.ToString());
                        lgsTransferDetailsTemp.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                        LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();

                        dgvItemDetails.DataSource = null;
                        lgsTransferDetailsTempList = lgsTransferOfGoodsService.GetDeleteLgsTransferDetailsTemp(lgsTransferDetailsTempList, lgsTransferDetailsTemp, lgsProductSerialNoTempList);
                        dgvItemDetails.DataSource = lgsTransferDetailsTempList;
                        dgvItemDetails.Refresh();

                        if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                        else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }

                        GetSummarizeFigures(lgsTransferDetailsTempList);
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

        private void txtGrnNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtGrnNo.Text.Trim().Equals(string.Empty))
                    {
                        if (RecallGRN(txtGrnNo.Text.Trim())) { ChkBarCodeScan.Enabled = false; }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool RecallGRN(string documentNo)
        {
            try
            {
                recallGRN = true;
                this.Cursor = Cursors.WaitCursor;
                LgsPurchaseHeader lgsPurchaseHeader = new LgsPurchaseHeader();
                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();
                

                lgsPurchaseHeader = lgsPurchaseService.GetLgsPurchaseHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticGoodsReceivedNote").DocumentID, txtGrnNo.Text.Trim(), Common.LoggedLocationID);
                if (lgsPurchaseHeader != null)
                {
                    //EnableControl(true);
                    LgsSupplierService supplierService = new LgsSupplierService();
                    LgsSupplier supplier = new LgsSupplier();
                    supplier = supplierService.GetLgsSupplierByID(lgsPurchaseHeader.SupplierID);

                    //dtpDocumentDate.Value = Common.FormatDate(DateTime.Now);
                    txtDocumentNo.Text = GetDocumentNo(true);
                    txtGrnNo.Text = lgsPurchaseHeader.DocumentNo;
                    txtRemark.Text = lgsPurchaseHeader.Remark;
                    
                    dgvItemDetails.DataSource = null;

                    int docID;

                    docID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticGoodsReceivedNote").DocumentID;
                    lgsTransferDetailsTempList = lgsTransferOfGoodsService.GetGRNDetails(lgsPurchaseHeader, docID);
                    
                    
                    dgvItemDetails.AutoGenerateColumns = false;
                    dgvItemDetails.DataSource = lgsTransferDetailsTempList;
                    dgvItemDetails.Refresh();

                    GetSummarizeFigures(lgsTransferDetailsTempList);

                    Common.EnableTextBox(false, txtDocumentNo, txtGrnNo, txtManNo, txtProductCode, txtProductName);
                    Common.EnableComboBox(false, cmbFromLocation);

                    grpFooter.Enabled = true;
                    grpBody.Enabled = true;
                    EnableLine(false);

                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    Common.EnableButton(false, btnDocumentDetails, btnGrnDetails, btnManDetails);
                    this.ActiveControl = txtProductCode;
                    txtProductCode.Focus();
                    dtpExpiry.Value = DateTime.Now;

                    this.Cursor = Cursors.Default;
                    return true;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                this.Cursor = Cursors.Default;
                return false;
            }
        }

        private void chkAutoCompleationGrnNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //Load GRN Document Numbers -- GRN Document ID- 24 (From AutogenerateInfo Table)
                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                Common.SetAutoComplete(txtGrnNo, lgsPurchaseService.GetAllDocumentNumbers(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticGoodsReceivedNote").DocumentID, Convert.ToInt32(cmbFromLocation.SelectedValue)), chkAutoCompleationGrnNo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void GenerateReport(string documentNo, int documentStatus)
        {
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            lgsReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
        }

        private void cmbTransferType_Validated(object sender, EventArgs e)
        {
            try
            {
                LocationService locationService = new LocationService();

                if (cmbTransferType.SelectedValue == null)
                {
                    Toast.Show("Transfer Type - " + cmbTransferType.Text.Trim().Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    cmbTransferType.Focus();
                    return;
                }
                else
                {

                    if (cmbFromLocation.SelectedValue == null || locationService.IsExistsLocation(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString().Trim())).Equals(false))
                    {
                        Toast.Show("From Location - " + cmbFromLocation.Text.Trim().Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                        cmbFromLocation.Focus();
                        return;
                    }
                    else
                    {

                        if (cmbToLocation.SelectedValue == null || locationService.IsExistsLocation(Common.ConvertStringToInt(cmbToLocation.SelectedValue.ToString().Trim())).Equals(false))
                        {
                            Toast.Show("To Location - " + cmbToLocation.Text.Trim().Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                            cmbToLocation.Focus();
                            return;
                        }
                        else
                        {
                            dgvItemDetails.Enabled = true;
                            Common.EnableTextBox(true, txtProductCode, txtProductName);
                            LoadProducts();
                            txtProductCode.Focus();
                        }
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
                if (string.IsNullOrEmpty(txtProductName.Text.Trim())) { return; }
                loadProductDetails(false, txtProductName.Text.Trim(), 0, dtpExpiry.Value); 
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        

        private void DisplayStockDetails()
        {
            if (chkViewStokDetails.Checked == false) { dgvStockDetails.DataSource = null; }
        }

        private void ChkBarCodeScan_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkBarCodeScan.Checked)
                {
                    chkAutoCompleationProduct.Checked = false;
                    chkAutoCompleationProduct.Enabled = false;

                    chkOverwrite.Checked = false;
                    chkOverwrite.Enabled = false;

                    txtProductName.Enabled = false;
                    txtProductCode.Enabled = true;
                    txtProductCode.Focus();
                }
                else
                {
                    chkAutoCompleationProduct.Checked = true;
                    chkAutoCompleationProduct.Enabled = true;

                    chkOverwrite.Checked = true;
                    chkOverwrite.Enabled = true;

                    txtProductName.Enabled = true;
                    txtProductCode.Enabled = true;
                    txtProductCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkViewStokDetails_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DisplayStockDetails();
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

            PrinterSettings settings = new PrinterSettings();
            foreach (PaperSize size in settings.PaperSizes)
            {
                cmbPaperSize.Items.Add(size.PaperName);
            }

            rdoPortrait.Checked = true;
        }

        private void chkAutoCompleationManNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoCompleationManNo.Checked)
                {
                    LoadbalancedAllocationDocuments();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadbalancedAllocationDocuments()
        {
            LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();
            Common.SetAutoComplete(txtManNo, lgsMaterialAllocationService.GetPendingMaterialAllocationList(), chkAutoCompleationManNo.Checked);
        }

        private void txtManNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtReference);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtManNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtManNo.Text.Trim()))
                {
                    RecallmaterialAllocationNote(txtManNo.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void RecallmaterialAllocationNote(string documentNo)
        {
            this.Cursor = Cursors.WaitCursor;
            LgsMaterialAllocationHeader lgsMaterialAllocationHeader = new LgsMaterialAllocationHeader();
            LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();
            LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();

            lgsMaterialAllocationHeader = lgsMaterialAllocationService.GetLgsMaterialAllocationHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmMaterialAllocationNote").DocumentID, txtManNo.Text.Trim(), Common.LoggedLocationID);
            if (lgsMaterialAllocationHeader != null)
            {
                recallMAN = true;
                //EnableControl(true);
            
                //dtpDocumentDate.Value = Common.FormatDate(DateTime.Now);
                txtDocumentNo.Text = GetDocumentNo(true);
                
                txtRemark.Text = lgsMaterialAllocationHeader.Remark;
                txtReference.Text = lgsMaterialAllocationHeader.ReferenceDocumentNo;

                dgvItemDetails.DataSource = null;

                int docID;

                docID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmMaterialAllocationNote").DocumentID;
                lgsTransferDetailsTempList = lgsTransferOfGoodsService.GetMANDetails(lgsMaterialAllocationHeader, docID);

                int toLocationID = lgsTransferDetailsTempList.Select(td => td.RequestLocationID).First();

                dgvItemDetails.AutoGenerateColumns = false;
                dgvItemDetails.DataSource = lgsTransferDetailsTempList;
                dgvItemDetails.Refresh();

                cmbToLocation.SelectedValue = toLocationID;
                Common.EnableComboBox(false, cmbToLocation);

                GetSummarizeFigures(lgsTransferDetailsTempList);

                Common.EnableTextBox(false, txtDocumentNo, txtGrnNo, txtManNo, txtProductCode, txtProductName);
                Common.EnableComboBox(false, cmbFromLocation);

                grpFooter.Enabled = true;
                grpBody.Enabled = true;
                EnableLine(false);

                if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                Common.EnableButton(false, btnDocumentDetails, btnGrnDetails);

                LoadProductsAccordingToMAN(lgsMaterialAllocationHeader);
                
                this.ActiveControl = txtProductCode;
                txtProductCode.Focus();

                dtpExpiry.Value = DateTime.Now;

                this.Cursor = Cursors.Default;
            }
        }



        private bool CheckBatchNumbers()
        {
            bool status = true;
            var tempTogList = lgsTransferDetailsTempList.ToArray();

            foreach (var temp in tempTogList)
            {
                if (string.IsNullOrEmpty(temp.BatchNo)) 
                { 
                    status = false; 
                    break;
                }
            }
            return status;
        }

        private void btnManDetails_Click(object sender, EventArgs e)
        {
            try
            {
                LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();
                DataView dvAllReferenceData = new DataView(lgsMaterialAllocationService.GetPendingMaterialAllocationDocuments());
                if (dvAllReferenceData.Count != 0)
                {
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), "Pending Allocation Documents", string.Empty, txtManNo);
                    txtManNo_Validated(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadReferenceSearchForm(DataView dvAllReferenceData, string parentOfSearch, string searchFormCaption, string searchText, Control focusControl)
        {
            try
            {
                FrmReferenceSearch referenceSearch = new FrmReferenceSearch();
                referenceSearch.ParentOfSearch = parentOfSearch.Trim();
                referenceSearch.FormCaption = searchFormCaption.Trim();
                referenceSearch.SearchText = searchText.Trim();
                referenceSearch.DvResults = dvAllReferenceData;
                referenceSearch.FocusControl = focusControl;

                if (referenceSearch.IsDisposed)
                {
                    referenceSearch = new FrmReferenceSearch();
                }

                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is FrmReferenceSearch)
                    {
                        FrmReferenceSearch masterSearch2 = (FrmReferenceSearch)frm;
                        if (string.Equals(masterSearch2.ParentOfSearch.Trim(), this.Name.Trim()))
                        {
                            return;
                        }
                    }
                }

                referenceSearch.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvBatchStock_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvBatchStock.CurrentCell != null && dgvBatchStock.CurrentCell.RowIndex >= 0)
                {
                    if (dgvBatchStock["Batch", dgvBatchStock.CurrentCell.RowIndex].Value == null)
                    {
                        Toast.Show("No data available to display", Toast.messageType.Information, Toast.messageAction.General, "");
                        return;
                    }
                    else
                    {
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        txtBatchNo.Text = dgvBatchStock["Batch", dgvBatchStock.CurrentCell.RowIndex].Value.ToString().Trim();
                        existingLgsTransferDetailsTemp.LgsProductBatchNoExpiaryDetailID = Common.ConvertStringToLong(dgvBatchStock["LgsProductBatchNoExpiaryDetailID", dgvBatchStock.CurrentCell.RowIndex].Value.ToString());
                        txtQty.Enabled = true;
                        txtQty.Focus();
                        txtQty.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
