using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Domain;
using Service;
using Utility;
using System.Reflection;

namespace UI.Windows
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public partial class FrmCompany : UI.Windows.FrmBaseMasterForm
    {
        private Company existingCompany;
        private GroupOfCompany existingGroupOfCompany;
        private Tax existingTax;
        private CostCentre existingCostCentre;
        UserPrivileges accessRights = new UserPrivileges();
        int documentID;

        AutoCompleteStringCollection autoCompleteCompanyCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autocompleteCompanyName = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autocompleteCostCenterName = new AutoCompleteStringCollection();

        private int fiscalYear;
        private int taxCount;

        public FrmCompany()
        {
            InitializeComponent();
        }

        private void FrmCompany_Load(object sender, EventArgs e)
        {
            
        }

        #region Button Click Events....

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Common.EnableButton(false, btnNew, btnDelete);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                
                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    CompanyService companyService = new CompanyService();
                    txtCompanyCode.Text = companyService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength);
                    Common.EnableTextBox(false, txtCompanyCode,txtGroupOfCompanyCode);
                    txtCompanyName.Focus();
                }
                else
                {
                    Common.EnableTextBox(true, txtCompanyCode);
                    txtCompanyCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Override Methods....

        public override void FormLoad()
        {
            try
            {

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;

                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                DisplayGroupOfCompanyCodeAndName();

                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                Common.SetAutoBindRecords(cmbCostingMethod, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.CostingMethod).ToString()));
                Common.SetAutoBindRecords(cmbFinancialYear, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.FiscalType).ToString()));

                base.FormLoad();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void InitializeForm()
        {
            try
            {


                CompanyService companyService = new CompanyService();

                ////Autocomplete company code
                autoCompleteCompanyCode.Clear();
                autoCompleteCompanyCode.AddRange(companyService.GetAllCompanyCodes());
                Common.SetAutoComplete(txtCompanyCode, autoCompleteCompanyCode, chkAutoCompleteCompanyCode.Checked);
                /////

                ////Autocomplete company name
                autocompleteCompanyName.Clear();
                autocompleteCompanyName.AddRange(companyService.GetAllCompanyNames());
                Common.SetAutoComplete(txtCompanyName, autocompleteCompanyName, chkAutoCompleteCompanyCode.Checked);
                ////

                /////Load cost center name to combo
                CostCentreService costCentreService = new CostCentreService();
                List<CostCentre> costCentres = new List<CostCentre>();
                costCentres = costCentreService.GetAllCostCentres();

                cmbCostCentre.DataSource = costCentres;
                cmbCostCentre.DisplayMember = "CostCentreName";
                cmbCostCentre.Refresh();
                cmbCostCentre.SelectedIndex = -1;
                /////

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete, btnView);

                Common.EnableTextBox(true, txtCompanyCode, txtCompanyName, txtOtherBusinessName1, txtOtherBusinessName2, txtOtherBusinessName3);
                Common.EnableTextBox(true, txtAddress1, txtAddress2, txtAddress3, txtTelephone, txtMobile, txtFax, txtEmail, txtWeb, txtContactPerson);
                Common.EnableTextBox(false, txtTax1No, txtTax2No, txtTax3No, txtTax4No, txtTax5No);
                Common.EnableComboBox(true, cmbCostingMethod, cmbFinancialYear, cmbCostCentre);

                Common.ClearTextBox(txtCompanyCode, txtCompanyName, txtOtherBusinessName1, txtOtherBusinessName2, txtOtherBusinessName3);
                Common.ClearTextBox(txtAddress1, txtAddress2, txtAddress3, txtTelephone, txtMobile, txtFax, txtEmail, txtWeb, txtContactPerson);
                Common.ClearTextBox(txtTax1No, txtTax2No, txtTax3No, txtTax4No, txtTax5No);
                Common.ClearComboBox(cmbCostingMethod, cmbFinancialYear, cmbCostCentre);
                Common.ClearCheckBox(chkTax1, chkTax2, chkTax3, chkTax4, chkTax5);

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                cmbCostingMethod.SelectedIndex = -1;
                cmbFinancialYear.SelectedIndex = -1;

                LoadTaxes();

                tabCompany.SelectedTab = tbpGeneralDetails;
                ActiveControl = txtCompanyCode;
                txtCompanyCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void Save()
        {
            try
            {
                if (ValidateTextBox().Equals(false))
                {
                    return;
                }
                if (ValidateComboBox().Equals(false))
                {
                    return;
                }

                Company company = new Company();
                GroupOfCompany groupOfCompany = new GroupOfCompany();
                Tax tax = new Tax();
                CostCentre costCentre = new CostCentre();

                CompanyService companyService = new CompanyService();
                GroupOfCompanyService groupOfCompanyService = new GroupOfCompanyService();
                TaxService taxService = new TaxService();
                CostCentreService costCentreService = new CostCentreService();
                bool isNew = false;

                // Assign values
                company.CompanyCode = txtCompanyCode.Text.Trim();
                groupOfCompany.GroupOfCompanyCode = txtGroupOfCompanyCode.Text;

                // Check availability of Group Of Company
                existingGroupOfCompany = groupOfCompanyService.GetGroupOfCompaniesByCode(txtGroupOfCompanyCode.Text.Trim());
                if (existingGroupOfCompany == null)
                {
                    Toast.Show("Group Of Company " + txtCompanyCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    txtCompanyCode.Focus();
                    return;
                }

                // Check availability of Cost Center
                existingCostCentre = costCentreService.GetCostCentresByName(cmbCostCentre.Text.Trim());
                if (existingCostCentre == null)
                {
                    Toast.Show("Cost Center " + cmbCostCentre.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    cmbCostCentre.Focus();
                    return;
                }

                // Check availability of Company
                existingCompany = companyService.GetCompaniesByCode(txtCompanyCode.Text.Trim());
                if (existingCompany == null || existingCompany.CompanyID == 0)
                {
                    existingCompany = new Company();
                    isNew = true;
                }

                existingCompany.GroupOfCompanyID = existingGroupOfCompany.GroupOfCompanyID;
                existingCompany.CompanyCode = txtCompanyCode.Text.Trim();
                existingCompany.CompanyName = txtCompanyName.Text.Trim();
                existingCompany.OtherBusinessName1 = txtOtherBusinessName1.Text.Trim();
                existingCompany.OtherBusinessName2 = txtOtherBusinessName2.Text.Trim();
                existingCompany.OtherBusinessName3 = txtOtherBusinessName3.Text.Trim();
                existingCompany.Address1 = txtAddress1.Text.Trim();
                existingCompany.Address2 = txtAddress2.Text.Trim();
                existingCompany.Address3 = txtAddress3.Text.Trim();
                existingCompany.Telephone = txtTelephone.Text.Trim();
                existingCompany.Mobile = txtMobile.Text.Trim();
                existingCompany.FaxNo = txtFax.Text.Trim();
                existingCompany.Email = txtEmail.Text.Trim();
                existingCompany.WebAddress = txtWeb.Text.Trim();
                existingCompany.ContactPerson = txtContactPerson.Text.Trim();

                existingTax = taxService.GetTaxByName(lblTax1.Text.Trim());
                if (chkTax1.Checked) { existingCompany.TaxID1 = existingTax.TaxID; }
                else { existingCompany.TaxID1 = 0; }

                existingTax = taxService.GetTaxByName(lblTax2.Text.Trim());
                if (chkTax2.Checked) { existingCompany.TaxID2 = existingTax.TaxID; }
                else { existingCompany.TaxID2 = 0; }

                existingTax = taxService.GetTaxByName(lblTax3.Text.Trim());
                if (chkTax3.Checked) { existingCompany.TaxID3 = existingTax.TaxID; }
                else { existingCompany.TaxID3 = 0; }

                existingTax = taxService.GetTaxByName(lblTax4.Text.Trim());
                if (chkTax4.Checked) { existingCompany.TaxID4 = existingTax.TaxID; }
                else { existingCompany.TaxID4 = 0; }

                existingTax = taxService.GetTaxByName(lblTax5.Text.Trim());
                if (chkTax5.Checked) { existingCompany.TaxID5 = existingTax.TaxID; }
                else { existingCompany.TaxID5 = 0; }

                existingCompany.TaxRegistrationNumber1 = txtTax1No.Text.Trim();
                existingCompany.TaxRegistrationNumber2 = txtTax2No.Text.Trim();
                existingCompany.TaxRegistrationNumber3 = txtTax3No.Text.Trim();
                existingCompany.TaxRegistrationNumber4 = txtTax4No.Text.Trim();
                existingCompany.TaxRegistrationNumber5 = txtTax5No.Text.Trim();
                existingCompany.CostCentreID = existingCostCentre.CostCentreID;
                existingCompany.CostingMethod = cmbCostingMethod.Text.Trim();
                existingCompany.StartOfFiscalYear = fiscalYear;
                existingCompany.IsDelete = false;

                if (existingCompany.CompanyID.Equals(0))
                {
                    if ((Toast.Show("Company - " + existingCompany.CompanyCode + " - " + existingCompany.CompanyName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    // Create new Company
                    companyService.AddCompany(existingCompany);

                    if ((Toast.Show("Company - " + existingCompany.CompanyCode + " - " + existingCompany.CompanyName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
                    {
                        if (chkAutoClear.Checked)
                        {
                            ClearForm();
                        }
                        btnNew.Enabled = true;
                        btnNew.PerformClick();
                    }
                    else
                    {
                        if (chkAutoClear.Checked)
                        {
                            ClearForm();
                        }
                        else
                        {
                            InitializeForm();
                        }
                    }
                }
                else
                {
                    if (isNew)
                    {
                        if ((Toast.Show("Company - " + existingCompany.CompanyCode + " - " + existingCompany.CompanyName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingCompany.CompanyCode + " - " + existingCompany.CompanyName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("Company - " + existingCompany.CompanyCode + " - " + existingCompany.CompanyName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    // Update Company deatils
                    companyService.UpdateCompany(existingCompany);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }

                    // Display updated iformation 
                    Toast.Show("Company - " + existingCompany.CompanyCode + " - " + existingCompany.CompanyName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }

                txtCompanyCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void Delete()
        {
            try
            {
                if (Toast.Show("Company - " + existingCompany.CompanyCode + " - " + existingCompany.CompanyName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                {
                    return;
                }

                CompanyService companyService = new CompanyService();
                existingCompany = companyService.GetCompaniesByCode(txtCompanyCode.Text.Trim());

                if (existingCompany != null && existingCompany.CompanyID != 0)
                {
                    companyService.DeleteCompany(existingCompany);

                    Toast.Show("Company - " + existingCompany.CompanyCode + " - " + existingCompany.CompanyName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    InitializeForm();
                    txtCompanyCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region KeyDown and Leave Events....

        private void txtCompanyCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    CompanyService companyService = new CompanyService();
                    DataView dvAllReferenceData = new DataView(companyService.GetAllActiveCompaniesDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtCompanyCode);
                        txtCompanyCode_Leave(this, e);
                    }
                }
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtCompanyName.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCompanyCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCompanyCode.Text.Trim() != string.Empty)
                {
                    CompanyService companyService = new CompanyService();
                    CostCentreService costCentreService=new CostCentreService();

                    existingCompany = companyService.GetCompaniesByCode(txtCompanyCode.Text.Trim());

                    if (existingCompany != null)
                    {
                        existingCostCentre = costCentreService.GetCostCentresByID(existingCompany.CostCentreID);
                        txtCompanyCode.Text = existingCompany.CompanyCode;
                        txtCompanyName.Text = existingCompany.CompanyName;
                        txtOtherBusinessName1.Text = existingCompany.OtherBusinessName1;
                        txtOtherBusinessName2.Text = existingCompany.OtherBusinessName2;
                        txtOtherBusinessName3.Text = existingCompany.OtherBusinessName3;
                        txtAddress1.Text = existingCompany.Address1;
                        txtAddress2.Text = existingCompany.Address2;
                        txtAddress3.Text = existingCompany.Address3;
                        txtTelephone.Text = existingCompany.Telephone;
                        txtMobile.Text = existingCompany.Mobile;
                        txtFax.Text = existingCompany.FaxNo;
                        txtEmail.Text = existingCompany.Email;
                        txtWeb.Text = existingCompany.WebAddress;
                        txtContactPerson.Text = existingCompany.ContactPerson;

                        txtTax1No.Text = existingCompany.TaxRegistrationNumber1;
                        if (string.IsNullOrEmpty(txtTax1No.Text)) { chkTax1.Checked = false; } else { chkTax1.Checked = true; }

                        txtTax2No.Text = existingCompany.TaxRegistrationNumber2;
                        if (string.IsNullOrEmpty(txtTax2No.Text)) { chkTax2.Checked = false; } else { chkTax2.Checked = true; }

                        txtTax3No.Text = existingCompany.TaxRegistrationNumber3;
                        if (string.IsNullOrEmpty(txtTax3No.Text)) { chkTax3.Checked = false; } else { chkTax3.Checked = true; }

                        txtTax4No.Text = existingCompany.TaxRegistrationNumber4;
                        if (string.IsNullOrEmpty(txtTax4No.Text)) { chkTax4.Checked = false; } else { chkTax4.Checked = true; }

                        txtTax5No.Text = existingCompany.TaxRegistrationNumber5;
                        if (string.IsNullOrEmpty(txtTax5No.Text)) { chkTax5.Checked = false; } else { chkTax5.Checked = true; } 

                        cmbCostCentre.Text = existingCostCentre.CostCentreName;
                        cmbCostingMethod.Text = existingCompany.CostingMethod;

                        if (existingCompany.StartOfFiscalYear.Equals(1))
                        {
                            cmbFinancialYear.Text="January";
                        }
                        else if (existingCompany.StartOfFiscalYear.Equals(4))
                        {
                            cmbFinancialYear.Text = "April";
                        }

                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify== true) Common.EnableButton(true, btnDelete);

                        Common.EnableButton(false, btnNew);
                    }
                    else
                    {
                        if (chkAutoClear.Checked)
                        {
                            Common.ClearTextBox(txtCompanyName);
                        }
                        if (btnNew.Enabled)
                        {
                            if (Toast.Show("Company - " + txtCompanyCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                            {
                                btnNew.PerformClick();
                            }
                        }
                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtCompanyCode);
                    }

                    tabCompany.SelectedTab = tbpGeneralDetails;
                    txtCompanyName.Focus();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    CompanyService companyService = new CompanyService();
                    DataView dvAllReferenceData = new DataView(companyService.GetAllActiveCompaniesDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtCompanyCode);
                        txtCompanyCode_Leave(this, e);
                    }
                }
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtOtherBusinessName1.Focus();
                tabCompany.SelectedTab = tbpGeneralDetails;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCompanyName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                {
                    return;
                }
                if (txtCompanyName.Text.Trim() != string.Empty)
                {
                    CompanyService companyService = new CompanyService();
                    CostCentreService costCentreService = new CostCentreService();

                    existingCompany = companyService.GetCompaniesByName(txtCompanyName.Text.Trim());

                    if (existingCompany != null)
                    {
                        existingCostCentre = costCentreService.GetCostCentresByID(existingCompany.CostCentreID);
                        txtCompanyCode.Text = existingCompany.CompanyCode.Trim();
                        txtCompanyName.Text = existingCompany.CompanyName.Trim();
                        txtOtherBusinessName1.Text = existingCompany.OtherBusinessName1.Trim();
                        txtOtherBusinessName2.Text = existingCompany.OtherBusinessName2.Trim();
                        txtOtherBusinessName3.Text = existingCompany.OtherBusinessName3.Trim();
                        txtAddress1.Text = existingCompany.Address1.Trim();
                        txtAddress2.Text = existingCompany.Address2.Trim();
                        txtAddress3.Text = existingCompany.Address3.Trim();
                        txtTelephone.Text = existingCompany.Telephone.Trim();
                        txtMobile.Text = existingCompany.Mobile.Trim();
                        txtFax.Text = existingCompany.FaxNo.Trim();
                        txtEmail.Text = existingCompany.Email.Trim();
                        txtWeb.Text = existingCompany.WebAddress.Trim();
                        txtContactPerson.Text = existingCompany.ContactPerson.Trim();

                        txtTax1No.Text = existingCompany.TaxRegistrationNumber1.Trim();
                        if (string.IsNullOrEmpty(txtTax1No.Text.Trim())) { chkTax1.Checked = false; } else { chkTax1.Checked = true; }

                        txtTax2No.Text = existingCompany.TaxRegistrationNumber2.Trim();
                        if (string.IsNullOrEmpty(txtTax2No.Text.Trim())) { chkTax2.Checked = false; } else { chkTax2.Checked = true; }

                        txtTax3No.Text = existingCompany.TaxRegistrationNumber3.Trim();
                        if (string.IsNullOrEmpty(txtTax3No.Text.Trim())) { chkTax3.Checked = false; } else { chkTax3.Checked = true; }

                        txtTax4No.Text = existingCompany.TaxRegistrationNumber4.Trim();
                        if (string.IsNullOrEmpty(txtTax4No.Text.Trim())) { chkTax4.Checked = false; } else { chkTax4.Checked = true; }

                        txtTax5No.Text = existingCompany.TaxRegistrationNumber5.Trim();
                        if (string.IsNullOrEmpty(txtTax5No.Text.Trim())) { chkTax5.Checked = false; } else { chkTax5.Checked = true; }

                        cmbCostCentre.Text = existingCostCentre.CostCentreName.Trim();
                        cmbCostingMethod.Text = existingCompany.CostingMethod.Trim();

                        if (existingCompany.StartOfFiscalYear.Equals(1))
                        {
                            cmbFinancialYear.Text = "January";
                        }
                        else if (existingCompany.StartOfFiscalYear.Equals(4))
                        {
                            cmbFinancialYear.Text = "April";
                        }
                        
                        if (accessRights.IsSave== true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                    }
                    else
                    {
                        if (chkAutoClear.Checked)
                        {
                            
                        }
                        if (btnNew.Enabled)
                        {
                            if (Toast.Show("Company - " + txtCompanyCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                            {
                                btnNew.PerformClick();
                                return;
                            }
                        }
                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtCompanyCode);
                    }

                    txtOtherBusinessName1.Focus();
                    tabCompany.SelectedTab = tbpGeneralDetails;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherBusinessName1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtOtherBusinessName2.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherBusinessName1_Leave(object sender, EventArgs e)
        {

        }

        private void txtOtherBusinessName2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtOtherBusinessName3.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherBusinessName2_Leave(object sender, EventArgs e)
        {

        }

        private void txtOtherBusinessName3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                tabCompany.SelectedIndex = (tabCompany.SelectedIndex + 1) % tabCompany.TabCount;
                ActiveControl = txtAddress1;
                txtAddress1.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherBusinessName3_Leave(object sender, EventArgs e)
        {

        }

        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtAddress2.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAddress1_Leave(object sender, EventArgs e)
        {

        }

        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtAddress3.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAddress2_Leave(object sender, EventArgs e)
        {

        }

        private void txtAddress3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtTelephone.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAddress3_Leave(object sender, EventArgs e)
        {

        }

        private void txtTelephone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtMobile.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTelephone_Leave(object sender, EventArgs e)
        {

        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtFax.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); MessageBox.Show(ex.Message);
            }
        }

        private void txtMobile_Leave(object sender, EventArgs e)
        {

        }

        private void txtFax_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtEmail.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtFax_Leave(object sender, EventArgs e)
        {

        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtWeb.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {

        }

        private void txtWeb_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtContactPerson.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtWeb_Leave(object sender, EventArgs e)
        {

        }

        private void txtContactPerson_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                else
                {
                    if (taxCount.Equals(0))
                    {
                        tabCompany.SelectedIndex = (tabCompany.SelectedIndex + 2) % tabCompany.TabCount;
                        ActiveControl = cmbCostCentre;
                        cmbCostCentre.Focus();
                    }
                    else
                    {
                        tabCompany.SelectedIndex = (tabCompany.SelectedIndex + 1) % tabCompany.TabCount;
                        ActiveControl = chkTax1;
                        chkTax1.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtContactPerson_Leave(object sender, EventArgs e)
        {

        }

        private void txtTax1No_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                else
                {
                    if (taxCount.Equals(1))
                    {
                        tabCompany.SelectedIndex = (tabCompany.SelectedIndex + 1) % tabCompany.TabCount;
                        ActiveControl = cmbCostCentre;
                        cmbCostCentre.Focus();
                    }
                    else if (taxCount >= 2)
                    {
                        ActiveControl = chkTax2;
                        chkTax2.Focus();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTax1No_Leave(object sender, EventArgs e)
        {

        }

        private void txtTax2No_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                else
                {
                    if (taxCount.Equals(2))
                    {
                        tabCompany.SelectedIndex = (tabCompany.SelectedIndex + 1) % tabCompany.TabCount;
                        ActiveControl = cmbCostCentre;
                        cmbCostCentre.Focus();
                    }
                    else if (taxCount >= 3)
                    {
                        ActiveControl = chkTax3;
                        chkTax3.Focus();
                    }
                }
                chkTax3.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTax2No_Leave(object sender, EventArgs e)
        {

        }

        private void txtTax3No_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                else
                {
                    if (taxCount.Equals(3))
                    {
                        tabCompany.SelectedIndex = (tabCompany.SelectedIndex + 1) % tabCompany.TabCount;
                        ActiveControl = cmbCostCentre;
                        cmbCostCentre.Focus();
                    }
                    else if (taxCount >= 4)
                    {
                        ActiveControl = chkTax4;
                        chkTax4.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTax3No_Leave(object sender, EventArgs e)
        {

        }

        private void txtTax4No_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                else
                {
                    if (taxCount.Equals(4))
                    {
                        tabCompany.SelectedIndex = (tabCompany.SelectedIndex + 1) % tabCompany.TabCount;
                        ActiveControl = cmbCostCentre;
                        cmbCostCentre.Focus();
                    }
                    else if (taxCount >= 5)
                    {
                        ActiveControl = chkTax5;
                        chkTax5.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTax4No_Leave(object sender, EventArgs e)
        {

        }

        private void txtTax5No_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                tabCompany.SelectedIndex = (tabCompany.SelectedIndex + 1) % tabCompany.TabCount;
                ActiveControl = cmbCostCentre;
                cmbCostCentre.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTax5No_Leave(object sender, EventArgs e)
        {

        }

        private void cmbCostCentre_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                cmbCostingMethod.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbCostingMethod_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                cmbFinancialYear.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbCostingMethod_Leave(object sender, EventArgs e)
        {

        }

        private void cmbFinancialYear_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                btnSave.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbFinancialYear_Leave(object sender, EventArgs e)
        {

        }

        #endregion

        #region Methods....

        private bool ValidateTextBox() 
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtCompanyCode, txtCompanyName);
        }

        private bool ValidateComboBox()
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbCostingMethod, cmbFinancialYear, cmbCostCentre);
        }

        private void DisplayGroupOfCompanyCodeAndName()
        {
            try
            {
                GroupOfCompany groupOfCompany = new GroupOfCompany();
                GroupOfCompanyService groupOfCompanyService = new GroupOfCompanyService();

                groupOfCompany = groupOfCompanyService.GetGroupOfCompaniesByID(1); 

                if (groupOfCompany != null)
                {
                    txtGroupOfCompanyCode.Text = groupOfCompany.GroupOfCompanyCode.Trim();
                    txtGroupOfCompanyName.Text = groupOfCompany.GroupOfCompanyName.Trim();
                }
                else
                {
                    txtGroupOfCompanyCode.Text = string.Empty;
                    txtGroupOfCompanyName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void GetStartOfFinancialYear()
        {
            try
            {
                if(cmbFinancialYear.Text.Equals("January"))
                {
                    fiscalYear = 1;
                }
                else if (cmbFinancialYear.Text.Equals("April"))
                {
                    fiscalYear = 4;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadTaxes()
        {
            try
            {
                Tax tax = new Tax();
                TaxService taxService = new TaxService();

                List<Tax> taxes = new List<Tax>();
                taxes = taxService.GetAllTaxes();

                taxCount = taxes.Count;

                for (int i = 0; i < taxCount; i++)
                {
                    switch (i)
                    {
                        case 0:
                            lblTax1.Text = taxes[i].TaxName.Trim();
                            break;
                        case 1:
                            lblTax2.Text = taxes[i].TaxName.Trim();
                            break;
                        case 2:
                            lblTax3.Text = taxes[i].TaxName.Trim();
                            break;
                        case 3:
                            lblTax4.Text = taxes[i].TaxName.Trim();
                            break;
                        case 4:
                            lblTax5.Text = taxes[i].TaxName.Trim();
                            break;
                    }

                }

                existingTax = taxService.GetTaxByName(lblTax1.Text.Trim());
                if (existingTax == null)
                {
                    lblTax1.Visible = false;
                    chkTax1.Visible = false;
                    txtTax1No.Visible = false;
                }

                existingTax = taxService.GetTaxByName(lblTax2.Text.Trim());
                if (existingTax == null)
                {
                    lblTax2.Visible = false;
                    chkTax2.Visible = false;
                    txtTax2No.Visible = false;
                }

                existingTax = taxService.GetTaxByName(lblTax3.Text.Trim());
                if (existingTax == null)
                {
                    lblTax3.Visible = false;
                    chkTax3.Visible = false;
                    txtTax3No.Visible = false;
                }

                existingTax = taxService.GetTaxByName(lblTax4.Text.Trim());
                if (existingTax == null)
                {
                    lblTax4.Visible = false;
                    chkTax4.Visible = false;
                    txtTax4No.Visible = false;
                }

                existingTax = taxService.GetTaxByName(lblTax5.Text.Trim());
                if (existingTax == null)
                {
                    lblTax5.Visible = false;
                    chkTax5.Visible = false;
                    txtTax5No.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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

        private void cmbFinancialYear_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                GetStartOfFinancialYear();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region Check box checked changed events....

        private void chkAutoCompleteCompanyCode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Common.SetAutoComplete(txtCompanyCode, autoCompleteCompanyCode, chkAutoCompleteCompanyCode.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTax1.Checked) { txtTax1No.Enabled = true; txtTax1No.Focus(); }
                else { txtTax1No.Enabled = false; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTax2.Checked) { txtTax2No.Enabled = true; txtTax2No.Focus(); }
                else { txtTax2No.Enabled = false; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTax3.Checked) { txtTax3No.Enabled = true; txtTax3No.Focus(); }
                else { txtTax3No.Enabled = false; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTax4.Checked) { txtTax4No.Enabled = true; txtTax4No.Focus(); }
                else { txtTax4No.Enabled = false; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTax5.Checked) { txtTax5No.Enabled = true; txtTax5No.Focus(); }
                else { txtTax5No.Enabled = false; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion 

        private void txtTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateEmailAddress(this, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(this, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(this, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
