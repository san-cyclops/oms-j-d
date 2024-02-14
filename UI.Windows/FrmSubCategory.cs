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
    
    public partial class FrmSubCategory : UI.Windows.FrmBaseMasterForm
    {

        UserPrivileges accessRights = new UserPrivileges();
        private InvCategory existingCategory;
        private InvSubCategory existingSubCategory;
        AutoCompleteStringCollection autoCompleteCategoryCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteCategoryName = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteSubCategoryCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteSubCategoryName = new AutoCompleteStringCollection();
        bool isDependSubCategory = false;
        bool isDependSubCategory2 = false;
        int documentID = 0;

        public FrmSubCategory()
        {
            InitializeComponent();
        }

        #region Form Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSubCategory_Load(object sender, EventArgs e)
        {
            
        }

        public override void FormLoad()
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                isDependSubCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).IsDepend;

                this.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).FormText.Trim();
                lblSubCategoryCode.Text = this.Text + " " + lblSubCategoryCode.Text;
                lblSubCategoryName.Text = this.Text + " " + lblSubCategoryName.Text;
                lblCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
                isDependSubCategory2 = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").IsDepend;

                if (!isDependSubCategory)
                {
                    txtCategoryCode.Visible = false;
                    txtCategoryName.Visible = false;
                    chkAutoCompleationCategory.Visible = false;
                    chkAutoCompleationCategory.Checked = false;
                    lblCategory.Visible = false;
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

                if (isDependSubCategory)
                {
                    InvCategoryService invCategoryService = new InvCategoryService();
                    existingCategory = invCategoryService.GetInvCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory);
                    if (existingCategory == null) 
                    {
                        Toast.Show(lblCategory.Text + " " + txtCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtCategoryCode.Focus();
                        ClearForm();
                        return;
                    }
                }

                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();

                    if (!isDependSubCategory)
                        txtSubCategoryCode.Text = invSubCategoryService.GetNewCode(this.Name, 1, "",isDependSubCategory2);
                    else
                        txtSubCategoryCode.Text = invSubCategoryService.GetNewCode(this.Name, existingCategory.InvCategoryID, existingCategory.CategoryCode, isDependSubCategory2);


                    Common.EnableTextBox(false, txtSubCategoryCode,txtCategoryCode,txtCategoryName);
                    txtSubCategoryName.Focus();
                }
                else
                {
                    Common.EnableTextBox(true, txtSubCategoryCode);
                    txtSubCategoryCode.Focus(); 
                }
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
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationSubCategory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Common.SetAutoComplete(txtSubCategoryCode, autoCompleteSubCategoryCode, chkAutoCompleationSubCategory.Checked);
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
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtSubCategoryCode.Focus();
                txtSubCategoryCode.SelectionStart = txtSubCategoryCode.Text.Length;
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
                InvCategoryService invCategoryService = new InvCategoryService();
                existingCategory = invCategoryService.GetInvCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory);
                if (existingCategory != null)
                { txtCategoryName.Text = existingCategory.CategoryName.Trim(); }
                else
                { }
                txtSubCategoryCode.Focus();
                txtSubCategoryCode.SelectionStart = txtSubCategoryCode.Text.Length;
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
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtSubCategoryCode.Focus();
                txtSubCategoryCode.SelectionStart = txtSubCategoryCode.Text.Length;
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
                InvCategoryService invCategoryService = new InvCategoryService();
                existingCategory = invCategoryService.GetInvCategoryByName(txtCategoryName.Text.Trim(), isDependSubCategory);
                if (existingCategory != null)
                { txtCategoryCode.Text = existingCategory.CategoryCode.Trim(); }
                else
                { }
                txtSubCategoryCode.Focus();
                txtSubCategoryCode.SelectionStart = txtSubCategoryCode.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    DataView dvAllReferenceData = new DataView(invSubCategoryService.GetSubCategoriesDataTable(isDependSubCategory2));
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtSubCategoryCode);
                        txtSubCategoryCode_Leave(this, e);
                    }
                }

                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtSubCategoryName.Focus();
                txtSubCategoryName.SelectionStart = txtSubCategoryName.Text.Length;
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

        private void txtSubCategoryCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSubCategoryCode.Text.Trim()))
                { return; }
            
                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                existingSubCategory = invSubCategoryService.GetInvSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2);

                if (existingSubCategory != null)
                {
                    if (isDependSubCategory)
                    {
                        InvCategoryService CategoryService = new InvCategoryService();
                        existingCategory = CategoryService.GetInvCategoryByID(existingSubCategory.InvCategoryID, isDependSubCategory);

                        txtCategoryCode.Text = existingCategory.CategoryCode.Trim();
                        txtCategoryName.Text = existingCategory.CategoryName.Trim();
                        txtSubCategoryCode.Text = existingSubCategory.SubCategoryCode.Trim();
                        txtSubCategoryName.Text = existingSubCategory.SubCategoryName.Trim();
                        txtRemark.Text = existingSubCategory.Remark.Trim();
                    }
                    else
                    {
                        txtSubCategoryCode.Text = existingSubCategory.SubCategoryCode.Trim();
                        txtSubCategoryName.Text = existingSubCategory.SubCategoryName.Trim();
                        txtRemark.Text = existingSubCategory.Remark.Trim();
                    }

                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                    Common.EnableButton(false, btnNew);
                }
                else
                {
                    if (chkAutoClear.Checked)
                    { Common.ClearTextBox(txtSubCategoryName, txtRemark); }

                    if (btnNew.Enabled)
                    {
                        if (Toast.Show("" + this.Text + " - " + txtSubCategoryCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                        { btnNew.PerformClick(); }
                    }
                }

                if (btnSave.Enabled)
                { 
                    Common.EnableTextBox(false, txtSubCategoryCode); 
                }

                txtSubCategoryName.Focus();
                txtSubCategoryName.SelectionStart = txtSubCategoryName.Text.Length;
            
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    DataView dvAllReferenceData = new DataView(invSubCategoryService.GetSubCategoriesDataTable(isDependSubCategory2));
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtSubCategoryCode);
                        txtSubCategoryCode_Leave(this, e);
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

        private void txtSubCategoryName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                { return; }

                if (string.IsNullOrEmpty(txtSubCategoryName.Text.Trim()))
                {
                    return;
                }
                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                existingSubCategory = invSubCategoryService.GetInvSubCategoryByName(txtSubCategoryName.Text.Trim(), isDependSubCategory2);

                if (existingSubCategory != null)
                {
                    if (isDependSubCategory)
                    {
                        InvCategoryService invCategoryService = new InvCategoryService();
                        existingCategory = invCategoryService.GetInvCategoryByID(existingSubCategory.InvCategoryID, isDependSubCategory);

                        txtCategoryCode.Text = existingCategory.CategoryCode.Trim();
                        txtCategoryName.Text = existingCategory.CategoryName.Trim();
                        txtSubCategoryCode.Text = existingSubCategory.SubCategoryCode.Trim();
                        txtSubCategoryName.Text = existingSubCategory.SubCategoryName.Trim();
                        txtRemark.Text = existingSubCategory.Remark.Trim();
                    }
                    else
                    {
                        txtSubCategoryCode.Text = existingSubCategory.SubCategoryCode.Trim();
                        txtSubCategoryName.Text = existingSubCategory.SubCategoryName.Trim();
                        txtRemark.Text = existingSubCategory.Remark.Trim();
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
                { Common.EnableTextBox(false, txtSubCategoryCode, txtCategoryCode, txtCategoryName); }

                txtRemark.Focus();
                txtRemark.SelectionStart = txtRemark.Text.Length;
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
                #region SubCategories auto load
                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                autoCompleteSubCategoryCode = new AutoCompleteStringCollection();
                autoCompleteSubCategoryCode.AddRange(invSubCategoryService.GetInvSubCategoryCodes(isDependSubCategory2));
                Common.SetAutoComplete(txtSubCategoryCode, autoCompleteSubCategoryCode, chkAutoCompleationSubCategory.Checked);

                ////Sub category name auto load
                autoCompleteSubCategoryName = new AutoCompleteStringCollection();
                autoCompleteSubCategoryName.AddRange(invSubCategoryService.GetInvSubCategoryNames(isDependSubCategory2));
                Common.SetAutoComplete(txtSubCategoryName, autoCompleteSubCategoryName, chkAutoCompleationSubCategory.Checked);
                ////
                #endregion

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete, btnView);
                Common.EnableTextBox(true, txtSubCategoryCode, txtCategoryCode, txtCategoryName);
                Common.ClearTextBox(txtSubCategoryCode);

                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                if (isDependSubCategory)
                {
                    #region Categories auto load

                    InvCategoryService invCategoryService = new InvCategoryService();
                    autoCompleteCategoryCode = new AutoCompleteStringCollection();
                    autoCompleteCategoryCode.AddRange(invCategoryService.GetInvCategoryCodes(isDependSubCategory));
                    Common.SetAutoComplete(txtCategoryCode, autoCompleteCategoryCode, chkAutoCompleationCategory.Checked);

                    autoCompleteCategoryName = new AutoCompleteStringCollection();
                    autoCompleteCategoryName.AddRange(invCategoryService.GetInvCategoryNames(isDependSubCategory));
                    Common.SetAutoComplete(txtCategoryName, autoCompleteCategoryName, chkAutoCompleationCategory.Checked);

                    #endregion
                    ActiveControl = txtCategoryCode;
                    txtCategoryCode.Focus();
                }
                else
                {
                    ActiveControl = txtSubCategoryCode;
                    txtSubCategoryCode.Focus();
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
                if (ValidateControls().Equals(false))
                { return; }


                InvCategory invCategory = new InvCategory();
                InvSubCategory invSubCategory = new InvSubCategory();

                InvCategoryService invCategoryService = new InvCategoryService();
                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                bool isNew = false;

                // Assign values

                invCategory.CategoryCode = txtCategoryCode.Text.Trim();
                invSubCategory.SubCategoryCode = txtSubCategoryCode.Text.Trim();

                // Check availability of Dapertment


                // Check availability of Category
                if (isDependSubCategory)
                {
                    existingCategory = invCategoryService.GetInvCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory);
                    if (existingCategory == null)
                    {
                        Toast.Show(lblCategory.Text + " " + txtCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtCategoryCode.Focus();
                        return;
                    }
                }

                // Check availability of Sub Category
                existingSubCategory = invSubCategoryService.GetInvSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2);
                if (existingSubCategory == null || existingSubCategory.InvSubCategoryID == 0)
                { existingSubCategory = new InvSubCategory(); }


                if (isDependSubCategory)
                    existingSubCategory.InvCategoryID = existingCategory.InvCategoryID;
                else
                {
                    existingSubCategory.InvCategoryID = 1;
                    if (existingSubCategory.InvSubCategoryID.Equals(1))
                        return;
                }

                existingSubCategory.SubCategoryCode = txtSubCategoryCode.Text.Trim();
                existingSubCategory.SubCategoryName = txtSubCategoryName.Text.Trim();
                existingSubCategory.Remark = txtRemark.Text.Trim();
                existingSubCategory.IsDelete = false;

                if (existingSubCategory.InvSubCategoryID.Equals(0))
                {
                    if ((Toast.Show("" + this.Text + " - " + existingSubCategory.SubCategoryCode + " - " + existingSubCategory.SubCategoryName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    { return; }
                    // Create new Sub Category
                    invSubCategoryService.AddInvSubCategory(existingSubCategory);

                    if ((Toast.Show("" + this.Text + " - " + existingSubCategory.SubCategoryCode + " - " + existingSubCategory.SubCategoryName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        if ((Toast.Show("" + this.Text + " - " + existingSubCategory.SubCategoryCode + " - " + existingSubCategory.SubCategoryName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        { return; }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingSubCategory.SubCategoryCode + " - " + existingSubCategory.SubCategoryName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("" + this.Text + " - " + existingSubCategory.SubCategoryCode + " - " + existingSubCategory.SubCategoryName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        { return; }
                    }
                    // Update Sub Category deatils
                    invSubCategoryService.UpdateInvSubCategory(existingSubCategory);
                    if (chkAutoClear.Checked)
                    { ClearForm(); }
                    else
                    { InitializeForm(); }

                    // Display updated iformation 
                    Toast.Show("" + this.Text + " - " + existingSubCategory.SubCategoryCode + " - " + existingSubCategory.SubCategoryName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }

                txtSubCategoryCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// Delete Sub Category(Change Delete status of relevent Sub Category)
        /// </summary>
        public override void Delete()
        {
            try
            {
                if (Toast.Show("" + this.Text + " - " + existingSubCategory.SubCategoryCode + " - " + existingSubCategory.SubCategoryName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                { return; }
                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                existingSubCategory = invSubCategoryService.GetInvSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2);
                if (existingSubCategory != null && existingSubCategory.InvSubCategoryID != 0)
                {
                    existingSubCategory.IsDelete = true;
                    invSubCategoryService.UpdateInvSubCategory(existingSubCategory);
                    MessageBox.Show("" + this.Text + " - " + existingSubCategory.SubCategoryCode.Trim() + " successfully deleted", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    txtSubCategoryCode.Focus();
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
                //string ReprotName = "Sub Category";
                //CryInvColumn5Template cryInvTemplate = new CryInvColumn5Template();
                //int x;

                //string categoryText = "", subCategoryText = "";

                //categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
                //subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;

                //if (isDependSubCategory)
                //{
                //    string[] stringfield = { "Code", subCategoryText, categoryText, "Remark" };
                //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
                //    //                                                               mdiService.GetAllCategoryWiseSubCategoryDataTable(),
                //    //                                                               0, cryInvTemplate);
                //    //frmReprotGenerator.ShowDialog();
                //}
                //else
                //{
                //    string[] stringfield = { "Code", subCategoryText, "Remark" };
                //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
                //    //                                                               mdiService.GetAllSubCategoryDataTable(),
                //    //                                                               0, cryInvTemplate);
                //    //frmReprotGenerator.ShowDialog();
                //}
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory");
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
            if (isDependSubCategory)
                return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty,  txtCategoryCode, txtSubCategoryCode, txtSubCategoryName);
            else
                return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty,  txtSubCategoryCode, txtSubCategoryName);

        }
        #endregion

        

        

    }
}
