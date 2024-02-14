using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;
using Utility;
using Service;
using System.Reflection;

namespace UI.Windows
{
    /// <summary>
    /// Developed by - asanka
    /// </summary>
    
    public partial class FrmProductExtendedProperty : UI.Windows.FrmBaseMasterForm
    {
        ErrorMessage errorMessage = new ErrorMessage();
        private InvProductExtendedProperty existingProductExtendedProperty;
        private InvProductExtendedProperty existingParentProperty;
        AutoCompleteStringCollection autoCompleteProductExtendedPropertyCode = new AutoCompleteStringCollection();
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        public FrmProductExtendedProperty()
        {
            InitializeComponent();
        }

        #region Form Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmProductExtendedProperty_Load(object sender, EventArgs e)
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                chkAutoCompleationExtendedProperty.Checked = true; // Change this properties according on user profile.
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

                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                    txtPropertyCode.Text = invProductExtendedPropertyService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength);
                    Common.EnableTextBox(false, txtPropertyCode);
                    txtPropertyName.Focus();
                }
                else
                { 
                    Common.EnableTextBox(true, txtPropertyCode);
                    txtPropertyCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPropertyCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                    DataView dvAllReferenceData = new DataView(invProductExtendedPropertyService.GetExtendedPropertyDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtPropertyCode);
                        txtPropertyCode_Leave(this, e);
                    }
                }

                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtPropertyName.Focus();
                txtPropertyName.SelectionStart = txtPropertyName.Text.Length;
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

        private void txtPropertyCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPropertyCode.Text.Trim()))
                { return; }
                InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                existingProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByCode(txtPropertyCode.Text.Trim());

                if (existingProductExtendedProperty!= null)
                {
                    txtPropertyCode.Text = existingProductExtendedProperty.ExtendedPropertyCode.Trim();
                    txtPropertyName.Text = existingProductExtendedProperty.ExtendedPropertyName.Trim();
                    cmbProertyType.Text = existingProductExtendedProperty.DataType.Trim();
                    cmbParent.Text = existingProductExtendedProperty.Parent.Trim();
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                    Common.EnableButton(false, btnNew);
                }
                else
                {
                    if (chkAutoClear.Checked)
                    { Common.ClearTextBox(txtPropertyName); }

                    if (btnNew.Enabled)
                    {
                        if (Toast.Show("Product Extended Property - " + txtPropertyCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                        { btnNew.PerformClick(); }
                    }
                }

                if (btnSave.Enabled)
                { Common.EnableTextBox(false, txtPropertyCode); }

                txtPropertyName.Focus();            
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPropertyName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                    DataView dvAllReferenceData = new DataView(invProductExtendedPropertyService.GetExtendedPropertyDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtPropertyCode);
                        txtPropertyCode_Leave(this, e);
                    }
                }

                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                cmbProertyType.Focus();
                cmbProertyType.SelectionStart = cmbProertyType.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationExtendedProperty_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                Common.SetAutoComplete(txtPropertyCode, invProductExtendedPropertyService.GetInvProductExtendedPropertyCodes(), chkAutoCompleationExtendedProperty.Checked);
                Common.SetAutoComplete(txtPropertyName, invProductExtendedPropertyService.GetInvProductExtendedPropertyNames(), chkAutoCompleationExtendedProperty.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbProertyType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                cmbParent.Focus();
                cmbParent.SelectionStart = cmbParent.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbProertyType_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    if (string.IsNullOrEmpty(cmbProertyType.Text.Trim()))
            //    { return; }

            //    if (IsProertyTypeExists())
            //    {
            //        cmbProertyType.Text = cmbProertyType.Text.Trim().ToUpper();
            //    }
            //    else { return; }
            //}
            //catch (Exception ex)
            //{
            //    Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbParent_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    if (string.IsNullOrEmpty(cmbParent.Text.Trim()))
            //    { return; }

            //    if (IsParentPropertyCodeExists())
            //    {
            //        cmbParent.Text = existingParentProperty.ExtendedPropertyCode.Trim();
            //    }
            //    else
            //    { return; }
            //}
            //catch (Exception ex)
            //{
            //    Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            //}
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
                InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                Common.SetAutoComplete(txtPropertyCode, invProductExtendedPropertyService.GetInvProductExtendedPropertyCodes(), chkAutoCompleationExtendedProperty.Checked);
                Common.SetAutoComplete(txtPropertyName, invProductExtendedPropertyService.GetInvProductExtendedPropertyNames(), chkAutoCompleationExtendedProperty.Checked);
                
                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtPropertyCode);
                Common.ClearTextBox(txtPropertyCode);

                cmbProertyType.DataSource = invProductExtendedPropertyService.GetPropertyTypes();

                cmbParent.DataSource = invProductExtendedPropertyService.GetInvProductExtendedPropertyCodes();


                cmbParent.SelectedIndex = -1;
                cmbProertyType.SelectedIndex = -1;

                ActiveControl = txtPropertyCode;
                txtPropertyCode.Focus();
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
        /// Save new Product Extended Property or Update existing Product Extended Property
        /// </summary>
        public override void Save()
        {
            try
            {
                if (!IsValidateControls())
                { return; }

                InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                bool isNew = false;
                
                existingProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByCode(txtPropertyCode.Text.Trim());

                if (existingProductExtendedProperty == null || existingProductExtendedProperty.InvProductExtendedPropertyID == 0)
                { existingProductExtendedProperty = new InvProductExtendedProperty(); }

                existingProductExtendedProperty.ExtendedPropertyCode = txtPropertyCode.Text.Trim();
                existingProductExtendedProperty.ExtendedPropertyName = txtPropertyName.Text.Trim();
                existingProductExtendedProperty.DataType = cmbProertyType.Text.Trim();
                existingProductExtendedProperty.Parent = cmbParent.Text.Trim();
                existingProductExtendedProperty.IsDelete = false;

                if (existingProductExtendedProperty.InvProductExtendedPropertyID.Equals(0))
                {
                    if ((Toast.Show("Product Extended Property - " + existingProductExtendedProperty.ExtendedPropertyCode + " - " + existingProductExtendedProperty.ExtendedPropertyName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    { return; }
                    // Create new Product Extended Property
                    invProductExtendedPropertyService.AddInvProductExtendedProperty(existingProductExtendedProperty);

                    if ((Toast.Show("Product Extended Property - " + existingProductExtendedProperty.ExtendedPropertyCode + " - " + existingProductExtendedProperty.ExtendedPropertyName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        { ClearForm(); }
                        else
                        { InitializeForm(); }
                    }
                }
                else
                {
                    if (isNew)
                    {
                        if ((Toast.Show("Product Extended Property - " + existingProductExtendedProperty.ExtendedPropertyCode + " - " + existingProductExtendedProperty.ExtendedPropertyName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        { return; }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingProductExtendedProperty.ExtendedPropertyCode + " - " + existingProductExtendedProperty.ExtendedPropertyName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("Product Extended Property - " + existingProductExtendedProperty.ExtendedPropertyCode + " - " + existingProductExtendedProperty.ExtendedPropertyName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        { return; }
                    }
                    // Update Product Extended Property deatils
                    invProductExtendedPropertyService.UpdateInvProductExtendedProperty(existingProductExtendedProperty);
                    if (chkAutoClear.Checked)
                    { ClearForm(); }
                    else
                    { InitializeForm(); }

                    // Display updated iformation 
                    Toast.Show("Product Extended Property - " + existingProductExtendedProperty.ExtendedPropertyCode + " - " + existingProductExtendedProperty.ExtendedPropertyName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }

                txtPropertyCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// Delete Product Extended Property(Change Delete status of relevent Product Extended Property)
        /// </summary>
        public override void Delete()
        {
            try
            {
                if (Toast.Show("Product Extended Property - " + existingProductExtendedProperty.ExtendedPropertyCode + " - " + existingProductExtendedProperty.ExtendedPropertyName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                { return; }

                InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                existingProductExtendedProperty= invProductExtendedPropertyService.GetInvProductExtendedPropertyByCode(txtPropertyCode.Text.Trim());

                if (existingProductExtendedProperty != null && existingProductExtendedProperty.InvProductExtendedPropertyID != 0)
                {
                    existingProductExtendedProperty.IsDelete = true;
                    invProductExtendedPropertyService.UpdateInvProductExtendedProperty(existingProductExtendedProperty);
                    MessageBox.Show("Product Extended Property - " + existingProductExtendedProperty.ExtendedPropertyCode.Trim() + " successfully deleted", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();

                    txtPropertyCode.Focus();
                }

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
        private bool IsValidateControls()
        {
            bool isControlsValidated = true;
            if(!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtPropertyCode, txtPropertyName))
            { 
                isControlsValidated = false; 
            }
            if (!Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbProertyType))
            { 
                isControlsValidated = false; 
            }
            return isControlsValidated;
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

        #region Confirm Existing Property Values

        // Product Extended Property Code
        private bool IsExtendedPropertyCodeExists()
        {
            bool recodFound = false;
            InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
            existingProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByCode(txtPropertyCode.Text.Trim());

            if (existingProductExtendedProperty != null)
            { recodFound = true; }
            else
            {
                Toast.Show("Product Extended Property Code - " + txtPropertyCode.Text.Trim() + " - " + txtPropertyName.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                recodFound = false;
            }

            return recodFound;
        }

        // Product Parent Property Code
        private bool IsParentPropertyCodeExists()
        {
            bool recodFound = false;
            InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
            existingParentProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByCode(cmbParent.Text.Trim());

            if (existingParentProperty != null)
            { recodFound = true; }
            else
            {
                Toast.Show("Parent Property Code - " + cmbParent.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                recodFound = false;
            }

            return recodFound;
        }

        // Proerty Type
        private bool IsProertyTypeExists()
        {
            bool recodFound = false;
            InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
            if (!invProductExtendedPropertyService.GetPropertyTypes().Contains(cmbProertyType.Text.Trim().ToUpper()))
            {
                Toast.Show("Property Type - " + cmbProertyType.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                recodFound = false; 
            }
            else
            { recodFound = true; }

            return recodFound;
        }
             
        #endregion

        #region Validate Logics
        
        /// <summary>
        /// Validate Parent property of the Child
        /// </summary>
        /// <returns></returns>
        private bool IsValidateParent()
        {
            InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
            bool isParentValidated = false;
            if (!invProductExtendedPropertyService.ValidateParent(cmbParent.Text.Trim(), txtPropertyCode.Text.Trim()))
            {
                Toast.Show("Invalid Parent selected for - " + txtPropertyCode.Text.Trim() + " - " + txtPropertyName.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.ValidationFailed);
                isParentValidated = false;
            }
            else
            { isParentValidated = true; }

            return isParentValidated;
        }

        #endregion

        private void txtPropertyName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                {
                    return;
                }

                if (string.IsNullOrEmpty(txtPropertyName.Text.Trim()))
                { return; }
                
                InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                existingProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByName(txtPropertyName.Text.Trim());

                if (existingProductExtendedProperty != null)
                {
                    txtPropertyCode.Text = existingProductExtendedProperty.ExtendedPropertyCode.Trim();
                    txtPropertyName.Text = existingProductExtendedProperty.ExtendedPropertyName.Trim();
                    cmbProertyType.Text = existingProductExtendedProperty.DataType.Trim();
                    cmbParent.Text = existingProductExtendedProperty.Parent.Trim();
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                    Common.EnableButton(false, btnNew);
                }
                else
                {
                    Toast.Show("" + this.Text + " - " + txtPropertyCode.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    txtPropertyName.Focus();
                    return;
                }
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion


    }
}
