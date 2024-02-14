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
using Report.Inventory;
using Report.Logistic;

namespace UI.Windows
{
    /// <summary>
    /// Developed by asanka
    /// </summary>
    /// 
    public partial class FrmMaterialRequestNote : UI.Windows.FrmBaseTransactionForm
    {
        List<LgsMaterialRequestDetailTemp> lgsMaterialRequestDetailsTempList = new List<LgsMaterialRequestDetailTemp>();
        LgsMaterialRequestDetailTemp existingLgsMaterialRequestDetailTemp = new LgsMaterialRequestDetailTemp();   
        int documentID;
        LgsProductMaster existingLgsProductMaster = new LgsProductMaster();
        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;

        public FrmMaterialRequestNote()
        {
            InitializeComponent();
        }

        #region Form Events

        public override void FormLoad()
        {
            try
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                documentID = autoGenerateInfo.DocumentID;
                this.Text = autoGenerateInfo.FormText.Trim();

                LoadPausedDocuments();

                chkAutoCompleationMrnNo.Checked = true;                                
                chkAutoCompleationProduct.Checked = true;
                InitializeForm();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoCompleationMrnNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            { LoadPausedDocuments(); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }        
        }

        private void chkAutoCompleationProduct_CheckedChanged(object sender, EventArgs e)
        {
            try
            { LoadProducts(); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtReferenceNo);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtDocumentNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDocumentNo.Text.Trim()))
                { return; }

                LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();
                if (lgsMaterialRequestService.GetPausedDocumentDetailsByDocumentNumber(txtDocumentNo.Text.Trim()) == null)
                {
                    Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                }
                else
                {
                    LoadPausedDocument(lgsMaterialRequestService.GetPausedDocumentDetailsByDocumentNumber(txtDocumentNo.Text.Trim()));
                    Common.EnableComboBox(false, cmbLocation);
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }
       
