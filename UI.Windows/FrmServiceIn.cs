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
    public partial class FrmServiceIn : UI.Windows.FrmBaseTransactionForm
    {

        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<ServiceInDetailTemp> serviceInDetailTempList = new List<ServiceInDetailTemp>();
        ServiceInDetailTemp existingServiceInDetailTemp = new ServiceInDetailTemp();

        List<LgsProductSerialNoTemp> lgsProductSerialNoTempList = new List<LgsProductSerialNoTemp>();
        LgsProductMaster existingLgsProductMaster;

        int documentID = 0;
        int documentState;
        bool isSupplierProduct;
        bool isInvProduct;

        public FrmServiceIn()
        {
            InitializeComponent();
        }

        private void ShowDocumentNo()
        {
            txtDocumentNo.Text = GetDocumentNo(true);
        }

        private void FrmServiceIn_Load(object sender, EventArgs e)
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
                Common.EnableTextBox(true, txtSupplierCode, txtSupplierName, txtEmployeeCode, txtEmployeeName, txtServiceOut, txtRemark, txtDocumentNo);
                Common.EnableComboBox(true, cmbLocation);
                Common.EnableButton(false, btnSave, btnPause);
                this.ActiveControl = txtServiceOut;
                txtServiceOut.Focus();
                cmbLocation.SelectedValue = Common.LoggedLocationID;

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
                ServiceInService serviceInService = new ServiceInService();
                LocationService locationService = new LocationService();
                return serviceInService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
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
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
            Common.SetAutoComplete(txtProductCode, lgsProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
            Common.SetAutoComplete(txtProductName, lgsProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
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

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

            documentID = autoGenerateInfo.DocumentID;

            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
            if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }
                
            base.FormLoad();

            ////Load Document Numbers
            ServiceInService serviceInService = new ServiceInService();
            Common.SetAutoComplete(txtDocumentNo, serviceInService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

            ////Load Service Out Document Numbers
            ServiceOutService serviceOutService = new ServiceOutService();
            Common.SetAutoComplete(txtServiceOut, serviceOutService.GetAllDocumentNumbersToServiceIn(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationServiceOut.Checked);

        }

        private void txtServiceOut_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtServiceOut_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtServiceOut_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtServiceOut.Text.Trim().Equals(string.Empty))
                {
                    RecallServiceOut(txtServiceOut.Text.Trim());
                    LoadProducts();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool RecallServiceOut(string documentNo)
        {
            try
            {
                ServiceOutService serviceOutService = new ServiceOutService();
                ServiceInService serviceInService = new ServiceInService();
                ServiceOutHeader serviceOutHeader = new ServiceOutHeader();
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                LgsSupplier lgsSupplier = new LgsSupplier();
                EmployeeService employeeService = new EmployeeService();
                Employee employee = new Employee();

                serviceOutHeader = serviceOutService.GetServiceOutHeaderToServiceIn(txtServiceOut.Text.Trim());
                if (serviceOutHeader != null)
                {
                    lgsSupplier = lgsSupplierService.GetLgsSupplierByID(serviceOutHeader.SupplierID);
                    employee = employeeService.GetEmployeesByID(serviceOutHeader.EmployeeID);

                    documentState = 0;
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

                    txtTotalAmount.Text = Common.ConvertDecimalToStringCurrency(serviceOutHeader.NetAmount);
                    txtTotalQty.Text = Common.ConvertDecimalToStringQty(serviceOutHeader.TotalQty);
                    txtRemark.Text = serviceOutHeader.Remark;

                    dgvItemDetails.DataSource = null;
                    serviceInDetailTempList = serviceInService.getServiceOutDetailForServiceIn(serviceOutHeader);
                    dgvItemDetails.DataSource = serviceInDetailTempList;
                    dgvItemDetails.Refresh();

                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtEmployeeName, txtEmployeeCode, txtDocumentNo, txtServiceOut);
                    Common.EnableComboBox(false, cmbLocation);

                    grpFooter.Enabled = true;
                    EnableLine(false);
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

                    ActiveControl = dtpServiceInDate;
                    dtpServiceInDate.Focus();

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

        private bool RecallDocument(string documentNo)
        {
            try
            {
                ServiceInService serviceInService = new ServiceInService();
                ServiceInHeader serviceInHeader = new ServiceInHeader();
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                LgsSupplier lgsSupplier = new LgsSupplier();
                EmployeeService employeeService = new EmployeeService();
                Employee employee = new Employee();

                serviceInHeader = serviceInService.GetPausedServiceInHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (serviceInHeader != null)
                {
                    lgsSupplier = lgsSupplierService.GetLgsSupplierByID(serviceInHeader.SupplierID);
                    employee = employeeService.GetEmployeesByID(serviceInHeader.EmployeeID);

                    cmbLocation.SelectedValue = serviceInHeader.LocationID;
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

                    documentState = serviceInHeader.DocumentStatus;

                    dtpServiceInDate.Value = Common.FormatDate(serviceInHeader.DocumentDate);

                    ServiceOutHeader serviceOutHeader = new ServiceOutHeader();
                    ServiceOutService serviceOutService = new ServiceOutService();
                    serviceOutHeader = serviceOutService.RecallServiceOutHeaderToServiceIn(serviceInHeader.ServiceOutHeaderID);
                    if (serviceOutHeader != null) { txtServiceOut.Text = serviceOutHeader.DocumentNo; }
                    else { txtServiceOut.Text = string.Empty; }
                    
                    txtDocumentNo.Text = serviceInHeader.DocumentNo;
                    txtTotalAmount.Text = Common.ConvertDecimalToStringCurrency(serviceInHeader.NetAmount);
                    txtTotalQty.Text = Common.ConvertDecimalToStringQty(serviceInHeader.TotalQty);
                    txtRemark.Text = serviceInHeader.Remark;

                    dgvItemDetails.DataSource = null;
                    serviceInDetailTempList = serviceInService.GetPausedServiceInDetail(serviceInHeader);
                    dgvItemDetails.DataSource = serviceInDetailTempList;
                    dgvItemDetails.Refresh();



                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtEmployeeName, txtEmployeeCode, txtDocumentNo, txtServiceOut);
                    Common.EnableComboBox(false, cmbLocation);

                    if (serviceInHeader.DocumentStatus.Equals(0))
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

        private void EnableProductDetails(bool state)
        {
            txtProductCode.Enabled = state;
            txtProductName.Enabled = state;
        }

        private void dgvItemDetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvItemDetails.CurrentCell.RowIndex >= 0)
                {
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
                    ServiceInService serviceInService = new ServiceInService();
                    if (serviceInDetailTempList == null)
                        serviceInDetailTempList = new List<ServiceInDetailTemp>();
                    existingServiceInDetailTemp = serviceInService.getServiceInDetailTemp(serviceInDetailTempList, existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID);
                    if (existingServiceInDetailTemp != null)
                    {
                        txtProductCode.Text = existingServiceInDetailTemp.ProductCode;
                        txtProductName.Text = existingServiceInDetailTemp.ProductName;
                        cmbUnit.SelectedValue = existingServiceInDetailTemp.UnitOfMeasureID;
                        txtBatchNo.Text = existingServiceInDetailTemp.BatchNo;
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingServiceInDetailTemp.OrderQty);
                        txtBalQty.Text = Common.ConvertDecimalToStringQty(existingServiceInDetailTemp.BalancedQty);
                        txtRate.Text = Common.ConvertDecimalToStringCurrency(existingServiceInDetailTemp.CostPrice);
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
                ServiceOutHeader serviceOutHeader = new ServiceOutHeader();
                ServiceOutService serviceOutService = new ServiceOutService();
                ServiceOutDetailTemp serviceOutDetailTemp = new ServiceOutDetailTemp();
                LgsProductMaster lgsProductMaster = new LgsProductMaster();
                LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                serviceOutHeader = serviceOutService.GetServiceOutHeaderToServiceIn(txtServiceOut.Text.Trim());
                if (serviceOutHeader != null)
                {
                    lgsProductMaster = lgsProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());
                    serviceOutDetailTemp.ProductID = (lgsProductMaster.LgsProductMasterID);
                    serviceOutDetailTemp.UnitOfMeasureID = Convert.ToInt32(cmbUnit.SelectedValue);
                    serviceOutDetailTemp.ServiceOutHeaderID = serviceOutHeader.ServiceOutHeaderID;
                }
                if (!serviceOutService.IsValidNoOfQty(qty, serviceOutDetailTemp))
                {
                    Toast.Show("Invalid Qty.\nQty cannot grater then Service out Qty", Toast.messageType.Information, Toast.messageAction.General, "");
                    txtQty.Focus();
                }
                else
                {
                    txtProductAmount.Enabled = true;
                    txtProductAmount.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtRate_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtRate_Leave(object sender, EventArgs e)
        {
            try
            {
                txtProductAmount.Enabled = true;
                txtProductAmount.Focus();
                CalculateLine();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void UpdateGrid(ServiceInDetailTemp serviceInDetailTemp)
        {
            try
            {
                serviceInDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                if (isInvProduct) { serviceInDetailTemp.BaseUnitID = existingLgsProductMaster.UnitOfMeasureID; }
                else { serviceInDetailTemp.BaseUnitID = existingLgsProductMaster.UnitOfMeasureID; }

                serviceInDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();

                serviceInDetailTemp.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text);
                CalculateLine();

                serviceInDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtRate.Text);
                serviceInDetailTemp.NetAmount = Common.ConvertStringToDecimalCurrency(txtProductAmount.Text);

                ServiceInService serviceInService = new ServiceInService();

                dgvItemDetails.DataSource = null;
                serviceInDetailTempList = serviceInService.getUpdateServiceInDetailTemp(serviceInDetailTempList, serviceInDetailTemp);
                dgvItemDetails.DataSource = serviceInDetailTempList;
                dgvItemDetails.Refresh();

                GetSummarizeFigures(serviceInDetailTempList);
                EnableLine(false);
                Common.EnableComboBox(false, cmbLocation);
                if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                ClearLine();
                if (serviceInDetailTempList.Count > 0)
                    grpFooter.Enabled = true;

                txtProductCode.Enabled = true;
                txtProductCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void GetSummarizeFigures(List<ServiceInDetailTemp> listItem)
        {
            decimal netAmount = listItem.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.NetAmount);
            decimal qty = listItem.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.OrderQty);
            netAmount = Common.ConvertStringToDecimalCurrency(netAmount.ToString());
            qty = Common.ConvertStringToDecimalQty(qty.ToString());

            txtTotalAmount.Text = Common.ConvertDecimalToStringCurrency(netAmount);
            txtTotalQty.Text = Common.ConvertDecimalToStringQty(qty);
        }

        public override void ClearForm()
        {
            errorProvider.Clear();
            dtpServiceInDate.Value = DateTime.Now;
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
                    UpdateGrid(existingServiceInDetailTemp);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void RefreshDocumentNumber()
        {
            ServiceInService serviceInService = new ServiceInService();
            Common.SetAutoComplete(txtDocumentNo, serviceInService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
        }

        private void RefreshServiceOutDocumentNumber()
        {
            ServiceOutService serviceOutService = new ServiceOutService();
            Common.SetAutoComplete(txtServiceOut, serviceOutService.GetAllDocumentNumbersToServiceIn(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationServiceOut.Checked);
        }

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo);
        }

        public override void Pause()
        {
            if ((Toast.Show("Service In  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateControls().Equals(false)) { return; }
                if (SaveDocument(0, txtDocumentNo.Text.Trim()).Equals(true))
                {
                    Toast.Show("Service In  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    GenerateReport(txtDocumentNo.Text.Trim(), 0);
                    ClearForm();
                    RefreshDocumentNumber();
                    RefreshServiceOutDocumentNumber();
                }
                else
                {
                    Toast.Show("Service In  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        public override void Save()
        {
            if ((Toast.Show("Service In  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateControls().Equals(false)) { return; }
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim());
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Service In  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(txtDocumentNo.Text.Trim().Trim(), 1);
                    ClearForm();
                    RefreshDocumentNumber();
                    RefreshServiceOutDocumentNumber();
                }
                else
                {
                    Toast.Show("Service In  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        private bool SaveDocument(int documentStatus, string documentNo)
        {
            try
            {
                ServiceInService serviceInService = new ServiceInService();
                ServiceInHeader serviceInHeader = new ServiceInHeader();
                LocationService locationService = new LocationService();
                Location Location = new Location();
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                LgsSupplier lgsSupplier = new LgsSupplier();
                EmployeeService employeeService = new EmployeeService();
                Employee employee = new Employee();

                lgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());
                employee = employeeService.GetEmployeesByCode(txtEmployeeCode.Text.Trim());

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                serviceInHeader = serviceInService.GetPausedServiceInHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (serviceInHeader == null)
                    serviceInHeader = new ServiceInHeader();

                if (documentStatus.Equals(1)) // update paused document
                {
                    documentNo = GetDocumentNo(false);
                    txtDocumentNo.Text = documentNo;
                }

                serviceInHeader.CompanyID = Location.CompanyID;
                serviceInHeader.DocumentDate = Common.ConvertStringToDate(dtpServiceInDate.Value.ToString());
                serviceInHeader.DocumentID = documentID;
                serviceInHeader.DocumentStatus = documentStatus;
                serviceInHeader.DocumentNo = documentNo.Trim();
                serviceInHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                serviceInHeader.LocationID = Location.LocationID;
                serviceInHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtTotalAmount.Text.ToString());
                serviceInHeader.TotalQty = Common.ConvertStringToDecimalQty(txtTotalQty.Text.ToString());
                serviceInHeader.Remark = txtRemark.Text.Trim();

                if (lgsSupplier != null) { serviceInHeader.SupplierID = lgsSupplier.LgsSupplierID; }
                else { serviceInHeader.SupplierID = 0; }

                if (employee != null) { serviceInHeader.EmployeeID = employee.EmployeeID; }
                else { serviceInHeader.EmployeeID = 0; }

                ServiceOutService serviceOutService = new ServiceOutService();
                ServiceOutHeader existingServiceOutHeader = new ServiceOutHeader();
                existingServiceOutHeader = serviceOutService.GetServiceOutByDocumentNo(txtServiceOut.Text.Trim());

                if (existingServiceOutHeader != null) { serviceInHeader.ServiceOutHeaderID = existingServiceOutHeader.ServiceOutHeaderID; }
                else { serviceInHeader.ServiceOutHeaderID = 0; }

                if (serviceInDetailTempList == null)
                    serviceInDetailTempList = new List<ServiceInDetailTemp>();

                return serviceInService.Save(serviceInHeader, serviceInDetailTempList);

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

        private void cmbLocation_Validated(object sender, EventArgs e)
        {
            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbLocation.Text.Trim()).LocationID, documentID);
            if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }

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

    }
}
