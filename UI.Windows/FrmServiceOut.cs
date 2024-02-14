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
    public partial class FrmServiceOut : UI.Windows.FrmBaseTransactionForm
    {

        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<ServiceOutDetailTemp> serviceOutDetailTempList = new List<ServiceOutDetailTemp>();
        ServiceOutDetailTemp existingServiceOutDetailTemp = new ServiceOutDetailTemp();

        List<InvProductSerialNoTemp> lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();
        LgsProductMaster existingLgsProductMaster;

        List<LgsProductBatchNoTemp> lgsProductBatchNoTempList = new List<LgsProductBatchNoTemp>();
        LgsProductBatchNoTemp existingLgsProductBatchNoTemp = new LgsProductBatchNoTemp();

        int documentID = 0;
        int documentState;
        static string batchNumber;
        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;

        public FrmServiceOut()
        {
            InitializeComponent();
        }

        private void FrmServiceOut_Load(object sender, EventArgs e)
        {

        }

        public override void InitializeForm()
        {
            try
            {
                // Disable product details controls
                grpFooter.Enabled = false;
                EnableLine(false);
                Common.EnableTextBox(false, txtProductCode, txtProductName);
                Common.EnableTextBox(true, txtSupplierCode, txtSupplierName, txtEmployeeCode, txtEmployeeName, txtReference, txtRemark, txtDocumentNo);
                Common.EnableComboBox(true, cmbLocation);
                Common.EnableButton(false, btnSave, btnPause);
                serviceOutDetailTempList = null;
                this.ActiveControl = txtReference;
                txtReference.Focus();

                selectedRowIndex = 0;
                isUpdateGrid = false;
                rowCount = 0;

                cmbLocation.SelectedValue = Common.LoggedLocationID;

                ShowDocumentNo();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void ShowDocumentNo()
        {
            txtDocumentNo.Text = GetDocumentNo(true);
        }

        private string GetDocumentNo(bool isTemporytNo)
        {
            try
            {
                ServiceOutService serviceOutService = new ServiceOutService();
                LocationService locationService = new LocationService();
                return serviceOutService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }
        }

        private void EnableLine(bool enable)
        {
            Common.EnableTextBox(enable, txtBatchNo, txtQty, txtRate, txtProductAmount);
            Common.EnableComboBox(enable, cmbUnit);
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode, txtProductName, txtBatchNo, txtRate, txtQty, txtProductAmount);
            Common.ClearComboBox(cmbUnit);
            txtProductCode.Focus();
        }

        private void LoadProducts()
        {
            //LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
            //Common.SetAutoComplete(txtProductCode, lgsProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
            //Common.SetAutoComplete(txtProductName, lgsProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);

            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
            Common.SetAutoComplete(txtProductCode, lgsProductMasterService.GetAllProductCodesAccordingToBatchNumber(), chkAutoCompleationProduct.Checked);
            Common.SetAutoComplete(txtProductName, lgsProductMasterService.GetAllProductNamesAccordingToBatchNumber(), chkAutoCompleationProduct.Checked);
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

            // Load Employee Codes and Names 
            EmployeeService employeeService = new EmployeeService();
            Common.SetAutoComplete(txtEmployeeName, employeeService.GetAllEmployeeNames(), chkAutoCompleteEmployee.Checked);
            Common.SetAutoComplete(txtEmployeeCode, employeeService.GetAllEmployeeCodes(), chkAutoCompleteEmployee.Checked);

            // Load Supplier Codes and Names 
            LgsSupplierService lgsSupplierService = new LgsSupplierService();
            Common.SetAutoComplete(txtSupplierCode, lgsSupplierService.GetAllLgsSupplierCodes(), chkAutoCompleteSupplier.Checked);
            Common.SetAutoComplete(txtSupplierName, lgsSupplierService.GetAllLgsSupplierNames(), chkAutoCompleteSupplier.Checked);

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

            chkOverwrite.Checked = autoGenerateInfo.IsOverWriteQty;
            documentID = autoGenerateInfo.DocumentID;


            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
            if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }
                
            base.FormLoad();

            ////Load Document Numbers
            ServiceOutService serviceOutService = new ServiceOutService();
            Common.SetAutoComplete(txtDocumentNo, serviceOutService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

        }

        private void txtReference_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtSupplierCode.Focus();
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
                    dtpServiceOutDate.Focus();
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

        private void dtpServiceOutDate_KeyDown(object sender, KeyEventArgs e)
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

        private void loadProductDetails(bool isCode, string strProduct, long unitofMeasureID)
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
                    ServiceOutService serviceOutService = new ServiceOutService();
                    if (serviceOutDetailTempList == null)
                        serviceOutDetailTempList = new List<ServiceOutDetailTemp>();
                    existingServiceOutDetailTemp = serviceOutService.getServiceOutDetailTemp(serviceOutDetailTempList, existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID);
                    if (existingServiceOutDetailTemp != null)
                    {
                        txtProductCode.Text = existingServiceOutDetailTemp.ProductCode;
                        txtProductName.Text = existingServiceOutDetailTemp.ProductName;
                        cmbUnit.SelectedValue = existingServiceOutDetailTemp.UnitOfMeasureID;
                        if (existingServiceOutDetailTemp.BatchNo != null) { txtBatchNo.Text = existingServiceOutDetailTemp.BatchNo; }
                        else { txtBatchNo.Text = string.Empty; }
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingServiceOutDetailTemp.OrderQty);
                        txtBalQty.Text = Common.ConvertDecimalToStringQty(existingServiceOutDetailTemp.BalancedQty);
                        txtRate.Text = Common.ConvertDecimalToStringCurrency(existingServiceOutDetailTemp.CostPrice);
                        Common.EnableComboBox(true, cmbUnit);
                        if (unitofMeasureID.Equals(0))
                            cmbUnit.Focus();
                    }
                }
                else
                {
                    Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    this.ActiveControl = txtProductCode;
                    txtProductCode.Focus();
                    return;
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
            loadProductDetails(false, txtProductName.Text.Trim(), 0);
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
            if (cmbUnit.SelectedValue == null)
                return;

            if (!existingLgsProductMaster.UnitOfMeasureID.Equals(Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())))
            {
                LgsProductUnitConversionService lgsProductUnitConversionService = new LgsProductUnitConversionService();
                if (lgsProductUnitConversionService.GetProductUnitByProductCode(existingLgsProductMaster.LgsProductMasterID, Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())) == null)
                {
                    Toast.Show("Unit - " + cmbUnit.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "Product - " + txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "");
                    cmbUnit.SelectedValue = existingLgsProductMaster.UnitOfMeasureID;
                    cmbUnit.Focus();
                    return;
                }
            }
            loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()));

            if (existingLgsProductMaster.IsBatch)
            {
                txtBatchNo.Enabled = true;
                txtBatchNo.Focus();
            }
            else
            {
                txtQty.Enabled = true;
                if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                    txtQty.Text = "1";
                txtQty.Focus();
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
                        lgsProductBatchNoTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                        lgsProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        ServiceOutService serviceOutService = new ServiceOutService();
                        lgsProductBatchNoTempList = serviceOutService.GetLgsBatchNoDetail(existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                        if (lgsProductBatchNoTempList == null)
                            lgsProductBatchNoTempList = new List<LgsProductBatchNoTemp>();

                        FrmBatchNumber frmBatchNumber = new FrmBatchNumber(lgsProductBatchNoTempList, lgsProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), false, FrmBatchNumber.transactionType.ServiceOut, existingLgsProductMaster.LgsProductMasterID);
                        frmBatchNumber.ShowDialog();

                        txtBatchNo.Text = batchNumber;

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

        private void UpdateGrid(ServiceOutDetailTemp serviceOutDetailTemp)
        {
            try
            {
                serviceOutDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                serviceOutDetailTemp.BaseUnitID = existingLgsProductMaster.UnitOfMeasureID;

                serviceOutDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();

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
                    txtQty.Text = (serviceOutDetailTemp.OrderQty + Common.ConvertStringToDecimalQty(txtQty.Text)).ToString();
                }

                serviceOutDetailTemp.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text);
                serviceOutDetailTemp.BalancedQty = Common.ConvertStringToDecimalQty(txtQty.Text);
                CalculateLine();

                serviceOutDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtRate.Text);
                serviceOutDetailTemp.NetAmount = Common.ConvertStringToDecimalCurrency(txtProductAmount.Text);
                serviceOutDetailTemp.BatchNo = txtBatchNo.Text.Trim();

                ServiceOutService serviceOutService = new ServiceOutService();

                dgvItemDetails.DataSource = null;
                serviceOutDetailTempList = serviceOutService.getUpdateServiceOutDetailTemp(serviceOutDetailTempList, serviceOutDetailTemp);
                dgvItemDetails.DataSource = serviceOutDetailTempList;
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

                GetSummarizeFigures(serviceOutDetailTempList);

                EnableLine(false);
                Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtEmployeeCode, txtEmployeeName, txtDocumentNo);
                Common.EnableComboBox(false, cmbLocation);
                if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                ClearLine();
                if (serviceOutDetailTempList.Count > 0)
                    grpFooter.Enabled = true;

                txtProductCode.Enabled = true;
                txtProductCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void GetSummarizeFigures(List<ServiceOutDetailTemp> listItem)
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
            ServiceOutService serviceOutService = new ServiceOutService();
            Common.SetAutoComplete(txtDocumentNo, serviceOutService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    decimal qty = Convert.ToDecimal(txtQty.Text.Trim());
                    ServiceOutService serviceOutService = new ServiceOutService();
                    CommonService commonService = new CommonService();

                    if (commonService.ValidateCurrentStock(qty, existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())))
                    {
                        if (existingLgsProductMaster.IsBatch)
                        {
                            if (commonService.ValidateBatchStock(qty, existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), txtBatchNo.Text.Trim(), 1)) { }
                            else
                            {
                                Toast.Show("", Toast.messageType.Information, Toast.messageAction.BatchQtyExceed);
                                txtQty.Focus();
                                return;
                            }
                        }

                        if (existingLgsProductMaster.IsSerial)
                        {
                            InvProductSerialNoTemp lgsProductSerialNoTemp = new InvProductSerialNoTemp();
                            lgsProductSerialNoTemp.DocumentID = documentID;
                            lgsProductSerialNoTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                            lgsProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                            lgsProductSerialNoTempList = serviceOutService.GetLgsSerialNoDetail(existingLgsProductMaster);

                            if (lgsProductSerialNoTempList == null)
                                lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                            FrmSerialCommon frmSerialCommon = new FrmSerialCommon(lgsProductSerialNoTempList, lgsProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), false, documentID, FrmSerialCommon.transactionType.ServiceOut);
                            frmSerialCommon.ShowDialog();
                            CalculateLine();

                            txtProductAmount.Enabled = true;
                            txtProductAmount.Focus();

                        }
                        else
                        {
                            txtProductAmount.Enabled = true;
                            txtProductAmount.Focus();
                        }
                    }
                    else
                    {
                        Toast.Show("Service out", Toast.messageType.Information, Toast.messageAction.QtyExceed);
                        return;
                    }
                }
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

        private void txtProductAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    UpdateGrid(existingServiceOutDetailTemp);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo);
        }

        public override void Pause()
        {
            if ((Toast.Show("Service Out  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateControls().Equals(false)) { return; }
                if (SaveDocument(0, txtDocumentNo.Text.Trim()).Equals(true))
                {
                    Toast.Show("Service Out  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    GenerateReport(txtDocumentNo.Text.Trim(), 0);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Service Out  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        public override void Save()
        {
            if ((Toast.Show("Service Out  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateControls().Equals(false)) { return; }
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim());
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Service Out  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(txtDocumentNo.Text.Trim().Trim(), 1);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Service Out  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        private bool SaveDocument(int documentStatus, string documentNo)
        {
            try
            {
                ServiceOutService serviceOutService = new ServiceOutService();
                ServiceOutHeader serviceOutHeader = new ServiceOutHeader();
                LocationService locationService = new LocationService();
                Location Location = new Location();
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                LgsSupplier lgsSupplier = new LgsSupplier();
                EmployeeService employeeService = new EmployeeService();
                Employee employee = new Employee();

                lgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());
                employee = employeeService.GetEmployeesByCode(txtEmployeeCode.Text.Trim());

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                serviceOutHeader = serviceOutService.GetPausedServiceOutHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (serviceOutHeader == null)
                    serviceOutHeader = new ServiceOutHeader();

                if (documentStatus.Equals(1)) // update paused document
                {
                    documentNo = GetDocumentNo(false);
                    txtDocumentNo.Text = documentNo;
                }

                //sampleOutHeader.SampleOutHeaderID = sampleOutHeader.SampleHeaderID;
                serviceOutHeader.CompanyID = Location.CompanyID;
                serviceOutHeader.DocumentDate = Common.ConvertStringToDate(dtpServiceOutDate.Value.ToString());
                serviceOutHeader.DocumentID = documentID;
                serviceOutHeader.DocumentStatus = documentStatus;
                serviceOutHeader.DocumentNo = documentNo.Trim();
                serviceOutHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                serviceOutHeader.LocationID = Location.LocationID;
                serviceOutHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtTotalAmount.Text.ToString());
                serviceOutHeader.TotalQty = Common.ConvertStringToDecimalQty(txtTotalQty.Text.ToString());
                serviceOutHeader.TotalBalancedQty = Common.ConvertStringToDecimalQty(txtTotalQty.Text.ToString());
                serviceOutHeader.ReferenceNo = txtReference.Text.Trim();
                serviceOutHeader.Remark = txtRemark.Text.Trim();

                if (lgsSupplier != null) { serviceOutHeader.SupplierID = lgsSupplier.LgsSupplierID; }
                else { serviceOutHeader.SupplierID = 0; }

                if (employee != null) { serviceOutHeader.EmployeeID = employee.EmployeeID; }
                else { serviceOutHeader.EmployeeID = 0; }

                if (serviceOutDetailTempList == null)
                    serviceOutDetailTempList = new List<ServiceOutDetailTemp>();

                if (lgsProductSerialNoTempList == null)
                    lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                return serviceOutService.Save(serviceOutHeader, serviceOutDetailTempList, lgsProductSerialNoTempList);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return false;
            }
        }

        public override void ClearForm()
        {
            errorProvider.Clear();
            dtpServiceOutDate.Value = DateTime.Now;
            dgvItemDetails.DataSource = null;
            dgvItemDetails.Refresh();
            base.ClearForm();
        }

        /// <summary>
        /// Generate Transaction report
        /// </summary>
        /// <param name="documentNo"></param>
        /// <param name="documentStatus"></param>
        private void GenerateReport(string documentNo, int documentStatus)
        {
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            lgsReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
        }

        private bool RecallDocument(string documentNo)
        {
            try
            {
                LgsPurchaseService lgsPurchaseServices = new LgsPurchaseService();

                ServiceOutService serviceOutService = new ServiceOutService();
                ServiceOutHeader serviceOutHeader = new ServiceOutHeader();
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                LgsSupplier lgsSupplier = new LgsSupplier();
                EmployeeService employeeService = new EmployeeService();
                Employee employee = new Employee();

                serviceOutHeader = serviceOutService.GetPausedServiceOutHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (serviceOutHeader != null)
                {
                    lgsSupplier = lgsSupplierService.GetLgsSupplierByID(serviceOutHeader.SupplierID);
                    employee = employeeService.GetEmployeesByID(serviceOutHeader.EmployeeID);

                    cmbLocation.SelectedValue = serviceOutHeader.LocationID;
                    cmbLocation.Refresh();

                    if (lgsSupplier != null)
                    {
                        txtSupplierCode.Text = lgsSupplier.SupplierCode.Trim();
                        txtSupplierName.Text = lgsSupplier.SupplierName.Trim();
                    }
                    else
                    {
                        txtSupplierCode.Text = string.Empty;
                        txtSupplierName.Text = string.Empty;
                    }

                    if (employee != null)
                    {
                        txtEmployeeCode.Text = employee.EmployeeCode.Trim();
                        txtEmployeeName.Text = employee.EmployeeName.Trim();
                    }
                    else
                    {
                        txtEmployeeCode.Text = string.Empty;
                        txtEmployeeName.Text = string.Empty;
                    }

                    documentState = serviceOutHeader.DocumentStatus;

                    dtpServiceOutDate.Value = Common.FormatDate(serviceOutHeader.DocumentDate);

                    txtDocumentNo.Text = serviceOutHeader.DocumentNo;
                    txtTotalAmount.Text = Common.ConvertDecimalToStringCurrency(serviceOutHeader.NetAmount);
                    txtTotalQty.Text = Common.ConvertDecimalToStringQty(serviceOutHeader.TotalQty);
                    txtReference.Text = serviceOutHeader.ReferenceNo;
                    txtRemark.Text = serviceOutHeader.Remark;

                    dgvItemDetails.DataSource = null;
                    serviceOutDetailTempList = serviceOutService.getPausedServiceOutDetail(serviceOutHeader);
                    dgvItemDetails.DataSource = serviceOutDetailTempList;

                    lgsProductSerialNoTempList = serviceOutService.getInvSerialNoDetailForServiceOut(serviceOutHeader);

                    dgvItemDetails.Refresh();

                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtEmployeeName, txtEmployeeCode, txtDocumentNo, txtReference);
                    Common.EnableComboBox(false, cmbLocation);

                    if (serviceOutHeader.DocumentStatus.Equals(0))
                    {
                        grpFooter.Enabled = true;
                        EnableLine(false);
                        EnableProductDetails(true);
                        LoadProducts();
                        if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    }
                    else
                    {
                        grpFooter.Enabled = false;
                        EnableLine(false);
                        EnableProductDetails(false);
                        Common.EnableButton(false, btnSave, btnPause);
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
            if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                RecallDocument(txtDocumentNo.Text.Trim());
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
                        if (documentState.Equals(1))
                        {
                            EnableLine(false);
                            EnableProductDetails(false);
                        }
                        else
                        {
                            EnableLine(false);
                            cmbUnit.Enabled = true;
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

        private void chkAutoCompleationProduct_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                Common.SetAutoComplete(txtProductCode, lgsProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                Common.SetAutoComplete(txtProductName, lgsProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
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
                ServiceOutService serviceOutService = new ServiceOutService();
                Common.SetAutoComplete(txtDocumentNo, serviceOutService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleteSupplier_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                Common.SetAutoComplete(txtSupplierCode, lgsSupplierService.GetAllLgsSupplierCodes(), chkAutoCompleteSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, lgsSupplierService.GetAllLgsSupplierNames(), chkAutoCompleteSupplier.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleteEmployee_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                EmployeeService employeeService = new EmployeeService();
                Common.SetAutoComplete(txtEmployeeName, employeeService.GetAllEmployeeNames(), chkAutoCompleteEmployee.Checked);
                Common.SetAutoComplete(txtEmployeeCode, employeeService.GetAllEmployeeCodes(), chkAutoCompleteEmployee.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadSupplier(bool isCode, string strsupplier)
        {
            try
            {
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                LgsSupplier existingLgsSupplier = new LgsSupplier();

                if (isCode)
                {
                    existingLgsSupplier = lgsSupplierService.GetLgsSupplierByCode(strsupplier);
                    if (isCode && strsupplier.Equals(string.Empty))
                    {
                        txtSupplierCode.Focus();
                        return;
                    }

                }
                else
                    existingLgsSupplier = lgsSupplierService.GetLgsSupplierByName(strsupplier);

                if (existingLgsSupplier != null)
                {
                    txtSupplierCode.Text = existingLgsSupplier.SupplierCode;
                    txtSupplierName.Text = existingLgsSupplier.SupplierName;
                    txtEmployeeCode.Focus();
                }

                else
                {
                    Toast.Show("Logistic Supplier - " + strsupplier.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadEmployee(bool isCode, string strEmployee) 
        {
            try
            {
                EmployeeService employeeService = new EmployeeService();
                Employee employee = new Employee();

                if (isCode)
                {
                    employee = employeeService.GetEmployeesByCode(strEmployee);
                    if (isCode && strEmployee.Equals(string.Empty))
                    {
                        txtEmployeeCode.Focus();
                        return;
                    }

                }
                else
                    employee = employeeService.GetEmployeesByName(strEmployee);

                if (employee != null)
                {
                    txtEmployeeCode.Text = employee.EmployeeCode;
                    txtEmployeeName.Text = employee.EmployeeName;
                    txtRemark.Focus();
                }

                else
                {
                    Toast.Show("Employee - " + strEmployee.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtSupplierName.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSupplierCode.Text.Trim()))
                {
                    LoadSupplier(true, txtSupplierCode.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtEmployeeCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSupplierName.Text.Trim()))
                {
                    LoadSupplier(false, txtSupplierName.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmployeeCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtEmployeeName.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmployeeCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEmployeeCode.Text.Trim()))
                {
                    LoadEmployee(true, txtEmployeeCode.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmployeeName_KeyDown(object sender, KeyEventArgs e)
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

        private void txtEmployeeName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEmployeeName.Text.Trim()))
                {
                    LoadEmployee(false, txtEmployeeName.Text.Trim());
                }
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
                        if (Toast.Show("Product " + dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + " - " + dgvItemDetails["ProductName", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                            return;

                        ServiceOutDetailTemp serviceOutDetailTemp = new ServiceOutDetailTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                        serviceOutDetailTemp.ProductID = lgsProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).LgsProductMasterID;
                        serviceOutDetailTemp.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                        ServiceOutService serviceOutService = new ServiceOutService();

                        dgvItemDetails.DataSource = null;
                        serviceOutDetailTempList = serviceOutService.GetDeleteServiceOutDetailTemp(serviceOutDetailTempList, serviceOutDetailTemp);
                        dgvItemDetails.DataSource = serviceOutDetailTempList;
                        dgvItemDetails.Refresh();

                        if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                        else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }

                        GetSummarizeFigures(serviceOutDetailTempList);
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

        



    }
}
