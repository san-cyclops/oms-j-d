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
using Report.Inventory;

namespace UI.Windows
{
    public partial class FrmTransferOfGoodsNote : UI.Windows.FrmBaseTransactionForm
    {
        /// <summary>
        /// Transfer Of Goods Note (TOG)
        /// Design By - C.S.Malluwawadu
        /// Developed By - C.S.Malluwawadu
        /// Date - 15/08/2013
        /// </summary>
        /// 

        public Thread thread = null;
        public Thread threadformload = null;

        int documentState;
        int documentID = 6;
        bool isInvProduct;
        static string batchNumber;
        static DateTime expiryDate;
        bool recallGRN;
        bool recallReturnDocument;
        bool isValidControls = true;
        decimal convertFactor = 1;
        bool isMinusStock = false;
        bool isBackDated;
        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;

        UserPrivileges accessRights = new UserPrivileges();
        UserPrivileges showCostPrice = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        InvProductMaster existingInvProductMaster;

        InvTransferDetailsTemp existingInvTransferDetailTemp = new InvTransferDetailsTemp();
        List<InvTransferDetailsTemp> invTransferDetailsTempList = new List<InvTransferDetailsTemp>();
        InvTransferDetailsTemp existingInvTransferDetailsTemp = new InvTransferDetailsTemp();
        List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();
        List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();
        List<InvPurchaseDetailTemp> invPurchaseDetailTempList = new List<InvPurchaseDetailTemp>();
        List<InvMinusProductDetailsTemp> invMinusProductDetailsTempList = new List<InvMinusProductDetailsTemp>();

        public FrmTransferOfGoodsNote()
        {
            InitializeComponent();
        }

        private void LoadAllScanners()
        {
            InvScannerService invScannerService = new InvScannerService();
            List<InvScanner> scanners = new List<InvScanner>();

            scanners = invScannerService.GetAllScanners();
            dgvScanner.DataSource = scanners;
            dgvScanner.Refresh();

        }

        public override void FormLoad()
        {
            try
            {
 
                // Hide Cost Price
                lblHideCostPrice.Visible = true;


                // Load Unit of measures
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                Common.LoadUnitOfMeasures(cmbUnit, unitOfMeasureService.GetAllUnitOfMeasures());

                // Load Locations
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbFromLocation, locationService.GetAllLocationsInventory());

                // Load TransferTypes
                InvTransferTypeService invTransferTypeService = new InvTransferTypeService();
                Common.LoadTransferTypes(cmbTransferType, invTransferTypeService.GetAllTransferTypes());
                cmbTransferType.SelectedIndex = 0;

                // Load Scanners
                dgvScanner.AutoGenerateColumns = false;
                LoadAllScanners();

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                chkOverwrite.Checked = autoGenerateInfo.IsOverWriteQty;
                documentID = autoGenerateInfo.DocumentID;
                isBackDated = autoGenerateInfo.IsBackDated;
                chkAutoCompleationProduct.Checked = true;

                tbFooter.TabPages.Remove(tabBatchStock);
                tbFooter.TabPages.Remove(tbpRemote);

                if (isBackDated)
                {
                    dtpTogDate.Enabled = true;
                }
                else
                {
                    dtpTogDate.Enabled = false;
                }

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                showCostPrice = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, 19002);

                //if (showCostPrice.IsAccess == true) 
                //{ 
                    lblHideCostPrice.Visible = false;
                    dgvItemDetails.Columns[6].DataPropertyName = "CostPrice";
                //} 
                //else 
                //{ 
                //    lblHideCostPrice.Visible = true;
                //    dgvItemDetails.Columns[6].DataPropertyName = "";
                //}

                isInvProduct = true;

                base.FormLoad();
                this.Visible = true;
                Application.DoEvents();

                ////Load Document Numbers
                InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                Common.SetAutoComplete(txtDocumentNo, invTransferOfGoodsService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbFromLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

                //Load GRN Document Numbers -- GRN Document ID- 24 (From AutogenerateInfo Table)
                
                //InvPurchaseService invPurchaseService = new InvPurchaseService();
                //Common.SetAutoComplete(txtGrnNo, invPurchaseService.GetAllDocumentNumbersAsReference(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote").DocumentID, Convert.ToInt32(cmbFromLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);


                //this.Cursor = Cursors.WaitCursor;
                //LoadProducts();
                //this.Cursor = Cursors.Default;

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
                dgvBatchStock.DataSource = null;
                grpFooter.Enabled = false;

                EnableLine(false);
                Common.EnableTextBox(true, txtDocumentNo, txtGrnNo, txtRemark, txtReference);
                Common.EnableTextBox(false, txtProductCode, txtProductName);
                Common.EnableButton(true, btnDocumentDetails, btnGrnDetails);
                Common.EnableComboBox(true, cmbFromLocation, cmbToLocation, cmbTransferType);
                Common.EnableButton(false, btnSave, btnPause, btnView);

                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                dtpExpiry.Value = DateTime.Now;
                dtpTogDate.Value = DateTime.Now;

                ChkBarCodeScan.Enabled = true;

                cmbFromLocation.SelectedValue = Common.LoggedLocationID;
                cmbToLocation.SelectedValue = -1;
                cmbReturnDocument.DataSource = null;
                Common.ClearComboBox(cmbReturnDocument);
                cmbReturnDocument.Enabled = false;
                btnReturnDetails.Enabled = false;
                lblPOSDocuments.Enabled = false;
                
                cmbReturnDocument.Refresh();
                //cmbTransferType.SelectedValue = -1;
                cmbTransferType.SelectedIndex = 0;

                //txtDocumentNo.Text = GetDocumentNo(true);
                this.ActiveControl = cmbFromLocation;

                existingInvProductMaster = null;
                existingInvTransferDetailsTemp = null;
                existingInvTransferDetailTemp = null;
                batchNumber = null;

                invProductBatchNoTempList = null;
                invProductSerialNoTempList = null;
                invTransferDetailsTempList = null;

                recallGRN = false;
                recallReturnDocument = false;

                selectedRowIndex = 0;
                isUpdateGrid = false;
                rowCount = 0;

                ShowDocumentNo();

                cmbFromLocation.Focus();

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
                InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                LocationService locationService = new LocationService();
                return invTransferOfGoodsService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
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
            Common.LoadLocations(cmbToLocation, locationService.GetLocationsExceptingCurrentLocationInventory(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString())));
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
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (txtProductCode.Text.Trim().Equals(string.Empty))
                    {
                        return;
                    }
                    else
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
                int indexOf = 0;
                string fillter = "";
                InvProductMaster invProductMasterPrm = new InvProductMaster();
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                CommonService commonService = new CommonService();
                              
