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
using Service;
using Utility;

namespace UI.Windows
{
    public partial class FrmPettyCashPaymentUpdate : UI.Windows.FrmBaseTransactionForm
    {
        List<AccPettyCashPaymentProcessDetail> accPettyCashPaymentProcessDetailList = new List<AccPettyCashPaymentProcessDetail>();
        List<AccPettyCashPaymentHeader> accPettyCashPaymentHeaderList = new List<AccPettyCashPaymentHeader>();
        private AccLedgerAccount existingAccLedgerAccount;
        int documentID = 6;

        public FrmPettyCashPaymentUpdate()
        {
            InitializeComponent();
        }

        #region Form Events
        private void btnDocumentDetails_Click(object sender, EventArgs e)
        {
            try
            {
                AccPettyCashPaymentProcessService accPettyCashPaymentProcessService = new AccPettyCashPaymentProcessService();
                DataView dvAllReferenceData = new DataView(accPettyCashPaymentProcessService.GetPendingPettyCashPaymentUpdateDocuments());
                if (dvAllReferenceData.Count != 0)
                {
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), "Pending Documents", string.Empty, txtDocumentNo);
                    if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                    { LoadPettyCashPaymentUpdateDocument(); }
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
                    //if (!txtPurchaseOrderNo.Text.Trim().Equals(string.Empty))
                    //    RecallDocument(txtPurchaseOrderNo.Text.Trim());
                    //else
                    //{
                    //    txtPurchaseOrderNo.Text = GetDocumentNo(true);
                    //    txtSupplierCode.Focus();
                    //}
                    txtDocumentNo_Validated(this, e);
                }


            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtDocumentNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDocumentNo.Text.Trim()))
                {
                    txtDocumentNo.Text = GetDocumentNo(true);
                    cmbLocation.Focus();
                }
                else
                {
                    AccPettyCashPaymentProcessService accPettyCashPaymentProcessDetailService = new AccPettyCashPaymentProcessService();
                    AccPettyCashPaymentProcessHeader accPettyCashPaymentProcessHeader = new AccPettyCashPaymentProcessHeader();

                    accPettyCashPaymentProcessHeader = accPettyCashPaymentProcessDetailService.GetAccPettyCashPaymentProcessHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                    if (accPettyCashPaymentProcessHeader == null)
                    {
                        //Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        return;
                    }
                    else
                    {
                        LoadPettyCashPaymentUpdateDocument();
                    }
                }
                
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtPettyCashBookCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    LocationService locationService = new LocationService();

                    DataView dvPettyCash = new DataView(accLedgerAccountService.GetAllActivePettyCashAccountsDataTable(locationService.GetHeadOfficeLocationsByName(cmbLocation.Text.Trim()).LocationID));
                    if (dvPettyCash.Count != 0)
                    {
                        LoadReferenceSearchForm(dvPettyCash, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtPettyCashBookCode);
                        txtPettyCashBookCode_Validated(this, e);
                    }
                }
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (txtPettyCashBookCode.Text != string.Empty)
                    {
                        //Common.SetFocus(e, cmbLocation); 
                        txtPettyCashBookCode_Validated(this, e);
                    }
                    else
                    { Common.SetFocus(e, txtPettyCashBookName); }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtPettyCashBookCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPettyCashBookCode.Text.Trim()))
                { return; }
                else
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByCode(txtPettyCashBookCode.Text.Trim());
                    if (existingAccLedgerAccount != null)
                    {
                        txtPettyCashBookCode.Text = existingAccLedgerAccount.LedgerCode;
                        txtPettyCashBookName.Text = existingAccLedgerAccount.LedgerName;
                        LoadPettyPaymentDetail();
                    }
                    else
                    {
                        Toast.Show(Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + txtPettyCashBookCode.Text.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                        txtPettyCashBookCode.Text = string.Empty;
                        txtPettyCashBookName.Text = string.Empty;
                        txtPettyCashBookCode.Focus();
                        return;
                    }
                }

            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtPettyCashBookName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    LocationService locationService = new LocationService();
                    DataView dvPettyCash = new DataView(accLedgerAccountService.GetAllActivePettyCashAccountsDataTable(locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID));
                    if (dvPettyCash.Count != 0)
                    {
                        LoadReferenceSearchForm(dvPettyCash, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtPettyCashBookName);
                        txtPettyCashBookName_Leave(this, e);
                    }
                }
                if (txtPettyCashBookName.Text != string.Empty)
                {
                    Common.SetFocus(e, txtPettyCashBookCode);
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtPettyCashBookName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPettyCashBookName.Text == string.Empty)
                {
                    return;
                }
                else
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByName(txtPettyCashBookName.Text.Trim());
                    if (existingAccLedgerAccount != null)
                    {
                        txtPettyCashBookCode.Text = existingAccLedgerAccount.LedgerCode;
                        txtPettyCashBookName.Text = existingAccLedgerAccount.LedgerName;
                        txtPettyCashBookCode.Focus();
                    }
                    else
                    {
                        Toast.Show(Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + txtPettyCashBookName.Text.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                        txtPettyCashBookCode.Text = string.Empty;
                        txtPettyCashBookName.Text = string.Empty;
                        txtPettyCashBookName.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dgvPaymentDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (int.Equals(e.ColumnIndex, 0))
                {
                    if (dgvPaymentDetail.CurrentCell != null && dgvPaymentDetail.CurrentCell.RowIndex >= 0)
                    {
                        dgvPaymentDetail.CurrentCell = dgvPaymentDetail.Rows[dgvPaymentDetail.CurrentRow.Index].Cells["Status"];

                        DataGridViewCheckBoxCell chSts = new DataGridViewCheckBoxCell();
                        chSts = (DataGridViewCheckBoxCell)dgvPaymentDetail.Rows[dgvPaymentDetail.CurrentRow.Index].Cells["Status"];

                        if (chSts.Value == null)
                            chSts.Value = false;

                        switch (chSts.Value.ToString())
                        {
                            case "True":
                                accPettyCashPaymentHeaderList[dgvPaymentDetail.CurrentRow.Index].Status = false;
                                break;
                            case "False":
                                accPettyCashPaymentHeaderList[dgvPaymentDetail.CurrentRow.Index].Status = true;
                                break;
                        }

                        if (dgvPaymentDetail["AccPettyCashPaymentHeaderID", dgvPaymentDetail.CurrentCell.RowIndex].Value != null)
                        {
                            GetSummarizeFigures(accPettyCashPaymentHeaderList.Where(v => v.PaymentStatus == 2 && v.Status == true).ToList());
                        }
                    }

                    if (accPettyCashPaymentHeaderList.Where(v => v.PaymentStatus == 2 && v.Status == true).Any())
                    {
                        Common.EnableTextBox(false, txtPettyCashBookCode, txtPettyCashBookName);
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtPettyCashBookCode); }
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
                    // Read Petty Cash Books
                    AccPettyCashMasterService accPettyCashMasterService = new AccPettyCashMasterService();
                    LocationService locationService = new LocationService();

                    Common.SetAutoComplete(txtPettyCashBookCode, accPettyCashMasterService.GetPettyCashLedgerCodesByLocationID(locationService.GetLocationsByName(cmbLocation.Text).LocationID), chkAutoCompleationPettyCash.Checked);
                    Common.SetAutoComplete(txtPettyCashBookName, accPettyCashMasterService.GetPettyCashLedgerNamesByLocationID(locationService.GetLocationsByName(cmbLocation.Text).LocationID), chkAutoCompleationPettyCash.Checked);
                    //LoadPettyCashBook();
                }
            }
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
        
        #endregion

        #region Methods

        /// <summary>
        /// Setup GUI contols (Set visibility, data binding, auto loading)
        /// </summary>
        public override void InitializeForm()
        {
            try
            {
                // Load Locations            
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
                cmbLocation.SelectedValue = Common.LoggedLocationID;

                // Read Petty Cash Books
                AccPettyCashMasterService accPettyCashMasterService = new AccPettyCashMasterService();
                Common.SetAutoComplete(txtPettyCashBookCode, accPettyCashMasterService.GetPettyCashLedgerCodesByLocationID(locationService.GetLocationsByName(cmbLocation.Text).LocationID), chkAutoCompleationPettyCash.Checked);
                Common.SetAutoComplete(txtPettyCashBookName, accPettyCashMasterService.GetPettyCashLedgerNamesByLocationID(locationService.GetLocationsByName(cmbLocation.Text).LocationID), chkAutoCompleationPettyCash.Checked);

                Common.SetDataGridviewColumnsReadOnly(true, dgvPaymentDetail, dgvPaymentDetail.Columns[1], dgvPaymentDetail.Columns[2], dgvPaymentDetail.Columns[3], dgvPaymentDetail.Columns[4]);

                Common.EnableTextBox(true, txtDocumentNo, txtPettyCashBookCode, txtPettyCashBookName);
                Common.EnableButton(true, btnDocumentDetails);
                Common.EnableButton(false, btnSave);

                dtpDocumentDate.Value = DateTime.Now;

                txtDocumentNo.Text = GetDocumentNo(true);

                existingAccLedgerAccount = null;

                accPettyCashPaymentProcessDetailList = null;
                accPettyCashPaymentHeaderList = null;

                grpBody.Enabled = false;
                grpFooter.Enabled = false;

                this.ActiveControl = txtPettyCashBookCode;
                txtPettyCashBookCode.Focus();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        public override void FormLoad()
        {
            try
            {
                dgvPaymentDetail.AutoGenerateColumns = false;
                btnPause.Visible = false;

                // Read Petty Cash Books
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                Common.SetAutoComplete(txtPettyCashBookCode, accLedgerAccountService.GetPettyCashLedgerCodes(), chkAutoCompleationPettyCash.Checked);
                Common.SetAutoComplete(txtPettyCashBookName, accLedgerAccountService.GetPettyCashLedgerNames(), chkAutoCompleationPettyCash.Checked);

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                this.Text = autoGenerateInfo.FormText;

                documentID = autoGenerateInfo.DocumentID;

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
            AccPettyCashPaymentProcessService accPettyCashPaymentProcessService = new AccPettyCashPaymentProcessService();
            Common.SetAutoComplete(txtDocumentNo, accPettyCashPaymentProcessService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
        }

        /// <summary>
        /// Save New Petty Cash Payment Process.
        /// </summary>
        public override void Save()
        {
            if (accPettyCashPaymentHeaderList.Where(v => v.Status == true).Count() > 0)
            {
                if (!ValidateControls()) { return; }
                if (!IsValidateExistsPettyCashLedgerByLocationID()) { return; }

                if ((Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
                {
                    if (!IsLocationExistsByName(cmbLocation.Text.Trim()))
                    {
                        return;
                    }
                    string NewDocumentNo;
                    bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                    if (saveDocument.Equals(true))
                    {
                        Toast.Show(this.Text + " - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                        ClearForm();
                        RefreshDocumentNumbers();
                    }
                    else
                    {
                        Toast.Show(this.Text + " - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                        return;
                    }
                }
            }
        }

        public override void ClearForm()
        {
            dgvPaymentDetail.DataSource = null;
            dgvPaymentDetail.Refresh();
            base.ClearForm();
        }

        private string GetDocumentNo(bool isTemporytNo)
        {
            try
            {
                AccPettyCashPaymentProcessService accPettyCashPaymentProcessDetailService = new AccPettyCashPaymentProcessService();
                LocationService locationService = new LocationService();
                return accPettyCashPaymentProcessDetailService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
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
            try
            {
                GetSummarizeFigures(accPettyCashPaymentHeaderList.Where(v => v.PaymentStatus == 2 && v.Status == true).ToList());

                AccPettyCashPaymentProcessService accPettyCashPaymentProcessService = new AccPettyCashPaymentProcessService();
                AccPettyCashPaymentProcessHeader accPettyCashPaymentProcessHeader = new AccPettyCashPaymentProcessHeader();
                LocationService locationService = new LocationService();
                Location location = new Location();

                location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                accPettyCashPaymentProcessHeader = accPettyCashPaymentProcessService.GetAccPettyCashPaymentProcessHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), location.LocationID);
                if (accPettyCashPaymentProcessHeader == null)
                { accPettyCashPaymentProcessHeader = new AccPettyCashPaymentProcessHeader(); }
                //if (documentStatus.Equals(1)) // update paused document
                //{
                //    documentNo = GetDocumentNo(false);
                //    txtDocumentNo.Text = documentNo;
                //}

                accPettyCashPaymentProcessHeader = FillSelectedPayments(documentStatus, documentNo, accPettyCashPaymentProcessHeader);

                return accPettyCashPaymentProcessService.SavePettyCashPaymentProcess(accPettyCashPaymentProcessHeader, accPettyCashPaymentProcessDetailList, out newDocumentNo, this.Name);
            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                newDocumentNo = string.Empty;
                return false;
            }
        }

        private void GetSummarizeFigures(List<AccPettyCashPaymentHeader> listItem)
        {
            decimal issuedTotalAmount = 0;

            try
            {
                issuedTotalAmount = listItem.GetSummaryAmount(x => x.Amount);

                txtTotalAmount.Text = Common.ConvertDecimalToStringCurrency(issuedTotalAmount);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private AccPettyCashPaymentProcessHeader FillSelectedPayments(int documentStatus, string documentNo, AccPettyCashPaymentProcessHeader accPettyCashPaymentProcessHeader)
        {
            try
            {
                if (accPettyCashPaymentHeaderList.Where(v => v.Status == true).Count() > 0)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    AccPettyCashPaymentProcessDetail accPettyCashPaymentProcessDetail;
                    LocationService locationService = new LocationService();
                    Location location = new Location();
                    location = locationService.GetLocationsByName(cmbLocation.Text);

                    if (accPettyCashPaymentProcessDetailList == null)
                    { accPettyCashPaymentProcessDetailList = new List<AccPettyCashPaymentProcessDetail>(); }

                    accPettyCashPaymentProcessHeader.AccPettyCashPaymentProcessHeaderID = 0;
                    accPettyCashPaymentProcessHeader.PettyCashPaymentProcessHeaderID = 0;
                    accPettyCashPaymentProcessHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                    accPettyCashPaymentProcessHeader.CompanyID = location.CompanyID;
                    accPettyCashPaymentProcessHeader.LocationID = location.LocationID;
                    accPettyCashPaymentProcessHeader.CostCentreID = location.CostCentreID;
                    accPettyCashPaymentProcessHeader.DocumentID = documentID;
                    accPettyCashPaymentProcessHeader.DocumentNo = documentNo.Trim();
                    accPettyCashPaymentProcessHeader.PettyCashLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtPettyCashBookCode.Text).AccLedgerAccountID;
                    //accPettyCashPaymentProcessHeader.EmployeeID = employeeService.GetEmployeesByCode(txtEmployeeCode.Text).EmployeeID;
                    accPettyCashPaymentProcessHeader.Amount = Common.ConvertStringToDecimalCurrency(txtTotalAmount.Text);
                    accPettyCashPaymentProcessHeader.DocumentDate = dtpDocumentDate.Value;
                    accPettyCashPaymentProcessHeader.PaymentDate = dtpDocumentDate.Value;
                    //accPettyCashPaymentProcessHeader.Reference = txtReferenceNo.Text.Trim();
                    //accPettyCashPaymentProcessHeader.Remark = txtRemark.Text.Trim();
                    //accPettyCashPaymentProcessHeader.PayeeName = txtPayeeName.Text.Trim();
                    if(documentStatus == 1) {accPettyCashPaymentProcessHeader.PaymentStatus = 3;}
                    accPettyCashPaymentProcessHeader.DocumentStatus = documentStatus;
                    accPettyCashPaymentProcessHeader.CreatedUser = Common.LoggedUser;
                    accPettyCashPaymentProcessHeader.CreatedDate = DateTime.UtcNow;
                    accPettyCashPaymentProcessHeader.ModifiedUser = Common.LoggedUser;
                    accPettyCashPaymentProcessHeader.ModifiedDate = DateTime.UtcNow;
                    
                    foreach (AccPettyCashPaymentHeader accPettyCashPaymentHeader in accPettyCashPaymentHeaderList.Where(v => v.Status == true))
                    {
                        accPettyCashPaymentProcessDetail = new AccPettyCashPaymentProcessDetail();
                        accPettyCashPaymentProcessDetail.AccPettyCashPaymentProcessDetailID = 0;
                        accPettyCashPaymentProcessDetail.PettyCashPaymentProcessDetailID = 0;
                        accPettyCashPaymentProcessDetail.AccPettyCashPaymentProcessHeaderID = accPettyCashPaymentProcessHeader.AccPettyCashPaymentProcessHeaderID;
                        accPettyCashPaymentProcessDetail.PettyCashPaymentProcessHeaderID = accPettyCashPaymentProcessHeader.AccPettyCashPaymentProcessHeaderID;
                        accPettyCashPaymentProcessDetail.GroupOfCompanyID = accPettyCashPaymentHeader.GroupOfCompanyID;
                        accPettyCashPaymentProcessDetail.CompanyID = accPettyCashPaymentHeader.CompanyID;
                        accPettyCashPaymentProcessDetail.LocationID = accPettyCashPaymentHeader.LocationID;
                        accPettyCashPaymentProcessDetail.CostCentreID = accPettyCashPaymentHeader.CostCentreID;
                        accPettyCashPaymentProcessDetail.DocumentID = accPettyCashPaymentProcessHeader.DocumentID;
                        accPettyCashPaymentProcessDetail.DocumentNo = accPettyCashPaymentProcessHeader.DocumentNo;
                        accPettyCashPaymentProcessDetail.DocumentDate = accPettyCashPaymentProcessHeader.DocumentDate;
                        accPettyCashPaymentProcessDetail.PaymentDate = accPettyCashPaymentProcessHeader.PaymentDate;
                        accPettyCashPaymentProcessDetail.LedgerID = accPettyCashPaymentHeader.PettyCashLedgerID;
                        accPettyCashPaymentProcessDetail.Amount = accPettyCashPaymentHeader.BalanceAmount;
                        accPettyCashPaymentProcessDetail.ReferenceDocumentID = accPettyCashPaymentHeader.AccPettyCashPaymentHeaderID;
                        accPettyCashPaymentProcessDetail.ReferenceDocumentDocumentID = accPettyCashPaymentHeader.DocumentID;
                        accPettyCashPaymentProcessDetail.ReferenceLocationID = accPettyCashPaymentHeader.LocationID;
                        accPettyCashPaymentProcessDetail.DocumentStatus = accPettyCashPaymentHeader.DocumentStatus;
                        accPettyCashPaymentProcessDetail.CreatedUser = accPettyCashPaymentHeader.CreatedUser;
                        accPettyCashPaymentProcessDetail.CreatedDate = accPettyCashPaymentHeader.CreatedDate;
                        accPettyCashPaymentProcessDetail.ModifiedUser = accPettyCashPaymentHeader.ModifiedUser;
                        accPettyCashPaymentProcessDetail.ModifiedDate = accPettyCashPaymentHeader.ModifiedDate;
                        accPettyCashPaymentProcessDetailList.Add(accPettyCashPaymentProcessDetail);
                    }
                    accPettyCashPaymentProcessHeader.AccPettyCashPaymentProcessDetails = accPettyCashPaymentProcessDetailList.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            return accPettyCashPaymentProcessHeader;
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

        /// <summary>
        /// Load paused document
        /// </summary>
        private void LoadPettyCashPaymentUpdateDocument()
        {
            try
            {
                AccPettyCashPaymentProcessService accPettyCashPaymentProcessService = new AccPettyCashPaymentProcessService();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                AccLedgerAccount accLedgerAccount = new AccLedgerAccount();
                AccPettyCashPaymentProcessService accPettyCashPaymentProcessDetailService = new AccPettyCashPaymentProcessService();
                AccPettyCashPaymentProcessHeader accPettyCashPaymentProcessHeader = new AccPettyCashPaymentProcessHeader();

                accPettyCashPaymentProcessHeader = accPettyCashPaymentProcessDetailService.GetAccPettyCashPaymentProcessHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                if (accPettyCashPaymentProcessHeader == null)
                {
                    //Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
                else
                {
                    accLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByID(accPettyCashPaymentProcessHeader.PettyCashLedgerID);
                    txtDocumentNo.Text = accPettyCashPaymentProcessHeader.DocumentNo;
                    dtpDocumentDate.Value = accPettyCashPaymentProcessHeader.DocumentDate;
                    cmbLocation.SelectedValue = accPettyCashPaymentProcessHeader.LocationID;
                    txtPettyCashBookCode.Text = accLedgerAccount.LedgerCode;
                    txtPettyCashBookName.Text = accLedgerAccount.LedgerName;
                    txtTotalAmount.Text = Common.ConvertDecimalToStringCurrency(accPettyCashPaymentProcessHeader.Amount);

                    dgvPaymentDetail.DataSource = null;
                    accPettyCashPaymentHeaderList = accPettyCashPaymentProcessService.GetAllPettyCashPaymentProcessDetail(accPettyCashPaymentProcessHeader);
                    dgvPaymentDetail.DataSource = accPettyCashPaymentHeaderList;
                    dgvPaymentDetail.Refresh();

                    Common.EnableTextBox(false, txtPettyCashBookCode, txtPettyCashBookName);
                    Common.EnableComboBox(false, cmbLocation);

                    if (accPettyCashPaymentProcessHeader.DocumentStatus.Equals(0))
                    {
                        grpBody.Enabled = true;
                        grpFooter.Enabled = true;
                        //EnableLine(true);
                        Common.EnableButton(true, btnSave);
                        Common.EnableButton(false, btnDocumentDetails);
                    }
                    else
                    {
                        grpBody.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        /// <summary>
        /// Load Payment documents
        /// </summary>
        private void LoadPettyPaymentDetail()
        {
            try
            {
                AccPettyCashPaymentProcessService accPettyCashPaymentProcessService = new AccPettyCashPaymentProcessService();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                AccLedgerAccount accLedgerAccount = new AccLedgerAccount();
                LocationService locationService = new LocationService();
                Location location = new Location();

                long ledgerID;
                int locationID;

                accLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByCode(txtPettyCashBookCode.Text);
                if (accLedgerAccount == null)
                { ledgerID = 0; }
                else
                { ledgerID = accLedgerAccount.AccLedgerAccountID; }
            
                location = locationService.GetLocationsByName(cmbLocation.Text);
                if (location == null)
                { locationID = 0; }
                else
                { locationID = location.LocationID; }

                dgvPaymentDetail.DataSource = null;
                accPettyCashPaymentHeaderList = accPettyCashPaymentProcessService.GetAllPettyCashPaymentHeader(ledgerID, locationID);
                dgvPaymentDetail.DataSource = accPettyCashPaymentHeaderList;
                dgvPaymentDetail.Refresh();

                if (!string.IsNullOrEmpty(txtPettyCashBookCode.Text))
                { 
                    Common.EnableTextBox(false, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation);
                }

                Common.SetDataGridviewColumnsReadOnly(false, dgvPaymentDetail, dgvPaymentDetail.Columns[0]);

                if (accPettyCashPaymentHeaderList.Count > 0)
                {
                    grpBody.Enabled = true;
                    grpFooter.Enabled = true;
                    //EnableLine(true);
                    Common.EnableButton(true, btnSave);
                    Common.EnableButton(false, btnDocumentDetails);
                }
                else
                {

                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private bool ValidateControls()
        {
            bool isValid = true;

            if (!Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtPettyCashBookCode, cmbLocation))
            { return false; }

            return isValid;
        }

        private bool IsValidateExistsPettyCashLedgerByLocationID()
        {
            bool isValid = true;

            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
            AccPettyCashPaymentUpdateValidator accPettyCashPaymentUpdateValidator = new AccPettyCashPaymentUpdateValidator();
            LocationService locationService = new LocationService();
            
            long pettyCashLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtPettyCashBookCode.Text.Trim()).AccLedgerAccountID;
            int locationID = locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID;

            if (!accPettyCashPaymentUpdateValidator.ValidateExistsPettyCashLedgerByLocationID(pettyCashLedgerID, locationID))
            {
                Toast.Show(Common.ConvertStringToDisplayFormat(lblLocation.Text.Trim()) + " - " + cmbLocation.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "selected - " + Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + Common.ConvertStringToDisplayFormat(txtPettyCashBookCode.Text.Trim()));
                isValid = false;
            }

            return isValid;
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
        #endregion

        

        

        
        
    }
}
