using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Linq;
using System.Transactions;


using Domain;
using Utility;
using Service;
using System.Reflection;

namespace UI.Windows
{
    /// <summary>
    /// Developed by - asanka
    /// </summary>

    public partial class FrmProductExtendedValue : UI.Windows.FrmBaseMasterForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;
        InvProductExtendedProperty existingProductExtendedProperty = new InvProductExtendedProperty();
        InvProductExtendedValue existingProductExtendedValue = new InvProductExtendedValue();
        List<InvProductExtendedValue> invProductExtendedValues = new List<InvProductExtendedValue>();
        AutoCompleteStringCollection autoCompleteProductExtendedPropertyCode = new AutoCompleteStringCollection();

        public FrmProductExtendedValue()
        {
            InitializeComponent();
        }

        #region Form Events

        private void FrmProductExtendedValues_Load(object sender, EventArgs e)
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                chkAutoCompleationProperty.Checked = true; // Change this properties according on user profile.
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

                if (e.KeyCode.Equals(Keys.Enter)) 
                {
                    if (string.IsNullOrEmpty(txtPropertyCode.Text.Trim()))
                    {
                        txtPropertyName.Focus();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtPropertyCode.Text.Trim()))
                        { return; }
                        InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                        existingProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByCode(txtPropertyCode.Text.Trim());

                        if (existingProductExtendedProperty != null)
                        {
                            txtPropertyCode.Text = existingProductExtendedProperty.ExtendedPropertyCode.Trim();
                            txtPropertyName.Text = existingProductExtendedProperty.ExtendedPropertyName.Trim();
                            txtDataType.Text = existingProductExtendedProperty.DataType.ToString().Trim();

                            txtExtendedValue.Focus();
                        }
                        else
                        {
                            Toast.Show("Product Extended Property - " + txtPropertyCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                            txtPropertyName.Text = string.Empty;
                            txtExtendedValue.Focus();
                            return;
                        }

                        InvProductExtendedValueService invProductExtendedValueService = new InvProductExtendedValueService();
                        invProductExtendedValues = invProductExtendedValueService.GetAllActiveInvProductExtendedValuesByProductExtendedPrpertyCode(txtPropertyCode.Text.Trim());
                        UpdatedgvPropertyValue();

                        Common.ClearTextBox(txtExtendedValue);

                        dgvPropertyValue.Focus();
                    }
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

