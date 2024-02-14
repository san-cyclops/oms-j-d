using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Domain;
using Service;
using Utility;

namespace UI.Windows
{
    public partial class FrmPayment : UI.Windows.FrmBaseTransactionForm
    {
        UserPrivileges accessRights = new UserPrivileges();

        int documentID = 5;
        bool isBackDated;

        public FrmPayment()
        {
            InitializeComponent();
        }

        #region Form Events
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBoxCurrency5_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Setup GUI contols (Set visibility, data binding, auto loading)
        /// </summary>
        public override void InitializeForm()
        {
            // Load Payment methods
            //PaymentMethodService paymentMethodService = new PaymentMethodService();
            //Common.LoadPaymentMethods(cmbPaymentTerms, paymentMethodService.GetAllPaymentMethods());

            // Load Locations            
            LocationService locationService = new LocationService();
            Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
            cmbLocation.SelectedValue = Common.LoggedLocationID;

            //Common.SetDataGridviewColumnsReadOnly(true, dgvVoucherBookDetails, dgvVoucherBookDetails.Columns[1], dgvVoucherBookDetails.Columns[2], dgvVoucherBookDetails.Columns[3]);
            //Common.ReadOnlyTextBox(true, txtGiftVoucherValue, txtNoOfVouchersOnBook, txtPercentageOfCoupon, txtSubTotalDiscount, txtTotalTaxAmount, txtNetAmount, txtVoucherSerialQty, txtVoucherNoQty);

            Common.EnableTextBox(true, txtSupplierCode, txtSupplierName, txtDocumentNo);
            Common.EnableButton(true, btnDocumentDetails);
            Common.EnableComboBox(true, cmbLocation);
            //Common.EnableComboBox(false, cmbSelectionCriteria);
            Common.EnableButton(false, btnSave, btnPause);
            //if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
            //if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

            // Disable gift voucher selection details controls
            //cmbSelectionCriteria.SelectedIndex = -1;
            //cmbBasedOn.SelectedIndex = -1;
            //IsCriteriaExistsByIndex(-1);

            dtpPaymentDate.Value = Common.GetSystemDate();
            //dtpPartyInvoiceDate.Value = Common.GetSystemDate();
            //dtpDispatchDate.Value = Common.GetSystemDate();

            txtDocumentNo.Text = GetDocumentNo(true);

            //invGiftVoucherPurchaseDetailsTemp = null;

            //existingReferenceType = null;
            //recallPO = false;

            grpHeader.Enabled = true;
            //grpFooter.Enabled = false;
            groupBox4.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;

            
        }

        public override void FormLoad()
        {
            try
            {
                //dgvVoucherBookDetails.AutoGenerateColumns = false;

                // Load Payment methods
                PaymentMethodService paymentMethodService = new PaymentMethodService();
                //Common.LoadPaymentMethods(cmbPaymentTerms, paymentMethodService.GetAllPaymentMethods());

                // Load Locations
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());

                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                Common.SetAutoComplete(txtAPAccountCode, accLedgerAccountService.GetExpenceLedgerCodes(), chkAutoCompleationOtherExpence.Checked);
                Common.SetAutoComplete(txtAPAccountName, accLedgerAccountService.GetExpenceLedgerNames(), chkAutoCompleationOtherExpence.Checked);

                // Read Trade/Expense/Non Trade Suppliers ????
                //based on a/p account - Supplier Type
                SupplierService supplierService = new SupplierService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                Common.SetAutoComplete(txtSupplierCode, supplierService.GetAllSupplierCodesBySupplierType(lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.SupplierType).ToString(), 1).LookupKey), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, supplierService.GetAllSupplierNamesBySupplierType(lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.SupplierType).ToString(), 1).LookupKey), chkAutoCompleationSupplier.Checked);

                // Load cost center name to combo
                CostCentreService costCentreService = new CostCentreService();
                cmbCostCentre.DataSource = costCentreService.GetAllCostCentres();
                cmbCostCentre.DisplayMember = "CostCentreName";
                cmbCostCentre.ValueMember = "CostCentreID";
                cmbCostCentre.Refresh();

                cmbCostCentre.SelectedValue = locationService.GetLocationsByID(Common.LoggedLocationID).CostCentreID;

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;
                isBackDated = autoGenerateInfo.IsBackDated;

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }

                base.FormLoad();

                //RefreshDocumentNumbers();
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
                //InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                //LocationService locationService = new LocationService();
                //return invGiftVoucherPurchaseService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
                return "01";
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }
        }

        private void LoadAccountsByPaymentMode()
        {
            try
            {
                //// Read Company Banks
                //List<AccLedgerAccount> accLedgerAccount = new List<AccLedgerAccount>();
                //accLedgerAccount = accLedgerAccountService.GetBankList();
                //cmbBankBookCode.DataSource = accLedgerAccount;
                //cmbBankBookCode.DisplayMember = "LedgerCode";
                //cmbBankBookCode.ValueMember = "AccLedgerAccountID";
                //cmbBankBookCode.Refresh();

                //cmbBankBookName.DataSource = accLedgerAccount;
                //cmbBankBookName.DisplayMember = "LedgerName";
                //cmbBankBookName.ValueMember = "AccLedgerAccountID";
                //cmbBankBookName.Refresh();

                //// Read Company cash books
                //accLedgerAccount = accLedgerAccountService.GetCashBookBookList();
                //cmbCashBookCode.DataSource = accLedgerAccount;
                //cmbCashBookCode.DisplayMember = "LedgerCode";
                //cmbCashBookCode.ValueMember = "AccLedgerAccountID";
                //cmbCashBookCode.Refresh();

                //cmbCashBookName.DataSource = accLedgerAccount;
                //cmbCashBookName.DisplayMember = "LedgerName";
                //cmbCashBookName.ValueMember = "AccLedgerAccountID";
                //cmbCashBookName.Refresh();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion

        private void chkAutoCompleationSupplier_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //based on a/p account,supplier type
                SupplierService supplierService = new SupplierService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                Common.SetAutoComplete(txtSupplierCode, supplierService.GetAllSupplierCodesBySupplierType(lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.SupplierType).ToString(), 1).LookupKey), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, supplierService.GetAllSupplierNamesBySupplierType(lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.SupplierType).ToString(), 1).LookupKey), chkAutoCompleationSupplier.Checked);
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
                //if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                //{ txtSupplierName.Focus(); }

                //if (e.KeyCode.Equals(Keys.F3))
                //{
                //    SupplierService supplierService = new SupplierService();
                //    DataView dvAllReferenceData = new DataView(supplierService.GetAllActiveSuppliersDataTable());
                //    if (dvAllReferenceData.Count != 0)
                //    {
                //        //LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtSupplierCode);
                //        //txtSupplierCode_Leave(this, e);
                //    }
                //}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
    }
}
