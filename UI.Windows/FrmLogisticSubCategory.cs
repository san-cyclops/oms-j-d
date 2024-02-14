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
    public partial class FrmLogisticSubCategory : FrmBaseMasterForm
    {
        private LgsCategory existingLgsCategory;
        private LgsSubCategory existingLgsSubCategory;
        AutoCompleteStringCollection autoCompleteLgsCategoryCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteLgsCategoryName = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteLgsSubCategoryCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteLgsSubCategoryName = new AutoCompleteStringCollection();
        bool isDependLgsSubCategory = false;
        bool isDependLgsSubCategory2 = false;

        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        public FrmLogisticSubCategory()
        {
            InitializeComponent();
        }

        private void FrmLogisticSubCategory_Load(object sender, EventArgs e)
        {

        }

        #region Override Methods....

        public override void FormLoad()
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                isDependLgsSubCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).IsDepend;
                this.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).FormText.Trim();
                lblSubCategoryCode.Text = this.Text + " " + lblSubCategoryCode.Text;
                lblSubCategoryName.Text = this.Text + " " + lblSubCategoryName.Text;
                lblCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").FormText;
                isDependLgsSubCategory2 = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory2").IsDepend;

                if (!isDependLgsSubCategory)
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

        public override void InitializeForm()
        {
            try
            {
                LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
                autoCompleteLgsSubCategoryCode = new AutoCompleteStringCollection();
                autoCompleteLgsSubCategoryCode.AddRange(lgsSubCategoryService.GetAllLgsSubCategoryCodes(isDependLgsSubCategory2));
                Common.SetAutoComplete(txtSubCategoryCode, autoCompleteLgsSubCategoryCode, chkAutoCompleationSubCategory.Checked);

                autoCompleteLgsSubCategoryName = new AutoCompleteStringCollection();
                autoCompleteLgsSubCategoryName.AddRange(lgsSubCategoryService.GetAllLgsSubCategoryNames(isDependLgsSubCategory2));
                Common.SetAutoComplete(txtSubCategoryName, autoCompleteLgsSubCategoryName, chkAutoCompleationSubCategory.Checked);

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtSubCategoryCode, txtCategoryCode, txtCategoryName);
                Common.ClearTextBox(txtSubCategoryCode);

                if (isDependLgsSubCategory)
                {
                    LgsCategoryService lgsCategoryService = new LgsCategoryService();
                    autoCompleteLgsCategoryCode = new AutoCompleteStringCollection();
                    autoCompleteLgsCategoryCode.AddRange(lgsCategoryService.GetAllLgsCategoryCodes(isDependLgsSubCategory));
                    Common.SetAutoComplete(txtCategoryCode, autoCompleteLgsCategoryCode, chkAutoCompleationCategory.Checked);

                    autoCompleteLgsCategoryName = new AutoCompleteStringCollection();
                    autoCompleteLgsCategoryName.AddRange(lgsCategoryService.GetAllLgsCategoryNames(isDependLgsSubCategory));
                    Common.SetAutoComplete(txtCategoryName, autoCompleteLgsCategoryName, chkAutoCompleationCategory.Checked);

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

                LgsCategory lgsCategory = new LgsCategory();
                LgsSubCategory lgsSubCategory = new LgsSubCategory();

                LgsCategoryService lgsCategoryService = new LgsCategoryService();
                LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
                bool isNew = false;

                // Assign values

                lgsCategory.CategoryCode = txtCategoryCode.Text.Trim();
                lgsSubCategory.SubCategoryCode = txtSubCategoryCode.Text.Trim();

                // Check availability of Category
                if (isDependLgsSubCategory)
                {
                    existingLgsCategory = lgsCategoryService.GetLgsCategoryByCode(txtCategoryCode.Text.Trim(), isDependLgsSubCategory);
                    if (existingLgsCategory == null)
                    {
                        Toast.Show(lblCategory.Text + " " + txtCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtCategoryCode.Focus();
                        return;
                    }
                }

                // Check availability of Sub Category
                existingLgsSubCategory = lgsSubCategoryService.GetLgsSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependLgsSubCategory2);
                if (existingLgsSubCategory == null || existingLgsSubCategory.LgsSubCategoryID == 0)
                { existingLgsSubCategory = new LgsSubCategory(); }


                if (isDependLgsSubCategory)
                    existingLgsSubCategory.LgsCategoryID = existingLgsCategory.LgsCategoryID;
                else
                {
                    existingLgsSubCategory.LgsCategoryID = 1;
                    if (existingLgsSubCategory.LgsSubCategoryID.Equals(1))
                        return;
                }

                existingLgsSubCategory.SubCategoryCode = txtSubCategoryCode.Text.Trim();
                existingLgsSubCategory.SubCategoryName = txtSubCategoryName.Text.Trim();
                existingLgsSubCategory.Remark = txtRemark.Text.Trim();
                existingLgsSubCategory.IsDelete = false;

                if (existingLgsSubCategory.LgsSubCategoryID.Equals(0))
                {
                    if ((Toast.Show("" + this.Text + " - " + existingLgsSubCategory.SubCategoryCode + " - " + existingLgsSubCategory.SubCategoryName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    { return; }
                    // Create new Sub Category
                    lgsSubCategoryService.AddLgsSubCategory(existingLgsSubCategory);

                    if ((Toast.Show("" + this.Text + " - " + existingLgsSubCategory.SubCategoryCode + " - " + existingLgsSubCategory.SubCategoryName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        if ((Toast.Show("" + this.Text + " - " + existingLgsSubCategory.SubCategoryCode + " - " + existingLgsSubCategory.SubCategoryName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        { return; }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingLgsSubCategory.SubCategoryCode + " - " + existingLgsSubCategory.SubCategoryName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("" + this.Text + " - " + existingLgsSubCategory.SubCategoryCode + " - " + existingLgsSubCategory.SubCategoryName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        { return; }
                    }
                    // Update Sub Category deatils
                    lgsSubCategoryService.UpdateLgsSubCategory(existingLgsSubCategory);
                    if (chkAutoClear.Checked)
                    { ClearForm(); }
                    else
                    { InitializeForm(); }

                    // Display updated iformation 
                    Toast.Show("" + this.Text + " - " + existingLgsSubCategory.SubCategoryCode + " - " + existingLgsSubCategory.SubCategoryName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }

                txtSubCategoryCode.Focus();
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
                if (Toast.Show("" + this.Text + " - " + existingLgsSubCategory.SubCategoryCode + " - " + existingLgsSubCategory.SubCategoryName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                { return; }
                LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
                existingLgsSubCategory = lgsSubCategoryService.GetLgsSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependLgsSubCategory2);
                if (existingLgsSubCategory != null && existingLgsSubCategory.LgsSubCategoryID != 0)
                {
                    lgsSubCategoryService.DeleteLgsSubCategory(existingLgsSubCategory);
                    MessageBox.Show("" + this.Text + " - " + existingLgsSubCategory.SubCategoryCode.Trim() + " successfully deleted", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    txtSubCategoryCode.Focus();
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
                //string ReprotName = "Sub Category";
                //CryLgsColumn5Template cryLgsTemplate = new CryLgsColumn5Template();
                //int x;

                //string categoryText = "", subCategoryText = "";

                //categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").FormText;
                //subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").FormText;

                //if (isDependLgsSubCategory)
                //{
                //    string[] stringfield = { "Code", subCategoryText, categoryText, "Remark" };
                //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
                //    //                                                               mdiService.GetAllLgsCategoryWiseSubCategoryDataTable(),
                //    //                                                               0, cryLgsTemplate);
                //    //frmReprotGenerator.ShowDialog();
                //}
                //else
                //{
                //    //string[] stringfield = { "Code", subCategoryText, "Remark" };
                //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
                //    //                                                               mdiService.GetAllLgsSubCategoryDataTable(),
                //    //                                                               0, cryLgsTemplate);
                //    //frmReprotGenerator.ShowDialog();
                //}
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory");
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
                Common.EnableButton(true, btnSave);

                if (isDependLgsSubCategory)
                {
                    LgsCategoryService lgsCategoryService = new LgsCategoryService();
                    existingLgsCategory = lgsCategoryService.GetLgsCategoryByCode(txtCategoryCode.Text.Trim(), isDependLgsSubCategory);
                    if (existingLgsCategory == null)
                    {
                        Toast.Show(lblCategory.Text + " " + txtCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtCategoryCode.Focus();
                        ClearForm();
                        return;
                    }
                }

                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();

                    if (!isDependLgsSubCategory)
                        txtSubCategoryCode.Text = lgsSubCategoryService.GetNewCode(this.Name, 1, "", isDependLgsSubCategory2);
                    else
                        txtSubCategoryCode.Text = lgsSubCategoryService.GetNewCode(this.Name, existingLgsCategory.LgsCategoryID, existingLgsCategory.CategoryCode, isDependLgsSubCategory2);


                    Common.EnableTextBox(false, txtSubCategoryCode, txtCategoryCode, txtCategoryName);
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

        #endregion

        #region Key Down and Leave Events....

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
                LgsCategoryService lgsCategoryService = new LgsCategoryService();
                existingLgsCategory = lgsCategoryService.GetLgsCategoryByCode(txtCategoryCode.Text.Trim(), isDependLgsSubCategory);
                if (existingLgsCategory != null)
                { txtCategoryName.Text = existingLgsCategory.CategoryName.Trim(); }
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
                LgsCategoryService lgsCategoryService = new LgsCategoryService();
                existingLgsCategory = lgsCategoryService.GetLgsCategoryByName(txtCategoryName.Text.Trim(), isDependLgsSubCategory);
                if (existingLgsCategory != null)
                { txtCategoryCode.Text = existingLgsCategory.CategoryCode.Trim(); }
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

        private void txtSubCategoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
                    DataView dvAllReferenceData = new DataView(lgsSubCategoryService.GetSubCategoriesDataTable(isDependLgsSubCategory2));
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

        private void txtSubCategoryCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSubCategoryCode.Text.Trim()))
                { return; }

                LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
                existingLgsSubCategory = lgsSubCategoryService.GetLgsSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependLgsSubCategory2);

                if (existingLgsSubCategory != null)
                {
                    if (isDependLgsSubCategory)
                    {
                        LgsCategoryService lgsCategoryService = new LgsCategoryService();
                        existingLgsCategory = lgsCategoryService.GetLgsCategoryByID(existingLgsSubCategory.LgsCategoryID, isDependLgsSubCategory);

                        txtCategoryCode.Text = existingLgsCategory.CategoryCode.Trim();
                        txtCategoryName.Text = existingLgsCategory.CategoryName.Trim();
                        txtSubCategoryCode.Text = existingLgsSubCategory.SubCategoryCode.Trim();
                        txtSubCategoryName.Text = existingLgsSubCategory.SubCategoryName.Trim();
                        txtRemark.Text = existingLgsSubCategory.Remark.Trim();
                    }
                    else
                    {
                        txtSubCategoryCode.Text = existingLgsSubCategory.SubCategoryCode.Trim();
                        txtSubCategoryName.Text = existingLgsSubCategory.SubCategoryName.Trim();
                        txtRemark.Text = existingLgsSubCategory.Remark.Trim();
                    }

                    Common.EnableButton(true, btnSave, btnDelete);
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
                    LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
                    DataView dvAllReferenceData = new DataView(lgsSubCategoryService.GetSubCategoriesDataTable(isDependLgsSubCategory2));
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
                LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
                existingLgsSubCategory = lgsSubCategoryService.GetLgsSubCategoryByName(txtSubCategoryName.Text.Trim(), isDependLgsSubCategory2);

                if (existingLgsSubCategory != null)
                {
                    if (isDependLgsSubCategory)
                    {
                        LgsCategoryService lgsCategoryService = new LgsCategoryService();
                        existingLgsCategory = lgsCategoryService.GetLgsCategoryByID(existingLgsSubCategory.LgsCategoryID, isDependLgsSubCategory);

                        txtCategoryCode.Text = existingLgsCategory.CategoryCode.Trim();
                        txtCategoryName.Text = existingLgsCategory.CategoryName.Trim();
                        txtSubCategoryCode.Text = existingLgsSubCategory.SubCategoryCode.Trim();
                        txtSubCategoryName.Text = existingLgsSubCategory.SubCategoryName.Trim();
                        txtRemark.Text = existingLgsSubCategory.Remark.Trim();
                    }
                    else
                    {
                        txtSubCategoryCode.Text = existingLgsSubCategory.SubCategoryCode.Trim();
                        txtSubCategoryName.Text = existingLgsSubCategory.SubCategoryName.Trim();
                        txtRemark.Text = existingLgsSubCategory.Remark.Trim();
                    }

                    Common.EnableButton(true, btnSave, btnDelete);
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

        #region Methods....

        private bool ValidateControls()
        {
            if (isDependLgsSubCategory)
                return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtCategoryCode, txtSubCategoryCode, txtSubCategoryName);
            else
                return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtSubCategoryCode, txtSubCategoryName);
        }

        private void chkAutoCompleationCategory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Common.SetAutoComplete(txtCategoryCode, autoCompleteLgsCategoryCode, chkAutoCompleationCategory.Checked);
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
                Common.SetAutoComplete(txtSubCategoryCode, autoCompleteLgsSubCategoryCode, chkAutoCompleationSubCategory.Checked);
                Common.SetAutoComplete(txtSubCategoryName, autoCompleteLgsSubCategoryName, chkAutoCompleationSubCategory.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        

        
    }
}