                if (string.IsNullOrEmpty(txtProductCode.Text.Trim())) { return; }

                if (ChkBarCodeScan.Checked)
                {
                    indexOf = txtProductCode.Text.Trim().IndexOf("*");

                    if (indexOf > 0)
                    {
                        fillter = txtProductCode.Text.Trim().Substring(indexOf + 1, txtProductCode.Text.Trim().Length - (indexOf + 1));
                        txtQty.Text = txtProductCode.Text.Trim().Substring(0, indexOf);

                        invProductMasterPrm = invProductMasterService.GetProductsByRefCodes(fillter);
                    }
                    else
                    {
                        txtQty.Text = "1";
                        fillter = txtProductCode.Text.Trim();

                        invProductMasterPrm = invProductMasterService.GetProductsByRefCodes(fillter);
                    }

                    if (loadBarcodeDetails(Common.ConvertStringToLong(fillter), invProductMasterPrm))
                    {
                        //if (commonService.ValidateBatchStock(Common.ConvertStringToDecimalQty(txtQty.Text.Trim()), invProductMasterPrm, Convert.ToInt32(cmbFromLocation.SelectedValue), txtBatchNo.Text.Trim(), convertFactor))
                        //{
                            CalculateLine();
                            if (existingInvTransferDetailsTemp != null)
                            {
                                UpdateBatchGrid(existingInvTransferDetailsTemp);
                            }
                        //}
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
                existingInvProductMaster = new InvProductMaster();

                if (strProduct.Equals(string.Empty))
                    return;

                InvProductMasterService InvProductMasterService = new InvProductMasterService();

                if (isCode)
                {

                    existingInvProductMaster = InvProductMasterService.GetProductsByRefCodes(strProduct);
                    if (isCode && strProduct.Equals(string.Empty))
                    {
                        txtProductName.Focus();
                        return;
                    }
                }
                else
                    existingInvProductMaster = InvProductMasterService.GetProductsByName(strProduct); ;

                if (existingInvProductMaster != null)
                {
                    InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                    if (invTransferDetailsTempList == null)
                        invTransferDetailsTempList = new List<InvTransferDetailsTemp>();
                    existingInvTransferDetailsTemp = invTransferOfGoodsService.getTransferDetailTemp(invTransferDetailsTempList, existingInvProductMaster, Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID, txtBatchNo.Text.Trim());
                    if (existingInvTransferDetailsTemp != null)
                    {
                        txtProductCode.Text = existingInvTransferDetailsTemp.ProductCode;
                        txtProductName.Text = existingInvTransferDetailsTemp.ProductName;
                        cmbUnit.SelectedValue = existingInvTransferDetailsTemp.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingInvTransferDetailsTemp.CostPrice);
                        txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingInvTransferDetailsTemp.SellingPrice);
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingInvTransferDetailsTemp.Qty);
                        //txtBatchNo.Text = existingInvTransferDetailsTemp.BatchNo;
                        
                        if (existingInvProductMaster.IsExpiry)
                        {
                            dtpExpiry.Value = Common.ConvertStringToDate((existingInvTransferDetailsTemp.ExpiryDate == null ? dtpExpiry.Value.ToString() : existingInvTransferDetailsTemp.ExpiryDate.ToString()));
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

                        if (chkViewStokDetails.Checked) { ShowStockDetails(existingInvTransferDetailsTemp.ProductID); }                        
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


        private void loadProductDetailsForGridDoubleClick(bool isCode, string strProduct, long unitofMeasureID, DateTime expiryDate, string batchNo)  
        {
            try
            {
                existingInvProductMaster = new InvProductMaster();

                if (strProduct.Equals(string.Empty))
                    return;

                InvProductMasterService InvProductMasterService = new InvProductMasterService();

                if (isCode)
                {

                    existingInvProductMaster = InvProductMasterService.GetProductsByRefCodes(strProduct);
                    if (isCode && strProduct.Equals(string.Empty))
                    {
                        txtProductName.Focus();
                        return;
                    }
                }
                else
                    existingInvProductMaster = InvProductMasterService.GetProductsByName(strProduct); ;

                if (existingInvProductMaster != null)
                {
                    InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                    if (invTransferDetailsTempList == null)
                        invTransferDetailsTempList = new List<InvTransferDetailsTemp>();
                    existingInvTransferDetailsTemp = invTransferOfGoodsService.getTransferDetailTemp(invTransferDetailsTempList, existingInvProductMaster, Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID, batchNo);
                    if (existingInvTransferDetailsTemp != null)
                    {
                        txtProductCode.Text = existingInvTransferDetailsTemp.ProductCode;
                        txtProductName.Text = existingInvTransferDetailsTemp.ProductName;
                        cmbUnit.SelectedValue = existingInvTransferDetailsTemp.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingInvTransferDetailsTemp.CostPrice);
                        txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingInvTransferDetailsTemp.SellingPrice);
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingInvTransferDetailsTemp.Qty);
                        txtBatchNo.Text = existingInvTransferDetailsTemp.BatchNo;

                        if (existingInvProductMaster.IsExpiry)
                        {
                            dtpExpiry.Value = Common.ConvertStringToDate((existingInvTransferDetailsTemp.ExpiryDate == null ? dtpExpiry.Value.ToString() : existingInvTransferDetailsTemp.ExpiryDate.ToString()));
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

                        if (chkViewStokDetails.Checked) { ShowStockDetails(existingInvTransferDetailsTemp.ProductID); }
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

        private bool loadBarcodeDetails(long barcode, InvProductMaster product) 
        {
            try
            {
                existingInvProductMaster = new InvProductMaster();
                InvProductMasterService InvProductMasterService = new InvProductMasterService();

                InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                if (invTransferDetailsTempList == null) { invTransferDetailsTempList = new List<InvTransferDetailsTemp>(); }

                InvBatchNoExpiaryDetailService batchNoExpiaryDetailService = new InvBatchNoExpiaryDetailService();
                InvProductBatchNoExpiaryDetail invProductBatchNoExpiaryDetail = new InvProductBatchNoExpiaryDetail();
                invProductBatchNoExpiaryDetail = batchNoExpiaryDetailService.GetBatchNoExpiaryDetailByBarcode(barcode, product, Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));

                if (invProductBatchNoExpiaryDetail != null)
                {

                    existingInvTransferDetailsTemp = invTransferOfGoodsService.GetTransferDetailTempForBarcodeScan(invProductBatchNoExpiaryDetail.BarCode, Common.ConvertStringToInt(txtQty.Text.Trim()));

                    if (existingInvTransferDetailsTemp != null)
                    {
                        txtProductCode.Text = existingInvTransferDetailsTemp.ProductCode;
                        txtProductName.Text = existingInvTransferDetailsTemp.ProductName;
                        cmbUnit.Text = existingInvTransferDetailsTemp.UnitOfMeasure;
                        txtBatchNo.Text = existingInvTransferDetailsTemp.BatchNo;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingInvTransferDetailsTemp.CostPrice);
                        txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingInvTransferDetailsTemp.SellingPrice);
                        txtQty.Text = existingInvTransferDetailsTemp.Qty.ToString();

                        if (existingInvTransferDetailsTemp.ExpiryDate == null)
                        {
                            dtpExpiry.Value = Common.ConvertDateTimeToDate(DateTime.Now);
                            dtpExpiry.Enabled = false;
                        }
                        else
                        {
                            dtpExpiry.Value = Common.ConvertStringToDate((existingInvTransferDetailsTemp.ExpiryDate.ToString()));
                            dtpExpiry.Enabled = true;
                        }


                        Common.EnableComboBox(true, cmbUnit);

                        if (chkViewStokDetails.Checked) { ShowStockDetails(existingInvTransferDetailsTemp.ProductID); }

                        return true;
                    }
                    else
                    {
                        Toast.Show("Invalid barcode or quantity", Toast.messageType.Information, Toast.messageAction.General);
                        txtProductCode.Focus();
                        txtProductCode.SelectAll();
                        return false;
                    }
                }
                else
                {
                    Toast.Show("Invalid barcode", Toast.messageType.Information, Toast.messageAction.General);
                    txtProductCode.Focus();
                    txtProductCode.SelectAll();
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
            List<InvProductStockMaster> invProductStockMasterList = new List<InvProductStockMaster>();
            invProductStockMasterList = CommonService.GetInvStockDetailsToStockGrid(existingInvProductMaster);
            dgvStockDetails.DataSource = invProductStockMasterList;
            dgvStockDetails.Refresh();

            // Batch wise stock

            dgvBatchStock.DataSource = null;
            dgvBatchStock.AutoGenerateColumns = false;
            List<InvProductBatchNoExpiaryDetail> invProductBatchNoList = new List<InvProductBatchNoExpiaryDetail>();
            invProductBatchNoList = CommonService.GetInvBatchStockDetailsToStockGrid(productID, Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));
            dgvBatchStock.DataSource = invProductBatchNoList;
            dgvBatchStock.Refresh();
        }

         
        private void LoadProducts()
        {
            try
            {
                //InvProductMasterService invProductMasterService = new InvProductMasterService();

                //Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                //Common.SetAutoComplete(txtProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);


                InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();

                Common.SetAutoComplete(txtProductCode, invTransferOfGoodsService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                Common.SetAutoComplete(txtProductName, invTransferOfGoodsService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void chkAutoCompleationProduct_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoCompleationProduct.Checked)
            {
                LoadProducts();
            }
        }

        private void cmbFromLocation_Validated(object sender, EventArgs e)
        {
            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbFromLocation.Text.Trim()).LocationID, documentID);
            if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }

            //LoadProducts();
        }

        public void SetInvSerialNoList(List<InvProductSerialNoTemp> setInvProductSerialNoTemp)
        {
            invProductSerialNoTempList = setInvProductSerialNoTemp;
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

                        if (!existingInvProductMaster.UnitOfMeasureID.Equals(Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())))
                        {
                            InvProductUnitConversionService invProductUnitConversionService = new InvProductUnitConversionService();

                            convertFactor = invProductUnitConversionService.GetProductUnitByProductCode(existingInvProductMaster.InvProductMasterID, Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())).ConvertFactor;

                        }
                        else
                        {
                            convertFactor = 1;
                        }
                        //if (commonService.ValidateBatchStock(Common.ConvertStringToDecimalQty(txtQty.Text.Trim()), existingInvProductMaster, Convert.ToInt32(cmbFromLocation.SelectedValue), txtBatchNo.Text.Trim(), convertFactor))
                        //{
                        if (existingInvProductMaster.IsSerial)
                        {
                            InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();
                            invProductSerialNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                            invProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                            InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                            invProductSerialNoTempList = invTransferOfGoodsService.GetSerialNoDetail(existingInvProductMaster);

                            if (invProductSerialNoTempList == null)
                                invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                            FrmSerialCommon frmSerialCommon = new FrmSerialCommon(invProductSerialNoTempList, invProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), isInvProduct, documentID, FrmSerialCommon.transactionType.TransferOfGoods);
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
                        //}
                        //else
                        //{
                        //    Toast.Show("Transfer", Toast.messageType.Information, Toast.messageAction.QtyExceed);
                        //    txtAmount.Enabled = false;
                        //    this.ActiveControl = txtQty;
                        //    txtQty.Focus();
                        //    txtQty.SelectAll();
                        //    return;
                        //}
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

