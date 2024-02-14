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
    public partial class FrmSubCategory2 : UI.Windows.FrmBaseMasterForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        private InvSubCategory existingSubCategory;
        private InvSubCategory2 existingSubCategory2;
        AutoCompleteStringCollection autoCompleteSubCategoryCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteSubCategoryName = new AutoCompleteStringCollection(); 
        AutoCompleteStringCollection autoCompleteSubCategory2Code = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteSubCategory2Name = new AutoCompleteStringCollection();
        bool isDependSubCategory2 = false;
        int documentID = 0;


        public FrmSubCategory2()
        {
            InitializeComponent();
        }

        private void FrmSubCategory2_Load(object sender, EventArgs e)
        {
            
        }

        public override void FormLoad()
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                isDependSubCategory2 = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).IsDepend;

                this.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).FormText.Trim();
                lblSubCategory2Code.Text = this.Text + " " + lblSubCategory2Code.Text;
                lblSubCategory2Name.Text = this.Text + " " + lblSubCategory2Name.Text;
                lblCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;

                if (!isDependSubCategory2)
                {
                    txtSubCategoryCode.Visible = false;
                    txtSubCategoryName.Visible = false;
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Common.EnableButton(false, btnNew, btnDelete);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

                if (isDependSubCategory2)
                {
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    existingSubCategory = invSubCategoryService.GetInvSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2);
                    if (existingSubCategory == null)
                    {
                        Toast.Show(lblCategory.Text + " " + txtSubCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtSubCategoryCode.Focus();
                        ClearForm();
                        return;
                    }
                }

                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();

                    if (!isDependSubCategory2)
                        txtSubCategory2Code.Text = invSubCategory2Service.GetNewCode(this.Name, 1, "");
                    else
                        txtSubCategory2Code.Text = invSubCategory2Service.GetNewCode(this.Name, existingSubCategory.InvSubCategoryID, existingSubCategory.SubCategoryCode);

                    Common.EnableTextBox(false, txtSubCategory2Code,txtSubCategoryCode,txtSubCategoryName);
                    txtSubCategory2Name.Focus();
                }
                else
                {
                    Common.EnableTextBox(true, txtSubCategory2Code);
                    txtSubCategory2Code.Focus(); 
                }
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
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtSubCategory2Code.Focus();
                txtSubCategory2Code.SelectionStart = txtSubCategory2Code.Text.Length;
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
                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                existingSubCategory = invSubCategoryService.GetInvSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2);
                if (existingSubCategory != null)
                { txtSubCategoryName.Text = existingSubCategory.SubCategoryName.Trim(); }
                else
                { }
                txtSubCategory2Code.Focus();
                txtSubCategory2Code.SelectionStart = txtSubCategory2Code.Text.Length;
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
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtSubCategory2Code.Focus();
                txtSubCategory2Code.SelectionStart = txtSubCategory2Code.Text.Length;
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
                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                existingSubCategory = invSubCategoryService.GetInvSubCategoryByName(txtSubCategoryName.Text.Trim(), isDependSubCategory2);
                if (existingSubCategory != null)
                { txtSubCategoryCode.Text = existingSubCategory.SubCategoryCode.Trim(); }
                else
                { }
                txtSubCategory2Code.Focus();
                txtSubCategory2Code.SelectionStart = txtSubCategory2Code.Text.Length;
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

        private void txtSubCategory2Code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    DataView dvAllReferenceData = new DataView(invSubCategory2Service.GetInvSubCategories2DataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtSubCategory2Code);
                    }
                }

                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtSubCategory2Name.Focus();
                txtSubCategory2Name.SelectionStart = txtSubCategory2Name.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategory2Code_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSubCategory2Code.Text.Trim()))
                { return; }

                InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                existingSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByCode(txtSubCategory2Code.Text.Trim());

                if (existingSubCategory2 != null)
                {
                    if (isDependSubCategory2)
                    {
                        InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                        existingSubCategory = invSubCategoryService.GetInvSubCategoryByID(existingSubCategory2.InvSubCategoryID, isDependSubCategory2);

                        txtSubCategoryCode.Text = existingSubCategory.SubCategoryCode.Trim();
                        txtSubCategoryName.Text = existingSubCategory.SubCategoryName.Trim();
                        txtSubCategory2Code.Text = existingSubCategory2.SubCategory2Code.Trim();
                        txtSubCategory2Name.Text = existingSubCategory2.SubCategory2Name.Trim();
                        txtRemark.Text = existingSubCategory2.Remark.Trim();
                    }
                    else
                    {
                        txtSubCategory2Code.Text = existingSubCategory2.SubCategory2Code.Trim();
                        txtSubCategory2Name.Text = existingSubCategory2.SubCategory2Name.Trim();
                        txtRemark.Text = existingSubCategory2.Remark.Trim();
                    }

                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    Common.EnableButton(true, btnDelete);
                    Common.EnableButton(false, btnNew); 
                }
                else
                {
                    if (chkAutoClear.Checked)
                    { Common.ClearTextBox(txtRemark); }

                    if (btnNew.Enabled)
                    {
                        if (Toast.Show("" + this.Text + " - " + txtSubCategory2Code.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                        { btnNew.PerformClick(); }
                    }
                }

                if (btnSave.Enabled)
                { 
                    Common.EnableTextBox(false, txtSubCategory2Code); 
                }

                txtSubCategory2Name.Focus();
                txtSubCategory2Name.SelectionStart = txtSubCategory2Name.Text.Length;

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategory2Name_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    DataView dvAllReferenceData = new DataView(invSubCategory2Service.GetInvSubCategories2DataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtSubCategory2Code);
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

        private void txtSubCategory2Name_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                { return; }

                if (string.IsNullOrEmpty(txtSubCategory2Name.Text.Trim()))
                { return; }

                InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                existingSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByName(txtSubCategory2Name.Text.Trim());

                if (existingSubCategory2 != null)
                {
                    if (isDependSubCategory2)
                    {
                        InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                        existingSubCategory = invSubCategoryService.GetInvSubCategoryByID(existingSubCategory2.InvSubCategoryID, isDependSubCategory2);

                        txtSubCategoryCode.Text = existingSubCategory.SubCategoryCode.Trim();
                        txtSubCategoryName.Text = existingSubCategory.SubCategoryName.Trim();
                        txtSubCategory2Code.Text = existingSubCategory2.SubCategory2Code.Trim();
                        txtSubCategory2Name.Text = existingSubCategory2.SubCategory2Name.Trim();
                        txtRemark.Text = existingSubCategory2.Remark.Trim();
                    }
                    else
                    {
                        txtSubCategory2Code.Text = existingSubCategory2.SubCategory2Code.Trim();
                        txtSubCategory2Name.Text = existingSubCategory2.SubCategory2Name.Trim();
                        txtRemark.Text = existingSubCategory2.Remark.Trim();
                    }

                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                    Common.EnableButton(false, btnNew);
                }
                else
                {
                    Toast.Show("" + this.Text + " - " + txtSubCategory2Name.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    if (chkAutoClear.Checked)
                    { Common.ClearTextBox(txtRemark); }
                }

                if (btnSave.Enabled)
                {
                    Common.EnableTextBox(false, txtSubCategory2Code);
                }

                txtRemark.Focus();
                txtRemark.SelectionStart = txtRemark.Text.Length;

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
                InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                autoCompleteSubCategory2Code = new AutoCompleteStringCollection();
                autoCompleteSubCategory2Code.AddRange(invSubCategory2Service.GetInvSubCategory2Codes());
                Common.SetAutoComplete(txtSubCategory2Code, autoCompleteSubCategory2Code, chkAutoCompleationSubCategory2.Checked);

                autoCompleteSubCategory2Name = new AutoCompleteStringCollection();
                autoCompleteSubCategory2Name.AddRange(invSubCategory2Service.GetInvSubCategory2Names());
                Common.SetAutoComplete(txtSubCategory2Name, autoCompleteSubCategory2Name, chkAutoCompleationSubCategory2.Checked);

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete, btnView);
                Common.EnableTextBox(true, txtSubCategory2Code, txtSubCategoryCode, txtSubCategoryName);
                Common.ClearTextBox(txtSubCategory2Code);

                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                if (isDependSubCategory2)
                {
                    #region Categories auto load

                    InvSubCategoryService InvSubCategoryService = new InvSubCategoryService();
                    autoCompleteSubCategoryCode = new AutoCompleteStringCollection();
                    autoCompleteSubCategoryCode.AddRange(InvSubCategoryService.GetInvSubCategoryCodes(isDependSubCategory2));
                    Common.SetAutoComplete(txtSubCategoryCode, autoCompleteSubCategoryCode, chkAutoCompleationCategory.Checked);

                    autoCompleteSubCategoryName = new AutoCompleteStringCollection();
                    autoCompleteSubCategoryName.AddRange(InvSubCategoryService.GetInvSubCategoryNames(isDependSubCategory2));
                    Common.SetAutoComplete(txtSubCategoryName, autoCompleteSubCategoryName, chkAutoCompleationCategory.Checked);

                    #endregion
                    ActiveControl = txtSubCategoryCode;
                    txtSubCategoryCode.Focus();
                }
                else
                {
                    ActiveControl = txtSubCategory2Code;
                    txtSubCategory2Code.Focus();
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationSubCategory2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                autoCompleteSubCategory2Code = new AutoCompleteStringCollection();
                autoCompleteSubCategory2Code.AddRange(invSubCategory2Service.GetInvSubCategory2Codes());
                Common.SetAutoComplete(txtSubCategory2Code, autoCompleteSubCategory2Code, chkAutoCompleationSubCategory2.Checked);
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
                if (isDependSubCategory2)
                {
                    #region Categories auto load
                    InvSubCategoryService InvSubCategoryService = new InvSubCategoryService();
                    autoCompleteSubCategoryCode = new AutoCompleteStringCollection();
                    autoCompleteSubCategoryCode.AddRange(InvSubCategoryService.GetInvSubCategoryCodes(isDependSubCategory2));
                    Common.SetAutoComplete(txtSubCategoryCode, autoCompleteSubCategoryCode, chkAutoCompleationCategory.Checked);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private bool ValidateControls()
        {
            if (isDependSubCategory2)
                return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtSubCategoryCode, txtSubCategory2Code, txtSubCategory2Name);
            else
                return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtSubCategory2Code, txtSubCategory2Name);

        }

        public override void Save()
        {
            try
            {
                if (ValidateControls().Equals(false))
                { return; }


                InvSubCategory invSubCategory = new InvSubCategory();
                InvSubCategory2 invSubCategory2 = new InvSubCategory2();

                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                bool isNew = false;

                // Check availability of Category
                if (isDependSubCategory2)
                {
                    existingSubCategory = invSubCategoryService.GetInvSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2);
                    if (existingSubCategory == null)
                    {
                        Toast.Show(lblCategory.Text + " " + txtSubCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtSubCategoryCode.Focus();
                        return;
                    }
                }

                // Check availability of Sub Category
                existingSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByCode(txtSubCategory2Code.Text.Trim());
                if (existingSubCategory2 == null || existingSubCategory2.InvSubCategory2ID == 0)
                { existingSubCategory2 = new InvSubCategory2(); }


                if (isDependSubCategory2)
                    existingSubCategory2.InvSubCategoryID = existingSubCategory.InvSubCategoryID;
                else
                {
                    existingSubCategory2.InvSubCategoryID = 1;

                }

                existingSubCategory2.SubCategory2Code = txtSubCategory2Code.Text.Trim();
                existingSubCategory2.SubCategory2Name = txtSubCategory2Name.Text.Trim();
                existingSubCategory2.Remark = txtRemark.Text.Trim();
                existingSubCategory2.IsDelete = false;

                if (existingSubCategory2.InvSubCategory2ID.Equals(0))
                {
                    if ((Toast.Show("" + this.Text + " - " + existingSubCategory2.SubCategory2Code + " - " + existingSubCategory2.SubCategory2Name + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    { return; }
                    // Create new Sub Category
                    invSubCategory2Service.AddInvSubCategory2(existingSubCategory2);

                    if ((Toast.Show("" + this.Text + " - " + existingSubCategory2.SubCategory2Code + " - " + existingSubCategory2.SubCategory2Name + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        if ((Toast.Show("" + this.Text + " - " + existingSubCategory2.SubCategory2Code + " - " + existingSubCategory2.SubCategory2Name + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        { return; }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingSubCategory2.SubCategory2Code + " - " + existingSubCategory2.SubCategory2Name + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("" + this.Text + " - " + existingSubCategory2.SubCategory2Code + " - " + existingSubCategory2.SubCategory2Name + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        { return; }
                    }
                    // Update Sub Category deatils
                    invSubCategory2Service.UpdateInvSubCategory2(existingSubCategory2);
                    if (chkAutoClear.Checked)
                    { ClearForm(); }
                    else
                    { InitializeForm(); }

                    // Display updated iformation 
                    Toast.Show("" + this.Text + " - " + existingSubCategory2.SubCategory2Code + " - " + existingSubCategory2.SubCategory2Name + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }

                txtSubCategory2Code.Focus();
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
                if (Toast.Show("" + this.Text + " - " + existingSubCategory2.SubCategory2Code + " - " + existingSubCategory2.SubCategory2Name + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                { return; }
                InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                existingSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByCode(txtSubCategory2Code.Text.Trim());
                if (existingSubCategory2 != null && existingSubCategory2.InvSubCategory2ID != 0)
                {
                    existingSubCategory2.IsDelete = true;
                    invSubCategory2Service.UpdateInvSubCategory2(existingSubCategory2);
                    MessageBox.Show("" + this.Text + " - " + existingSubCategory2.SubCategory2Code.Trim() + " successfully deleted", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InitializeForm();
                    ClearForm();
                    txtSubCategory2Code.Focus();
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
                //string ReprotName = "Sub Category 02";
                //CryInvColumn5Template cryInvTemplate = new CryInvColumn5Template();
                //int x;

                //string subCategoryText = "", subCategory2Text = "";

                //subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
                //subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;

                //if (isDependSubCategory2)
                //{
                //    string[] stringfield = { "Code", subCategory2Text, subCategoryText, "Remark" };
                //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
                //    //                                                               mdiService.GetAllSubCategoryWiseSub2CategoryDataTable(),
                //    //                                                               0, cryInvTemplate);
                //    //frmReprotGenerator.ShowDialog();
                //}
                //else
                //{
                //    string[] stringfield = { "Code", subCategory2Text, "Remark" };
                //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
                //    //                                                               mdiService.GetAllSub2CategoryDataTable(),
                //    //                                                               0, cryInvTemplate);
                //    //frmReprotGenerator.ShowDialog();
                //}

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2");
                InvReportGenerator invReportGenerator = new InvReportGenerator();
                invReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
