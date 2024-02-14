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
using Report.Logistic;
using Report.Com;

namespace UI.Windows
{
    /// <summary>
    /// Developed By Nuwan
    /// </summary>
    public partial class FrmOpeningStock : UI.Windows.FrmBaseTransactionForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<OpeningStockDetailTemp> openingStockDetailTempList = new List<OpeningStockDetailTemp>();
        OpeningStockDetailTemp existingOpeningStockDetailTemp = new OpeningStockDetailTemp();

        List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

        List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();
        InvProductBatchNoTemp existingInvProductBatchNoTemp = new InvProductBatchNoTemp();
        List<LgsProductBatchNoTemp> lgsProductBatchNoTempList = new List<LgsProductBatchNoTemp>();
        LgsProductBatchNoTemp existingLgsProductBatchNoTemp = new LgsProductBatchNoTemp();

        InvProductMaster existingInvProductMaster;
        LgsProductMaster existingLgsProductMaster;

        bool recallDocument;
        int documentState;
        int documentID = 0;
        bool isInvProduct;
        bool isRecall;
        int type; // 1-->Inventry, 2-->Logistic
        int rowCount;
        int selectedRowIndex; 
        bool isUpdateGrid = false;

        public FrmOpeningStock()
        {
            InitializeComponent();
        }

        private void FrmOpeningStock_Load(object sender, EventArgs e)
        {

        }

