using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Domain;
using Utility;
using Service;
using Report.Logistic;

namespace UI.Windows
{
    public partial class FrmLogisticStockAdjustment : UI.Windows.FrmBaseTransactionForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<LgsStockAdjustmentDetailTemp> lgsStockAdjustmentDetailTempList = new List<LgsStockAdjustmentDetailTemp>();
        LgsStockAdjustmentDetailTemp existingLgsStockAdjustmentDetailTemp = new LgsStockAdjustmentDetailTemp();

        List<InvProductSerialNoTemp> lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

        List<LgsProductBatchNoTemp> lgsProductBatchNoTempList = new List<LgsProductBatchNoTemp>();
        LgsProductBatchNoTemp existingLgsProductBatchNoTemp = new LgsProductBatchNoTemp();

        List<LgsProductExpiaryTemp> lgsProductExpiaryTempList = new List<LgsProductExpiaryTemp>();

        LgsProductMaster existingLgsProductMaster;

        int documentID = 0;
        int documentState;
        bool isExcess = false;
        bool isShortage = false;
        bool isOverwrite = false;
        bool isInvProduct;
        int adjustmentMode; // 1-->Excess, 2-->Shortage, 3-->Overwrite
        static string batchNumber;
        static DateTime expiryDate;

        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;
         
        public FrmLogisticStockAdjustment()
        {
            InitializeComponent();
        }

        private void FrmLogisticStockAdjustment_Load(object sender, EventArgs e)
        {

        }

        private void ShowDocumentNo()
        {
            txtDocumentNo.Text = GetDocumentNo(true);
        }

