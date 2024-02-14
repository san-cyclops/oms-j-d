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
    public partial class FrmCardMaster : UI.Windows.FrmBaseMasterForm
    {
        
        /// <summary>
        /// Sanjeewa
        /// </summary>


        CardMaster existingCardMaster;
        AutoCompleteStringCollection autoCompleteCode;
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        public FrmCardMaster()
        {
            InitializeComponent();
        }

        public override void InitializeForm()
        {
            try
            {
                CardMaster cardMaster = new CardMaster();
                CardMasterService cardMasterService = new CardMasterService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();

                Common.SetAutoComplete(txtCode, cardMasterService.GetAllCardCodes(), chkAutoCompleationCard.Checked);
                Common.SetAutoComplete(txtName, cardMasterService.GetAllCardNames(), chkAutoCompleationCard.Checked);

                this.cmbType.SelectedIndexChanged -= new System.EventHandler(this.cmbType_SelectedIndexChanged);
                //SetAutoBindRecords(cmbType, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.LCardType).ToString()));                                
                Common.SetAutoBindRecords(cmbType, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.LCardType).ToString()));

                this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtCode);
                Common.ClearTextBox(txtCode);

                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;

                ActiveControl = txtCode;
                txtCode.Focus();
            }
            catch(Exception ex)
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
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name,
                                Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        //public static void SetAutoBindRecords(ComboBox comboBox, string[] stringCollection)
        //{
        //    AutoCompleteStringCollection autoCompleteCode = new AutoCompleteStringCollection();
        //    autoCompleteCode.AddRange(stringCollection);
        //    comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    comboBox.AutoCompleteCustomSource = autoCompleteCode;
        //    comboBox.DataSource = autoCompleteCode;
        //    comboBox.SelectedIndex = -1;
        //}

        private void FrmSize_Load(object sender, EventArgs e)
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                chkAutoCompleationCard.Checked = true;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtCode, txtName);
            //return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbType);
        }

        public override void Save()
        {
            try
            {

                if (ValidateControls() == false) return;

                if (cmbType.Text == string.Empty) return;
                CardMaster cardMaster = new CardMaster();
                CardMasterService cardMasterService = new CardMasterService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                
                bool isNew = false;

                existingCardMaster = cardMasterService.GetCardMasterByCode(txtCode.Text.Trim());

                if (existingCardMaster == null || existingCardMaster.CardMasterID == 0)
                {
                    existingCardMaster = new CardMaster();
                    isNew = true;
                }
                //existingCardMaster.CardType = cmbType.SelectedIndex+1;
                existingCardMaster.CardType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.CardType).ToString(), cmbType.Text.Trim()).LookupKey;              
                existingCardMaster.CardCode = txtCode.Text.Trim();
                existingCardMaster.CardName = txtName.Text.Trim();
                existingCardMaster.Remark = txtDescription.Text.Trim();
                existingCardMaster.Discount = Common.ConvertStringToDecimal(txtDiscount.Text.Trim());
                existingCardMaster.MinimumPoints = Common.ConvertStringToInt(txtMinimumPoints.Text.Trim());
                existingCardMaster.PointValue = Common.ConvertStringToDecimal(txtPointValue.Text.Trim());

                if (existingCardMaster.CardMasterID == 0)
                {
                    if ((Toast.Show("Card   - " + existingCardMaster.CardCode + " - " + existingCardMaster.CardName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    cardMasterService.AddCardMasters(existingCardMaster);
                    
                    InitializeForm();
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    if ((Toast.Show("Card   - " + existingCardMaster.CardCode + " - " + existingCardMaster.CardName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
                    {
                        cmbType.SelectedIndex = existingCardMaster.CardType;
                        btnNew.PerformClick();
                    }
                }
                else
                {
                    if (isNew)
                    {
                        if ((Toast.Show("Card   - " + existingCardMaster.CardCode + " - " + existingCardMaster.CardName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingCardMaster.CardCode + " - " + existingCardMaster.CardName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("Card   - " + existingCardMaster.CardCode + " - " + existingCardMaster.CardName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    cardMasterService.UpdateCardMasters(existingCardMaster);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    Toast.Show("Card   - " + existingCardMaster.CardCode + " - " + existingCardMaster.CardName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }
                txtCode.Focus();

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
                if (Toast.Show("Card   - " + existingCardMaster.CardCode + " - " + existingCardMaster.CardName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                    return;

                CardMasterService cardMasterService = new CardMasterService();
                existingCardMaster = cardMasterService.GetCardMasterByCode(txtCode.Text.Trim());

                if (existingCardMaster != null && existingCardMaster.CardMasterID != 0)
                {
                    existingCardMaster.IsDelete = true;
                    cardMasterService.UpdateCardMasters(existingCardMaster);
                    ClearForm();
                    Toast.Show("Card   - " + existingCardMaster.CardCode + " - " + existingCardMaster.CardName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    txtCode.Focus();
                }
                else
                    Toast.Show("Card   - " + existingCardMaster.CardCode + " - " + existingCardMaster.CardName + "", Toast.messageType.Warning, Toast.messageAction.NotExists);


            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtName);
            }
            catch(Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text.Trim() != string.Empty)
                {
                    CardMasterService cardMasterService = new CardMasterService();

                    existingCardMaster = cardMasterService.GetCardMasterByCode(txtCode.Text.Trim());

                    if (existingCardMaster != null)
                    {
                        txtCode.Text = existingCardMaster.CardCode.Trim();
                        cmbType.SelectedIndex = existingCardMaster.CardType-1;
                        txtName.Text = existingCardMaster.CardName.Trim();
                        txtDescription.Text = existingCardMaster.Remark.Trim();
                        txtDiscount.Text = existingCardMaster.Discount.ToString();
                        txtMinimumPoints.Text = existingCardMaster.MinimumPoints.ToString();
                        txtPointValue.Text = existingCardMaster.PointValue.ToString();
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        txtName.Focus();

                    }
                    else
                    {
                        if (chkAutoClear.Checked)
                            Common.ClearTextBox(txtName, txtDescription);
                        if (btnNew.Enabled)
                            if (Toast.Show("Card  - " + txtCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                                btnNew.PerformClick();

                        if (btnSave.Enabled)
                        {
                            Common.EnableTextBox(false, txtCode);
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtDescription);
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
                if (cmbType.SelectedIndex < 0)
                {
                    Toast.Show("Card Type ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    cmbType.Focus();
                    return;
                }
                Common.EnableButton(false, btnNew, btnDelete);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                if (chkAutoClear.Checked)
                    Common.ClearTextBox(txtName, txtDescription);
                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    CardMasterService cardMasterService = new CardMasterService();
                    txtCode.Text = cardMasterService.GetNewCode(this.Name);
                    Common.EnableTextBox(false, txtCode);
                    txtName.Focus();

                }
                else
                {
                    txtCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void chkAutoCompleationCard_CheckedChanged(object sender, EventArgs e)
        {            
            try
            {
                if (chkAutoCompleationCard.Checked)
                {
                    Common.SetAutoComplete(txtCode, autoCompleteCode, chkAutoCompleationCard.Checked);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {            
            try
            {
                if (cmbType.SelectedIndex == 0)
                { Common.SetFocus(e, txtDiscount); }
                else
                { Common.SetFocus(e, txtMinimumPoints); }
            }
            catch (Exception ex)
            {
                if (chkAutoCompleationCard.Checked)
                {
                    Common.SetAutoComplete(txtCode, autoCompleteCode, chkAutoCompleationCard.Checked);
                }
            }
            
        }

        private void txtMinimumPoints_KeyDown(object sender, KeyEventArgs e)
        {            
            try
            {
                if (cmbType.SelectedIndex == 0)
                { Common.SetFocus(e, txtPointValue); }
                else if (cmbType.SelectedIndex == 1)
                { Common.SetFocus(e, txtPointValue); }
                else if (cmbType.SelectedIndex == 2)
                { Common.SetFocus(e, txtDiscount); }
            }
            catch (Exception ex)
            {
                Common.SetAutoComplete(txtCode, autoCompleteCode, chkAutoCompleationCard.Checked);
            }
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {            
            try
            {
                Common.SetFocus(e, txtPointValue);
            }
            catch (Exception ex)
            {
                Common.SetAutoComplete(txtCode, autoCompleteCode, chkAutoCompleationCard.Checked);
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text.Trim() != string.Empty)
                {
                    CardMasterService cardMasterService = new CardMasterService();

                    existingCardMaster = cardMasterService.GetCardMasterByName(txtName.Text.Trim());
 
                    if (existingCardMaster != null)
                    {
                        txtCode.Text = existingCardMaster.CardCode.Trim();
                        txtName.Text = existingCardMaster.CardName.Trim();
                        txtDescription.Text = existingCardMaster.Remark.Trim();
                        txtDiscount.Text = existingCardMaster.Discount.ToString();
                        txtMinimumPoints.Text = existingCardMaster.MinimumPoints.ToString();
                        txtPointValue.Text = existingCardMaster.PointValue.ToString();
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        txtDescription.Focus();

                    }
                    else
                    {
                        //if (chkAutoClear.Checked)
                        //    Common.ClearTextBox(txtName, txtDescription,txtPointValue,txtDiscount,txtMinimumPoints);
                        //if (btnNew.Enabled)
                        //    if (Toast.Show("Card  - " + txtCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                        //        btnNew.PerformClick();

                        //if (btnSave.Enabled)
                        //{
                        //    Common.EnableTextBox(false, txtCode);
                        //}
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {

        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {                    
            try
            {
                if (cmbType.SelectedIndex == 0)
                {
                    txtDiscount.Enabled = true;
                    txtPointValue.Enabled = false;
                    txtMinimumPoints.Enabled = false;

                    txtPointValue.Text = string.Empty;
                    txtMinimumPoints.Text = string.Empty;

                }
                else if (cmbType.SelectedIndex == 1)
                {
                    txtDiscount.Enabled = false;
                    txtPointValue.Enabled = true;
                    txtMinimumPoints.Enabled = true;

                    txtDiscount.Text = string.Empty;
                }
                else if (cmbType.SelectedIndex == 2)
                {
                    txtDiscount.Enabled = true;
                    txtPointValue.Enabled = true;
                    txtMinimumPoints.Enabled = true;
                }
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
                if (cmbType.SelectedIndex > 0)
                {
                    Common.SetFocus(e, txtCode);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
    }
}
