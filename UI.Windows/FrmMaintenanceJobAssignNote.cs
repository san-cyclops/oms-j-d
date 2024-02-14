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
    /// <summary>
    /// Developed by asanka
    /// </summary>
    /// 
    public partial class FrmMaintenanceJobAssignNote : UI.Windows.FrmBaseTransactionForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();
        private ReferenceType existingReferenceType;

        List<LgsMaintenanceJobAssignProductDetailTemp> lgsMaintenanceJobAssignProductDetailsTemp = new List<LgsMaintenanceJobAssignProductDetailTemp>();
        List<LgsMaintenanceJobAssignEmployeeDetailTemp> lgsMaintenanceJobAssignEmployeeDetailsTemp = new List<LgsMaintenanceJobAssignEmployeeDetailTemp>();
        int documentID;

        public FrmMaintenanceJobAssignNote()
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

                EmployeeService employeeService = new EmployeeService();
                Common.SetAutoComplete(txtEmployeeCode, employeeService.GetAllEmployeeCodes(), chkAutoCompleationEmployee.Checked);
                Common.SetAutoComplete(txtEmployeeName, employeeService.GetAllEmployeeNames(), chkAutoCompleationEmployee.Checked);

                // Load Unit of measures
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                Common.LoadUnitOfMeasures(cmbUnit, unitOfMeasureService.GetAllUnitOfMeasures());
                cmbUnit.SelectedIndex = 0;

                // Load Cost centers
                CostCentreService costCentreService = new CostCentreService();
                Common.LoadCostCenters(cmbCostCentre, costCentreService.GetAllCostCentres());

                LoadPausedDocuments();
                LoadPendingJobs();
                LoadProducts();

                documentID = autoGenerateInfo.DocumentID;

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                
                base.FormLoad();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void EnableEmployeeLine(bool status)
        {
            txtEmployeeCode.Enabled = status;
            txtEmployeeName.Enabled = status;
            txtEmployeeTitle.Enabled = status;
        }

        private void EnableProductLine(bool status)
        {
            txtProductCode.Enabled = status;
            txtProductName.Enabled = status;
            cmbUnit.Enabled = status;
            txtQty.Enabled = status;
        }

        private void chkAutoCompleationJobNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoCompleationMjaNo.Checked)
                { LoadPendingJobs(); }                
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

                LgsMaintenanceJobAssignService lgsMaintenanceJobAssignService = new LgsMaintenanceJobAssignService();
                if (lgsMaintenanceJobAssignService.GetPausedDocumentDetailsByDocumentNumber(txtDocumentNo.Text.Trim()) == null)
                {
                    Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                }
                else
                {
                    LoadPausedDocument(lgsMaintenanceJobAssignService.GetPausedDocumentDetailsByDocumentNumber(txtDocumentNo.Text.Trim()));
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
                Common.SetFocus(e,tabAssignedDetails,tbpTechnician);
                txtEmployeeCode.Enabled = true;
                Common.SetFocus(e, txtEmployeeCode); 
            }
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
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProductCode.Text.Trim())) { return; }
                if (IsProductExistsByCode(txtProductCode.Text.Trim()))
                {
                    txtProductName.Enabled = true;
                    cmbUnit.Enabled = true;
                    txtProductName.Focus();
                }
                else
                {
                    txtProductCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductCode_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!IsLocationExistsByName(cmbLocation.Text.Trim()))
            //    {
            //        cmbLocation.Focus();
            //        return;
            //    }
            //    if (string.IsNullOrEmpty(txtProductCode.Text.Trim()))
            //    {
            //        EnableProductDetailControls(false);
            //        return;
            //    }

            //    this.cmbUnit.SelectedIndexChanged -= new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
            //    if (!IsProductExistsByCode(txtProductCode.Text.Trim()))
            //    {
            //        txtProductCode.Focus();
            //        EnableProductDetailControls(false);
            //        return;
            //    }
            //    else
            //    { EnableProductDetailControls(true); }
            //    this.cmbUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
            //}
            //catch (Exception ex)
            //{ Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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
                if (!IsProductExistsByName(txtProductName.Text.Trim()))
                {
                    txtProductName.Focus();
                    EnableProductDetailControls(false);
                    return;
                }
                else
                { EnableProductDetailControls(true); }
                
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                txtQty.Enabled = true;
                Common.SetFocus(e, txtQty);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbUnit_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!IsUnitOfMeasureExistsByID(long.Parse(cmbUnit.SelectedValue.ToString())))
            //    { return; }
            //}
            //catch (Exception ex)
            //{ Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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
                { AssignProductProperties(); }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }


        private void dgvItemDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!Common.AllowDeleteGridRow(e))
                { return; }

                LgsMaintenanceJobAssignService lgsMaintenanceJobAssignService = new LgsMaintenanceJobAssignService();
                lgsMaintenanceJobAssignProductDetailsTemp = lgsMaintenanceJobAssignService.DeleteProductLgsMaintenanceJobAssignProductDetailTemp(lgsMaintenanceJobAssignProductDetailsTemp, dgvItemDetails[1, dgvItemDetails.CurrentRow.Index].Value.ToString().Trim());
                UpdatedgvItemDetails();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void FrmMaintenanceJobAssignNote_Activated(object sender, EventArgs e)
        {
            try
            { Common.SetFormOpacity(this, true); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void FrmMaintenanceJobAssignNote_Deactivate(object sender, EventArgs e)
        {
            try
            { Common.SetFormOpacity(this, false); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoCompleationEmployee_CheckedChanged(object sender, EventArgs e)
        {
            try
            { LoadEmployees(); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtEmployeeCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtEmployeeName.Enabled = true;
                    txtEmployeeName.Focus();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtEmployeeCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEmployeeCode.Text.Trim())) { return; }
                if (IsEmployeeExistsByCode(txtEmployeeCode.Text.Trim()))
                {
                    txtEmployeeTitle.Enabled = true;
                    txtEmployeeTitle.Focus();
                }
                else
                {
                    txtEmployeeCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmployeeCode_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!string.IsNullOrEmpty(txtEmployeeCode.Text.Trim()))
            //    {

            //    }
            //    //if (!IsLocationExistsByName(cmbLocation.Text.Trim()))
            //    //{
            //    //    cmbLocation.Focus();
            //    //    return;
            //    //}
            //    //if (string.IsNullOrEmpty(txtEmployeeCode.Text.Trim()))
            //    //{
            //    //    EnableEmployeeDetailControls(false);
            //    //    return;
            //    //}

            //    if (!IsEmployeeExistsByCode(txtEmployeeCode.Text.Trim()))
            //    {
            //        txtEmployeeCode.Focus();
            //        //EnableEmployeeDetailControls(false);
            //        return;
            //    }
            //    else
            //    {
            //        txtEmployeeTitle.Enabled = true;
            //        txtEmployeeTitle.Focus();
            //    }
            //}
            //catch (Exception ex)
            //{ Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtEmployeeName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                txtEmployeeTitle.Enabled = true;
                Common.SetFocus(e, txtEmployeeTitle); 
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtEmployeeName_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!IsLocationExistsByName(cmbLocation.Text.Trim()))
                {
                    cmbLocation.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtEmployeeName.Text.Trim()))
                {
                    EnableEmployeeDetailControls(false);
                    return;
                }

                if (!IsEmployeeExistsByName(txtEmployeeName.Text.Trim()))
                {
                    txtEmployeeCode.Focus();
                    EnableEmployeeDetailControls(false);
                    return;
                }
                else
                { EnableEmployeeDetailControls(true); }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtEmployeeTitle_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                AssignEmployeeProperties();
                //if (!IsLocationExistsByName(cmbLocation.Text.Trim()))
                //{
                //    cmbLocation.Focus();
                //    return;
                //}
                //if (string.IsNullOrEmpty(txtEmployeeCode.Text.Trim()))
                //{
                //    EnableEmployeeDetailControls(false);
                //    return;
                //}

                //if (!IsEmployeeExistsByCode(txtEmployeeCode.Text.Trim()))
                //{
                //    txtEmployeeCode.Focus();
                //    EnableEmployeeDetailControls(false);
                //    return;
                //}
                
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtEmployeeTitle_Validated(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dgvEmployeeDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!Common.AllowDeleteGridRow(e))
                { return; }

                LgsMaintenanceJobAssignService lgsMaintenanceJobAssignService = new LgsMaintenanceJobAssignService();
                lgsMaintenanceJobAssignEmployeeDetailsTemp = lgsMaintenanceJobAssignService.DeleteEmployeeLgsMaintenanceJobAssignEmployeeDetailTemp(lgsMaintenanceJobAssignEmployeeDetailsTemp, dgvEmployeeDetails[1, dgvItemDetails.CurrentRow.Index].Value.ToString().Trim());
                UpdatedgvEmployeeDetails();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoCompleationMjaNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoCompleationMjaNo.Checked)
                { LoadPausedDocuments(); }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtMaintenanceJobRequestNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtReferenceNo); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtMaintenanceJobRequestNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMaintenanceJobRequestNo.Text.Trim()))
                {
                    LgsMaintenanceJobRequisitionHeader lgsMaintenanceJobRequisitionHeader = new LgsMaintenanceJobRequisitionHeader();
                    LgsMaintenanceJobRequisitionService lgsMaintenanceJobRequisitionService = new LgsMaintenanceJobRequisitionService();

                    lgsMaintenanceJobRequisitionHeader = lgsMaintenanceJobRequisitionService.GetLgsMaintenanceJobRequisitionHeaderByDocumentNo(txtMaintenanceJobRequestNo.Text.Trim());

                    if (lgsMaintenanceJobRequisitionHeader != null)
                    {
                        txtJobDescription.Text = lgsMaintenanceJobRequisitionHeader.JobDescription.Trim();
                        Common.EnableTextBox(false, txtMaintenanceJobRequestNo, txtJobDescription);
                    }
                    else
                    {
                        Toast.Show(lblMaintenanceJobNo.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtMaintenanceJobRequestNo.SelectAll();
                        txtMaintenanceJobRequestNo.Focus();
                    }
                }
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

            Common.ClearTextBox(txtMaintenanceJobRequestNo, txtJobDescription);

            // Disable product details controls
            //EnableProductDetailControls(false);

            EnableEmployeeLine(false);
            EnableProductLine(false);

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
                Common.ClearTextBox(txtQty);
            }
            Common.EnableComboBox(enabled, cmbUnit);
            Common.EnableTextBox(enabled, txtQty);
        }

        /// <summary>
        /// Enable or disable Employee details controls
        /// </summary>
        /// <param name="enabled"></param>
        private void EnableEmployeeDetailControls(bool enabled)
        {
            if (!enabled)
            {
                Common.ClearTextBox(txtEmployeeTitle);
            }
            Common.EnableTextBox(enabled, txtEmployeeTitle);
        }

        /// <summary>
        /// Get a Document number
        /// </summary>
        /// <param name="isTemporytNo"></param>
        /// <returns></returns>
        private string GetDocumentNo(bool isTemporytNo)
        {
            LgsMaintenanceJobAssignService lgsMaintenanceJobAssignService = new LgsMaintenanceJobAssignService();
            LocationService locationService = new LocationService();
            return lgsMaintenanceJobAssignService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
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
        /// Load and bind employee into text boxes
        /// </summary>
        private void LoadEmployees()
        {
            EmployeeService employeeService = new EmployeeService();
            Common.SetAutoComplete(txtEmployeeCode, employeeService.GetAllEmployeeCodes(), chkAutoCompleationEmployee.Checked);
            Common.SetAutoComplete(txtEmployeeName, employeeService.GetAllEmployeeNames(), chkAutoCompleationEmployee.Checked);
        }

        /// <summary>
        /// Load and bind pending jobs
        /// </summary>
        private void LoadPendingJobs()
        {
            LgsMaintenanceJobRequisitionService lgsMaintenanceJobRequisitionService = new LgsMaintenanceJobRequisitionService();
            Common.SetAutoComplete(txtMaintenanceJobRequestNo, lgsMaintenanceJobRequisitionService.GetPendingMaintenanceJobList(), chkAutoCompleationJobNo.Checked);
        }

        /// <summary>
        /// Load paused documents
        /// </summary>
        private void LoadPausedDocuments()
        {
            LgsMaintenanceJobAssignService lgsMaintenanceJobAssignService = new LgsMaintenanceJobAssignService();
            Common.SetAutoComplete(txtDocumentNo, lgsMaintenanceJobAssignService.GetPausedDocumentNumbers(), chkAutoCompleationMjaNo.Checked);			
        }

        /// <summary>
        /// Assign product properties before adding into grid view
        /// </summary>
        private void AssignProductProperties()
        {
            LgsMaintenanceJobAssignService lgsMaintenanceJobAssignService = new LgsMaintenanceJobAssignService();
            LgsMaintenanceJobAssignProductDetailTemp lgsMaintenanceJobAssignProductDetailTemp = new LgsMaintenanceJobAssignProductDetailTemp();
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
            LgsProductMaster lgsProductMaster = new LgsProductMaster();
            CommonService commonService = new CommonService();

            lgsProductMaster = lgsProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

            lgsMaintenanceJobAssignProductDetailTemp.ProductID = lgsProductMaster.LgsProductMasterID;
            lgsMaintenanceJobAssignProductDetailTemp.ProductCode = lgsProductMaster.ProductCode;
            lgsMaintenanceJobAssignProductDetailTemp.ProductName = lgsProductMaster.ProductName;
            lgsMaintenanceJobAssignProductDetailTemp.UnitOfMeasure = cmbUnit.Text.Trim();
            lgsMaintenanceJobAssignProductDetailTemp.UnitOfMeasureID = long.Parse(cmbUnit.SelectedValue.ToString().Trim());
            lgsMaintenanceJobAssignProductDetailTemp.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text.Trim());
            lgsMaintenanceJobAssignProductDetailTemp.DocumentDate = DateTime.UtcNow;

            lgsMaintenanceJobAssignProductDetailsTemp = lgsMaintenanceJobAssignService.GetLgsMaintenanceJobAssignProductDetailTempList(lgsMaintenanceJobAssignProductDetailsTemp, lgsMaintenanceJobAssignProductDetailTemp);
           
            UpdatedgvItemDetails();
        }

        /// <summary>
        /// Update products grid view
        /// </summary>
        private void UpdatedgvItemDetails()
        {
            dgvItemDetails.DataSource = null;
            dgvItemDetails.DataSource = lgsMaintenanceJobAssignProductDetailsTemp.OrderBy(pr => pr.LineNo).ToList();
            GetSummarizeFigures(lgsMaintenanceJobAssignProductDetailsTemp.ToList());
            if (dgvItemDetails.Rows.Count > 0)
            { 
                dgvItemDetails.FirstDisplayedScrollingRowIndex = dgvItemDetails.RowCount - 1; 
            }
            else
            { 
                Common.EnableComboBox(true, cmbLocation); 
            }

            Common.ClearTextBox(txtProductCode, txtProductName, txtQty);
            EnableProductLine(false);
            txtProductCode.Enabled = true;
            txtProductCode.Focus();
        }

        /// <summary>
        /// Update Total Qty
        /// </summary>
        /// <param name="Productlist"></param>
        private void GetSummarizeFigures(List<LgsMaintenanceJobAssignProductDetailTemp> Productlist)
        {
            //txtTotalQty.Text = Productlist.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.OrderQty).ToString();
        }

        /// <summary>
        /// Pause Maintenance Job Assign Note.
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
        /// Save New Maintenance Job Assign request Note.
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
            GetSummarizeFigures(lgsMaintenanceJobAssignProductDetailsTemp);

            LgsMaintenanceJobAssignService lgsMaintenanceJobAssignService = new LgsMaintenanceJobAssignService();
            LgsMaintenanceJobAssignHeader lgsMaintenanceJobAssignHeader = new LgsMaintenanceJobAssignHeader();
            LgsMaintenanceJobRequisitionService lgsMaintenanceJobRequisitionService = new LgsMaintenanceJobRequisitionService();
            LgsMaintenanceJobRequisitionHeader lgsMaintenanceJobRequisitionHeader = new LgsMaintenanceJobRequisitionHeader();
            Location location = new Location();
            LocationService locationService = new LocationService();

            location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
            lgsMaintenanceJobRequisitionHeader = lgsMaintenanceJobRequisitionService.GetMaintananceJobHeader(txtMaintenanceJobRequestNo.Text.Trim());

            lgsMaintenanceJobAssignHeader = lgsMaintenanceJobAssignService.GetPausedLgsMaintenanceJobAssignHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
            if (lgsMaintenanceJobAssignHeader == null)
            { lgsMaintenanceJobAssignHeader = new LgsMaintenanceJobAssignHeader(); }

            lgsMaintenanceJobAssignHeader = GetReferenceDocumentId(lgsMaintenanceJobAssignHeader);
            //if (lgsMaintenanceJobAssignService.GetLgsMaintenanceJobAssignHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), location.LocationID) != null)
            //{
            //    lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignHeaderID = lgsMaintenanceJobAssignService.GetLgsMaintenanceJobAssignHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), location.LocationID).LgsMaintenanceJobAssignHeaderID;
            //}

            if (documentStatus.Equals(1))
            {
                documentNo = GetDocumentNo(false);
                txtDocumentNo.Text = documentNo;
            }

            lgsMaintenanceJobAssignHeader.DocumentStatus = documentStatus;
            lgsMaintenanceJobAssignHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
            lgsMaintenanceJobAssignHeader.CompanyID = location.CompanyID;
            lgsMaintenanceJobAssignHeader.LocationID = location.LocationID;
            lgsMaintenanceJobAssignHeader.DocumentID = documentID;
            lgsMaintenanceJobAssignHeader.DocumentNo = documentNo.Trim();
            lgsMaintenanceJobAssignHeader.DocumentDate = dtpDocumentDate.Value;
            lgsMaintenanceJobAssignHeader.DiliveryDate = dtpDeliveryDate.Value;
            lgsMaintenanceJobAssignHeader.LgsMaintenanceJobRequisitionHeaderID = lgsMaintenanceJobRequisitionHeader.LgsMaintenanceJobRequisitionHeaderID;
            //lgsMaintenanceJobAssignHeader.CostCentreID = int.Parse(cmbCostCentre.SelectedValue.ToString());

            lgsMaintenanceJobAssignHeader.RequestedBy = Common.LoggedUser;
            lgsMaintenanceJobAssignHeader.ReferenceDocumentNo = txtReferenceNo.Text.Trim();
            lgsMaintenanceJobAssignHeader.Remark = txtRemark.Text.Trim();
            lgsMaintenanceJobAssignHeader.CreatedUser = Common.LoggedUser;
            lgsMaintenanceJobAssignHeader.CreatedDate = DateTime.UtcNow;
            lgsMaintenanceJobAssignHeader.ModifiedUser = Common.LoggedUser;
            lgsMaintenanceJobAssignHeader.ModifiedDate = DateTime.UtcNow;
            lgsMaintenanceJobAssignHeader.DataTransfer = 0;

            return lgsMaintenanceJobAssignService.SaveMaintenanceJobAssignNote(lgsMaintenanceJobAssignHeader, lgsMaintenanceJobAssignProductDetailsTemp, lgsMaintenanceJobAssignEmployeeDetailsTemp);

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

        /// <summary>
        /// Recal Paused document
        /// </summary>
        /// <param name="existingLgsMaintenanceJobAssignHeader"></param>
        private void LoadPausedDocument(LgsMaintenanceJobAssignHeader existingLgsMaintenanceJobAssignHeader)
        {
            LgsMaintenanceJobRequisitionService lgsMaintenanceJobRequisitionService = new LgsMaintenanceJobRequisitionService();
            txtMaintenanceJobRequestNo.Text = lgsMaintenanceJobRequisitionService.GetSavedDocumentNoByDocumentID(existingLgsMaintenanceJobAssignHeader.ReferenceDocumentID);
            txtDocumentNo.Text = existingLgsMaintenanceJobAssignHeader.DocumentNo;

            //txtMaintenanceJobNo.Text =
            cmbLocation.SelectedValue = existingLgsMaintenanceJobAssignHeader.LocationID;
            dtpDocumentDate.Value = existingLgsMaintenanceJobAssignHeader.DocumentDate;
            dtpDeliveryDate.Value = existingLgsMaintenanceJobAssignHeader.DiliveryDate;
            cmbCostCentre.SelectedValue = existingLgsMaintenanceJobAssignHeader.CostCentreID;
            txtReferenceNo.Text = existingLgsMaintenanceJobAssignHeader.ReferenceDocumentNo;
            txtRemark.Text = existingLgsMaintenanceJobAssignHeader.Remark.Trim();

            LgsMaintenanceJobAssignService lgsMaintenanceJobAssignService = new LgsMaintenanceJobAssignService();
            lgsMaintenanceJobAssignProductDetailsTemp = lgsMaintenanceJobAssignService.GetLgsMaintenanceJobAssignProductDetails(existingLgsMaintenanceJobAssignHeader);
            UpdatedgvItemDetails();
            lgsMaintenanceJobAssignEmployeeDetailsTemp = lgsMaintenanceJobAssignService.GetLgsMaintenanceJobAssignEmployeeDetails(existingLgsMaintenanceJobAssignHeader);
            UpdatedgvEmployeeDetails();
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

        // Confirm Product by Code
        private bool IsProductExistsByCode(string productCode)
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
        private bool IsProductExistsByName(string productName)
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

        // Confirm Employee by Code
        private bool IsEmployeeExistsByCode(string employeeCode)
        {
            bool recodFound = false;
            EmployeeService employeeService = new EmployeeService();
            Employee employee = new Employee();
            employee = employeeService.GetEmployeesByCode(employeeCode);

            if (employee != null)
            {
                recodFound = true;
                txtEmployeeCode.Text = employee.EmployeeCode;
                txtEmployeeName.Text = employee.EmployeeName;

                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.TitleType).ToString(), employee.EmployeeTitle);
                if (existingReferenceType != null)
                {
                    txtEmployeeTitle.Text = existingReferenceType.LookupValue;
                }
                else
                {
                    txtEmployeeTitle.Text = string.Empty;
                }
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtEmployeeCode, txtEmployeeName, txtEmployeeTitle);
                Toast.Show("Employee Code" + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }

            return recodFound;
        }

        // Confirm Employee by Name
        private bool IsEmployeeExistsByName(string employeeName)
        {
            bool recodFound = false;
            EmployeeService employeeService = new EmployeeService();
            Employee employee = new Employee();
            employee = employeeService.GetEmployeesByName(employeeName);

            if (employee != null)
            {
                recodFound = true;
                txtEmployeeCode.Text = employee.EmployeeCode;
                txtEmployeeName.Text = employee.EmployeeName;

                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.TitleType).ToString(), employee.EmployeeTitle);
                if (existingReferenceType != null)
                {
                    txtEmployeeTitle.Text = existingReferenceType.LookupValue;
                }
                else
                {
                    txtEmployeeTitle.Text = string.Empty;
                }
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtEmployeeCode, txtEmployeeName, txtEmployeeTitle);
                Toast.Show("Employee Name" + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }

            return recodFound;
        }

        #endregion

        /// <summary>
        /// Assign Employee properties before adding into grid view
        /// </summary>
        private void AssignEmployeeProperties()
        {
            LgsMaintenanceJobAssignService lgsMaintenanceJobAssignService = new LgsMaintenanceJobAssignService();
            LgsMaintenanceJobAssignEmployeeDetailTemp lgsMaintenanceJobAssignEmployeeDetailTemp = new LgsMaintenanceJobAssignEmployeeDetailTemp();
            EmployeeService employeeService = new EmployeeService();
            Employee employee = new Employee();
            CommonService commonService = new CommonService();
            LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();

            employee = employeeService.GetEmployeesByCode(txtEmployeeCode.Text.Trim());

            lgsMaintenanceJobAssignEmployeeDetailTemp.EmployeeID = employee.EmployeeID;
            lgsMaintenanceJobAssignEmployeeDetailTemp.EmployeeCode = employee.EmployeeCode;
            lgsMaintenanceJobAssignEmployeeDetailTemp.EmployeeName = employee.EmployeeName;

            existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.TitleType).ToString(), employee.EmployeeTitle);
            if (existingReferenceType != null)
            {
                lgsMaintenanceJobAssignEmployeeDetailTemp.EmployeeTitle = existingReferenceType.LookupValue;
            }

            lgsMaintenanceJobAssignEmployeeDetailTemp.DocumentDate = dtpDocumentDate.Value;

            lgsMaintenanceJobAssignEmployeeDetailsTemp = lgsMaintenanceJobAssignService.GetLgsMaintenanceJobAssignEmployeeDetailTempList(lgsMaintenanceJobAssignEmployeeDetailsTemp, lgsMaintenanceJobAssignEmployeeDetailTemp);
            UpdatedgvEmployeeDetails();

        }

        /// <summary>
        /// Update employee grid view
        /// </summary>
        private void UpdatedgvEmployeeDetails()
        {
            dgvEmployeeDetails.DataSource = null;
            dgvEmployeeDetails.DataSource = lgsMaintenanceJobAssignEmployeeDetailsTemp.OrderBy(pr => pr.LineNo).ToList();
            if (dgvEmployeeDetails.Rows.Count > 0)
            { dgvEmployeeDetails.FirstDisplayedScrollingRowIndex = dgvEmployeeDetails.RowCount - 1; }
            else
            { Common.EnableComboBox(true, cmbLocation); }

            
            Common.ClearTextBox(txtEmployeeCode, txtEmployeeName, txtEmployeeTitle);
            EnableEmployeeLine(false);
            txtEmployeeCode.Enabled = true;
            txtEmployeeCode.Focus();
        }

        public override void ClearForm()
        {
            Common.ClearTextBox(txtJobDescription, txtMaintenanceJobRequestNo);
            Common.EnableTextBox(true, txtMaintenanceJobRequestNo, txtJobDescription);
            dgvItemDetails.DataSource = null;
            dgvItemDetails.Refresh();
            dgvEmployeeDetails.DataSource = null;
            dgvEmployeeDetails.Refresh();
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
            if (chkAutoCompleationMjaNo.Checked)
            {
                LoadPausedDocuments();
            }

            if (chkAutoCompleationJobNo.Checked)
            {
                LoadPendingJobs();
            }

        }

        /// <summary>
        /// Get the referece document id of the current document 
        /// </summary>
        /// <returns></returns>
        private LgsMaintenanceJobAssignHeader GetReferenceDocumentId(LgsMaintenanceJobAssignHeader lgsMaintenanceJobAssignHeader)
        {

            if (!string.IsNullOrEmpty(txtMaintenanceJobRequestNo.Text.Trim()))
            {
                LgsMaintenanceJobRequisitionService lgsMaintenanceJobRequisitionService = new LgsMaintenanceJobRequisitionService();
                int refDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmMaintenanceJobRequisitionNote").DocumentID;
                if (lgsMaintenanceJobRequisitionService.GetLgsMaintenanceJobRequisitionHeaderByDocumentNo(refDocumentID, txtMaintenanceJobRequestNo.Text.Trim(), Common.LoggedLocationID) != null)
                {
                    lgsMaintenanceJobAssignHeader.ReferenceDocumentDocumentID = refDocumentID;
                    lgsMaintenanceJobAssignHeader.ReferenceDocumentID = lgsMaintenanceJobRequisitionService.GetLgsMaintenanceJobRequisitionHeaderByDocumentNo(refDocumentID, txtMaintenanceJobRequestNo.Text.Trim(), Common.LoggedLocationID).LgsMaintenanceJobRequisitionHeaderID;
                }
            }            
            else
            {
                lgsMaintenanceJobAssignHeader.ReferenceDocumentID = 0;
                lgsMaintenanceJobAssignHeader.ReferenceDocumentDocumentID = 0;
            }

            return lgsMaintenanceJobAssignHeader;
        }
        #endregion

        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtQty.Text.Trim()))
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
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void tabAssignedDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabAssignedDetails.SelectedIndex.Equals(1))
                {
                    txtProductCode.Enabled = true;
                    txtProductCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void BtnMaintenanceJobDetails_Click(object sender, EventArgs e)
        {
            try
            {
                LgsMaintenanceJobRequisitionService lgsMaintenanceJobRequisitionService = new LgsMaintenanceJobRequisitionService();
                DataView dvAllReferenceData = new DataView(lgsMaintenanceJobRequisitionService.GetPendingMaintananceRequsitionDocuments());
                if (dvAllReferenceData.Count != 0)
                {
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), "Pending Job Requestions", string.Empty, txtMaintenanceJobRequestNo);
                    txtMaintenanceJobRequestNo_Validated(this, e);
                    txtReferenceNo.Focus();
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

        


    }
}
