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
    public partial class FrmSampleOut : UI.Windows.FrmBaseTransactionForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<SampleOutDetailTemp> sampleOutDetailTempList = new List<SampleOutDetailTemp>();
        SampleOutDetailTemp existingSampleOutDetailTemp = new SampleOutDetailTemp();

        InvProductMaster existingInvProductMaster;
        List<InvProductSerialNoTemp> lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

        LgsProductMaster existingLgsProductMaster;
        
        List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();
        InvProductBatchNoTemp existingInvProductBatchNoTemp = new InvProductBatchNoTemp();

        List<LgsProductBatchNoTemp> lgsProductBatchNoTempList = new List<LgsProductBatchNoTemp>();
        LgsProductBatchNoTemp existingLgsProductBatchNoTemp = new LgsProductBatchNoTemp();

        int documentID = 0;
        int documentState;
        bool isSupplierProduct;
        bool isInvProduct;
        static string batchNumber;
        int type;
        decimal convertFactor;
        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;

        public FrmSampleOut()
        {
            InitializeComponent();
        }

        private void FrmSampleOut_Load(object sender, EventArgs e)
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
                Common.EnableTextBox(true, txtIssuedTo, txtRemark, txtDeliveryPerson, txtDocumentNo);
                Common.EnableComboBox(true, cmbLocation, cmbType); 
                Common.EnableButton(false, btnSave, btnPause);
                sampleOutDetailTempList = null;
                this.ActiveControl = txtIssuedTo;
                txtIssuedTo.Focus();
                cmbLocation.SelectedValue = Common.LoggedLocationID;
                cmbType.SelectedIndex = -1;

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

        private void EnableLine(bool enable)
        {
            Common.EnableTextBox(enable, txtBatchNo, txtQty, txtRate, txtProductAmount);
            Common.EnableComboBox(enable, cmbUnit);
        }

        private string GetDocumentNo(bool isTemporytNo, bool isInv)
        {
            try
            {
                SampleOutService sampleOutService = new SampleOutService();
                LocationService locationService = new LocationService();
                return sampleOutService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo, isInv).Trim();
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
                SampleOutService sampleOutService = new SampleOutService();

                Common.SetAutoComplete(txtProductCode, sampleOutService.GetAllInvProductCodes(), chkAutoCompleationProduct.Checked);
                Common.SetAutoComplete(txtProductName, sampleOutService.GetAllInvProductNames(), chkAutoCompleationProduct.Checked);

                //InvProductMasterService invProductMasterService = new InvProductMasterService();
                //Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                //Common.SetAutoComplete(txtProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
            }
            else
            {
                SampleOutService sampleOutService = new SampleOutService();

                Common.SetAutoComplete(txtProductCode, sampleOutService.GetAllLgsProductCodes(), chkAutoCompleationProduct.Checked);
                Common.SetAutoComplete(txtProductName, sampleOutService.GetAllLgsProductNames(), chkAutoCompleationProduct.Checked);

                //LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                //Common.SetAutoComplete(txtProductCode, lgsProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                //Common.SetAutoComplete(txtProductName, lgsProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
            } 
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
            SampleOutService SampleOutService = new SampleOutService();
            Common.SetAutoComplete(txtIssuedTo, SampleOutService.GetAllIssuedToNames(), true);

            //// Load Delivery Person
            Common.SetAutoComplete(txtDeliveryPerson, SampleOutService.GetAllDeliveryPersons(), true);

            //Load Sample Out Type
            LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
            Common.SetAutoBindRecords(cmbType, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.ModuleType).ToString()));

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

            isSupplierProduct = autoGenerateInfo.IsSupplierProduct;
            chkOverwrite.Checked = autoGenerateInfo.IsOverWriteQty;
            documentID = autoGenerateInfo.DocumentID;


            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
            if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }

            base.FormLoad();

            ////Load Sample out Document Numbers
            Common.SetAutoComplete(txtDocumentNo, SampleOutService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

        }

        

        private void txtIssuedTo_KeyDown(object sender, KeyEventArgs e)
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
                    cmbType.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbSampleOutMethod_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    GetSampleOutType();
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
                    dtpIssuedDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpIssuedDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtDeliveryPerson.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDeliveryPerson_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    chkOverwrite.Focus();
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
                    LoadProducts(isInvProduct); 
                    txtProductCode.Focus();
                }

                if (string.IsNullOrEmpty(txtIssuedTo.Text.Trim()))
                {
                    Toast.Show("Issued to", Toast.messageType.Information, Toast.messageAction.Empty);
                    txtIssuedTo.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDeliveryPerson.Text.Trim()))
                {
                    Toast.Show("Delivery person", Toast.messageType.Information, Toast.messageAction.Empty);
                    txtDeliveryPerson.Focus();
                    return;
                }

                if (cmbType.SelectedIndex == -1)
                {
                    Toast.Show("Sample out", Toast.messageType.Information, Toast.messageAction.Empty);
                    cmbType.Focus();
                    return;
                }
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkOverwrite_KeyDown(object sender, KeyEventArgs e)
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
                        if (sampleOutDetailTempList == null)
                            sampleOutDetailTempList = new List<SampleOutDetailTemp>();
                        existingSampleOutDetailTemp = sampleOutService.getSampleOutDetailTemp(sampleOutDetailTempList, existingLgsProductMaster, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID, isInvProduct);
                        if (existingSampleOutDetailTemp != null)
                        {
                            txtProductCode.Text = existingSampleOutDetailTemp.ProductCode;
                            txtProductName.Text = existingSampleOutDetailTemp.ProductName;
                            if (existingSampleOutDetailTemp.BatchNo != null) { txtBatchNo.Text = existingSampleOutDetailTemp.BatchNo; }
                            else { txtBatchNo.Text = string.Empty; }
                            cmbUnit.SelectedValue = existingSampleOutDetailTemp.UnitOfMeasureID;
                            txtQty.Text = Common.ConvertDecimalToStringQty(existingSampleOutDetailTemp.OrderQty);
                            txtBalQty.Text = Common.ConvertDecimalToStringQty(existingSampleOutDetailTemp.BalancedQty);
                            txtRate.Text = Common.ConvertDecimalToStringCurrency(existingSampleOutDetailTemp.CostPrice);
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
                        SampleOutService sampleOutService = new SampleOutService();
                        if (sampleOutDetailTempList == null)
                            sampleOutDetailTempList = new List<SampleOutDetailTemp>();
                        existingSampleOutDetailTemp = sampleOutService.getSampleOutDetailTemp(sampleOutDetailTempList, existingLgsProductMaster, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID, isInvProduct);
                        if (existingSampleOutDetailTemp != null)
                        {
                            txtProductCode.Text = existingSampleOutDetailTemp.ProductCode;
                            txtProductName.Text = existingSampleOutDetailTemp.ProductName;
                            if (existingSampleOutDetailTemp.BatchNo != null) { txtBatchNo.Text = existingSampleOutDetailTemp.BatchNo; }
                            else { txtBatchNo.Text = string.Empty; }
                            cmbUnit.SelectedValue = existingSampleOutDetailTemp.UnitOfMeasureID;
                            txtQty.Text = Common.ConvertDecimalToStringQty(existingSampleOutDetailTemp.OrderQty);
                            txtRate.Text = Common.ConvertDecimalToStringCurrency(existingSampleOutDetailTemp.CostPrice);
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

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode, txtProductName, txtBatchNo, txtRate,  txtQty, txtProductAmount);
            Common.ClearComboBox(cmbUnit);
            txtProductCode.Focus();
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
                else if (e.KeyCode.Equals(Keys.F3))
                {
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    DataView dvAllReferenceData = new DataView(invProductMasterService.GetProductDataTableForBatchTransaction());
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim());
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void LoadReferenceSearchForm(DataView dvAllReferenceData, string parentOfSearch, string searchFormCaption, string searchText)
        {
            try
            {
                FrmReferenceSearch referenceSearch = new FrmReferenceSearch();
                referenceSearch.ParentOfSearch = parentOfSearch.Trim();
                referenceSearch.FormCaption = searchFormCaption.Trim();
                referenceSearch.SearchText = searchText.Trim();
                referenceSearch.DvResults = dvAllReferenceData;

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

        private void txtProductCode_Leave(object sender, EventArgs e)
        {
            loadProductDetails(true, txtProductCode.Text.Trim(), 0);
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
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    DataView dvAllReferenceData = new DataView(invProductMasterService.GetProductDataTableForBatchTransaction());
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim());
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
                        cmbUnit.SelectedValue = existingInvProductMaster.UnitOfMeasureID;
                        cmbUnit.Focus();
                        return;
                    }
                }
                loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()));

                if (existingInvProductMaster.IsBatch)
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
            else
            {
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
                        if (isInvProduct)
                        {
                            InvProductBatchNoTemp invProductBatchNoTemp = new InvProductBatchNoTemp();
                            //invProductBatchNoTemp.DocumentID = documentID;
                            invProductBatchNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                            invProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                            SampleOutService sampleOutService = new SampleOutService();
                            invProductBatchNoTempList = sampleOutService.GetInvBatchNoDetail(existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                            if (invProductBatchNoTempList == null)
                                invProductBatchNoTempList = new List<InvProductBatchNoTemp>();

                            FrmBatchNumber frmBatchNumber = new FrmBatchNumber(invProductBatchNoTempList, invProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, FrmBatchNumber.transactionType.SampleOut, existingInvProductMaster.InvProductMasterID);
                            frmBatchNumber.ShowDialog();

                            txtBatchNo.Text = batchNumber;

                            txtQty.Enabled = true;
                            if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                                txtQty.Text = "1";
                            txtQty.Focus();
                        }
                        else
                        {
                            LgsProductBatchNoTemp lgsProductBatchNoTemp = new LgsProductBatchNoTemp();
                            //lgsProductBatchNoTemp.DocumentID = documentID;
                            lgsProductBatchNoTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                            lgsProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                            SampleOutService sampleOutService = new SampleOutService();
                            lgsProductBatchNoTempList = sampleOutService.GetLgsBatchNoDetail(existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                            if (lgsProductBatchNoTempList == null)
                                lgsProductBatchNoTempList = new List<LgsProductBatchNoTemp>();

                            FrmBatchNumber frmBatchNumber = new FrmBatchNumber(lgsProductBatchNoTempList, lgsProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, FrmBatchNumber.transactionType.SampleOut, existingLgsProductMaster.LgsProductMasterID);
                            frmBatchNumber.ShowDialog();

                            txtBatchNo.Text = batchNumber;

                            txtQty.Enabled = true;
                            if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                                txtQty.Text = "1";
                            txtQty.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtRate_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txtRate_Leave(object sender, EventArgs e)
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

        private void UpdateGrid(SampleOutDetailTemp sampleOutDetailTemp)
        {
            try
            {
                sampleOutDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                if (isInvProduct) { sampleOutDetailTemp.BaseUnitID = existingInvProductMaster.UnitOfMeasureID; }
                else { sampleOutDetailTemp.BaseUnitID = existingLgsProductMaster.UnitOfMeasureID; }
                
                sampleOutDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();

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
                    txtQty.Text = (sampleOutDetailTemp.OrderQty + Common.ConvertStringToDecimalQty(txtQty.Text)).ToString();
                }

                sampleOutDetailTemp.BatchNo = txtBatchNo.Text.Trim();

                sampleOutDetailTemp.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text);
                sampleOutDetailTemp.BalancedQty = Common.ConvertStringToDecimalQty(txtQty.Text);
                CalculateLine();

                sampleOutDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtRate.Text);
                sampleOutDetailTemp.NetAmount = Common.ConvertStringToDecimalCurrency(txtProductAmount.Text);

                SampleOutService sampleOutService = new SampleOutService();

                dgvItemDetails.DataSource = null;
                sampleOutDetailTempList = sampleOutService.getUpdateSampleOutDetailTemp(sampleOutDetailTempList, sampleOutDetailTemp);
                dgvItemDetails.DataSource = sampleOutDetailTempList;
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

                GetSummarizeFigures(sampleOutDetailTempList);
                EnableLine(false);
                Common.EnableTextBox(false, txtIssuedTo, txtDeliveryPerson, txtDocumentNo);
                Common.EnableComboBox(false, cmbLocation,cmbType);
                if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                ClearLine();
                if (sampleOutDetailTempList.Count > 0)
                    grpFooter.Enabled = true;

                txtProductCode.Enabled = true;
                txtProductCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void GetSummarizeFigures(List<SampleOutDetailTemp> listItem)
        {
            decimal netAmount = listItem.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.NetAmount);
            decimal qty = listItem.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.OrderQty);
            netAmount = Common.ConvertStringToDecimalCurrency(netAmount.ToString());
            qty = Common.ConvertStringToDecimalQty(qty.ToString());

            txtTotalAmount.Text = Common.ConvertDecimalToStringCurrency(netAmount);
            txtTotalQty.Text = Common.ConvertDecimalToStringQty(qty);
        }

        private void RefreshTextBoxes()
        {
            ////Load Quotation Document Numbers
            SampleOutService sampleOutService = new SampleOutService();
            Common.SetAutoComplete(txtDocumentNo, sampleOutService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

            //// Load Issued To
            SampleOutService SampleOutService = new SampleOutService();
            Common.SetAutoComplete(txtIssuedTo, SampleOutService.GetAllIssuedToNames(), true);

            //// Load Delivery Person
            Common.SetAutoComplete(txtDeliveryPerson, SampleOutService.GetAllDeliveryPersons(), true);
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

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if ((Common.ConvertStringToDecimal(txtQty.Text.Trim())) > 0)
                    {
                        decimal qty = Convert.ToDecimal(txtQty.Text.Trim());
                        SampleOutService sampleOutService = new SampleOutService();
                        CommonService commonService = new CommonService();

                        if (isInvProduct)
                        {
                            if (existingInvProductMaster.IsBatch)
                            {
                                if (!existingInvProductMaster.UnitOfMeasureID.Equals(Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())))
                                {
                                    InvProductUnitConversionService invProductUnitConversionService = new InvProductUnitConversionService();
                                    convertFactor = invProductUnitConversionService.GetProductUnitByProductCode(existingInvProductMaster.InvProductMasterID, Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())).ConvertFactor;
                                }
                                else
                                {
                                    convertFactor = 1;
                                }

                                if (commonService.ValidateBatchStock(qty, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), txtBatchNo.Text.Trim(), convertFactor)) { }
                                else
                                {
                                    Toast.Show("", Toast.messageType.Information, Toast.messageAction.BatchQtyExceed);
                                    txtQty.Focus();
                                    return;
                                }
                            }

                            if (existingInvProductMaster.IsSerial)
                            {
                                InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();
                                invProductSerialNoTemp.DocumentID = documentID;
                                invProductSerialNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                                invProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                                lgsProductSerialNoTempList = sampleOutService.GetInvSerialNoDetail(existingInvProductMaster);

                                if (lgsProductSerialNoTempList == null)
                                    lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                                FrmSerialCommon frmSerialCommon = new FrmSerialCommon(lgsProductSerialNoTempList, invProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, documentID, FrmSerialCommon.transactionType.SampleOut);
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
                            CalculateLine();
                        }
                        else
                        {
                            if (existingLgsProductMaster.IsBatch)
                            {
                                if (!existingLgsProductMaster.UnitOfMeasureID.Equals(Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())))
                                {
                                    LgsProductUnitConversionService lgsProductUnitConversionService = new LgsProductUnitConversionService();
                                    convertFactor = lgsProductUnitConversionService.GetProductUnitByProductCode(existingInvProductMaster.InvProductMasterID, Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())).ConvertFactor;
                                }
                                else
                                {
                                    convertFactor = 1;
                                }

                                if (commonService.ValidateBatchStock(qty, existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), txtBatchNo.Text.Trim(), convertFactor)) { }
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

                                lgsProductSerialNoTempList = sampleOutService.GetLgsSerialNoDetail(existingLgsProductMaster);

                                if (lgsProductSerialNoTempList == null)
                                    lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                                FrmSerialCommon frmSerialCommon = new FrmSerialCommon(lgsProductSerialNoTempList, lgsProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, documentID, FrmSerialCommon.transactionType.SampleOut);
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
                            CalculateLine();
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
                            invProductSerialNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                            invProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                            if (lgsProductSerialNoTempList == null)
                                lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                            FrmSerialCommon frmSerialCommon = new FrmSerialCommon(lgsProductSerialNoTempList, invProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, documentID, FrmSerialCommon.transactionType.SampleOut);
                            frmSerialCommon.ShowDialog();
                        }
                    }
                    else
                    {
                        if (existingLgsProductMaster.IsSerial)
                        {
                            InvProductSerialNoTemp lgsProductSerialNoTemp = new InvProductSerialNoTemp();
                            lgsProductSerialNoTemp.DocumentID = documentID;
                            lgsProductSerialNoTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                            lgsProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                            if (lgsProductSerialNoTempList == null)
                                lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                            FrmSerialCommon frmSerialCommon = new FrmSerialCommon(lgsProductSerialNoTempList, lgsProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, documentID, FrmSerialCommon.transactionType.SampleOut);
                            frmSerialCommon.ShowDialog();
                        }
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
            lgsProductSerialNoTempList = setInvProductSerialNoTemp;
        }

        private void txtProductAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    UpdateGrid(existingSampleOutDetailTemp);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo, txtIssuedTo, txtDeliveryPerson);
        }

        public override void Pause()
        {
            if ((Toast.Show("Sample Out  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateControls().Equals(false)) { return; }
                if (SaveDocument(0, txtDocumentNo.Text.Trim()).Equals(true))
                {
                    Toast.Show("Sample Out  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    ClearForm();
                    RefreshTextBoxes();
                }
                else
                {
                    Toast.Show("Sample Out  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        public override void Save()
        {
            if ((Toast.Show("Sample Out  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateControls().Equals(false)) { return; }
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim());
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Sample Out  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(txtDocumentNo.Text.Trim(), 1);
                    ClearForm();
                    RefreshTextBoxes();
                }
                else
                {
                    Toast.Show("Sample Out  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        private bool SaveDocument(int documentStatus, string documentNo)
        {
            try
            {
                SampleOutService sampleOutService = new SampleOutService();
                SampleOutHeader sampleOutHeader = new SampleOutHeader();
                LocationService locationService = new LocationService();
                Location Location = new Location();

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                sampleOutHeader = sampleOutService.GetPausedSampleOutHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (sampleOutHeader == null)
                    sampleOutHeader = new SampleOutHeader();

                if (documentStatus.Equals(1)) // update paused document
                {
                    documentNo = GetDocumentNo(false, isInvProduct);
                    txtDocumentNo.Text = documentNo;
                }

                sampleOutHeader.CompanyID = Location.CompanyID;
                sampleOutHeader.DocumentDate = Common.ConvertStringToDate(dtpIssuedDate.Value.ToString());
                sampleOutHeader.IssuedTo = txtIssuedTo.Text.Trim();
                sampleOutHeader.SampleOutType = type;
                sampleOutHeader.DeliveryPerson = txtDeliveryPerson.Text.Trim();
                sampleOutHeader.DocumentID = documentID;
                sampleOutHeader.DocumentStatus = documentStatus;
                sampleOutHeader.DocumentNo = documentNo.Trim();
                sampleOutHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                sampleOutHeader.LocationID = Location.LocationID;
                sampleOutHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtTotalAmount.Text.ToString());
                sampleOutHeader.TotalQty = Common.ConvertStringToDecimalQty(txtTotalQty.Text.ToString());
                sampleOutHeader.TotalBalancedQty = Common.ConvertStringToDecimalQty(txtTotalQty.Text.ToString());
                sampleOutHeader.ReferenceNo = txtReference.Text.Trim();
                sampleOutHeader.Remark = txtRemark.Text.Trim();

                if (sampleOutDetailTempList == null)
                    sampleOutDetailTempList = new List<SampleOutDetailTemp>();

                if (lgsProductSerialNoTempList == null)
                    lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                return sampleOutService.Save(sampleOutHeader, sampleOutDetailTempList, lgsProductSerialNoTempList, isInvProduct);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return false;
            }


        }

        private void RecallSampleOutType(int type) 
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
                SampleOutService sampleOutService = new SampleOutService();
                SampleOutHeader sampleOutHeader = new SampleOutHeader();

                sampleOutHeader = sampleOutService.GetPausedSampleOutHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (sampleOutHeader != null)
                {
                    cmbLocation.SelectedValue = sampleOutHeader.LocationID;
                    cmbLocation.Refresh();

                    documentState = sampleOutHeader.DocumentStatus;
                    dtpIssuedDate.Value = Common.FormatDate(sampleOutHeader.DocumentDate);

                    txtDocumentNo.Text = sampleOutHeader.DocumentNo;
                    txtTotalAmount.Text = Common.ConvertDecimalToStringCurrency(sampleOutHeader.NetAmount);
                    txtTotalQty.Text = Common.ConvertDecimalToStringQty(sampleOutHeader.TotalQty);
                    txtReference.Text = sampleOutHeader.ReferenceNo;
                    txtRemark.Text = sampleOutHeader.Remark;
                    txtIssuedTo.Text = sampleOutHeader.IssuedTo;
                    txtDeliveryPerson.Text = sampleOutHeader.DeliveryPerson;

                    if (sampleOutHeader.SampleOutType.Equals(1))
                    {
                        RecallSampleOutType(1);
                    }
                    else if (sampleOutHeader.SampleOutType.Equals(2))
                    {
                        RecallSampleOutType(2);
                    }


                    dgvItemDetails.DataSource = null;
                    sampleOutDetailTempList = sampleOutService.GetPausedSampleOutDetail(sampleOutHeader, isInvProduct);
                    dgvItemDetails.DataSource = sampleOutDetailTempList;
                    dgvItemDetails.Refresh();

                    if (isInvProduct)
                    {
                        lgsProductSerialNoTempList = sampleOutService.getInvSerialNoDetailForSampleOut(sampleOutHeader); 
                    }
                    else
                    {
                        lgsProductSerialNoTempList = sampleOutService.getLgsSerialNoDetailForSampleOut(sampleOutHeader);
                    }

                    Common.EnableTextBox(false, txtIssuedTo, txtDeliveryPerson, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation, cmbType);

                    if (sampleOutHeader.DocumentStatus.Equals(0))
                    {
                        grpFooter.Enabled = true;
                        EnableLine(false);
                        EnableProductDetails(true);
                        LoadProducts(isInvProduct);
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

        public override void ClearForm()
        {
            errorProvider.Clear();
            dtpIssuedDate.Value = DateTime.Now;
            dgvItemDetails.DataSource = null;
            dgvItemDetails.Refresh();
            base.ClearForm();
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
                    RecallDocument(txtDocumentNo.Text.Trim());
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBatchNo_Leave(object sender, EventArgs e)
        {
            try
            {
                txtQty.Enabled = true;
                if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                    txtQty.Text = "1";
                txtQty.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                if (isInvProduct)
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
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationDocumentNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SampleOutService SampleOutService = new SampleOutService();
                Common.SetAutoComplete(txtDocumentNo, SampleOutService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
       }

        private void btnDocumentDetails_Click(object sender, EventArgs e)
        {
            try
            {
                //SampleOutService sampleOutService = new SampleOutService();
                //DataView dtvSampleOut = new DataView(sampleOutService.GetSampleOutDocumentNumberDataTable(Common.LoggedLocationID));
                //LoadReferenceSearchForm(dtvSampleOut, this.Name.Trim(), this.Name.Trim(), string.Empty, txtDocumentNo);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// Load Reference Search form on demand
        /// </summary>
        private void LoadReferenceSearchForm(DataView dvAllReferenceData, string parentOfSearch, string searchFormCaption, string searchText, Control focusControl)
        {
            try
            {
                FrmReferenceSearch referenceSearch = new FrmReferenceSearch();
                referenceSearch.DvResults = dvAllReferenceData;
                referenceSearch.ParentOfSearch = parentOfSearch.Trim();
                referenceSearch.FormCaption = searchFormCaption.Trim();
                referenceSearch.SearchText = searchText.Trim();
                referenceSearch.FocusControl = focusControl;

                if (referenceSearch.IsDisposed)
                { referenceSearch = new FrmReferenceSearch(); }

                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is FrmReferenceSearch)
                    {
                        FrmReferenceSearch masterSearch2 = (FrmReferenceSearch)frm;
                        if (string.Equals(masterSearch2.ParentOfSearch.Trim(), this.Name.Trim()))
                        { return; }
                    }
                }

                referenceSearch.ShowDialog();
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

                            SampleOutDetailTemp sampleOutDetailTemp = new SampleOutDetailTemp();
                            UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                            InvProductMasterService invProductMasterService = new InvProductMasterService();

                            sampleOutDetailTemp.ProductID = invProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).InvProductMasterID;
                            sampleOutDetailTemp.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                            SampleOutService sampleOutService = new SampleOutService();

                            dgvItemDetails.DataSource = null;
                            sampleOutDetailTempList = sampleOutService.GetDeleteSampleOutDetailTemp(sampleOutDetailTempList, sampleOutDetailTemp);
                            dgvItemDetails.DataSource = sampleOutDetailTempList;
                            dgvItemDetails.Refresh();

                            if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                            else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }

                            GetSummarizeFigures(sampleOutDetailTempList);
                            this.ActiveControl = txtProductCode;
                            txtProductCode.Focus();
                        }
                        else
                        {
                            if (Toast.Show("Product " + dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + " - " + dgvItemDetails["ProductName", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                                return;

                            SampleOutDetailTemp sampleOutDetailTemp = new SampleOutDetailTemp();
                            UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                            sampleOutDetailTemp.ProductID = lgsProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).LgsProductMasterID;
                            sampleOutDetailTemp.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                            SampleOutService sampleOutService = new SampleOutService();

                            dgvItemDetails.DataSource = null;
                            sampleOutDetailTempList = sampleOutService.GetDeleteSampleOutDetailTemp(sampleOutDetailTempList, sampleOutDetailTemp);
                            dgvItemDetails.DataSource = sampleOutDetailTempList;
                            dgvItemDetails.Refresh();

                            if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                            else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }

                            GetSummarizeFigures(sampleOutDetailTempList);
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

    }
}
