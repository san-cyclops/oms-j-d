using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Domain;
using Report;
using Report.Inventory;
using Report.Inventory.Reference.Reports;
using Utility;
using Service;
using System.Reflection;

namespace UI.Windows
{
    /// <summary>
    /// Developed by - asanka
    /// </summary>
    public partial class FrmCategory : UI.Windows.FrmBaseMasterForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        ErrorMessage errorMessage = new ErrorMessage();
        private InvDepartment existingDeparment;
        private InvCategory existingCategory;
        AutoCompleteStringCollection autoCompleteDepartmentCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteDepartmentName = new AutoCompleteStringCollection(); 
        AutoCompleteStringCollection autoCompleteCategoryCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autocompleteCategoryName = new AutoCompleteStringCollection();
        bool isDepend = false;
        bool isDependSubCategory = false;
        int documentID = 0;

        public FrmCategory()
        {
            InitializeComponent();
        }

        #region Form Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCategory_Load(object sender, EventArgs e)
        {
            
        }

        public override void FormLoad()
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                //By Pravin  // For Dependency
                isDepend = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).IsDepend;
                isDependSubCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend;
                this.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).FormText.Trim();
                lblCategoryCode.Text = this.Text + " " + lblCategoryCode.Text;
                lblCategoryDescription.Text = this.Text + " " + lblCategoryDescription.Text;
                lblDepartment.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;

                if (!isDepend)
                {
                    txtDepartmentCode.Visible = false;
                    txtDepartmentName.Visible = false;
                    chkAutoCompleationDepartment.Visible = false;
                    chkAutoCompleationDepartment.Checked = false;
                    lblDepartment.Visible = false;

                }

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

        /// <summary>
        /// Get new code on user demand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Common.EnableButton(false, btnNew, btnDelete);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

                if (isDepend)
                {
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    existingDeparment = invDepartmentService.GetInvDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDepend);
                    if (existingDeparment == null)
                    {
                        Toast.Show(lblDepartment.Text + " " + txtDepartmentCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtDepartmentCode.Focus();
                        ClearForm();
                        return;
                    }
                }
                
                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    InvCategoryService invCategoryService = new InvCategoryService();

                    if(!isDepend)
                        txtCategoryCode.Text = invCategoryService.GetNewCode(this.Name, 1, "", isDependSubCategory);
                    else
                        txtCategoryCode.Text = invCategoryService.GetNewCode(this.Name, existingDeparment.InvDepartmentID, existingDeparment.DepartmentCode, isDependSubCategory);

                    
                    Common.EnableTextBox(false,txtCategoryCode,txtDepartmentCode,txtDepartmentName);
                    txtCategoryName.Focus();
                }
                else
                {
                    Common.EnableTextBox(true, txtCategoryCode);
                    txtCategoryCode.Focus(); 
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtCategoryCode.Focus();
                txtCategoryCode.SelectionStart = txtCategoryCode.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentCode_Leave(object sender, EventArgs e)
        {
            try
            {
                InvDepartmentService invDepartmentService = new InvDepartmentService();
                existingDeparment = invDepartmentService.GetInvDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDepend);
                if (existingDeparment != null)
                {
                    txtDepartmentName.Text = existingDeparment.DepartmentName.Trim();
                }
                else
                {}
                txtCategoryCode.Focus();
                txtCategoryCode.SelectionStart = txtCategoryCode.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtCategoryCode.Focus();
                txtCategoryCode.SelectionStart = txtCategoryCode.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentName_Leave(object sender, EventArgs e)
        {
            try
            {
                InvDepartmentService invDepartmentService = new InvDepartmentService();
                existingDeparment = invDepartmentService.GetInvDepartmentsByName(txtDepartmentName.Text.Trim(), isDepend);
                if (existingDeparment != null)
                {
                    txtDepartmentCode.Text = existingDeparment.DepartmentCode.Trim();
                }
                else
                { }
                txtCategoryCode.Focus();
                txtCategoryCode.SelectionStart = txtCategoryCode.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvCategoryService invCategoryService = new InvCategoryService();
                    DataView dvAllReferenceData = new DataView(invCategoryService.GetAllActiveInvCategoriesDataTable(isDependSubCategory));
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtCategoryCode);
                        txtCategoryCode_Leave(this, e);
                    }
                }

                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtCategoryName.Focus();
                txtCategoryName.SelectionStart = txtCategoryName.Text.Length;
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

        private void txtCategoryCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCategoryCode.Text.Trim()))
                { return; }

                InvCategoryService invCategoryService = new InvCategoryService();
                existingCategory = invCategoryService.GetInvCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory);

                if (existingCategory != null)
                {
                    if (isDepend)
                    {
                        InvDepartmentService invDepartmentService = new InvDepartmentService();
                        existingDeparment = invDepartmentService.GetInvDepartmentsByID(existingCategory.InvDepartmentID, isDependSubCategory);

                        txtDepartmentCode.Text = existingDeparment.DepartmentCode;
                        txtDepartmentName.Text = existingDeparment.DepartmentName;
                        txtCategoryCode.Text = existingCategory.CategoryCode.Trim();
                        txtCategoryName.Text = existingCategory.CategoryName.Trim();
                        txtRemark.Text = existingCategory.Remark.Trim();
                    }
                    else
                    {
                        txtCategoryCode.Text = existingCategory.CategoryCode.Trim();
                        txtCategoryName.Text = existingCategory.CategoryName.Trim();
                        txtRemark.Text = existingCategory.Remark.Trim();
                    }

                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                    Common.EnableButton(false, btnNew);
                }
                else
                {
                    if (chkAutoClear.Checked)
                    { Common.ClearTextBox(txtRemark); }

                    if (btnNew.Enabled)
                    {
                        if (Toast.Show("" + this.Text + " - " + txtCategoryCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                        { 
                            btnNew.PerformClick(); 
                        }
                    }
                }

                if (btnSave.Enabled)
                { 
                    Common.EnableTextBox(false, txtCategoryCode); 
                }

                txtCategoryName.Focus();
                txtCategoryName.SelectionStart = txtCategoryName.Text.Length;
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvCategoryService invCategoryService = new InvCategoryService();
                    DataView dvAllReferenceData = new DataView(invCategoryService.GetAllActiveInvCategoriesDataTable(isDependSubCategory));
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtCategoryCode);
                        txtCategoryCode_Leave(this, e);
                    }
                }

                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }
                txtRemark.Focus();
                txtRemark.SelectionStart = txtRemark.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        private void txtCategoryName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                { return; }

                if (string.IsNullOrEmpty(txtCategoryName.Text.Trim()))
                { return; }

                InvCategoryService invCategoryService = new InvCategoryService();
                existingCategory = invCategoryService.GetInvCategoryByName(txtCategoryName.Text.Trim(), isDependSubCategory);

                if (existingCategory != null)
                {
                    if (isDepend)
                    {
                        InvDepartmentService invDepartmentService = new InvDepartmentService();
                        existingDeparment = invDepartmentService.GetInvDepartmentsByID(existingCategory.InvDepartmentID, isDependSubCategory);

                        txtDepartmentCode.Text = existingDeparment.DepartmentCode;
                        txtDepartmentName.Text = existingDeparment.DepartmentName;
                        txtCategoryCode.Text = existingCategory.CategoryCode.Trim();
                        txtCategoryName.Text = existingCategory.CategoryName.Trim();
                        txtRemark.Text = existingCategory.Remark.Trim();
                    }
                    else
                    {
                        txtCategoryCode.Text = existingCategory.CategoryCode.Trim();
                        txtCategoryName.Text = existingCategory.CategoryName.Trim();
                        txtRemark.Text = existingCategory.Remark.Trim();
                    }

                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                    Common.EnableButton(false, btnNew);
                }
                else
                {
                    Toast.Show("" + this.Text + " - " + txtCategoryName.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    if (chkAutoClear.Checked)
                    { Common.ClearTextBox(txtRemark); }

                }

                if (btnSave.Enabled)
                {
                    Common.EnableTextBox(false, txtCategoryCode, txtDepartmentCode, txtDepartmentName);
                }

                txtRemark.Focus();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationDepartment_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Common.SetAutoComplete(txtDepartmentCode, autoCompleteDepartmentCode, chkAutoCompleationDepartment.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationCategory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Common.SetAutoComplete(txtCategoryCode, autoCompleteCategoryCode, chkAutoCompleationCategory.Checked);
                Common.SetAutoComplete(txtCategoryName, autocompleteCategoryName, chkAutoCompleationCategory.Checked);
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
            try
            {
                #region Categories auto load
                InvCategoryService invCategoryService = new InvCategoryService();
                autoCompleteCategoryCode = new AutoCompleteStringCollection();
                autoCompleteCategoryCode.AddRange(invCategoryService.GetInvCategoryCodes(isDependSubCategory));
                Common.SetAutoComplete(txtCategoryCode, autoCompleteCategoryCode, chkAutoCompleationCategory.Checked);

                autocompleteCategoryName = new AutoCompleteStringCollection();
                autocompleteCategoryName.AddRange(invCategoryService.GetInvCategoryNames(isDependSubCategory));
                Common.SetAutoComplete(txtCategoryName, autocompleteCategoryName, chkAutoCompleationCategory.Checked);
                #endregion

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete, btnView);
                Common.EnableTextBox(true, txtCategoryCode, txtDepartmentCode, txtDepartmentName);
                Common.ClearTextBox(txtCategoryCode);

                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                //By Pravin  // For Dependency

                if (isDepend)
                {
                    #region Departments auto load
                    
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    autoCompleteDepartmentCode = new AutoCompleteStringCollection();
                    autoCompleteDepartmentCode.AddRange(invDepartmentService.GetInvDepartmentCodes(isDepend));
                    Common.SetAutoComplete(txtDepartmentCode, autoCompleteDepartmentCode, chkAutoCompleationDepartment.Checked);

                    autoCompleteDepartmentName = new AutoCompleteStringCollection();
                    autoCompleteDepartmentName.AddRange(invDepartmentService.GetInvDepartmentNames(isDepend));
                    Common.SetAutoComplete(txtDepartmentName, autoCompleteDepartmentName, chkAutoCompleationDepartment.Checked);

                    #endregion
                    
                    ActiveControl = txtDepartmentCode;
                    txtDepartmentCode.Focus();
                }
                else
                {
                    ActiveControl = txtCategoryCode;
                    txtCategoryCode.Focus();
                }
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
                if (!ValidateControls())
                { return; }

                InvDepartment invDepartment = new InvDepartment();
                InvCategory invCategory = new InvCategory();
                InvDepartmentService invDepartmentService = new InvDepartmentService();
                InvCategoryService invCategoryService = new InvCategoryService();
                bool isNew = false;

                // Assign values
                invDepartment.DepartmentCode = txtDepartmentCode.Text.Trim();
                invCategory.CategoryCode = txtCategoryCode.Text.Trim();

                // Check availability of Dapertment
                //By Pravin  // For Dependency

                if (isDepend)
                {
                    existingDeparment = invDepartmentService.GetInvDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDepend);
                    if (existingDeparment == null)
                    {
                        Toast.Show(lblDepartment.Text + " " + txtDepartmentCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtDepartmentCode.Focus();
                        return;
                    }
                }


                // Check availability of Category
                existingCategory = invCategoryService.GetInvCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory);
                if (existingCategory == null || existingCategory.InvCategoryID == 0)
                { existingCategory = new InvCategory(); }


                if (isDepend)
                    existingCategory.InvDepartmentID = existingDeparment.InvDepartmentID;
                else
                {
                    existingCategory.InvDepartmentID = 1;
                    if (existingCategory.InvCategoryID.Equals(1))
                        return;
                }

                existingCategory.CategoryCode = txtCategoryCode.Text.Trim();
                existingCategory.CategoryName = txtCategoryName.Text.Trim();
                existingCategory.Remark = txtRemark.Text.Trim();
                existingCategory.IsDelete = false;

                if (existingCategory.InvCategoryID.Equals(0))
                {
                    if ((Toast.Show("" + this.Text + " - " + existingCategory.CategoryCode + " - " + existingCategory.CategoryName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    { return; }
                    
                    // Create new Category
                    invCategoryService.AddInvCategory(existingCategory);

                    if ((Toast.Show("" + this.Text + " - " + existingCategory.CategoryCode + " - " + existingCategory.CategoryName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
                    {
                        if (chkAutoClear.Checked)
                        {
                            Common.ClearTextBox(txtCategoryCode, txtCategoryName, txtRemark);
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
                        if ((Toast.Show("" + this.Text + " - " + existingCategory.CategoryCode + " - " + existingCategory.CategoryName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        { return; }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingCategory.CategoryCode + " - " + existingCategory.CategoryName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("" + this.Text + " - " + existingCategory.CategoryCode + " - " + existingCategory.CategoryName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        { return; }
                    }
                    // Update Category deatils
                    invCategoryService.UpdateInvCategory(existingCategory);
                    if (chkAutoClear.Checked)
                    { ClearForm(); }
                    else
                    { InitializeForm(); }

                    // Display updated iformation 
                    Toast.Show("" + this.Text + " - " + existingCategory.CategoryCode + " - " + existingCategory.CategoryName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }

                txtCategoryCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// Delete Category(Change Delete status of relevent Category)
        /// </summary>
        public override void Delete()
        {
            try
            {
                if (Toast.Show("" + this.Text + " - " + existingCategory.CategoryCode + " - " + existingCategory.CategoryName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                { return; }

                InvCategoryService invCategoryService = new InvCategoryService();
                existingCategory = invCategoryService.GetInvCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory);
                if (existingCategory != null && existingCategory.InvCategoryID != 0)
                {
                    existingCategory.IsDelete = true;
                    invCategoryService.UpdateInvCategory(existingCategory);
                    Toast.Show("" + this.Text + " - " + existingCategory.CategoryCode + " - " + existingCategory.CategoryName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    ClearForm();
                    txtCategoryCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void CloseForm()
        {
            try
            {
                base.CloseForm();
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
                //string[] decimalfield = { };
                //MdiService mdiService = new MdiService();
                //string ReprotName = "Category";
                //CryInvColumn5Template cryInvTemplate = new CryInvColumn5Template();
                //int x;

                //string departmentText = "", categoryText = "";

                //departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
                //categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
                
                //if (isDepend)
                //{
                //    string[] stringfield = { "Code", categoryText, departmentText, "Remark" };
                //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
                //    //                                                               mdiService.GetAllDepartmentWiseCategoryDataTable(),
                //    //                                                               0, cryInvTemplate);
                //    //frmReprotGenerator.ShowDialog();
                //}
                //else
                //{
                //    string[] stringfield = { "Code", categoryText, "Remark" };
                //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
                //    //                                                               mdiService.GetAllCategoryDataTable(),
                //    //                                                               0, cryInvTemplate);
                //    //frmReprotGenerator.ShowDialog();
                //}

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory");
                InvReportGenerator invReportGenerator = new InvReportGenerator();
                invReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// Validate form controls
        /// </summary>
        /// <returns></returns>
        private bool ValidateControls()
        {
            if(isDepend)
                return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDepartmentCode, txtCategoryCode, txtCategoryName);
            else
                return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty,  txtCategoryCode, txtCategoryName);
        }

        #endregion

        

        

        


        


    }
}
