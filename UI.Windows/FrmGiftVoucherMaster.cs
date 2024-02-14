using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Domain;
using Service;
using Utility;

namespace UI.Windows
{
    public partial class FrmGiftVoucherMaster : UI.Windows.FrmBaseMasterForm
    {
        private InvGiftVoucherMaster existingInvGiftVoucherMaster;
        private InvGiftVoucherGroup existingInvGiftVoucherGroup;
        private InvGiftVoucherBookCode existingInvGiftVoucherBookCode;
        List<InvGiftVoucherMasterTemp> invGiftVoucherMastersTemp = new List<InvGiftVoucherMasterTemp>();
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        public FrmGiftVoucherMaster()
        {
            InitializeComponent();
        }

        #region Form Events
        
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateControls()) { return; }

                if (!IsValidateControls())
                { return; }

                if ((Toast.Show(this.Text + " - " + txtGroupCode.Text.Trim() + " - " + txtVoucherValue.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.Generate).Equals(DialogResult.No)))
                {
                    return;
                }

                dgvDisplay.DataSource = null;
                dgvDisplay.DataSource = GenerateGiftVoucherBook(txtPrefix.Text.ToString(), Common.ConvertStringToInt(txtLength.Text.ToString()), Common.ConvertStringToInt(txtStartingNo.Text.ToString()), Common.ConvertStringToInt(txtNoOfVouchersOnBook.Text.ToString()), Common.ConvertStringToInt(txtNoOfVouchers.Text.Trim()));

