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
using Report.Inventory;

namespace UI.Windows
{
    public partial class FrmStockAdjustment : UI.Windows.FrmBaseTransactionForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<InvStockAdjustmentDetailTemp> invStockAdjustmentDetailTempList = new List<InvStockAdjustmentDetailTemp>();
        InvStockAdjustmentDetailTemp existingInvStockAdjustmentDetailTemp = new InvStockAdjustmentDetailTemp();

        List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

        List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();
        InvProductBatchNoTemp existingInvProductBatchNoTemp = new InvProductBatchNoTemp();

        List<InvProductExpiaryTemp> invProductExpiaryTempList = new List<InvProductExpiaryTemp>();

        InvProductMaster existingInvProductMaster;

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
         
        public FrmStockAdjustment()
        {
            InitializeComponent();
        }

        private void FrmStockAdjustment_Load(object sender, EventArgs e)
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
                invStockAdjustmentDetailTempList = null;
                this.ActiveControl = cmbMode;
                cmbMode.Focus();
                cmbLocation.SelectedValue = Common.LoggedLocationID;

                cmbLayer.SelectedIndex = -1;
                lstLayer.Clear();

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
                InvStockAdjustmentService stockAdjustmentService = new InvStockAdjustmentService();
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

            InvStockAdjustmentService invStockAdjustmentService = new InvStockAdjustmentService();

            Common.SetAutoComplete(txtProductCode, invStockAdjustmentService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
            Common.SetAutoComplete(txtProductName, invStockAdjustmentService.GetAllProductNames(), chkAutoCompleationProduct.Checked);

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
                if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }
                
                isInvProduct = true;

                base.FormLoad();