                    InvProductMaster invProductMaster = new InvProductMaster();
                    InvProductMasterService invProductMasterService = new InvProductMasterService();

                    invProductMaster = invProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());
                    if (invProductMaster != null)
                    {
                        InvPurchaseHeader invPurchaseHeader = new InvPurchaseHeader();
                        InvPurchaseService invPurchaseService = new InvPurchaseService();
                        invPurchaseHeader = invPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());

                        if (!invPurchaseService.IsValidNoOfQty(qty, invProductMaster.InvProductMasterID, Convert.ToInt32(cmbUnit.SelectedValue), invPurchaseHeader.InvPurchaseHeaderID))
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
                else
                {
                    if (Common.ConvertStringToDecimalQty(txtQty.Text) > 0)
                    {
                        CalculateLine();
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
                        UpdateGrid(existingInvTransferDetailsTemp);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void UpdateGrid(InvTransferDetailsTemp invTransferDetailTemp)
        {
            try
            {
                decimal qty = 0;

                if ((Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) > 0))  // && (Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text.Trim()) > 0
                {
                    string batchNo = txtBatchNo.Text.Trim();
                    if (chkOverwrite.Checked)
                    {
                        string productCode = txtProductCode.Text.Trim();
                        string unit = cmbUnit.Text.Trim();

                        if (dgvItemDetails["ProductCode", 0].Value == null) { }
                        else
                        {
                            for (int i = 0; i < dgvItemDetails.RowCount; i++)
                            {
                                if (productCode.Equals(dgvItemDetails["ProductCode", i].Value.ToString()) && unit.Equals(dgvItemDetails["Unit", i].Value.ToString()))  //&& batchNo.Equals(dgvItemDetails["BatchNo", i].Value.ToString())
                                {
                                    if (Toast.Show("", Toast.messageType.Question, Toast.messageAction.OverwriteQty).Equals(DialogResult.Yes)) { }
                                    else { return; }
                                }
                            }
                        }

                    }
                    else
                    {
                        InvTransferDetailsTemp transferDetailTemp = new InvTransferDetailsTemp();
                        transferDetailTemp = invTransferDetailsTempList.Where(t => t.ProductID.Equals(invTransferDetailTemp.ProductID) && t.BatchNo.Equals(batchNo)).FirstOrDefault();
                        if (transferDetailTemp != null)
                        {
                            //qty = (invTransferDetailTemp.Qty + Common.ConvertStringToDecimalQty(txtQty.Text.Trim()));
                            qty = (transferDetailTemp.Qty + Common.ConvertStringToDecimalQty(txtQty.Text.Trim()));
                        }
                        else
                        {
                            qty = (invTransferDetailTemp.Qty + Common.ConvertStringToDecimalQty(txtQty.Text.Trim()));
                        }
                    }

                    if (!chkOverwrite.Checked)
                    {
                        if (!txtGrnNo.Text.Trim().Equals(string.Empty))
                        {
                            if (existingInvProductMaster != null)
                            {
                                InvPurchaseHeader invPurchaseHeader = new InvPurchaseHeader();
                                InvPurchaseService invPurchaseService = new InvPurchaseService();
                                invPurchaseHeader = invPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());

                                if (!invPurchaseService.IsValidNoOfQty(qty, existingInvProductMaster.InvProductMasterID, Convert.ToInt32(cmbUnit.SelectedValue), invPurchaseHeader.InvPurchaseHeaderID))
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
                        invTransferDetailTemp.Qty = Common.ConvertDecimalToDecimalQty(qty);
                    }
                    else
                    {
                        CalculateLine();
                        invTransferDetailTemp.Qty = Common.ConvertStringToDecimalQty(txtQty.Text);
                    }


                    //if (recallGRN) { invTransferDetailTemp.Qty = Common.ConvertStringToDecimalQty(txtQty.Text); }
                    //else { invTransferDetailTemp.Qty = qty; }

                    invTransferDetailTemp.DocumentNo = txtDocumentNo.Text.Trim();
                    invTransferDetailTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                    invTransferDetailTemp.ProductCode = txtProductCode.Text.Trim();
                    invTransferDetailTemp.ProductName = txtProductName.Text.Trim();
                    invTransferDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                    invTransferDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();
                    invTransferDetailTemp.BaseUnitID = existingInvProductMaster.UnitOfMeasureID;                  
                
                                   
                    invTransferDetailTemp.BatchNo = txtBatchNo.Text.Trim();
                    invTransferDetailTemp.ExpiryDate = Common.FormatDate(dtpExpiry.Value);              

                    invTransferDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text);
                    invTransferDetailTemp.SellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text);
                    invTransferDetailTemp.NetAmount = Common.ConvertStringToDecimalCurrency(txtAmount.Text);

                    InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();

                    dgvItemDetails.DataSource = null;
                    invTransferDetailsTempList = invTransferOfGoodsService.getUpdateInvTransferDetailTemp(invTransferDetailsTempList, invTransferDetailTemp, existingInvProductMaster);
                    dgvItemDetails.AutoGenerateColumns = false;
                    dgvItemDetails.DataSource = invTransferDetailsTempList;
                    dgvItemDetails.Refresh();

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

                    GetSummarizeFigures(invTransferDetailsTempList);
                    EnableLine(false);
                    Common.EnableTextBox(false, txtDocumentNo);
                    Common.EnableComboBox(false, cmbFromLocation);
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave); 
                    ClearLine();

                    if (invTransferDetailsTempList.Count > 0)
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

        private void UpdateBatchGrid(InvTransferDetailsTemp invTransferDetailTemp) 
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

                
                invTransferDetailTemp.DocumentNo = txtDocumentNo.Text.Trim();
                invTransferDetailTemp.ProductID = invTransferDetailTemp.ProductID;

                InvProductMaster invProductMaster = new InvProductMaster();
                InvProductMasterService invProductMasterService = new InvProductMasterService();

                invProductMaster = invProductMasterService.GetProductDetailsByID(invTransferDetailTemp.ProductID);

                invTransferDetailTemp.ProductCode = txtProductCode.Text.Trim();
                invTransferDetailTemp.ProductName = txtProductName.Text.Trim();

                if (cmbUnit.SelectedValue != null) { invTransferDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()); }
                else { invTransferDetailTemp.UnitOfMeasureID = 0; }
                
                invTransferDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();
                invTransferDetailTemp.BaseUnitID = invProductMaster.UnitOfMeasureID;
                invTransferDetailTemp.Qty = Common.ConvertStringToDecimalQty(txtQty.Text);

                CalculateLine();

                invTransferDetailTemp.BatchNo = txtBatchNo.Text.Trim();
                invTransferDetailTemp.ExpiryDate = Common.FormatDate(dtpExpiry.Value);
                invTransferDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text);
                invTransferDetailTemp.SellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text);
                invTransferDetailTemp.NetAmount = Common.ConvertStringToDecimalCurrency(txtAmount.Text);

                InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();

                dgvItemDetails.DataSource = null;
                invTransferDetailsTempList = invTransferOfGoodsService.GetUpdateInvTransferDetailTempForBarcodeScan(invTransferDetailsTempList, invTransferDetailTemp);
                dgvItemDetails.AutoGenerateColumns = false;
                dgvItemDetails.DataSource = invTransferDetailsTempList;
                dgvItemDetails.Refresh();

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

                GetSummarizeFigures(invTransferDetailsTempList);
                EnableLine(false);
                Common.EnableTextBox(false, txtDocumentNo);
                Common.EnableComboBox(false, cmbFromLocation, cmbToLocation, cmbTransferType);
                if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                ClearLine();

                if (invTransferDetailsTempList.Count > 0)
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

                    txtAmount.Text = ((Common.ConvertDecimalToDecimalQty(qty)) * (Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text.Trim()))).ToString() ;

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void GetSummarizeFigures(List<InvTransferDetailsTemp> listItem)
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
            if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
            {
                cmbTransferType.Focus();
            }
        }

        private void cmbTransferType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
            {
                cmbTransferType_Validated(this, e);
                ////txtProductCode.Focus(); 
            }
        }

        private void cmbUnit_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbUnit.SelectedValue == null)
                    return;

                if (existingInvProductMaster != null)
                {
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
                else
                {
                    txtQty.Enabled = true;
                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                    {
                        txtQty.Text = "0.00";
                        txtQty.SelectAll();
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
            if (e.KeyCode.Equals(Keys.Enter))
            {
                cmbUnit_Leave(this, e);
            }
        }

        public void SetBatchNumber(string batchNo)
        {
            batchNumber = batchNo;
        }

        private void txtBatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
            {
                batchNumber = null; 

                if (!txtProductName.Text.Trim().Equals(string.Empty))
                {
                    InvProductBatchNoTemp invProductBatchNoTemp = new InvProductBatchNoTemp();
                    LocationService locationService = new LocationService();
                    //invProductBatchNoTemp.DocumentID = documentID;
                    invProductBatchNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                    invProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                    if (txtBatchNo.Text.Trim() != "")
                    {
                        InvProductBatchNoExpiaryDetail invProductBatchNo = new InvProductBatchNoExpiaryDetail();

                        CommonService commonService = new CommonService();
                        //LocationService locationService = new LocationService();
                        invProductBatchNo = commonService.CheckInvBatchNumber(txtBatchNo.Text.Trim(), existingInvProductMaster.InvProductMasterID, locationService.GetLocationsByName(cmbFromLocation.Text).LocationID, existingInvProductMaster.UnitOfMeasureID);

                        if (invProductBatchNo == null)
                        {
                            if ((Toast.Show("This Batch No Not exists!", Toast.messageType.Error, Toast.messageAction.General).Equals(DialogResult.OK)))
                                return;
                        }
                        else
                        {
                            txtCostPrice.Text = invProductBatchNo.CostPrice.ToString();
                            txtSellingPrice.Text = invProductBatchNo.SellingPrice.ToString();

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
                                    txtQty.Text = "1";
                                txtQty.Focus();
                                dtpExpiry.Enabled = true;
                            }
                            return;
                        }
                    }
                    else
                    {
                        
                        InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                        invProductBatchNoTempList = invTransferOfGoodsService.getBatchNoDetail(existingInvProductMaster, locationService.GetLocationsByName(cmbFromLocation.Text).LocationID);


                        if (invProductSerialNoTempList == null)
                            invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                        InvProductMasterService invProductMasterService = new InvProductMasterService();

                        FrmBatchNumber frmBatchNumber = new FrmBatchNumber(invProductBatchNoTempList, invProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), isInvProduct, FrmBatchNumber.transactionType.TransferOfGoods, existingInvProductMaster.InvProductMasterID);
                        frmBatchNumber.ShowDialog();
                        InvProductBatchNoExpiaryDetail invProductBatchNo = new InvProductBatchNoExpiaryDetail();
                        txtBatchNo.Text = batchNumber;
                        

                        CommonService commonService = new CommonService();
                        
                        invProductBatchNo = commonService.CheckInvBatchNumber(txtBatchNo.Text.Trim(), existingInvProductMaster.InvProductMasterID, locationService.GetLocationsByName(cmbFromLocation.Text).LocationID, existingInvProductMaster.UnitOfMeasureID);

                        if (invProductBatchNo == null)
                        {
                            if ((Toast.Show("This Batch No Not exists!", Toast.messageType.Error, Toast.messageAction.General).Equals(DialogResult.OK)))
                                return;
                        }
                        else
                        {
                            txtCostPrice.Text = invProductBatchNo.CostPrice.ToString();
                            txtSellingPrice.Text = invProductBatchNo.SellingPrice.ToString();

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
                                    txtQty.Text = "1";
                                txtQty.Focus();

                            }
                            return;
                        }
                    }

                }
            }
        }

        public void SetExpiryDate(DateTime expiary)
        {
            expiryDate = expiary;
        }

        private void dtpExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
            {
                if (!txtProductName.Text.Trim().Equals(string.Empty))
                {
                    InvProductBatchNoTemp invProductBatchNoTemp = new InvProductBatchNoTemp();
                    //invProductBatchNoTemp.DocumentID = documentID;
                    invProductBatchNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                    invProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                    InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                    invProductBatchNoTempList = invTransferOfGoodsService.getExpiryDetail(existingInvProductMaster, txtBatchNo.Text.Trim());

                    if (invProductSerialNoTempList == null)
                        invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                    if (invProductBatchNoTempList.Count > 1)
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

        private void FrmTransferOfGoodsNote_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private string GetDocumentNo(bool isTemporytNo, bool isInv)
        {
            try
            {
                InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                LocationService locationService = new LocationService();
                return invTransferOfGoodsService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo, isInv).Trim();
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
            InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
            Common.SetAutoComplete(txtDocumentNo, invTransferOfGoodsService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbFromLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
        }

        public override void Pause()
        {

            if (ValidateControls() == false) return;

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
            if (ValidateControls() == false) return;

            if (!recallReturnDocument)
            {
                ShowMinusStockProducts();
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
            InvMinusProductDetailsTemp invMinusProductDetailsTemp = new InvMinusProductDetailsTemp();

            InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();

            if (invTransferDetailsTempList == null)
                invTransferDetailsTempList = new List<InvTransferDetailsTemp>();

            invMinusProductDetailsTempList = invTransferOfGoodsService.getMinusProductDetails(invTransferDetailsTempList, locationService.GetLocationsByName(cmbFromLocation.Text).LocationID);

            if (invMinusProductDetailsTempList.Count > 0)
            {
                FrmMinusProducts frmMinusProducts = new FrmMinusProducts(invMinusProductDetailsTempList, invMinusProductDetailsTemp, Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), isInvProduct, FrmMinusProducts.transactionType.TransferOfGoods);
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
                InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                InvTransferNoteHeader invTransferNoteHeader = new InvTransferNoteHeader();
                LocationService locationService = new LocationService();
                Location FromLocation = new Location();
                Location ToLocation = new Location();

                FromLocation = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));
                ToLocation = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbToLocation.SelectedValue.ToString()));

                invTransferNoteHeader = invTransferOfGoodsService.GetPausedTogHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbFromLocation.SelectedValue));
                if (invTransferNoteHeader == null)
                    invTransferNoteHeader = new InvTransferNoteHeader();

                //if (documentStatus.Equals(1)) // update paused document
                //{
                //    documentNo = GetDocumentNo(false, isInvProduct);
                //    txtDocumentNo.Text = documentNo;
                //}

                
                invTransferNoteHeader.CompanyID = FromLocation.CompanyID;
                invTransferNoteHeader.DocumentDate = Common.FormatDate(dtpTogDate.Value);

                InvTransferTypeService invTransferTypeService = new InvTransferTypeService();
                invTransferNoteHeader.TransferTypeID = invTransferTypeService.GetTransferTypesByName(cmbTransferType.Text.Trim()).InvTransferTypeID;

                invTransferNoteHeader.DocumentID = documentID;
                invTransferNoteHeader.DocumentStatus = documentStatus;
                invTransferNoteHeader.DocumentNo = documentNo.Trim();
                invTransferNoteHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                invTransferNoteHeader.LocationID = FromLocation.LocationID;
                invTransferNoteHeader.ToLocationID = ToLocation.LocationID;
                invTransferNoteHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtTotalAmount.Text.ToString());
                invTransferNoteHeader.GrossAmount = Common.ConvertStringToDecimalCurrency(txtTotalAmount.Text.ToString());
                invTransferNoteHeader.ReferenceNo = txtReference.Text.Trim();

                if (invTransferNoteHeader.TransferTypeID != 1) { invTransferNoteHeader.PosReferenceNo = cmbReturnDocument.Text.Trim(); }
                else { invTransferNoteHeader.PosReferenceNo = ""; }

                if (invTransferNoteHeader.TransferTypeID != 1 && cmbTransferType.Text.Trim() == "BRANCH RETURN") { invTransferNoteHeader.TransStatusId = 4; }
                else if (invTransferNoteHeader.TransferTypeID != 1 && cmbTransferType.Text.Trim() == "DAMAGE RETURN") { invTransferNoteHeader.TransStatusId = 5; }
                else if (invTransferNoteHeader.TransferTypeID != 1 && cmbTransferType.Text.Trim() == "TRANSFER NOTE") { invTransferNoteHeader.TransStatusId = 2; }

                invTransferNoteHeader.Remark = txtRemark.Text.Trim();


                if (!txtGrnNo.Text.Trim().Equals(string.Empty))
                {
                    InvPurchaseService invPurchaseService = new InvPurchaseService();
                    InvPurchaseHeader invPurchaseHeader = new InvPurchaseHeader();

                    invPurchaseHeader = invPurchaseService.GetInvPurchaseHeaderByDocumentNo((AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote").DocumentID), txtGrnNo.Text.Trim(), locationService.GetLocationsByName(cmbFromLocation.Text.Trim()).LocationID);
                   
                    invTransferNoteHeader.ReferenceDocumentDocumentID = invPurchaseHeader.DocumentID;
                    invTransferNoteHeader.ReferenceDocumentID = invPurchaseHeader.InvPurchaseHeaderID;
                }
                else
                {
                    invTransferNoteHeader.ReferenceDocumentDocumentID = 0;
                    invTransferNoteHeader.ReferenceDocumentID = 0;

                }

                if (invTransferDetailsTempList == null)
                    invTransferDetailsTempList = new List<InvTransferDetailsTemp>();
                

                if (invProductSerialNoTempList == null)
                    invProductSerialNoTempList = new List<InvProductSerialNoTemp>();
                

                return invTransferOfGoodsService.Save(invTransferNoteHeader, invTransferDetailsTempList, invProductSerialNoTempList, out newDocumentNo, this.Name);

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
                InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                InvTransferNoteHeader invTransferNoteHeader = new InvTransferNoteHeader();

                invTransferNoteHeader = invTransferOfGoodsService.GetPausedTogHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbFromLocation.SelectedValue));
                if (invTransferNoteHeader != null)
                {
                    cmbFromLocation.SelectedValue = invTransferNoteHeader.LocationID;
                    cmbFromLocation.Refresh();

                    cmbToLocation.SelectedValue = invTransferNoteHeader.ToLocationID;
                    cmbToLocation.Refresh();

                    documentState = invTransferNoteHeader.DocumentStatus;

                    dtpTogDate.Value = Common.FormatDate(invTransferNoteHeader.DocumentDate);

                    txtDocumentNo.Text = invTransferNoteHeader.DocumentNo;
                    txtTotalAmount.Text = Common.ConvertDecimalToStringCurrency(invTransferNoteHeader.NetAmount);
                    
                    txtReference.Text = invTransferNoteHeader.ReferenceNo;
                    txtRemark.Text = invTransferNoteHeader.Remark;

                    //txtGrnNo.Text = invTransferNoteHeader.
                    
                    InvTransferTypeService invTransferTypeService = new InvTransferTypeService();
                    cmbTransferType.Text = invTransferTypeService.GetTransferTypesByID(invTransferNoteHeader.TransferTypeID).TransferType;

                    dgvItemDetails.AutoGenerateColumns = false;

                    dgvItemDetails.DataSource = null;
                    invTransferDetailsTempList = invTransferOfGoodsService.getPausedTOGDetail(invTransferNoteHeader);
                    dgvItemDetails.DataSource = invTransferDetailsTempList;
                    dgvItemDetails.Refresh();


                    InvPurchaseService invPurchaseService = new InvPurchaseService();
                    //invProductSerialNoTempList = invPurchaseService.getSerialNoDetailForTOG(invTransferNoteHeader);
                    

                    Common.EnableTextBox(false, txtDocumentNo, txtReference, txtRemark);
                    Common.EnableComboBox(false, cmbFromLocation, cmbToLocation, cmbTransferType);

                    if (invTransferNoteHeader.DocumentStatus.Equals(0))
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
            if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                RecallDocument(txtDocumentNo.Text.Trim());
            else
            {
                txtDocumentNo.Text = GetDocumentNo(true);
                cmbToLocation.Focus();
            }
        }

        private void chkAutoCompleationDocumentNo_CheckedChanged(object sender, EventArgs e)
        {
            InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
            Common.SetAutoComplete(txtDocumentNo, invTransferOfGoodsService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbFromLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

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
                        loadProductDetailsForGridDoubleClick(true, dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim(), unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID, Common.ConvertStringToDate((dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value == null ? DateTime.Now.ToString() : dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value.ToString())), dgvItemDetails["BatchNo", dgvItemDetails.CurrentCell.RowIndex].Value.ToString());
                        CalculateLine();


                        if (documentState.Equals(1))
                        {
                            EnableLine(false);
                            txtProductCode.Enabled = false;
                            txtProductName.Enabled = false;
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

                        InvTransferDetailsTemp invTransferDetailsTemp = new InvTransferDetailsTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        InvProductMasterService invProductMasterService = new InvProductMasterService();

                        invTransferDetailsTemp.ProductID = invProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).InvProductMasterID;
                        existingInvProductMaster = invProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim());
                        if (existingInvProductMaster.IsExpiry.Equals(true))
                            invTransferDetailsTemp.ExpiryDate = Common.ConvertStringToDate(dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value.ToString());
                        invTransferDetailsTemp.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                        InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();

                        dgvItemDetails.DataSource = null;
                        invTransferDetailsTempList = invTransferOfGoodsService.GetDeleteInvTransferDetailsTemp(invTransferDetailsTempList, invTransferDetailsTemp, invProductSerialNoTempList);
                        dgvItemDetails.DataSource = invTransferDetailsTempList;
                        dgvItemDetails.Refresh();

                        if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                        else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }

                        GetSummarizeFigures(invTransferDetailsTempList);
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
            if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
            {
                if (!txtGrnNo.Text.Trim().Equals(string.Empty))
                {
                    if (RecallGRN(txtGrnNo.Text.Trim())) { ChkBarCodeScan.Enabled = false; }
                }
            }
        }

        private bool RecallGRN(string documentNo)
        {
            try
            {
                recallGRN = true;
                this.Cursor = Cursors.WaitCursor;
                InvPurchaseHeader invPurchaseHeader = new InvPurchaseHeader();
                InvPurchaseService invPurchaseService = new Service.InvPurchaseService();
                InvTransferOfGoodsService invTransferOfGoodsService = new Service.InvTransferOfGoodsService();
                

                invPurchaseHeader = invPurchaseService.GetInvPurchaseHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote").DocumentID, txtGrnNo.Text.Trim(), Common.LoggedLocationID);
                if (invPurchaseHeader != null)
                {
                    //EnableControl(true);
                    SupplierService supplierService = new SupplierService();
                    Supplier supplier = new Supplier();
                    supplier = supplierService.GetSupplierByID(invPurchaseHeader.SupplierID);

                    //dtpDocumentDate.Value = Common.FormatDate(DateTime.Now);
                    txtDocumentNo.Text = GetDocumentNo(true);
                    txtGrnNo.Text = invPurchaseHeader.DocumentNo;
                    txtRemark.Text = invPurchaseHeader.Remark;
                    

                    dgvItemDetails.DataSource = null;

                    int docID;

                    docID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote").DocumentID;
                    invTransferDetailsTempList = invTransferOfGoodsService.GetGRNDetails(invPurchaseHeader, docID);
                    
                    
                    dgvItemDetails.AutoGenerateColumns = false;
                    dgvItemDetails.DataSource = invTransferDetailsTempList;
                    dgvItemDetails.Refresh();

                    GetSummarizeFigures(invTransferDetailsTempList);

                    Common.EnableTextBox(false, txtDocumentNo, txtGrnNo, txtProductCode, txtProductName);
                    Common.EnableComboBox(false, cmbFromLocation);

                    grpFooter.Enabled = true;
                    grpBody.Enabled = true;
                    EnableLine(false);

                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    Common.EnableButton(false, btnDocumentDetails, btnGrnDetails);
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

        private bool RecallReturnDocument(string documentNo)
        {
            try
            {
                recallReturnDocument = true;
                this.Cursor = Cursors.WaitCursor;
                TransactionDet transactionDet = new TransactionDet();
                InvPurchaseService invPurchaseService = new Service.InvPurchaseService();
                LocationService locationService = new Service.LocationService();
                InvTransferOfGoodsService invTransferOfGoodsService = new Service.InvTransferOfGoodsService();



                int transStatus = 0;
                if (cmbTransferType.Text.Trim() == "BRANCH RETURN") { transStatus = 4; }
                else if (cmbTransferType.Text.Trim() == "DAMAGE RETURN") { transStatus = 5; }
                else if (cmbTransferType.Text.Trim() == "TRANSFER NOTE") { transStatus = 2; }
                   
                    txtDocumentNo.Text = GetDocumentNo(true);
                    
                    dgvItemDetails.DataSource = null;

                    invTransferDetailsTempList = invTransferOfGoodsService.GetReturnDocumentDetails(cmbReturnDocument.Text.Trim(), locationService.GetLocationsByName(cmbFromLocation.Text.Trim()).LocationID, transStatus);


                    dgvItemDetails.AutoGenerateColumns = false;
                    dgvItemDetails.DataSource = invTransferDetailsTempList;
                    dgvItemDetails.Refresh();

                    GetSummarizeFigures(invTransferDetailsTempList);

                    Common.EnableTextBox(false, txtDocumentNo, txtGrnNo, txtProductCode, txtProductName);
                    Common.EnableComboBox(false, cmbFromLocation);

                    grpFooter.Enabled = true;
                    grpBody.Enabled = true;
                    EnableLine(false);

                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    Common.EnableButton(false, btnDocumentDetails, btnGrnDetails);
                    this.ActiveControl = txtProductCode;
                    txtProductCode.Focus();
                    dtpExpiry.Value = DateTime.Now;

                    this.Cursor = Cursors.Default;
                    return true;
                //}
                //else
                //{
                //    this.Cursor = Cursors.Default;
                //    return false;
                //}
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
            if (chkAutoCompleationGrnNo.Checked == true)
            {
                //Load GRN Document Numbers -- GRN Document ID- 24 (From AutogenerateInfo Table)
                InvPurchaseService invPurchaseService = new InvPurchaseService();
                Common.SetAutoComplete(txtGrnNo, invPurchaseService.GetAllDocumentNumbers(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote").DocumentID, Convert.ToInt32(cmbFromLocation.SelectedValue)), chkAutoCompleationGrnNo.Checked);
            }
          
        }

        /// <summary>
        /// 
        /// </summary>
        private void GenerateReport(string documentNo, int documentStatus)
        {
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            invReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
        }

        private void cmbTransferType_Validated(object sender, EventArgs e)
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
                if (cmbTransferType.Text.Trim() == "TRANSFER")
                {
                    cmbReturnDocument.Enabled = false;
                    btnReturnDetails.Enabled = false;
                    lblPOSDocuments.Enabled = false;

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
                            //LoadProducts();
                            txtProductCode.Focus();
                        }
                    }
                }
                else
                {
                    cmbReturnDocument.Enabled = true;
                    btnReturnDetails.Enabled = true;
                    lblPOSDocuments.Enabled = true;
                }
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
            if (chkViewStokDetails.Checked == false) { dgvStockDetails.DataSource = null; dgvBatchStock.DataSource = null; }
        }

        private void ChkBarCodeScan_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkBarCodeScan.Checked)
                {
                    chkAutoCompleationProduct.Checked = false;
                    chkAutoCompleationProduct.Enabled = false;

                    //chkOverwrite.Checked = false;
                    //chkOverwrite.Enabled = false;

                    txtProductName.Enabled = false;
                    txtProductCode.Enabled = true;
                    txtProductCode.Focus();
                }
                else
                {
                    this.Cursor = Cursors.WaitCursor;
                    //LoadProducts();
                    this.Cursor = Cursors.Default;

                    chkAutoCompleationProduct.Checked = true;
                    chkAutoCompleationProduct.Enabled = true;

                    //chkOverwrite.Checked = true;
                    //chkOverwrite.Enabled = true;

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
                if (chkViewStokDetails.Checked)
                {
                    grpStockDetails.Enabled = true;
                    DisplayStockDetails();
                }
                else
                {
                    grpStockDetails.Enabled = false;
                }
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

        private void btnReturnDetails_Click(object sender, EventArgs e)
        {
            if (!cmbReturnDocument.Text.Trim().Equals(string.Empty))
            {
                if (RecallReturnDocument(cmbReturnDocument.Text.Trim())) { ChkBarCodeScan.Enabled = false; }
            }
        }

        private void btnGetReturnDocuments_Click(object sender, EventArgs e)
        {
           
            int transStatus = 0;

            if (cmbTransferType.Text.Trim() != "TRANSFER")
            {
                // Load POS Documents
                if (cmbTransferType.Text.Trim() == "BRANCH RETURN") { transStatus = 4; }
                else if (cmbTransferType.Text.Trim() == "DAMAGE RETURN") { transStatus = 5; }
                else if (cmbTransferType.Text.Trim() == "TRANSFER NOTE") { transStatus = 2; }

                InvTransferTypeService invTransferTypeService = new InvTransferTypeService();
                LocationService locationService = new LocationService();
                Common.LoadPOSDocuments(cmbReturnDocument, invTransferTypeService.GetPOSDocuments(locationService.GetLocationsByName(cmbFromLocation.Text.Trim()).LocationID, transStatus));
                cmbReturnDocument.SelectedIndex = -1;

                cmbReturnDocument.Enabled = true;
            }
            
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            string PreviousDocumentNo = "";
            InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
           
            PreviousDocumentNo = invTransferOfGoodsService.GetPreviousDocumentNo(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), documentID, Common.LoggedUser.Trim() );
            if (string.IsNullOrEmpty(PreviousDocumentNo))
            {
                Toast.Show("No data available to display report", Toast.messageType.Information, Toast.messageAction.General, "");
                return;
            }
            else
            {
                GenerateReport(PreviousDocumentNo.Trim(), 2);
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

        private void btnFromScanner_Click(object sender, EventArgs e)
        {
            try
            {
                PnlRemoteData.Visible = true;
            }

            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Toast.Show("Are you sure you want to download selected data" , Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.Yes)))
                {
                    PnlRemoteData.Visible = false;
                }
            }

            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnDiscard_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Toast.Show("Are you sure you want to ignore", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.Yes)))
                {
                    PnlRemoteData.Visible = false;
                }
            }

            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnLoadDocuments_Click(object sender, EventArgs e)
        {
            try
            {
               
                for (int i = 0; i < dgvScanner.RowCount; i++)
                {
                    if (dgvScanner.Rows[i].Cells["ChkScSelect"].Value == "1")
                    {

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
