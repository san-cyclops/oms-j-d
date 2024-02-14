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
using Utility;

namespace UI.Windows
{
    public partial class FrmGiftVoucherTransfer : UI.Windows.FrmBaseTransactionForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<InvGiftVoucherTransferNoteDetailTemp> invGiftVoucherTransferNoteDetailsTemp = new List<InvGiftVoucherTransferNoteDetailTemp>();
        int documentID = 4;
        bool recallGRN;

        private int noOfVouchers;

        public FrmGiftVoucherTransfer()
        {
            InitializeComponent();
        }

        #region Form Events

        private void btnDocumentDetails_Click(object sender, EventArgs e)
        {
            try
            {
                InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                DataView dvAllReferenceData = new DataView(invGiftVoucherTransferNoteService.GetPendingTransferNoteDocuments());
                if (dvAllReferenceData.Count != 0)
                {
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), "Pending Documents", string.Empty, txtDocumentNo);
                    if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                    { LoadTransferedNoteDocument(); }
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
                    {LoadTransferedNoteDocument();}
                    else
                    {
                        txtDocumentNo.Text = GetDocumentNo(true);
                        cmbFromLocation.Focus();
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void btnGrnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                DataView dvAllReferenceData = new DataView(invGiftVoucherPurchaseService.GetPendingPurchaseDocumentsToTransfer());
                if (dvAllReferenceData.Count != 0)
                {
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), "Pending Documents", string.Empty, txtGrnNo);
                    if (!txtGrnNo.Text.Trim().Equals(string.Empty))
                    { LoadGoodReceivedDocument(); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGrnNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtGrnNo.Text.Trim().Equals(string.Empty))
                    {
                        LoadGoodReceivedDocument();
                    }
                }
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
        