                //Load Document Numbers
                InvStockAdjustmentService stockAdjustmentService = new InvStockAdjustmentService();
                Common.SetAutoComplete(txtDocumentNo, stockAdjustmentService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

                InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                Common.LoadTOGDocuments(cmbTogDocument, invTransferOfGoodsService.GetTOGDocumentsForStAdj(Common.LoggedLocationID));
                cmbTogDocument.SelectedIndex = -1;
                cmbTogDocument.Enabled = true;
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
            InvStockAdjustmentService stockAdjustmentService = new InvStockAdjustmentService();
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
                if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }


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
                    InvStockAdjustmentService stockAdjustmentService = new InvStockAdjustmentService();
                    if (invStockAdjustmentDetailTempList == null)
                        invStockAdjustmentDetailTempList = new List<InvStockAdjustmentDetailTemp>();
                    existingInvStockAdjustmentDetailTemp = stockAdjustmentService.getStockAdjustmentDetailTemp(invStockAdjustmentDetailTempList, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID);
                    if (existingInvStockAdjustmentDetailTemp != null)
                    {
                        txtProductCode.Text = existingInvStockAdjustmentDetailTemp.ProductCode;
                        txtProductName.Text = existingInvStockAdjustmentDetailTemp.ProductName;
                        cmbUnit.SelectedValue = existingInvStockAdjustmentDetailTemp.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingInvStockAdjustmentDetailTemp.CostPrice);
                        txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingInvStockAdjustmentDetailTemp.SellingPrice);
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingInvStockAdjustmentDetailTemp.OrderQty);
                        
                        if (existingInvProductMaster.IsExpiry)
                        {
                            dtpExpiry.Value = Common.ConvertStringToDate((existingInvStockAdjustmentDetailTemp.ExpiryDate == null ? dtpExpiry.Value.ToString() : existingInvStockAdjustmentDetailTemp.ExpiryDate.ToString()));
                            dtpExpiry.Enabled = true;
                        }
                        else
                        {
                            dtpExpiry.Value = Common.ConvertDateTimeToDate(DateTime.Now);
                            dtpExpiry.Enabled = false;
                        }
                        //Common.EnableComboBox(true, cmbUnit);
                        //if (unitofMeasureID.Equals(0))
                        //    cmbUnit.Focus();
                        txtBatchNo.Enabled = true;
                        txtBatchNo.Focus();
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
                else if(existingInvProductMaster.IsExpiry)
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
                        
                            InvStockAdjustmentService stockAdjustmentService = new InvStockAdjustmentService();
                            invProductBatchNoTempList = stockAdjustmentService.GetBatchNoDetail(existingInvProductMaster, locationService.GetLocationsByName(cmbLocation.Text).LocationID);

                            if (invProductBatchNoTempList == null)
                                invProductBatchNoTempList = new List<InvProductBatchNoTemp>();

                            FrmBatchNumber frmBatchNumber = new FrmBatchNumber(invProductBatchNoTempList, invProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, FrmBatchNumber.transactionType.StockAdjustment, existingInvProductMaster.InvProductMasterID);
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
                        InvProductBatchNoTemp invProductBatchNoTemp = new InvProductBatchNoTemp();
                        //invProductBatchNoTemp.DocumentID = documentID;
                        invProductBatchNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                        invProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        InvStockAdjustmentService stockAdjustmentService = new InvStockAdjustmentService();
                        invProductBatchNoTempList = stockAdjustmentService.GetExpiryDetail(existingInvProductMaster);

                        if (invProductSerialNoTempList == null)
                            invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

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
                    if (existingInvProductMaster.IsSerial)
                    {
                        if (!txtProductName.Text.Trim().Equals(string.Empty))
                        {
                            InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();
                            //invProductSerialNoTemp.DocumentID = documentID;
                            invProductSerialNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                            invProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                            InvStockAdjustmentService stockAdjustmentService = new InvStockAdjustmentService();
                            invProductSerialNoTempList = stockAdjustmentService.GetSerialNoDetail(existingInvProductMaster);

                            if (invProductSerialNoTempList == null)
                                invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                            FrmSerialCommon frmSerialCommon = new FrmSerialCommon(invProductSerialNoTempList, invProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, documentID, FrmSerialCommon.transactionType.StockAdjustment);
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

        public void SetInvSerialNoList(List<InvProductSerialNoTemp> setInvProductSerialNoTemp)
        {
            invProductSerialNoTempList = setInvProductSerialNoTemp;
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
                    UpdateGrid(existingInvStockAdjustmentDetailTemp);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void UpdateGrid(InvStockAdjustmentDetailTemp invStockAdjustmentDetailTemp)
        {
            try
            {
                decimal qty = 0;
                if ((Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) > 0) && (Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) > 0) && (Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text.Trim()) > 0))
                {
                    invStockAdjustmentDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                    if (isInvProduct) { invStockAdjustmentDetailTemp.BaseUnitID = existingInvProductMaster.UnitOfMeasureID; }
                    else { invStockAdjustmentDetailTemp.BaseUnitID = existingInvProductMaster.UnitOfMeasureID; }

                    invStockAdjustmentDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();

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
                        qty = (invStockAdjustmentDetailTemp.OrderQty + Common.ConvertStringToDecimalQty(txtQty.Text));
                    }

                    if (!chkOverwrite.Checked)
                    {
                        CalculateLine(qty);
                        invStockAdjustmentDetailTemp.OrderQty = qty;
                    }
                    else
                    {
                        CalculateLine();
                        invStockAdjustmentDetailTemp.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text.Trim());
                    }

                    
                    invStockAdjustmentDetailTemp.BatchNo = txtBatchNo.Text.Trim();
                    invStockAdjustmentDetailTemp.ExpiryDate = dtpExpiry.Value;
                    invStockAdjustmentDetailTemp.DocumentNo = txtDocumentNo.Text.Trim();

                    invStockAdjustmentDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text);
                    invStockAdjustmentDetailTemp.CostValue = Common.ConvertStringToDecimalCurrency(txtCostValue.Text);
                    invStockAdjustmentDetailTemp.SellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text);
                    invStockAdjustmentDetailTemp.SellingValue = Common.ConvertStringToDecimalCurrency(txtSellingValue.Text);

                    InvStockAdjustmentService stockAdjustmentService = new InvStockAdjustmentService();

                    dgvItemDetails.DataSource = null;
                    invStockAdjustmentDetailTempList = stockAdjustmentService.getUpdateInvStockAdjustmentDetailTemp(invStockAdjustmentDetailTempList, invStockAdjustmentDetailTemp, existingInvProductMaster);
                    dgvItemDetails.DataSource = invStockAdjustmentDetailTempList;
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

                    GetSummarizeFigures(invStockAdjustmentDetailTempList);
                    EnableLine(false);
                    Common.EnableTextBox(false, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation);
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    ClearLine();

                    if (invStockAdjustmentDetailTempList.Count > 0)
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

        private void GetSummarizeFigures(List<InvStockAdjustmentDetailTemp> listItem) 
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
                InvStockAdjustmentService stockAdjustmentService = new InvStockAdjustmentService();
                InvStockAdjustmentHeader invStockAdjustmentHeader = new InvStockAdjustmentHeader();
                LocationService locationService = new LocationService();
                Location Location = new Location();

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                invStockAdjustmentHeader = stockAdjustmentService.GetPausedInvStockAdjustmentHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (invStockAdjustmentHeader == null)
                    invStockAdjustmentHeader = new InvStockAdjustmentHeader();

                if (documentStatus.Equals(1)) // update paused document
                {
                    documentNo = GetDocumentNo(false);
                    txtDocumentNo.Text = documentNo;
                }

                //sampleOutHeader.SampleOutHeaderID = sampleOutHeader.SampleHeaderID;
                invStockAdjustmentHeader.CompanyID = Location.CompanyID;
                invStockAdjustmentHeader.DocumentDate = dtpAdjustmentDate.Value;
                invStockAdjustmentHeader.DocumentID = documentID;
                invStockAdjustmentHeader.DocumentStatus = documentStatus;
                invStockAdjustmentHeader.DocumentNo = documentNo.Trim();
                invStockAdjustmentHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                invStockAdjustmentHeader.LocationID = Location.LocationID;
                invStockAdjustmentHeader.Narration = txtNarration.Text.Trim();
                invStockAdjustmentHeader.TotalQty = Common.ConvertStringToDecimalQty(txtTotalQty.Text.ToString());
                invStockAdjustmentHeader.TotalCostValue = Common.ConvertStringToDecimalCurrency(txtTotalCostValue.Text.Trim());
                invStockAdjustmentHeader.TotalSellingtValue = Common.ConvertStringToDecimalCurrency(txtTotalSellingValue.Text.Trim());
                invStockAdjustmentHeader.ReferenceDocumentNo = txtReference.Text.Trim();
                invStockAdjustmentHeader.Remark = txtRemark.Text.Trim();
                invStockAdjustmentHeader.StockAdjustmentMode = adjustmentMode;

                if (invStockAdjustmentDetailTempList == null)
                    invStockAdjustmentDetailTempList = new List<InvStockAdjustmentDetailTemp>(); 

                if (invProductSerialNoTempList == null)
                    invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                return stockAdjustmentService.Save(invStockAdjustmentHeader, invStockAdjustmentDetailTempList, invProductSerialNoTempList, isExcess, isShortage, isOverwrite);

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
                InvStockAdjustmentService stockAdjustmentService = new InvStockAdjustmentService();
                InvStockAdjustmentHeader invStockAdjustmentHeader = new InvStockAdjustmentHeader();

                invStockAdjustmentHeader = stockAdjustmentService.GetPausedInvStockAdjustmentHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (invStockAdjustmentHeader != null)
                {
                    cmbLocation.SelectedValue = invStockAdjustmentHeader.LocationID;
                    cmbLocation.Refresh();

                    documentState = invStockAdjustmentHeader.DocumentStatus;

                    dtpAdjustmentDate.Value = Common.FormatDate(invStockAdjustmentHeader.DocumentDate);

                    txtDocumentNo.Text = invStockAdjustmentHeader.DocumentNo;
                    txtTotalCostValue.Text = Common.ConvertDecimalToStringCurrency(invStockAdjustmentHeader.TotalCostValue);
                    txtTotalSellingValue.Text = Common.ConvertDecimalToStringCurrency(invStockAdjustmentHeader.TotalSellingtValue);
                    txtTotalQty.Text = Common.ConvertDecimalToStringQty(invStockAdjustmentHeader.TotalQty);
                    txtReference.Text = invStockAdjustmentHeader.ReferenceDocumentNo;
                    txtRemark.Text = invStockAdjustmentHeader.Remark;
                    txtNarration.Text = invStockAdjustmentHeader.Narration;

                    if (invStockAdjustmentHeader.StockAdjustmentMode.Equals(1))
                    {
                        RecallAdjustmentMode(1);
                    }
                    else if (invStockAdjustmentHeader.StockAdjustmentMode.Equals(2))
                    {
                        RecallAdjustmentMode(2);
                    }
                    else if (invStockAdjustmentHeader.StockAdjustmentMode.Equals(3))
                    {
                        RecallAdjustmentMode(3);
                    }

                    dgvItemDetails.DataSource = null;
                    invStockAdjustmentDetailTempList = stockAdjustmentService.getPausedInvStockAdjustmentDetailTemp(invStockAdjustmentHeader);
                    dgvItemDetails.DataSource = invStockAdjustmentDetailTempList;
                    dgvItemDetails.Refresh();

                    Common.EnableTextBox(false, txtNarration, txtReference, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation, cmbMode);

                    if (invStockAdjustmentHeader.DocumentStatus.Equals(0))
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

                        InvStockAdjustmentDetailTemp invStockAdjustmentTempDetail = new InvStockAdjustmentDetailTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        InvProductMasterService invProductMasterService = new InvProductMasterService();

                        invStockAdjustmentTempDetail.ProductID = invProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).InvProductMasterID;
                        invStockAdjustmentTempDetail.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                        InvStockAdjustmentService invStockAdjustmentService = new InvStockAdjustmentService();

                        dgvItemDetails.DataSource = null;
                        invStockAdjustmentDetailTempList = invStockAdjustmentService.GetDeleteStockAdjustmentDetailTemp(invStockAdjustmentDetailTempList, invStockAdjustmentTempDetail);
                        dgvItemDetails.DataSource = invStockAdjustmentDetailTempList;
                        dgvItemDetails.Refresh();

                        if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                        else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; } 

                        GetSummarizeFigures(invStockAdjustmentDetailTempList);
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
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            invReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
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

        private void btnTogDetails_Click(object sender, EventArgs e)
        {
            if (!cmbTogDocument.Text.Trim().Equals(string.Empty))
            {
                if (RecallTogDocument(cmbTogDocument.Text.Trim()))
                {   }
            }
        }

        private bool RecallTogDocument(string documentNo)
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;
                InvStockAdjustmentHeader invStockAdjustmentHeader = new InvStockAdjustmentHeader();
                InvTransferOfGoodsService invTransferOfGoodsService = new Service.InvTransferOfGoodsService();
                InvStockAdjustmentService invStockAdjustmentService = new Service.InvStockAdjustmentService();
                txtDocumentNo.Text = GetDocumentNo(true);

                dgvItemDetails.DataSource = null;
 
                invStockAdjustmentDetailTempList = invStockAdjustmentService.GetSavedTogByDocumentNumber(cmbTogDocument.Text.Trim());
                dgvItemDetails.DataSource = invStockAdjustmentDetailTempList;
                dgvItemDetails.Refresh();

                txtTotalQty.Text = Common.ConvertDecimalToStringQty(invStockAdjustmentDetailTempList.GetSummaryAmount(x => x.OrderQty));
                txtTotalCostValue.Text = Common.ConvertDecimalToStringQty(invStockAdjustmentDetailTempList.GetSummaryAmount(x => x.CostValue));
                txtTotalSellingValue.Text = Common.ConvertDecimalToStringQty(invStockAdjustmentDetailTempList.GetSummaryAmount(x => x.SellingValue));

                Common.EnableTextBox(false, txtDocumentNo);
                Common.EnableComboBox(false, cmbLocation);
                Common.EnableComboBox(true, cmbMode);
                if (invStockAdjustmentHeader.DocumentStatus.Equals(0))
                {
                    grpFooter.Enabled = true;
                    EnableLine(true);
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                }
                this.Cursor = Cursors.Default;
                return true;

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                this.Cursor = Cursors.Default;
                return false;
            }
        }

        private void cmbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                prgBar.Visible = true;
                prgBar.Value = 0;
                int index = 0;
                lstLayer.SmallImageList = imgList;
                int imgIndex = 0;

                if (cmbLayer.Text == "Department")
                {
                    imgIndex = 0;
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    var departmentList = invDepartmentService.GetDepartmentList();
                    lstLayer.Clear();

                    prgBar.Maximum = departmentList.Count - 1;
                    foreach (var item in departmentList)
                    {
                        lstLayer.Items.Add(item.DepartmentCodeAndName, imgIndex);
                        prgBar.Value = index;
                        index++;
                        imgIndex++;
                        if (imgIndex == 4) { imgIndex = 0; }
                    }
                }
                else if (cmbLayer.Text == "Lifestyle")
                {
                    imgIndex = 0;
                    InvCategoryService invCategoryService = new InvCategoryService();
                    var categoryList = invCategoryService.GetCategorytList();
                    lstLayer.Clear();

                    prgBar.Maximum = categoryList.Count;
                    foreach (var item in categoryList)
                    {
                        lstLayer.Items.Add(item.CategoryCodeAndName, imgIndex);
                        prgBar.Value = index;
                        index++;
                        imgIndex++;
                        if (imgIndex == 4) { imgIndex = 0; }
                    }
                }
                else if (cmbLayer.Text == "Product")
                {
                    imgIndex = 0;
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    var subCategoryList = invSubCategoryService.GetSubCategorytList();
                    lstLayer.Clear();

                    prgBar.Maximum = subCategoryList.Count;
                    foreach (var item in subCategoryList)
                    {
                        lstLayer.Items.Add(item.SubCategoryCodeAndName, imgIndex);
                        prgBar.Value = index;
                        index++;
                        imgIndex++;
                        if (imgIndex == 4) { imgIndex = 0; }
                    }
                }
                else if (cmbLayer.Text == "Brand")
                {
                    imgIndex = 0;
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    var subCategory2List = invSubCategory2Service.GetSubCategory2List();
                    lstLayer.Clear();

                    prgBar.Maximum = subCategory2List.Count;
                    foreach (var item in subCategory2List)
                    {
                        lstLayer.Items.Add(item.SubCategory2CodeAndName, imgIndex);
                        prgBar.Value = index;
                        index++;
                        imgIndex++;
                        if (imgIndex == 4) { imgIndex = 0; }
                    }
                }

                prgBar.Visible = false;
                lstLayer.Enabled = true;
                lstLayer.Refresh();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnEnterData_Click(object sender, EventArgs e)
        {
            try
            {
                this.pnlInfo.Visible = false;
            }
            catch (Exception ex)
            {
               Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
