using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Domain;
using Report;
using Report.Logistic;
using Utility;
using Service;
using System.Reflection;

namespace UI.Windows
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public partial class FrmLogisticCategory : FrmBaseMasterForm
    {
        private LgsDepartment existingLgsDeparment;
        private LgsCategory existingLgsCategory;
        AutoCompleteStringCollection autoCompleteLgsDepartmentCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteDepartmentName = new AutoCompleteStringCollection(); 
        AutoCompleteStringCollection autoCompleteLgsCategoryCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autocompleteLgsCategoryName = new AutoCompleteStringCollection();
        bool isDepend = false;
        bool isDependLgsSubCategory = false;

        UserPrivileges accessRights = new UserPrivileges();
        int documentID;


        public FrmLogisticCategory()
        {
            InitializeComponent();
        }

        private void FrmLogisticCategory_Load(object sender, EventArgs e)
        {

        }

        #region Override Methods....

        public override void FormLoad()
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                isDepend = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).IsDepend;
                isDependLgsSubCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").IsDepend;
                this.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).FormText.Trim();
                lblCategoryCode.Text = this.Text + " " + lblCategoryCode.Text;
                lblCategoryDescription.Text = this.Text + " " + lblCategoryDescription.Text;
                lblDepartment.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticDepartment").FormText;

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

        public override void InitializeForm()
        {
            try
            {

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;

                LgsCategoryService lgsCategoryService = new LgsCategoryService();
                autoCompleteLgsCategoryCode = new AutoCompleteStringCollection();
                autoCompleteLgsCategoryCode.AddRange(lgsCategoryService.GetAllLgsCategoryCodes(isDependLgsSubCategory));
                Common.SetAutoComplete(txtCategoryCode, autoCompleteLgsCategoryCode, chkAutoCompleationCategory.Checked);

                autocompleteLgsCategoryName = new AutoCompleteStringCollection();
                autocompleteLgsCategoryName.AddRange(lgsCategoryService.GetAllLgsCategoryNames(isDependLgsSubCategory));
                Common.SetAutoComplete(txtCategoryName, autocompleteLgsCategoryName, chkAutoCompleationCategory.Checked);

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete, btnView);
                Common.EnableTextBox(true, txtCategoryCode, txtDepartmentCode, txtDepartmentName);
                Common.ClearTextBox(txtCategoryCode);

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights.IsView == true) Common.EnableButton(true, btnView);


                if (isDepend)
                {

                    LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                    autoCompleteLgsDepartmentCode = new AutoCompleteStringCollection();
                    autoCompleteLgsDepartmentCode.AddRange(lgsDepartmentService.GetAllLgsDepartmentCodes(isDepend));
                    Common.SetAutoComplete(txtDepartmentCode, autoCompleteLgsDepartmentCode, chkAutoCompleationDepartment.Checked);

                    autoCompleteDepartmentName = new AutoCompleteStringCollection();
                    autoCompleteDepartmentName.AddRange(lgsDepartmentService.GetAllLgsDepartmentNames(isDepend));
                    Common.SetAutoComplete(txtDepartmentName, autoCompleteDepartmentName, chkAutoCompleationDepartment.Checked);

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

                LgsDepartment lgsDepartment = new LgsDepartment();
                LgsCategory lgsCategory = new LgsCategory();
                LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                LgsCategoryService lgsCategoryService = new LgsCategoryService();
                bool isNew = false;

                // Assign values
                lgsDepartment.DepartmentCode = txtDepartmentCode.Text.Trim();
                lgsCategory.CategoryCode = txtCategoryCode.Text.Trim();

                // Check availability of Dapertment
                //By Pravin  // For Dependency

                if (isDepend)
                {
                    existingLgsDeparment = lgsDepartmentService.GetLgsDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDepend);
                    if (existingLgsDeparment == null)
                    {
                        Toast.Show(lblDepartment.Text + " " + txtDepartmentCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtDepartmentCode.Focus();
                        return;
                    }
                }

                // Check availability of Category
                existingLgsCategory = lgsCategoryService.GetLgsCategoryByCode(txtCategoryCode.Text.Trim(), isDependLgsSubCategory);
                if (existingLgsCategory == null || existingLgsCategory.LgsCategoryID == 0)
                { existingLgsCategory = new LgsCategory(); }


                if (isDepend)
                    existingLgsCategory.LgsDepartmentID = existingLgsDeparment.LgsDepartmentID;
                else
                {
                    existingLgsCategory.LgsDepartmentID = 1;
                    if (existingLgsCategory.LgsCategoryID.Equals(1))
                        return;
                }

                existingLgsCategory.CategoryCode = txtCategoryCode.Text.Trim();
                existingLgsCategory.CategoryName = txtCategoryName.Text.Trim();
                existingLgsCategory.Remark = txtRemark.Text.Trim();
                existingLgsCategory.IsDelete = false;

                if (existingLgsCategory.LgsCategoryID.Equals(0))
                {
                    if ((Toast.Show("" + this.Text + " - " + existingLgsCategory.CategoryCode + " - " + existingLgsCategory.CategoryName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    { return; }
                    // Create new Category
                    lgsCategoryService.AddLgsCategory(existingLgsCategory);

                    if ((Toast.Show("" + this.Text + " - " + existingLgsCategory.CategoryCode + " - " + existingLgsCategory.CategoryName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        if ((Toast.Show("" + this.Text + " - " + existingLgsCategory.CategoryCode + " - " + existingLgsCategory.CategoryName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        { return; }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingLgsCategory.CategoryCode + " - " + existingLgsCategory.CategoryName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("" + this.Text + " - " + existingLgsCategory.CategoryCode + " - " + existingLgsCategory.CategoryName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        { return; }
                    }
                    // Update Category deatils
                    lgsCategoryService.UpdateLgsCategory(existingLgsCategory);
                    if (chkAutoClear.Checked)
                    { ClearForm(); }
                    else
                    { InitializeForm(); }

                    // Display updated iformation 
                    Toast.Show("" + this.Text + " - " + existingLgsCategory.CategoryCode + " - " + existingLgsCategory.CategoryName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }

                txtCategoryCode.Focus();
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
                if (Toast.Show("" + this.Text + " - " + existingLgsCategory.CategoryCode + " - " + existingLgsCategory.CategoryName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                { return; }

                LgsCategoryService lgsCategoryService = new LgsCategoryService();
                existingLgsCategory = lgsCategoryService.GetLgsCategoryByCode(txtCategoryCode.Text.Trim(), isDependLgsSubCategory);
                if (existingLgsCategory != null && existingLgsCategory.LgsCategoryID != 0)
                {
                    lgsCategoryService.DeleteLgsCategory(existingLgsCategory);
                    Toast.Show("" + this.Text + " - " + existingLgsCategory.CategoryCode + " - " + existingLgsCategory.CategoryName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    ClearForm();
                    txtCategoryCode.Focus();
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
                //string[] decimalfield = { };
                //MdiService mdiService = new MdiService();
                //string ReprotName = "Category";
                //CryLgsColumn5Template cryLgsTemplate = new CryLgsColumn5Template();
                //int x;

                //string departmentText = "", categoryText = "";

                //departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticDepartment").FormText;
                //categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").FormText;

                //if (isDepend)
                //{
                //    string[] stringfield = { "Code", categoryText, departmentText, "Remark" };
                //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
                //    //                                                               mdiService.GetAllLgsDepartmentWiseCategoryDataTable(),
                //    //                                                               0, cryLgsTemplate);
                //    //frmReprotGenerator.ShowDialog();
                //}
                //else
                //{
                //    //string[] stringfield = { "Code", categoryText, "Remark" };
                //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
                //    //                                                               mdiService.GetAllLgsCategoryDataTable(),
                //    //                                                               0, cryLgsTemplate);
                //    //frmReprotGenerator.ShowDialog();
                //}
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory");
                LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
                lgsReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
        #endregion

        #region Button Click Events....

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Common.EnableButton(false, btnNew, btnDelete);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

                if (isDepend)
                {
                    LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                    existingLgsDeparment = lgsDepartmentService.GetLgsDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDepend);
                    if (existingLgsDeparment == null)
                    {
                        Toast.Show(lblDepartment.Text + " " + txtDepartmentCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtDepartmentCode.Focus();
                        ClearForm();
                        return;
                    }
                }

                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    LgsCategoryService lgsCategoryService = new LgsCategoryService();

                    if (!isDepend)
                        txtCategoryCode.Text = lgsCategoryService.GetNewCode(this.Name, 1, "", isDependLgsSubCategory);
                    else
                        txtCategoryCode.Text = lgsCategoryService.GetNewCode(this.Name, existingLgsDeparment.LgsDepartmentID, existingLgsDeparment.DepartmentCode, isDependLgsSubCategory);


                    Common.EnableTextBox(false, txtCategoryCode, txtDepartmentCode, txtDepartmentName);
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

        #endregion

        #region Key Down and Leave Events....

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
                LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                existingLgsDeparment = lgsDepartmentService.GetLgsDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDepend);
                if (existingLgsDeparment != null)
                {
                    txtDepartmentName.Text = existingLgsDeparment.DepartmentName.Trim();
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
                LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                existingLgsDeparment = lgsDepartmentService.GetLgsDepartmentsByName(txtDepartmentName.Text.Trim(), isDepend);
                if (existingLgsDeparment != null)
                {
                    txtDepartmentCode.Text = existingLgsDeparment.DepartmentCode.Trim();
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

        private void txtCategoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    LgsCategoryService lgsCategoryService = new LgsCategoryService();
                    DataView dvAllReferenceData = new DataView(lgsCategoryService.GetAllActiveLgsCategoriesDataTable(isDependLgsSubCategory));
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

        private void txtCategoryCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCategoryCode.Text.Trim()))
                { return; }

                LgsCategoryService lgsCategoryService = new LgsCategoryService();
                existingLgsCategory = lgsCategoryService.GetLgsCategoryByCode(txtCategoryCode.Text.Trim(), isDependLgsSubCategory);

                if (existingLgsCategory != null)
                {
                    if (isDepend)
                    {
                        LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                        existingLgsDeparment = lgsDepartmentService.GetLgsDepartmentsByID(existingLgsCategory.LgsDepartmentID, isDependLgsSubCategory);

                        txtDepartmentCode.Text = existingLgsDeparment.DepartmentCode;
                        txtDepartmentName.Text = existingLgsDeparment.DepartmentName;
                        txtCategoryCode.Text = existingLgsCategory.CategoryCode.Trim();
                        txtCategoryName.Text = existingLgsCategory.CategoryName.Trim();
                        txtRemark.Text = existingLgsCategory.Remark.Trim();
                    }
                    else
                    {
                        txtCategoryCode.Text = existingLgsCategory.CategoryCode.Trim();
                        txtCategoryName.Text = existingLgsCategory.CategoryName.Trim();
                        txtRemark.Text = existingLgsCategory.Remark.Trim();
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
                    LgsCategoryService lgsCategoryService = new LgsCategoryService();
                    DataView dvAllReferenceData = new DataView(lgsCategoryService.GetAllActiveLgsCategoriesDataTable(isDependLgsSubCategory));
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

                LgsCategoryService lgsCategoryService = new LgsCategoryService();
                existingLgsCategory = lgsCategoryService.GetLgsCategoryByName(txtCategoryName.Text.Trim(), isDependLgsSubCategory);

                if (existingLgsCategory != null)
                {
                    LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                    existingLgsDeparment = lgsDepartmentService.GetLgsDepartmentsByID(existingLgsCategory.LgsDepartmentID, isDependLgsSubCategory);

                    txtDepartmentCode.Text = existingLgsDeparment.DepartmentCode;
                    txtDepartmentName.Text = existingLgsDeparment.DepartmentName;
                    txtCategoryCode.Text = existingLgsCategory.CategoryCode.Trim();
                    txtCategoryName.Text = existingLgsCategory.CategoryName.Trim();
                    txtRemark.Text = existingLgsCategory.Remark.Trim();

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

        #endregion

        #region Methods....

        private void chkAutoCompleationDepartment_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Common.SetAutoComplete(txtDepartmentCode, autoCompleteLgsDepartmentCode, chkAutoCompleationDepartment.Checked);
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
                Common.SetAutoComplete(txtCategoryCode, autoCompleteLgsCategoryCode, chkAutoCompleationCategory.Checked);
                Common.SetAutoComplete(txtCategoryName, autocompleteLgsCategoryName, chkAutoCompleationCategory.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateControls()
        {
            if (isDepend)
                return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDepartmentCode, txtCategoryCode, txtCategoryName);
            else
                return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtCategoryCode, txtCategoryName);

        }

        #endregion

    }
}