        private void cmbFromLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, cmbToLocation); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbFromLocation_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbFromLocation.Text.Trim()))
                { return; }

                if (!IsFromLocationExistsByName(cmbFromLocation.Text.Trim()))
                {
                    cmbFromLocation.Focus();
                    return;
                }
                else
                {
                    accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbFromLocation.Text.Trim()).LocationID, documentID);
                    if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }

                    cmbToLocation.Focus();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbToLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtRemark); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbToLocation_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbToLocation.Text.Trim()))
                { return; }

                if (!IsToLocationExistsByName(cmbToLocation.Text.Trim()))
                {
                    cmbToLocation.Focus();
                    return;
                }
                else
                {
                    txtRemark.Focus();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, dtpTransferDate); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dtpTransferDate_KeyDown(object sender, KeyEventArgs e)
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
                if (e.KeyCode.Equals(Keys.Enter))
                    txtReferenceNo_Validated(this, e);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtReferenceNo_Validated(object sender, EventArgs e)
        {
            try
            {
                ResetVoucherType();
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
                    if (!recallGRN)
                    {
                        InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterBookService = new InvGiftVoucherBookCodeGenerationService();
                        DataView dvAllReferenceData = new DataView(invGiftVoucherMasterBookService.GetAllActiveGiftVoucherMasterBooksDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2)));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtBookCode);
                            txtBookCode_Validated(this, e);
                        }
                    }
                    else
                    {
                        InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                        InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                        InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader = invGiftVoucherPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());
                        DataView dvAllReferenceData = new DataView(invGiftVoucherTransferNoteService.GetAllActiveGiftVoucherMasterRecallGRNBooksDataTable(invGiftVoucherPurchaseHeader));
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
                    if (!recallGRN)
                    {
                        InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterBookService = new InvGiftVoucherBookCodeGenerationService();
                        DataView dvAllReferenceData = new DataView(invGiftVoucherMasterBookService.GetAllActiveGiftVoucherMasterBooksDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2)));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtBookCode);
                            txtBookName_Leave(this, e);
                        }
                    }
                    else
                    {
                        InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                        InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                        InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader = invGiftVoucherPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());
                        DataView dvAllReferenceData = new DataView(invGiftVoucherTransferNoteService.GetAllActiveGiftVoucherMasterRecallGRNBooksDataTable(invGiftVoucherPurchaseHeader));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtBookCode);
                            txtBookName_Leave(this, e);
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
                    if (!recallGRN)
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
                        InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                        InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                        InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader = invGiftVoucherPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());
                        DataView dvAllReferenceData = new DataView(invGiftVoucherTransferNoteService.GetAllActiveGiftVoucherRecallGRNGroupsDataTable(invGiftVoucherPurchaseHeader));
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
                    if (!recallGRN)
                    {
                        InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                        DataView dvAllReferenceData = new DataView(invGiftVoucherGroupService.GetAllActiveGiftVoucherGroupsDataTable());
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtGroupCode);
                            txtGroupName_Leave(this, e);
                        }
                    }
                    else
                    {
                        InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                        InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                        InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader = invGiftVoucherPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());
                        DataView dvAllReferenceData = new DataView(invGiftVoucherTransferNoteService.GetAllActiveGiftVoucherRecallGRNGroupsDataTable(invGiftVoucherPurchaseHeader));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtGroupCode);
                            txtGroupName_Leave(this, e);
                        }
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

                Common.SetFocus(e, txtNoOfVouchersOnBook); 
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtVoucherNoFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();

                    if (!recallGRN)
                    {
                        LocationService locationService = new LocationService();
                        Location location = new Location();

                        location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));

                        DataView dvAllReferenceData = new DataView(invGiftVoucherTransferNoteService.GetAllActiveGiftVoucherNosDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2), invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text).InvGiftVoucherGroupID, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text).InvGiftVoucherBookCodeID, location.LocationID, location.IsHeadOffice));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), this.ActiveControl);
                            txtVoucherNoFrom_Leave(this, e);
                        }
                    }
                    else
                    {
                        InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                        InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader = invGiftVoucherPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());
                        DataView dvAllReferenceData = new DataView(invGiftVoucherTransferNoteService.GetAllActiveGiftVoucherMasterRecallGRNVoucherNosDataTable(invGiftVoucherPurchaseHeader));
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

        private void txtVoucherSerialFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
                    
                    if (!recallGRN)
                    {
                        LocationService locationService = new LocationService();
                        Location location = new Location();

                        location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));

                        DataView dvAllReferenceData = new DataView(invGiftVoucherTransferNoteService.GetAllActiveGiftVoucherSerialsDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2), invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text).InvGiftVoucherGroupID, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text).InvGiftVoucherBookCodeID, location.LocationID, location.IsHeadOffice));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtVoucherSerialFrom);
                            txtVoucherSerialFrom_Leave(this, e);
                        }
                    }
                    else
                    {
                        InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                        InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader = invGiftVoucherPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());
                        DataView dvAllReferenceData = new DataView(invGiftVoucherTransferNoteService.GetAllActiveGiftVoucherMasterRecallGRNVoucherSerialsDataTable(invGiftVoucherPurchaseHeader));
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

        private void txtBookName_Leave(object sender, EventArgs e)
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

        private void txtGroupName_Leave(object sender, EventArgs e)
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

        private void chkAutoCompleationDocumentNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                Common.SetAutoComplete(txtDocumentNo, invGiftVoucherTransferNoteService.GetAllDocumentNumbers(), chkAutoCompleationDocumentNo.Checked);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

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

        private void cmbFromLocation_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {// Load Loacations Excepting Current Location
                if (cmbFromLocation.SelectedValue != null && Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()) != -1)
                {
                    LoadLoacationsExceptingCurrentLocation();
                    //RefreshDocumentNumbers();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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

        private void txtVoucherSerialTo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtVoucherSerialTo.Text.Trim()))
                {
                    LoadVoucherSerial(lblVoucherSerialTo.Text, txtVoucherSerialTo.Text);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void LoadVoucherSerial(string lblName, string voucherSerial)
        {
            bool isValid = true;
            InvGiftVoucherMaster invGiftVoucherMaster = new InvGiftVoucherMaster();
            InvGiftVoucherMasterService invGiftVoucherMasterService = new InvGiftVoucherMasterService();

            invGiftVoucherMaster = invGiftVoucherMasterService.GetInvGiftVoucherMasterToTOGByVoucherSerial(voucherSerial);
            if (invGiftVoucherMaster == null)
            {
                isValid = false;
            }
            if (!isValid)
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherSerial.Text.ToString()), " ", lblName.ToString(), " - ", voucherSerial.ToString()), Toast.messageType.Information, Toast.messageAction.Invalid);
                return;
            }
            if (!string.IsNullOrEmpty(txtVoucherSerialTo.Text))
            {
                txtVoucherSerialQty.Text = LoadSelectedVoucherQty().ToString();
                noOfVouchers = Common.ConvertStringToInt(txtVoucherSerialQty.Text);
            }
        }

        public void LoadVoucherNo(string lblName, string voucherNo)
        {
            bool isValid = true;
            InvGiftVoucherMaster invGiftVoucherMaster = new InvGiftVoucherMaster();
            InvGiftVoucherMasterService invGiftVoucherMasterService = new InvGiftVoucherMasterService();

            invGiftVoucherMaster = invGiftVoucherMasterService.GetInvGiftVoucherMasterToTOGByVoucherNo(txtVoucherNoTo.Text);
            if (invGiftVoucherMaster == null)
            {
                isValid = false;
            }
            if (!isValid)
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherNo.Text.ToString()), " ", lblName.ToString(), " - ", voucherNo.ToString()), Toast.messageType.Information, Toast.messageAction.Invalid);
                return;
            }
            if (!string.IsNullOrEmpty(txtVoucherNoTo.Text))
            {
                txtVoucherNoQty.Text = LoadSelectedVoucherQty().ToString();
                noOfVouchers = Common.ConvertStringToInt(txtVoucherNoQty.Text);
            }
        }

        private void txtVoucherNoTo_Leave(object sender, EventArgs e)
        {
           try
            {
                if (!string.IsNullOrEmpty(txtVoucherNoTo.Text.Trim()))
                {
                    LoadVoucherNo(lblVoucherNoTo.Text, txtVoucherNoTo.Text);
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

        private void txtVoucherSerialTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();

                    if (!recallGRN)
                    {
                        LocationService locationService = new LocationService();
                        Location location = new Location();

                        location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));

                        DataView dvAllReferenceData = new DataView(invGiftVoucherTransferNoteService.GetAllActiveGiftVoucherSerialsDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2), invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text).InvGiftVoucherGroupID, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text).InvGiftVoucherBookCodeID, location.LocationID, location.IsHeadOffice));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtVoucherSerialTo);
                            txtVoucherSerialTo_Leave(this, e);
                        }
                    }
                    else
                    {
                        InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                        InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader = invGiftVoucherPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());
                        DataView dvAllReferenceData = new DataView(invGiftVoucherTransferNoteService.GetAllActiveGiftVoucherMasterRecallGRNVoucherSerialsDataTable(invGiftVoucherPurchaseHeader));
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

        private void txtVoucherNoTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();

                    if (!recallGRN)
                    {
                        LocationService locationService = new LocationService();
                        Location location = new Location();

                        location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));

                        DataView dvAllReferenceData = new DataView(invGiftVoucherTransferNoteService.GetAllActiveGiftVoucherNosDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2), invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text).InvGiftVoucherGroupID, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text).InvGiftVoucherBookCodeID, location.LocationID, location.IsHeadOffice));
                        if (dvAllReferenceData.Count > 0)
                        {
                            LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtVoucherNoTo);
                            txtVoucherNoTo_Leave(this, e);
                        }
                    }
                    else
                    {
                        InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                        InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader = invGiftVoucherPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());
                        DataView dvAllReferenceData = new DataView(invGiftVoucherTransferNoteService.GetAllActiveGiftVoucherMasterRecallGRNVoucherNosDataTable(invGiftVoucherPurchaseHeader));
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

        private void chkAutoCompleationGroup_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadGroups();
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
            // Load Locations            
            LocationService locationService = new LocationService();           

            cmbFromLocation.SelectedValueChanged -= new System.EventHandler(this.cmbFromLocation_SelectedValueChanged);
            Common.LoadLocations(cmbFromLocation, locationService.GetAllLocations());
            cmbFromLocation.SelectedValueChanged += new System.EventHandler(this.cmbFromLocation_SelectedValueChanged);

            cmbFromLocation.SelectedValue = Common.LoggedLocationID;

            Common.SetDataGridviewColumnsReadOnly(true, dgvVoucherBookDetails, dgvVoucherBookDetails.Columns[1], dgvVoucherBookDetails.Columns[2], dgvVoucherBookDetails.Columns[3]);
            Common.ReadOnlyTextBox(true, txtGiftVoucherValue, txtNoOfVouchersOnBook, txtVoucherSerialQty, txtVoucherNoQty);

            Common.EnableTextBox(true, txtDocumentNo, txtRemark, txtReferenceNo, txtGrnNo);
            Common.EnableButton(true, btnDocumentDetails, btnGrnDetails);
            Common.EnableComboBox(true, cmbFromLocation, cmbToLocation);
            Common.EnableComboBox(false, cmbSelectionCriteria);
            Common.EnableButton(false, btnSave, btnPause);
            //if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
            //if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

            // Disable gift voucher selection details controls
            cmbSelectionCriteria.SelectedIndex = -1;
            cmbBasedOn.SelectedIndex = -1;
            //IsCriteriaExistsByIndex(-1);
            dtpTransferDate.Value = DateTime.Now;

            txtDocumentNo.Text = GetDocumentNo(true);

            invGiftVoucherTransferNoteDetailsTemp = null;
            recallGRN = false;
            GrpHeader.Enabled = true;
            grpFooter.Enabled = false;
            groupBox4.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;

            this.ActiveControl = cmbFromLocation;
            cmbFromLocation.Focus();
        }

        public override void FormLoad()
        {
            try
            {
                dgvVoucherBookDetails.AutoGenerateColumns = false;
                
                // Load Locations
                LocationService locationService = new LocationService();
                cmbFromLocation.SelectedValueChanged -= new System.EventHandler(this.cmbFromLocation_SelectedValueChanged);
                Common.LoadLocations(cmbFromLocation, locationService.GetAllLocations());
                cmbFromLocation.SelectedValueChanged += new System.EventHandler(this.cmbFromLocation_SelectedValueChanged);

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
            //Load Transfer Document Numbers
            InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
            Common.SetAutoComplete(txtDocumentNo, invGiftVoucherTransferNoteService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbFromLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

            //Load GRN Document Numbers
            InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
            Common.SetAutoComplete(txtGrnNo, invGiftVoucherPurchaseService.GetAllDocumentNumbersToTransfer(Convert.ToInt32(cmbFromLocation.SelectedValue)), chkAutoCompleationGrnNo.Checked);
        }

        /// <summary>
        /// Pause Gift Voucher Purchase order.
        /// </summary>
        public override void Pause()
        {
            //if (!ValidateControls())
            //{ return; }

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
            //if (!ValidateControls())
            //{ return; }

            if ((Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                if (!IsFromLocationExistsByName(cmbFromLocation.Text.Trim())) { return; }
                if (!IsToLocationExistsByName(cmbToLocation.Text.Trim())) { return; }

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

        public override void ClearForm()
        {
            dgvVoucherBookDetails.DataSource = null;
            dgvVoucherBookDetails.Refresh();
            base.ClearForm();
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
        private void LoadTransferedNoteDocument()
        {
            InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
            InvGiftVoucherTransferNoteHeader invGiftVoucherTransferNoteHeader = new InvGiftVoucherTransferNoteHeader();

            invGiftVoucherTransferNoteHeader = invGiftVoucherTransferNoteService.GetGiftVoucherTransferNoteHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));

            if (invGiftVoucherTransferNoteHeader == null)
            {
                Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            else
            {
                txtDocumentNo.Text = invGiftVoucherTransferNoteHeader.DocumentNo;
                txtRemark.Text = invGiftVoucherTransferNoteHeader.Remark;
                txtReferenceNo.Text = invGiftVoucherTransferNoteHeader.ReferenceNo;
                cmbFromLocation.SelectedValue = invGiftVoucherTransferNoteHeader.LocationID;
                cmbToLocation.SelectedValue = invGiftVoucherTransferNoteHeader.ToLocationID;
                dtpTransferDate.Value = Common.ConvertStringToDate(invGiftVoucherTransferNoteHeader.DocumentDate.ToString());
                txtTotalQty.Text = invGiftVoucherTransferNoteHeader.GiftVoucherQty.ToString();
                if (invGiftVoucherTransferNoteHeader.VoucherType == 1)
                {
                    rdoVoucher.Checked = true;
                }
                else if (invGiftVoucherTransferNoteHeader.VoucherType == 2)
                {
                    rdoCoupon.Checked = true;
                }

                if (!invGiftVoucherTransferNoteHeader.ReferenceDocumentID.Equals(0))
                {
                    InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                    txtGrnNo.Text = invGiftVoucherPurchaseService.GetSavedDocumentDetailsByDocumentID(invGiftVoucherTransferNoteHeader.ReferenceDocumentID).DocumentNo;
                }
                else
                {
                    txtGrnNo.Text = string.Empty;
                }

                dgvVoucherBookDetails.DataSource = null;
                invGiftVoucherTransferNoteDetailsTemp = invGiftVoucherTransferNoteService.GetAllTransferDetail(invGiftVoucherTransferNoteHeader);
                dgvVoucherBookDetails.DataSource = invGiftVoucherTransferNoteDetailsTemp;
                dgvVoucherBookDetails.Refresh();

                Common.EnableTextBox(false, txtDocumentNo, txtGrnNo);
                Common.EnableComboBox(false, cmbFromLocation, cmbToLocation);
                Common.EnableButton(false, btnDocumentDetails, btnGrnDetails);

                if (invGiftVoucherTransferNoteHeader.DocumentStatus.Equals(0))
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
                    GrpHeader.Enabled = false;
                    Common.EnableButton(false, btnPause, btnSave);
                }
            }
        }

        /// <summary>
        /// Load reference document
        /// </summary>
        private void LoadGoodReceivedDocument()
        {
            InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
            InvGiftVoucherPurchaseHeader existingInvGiftVoucherPurchaseHeader = new InvGiftVoucherPurchaseHeader();

            existingInvGiftVoucherPurchaseHeader = invGiftVoucherPurchaseService.GetInvGiftVoucherPurchaseHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherGoodsReceivedNote").DocumentID, txtGrnNo.Text.Trim(), Common.LoggedLocationID);
            if (existingInvGiftVoucherPurchaseHeader == null)
            {
                Toast.Show(this.Text + " - " + txtGrnNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            else
            {
                InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();

                txtDocumentNo.Text = GetDocumentNo(true);
                recallGRN = true;
                txtRemark.Text = existingInvGiftVoucherPurchaseHeader.Remark;
                txtReferenceNo.Text = existingInvGiftVoucherPurchaseHeader.ReferenceNo;
                cmbFromLocation.SelectedValue = existingInvGiftVoucherPurchaseHeader.LocationID;
                dtpTransferDate.Value = Common.FormatDate(DateTime.Now);
                txtTotalQty.Text = existingInvGiftVoucherPurchaseHeader.GiftVoucherQty.ToString();
                txtGrnNo.Text = existingInvGiftVoucherPurchaseHeader.DocumentNo;
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

                dgvVoucherBookDetails.DataSource = null;
                invGiftVoucherTransferNoteDetailsTemp = invGiftVoucherTransferNoteService.GetGiftVoucherPurchaseDetail(existingInvGiftVoucherPurchaseHeader);
                dgvVoucherBookDetails.DataSource = invGiftVoucherTransferNoteDetailsTemp;
                dgvVoucherBookDetails.Refresh();

                Common.EnableTextBox(false, txtDocumentNo, txtGrnNo);
                Common.EnableComboBox(false, cmbFromLocation);
                Common.EnableButton(false, btnDocumentDetails, btnGrnDetails);
                
                grpFooter.Enabled = true;
                EnableLine(true);
                grpBody.Enabled = true;
                dgvVoucherBookDetails.Enabled = true;
                
                if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                
                if(recallGRN)
                {
                    Common.SetAutoComplete(txtBookCode, invGiftVoucherTransferNoteService.GetRecallGRNInvGiftVoucherBookCodesByVoucherType(existingInvGiftVoucherPurchaseHeader), chkAutoCompleationBook.Checked);
                    Common.SetAutoComplete(txtBookName, invGiftVoucherTransferNoteService.GetRecallGRNInvGiftVoucherBookNamesByVoucherType(existingInvGiftVoucherPurchaseHeader), chkAutoCompleationBook.Checked);

                    Common.SetAutoComplete(txtGroupCode, invGiftVoucherTransferNoteService.GetRecallGRNInvGiftVoucherGroupCodes(existingInvGiftVoucherPurchaseHeader), chkAutoCompleationGroup.Checked);
                    Common.SetAutoComplete(txtGroupName, invGiftVoucherTransferNoteService.GetRecallGRNInvGiftVoucherGroupNames(existingInvGiftVoucherPurchaseHeader), chkAutoCompleationGroup.Checked);
                }
                ResetVoucherType();
            }
        }

        /// <summary>
        /// Assign Gift Voucher properties before adding into grid view
        /// </summary>
        private void AssignGiftVoucherProperties()
        {
            List<InvGiftVoucherMaster> invGiftVoucherMastersList = LoadVoucherBookDetails();
            LocationService locationService = new LocationService();
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
            List<InvGiftVoucherTransferNoteDetailTemp> invGiftVoucherTransferNoteDetailTempList = new List<InvGiftVoucherTransferNoteDetailTemp>();
            Location toLocation = new Location();

            toLocation = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbToLocation.SelectedValue.ToString()));
            
            if (invGiftVoucherTransferNoteDetailsTemp == null)
            { 
                invGiftVoucherTransferNoteDetailsTemp = new List<InvGiftVoucherTransferNoteDetailTemp>();
                txtDocumentNo.Text = GetDocumentNo(true);
            }
            else
            {
               // && invGiftVoucherBookCode.PageCount == 0
                //InvGiftVoucherTransferNoteDetailTemp invGiftVoucherTransferNoteDetailTemp = invGiftVoucherTransferNoteDetailsTemp.Find(x => x.InvGiftVoucherMasterID == invGiftVoucherMaster.InvGiftVoucherMasterID);

            }
            long lineNo = 0;
           
            int bookID = invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID;
            
            invGiftVoucherTransferNoteDetailsTemp.RemoveAll(x => x.InvGiftVoucherBookCodeID == bookID);

            invGiftVoucherTransferNoteDetailsTemp.ToList().ForEach(x => x.LineNo = lineNo += 1);

            if (invGiftVoucherTransferNoteDetailsTemp.Count.Equals(0))
            { lineNo = 0; }
            else
            { lineNo = invGiftVoucherTransferNoteDetailsTemp.Max(s => s.LineNo); }

            if (invGiftVoucherTransferNoteDetailsTemp != null)
            {
                foreach (InvGiftVoucherMaster invGiftVoucherMaster in invGiftVoucherMastersList)
                {
                    lineNo += 1;
                    InvGiftVoucherTransferNoteDetailTemp invGiftVoucherTransferNoteDetailTemp = invGiftVoucherTransferNoteDetailsTemp.Find(x => x.InvGiftVoucherMasterID == invGiftVoucherMaster.InvGiftVoucherMasterID);
                    if (invGiftVoucherTransferNoteDetailTemp != null)
                    {
                        var tc = new InvGiftVoucherTransferNoteDetailTemp
                            {
                                InvGiftVoucherTransferNoteDetailID = invGiftVoucherTransferNoteDetailTemp.InvGiftVoucherTransferNoteDetailID,
                                InvGiftVoucherTransferNoteHeaderID = invGiftVoucherTransferNoteDetailTemp.InvGiftVoucherTransferNoteHeaderID,
                                CompanyID = invGiftVoucherTransferNoteDetailTemp.CompanyID,
                                LocationID = invGiftVoucherTransferNoteDetailTemp.LocationID,
                                DocumentNo = txtDocumentNo.Text.Trim(),
                                DocumentID = invGiftVoucherTransferNoteDetailTemp.DocumentID,
                                DocumentDate = Common.GetSystemDateWithTime(),
                                //invGiftVoucherPurchaseOrderDetailtemp.DocumentDate,
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
                                ToLocationID = toLocation.LocationID,
                            };
                        invGiftVoucherTransferNoteDetailTempList.Add(tc);
                    }
                    else
                    {
                        var tc = new InvGiftVoucherTransferNoteDetailTemp
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
                                ToLocationID = toLocation.LocationID,
                            };
                        invGiftVoucherTransferNoteDetailTempList.Add(tc);
                    }
                }


                var deletedInvGiftVoucherPurchaseOrderDetails = CommonService.Except(invGiftVoucherTransferNoteDetailsTemp, invGiftVoucherTransferNoteDetailTempList, paymentDeta => paymentDeta.VoucherSerial);

                invGiftVoucherTransferNoteDetailsTemp.AddRange(invGiftVoucherTransferNoteDetailTempList);

            }
            else if (invGiftVoucherTransferNoteDetailsTemp == null)
            {
                foreach (InvGiftVoucherMaster invGiftVoucherMaster in invGiftVoucherMastersList)
                {
                    lineNo += 1;
                    var tc = new InvGiftVoucherTransferNoteDetailTemp
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
                    invGiftVoucherTransferNoteDetailTempList.Add(tc);                    
                }
                invGiftVoucherTransferNoteDetailsTemp = invGiftVoucherTransferNoteDetailTempList;
            }      

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
            LocationService locationService = new LocationService();
            Location location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));
            if (!recallGRN)
            {
                if (cmbSelectionCriteria.SelectedIndex.Equals(0))
                {
                    if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
                    {
                        invGiftVoucherMastersList = invGiftVoucherMasterService.GetPurchasedGiftVouchersByBookIDWithQty(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtGiftVoucherQty.Text.Trim()), location.LocationID, location.IsHeadOffice);
                    }
                }
                else if (cmbSelectionCriteria.SelectedIndex.Equals(2))
                {
                    if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
                    {
                        invGiftVoucherMastersList = invGiftVoucherMasterService.GetPurchasedGiftVouchersByBookIDWithVoucherNoRange(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherNoFrom.Text.Trim(), txtVoucherNoTo.Text.Trim(), location.LocationID, location.IsHeadOffice);
                    }
                }
                else if (cmbSelectionCriteria.SelectedIndex.Equals(1))
                {
                    if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
                    {
                        invGiftVoucherMastersList = invGiftVoucherMasterService.GetPurchasedGiftVouchersByBookIDWithVoucherSerialRange(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherSerialFrom.Text.Trim(), txtVoucherSerialTo.Text.Trim(), location.LocationID, location.IsHeadOffice);
                    }
                }
            }
            else
            {
                InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                if (cmbSelectionCriteria.SelectedIndex.Equals(0))
                {
                    if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
                    {
                        invGiftVoucherMastersList = invGiftVoucherTransferNoteService.GetGeneratedGRNGiftVouchersByBookIDWithQtyForTransfer(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, invGiftVoucherPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim()).InvGiftVoucherPurchaseHeaderID, location.LocationID, Common.ConvertStringToInt(txtGiftVoucherQty.Text.Trim()));
                    }
                }
                else if (cmbSelectionCriteria.SelectedIndex.Equals(2))
                {
                    if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
                    {
                        invGiftVoucherMastersList = invGiftVoucherTransferNoteService.GetGeneratedGRNGiftVouchersByBookIDWithVoucherNoRangeForTransfer(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, invGiftVoucherPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim()).InvGiftVoucherPurchaseHeaderID, location.LocationID, txtVoucherNoFrom.Text.Trim(), txtVoucherNoTo.Text.Trim());
                    }
                }
                else if (cmbSelectionCriteria.SelectedIndex.Equals(1))
                {
                    if (invGiftVoucherMasterService.GetInvGiftVoucherMasterByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID) != null)
                    {
                        invGiftVoucherMastersList = invGiftVoucherTransferNoteService.GetGeneratedGRNGiftVouchersByBookIDWithVoucherSerialRangeForTransfer(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, invGiftVoucherPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim()).InvGiftVoucherPurchaseHeaderID, location.LocationID, txtVoucherSerialFrom.Text.Trim(), txtVoucherSerialTo.Text.Trim());
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

            invGiftVoucherTransferNoteDetailsTemp = invGiftVoucherTransferNoteDetailsTemp.OrderBy(pr => pr.LineNo).ToList();
            dgvVoucherBookDetails.DataSource = invGiftVoucherTransferNoteDetailsTemp.OrderBy(pr => pr.LineNo).ToList();

            GetSummarizeFigures(invGiftVoucherTransferNoteDetailsTemp);
            if (dgvVoucherBookDetails.Rows.Count > 0)
            {
                Common.EnableComboBox(false, cmbFromLocation, cmbToLocation);
                //if (loadSupplierProducts)
                //{ Common.EnableTextBox(false, txtSupplierCode, txtSupplierName); }
                //else
                //{ Common.EnableTextBox(true, txtSupplierCode, txtSupplierName); }

                dgvVoucherBookDetails.FirstDisplayedScrollingRowIndex = dgvVoucherBookDetails.RowCount - 1;
            }
            else
            {
                Common.EnableComboBox(true, cmbFromLocation, cmbToLocation);
            }

            //EnableLine(false);
            EnableReloadLine(false);
            Common.EnableTextBox(false, txtDocumentNo, txtGrnNo);
            Common.EnableComboBox(false, cmbFromLocation, cmbToLocation);
            if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
            if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
            Common.EnableButton(false, btnDocumentDetails, btnGrnDetails);
            //groupBox4.Enabled = false;

            if (invGiftVoucherTransferNoteDetailsTemp.Count > 0)
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

        private void EnableReloadLine(bool enable)
        {
            //Common.EnableTextBox(enable, txtVoucherValue, txtQty);
            //Common.EnableComboBox(enable, cmbSelectionCriteria);

            //groupBox1.Enabled = enable;

            groupBox2.Enabled = enable;
            groupBox3.Enabled = enable;

            rdoVoucher.Enabled = enable;
            rdoCoupon.Enabled = enable;
            btnLoad.Enabled = enable;
            cmbSelectionCriteria.Enabled = enable;
        }

        private void ClearLine()
        {
            //Common.ClearTextBox(txtVoucherValue, txtQty);
            Common.ClearTextBox(txtBookCode, txtBookName, txtGroupCode, txtGroupName, txtNoOfVouchersOnBook, txtGiftVoucherQty, txtVoucherNoFrom, txtVoucherNoTo, txtVoucherSerialFrom, txtVoucherSerialTo);
            Common.ClearComboBox(cmbSelectionCriteria);
        }

        private void GetSummarizeFigures(List<InvGiftVoucherTransferNoteDetailTemp> listItem)
        {
            int qtyTotalCount = 0;

            qtyTotalCount = listItem.GetTotalCount();

            qtyTotalCount = Common.ConvertStringToInt(qtyTotalCount.ToString());

            txtTotalQty.Text = qtyTotalCount.ToString();

            CalculateFigures();
        }

        private void CalculateFigures()
        {
            decimal grossAmount = 0;
            //grossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.Trim());

            ////Get Discount Amount
            //bool isSubDiscount = chkSubTotalDiscountPercentage.Checked;
            //decimal subDiscount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());
            //decimal discountAmount = Common.ConvertStringToDecimalCurrency(Common.GetDiscountAmount(isSubDiscount, grossAmount, subDiscount).ToString());

            ////Get Net Amount
            //decimal netAmount = Common.ConvertStringToDecimalCurrency((Common.GetTotalAmount(grossAmount) - Common.GetTotalAmount(discountAmount)).ToString());

            ////Assign calculated values
            //txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(grossAmount);
            //txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(discountAmount);
            //txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(netAmount);
        }

        // Confirm Locations by ID
        private bool IsFromLocationExistsByID(int locationID)
        {
            bool recodFound = false;
            LocationService locationService = new LocationService();

            if (locationService.GetLocationsByID(locationID) != null)
            { recodFound = true; }
            else
            {
                recodFound = false;
                Toast.Show(cmbFromLocation.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

        // Confirm Locations by ID
        private bool IsToLocationExistsByID(int locationID)
        {
            bool recodFound = false;
            LocationService locationService = new LocationService();

            if (locationService.GetLocationsByID(locationID) != null)
            { recodFound = true; }
            else
            {
                recodFound = false;
                Toast.Show(cmbToLocation.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

        // Confirm Location by Name
        private bool IsFromLocationExistsByName(string locationName)
        {
            bool recodFound = false;
            LocationService locationService = new LocationService();
            Location location = new Location();
            location = locationService.GetLocationsByName(locationName);
            if (location != null)
            {
                recodFound = true;
                cmbFromLocation.Text = location.LocationName;
            }
            else
            {
                recodFound = false;
                Common.ClearComboBox(cmbFromLocation);
                Toast.Show(cmbFromLocation.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

        // Confirm Location by Name
        private bool IsToLocationExistsByName(string locationName)
        {
            bool recodFound = false;
            LocationService locationService = new LocationService();
            Location location = new Location();
            location = locationService.GetLocationsByName(locationName);
            if (location != null)
            {
                recodFound = true;
                cmbToLocation.Text = location.LocationName;
            }
            else
            {
                recodFound = false;
                Common.ClearComboBox(cmbToLocation);
                Toast.Show(cmbToLocation.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

        // Confirm Book by BookCode
        private bool IsBookCodeExistsByCode(string bookCode)
        {
            bool recodFound = false;
            InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
            InvGiftVoucherBookCode invGiftVoucherBookCode = new InvGiftVoucherBookCode();
            LocationService locationService = new LocationService();
            Location location = new Location();

            location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));

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

                if (invGiftVoucherTransferNoteDetailsTemp != null && invGiftVoucherTransferNoteDetailsTemp.Count > 0)
                {
                    InvGiftVoucherBookCode invGiftVoucherBookCodeTemp = new InvGiftVoucherBookCode();
                    InvGiftVoucherTransferNoteDetailTemp invGiftVoucherTransferNoteDetailTemp = invGiftVoucherTransferNoteDetailsTemp.Select(x => x).FirstOrDefault();

                    invGiftVoucherBookCodeTemp = invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByID(invGiftVoucherTransferNoteDetailTemp.InvGiftVoucherBookCodeID);
                    bool validBookType = false;

                    if (invGiftVoucherBookCodeTemp!= null && invGiftVoucherBookCodeTemp.PageCount != 0)
                    {
                        validBookType = true;
                    }
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

                if (recallGRN)
                {
                    InvGiftVoucherPurchaseHeader existingInvGiftVoucherPurchaseHeader = new InvGiftVoucherPurchaseHeader();
                    InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();

                    existingInvGiftVoucherPurchaseHeader = invGiftVoucherPurchaseService.GetInvGiftVoucherPurchaseHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherGoodsReceivedNote").DocumentID, txtGrnNo.Text.Trim(), Common.LoggedLocationID);
                    Common.SetAutoComplete(txtVoucherSerialFrom, invGiftVoucherTransferNoteService.GetVoucherSerialByRecallGRN(existingInvGiftVoucherPurchaseHeader, invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                    Common.SetAutoComplete(txtVoucherSerialTo, invGiftVoucherTransferNoteService.GetVoucherSerialByRecallGRN(existingInvGiftVoucherPurchaseHeader, invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);

                    Common.SetAutoComplete(txtVoucherNoFrom, invGiftVoucherTransferNoteService.GetVoucherNoByRecallGRN(existingInvGiftVoucherPurchaseHeader, invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                    Common.SetAutoComplete(txtVoucherNoTo, invGiftVoucherTransferNoteService.GetVoucherNoByRecallGRN(existingInvGiftVoucherPurchaseHeader, invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                }
                else
                {
                    Common.SetAutoComplete(txtVoucherSerialFrom, invGiftVoucherTransferNoteService.GetVoucherSerialByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID, location.LocationID, location.IsHeadOffice), true);
                    Common.SetAutoComplete(txtVoucherSerialTo, invGiftVoucherTransferNoteService.GetVoucherSerialByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID, location.LocationID, location.IsHeadOffice), true);

                    Common.SetAutoComplete(txtVoucherNoFrom, invGiftVoucherTransferNoteService.GetVoucherNoByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID, location.LocationID, location.IsHeadOffice), true);
                    Common.SetAutoComplete(txtVoucherNoTo, invGiftVoucherTransferNoteService.GetVoucherNoByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID, location.LocationID, location.IsHeadOffice), true);

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

        private void LoadSelectionCriteria()
        {
            Common.SetAutoBindRecords(cmbSelectionCriteria, Common.GetGiftVoucherSelectionCriteria(cmbBasedOn.SelectedIndex));
            cmbSelectionCriteria.SelectedIndex = -1;
        }

        // Confirm Book by BookName
        private bool IsBookCodeExistsByName(string bookName)
        {
            bool recodFound = false;
            InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new Service.InvGiftVoucherTransferNoteService();
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
            InvGiftVoucherBookCode invGiftVoucherBookCode = new InvGiftVoucherBookCode();
            LocationService locationService = new LocationService();
            Location location = new Location();

            location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));

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

                if (recallGRN)
                {
                    InvGiftVoucherPurchaseHeader existingInvGiftVoucherPurchaseHeader = new InvGiftVoucherPurchaseHeader();
                    InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();

                    existingInvGiftVoucherPurchaseHeader = invGiftVoucherPurchaseService.GetInvGiftVoucherPurchaseHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherGoodsReceivedNote").DocumentID, txtGrnNo.Text.Trim(), Common.LoggedLocationID);
                    Common.SetAutoComplete(txtVoucherSerialFrom, invGiftVoucherTransferNoteService.GetVoucherSerialByRecallGRN(existingInvGiftVoucherPurchaseHeader, invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                    Common.SetAutoComplete(txtVoucherSerialTo, invGiftVoucherTransferNoteService.GetVoucherSerialByRecallGRN(existingInvGiftVoucherPurchaseHeader, invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);

                    Common.SetAutoComplete(txtVoucherNoFrom, invGiftVoucherTransferNoteService.GetVoucherNoByRecallGRN(existingInvGiftVoucherPurchaseHeader, invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                    Common.SetAutoComplete(txtVoucherNoTo, invGiftVoucherTransferNoteService.GetVoucherNoByRecallGRN(existingInvGiftVoucherPurchaseHeader, invGiftVoucherBookCode.InvGiftVoucherBookCodeID), true);
                }
                else
                {
                    Common.SetAutoComplete(txtVoucherSerialFrom, invGiftVoucherTransferNoteService.GetVoucherSerialByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID, location.LocationID, location.IsHeadOffice), true);
                    Common.SetAutoComplete(txtVoucherSerialTo, invGiftVoucherTransferNoteService.GetVoucherSerialByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID, location.LocationID, location.IsHeadOffice), true);

                    Common.SetAutoComplete(txtVoucherNoFrom, invGiftVoucherTransferNoteService.GetVoucherNoByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID, location.LocationID, location.IsHeadOffice), true);
                    Common.SetAutoComplete(txtVoucherNoTo, invGiftVoucherTransferNoteService.GetVoucherNoByBookID(invGiftVoucherBookCode.InvGiftVoucherBookCodeID, location.LocationID, location.IsHeadOffice), true);
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
            //Common.EnableTextBox(true, txtVoucherNo, txtBookName, txtGroupCode, txtGroupName);
            Common.ClearTextBox(txtGiftVoucherQty, txtVoucherNoFrom, txtVoucherNoTo, txtVoucherSerialFrom, txtVoucherSerialTo, txtVoucherNoQty, txtVoucherSerialQty);
            if (criteriaIndex == -1)
            {
                //groupBox1.Enabled = false;
                //Common.EnableTextBox(false, txtVoucherNo, txtBookName, txtGroupCode, txtGroupName, txtGiftVoucherValue, txtNoOfVouchersOnBook);
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

        public void LoadLoacationsExceptingCurrentLocation()
        {
            LocationService locationService = new LocationService();
            Location location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));
            if (location.IsHeadOffice)
            {Common.LoadLocations(cmbToLocation, locationService.GetLocationsExceptingCurrentLocation(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString())));}
            else
            { Common.LoadLocations(cmbToLocation, locationService.GetHeadOfficeLocationsExceptingCurrentHOLocation(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()))); }
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
            try
            {
                GetSummarizeFigures(invGiftVoucherTransferNoteDetailsTemp);

                InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                InvGiftVoucherTransferNoteHeader invGiftVoucherTransferNoteHeader = new InvGiftVoucherTransferNoteHeader();
                LocationService locationService = new LocationService();
                Location location = new Location();

                location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));

                invGiftVoucherTransferNoteHeader = invGiftVoucherTransferNoteService.GetGiftVoucherTransferNoteHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), location.LocationID);
                if (invGiftVoucherTransferNoteHeader == null)
                {invGiftVoucherTransferNoteHeader = new InvGiftVoucherTransferNoteHeader();}
                
                invGiftVoucherTransferNoteHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                invGiftVoucherTransferNoteHeader.CompanyID = location.CompanyID;
                invGiftVoucherTransferNoteHeader.LocationID = location.LocationID;
                invGiftVoucherTransferNoteHeader.CostCentreID = location.CostCentreID;
                invGiftVoucherTransferNoteHeader.DocumentID = documentID;
                invGiftVoucherTransferNoteHeader.DocumentNo = documentNo.Trim();
                invGiftVoucherTransferNoteHeader.DocumentDate = Common.ConvertStringToDate(dtpTransferDate.Value.ToString());
                invGiftVoucherTransferNoteHeader.ToLocationID = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbToLocation.SelectedValue.ToString())).LocationID;
                invGiftVoucherTransferNoteHeader.GiftVoucherQty = Common.ConvertStringToInt(txtTotalQty.Text.ToString());
                invGiftVoucherTransferNoteHeader.GiftVoucherAmount = Common.ConvertStringToDecimalCurrency(txtGiftVoucherValue.Text.Trim());
                invGiftVoucherTransferNoteHeader.GiftVoucherPercentage = Common.ConvertStringToDecimalCurrency(txtPercentageOfCoupon.Text.Trim());

                if (!txtGrnNo.Text.Trim().Equals(string.Empty))
                {
                    InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                    InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader = new InvGiftVoucherPurchaseHeader();

                    invGiftVoucherPurchaseHeader = invGiftVoucherPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());
                    invGiftVoucherTransferNoteHeader.ReferenceDocumentDocumentID = invGiftVoucherPurchaseHeader.DocumentID;
                    invGiftVoucherTransferNoteHeader.ReferenceDocumentID = invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID;
                }
                invGiftVoucherTransferNoteHeader.ReferenceNo = txtReferenceNo.Text.Trim();
                invGiftVoucherTransferNoteHeader.Remark = txtRemark.Text.Trim();
                invGiftVoucherTransferNoteHeader.DocumentStatus = documentStatus;
                invGiftVoucherTransferNoteHeader.CreatedUser = Common.LoggedUser;
                invGiftVoucherTransferNoteHeader.CreatedDate = Common.GetSystemDateWithTime();
                invGiftVoucherTransferNoteHeader.ModifiedUser = Common.LoggedUser;
                invGiftVoucherTransferNoteHeader.ModifiedDate = Common.GetSystemDateWithTime();

                return invGiftVoucherTransferNoteService.SaveGiftVoucherTransfer(invGiftVoucherTransferNoteHeader, invGiftVoucherTransferNoteDetailsTemp, out newDocumentNo, this.Name);
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
                InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                LocationService locationService = new LocationService();
                return invGiftVoucherTransferNoteService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
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
                InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();

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

                if (!recallGRN)
                {
                    LoadBookByVoucherType();

                    Common.SetAutoComplete(txtGroupCode, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupCodes(), chkAutoCompleationGroup.Checked);
                    Common.SetAutoComplete(txtGroupName, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupNames(), chkAutoCompleationGroup.Checked);
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
            InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();

            LocationService locationService = new LocationService();
            Location location = new Location();

            location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));

            long qty = 0;
            if (!recallGRN)
            {
                if (cmbSelectionCriteria.SelectedIndex == 1)
                {
                    qty = invGiftVoucherTransferNoteService.GetVoucherQtyByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherSerialFrom.Text, txtVoucherSerialTo.Text, cmbBasedOn.SelectedIndex, location.IsHeadOffice);
                }
                else if (cmbSelectionCriteria.SelectedIndex == 2)
                {
                    qty = invGiftVoucherTransferNoteService.GetVoucherQtyByBookID(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherNoFrom.Text, txtVoucherNoTo.Text, cmbBasedOn.SelectedIndex, location.IsHeadOffice);
                }
            }
            else
            {
                InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                InvGiftVoucherPurchaseHeader existingInvGiftVoucherPurchaseHeaderTemp = invGiftVoucherPurchaseService.GetInvGiftVoucherPurchaseHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherGoodsReceivedNote").DocumentID, txtGrnNo.Text.Trim(), Common.LoggedLocationID);
                
                if (cmbSelectionCriteria.SelectedIndex == 1)
                {
                    qty = invGiftVoucherTransferNoteService.GetGRNVoucherQtyByBookID(existingInvGiftVoucherPurchaseHeaderTemp, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherSerialFrom.Text, txtVoucherSerialTo.Text, cmbBasedOn.SelectedIndex, location.IsHeadOffice);
                }
                else if (cmbSelectionCriteria.SelectedIndex == 2)
                {
                    qty = invGiftVoucherTransferNoteService.GetGRNVoucherQtyByBookID(existingInvGiftVoucherPurchaseHeaderTemp, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherNoFrom.Text, txtVoucherNoTo.Text, cmbBasedOn.SelectedIndex, location.IsHeadOffice);
                }
            }
            return qty;
        }

        private void LoadBookByVoucherType()
        {
            if (!recallGRN)
            {
                InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterGroupValueService = new InvGiftVoucherBookCodeGenerationService();
                Common.SetAutoComplete(txtBookCode, invGiftVoucherMasterGroupValueService.GetInvGiftVoucherBookGenerationCodesByVoucherType((rdoVoucher.Checked ? 1 : 2)), chkAutoCompleationBook.Checked);
                Common.SetAutoComplete(txtBookName, invGiftVoucherMasterGroupValueService.GetInvGiftVoucherBookGenerationNamesByVoucherType((rdoVoucher.Checked ? 1 : 2)), chkAutoCompleationBook.Checked);
            }
            else
            {
                InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                InvGiftVoucherPurchaseHeader existingInvGiftVoucherPurchaseHeaderTemp = invGiftVoucherPurchaseService.GetInvGiftVoucherPurchaseHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherGoodsReceivedNote").DocumentID, txtGrnNo.Text.Trim(), Common.LoggedLocationID);

                Common.SetAutoComplete(txtBookCode, invGiftVoucherTransferNoteService.GetRecallGRNInvGiftVoucherBookCodesByVoucherType(existingInvGiftVoucherPurchaseHeaderTemp), chkAutoCompleationBook.Checked);
                Common.SetAutoComplete(txtBookName, invGiftVoucherTransferNoteService.GetRecallGRNInvGiftVoucherBookNamesByVoucherType(existingInvGiftVoucherPurchaseHeaderTemp), chkAutoCompleationBook.Checked);
            }
        }

        private void LoadGroups()
        {
            if (!recallGRN)
            {
                InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                Common.SetAutoComplete(txtGroupCode, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupCodes(), chkAutoCompleationGroup.Checked);
                Common.SetAutoComplete(txtGroupName, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupNames(), chkAutoCompleationGroup.Checked);
            }
            else
            {
                InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();

                InvGiftVoucherPurchaseHeader existingInvGiftVoucherPurchaseHeaderTemp = invGiftVoucherPurchaseService.GetInvGiftVoucherPurchaseHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherGoodsReceivedNote").DocumentID, txtGrnNo.Text.Trim(), Common.LoggedLocationID);
                Common.SetAutoComplete(txtGroupCode, invGiftVoucherTransferNoteService.GetRecallGRNInvGiftVoucherGroupCodes(existingInvGiftVoucherPurchaseHeaderTemp), chkAutoCompleationGroup.Checked);
                Common.SetAutoComplete(txtGroupName, invGiftVoucherTransferNoteService.GetRecallGRNInvGiftVoucherGroupNames(existingInvGiftVoucherPurchaseHeaderTemp), chkAutoCompleationGroup.Checked);
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

            if (!Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbFromLocation, cmbToLocation))
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
            InvGiftVoucherTransferNoteValidator invGiftVoucherTransferNoteValidator = new InvGiftVoucherTransferNoteValidator();
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();

            LocationService locationService = new LocationService();
            Location location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()));

            InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
            InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader = invGiftVoucherPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());

            bool isValidated = false;
            if (!recallGRN && cmbSelectionCriteria.SelectedIndex == 0 && !invGiftVoucherTransferNoteValidator.ValidateVoucherQty(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtGiftVoucherQty.Text.Trim()), cmbBasedOn.SelectedIndex, location.IsHeadOffice))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(lblGiftVoucherQty.Text.ToString()), " - ", txtGiftVoucherQty.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            if (recallGRN && cmbSelectionCriteria.SelectedIndex == 0 && !invGiftVoucherTransferNoteValidator.ValidatePurchaseVoucherQty(invGiftVoucherPurchaseHeader, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, Common.ConvertStringToInt(txtGiftVoucherQty.Text.Trim()), cmbBasedOn.SelectedIndex, location.IsHeadOffice))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(lblGiftVoucherQty.Text.ToString()), " - ", txtGiftVoucherQty.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else if (cmbSelectionCriteria.SelectedIndex == 2 && cmbBasedOn.SelectedIndex == 0 && !invGiftVoucherTransferNoteValidator.ValidateVoucherNoFromTo(txtVoucherNoFrom.Text.Trim(), txtVoucherNoTo.Text.Trim(), location.IsHeadOffice))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherNo.Text.ToString()), " - ", txtVoucherNoFrom.Text.Trim(), " - ", txtVoucherNoTo.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else if (!recallGRN && cmbSelectionCriteria.SelectedIndex == 2 && cmbBasedOn.SelectedIndex == 0 && !invGiftVoucherTransferNoteValidator.ValidateVoucherNoRange(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherNoFrom.Text.Trim(), txtVoucherNoTo.Text.Trim(), location.IsHeadOffice))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherNo.Text.ToString()), " - ", txtVoucherNoFrom.Text.Trim(), " - ", txtVoucherNoTo.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else if (recallGRN && cmbSelectionCriteria.SelectedIndex == 2 && cmbBasedOn.SelectedIndex == 0 && !invGiftVoucherTransferNoteValidator.ValidatePurchaseVoucherNoRange(invGiftVoucherPurchaseHeader, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherNoFrom.Text.Trim(), txtVoucherNoTo.Text.Trim(), location.IsHeadOffice))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherNo.Text.ToString()), " - ", txtVoucherNoFrom.Text.Trim(), " - ", txtVoucherNoTo.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else if (cmbSelectionCriteria.SelectedIndex == 1 && !invGiftVoucherTransferNoteValidator.ValidateVoucherSerialFromTo(txtVoucherSerialFrom.Text.Trim(), txtVoucherSerialTo.Text.Trim(), location.IsHeadOffice))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherSerial.Text.ToString()), " - ", txtVoucherSerialFrom.Text.Trim(), " - ", txtVoucherSerialTo.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else if (!recallGRN && cmbSelectionCriteria.SelectedIndex == 1 && !invGiftVoucherTransferNoteValidator.ValidateVoucherSerialRange(invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherSerialFrom.Text.Trim(), txtVoucherSerialTo.Text.Trim(), location.IsHeadOffice))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherSerial.Text.ToString()), " - ", txtVoucherSerialFrom.Text.Trim(), " - ", txtVoucherSerialTo.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else if (recallGRN && cmbSelectionCriteria.SelectedIndex == 1 && !invGiftVoucherTransferNoteValidator.ValidatePurchaseVoucherSerialRange(invGiftVoucherPurchaseHeader, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, txtVoucherSerialFrom.Text.Trim(), txtVoucherSerialTo.Text.Trim(), location.IsHeadOffice))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(tbpVoucherSerial.Text.ToString()), " - ", txtVoucherSerialFrom.Text.Trim(), " - ", txtVoucherSerialTo.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else
            { isValidated = true; }

            return isValidated;
        }

        #endregion

        private void rdoVoucher_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ResetVoucherType();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }
        
        #endregion

        


    }
}
