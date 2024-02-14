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

namespace UI.Windows
{
    public partial class FrmSampleIn : UI.Windows.FrmBaseTransactionForm
    {

        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<SampleInDetailTemp> sampleInDetailTempList = new List<SampleInDetailTemp>();
        SampleInDetailTemp existingSampleInDetailTemp = new SampleInDetailTemp();

        List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();
        List<LgsProductSerialNoTemp> lgsProductSerialNoTempList = new List<LgsProductSerialNoTemp>();

        LgsProductMaster existingLgsProductMaster;
        InvProductMaster existingInvProductMaster;

        int documentID = 0;
        int documentState;
        bool isSupplierProduct;
        bool isInvProduct;
        int type;
        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;

        public FrmSampleIn()
        {
            InitializeComponent();
        }

        private void FrmSampleIn_Load(object sender, EventArgs e)
        {

        }

        private void ShowDocumentNo()
        {
            txtDocumentNo.Text = GetDocumentNo(true, isInvProduct);
        }

        public override void InitializeForm()
        {
            try
            {
                // Disable product details controls
                grpFooter.Enabled = false;
                EnableLine(false);
                Common.EnableTextBox(true, txtIssuedTo, txtRemark, txtDeliveryPerson, txtDocumentNo, txtSampleOut);
                Common.EnableComboBox(true, cmbLocation, cmbType);
                Common.EnableButton(false, btnSave, btnPause);
                this.ActiveControl = txtSampleOut;
                txtSampleOut.Focus();
                cmbLocation.SelectedValue = Common.LoggedLocationID;

                selectedRowIndex = 0;
                isUpdateGrid = false;
                rowCount = 0;

                cmbType.SelectedIndex = -1;

                ShowDocumentNo();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private string GetDocumentNo(bool isTemporytNo, bool isInv)
        {
            try
            {
                SampleInService sampleInService = new SampleInService();
                LocationService locationService = new LocationService();
                return sampleInService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo, isInv).Trim();
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

        private void LoadProductsFromSampleOut(string sampleOutDocNo)
        {
            SampleOutService sampleOutService = new SampleOutService();
            Common.SetAutoComplete(txtProductCode, sampleOutService.GetAllProductCodesAccordingToSampleOut(isInvProduct, txtSampleOut.Text.Trim()), chkAutoCompleationProduct.Checked);
            Common.SetAutoComplete(txtProductName, sampleOutService.GetAllProductNamesAccordingToSampleOut(isInvProduct, txtSampleOut.Text.Trim()), chkAutoCompleationProduct.Checked);
        }

        public override void FormLoad()
        {
            dgvItemDetails.AutoGenerateColumns = false;

            // Load Unit of measures
            UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
            Common.LoadUnitOfMeasures(cmbUnit, unitOfMeasureService.GetAllUnitOfMeasures());

            // Load Locations
            LocationService locationService = new LocationService();
            Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
            cmbLocation.SelectedValue = Common.LoggedLocationID;

            //// Load Issued To
            SampleInService SampleInService = new SampleInService();
            Common.SetAutoComplete(txtIssuedTo, SampleInService.GetAllIssuedToNames(), true);

            //Load Sample In Type
            LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
            Common.SetAutoBindRecords(cmbType, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.ModuleType).ToString()));

            //// Load Delivery Person
            Common.SetAutoComplete(txtDeliveryPerson, SampleInService.GetAllDeliveryPersons(), true);

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

            isSupplierProduct = autoGenerateInfo.IsSupplierProduct;
            documentID = autoGenerateInfo.DocumentID;

            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
            if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }

            base.FormLoad();

            ////Load Sample in Document Numbers
            SampleInService sampleInService = new SampleInService();
            Common.SetAutoComplete(txtDocumentNo, sampleInService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

            ////Load Sample out Document Numbers
            SampleOutService sampleOutService = new SampleOutService();
            Common.SetAutoComplete(txtSampleOut, sampleOutService.GetAllDocumentNumbersToSampleIn(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationSampleOut.Checked);

            this.ActiveControl = txtSampleOut;
            txtSampleOut.Focus();
        }

        private void txtSampleOut_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtSampleOut_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSampleOut_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtSampleOut.Text.Trim().Equals(string.Empty))
                {
                    RecallSampleOut(txtSampleOut.Text.Trim());
                    //LoadProducts(isInvProduct);
                    LoadProductsFromSampleOut(txtSampleOut.Text.Trim());
                    this.ActiveControl = txtProductCode;
                    txtProductCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool RecallDocument(string documentNo)
        {
            try
            {
                SampleInService sampleInService = new SampleInService();
                SampleInHeader sampleInHeader = new SampleInHeader();

                sampleInHeader = sampleInService.GetPausedSampleInHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (sampleInHeader != null)
                {
                    cmbLocation.SelectedValue = sampleInHeader.LocationID;
                    cmbLocation.Refresh();

                    documentState = sampleInHeader.DocumentStatus;
                    dtpInDate.Value = Common.FormatDate(sampleInHeader.DocumentDate);

                    txtDocumentNo.Text = sampleInHeader.DocumentNo;

                    SampleOutHeader sampleOutHeader = new SampleOutHeader();
                    SampleOutService sampleOutService = new SampleOutService();
                    sampleOutHeader = sampleOutService.RecallSampleOutHeaderToServiceIn(sampleInHeader.SampleOutHeaderID);
                    if (sampleOutHeader != null) { txtSampleOut.Text = sampleOutHeader.DocumentNo; }
                    else { txtSampleOut.Text = string.Empty; }

                    txtTotalAmount.Text = Common.ConvertDecimalToStringCurrency(sampleInHeader.NetAmount);
                    txtTotalQty.Text = Common.ConvertDecimalToStringQty(sampleInHeader.TotalQty);
                    txtRemark.Text = sampleInHeader.Remark;
                    txtIssuedTo.Text = sampleInHeader.IssuedTo;
                    txtDeliveryPerson.Text = sampleInHeader.DeliveryPerson;


                    if (sampleInHeader.SampleOutType.Equals(1))
                    {
                        RecallSampleInType(1);
                    }
                    else if (sampleInHeader.SampleOutType.Equals(2))
                    {
                        RecallSampleInType(2);
                    }


                    dgvItemDetails.DataSource = null;
                    sampleInDetailTempList = sampleInService.getPausedSampleInDetail(sampleInHeader, isInvProduct);
                    dgvItemDetails.DataSource = sampleInDetailTempList;
                    dgvItemDetails.Refresh();

                    Common.EnableTextBox(false, txtIssuedTo, txtDeliveryPerson, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation, cmbType);

                    if (sampleInHeader.DocumentStatus.Equals(0))
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

        private void RecallSampleInType(int type)
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

        private bool RecallSampleOut(string documentNo)
        {
            try
            {
                SampleOutService sampleOutService = new SampleOutService();
                SampleInService sampleInService = new SampleInService();
                SampleOutHeader sampleOutHeader = new SampleOutHeader();

                sampleOutHeader = sampleOutService.GetSampleOutHeaderToSampleIn(txtSampleOut.Text.Trim());
                if (sampleOutHeader != null)
                {
                    documentState = 0;
                    cmbLocation.SelectedValue = sampleOutHeader.LocationID;
                    cmbLocation.Refresh();

                    txtTotalAmount.Text = Common.ConvertDecimalToStringCurrency(sampleOutHeader.NetAmount);
                    txtTotalQty.Text = Common.ConvertDecimalToStringQty(sampleOutHeader.TotalQty);
                    txtRemark.Text = sampleOutHeader.Remark;
                    txtIssuedTo.Text = sampleOutHeader.IssuedTo;
                    txtDeliveryPerson.Text = sampleOutHeader.DeliveryPerson;

                    if (sampleOutHeader.SampleOutType.Equals(1))
                    {
                        RecallSampleInType(1);
                    }
                    else if (sampleOutHeader.SampleOutType.Equals(2))
                    {
                        RecallSampleInType(2);
                    }

                    dgvItemDetails.DataSource = null;
                    sampleInDetailTempList = sampleInService.GetSampleOutDetailForSampleIn(sampleOutHeader, isInvProduct);
                    dgvItemDetails.DataSource = sampleInDetailTempList; 
                    dgvItemDetails.Refresh();

                    Common.EnableTextBox(false, txtIssuedTo, txtDocumentNo, txtSampleOut);
                    Common.EnableComboBox(false, cmbLocation, cmbType);

                    grpFooter.Enabled = true;
                    EnableLine(false);
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

                    //ActiveControl = txtProductCode;
                    //txtProductCode.Focus();

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

        private void cmbSampleOutMethod_SelectedValueChanged(object sender, EventArgs e)
        {
            GetSampleOutType();
        }

        private void GetSampleOutType()
        {
            if (cmbType.Text.Equals("Inventory"))
            {
                isInvProduct = true;
                type = 1;
            }
            else
            {
                isInvProduct = false;
                type = 2;
            }
        }

        private void EnableLine(bool enable)
        {
            Common.EnableTextBox(enable,txtBatchNo, txtQty, txtProductAmount);
            Common.EnableComboBox(enable, cmbUnit);
        }

        private void cmbLocation_Validated(object sender, EventArgs e)
        {
            try
            {
                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbLocation.Text.Trim()).LocationID, documentID);
                if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpInDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    dgvItemDetails.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void EnableProductDetails(bool state)
        {
            txtProductCode.Enabled = state;
            txtProductName.Enabled = state;
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
                        EnableProductDetails(false);
                        cmbUnit.Enabled = false;

                        if (documentState.Equals(1))
                        {
                            EnableProductDetails(false);
                            EnableLine(false);
                        }
                        else
                        {
                            EnableProductDetails(false);
                            EnableLine(false);
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

        private void CalculateLine()
        {
            try
            {
                txtProductAmount.Text = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim())).ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                        SampleOutService sampleOutService = new SampleOutService();

                        if (sampleOutService.checkIsSampleOutProduct(txtSampleOut.Text.Trim(), existingInvProductMaster.InvProductMasterID))
                        {
                            SampleInService sampleInService = new SampleInService();
                            if (sampleInDetailTempList == null)
                                sampleInDetailTempList = new List<SampleInDetailTemp>();
                            existingSampleInDetailTemp = sampleInService.getSampleInDetailTemp(sampleInDetailTempList, existingLgsProductMaster, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID, isInvProduct);
                            if (existingSampleInDetailTemp != null)
                            {
                                txtProductCode.Text = existingSampleInDetailTemp.ProductCode;
                                txtProductName.Text = existingSampleInDetailTemp.ProductName;
                                cmbUnit.SelectedValue = existingSampleInDetailTemp.UnitOfMeasureID;
                                txtBatchNo.Text = existingSampleInDetailTemp.BatchNo;
                                txtQty.Text = Common.ConvertDecimalToStringQty(existingSampleInDetailTemp.OrderQty);
                                txtBalQty.Text = Common.ConvertDecimalToStringQty(existingSampleInDetailTemp.BalancedQty);
                                txtRate.Text = Common.ConvertDecimalToStringCurrency(existingSampleInDetailTemp.CostPrice);
                                Common.EnableComboBox(false, cmbUnit);

                                txtQty.Enabled = true;
                                txtQty.Focus();
                            }
                        }
                        else
                        {
                            Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                            if (isCode) { txtProductCode.SelectAll(); txtProductCode.Focus(); }
                            else { txtProductName.SelectAll(); txtProductName.Focus(); }
                            return;
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
                        SampleInService sampleInService = new SampleInService();
                        if (sampleInDetailTempList == null)
                            sampleInDetailTempList = new List<SampleInDetailTemp>();
                        existingSampleInDetailTemp = sampleInService.getSampleInDetailTemp(sampleInDetailTempList, existingLgsProductMaster, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID, isInvProduct);
                        if (existingSampleInDetailTemp != null)
                        {
                            txtProductCode.Text = existingSampleInDetailTemp.ProductCode;
                            txtProductName.Text = existingSampleInDetailTemp.ProductName;
                            cmbUnit.SelectedValue = existingSampleInDetailTemp.UnitOfMeasureID;
                            txtQty.Text = Common.ConvertDecimalToStringQty(existingSampleInDetailTemp.OrderQty);
                            txtRate.Text = Common.ConvertDecimalToStringCurrency(existingSampleInDetailTemp.CostPrice);
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

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtQty_Leave(this, e);
                    CalculateLine();
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
                if (string.IsNullOrEmpty(txtQty.Text.Trim()))
                {
                    txtQty.Text = "0";
                }
                decimal strQty = Convert.ToDecimal(txtQty.Text.Trim());
                int qty = (int)strQty;

                SampleOutHeader sampleOutHeader = new SampleOutHeader();
                SampleOutDetail sampleOutDetail = new SampleOutDetail();
                SampleOutService sampleOutService = new SampleOutService(); 
                SampleOutDetailTemp sampleOutDetailTemp = new SampleOutDetailTemp();
                InvProductMaster invProductMaster = new InvProductMaster();
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                LgsProductMaster lgsProductMaster = new LgsProductMaster();
                LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                if (isInvProduct)
                {
                    sampleOutHeader = sampleOutService.GetSampleOutHeaderToSampleIn(txtSampleOut.Text.Trim());
                    if (sampleOutHeader != null)
                    {
                        invProductMaster = invProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());
                        sampleOutDetailTemp.ProductID = (invProductMaster.InvProductMasterID);
                        sampleOutDetailTemp.UnitOfMeasureID = Convert.ToInt32(cmbUnit.SelectedValue);
                        sampleOutDetailTemp.SampleOutHeaderID = sampleOutHeader.SampleOutHeaderID;
                    }
                    if (!sampleOutService.IsValidNoOfQty(qty, sampleOutDetailTemp))
                    {
                        Toast.Show("Invalid Qty.\nQty cannot grater then Sample out Qty", Toast.messageType.Information, Toast.messageAction.General, "");
                        txtQty.SelectAll();
                        txtQty.Focus();
                    }
                    else
                    {
                        txtProductAmount.Enabled = true;
                        txtProductAmount.Focus();
                    }
                }
                else
                {
                    sampleOutHeader = sampleOutService.GetSampleOutHeaderToSampleIn(txtSampleOut.Text.Trim());
                    if (sampleOutHeader != null)
                    {
                        lgsProductMaster = lgsProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());
                        sampleOutDetailTemp.ProductID = (lgsProductMaster.LgsProductMasterID);
                        sampleOutDetailTemp.UnitOfMeasureID = Convert.ToInt32(cmbUnit.SelectedValue);
                        sampleOutDetailTemp.SampleOutHeaderID = sampleOutHeader.SampleOutHeaderID;
                    }
                    if (!sampleOutService.IsValidNoOfQty(qty, sampleOutDetailTemp))
                    {
                        Toast.Show("Invalid Qty.\nQty cannot grater then Sample out Qty", Toast.messageType.Information, Toast.messageAction.General, "");
                        txtQty.Focus();
                    }
                    else
                    {
                        txtProductAmount.Enabled = true;
                        txtProductAmount.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void UpdateGrid(SampleInDetailTemp sampleInDetailTemp)
        {
            try
            {
                sampleInDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                if (isInvProduct) { sampleInDetailTemp.BaseUnitID = existingInvProductMaster.UnitOfMeasureID; }
                else { sampleInDetailTemp.BaseUnitID = existingLgsProductMaster.UnitOfMeasureID; }

                sampleInDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();
                
                sampleInDetailTemp.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text);
                CalculateLine();

                sampleInDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtRate.Text);
                sampleInDetailTemp.NetAmount = Common.ConvertStringToDecimalCurrency(txtProductAmount.Text);

                SampleInService sampleInService = new SampleInService();

                dgvItemDetails.DataSource = null;
                sampleInDetailTempList = sampleInService.getUpdateSampleInDetailTemp(sampleInDetailTempList, sampleInDetailTemp);
                dgvItemDetails.DataSource = sampleInDetailTempList;
                dgvItemDetails.Refresh();

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

                GetSummarizeFigures(sampleInDetailTempList);
                EnableLine(false);
                Common.EnableTextBox(false, txtIssuedTo, txtDeliveryPerson);
                Common.EnableComboBox(false, cmbLocation, cmbType);
                if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                ClearLine();
                if (sampleInDetailTempList.Count > 0)
                    grpFooter.Enabled = true;

                txtProductCode.Enabled = true;
                txtProductCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void GetSummarizeFigures(List<SampleInDetailTemp> listItem)
        {
            decimal netAmount = listItem.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.NetAmount);
            decimal qty = listItem.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.OrderQty);
            netAmount = Common.ConvertStringToDecimalCurrency(netAmount.ToString());
            qty = Common.ConvertStringToDecimalQty(qty.ToString());

            txtTotalAmount.Text = Common.ConvertDecimalToStringCurrency(netAmount);
            txtTotalQty.Text = Common.ConvertDecimalToStringQty(qty);
        }

        private void RefreshDocumentNumber()
        {
            ////Load Quotation Document Numbers
            SampleInService sampleInService = new SampleInService();
            Common.SetAutoComplete(txtDocumentNo, sampleInService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
        }

        private void RefreshSampleOutDocumentNumber()
        {
            SampleOutService sampleOutService = new SampleOutService();
            Common.SetAutoComplete(txtSampleOut, sampleOutService.GetAllDocumentNumbersToSampleIn(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationSampleOut.Checked);
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode, txtProductName, txtBatchNo, txtRate, txtQty, txtProductAmount);
            Common.ClearComboBox(cmbUnit);
            txtProductCode.Focus();
        }

        public override void ClearForm()
        {
            errorProvider.Clear();
            dtpInDate.Value = DateTime.Now;
            dgvItemDetails.DataSource = null;
            dgvItemDetails.Refresh();
            base.ClearForm();
        }

        private void txtProductAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    UpdateGrid(existingSampleInDetailTemp); 
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo, txtSampleOut, txtIssuedTo, txtDeliveryPerson);
        }

        public override void Pause()
        {
            if ((Toast.Show("Sample In  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateControls().Equals(false)) { return; }
                if (SaveDocument(0, txtDocumentNo.Text.Trim()).Equals(true))
                {
                    Toast.Show("Sample In  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    ClearForm();
                    RefreshDocumentNumber();
                    RefreshSampleOutDocumentNumber();
                }
                else
                {
                    Toast.Show("Sample In  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        public override void Save()
        {
            if ((Toast.Show("Sample In  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateControls().Equals(false)) { return; }
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim());
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Sample In  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(txtDocumentNo.Text.Trim(), 1);
                    ClearForm();
                    RefreshDocumentNumber();
                    RefreshSampleOutDocumentNumber();
                }
                else
                {
                    Toast.Show("Sample In  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        private bool SaveDocument(int documentStatus, string documentNo)
        {
            try
            {
                SampleInService sampleInService = new SampleInService();
                SampleInHeader sampleInHeader = new SampleInHeader();
                LocationService locationService = new LocationService();
                Location Location = new Location();

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                sampleInHeader = sampleInService.GetPausedSampleInHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (sampleInHeader == null)
                    sampleInHeader = new SampleInHeader();

                if (documentStatus.Equals(1)) // update paused document
                {
                    documentNo = GetDocumentNo(false, isInvProduct);
                    txtDocumentNo.Text = documentNo;
                }

                //sampleOutHeader.SampleOutHeaderID = sampleOutHeader.SampleHeaderID;
                sampleInHeader.CompanyID = Location.CompanyID;
                sampleInHeader.DocumentDate = Common.ConvertStringToDate(dtpInDate.Value.ToString());
                sampleInHeader.IssuedTo = txtIssuedTo.Text.Trim();

                sampleInHeader.SampleOutType = type;

                sampleInHeader.DeliveryPerson = txtDeliveryPerson.Text.Trim();
                sampleInHeader.DocumentID = documentID;
                sampleInHeader.DocumentStatus = documentStatus;
                sampleInHeader.DocumentNo = documentNo.Trim();
                sampleInHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                sampleInHeader.LocationID = Location.LocationID;
                sampleInHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtTotalAmount.Text.ToString());
                sampleInHeader.TotalQty = Common.ConvertStringToDecimalQty(txtTotalQty.Text.ToString());
                sampleInHeader.Remark = txtRemark.Text.Trim();

                SampleOutService sampleOutService = new SampleOutService();
                SampleOutHeader existingSampleOutHeader = new SampleOutHeader();
                existingSampleOutHeader = sampleOutService.GetSampleOutByDocumentNo(txtSampleOut.Text.Trim());

                if (existingSampleOutHeader != null) { sampleInHeader.SampleOutHeaderID = existingSampleOutHeader.SampleOutHeaderID; }
                else { sampleInHeader.SampleOutHeaderID = 0; }


                if (sampleInDetailTempList == null)
                    sampleInDetailTempList = new List<SampleInDetailTemp>();

                return sampleInService.Save(sampleInHeader, sampleInDetailTempList, isInvProduct);

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
                RecallDocument(txtDocumentNo.Text.Trim());
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationSampleOut_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SampleOutService sampleOutService = new SampleOutService();
                Common.SetAutoComplete(txtSampleOut, sampleOutService.GetAllDocumentNumbersToSampleIn(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationSampleOut.Checked);
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
                //LoadProducts(isInvProduct);
                LoadProductsFromSampleOut(txtSampleOut.Text.Trim());
            }
        }

        private void chkAutoCompleationDocumentNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SampleInService sampleInService = new SampleInService();
                Common.SetAutoComplete(txtDocumentNo, sampleInService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
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
            if (cmbType.SelectedValue.Equals(1))
            {
                InvReportGenerator invReportGenerator = new InvReportGenerator();
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                invReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), 1);
            }
            if (cmbType.SelectedValue.Equals(2))
            {
                LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                lgsReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), 1);
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
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductCode_Leave(object sender, EventArgs e)
        {
            try
            {
                loadProductDetails(true, txtProductCode.Text.Trim(), 0);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