        private void ShowDocumentNo()
        {
            txtDocumentNo.Text = GetDocumentNo(true);
        }
        public override void View()
        {
            //base.View();
            GenerateReport(txtDocumentNo.Text.Trim(), 1);

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

                //Load Opening Stock Type
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                Common.SetAutoBindRecords(cmbType, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.ModuleType).ToString()));

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                chkOverwrite.Checked = autoGenerateInfo.IsOverWriteQty;
                documentID = autoGenerateInfo.DocumentID;

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }

                isInvProduct = true;

                base.FormLoad();

                ////Load Document Numbers
                OpeningStockService openingStockService = new OpeningStockService();
                Common.SetAutoComplete(txtDocumentNo, openingStockService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
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
                grpFooter.Enabled = false;
                EnableLine(false);
                Common.EnableTextBox(false, txtProductCode, txtProductName);
                Common.EnableTextBox(true, txtReference, txtRemark, txtNarration, txtDocumentNo);
                Common.EnableComboBox(true, cmbLocation, cmbType);
                Common.EnableButton(false, btnSave, btnPause, btnView);

                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                openingStockDetailTempList = null;
                this.ActiveControl = txtReference;
                txtReference.Focus();
                cmbLocation.SelectedValue = Common.LoggedLocationID;

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                chkOverwrite.Checked = autoGenerateInfo.IsOverWriteQty;

                //cmbType.SelectedIndex = -1;
                cmbType.SelectedIndex = 0;

                lblScannedCode.Text = string.Empty;
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
                OpeningStockService openingStockService = new OpeningStockService();
                LocationService locationService = new LocationService();
                return openingStockService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo, isInvProduct).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }
        }

        private void LoadProducts(bool isInv)
        {
            if (isInv)
            {
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                Common.SetAutoComplete(txtProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
            }
            else
            {
                LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                Common.SetAutoComplete(txtProductCode, lgsProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                Common.SetAutoComplete(txtProductName, lgsProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
            }
        }

        private void EnableLine(bool enable)
        {
            Common.EnableTextBox(enable, txtQty, txtCostPrice, txtSellingPrice, txtQty, txtCostValue, txtSellingValue);
            Common.EnableComboBox(enable, cmbUnit);
            dtpExpiry.Enabled = enable;
        }


        private void GetOpeningStockType()
        {
            if (cmbType.Text.Equals("Inventory"))
            {
                isInvProduct = true;
                OpeningStockService.isInv = true;
                type = 1;
                ChkBarCodeScan.Checked = false;
                Common.EnableTextBox(false, txtProductCode, txtProductName);
            }
            else
            {
                isInvProduct = false;
                OpeningStockService.isInv = false;
                type = 2;
                LoadProducts(isInvProduct);
                ChkBarCodeScan.Checked = false;
                txtRemark.Focus();
                Common.EnableTextBox(false, txtProductCode, txtProductName);
            }
        }

        private void cmbType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                GetOpeningStockType();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void RefreshDocumentNumber()
        {
            OpeningStockService openingStockService = new OpeningStockService();
            Common.SetAutoComplete(txtDocumentNo, openingStockService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
        }

        private void txtReference_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    cmbType.Focus();
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
                    dtpOpeningStockDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbType_KeyDown(object sender, KeyEventArgs e)
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

        private void dtpOpeningStockDate_KeyDown(object sender, KeyEventArgs e)
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
                    cmbLocation.Focus();
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
                    if (ChkBarCodeScan.Checked)
                    {
                        Common.EnableTextBox(true, txtProductCode, txtProductName);
                        txtProductCode.AutoCompleteCustomSource = null;
                        txtProductName.AutoCompleteCustomSource = null;
                        //LoadProducts(isInvProduct);
                        txtProductCode.Focus();
                    }
                    else
                    {
                        Common.EnableTextBox(true, txtProductCode, txtProductName);
                        LoadProducts(isInvProduct);
                        txtProductCode.Focus();
                    }
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
                    if (!txtProductCode.Text.Trim().Equals(string.Empty))
                    {
                        txtProductName.Enabled = true;
                        txtProductName.Focus();
                    }
                    else
                    {
                        txtProductName.Focus();
                    }
                }
                else if (e.KeyCode.Equals(Keys.F3))
                {
                    if (isInvProduct)
                    {
                        SupplierService supplierService = new SupplierService();
                        InvProductMasterService invProductMasterService = new InvProductMasterService();
                        DataView dvAllReferenceData = new DataView(invProductMasterService.GetProductsDataTableForTransactions());
                        if (dvAllReferenceData.Count != 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtProductCode);
                            txtProductCode_Leave(this, e);
                        }
                    }
                    else
                    {
                        LgsSupplierService supplierService = new LgsSupplierService();
                        LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                        DataView dvAllReferenceData = new DataView(lgsProductMasterService.GetProductsDataTableForTransactions());
                        if (dvAllReferenceData.Count != 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtProductCode);
                            txtProductCode_Leave(this, e);
                        }
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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

        //private void UpdateBatchGrid(OpeningStockDetailTemp openingStockDetailTemp)
        //{
        //    try
        //    {
        //        string productCode = txtProductCode.Text.Trim();
        //        string unit = cmbUnit.Text.Trim();

        //        if (isInvProduct)
        //        {
        //            if (dgvItemDetails["ProductCode", 0].Value == null) { }
        //            else
        //            {
        //                for (int i = 0; i < dgvItemDetails.RowCount; i++)
        //                {
        //                    if (productCode.Equals(dgvItemDetails["ProductCode", i].Value.ToString()) && unit.Equals(dgvItemDetails["Unit", i].Value.ToString()))
        //                    {
        //                        decimal tot, qty;
        //                        qty = Convert.ToDecimal(dgvItemDetails["Qty", i].Value.ToString());
        //                        tot = qty + Convert.ToDecimal(txtQty.Text.Trim());
        //                        txtQty.Text = tot.ToString();
        //                    }
        //                }
        //            }

        //            openingStockDetailTemp.DocumentNo = txtDocumentNo.Text.Trim();
        //            openingStockDetailTemp.ProductID = openingStockDetailTemp.ProductID;
        //            openingStockDetailTemp.ProductCode = txtProductCode.Text.Trim();
        //            openingStockDetailTemp.ProductName = txtProductName.Text.Trim();

        //            if (cmbUnit.SelectedValue != null) { openingStockDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()); }
        //            else { openingStockDetailTemp.UnitOfMeasureID = 0; }

        //            openingStockDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();
        //            openingStockDetailTemp.BaseUnitID = existingInvProductMaster.UnitOfMeasureID;
        //            openingStockDetailTemp.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text);

        //            CalculateLine();

        //            openingStockDetailTemp.ExpiryDate = Common.FormatDate(dtpExpiry.Value);
        //            openingStockDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text);
        //            openingStockDetailTemp.SellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text);

        //            openingStockDetailTemp.CostValue = Common.ConvertStringToDecimalCurrency(txtCostValue.Text);
        //            openingStockDetailTemp.SellingValue = Common.ConvertStringToDecimalCurrency(txtSellingValue.Text);

        //            OpeningStockService openingStockService = new OpeningStockService();

        //            dgvItemDetails.DataSource = null;
        //            openingStockDetailTempList = openingStockService.GetUpdateInvOpeningStockDetailTempForBarcodeScan(openingStockDetailTempList, openingStockDetailTemp);
        //            dgvItemDetails.AutoGenerateColumns = false;
        //            dgvItemDetails.DataSource = openingStockDetailTempList;
        //            dgvItemDetails.Refresh();

        //            GetSummarizeFigures(openingStockDetailTempList);
        //            EnableLine(false);
        //            Common.EnableTextBox(false, txtDocumentNo);
        //            Common.EnableComboBox(false, cmbLocation, cmbType);
        //            if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
        //            if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
        //            ClearLine();

        //            if (openingStockDetailTempList.Count > 0)
        //                grpFooter.Enabled = true;

        //            txtProductCode.Enabled = true;
        //            txtProductCode.Focus();
        //        }
        //        else
        //        {
        //            if (dgvItemDetails["ProductCode", 0].Value == null) { }
        //            else
        //            {
        //                for (int i = 0; i < dgvItemDetails.RowCount; i++)
        //                {
        //                    if (productCode.Equals(dgvItemDetails["ProductCode", i].Value.ToString()) && unit.Equals(dgvItemDetails["Unit", i].Value.ToString()))
        //                    {
        //                        decimal tot, qty;
        //                        qty = Convert.ToDecimal(dgvItemDetails["Qty", i].Value.ToString());
        //                        tot = qty + Convert.ToDecimal(txtQty.Text.Trim());
        //                        txtQty.Text = tot.ToString();
        //                    }
        //                }
        //            }

        //            openingStockDetailTemp.DocumentNo = txtDocumentNo.Text.Trim();
        //            openingStockDetailTemp.ProductID = openingStockDetailTemp.ProductID;
        //            openingStockDetailTemp.ProductCode = txtProductCode.Text.Trim();
        //            openingStockDetailTemp.ProductName = txtProductName.Text.Trim();

        //            if (cmbUnit.SelectedValue != null) { openingStockDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()); }
        //            else { openingStockDetailTemp.UnitOfMeasureID = 0; }

        //            openingStockDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();
        //            openingStockDetailTemp.BaseUnitID = existingInvProductMaster.UnitOfMeasureID;
        //            openingStockDetailTemp.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text);

        //            CalculateLine();

        //            openingStockDetailTemp.ExpiryDate = Common.FormatDate(dtpExpiry.Value);
        //            openingStockDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text);
        //            openingStockDetailTemp.SellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text);

        //            openingStockDetailTemp.CostValue = Common.ConvertStringToDecimalCurrency(txtCostValue.Text);
        //            openingStockDetailTemp.SellingValue = Common.ConvertStringToDecimalCurrency(txtSellingValue.Text);

        //            OpeningStockService openingStockService = new OpeningStockService();

        //            dgvItemDetails.DataSource = null;
        //            openingStockDetailTempList = openingStockService.GetUpdateLgsOpeningStockDetailTempForBarcodeScan(openingStockDetailTempList, openingStockDetailTemp);
        //            dgvItemDetails.AutoGenerateColumns = false;
        //            dgvItemDetails.DataSource = openingStockDetailTempList;
        //            dgvItemDetails.Refresh();

        //            GetSummarizeFigures(openingStockDetailTempList);
        //            EnableLine(false);
        //            Common.EnableTextBox(false, txtDocumentNo);
        //            Common.EnableComboBox(false, cmbLocation, cmbType);
        //            if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
        //            if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
        //            ClearLine();

        //            if (openingStockDetailTempList.Count > 0)
        //                grpFooter.Enabled = true;

        //            txtProductCode.Enabled = true;
        //            txtProductCode.Focus();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
        //    }
        //}

        private void txtProductCode_Leave(object sender, EventArgs e)
        {
            int indexOf = 0;
            string fillter = "";

            if (string.IsNullOrEmpty(txtProductCode.Text.Trim())) { return; }

            if (ValidateComboBoxes().Equals(false)) 
            {
                txtProductCode.SelectAll();
                txtProductCode.Focus();
                return; 
            }

            if (ChkBarCodeScan.Checked)
            {
                indexOf = txtProductCode.Text.Trim().IndexOf("*");
                
                if (indexOf > 0)
                {
                    fillter = txtProductCode.Text.Trim().Substring(indexOf + 1, txtProductCode.Text.Trim().Length - (indexOf + 1));
                    lblScannedCode.Text = fillter;
                    txtQty.Text = txtProductCode.Text.Trim().Substring(0, indexOf);
                }
                else
                {
                    txtQty.Text = "1";
                    fillter =  txtProductCode.Text.Trim();
                    lblScannedCode.Text = fillter;
                }

                if (loadBarcodeDetails(fillter, 0))
                {
                    UpdateGrid(existingOpeningStockDetailTemp);
                }
                else
                {
                    Toast.Show("Invalid barcode", Toast.messageType.Information, Toast.messageAction.General);
                    txtProductCode.Focus();
                    return;
                }
            }
            else
            {
                loadProductDetails(true, txtProductCode.Text.Trim(), 0);
                lblScannedCode.Text = txtProductCode.Text.Trim();
            }
        }


        private bool loadBarcodeDetails(string strProduct, long unitofMeasureID) 
        {
            try
            {
                if (string.IsNullOrEmpty(strProduct)) { return false; }
                if (isInvProduct)
                {
                    existingInvProductMaster = new InvProductMaster();
                    InvProductMasterService invProductMasterService = new InvProductMasterService();

                    existingInvProductMaster = invProductMasterService.GetProductsByRefCodes(strProduct);

                    if (existingInvProductMaster != null)
                    {
                        OpeningStockService OpeningStockService = new OpeningStockService();
                        if (openingStockDetailTempList == null)
                            openingStockDetailTempList = new List<OpeningStockDetailTemp>();
                        existingOpeningStockDetailTemp = OpeningStockService.getOpeningStockDetailTemp(openingStockDetailTempList, existingLgsProductMaster, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID, isInvProduct);
                        if (existingOpeningStockDetailTemp != null)
                        {
                            txtProductCode.Text = existingOpeningStockDetailTemp.ProductCode;
                            txtProductName.Text = existingOpeningStockDetailTemp.ProductName;
                            cmbUnit.SelectedValue = existingOpeningStockDetailTemp.UnitOfMeasureID;
                            if (isRecall)
                            {

                                dtpExpiry.Value = existingOpeningStockDetailTemp.ExpiryDate.Value;
                            }
                            //txtQty.Text = Common.ConvertDecimalToStringQty(existingOpeningStockDetailTemp.OrderQty);
                            txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingOpeningStockDetailTemp.CostPrice);
                            txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingOpeningStockDetailTemp.SellingPrice);
                            Common.EnableComboBox(true, cmbUnit);
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
                else
                {
                    existingLgsProductMaster = new LgsProductMaster();
                    LgsProductMasterService LgsProductMasterService = new LgsProductMasterService();

                    existingLgsProductMaster = LgsProductMasterService.GetProductsByRefCodes(strProduct);

                    if (existingLgsProductMaster != null)
                    {
                        OpeningStockService OpeningStockService = new OpeningStockService();
                        if (openingStockDetailTempList == null)
                            openingStockDetailTempList = new List<OpeningStockDetailTemp>();
                        existingOpeningStockDetailTemp = OpeningStockService.getOpeningStockDetailTemp(openingStockDetailTempList, existingLgsProductMaster, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID, isInvProduct);
                        if (existingOpeningStockDetailTemp != null)
                        {
                            txtProductCode.Text = existingOpeningStockDetailTemp.ProductCode;
                            txtProductName.Text = existingOpeningStockDetailTemp.ProductName;
                            cmbUnit.SelectedValue = existingOpeningStockDetailTemp.UnitOfMeasureID;
                            if (isRecall)
                            {
                                //txtBatchNo.Text = existingOpeningStockDetailTemp.BatchNo;
                                dtpExpiry.Value = existingOpeningStockDetailTemp.ExpiryDate.Value;
                            }
                            //txtQty.Text = Common.ConvertDecimalToStringQty(existingOpeningStockDetailTemp.OrderQty);
                            txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingOpeningStockDetailTemp.CostPrice);
                            txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingOpeningStockDetailTemp.SellingPrice);
                            Common.EnableComboBox(true, cmbUnit);
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
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return false;
            }
        }

       

        private void loadProductDetails(bool isCode, string strProduct, long unitofMeasureID)
        {
            try
            {
                if (isInvProduct)
                {
                    existingInvProductMaster = new InvProductMaster();

                    if (strProduct.Equals(string.Empty))
                        return;

                    InvProductMasterService invProductMasterService = new InvProductMasterService();

                    if (isCode)
                    {
                        existingInvProductMaster = invProductMasterService.GetProductsByRefCodes(strProduct);
                        if (isCode && strProduct.Equals(string.Empty))
                        {
                            txtProductName.Focus();
                            return;
                        }
                    }
                    else
                        existingInvProductMaster = invProductMasterService.GetProductsByName(strProduct); ;

                    if (existingInvProductMaster != null)
                    {
                        OpeningStockService OpeningStockService = new OpeningStockService();
                        if (openingStockDetailTempList == null)
                            openingStockDetailTempList = new List<OpeningStockDetailTemp>();
                        existingOpeningStockDetailTemp = OpeningStockService.getOpeningStockDetailTemp(openingStockDetailTempList, existingLgsProductMaster, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID, isInvProduct);
                        if (existingOpeningStockDetailTemp != null)
                        {
                            txtProductCode.Text = existingOpeningStockDetailTemp.ProductCode;
                            txtProductName.Text = existingOpeningStockDetailTemp.ProductName;
                            cmbUnit.SelectedValue = existingOpeningStockDetailTemp.UnitOfMeasureID;
                            if (isRecall)
                            {
                                
                                dtpExpiry.Value = existingOpeningStockDetailTemp.ExpiryDate.Value;
                            }
                            txtQty.Text = Common.ConvertDecimalToStringQty(existingOpeningStockDetailTemp.OrderQty);
                            txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingOpeningStockDetailTemp.CostPrice);
                            txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingOpeningStockDetailTemp.SellingPrice);
                            Common.EnableComboBox(true, cmbUnit);
                            if (unitofMeasureID.Equals(0))
                                cmbUnit.Focus();
                        }
                    }
                    else
                    {
                        Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                        return;
                    }
                }
                else
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
                        OpeningStockService OpeningStockService = new OpeningStockService();
                        if (openingStockDetailTempList == null)
                            openingStockDetailTempList = new List<OpeningStockDetailTemp>();
                        existingOpeningStockDetailTemp = OpeningStockService.getOpeningStockDetailTemp(openingStockDetailTempList, existingLgsProductMaster, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID, isInvProduct);
                        if (existingOpeningStockDetailTemp != null)
                        {
                            txtProductCode.Text = existingOpeningStockDetailTemp.ProductCode;
                            txtProductName.Text = existingOpeningStockDetailTemp.ProductName;
                            cmbUnit.SelectedValue = existingOpeningStockDetailTemp.UnitOfMeasureID;
                            if (isRecall)
                            {
                                //txtBatchNo.Text = existingOpeningStockDetailTemp.BatchNo;
                                dtpExpiry.Value = existingOpeningStockDetailTemp.ExpiryDate.Value;
                            }
                            txtQty.Text = Common.ConvertDecimalToStringQty(existingOpeningStockDetailTemp.OrderQty);
                            txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingOpeningStockDetailTemp.CostPrice);
                            txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingOpeningStockDetailTemp.SellingPrice);
                            Common.EnableComboBox(true, cmbUnit);
                            if (unitofMeasureID.Equals(0))
                                cmbUnit.Focus();
                        }
                    }
                    else
                    {
                        Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                        return;
                    }
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
                else if (e.KeyCode.Equals(Keys.F3))
                {
                    if (isInvProduct)
                    {
                        SupplierService supplierService = new SupplierService();
                        InvProductMasterService invProductMasterService = new InvProductMasterService();
                        DataView dvAllReferenceData = new DataView(invProductMasterService.GetProductsDataTableForTransactions());
                        if (dvAllReferenceData.Count != 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtProductCode);
                            txtProductCode_Leave(this, e);
                        }
                    }
                    else
                    {
                        LgsSupplierService supplierService = new LgsSupplierService();
                        LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                        DataView dvAllReferenceData = new DataView(lgsProductMasterService.GetProductsDataTableForTransactions());
                        if (dvAllReferenceData.Count != 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtProductCode);
                            txtProductCode_Leave(this, e);
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
                loadProductDetails(false, txtProductName.Text.Trim(), 0);
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

        private void cmbUnit_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbUnit.SelectedValue == null)
                    return;
                if (isInvProduct)
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
                    loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()));

                    //if (existingInvProductMaster.IsBatch.Equals(true))
                    //{
                    //    txtBatchNo.Enabled = true;
                    //    txtBatchNo.Focus();
                    //}
                    //else 
                    if (existingInvProductMaster.IsExpiry.Equals(true))
                    {
                        dtpExpiry.Enabled = true;
                        dtpExpiry.Focus();
                    }
                    //else if (existingInvProductMaster.IsSerial.Equals(true))
                    //{
                    //    txtQty.Enabled = true;
                    //    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                    //        txtQty.Text = "1";
                    //    txtQty.Focus();
                    //}
                    else
                    {
                        txtQty.Enabled = true;
                        if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                            txtQty.Text = "1";
                        txtQty.Focus();
                    }
                }
                else
                {
                    if (!existingLgsProductMaster.UnitOfMeasureID.Equals(Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())))
                    {
                        LgsProductUnitConversionService lgsProductUnitConversionService = new LgsProductUnitConversionService();
                        if (lgsProductUnitConversionService.GetProductUnitByProductCode(existingLgsProductMaster.LgsProductMasterID, Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())) == null)
                        {
                            Toast.Show("Unit - " + cmbUnit.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "Product - " + txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "");
                            dtpExpiry.Enabled = false;
                            cmbUnit.SelectedValue = existingInvProductMaster.UnitOfMeasureID;
                            cmbUnit.Focus();
                            return;
                        }
                    }
                    loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()));

                    //if (existingLgsProductMaster.IsBatch.Equals(true))
                    //{
                    //    txtBatchNo.Enabled = true;
                    //    txtBatchNo.Focus();
                    //}
                    //else 
                    if (existingLgsProductMaster.IsExpiry.Equals(true))
                    {
                        dtpExpiry.Enabled = true;
                        dtpExpiry.Focus();
                    }
                    //else if (existingLgsProductMaster.IsSerial.Equals(true))
                    //{
                    //    txtQty.Enabled = true;
                    //    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                    //        txtQty.Text = "1";
                    //    txtQty.Focus();
                    //}
                    else
                    {
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

        

        private void dtpExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtProductName.Text.Trim().Equals(string.Empty))
                    {
                        //if (isInvProduct)
                        //{
                        //    txtQty.Enabled = true;
                        //    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                        //        txtQty.Text = "1";
                        //    txtQty.Focus();
                        //}
                        //else
                        //{
                            txtQty.Enabled = true;
                            if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                                txtQty.Text = "1";
                            txtQty.Focus();
                        //}
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
                    if ((Common.ConvertStringToDecimal(txtQty.Text.Trim())) > 0)
                    {
                        if (isInvProduct)
                        {
                            if (existingInvProductMaster.IsSerial)
                            {
                                InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();
                                invProductSerialNoTemp.DocumentID = documentID;
                                invProductSerialNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                                invProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                            
                                if (existingInvProductMaster.IsExpiry.Equals(true))
                                    invProductSerialNoTemp.ExpiryDate = Common.ConvertStringToDate(dtpExpiry.Value.ToString());

                                if (invProductSerialNoTempList == null)
                                    invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                                OpeningStockService openingStockService = new OpeningStockService();

                                if (openingStockService.IsValidNoOfInvSerialNo(invProductSerialNoTempList, invProductSerialNoTemp,Common.ConvertStringToDecimalQty(txtQty.Text.Trim())))
                                {
                                    txtSellingValue.Enabled = true;
                                    txtSellingValue.Focus();
                                    CalculateLine();
                                }
                                else
                                {
                                    FrmSerial frmSerial = new FrmSerial(invProductSerialNoTempList, invProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim()) ), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), FrmSerial.transactionType.OpeningStock);
                                    frmSerial.ShowDialog();
                                }
                            }
                            else
                            {
                                txtSellingValue.Enabled = true;
                                txtSellingValue.Focus();
                                CalculateLine();
                            }
                        }
                        else
                        {
                            if (existingLgsProductMaster.IsSerial)
                            {
                                InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();
                                invProductSerialNoTemp.DocumentID = documentID;
                                invProductSerialNoTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                                invProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                           
                                if (existingLgsProductMaster.IsExpiry.Equals(true))
                                    invProductSerialNoTemp.ExpiryDate = Common.ConvertStringToDate(dtpExpiry.Value.ToString());

                                if (invProductSerialNoTempList == null)
                                    invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                                OpeningStockService openingStockService = new OpeningStockService();

                                if (openingStockService.IsValidNoOfInvSerialNo(invProductSerialNoTempList, invProductSerialNoTemp, Common.ConvertStringToDecimalQty(txtQty.Text.Trim())))
                                {
                                    txtSellingValue.Enabled = true;
                                    txtSellingValue.Focus();
                                    CalculateLine();
                                }
                                else
                                {
                                    FrmSerial frmSerial = new FrmSerial(invProductSerialNoTempList, invProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), FrmSerial.transactionType.OpeningStock);
                                    frmSerial.ShowDialog();
                                }
                            }
                            else
                            {
                                txtSellingValue.Enabled = true;
                                txtSellingValue.Focus();
                                CalculateLine();
                            }
                        }
                    }

                }

                else if (e.KeyCode.Equals(Keys.F3))
                {
                    if (isInvProduct)
                    {
                        if (existingInvProductMaster.IsSerial)
                        {
                            InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();
                            invProductSerialNoTemp.DocumentID = documentID;
                            if (existingInvProductMaster.IsExpiry.Equals(true))
                                invProductSerialNoTemp.ExpiryDate = Common.ConvertStringToDate(dtpExpiry.Value.ToString());
                            invProductSerialNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                            invProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                            if (invProductSerialNoTempList == null)
                                invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                            FrmSerial frmSerial = new FrmSerial(invProductSerialNoTempList, invProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), FrmSerial.transactionType.GoodReceivedNote);
                            frmSerial.ShowDialog();
                        }
                    }
                    else
                    {
                        if (existingLgsProductMaster.IsSerial)
                        {
                            InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();
                            invProductSerialNoTemp.DocumentID = documentID;
                            if (existingLgsProductMaster.IsExpiry.Equals(true))
                                invProductSerialNoTemp.ExpiryDate = Common.ConvertStringToDate(dtpExpiry.Value.ToString());
                            invProductSerialNoTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                            invProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                            if (invProductSerialNoTempList == null)
                                invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                            FrmSerial frmSerial = new FrmSerial(invProductSerialNoTempList, invProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), FrmSerial.transactionType.GoodReceivedNote);
                            frmSerial.ShowDialog();
                        }
                    }
            }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void SetSerialNoList(List<InvProductSerialNoTemp> setInvProductSerialNoTemp, bool isValidNoOfSerialNo)
        {
            invProductSerialNoTempList = setInvProductSerialNoTemp;
            if (isValidNoOfSerialNo)
            {
                txtQty.Enabled = true;
                txtQty.Focus();
            }
            else
            {
                txtQty.Focus();
            }
        }

        public void SetInvSerialNoList(List<InvProductSerialNoTemp> setInvProductSerialNoTemp)
        {
            invProductSerialNoTempList = setInvProductSerialNoTemp;
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
                    UpdateGrid(existingOpeningStockDetailTemp);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void UpdateGrid(OpeningStockDetailTemp openingStockDetailTemp)
        {
            try
            {
                decimal qty = 0;

                if (Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) > 0)
                {
                    if (isInvProduct)
                    {
                        openingStockDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                        if (isInvProduct) { openingStockDetailTemp.BaseUnitID = existingInvProductMaster.UnitOfMeasureID; }
                        else { openingStockDetailTemp.BaseUnitID = existingInvProductMaster.UnitOfMeasureID; }

                        openingStockDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();

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
                            qty = (openingStockDetailTemp.OrderQty + Common.ConvertStringToDecimalQty(txtQty.Text));
                        }

                        if (!chkOverwrite.Checked)
                        {
                            CalculateLine(qty);
                            openingStockDetailTemp.OrderQty = Common.ConvertDecimalToDecimalQty(qty);
                        }
                        else
                        {
                            CalculateLine();
                            openingStockDetailTemp.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text.Trim());

                        }                        
                        
                        openingStockDetailTemp.ExpiryDate = Common.ConvertDateTimeToDate(dtpExpiry.Value);
                        openingStockDetailTemp.DocumentNo = txtDocumentNo.Text.Trim();

                        openingStockDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text);
                        openingStockDetailTemp.CostValue = Common.ConvertStringToDecimalCurrency(txtCostValue.Text);
                        openingStockDetailTemp.SellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text);
                        
                        openingStockDetailTemp.SellingValue = Common.ConvertStringToDecimalCurrency(txtSellingValue.Text);

                        OpeningStockService openingStockService = new OpeningStockService();

                        dgvItemDetails.ClearSelection();
                        dgvItemDetails.DataSource = null;
                        openingStockDetailTempList = openingStockService.GetUpdateOpeningStockDetailTemp(openingStockDetailTempList, openingStockDetailTemp);
                        dgvItemDetails.DataSource = openingStockDetailTempList;
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

                        GetSummarizeFigures(openingStockDetailTempList);
                        EnableLine(false);
                        Common.EnableTextBox(false, txtDocumentNo);
                        Common.EnableComboBox(false, cmbLocation);
                        if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave); ;
                        ClearLine();

                        if (openingStockDetailTempList.Count > 0)
                            grpFooter.Enabled = true;

                        txtProductName.Enabled = true;
                        txtProductCode.Enabled = true;
                        txtProductCode.Focus();


                    }
                    else
                    {
                        openingStockDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                        if (isInvProduct) { openingStockDetailTemp.BaseUnitID = existingLgsProductMaster.UnitOfMeasureID; }
                        else { openingStockDetailTemp.BaseUnitID = existingLgsProductMaster.UnitOfMeasureID; }

                        openingStockDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();
                        if (!chkOverwrite.Checked)
                            txtQty.Text = (openingStockDetailTemp.OrderQty + Common.ConvertStringToDecimalQty(txtQty.Text)).ToString();

                        openingStockDetailTemp.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text);
                        CalculateLine();

                    
                        openingStockDetailTemp.ExpiryDate = dtpExpiry.Value;
                        openingStockDetailTemp.DocumentNo = txtDocumentNo.Text.Trim();

                        openingStockDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text);
                        openingStockDetailTemp.CostValue = Common.ConvertStringToDecimalCurrency(txtCostValue.Text);
                        openingStockDetailTemp.SellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text);
                        openingStockDetailTemp.SellingValue = Common.ConvertStringToDecimalCurrency(txtSellingValue.Text);

                        OpeningStockService openingStockService = new OpeningStockService();

                        dgvItemDetails.ClearSelection();
                        dgvItemDetails.DataSource = null;
                        openingStockDetailTempList = openingStockService.GetUpdateOpeningStockDetailTemp(openingStockDetailTempList, openingStockDetailTemp);
                        dgvItemDetails.DataSource = openingStockDetailTempList;
                        dgvItemDetails.Refresh();

                        //rowCount = dgvItemDetails.Rows.Count;
                        //dgvItemDetails.CurrentCell = dgvItemDetails.Rows[rowCount - 1].Cells[0];

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

                        GetSummarizeFigures(openingStockDetailTempList);
                        EnableLine(false);
                        Common.EnableTextBox(false, txtDocumentNo);
                        Common.EnableComboBox(false, cmbLocation, cmbType);
                        if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        ClearLine();

                        if (openingStockDetailTempList.Count > 0)
                            grpFooter.Enabled = true;

                        txtProductCode.Enabled = true;
                        txtProductCode.Focus();
                    }
                }
                else
                {
                    Toast.Show("Invalid Qty", Toast.messageType.Information, Toast.messageAction.General);
                    txtProductCode.SelectAll();
                    txtProductCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void GetSummarizeFigures(List<OpeningStockDetailTemp> listItem)
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
            Common.ClearTextBox(txtProductCode, txtProductName, txtQty, txtCostPrice, txtSellingPrice, txtCostValue, txtSellingValue);
            Common.ClearComboBox(cmbUnit);
            dtpExpiry.Value = DateTime.Now;
            txtProductCode.Focus();
        }

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo);
        }

        //public override void Pause()
        //{
        //    if ((Toast.Show("Opening Stock  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
        //    {
        //        if (ValidateControls().Equals(false)) { return; }
        //        if (SaveDocument(0, txtDocumentNo.Text.Trim()).Equals(true))
        //        {
        //            Toast.Show("Opening Stock  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
        //            ClearForm();
        //            RefreshDocumentNumber();
        //        }
        //        else
        //        {
        //            Toast.Show("Opening Stock  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
        //            return;
        //        }
        //    }
        //}

        //public override void Save()
        //{
        //    if ((Toast.Show("Opening Stock  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
        //    {
        //        if (ValidateControls().Equals(false)) { return; }
        //        bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim());
        //        if (saveDocument.Equals(true))
        //        {
        //            Toast.Show("Opening Stock  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
        //            ClearForm();
        //            RefreshDocumentNumber();
        //        }
        //        else
        //        {
        //            Toast.Show("Opening Stock  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
        //            return;
        //        }
        //    }
        //}

        public override void Pause()
        {
            if (ValidateTextBoxes().Equals(false)) { return; }
            if (ValidateComboBoxes().Equals(false)) { return; }

            if (isInvProduct)
            {
                if ((Toast.Show("Opening stock Type : Inventry\nDo you want to Continue ?", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.No))) { return; }
            }
            else
            {
                if ((Toast.Show("Opening stock Type : Logistic\nDo you want to Continue ?", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.No))) { return; }
            }

            if ((Toast.Show("Opening Stock  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {

                this.Cursor = Cursors.WaitCursor;
                string NewDocumentNo;
                bool saveDocument = SaveDocument(0, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                this.Cursor = Cursors.Default;
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Opening Stock  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 0);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Opening Stock  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        private bool ValidateTextBoxes()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo);
        }

        private bool ValidateComboBoxes()
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbType, cmbLocation);
        }

        public override void Save()
        {
            if (ValidateTextBoxes().Equals(false)) { return; }
            if (ValidateComboBoxes().Equals(false)) { return; }

            if (isInvProduct)
            {
                if ((Toast.Show("Opening stock Type : Inventry\nDo you want to Continue ?", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.No))) { return; }
            }
            else
            {
                if ((Toast.Show("Opening stock Type : Logistic\nDo you want to Continue ?", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.No))) { return; }
            }

            if ((Toast.Show("Opening Stock  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                this.Cursor = Cursors.WaitCursor;
                string NewDocumentNo;
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                this.Cursor = Cursors.Default;
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Opening Stock  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 1);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Opening Stock  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        private bool SaveDocument(int documentStatus, string documentNo, out string newDocumentNo)
        {
            try
            {
                OpeningStockService openingStockService = new OpeningStockService();
                OpeningStockHeader openingStockHeader = new OpeningStockHeader();
                LocationService locationService = new LocationService();
                Location Location = new Location();

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                openingStockHeader = openingStockService.GetPausedOpeningStockHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (openingStockHeader == null)
                    openingStockHeader = new OpeningStockHeader();

                //if (documentStatus.Equals(1)) // update paused document
                //{
                //    documentNo = GetDocumentNo(false);
                //    txtDocumentNo.Text = documentNo;
                //}

                //sampleOutHeader.SampleOutHeaderID = sampleOutHeader.SampleHeaderID;
                openingStockHeader.CompanyID = Location.CompanyID;
                openingStockHeader.DocumentDate = dtpOpeningStockDate.Value;
                openingStockHeader.DocumentID = documentID;
                openingStockHeader.DocumentStatus = documentStatus;
                openingStockHeader.DocumentNo = documentNo.Trim();
                openingStockHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                openingStockHeader.LocationID = Location.LocationID;
                openingStockHeader.Narration = txtNarration.Text.Trim();
                openingStockHeader.TotalQty = Common.ConvertStringToDecimalQty(txtTotalQty.Text.ToString());
                openingStockHeader.TotalCostValue = Common.ConvertStringToDecimalCurrency(txtTotalCostValue.Text.Trim());
                openingStockHeader.TotalSellingtValue = Common.ConvertStringToDecimalCurrency(txtTotalSellingValue.Text.Trim());
                openingStockHeader.ReferenceDocumentNo = txtReference.Text.Trim();
                openingStockHeader.Remark = txtRemark.Text.Trim();
                openingStockHeader.OpeningStockType = type;

                if (openingStockDetailTempList == null)
                    openingStockDetailTempList = new List<OpeningStockDetailTemp>();

                if (invProductSerialNoTempList == null)
                    invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                return openingStockService.Save(openingStockHeader, openingStockDetailTempList, invProductSerialNoTempList, out newDocumentNo, this.Name);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                newDocumentNo = string.Empty;
                this.Cursor = Cursors.Default;

                return false;
            }

        }

        private void RecallOpeningStockType(int type)
        {
            if (type.Equals(1))
            {
                cmbType.SelectedIndex = 0;
            }
            else if (type.Equals(2))
            {
                cmbType.SelectedIndex = 1;
            }
        }

        private bool RecallDocument(string documentNo)
        {
            try
            {
                OpeningStockService openingStockService = new OpeningStockService();
                OpeningStockHeader openingStockHeader = new OpeningStockHeader();

                openingStockHeader = openingStockService.GetPausedOpeningStockHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (openingStockHeader != null)
                {
                    isRecall = true;
                    documentState = openingStockHeader.DocumentStatus;
                    cmbLocation.SelectedValue = openingStockHeader.LocationID;
                    cmbLocation.Refresh();

                    dtpOpeningStockDate.Value = Common.FormatDate(openingStockHeader.DocumentDate);

                    txtDocumentNo.Text = openingStockHeader.DocumentNo;
                    txtTotalCostValue.Text = Common.ConvertDecimalToStringCurrency(openingStockHeader.TotalCostValue);
                    txtTotalSellingValue.Text = Common.ConvertDecimalToStringCurrency(openingStockHeader.TotalSellingtValue);
                    txtTotalQty.Text = Common.ConvertDecimalToStringQty(openingStockHeader.TotalQty);
                    txtReference.Text = openingStockHeader.ReferenceDocumentNo;
                    txtRemark.Text = openingStockHeader.Remark;
                    txtNarration.Text = openingStockHeader.Narration;

                    if (openingStockHeader.OpeningStockType.Equals(1))
                    {
                        RecallOpeningStockType(1);
                    }
                    else if (openingStockHeader.OpeningStockType.Equals(2))
                    {
                        RecallOpeningStockType(2);
                    }
                    
                    dgvItemDetails.DataSource = null;
                    openingStockDetailTempList = openingStockService.getPausedOpeningStockDetail(openingStockHeader, isInvProduct);
                    dgvItemDetails.DataSource = openingStockDetailTempList;
                    dgvItemDetails.Refresh();

                    invProductSerialNoTempList = openingStockService.GetPausedPurchaseSerialNoDetail(openingStockHeader, isInvProduct);

                    Common.EnableTextBox(false, txtNarration, txtReference, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation, cmbType);

                    if (openingStockHeader.DocumentStatus.Equals(0))
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

        private void txtDocumentNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                {
                    RecallDocument(txtDocumentNo.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void ClearForm()
        {
            isRecall = false;
            errorProvider.Clear();
            dtpOpeningStockDate.Value = DateTime.Now;
            dtpExpiry.Value = DateTime.Now;
            dgvItemDetails.DataSource = null;
            dgvItemDetails.Refresh();

            Common.EnableButton(true, btnDocumentDetails, btnPosDetails, btnLoadPosDocuments);
            Common.EnableComboBox(true, cmbPosDocument);
            Common.ClearComboBox(cmbPosDocument);
            
            base.ClearForm();
        }

        private void dgvItemDetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                isUpdateGrid = true;

                if (dgvItemDetails.CurrentCell.RowIndex >= 0)
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
                        loadProductDetails(true, dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim(), unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID);
                        CalculateLine();
                        if (documentState.Equals(1))
                        {
                            EnableLine(false);
                            txtProductCode.Enabled = false;
                            txtProductName.Enabled = false;
                        }
                        else
                        {
                            EnableLine(false);
                            txtProductCode.Enabled = false;
                            txtProductName.Enabled = false;

                            txtQty.Enabled = true;
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

        private void chkAutoCompleationDocumentNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                OpeningStockService openingStockService = new OpeningStockService();
                Common.SetAutoComplete(txtDocumentNo, openingStockService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                        if (isInvProduct)
                        {
                            if (Toast.Show("Product " + dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + " - " + dgvItemDetails["ProductName", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                                return;

                            OpeningStockDetailTemp OpeningStockTempDetail = new OpeningStockDetailTemp();
                            UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                            InvProductMasterService invProductMasterService = new InvProductMasterService();

                            OpeningStockTempDetail.ProductID = invProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).InvProductMasterID;
                            OpeningStockTempDetail.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                            OpeningStockService openingStockService = new OpeningStockService();

                            dgvItemDetails.DataSource = null;
                            openingStockDetailTempList = openingStockService.GetDeleteOpeningStockDetailTemp(openingStockDetailTempList, OpeningStockTempDetail);
                            dgvItemDetails.DataSource = openingStockDetailTempList;
                            dgvItemDetails.Refresh();

                            if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                            else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }

                            GetSummarizeFigures(openingStockDetailTempList);
                            this.ActiveControl = txtProductCode;
                            txtProductCode.Focus();
                        }
                        else
                        {
                            if (Toast.Show("Product " + dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + " - " + dgvItemDetails["ProductName", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                                return;

                            OpeningStockDetailTemp OpeningStockTempDetail = new OpeningStockDetailTemp();
                            UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                            OpeningStockTempDetail.ProductID = lgsProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).LgsProductMasterID;
                            OpeningStockTempDetail.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                            OpeningStockService openingStockService = new OpeningStockService();

                            dgvItemDetails.DataSource = null;
                            openingStockDetailTempList = openingStockService.GetDeleteOpeningStockDetailTemp(openingStockDetailTempList, OpeningStockTempDetail);
                            dgvItemDetails.DataSource = openingStockDetailTempList;
                            dgvItemDetails.Refresh();

                            dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0];

                            GetSummarizeFigures(openingStockDetailTempList);
                            this.ActiveControl = txtProductCode;
                            txtProductCode.Focus();
                        }
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
            if (cmbType.SelectedIndex.Equals(0))
            {
                //InvReportGenerator invReportGenerator = new InvReportGenerator();
                //AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                //autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                //invReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), 1);

                ComReportGenerator comReportGenerator = new ComReportGenerator();
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                comReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(),documentStatus);
            }
            if (cmbType.SelectedIndex.Equals(1))
            {
                LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                lgsReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), 1);
            }
        }

        private void dgvItemDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnLoadPosDocuments_Click(object sender, EventArgs e)
        {

            int transStatus = 1;

            if (cmbLocation.Text.Trim() != "")
            {
                OpeningStockService openingStockService = new OpeningStockService();
                LocationService locationService = new LocationService();
                Common.LoadPOSDocuments(cmbPosDocument, openingStockService.GetPOSDocuments(locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID, transStatus));
                cmbPosDocument.SelectedIndex = -1;

                cmbPosDocument.Enabled = true;
                btnPosDetails.Enabled = true;
            }
            
        }

       

        private bool RecallReturnDocument(string documentNo)
        {
            try
            {
                recallDocument = true;
                this.Cursor = Cursors.WaitCursor;
                TransactionDet transactionDet = new TransactionDet();
                InvPurchaseService invPurchaseService = new Service.InvPurchaseService();
                LocationService locationService = new Service.LocationService();
                OpeningStockService openingStockService = new Service.OpeningStockService();


                txtDocumentNo.Text = GetDocumentNo(true);

                dgvItemDetails.DataSource = null;

                openingStockDetailTempList = openingStockService.GetPosDocumentDetails(cmbPosDocument.Text.Trim(), locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID);


                dgvItemDetails.AutoGenerateColumns = false;
                dgvItemDetails.DataSource = openingStockDetailTempList;
                dgvItemDetails.Refresh();

                GetSummarizeFigures(openingStockDetailTempList);

                Common.EnableTextBox(false, txtDocumentNo, txtProductCode, txtProductName);
                Common.EnableComboBox(false, cmbLocation);

                grpFooter.Enabled = true;
                grpBody.Enabled = true;
                EnableLine(false);

                if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                Common.EnableButton(false, btnPosDetails, btnLoadPosDocuments);
                Common.EnableComboBox(false, cmbPosDocument);
                this.ActiveControl = txtProductCode;
                txtProductCode.Focus();
                dtpExpiry.Value = DateTime.Now;

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

        private void btnPosDetails_Click(object sender, EventArgs e)
        {
            if (!cmbPosDocument.Text.Trim().Equals(string.Empty))
            {
                RecallReturnDocument(cmbPosDocument.Text.Trim());
                txtReference.Text = cmbPosDocument.Text.Trim();
            }
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
                    txtProductName.Enabled = true;
                    txtProductCode.Enabled = true;

                    chkAutoCompleationProduct.Checked = true;
                    chkAutoCompleationProduct.Enabled = true;

                    chkOverwrite.Checked = true;
                    chkOverwrite.Enabled = true;

                    //txtProductName.Enabled = true;
                    //txtProductCode.Enabled = true;
                    txtProductCode.Focus();
                }
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
                if (chkAutoCompleationProduct.Checked)
                {
                    LoadProducts(isInvProduct);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