        private void txtReferenceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtRemark); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dtpDeliveryDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, dtpDocumentDate); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dtpDocumentDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, cmbLocation); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, dtpDeliveryDate); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtProductCode.Enabled = true;
                    txtProductCode.Focus();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbLocation_Validated(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(cmbLocation.Text.Trim()))
                //{ return; }

                if (!IsLocationExistsByName(cmbLocation.Text.Trim()))
                {
                    Toast.Show("Location ", Toast.messageType.Warning, Toast.messageAction.NotExists);
                    cmbLocation.Focus();
                    return;
                }
                else
                {
                    LoadProducts();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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

        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (!txtProductName.Text.Trim().Equals(string.Empty))
                    {
                        cmbUnit.Enabled = true;
                        cmbUnit.Focus();
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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


        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtCostPrice.Enabled = true;
                    txtCostPrice.Focus();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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

                        LgsMaterialRequestDetailTemp lgsMaterialRequestDetailTemp = new LgsMaterialRequestDetailTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                        lgsMaterialRequestDetailTemp.ProductID = lgsProductMasterService.GetProductsByCode(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).LgsProductMasterID;
                        lgsMaterialRequestDetailTemp.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["UnitOfMeasure", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                        LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();

                        dgvItemDetails.DataSource = null;
                        lgsMaterialRequestDetailsTempList = lgsMaterialRequestService.GetDeleteLgsMaterialRequestDetailTemp(lgsMaterialRequestDetailsTempList, lgsMaterialRequestDetailTemp);
                        dgvItemDetails.DataSource = lgsMaterialRequestDetailsTempList;
                        dgvItemDetails.Refresh();

                        GetSummarizeFigures(lgsMaterialRequestDetailsTempList);

                        if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                        else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }

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

        private void FrmMaterialRequestNote_Activated(object sender, EventArgs e)
        {
            try
            { Common.SetFormOpacity(this, true); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void FrmMaterialRequestNote_Deactivate(object sender, EventArgs e)
        {
            try
            { Common.SetFormOpacity(this, false); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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
                        loadProductDetails(true, dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim(), unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["UnitOfMeasure", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID);

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

        public void EnableLine(bool status)
        {
            Common.EnableTextBox(status, txtProductCode, txtProductName, txtQty, txtCostPrice, txtProductAmount);
            Common.EnableComboBox(status, cmbUnit);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Setup GUI contols (Set visibility, data binding, auto loading)
        /// </summary>
        public override void InitializeForm()
        {
            // Load Locations            
            LocationService locationService = new LocationService();
            Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
            cmbLocation.SelectedValue = Common.LoggedLocationID;

            // Load Unit of measures
            this.cmbUnit.SelectedIndexChanged -= new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
            UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
            Common.LoadUnitOfMeasures(cmbUnit, unitOfMeasureService.GetAllUnitOfMeasures());
            this.cmbUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);

            Common.EnableButton(false, btnSave, btnPause, btnView);

            // Load Cost centers
            CostCentreService costCentreService = new CostCentreService();
            Common.LoadCostCenters(cmbCostCentre, costCentreService.GetAllCostCentres());

            // Disable product details controls
            EnableProductDetailControls(false);
            Common.EnableTextBox(false, txtProductName, txtProductCode);
            Common.EnableComboBox(true, cmbLocation);

            EnableLine(false);

            // Set Read only columns of Data Grid View
            Common.SetDataGridviewColumnsReadOnly(true, dgvItemDetails, dgvItemDetails.Columns[4]);
            //Common.ReadOnlyTextBox(true, );

            txtDocumentNo.Text = GetDocumentNo(true);

            selectedRowIndex = 0;
            isUpdateGrid = false;
            rowCount = 0;

            this.ActiveControl = txtReferenceNo;
            txtReferenceNo.Focus();
        }

        /// <summary>
        /// Enable or disable product details controls
        /// </summary>
        /// <param name="enabled"></param>
        private void EnableProductDetailControls(bool enabled)
        {
            if (!enabled)
            {
                Common.ClearComboBox(cmbUnit);
                Common.ClearTextBox(txtQty);
            }
            Common.EnableComboBox(enabled, cmbUnit);
            Common.EnableTextBox(enabled, txtQty);
        }

        /// <summary>
        /// Get a Document number
        /// </summary>
        /// <param name="isTemporytNo"></param>
        /// <returns></returns>
        private string GetDocumentNo(bool isTemporytNo)
        {
            LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();
            LocationService locationService = new LocationService();
            return lgsMaterialRequestService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
        }

        /// <summary>
        /// Load and bind products into text boxes
        /// </summary>
        private void LoadProducts()
        {
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();            
            Common.SetAutoComplete(txtProductCode, lgsProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
            Common.SetAutoComplete(txtProductName, lgsProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
        }

        /// <summary>
        /// Update Total Qty
        /// </summary>
        /// <param name="Productlist"></param>
        private void GetSummarizeFigures(List<LgsMaterialRequestDetailTemp> Productlist)
        {
            txtTotalQty.Text = Productlist.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.OrderQty).ToString();
            txtTotalAmount.Text = Productlist.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.NetAmount).ToString();
        }

        private void CalculateLine()
        {
            txtProductAmount.Text = ((Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim())) * (Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()))).ToString();
        }

        /// <summary>
        /// Pause Material request Note.
        /// </summary>
        public override void Pause()
        {
            if ((Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                if (!IsLocationExistsByName(cmbLocation.Text.Trim())) { return; }
                if (SaveDocument(0, txtDocumentNo.Text.Trim()).Equals(true))
                {
                    Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    GenerateReport(txtDocumentNo.Text.Trim(), 0);
                    ClearForm();
                }
                else
                {
                    Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }

        }

        /// <summary>
        /// Save New Material request Note.
        /// </summary>
        public override void Save()
        {
            if (!ValidateControls())
            { return; }

            if ((Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {            
                if (!IsLocationExistsByName(cmbLocation.Text.Trim())) { return; }
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim());
                if (saveDocument.Equals(true))
                {
                    Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(txtDocumentNo.Text.Trim().Trim(), 1);
                    ClearForm();
                }
                else
                {
                    Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        /// <summary>
        ///  Save data into DB
        /// </summary>
        /// <param name="documentStatus"></param>
        /// <param name="documentNo"></param>
        /// <returns></returns>
        private bool SaveDocument(int documentStatus, string documentNo)
        {
            GetSummarizeFigures(lgsMaterialRequestDetailsTempList);

            LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();
            LgsMaterialRequestHeader lgsMaterialRequestHeader = new LgsMaterialRequestHeader();
            Location Location = new Location();
            LocationService locationService = new LocationService();
            
            Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

            lgsMaterialRequestHeader = lgsMaterialRequestService.GetPausedLgsMaterialRequestHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
            if (lgsMaterialRequestHeader == null)
            { lgsMaterialRequestHeader = new LgsMaterialRequestHeader(); }
            
            //if (lgsMaterialRequestService.GetLgsMaterialRequestHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Location.LocationID) != null)
            //{
            //    lgsMaterialRequestHeader.LgsMaterialRequestHeaderID = lgsMaterialRequestService.GetLgsMaterialRequestHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Location.LocationID).LgsMaterialRequestHeaderID;
            //}
            
            if (documentStatus.Equals(1))
            {
                documentNo = GetDocumentNo(false);
                txtDocumentNo.Text = documentNo;
            }

            lgsMaterialRequestHeader.DocumentStatus = documentStatus;
            lgsMaterialRequestHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
            lgsMaterialRequestHeader.CompanyID = Location.CompanyID;
            lgsMaterialRequestHeader.LocationID = Location.LocationID;
            lgsMaterialRequestHeader.DocumentID = documentID;
            lgsMaterialRequestHeader.DocumentNo = documentNo.Trim();
            lgsMaterialRequestHeader.DocumentDate = dtpDocumentDate.Value;
            lgsMaterialRequestHeader.DiliveryDate = dtpDeliveryDate.Value;
            //lgsMaterialRequestHeader.CostCentreID = int.Parse(cmbCostCentre.SelectedValue.ToString());
            lgsMaterialRequestHeader.ValidityPeriod = Common.ConvertStringToInt(txtValidityPeriod.Text.Trim());
            lgsMaterialRequestHeader.ExpiryDate = Common.FormatDate(Common.FormatDate(dtpDocumentDate.Value).AddDays(Convert.ToDouble(txtValidityPeriod.Text.Trim())));

            lgsMaterialRequestHeader.RequestedBy = Common.LoggedUser;
            lgsMaterialRequestHeader.ReferenceDocumentNo = txtReferenceNo.Text.Trim();
            lgsMaterialRequestHeader.Remark = txtRemark.Text.Trim();
            lgsMaterialRequestHeader.CreatedUser = lgsMaterialRequestHeader.LgsMaterialRequestHeaderID.Equals(0) ? Common.LoggedUser : lgsMaterialRequestHeader.CreatedUser;
            lgsMaterialRequestHeader.CreatedDate = lgsMaterialRequestHeader.LgsMaterialRequestHeaderID.Equals(0) ? DateTime.UtcNow : lgsMaterialRequestHeader.CreatedDate;
            lgsMaterialRequestHeader.ModifiedUser = Common.LoggedUser;
            lgsMaterialRequestHeader.ModifiedDate = DateTime.UtcNow;
            lgsMaterialRequestHeader.DataTransfer = 0;

            return lgsMaterialRequestService.SaveMaterialRequestNote(lgsMaterialRequestHeader, lgsMaterialRequestDetailsTempList);

        }

        /// <summary>
        /// Recal Paused document
        /// </summary>
        /// <param name="existingLgsMaterialRequestHeader"></param>
        private void LoadPausedDocument(LgsMaterialRequestHeader existingLgsMaterialRequestHeader)
        {
            txtDocumentNo.Text = existingLgsMaterialRequestHeader.DocumentNo;

            cmbLocation.SelectedValue = existingLgsMaterialRequestHeader.LocationID;
            dtpDocumentDate.Value = existingLgsMaterialRequestHeader.DocumentDate;
            dtpDeliveryDate.Value = existingLgsMaterialRequestHeader.DiliveryDate;
            cmbCostCentre.SelectedValue = existingLgsMaterialRequestHeader.CostCentreID;
            txtReferenceNo.Text = existingLgsMaterialRequestHeader.ReferenceDocumentNo;
            txtRemark.Text = existingLgsMaterialRequestHeader.Remark.Trim();

            LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();
            lgsMaterialRequestDetailsTempList = lgsMaterialRequestService.GetLgsMaterialRequestDetails(existingLgsMaterialRequestHeader);

            dgvItemDetails.DataSource = null;
            dgvItemDetails.DataSource = lgsMaterialRequestDetailsTempList;
            dgvItemDetails.Refresh();
        }

        private bool ValidateControls()
        {
            if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo))
            { return false; }
            else if (!Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbLocation))
            { return false; }
            else
            { return true; }
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

        public override void ClearForm()
        {
            base.ClearForm();
            lgsMaterialRequestDetailsTempList.Clear();
            dgvItemDetails.DataSource = null;
            dgvItemDetails.Refresh();
            ResetDates();
            ResetAutoCompleteControlData();
        }

        private void ResetDates()
        {
            dtpDocumentDate.Value = DateTime.Now;
            dtpDeliveryDate.Value = DateTime.Now;
        }


        /// <summary>
        /// Reload auto complete data lists
        /// </summary>
        private void ResetAutoCompleteControlData()
        {
            if (chkAutoCompleationMrnNo.Checked)
            {
                LoadPausedDocuments();
            }
        
        }

        #region Confirm Existing Property Values

        // Confirm Location by Name
        private bool IsLocationExistsByName(string locationName)
        {
            bool recodFound = false;
            LocationService locationService = new LocationService();
            Location location = new Location();
            location = locationService.GetLocationsByName(locationName);
            if (location != null)
            {
                recodFound = true;
                cmbLocation.Text = location.LocationName;
            }
            else
            {
                recodFound = false;
                Common.ClearComboBox(cmbLocation);
                Toast.Show(lblLocation.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }

            return recodFound;
        }

        // Confirm Product by Code
        private bool IsProductExistsByCode(string productCode, long unitOfMeasureID)
        {
            bool recodFound = false;
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
            LgsProductMaster lgsProductMaster = new LgsProductMaster();
            lgsProductMaster = lgsProductMasterService.GetProductsByCode(productCode);

            if (lgsProductMaster != null)
            {
                recodFound = true;
                txtProductCode.Text = lgsProductMaster.ProductCode;
                txtProductName.Text = lgsProductMaster.ProductName;
                if (lgsProductMaster.UnitOfMeasureID.Equals(unitOfMeasureID) || unitOfMeasureID.Equals(0))
                {
                    cmbUnit.SelectedValue = lgsProductMaster.UnitOfMeasureID;                   
                }                
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtProductCode, txtProductName);
                Toast.Show("Product Code" + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }

            return recodFound;
        }

        // Confirm Product by Name
        private bool IsProductExistsByName(string productName, long unitOfMeasureID)
        {
            bool recodFound = false;
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
            LgsProductMaster lgsProductMaster = new LgsProductMaster();

            lgsProductMaster = lgsProductMasterService.GetProductsByName(productName);
            if (lgsProductMaster != null)
            {
                recodFound = true;
                txtProductCode.Text = lgsProductMaster.ProductCode;
                txtProductName.Text = lgsProductMaster.ProductName;
                if (lgsProductMaster.UnitOfMeasureID.Equals(unitOfMeasureID) || unitOfMeasureID.Equals(0))
                {
                    cmbUnit.SelectedValue = lgsProductMaster.UnitOfMeasureID;
                }
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtProductCode, txtProductName);
                Toast.Show("Product Name" + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }

            return recodFound;
        }

        // Confirm Unit of Measure By ID
        private bool IsUnitOfMeasureExistsByID(long unitOfMeasureID)
        {
            bool recodFound = false;
            UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
            UnitOfMeasure unitOfMeasure = new UnitOfMeasure();
            unitOfMeasure = unitOfMeasureService.GetUnitOfMeasureById(unitOfMeasureID);
            if (unitOfMeasure != null)
            {
                recodFound = true;
                cmbUnit.Text = unitOfMeasure.UnitOfMeasureName;
            }
            else
            {
                recodFound = false;
                Common.ClearComboBox(cmbUnit);
                Toast.Show("Unit of measure" + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }

            return recodFound;
        }

        #endregion

        private void LoadPausedDocuments()
        {
            LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();
            Common.SetAutoComplete(txtDocumentNo, lgsMaterialRequestService.GetPausedDocumentNumbers(), chkAutoCompleationMrnNo.Checked);
        }

        #endregion

        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            {
                CalculateLine();
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
                if (!string.IsNullOrEmpty(txtProductCode.Text.Trim()))
                {
                    loadProductDetails(true, txtProductCode.Text.Trim(), 0);
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

                LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                if (isCode)
                {
                    existingLgsProductMaster = lgsProductMasterService.GetProductsByCode(strProduct);
                    if (isCode && strProduct.Equals(string.Empty))
                    {
                        txtProductName.Focus();
                        return;
                    }
                }
                else
                    existingLgsProductMaster = lgsProductMasterService.GetProductsByName(strProduct); ;

                if (existingLgsProductMaster != null)
                {
                    LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();
                    if (lgsMaterialRequestDetailsTempList == null)
                        lgsMaterialRequestDetailsTempList = new List<LgsMaterialRequestDetailTemp>();
                    existingLgsMaterialRequestDetailTemp = lgsMaterialRequestService.GetLgsMaterialRequestDetailTemp(lgsMaterialRequestDetailsTempList, existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID);
                    if (existingLgsMaterialRequestDetailTemp != null)
                    {
                        txtProductCode.Text = existingLgsMaterialRequestDetailTemp.ProductCode;
                        txtProductName.Text = existingLgsMaterialRequestDetailTemp.ProductName;
                        cmbUnit.SelectedValue = existingLgsMaterialRequestDetailTemp.UnitOfMeasureID;
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingLgsMaterialRequestDetailTemp.OrderQty);
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingLgsMaterialRequestDetailTemp.CostPrice);

                        Common.EnableComboBox(true, cmbUnit);
                        if (unitofMeasureID.Equals(0))
                            cmbUnit.Focus();
                    }
                }
                else
                {
                    Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    if (isCode) { txtProductCode.Focus(); }
                    else { txtProductName.Focus(); }
                    return;
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

        private void cmbUnit_Leave(object sender, EventArgs e)
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
                        cmbUnit.SelectedValue = existingLgsProductMaster.UnitOfMeasureID;
                        cmbUnit.Focus();
                        return;
                    }
                }
                loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()));

                txtQty.Enabled = true;
                if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                    txtQty.Text = "1";
                txtQty.Focus();
                txtQty.SelectAll();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void UpdateGrid(LgsMaterialRequestDetailTemp lgsMaterialRequestDetailTemp)
        {
            try
            {
                decimal qty = 0;

                if (Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) > 0)
                {
                    lgsMaterialRequestDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                    lgsMaterialRequestDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();

                    if (!chkOverwrite.Checked)
                    {
                        lgsMaterialRequestDetailTemp.OrderQty = Common.ConvertDecimalToDecimalQty(qty);
                    }
                    else
                    {
                        lgsMaterialRequestDetailTemp.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text.Trim());
                    }

                    lgsMaterialRequestDetailTemp.NetAmount = Common.ConvertStringToDecimalCurrency(txtProductAmount.Text.Trim());

                    LgsMaterialRequestService LgsMaterialRequestService = new LgsMaterialRequestService();

                    dgvItemDetails.DataSource = null;
                    lgsMaterialRequestDetailsTempList = LgsMaterialRequestService.GetUpdateLgsMaterialRequestDetailTemp(lgsMaterialRequestDetailsTempList, lgsMaterialRequestDetailTemp, existingLgsProductMaster);
                    dgvItemDetails.DataSource = lgsMaterialRequestDetailsTempList;
                    dgvItemDetails.Refresh();

                    GetSummarizeFigures(lgsMaterialRequestDetailsTempList);

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

                    Common.EnableComboBox(false, cmbLocation);
                    Common.EnableButton(true, btnPause, btnSave);

                    //if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    //if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

                    ClearLine();

                    if (lgsMaterialRequestDetailsTempList.Count > 0)
                    {
                        grpFooter.Enabled = true;
                    }

                    txtProductCode.Enabled = true;
                    txtProductCode.Focus();
                }
                else
                {
                    Toast.Show("Quantity", Toast.messageType.Information, Toast.messageAction.ZeroQty);
                    txtQty.SelectAll();
                    txtQty.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode, txtProductName, txtQty, txtCostPrice, txtProductAmount);
            Common.ClearComboBox(cmbUnit);
            txtProductCode.Focus();
        }

        private void txtProductAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    UpdateGrid(existingLgsMaterialRequestDetailTemp);
                    grpFooter.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCostPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
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

    }
}
