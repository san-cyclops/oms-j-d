using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Domain;
using Report.GV;
using Service;
using UI.Windows.CustomControls;
using Utility;

namespace UI.Windows
{
    public partial class FrmGiftVoucherGoodsReceivedNote : UI.Windows.FrmBaseTransactionForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<InvGiftVoucherPurchaseDetailTemp> invGiftVoucherPurchaseDetailsTemp = new List<InvGiftVoucherPurchaseDetailTemp>();
        private ReferenceType existingReferenceType;
        
        int documentID = 3;
        bool poIsMandatory;
        private decimal creditLimit;
        private int creditPeriod;
        private decimal chequeLimit;
        private int chequePeriod;
        private int noOfVouchers;
        bool recallPO;
        
        public FrmGiftVoucherGoodsReceivedNote()
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
                //UpdatedgvGiftVoucherDetails();

                //Toast.Show(this.Text + " - " + txtGroupCode.Text.Trim() + " - " + txtVoucherValue.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.Generate);
                //Common.EnableButton(true, btnSave);
                //Common.EnableTextBox(false, txtBookCode, txtBookName, txtGroupCode, txtGroupName, txtVoucherValue, txtNoOfVouchersOnBook, txtNoOfVouchers, txtPrefix, txtLength, txtStartingNo);
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
                InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseService.GetPendingPurchaseDocuments());
                if (dvAllReferenceData.Count != 0)
                {
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), "Pending Documents", string.Empty, txtDocumentNo);
                    if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                    { LoadGoodsReceivedDocument(); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                else
                {
                    if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                    {
                        LoadGoodsReceivedDocument(); 
                    }
                    else
                    {
                        txtDocumentNo.Text = GetDocumentNo(true);
                        txtSupplierCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void btnPoDetails_Click(object sender, EventArgs e)
        {
            try
            {
                InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseOrderService.GetPendingPurchaseOrderDocumentsToGRN());
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

        private void txtSupplierCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                    DataView dvAllReferenceData = new DataView(lgsSupplierService.GetAllActiveLgsSuppliersDataTableBySupplierType(lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.LogisticSupplierType).ToString(), 1).LookupKey));
                    if (dvAllReferenceData.Count > 0)
                    { 
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtSupplierCode);
                        txtSupplierCode_Validated(this, e);
                    }
                }

                Common.SetFocus(e, txtDeliveryPerson);
            }
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

        private void txtSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                    DataView dvAllReferenceData = new DataView(lgsSupplierService.GetAllActiveLgsSuppliersDataTableBySupplierType(lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.LogisticSupplierType).ToString(), 1).LookupKey));
                    if (dvAllReferenceData.Count > 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtSupplierCode);
                        txtSupplierCode_Validated(this, e);
                    }
                }

                Common.SetFocus(e, txtSupplierCode);
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

        private void txtDeliveryPerson_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtNic); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtNic_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtVehicle); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtVehicle_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtRemark); }
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

        private void txtReferenceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, dtpDocumentDate); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dtpDocumentDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtPartyInvoiceNo); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtPartyInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, dtpPartyInvoiceDate); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dtpPartyInvoiceDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtDispatchNo); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtDispatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, dtpDispatchDate); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dtpDispatchDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, cmbLocation); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, cmbPaymentTerms); }
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
                        //LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                        //LgsSupplierService supplierService = new LgsSupplierService();
                        //LgsSupplier supplier = new LgsSupplier();
                        //supplier = supplierService.GetLgsSupplierByCode(txtSupplierCode.Text);
                        //if (supplier != null)
                        //{
                        //    FrmPaymentMethodLimit frmPaymentMethodLimit = new FrmPaymentMethodLimit(!poIsMandatory, cmbPaymentTerms.Text.Trim(), creditLimit, creditPeriod, chequeLimit, chequePeriod, FrmPaymentMethodLimit.transactionType.GiftVoucherPurchase);
                        //    frmPaymentMethodLimit.ShowDialog();
                        //}
                        //else
                        //{
                        //    Common.ClearTextBox(txtSupplierCode, txtSupplierName);
                        //    Toast.Show(lblSupplier.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        //}
                        cmbPaymentTerms_Validated(this, e);
                    }
                    
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbSelectionCriteria_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(cmbSelectionCriteria.Text.Trim()))
                //{ Common.SetFocus(e, txtBookCode); }
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
                    if (!poIsMandatory)
                    {
                        InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterBookService = new InvGiftVoucherBookCodeGenerationService();
                        DataView dvAllReferenceData = new DataView(invGiftVoucherMasterBookService.GetAllActiveGiftVoucherMasterBooksDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2)));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(),this.ActiveControl.Text.Trim(), txtBookCode);
                            txtBookCode_Validated(this, e);
                        }
                    }
                    else
                    {
                        InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                        InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                        InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader = invGiftVoucherPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());
                        DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseService.GetAllActiveGiftVoucherMasterRecallPOBooksDataTable(invGiftVoucherPurchaseOrderHeader));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtBookCode);
                            txtBookCode_Validated(this, e);
                        }
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
                    if (!poIsMandatory)
                    {
                        InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterBookService = new InvGiftVoucherBookCodeGenerationService();
                        DataView dvAllReferenceData = new DataView(invGiftVoucherMasterBookService.GetAllActiveGiftVoucherMasterBooksDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2)));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtBookCode);
                            txtBookName_Validated(this, e);
                        }
                    }
                    else
                    {
                        InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                        InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                        InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader = invGiftVoucherPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());
                        DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseService.GetAllActiveGiftVoucherMasterRecallPOBooksDataTable(invGiftVoucherPurchaseOrderHeader));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtBookCode);
                            txtBookName_Validated(this, e);
                        }
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
                    if (!poIsMandatory)
                    {
                        InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                        DataView dvAllReferenceData = new DataView(invGiftVoucherGroupService.GetAllActiveGiftVoucherGroupsDataTable());
                        if (dvAllReferenceData.Count > 0)
                        { 
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtGroupCode);
                            txtGroupCode_Validated(this, e);
                        }
                    }
                    else
                    {
                        InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                        InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                        InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader = invGiftVoucherPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());
                        DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseService.GetAllActiveGiftVoucherRecallPOGroupsDataTable(invGiftVoucherPurchaseOrderHeader));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtGroupCode);
                            txtGroupCode_Validated(this, e);
                        }
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
                    if (!poIsMandatory)
                    {
                        InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                        DataView dvAllReferenceData = new DataView(invGiftVoucherGroupService.GetAllActiveGiftVoucherGroupsDataTable());
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtGroupCode);
                            txtGroupCode_Validated(this, e);
                        }
                    }
                    else
                    {
                        InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                        InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                        InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader = invGiftVoucherPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());
                        DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseService.GetAllActiveGiftVoucherRecallPOGroupsDataTable(invGiftVoucherPurchaseOrderHeader));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtGroupCode);
                            txtGroupCode_Validated(this, e);
                        }
                    }
                }
                Common.SetFocus(e, txtGroupCode); 
            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
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
                    InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();

                    if (!poIsMandatory)
                    {
                        DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseService.GetAllActiveGiftVoucherNosDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2), invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text).InvGiftVoucherGroupID, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text).InvGiftVoucherBookCodeID));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtVoucherNoFrom);
                            txtVoucherNoFrom_Leave(this, e);
                        }
                    }
                    else
                    {
                        InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                        InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader = invGiftVoucherPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());
                        DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseService.GetAllActiveGiftVoucherMasterRecallPOVoucherNosDataTable(invGiftVoucherPurchaseOrderHeader));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtVoucherNoFrom);
                            txtVoucherNoFrom_Leave(this, e);
                        }
                    }
                }
                Common.SetFocus(e, txtVoucherNoTo);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtVoucherNoTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();

                    if(!poIsMandatory)
                    {
                        DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseService.GetAllActiveGiftVoucherNosDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2), invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text).InvGiftVoucherGroupID, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text).InvGiftVoucherBookCodeID));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtVoucherNoTo);
                            txtVoucherNoTo_Leave(this, e);
                        }
                    }
                    else
                    {
                        InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                        InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader = invGiftVoucherPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());
                        DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseService.GetAllActiveGiftVoucherMasterRecallPOVoucherNosDataTable(invGiftVoucherPurchaseOrderHeader));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtVoucherNoTo);
                            txtVoucherNoTo_Leave(this, e);
                        }
                    }
                }
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
                    InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();

                    if (!poIsMandatory)
                    {
                        DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseService.GetAllActiveGiftVoucherSerialsDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2), invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text).InvGiftVoucherGroupID, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text).InvGiftVoucherBookCodeID));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtVoucherSerialFrom);
                            txtVoucherSerialFrom_Leave(this, e);
                        }
                    }
                    else
                    {
                        InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                        InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader = invGiftVoucherPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());
                        DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseService.GetAllActiveGiftVoucherMasterRecallPOVoucherSerialsDataTable(invGiftVoucherPurchaseOrderHeader));
                        if (dvAllReferenceData.Count > 0)
                        { 
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtVoucherSerialFrom);
                            txtVoucherSerialFrom_Leave(this, e);
                        }
                    }
                }
                Common.SetFocus(e, txtVoucherSerialTo);
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

        private void txtGrossAmount_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                CalculateFigures();

                txtGrossAmount.Text = txtGrossAmount.Text.ToString();
            }
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
                    if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return;  }

                    Common.EnableComboBox(true, cmbPaymentTerms);
                    cmbPaymentTerms.Focus();
                }
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
                    //ResetVoucherType();
                    btnPaymentTermLimits.Focus();
                    //Common.EnableComboBox(true, cmbSelectionCriteria);
                    //cmbSelectionCriteria.Focus();
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
                    //LoadSelectionCriteria();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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

        private void txtOtherCharges_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {   
                CalculateFigures();
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

        private void chkAutoCompleationBook_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadBookByVoucherType();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoCompleationGroup_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadGroups();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtPurchaseOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtPurchaseOrderNo.Text.Trim().Equals(string.Empty))
                    {
                        LoadPurchaseOrderDocument();
                        invGiftVoucherPurchaseDetailsTemp.Count();
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
                { cmbSelectionCriteria.Focus(); }
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
                if (string.IsNullOrEmpty(txtGrossAmount.Text.Trim()))
                {
                    return;
                }
                else
                {
                    CalculateFigures();
                    txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text));
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtSubTotalDiscountPercentage_Validated(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtSubTotalDiscountPercentage.Text.Trim()))
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

        private void chkAutoCompleationDocumentNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                Common.SetAutoComplete(txtDocumentNo, invGiftVoucherPurchaseService.GetPausedDocumentNumbers(), chkAutoCompleationPoNo.Checked);
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

        private void chkAutoCompleationPoNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //Load purchase order document numbers
                InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                Common.SetAutoComplete(txtPurchaseOrderNo, invGiftVoucherPurchaseOrderService.GetAllDocumentNumbersToGRN(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationPoNo.Checked);
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
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtVoucherSerialTo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtVoucherSerialTo.Text.Trim()))
                {
                    //InvGiftVoucherMaster invGiftVoucherMaster = new InvGiftVoucherMaster();
                    //InvGiftVoucherMasterService invGiftVoucherMasterService = new InvGiftVoucherMasterService();

                    LoadVoucherSerial(lblVoucherSerialFrom.Text, txtVoucherSerialFrom.Text);

                    //invGiftVoucherMaster = invGiftVoucherMasterService.GetInvGiftVoucherMasterToGRNByVoucherSerial(txtVoucherSerialTo.Text);
                    //if (invGiftVoucherMaster == null)
                    //{
                    //    Toast.Show(tbpVoucherSerial.Text + " " + lblVoucherNoTo + " - " + txtVoucherNoTo.Text, Toast.messageType.Information, Toast.messageAction.Invalid);
                    //    return;
                    //}
                    //else
                    //{
                    //txtVoucherSerialQty.Text = Common.ConvertDecimalToStringQty(LoadSelectedVoucherQty());
                    //}
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
                    LoadVoucherNo(lblVoucherNoTo.Text, txtVoucherNoTo.Text);
                    //InvGiftVoucherMaster invGiftVoucherMaster = new InvGiftVoucherMaster();
                    //InvGiftVoucherMasterService invGiftVoucherMasterService = new InvGiftVoucherMasterService();

                    //invGiftVoucherMaster = invGiftVoucherMasterService.GetInvGiftVoucherMasterToGRNByVoucherNo(txtVoucherNoTo.Text);
                    //if (invGiftVoucherMaster == null)
                    //{
                    //    Toast.Show(tbpVoucherNo.Text + " " + lblVoucherNoTo + " - " + txtVoucherNoTo.Text, Toast.messageType.Information, Toast.messageAction.Invalid);
                    //    return;
                    //}
                    //else
                    //{
                    //    txtVoucherNoQty.Text = Common.ConvertDecimalToStringQty(LoadSelectedVoucherQty());
                    //}
                }
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
                    LoadVoucherSerial(lblVoucherSerialFrom.Text, txtVoucherSerialFrom.Text);
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
                    LoadVoucherNo(lblVoucherNoFrom.Text, txtVoucherNoFrom.Text);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGiftVoucherQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    btnLoad.Focus();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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

        private void txtGrossAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtGrossAmount.Text.Trim()))
                {
                    return;
                }
                else
                {

                    txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text));
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtOtherCharges_Leave(object sender, EventArgs e)
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

        private void txtVoucherSerialTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();

                    if(!poIsMandatory)
                    {
                        DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseService.GetAllActiveGiftVoucherSerialsDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2), invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text).InvGiftVoucherGroupID, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text).InvGiftVoucherBookCodeID));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtVoucherSerialTo);
                            txtVoucherSerialTo_Leave(this, e);
                        }
                    }
                    else
                    {
                        InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                        InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader = invGiftVoucherPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());
                        DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseService.GetAllActiveGiftVoucherMasterRecallPOVoucherSerialsDataTable(invGiftVoucherPurchaseOrderHeader));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtVoucherSerialTo);
                            txtVoucherSerialTo_Leave(this, e);
                        }
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
            // Load Payment methods
            PaymentMethodService paymentMethodService = new PaymentMethodService();
            Common.LoadPaymentMethods(cmbPaymentTerms, paymentMethodService.GetAllPaymentMethods());

            // Load Locations            
            LocationService locationService = new LocationService();
            //Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
            Common.LoadLocations(cmbLocation, locationService.GetHeadOfficeLocation());
            cmbLocation.SelectedValue = Common.LoggedLocationID;

            Common.SetDataGridviewColumnsReadOnly(true, dgvVoucherBookDetails, dgvVoucherBookDetails.Columns[1], dgvVoucherBookDetails.Columns[2], dgvVoucherBookDetails.Columns[3]);
            Common.ReadOnlyTextBox(true, txtGiftVoucherValue, txtNoOfVouchersOnBook, txtPercentageOfCoupon, txtSubTotalDiscount, txtTotalTaxAmount, txtNetAmount, txtVoucherSerialQty, txtVoucherNoQty);

            Common.EnableTextBox(true, txtSupplierCode, txtSupplierName, txtDocumentNo, txtPurchaseOrderNo);
            Common.EnableButton(true, btnDocumentDetails, btnPoDetails);
            Common.EnableComboBox(true, cmbLocation);
            Common.EnableComboBox(false, cmbSelectionCriteria);
            Common.EnableButton(false, btnSave, btnPause);
            //if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
            //if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

            // Disable gift voucher selection details controls
            cmbSelectionCriteria.SelectedIndex = -1;
            cmbBasedOn.SelectedIndex = -1;
            //IsCriteriaExistsByIndex(-1);

            dtpDocumentDate.Value = Common.GetSystemDate();
            dtpPartyInvoiceDate.Value = Common.GetSystemDate();
            dtpDispatchDate.Value = Common.GetSystemDate();

            txtDocumentNo.Text = GetDocumentNo(true);

            invGiftVoucherPurchaseDetailsTemp = null;

            existingReferenceType = null;
            recallPO = false;

            grpHeader.Enabled = true;
            grpFooter.Enabled = false;
            groupBox4.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;

            if (poIsMandatory)
            {
                EnableControl(false);
                this.ActiveControl = txtPurchaseOrderNo;
                txtPurchaseOrderNo.Focus();
            }
            else
            {
                EnableControl(true);
                this.ActiveControl = txtSupplierCode;
                txtSupplierCode.Focus();
            }
        }

        private void EnableControl(bool status)
        {
            Common.EnableTextBox(status, txtSupplierCode, txtSupplierName);
            Common.EnableComboBox(status, cmbLocation, cmbPaymentTerms);
            Common.EnableButton(status, btnPaymentTermLimits);
            //dtpDocumentDate.Enabled = status;
            grpBody.Enabled = status;
            grpFooter.Enabled = status;
        }

        public void SetPaymentTermLimit(decimal creditLimit, int creditPeriod, decimal chequeLimit, int chequePeriod)
        {
            creditLimit = creditLimit;
            creditPeriod = creditPeriod;
            chequeLimit = chequeLimit;
            chequePeriod = chequePeriod;
        }

        public override void FormLoad()
        {
            try
            {
                dgvVoucherBookDetails.AutoGenerateColumns = false;
               
                // Load Payment methods
                PaymentMethodService paymentMethodService = new PaymentMethodService();
                Common.LoadPaymentMethods(cmbPaymentTerms, paymentMethodService.GetAllPaymentMethods());

                // Load Locations
                LocationService locationService = new LocationService();
                //Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
                Common.LoadLocations(cmbLocation, locationService.GetHeadOfficeLocation());

                // Read Non Trade Logistic Suppliers
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                Common.SetAutoComplete(txtSupplierCode, lgsSupplierService.GetAllLgsSupplierCodesBySupplierType(lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.LogisticSupplierType).ToString(), 1).LookupKey), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, lgsSupplierService.GetAllLgsSupplierNamesBySupplierType(lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.LogisticSupplierType).ToString(), 1).LookupKey), chkAutoCompleationSupplier.Checked);

                LoadBookByVoucherType();

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }

                poIsMandatory = autoGenerateInfo.PoIsMandatory;

                base.FormLoad();

                RefreshDocumentNumbers();

                if (poIsMandatory)
                {
                    EnableControl(false);
                    this.ActiveControl = txtPurchaseOrderNo;
                    txtPurchaseOrderNo.Focus();
                }
                else
                {
                    EnableControl(true);
                    this.ActiveControl = txtSupplierCode;
                    txtSupplierCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void RefreshDocumentNumbers()
        {
            //Load purchase order document numbers
            InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
            Common.SetAutoComplete(txtPurchaseOrderNo, invGiftVoucherPurchaseOrderService.GetAllDocumentNumbersToGRN(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationPoNo.Checked);

            //Load GRN Document Numbers
            InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
            Common.SetAutoComplete(txtDocumentNo, invGiftVoucherPurchaseService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
        }

        /// <summary>
        /// Pause Gift Voucher Purchase order.
        /// </summary>
        public override void Pause()
        {
            if (!IsValidateExistsRecords()) { return; }
            if ((Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                string NewDocumentNo;
                bool saveDocument = SaveDocument(0, txtDocumentNo.Text.Trim(), out NewDocumentNo);
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
            if (!IsValidateExistsRecords()) { return; }

            if ((Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                if (!IsSupplierExistsByCode(txtSupplierCode.Text.Trim())) { return; }
                if (!IsPaymentMethodExistsByID(int.Parse(cmbPaymentTerms.SelectedValue == null ? "-1" : cmbPaymentTerms.SelectedValue.ToString().Trim()))) { return; }
                if (!IsLocationExistsByName(cmbLocation.Text.Trim())) { return; }

                string NewDocumentNo;
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim(), out NewDocumentNo);
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

        private void GenerateReport(string documentNo, int documentStatus)
        {
            GvReportGenerator gvReportGenerator = new GvReportGenerator();
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            gvReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
        }

        public override void ClearForm()
        {
            dgvVoucherBookDetails.DataSource = null;
            dgvVoucherBookDetails.Refresh();
            
            base.ClearForm();
            txtSubTotalDiscount.Clear();
        }

        private void LoadSelectionCriteria()
        {
            Common.SetAutoBindRecords(cmbSelectionCriteria, Common.GetGiftVoucherSelectionCriteria(cmbBasedOn.SelectedIndex));
            cmbSelectionCriteria.SelectedIndex = -1;
        }

        /// <summary>
        /// Load paused document
        /// </summary>
        private void LoadGoodsReceivedDocument()
        {
            InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
            InvGiftVoucherPurchaseHeader existingInvGiftVoucherPurchaseHeader = new InvGiftVoucherPurchaseHeader();

            existingInvGiftVoucherPurchaseHeader = invGiftVoucherPurchaseService.GetGiftVoucherPurchaseHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

            if (existingInvGiftVoucherPurchaseHeader == null)
            {
                Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            else
            {
                txtDocumentNo.Text = existingInvGiftVoucherPurchaseHeader.DocumentNo;
                IsSupplierExistsByID(existingInvGiftVoucherPurchaseHeader.SupplierID);
                txtDeliveryPerson.Text = existingInvGiftVoucherPurchaseHeader.DeliveryPerson;
                txtNic.Text = existingInvGiftVoucherPurchaseHeader.DeliveryPersonNICNo;
                txtVehicle.Text = existingInvGiftVoucherPurchaseHeader.VehicleNo;
                txtRemark.Text = existingInvGiftVoucherPurchaseHeader.Remark;
                txtReferenceNo.Text = existingInvGiftVoucherPurchaseHeader.ReferenceNo;
                txtPartyInvoiceNo.Text = existingInvGiftVoucherPurchaseHeader.PartyInvoiceNo;
                txtDispatchNo.Text = existingInvGiftVoucherPurchaseHeader.DispatchNo;
                cmbLocation.SelectedValue = existingInvGiftVoucherPurchaseHeader.LocationID;
                dtpPartyInvoiceDate.Value = Common.ConvertStringToDate(existingInvGiftVoucherPurchaseHeader.PartyInvoiceDate.ToString());
                dtpDispatchDate.Value = Common.ConvertStringToDate(existingInvGiftVoucherPurchaseHeader.DispatchDate.ToString());
                dtpDocumentDate.Value = Common.ConvertStringToDate(existingInvGiftVoucherPurchaseHeader.DocumentDate.ToString());
                cmbPaymentTerms.SelectedValue = existingInvGiftVoucherPurchaseHeader.PaymentMethodID;

                creditLimit = existingInvGiftVoucherPurchaseHeader.ChequeLimit;
                creditPeriod = existingInvGiftVoucherPurchaseHeader.CreditPeriod;
                chequeLimit = existingInvGiftVoucherPurchaseHeader.ChequeLimit;
                chequePeriod = existingInvGiftVoucherPurchaseHeader.ChequePeriod;

                if (!existingInvGiftVoucherPurchaseHeader.DiscountPercentage.Equals(0))
                {
                    txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseHeader.DiscountPercentage);
                    chkSubTotalDiscountPercentage.Checked = true;
                }
                else
                {
                    txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseHeader.DiscountAmount);
                        //Common.ConvertDecimalToStringCurrency(0);
                    chkSubTotalDiscountPercentage.Checked = false;
                }

                if (!existingInvGiftVoucherPurchaseHeader.TaxAmount.Equals(0))
                {
                    chkTaxEnable.Checked = true;
                    txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseHeader.TaxAmount);
                }
                else
                {
                    chkTaxEnable.Checked = false;
                    txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                }
                txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseHeader.OtherCharges);
                txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseHeader.DiscountAmount);
                txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseHeader.GrossAmount);
                txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseHeader.NetAmount);
                txtTotalQty.Text = existingInvGiftVoucherPurchaseHeader.GiftVoucherQty.ToString();

                if (existingInvGiftVoucherPurchaseHeader.VoucherType == 1)
                {
                    rdoVoucher.Checked = true;
                    txtGiftVoucherValue.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseHeader.GiftVoucherAmount);
                }
                else if (existingInvGiftVoucherPurchaseHeader.VoucherType == 2)
                {
                    rdoCoupon.Checked = true;
                    txtPercentageOfCoupon.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseHeader.GiftVoucherPercentage);
                }

                if (!existingInvGiftVoucherPurchaseHeader.ReferenceDocumentID.Equals(0))
                {
                    InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                    txtPurchaseOrderNo.Text = invGiftVoucherPurchaseOrderService.GetSavedDocumentDetailsByDocumentID(existingInvGiftVoucherPurchaseHeader.ReferenceDocumentID).DocumentNo;
                }
                else
                {
                    txtPurchaseOrderNo.Text = string.Empty;
                }

                dgvVoucherBookDetails.DataSource = null;
                invGiftVoucherPurchaseDetailsTemp = invGiftVoucherPurchaseService.GetAllPurchaseDetail(existingInvGiftVoucherPurchaseHeader);
                dgvVoucherBookDetails.DataSource = invGiftVoucherPurchaseDetailsTemp;
                dgvVoucherBookDetails.Refresh();

                Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo, txtPurchaseOrderNo);
                Common.EnableComboBox(false, cmbLocation);
                Common.EnableButton(false, btnDocumentDetails, btnPoDetails);

                if (existingInvGiftVoucherPurchaseHeader.DocumentStatus.Equals(0))
                {
                    //tabGRN.Enabled = true;
                    grpFooter.Enabled = true;
                    EnableLine(true);
                    grpBody.Enabled = true;
                    dgvVoucherBookDetails.Enabled = true;
                    ResetVoucherType();
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    txtGroupCode.Focus();
                }
                else
                {
                    Common.EnableComboBox(false, cmbPaymentTerms);
                    grpHeader.Enabled = false;
                    Common.EnableButton(false, btnPause, btnSave, btnPaymentTermLimits);
                }
            }
        }

        /// <summary>
        /// Load reference document
        /// </summary>
        private void LoadPurchaseOrderDocument()
        {
            InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
            InvGiftVoucherPurchaseOrderHeader existingInvGiftVoucherPurchaseOrderHeader = new InvGiftVoucherPurchaseOrderHeader();

            existingInvGiftVoucherPurchaseOrderHeader = invGiftVoucherPurchaseOrderService.GetInvGiftVoucherPurchaseOrderHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherPurchaseOrder").DocumentID, txtPurchaseOrderNo.Text.Trim(), Common.LoggedLocationID);
            if (existingInvGiftVoucherPurchaseOrderHeader == null)
            {
                Toast.Show(this.Text + " - " + txtPurchaseOrderNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            else
            {
                InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();

                txtDocumentNo.Text = GetDocumentNo(true);
                recallPO = true;
                IsSupplierExistsByID(existingInvGiftVoucherPurchaseOrderHeader.SupplierID);
                txtDeliveryPerson.Text = "";
                txtNic.Text = "";
                txtVehicle.Text = "";
                txtRemark.Text = existingInvGiftVoucherPurchaseOrderHeader.Remark;
                txtReferenceNo.Text = existingInvGiftVoucherPurchaseOrderHeader.ReferenceNo;
                txtPartyInvoiceNo.Text = "";
                txtDispatchNo.Text = "";
                cmbLocation.SelectedValue = existingInvGiftVoucherPurchaseOrderHeader.LocationID;
                dtpPartyInvoiceDate.Value = Common.GetSystemDate();
                dtpDispatchDate.Value = Common.GetSystemDate();
                dtpDocumentDate.Value = Common.GetSystemDate();
                cmbPaymentTerms.SelectedValue = -1;
                cmbPaymentTerms.SelectedValue = existingInvGiftVoucherPurchaseOrderHeader.PaymentMethodID;

                if (!existingInvGiftVoucherPurchaseOrderHeader.DiscountPercentage.Equals(0))
                {
                    txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseOrderHeader.DiscountPercentage);
                    chkSubTotalDiscountPercentage.Checked = true;
                }
                else
                {
                    txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherPurchaseOrderHeader.DiscountAmount); //Common.ConvertDecimalToStringCurrency(0);
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
                txtPurchaseOrderNo.Text = existingInvGiftVoucherPurchaseOrderHeader.DocumentNo;
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

                creditLimit = existingInvGiftVoucherPurchaseOrderHeader.ChequeLimit;
                creditPeriod = existingInvGiftVoucherPurchaseOrderHeader.CreditPeriod;
                chequeLimit = existingInvGiftVoucherPurchaseOrderHeader.ChequeLimit;
                chequePeriod = existingInvGiftVoucherPurchaseOrderHeader.ChequePeriod;

                dgvVoucherBookDetails.DataSource = null;
                invGiftVoucherPurchaseDetailsTemp = invGiftVoucherPurchaseService.GetGiftVoucherPurchaseOrderDetail(existingInvGiftVoucherPurchaseOrderHeader, txtDocumentNo.Text.Trim());
                dgvVoucherBookDetails.DataSource = invGiftVoucherPurchaseDetailsTemp;
                dgvVoucherBookDetails.Refresh();
                txtTotalQty.Text = invGiftVoucherPurchaseDetailsTemp.Count().ToString();

                Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo, txtPurchaseOrderNo);
                Common.EnableComboBox(false, cmbLocation);
                Common.EnableButton(false, btnDocumentDetails, btnPoDetails);
                
                grpFooter.Enabled = true;
                EnableLine(true);
                grpBody.Enabled = true;
                dgvVoucherBookDetails.Enabled = true;

                if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

                if (poIsMandatory)
                {
                    Common.SetAutoComplete(txtGroupCode, invGiftVoucherPurchaseService.GetRecallPOInvGiftVoucherGroupCodes(existingInvGiftVoucherPurchaseOrderHeader), chkAutoCompleationGroup.Checked);
                    Common.SetAutoComplete(txtGroupName, invGiftVoucherPurchaseService.GetRecallPOInvGiftVoucherGroupNames(existingInvGiftVoucherPurchaseOrderHeader), chkAutoCompleationGroup.Checked);

                    Common.SetAutoComplete(txtBookCode, invGiftVoucherPurchaseService.GetRecallPOInvGiftVoucherBookCodesByVoucherType(existingInvGiftVoucherPurchaseOrderHeader), chkAutoCompleationBook.Checked);
                    Common.SetAutoComplete(txtBookName, invGiftVoucherPurchaseService.GetRecallPOInvGiftVoucherBookNamesByVoucherType(existingInvGiftVoucherPurchaseOrderHeader), chkAutoCompleationBook.Checked);
                }

                ResetVoucherType();
            }
        }

        /// <summary>
        /// Load and bind gift vouchers into text boxes depending on selection criteria
        /// </summary>
        private void LoadGiftVouchers()
        {
            InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
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
            List<InvGiftVoucherMaster> invGiftVoucherMastersList = LoadVoucherBookDetails();
            List<InvGiftVoucherPurchaseDetailTemp> invGiftVoucherPurchaseDetailsTempList = new List<InvGiftVoucherPurchaseDetailTemp>();
            long lineNo = 0;

            //if (invGiftVoucherPurchaseDetailsTemp == null)
            //{ invGiftVoucherPurchaseDetailsTemp = new List<InvGiftVoucherPurchaseDetailTemp>(); }

            int bookID = invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID;
            
            invGiftVoucherPurchaseDetailsTemp.RemoveAll(x => x.InvGiftVoucherBookCodeID == bookID);

            invGiftVoucherPurchaseDetailsTemp.ToList().ForEach(x => x.LineNo = lineNo += 1);

            if (invGiftVoucherPurchaseDetailsTemp.Count.Equals(0))
            { lineNo = 0; }
            else
            { lineNo = invGiftVoucherPurchaseDetailsTemp.Max(s => s.LineNo); }

            if (invGiftVoucherPurchaseDetailsTemp != null)
            {
                foreach (InvGiftVoucherMaster invGiftVoucherMaster in invGiftVoucherMastersList)
                {
                    lineNo += 1;
                    InvGiftVoucherPurchaseDetailTemp invGiftVoucherPurchaseDetailtemp = invGiftVoucherPurchaseDetailsTemp.Find(x => x.InvGiftVoucherMasterID == invGiftVoucherMaster.InvGiftVoucherMasterID);
                    if (invGiftVoucherPurchaseDetailtemp != null)
                    {
                        var tc = new InvGiftVoucherPurchaseDetailTemp
                        {
                            InvGiftVoucherPurchaseDetailID = invGiftVoucherPurchaseDetailtemp.InvGiftVoucherPurchaseDetailID,
                            InvGiftVoucherPurchaseHeaderID = invGiftVoucherPurchaseDetailtemp.InvGiftVoucherPurchaseHeaderID,
                            CompanyID = invGiftVoucherPurchaseDetailtemp.CompanyID,
                            LocationID = invGiftVoucherPurchaseDetailtemp.LocationID,
                            DocumentNo = txtDocumentNo.Text.Trim(),
                            DocumentID = invGiftVoucherPurchaseDetailtemp.DocumentID,
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
                        };
                        invGiftVoucherPurchaseDetailsTempList.Add(tc);
                    }
                    else
                    {
                        var tc = new InvGiftVoucherPurchaseDetailTemp
                        {
                            LineNo = lineNo,
                            DocumentDate = Common.GetSystemDateWithTime(),
                            DocumentNo = txtDocumentNo.Text.Trim(),
                            DocumentID = documentID,
                            InvGiftVoucherMasterID = invGiftVoucherMaster.InvGiftVoucherMasterID,
                            InvGiftVoucherBookCodeID = invGiftVoucherMaster.InvGiftVoucherBookCodeID,
                            InvGiftVoucherGroupID = invGiftVoucherMaster.InvGiftVoucherGroupID,
                            VoucherNo = invGiftVoucherMaster.VoucherNo,
                            VoucherSerial = invGiftVoucherMaster.VoucherSerial,
                            VoucherAmount = invGiftVoucherMaster.GiftVoucherValue,
                            GiftVoucherPercentage = invGiftVoucherMaster.GiftVoucherPercentage,
                            VoucherValue = (rdoVoucher.Checked ? invGiftVoucherMaster.GiftVoucherValue : invGiftVoucherMaster.GiftVoucherPercentage),
                            VoucherType = (rdoVoucher.Checked ? 1 : 2),
                            //DocumentStatus = 1,
                            NumberOfCount = 1,
                        };
                        invGiftVoucherPurchaseDetailsTempList.Add(tc);
                    }
                }

                var deletedInvGiftVoucherPurchaseOrderDetails = CommonService.Except(invGiftVoucherPurchaseDetailsTemp, invGiftVoucherPurchaseDetailsTempList, paymentDeta => paymentDeta.VoucherSerial);

               // invGiftVoucherPurchaseDetailsTemp = invGiftVoucherPurchaseDetailsTempList;
                invGiftVoucherPurchaseDetailsTemp.AddRange(invGiftVoucherPurchaseDetailsTempList);
            }
            else if (invGiftVoucherPurchaseDetailsTemp == null)
            {
                foreach (InvGiftVoucherMaster invGiftVoucherMaster in invGiftVoucherMastersList)
                {
                    lineNo += 1;
                    var tc = new InvGiftVoucherPurchaseDetailTemp
                    {
                        LineNo = lineNo,
                        DocumentDate = Common.GetSystemDateWithTime(),
                        DocumentNo = txtDocumentNo.Text.Trim(),
                        DocumentID = documentID,
                        InvGiftVoucherMasterID = invGiftVoucherMaster.InvGiftVoucherMasterID,
                        InvGiftVoucherBookCodeID = invGiftVoucherMaster.InvGiftVoucherBookCodeID,
                        InvGiftVoucherGroupID = invGiftVoucherMaster.InvGiftVoucherGroupID,
                        VoucherNo = invGiftVoucherMaster.VoucherNo,
                        VoucherSerial = invGiftVoucherMaster.VoucherSerial,
                        VoucherAmount = invGiftVoucherMaster.GiftVoucherValue,
                        GiftVoucherPercentage = invGiftVoucherMaster.GiftVoucherPercentage,
                        VoucherValue = (rdoVoucher.Checked ? invGiftVoucherMaster.GiftVoucherValue : invGiftVoucherMaster.GiftVoucherPercentage),
                        VoucherType = (rdoVoucher.Checked ? 1 : 2),
                        //DocumentStatus = 1,
                        NumberOfCount = 1,
                    };
                    invGiftVoucherPurchaseDetailsTempList.Add(tc);                    
                }
                invGiftVoucherPurchaseDetailsTemp = invGiftVoucherPurchaseDetailsTempList;
            }      
            
            //foreach (InvGiftVoucherMaster invGiftVoucherMaster in invGiftVoucherMastersList)
            //{
            //    var tc = new InvGiftVoucherPurchaseDetailTemp
            //    {
            //        LineNo = lineNo,
            //        DocumentDate = DateTime.UtcNow,
            //        DocumentNo = txtDocumentNo.Text.Trim(),
            //        DocumentID = documentID,
            //        InvGiftVoucherMasterID = invGiftVoucherMaster.InvGiftVoucherMasterID,
            //        InvGiftVoucherBookCodeID = invGiftVoucherMaster.InvGiftVoucherBookCodeID,
            //        InvGiftVoucherGroupID = invGiftVoucherMaster.InvGiftVoucherGroupID,
            //        VoucherNo = invGiftVoucherMaster.VoucherNo,
            //        VoucherSerial = invGiftVoucherMaster.VoucherSerial,
            //        VoucherAmount = invGiftVoucherMaster.GiftVoucherValue,
            //        GiftVoucherPercentage = invGiftVoucherMaster.GiftVoucherPercentage,
            //        VoucherValue = (rdoVoucher.Checked ? invGiftVoucherMaster.GiftVoucherValue : invGiftVoucherMaster.GiftVoucherPercentage),
            //        VoucherType = (rdoVoucher.Checked ? 1 : 2),
            //        DocumentStatus = 1,
            //    };
            //    invGiftVoucherPurchaseDetailsTemp.Add(tc);
            //    lineNo += 1;
            //}
            UpdatedgvGiftVoucherDetails();
        }

        /// <summary>
        /// Load and bind gift vouchers depending on selection criteria
        /// </summary>
        private List<InvGiftVoucherMaster> LoadVoucherBookDetails()
        {
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
            InvGiftVoucherMasterService invGiftVoucherMasterService = new InvGiftVoucherMasterService();

            List<InvGiftVoucherMaster> invGiftVoucherMastersList = new List<InvGiftVoucherMaster>();

            if (!poIsMandatory)
            {
                if (cmbSelectionCriteria.SelectedIndex.Equals(0))
                {
                    if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
                    {
                        invGiftVoucherMastersList = invGiftVoucherMasterService.GetGeneratedGiftVouchersByBookIDWithQtyForPurchase(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtGiftVoucherQty.Text.Trim()));
                    }
                }
                else if (cmbSelectionCriteria.SelectedIndex.Equals(2))
                {
                    if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
                    {
                        //invGiftVoucherMastersList = invGiftVoucherMasterService.GetGeneratedGiftVouchersByBookIDWithVoucherNoRangeForPurchase(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtVoucherNoFrom.Text.Trim()), Common.ConvertStringToInt(txtVoucherNoTo.Text.Trim()));
                        invGiftVoucherMastersList = invGiftVoucherMasterService.GetGeneratedGiftVouchersByBookIDWithVoucherNoRangeForPurchase(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherNoFrom.Text.Trim(), txtVoucherNoTo.Text.Trim());
                    }
                }
                else if (cmbSelectionCriteria.SelectedIndex.Equals(1))
                {
                    if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
                    {
                        //invGiftVoucherMastersList = invGiftVoucherMasterService.GetGeneratedPOGiftVouchersByBookIDWithVoucherSerialRangeForPurchase(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtVoucherSerialFrom.Text.Trim()), Common.ConvertStringToInt(txtVoucherSerialTo.Text.Trim()));
                        invGiftVoucherMastersList = invGiftVoucherMasterService.GetGeneratedGiftVouchersByBookIDWithVoucherSerialRangeForPurchase(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherSerialFrom.Text.Trim(),txtVoucherSerialTo.Text.Trim());
                    }
                }
            }
            else
            {
                InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                if (cmbSelectionCriteria.SelectedIndex.Equals(0))
                {
                    
                    if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
                    {
                        invGiftVoucherMastersList = invGiftVoucherPurchaseService.GetGeneratedPOGiftVouchersByBookIDWithQtyForPurchase(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, invGiftVoucherPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim()).InvGiftVoucherPurchaseOrderHeaderID, Common.ConvertStringToInt(txtGiftVoucherQty.Text.Trim()));
                    }
                }
                else if (cmbSelectionCriteria.SelectedIndex.Equals(2))
                {
                    if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
                    {
                        //invGiftVoucherMastersList = invGiftVoucherMasterService.GetGeneratedGiftVouchersByBookIDWithVoucherNoRangeForPurchase(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtVoucherNoFrom.Text.Trim()), Common.ConvertStringToInt(txtVoucherNoTo.Text.Trim()));
                        invGiftVoucherMastersList = invGiftVoucherPurchaseService.GetGeneratedPOGiftVouchersByBookIDWithVoucherNoRangeForPurchase(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, invGiftVoucherPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim()).InvGiftVoucherPurchaseOrderHeaderID, txtVoucherNoFrom.Text.Trim(), txtVoucherNoTo.Text.Trim());
                    }
                }
                else if (cmbSelectionCriteria.SelectedIndex.Equals(1))
                {
                    if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
                    {
                        //invGiftVoucherMastersList = invGiftVoucherMasterService.GetGeneratedPOGiftVouchersByBookIDWithVoucherSerialRangeForPurchase(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtVoucherSerialFrom.Text.Trim()), Common.ConvertStringToInt(txtVoucherSerialTo.Text.Trim()));
                        invGiftVoucherMastersList = invGiftVoucherPurchaseService.GetGeneratedPOGiftVouchersByBookIDWithVoucherSerialRangeForPurchase(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, invGiftVoucherPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim()).InvGiftVoucherPurchaseOrderHeaderID, txtVoucherSerialFrom.Text.Trim(), txtVoucherSerialTo.Text.Trim());
                    }
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

            invGiftVoucherPurchaseDetailsTemp = invGiftVoucherPurchaseDetailsTemp.OrderBy(pr => pr.LineNo).ToList();
            dgvVoucherBookDetails.DataSource = invGiftVoucherPurchaseDetailsTemp.OrderBy(pr => pr.LineNo).ToList();

            GetSummarizeFigures(invGiftVoucherPurchaseDetailsTemp);
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

            EnableLine(false);
            Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo, txtPurchaseOrderNo);
            Common.EnableComboBox(false, cmbLocation);
            if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
            if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
            Common.EnableButton(false, btnDocumentDetails, btnPoDetails);
            //groupBox4.Enabled = false;
            
            if (invGiftVoucherPurchaseDetailsTemp.Count > 0)
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
            //Common.EnableTextBox(enable, txtVoucherValue, txtQty);
            Common.EnableComboBox(enable, cmbSelectionCriteria);
            //groupBox1.Enabled = enable;
            groupBox2.Enabled = enable;
            groupBox3.Enabled = enable;
            btnLoad.Enabled = enable;
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtBookCode, txtBookName, txtGroupCode, txtGroupName, txtNoOfVouchersOnBook, txtGiftVoucherQty, txtVoucherNoFrom, txtVoucherNoTo, txtVoucherSerialFrom, txtVoucherSerialTo);
            Common.ClearComboBox(cmbSelectionCriteria);
        }

        private void GetSummarizeFigures(List<InvGiftVoucherPurchaseDetailTemp> listItem)
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
            otherChargersAmount = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.Trim());

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
            //txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(grossAmount);
            txtGrossAmount.Text = grossAmount.ToString();
            txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(discountAmount);
            //txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(otherChargersAmount);
            txtOtherCharges.Text = otherChargersAmount.ToString();
            txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(taxAmount);
            txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(netAmount);
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

        // Confirm Payment methods by ID
        private bool IsPaymentMethodExistsByID(int paymentMethodID)
        {
            bool recodFound = false;
            PaymentMethodService paymentMethodService = new PaymentMethodService();

            if (paymentMethodService.GetPaymentMethodsByID(paymentMethodID) != null)
            {
                recodFound = true;
                //txtDocumentNo.Text = GetDocumentNo(true);
                //txtDocumentNo.Enabled = false;
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
            InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
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

                if (poIsMandatory)
                {
                    InvGiftVoucherPurchaseOrderHeader existingInvGiftVoucherPurchaseOrderHeader = new InvGiftVoucherPurchaseOrderHeader();
                    InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();

                    existingInvGiftVoucherPurchaseOrderHeader = invGiftVoucherPurchaseOrderService.GetInvGiftVoucherPurchaseOrderHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherPurchaseOrder").DocumentID, txtPurchaseOrderNo.Text.Trim(), Common.LoggedLocationID);
                    Common.SetAutoComplete(txtVoucherSerialFrom, invGiftVoucherPurchaseService.GetVoucherSerialByRecallPO(existingInvGiftVoucherPurchaseOrderHeader, invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                    Common.SetAutoComplete(txtVoucherSerialTo, invGiftVoucherPurchaseService.GetVoucherSerialByRecallPO(existingInvGiftVoucherPurchaseOrderHeader, invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);

                    Common.SetAutoComplete(txtVoucherNoFrom, invGiftVoucherPurchaseService.GetVoucherNoByRecallPO(existingInvGiftVoucherPurchaseOrderHeader, invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                    Common.SetAutoComplete(txtVoucherNoTo, invGiftVoucherPurchaseService.GetVoucherNoByRecallPO(existingInvGiftVoucherPurchaseOrderHeader, invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                }
                else
                {
                    Common.SetAutoComplete(txtVoucherSerialFrom, invGiftVoucherPurchaseService.GetVoucherSerialByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                    Common.SetAutoComplete(txtVoucherSerialTo, invGiftVoucherPurchaseService.GetVoucherSerialByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);

                    Common.SetAutoComplete(txtVoucherNoFrom, invGiftVoucherPurchaseService.GetVoucherNoByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                    Common.SetAutoComplete(txtVoucherNoTo, invGiftVoucherPurchaseService.GetVoucherNoByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                }
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
            InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
            InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
            InvGiftVoucherBookCode invGiftVoucherBookCode = new InvGiftVoucherBookCode();
            InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader = new InvGiftVoucherPurchaseOrderHeader();

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
                txtPercentageOfCoupon.Text = Common.ConvertDecimalToStringCurrency(invGiftVoucherBookCode.GiftVoucherPercentage);

                Common.EnableComboBox(true, cmbSelectionCriteria);
                Common.EnableButton(true, btnLoad);

                if (poIsMandatory)
                {
                    InvGiftVoucherPurchaseOrderHeader existingInvGiftVoucherPurchaseOrderHeader = new InvGiftVoucherPurchaseOrderHeader();

                    existingInvGiftVoucherPurchaseOrderHeader = invGiftVoucherPurchaseOrderService.GetInvGiftVoucherPurchaseOrderHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherPurchaseOrder").DocumentID, txtPurchaseOrderNo.Text.Trim(), Common.LoggedLocationID);
                    Common.SetAutoComplete(txtVoucherSerialFrom, invGiftVoucherPurchaseService.GetVoucherSerialByRecallPO(existingInvGiftVoucherPurchaseOrderHeader, invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                    Common.SetAutoComplete(txtVoucherSerialTo, invGiftVoucherPurchaseService.GetVoucherSerialByRecallPO(existingInvGiftVoucherPurchaseOrderHeader, invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);

                    Common.SetAutoComplete(txtVoucherNoFrom, invGiftVoucherPurchaseService.GetVoucherNoByRecallPO(existingInvGiftVoucherPurchaseOrderHeader, invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                    Common.SetAutoComplete(txtVoucherNoTo, invGiftVoucherPurchaseService.GetVoucherNoByRecallPO(existingInvGiftVoucherPurchaseOrderHeader, invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                }
                else
                {
                    Common.SetAutoComplete(txtVoucherSerialFrom, invGiftVoucherPurchaseService.GetVoucherSerialByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                    Common.SetAutoComplete(txtVoucherSerialTo, invGiftVoucherPurchaseService.GetVoucherSerialByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);

                    Common.SetAutoComplete(txtVoucherNoFrom, invGiftVoucherPurchaseService.GetVoucherNoByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                    Common.SetAutoComplete(txtVoucherNoTo, invGiftVoucherPurchaseService.GetVoucherNoByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                }
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtVoucherNo, txtBookName);
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
            }
            else if (criteriaIndex == 2)
            {
                EnableSelectionCriteriaControls(true, 1, groupBox3);
                EnableSelectionCriteriaControls(false, 1, groupBox2);
            }
            else if (criteriaIndex == 1)
            {
                EnableSelectionCriteriaControls(true, 2, groupBox3);
                EnableSelectionCriteriaControls(false, 2, groupBox2);
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
                        
                        Common.EnableTextBox(enabled, txtVoucherNoFrom, txtVoucherNoTo);
                        tbpVoucherNo.Focus();
                        tbMore.SelectedTab = tbpVoucherNo;
                        txtVoucherNoFrom.Focus();
                    }
                    else if (tabIndex == 2)
                    {
                        tbpVoucherNo.Enabled = false;
                        tbpVoucherSerial.Enabled = true;
                        
                        Common.EnableTextBox(enabled, txtVoucherSerialFrom, txtVoucherSerialTo);
                        tbpVoucherSerial.Focus();
                        tbMore.SelectedTab = tbpVoucherSerial;
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
        /// <param name="newDocumentNo"></param>
        /// <returns></returns>
        private bool SaveDocument(int documentStatus, string documentNo, out string newDocumentNo)
        {
            //GetSummarizeFigures();

            InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
            InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader = new InvGiftVoucherPurchaseHeader();
            Location location = new Location();
            LocationService locationService = new LocationService();
            LgsSupplierService lgsSupplierService = new LgsSupplierService();
            LgsSupplier lgsSupplier = new LgsSupplier();

            location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
            lgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());

            invGiftVoucherPurchaseHeader = invGiftVoucherPurchaseService.GetInvGiftVoucherPurchaseHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), location.LocationID);
            if (invGiftVoucherPurchaseHeader == null)
            {invGiftVoucherPurchaseHeader = new InvGiftVoucherPurchaseHeader();}
            ////if (documentStatus.Equals(1)) // update paused document
            ////{
            ////    documentNo = GetDocumentNo(false);
            ////    txtDocumentNo.Text = documentNo;
            ////}

            invGiftVoucherPurchaseHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
            invGiftVoucherPurchaseHeader.CompanyID = location.CompanyID;
            invGiftVoucherPurchaseHeader.LocationID = location.LocationID;
            invGiftVoucherPurchaseHeader.CostCentreID = location.CostCentreID;
            invGiftVoucherPurchaseHeader.DocumentID = documentID;
            invGiftVoucherPurchaseHeader.DocumentNo = documentNo.Trim();
            invGiftVoucherPurchaseHeader.DocumentDate = Common.ConvertStringToDate(dtpDocumentDate.Value.ToString());
            invGiftVoucherPurchaseHeader.SupplierID = lgsSupplier.LgsSupplierID;
            invGiftVoucherPurchaseHeader.DeliveryPerson = txtDeliveryPerson.Text.Trim();
            invGiftVoucherPurchaseHeader.DeliveryPersonNICNo = txtNic.Text.Trim();
            invGiftVoucherPurchaseHeader.VehicleNo = txtVehicle.Text.Trim();
            invGiftVoucherPurchaseHeader.DispatchNo = txtDispatchNo.Text.Trim();
            invGiftVoucherPurchaseHeader.PartyInvoiceNo = txtPartyInvoiceNo.Text.Trim();
            invGiftVoucherPurchaseHeader.PaymentMethodID = int.Parse(cmbPaymentTerms.SelectedValue.ToString().Trim());
            invGiftVoucherPurchaseHeader.PartyInvoiceDate = Common.ConvertStringToDate(dtpPartyInvoiceDate.Value.ToString());
            invGiftVoucherPurchaseHeader.DispatchDate = Common.ConvertStringToDate(dtpDispatchDate.Value.ToString());
            invGiftVoucherPurchaseHeader.GiftVoucherAmount = Common.ConvertStringToDecimalCurrency(txtGiftVoucherValue.Text.Trim());
            invGiftVoucherPurchaseHeader.GiftVoucherPercentage = Common.ConvertStringToDecimalCurrency(txtPercentageOfCoupon.Text.Trim());
            invGiftVoucherPurchaseHeader.GrossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.ToString());
            invGiftVoucherPurchaseHeader.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscount.Text.ToString());
            if (chkSubTotalDiscountPercentage.Checked)
            {invGiftVoucherPurchaseHeader.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());}
            invGiftVoucherPurchaseHeader.OtherCharges = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.ToString());
            invGiftVoucherPurchaseHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtNetAmount.Text.ToString());
            invGiftVoucherPurchaseHeader.GiftVoucherQty = Common.ConvertStringToInt(txtTotalQty.Text.ToString());
            invGiftVoucherPurchaseHeader.VoucherType = (rdoVoucher.Checked ? 1 : 2);
            invGiftVoucherPurchaseHeader.CreditLimit = creditLimit;
            invGiftVoucherPurchaseHeader.CreditPeriod = creditPeriod;
            invGiftVoucherPurchaseHeader.ChequeLimit = chequeLimit;
            invGiftVoucherPurchaseHeader.ChequePeriod = chequePeriod;

            if (chkTaxEnable.Checked)
            {
                decimal tax1 = 0;
                decimal tax2 = 0;
                decimal tax3 = 0;
                decimal tax4 = 0;
                decimal tax5 = 0;

                CommonService commonService = new CommonService();
                invGiftVoucherPurchaseHeader.TaxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(3, invGiftVoucherPurchaseHeader.GrossAmount, lgsSupplier.LgsSupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());

                invGiftVoucherPurchaseHeader.TaxAmount1 = tax1;
                invGiftVoucherPurchaseHeader.TaxAmount2 = tax2;
                invGiftVoucherPurchaseHeader.TaxAmount3 = tax3;
                invGiftVoucherPurchaseHeader.TaxAmount4 = tax4;
                invGiftVoucherPurchaseHeader.TaxAmount5 = tax5;
            }
            
            if (!txtPurchaseOrderNo.Text.Trim().Equals(string.Empty))
            {
                InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader = new InvGiftVoucherPurchaseOrderHeader();

                invGiftVoucherPurchaseOrderHeader = invGiftVoucherPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());
                invGiftVoucherPurchaseHeader.ReferenceDocumentDocumentID = invGiftVoucherPurchaseOrderHeader.DocumentID;
                invGiftVoucherPurchaseHeader.ReferenceDocumentID = invGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID;
            }

            //invPurchaseOrderHeader.RequestedBy = Common.LoggedUser;
            ////invPurchaseOrderHeader.ReferenceDocumentID = 1;
            ////invPurchaseOrderHeader.ReferenceDocumentNo = "REF001";
            invGiftVoucherPurchaseHeader.ReferenceNo = txtReferenceNo.Text.Trim();
            invGiftVoucherPurchaseHeader.Remark = txtRemark.Text.Trim();
            invGiftVoucherPurchaseHeader.DocumentStatus = documentStatus;
            invGiftVoucherPurchaseHeader.CreatedUser = Common.LoggedUser;
            invGiftVoucherPurchaseHeader.CreatedDate = Common.GetSystemDateWithTime();
            invGiftVoucherPurchaseHeader.ModifiedUser = Common.LoggedUser;
            invGiftVoucherPurchaseHeader.ModifiedDate = Common.GetSystemDateWithTime();
            ////invPurchaseOrderHeader.DataTransfer = ;           

            return invGiftVoucherPurchaseService.SaveGiftVoucherPurchase(invGiftVoucherPurchaseHeader, invGiftVoucherPurchaseDetailsTemp, out newDocumentNo, this.Name);
        }

        private string GetDocumentNo(bool isTemporytNo)
        {

            try
            {
                InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                LocationService locationService = new LocationService();
                return invGiftVoucherPurchaseService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }
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

                if (!poIsMandatory)
                {
                    Common.SetAutoComplete(txtGroupCode, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupCodes(), chkAutoCompleationGroup.Checked);
                    Common.SetAutoComplete(txtGroupName, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupNames(), chkAutoCompleationGroup.Checked);
                    LoadBookByVoucherType();
                }
                txtGroupCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private long LoadSelectedVoucherQty()
        {
            InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
            long qty = 0;
            if (cmbSelectionCriteria.SelectedIndex == 1)
            {
                qty = invGiftVoucherPurchaseService.GetVoucherQtyByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherSerialFrom.Text, txtVoucherSerialTo.Text, cmbBasedOn.SelectedIndex);
            }
            else if (cmbSelectionCriteria.SelectedIndex == 2)
            {
                qty = invGiftVoucherPurchaseService.GetVoucherQtyByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherNoFrom.Text, txtVoucherNoTo.Text, cmbBasedOn.SelectedIndex);
            }
            return qty;
        }

        private void LoadBookByVoucherType()
        {
            if (!poIsMandatory)
            {
                InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterGroupValueService = new InvGiftVoucherBookCodeGenerationService();
                Common.SetAutoComplete(txtBookCode, invGiftVoucherMasterGroupValueService.GetInvGiftVoucherBookGenerationCodesByVoucherType((rdoVoucher.Checked ? 1 : 2)), chkAutoCompleationBook.Checked);
                Common.SetAutoComplete(txtBookName, invGiftVoucherMasterGroupValueService.GetInvGiftVoucherBookGenerationNamesByVoucherType((rdoVoucher.Checked ? 1 : 2)), chkAutoCompleationBook.Checked);
            }
            else
            {
                InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                InvGiftVoucherPurchaseOrderHeader existingInvGiftVoucherPurchaseOrderHeaderTemp = invGiftVoucherPurchaseOrderService.GetInvGiftVoucherPurchaseOrderHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherPurchaseOrder").DocumentID, txtPurchaseOrderNo.Text.Trim(), Common.LoggedLocationID);

                Common.SetAutoComplete(txtBookCode, invGiftVoucherPurchaseService.GetRecallPOInvGiftVoucherBookCodesByVoucherType(existingInvGiftVoucherPurchaseOrderHeaderTemp), chkAutoCompleationBook.Checked);
                Common.SetAutoComplete(txtBookName, invGiftVoucherPurchaseService.GetRecallPOInvGiftVoucherBookNamesByVoucherType(existingInvGiftVoucherPurchaseOrderHeaderTemp), chkAutoCompleationBook.Checked);
            }
        }

        private void LoadGroups()
        {
            if (!poIsMandatory)
            {
                InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                Common.SetAutoComplete(txtGroupCode, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupCodes(), chkAutoCompleationGroup.Checked);
                Common.SetAutoComplete(txtGroupName, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupNames(), chkAutoCompleationGroup.Checked);
            }
            else
            {
                InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                InvGiftVoucherPurchaseOrderHeader existingInvGiftVoucherPurchaseOrderHeaderTemp = invGiftVoucherPurchaseOrderService.GetInvGiftVoucherPurchaseOrderHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherPurchaseOrder").DocumentID, txtPurchaseOrderNo.Text.Trim(), Common.LoggedLocationID);

                Common.SetAutoComplete(txtGroupCode, invGiftVoucherPurchaseService.GetRecallPOInvGiftVoucherGroupCodes(existingInvGiftVoucherPurchaseOrderHeaderTemp), chkAutoCompleationGroup.Checked);
                Common.SetAutoComplete(txtGroupName, invGiftVoucherPurchaseService.GetRecallPOInvGiftVoucherGroupNames(existingInvGiftVoucherPurchaseOrderHeaderTemp), chkAutoCompleationGroup.Checked);
            }
        }

        public void LoadVoucherSerial(string lblName, string voucherSerial)
        {
            bool isValid = true;
            if (poIsMandatory)
            {
                InvGiftVoucherMaster invGiftVoucherMaster = new InvGiftVoucherMaster();
                InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();

                InvGiftVoucherPurchaseOrderHeader existingInvGiftVoucherPurchaseOrderHeader = new InvGiftVoucherPurchaseOrderHeader();

                existingInvGiftVoucherPurchaseOrderHeader = invGiftVoucherPurchaseOrderService.GetInvGiftVoucherPurchaseOrderHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherPurchaseOrder").DocumentID, txtPurchaseOrderNo.Text.Trim(), Common.LoggedLocationID);

                invGiftVoucherMaster = invGiftVoucherPurchaseService.GetInvGiftVoucherMasterToGRNByPOVoucherSerial(existingInvGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), voucherSerial);
                if (invGiftVoucherMaster == null)
                {
                    isValid = false;
                }
            }
            else
            {
                InvGiftVoucherMaster invGiftVoucherMaster = new InvGiftVoucherMaster();
                InvGiftVoucherMasterService invGiftVoucherMasterService = new InvGiftVoucherMasterService();

                invGiftVoucherMaster = invGiftVoucherMasterService.GetInvGiftVoucherMasterToGRNByVoucherSerial(voucherSerial);
                if (invGiftVoucherMaster == null)
                {
                    isValid = false;
                }
            }
            if (!isValid)
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherSerial.Text.ToString()), " ", lblName.ToString(), " - ", voucherSerial.ToString()), Toast.messageType.Information, Toast.messageAction.Invalid);
                 
                return;
            }
            if (!string.IsNullOrEmpty(txtVoucherSerialTo.Text))
            {
                txtVoucherSerialQty.Text = LoadSelectedVoucherQty().ToString();
            }
        }

        public void LoadVoucherNo(string lblName, string voucherNo)
        {
            bool isValid = true;
            if (poIsMandatory)
            {
                InvGiftVoucherMaster invGiftVoucherMaster = new InvGiftVoucherMaster();
                InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();

                InvGiftVoucherPurchaseOrderHeader existingInvGiftVoucherPurchaseOrderHeader = new InvGiftVoucherPurchaseOrderHeader();

                existingInvGiftVoucherPurchaseOrderHeader = invGiftVoucherPurchaseOrderService.GetInvGiftVoucherPurchaseOrderHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherPurchaseOrder").DocumentID, txtPurchaseOrderNo.Text.Trim(), Common.LoggedLocationID);

                invGiftVoucherMaster = invGiftVoucherPurchaseService.GetInvGiftVoucherMasterToGRNByPOVoucherNo(existingInvGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), voucherNo);
                if (invGiftVoucherMaster == null)
                {
                    isValid = false;
                }
            }
            else
            {
                InvGiftVoucherMaster invGiftVoucherMaster = new InvGiftVoucherMaster();
                InvGiftVoucherMasterService invGiftVoucherMasterService = new InvGiftVoucherMasterService();

                invGiftVoucherMaster = invGiftVoucherMasterService.GetInvGiftVoucherMasterToGRNByVoucherNo(voucherNo);
                if (invGiftVoucherMaster == null)
                {
                    isValid = false;
                }
            }
            if (!isValid)
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherNo.Text.ToString()), " ", lblName.ToString(), " - ", voucherNo.ToString()), Toast.messageType.Information, Toast.messageAction.Invalid);
                return;
            }
            if (!string.IsNullOrEmpty(txtVoucherNoTo.Text))
            {
                txtVoucherNoQty.Text = LoadSelectedVoucherQty().ToString();
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
            { isValid = false; }
            else if (!Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbLocation))
            { isValid = false; }
            //else if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Zero, txtPackSize))
            //{ return false; }

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
            InvGiftVoucherPurchaseValidator invGiftVoucherPurchaseValidator = new InvGiftVoucherPurchaseValidator();
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();

            bool isValidated = false;
            long invPurchaseOrderHeaderID = 0;
            if (!string.IsNullOrEmpty(txtPurchaseOrderNo.Text))
            {
                InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();

                invPurchaseOrderHeaderID = invGiftVoucherPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text).InvGiftVoucherPurchaseOrderHeaderID;
            }

            if (cmbSelectionCriteria.SelectedIndex == 0 && !invGiftVoucherPurchaseValidator.ValidateVoucherQty(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtGiftVoucherQty.Text.Trim()), cmbBasedOn.SelectedIndex, invPurchaseOrderHeaderID))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(lblGiftVoucherQty.Text.ToString()), " - ", txtGiftVoucherQty.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else if (cmbSelectionCriteria.SelectedIndex == 2 && cmbBasedOn.SelectedIndex == 0 && !invGiftVoucherPurchaseValidator.ValidateVoucherNoFromTo(txtVoucherNoFrom.Text.Trim(), txtVoucherNoTo.Text.Trim()))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherNo.Text.ToString()), " - ", txtVoucherNoFrom.Text.Trim(), " - ", txtBookCode.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else if (cmbSelectionCriteria.SelectedIndex == 2 && cmbBasedOn.SelectedIndex == 0 && !invGiftVoucherPurchaseValidator.ValidateVoucherNoRange(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherNoFrom.Text.Trim(), txtVoucherNoTo.Text.Trim(), invPurchaseOrderHeaderID))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherNo.Text.ToString()), " - ", txtVoucherNoFrom.Text.Trim(), " - ", txtBookCode.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else if (cmbSelectionCriteria.SelectedIndex == 1 && !invGiftVoucherPurchaseValidator.ValidateVoucherSerialFromTo(txtVoucherSerialFrom.Text.Trim(), txtVoucherSerialTo.Text.Trim()))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherSerial.Text.ToString()), " - ", txtVoucherSerialFrom.Text.Trim(), " - ", txtVoucherSerialTo.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else if (invGiftVoucherPurchaseDetailsTemp != null && invGiftVoucherPurchaseDetailsTemp.Count > 0 && !invGiftVoucherPurchaseValidator.ValidateVoucherSerialPageType(invGiftVoucherPurchaseDetailsTemp, txtDocumentNo.Text.Trim(), (rdoVoucher.Checked ? 1 : 2), invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(lblBasedOn.Text.ToString()), " - ", cmbBasedOn.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else if (cmbSelectionCriteria.SelectedIndex == 1 && !invGiftVoucherPurchaseValidator.ValidateVoucherSerialRange(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherSerialFrom.Text.Trim(), txtVoucherSerialTo.Text.Trim(), invPurchaseOrderHeaderID))
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
            InvGiftVoucherPurchaseValidator invGiftVoucherPurchaseValidator = new InvGiftVoucherPurchaseValidator();

            bool isValidated = false;

            if (!invGiftVoucherPurchaseValidator.ValidateExistsPausedVoucherSerial(invGiftVoucherPurchaseDetailsTemp, txtDocumentNo.Text.Trim(), (rdoVoucher.Checked ? 1 : 2)))
            {
                Toast.Show("Voucher Serial ", Toast.messageType.Information, Toast.messageAction.ExistsForSelected, " - paused document." );
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

        

        
       #endregion

        

        
        
        
    }
}
