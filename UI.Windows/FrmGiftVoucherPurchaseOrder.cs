using System.Linq;
using Domain;
using Report.GV;
using Service;
using Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace UI.Windows
{
    public partial class FrmGiftVoucherPurchaseOrder : UI.Windows.FrmBaseTransactionForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<InvGiftVoucherPurchaseOrderDetail> invGiftVoucherPurchaseOrderDetailsTemp = new List<InvGiftVoucherPurchaseOrderDetail>();
        List<InvGiftVoucherPurchaseOrderDetail> invGiftVoucherPurchaseOrderDetailsTempList = new List<InvGiftVoucherPurchaseOrderDetail>();
        int documentID = 2; // ??????????????

        private decimal creditLimit;
        private int creditPeriod;
        private decimal chequeLimit;
        private int chequePeriod;
        private int noOfVouchers;

        public FrmGiftVoucherPurchaseOrder()
        {
            InitializeComponent();
        }

        #region Form Events
        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateControls()) { return; }

                if (!IsValidateControls())
                { return; }
                
                if ((Toast.Show(this.Text + Common.ConvertStringToDisplayFormat(lblBook.Text.Trim()) + " - " + txtBookCode.Text.Trim() + " - " + txtVoucherValue.Text.Trim() + ", Count - " + noOfVouchers, Toast.messageType.Question, Toast.messageAction.Generate).Equals(DialogResult.No)))
                {
                    return;
                }

                //LoadGiftVouchers();
                AssignGiftVoucherProperties();

                //Toast.Show(this.Text + " - " + txtGroupCode.Text.Trim() + " - " + txtVoucherValue.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.Generate);
                //Common.EnableButton(true, btnSave);
                //Common.EnableTextBox(false, txtBookCode, txtBookName, txtGroupCode, txtGroupName, txtVoucherValue, txtNoOfVouchersOnBook, txtNoOfVouchers, txtPrefix, txtLength, txtStartingNo);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPurchaseOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { 
                    return; 
                }
                else
                {
                    if (!txtPurchaseOrderNo.Text.Trim().Equals(string.Empty))
                    {LoadPurchaseOrderDocument();}
                    else
                    {
                        txtPurchaseOrderNo.Text = GetDocumentNo(true);
                        txtSupplierCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtSupplierCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                    DataView dvAllReferenceData = new DataView(lgsSupplierService.GetAllActiveLgsSuppliersDataTableBySupplierType(lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.SupplierType).ToString(), 1).LookupKey));
                    if (dvAllReferenceData.Count > 0)
                    { 
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtSupplierCode);
                        txtSupplierCode_Validated(this, e);
                    }
                }

                Common.SetFocus(e, txtRemark); 
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                    DataView dvAllReferenceData = new DataView(lgsSupplierService.GetAllActiveLgsSuppliersDataTableBySupplierType(lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.SupplierType).ToString(), 1).LookupKey));
                    if (dvAllReferenceData.Count > 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtSupplierCode);
                        txtSupplierCode_Validated(this, e);
                    }
                }

                Common.SetFocus(e, txtSupplierCode);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, dtpExpectedDate); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dtpExpectedDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, dtpDocumentDate); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dtpDocumentDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtValidityPeriod); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtValidityPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtReferenceNo); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtReferenceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { 
                Common.SetFocus(e, cmbLocation); 
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { 
                if (e.KeyCode.Equals(Keys.Enter))
                {cmbLocation_Validated(this, e);}
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbSelectionCriteria_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(cmbSelectionCriteria.Text.Trim()))
                //{Common.SetFocus(e, txtBookCode);} 
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtBookCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterBookService = new InvGiftVoucherBookCodeGenerationService();
                    DataView dvAllReferenceData = new DataView(invGiftVoucherMasterBookService.GetAllActiveGiftVoucherMasterBooksDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2)));
                    if (dvAllReferenceData.Count > 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtBookCode);
                        txtBookCode_Validated(this, e);
                    }
                }
                Common.SetFocus(e, cmbSelectionCriteria);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtBookName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterBookService = new InvGiftVoucherBookCodeGenerationService();
                    DataView dvAllReferenceData = new DataView(invGiftVoucherMasterBookService.GetAllActiveGiftVoucherMasterBooksDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2)));
                    if (dvAllReferenceData.Count > 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtBookCode);
                        txtBookCode_Validated(this, e);
                    }
                }
                Common.SetFocus(e, txtBookCode); 
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtGroupCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                    DataView dvAllReferenceData = new DataView(invGiftVoucherGroupService.GetAllActiveGiftVoucherGroupsDataTable());
                    if (dvAllReferenceData.Count > 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtGroupCode);
                        txtGroupCode_Validated(this, e);
                    }
                }
                Common.SetFocus(e, txtBookCode);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtGroupName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                    DataView dvAllReferenceData = new DataView(invGiftVoucherGroupService.GetAllActiveGiftVoucherGroupsDataTable());
                    if (dvAllReferenceData.Count > 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtGroupCode);
                        txtGroupCode_Validated(this, e);
                    }
                }
                Common.SetFocus(e, txtGroupCode); 
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtGiftVoucherValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtNoOfVouchersOnBook); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtVoucherNoFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();

                    DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseOrderService.GetAllActiveGiftVoucherNosDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2), invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text).InvGiftVoucherGroupID, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text).InvGiftVoucherBookCodeID));
                    if (dvAllReferenceData.Count > 0)
                    { LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), this.ActiveControl); }
                }
                Common.SetFocus(e, txtVoucherNoTo);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtVoucherSerialFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();

                    DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseOrderService.GetAllActiveGiftVoucherSerialsDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2), invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text).InvGiftVoucherGroupID, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text).InvGiftVoucherBookCodeID));
                    if (dvAllReferenceData.Count > 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtVoucherSerialFrom);
                        txtVoucherSerialFrom_Leave(this, e);
                    }
                }
                Common.SetFocus(e, txtVoucherSerialTo);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dgvVoucherBookDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!Common.AllowDeleteGridRow(e))
                { return; }

                //InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                //invPurchaseOrderDetailsTemp = invPurchaseOrderService.DeleteProductInvPurchaseOrderDetailTemp(invPurchaseOrderDetailsTemp, dgvItemDetails[1, dgvItemDetails.CurrentRow.Index].Value.ToString().Trim());
                //UpdatedgvItemDetails();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtGrossAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtSubTotalDiscountPercentage); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtSubTotalDiscountPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtGrossAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtGrossAmount_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { CalculateFigures(); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtSubTotalDiscountPercentage_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { CalculateFigures(); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtSupplierCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSupplierCode.Text.Trim()))
                {
                    txtSupplierName.Focus();
                    return;
                }

                if (!IsSupplierExistsByCode(txtSupplierCode.Text.Trim()))
                { txtSupplierCode.Focus(); }
                else
                { //LoadGiftVouchers(); 
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtSupplierName_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSupplierName.Text.Trim()))
                { return; }

                if (!IsSupplierExistsByName(txtSupplierName.Text.Trim()))
                { txtSupplierName.Focus(); }
                else
                { //LoadGiftVouchers(); 
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtValidityPeriod_Validated(object sender, EventArgs e)
        {
            try
            { }
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
                else
                {
                    accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbLocation.Text.Trim()).LocationID, documentID);
                    if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }

                    Common.EnableComboBox(true, cmbSelectionCriteria,cmbPaymentTerms);
                    cmbPaymentTerms.Focus();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtBookCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBookCode.Text.Trim()))
                {
                    txtBookName.Focus();
                    return;
                }

                if (!IsBookCodeExistsByCode(txtBookCode.Text.Trim()))
                { txtBookCode.Focus(); }
                else
                { //LoadGiftVouchers(); 
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtBookName_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBookName.Text.Trim()))
                { return; }

                if (!IsBookCodeExistsByName(txtBookName.Text.Trim()))
                { txtBookName.Focus(); }
                else
                { //LoadGiftVouchers(); 
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtGroupCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtGroupCode.Text.Trim()))
                {
                    txtGroupName.Focus();
                    return;
                }

                if (!IsGroupCodeExistsByCode(txtGroupCode.Text.Trim()))
                { txtGroupCode.Focus(); }
                else
                { //LoadGiftVouchers(); 
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtGroupName_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtGroupName.Text.Trim()))
                {
                    return;
                }

                if (!IsGroupCodeExistsByName(txtGroupName.Text.Trim()))
                { txtGroupName.Focus(); }
                else
                { //LoadGiftVouchers(); 
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }
        
        private void txtGrossAmount_Validated(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtGrossAmount.Text.Trim()))
                //{
                //    return;
                //}
                //else
                //{
                //    CalculateFigures();
                //}
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtSubTotalDiscountPercentage_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSubTotalDiscountPercentage.Text.Trim()))
                {
                    return;
                }
                else
                {
                    CalculateFigures();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoCompleationPoNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                Common.SetAutoComplete(txtPurchaseOrderNo, invGiftVoucherPurchaseOrderService.GetPausedDocumentNumbers(), chkAutoCompleationPoNo.Checked);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoCompleationSupplier_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                Common.SetAutoComplete(txtSupplierCode, lgsSupplierService.GetAllLgsSupplierCodesBySupplierType(lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.LogisticSupplierType).ToString(), 1).LookupKey), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, lgsSupplierService.GetAllLgsSupplierNamesBySupplierType(lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.LogisticSupplierType).ToString(), 1).LookupKey), chkAutoCompleationSupplier.Checked);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }
       
        private void chkSubTotalDiscountPercentage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateFigures();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbSelectionCriteria_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbSelectionCriteria.Text.Trim()))
                { return; }

                if (!IsCriteriaExistsByIndex(cmbSelectionCriteria.SelectedIndex))
                {
                    //btnLoad.Enabled = true;
                    //txtBookCode.Focus();
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void rdoVoucher_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ResetVoucherType();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }
        private void rdoCoupon_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ResetVoucherType();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbPaymentTerms_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbPaymentTerms.Text.Trim()))
                { return; }
               
                if (!IsPaymentMethodExistsByID(cmbPaymentTerms.SelectedIndex + 1))
                {
                    cmbPaymentTerms.Focus();
                    return;
                }
                else
                {
                    //groupBox4.Enabled = true;

                    //rdoVoucher.Focus();
                    //LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                    //LgsSupplierService supplierService = new LgsSupplierService();
                    //LgsSupplier supplier = new LgsSupplier();
                    //supplier = supplierService.GetLgsSupplierByCode(txtSupplierCode.Text);
                    //if (supplier != null)
                    //{
                    //    FrmPaymentMethodLimit frmPaymentMethodLimit = new FrmPaymentMethodLimit(true, cmbPaymentTerms.Text.Trim(), creditLimit, creditPeriod, chequeLimit, chequePeriod, FrmPaymentMethodLimit.transactionType.GiftVoucherPurchase);
                    //    frmPaymentMethodLimit.ShowDialog();
                    //}
                    //else
                    //{
                    //    Common.ClearTextBox(txtSupplierCode, txtSupplierName);
                    //    Toast.Show(lblSupplier.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    //}

                    
                    btnPaymentTermLimits.Focus();

                    //Common.EnableComboBox(true, cmbSelectionCriteria);
                    //cmbSelectionCriteria.Focus();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtOtherCharges_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { CalculateFigures(); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbPaymentTerms_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (txtSupplierCode.Text.Trim() != "" && cmbPaymentTerms.Text.Trim() != "")
                    {
                        cmbPaymentTerms_Validated(this, e);
                    }
                   
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void btnTaxBreakdown_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplierCode.Text.Trim() != "")
                {
                    LgsSupplierService supplierService = new LgsSupplierService();

                    FrmTaxBreakdown frmTaxBreakdown = new FrmTaxBreakdown(supplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim()).LgsSupplierID, 3, Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.Trim()));
                    frmTaxBreakdown.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTaxEnable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateFigures();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoCompleationGroup_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                Common.SetAutoComplete(txtGroupCode, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupCodes(), chkAutoCompleationGroup.Checked);
                Common.SetAutoComplete(txtGroupName, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupNames(), chkAutoCompleationGroup.Checked);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoCompleationBook_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadBookByVoucherType();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void btnPaymentTermLimits_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplierCode.Text.Trim() != "" && cmbPaymentTerms.Text.Trim() != "")
                {
                    LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                    LgsSupplierService supplierService = new LgsSupplierService();
                    LgsSupplier supplier = new LgsSupplier();
                    supplier = supplierService.GetLgsSupplierByCode(txtSupplierCode.Text);
                    if (supplier != null)
                    {
                        FrmPaymentMethodLimit frmPaymentMethodLimit = new FrmPaymentMethodLimit(true, cmbPaymentTerms.Text.Trim(), creditLimit, creditPeriod, chequeLimit, chequePeriod, FrmPaymentMethodLimit.transactionType.GiftVoucherPurchase);
                        frmPaymentMethodLimit.ShowDialog();
                        txtPurchaseOrderNo.Text = GetDocumentNo(true);
                        txtPurchaseOrderNo.Enabled = false;
                        ResetVoucherType();
                        
                    }
                    else
                    {
                        Common.ClearTextBox(txtSupplierCode, txtSupplierName);
                        Toast.Show(lblSupplier.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtVoucherSerialTo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtVoucherSerialTo.Text.Trim()))
                {
                    InvGiftVoucherMaster invGiftVoucherMaster = new InvGiftVoucherMaster();
                    InvGiftVoucherMasterService invGiftVoucherMasterService = new InvGiftVoucherMasterService();

                    invGiftVoucherMaster = invGiftVoucherMasterService.GetInvGiftVoucherMasterToPOByVoucherSerial(txtVoucherSerialTo.Text);
                    if (invGiftVoucherMaster == null)
                    {
                        Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherSerial.Text.ToString())," ",lblVoucherNoTo.Text.ToString()," - ",txtVoucherNoTo.Text.ToString()), Toast.messageType.Information, Toast.messageAction.Invalid);
                        return;
                    }
                    else
                    {
                        txtVoucherSerialQty.Text = Common.ConvertDecimalToStringQty(LoadSelectedVoucherQty());
                        noOfVouchers = Common.ConvertStringToInt(txtVoucherSerialQty.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtVoucherNoTo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtVoucherNoTo.Text.Trim()))
                {
                    InvGiftVoucherMaster invGiftVoucherMaster = new InvGiftVoucherMaster();
                    InvGiftVoucherMasterService invGiftVoucherMasterService = new InvGiftVoucherMasterService();

                    invGiftVoucherMaster = invGiftVoucherMasterService.GetInvGiftVoucherMasterToPOByVoucherNo(txtVoucherNoTo.Text);
                    if (invGiftVoucherMaster == null)
                    {
                        Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherNo.Text.ToString()), " ", lblVoucherNoTo.Text.ToString(), " - ", txtVoucherNoTo.Text.ToString()), Toast.messageType.Information, Toast.messageAction.Invalid);
                        return;
                    }
                    else
                    {
                        txtVoucherNoQty.Text = Common.ConvertDecimalToStringQty(LoadSelectedVoucherQty());
                        noOfVouchers = Common.ConvertStringToInt(txtVoucherNoQty.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtVoucherNoFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtVoucherNoFrom.Text.Trim()))
                {
                    InvGiftVoucherMaster invGiftVoucherMaster = new InvGiftVoucherMaster();
                    InvGiftVoucherMasterService invGiftVoucherMasterService = new InvGiftVoucherMasterService();

                    invGiftVoucherMaster = invGiftVoucherMasterService.GetInvGiftVoucherMasterToPOByVoucherNo(txtVoucherNoFrom.Text);
                    if (invGiftVoucherMaster == null)
                    {
                        Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherNo.Text.ToString()), " ", lblVoucherNoFrom.Text.ToString(), " - ", txtVoucherNoFrom.Text.ToString()), Toast.messageType.Information, Toast.messageAction.Invalid);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherCharges_Validated(object sender, EventArgs e)
        {
            try
            {
                txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text));
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherCharges_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtNetAmount);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtVoucherSerialFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtVoucherSerialFrom.Text.Trim()))
                {
                    InvGiftVoucherMaster invGiftVoucherMaster = new InvGiftVoucherMaster();
                    InvGiftVoucherMasterService invGiftVoucherMasterService = new InvGiftVoucherMasterService();

                    invGiftVoucherMaster = invGiftVoucherMasterService.GetInvGiftVoucherMasterToPOByVoucherSerial(txtVoucherSerialFrom.Text);
                    if (invGiftVoucherMaster == null)
                    {
                        Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherSerial.Text.ToString()), " ", lblVoucherSerialFrom.Text.ToString(), " - ", txtVoucherSerialFrom.Text.ToString()), Toast.messageType.Information, Toast.messageAction.Invalid);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Setup GUI contols (Set visibility, data binding, auto loading)
        /// </summary>
        public override void InitializeForm()
        {
            // Load Payment methods
            PaymentMethodService paymentMethodService = new PaymentMethodService();
            Common.LoadPaymentMethods(cmbPaymentTerms, paymentMethodService.GetAllPaymentMethods());

            // Load Locations            
            LocationService locationService = new LocationService();
            //Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
            Common.LoadLocations(cmbLocation, locationService.GetHeadOfficeLocation());
            cmbLocation.SelectedValue = Common.LoggedLocationID;

            // Disable gift voucher selection details controls
            cmbSelectionCriteria.SelectedIndex = -1;
            cmbBasedOn.SelectedIndex = -1;
            //IsCriteriaExistsByIndex(-1);

            Common.SetDataGridviewColumnsReadOnly(true, dgvVoucherBookDetails, dgvVoucherBookDetails.Columns[1], dgvVoucherBookDetails.Columns[2], dgvVoucherBookDetails.Columns[3]);
            Common.ReadOnlyTextBox(true, txtGiftVoucherValue, txtNoOfVouchersOnBook, txtVoucherSerialQty, txtVoucherNoQty);

            Common.EnableTextBox(true, txtSupplierCode, txtSupplierName, txtPurchaseOrderNo);
            Common.EnableButton(true, btnDocumentDetails,btnPaymentTermLimits);
            Common.EnableComboBox(true, cmbLocation,cmbPaymentTerms);
            Common.EnableComboBox(false, cmbSelectionCriteria);
            Common.EnableButton(false, btnSave, btnPause);
            //if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
            //if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

            dtpDocumentDate.Value = DateTime.Now;
            dtpExpectedDate.Value = DateTime.Now;

            txtPurchaseOrderNo.Text = GetDocumentNo(true);

            invGiftVoucherPurchaseOrderDetailsTemp = null;

            grpHeader.Enabled = true;
            grpFooter.Enabled = false;
            groupBox4.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;

            this.ActiveControl = txtSupplierCode;
            txtSupplierCode.Focus();
        }

        public override void FormLoad()
        {
            try
            {
                dgvVoucherBookDetails.AutoGenerateColumns = false;

                // Load Locations
                LocationService locationService = new LocationService();
                //Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
                Common.LoadLocations(cmbLocation, locationService.GetHeadOfficeLocation());

                // Read Non Trade Logistic Suppliers
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                Common.SetAutoComplete(txtSupplierCode, lgsSupplierService.GetAllLgsSupplierCodesBySupplierType(lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.LogisticSupplierType).ToString(), 1).LookupKey), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, lgsSupplierService.GetAllLgsSupplierNamesBySupplierType(lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.LogisticSupplierType).ToString(), 1).LookupKey), chkAutoCompleationSupplier.Checked);

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }
                
                base.FormLoad();

                RefreshDocumentNumbers();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void RefreshDocumentNumbers()
        {
            // Load PO Document Numbers
            InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
            Common.SetAutoComplete(txtPurchaseOrderNo, invGiftVoucherPurchaseOrderService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationPoNo.Checked);
        }

        public void SetPaymentTermLimit(decimal creditLimit, int creditPeriod, decimal chequeLimit, int chequePeriod)
        {
            creditLimit = creditLimit;
            creditPeriod = creditPeriod;
            chequeLimit = chequeLimit;
            chequePeriod = chequePeriod;
        }

        /// <summary>
        /// Pause Gift Voucher Purchase order.
        /// </summary>
        public override void Pause()
        {
            if ((Toast.Show(this.Text + " - " + txtPurchaseOrderNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                string NewDocumentNo;
                bool saveDocument = SaveDocument(0, txtPurchaseOrderNo.Text.Trim(), out NewDocumentNo);
                if (saveDocument.Equals(true))
                {
                    Toast.Show(this.Text + " - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 0);
                    RefreshDocumentNumbers();
                    ClearForm();
                }
                else
                {
                    Toast.Show(this.Text + " - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        /// <summary>
        /// Save New Gift Voucher Purchase order.
        /// </summary>
        public override void Save()
        {
            //if (!ValidateControls())
            //{ return; }

            if (!IsValidateExistsRecords()) { return; }

            if ((Toast.Show(this.Text + " - " + txtPurchaseOrderNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                if (!IsSupplierExistsByCode(txtSupplierCode.Text.Trim())) { return; }
                if (!IsLocationExistsByName(cmbLocation.Text.Trim())) { return; }
                //if (!IsLocationExistsByID(cmbLocation.SelectedIndex)) { return; }
                string NewDocumentNo;
                bool saveDocument = SaveDocument(1, txtPurchaseOrderNo.Text.Trim(), out NewDocumentNo);
                if (saveDocument.Equals(true))
                {
                    Toast.Show(this.Text + " - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 1);
                    RefreshDocumentNumbers();
                    ClearForm();
                }
                else
                {
                    Toast.Show(this.Text + " - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        public override void ClearForm()
        {
            dgvVoucherBookDetails.DataSource = null;
            dgvVoucherBookDetails.Refresh();
            base.ClearForm();
            txtSubTotalDiscount.Text = string.Empty;
        }

        private void GenerateReport(string documentNo, int documentStatus)
        {
            GvReportGenerator gvReportGenerator = new GvReportGenerator();
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            gvReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
        }

        /// <summary>
        /// Load paused document
        /// </summary>
        private void LoadPurchaseOrderDocument()
        {
            InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
            InvGiftVoucherPurchaseOrderHeader existingInvGiftVoucherPurchaseOrderHeader = new InvGiftVoucherPurchaseOrderHeader();

            existingInvGiftVoucherPurchaseOrderHeader = invGiftVoucherPurchaseOrderService.GetInvGiftVoucherPurchaseOrderHeaderByDocumentNo(documentID, txtPurchaseOrderNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

            if (existingInvGiftVoucherPurchaseOrderHeader == null)
            {
                Toast.Show(this.Text + " - " + txtPurchaseOrderNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            else
            {
                txtPurchaseOrderNo.Text = existingInvGiftVoucherPurchaseOrderHeader.DocumentNo;
                IsSupplierExistsByID(existingInvGiftVoucherPurchaseOrderHeader.SupplierID);
                txtRemark.Text = existingInvGiftVoucherPurchaseOrderHeader.Remark;
                dtpExpectedDate.Value = existingInvGiftVoucherPurchaseOrderHeader.ExpectedDate;
                txtValidityPeriod.Text = existingInvGiftVoucherPurchaseOrderHeader.ExpiryDate.Subtract(existingInvGiftVoucherPurchaseOrderHeader.ExpectedDate).Days.ToString();

                dtpDocumentDate.Value = existingInvGiftVoucherPurchaseOrderHeader.DocumentDate;
                txtReferenceNo.Text = existingInvGiftVoucherPurchaseOrderHeader.ReferenceNo;
                cmbLocation.SelectedValue = existingInvGiftVoucherPurchaseOrderHeader.LocationID;

                cmbPaymentTerms.SelectedValue = existingInvGiftVoucherPurchaseOrderHeader.PaymentMethodID;

                creditLimit = existingInvGiftVoucherPurchaseOrderHeader.ChequeLimit;
                creditPeriod = existingInvGiftVoucherPurchaseOrderHeader.CreditPeriod;
                chequeLimit = existingInvGiftVoucherPurchaseOrderHeader.ChequeLimit;
                chequePeriod = existingInvGiftVoucherPurchaseOrderHeader.ChequePeriod;

                if (!existingInvGiftVoucherPurchaseOrderHeader.DiscountPercentage.Equals(0))
                {
                    txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseOrderHeader.DiscountPercentage);
                    chkSubTotalDiscountPercentage.Checked = true;
                }
                else
                {
                    txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseOrderHeader.DiscountAmount);
                    chkSubTotalDiscountPercentage.Checked = false;
                }

                if (!existingInvGiftVoucherPurchaseOrderHeader.TaxAmount.Equals(0))
                {
                    chkTaxEnable.Checked = true;
                    txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseOrderHeader.TaxAmount);
                }
                else
                {
                    chkTaxEnable.Checked = false;
                    txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                }
                txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseOrderHeader.OtherCharges);
                txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseOrderHeader.DiscountAmount);
                txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseOrderHeader.GrossAmount);
                txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseOrderHeader.NetAmount);
                txtTotalQty.Text = existingInvGiftVoucherPurchaseOrderHeader.GiftVoucherQty.ToString();
                if (existingInvGiftVoucherPurchaseOrderHeader.VoucherType == 1)
                {
                    rdoVoucher.Checked = true;
                    txtGiftVoucherValue.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseOrderHeader.GiftVoucherAmount);
                }
                else if (existingInvGiftVoucherPurchaseOrderHeader.VoucherType == 2)
                {
                    rdoCoupon.Checked = true;
                    txtPercentageOfCoupon.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseOrderHeader.GiftVoucherPercentage);
                }

                dgvVoucherBookDetails.DataSource = null;
                invGiftVoucherPurchaseOrderDetailsTemp = invGiftVoucherPurchaseOrderService.GetAllPurchaseOrderDetail(existingInvGiftVoucherPurchaseOrderHeader);
                dgvVoucherBookDetails.DataSource = invGiftVoucherPurchaseOrderDetailsTemp;
                dgvVoucherBookDetails.Refresh();

                Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtPurchaseOrderNo);
                Common.EnableComboBox(false, cmbLocation, cmbPaymentTerms);
                Common.EnableButton(false, btnDocumentDetails, btnPaymentTermLimits);

                if (existingInvGiftVoucherPurchaseOrderHeader.DocumentStatus.Equals(0))
                {
                    //tabGRN.Enabled = true;
                    grpFooter.Enabled = true;
                    //EnableLine(true);
                    
                    grpBody.Enabled = true;
                    dgvVoucherBookDetails.Enabled = true;
                    ResetVoucherType();
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    txtGroupCode.Focus();
                }
                else
                {
                    grpHeader.Enabled = false;
                    Common.EnableButton(false, btnPause, btnSave, btnPaymentTermLimits);
                }
            }
        }

        /// <summary>
        /// Load and bind gift vouchers into text boxes depending on selection criteria
        /// </summary>
        private void LoadGiftVouchers()
        {
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
            InvGiftVoucherMasterService invGiftVoucherMasterService = new InvGiftVoucherMasterService();

            //if (cmbSelectionCriteria.SelectedIndex.Equals(0))
            //{
            //    if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
            //    {
            //        Common.SetAutoComplete(txtVoucherNo, invGiftVoucherMasterService.GetAllVoucherNosByBookIDForQty(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtGiftVoucherQty.Text.Trim())), chkAutoCompleationVoucher.Checked);
            //        Common.SetAutoComplete(txtVoucherSerial, invGiftVoucherMasterService.GetAllVoucherSerialsByBookIDForQty(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtGiftVoucherQty.Text.Trim())), chkAutoCompleationVoucher.Checked);
            //    }
            //}
            //else if (cmbSelectionCriteria.SelectedIndex.Equals(1))
            //{
            //    if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
            //    {
            //        Common.SetAutoComplete(txtVoucherNo, invGiftVoucherMasterService.GetAllVoucherNosByBookIDForVoucherNoRange(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtVoucherNoFrom.Text.Trim()), Common.ConvertStringToInt(txtVoucherNoTo.Text.Trim())), chkAutoCompleationVoucher.Checked);
            //        Common.SetAutoComplete(txtVoucherSerial, invGiftVoucherMasterService.GetAllVoucherSerialsByBookIDForVoucherNoRange(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtVoucherNoFrom.Text.Trim()), Common.ConvertStringToInt(txtVoucherNoTo.Text.Trim())), chkAutoCompleationVoucher.Checked);
            //    }
            //}
            //else if (cmbSelectionCriteria.SelectedIndex.Equals(2))
            //{
            //    if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
            //    {
            //        Common.SetAutoComplete(txtVoucherNo, invGiftVoucherMasterService.GetAllVoucherNosByBookIDForVoucherSerialRange(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtVoucherSerialFrom.Text.Trim()), Common.ConvertStringToInt(txtVoucherSerialTo.Text.Trim())), chkAutoCompleationVoucher.Checked);
            //        Common.SetAutoComplete(txtVoucherSerial, invGiftVoucherMasterService.GetAllVoucherSerialsByBookIDForVoucherSerialRange(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtVoucherSerialFrom.Text.Trim()), Common.ConvertStringToInt(txtVoucherSerialTo.Text.Trim())), chkAutoCompleationVoucher.Checked);
            //    }
            //}
            //else
            //{
            //    Common.SetAutoComplete(txtVoucherNo, invGiftVoucherMasterService.GetAllGiftVoucherNos(), chkAutoCompleationVoucher.Checked);
            //    Common.SetAutoComplete(txtVoucherSerial, invGiftVoucherMasterService.GetAllGiftVoucherSerials(), chkAutoCompleationVoucher.Checked);
            //}
        }

        /// <summary>
        /// Assign Gift Voucher properties before adding into grid view
        /// </summary>
        private void AssignGiftVoucherProperties()
        {
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
            List<InvGiftVoucherPurchaseOrderDetail> existingAccPettyCashPaymentDetailList = new List<InvGiftVoucherPurchaseOrderDetail>();
            List<InvGiftVoucherMaster> invGiftVoucherMastersList = LoadVoucherBookDetails();

            //if (invGiftVoucherPurchaseOrderDetailsTemp == null)
            //{invGiftVoucherPurchaseOrderDetailsTemp = new List<InvGiftVoucherPurchaseOrderDetail>();}
             //invGiftVoucherPurchaseOrderDetailsTemp.Clear();
            
            if (invGiftVoucherPurchaseOrderDetailsTempList == null)
            {invGiftVoucherPurchaseOrderDetailsTempList = new List<InvGiftVoucherPurchaseOrderDetail>();}
            
            invGiftVoucherPurchaseOrderDetailsTempList.Clear();
            //txtPurchaseOrderNo.Text = GetDocumentNo(true);

            //
            if (invGiftVoucherPurchaseOrderDetailsTemp != null)
            {
                int bookID = invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID;

                invGiftVoucherPurchaseOrderDetailsTemp.RemoveAll(x => x.InvGiftVoucherBookCodeID == bookID);

                long lineNo = 0;
                invGiftVoucherPurchaseOrderDetailsTemp.ToList().ForEach(x => x.LineNo = lineNo += 1);
                
                if (invGiftVoucherPurchaseOrderDetailsTemp.Count.Equals(0))
                { lineNo = 0; }
                else
                { lineNo = invGiftVoucherPurchaseOrderDetailsTemp.Max(s => s.LineNo); }
                
                foreach (InvGiftVoucherMaster invGiftVoucherMaster in invGiftVoucherMastersList)
                {
                    lineNo += 1;
                    InvGiftVoucherPurchaseOrderDetail invGiftVoucherPurchaseOrderDetailtemp = invGiftVoucherPurchaseOrderDetailsTemp.Find(x => x.InvGiftVoucherMasterID == invGiftVoucherMaster.InvGiftVoucherMasterID);
                    if (invGiftVoucherPurchaseOrderDetailtemp != null)
                    {
                        var tc = new InvGiftVoucherPurchaseOrderDetail
                        {
                            InvGiftVoucherPurchaseOrderDetailID = invGiftVoucherPurchaseOrderDetailtemp.InvGiftVoucherPurchaseOrderDetailID,
                            GiftVoucherPurchaseOrderDetailID = invGiftVoucherPurchaseOrderDetailtemp.GiftVoucherPurchaseOrderDetailID,
                            InvGiftVoucherPurchaseOrderHeaderID = invGiftVoucherPurchaseOrderDetailtemp.InvGiftVoucherPurchaseOrderHeaderID,
                            CompanyID = invGiftVoucherPurchaseOrderDetailtemp.CompanyID,
                            LocationID = invGiftVoucherPurchaseOrderDetailtemp.LocationID,
                            DocumentID = invGiftVoucherPurchaseOrderDetailtemp.DocumentID,
                            DocumentNo = txtPurchaseOrderNo.Text.Trim(),
                            DocumentDate = Common.GetSystemDateWithTime(), //invGiftVoucherPurchaseOrderDetailtemp.DocumentDate,
                            LineNo = lineNo,
                            InvGiftVoucherMasterID = invGiftVoucherMaster.InvGiftVoucherMasterID,
                            InvGiftVoucherBookCodeID = invGiftVoucherMaster.InvGiftVoucherBookCodeID,
                            InvGiftVoucherGroupID = invGiftVoucherMaster.InvGiftVoucherGroupID,
                            VoucherNo = invGiftVoucherMaster.VoucherNo,
                            VoucherSerial = invGiftVoucherMaster.VoucherSerial,
                            VoucherAmount = invGiftVoucherMaster.GiftVoucherValue,
                            GiftVoucherPercentage = invGiftVoucherMaster.GiftVoucherPercentage,
                            VoucherValue = (rdoVoucher.Checked ? invGiftVoucherMaster.GiftVoucherValue : invGiftVoucherMaster.GiftVoucherPercentage),
                            VoucherType = (rdoVoucher.Checked ? 1 : 2),
                            NumberOfCount = 1,
                            //DocumentStatus = documentStatus,
                        };
                        invGiftVoucherPurchaseOrderDetailsTempList.Add(tc);
                    }
                    else
                    {
                        var tc = new InvGiftVoucherPurchaseOrderDetail
                        {
                            LineNo = lineNo,
                            DocumentDate = Common.GetSystemDateWithTime(),
                            DocumentNo = txtPurchaseOrderNo.Text.Trim(),
                            InvGiftVoucherMasterID = invGiftVoucherMaster.InvGiftVoucherMasterID,
                            InvGiftVoucherBookCodeID = invGiftVoucherMaster.InvGiftVoucherBookCodeID,
                            InvGiftVoucherGroupID = invGiftVoucherMaster.InvGiftVoucherGroupID,
                            VoucherNo = invGiftVoucherMaster.VoucherNo,
                            VoucherSerial = invGiftVoucherMaster.VoucherSerial,
                            VoucherAmount = invGiftVoucherMaster.GiftVoucherValue,
                            GiftVoucherPercentage = invGiftVoucherMaster.GiftVoucherPercentage,
                            VoucherValue = (rdoVoucher.Checked ? invGiftVoucherMaster.GiftVoucherValue : invGiftVoucherMaster.GiftVoucherPercentage),
                            VoucherType = (rdoVoucher.Checked ? 1 : 2),
                            NumberOfCount = 1,
                            //DocumentStatus = documentStatus,
                        };
                        invGiftVoucherPurchaseOrderDetailsTempList.Add(tc);
                    }

                }


                //if (invGiftVoucherPurchaseOrderDetailsTempList.Count() < invGiftVoucherPurchaseOrderDetailsTemp.Count())
                //{
                var deletedInvGiftVoucherPurchaseOrderDetails = CommonService.Except(invGiftVoucherPurchaseOrderDetailsTemp, invGiftVoucherPurchaseOrderDetailsTempList, paymentDeta => paymentDeta.VoucherSerial);
                //invGiftVoucherPurchaseOrderDetailsTemp = invGiftVoucherPurchaseOrderDetailsTempList;
                invGiftVoucherPurchaseOrderDetailsTemp.AddRange(invGiftVoucherPurchaseOrderDetailsTempList);

                //foreach (InvGiftVoucherPurchaseOrderDetail invGiftVoucherPurchaseOrderDetailDelete in deletedInvGiftVoucherPurchaseOrderDetails)
                //{
                //    invGiftVoucherPurchaseOrderDetailsTemp.Remove(invGiftVoucherPurchaseOrderDetailDelete);
                //}
               

                //    var insertedInvGiftVoucherPurchaseOrderDetails = CommonService.Except(invGiftVoucherPurchaseOrderDetailsTemp, deletedInvGiftVoucherPurchaseOrderDetails.ToList(), paymentDeta => paymentDeta.VoucherSerial);
                //    invGiftVoucherPurchaseOrderDetailsTemp = insertedInvGiftVoucherPurchaseOrderDetails.ToList();
                //}
                ////else
                //{
                //    var updatedInvGiftVoucherPurchaseOrderDetails = invGiftVoucherPurchaseOrderDetailsTempList.ToList();

                //    var addedInvGiftVoucherPurchaseOrderDetails = CommonService.Except(updatedInvGiftVoucherPurchaseOrderDetails, invGiftVoucherPurchaseOrderDetailsTemp, paymentDeta => paymentDeta.VoucherSerial);

                //    if (addedInvGiftVoucherPurchaseOrderDetails != null && addedInvGiftVoucherPurchaseOrderDetails.Any())
                //    {
                //        invGiftVoucherPurchaseOrderDetailsTemp = addedInvGiftVoucherPurchaseOrderDetails.ToList();

                //        var modifiedAccPettyCashPaymentDetails = CommonService.Except(updatedInvGiftVoucherPurchaseOrderDetails, addedInvGiftVoucherPurchaseOrderDetails, paymentDeta => paymentDeta.VoucherSerial);
                //        invGiftVoucherPurchaseOrderDetailsTemp.AddRange(modifiedAccPettyCashPaymentDetails.ToList());
                //    }
                //    invGiftVoucherPurchaseOrderDetailsTemp.OrderBy(x=>x.VoucherSerial).ToList();
                //}
            }
            else if (invGiftVoucherPurchaseOrderDetailsTemp==null)
            {
                int lineNo = 0;
                foreach (InvGiftVoucherMaster invGiftVoucherMaster in invGiftVoucherMastersList)
                {
                    lineNo += 1;
                    var tc = new InvGiftVoucherPurchaseOrderDetail
                    {
                        LineNo = lineNo,
                        DocumentDate = Common.GetSystemDateWithTime(),
                        DocumentNo = txtPurchaseOrderNo.Text.Trim(),
                        InvGiftVoucherMasterID = invGiftVoucherMaster.InvGiftVoucherMasterID,
                        InvGiftVoucherBookCodeID = invGiftVoucherMaster.InvGiftVoucherBookCodeID,
                        InvGiftVoucherGroupID = invGiftVoucherMaster.InvGiftVoucherGroupID,
                        VoucherNo = invGiftVoucherMaster.VoucherNo,
                        VoucherSerial = invGiftVoucherMaster.VoucherSerial,
                        VoucherAmount = invGiftVoucherMaster.GiftVoucherValue,
                        GiftVoucherPercentage = invGiftVoucherMaster.GiftVoucherPercentage,
                        VoucherValue = (rdoVoucher.Checked ? invGiftVoucherMaster.GiftVoucherValue : invGiftVoucherMaster.GiftVoucherPercentage),
                        VoucherType = (rdoVoucher.Checked ? 1 : 2),
                        NumberOfCount = 1,
                        //DocumentStatus = documentStatus,
                    };
                    invGiftVoucherPurchaseOrderDetailsTempList.Add(tc);
                }

                //invGiftVoucherPurchaseOrderDetailsTemp.Clear();

                //invGiftVoucherPurchaseOrderDetailsTemp = invGiftVoucherPurchaseOrderDetailsTempList.ToList();
                invGiftVoucherPurchaseOrderDetailsTemp = invGiftVoucherPurchaseOrderDetailsTempList;
            }             

            UpdatedgvGiftVoucherDetails();
        }

        /// <summary>
        /// Load and bind gift vouchers depending on selection criteria
        /// </summary>
        private List<InvGiftVoucherMaster> LoadVoucherBookDetails()
        {
            InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
            InvGiftVoucherMasterService invGiftVoucherMasterService = new InvGiftVoucherMasterService();

            List<InvGiftVoucherMaster> invGiftVoucherMastersList = new List<InvGiftVoucherMaster>();

            if (cmbSelectionCriteria.SelectedIndex.Equals(0))
            {
                if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
                {
                    invGiftVoucherMastersList = invGiftVoucherMasterService.GetAllGiftVouchersByBookIDWithQty(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtGiftVoucherQty.Text.Trim()));
                }
            }
            else if (cmbSelectionCriteria.SelectedIndex.Equals(2))
            {
                if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
                {
                    //invGiftVoucherMastersList = invGiftVoucherMasterService.GetAllGiftVouchersByBookIDWithVoucherNoRange(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtVoucherNoFrom.Text.Trim()), Common.ConvertStringToInt(txtVoucherNoTo.Text.Trim()));
                    invGiftVoucherMastersList = invGiftVoucherMasterService.GetAllGiftVouchersByBookIDWithVoucherNoRange(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherNoFrom.Text.Trim(), txtVoucherNoTo.Text.Trim());
                }
            }
            else if (cmbSelectionCriteria.SelectedIndex.Equals(1))
            {
                if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
                {
                    //invGiftVoucherMastersList = invGiftVoucherMasterService.GetAllGiftVouchersByBookIDWithVoucherSerialRange(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtVoucherSerialFrom.Text.Trim()), Common.ConvertStringToInt(txtVoucherSerialTo.Text.Trim()));
                    invGiftVoucherMastersList = invGiftVoucherMasterService.GetAllGiftVouchersByBookIDWithVoucherSerialRange(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherSerialFrom.Text.Trim(), txtVoucherSerialTo.Text.Trim());
                }
            }

            return invGiftVoucherMastersList;
        }

        /// <summary>
        /// Update products grid view
        /// </summary>
        private void UpdatedgvGiftVoucherDetails()
        {
            dgvVoucherBookDetails.DataSource = null;
            
            invGiftVoucherPurchaseOrderDetailsTemp = invGiftVoucherPurchaseOrderDetailsTemp.OrderBy(pr => pr.LineNo).ToList();
            dgvVoucherBookDetails.DataSource = invGiftVoucherPurchaseOrderDetailsTemp.OrderBy(pr => pr.LineNo).ToList();

            if (dgvVoucherBookDetails.Rows.Count > 0)
            {
                Common.EnableComboBox(false, cmbLocation);
                //if (loadSupplierProducts)
                //{ Common.EnableTextBox(false, txtSupplierCode, txtSupplierName); }
                //else
                //{ Common.EnableTextBox(true, txtSupplierCode, txtSupplierName); }

                dgvVoucherBookDetails.FirstDisplayedScrollingRowIndex = dgvVoucherBookDetails.RowCount - 1;
            }
            else
            {
                Common.EnableComboBox(true, cmbLocation);
                Common.EnableTextBox(true, txtSupplierCode, txtSupplierName);
            }

            //IsCriteriaExistsByIndex(-1);

            EnableLine(false);
            Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtPurchaseOrderNo);
            Common.EnableComboBox(false, cmbLocation);
            Common.EnableButton(false, btnDocumentDetails);
            if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
            if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
            
            //groupBox4.Enabled = false;
            GetSummarizeFigures(invGiftVoucherPurchaseOrderDetailsTemp);
            if (invGiftVoucherPurchaseOrderDetailsTemp.Count > 0)
            { 
                grpFooter.Enabled = true;
                rdoVoucher.Enabled = false;
                rdoCoupon.Enabled = false;
            }
            ClearLine();

            //cmbSelectionCriteria.Enabled = true;
            //cmbSelectionCriteria.Focus();
        }

        private void EnableLine(bool enable)
        {
            //Common.EnableTextBox(enable, txtVoucherNo, txtVoucherSerial, txtVoucherValue, txtQty);
            Common.EnableComboBox(enable, cmbSelectionCriteria);

            //groupBox1.Enabled = enable;
            groupBox2.Enabled = enable;
            groupBox3.Enabled = enable;
            btnLoad.Enabled = enable;
        }

        private void ClearLine()
        {
            //Common.ClearTextBox(txtVoucherNo, txtVoucherSerial, txtVoucherValue, txtQty);
            Common.ClearTextBox(txtBookCode, txtBookName, txtGroupCode, txtGroupName, txtNoOfVouchersOnBook, txtGiftVoucherQty, txtVoucherNoFrom, txtVoucherNoTo, txtVoucherSerialFrom, txtVoucherSerialTo);
            Common.ClearComboBox(cmbSelectionCriteria);
        }

        private void GetSummarizeFigures(List<InvGiftVoucherPurchaseOrderDetail> listItem)
        {
            int qtyTotalCount = 0;
            
            qtyTotalCount = listItem.GetTotalCount();

            qtyTotalCount = Common.ConvertStringToInt(qtyTotalCount.ToString());

            txtTotalQty.Text = qtyTotalCount.ToString();

            CalculateFigures();
        }

        private void CalculateFigures()
        {
            decimal grossAmount = 0, otherChargersAmount = 0;
            decimal tax1 = 0;
            decimal tax2 = 0;
            decimal tax3 = 0;
            decimal tax4 = 0;
            decimal tax5 = 0;
            CommonService commonService = new CommonService();
            LgsSupplierService supplierService = new LgsSupplierService();

            grossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.Trim());
            otherChargersAmount = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.ToString());

            //Get Discount Amount
            bool isSubDiscount = chkSubTotalDiscountPercentage.Checked;
            decimal subDiscount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());
            decimal discountAmount = Common.ConvertStringToDecimalCurrency(Common.GetDiscountAmount(isSubDiscount, grossAmount, subDiscount).ToString());

            //Read from Tax
            decimal taxAmount = 0;
            if (chkTaxEnable.Checked)
            {
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(3, grossAmount, supplierService.GetLgsSupplierByCode(txtSupplierCode.Text.ToString()).LgsSupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
            }

            //Get Net Amount
            decimal netAmount = Common.ConvertStringToDecimalCurrency((Common.GetTotalAmount(grossAmount, otherChargersAmount, taxAmount) - Common.GetTotalAmount(discountAmount)).ToString());

            //Assign calculated values
            txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(grossAmount);
            txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(discountAmount);
            txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(otherChargersAmount);
            txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(taxAmount);
            txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(netAmount);
        }

        private void ResetVoucherType()
        {
            try
            {
                InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();

                groupBox4.Enabled = true;
                rdoVoucher.Enabled = true;
                rdoCoupon.Enabled = true;
                btnLoad.Enabled = false;
                groupBox1.Enabled = true;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;

                if (rdoVoucher.Checked)
                {
                    Common.EnableComboBox(false, cmbBasedOn);
                }
                else if (rdoCoupon.Checked)
                {
                    Common.EnableComboBox(false, cmbBasedOn);
                }
                else
                {
                    rdoVoucher.Checked = true;
                    Common.EnableComboBox(false, cmbBasedOn);
                }
                Common.ClearTextBox(txtBookCode, txtBookName, txtGroupCode, txtGroupName, txtVoucherValue, txtNoOfVouchersOnBook, txtPercentageOfCoupon);
                Common.EnableTextBox(true, txtBookCode, txtBookName, txtGroupCode, txtGroupName);
                Common.EnableComboBox(false, cmbSelectionCriteria);
                cmbBasedOn.SelectedIndex = -1;

                LoadBookByVoucherType();

                Common.SetAutoComplete(txtGroupCode, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupCodes(), chkAutoCompleationGroup.Checked);
                Common.SetAutoComplete(txtGroupName, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupNames(), chkAutoCompleationGroup.Checked);

                txtGroupCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadBookByVoucherType()
        {
            if (string.IsNullOrEmpty(txtGroupCode.Text.Trim()))
            {
                InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterGroupValueService = new InvGiftVoucherBookCodeGenerationService();
                Common.SetAutoComplete(txtBookCode, invGiftVoucherMasterGroupValueService.GetInvGiftVoucherBookGenerationCodesByVoucherType((rdoVoucher.Checked ? 1 : 2)), chkAutoCompleationBook.Checked);
                Common.SetAutoComplete(txtBookName, invGiftVoucherMasterGroupValueService.GetInvGiftVoucherBookGenerationNamesByVoucherType((rdoVoucher.Checked ? 1 : 2)), chkAutoCompleationBook.Checked);
            }
            else
            {
                InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterGroupValueService = new InvGiftVoucherBookCodeGenerationService();
                Common.SetAutoComplete(txtBookCode, invGiftVoucherMasterGroupValueService.GetInvGiftVoucherBookGenerationCodesByVoucherType((rdoVoucher.Checked ? 1 : 2),txtGroupCode.Text.Trim()), chkAutoCompleationBook.Checked);
                Common.SetAutoComplete(txtBookName, invGiftVoucherMasterGroupValueService.GetInvGiftVoucherBookGenerationNamesByVoucherType((rdoVoucher.Checked ? 1 : 2), txtGroupCode.Text.Trim()), chkAutoCompleationBook.Checked);
            }
        }

        private void LoadSelectionCriteria()
        {
            Common.SetAutoBindRecords(cmbSelectionCriteria, Common.GetGiftVoucherSelectionCriteria(cmbBasedOn.SelectedIndex));
            cmbSelectionCriteria.SelectedIndex = -1;
        }

        // Confirm Supplier by SupplierID
        private bool IsSupplierExistsByID(long supplierID)
        {
            bool recodFound = false;
            LgsSupplierService supplierService = new LgsSupplierService();
            LgsSupplier supplier = new LgsSupplier();
            supplier = supplierService.GetLgsSupplierByID(supplierID);
            if (supplier != null)
            {
                recodFound = true;
                txtSupplierCode.Text = supplier.SupplierCode;
                txtSupplierName.Text = supplier.SupplierName;
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtSupplierCode, txtSupplierName);
                Toast.Show(lblSupplier.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

        // Confirm Supplier by SupplierID
        private bool IsSupplierExistsByCode(string supplierCode)
        {
            bool recodFound = false;
            LgsSupplierService supplierService = new LgsSupplierService();
            LgsSupplier supplier = new LgsSupplier();
            supplier = supplierService.GetLgsSupplierByCode(supplierCode);
            if (supplier != null)
            {
                recodFound = true;
                txtSupplierCode.Text = supplier.SupplierCode;
                txtSupplierName.Text = supplier.SupplierName;
                txtValidityPeriod.Text = supplier.OrderCircle.ToString();

                creditLimit = supplier.ChequeLimit;
                creditPeriod = supplier.CreditPeriod;
                chequeLimit = supplier.ChequeLimit;
                chequePeriod = supplier.ChequePeriod;
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtSupplierCode, txtSupplierName);
                Toast.Show(lblSupplier.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

        // Confirm Supplier by SupplierName
        private bool IsSupplierExistsByName(string supplierName)
        {
            bool recodFound = false;
            LgsSupplierService supplierService = new LgsSupplierService();
            LgsSupplier supplier = new LgsSupplier();
            supplier = supplierService.GetLgsSupplierByName(supplierName);
            if (supplier != null)
            {
                recodFound = true;
                txtSupplierCode.Text = supplier.SupplierCode;
                txtSupplierName.Text = supplier.SupplierName;
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtSupplierCode, txtSupplierName);
                Toast.Show(lblSupplier.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

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

        private bool IsLocationExistsByID(int locationID)
        {
            bool recodFound = false;
            LocationService locationService = new LocationService();
            Location location = new Location();
            location = locationService.GetLocationsByID(locationID);
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

        // Confirm Payment methods by ID
        private bool IsPaymentMethodExistsByID(int paymentMethodID)
        {
            bool recodFound = false;
            PaymentMethodService paymentMethodService = new PaymentMethodService();

            if (paymentMethodService.GetPaymentMethodsByID(paymentMethodID) != null)
            { 
                recodFound = true;
                
            }
            else
            {
                recodFound = false;
                Toast.Show(lblPaymentTerms.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

        // Confirm Book by BookCode
        private bool IsBookCodeExistsByCode(string bookCode)
        {
            bool recodFound = false;
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
            InvGiftVoucherBookCode invGiftVoucherBookCode = new InvGiftVoucherBookCode();

            invGiftVoucherBookCode = invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(bookCode);
            if (invGiftVoucherBookCode != null)
            {
                recodFound = true;
                if (invGiftVoucherBookCode.PageCount != 0)
                {
                    cmbBasedOn.SelectedIndex = 0;
                }
                else
                {
                    cmbBasedOn.SelectedIndex = 1;
                }

                Common.EnableComboBox(true, cmbSelectionCriteria);
                Common.EnableButton(true, btnLoad);

                LoadSelectionCriteria();
                IsCriteriaExistsByIndex(cmbSelectionCriteria.SelectedIndex);

                txtBookCode.Text = invGiftVoucherBookCode.BookCode;
                txtBookName.Text = invGiftVoucherBookCode.BookName;

                txtGroupCode.Text = invGiftVoucherBookCode.InvGiftVoucherGroup.GiftVoucherGroupCode.Trim();
                txtGroupName.Text = invGiftVoucherBookCode.InvGiftVoucherGroup.GiftVoucherGroupName.Trim();

                txtGiftVoucherValue.Text = Common.ConvertDecimalToStringCurrency(invGiftVoucherBookCode.GiftVoucherValue);
                txtNoOfVouchersOnBook.Text = invGiftVoucherBookCode.PageCount.ToString();
                txtPercentageOfCoupon.Text = Common.ConvertDecimalToStringCurrency(invGiftVoucherBookCode.GiftVoucherPercentage);

                //Common.SetAutoComplete(txtVoucherSerialFrom, invGiftVoucherPurchaseOrderService.GetVoucherSerialByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                //Common.SetAutoComplete(txtVoucherSerialTo, invGiftVoucherPurchaseOrderService.GetVoucherSerialByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);

                //Common.SetAutoComplete(txtVoucherNoFrom, invGiftVoucherPurchaseOrderService.GetVoucherNoByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                //Common.SetAutoComplete(txtVoucherNoTo, invGiftVoucherPurchaseOrderService.GetVoucherNoByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtBookCode, txtBookName);
                Toast.Show(lblBook.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

        // Confirm Book by BookName
        private bool IsBookCodeExistsByName(string bookName)
        {
            bool recodFound = false;
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
            InvGiftVoucherBookCode invGiftVoucherBookCode = new InvGiftVoucherBookCode();

            invGiftVoucherBookCode = invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByName(bookName);
            if (invGiftVoucherBookCode != null)
            {
                recodFound = true;
                txtBookCode.Text = invGiftVoucherBookCode.BookCode;
                txtBookName.Text = invGiftVoucherBookCode.BookName;

                txtGroupCode.Text = invGiftVoucherBookCode.InvGiftVoucherGroup.GiftVoucherGroupCode.Trim();
                txtGroupName.Text = invGiftVoucherBookCode.InvGiftVoucherGroup.GiftVoucherGroupName.Trim();

                txtGiftVoucherValue.Text = Common.ConvertDecimalToStringCurrency(invGiftVoucherBookCode.GiftVoucherValue);
                txtNoOfVouchersOnBook.Text = invGiftVoucherBookCode.PageCount.ToString();

                Common.EnableComboBox(true, cmbSelectionCriteria);
                Common.EnableButton(true, btnLoad);
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtBookCode, txtBookName);
                Toast.Show(lblBook.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

        // Confirm Group by groupCode
        private bool IsGroupCodeExistsByCode(string groupCode)
        {
            bool recodFound = false;
            InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
            InvGiftVoucherGroup invGiftVoucherGroup = new InvGiftVoucherGroup();

            invGiftVoucherGroup = invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(groupCode);
            if (invGiftVoucherGroup != null)
            {
                recodFound = true;
                txtGroupCode.Text = invGiftVoucherGroup.GiftVoucherGroupCode;
                txtGroupName.Text = invGiftVoucherGroup.GiftVoucherGroupName;
                LoadBookByVoucherType();
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtGroupCode, txtGroupName);
                Toast.Show(lblGiftVoucherGroup.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

        // Confirm Group by groupName
        private bool IsGroupCodeExistsByName(string groupName)
        {
            bool recodFound = false;
            InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
            InvGiftVoucherGroup invGiftVoucherGroup = new InvGiftVoucherGroup();

            invGiftVoucherGroup = invGiftVoucherGroupService.GetInvGiftVoucherGroupByName(groupName);
            if (invGiftVoucherGroup != null)
            {
                recodFound = true;
                txtGroupCode.Text = invGiftVoucherGroup.GiftVoucherGroupCode;
                txtGroupName.Text = invGiftVoucherGroup.GiftVoucherGroupName;
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtGroupCode, txtGroupName);
                Toast.Show(lblGiftVoucherGroup.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }
        
        // Confirm Criteria by Index
        private bool IsCriteriaExistsByIndex(int criteriaIndex)
        {
            bool recodFound = false;

            //groupBox1.Enabled = true;
            //Common.EnableTextBox(true, txtBookCode, txtBookName, txtGroupCode, txtGroupName);
            Common.ClearTextBox(txtGiftVoucherQty, txtVoucherNoFrom, txtVoucherNoTo, txtVoucherSerialFrom, txtVoucherSerialTo, txtVoucherNoQty, txtVoucherSerialQty);
            if (criteriaIndex == -1)
            {
                //groupBox1.Enabled = false;
                //Common.EnableTextBox(false, txtBookCode, txtBookName, txtGroupCode, txtGroupName, txtGiftVoucherValue, txtNoOfVouchersOnBook);
                EnableSelectionCriteriaControls(false, 0, groupBox2);
                EnableSelectionCriteriaControls(false, 0, groupBox3);
                //btnLoad.Enabled = false;
            }
            else if (criteriaIndex == 0)
            {
                EnableSelectionCriteriaControls(true, 0, groupBox2);
                EnableSelectionCriteriaControls(false, 0, groupBox3);
                txtGiftVoucherQty.Focus();
            }
            else if (criteriaIndex == 2)
            {
                EnableSelectionCriteriaControls(true, 1, groupBox3);
                EnableSelectionCriteriaControls(false, 1, groupBox2);
                txtVoucherNoFrom.Focus();
            }
            else if (criteriaIndex == 1)
            {
                EnableSelectionCriteriaControls(true, 2, groupBox3);
                EnableSelectionCriteriaControls(false, 2, groupBox2);
                
                txtVoucherSerialFrom.Focus();
            }
            return recodFound;
        }

        /// <summary>
        /// Enable or disable selection Criteria controls
        /// </summary>
        /// <param name="enabled"></param>
        /// <param name="tabIndex"></param>
        /// <param name="groupBox"></param> 
        private void EnableSelectionCriteriaControls(bool enabled, int tabIndex, params GroupBox[] groupBox)
        {
            foreach (GroupBox b in groupBox)
            {
                b.Enabled = enabled;
                if (b == groupBox2)
                {
                    if (tabIndex == 0)
                    {
                        tbpVoucherNo.Enabled = false;
                        tbpVoucherSerial.Enabled = false;
                    }
                    Common.EnableTextBox(enabled, txtGiftVoucherQty);
                    txtGiftVoucherQty.Focus();
                }
                if (b == groupBox3)
                {
                    if (tabIndex == 1)
                    {
                        tbpVoucherNo.Enabled = true;
                        tbpVoucherSerial.Enabled = false;
                        tbpVoucherNo.Focus();
                        tbMore.SelectedTab = tbpVoucherNo;
                        Common.EnableTextBox(enabled, txtVoucherNoFrom, txtVoucherNoTo);
                        txtVoucherNoFrom.Focus();
                    }
                    else if (tabIndex == 2)
                    {
                        tbpVoucherNo.Enabled = false;
                        tbpVoucherSerial.Enabled = true;
                        tbpVoucherSerial.Focus();
                        tbMore.SelectedTab = tbpVoucherSerial;
                        Common.EnableTextBox(enabled, txtVoucherSerialFrom, txtVoucherSerialTo);
                        txtVoucherSerialFrom.Focus();
                    }
                }
                if (!enabled)
                {
                    Common.ClearTextBox(txtGiftVoucherQty, txtVoucherNoFrom, txtVoucherNoTo, txtVoucherSerialFrom, txtVoucherSerialTo);
                }
            }            
        }

        /// <summary>
        ///  Save data into DB
        /// </summary>
        /// <param name="documentStatus"></param>
        /// <param name="documentNo"></param>
        /// <returns></returns>
        private bool SaveDocument(int documentStatus, string documentNo, out string newDocumentNo)
        {
            try
            {
                GetSummarizeFigures(invGiftVoucherPurchaseOrderDetailsTemp);

                InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader = new InvGiftVoucherPurchaseOrderHeader();
                LocationService locationService = new LocationService();
                Location location = new Location();
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                LgsSupplier lgsSupplier = new LgsSupplier();

                location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                lgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());

                invGiftVoucherPurchaseOrderHeader = invGiftVoucherPurchaseOrderService.GetInvGiftVoucherPurchaseOrderHeaderByDocumentNo(documentID, txtPurchaseOrderNo.Text.Trim(), location.LocationID);
                if (invGiftVoucherPurchaseOrderHeader == null)
                {invGiftVoucherPurchaseOrderHeader = new InvGiftVoucherPurchaseOrderHeader();}
                ////if (documentStatus.Equals(1)) // update paused document
                ////{
                ////    documentNo = GetDocumentNo(false);
                ////    txtPurchaseOrderNo.Text = documentNo;
                ////}

                invGiftVoucherPurchaseOrderHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                invGiftVoucherPurchaseOrderHeader.CompanyID = location.CompanyID;
                invGiftVoucherPurchaseOrderHeader.LocationID = location.LocationID;
                invGiftVoucherPurchaseOrderHeader.DocumentID = documentID;
                invGiftVoucherPurchaseOrderHeader.DocumentNo = documentNo.Trim();
                invGiftVoucherPurchaseOrderHeader.DocumentDate = dtpDocumentDate.Value;
                invGiftVoucherPurchaseOrderHeader.SupplierID = lgsSupplier.LgsSupplierID;
                invGiftVoucherPurchaseOrderHeader.ExpectedDate = dtpExpectedDate.Value;
                invGiftVoucherPurchaseOrderHeader.ExpiryDate = dtpExpectedDate.Value.AddDays(string.IsNullOrEmpty(txtValidityPeriod.Text.Trim()) ? 0 : int.Parse(txtValidityPeriod.Text));
                invGiftVoucherPurchaseOrderHeader.PaymentMethodID = int.Parse(cmbPaymentTerms.SelectedValue.ToString().Trim());
                invGiftVoucherPurchaseOrderHeader.GiftVoucherAmount = Common.ConvertStringToDecimalCurrency(txtGiftVoucherValue.Text.Trim());
                invGiftVoucherPurchaseOrderHeader.GiftVoucherPercentage = Common.ConvertStringToDecimalCurrency(txtPercentageOfCoupon.Text.Trim());
                invGiftVoucherPurchaseOrderHeader.GrossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.ToString());
                invGiftVoucherPurchaseOrderHeader.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscount.Text.ToString());
                if (chkSubTotalDiscountPercentage.Checked)
                { invGiftVoucherPurchaseOrderHeader.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString()); }
                invGiftVoucherPurchaseOrderHeader.OtherCharges = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.ToString());
                invGiftVoucherPurchaseOrderHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtNetAmount.Text.ToString());
                invGiftVoucherPurchaseOrderHeader.GiftVoucherQty = Common.ConvertStringToInt(txtTotalQty.Text.ToString());
                invGiftVoucherPurchaseOrderHeader.VoucherType = (rdoVoucher.Checked ? 1 : 2);

                invGiftVoucherPurchaseOrderHeader.CreditLimit = creditLimit;
                invGiftVoucherPurchaseOrderHeader.CreditPeriod = creditPeriod;
                invGiftVoucherPurchaseOrderHeader.ChequeLimit = chequeLimit;
                invGiftVoucherPurchaseOrderHeader.ChequePeriod = chequePeriod;

                if (chkTaxEnable.Checked)
                {
                    decimal tax1 = 0;
                    decimal tax2 = 0;
                    decimal tax3 = 0;
                    decimal tax4 = 0;
                    decimal tax5 = 0;

                    CommonService commonService = new CommonService();
                    invGiftVoucherPurchaseOrderHeader.TaxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(3, invGiftVoucherPurchaseOrderHeader.GrossAmount, lgsSupplier.LgsSupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());

                    invGiftVoucherPurchaseOrderHeader.TaxAmount1 = tax1;
                    invGiftVoucherPurchaseOrderHeader.TaxAmount2 = tax2;
                    invGiftVoucherPurchaseOrderHeader.TaxAmount3 = tax3;
                    invGiftVoucherPurchaseOrderHeader.TaxAmount4 = tax4;
                    invGiftVoucherPurchaseOrderHeader.TaxAmount5 = tax5;
                }

                //invPurchaseOrderHeader.RequestedBy = Common.LoggedUser;
                ////invPurchaseOrderHeader.ReferenceDocumentID = 1;
                ////invPurchaseOrderHeader.ReferenceDocumentNo = "REF001";
                invGiftVoucherPurchaseOrderHeader.ReferenceNo = txtReferenceNo.Text.Trim();
                invGiftVoucherPurchaseOrderHeader.Remark = txtRemark.Text.Trim();
                invGiftVoucherPurchaseOrderHeader.DocumentStatus = documentStatus;
                invGiftVoucherPurchaseOrderHeader.CreatedUser = Common.LoggedUser;
                invGiftVoucherPurchaseOrderHeader.CreatedDate = Common.GetSystemDateWithTime();
                invGiftVoucherPurchaseOrderHeader.ModifiedUser = Common.LoggedUser;
                invGiftVoucherPurchaseOrderHeader.ModifiedDate = Common.GetSystemDateWithTime();
                //{"The transaction associated with the current connection has completed but has not been disposed.  The transaction must be disposed before the connection can be used to execute SQL statements."}

                return invGiftVoucherPurchaseOrderService.SaveGiftVoucherPurchaseOrder(invGiftVoucherPurchaseOrderHeader, invGiftVoucherPurchaseOrderDetailsTemp, out newDocumentNo, this.Name);
            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                newDocumentNo = string.Empty;
                return false;
            }
        }
        
        private string GetDocumentNo(bool isTemporytNo)
        {
            try
            {
                InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                LocationService locationService = new LocationService();
                return invGiftVoucherPurchaseOrderService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }
        }

        private long LoadSelectedVoucherQty()
        {
            InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
            long qty = 0;
            if (cmbSelectionCriteria.SelectedIndex == 1)
            {
                qty = invGiftVoucherPurchaseOrderService.GetVoucherQtyByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherSerialFrom.Text, txtVoucherSerialTo.Text, cmbBasedOn.SelectedIndex);
            }
            else if (cmbSelectionCriteria.SelectedIndex == 2)
            {
                qty = invGiftVoucherPurchaseOrderService.GetVoucherQtyByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherNoFrom.Text, txtVoucherNoTo.Text, cmbBasedOn.SelectedIndex);
            }
            return qty;
        }

        /// <summary>
        /// Load Reference Search form on demand
        /// </summary>
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

        private bool ValidateControls()
        {
            bool isValid = true;

            if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtSupplierCode))
            { return false; }
            else if (!Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbLocation))
            { return false; }
            else if (cmbSelectionCriteria.SelectedIndex == 0)
            {
                isValid = Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtBookCode, txtGroupCode, txtGiftVoucherValue, txtGiftVoucherQty);
            }
            else if (cmbSelectionCriteria.SelectedIndex == 2)
            {
                isValid = Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtBookCode, txtGroupCode, txtGiftVoucherValue, txtVoucherNoFrom, txtVoucherNoTo);
            }
            else if (cmbSelectionCriteria.SelectedIndex == 1)
            {
                isValid = Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtBookCode, txtGroupCode, txtGiftVoucherValue, txtVoucherSerialFrom, txtVoucherSerialTo);
            }
            return isValid;
        }

        #region Validate Logics

        /// <summary>
        /// Validate Parent property of the Child
        /// </summary>
        /// <returns></returns>
        private bool IsValidateControls()
        {
            InvGiftVoucherPurchaseOrderValidator invGiftVoucherPurchaseOrderValidator = new InvGiftVoucherPurchaseOrderValidator();
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
            
            bool isValidated = false;
            if (cmbSelectionCriteria.SelectedIndex == 0 && !invGiftVoucherPurchaseOrderValidator.ValidateVoucherQty(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtGiftVoucherQty.Text.Trim()), cmbBasedOn.SelectedIndex))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(lblGiftVoucherQty.Text.ToString()), " - ", txtGiftVoucherQty.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else if (cmbSelectionCriteria.SelectedIndex == 2 && cmbBasedOn.SelectedIndex == 0 && !invGiftVoucherPurchaseOrderValidator.ValidateVoucherNoFromTo(txtVoucherNoFrom.Text.Trim(), txtVoucherNoTo.Text.Trim()))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherNo.Text.ToString()), " - ", txtVoucherNoFrom.Text.Trim(), " - ", txtVoucherNoTo.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else if (cmbSelectionCriteria.SelectedIndex == 2 && cmbBasedOn.SelectedIndex == 0 && !invGiftVoucherPurchaseOrderValidator.ValidateVoucherNoRange(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherNoFrom.Text.Trim(), txtVoucherNoTo.Text.Trim()))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherNo.Text.ToString()), " - ", txtVoucherNoFrom.Text.Trim(), " - ", txtVoucherNoTo.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else if (cmbSelectionCriteria.SelectedIndex == 1 && !invGiftVoucherPurchaseOrderValidator.ValidateVoucherSerialFromTo(txtVoucherSerialFrom.Text.Trim(), txtVoucherSerialTo.Text.Trim()))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherSerial.Text.ToString()), " - ", txtVoucherSerialFrom.Text.Trim(), " - ", txtVoucherSerialTo.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);

                

                isValidated = false;
            }
            else if (invGiftVoucherPurchaseOrderDetailsTemp != null && invGiftVoucherPurchaseOrderDetailsTemp.Count > 0 && !invGiftVoucherPurchaseOrderValidator.ValidateVoucherSerialPageType(invGiftVoucherPurchaseOrderDetailsTemp, txtPurchaseOrderNo.Text.Trim(), (rdoVoucher.Checked ? 1 : 2), invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(lblBasedOn.Text.ToString()), " - ", cmbBasedOn.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else if (cmbSelectionCriteria.SelectedIndex == 1 && !invGiftVoucherPurchaseOrderValidator.ValidateVoucherSerialRange(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherSerialFrom.Text.Trim(), txtVoucherSerialTo.Text.Trim()))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherSerial.Text.ToString()), " - ", txtVoucherSerialFrom.Text.Trim(), " - ", txtVoucherSerialTo.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else
            { isValidated = true; }

            return isValidated;
        }

        private bool IsValidateExistsRecords()
        {
            InvGiftVoucherPurchaseOrderValidator invGiftVoucherPurchaseOrderValidator = new InvGiftVoucherPurchaseOrderValidator();

            bool isValidated = false;

            if (!invGiftVoucherPurchaseOrderValidator.ValidateExistsPausedVoucherSerial(invGiftVoucherPurchaseOrderDetailsTemp, txtPurchaseOrderNo.Text.Trim(), (rdoVoucher.Checked ? 1 : 2)))
            {
                Toast.Show("Voucher Serial ", Toast.messageType.Information, Toast.messageAction.ExistsForSelected, " - paused document.");
                isValidated = false;
            }
            //else if (!invGiftVoucherPurchaseValidator.ValidateExistsSavedVoucherSerial(invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text.Trim()).InvGiftVoucherGroupID, Common.ConvertStringToDecimalCurrency(txtVoucherValue.Text.Trim()), Common.ConvertStringToInt(txtStartingNo.Text.Trim()), Common.ConvertStringToInt(txtNoOfVouchers.Text.Trim())))
            //{
            //    //Toast.Show(Common.ConvertStringToDisplayFormat(lblSerialFormat.Text.Trim()) + " ", Toast.messageType.Information, Toast.messageAction.ExistsForSelected, "selected - " + Common.ConvertStringToDisplayFormat(lblGiftVoucherGroup.Text.Trim()) + " - " + txtGroupCode.Text.Trim() + ", " + Common.ConvertStringToDisplayFormat(lblVoucherValue.Text.Trim()) + " - " + txtVoucherValue.Text.Trim());
            //    isValidated = false;
            //}
            else
            { isValidated = true; }

            return isValidated;
        }
        #endregion

        private void txtVoucherSerialTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();

                    DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseOrderService.GetAllActiveGiftVoucherSerialsDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2), invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text).InvGiftVoucherGroupID, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text).InvGiftVoucherBookCodeID));
                    if (dvAllReferenceData.Count > 0)
                    { 
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), this.ActiveControl);
                        txtVoucherSerialTo_Leave(this, e);
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        

       
        #endregion

        private void txtVoucherNoTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();

                    DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseOrderService.GetAllActiveGiftVoucherNosDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2), invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text).InvGiftVoucherGroupID, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text).InvGiftVoucherBookCodeID));
                    if (dvAllReferenceData.Count > 0)
                    { LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), this.ActiveControl); }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtGiftVoucherQty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtGiftVoucherQty.Text.Trim()))
                {
                    noOfVouchers = Common.ConvertStringToInt(txtGiftVoucherQty.Text);
                }
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
                InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseOrderService.GetPendingPurchaseOrderDocuments());
                if (dvAllReferenceData.Count != 0)
                {
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), "Pending Documents", string.Empty, txtPurchaseOrderNo);
                    if (!txtPurchaseOrderNo.Text.Trim().Equals(string.Empty))
                    { LoadPurchaseOrderDocument(); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        

        
    }
}