        public override void InitializeForm()
        {
            try
            {
                grpFooter.Enabled = false;
                EnableLine(false);
                Common.EnableTextBox(false, txtProductCode, txtProductName);
                Common.EnableTextBox(true, txtReference, txtRemark, txtNarration, txtDocumentNo);
                Common.EnableComboBox(true, cmbLocation, cmbMode);
                Common.EnableButton(false, btnSave, btnPause);
                lgsStockAdjustmentDetailTempList = null;
                this.ActiveControl = cmbMode;
                cmbMode.Focus();
                cmbLocation.SelectedValue = Common.LoggedLocationID;
                isInvProduct = false;

                selectedRowIndex = 0;
                isUpdateGrid = false;
                rowCount = 0;

                ShowDocumentNo();
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
                LgsStockAdjustmentService stockAdjustmentService = new LgsStockAdjustmentService();
                LocationService locationService = new LocationService();
                return stockAdjustmentService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo, isExcess, isShortage, isOverwrite).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }
        }

        private void LoadProducts()
        {
            //InvProductMasterService invProductMasterService = new InvProductMasterService();
            //Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
            //Common.SetAutoComplete(txtProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);

            LgsStockAdjustmentService lgsStockAdjustmentService = new LgsStockAdjustmentService();

            Common.SetAutoComplete(txtProductCode, lgsStockAdjustmentService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
            Common.SetAutoComplete(txtProductName, lgsStockAdjustmentService.GetAllProductNames(), chkAutoCompleationProduct.Checked);

        }

        private void EnableLine(bool enable)
        {
            Common.EnableTextBox(enable, txtQty, txtBatchNo, txtCostPrice, txtSellingPrice, txtQty, txtCostValue, txtSellingValue);
            Common.EnableComboBox(enable, cmbUnit);
            dtpExpiry.Enabled = enable;
        }

        public override void FormLoad()
        {
            try
            {
                dgvItemDetails.AutoGenerateColumns = false;
                
                // Load Unit of measures
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                Common.LoadUnitOfMeasures(cmbUnit, unitOfMeasureService.GetAllUnitOfMeasures());

                // Load Locations
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                //Load Stock Adjustment Mode
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                Common.SetAutoBindRecords(cmbMode, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.StockAdjustmentMode).ToString()));

                chkOverwrite.Checked = autoGenerateInfo.IsOverWriteQty;
                documentID = autoGenerateInfo.DocumentID;

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }
                
                isInvProduct = false;

                base.FormLoad();

                //Load Document Numbers
                LgsStockAdjustmentService stockAdjustmentService = new LgsStockAdjustmentService();
                Common.SetAutoComplete(txtDocumentNo, stockAdjustmentService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void GetAdjustmentMode()
        {
            if (cmbMode.Text.Equals("Add Stock"))
            {
                isExcess = true;
                isShortage = false;
                isOverwrite = false;
                adjustmentMode = 1;
            }
            else if (cmbMode.Text.Equals("Reduce Stock"))
            {
                isShortage = true;
                isExcess = false;
                isOverwrite = false;
                adjustmentMode = 2;
            }
            else
            {
                isOverwrite = true;
                isExcess = false;
                isShortage = false;
                adjustmentMode = 3;
            }
        }

        private void RefreshDocumentNumber() 
        {
            ////Load Quotation Document Numbers
            LgsStockAdjustmentService stockAdjustmentService = new LgsStockAdjustmentService();
            Common.SetAutoComplete(txtDocumentNo, stockAdjustmentService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
        }

        private void cmbMode_SelectedValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                GetAdjustmentMode();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbMode_KeyDown(object sender, KeyEventArgs e)
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
                    txtNarration.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    dtpAdjustmentDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpAdjustmentDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtReference.Focus();
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
                    cmbLocation.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbLocation_Validated(object sender, System.EventArgs e)
        {
            try
            {
                LocationService locationService = new LocationService();

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbLocation.Text.Trim()).LocationID, documentID);
                if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }


                if (cmbLocation.SelectedValue == null || locationService.IsExistsLocation(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim())).Equals(false))
                {
                    Toast.Show("Location - " + cmbLocation.Text.Trim().Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    cmbLocation.Focus();
                    return;
                }
                else
                {
                    Common.EnableTextBox(true, txtProductCode, txtProductName);
                    LoadProducts();
                    txtProductCode.Focus();
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
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    cmbLocation_Validated(this, e);
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
                    //if (!txtProductCode.Text.Trim().Equals(string.Empty))
                    //{
                        txtProductName.Enabled = true;
                        txtProductName.Focus();
                    //}
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductCode_Leave(object sender, System.EventArgs e)
        {
            loadProductDetails(true, txtProductCode.Text.Trim(), 0, dtpExpiry.Value);
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
                    LgsStockAdjustmentService stockAdjustmentService = new LgsStockAdjustmentService();
                    if (lgsStockAdjustmentDetailTempList == null)
                        lgsStockAdjustmentDetailTempList = new List<LgsStockAdjustmentDetailTemp>();
                    existingLgsStockAdjustmentDetailTemp = stockAdjustmentService.getStockAdjustmentDetailTemp(lgsStockAdjustmentDetailTempList, existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID);
                    if (existingLgsStockAdjustmentDetailTemp != null)
                    {
                        txtProductCode.Text = existingLgsStockAdjustmentDetailTemp.ProductCode;
                        txtProductName.Text = existingLgsStockAdjustmentDetailTemp.ProductName;
                        cmbUnit.SelectedValue = existingLgsStockAdjustmentDetailTemp.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingLgsStockAdjustmentDetailTemp.CostPrice);
                        txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingLgsStockAdjustmentDetailTemp.SellingPrice);
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingLgsStockAdjustmentDetailTemp.OrderQty);
                        
                        if (existingLgsProductMaster.IsExpiry)
                        {
                            dtpExpiry.Value = Common.ConvertStringToDate((existingLgsStockAdjustmentDetailTemp.ExpiryDate == null ? dtpExpiry.Value.ToString() : existingLgsStockAdjustmentDetailTemp.ExpiryDate.ToString()));
                            dtpExpiry.Enabled = true;
                        }
                        else
                        {
                            dtpExpiry.Value = Common.ConvertDateTimeToDate(DateTime.Now);
                            dtpExpiry.Enabled = false;
                        }
                    }
                }
                else
                {
                    Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
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

        private void txtProductName_Leave(object sender, System.EventArgs e)
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

        private void cmbUnit_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (cmbUnit.SelectedValue == null)
                    return;

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
                loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()), dtpExpiry.Value);
            
                if (existingLgsProductMaster.IsBatch)
                {
                    txtBatchNo.Enabled = true;
                    txtBatchNo.Focus();
                }
                else if(existingLgsProductMaster.IsExpiry)
                {
                    dtpExpiry.Enabled = true;
                    dtpExpiry.Focus();
                }
                else if (existingLgsProductMaster.IsSerial)
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
                    if (!txtProductName.Text.Trim().Equals(string.Empty))
                    {
                        LgsProductBatchNoTemp lgsProductBatchNoTemp = new LgsProductBatchNoTemp();
                        //invProductBatchNoTemp.DocumentID = documentID;
                        lgsProductBatchNoTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                        lgsProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        LocationService locationService = new LocationService();

                        if (txtBatchNo.Text.Trim() != "")
                        {
                            LgsProductBatchNoExpiaryDetail lgsProductBatchNo = new LgsProductBatchNoExpiaryDetail();

                            CommonService commonService = new CommonService();
                            lgsProductBatchNo = commonService.CheckLgsBatchNumber(txtBatchNo.Text.Trim(), existingLgsProductMaster.LgsProductMasterID, locationService.GetLocationsByName(cmbLocation.Text).LocationID, existingLgsProductMaster.UnitOfMeasureID);

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

                            LgsStockAdjustmentService stockAdjustmentService = new LgsStockAdjustmentService();
                            lgsProductBatchNoTempList = stockAdjustmentService.GetBatchNoDetail(existingLgsProductMaster, locationService.GetLocationsByName(cmbLocation.Text).LocationID);

                            if (lgsProductBatchNoTempList == null)
                                lgsProductBatchNoTempList = new List<LgsProductBatchNoTemp>();

                            FrmBatchNumber frmBatchNumber = new FrmBatchNumber(lgsProductBatchNoTempList, lgsProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, FrmBatchNumber.transactionType.LogisticStockAdjustment, existingLgsProductMaster.LgsProductMasterID);
                            frmBatchNumber.ShowDialog();

                            LgsProductBatchNoExpiaryDetail lgsProductBatchNo = new LgsProductBatchNoExpiaryDetail();
                            txtBatchNo.Text = batchNumber;                        

                            CommonService commonService = new CommonService();
                        
                            lgsProductBatchNo = commonService.CheckLgsBatchNumber(txtBatchNo.Text.Trim(), existingLgsProductMaster.LgsProductMasterID, locationService.GetLocationsByName(cmbLocation.Text).LocationID, existingLgsProductMaster.UnitOfMeasureID);

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
        }

        private void txtBatchNo_Leave(object sender, System.EventArgs e)
        {
            
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

                        LgsStockAdjustmentService stockAdjustmentService = new LgsStockAdjustmentService();
                        lgsProductBatchNoTempList = stockAdjustmentService.GetExpiryDetail(existingLgsProductMaster);

                        if (lgsProductSerialNoTempList == null)
                            lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                        //FrmExpiary frmExpiry = new FrmExpiary(invProductBatchNoTempList, invProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, FrmExpiary.transactionType.StockAdjustment);
                        //frmExpiry.ShowDialog();

                        //dtpExpiry.Value = expiryDate;

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

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (existingLgsProductMaster.IsSerial)
                    {
                        if (!txtProductName.Text.Trim().Equals(string.Empty))
                        {
                            InvProductSerialNoTemp lgsProductSerialNoTemp = new InvProductSerialNoTemp();
                            //invProductSerialNoTemp.DocumentID = documentID;
                            lgsProductSerialNoTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                            lgsProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                            LgsStockAdjustmentService stockAdjustmentService = new LgsStockAdjustmentService();
                            lgsProductSerialNoTempList = stockAdjustmentService.GetSerialNoDetail(existingLgsProductMaster);

                            if (lgsProductSerialNoTempList == null)
                                lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                            FrmSerialCommon frmSerialCommon = new FrmSerialCommon(lgsProductSerialNoTempList, lgsProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, documentID, FrmSerialCommon.transactionType.StockAdjustment);
                            frmSerialCommon.ShowDialog();

                            CalculateLine();

                            txtSellingValue.Enabled = true;
                            txtSellingValue.Focus();
                        }
                    }
                    else
                    {
                        CalculateLine();
                        txtSellingValue.Enabled = true;
                        txtSellingValue.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void SetLgsSerialNoList(List<InvProductSerialNoTemp> setInvProductSerialNoTemp) 
        {
            lgsProductSerialNoTempList = setInvProductSerialNoTemp;
        }

        private void txtCostPrice_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void CalculateLine(decimal qty = 0)
        {
            try
            {
                if (qty == 0)
                {
                    txtCostValue.Text = (Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim())).ToString();
                    txtSellingValue.Text = (Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text.Trim())).ToString();
                }
                else
                {
                    txtCostValue.Text = (Common.ConvertDecimalToDecimalQty(qty) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim())).ToString();
                    txtSellingValue.Text = (Common.ConvertDecimalToDecimalQty(qty) * Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text.Trim())).ToString();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }



        private void txtSellingValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    UpdateGrid(existingLgsStockAdjustmentDetailTemp);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void UpdateGrid(LgsStockAdjustmentDetailTemp lgsStockAdjustmentDetailTemp)
        {
            try
            {
                decimal qty = 0;
                if ((Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) > 0) && (Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) > 0) && (Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text.Trim()) > 0))
                {
                    lgsStockAdjustmentDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                    if (isInvProduct) { lgsStockAdjustmentDetailTemp.BaseUnitID = existingLgsProductMaster.UnitOfMeasureID; }
                    else { lgsStockAdjustmentDetailTemp.BaseUnitID = existingLgsProductMaster.UnitOfMeasureID; }

                    lgsStockAdjustmentDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();

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
                        qty = (lgsStockAdjustmentDetailTemp.OrderQty + Common.ConvertStringToDecimalQty(txtQty.Text));
                    }

                    if (!chkOverwrite.Checked)
                    {
                        CalculateLine(qty);
                        lgsStockAdjustmentDetailTemp.OrderQty = qty;
                    }
                    else
                    {
                        CalculateLine();
                        lgsStockAdjustmentDetailTemp.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text.Trim());
                    }

                    
                    lgsStockAdjustmentDetailTemp.BatchNo = txtBatchNo.Text.Trim();
                    lgsStockAdjustmentDetailTemp.ExpiryDate = dtpExpiry.Value;
                    lgsStockAdjustmentDetailTemp.DocumentNo = txtDocumentNo.Text.Trim();

                    lgsStockAdjustmentDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text);
                    lgsStockAdjustmentDetailTemp.CostValue = Common.ConvertStringToDecimalCurrency(txtCostValue.Text);
                    lgsStockAdjustmentDetailTemp.SellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text);
                    lgsStockAdjustmentDetailTemp.SellingValue = Common.ConvertStringToDecimalCurrency(txtSellingValue.Text);

                    LgsStockAdjustmentService stockAdjustmentService = new LgsStockAdjustmentService();

                    dgvItemDetails.DataSource = null;
                    lgsStockAdjustmentDetailTempList = stockAdjustmentService.getUpdateLgsStockAdjustmentDetailTemp(lgsStockAdjustmentDetailTempList, lgsStockAdjustmentDetailTemp, existingLgsProductMaster);
                    dgvItemDetails.DataSource = lgsStockAdjustmentDetailTempList;
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

                    GetSummarizeFigures(lgsStockAdjustmentDetailTempList);
                    EnableLine(false);
                    Common.EnableTextBox(false, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation);
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    ClearLine();

                    if (lgsStockAdjustmentDetailTempList.Count > 0)
                        grpFooter.Enabled = true;

                    txtProductCode.Enabled = true;
                    txtProductCode.Focus();
                }
                else
                {
                    Toast.Show("Invalid Operation", Toast.messageType.Information, Toast.messageAction.ValidationFailed);
                    txtProductCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void GetSummarizeFigures(List<LgsStockAdjustmentDetailTemp> listItem) 
        {
            decimal totSellingValue = listItem.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.SellingValue);
            decimal totCostValue = listItem.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.CostValue);
            decimal totQty = listItem.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.OrderQty);

            totSellingValue = Common.ConvertStringToDecimalCurrency(totSellingValue.ToString());
            totCostValue = Common.ConvertStringToDecimalCurrency(totCostValue.ToString());
            totQty = Common.ConvertStringToDecimalQty(totQty.ToString());

            txtTotalSellingValue.Text = Common.ConvertDecimalToStringCurrency(totSellingValue);
            txtTotalCostValue.Text = Common.ConvertDecimalToStringCurrency(totCostValue);
            txtTotalQty.Text = Common.ConvertDecimalToStringQty(totQty);
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode, txtProductName, txtBatchNo, txtQty, txtCostPrice, txtSellingPrice, txtCostValue, txtSellingValue);
            Common.ClearComboBox(cmbUnit);
            dtpExpiry.Value = DateTime.Now;
            txtProductCode.Focus();
        }

        public override void ClearForm()
        {
            errorProvider.Clear();
            dtpAdjustmentDate.Value = DateTime.Now;
            dtpExpiry.Value = DateTime.Now;
            dgvItemDetails.DataSource = null;
            dgvItemDetails.Refresh();
            base.ClearForm();
        }

        private bool ValidateTextBoxes()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo);
        }

        private bool ValidateComboBoxes()
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbMode, cmbLocation);
        }

        public override void Pause()   
        {
            if (ValidateTextBoxes().Equals(false)) { return; }
            if (ValidateComboBoxes().Equals(false)) { return; }

            if ((Toast.Show("Stock Adjustment  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                
                if (SaveDocument(0, txtDocumentNo.Text.Trim()).Equals(true))
                {
                    Toast.Show("Stock Adjustment  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    GenerateReport(txtDocumentNo.Text.Trim(), 0);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Stock Adjustment  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        public override void Save()
        {
            if (ValidateTextBoxes().Equals(false)) { return; }
            if (ValidateComboBoxes().Equals(false)) { return; }

            if ((Toast.Show("Stock Adjustment  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim());
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Stock Adjustment  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(txtDocumentNo.Text.Trim(), 1);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Stock Adjustment  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        private bool SaveDocument(int documentStatus, string documentNo)
        {
            try
            {
                LgsStockAdjustmentService stockAdjustmentService = new LgsStockAdjustmentService();
                LgsStockAdjustmentHeader lgsStockAdjustmentHeader = new LgsStockAdjustmentHeader();
                LocationService locationService = new LocationService();
                Location Location = new Location();

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                lgsStockAdjustmentHeader = stockAdjustmentService.GetPausedLgsStockAdjustmentHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (lgsStockAdjustmentHeader == null)
                    lgsStockAdjustmentHeader = new LgsStockAdjustmentHeader();

                if (documentStatus.Equals(1)) // update paused document
                {
                    documentNo = GetDocumentNo(false);
                    txtDocumentNo.Text = documentNo;
                }

                //sampleOutHeader.SampleOutHeaderID = sampleOutHeader.SampleHeaderID;
                lgsStockAdjustmentHeader.CompanyID = Location.CompanyID;
                lgsStockAdjustmentHeader.DocumentDate = dtpAdjustmentDate.Value;
                lgsStockAdjustmentHeader.DocumentID = documentID;
                lgsStockAdjustmentHeader.DocumentStatus = documentStatus;
                lgsStockAdjustmentHeader.DocumentNo = documentNo.Trim();
                lgsStockAdjustmentHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                lgsStockAdjustmentHeader.LocationID = Location.LocationID;
                lgsStockAdjustmentHeader.Narration = txtNarration.Text.Trim();
                lgsStockAdjustmentHeader.TotalQty = Common.ConvertStringToDecimalQty(txtTotalQty.Text.ToString());
                lgsStockAdjustmentHeader.TotalCostValue = Common.ConvertStringToDecimalCurrency(txtTotalCostValue.Text.Trim());
                lgsStockAdjustmentHeader.TotalSellingtValue = Common.ConvertStringToDecimalCurrency(txtTotalSellingValue.Text.Trim());
                lgsStockAdjustmentHeader.ReferenceDocumentNo = txtReference.Text.Trim();
                lgsStockAdjustmentHeader.Remark = txtRemark.Text.Trim();
                lgsStockAdjustmentHeader.StockAdjustmentMode = adjustmentMode;

                if (lgsStockAdjustmentDetailTempList == null)
                    lgsStockAdjustmentDetailTempList = new List<LgsStockAdjustmentDetailTemp>(); 

                if (lgsProductSerialNoTempList == null)
                    lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                return stockAdjustmentService.Save(lgsStockAdjustmentHeader, lgsStockAdjustmentDetailTempList, lgsProductSerialNoTempList, isExcess, isShortage, isOverwrite);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return false;
            }

        }

        private void RecallAdjustmentMode(int mode)
        {
            if (mode.Equals(1))
            {
                cmbMode.SelectedIndex = 0;
            }
            else if (mode.Equals(2))
            {
                cmbMode.SelectedIndex = 1;
            }
            else if (mode.Equals(3))
            {
                cmbMode.SelectedIndex = 2;
            }
        }

        private bool RecallDocument(string documentNo)
        {
            try
            {
                LgsStockAdjustmentService stockAdjustmentService = new LgsStockAdjustmentService();
                LgsStockAdjustmentHeader lgsStockAdjustmentHeader = new LgsStockAdjustmentHeader();

                lgsStockAdjustmentHeader = stockAdjustmentService.GetPausedLgsStockAdjustmentHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (lgsStockAdjustmentHeader != null)
                {
                    cmbLocation.SelectedValue = lgsStockAdjustmentHeader.LocationID;
                    cmbLocation.Refresh();

                    documentState = lgsStockAdjustmentHeader.DocumentStatus;

                    dtpAdjustmentDate.Value = Common.FormatDate(lgsStockAdjustmentHeader.DocumentDate);

                    txtDocumentNo.Text = lgsStockAdjustmentHeader.DocumentNo;
                    txtTotalCostValue.Text = Common.ConvertDecimalToStringCurrency(lgsStockAdjustmentHeader.TotalCostValue);
                    txtTotalSellingValue.Text = Common.ConvertDecimalToStringCurrency(lgsStockAdjustmentHeader.TotalSellingtValue);
                    txtTotalQty.Text = Common.ConvertDecimalToStringQty(lgsStockAdjustmentHeader.TotalQty);
                    txtReference.Text = lgsStockAdjustmentHeader.ReferenceDocumentNo;
                    txtRemark.Text = lgsStockAdjustmentHeader.Remark;
                    txtNarration.Text = lgsStockAdjustmentHeader.Narration;

                    if (lgsStockAdjustmentHeader.StockAdjustmentMode.Equals(1))
                    {
                        RecallAdjustmentMode(1);
                    }
                    else if (lgsStockAdjustmentHeader.StockAdjustmentMode.Equals(2))
                    {
                        RecallAdjustmentMode(2);
                    }
                    else if (lgsStockAdjustmentHeader.StockAdjustmentMode.Equals(3))
                    {
                        RecallAdjustmentMode(3);
                    }

                    dgvItemDetails.DataSource = null;
                    lgsStockAdjustmentDetailTempList = stockAdjustmentService.getPausedLgsStockAdjustmentDetailTemp(lgsStockAdjustmentHeader);
                    dgvItemDetails.DataSource = lgsStockAdjustmentDetailTempList;
                    dgvItemDetails.Refresh();

                    Common.EnableTextBox(false, txtNarration, txtReference, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation, cmbMode);

                    if (lgsStockAdjustmentHeader.DocumentStatus.Equals(0))
                    {
                        grpFooter.Enabled = true;
                        EnableLine(true);
                        if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    }
                    else
                    {

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

        private void txtDocumentNo_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                    RecallDocument(txtDocumentNo.Text.Trim());
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
                            txtBatchNo.Enabled = true;
                            this.ActiveControl = txtBatchNo;
                            txtBatchNo.Focus();
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

                        LgsStockAdjustmentDetailTemp lgsStockAdjustmentTempDetail = new LgsStockAdjustmentDetailTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                        lgsStockAdjustmentTempDetail.ProductID = lgsProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).LgsProductMasterID;
                        lgsStockAdjustmentTempDetail.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                        LgsStockAdjustmentService lgsStockAdjustmentService = new LgsStockAdjustmentService();

                        dgvItemDetails.DataSource = null;
                        lgsStockAdjustmentDetailTempList = lgsStockAdjustmentService.GetDeleteStockAdjustmentDetailTemp(lgsStockAdjustmentDetailTempList, lgsStockAdjustmentTempDetail);
                        dgvItemDetails.DataSource = lgsStockAdjustmentDetailTempList;
                        dgvItemDetails.Refresh();

                        if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                        else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }

                        GetSummarizeFigures(lgsStockAdjustmentDetailTempList);
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

        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
