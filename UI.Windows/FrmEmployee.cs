using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Domain;
using Report.Inventory;
using Service;
using Utility;
using System.IO;
using System.Reflection;

namespace UI.Windows
{
    public partial class FrmEmployee : UI.Windows.FrmBaseMasterForm
    {
        /// <summary>
        /// Sanjeewa
        /// </summary>

        Employee existingEmployee;
        string imagename;
        private ReferenceType existingReferenceType;
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        public FrmEmployee()
        {
            InitializeComponent();
        }

        public override void InitializeForm()
        {
            try
            {
                Employee employee = new Employee();
                EmployeeService employeeService = new EmployeeService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();

                Common.SetAutoComplete(txtEmployeeCode, employeeService.GetAllEmployeeCodes(), chkAutoCompleationEmployee.Checked);
                Common.SetAutoComplete(txtEmployeeName, employeeService.GetAllEmployeeNames(), chkAutoCompleationEmployee.Checked);

                SetAutoBindRecords(cmbGender, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.GenderType).ToString()));
                cmbGender.SelectedIndex = -1;

                SetAutoBindRecords(cmbTitle, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.TitleType).ToString()));
                cmbTitle.SelectedIndex = -1;

                Common.SetAutoBindRecords(cmbDesignation, employeeService.GetAllemployeeDesignations());
                cmbDesignation.SelectedIndex = -1;

                cmbCostCentre.SelectedIndex = -1;
                cmbLocation.SelectedIndex = -1;

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtEmployeeCode);
                Common.ClearTextBox(txtEmployeeCode, txtEmployeeName, txtAddress1, txtAddress2, txtAddress3, txtEmail, txtMobile, txtReferenceNo, txtRemark, txtTelephone);
                pbEmployee.Image = UI.Windows.Properties.Resources.Default_Salesman;
                tabEmployee.SelectedTab = tbpGeneral;

                this.ActiveControl = txtEmployeeCode;
                txtEmployeeCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void FormLoad()
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                //InitializeForm();

                // Load Locations
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());

                //Load Cost Centres
                CostCentreService costCentreService = new CostCentreService();
                Common.LoadCostCenters(cmbCostCentre, costCentreService.GetAllCostCentres());

                ActiveControl = txtEmployeeCode;
                txtEmployeeCode.Focus();

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

        public static void SetAutoBindRecords(ComboBox comboBox, string[] stringCollection)
        {
            try
            {
                AutoCompleteStringCollection autoCompleteCode = new AutoCompleteStringCollection();
                autoCompleteCode.AddRange(stringCollection);
                comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox.AutoCompleteCustomSource = autoCompleteCode;
                comboBox.DataSource = autoCompleteCode;
                comboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), "FrmEmployee", Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtEmployeeCode, txtEmployeeName, txtReferenceNo, txtMobile, txtAddress1, txtAddress2);
        }

        private bool ValidateComboBoxes() 
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbGender, cmbTitle, cmbLocation, cmbCostCentre);
        }

        private Employee FillEmployee()
        {
            try
            {
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                CommissionSchemaService commisionSchemaService = new CommissionSchemaService();
                existingEmployee.EmployeeCode = txtEmployeeCode.Text.Trim();
                existingEmployee.EmployeeName = txtEmployeeName.Text.Trim();

                existingEmployee.ReferenceNo = txtReferenceNo.Text.Trim();
                existingEmployee.Address1 = txtAddress1.Text.Trim();
                existingEmployee.Address2 = txtAddress2.Text.Trim();
                existingEmployee.Address3 = txtAddress3.Text.Trim();
                existingEmployee.Email = txtEmail.Text.Trim();
                existingEmployee.Telephone = txtTelephone.Text.Trim();
                existingEmployee.Mobile = txtMobile.Text.Trim();
                existingEmployee.Remark = txtRemark.Text.Trim();
                existingEmployee.IsActive = chkIsActive.Checked;
                existingEmployee.Designation = cmbDesignation.Text.Trim();

                //existingEmployee.LocationID = Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString());
                //existingEmployee.CostCenterID = Common.ConvertStringToInt(cmbCostCentre.SelectedValue.ToString());
                if (!string.IsNullOrEmpty(cmbLocation.Text.Trim())) { existingEmployee.LocationID = Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()); }
                if (!string.IsNullOrEmpty(cmbCostCentre.Text.Trim())) { existingEmployee.CostCenterID = Common.ConvertStringToInt(cmbCostCentre.SelectedValue.ToString()); }

                //Read Gender Type
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.GenderType).ToString(), cmbGender.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingEmployee.Gender = existingReferenceType.LookupKey;
                }
                else
                {
                    existingEmployee.Gender = 0;
                }

                //Read Title Type
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.TitleType).ToString(), cmbTitle.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingEmployee.EmployeeTitle = existingReferenceType.LookupKey;
                }
                else
                {
                    existingEmployee.EmployeeTitle = 0;
                }
                

                MemoryStream stream = new MemoryStream();
                pbEmployee.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] pic = stream.ToArray();
                existingEmployee.Image = pic;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            return existingEmployee;
        }

        public override void Save()
        {
            try
            {

                if (ValidateControls() == false) return;
                if (ValidateComboBoxes() == false) return;

                EmployeeService employeeService = new EmployeeService();
                bool isNew = false;
                existingEmployee = employeeService.GetEmployeesByCode(txtEmployeeCode.Text.Trim());

                if ( existingEmployee== null || existingEmployee.EmployeeID == 0)
                {
                    existingEmployee = new Employee();
                    isNew = true;
                }

                FillEmployee();

                if (existingEmployee.EmployeeID == 0)
                {
                    if ((Toast.Show("Employee  - " + existingEmployee.EmployeeCode + " - " + existingEmployee.EmployeeName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    employeeService.AddEmployee(existingEmployee);

                    if ((Toast.Show("Salesman  - " + existingEmployee.EmployeeCode + " - " + existingEmployee.EmployeeName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        if ((Toast.Show("Employee  - " + existingEmployee.EmployeeCode + " - " + existingEmployee.EmployeeName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingEmployee.EmployeeCode + " - " + existingEmployee.EmployeeName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("Employee  - " + existingEmployee.EmployeeCode + " - " + existingEmployee.EmployeeName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    employeeService.UpdateEmployee(existingEmployee);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    Toast.Show("Employee  - " + existingEmployee.EmployeeCode + " - " + existingEmployee.EmployeeName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }
                txtEmployeeCode.Focus();

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
                if (Toast.Show("Employee  - " + existingEmployee.EmployeeCode + " - " + existingEmployee.EmployeeName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                    return;

                EmployeeService employeeService = new EmployeeService();
                existingEmployee = employeeService.GetEmployeesByCode(txtEmployeeCode.Text.Trim());

                if (existingEmployee != null && existingEmployee.EmployeeID != 0)
                {
                    existingEmployee.IsDelete = true;
                    employeeService.UpdateEmployee(existingEmployee);
                    ClearForm();
                    Toast.Show("Employee  - " + existingEmployee.EmployeeCode + " - " + existingEmployee.EmployeeName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    txtEmployeeCode.Focus();
                }
                else
                    Toast.Show("Employee  - " + existingEmployee.EmployeeCode + " - " + existingEmployee.EmployeeName + "", Toast.messageType.Warning, Toast.messageAction.NotExists);


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
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmEmployee");
                InvReportGenerator invReportGenerator = new InvReportGenerator();
                invReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();
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

        private void txtEmployeeCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    EmployeeService employeeService = new EmployeeService();
                    DataView dvEmployee = new DataView(employeeService.GetAllEmployeeDataTable());
                    if (dvEmployee.Count != 0)
                    {
                        LoadReferenceSearchForm(dvEmployee, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtEmployeeCode);
                        txtEmployeeCode_Leave(this, e);
                    }
                }
                else if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtEmployeeName.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmployeeCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeCode.Text.Trim() != string.Empty)
                {
                    EmployeeService employeeService = new EmployeeService();

                    existingEmployee = employeeService.GetEmployeesByCode(txtEmployeeCode.Text.Trim());

                    if (existingEmployee != null)
                    {
                        LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();

                        txtEmployeeCode.Text = existingEmployee.EmployeeCode;
                        txtEmployeeName.Text = existingEmployee.EmployeeName;

                        //Read Gender Type
                        existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.GenderType).ToString(), existingEmployee.Gender);
                        if (existingReferenceType != null)
                        {
                            cmbGender.Text = existingReferenceType.LookupValue;
                        }
                        else
                        {
                            cmbGender.SelectedIndex = -1;
                        }

                        existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.TitleType).ToString(), existingEmployee.EmployeeTitle);
                        if (existingReferenceType != null)
                        {
                            cmbTitle.Text = existingReferenceType.LookupValue;
                        }
                        else
                        {
                            cmbTitle.SelectedIndex = -1;
                        }

                        txtReferenceNo.Text = existingEmployee.ReferenceNo;
                        txtAddress1.Text = existingEmployee.Address1;
                        txtAddress2.Text = existingEmployee.Address2;
                        txtAddress3.Text = existingEmployee.Address3;
                        txtEmail.Text = existingEmployee.Email;
                        txtTelephone.Text = existingEmployee.Telephone;
                        txtMobile.Text = existingEmployee.Mobile;
                        txtRemark.Text = existingEmployee.Remark;
                        chkIsActive.Checked = existingEmployee.IsActive;
                        cmbDesignation.Text = existingEmployee.Designation;
                        lblDesig.Text = existingEmployee.Designation;
                        lblDivisionAndDept.Text = existingEmployee.Department;

                        Location location = new Location();
                        LocationService locationService = new LocationService();
                        location = locationService.GetLocationsByID(existingEmployee.LocationID);
                        if (location != null) { cmbLocation.SelectedValue = location.LocationID; }

                        CostCentre costCentre = new CostCentre();
                        CostCentreService CcstCentreService = new CostCentreService();
                        costCentre = CcstCentreService.GetCostCentresByID(existingEmployee.CostCenterID);
                        if (costCentre != null) { cmbCostCentre.SelectedValue = costCentre.CostCentreID; }

                        if (existingEmployee.Image != null)
                        {
                            MemoryStream ms = new MemoryStream((byte[])existingEmployee.Image);
                            pbEmployee.Image = Image.FromStream(ms);
                            pbEmployee.SizeMode = PictureBoxSizeMode.StretchImage;
                            pbEmployee.Refresh();
                        }
                        else
                        {
                            pbEmployee.Image = UI.Windows.Properties.Resources.Default_Salesman;
                            pbEmployee.Refresh();
                        }
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                    }
                    else
                    {
                        if (btnNew.Enabled)
                        {
                            if (Toast.Show("Employee  - " + txtEmployeeCode.Text.Trim() + " ", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                            {
                                btnNew.PerformClick();
                                txtEmployeeName.Focus();
                            }
                        }
                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtEmployeeCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex,MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Common.EnableButton(false, btnNew, btnDelete);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    EmployeeService employeeService = new EmployeeService();
                    txtEmployeeCode.Text = employeeService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength);
                    Common.EnableTextBox(false, txtEmployeeCode);
                    txtEmployeeName.Focus();

                }
                else
                {
                    Common.EnableTextBox(true, txtEmployeeCode);
                    txtEmployeeCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmployeeName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    EmployeeService employeeService = new EmployeeService();
                    DataView dvEmployee = new DataView(employeeService.GetAllEmployeeDataTable());
                    if (dvEmployee.Count != 0)
                    {
                        LoadReferenceSearchForm(dvEmployee, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtEmployeeCode);
                        txtEmployeeCode_Leave(this, e);
                    }
                }
                else if (e.KeyCode.Equals(Keys.Enter))
                {
                    tabEmployee.SelectedTab = tbpGeneral;
                    cmbTitle.Focus();
                }
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbGender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtReferenceNo);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtReferenceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabEmployee, tbpContactDetails);
                Common.SetFocus(e, txtAddress1);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabEmployee, tbpContactDetails);
                Common.SetFocus(e, txtAddress1);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                //specify your own initial directory
                fldlg.InitialDirectory = @":D\";
                //this will allow only those file extensions to be added
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imagename = fldlg.FileName;
                    Bitmap newimg = new Bitmap(imagename);
                    pbEmployee.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbEmployee.Image = (Image)newimg;
                }
                fldlg = null;
            }

            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                MessageBox.Show(ae.Message.ToString());
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabEmployee, tbpFinancial);
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
                Common.SetFocus(e, txtAddress2);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtAddress3);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAddress3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtEmail);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtTelephone);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTelephone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtMobile);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        
        private void btnClearImage_Click(object sender, EventArgs e)
        {
            try
            {
                pbEmployee.Image = UI.Windows.Properties.Resources.Default_Salesman;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationEmployee_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                EmployeeService employeeService = new EmployeeService();

                Common.SetAutoComplete(txtEmployeeCode, employeeService.GetAllEmployeeCodes(), chkAutoCompleationEmployee.Checked);
                Common.SetAutoComplete(txtEmployeeName, employeeService.GetAllEmployeeNames(), chkAutoCompleationEmployee.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        

        private void txtEmployeeName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                {
                    return;
                }
                if (txtEmployeeName.Text.Trim() != string.Empty)
                {
                    EmployeeService employeeService = new EmployeeService();

                    existingEmployee = employeeService.GetEmployeesByName(txtEmployeeName.Text.Trim()); 

                    if (existingEmployee != null)
                    {
                        LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();

                        txtEmployeeCode.Text = existingEmployee.EmployeeCode;
                        txtEmployeeName.Text = existingEmployee.EmployeeName;

                        //Read Gender Type
                        existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.GenderType).ToString(), existingEmployee.Gender);
                        if (existingReferenceType != null)
                        {
                            cmbGender.Text = existingReferenceType.LookupValue;
                        }
                        else
                        {
                            cmbGender.SelectedIndex = -1;
                        }

                        existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.TitleType).ToString(), existingEmployee.EmployeeTitle);
                        if (existingReferenceType != null)
                        {
                            cmbTitle.Text = existingReferenceType.LookupValue;
                        }
                        else
                        {
                            cmbTitle.SelectedIndex = -1;
                        }

                        txtReferenceNo.Text = existingEmployee.ReferenceNo;
                        txtAddress1.Text = existingEmployee.Address1;
                        txtAddress2.Text = existingEmployee.Address2;
                        txtAddress3.Text = existingEmployee.Address3;
                        txtEmail.Text = existingEmployee.Email;
                        txtTelephone.Text = existingEmployee.Telephone;
                        txtMobile.Text = existingEmployee.Mobile;
                        txtRemark.Text = existingEmployee.Remark;
                        chkIsActive.Checked = existingEmployee.IsActive;
                        cmbDesignation.Text = existingEmployee.Designation;


                        Location location = new Location();
                        LocationService locationService = new LocationService();
                        location = locationService.GetLocationsByID(existingEmployee.LocationID);
                        if (location != null) { cmbLocation.SelectedValue = location.LocationID; }

                        CostCentre costCentre = new CostCentre();
                        CostCentreService CcstCentreService = new CostCentreService();
                        costCentre = CcstCentreService.GetCostCentresByID(existingEmployee.CostCenterID);
                        if (costCentre != null) { cmbCostCentre.SelectedValue = costCentre.CostCentreID; }

                        if (existingEmployee.Image != null)
                        {
                            MemoryStream ms = new MemoryStream((byte[])existingEmployee.Image);
                            pbEmployee.Image = Image.FromStream(ms);
                            pbEmployee.SizeMode = PictureBoxSizeMode.StretchImage;
                            pbEmployee.Refresh();
                        }
                        else
                        {
                            pbEmployee.Image = UI.Windows.Properties.Resources.Default_Salesman;
                            pbEmployee.Refresh();
                        }
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        Common.EnableTextBox(false, txtEmployeeCode);
                    }
                    else
                    {
                        Toast.Show("" + this.Text + " - " + txtEmployeeName.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtEmployeeName.Focus();
                        return;
                    }
                }
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

        private void txtTelephone_KeyPress(object sender, KeyPressEventArgs e)
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

        private void cmbTitle_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbGender);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