        private void chkAutoCompleationProperty_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                Common.SetAutoComplete(txtPropertyCode, invProductExtendedPropertyService.GetInvProductExtendedPropertyCodes(), chkAutoCompleationProperty.Checked);
                Common.SetAutoComplete(txtPropertyName, invProductExtendedPropertyService.GetInvProductExtendedPropertyNames(), chkAutoCompleationProperty.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvPropertyValue_RowValidated(object sender, DataGridViewCellEventArgs e)
        {

        }        

        private void txtExtendedValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter) || string.IsNullOrEmpty(txtExtendedValue.Text.Trim()))
                { return; }

                InvProductExtendedValueService invProductExtendedValueService = new InvProductExtendedValueService();             
                if (!invProductExtendedValueService.IsValidPropertyValueDataType(existingProductExtendedProperty, txtExtendedValue.Text.Trim()))
                {
                    Toast.Show("Product Extended Value for - " + txtPropertyCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.Invalid);
                    return;
                }
                AssignExtendedValueProperties();
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
                existingProductExtendedProperty = null;

                InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                Common.SetAutoComplete(txtPropertyCode, invProductExtendedPropertyService.GetInvProductExtendedPropertyCodes(), chkAutoCompleationProperty.Checked);
                Common.SetAutoComplete(txtPropertyName, invProductExtendedPropertyService.GetInvProductExtendedPropertyNames(), chkAutoCompleationProperty.Checked);

                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                Common.EnableTextBox(true, txtPropertyCode, txtPropertyName);
                Common.ClearTextBox(txtPropertyCode, txtPropertyName);

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
        

        

        public override void Clear()
        {
            dgvPropertyValue.DataSource = null;
            base.Clear();
        }

        /// <summary>
        /// Save new Product Extended Value or Update existing Product Extended Value
        /// </summary>
        public override void Save()
        {
            try
            {
                if ((Toast.Show("Product Extended Property Values for - " + existingProductExtendedProperty.ExtendedPropertyCode + " - " + existingProductExtendedProperty.ExtendedPropertyName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                { return; }

                InvProductExtendedValueService invProductExtendedValueService = new InvProductExtendedValueService();
                InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                InvProductExtendedProperty invProductExtendedProperty = new InvProductExtendedProperty();
                invProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByCode(txtPropertyCode.Text.Trim());

                invProductExtendedValueService.UpdateExtendedValues(invProductExtendedValues);

                //foreach (InvProductExtendedValue temp in invProductExtendedValues) 
                //{
                //    InvProductExtendedValue invProductExtendedValueSave = new InvProductExtendedValue();

                //    invProductExtendedValueSave.InvProductExtendedPropertyID = temp.InvProductExtendedPropertyID;
                //    invProductExtendedValueSave.ValueData = temp.ValueData;
                //    invProductExtendedValueSave.ParentValueData = temp.ParentValueData;
                //    invProductExtendedValueSave.IsDelete = false;

                //    invProductExtendedValueService.AddInvProductExtendedValues(invProductExtendedValues);
                //}

               // invProductExtendedValueService.saveQuery(invProductExtendedValues);

                

                Toast.Show("Product Extended Property Values for  - " + existingProductExtendedProperty.ExtendedPropertyCode + " - " + existingProductExtendedProperty.ExtendedPropertyName + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);

                if (chkAutoClear.Checked)
                { ClearForm(); }
                else
                { InitializeForm(); }

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void ClearForm()
        {
            dgvPropertyValue.DataSource = null;
            base.ClearForm();
        }

        /// <summary>
        /// 
        /// </summary>
        private void AssignExtendedValueProperties()
        {
            InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
            existingProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByCode(txtPropertyCode.Text.Trim());

            if (existingProductExtendedProperty == null)
            {
                Toast.Show("Product Extended Property - " + txtPropertyCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                return;
            }

            InvProductExtendedValueService invProductExtendedValueService = new InvProductExtendedValueService();
            InvProductExtendedValue invProductExtendedValue = new InvProductExtendedValue();
            invProductExtendedValue.InvProductExtendedPropertyID = existingProductExtendedProperty.InvProductExtendedPropertyID;
            invProductExtendedValue.ValueData = txtExtendedValue.Text.Trim();
            invProductExtendedValue.IsDelete = false;
            invProductExtendedValue.GroupOfCompanyID = Common.GroupOfCompanyID;
            invProductExtendedValue.CreatedUser = Common.LoggedUser;
            invProductExtendedValue.CreatedDate = DateTime.UtcNow;
            invProductExtendedValue.ModifiedUser = Common.LoggedUser;
            invProductExtendedValue.ModifiedDate = DateTime.UtcNow;

            invProductExtendedValues = invProductExtendedValueService.GetInvProductExtendedValuesList(invProductExtendedValues, invProductExtendedValue);
            UpdatedgvPropertyValue();
        }

        private void UpdatedgvPropertyValue()
        {
            dgvPropertyValue.DataSource = null;
            dgvPropertyValue.DataSource = invProductExtendedValues.ToList(); // (List<InvProductExtendedValue>)existingProductExtendedProperty.InvProductExtendedValues.ToList();
            //dgvPropertyValue.DataSource = (List<InvProductExtendedValue>)existingProductExtendedProperty.InvProductExtendedValues.ToList();

            Common.EnableTextBox(false, txtPropertyCode, txtPropertyName);
            Common.ClearTextBox(txtExtendedValue);
        }

        private void dgvPropertyValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F2))
                {
                    if (dgvPropertyValue.CurrentCell != null && dgvPropertyValue.CurrentCell.RowIndex >= 0)
                    {
                        if (Toast.Show("Value " + dgvPropertyValue["ValueData", dgvPropertyValue.CurrentCell.RowIndex].Value.ToString(), Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                            return;

                        InvProductExtendedValueService invProductExtendedValueService = new InvProductExtendedValueService();
                        InvProductExtendedValue invProductExtendedValue = new InvProductExtendedValue();

                        invProductExtendedValue.ValueData = dgvPropertyValue["ValueData", dgvPropertyValue.CurrentCell.RowIndex].Value.ToString();

                        dgvPropertyValue.DataSource = null;
                        invProductExtendedValues = invProductExtendedValueService.GetDeleteInvProductExtendedValuesList(invProductExtendedValues, invProductExtendedValue);
                        dgvPropertyValue.DataSource = invProductExtendedValues;
                        dgvPropertyValue.Refresh();

                        this.ActiveControl = txtExtendedValue;
                        txtExtendedValue.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPropertyName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPropertyName.Text.Trim()))
                { return; }
                InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                existingProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByName(txtPropertyName.Text.Trim());

                if (existingProductExtendedProperty != null)
                {
                    txtPropertyCode.Text = existingProductExtendedProperty.ExtendedPropertyCode.Trim();
                    txtPropertyName.Text = existingProductExtendedProperty.ExtendedPropertyName.Trim();
                    txtDataType.Text = existingProductExtendedProperty.DataType.ToString().Trim();
                    txtExtendedValue.Focus();
                }
                else
                {
                    Toast.Show("Product Extended Property - " + txtPropertyCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    txtPropertyName.Text = string.Empty;
                    txtPropertyName.Focus();
                    return;
                }

                InvProductExtendedValueService invProductExtendedValueService = new InvProductExtendedValueService();
                invProductExtendedValues = invProductExtendedValueService.GetAllActiveInvProductExtendedValuesByProductExtendedPrpertyCode(txtPropertyCode.Text.Trim());
                UpdatedgvPropertyValue();

                Common.ClearTextBox(txtExtendedValue);

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
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtExtendedValue.Focus();
                }

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
                //if (string.IsNullOrEmpty(txtPropertyCode.Text.Trim()))
                //{ return; }
                //InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                //existingProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByCode(txtPropertyCode.Text.Trim());

                //if (existingProductExtendedProperty != null)
                //{
                //    txtPropertyCode.Text = existingProductExtendedProperty.ExtendedPropertyCode.Trim();
                //    txtPropertyName.Text = existingProductExtendedProperty.ExtendedPropertyName.Trim();
                //    txtDataType.Text = existingProductExtendedProperty.DataType.ToString().Trim();

                //    txtExtendedValue.Focus();
                //}
                //else
                //{
                //    Toast.Show("Product Extended Property - " + txtPropertyCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                //    txtPropertyName.Text = string.Empty;
                //    txtExtendedValue.Focus();
                //    return;
                //}

                //InvProductExtendedValueService invProductExtendedValueService = new InvProductExtendedValueService();
                //invProductExtendedValues = invProductExtendedValueService.GetAllActiveInvProductExtendedValuesByProductExtendedPrpertyCode(txtPropertyCode.Text.Trim());
                //UpdatedgvPropertyValue();

                //Common.ClearTextBox(txtExtendedValue);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        
       

        /// <summary>
        /// Set up 'dgvPropertyValue' DataGridView
        /// bind data, add combo box column
        /// </summary>
        //private void SetUpDataGridViewdgvPropertyValue()
        //{
        //    try
        //    {
        //        InvProductExtendedValueService invProductExtendedValueService = new InvProductExtendedValueService();
        //        // Bind data to dgvPropertyValue
        //        //dgvPropertyValue.DataSource = invProductExtendedValueService.GetAllActiveInvProductExtendedValues();
        //        dgvPropertyValue.DataSource = invProductExtendedValueService.GetAllActiveInvProductExtendedValuesByProductExtendedPrpertyCode(txtPropertyCode.Text.Trim());
        //        dgvPropertyValue.AllowUserToAddRows = true;
        //        AddCustomCoulmns();
        //        //HideColumns();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
        //    }
        //}

        /// <summary>
        /// Create and add Custom columns(Combobox, Button, Link etc...) into DataGridView 
        /// </summary>
        //private void AddCustomCoulmns()
        //{
            
        //    #region Combobox

        //    try
        //    {
        //        InvProductExtendedValueService invProductExtendedValueService = new InvProductExtendedValueService();
        //        // Remove columns to be replaced with combobox
        //        int colIndex = dgvPropertyValue.Columns["ParentValueData"].Index; // Get index of removing column 
        //        dgvPropertyValue.Columns.Remove("ParentValueData");

        //        // Create DataGridViewComboBoxColumn
        //        DataGridViewComboBoxColumn comboboxColumnParentValues = new DataGridViewComboBoxColumn();

        //        // Bind data to DataGridViewComboBoxColumn, set properties and add into DataGridView
        //        comboboxColumnParentValues.DataSource = invProductExtendedValueService.GetInvProductExtendedValuesForParent();
        //        comboboxColumnParentValues.DataPropertyName = "ParentValueData";
        //        comboboxColumnParentValues.Name = "ParentValueData";
        //        comboboxColumnParentValues.HeaderText = "Parent Value";
        //        comboboxColumnParentValues.MaxDropDownItems = 4;
        //        comboboxColumnParentValues.FlatStyle = FlatStyle.Flat;
        //        dgvPropertyValue.Columns.Insert(colIndex, comboboxColumnParentValues);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
        //    }
            //#endregion
        //}

        /// <summary>
        /// Hide unnecessary columns to display
        /// </summary>
        //private void HideColumns()
        //{
        //    try
        //    {
        //        dgvPropertyValue.Columns["InvProductExtendedValueID"].Visible = false;
        //        dgvPropertyValue.Columns["InvExtendedPropertyID"].Visible = false;
        //        dgvPropertyValue.Columns["IsDelete"].Visible = false;
        //        dgvPropertyValue.Columns["GroupOfCompanyID"].Visible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
        //    }
        //}

        #endregion

    }
}
