using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Domain;
using Report;
using Report.Com;
using Service;
using Utility;
using System.Reflection;

namespace UI.Windows
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public partial class FrmLocation : UI.Windows.FrmBaseMasterForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;
        private Location existingLocation; 
        private Company existingCompany;
        private CostCentre existingCostCentre;

        List<CostCentre> existingCostCentreList = new List<CostCentre>(); 

        List<LocationAssignedCostCentre> locationAssignedCostCentreList = new List<LocationAssignedCostCentre>();
        LocationAssignedCostCentre locationAssignedCostCentre = new LocationAssignedCostCentre();

        List<LocationAssignedCostCentre> existingLocationAssignedCostCentreList = new List<LocationAssignedCostCentre>(); 
        LocationAssignedCostCentre existingLocationAssignedCostCentre = new LocationAssignedCostCentre(); 

        AutoCompleteStringCollection autoCompleteLocationCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autocompleteLocationName = new AutoCompleteStringCollection();

        private bool taxFacilitated;
        private bool stockFacilitated;  

        public FrmLocation()
        {
            InitializeComponent();
        }

        private void FrmLocation_Load(object sender, EventArgs e)
        {
            
        }

        #region Button Click Events....

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Common.EnableButton(false, btnNew, btnDelete);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                //if (chkAutoClear.Checked)
                //{
                //    Common.ClearTextBox(txtAddress1, txtAddress2, txtAddress3, txtTelephone, txtMobile, txtFax, txtEmail, txtContactPerson);
                //    Common.ClearTextBox(txtOtherBusinessName, txtLocationPrefix, txtTypeOfBusiness);
                //    Common.ClearComboBox(cmbCompany, cmbCostingMethod);
                //}
                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    LocationService locationService = new LocationService();
                    txtLocationCode.Text = locationService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength);
                    Common.EnableTextBox(false, txtLocationCode);
                    txtLocationName.Focus();
                }
                else
                {
                    Common.EnableTextBox(true, txtLocationCode);
                    txtLocationCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region Override Methods....

        public override void FormLoad()
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;

                dgvCostCentres.AutoGenerateColumns = false;

                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                Common.SetAutoBindRecords(cmbCostingMethod, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.CostingMethod).ToString()));

                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbExistingLocations, locationService.GetAllLocations());

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);

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
                LocationService locationService = new LocationService();
                autoCompleteLocationCode.Clear();
                autoCompleteLocationCode.AddRange(locationService.GetAllLocationCodes());
                Common.SetAutoComplete(txtLocationCode, autoCompleteLocationCode, chkAutoCompleteLocationCode.Checked);

                autocompleteLocationName.Clear();
                autocompleteLocationName.AddRange(locationService.GetAllLocationNames());
                Common.SetAutoComplete(txtLocationName, autocompleteLocationName, chkAutoCompleteLocationCode.Checked);

                Company company = new Company();
                CompanyService companyService = new CompanyService();

                /////load company names to combo
                List<Company> companies = new List<Company>();
                companies = companyService.GetAllCompanies();

                cmbCompany.DataSource = companies;
                cmbCompany.DisplayMember = "CompanyName";
                cmbCompany.Refresh();
                cmbCompany.SelectedIndex = -1;
                /////

                cmbExistingLocations.SelectedIndex = 0;

                /////Load cost center name to Grid
                CostCentreService costCentreService = new CostCentreService();
                List<CostCentre> costCentres = new List<CostCentre>();
                costCentres = costCentreService.GetAllCostCentres();

                dgvCostCentres.DataSource = costCentres;


                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete);

                Common.EnableTextBox(true, txtLocationCode, txtLocationName);
                Common.EnableTextBox(true, txtAddress1, txtAddress2, txtAddress3, txtTelephone, txtMobile, txtFax, txtEmail, txtContactPerson);
                Common.EnableTextBox(true, txtOtherBusinessName, txtLocationPrefix, txtTypeOfBusiness);
                Common.EnableComboBox(true, cmbCompany, cmbCostingMethod);
                Common.EnableComboBox(false, cmbExistingLocations);

                Common.ClearTextBox(txtLocationCode, txtLocationName);
                Common.ClearTextBox(txtAddress1, txtAddress2, txtAddress3, txtTelephone, txtMobile, txtFax, txtEmail, txtContactPerson);
                Common.ClearTextBox(txtOtherBusinessName, txtLocationPrefix, txtTypeOfBusiness);
                Common.ClearComboBox(cmbCompany, cmbCostingMethod);

                cmbCostingMethod.SelectedIndex = -1;

                locationAssignedCostCentreList = new List<LocationAssignedCostCentre>();

                tabLocation.SelectedTab = tbpContactDetails;
                ActiveControl = txtLocationCode;
                txtLocationCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void GetSelectedCostCentres() 
        {
            for (int i = 0; i < dgvCostCentres.RowCount; i++)
            {
                if (Convert.ToBoolean(dgvCostCentres.Rows[i].Cells["Select"].Value) == true)
                {
                    CostCentre costCentre = new CostCentre();
                    CostCentreService costCentreService = new CostCentreService();
                    int costCentreID = Common.ConvertStringToInt(dgvCostCentres.Rows[i].Cells["CostCentreID"].Value.ToString().Trim());
                    costCentre = costCentreService.GetCostCentresByID(costCentreID);

                    Location location = new Location();
                    LocationService locationService = new LocationService();
                    location = locationService.GetLocationsByCode(txtLocationCode.Text.Trim());

                    locationAssignedCostCentre = new LocationAssignedCostCentre();

                    locationAssignedCostCentre.CostCentreID = costCentre.CostCentreID;
                    locationAssignedCostCentre.LocationID = location.LocationID;
                    locationAssignedCostCentre.Select = true;

                    locationAssignedCostCentreList.Add(locationAssignedCostCentre);
                }
                else
                {
                    CostCentre costCentre = new CostCentre();
                    CostCentreService costCentreService = new CostCentreService();
                    int costCentreID = Common.ConvertStringToInt(dgvCostCentres.Rows[i].Cells["CostCentreID"].Value.ToString().Trim());
                    costCentre = costCentreService.GetCostCentresByID(costCentreID);

                    Location location = new Location();
                    LocationService locationService = new LocationService();
                    location = locationService.GetLocationsByCode(txtLocationCode.Text.Trim());

                    locationAssignedCostCentre = new LocationAssignedCostCentre();

                    locationAssignedCostCentre.CostCentreID = costCentre.CostCentreID;
                    locationAssignedCostCentre.LocationID = location.LocationID;
                    locationAssignedCostCentre.Select = false;

                    locationAssignedCostCentreList.Add(locationAssignedCostCentre);
                }
            }
        }

        private void GetLoationAssignedCostCentres(int locationID)
        {
            Location location = new Location();
            LocationService locationService = new LocationService();
            location = locationService.GetLocationsByID(locationID);

            if (location != null)
            {
                existingCostCentreList = locationService.GetLoationAssignedCostCentres(location);
                dgvCostCentres.DataSource = existingCostCentreList;
                dgvCostCentres.Refresh();
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
                if (txtLocationPrefix.TextLength >= 3)
                {
                    MessageBox.Show("Maximum location prefix code length is 2","Validation Failed",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActiveControl = txtLocationPrefix;
                    txtLocationPrefix.Focus();
                }
                else
                {
                    CompanyService companyService = new CompanyService();
                    Company company = new Company();

                    Location location = new Location();
                    LocationService locationService = new LocationService();

                    CostCentre costCentre = new CostCentre();
                    CostCentreService costCentreService = new CostCentreService();

                    GroupOfCompany groupOfCompany = new GroupOfCompany();
                    GroupOfCompanyService groupOfCompanyService = new GroupOfCompanyService();

                    bool isNew = false;

                    // Assign values
                    location.LocationCode = txtLocationCode.Text.Trim();
                    company.CompanyName = cmbCompany.Text.Trim();


                    // Check availability of Company
                    existingCompany = companyService.GetCompaniesByName(cmbCompany.Text.Trim());
                    if (existingCompany == null)
                    {
                        Toast.Show("Company " + cmbCompany.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        cmbCompany.Focus();
                        return;
                    }


                    // Check availability of Location
                    existingLocation = locationService.GetLocationsByCode(txtLocationCode.Text.Trim());
                    if (existingLocation == null || existingLocation.LocationID == 0)
                    {
                        existingLocation = new Location();
                        isNew = true;
                    }

                    existingLocation.GroupOfCompanyID = existingCompany.GroupOfCompanyID;
                    existingLocation.LocationCode = txtLocationCode.Text.Trim();
                    existingLocation.LocationName = txtLocationName.Text.Trim();
                    existingLocation.Address1 = txtAddress1.Text.Trim();
                    existingLocation.Address2 = txtAddress2.Text.Trim();
                    existingLocation.Address3 = txtAddress3.Text.Trim();
                    existingLocation.Telephone = txtTelephone.Text.Trim();
                    existingLocation.Mobile = txtMobile.Text.Trim();
                    existingLocation.FaxNo = txtFax.Text.Trim();
                    existingLocation.Email = txtEmail.Text.Trim();
                    existingLocation.ContactPersonName = txtContactPerson.Text.Trim();
                    
                    existingLocation.CompanyID = existingCompany.CompanyID;
                    //existingLocation.CostCentreID = existingCostCentre.CostCentreID;
                    
                    existingLocation.OtherBusinessName = txtOtherBusinessName.Text.Trim();
                    existingLocation.LocationPrefixCode = txtLocationPrefix.Text.Trim();
                    existingLocation.TypeOfBusiness = txtTypeOfBusiness.Text.Trim();
                    existingLocation.CostingMethod = cmbCostingMethod.Text.Trim();
                    existingLocation.IsVat = taxFacilitated;
                    existingLocation.IsStockLocation = stockFacilitated;
                    existingLocation.IsDelete = false;

                    if (existingLocation.LocationID.Equals(0))
                    {
                        if ((Toast.Show("Location - " + existingLocation.LocationCode + " - " + existingLocation.LocationName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                        {
                            return;
                        }
                        // Create new Location
                        locationService.AddLocation(existingLocation);
                        SaveCostCentres();
                        SaveSubDetails();

                        if ((Toast.Show("Location - " + existingLocation.LocationCode + " - " + existingLocation.LocationName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
                        {
                            if (chkAutoClear.Checked)
                            {
                                ClearForm();
                                InitializeForm();
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
                            if ((Toast.Show("Location - " + existingLocation.LocationCode + " - " + existingLocation.LocationName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingLocation.LocationCode + " - " + existingLocation.LocationName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }

                            if ((Toast.Show("Location - " + existingLocation.LocationCode + " - " + existingLocation.LocationName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                            {
                                return;
                            }
                        }
                        // Update Location deatils
                        locationService.UpdateLocation(existingLocation);
                        SaveCostCentres();
                        if (chkAutoClear.Checked)
                        {
                            ClearForm();
                        }
                        else
                        {
                            InitializeForm();
                        }

                        // Display updated iformation 
                        Toast.Show("Location - " + existingLocation.LocationCode + " - " + existingLocation.LocationName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                    }

                    txtLocationCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void SaveSubDetails()
        {
            LocationService locationService = new LocationService();
            //Add Product Stock Master and Document Numbers
            locationService.AddSubDetails(txtLocationCode.Text.Trim(), Convert.ToInt32(cmbExistingLocations.SelectedValue));
        }

        private void SaveCostCentres()
        {
            GetSelectedCostCentres();
            LocationService locationService = new LocationService();
            locationService.SaveCostCentres(locationAssignedCostCentreList);
        }

        public override void Delete()
        {
            try
            {
                if (Toast.Show("Location - " + existingLocation.LocationCode + " - " + existingLocation.LocationName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                {
                    return;
                }

                LocationService locationService = new LocationService();
                existingLocation = locationService.GetLocationsByCode(txtLocationCode.Text.Trim());

                if (existingLocation != null && existingLocation.LocationID != 0)
                {
                    locationService.DeleteLocation(existingLocation);

                    Toast.Show("Location - " + existingLocation.LocationCode + " - " + existingLocation.LocationName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    InitializeForm();
                    txtLocationCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void View()
        {
            try
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLocation");
                ComReportGenerator comReportGenerator = new ComReportGenerator();
                comReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region KeyDown and Leave Events....

        private void txtLocationCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    LocationService locationService = new LocationService();
                    DataView dvAllReferenceData = new DataView(locationService.GetAllLocationsDataTble());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtLocationCode);
                        txtLocationCode_Leave(this, e);
                    }
                }
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtLocationName.Focus();
                tabLocation.SelectedTab = tbpContactDetails;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLocationCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtLocationCode.Text.Trim() != string.Empty)
                {
                    LocationService locationServise = new LocationService();
                    CompanyService companyService = new CompanyService();
                    CostCentreService costCentreService = new CostCentreService();

                    existingLocation = locationServise.GetLocationsByCode(txtLocationCode.Text.Trim());

                    if (existingLocation != null)
                    {
                        existingCostCentre = costCentreService.GetCostCentresByID(existingLocation.CostCentreID);
                        existingCompany=companyService.GetCompaniesByID(existingLocation.CompanyID);

                        txtLocationCode.Text = existingLocation.LocationCode;
                        txtLocationName.Text = existingLocation.LocationName;
                        txtAddress1.Text = existingLocation.Address1;
                        txtAddress2.Text = existingLocation.Address2;
                        txtAddress3.Text = existingLocation.Address3;
                        txtTelephone.Text = existingLocation.Telephone;
                        txtMobile.Text = existingLocation.Mobile;
                        txtFax.Text = existingLocation.FaxNo;
                        txtEmail.Text = existingLocation.Email;
                        txtContactPerson.Text = existingLocation.ContactPersonName;
                        cmbCompany.Text = existingCompany.CompanyName;
                        
                        
                        
                        txtOtherBusinessName.Text = existingLocation.OtherBusinessName;
                        txtLocationPrefix.Text = existingLocation.LocationPrefixCode;
                        txtTypeOfBusiness.Text = existingLocation.TypeOfBusiness;
                        cmbCostingMethod.Text = existingLocation.CostingMethod;

                        if (existingLocation.IsVat.Equals(true))
                        {
                            chkTaxFacilitated.Checked = true;
                        }
                        else
                        {
                            chkTaxFacilitated.Checked = false;
                        }

                        if (existingLocation.IsStockLocation.Equals(true))
                        {
                            chkStockFacilitated.Checked = true;
                        }
                        else
                        {
                            chkStockFacilitated.Checked = false;
                        }


                        GetLoationAssignedCostCentres(existingLocation.LocationID);

                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                    }
                    else
                    {
                        if (chkAutoClear.Checked)
                        {
                            Common.ClearTextBox(txtLocationName);
                        }
                        if (btnNew.Enabled)
                        {
                            if (Toast.Show("Location - " + txtLocationCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                            {
                                btnNew.PerformClick();
                            }
                        }
                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtLocationCode);
                    }

                    txtLocationName.Focus();
                    txtLocationName.SelectionStart = txtLocationName.Text.Length;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLocationName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    LocationService locationService = new LocationService();
                    DataView dvAllReferenceData = new DataView(locationService.GetAllLocationsDataTble());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtLocationCode);
                        txtLocationCode_Leave(this, e);
                    }
                }
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtAddress1.Focus();
                tabLocation.SelectedTab = tbpContactDetails;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLocationName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                {
                    return;
                }
                if (txtLocationName.Text.Trim() != string.Empty)
                {
                    LocationService locationServise = new LocationService();
                    CompanyService companyService = new CompanyService();
                    CostCentreService costCentreService = new CostCentreService();

                    existingLocation = locationServise.GetLocationsByName(txtLocationName.Text.Trim());

                    if (existingLocation != null)
                    {
                        existingCostCentre = costCentreService.GetCostCentresByID(existingLocation.CostCentreID);
                        existingCompany = companyService.GetCompaniesByID(existingLocation.CompanyID);

                        txtLocationCode.Text = existingLocation.LocationCode;
                        txtLocationName.Text = existingLocation.LocationName;
                        txtAddress1.Text = existingLocation.Address1;
                        txtAddress2.Text = existingLocation.Address2;
                        txtAddress3.Text = existingLocation.Address3;
                        txtTelephone.Text = existingLocation.Telephone;
                        txtMobile.Text = existingLocation.Mobile;
                        txtFax.Text = existingLocation.FaxNo;
                        txtEmail.Text = existingLocation.Email;
                        txtContactPerson.Text = existingLocation.ContactPersonName;
                        cmbCompany.Text = existingCompany.CompanyName;
                        
                        txtOtherBusinessName.Text = existingLocation.OtherBusinessName;
                        txtLocationPrefix.Text = existingLocation.LocationPrefixCode;
                        txtTypeOfBusiness.Text = existingLocation.TypeOfBusiness;
                        cmbCostingMethod.Text = existingLocation.CostingMethod;

                        if (existingLocation.IsVat.Equals(true))
                        {
                            chkTaxFacilitated.Checked = true;
                        }
                        else
                        {
                            chkTaxFacilitated.Checked = false;
                        }

                        if (existingLocation.IsStockLocation.Equals(true))
                        {
                            chkStockFacilitated.Checked = true;
                        }
                        else
                        {
                            chkStockFacilitated.Checked = false;
                        }


                        GetLoationAssignedCostCentres(existingLocation.LocationID);

                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                    }
                    else
                    {
                        if (btnNew.Enabled)
                        {
                            if (Toast.Show("Location - " + txtLocationCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                            {
                                btnNew.PerformClick();
                                return;
                            }
                        }
                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtLocationCode);
                    }

                    txtAddress1.Focus();
                    txtAddress1.SelectionStart = txtAddress1.Text.Length;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                txtContactPerson.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
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
                tabLocation.SelectedIndex = (tabLocation.SelectedIndex + 1) % tabLocation.TabCount;
                ActiveControl = cmbCompany;
                cmbCompany.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtContactPerson_Leave(object sender, EventArgs e)
        {

        }

        private void cmbCompany_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtOtherBusinessName.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbCompany_Leave(object sender, EventArgs e)
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
                txtOtherBusinessName.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        
        private void txtOtherBusinessName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtLocationPrefix.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherBusinessName_Leave(object sender, EventArgs e)
        {

        }

        private void txtLocationPrefix_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtTypeOfBusiness.Focus(); 
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLocationPrefix_Leave(object sender, EventArgs e)
        {

        }

        private void txtTypeOfBusiness_KeyDown(object sender, KeyEventArgs e)
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

        private void txtTypeOfBusiness_Leave(object sender, EventArgs e)
        {

        }

        #endregion

        #region Methods....

        private bool ValidateTextBox()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtLocationCode, txtLocationName, txtAddress1, txtAddress2);
        }

        private bool ValidateComboBox()
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbCompany);
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

        #endregion

        #region Check box checked changed events....

        private void chkTaxFacilitated_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTaxFacilitated.Checked)
                {
                    taxFacilitated = true;
                }
                else
                {
                    taxFacilitated = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkStockFacilitated_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkStockFacilitated.Checked)
                {
                    stockFacilitated = true;
                }
                else
                {
                    stockFacilitated = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleteLocationCode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Common.SetAutoComplete(txtLocationCode, autoCompleteLocationCode, chkAutoCompleteLocationCode.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        private void grpLocation_Enter(object sender, EventArgs e)
        {

        }

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

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
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
                Validater.ValidateEmailAddress(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAddProducts_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAddProducts.Checked)
                {
                    cmbExistingLocations.Enabled = true;
                }
                else
                {
                    cmbExistingLocations.SelectedIndex = 0;
                    cmbExistingLocations.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