                Toast.Show(this.Text + " - " + txtGroupCode.Text.Trim() + " - " + txtVoucherValue.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.Generate);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                Common.EnableTextBox(false, txtBookCode, txtBookName, txtGroupCode, txtGroupName, txtVoucherValue, txtNoOfVouchersOnBook, txtNoOfVouchers, txtPrefix, txtLength, txtStartingNo);
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
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterBookService = new InvGiftVoucherBookCodeGenerationService();
                    DataView dvAllReferenceData = new DataView(invGiftVoucherMasterBookService.GetAllActiveGiftVoucherMasterBooksDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2)));
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtBookCode);
                        txtBookCode_Leave(this, e);
                    }
                }

                Common.SetFocus(e, txtNoOfVouchers);
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
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterBookService = new InvGiftVoucherBookCodeGenerationService();
                    DataView dvAllReferenceData = new DataView(invGiftVoucherMasterBookService.GetAllActiveGiftVoucherMasterBooksDataTableByVoucherType((rdoVoucher.Checked ? 1 : 2)));
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtBookCode);
                        txtBookCode_Leave(this, e);
                    }
                }

                Common.SetFocus(e, txtBookCode);
                txtBookCode.SelectionStart = txtBookCode.Text.Length;
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
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                    DataView dvAllReferenceData = new DataView(invGiftVoucherGroupService.GetAllActiveGiftVoucherGroupsDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtGroupCode);
                        txtGroupCode_Leave(this, e);
                    }
                }
                Common.SetFocus(e, txtBookCode);
                txtVoucherValue.SelectionStart = txtVoucherValue.Text.Length;
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
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
                    DataView dvAllReferenceData = new DataView(invGiftVoucherGroupService.GetAllActiveGiftVoucherGroupsDataTable());
                    if (dvAllReferenceData.Count != 0)
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
       
        private void txtPrefix_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, btnGenerate);
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
                if (e.KeyCode == Keys.Return)
                {
                    if (Common.ConvertStringToDecimal(txtLength.Text) <= 0) { txtLength.SelectionStart = txtLength.Text.Length; return; } 
 
                    Common.SetFocus(e, txtStartingNo);
                    txtStartingNo.SelectionStart = txtStartingNo.Text.Length;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtVoucherValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtNoOfVouchers);
                txtNoOfVouchers.SelectionStart = txtNoOfVouchers.Text.Length;
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
                if (e.KeyCode == Keys.Return)
                {
                    if (Common.ConvertStringToDecimal(txtNoOfVouchers.Text) <= 0) { txtNoOfVouchers.SelectionStart = txtNoOfVouchers.Text.Length; return; } 
                    Common.SetFocus(e, txtLength);
                    txtLength.SelectionStart = txtLength.Text.Length;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtNoOfVouchersOnBook_KeyDown(object sender, KeyEventArgs e)
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

        private void txtStartingNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtPrefix);
                txtPrefix.SelectionStart = txtPrefix.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBookCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtBookCode.Text.Trim() != string.Empty)
                {
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterBookService = new InvGiftVoucherBookCodeGenerationService();

                    existingInvGiftVoucherBookCode = invGiftVoucherMasterBookService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim());

                    if (existingInvGiftVoucherBookCode != null)
                    {
                        txtBookCode.Text = existingInvGiftVoucherBookCode.BookCode.Trim();
                        txtBookName.Text = existingInvGiftVoucherBookCode.BookName.Trim();

                        txtGroupCode.Text = existingInvGiftVoucherBookCode.InvGiftVoucherGroup.GiftVoucherGroupCode.Trim();
                        txtGroupName.Text = existingInvGiftVoucherBookCode.InvGiftVoucherGroup.GiftVoucherGroupName.Trim();

                        Common.EnableTextBox(false, txtGroupCode, txtGroupName);

                        txtVoucherValue.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherBookCode.GiftVoucherValue);
                        txtNoOfVouchersOnBook.Text = existingInvGiftVoucherBookCode.PageCount.ToString();
                        txtPercentageOfCoupon.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherBookCode.GiftVoucherPercentage);
                        txtPrefix.Text = existingInvGiftVoucherBookCode.BookPrefix;
                        if (existingInvGiftVoucherBookCode.PageCount != 0)
                        {
                            cmbBasedOn.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbBasedOn.SelectedIndex = 1;
                        }

                        GetStartingNoByVoucherType();
                        
                        txtStartingNo.Enabled = false;
                        //txtPrefix.Enabled = false;
                        txtLength.Enabled = false;

                        Common.EnableButton(true, btnGenerate);
                    }

                    txtNoOfVouchers.Focus();
                    txtNoOfVouchers.SelectionStart = txtNoOfVouchers.Text.Length;
                }
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
                if (txtGroupName.Text.Trim() != string.Empty)
                {
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterBookService = new InvGiftVoucherBookCodeGenerationService();
                    existingInvGiftVoucherBookCode = invGiftVoucherMasterBookService.GetInvGiftVoucherMasterBookByName(txtBookName.Text.Trim());

                    if (existingInvGiftVoucherBookCode != null)
                    {
                        txtBookCode.Text = existingInvGiftVoucherBookCode.BookCode.Trim();
                        txtBookName.Text = existingInvGiftVoucherBookCode.BookName.Trim();
                        txtGroupCode.Text = existingInvGiftVoucherBookCode.InvGiftVoucherGroup.GiftVoucherGroupCode.Trim();
                        txtGroupName.Text = existingInvGiftVoucherBookCode.InvGiftVoucherGroup.GiftVoucherGroupName.Trim();
                        txtVoucherValue.Text = Common.ConvertDecimalToStringCurrency(existingInvGiftVoucherBookCode.GiftVoucherValue);
                        txtNoOfVouchersOnBook.Text = existingInvGiftVoucherBookCode.PageCount.ToString();
                    }

                    txtBookCode.Focus();
                    txtBookCode.SelectionStart = txtBookCode.Text.Length;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGroupCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtGroupCode.Text.Trim() != string.Empty)
                {
                    InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                    existingInvGiftVoucherGroup = invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text.Trim());

                    if (existingInvGiftVoucherGroup != null)
                    {
                            txtGroupCode.Text = existingInvGiftVoucherGroup.GiftVoucherGroupCode.Trim();
                            txtGroupName.Text = existingInvGiftVoucherGroup.GiftVoucherGroupName.Trim();
                           // InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterGroupValueService = new InvGiftVoucherBookCodeGenerationService();
                           // Common.SetAutoComplete(txtBookCode, invGiftVoucherMasterGroupValueService.GetInvGiftVoucherBookGenerationCodesByVoucherType((rdoVoucher.Checked ? 1 : 2), existingInvGiftVoucherGroup.InvGiftVoucherGroupID), chkAutoCompleationBook.Checked);
                           // Common.SetAutoComplete(txtBookName, invGiftVoucherMasterGroupValueService.GetInvGiftVoucherBookGenerationNamesByVoucherType((rdoVoucher.Checked ? 1 : 2),existingInvGiftVoucherGroup.InvGiftVoucherGroupID), chkAutoCompleationBook.Checked);

                    }
 
                    txtBookCode.Focus();
                    txtBookCode.SelectionStart = txtBookCode.Text.Length;
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
                    existingInvGiftVoucherGroup = invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupByName(txtGroupName.Text.Trim());

                    if (existingInvGiftVoucherGroup != null)
                    {
                        txtGroupCode.Text = existingInvGiftVoucherGroup.GiftVoucherGroupCode.Trim();
                        txtGroupName.Text = existingInvGiftVoucherGroup.GiftVoucherGroupName.Trim();
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

        private void txtPrefix_Validated(object sender, EventArgs e)
        {
            try
            {
                SetBookCodeFormat();
                GetStartingNoByVoucherType();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLength_Validated(object sender, EventArgs e)
        {
            try
            {
                SetBookCodeFormat();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtStartingNo_Validated(object sender, EventArgs e)
        {
            try
            {
                SetBookCodeFormat();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPrefix_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtLength.MaxLength = 2;
        }

        private void txtLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtLength.MaxLength = 2;
        }

        private void rdoVoucher_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ResetVoucherType();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }
        
        private void rdoCoupon_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ResetVoucherType();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtNoOfVouchers_Validated(object sender, EventArgs e)
        {
            try
            {
                SetBookCodeFormat();
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
                InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();

                LoadBookByVoucherType();

                Common.EnableButton(false, btnSave, btnDelete, btnGenerate);
                Common.EnableTextBox(true, txtBookCode, txtBookName, txtGroupCode, txtGroupName, txtVoucherValue, txtNoOfVouchersOnBook, txtNoOfVouchers, txtLength, txtStartingNo, txtPrefix);
                Common.ClearTextBox(txtBookCode, txtBookName, txtGroupCode, txtGroupName, txtVoucherValue, txtNoOfVouchersOnBook, txtNoOfVouchers, txtLength, txtStartingNo, txtPrefix, txtFormat);
                Common.ReadOnlyTextBox(true, txtVoucherValue, txtPercentageOfCoupon, txtNoOfVouchersOnBook, txtFormat);

                invGiftVoucherMastersTemp = null;
                //dgvDisplay.DataSource = null;

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
                existingInvGiftVoucherMaster = null;
                existingInvGiftVoucherGroup = null;
                existingInvGiftVoucherBookCode = null;

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);

                base.FormLoad();
                txtGroupCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Save()
        {
            try
            {
                //Check validate requirement ==
                //if (!IsValidateExistsRecords()) { return; }

                InvGiftVoucherMasterService invGiftVoucherMasterService = new InvGiftVoucherMasterService();

                bool isNew = false;
                string displayValue = "";
                if (rdoVoucher.Checked)
                {
                    displayValue = this.Text + " - " + txtGroupCode.Text.Trim() + " - " + txtVoucherValue.Text.Trim() + "";
                }
                else if (rdoCoupon.Checked)
                {
                    displayValue = this.Text + " - " + txtGroupCode.Text.Trim() + " - " + txtPercentageOfCoupon.Text.Trim() + "";
                }

                if ((Toast.Show(displayValue, Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                {
                    return;
                }

                invGiftVoucherMasterService.AddInvGiftVoucherMaster(invGiftVoucherMastersTemp);
                ClearForm();
                Toast.Show(displayValue, Toast.messageType.Information, Toast.messageAction.Save);
                InitializeForm();
                txtBookCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void ClearForm()
        {
            existingInvGiftVoucherMaster = null;
            existingInvGiftVoucherGroup = null;
            existingInvGiftVoucherBookCode = null;
            dgvDisplay.DataSource = null;
            dgvDisplay.Refresh();
            Common.EnableTextBox(true, txtGroupName, txtGroupCode);
            base.ClearForm();
        }
        
        private void SetBookCodeFormat()
        {
            string prefix = txtPrefix.Text.ToString();
            int length = Common.ConvertStringToInt(txtLength.Text.ToString());
            int startingNo = Common.ConvertStringToInt(txtStartingNo.Text.ToString());
            
            txtFormat.Text = GetBookCodeFormat(prefix, length, startingNo);
        }

        private string GetBookCodeFormat(string prefix, int length, int pageNo)
        {
            string bookFormat = "";

            if (length > 0)
            {
                length = (length - prefix.Length);
            }

            if (!string.IsNullOrEmpty(length.ToString()))
            {
                bookFormat = String.Format("{0}{1," + length + ":D" + length + "} ", prefix, pageNo);
            }
            return bookFormat;
        }

        private List<InvGiftVoucherMasterTemp> GenerateGiftVoucherBook(string prefix, int bookCodeLength, int startingNo, int noOfPagesOnBook, int noOfVouchers)
        {
            int //bookNo = 0,
                currentBookNo = 0;

            if (invGiftVoucherMastersTemp == null)
            {invGiftVoucherMastersTemp = new List<InvGiftVoucherMasterTemp>();}

            invGiftVoucherMastersTemp.Clear();

            InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterBookService = new InvGiftVoucherBookCodeGenerationService();
            existingInvGiftVoucherBookCode = invGiftVoucherMasterBookService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.Trim());

            InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
            existingInvGiftVoucherGroup = invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text.Trim());

            //bookNo = 1;
            
            currentBookNo = existingInvGiftVoucherBookCode.CurrentSerialNo;
            int y = 0;

            if (noOfPagesOnBook != 0)
            {
                for (int x = 1; x <= noOfVouchers; x = x + y)
                {
                    for (y = 0; y < noOfPagesOnBook; y++)
                    {
                        var tc = new InvGiftVoucherMasterTemp
                            {
                                InvGiftVoucherBookCodeID = existingInvGiftVoucherBookCode.InvGiftVoucherBookCodeID,
                                InvGiftVoucherGroupID = existingInvGiftVoucherGroup.InvGiftVoucherGroupID,
                                CompanyID = Common.LoggedCompanyID,
                                LocationID = Common.LoggedLocationID,
                                VoucherNo = GetBookCodeFormat(existingInvGiftVoucherBookCode.BookPrefix,
                                                      existingInvGiftVoucherBookCode.SerialLength, currentBookNo),
                                VoucherNoSerial = Common.ConvertStringToInt(currentBookNo.ToString()),
                                VoucherSerial = GetBookCodeFormat(prefix, bookCodeLength, startingNo),
                                VoucherSerialNo = Common.ConvertStringToInt(startingNo.ToString()),
                                GiftVoucherValue = Common.ConvertStringToDecimal(txtVoucherValue.Text.Trim()),
                                GiftVoucherPercentage = Common.ConvertStringToDecimal(txtPercentageOfCoupon.Text.ToString().Trim()),
                                VoucherCount = Common.ConvertStringToInt(txtNoOfVouchers.Text.Trim()),
                                VoucherPrefix = txtPrefix.Text.Trim(),
                                SerialLength = Common.ConvertStringToInt(txtLength.Text.Trim()),
                                StartingNo = Common.ConvertStringToInt(txtStartingNo.Text.Trim()),
                                PageCount = Common.ConvertStringToInt(txtNoOfVouchersOnBook.Text.Trim()),
                                VoucherType = (rdoVoucher.Checked ? 1 : 2),
                                VoucherStatus = 0,
                                IsDelete = false,
                                ToLocationID = Common.LoggedLocationID,
                                CurrentSerial = currentBookNo,
                            };
                        invGiftVoucherMastersTemp.Add(tc);
                        startingNo = startingNo + 1;
                    }
                    currentBookNo = currentBookNo + 1;
                }
            }
            else
            {
                for (int x = 1; x <= noOfVouchers; x = x + 1)
                {
                    //for (y = 0; y < noOfPagesOnBook; y++)
                    {
                        var tc = new InvGiftVoucherMasterTemp
                        {
                            InvGiftVoucherBookCodeID = existingInvGiftVoucherBookCode.InvGiftVoucherBookCodeID,
                            InvGiftVoucherGroupID = existingInvGiftVoucherGroup.InvGiftVoucherGroupID,
                            CompanyID = Common.LoggedCompanyID,
                            LocationID = Common.LoggedLocationID,
                            VoucherNo =
                                GetBookCodeFormat(existingInvGiftVoucherBookCode.BookPrefix,
                                                  existingInvGiftVoucherBookCode.SerialLength, existingInvGiftVoucherBookCode.CurrentSerialNo),
                            VoucherNoSerial = Common.ConvertStringToInt(existingInvGiftVoucherBookCode.CurrentSerialNo.ToString()),

                            VoucherSerial = GetBookCodeFormat(prefix, bookCodeLength, startingNo),
                            VoucherSerialNo = Common.ConvertStringToInt(startingNo.ToString()),
                            GiftVoucherValue = Common.ConvertStringToDecimal(txtVoucherValue.Text.Trim()),
                            GiftVoucherPercentage = Common.ConvertStringToDecimal(txtPercentageOfCoupon.Text.ToString().Trim()),
                            VoucherCount = Common.ConvertStringToInt(txtNoOfVouchers.Text.Trim()),
                            VoucherPrefix = txtPrefix.Text.Trim(),
                            SerialLength = Common.ConvertStringToInt(txtLength.Text.Trim()),
                            StartingNo = Common.ConvertStringToInt(txtStartingNo.Text.Trim()),
                            PageCount = Common.ConvertStringToInt(txtNoOfVouchersOnBook.Text.Trim()),
                            VoucherType = (rdoVoucher.Checked ? 1 : 2),
                            VoucherStatus = 0,
                            IsDelete = false,
                            ToLocationID = Common.LoggedLocationID,
                            CurrentSerial = currentBookNo,
                        };
                        invGiftVoucherMastersTemp.Add(tc);
                        startingNo = startingNo + 1;
                    }
                }
                currentBookNo = currentBookNo + 1;
            }
            return invGiftVoucherMastersTemp;
        }

        private void LoadBookByVoucherType()
        {
            InvGiftVoucherBookCodeGenerationService invGiftVoucherMasterGroupValueService = new InvGiftVoucherBookCodeGenerationService();
            Common.SetAutoComplete(txtBookCode, invGiftVoucherMasterGroupValueService.GetInvGiftVoucherBookGenerationCodesByVoucherType((rdoVoucher.Checked ? 1 : 2)), chkAutoCompleationBook.Checked);
            Common.SetAutoComplete(txtBookName, invGiftVoucherMasterGroupValueService.GetInvGiftVoucherBookGenerationNamesByVoucherType((rdoVoucher.Checked ? 1 : 2)), chkAutoCompleationBook.Checked);
            
            InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
            Common.SetAutoComplete(txtGroupCode, invGiftVoucherGroupService.GetInvGiftVoucherGroupCodes(),chkAutoCompleationGroup.Checked);
            Common.SetAutoComplete(txtGroupName, invGiftVoucherGroupService.GetInvGiftVoucherGroupNames(), chkAutoCompleationGroup.Checked);
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
            Common.ClearTextBox(txtBookCode, txtBookName, txtGroupCode, txtGroupName, txtVoucherValue, txtNoOfVouchers, txtFormat, txtNoOfVouchersOnBook, txtLength, txtStartingNo, txtPrefix, txtPercentageOfCoupon);
            if (rdoVoucher.Checked)
            {
                Common.EnableComboBox(false, cmbBasedOn);
                txtLength.Text = Common.GiftVoucherSerialLength.ToString();
            }
            else if (rdoCoupon.Checked)
            {
                Common.EnableComboBox(false, cmbBasedOn);
                txtLength.Text = Common.GiftVoucherCoupanSerialLength.ToString();
            }
            else
            {
                rdoVoucher.Checked = true;
                Common.EnableComboBox(false, cmbBasedOn);
            }

            cmbBasedOn.SelectedIndex = -1;
            
            txtLength.Enabled = false;

            LoadBookByVoucherType();
            GetStartingNoByVoucherType();
        }

        private void GetStartingNoByVoucherType()
        {
            try
            {
                InvGiftVoucherMasterService invGiftVoucherMasterService = new InvGiftVoucherMasterService();
                InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();
                InvGiftVoucherBookCode invGiftVoucherBookCode = new InvGiftVoucherBookCode();
                long startingNo = 1;
                invGiftVoucherBookCode = invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(txtBookCode.Text.ToString());
                if (invGiftVoucherBookCode != null)
                { 
                    startingNo = invGiftVoucherMasterService.GetStartingNoByVoucherType((rdoVoucher.Checked ? 1 : 2), invGiftVoucherBookCode.InvGiftVoucherBookCodeID, txtPrefix.Text);
                    txtStartingNo.Text = startingNo.ToString();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private bool ValidateControls()
        {
            bool isValidated = false;
            if (rdoVoucher.Checked && cmbBasedOn.SelectedIndex == 0)
            {
                isValidated = Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtBookCode, txtLength, txtVoucherValue, txtStartingNo, txtNoOfVouchersOnBook, txtNoOfVouchers);
            }
            if (cmbBasedOn.SelectedIndex != 0)
            {
                isValidated = Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtBookCode, txtLength, txtVoucherValue, txtStartingNo, txtNoOfVouchers);
            }
            if (!Validater.ValidateControls(errorProvider, Validater.ValidateType.Zero, txtNoOfVouchers))
            {
                isValidated = false;
            }
            else
            {
                isValidated = true;
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
            InvGiftVoucherMasterValidator invGiftVoucherMasterValidator = new InvGiftVoucherMasterValidator();
            bool isValidated = false;
            if (!invGiftVoucherMasterValidator.ValidateLength(Common.ConvertStringToInt(txtLength.Text.Trim()), string.Concat(txtPrefix.Text.Trim(), (Common.ConvertStringToInt(txtStartingNo.Text.Trim()) + Common.ConvertStringToInt(txtNoOfVouchers.Text.Trim())))))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(lblSerialLength.Text.ToString()), " - ", txtLength.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Length);
                isValidated = false;
            }
            else if (rdoVoucher.Checked && cmbBasedOn.SelectedIndex == 0)
            {
                if (!invGiftVoucherMasterValidator.ValidateNoOfVouchers(Common.ConvertStringToInt(txtNoOfVouchers.Text.Trim()), Common.ConvertStringToInt(txtNoOfVouchersOnBook.Text.Trim())))
                {
                    Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(lblNoOfVouchers.Text.ToString()), " - ", txtNoOfVouchers.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                    isValidated = false;
                }
                else
                {
                    isValidated = true;
                }
            }
            else
            {
                isValidated = true;
            }

            return isValidated;
        }

        private bool IsValidateExistsRecords()
        {
            InvGiftVoucherMasterValidator invGiftVoucherMasterValidator = new InvGiftVoucherMasterValidator();
            InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();
            bool isValidated = false;
            if (!invGiftVoucherMasterValidator.ValidateExistsVoucherSerial(invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text.Trim()).InvGiftVoucherGroupID, Common.ConvertStringToDecimalCurrency(txtVoucherValue.Text.Trim()), Common.ConvertStringToInt(txtStartingNo.Text.Trim()), Common.ConvertStringToInt(txtNoOfVouchers.Text.Trim())))
            {
                Toast.Show(Common.ConvertStringToDisplayFormat(lblSerialFormat.Text.Trim()) + " ", Toast.messageType.Information, Toast.messageAction.ExistsForSelected, "selected - " + Common.ConvertStringToDisplayFormat(lblGiftVoucherGroup.Text.Trim()) + " - " + txtGroupCode.Text.Trim() + ", " + Common.ConvertStringToDisplayFormat(lblVoucherValue.Text.Trim()) + " - " + txtVoucherValue.Text.Trim());
                isValidated = false;
            }
            else
            { isValidated = true; }

            return isValidated;
        }
        #endregion

        #endregion
    }
}
