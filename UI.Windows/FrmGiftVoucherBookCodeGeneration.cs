using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Domain;
using Service;
using Utility;

namespace UI.Windows
{
    public partial class FrmGiftVoucherBookCodeGeneration : UI.Windows.FrmBaseMasterForm
    {
        ErrorMessage errorMessage = new ErrorMessage();

        private InvGiftVoucherBookCode existingGiftVoucherMasterBook;
        private InvGiftVoucherGroup existingGiftVoucherMasterGroup;
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        public FrmGiftVoucherBookCodeGeneration()
        {
            InitializeComponent();
        }

        #region Form Events
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
                if (chkAutoClear.Checked)
                { Common.ClearTextBox(txtBookCode, txtBookName, txtGiftVoucherValue, txtStartingNo, txtBookPrefix, txtLength, txtNoOfVouchers); }

                InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                existingGiftVoucherMasterGroup = invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text.Trim());
                if (existingGiftVoucherMasterGroup == null)
                {
                    Toast.Show(Common.ConvertStringToDisplayFormat(lblGiftVoucherGroup.Text.ToString()) + " " + txtGroupCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    txtGroupCode.Focus();
                    ClearForm();
                    return;
                }

                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
                    txtBookCode.Text = invGiftVoucherBookCodeGenerationService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength);
                    Common.EnableTextBox(false, txtGroupCode, txtGroupName);
                    Common.ClearTextBox(txtBookName, txtGiftVoucherValue, txtStartingNo, txtBookPrefix, txtLength, txtNoOfVouchers);
                    txtBookName.Focus();
                }
                else
                {
                    txtBookCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGroupCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                    DataView dvAllReferenceData = new DataView(invGiftVoucherMasterGroupService.GetAllActiveGiftVoucherGroupsDataTable());
                    if (dvAllReferenceData.Count >0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtGroupCode);
                        txtGroupCode_Leave(this, e);
                    }
                }
                if (e.KeyCode.Equals(Keys.Return))
                {
                    if (txtGroupCode.Text.Trim() == string.Empty) return;
                    cmbBasedOn.SelectedIndex = 1;
                    Common.SetFocus(e, cmbBasedOn);
                    
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGroupName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                    DataView dvAllReferenceData = new DataView(invGiftVoucherMasterGroupService.GetAllActiveGiftVoucherGroupsDataTable());
                    if (dvAllReferenceData.Count > 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtGroupCode);
                        txtGroupCode_Leave(this, e);
                    }
                }
                Common.SetFocus(e, txtGroupCode);
                txtGroupCode.SelectionStart = txtGroupCode.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBookCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterGroupValueService = new InvGiftVoucherBookCodeGenerationService();
                    DataView dvAllReferenceData = new DataView(invGiftVoucherMasterGroupValueService.GetAllActiveGiftVoucherMasterBooksDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2)));
                    if (dvAllReferenceData.Count >0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtBookCode);
                        txtBookCode_Leave(this, e);
                    }
                }
                if (e.KeyCode.Equals(Keys.Return))
                {
                    if (txtBookCode.Text == string.Empty) return;
                    Common.SetFocus(e, txtBookName);
                    txtBookName.SelectionStart = txtBookName.Text.Length;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBookName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterGroupValueService = new InvGiftVoucherBookCodeGenerationService();
                    DataView dvAllReferenceData = new DataView(invGiftVoucherMasterGroupValueService.GetAllActiveGiftVoucherMasterBooksDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2)));
                    if (dvAllReferenceData.Count > 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtBookCode);
                        txtBookCode_Leave(this, e);
                    }
                }
                Common.SetFocus(e, txtStartingNo);
                txtStartingNo.SelectionStart = txtStartingNo.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGiftVoucherValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Common.SetFocus(e, txtStartingNo);
                //txtStartingNo.SelectionStart = txtStartingNo.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtStartingNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtLength);
                txtLength.SelectionStart = txtLength.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBookPrefix_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (rdoVoucher.Checked)
                {
                    Common.SetFocus(e, txtGiftVoucherValue);
                    txtGiftVoucherValue.SelectionStart = txtGiftVoucherValue.Text.Length;
                }
                else
                {
                    Common.SetFocus(e, txtValidityPeriod);
                    txtValidityPeriod.SelectionStart = txtValidityPeriod.Text.Length;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLength_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (cmbBasedOn.SelectedIndex == 0)
                {
                    Common.SetFocus(e, txtNoOfVouchers);
                    txtNoOfVouchers.SelectionStart = txtNoOfVouchers.Text.Length;
                }
                else if (cmbBasedOn.SelectedIndex == 1)
                {
                    Common.SetFocus(e, txtBookPrefix);
                    txtBookPrefix.SelectionStart = txtBookPrefix.Text.Length;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBookPrefix_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtBookPrefix.MaxLength = 2;
        }

        private void txtLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtLength.MaxLength = 2;
        }

        private void txtGroupCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtGroupCode.Text.Trim() != string.Empty)
                {
                    InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                    existingGiftVoucherMasterGroup = invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text.Trim());

                    if (existingGiftVoucherMasterGroup != null)
                    {
                        txtGroupCode.Text = existingGiftVoucherMasterGroup.GiftVoucherGroupCode.Trim();
                        txtGroupName.Text = existingGiftVoucherMasterGroup.GiftVoucherGroupName.Trim();
                        cmbBasedOn.Focus();
                       // cmbBasedOn.SelectionStart = cmbBasedOn.Text.Length;
                    }
                    else
                    {
                        Toast.Show("Group Code Does Not Exist", Toast.messageType.Warning, Toast.messageAction.Invalid, "");  
                        txtGroupCode.Text = "";
                        txtGroupCode.Focus();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGroupName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtGroupName.Text.Trim() != string.Empty)
                {
                    InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                    existingGiftVoucherMasterGroup = invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupByName(txtGroupName.Text.Trim());

                    if (existingGiftVoucherMasterGroup != null)
                    {
                        txtGroupCode.Text = existingGiftVoucherMasterGroup.GiftVoucherGroupCode.Trim();
                        txtGroupName.Text = existingGiftVoucherMasterGroup.GiftVoucherGroupName.Trim();
                    }

                    txtGroupCode.Focus();
                    txtGroupCode.SelectionStart = txtGroupCode.Text.Length;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBookCode_Validated(object sender, EventArgs e)
        {
            
        }

        private void txtBookCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBookCode.Text.Trim()))
                { return; }

                InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterBookService = new InvGiftVoucherBookCodeGenerationService();
                existingGiftVoucherMasterBook = invGiftVoucherMasterBookService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim());

                if (existingGiftVoucherMasterBook != null)
                {
                    txtGroupCode.Text = existingGiftVoucherMasterBook.InvGiftVoucherGroup.GiftVoucherGroupCode.Trim();
                    txtGroupName.Text = existingGiftVoucherMasterBook.InvGiftVoucherGroup.GiftVoucherGroupName.Trim();
                    txtBookCode.Text = existingGiftVoucherMasterBook.BookCode.Trim();
                    txtBookName.Text = existingGiftVoucherMasterBook.BookName.Trim();
                    txtGiftVoucherValue.Text = Common.ConvertDecimalToStringCurrency(existingGiftVoucherMasterBook.GiftVoucherValue);
                    txtStartingNo.Text = existingGiftVoucherMasterBook.StartingNo.ToString();
                    txtBookPrefix.Text = existingGiftVoucherMasterBook.BookPrefix.Trim();
                    txtLength.Text = existingGiftVoucherMasterBook.SerialLength.ToString();
                    txtNoOfVouchers.Text = existingGiftVoucherMasterBook.PageCount.ToString();
                    cmbBasedOn.SelectedIndex = existingGiftVoucherMasterBook.BasedOn;

                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                    Common.EnableButton(false, btnNew);
                    Common.EnableTextBox(false, txtGroupCode, txtGroupName);
                    Common.EnableComboBox(false, cmbBasedOn);
                }
                else
                {
                    if (chkAutoClear.Checked)
                    { Common.ClearTextBox(txtBookName, txtGiftVoucherValue, txtStartingNo, txtBookPrefix, txtLength, txtNoOfVouchers); }

                    if (btnNew.Enabled)
                    {
                        if (Toast.Show("" + this.Text + " - " + txtBookCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                        { btnNew.PerformClick(); }
                    }
                }

                if (btnSave.Enabled)
                {
                    Common.EnableTextBox(false, txtBookCode);
                }

                txtBookName.Focus();
                txtBookName.SelectionStart = txtBookName.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBookName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                {
                    return;
                }
                if (string.IsNullOrEmpty(txtBookName.Text.Trim()))
                { return; }
                
                InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterGroupValueService = new InvGiftVoucherBookCodeGenerationService();
                existingGiftVoucherMasterBook = invGiftVoucherMasterGroupValueService.GetInvGiftVoucherMasterBookByName(txtBookName.Text.Trim());

                if (existingGiftVoucherMasterBook != null)
                {
                    txtGroupCode.Text = existingGiftVoucherMasterBook.InvGiftVoucherGroup.GiftVoucherGroupCode.Trim();
                    txtGroupName.Text = existingGiftVoucherMasterBook.InvGiftVoucherGroup.GiftVoucherGroupName.Trim();
                    txtBookCode.Text = existingGiftVoucherMasterBook.BookCode.Trim();
                    txtBookName.Text = existingGiftVoucherMasterBook.BookName.Trim();
                    txtGiftVoucherValue.Text = Common.ConvertDecimalToStringCurrency(existingGiftVoucherMasterBook.GiftVoucherValue);
                    txtStartingNo.Text = existingGiftVoucherMasterBook.StartingNo.ToString();
                    txtBookPrefix.Text = existingGiftVoucherMasterBook.BookPrefix.Trim();
                    txtLength.Text = existingGiftVoucherMasterBook.SerialLength.ToString();
                    txtNoOfVouchers.Text = existingGiftVoucherMasterBook.PageCount.ToString();

                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                    Common.EnableButton(false, btnNew);
                }
                else
                {
                    Toast.Show("" + this.Text + " - " + txtBookName.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    if (chkAutoClear.Checked)
                    { Common.ClearTextBox(txtBookName, txtGiftVoucherValue, txtStartingNo, txtBookPrefix, txtLength, txtNoOfVouchers); }
                }

                txtGiftVoucherValue.Focus();
                txtGiftVoucherValue.SelectionStart = txtGiftVoucherValue.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
        #endregion

        #region Methods
        public override void InitializeForm()
        {
            try
            {
                
                InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();

                LoadBookByVoucherType();

                Common.SetAutoComplete(txtGroupCode, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupCodes(), chkAutoCompleationGroup.Checked);
                Common.SetAutoComplete(txtGroupName, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupNames(), chkAutoCompleationGroup.Checked);

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtBookCode, txtGroupCode, txtGroupName);
                Common.EnableComboBox(false, cmbBasedOn);
                Common.ClearTextBox(txtBookCode);

                cmbBasedOn.SelectedIndex = -1;
                ResetVoucherType();
                
                ActiveControl = txtGroupCode;
                txtGroupCode.Focus();
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
                existingGiftVoucherMasterBook = null;
                existingGiftVoucherMasterGroup = null;

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

        public override void Save()
        {
            try
            {
                if (!ValidateControls()) { return; }

                if (!IsValidateControls()) { return; }

                InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterBookService = new InvGiftVoucherBookCodeGenerationService();
                InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                bool isNew = false;
                existingGiftVoucherMasterBook = invGiftVoucherMasterBookService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim());

                if (existingGiftVoucherMasterBook == null || existingGiftVoucherMasterBook.InvGiftVoucherBookCodeID == 0)
                {
                    //existingGiftVoucherMasterBook = invGiftVoucherMasterBookService.GetInvGiftVoucherMasterBookByCodeValue(invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text.Trim()).InvGiftVoucherGroupID, Common.ConvertStringToDecimalCurrency(txtGiftVoucherValue.Text.Trim()));
                    //if (existingGiftVoucherMasterBook == null || existingGiftVoucherMasterBook.InvGiftVoucherBookCodeID == 0)
                    {
                        existingGiftVoucherMasterBook = new InvGiftVoucherBookCode();
                        isNew = true;
                        existingGiftVoucherMasterBook.CurrentSerialNo = 1;
                    }
                }

                existingGiftVoucherMasterBook.BookCode = txtBookCode.Text.Trim();
                existingGiftVoucherMasterBook.BookName = txtBookName.Text.Trim();

                existingGiftVoucherMasterGroup = invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text.Trim());
                if (existingGiftVoucherMasterGroup != null)
                {
                    existingGiftVoucherMasterBook.InvGiftVoucherGroupID = existingGiftVoucherMasterGroup.InvGiftVoucherGroupID;
                    //existingGiftVoucherMasterBook.InvGiftVoucherGroup = existingGiftVoucherMasterGroup;
                }

                existingGiftVoucherMasterBook.GiftVoucherValue = Common.ConvertStringToDecimalCurrency(txtGiftVoucherValue.Text.Trim());
                existingGiftVoucherMasterBook.BookPrefix = txtBookPrefix.Text.Trim();
                existingGiftVoucherMasterBook.StartingNo = Common.ConvertStringToInt(txtStartingNo.Text.Trim());
                existingGiftVoucherMasterBook.SerialLength = Common.ConvertStringToInt(txtLength.Text.Trim());
                existingGiftVoucherMasterBook.PageCount = Common.ConvertStringToInt(txtNoOfVouchers.Text.Trim());
                existingGiftVoucherMasterBook.ValidityPeriod = Common.ConvertStringToInt(txtValidityPeriod.Text.Trim());
                existingGiftVoucherMasterBook.GiftVoucherPercentage = Common.ConvertStringToDecimalCurrency(txtPercentageOfCoupon.Text.Trim());
                existingGiftVoucherMasterBook.VoucherType = (rdoVoucher.Checked ? 1 : 2);
                existingGiftVoucherMasterBook.BasedOn = cmbBasedOn.SelectedIndex;

                string displayValue = "";
                if (rdoVoucher.Checked)
                {
                    //displayValue = existingGiftVoucherMasterBook.InvGiftVoucherGroup.GiftVoucherGroupCode + " - " + Common.ConvertDecimalToStringCurrency(existingGiftVoucherMasterBook.GiftVoucherValue);
                    displayValue = Common.ConvertStringToDisplayFormat(lblGiftVoucherGroup.Text) + " : " + txtGroupCode.Text + " - " + Common.ConvertDecimalToStringCurrency(existingGiftVoucherMasterBook.GiftVoucherValue);
                }
                else if (rdoCoupon.Checked)
                {
                    displayValue = Common.ConvertStringToDisplayFormat(lblGiftVoucherGroup.Text) + " : " + txtGroupCode.Text + " - " + Common.ConvertDecimalToStringCurrency(existingGiftVoucherMasterBook.GiftVoucherPercentage);
                }

                if (existingGiftVoucherMasterBook.InvGiftVoucherBookCodeID == 0)
                {
                    if ((Toast.Show(this.Text + " - " + displayValue + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    invGiftVoucherMasterBookService.AddInvGiftVoucherMasterBook(existingGiftVoucherMasterBook);

                    if ((Toast.Show(this.Text + " - " + displayValue + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        if ((Toast.Show(this.Text + " - " + displayValue + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (!IsValidateExistsRecords()) { return; }
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + displayValue + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show(this.Text + " - " + displayValue + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    invGiftVoucherMasterBookService.UpdateInvGiftVoucherMasterBook(existingGiftVoucherMasterBook);
                    Toast.Show(this.Text + " - " + displayValue + "", Toast.messageType.Information, Toast.messageAction.Modify);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                }
                txtGroupCode.Focus();
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
                if (Toast.Show(this.Text + " - " + existingGiftVoucherMasterBook.InvGiftVoucherGroup.GiftVoucherGroupCode + " - " + existingGiftVoucherMasterBook.GiftVoucherValue + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                { return; }

                InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterBookService = new InvGiftVoucherBookCodeGenerationService();

                existingGiftVoucherMasterBook = invGiftVoucherMasterBookService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim());

                if (existingGiftVoucherMasterBook != null && existingGiftVoucherMasterBook.InvGiftVoucherBookCodeID != 0)
                {
                    existingGiftVoucherMasterBook.IsDelete = true;
                    invGiftVoucherMasterBookService.UpdateInvGiftVoucherMasterBook(existingGiftVoucherMasterBook);
                    ClearForm();
                    Toast.Show(this.Text + " - " + existingGiftVoucherMasterBook.InvGiftVoucherGroup.GiftVoucherGroupCode + " - " + existingGiftVoucherMasterBook.GiftVoucherValue + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    txtGroupCode.Focus();
                }
                else
                {
                    Toast.Show(this.Text + " - " + existingGiftVoucherMasterBook.BookCode + " - " + existingGiftVoucherMasterBook.BookName + "", Toast.messageType.Warning, Toast.messageAction.NotExists);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void ClearForm()
        {
            existingGiftVoucherMasterBook = null;
            existingGiftVoucherMasterGroup = null;
            base.ClearForm();
        }

        private void LoadBookByVoucherType()
        {
            InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterGroupValueService = new InvGiftVoucherBookCodeGenerationService();
            Common.SetAutoComplete(txtBookCode, invGiftVoucherMasterGroupValueService.GetInvGiftVoucherBookGenerationCodesByVoucherType((rdoVoucher.Checked ? 1 : 2)), chkAutoCompleationBook.Checked);
            Common.SetAutoComplete(txtBookName, invGiftVoucherMasterGroupValueService.GetInvGiftVoucherBookGenerationNamesByVoucherType((rdoVoucher.Checked ? 1 : 2)), chkAutoCompleationBook.Checked);
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

        private void ResetVoucherType()
        {
            if (rdoVoucher.Checked)
            {
                grpVoucher.Enabled = true;
                grpCoupon.Enabled = false;

                Common.EnableTextBox(true,txtGiftVoucherValue);
                Common.EnableComboBox(true, cmbBasedOn);
                Common.ClearTextBox(txtGiftVoucherValue, txtValidityPeriod, txtPercentageOfCoupon);
            }
            else if (rdoCoupon.Checked)
            {
                grpVoucher.Enabled = false;
                grpCoupon.Enabled = true;
                Common.EnableTextBox(true, txtValidityPeriod, txtPercentageOfCoupon);
                Common.EnableComboBox(true, cmbBasedOn);
                Common.ClearTextBox(txtGiftVoucherValue, txtValidityPeriod, txtPercentageOfCoupon);
            }
            else
            {
                rdoVoucher.Checked = true;
                grpVoucher.Enabled = true;
                grpCoupon.Enabled = false;
                Common.EnableTextBox(true, txtGiftVoucherValue);
                Common.EnableComboBox(true, cmbBasedOn);
                Common.ClearTextBox(txtGiftVoucherValue, txtValidityPeriod, txtPercentageOfCoupon);
            }
            LoadBookByVoucherType();
        }

        private void ResetBasedType()
        {
            if (cmbBasedOn.SelectedIndex == 0)
            {
                Common.EnableTextBox(true, txtNoOfVouchers);
            }
            else
            {
                Common.EnableTextBox(false, txtNoOfVouchers);
            }
        }

        private bool ValidateControls()
        {
            bool isValidated = false;
            if (rdoVoucher.Checked)
            {
                if (cmbBasedOn.SelectedIndex == 0)
                {
                    isValidated = Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtGroupCode, txtBookCode, txtBookName, txtGiftVoucherValue, txtStartingNo, txtLength, txtNoOfVouchers);     
                }
                else if (cmbBasedOn.SelectedIndex == 1)
                {
                    isValidated = Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtGroupCode, txtBookCode, txtBookName, txtGiftVoucherValue, txtStartingNo, txtLength);
                }
            }
            else if (rdoCoupon.Checked)
            {
                if (cmbBasedOn.SelectedIndex == 0)
                {
                    isValidated = Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtGroupCode, txtBookCode, txtBookName, txtPercentageOfCoupon, txtStartingNo, txtLength, txtNoOfVouchers);
                }
                else if (cmbBasedOn.SelectedIndex == 1)
                {
                    isValidated = Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtGroupCode, txtBookCode, txtBookName, txtPercentageOfCoupon, txtStartingNo, txtLength);
                }
            }
            return isValidated;
        }

        #region Validate Logics

        /// <summary>
        /// Validate Parent property of the Child
        /// </summary>
        /// <returns></returns>
        private bool IsValidateControls()
        {

            InvGiftVoucherBookCodeGenerationValidator invGiftVoucherMasterBookValidator = new InvGiftVoucherBookCodeGenerationValidator();
            bool isValidated = false;
            //if (rdoVoucher.Checked && cmbBasedOn.SelectedIndex == 0)
            {
                if (!invGiftVoucherMasterBookValidator.ValidateLength(Common.ConvertStringToInt(txtLength.Text.Trim()), string.Concat(txtBookPrefix.Text.Trim(), txtStartingNo.Text.Trim())))
                {
                    Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(lblSerialLength.Text.ToString()), " - ", txtLength.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Length);
                    isValidated = false;
                }
                else
                { isValidated = true; }

                if (Common.ConvertStringToDecimal(txtLength.Text.Trim()) > 20)
                {
                    Toast.Show(Common.ConvertStringToDisplayFormat(lblSerialLength.Text), Toast.messageType.Warning, Toast.messageAction.GreaterThan, "20");
                    isValidated = false;
                }
                else
                { isValidated = true; }
            }
            //else
            //{
            //    isValidated = true;
            //}

            return isValidated;
        }

        private bool IsValidateExistsRecords()
        {
            InvGiftVoucherBookCodeGenerationValidator invGiftVoucherMasterBookValidator = new InvGiftVoucherBookCodeGenerationValidator();
            InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
            bool isValidated = false;
            int voucherType = (rdoVoucher.Checked ? 1 : 2);
            string voucherTypeText = "";
            decimal voucherValue = 0;
            if (rdoVoucher.Checked)
            {
                voucherValue = Common.ConvertStringToDecimalCurrency(txtGiftVoucherValue.Text.Trim());
                voucherTypeText = Common.ConvertStringToDisplayFormat(lblGiftVoucherValue.Text.Trim());
            }
            else if (rdoCoupon.Checked)
            {
                voucherValue = Common.ConvertStringToDecimalCurrency(txtPercentageOfCoupon.Text.Trim());
                voucherTypeText = Common.ConvertStringToDisplayFormat(lblPercentageOfCoupon.Text.Trim());
            }

            if (!invGiftVoucherMasterBookValidator.ValidateExistsGiftVoucherMaster(voucherType, invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim()).InvGiftVoucherBookCodeID, voucherValue))
            {
                Toast.Show("Generated voucher(s) ", Toast.messageType.Information, Toast.messageAction.ExistsForSelected, "selected - " + Common.ConvertStringToDisplayFormat(lblBookCode.Text) + " : " + txtBookCode.Text + "," + voucherTypeText + " : " + voucherValue);
                isValidated = false;
            }
            else
            { isValidated = true; }

            return isValidated;
        }
        #endregion

        private void rdoVoucher_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ResetVoucherType();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        #endregion

        private void rdoCoupon_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ResetVoucherType();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbBasedOn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    ResetBasedType();
                    Common.SetFocus(e, txtBookCode);
                    txtBookCode.SelectionStart = txtBookCode.Text.Length;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        

        private void txtNoOfVouchers_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (rdoVoucher.Checked)
                {
                    grpVoucher.Enabled = true;
                    grpCoupon.Enabled = false;
                    Common.EnableTextBox(true, txtNoOfVouchers);
                    Common.EnableTextBox(false, txtValidityPeriod, txtPercentageOfCoupon);
                    Common.SetFocus(e, txtNoOfVouchers);
                    txtNoOfVouchers.SelectionStart = txtNoOfVouchers.Text.Length;
                }
                else if (rdoCoupon.Checked)
                {
                    grpVoucher.Enabled = false;
                    grpCoupon.Enabled = true;
                    Common.EnableTextBox(false, txtNoOfVouchers);
                    Common.EnableTextBox(true, txtValidityPeriod, txtPercentageOfCoupon);
                    Common.SetFocus(e, txtValidityPeriod);
                    txtValidityPeriod.SelectionStart = txtValidityPeriod.Text.Length;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbBasedOn_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                ResetBasedType();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtValidityPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtPercentageOfCoupon);
                txtPercentageOfCoupon.SelectionStart = txtPercentageOfCoupon.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbBasedOn_Leave(object sender, EventArgs e)
        {
            //if (cmbBasedOn.Text == string.Empty)
            //{
            //    Toast.Show("Select Based On Value", Toast.messageType.Warning, Toast.messageAction.Invalid, "");
            //    cmbBasedOn.Focus();
            //}
        }

        
    }
}
