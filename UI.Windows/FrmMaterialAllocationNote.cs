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
using Report.Logistic;
namespace UI.Windows
{
    public partial class FrmMaterialAllocationNote : UI.Windows.FrmBaseTransactionForm
    {
        /// <summary>
        /// Developed by asanka
        /// </summary>
        /// 

        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        LgsProductMaster existingLgsProductMaster = new LgsProductMaster();

        List<LgsMaterialAllocationDetailTemp> lgsMaterialAllocationDetailsTempList = new List<LgsMaterialAllocationDetailTemp>();
        LgsMaterialAllocationDetailTemp existingLgsMaterialAllocationDetailTemp = new LgsMaterialAllocationDetailTemp();

        int documentID;

        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;
        string mrnNo;

        public FrmMaterialAllocationNote()
        {
            InitializeComponent();
        }

        public FrmMaterialAllocationNote(string documentNo)
        {
            InitializeComponent();
            mrnNo = documentNo;
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


                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }
                
                chkAutoCompleationDocumentNo.Checked = true;
                chkAutoCompleationProduct.Checked = true;
                chkAutoCompleationMJANo.Checked = true;
                chkAutoCompleationMRNNo.Checked = true;

                LoadPendingMaterialRequasitionNotes();

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

                LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();
                if (lgsMaterialAllocationService.GetPausedDocumentDetailsByDocumentNumber(txtDocumentNo.Text.Trim()) == null)
                {
                    Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                }
                else
                {
                    LoadPausedDocument(lgsMaterialAllocationService.GetPausedDocumentDetailsByDocumentNumber(txtDocumentNo.Text.Trim()));
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtJobAssignNoteNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtRemark); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtJobAssignNoteNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtJobAssignNoteNo.Text.Trim()))
                {
                    RecallMaintenanceJobAssignNote(txtJobAssignNoteNo.Text.Trim());
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, dtpAllocationDate); }
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
            { Common.SetFocus(e, txtRemark); }
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

        private void dtpAllocationDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, cmbLocation); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dtpAllocationDate_Validated(object sender, EventArgs e)
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
            { Common.SetFocus(e, cmbRequestLocationCode); }
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

        private void cmbRequestLocationCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtRequestNo); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbRequestLocationCode_Validated(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(cmbLocation.Text.Trim()))
                //{ return; }
                
                //if (!IsLocationExistsByCode(cmbLocation.Text.Trim()))
                //{
                //    cmbRequestLocationCode.Focus();
                //    return;
                //}
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtRequestNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                txtQty.Enabled = true;
                Common.SetFocus(e, txtQty); 
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtRequestNo_Validated(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtRate.Enabled = true;
                    txtRate.Focus(); 
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtRate_KeyDown(object sender, KeyEventArgs e)
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
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtRate_Validated(object sender, EventArgs e)
        {
            try
            {
                CalculateLine();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    //AssignProductProperties();
                    UpdateGrid(existingLgsMaterialAllocationDetailTemp);
                }
            }
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

        private void FrmMaterialAllocationNote_Activated(object sender, EventArgs e)
        {
            try
            { 
                //Common.SetFormOpacity(this, true); 
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void FrmMaterialAllocationNote_Deactivate(object sender, EventArgs e)
        {
            try
            {
                //Common.SetFormOpacity(this, false); 
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoCompleationDocumentNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoCompleationDocumentNo.Checked)
                { LoadPausedDocuments(); }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoCompleationMJANo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoCompleationMJANo.Checked)
                { LoadPendingAssignedJobs(); }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        public override void ClearForm()
        {
            base.ClearForm();
            lgsMaterialAllocationDetailsTempList.Clear();
            dgvItemDetails.DataSource = null;
            dgvItemDetails.Refresh();            
            ResetDates();
            ResetAutoCompleteControlData();
        }

        private void ResetDates()
        {
            dtpAllocationDate.Value = DateTime.Now;
        }

        /// <summary>
        /// Reload auto complete data lists
        /// </summary>
        private void ResetAutoCompleteControlData()
        {
            if (chkAutoCompleationDocumentNo.Checked)
            { LoadPausedDocuments(); }

            if (chkAutoCompleationMJANo.Checked)
            { LoadPendingAssignedJobs(); }

            if (chkAutoCompleationMRNNo.Checked)
            { LoadPendingMaterialRequasitionNotes(); }

        }

        private void chkAutoCompleationMRNNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoCompleationMRNNo.Checked)
                { LoadPendingMaterialRequasitionNotes(); }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }

        }


        private void txtMRNNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMRNNo.Text.Trim()))
                {
                    RecallMateriaRequasitionNote(txtMRNNo.Text.Trim());
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }

        }

        private void txtMRNNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtReferenceNo);
            }
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
                        loadProductDetails(true, dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim(), unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["unitOfMeasure", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID);
                        CalculateLine();

                        txtRequestNo.Enabled = true;

                        this.ActiveControl = txtRequestNo;
                        txtRequestNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void CalculateLine(decimal qty = 0)
        {
            txtProductAmount.Text = Common.ConvertDecimalToStringCurrency(Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()));
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
                    //////
                    LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();

                    if (lgsMaterialAllocationDetailsTempList == null)
                        lgsMaterialAllocationDetailsTempList = new List<LgsMaterialAllocationDetailTemp>();

                    existingLgsMaterialAllocationDetailTemp = lgsMaterialAllocationService.GetLgsMaterialAllocationDetailTemp(lgsMaterialAllocationDetailsTempList, existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID);

                    if (existingLgsMaterialAllocationDetailTemp != null)
                    {
                        txtProductCode.Text = existingLgsMaterialAllocationDetailTemp.ProductCode;
                        txtProductName.Text = existingLgsMaterialAllocationDetailTemp.ProductName;
                        cmbUnit.SelectedValue = existingLgsMaterialAllocationDetailTemp.UnitOfMeasureID;
                        txtRate.Text = Common.ConvertDecimalToStringCurrency(existingLgsMaterialAllocationDetailTemp.CostPrice);
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingLgsMaterialAllocationDetailTemp.Qty);

                        Common.EnableComboBox(true, cmbUnit);
                        if (unitofMeasureID.Equals(0))
                            cmbUnit.Focus();

                        if (chkViewStokDetails.Checked) { ShowStockDetails(existingLgsMaterialAllocationDetailTemp.ProductID); }        
                    }
                    /////
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

        private void UpdateGrid(LgsMaterialAllocationDetailTemp lgsMaterialAllocationDetailTemp)
        {
            try
            {
                //decimal qty = 0;

                if ((Common.ConvertStringToDecimalQty(txtQty.Text.Trim())) > 0)
                {
                    lgsMaterialAllocationDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                    lgsMaterialAllocationDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();


                    CalculateLine();
                    //lgsMaterialAllocationDetailTemp.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text.Trim());
                    lgsMaterialAllocationDetailTemp.Qty = Common.ConvertStringToDecimalQty(txtQty.Text.Trim());
                    lgsMaterialAllocationDetailTemp.GrossAmount = (Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()));
                    lgsMaterialAllocationDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtRate.Text);

                    lgsMaterialAllocationDetailTemp.GrossAmount = Common.ConvertStringToDecimalCurrency(txtProductAmount.Text.Trim());
                    lgsMaterialAllocationDetailTemp.RequestNo = txtRequestNo.Text.Trim();

                    LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();

                    dgvItemDetails.DataSource = null;
                    lgsMaterialAllocationDetailsTempList = lgsMaterialAllocationService.GetUpdateLgsMaterialAllocationDetailTemp(lgsMaterialAllocationDetailsTempList, lgsMaterialAllocationDetailTemp, existingLgsProductMaster);
                    dgvItemDetails.DataSource = lgsMaterialAllocationDetailsTempList;
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

                    GetSummarizeFigures(lgsMaterialAllocationDetailsTempList);
                    EnableLine(false);
                    Common.EnableTextBox(false, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation);
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    
                    ClearLine();

                    if (lgsMaterialAllocationDetailsTempList.Count > 0)
                    {
                        grpFooter.Enabled = true;
                    }

                    //txtProductCode.Enabled = true;
                    //txtProductCode.Focus();
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

        private void EnableLine(bool enable)
        {
            Common.EnableTextBox(enable, txtQty, txtRate, txtProductAmount, txtRequestNo);
            Common.EnableComboBox(enable, cmbUnit);
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode, txtProductName, txtQty, txtRate, txtProductAmount, txtRequestNo);
            Common.ClearComboBox(cmbUnit);
            txtProductCode.Focus();
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

            // Load Locations in to cmbRequestLocationCode
            Common.LoadLocationCodes(cmbRequestLocationCode, locationService.GetAllLocations());
            cmbLocation.SelectedValue = Common.LoggedLocationID;

            // Load Unit of measures
            this.cmbUnit.SelectedIndexChanged -= new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
            UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
            Common.LoadUnitOfMeasures(cmbUnit, unitOfMeasureService.GetAllUnitOfMeasures());
            this.cmbUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);

            // Load Cost centers
            CostCentreService costCentreService = new CostCentreService();
            Common.LoadCostCenters(cmbCostCentre, costCentreService.GetAllCostCentres());

            Common.EnableButton(false, btnSave, btnPause, btnView);

            // Disable product details controls
            EnableProductDetailControls(false);
            Common.EnableTextBox(false, txtProductCode, txtProductName);

            // Set Read only columns of Data Grid View
            Common.SetDataGridviewColumnsReadOnly(true, dgvItemDetails, dgvItemDetails.Columns[4]);
            //Common.ReadOnlyTextBox(true, );

            txtDocumentNo.Text = GetDocumentNo(true);

            Common.EnableTextBox(true, txtMRNNo, txtJobAssignNoteNo, txtReferenceNo, txtRemark);
            Common.EnableComboBox(true, cmbLocation);

            if (!string.IsNullOrEmpty(mrnNo))
            {
                txtMRNNo.Text = mrnNo;
                var e = new EventArgs();
                txtMRNNo_Validated(this, e);
            }

            dgvBatchStock.DataSource = null;

            selectedRowIndex = 0;
            isUpdateGrid = false;
            rowCount = 0;

            this.ActiveControl = txtMRNNo;
            txtMRNNo.Focus();
        }

        /// <summary>
        /// Enable or disable product details controls
        /// </summary>
        /// <param name="enabled"></param>
        private void EnableProductDetailControls(bool enabled)
        {
            if (!enabled)
            {
                Common.ClearComboBox(cmbUnit, cmbRequestLocationCode);
                Common.ClearTextBox(txtQty, txtRequestNo, txtRate, txtProductAmount);
            }
            Common.EnableComboBox(enabled, cmbUnit, cmbRequestLocationCode);
            Common.EnableTextBox(enabled, txtQty, txtRequestNo, txtRate, txtProductAmount);
        }

        /// <summary>
        /// Get a Document number
        /// </summary>
        /// <param name="isTemporytNo"></param>
        /// <returns></returns>
        private string GetDocumentNo(bool isTemporytNo)
        {
            LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();
            LocationService locationService = new LocationService();
            return lgsMaterialAllocationService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
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
        /// Assign product properties before adding into grid view
        /// </summary>
        private void AssignProductProperties()
        {
            LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();
            LgsMaterialAllocationDetailTemp lgsMaterialAllocationDetailTemp = new LgsMaterialAllocationDetailTemp();
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
            LgsProductMaster lgsProductMaster = new LgsProductMaster();
            CommonService commonService = new CommonService();

            lgsProductMaster = lgsProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

            lgsMaterialAllocationDetailTemp.ProductID = lgsProductMaster.LgsProductMasterID;
            lgsMaterialAllocationDetailTemp.ProductCode = lgsProductMaster.ProductCode;
            lgsMaterialAllocationDetailTemp.ProductName = lgsProductMaster.ProductName;
            lgsMaterialAllocationDetailTemp.CostPrice = lgsProductMaster.CostPrice;

            lgsMaterialAllocationDetailTemp.UnitOfMeasure = cmbUnit.Text.Trim();
            lgsMaterialAllocationDetailTemp.UnitOfMeasureID = long.Parse(cmbUnit.SelectedValue.ToString().Trim());
            lgsMaterialAllocationDetailTemp.RequestLocationID = int.Parse(cmbRequestLocationCode.SelectedValue.ToString().Trim());
            lgsMaterialAllocationDetailTemp.RequestLocationCode = cmbRequestLocationCode.Text.Trim();
            lgsMaterialAllocationDetailTemp.RequestNo = txtRequestNo.Text.Trim();
            lgsMaterialAllocationDetailTemp.Qty = Common.ConvertStringToDecimalQty(txtQty.Text.Trim());
            lgsMaterialAllocationDetailTemp.GrossAmount = Common.ConvertStringToDecimalCurrency((lgsMaterialAllocationDetailTemp.Qty * lgsMaterialAllocationDetailTemp.CostPrice).ToString());

            lgsMaterialAllocationDetailTemp.DocumentDate = DateTime.UtcNow;

            lgsMaterialAllocationDetailsTempList = lgsMaterialAllocationService.GetLgsMaterialAllocationDetailTempList(lgsMaterialAllocationDetailsTempList, lgsMaterialAllocationDetailTemp);
            UpdatedgvItemDetails();

        }

        /// <summary>
        /// Recal Paused document
        /// </summary>
        /// <param name="existingLgsMaterialAllocationHeader"></param>
        private void LoadPausedDocument(LgsMaterialAllocationHeader existingLgsMaterialAllocationHeader)
        { 
            txtDocumentNo.Text = existingLgsMaterialAllocationHeader.DocumentNo;

            cmbLocation.SelectedValue = existingLgsMaterialAllocationHeader.LocationID;
            dtpAllocationDate.Value = existingLgsMaterialAllocationHeader.DocumentDate;            
            cmbCostCentre.SelectedValue = existingLgsMaterialAllocationHeader.CostCentreID;
            txtReferenceNo.Text = existingLgsMaterialAllocationHeader.ReferenceDocumentNo;
            txtRemark.Text = existingLgsMaterialAllocationHeader.Remark.Trim();

            LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();
            lgsMaterialAllocationDetailsTempList = lgsMaterialAllocationService.GetLgsMaterialAllocationDetails(existingLgsMaterialAllocationHeader);
            UpdatedgvItemDetails();
        }

        /// <summary>
        /// Update products grid view
        /// </summary>
        private void UpdatedgvItemDetails()
        {
            dgvItemDetails.DataSource = null;
            dgvItemDetails.DataSource = lgsMaterialAllocationDetailsTempList.OrderBy(pr => pr.LineNo).ToList();
            GetSummarizeFigures(lgsMaterialAllocationDetailsTempList.ToList());
            if (dgvItemDetails.Rows.Count > 0)
            { 
                dgvItemDetails.FirstDisplayedScrollingRowIndex = dgvItemDetails.RowCount - 1;
                txtMRNNo.Enabled = false;
                txtJobAssignNoteNo.Enabled = false;
            }
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
        private void GetSummarizeFigures(List<LgsMaterialAllocationDetailTemp> Productlist)
        {
            txtTotalAmount.Text = Productlist.GetSummaryAmount(x => x.DocumentNo == txtMRNNo.Text.Trim(), x => x.GrossAmount).ToString();
            txtTotalQty.Text = Productlist.GetSummaryAmount(x => x.DocumentNo == txtMRNNo.Text.Trim(), x => x.Qty).ToString();
        }

        /// <summary>
        /// Pause Material Allocation Note.
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
                    GenerateReport(txtDocumentNo.Text.Trim(), 1);
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
            GetSummarizeFigures(lgsMaterialAllocationDetailsTempList);

            LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();
            LgsMaterialAllocationHeader lgsMaterialAllocationHeader = new LgsMaterialAllocationHeader();

            lgsMaterialAllocationHeader = lgsMaterialAllocationService.GetPausedMaterialAllocationHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
            if (lgsMaterialAllocationHeader == null)
            { lgsMaterialAllocationHeader = new LgsMaterialAllocationHeader(); }

            // Assign Reference Document details
            lgsMaterialAllocationHeader = GetReferenceDocumentId(lgsMaterialAllocationHeader);

            Location Location = new Location();
            LocationService locationService = new LocationService();
            Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

            //if (lgsMaterialAllocationService.GetLgsMaterialAllocationHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Location.LocationID) != null)
            //{
            //    lgsMaterialAllocationHeader.LgsMaterialAllocationHeaderID = lgsMaterialAllocationService.GetLgsMaterialAllocationHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Location.LocationID).LgsMaterialAllocationHeaderID;
            //}

            if (documentStatus.Equals(1))
            {
                documentNo = GetDocumentNo(false);
                txtDocumentNo.Text = documentNo;
            }

            lgsMaterialAllocationHeader.DocumentStatus = documentStatus;
            lgsMaterialAllocationHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
            lgsMaterialAllocationHeader.CompanyID = Location.CompanyID;
            lgsMaterialAllocationHeader.LocationID = Location.LocationID;
            lgsMaterialAllocationHeader.DocumentID = documentID;
            lgsMaterialAllocationHeader.DocumentNo = documentNo.Trim();
            lgsMaterialAllocationHeader.DocumentDate = dtpAllocationDate.Value;
            //lgsMaterialAllocationHeader.LgsMaintenanceJobAssignHeaderID = lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignHeaderID;
            //lgsMaterialRequestHeader.CostCentreID = int.Parse(cmbCostCentre.SelectedValue.ToString());

            lgsMaterialAllocationHeader.ReferenceDocumentNo = txtReferenceNo.Text.Trim();
            lgsMaterialAllocationHeader.Remark = txtRemark.Text.Trim();
            lgsMaterialAllocationHeader.CreatedUser = lgsMaterialAllocationHeader.LgsMaterialAllocationHeaderID.Equals(0) ? Common.LoggedUser : lgsMaterialAllocationHeader.CreatedUser;
            lgsMaterialAllocationHeader.CreatedDate = lgsMaterialAllocationHeader.LgsMaterialAllocationHeaderID.Equals(0) ? DateTime.UtcNow : lgsMaterialAllocationHeader.CreatedDate;
            lgsMaterialAllocationHeader.ModifiedUser =  Common.LoggedUser;
            lgsMaterialAllocationHeader.ModifiedDate = DateTime.UtcNow;
            lgsMaterialAllocationHeader.DataTransfer = 0;

            return lgsMaterialAllocationService.SaveMaterialAllocationNote(lgsMaterialAllocationHeader, lgsMaterialAllocationDetailsTempList);

        }


        /// <summary>
        /// Get the referece document id of the current document (Job Assign note or Material Request No or None)
        /// </summary>
        /// <returns></returns>
        private LgsMaterialAllocationHeader GetReferenceDocumentId(LgsMaterialAllocationHeader lgsMaterialAllocationHeader)
        {
            
            if (!string.IsNullOrEmpty(txtJobAssignNoteNo.Text.Trim()))
            {
                int refDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmMaintenanceJobAssignNote").DocumentID;
                LgsMaintenanceJobAssignService lgsMaintenanceJobAssignService = new LgsMaintenanceJobAssignService();
                if (lgsMaintenanceJobAssignService.GetLgsMaintenanceJobAssignHeaderByDocumentNo(refDocumentID, txtJobAssignNoteNo.Text.Trim(), Common.LoggedLocationID) != null)
                {
                    lgsMaterialAllocationHeader.ReferenceDocumentDocumentID = refDocumentID;
                    lgsMaterialAllocationHeader.ReferenceDocumentID = lgsMaintenanceJobAssignService.GetLgsMaintenanceJobAssignHeaderByDocumentNo(refDocumentID, txtJobAssignNoteNo.Text.Trim(), Common.LoggedLocationID).LgsMaintenanceJobAssignHeaderID;
                }                
            }
            else if (!string.IsNullOrEmpty(txtMRNNo.Text.Trim()))
            {
                int refDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmMaterialRequestNote").DocumentID;
                LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();
                if (lgsMaterialRequestService.GetLgsMaterialRequestHeaderByDocumentNo(refDocumentID, txtMRNNo.Text.Trim(), Common.LoggedLocationID) != null)
                {
                    lgsMaterialAllocationHeader.ReferenceDocumentID = lgsMaterialRequestService.GetLgsMaterialRequestHeaderByDocumentNo(refDocumentID, txtMRNNo.Text.Trim(), Common.LoggedLocationID).LgsMaterialRequestHeaderID;
                    lgsMaterialAllocationHeader.ReferenceDocumentDocumentID = refDocumentID;
                }                
            }
            else
            {
                lgsMaterialAllocationHeader.ReferenceDocumentID = 0;
                lgsMaterialAllocationHeader.ReferenceDocumentDocumentID = 0;
            }

            return lgsMaterialAllocationHeader;
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

        private void RecallMaintenanceJobAssignNote(string documentNo)
        {
            
            LgsMaintenanceJobAssignService lgsMaintenanceJobAssignService = new LgsMaintenanceJobAssignService();
            LgsMaintenanceJobAssignHeader lgsMaintenanceJobAssignHeader = new LgsMaintenanceJobAssignHeader();
            LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();

            lgsMaintenanceJobAssignHeader = lgsMaintenanceJobAssignService.GetLgsMaintenanceJobAssignHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmMaintenanceJobAssignNote").DocumentID, documentNo, Common.LoggedLocationID);
            if (lgsMaintenanceJobAssignHeader == null)
            {
                Toast.Show(" Maintenance Job Assign Note - " + txtJobAssignNoteNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                Common.ClearTextBox(txtJobAssignNoteNo);
                return;
            }

            lgsMaterialAllocationDetailsTempList = lgsMaterialAllocationService.GetLgsMaintenanceJobAssignProductDetails(lgsMaintenanceJobAssignHeader);
            UpdatedgvItemDetails();
        }


        private void RecallMateriaRequasitionNote(string documentNo)
        {

            LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();
            LgsMaterialRequestHeader lgsMaterialRequestHeader = new LgsMaterialRequestHeader();
            LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();

            lgsMaterialRequestHeader = lgsMaterialRequestService.GetLgsMaterialRequestHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmMaterialRequestNote").DocumentID, documentNo, Common.LoggedLocationID);
            if (lgsMaterialRequestHeader == null)
            {
                Toast.Show("Material Requesition Note - " + txtMRNNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                Common.ClearTextBox(txtMRNNo);                
                return;
            }

            txtReferenceNo.Text = lgsMaterialRequestHeader.ReferenceDocumentNo;
            txtRemark.Text = lgsMaterialRequestHeader.Remark;

            lgsMaterialAllocationDetailsTempList = lgsMaterialAllocationService.GetLgsMateriaRequsitionProductDetails(lgsMaterialRequestHeader);
            dgvItemDetails.DataSource = null;
            dgvItemDetails.DataSource = lgsMaterialAllocationDetailsTempList;
            dgvItemDetails.Refresh();

            GetSummarizeFigures(lgsMaterialAllocationDetailsTempList);

            Common.EnableComboBox(false, cmbLocation);
            Common.EnableTextBox(false, txtMRNNo);
            Common.EnableButton(true, btnSave, btnPause, btnView);   
        }

        /// <summary>
        /// Load and bind pending Assigne jobs
        /// </summary>
        private void LoadPendingAssignedJobs()
        {
            LgsMaintenanceJobAssignService lgsMaintenanceJobAssignService = new LgsMaintenanceJobAssignService();
            Common.SetAutoComplete(txtJobAssignNoteNo, lgsMaintenanceJobAssignService.GetPendingAssignedobList(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationMJANo.Checked);
        }

        /// <summary>
        /// Load and bind pending Material requesition notes
        /// </summary>
        private void LoadPendingMaterialRequasitionNotes()
        {
            LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();
            Common.SetAutoComplete(txtMRNNo, lgsMaterialRequestService.GetPendingMaterialRequsitionList(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationMRNNo.Checked);
        }

        /// <summary>
        /// Load paused documents
        /// </summary>
        private void LoadPausedDocuments()
        {
            LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();
            Common.SetAutoComplete(txtDocumentNo, lgsMaterialAllocationService.GetPausedDocumentNumbers(), chkAutoCompleationDocumentNo.Checked);
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
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
            LgsProductMaster lgsProductMaster = new LgsProductMaster();
            lgsProductMaster = lgsProductMasterService.GetProductsByCode(productCode);

            if (lgsProductMaster != null)
            {
                recodFound = true;
                txtProductCode.Text = lgsProductMaster.ProductCode;
                txtProductName.Text = lgsProductMaster.ProductName;
                txtRate.Text = lgsProductMaster.CostPrice.ToString();
                if (lgsProductMaster.UnitOfMeasureID.Equals(unitOfMeasureID) || unitOfMeasureID.Equals(0))
                {
                    cmbUnit.SelectedValue = lgsProductMaster.UnitOfMeasureID;
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
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
            LgsProductMaster lgsProductMaster = new LgsProductMaster();

            lgsProductMaster = lgsProductMasterService.GetProductsByName(productName);
            if (lgsProductMaster != null)
            {
                recodFound = true;
                txtProductCode.Text = lgsProductMaster.ProductCode;
                txtProductName.Text = lgsProductMaster.ProductName;
                txtRate.Text = lgsProductMaster.CostPrice.ToString();
                if (lgsProductMaster.UnitOfMeasureID.Equals(unitOfMeasureID) || unitOfMeasureID.Equals(0))
                {
                    cmbUnit.SelectedValue = lgsProductMaster.UnitOfMeasureID;
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

        private void loadProductDetails()
        {
            EnableProductDetailControls(true);

            txtProductCode.Text = dgvItemDetails["productCode", dgvItemDetails.CurrentRow.Index].Value.ToString().Trim();
            txtProductName.Text = dgvItemDetails["ProductName", dgvItemDetails.CurrentRow.Index].Value.ToString().Trim();
            cmbUnit.SelectedIndex = cmbUnit.FindString(dgvItemDetails["UnitOfMeasure", dgvItemDetails.CurrentRow.Index].Value.ToString().Trim());
            cmbRequestLocationCode.SelectedIndex = cmbRequestLocationCode.FindString(dgvItemDetails["RequestLocationCode", dgvItemDetails.CurrentRow.Index].Value.ToString().Trim());
            txtRequestNo.Text = (dgvItemDetails["requestNo", dgvItemDetails.CurrentRow.Index].Value) == null ? "" : dgvItemDetails["requestNo", dgvItemDetails.CurrentRow.Index].Value.ToString().Trim();
            txtQty.Text = dgvItemDetails["Qty", dgvItemDetails.CurrentRow.Index].Value.ToString().Trim();
            txtRate.Text = dgvItemDetails["costPrice", dgvItemDetails.CurrentRow.Index].Value.ToString().Trim();
            txtProductAmount.Text = dgvItemDetails["GrossAmount", dgvItemDetails.CurrentRow.Index].Value.ToString().Trim();
        }

        #endregion

        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtQty.Text.Trim())) { txtQty.Text = "0"; }
                decimal qty = Convert.ToDecimal(txtQty.Text.Trim());

                LgsProductMaster lgsProductMaster = new LgsProductMaster();
                LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                lgsProductMaster = lgsProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());
                if (lgsProductMaster != null)
                {
                    LgsMaterialRequestHeader lgsMaterialRequestHeader = new LgsMaterialRequestHeader();
                    LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();
                    lgsMaterialRequestHeader = lgsMaterialRequestService.GetSavedDocumentDetailsByDocumentNumber(txtMRNNo.Text.Trim());

                    if (!lgsMaterialRequestService.IsValidNoOfQty(qty, lgsProductMaster.LgsProductMasterID, Convert.ToInt32(cmbUnit.SelectedValue), lgsMaterialRequestHeader.LgsMaterialRequestHeaderID))
                    {
                        Toast.Show("Invalid Qty.\nQty cannot grater then request order Qty", Toast.messageType.Information, Toast.messageAction.General, "");
                        txtQty.Focus();
                        txtQty.SelectAll();
                    }
                }
                else
                {
                    Toast.Show("Product", Toast.messageType.Information, Toast.messageAction.NotExists);
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

                        LgsMaterialAllocationDetailTemp lgsMaterialAllocationDetailTemp = new LgsMaterialAllocationDetailTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                        lgsMaterialAllocationDetailTemp.ProductID = lgsProductMasterService.GetProductsByCode(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).LgsProductMasterID;
                        lgsMaterialAllocationDetailTemp.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["UnitOfMeasure", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                        LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();

                        dgvItemDetails.DataSource = null;
                        lgsMaterialAllocationDetailsTempList = lgsMaterialAllocationService.GetDeleteLgsMaterialAllocationDetailTemp(lgsMaterialAllocationDetailsTempList, lgsMaterialAllocationDetailTemp);
                        dgvItemDetails.DataSource = lgsMaterialAllocationDetailsTempList;
                        dgvItemDetails.Refresh();

                        if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                        else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }

                        GetSummarizeFigures(lgsMaterialAllocationDetailsTempList);
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

        private void btnMRNDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                LgsMaterialRequestService LlsMaterialRequestService = new LgsMaterialRequestService();
                DataView dvAllReferenceData = new DataView(LlsMaterialRequestService.GetPendingMaterialRequsitionDocuments());
                if (dvAllReferenceData.Count != 0)
                {
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), "Pending Request Documents", string.Empty, txtMRNNo);
                    txtMRNNo_Validated(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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

        private void DisplayStockDetails()
        {
            if (chkViewStokDetails.Checked == false) { dgvBatchStock.DataSource = null; }
        }

        private void ShowStockDetails(long productID = 0)
        {
            CommonService CommonService = new CommonService();

            // Batch wise stock

            dgvBatchStock.DataSource = null;
            dgvBatchStock.AutoGenerateColumns = false;
            List<LgsProductBatchNoExpiaryDetail> lgsProductBatchNoList = new List<LgsProductBatchNoExpiaryDetail>();
            lgsProductBatchNoList = CommonService.GetLgsBatchStockDetailsToStockGrid(productID, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
            dgvBatchStock.DataSource = lgsProductBatchNoList;
            dgvBatchStock.Refresh();
        }

    }
}
