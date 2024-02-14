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
namespace UI.Windows
{
    public partial class FrmMaterialConsumptionNote : UI.Windows.FrmBaseTransactionForm
    {
        /// <summary>
        /// Developed by asanka
        /// </summary>
        /// 

        List<LgsMaterialConsumptionDetailTemp> lgsMaterialConsumptionDetailsTemp = new List<LgsMaterialConsumptionDetailTemp>();
        int documentID;

        public FrmMaterialConsumptionNote()
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

                chkAutoCompleationDocumentNo.Checked = true;
                chkAutoCompleationProduct.Checked = true;
                InitializeForm();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtReferenceNo); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtDocumentNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDocumentNo.Text.Trim()))
                { return; }

                LgsMaterialConsumptionService lgsMaterialConsumptionService = new LgsMaterialConsumptionService();
                if (lgsMaterialConsumptionService.GetPausedDocumentDetailsByDocumentNumber(txtDocumentNo.Text.Trim()) == null)
                {
                    Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                }
                else
                {
                    LoadPausedDocument(lgsMaterialConsumptionService.GetPausedDocumentDetailsByDocumentNumber(txtDocumentNo.Text.Trim()));
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtReferenceNo); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtRemark_Validated(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtReferenceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, dtpConsumptionDate); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtReferenceNo_Validated(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dtpConsumptionDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, cmbLocation); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dtpConsumptionDate_Validated(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtProductCode); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbLocation_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbLocation.Text.Trim()))
                { return; }

                if (!IsLocationExistsByName(cmbLocation.Text.Trim()))
                {
                    cmbLocation.Focus();
                    return;
                }
            }
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

        private void txtProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                cmbUnit.Enabled = true;
                Common.SetFocus(e, cmbUnit);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!IsLocationExistsByName(cmbLocation.Text.Trim()))
                {
                    cmbLocation.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtProductCode.Text.Trim()))
                {
                    EnableProductDetailControls(false);
                    return;
                }

                this.cmbUnit.SelectedIndexChanged -= new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
                if (!IsProductExistsByCode(txtProductCode.Text.Trim(), 0))
                {
                    txtProductCode.Focus();
                    EnableProductDetailControls(false);
                    return;
                }
                else
                { EnableProductDetailControls(true); }
                this.cmbUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                cmbUnit.Enabled = true;
                Common.SetFocus(e, cmbUnit);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductName_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!IsLocationExistsByName(cmbLocation.Text.Trim()))
                {
                    cmbLocation.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtProductName.Text.Trim()))
                {
                    EnableProductDetailControls(false);
                    return;
                }

                this.cmbUnit.SelectedIndexChanged -= new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
                if (!IsProductExistsByName(txtProductName.Text.Trim(), 0))
                {
                    txtProductName.Focus();
                    EnableProductDetailControls(false);
                    return;
                }
                else
                { EnableProductDetailControls(true); }
                this.cmbUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtQty); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbUnit.SelectedIndex >= 0)
                {
                    if (!IsProductExistsByCode(txtProductCode.Text, long.Parse(cmbUnit.SelectedValue.ToString().Trim())))
                    { return; }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbUnit_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!IsUnitOfMeasureExistsByID(long.Parse(cmbUnit.SelectedValue.ToString())))
                { return; }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                { AssignProductProperties(); }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtQty_Validated(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtRate_Validated(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductAmount_Validated(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void FrmMaterialConsumptionNote_Activated(object sender, EventArgs e)
        {
            try
            {
                //Common.SetFormOpacity(this, true); 
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void FrmMaterialConsumptionNote_Deactivate(object sender, EventArgs e)
        {
            try
            {
                //Common.SetFormOpacity(this, false); 
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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

            // Load Cost centers
            CostCentreService costCentreService = new CostCentreService();
            Common.LoadCostCenters(cmbCostCentre, costCentreService.GetAllCostCentres());

            // Disable product details controls
            EnableProductDetailControls(false);

            // Set Read only columns of Data Grid View
            Common.SetDataGridviewColumnsReadOnly(true, dgvItemDetails, dgvItemDetails.Columns[4]);
            //Common.ReadOnlyTextBox(true, );

            txtDocumentNo.Text = GetDocumentNo(true);

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
                Common.ClearTextBox(txtQty, txtRate, txtProductAmount);
            }
            Common.EnableComboBox(enabled, cmbUnit);
            Common.EnableTextBox(enabled, txtQty, txtRate, txtProductAmount);
        }

        /// <summary>
        /// Get a Document number
        /// </summary>
        /// <param name="isTemporytNo"></param>
        /// <returns></returns>
        private string GetDocumentNo(bool isTemporytNo)
        {
            LgsMaterialConsumptionService lgsMaterialConsumptionService = new LgsMaterialConsumptionService();
            LocationService locationService = new LocationService();
            return lgsMaterialConsumptionService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
        }

        /// <summary>
        /// Load and bind products into text boxes        
        /// </summary>
        private void LoadProducts()
        {
            InvProductMasterService invProductMasterService = new InvProductMasterService();
            Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
            Common.SetAutoComplete(txtProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
        }

        /// <summary>
        /// Assign product properties before adding into grid view
        /// </summary>
        private void AssignProductProperties()
        {
            LgsMaterialConsumptionService lgsMaterialConsumptionService = new LgsMaterialConsumptionService();
            LgsMaterialConsumptionDetailTemp lgsMaterialConsumptionDetailTemp = new LgsMaterialConsumptionDetailTemp();
            InvProductMasterService invProductMasterService = new InvProductMasterService();
            InvProductMaster invProductMaster = new InvProductMaster();
            CommonService commonService = new CommonService();

            invProductMaster = invProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

            lgsMaterialConsumptionDetailTemp.ProductID = invProductMaster.InvProductMasterID;
            lgsMaterialConsumptionDetailTemp.ProductCode = invProductMaster.ProductCode;
            lgsMaterialConsumptionDetailTemp.ProductName = invProductMaster.ProductName;
            lgsMaterialConsumptionDetailTemp.CostPrice = invProductMaster.CostPrice;

            lgsMaterialConsumptionDetailTemp.UnitOfMeasure = cmbUnit.Text.Trim();
            lgsMaterialConsumptionDetailTemp.UnitOfMeasureID = long.Parse(cmbUnit.SelectedValue.ToString().Trim());
            lgsMaterialConsumptionDetailTemp.Qty = Common.ConvertStringToDecimalQty(txtQty.Text.Trim());
            lgsMaterialConsumptionDetailTemp.GrossAmount = Common.ConvertStringToDecimalCurrency((lgsMaterialConsumptionDetailTemp.Qty * lgsMaterialConsumptionDetailTemp.CostPrice).ToString());

            lgsMaterialConsumptionDetailTemp.DocumentDate = DateTime.UtcNow;

            lgsMaterialConsumptionDetailsTemp = lgsMaterialConsumptionService.GetLgsMaterialConsumptionDetailTempList(lgsMaterialConsumptionDetailsTemp, lgsMaterialConsumptionDetailTemp);
            UpdatedgvItemDetails();

        }

        /// <summary>
        /// Recal Paused document
        /// </summary>
        /// <param name="existingLgsMaterialConsumptionHeader"></param>
        private void LoadPausedDocument(LgsMaterialConsumptionHeader existingLgsMaterialConsumptionHeader)
        {
            txtDocumentNo.Text = existingLgsMaterialConsumptionHeader.DocumentNo;

            cmbLocation.SelectedValue = existingLgsMaterialConsumptionHeader.LocationID;
            dtpConsumptionDate.Value = existingLgsMaterialConsumptionHeader.DocumentDate;
            cmbCostCentre.SelectedValue = existingLgsMaterialConsumptionHeader.CostCentreID;
            txtReferenceNo.Text = existingLgsMaterialConsumptionHeader.ReferenceDocumentNo;
            txtRemark.Text = existingLgsMaterialConsumptionHeader.Remark.Trim();

            LgsMaterialConsumptionService lgsMaterialConsumptionService = new LgsMaterialConsumptionService();
            lgsMaterialConsumptionDetailsTemp = lgsMaterialConsumptionService.GetLgsMaterialConsumptionDetails(existingLgsMaterialConsumptionHeader);
            UpdatedgvItemDetails();
        }

        /// <summary>
        /// Update products grid view
        /// </summary>
        private void UpdatedgvItemDetails()
        {
            dgvItemDetails.DataSource = null;
            dgvItemDetails.DataSource = lgsMaterialConsumptionDetailsTemp.OrderBy(pr => pr.LineNo).ToList();
            GetSummarizeFigures(lgsMaterialConsumptionDetailsTemp.ToList());
            if (dgvItemDetails.Rows.Count > 0)
            { dgvItemDetails.FirstDisplayedScrollingRowIndex = dgvItemDetails.RowCount - 1; }
            else
            { Common.EnableComboBox(true, cmbLocation); }

            EnableProductDetailControls(false);
            Common.ClearTextBox(txtProductCode, txtProductName);
            txtProductCode.Focus();
        }

        /// <summary>
        /// Update Total Amount
        /// </summary>
        /// <param name="Productlist"></param>
        private void GetSummarizeFigures(List<LgsMaterialConsumptionDetailTemp> Productlist)
        {
            txtTotalAmount.Text = Productlist.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.OrderQty).ToString();
        }

        /// <summary>
        /// Pause Material Consumption Note.
        /// </summary>
        public override void Pause()
        {
            if ((Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                if (!IsLocationExistsByName(cmbLocation.Text.Trim())) { return; }
                if (SaveDocument(0, txtDocumentNo.Text.Trim()).Equals(true))
                {
                    Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
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
            GetSummarizeFigures(lgsMaterialConsumptionDetailsTemp);

            LgsMaterialConsumptionService lgsMaterialConsumptionService = new LgsMaterialConsumptionService();
            LgsMaterialConsumptionHeader lgsMaterialConsumptionHeader = new LgsMaterialConsumptionHeader();
            Location Location = new Location();
            LocationService locationService = new LocationService();

            Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

            if (lgsMaterialConsumptionService.GetLgsMaterialConsumptionHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Location.LocationID) != null)
            {
                lgsMaterialConsumptionHeader.LgsMaterialConsumptionHeaderID = lgsMaterialConsumptionService.GetLgsMaterialConsumptionHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Location.LocationID).LgsMaterialConsumptionHeaderID;
            }

            if (documentStatus.Equals(1))
            {
                documentNo = GetDocumentNo(false);
                txtDocumentNo.Text = documentNo;
            }

            lgsMaterialConsumptionHeader.DocumentStatus = documentStatus;
            lgsMaterialConsumptionHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
            lgsMaterialConsumptionHeader.CompanyID = Location.CompanyID;
            lgsMaterialConsumptionHeader.LocationID = Location.LocationID;
            lgsMaterialConsumptionHeader.DocumentID = documentID;
            lgsMaterialConsumptionHeader.DocumentNo = documentNo.Trim();
            lgsMaterialConsumptionHeader.DocumentDate = dtpConsumptionDate.Value;
            //lgsMaterialRequestHeader.CostCentreID = int.Parse(cmbCostCentre.SelectedValue.ToString());

            lgsMaterialConsumptionHeader.ReferenceDocumentNo = txtReferenceNo.Text.Trim();
            lgsMaterialConsumptionHeader.Remark = txtRemark.Text.Trim();
            lgsMaterialConsumptionHeader.CreatedUser = Common.LoggedUser;
            lgsMaterialConsumptionHeader.CreatedDate = DateTime.UtcNow;
            lgsMaterialConsumptionHeader.ModifiedUser = Common.LoggedUser;
            lgsMaterialConsumptionHeader.ModifiedDate = DateTime.UtcNow;
            lgsMaterialConsumptionHeader.DataTransfer = 0;

            return lgsMaterialConsumptionService.SaveMaterialConsumptionNote(lgsMaterialConsumptionHeader, lgsMaterialConsumptionDetailsTemp);

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

        // Confirm Location by Code
        private bool IsLocationExistsByCode(string locationCode)
        {
            bool recodFound = false;
            LocationService locationService = new LocationService();
            Location location = new Location();
            location = locationService.GetLocationsByCode(locationCode);
            if (location != null)
            {
                recodFound = true;
                cmbLocation.Text = location.LocationCode;
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
            InvProductMasterService invProductMasterService = new InvProductMasterService();
            InvProductMaster invProductMaster = new InvProductMaster();
            invProductMaster = invProductMasterService.GetProductsByCode(productCode);

            if (invProductMaster != null)
            {
                recodFound = true;
                txtProductCode.Text = invProductMaster.ProductCode;
                txtProductName.Text = invProductMaster.ProductName;
                txtRate.Text = invProductMaster.CostPrice.ToString();
                if (invProductMaster.UnitOfMeasureID.Equals(unitOfMeasureID) || unitOfMeasureID.Equals(0))
                {
                    cmbUnit.SelectedValue = invProductMaster.UnitOfMeasureID;
                }
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtProductCode, txtProductName, txtRate);
                Toast.Show("Product Code" + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }

            return recodFound;
        }

        // Confirm Product by Name
        private bool IsProductExistsByName(string productName, long unitOfMeasureID)
        {
            bool recodFound = false;
            InvProductMasterService invProductMasterService = new InvProductMasterService();
            InvProductMaster invProductMaster = new InvProductMaster();

            invProductMaster = invProductMasterService.GetProductsByName(productName);
            if (invProductMaster != null)
            {
                recodFound = true;
                txtProductCode.Text = invProductMaster.ProductCode;
                txtProductName.Text = invProductMaster.ProductName;
                txtRate.Text = invProductMaster.CostPrice.ToString();
                if (invProductMaster.UnitOfMeasureID.Equals(unitOfMeasureID) || unitOfMeasureID.Equals(0))
                {
                    cmbUnit.SelectedValue = invProductMaster.UnitOfMeasureID;
                }
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtProductCode, txtProductName, txtRate);
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

        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (Common.ConvertStringToDecimalQty(txtQty.Text) > 0)
            {

            }
            else
            {
                Toast.Show("Qty", Toast.messageType.Information, Toast.messageAction.ZeroQty);

                txtQty.Focus();
                txtQty.SelectAll();
                return;
            }
        }

        #endregion


    }
}
