using Domain;
using Service;
using Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Windows
{
    /// <summary>
    /// Developed by Nuwan 
    /// </summary>
    public partial class FrmLostAndRenew : FrmBaseMasterForm
    {
        LoyaltyCustomer existingLoyaltyCustomer;
        string LoyalityCustomerCode;
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        public FrmLostAndRenew()
        {
            InitializeComponent();
        }

        public override void InitializeForm()
        {
            try
            {
                Common.ClearForm(this);
                Common.EnableTextBox(true, txtCustomerCode, txtCustomerName);
                Common.EnableButton(false, btnSave);
                lblCPoints.Text = "0.00";
                lblEPoints.Text = "0.00";
                lblRPoints.Text = "0.00";

                dtpIssuedDate.Value = Common.GetSystemDate();
                dtpRenewedDate.Value = Common.GetSystemDate();

                Common.EnableTextBox(true, txtCustomerCode, txtNewCustomerCode, txtCardNo, txtNewCardNo, txtNicNo);

                this.ActiveControl = txtCustomerCode;
                txtCustomerCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void FormLoad()
        {
            txtNewCustomerCode.MaxLength = 7;
            txtCustomerCode.MaxLength = 7;
            txtCustomerCode.Focus();

            this.ActiveControl = txtCustomerCode;
            txtCustomerCode.Focus();

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

            documentID = autoGenerateInfo.DocumentID;

            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);

        }
      

        private bool ValidateControls()
        {
            return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtCustomerCode, txtNewCustomerCode, txtCardNo);
        }

        public override void Save()
        {
            try
            {
                if ((Toast.Show("", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                { return; }

                string newCardNo = "";
                if (ValidateControls() == false) { return; }

                LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                LostAndRenewService lostAndRenewService = new LostAndRenewService();
                LostAndRenew lostAndRenew = new LostAndRenew();

                existingLoyaltyCustomer = loyaltyCustomerService.GetLoyaltyCustomerByCode(txtCustomerCode.Text.Trim());
                if (existingLoyaltyCustomer == null || existingLoyaltyCustomer.LoyaltyCustomerID == 0)
                {
                    Toast.Show("Customer code - " + txtCustomerCode.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }

                lostAndRenew.LoyaltyCustomerID = existingLoyaltyCustomer.LoyaltyCustomerID;
                lostAndRenew.OldCardNo = txtCardNo.Text.Trim();
                if (string.IsNullOrEmpty(txtNewCardNo.Text.Trim())) 
                { 
                    lostAndRenew.NewCardNo = txtCardNo.Text.Trim(); 
                    newCardNo = txtCardNo.Text.Trim(); 
                }
                else 
                { 
                    lostAndRenew.NewCardNo = txtNewCardNo.Text.Trim();
                    newCardNo = txtNewCardNo.Text.Trim(); 
                }
                lostAndRenew.OldCustomerCode = txtCustomerCode.Text.Trim();
                lostAndRenew.NewCustomerCode = txtNewCustomerCode.Text.Trim();
                lostAndRenew.OldEncodeNo = txtCustomerCode.Text.Trim();
                lostAndRenew.NewEncodeNo = txtNewCustomerCode.Text.Trim();
                
                lostAndRenew.RenewedDate = dtpRenewedDate.Value;
                lostAndRenew.Remark = txtRemark.Text.Trim();

                if (!lostAndRenewService.UpdateLoyaltyCustomerDetails(txtCustomerCode.Text.Trim(), txtNewCustomerCode.Text.Trim(), newCardNo, txtRemark.Text.Trim()))
                {
                    Toast.Show("Error found", Toast.messageType.Information, Toast.messageAction.General);
                    return;
                }
                else
                {
                    lostAndRenewService.AddlostAndRenew(lostAndRenew);
                }

                Toast.Show("Successfully saved", Toast.messageType.Information, Toast.messageAction.General);

                InitializeForm();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtNewCustomerCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void chkAutoCompleationCustomer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoCompleationCustomer.Checked)
                {
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                    Common.SetAutoComplete(txtCustomerCode, loyaltyCustomerService.GetAllLoyaltyCustomerCodes(), chkAutoCompleationCustomer.Checked);
                    Common.SetAutoComplete(txtCustomerName, loyaltyCustomerService.GetAllLoyaltyCustomerNames(), chkAutoCompleationCustomer.Checked);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationNic_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoCompleationNic.Checked)
                {
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                    Common.SetAutoComplete(txtNicNo, loyaltyCustomerService.GetAllNicNos(), chkAutoCompleationNic.Checked);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCustomerCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerCode.Text.Trim() != string.Empty)
                {
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                    CardMaster cardMaster = new CardMaster();
                    CardMasterService cardMasterService = new CardMasterService();

                    existingLoyaltyCustomer = loyaltyCustomerService.GetLoyaltyCustomerByCode(txtCustomerCode.Text.Trim());
                    if (existingLoyaltyCustomer != null)
                    {
                        txtCustomerCode.Text = existingLoyaltyCustomer.CustomerCode;
                        txtCardNo.Text = existingLoyaltyCustomer.CardNo;
                        txtCustomerName.Text = existingLoyaltyCustomer.CustomerName.Trim();
                        txtNicNo.Text = existingLoyaltyCustomer.NicNo.Trim();
                        txtNameOnCard.Text = existingLoyaltyCustomer.NameOnCard.Trim();
                        dtpIssuedDate.Value = existingLoyaltyCustomer.IssuedOn;

                        cardMaster = cardMasterService.GetCardMasterById(existingLoyaltyCustomer.CardMasterID);
                        if (cardMaster != null) { txtCardType.Text = cardMaster.CardName.Trim(); }

                        lblCPoints.Text = Common.ConvertDecimalToStringCurrency(existingLoyaltyCustomer.CPoints);
                        lblEPoints.Text = Common.ConvertDecimalToStringCurrency(existingLoyaltyCustomer.EPoints);
                        lblRPoints.Text = Common.ConvertDecimalToStringCurrency(existingLoyaltyCustomer.RPoints);

                        txtRemark.Text = existingLoyaltyCustomer.Remark;

                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        Common.EnableTextBox(false, txtCustomerCode, txtNicNo, txtCardNo);
                    }
                    else
                    {
                        Toast.Show("Customer code - " + txtCustomerCode.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtNicNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtNicNo.Text.Trim() != string.Empty)
                {
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                    CardMaster cardMaster = new CardMaster();
                    CardMasterService cardMasterService = new CardMasterService();

                    existingLoyaltyCustomer = loyaltyCustomerService.GetLoyaltyCustomerByNicNo(txtNicNo.Text.Trim());
                    if (existingLoyaltyCustomer != null)
                    {
                        txtCustomerCode.Text = existingLoyaltyCustomer.CustomerCode;
                        txtCardNo.Text = existingLoyaltyCustomer.CardNo;
                        txtCustomerName.Text = existingLoyaltyCustomer.CustomerName.Trim();
                        txtNicNo.Text = existingLoyaltyCustomer.NicNo.Trim();
                        txtNameOnCard.Text = existingLoyaltyCustomer.NameOnCard.Trim();
                        dtpIssuedDate.Value = existingLoyaltyCustomer.IssuedOn;

                        cardMaster = cardMasterService.GetCardMasterById(existingLoyaltyCustomer.CardMasterID);
                        if (cardMaster != null) { txtCardType.Text = cardMaster.CardName.Trim(); }

                        lblCPoints.Text = Common.ConvertDecimalToStringCurrency(existingLoyaltyCustomer.CPoints);
                        lblEPoints.Text = Common.ConvertDecimalToStringCurrency(existingLoyaltyCustomer.EPoints);
                        lblRPoints.Text = Common.ConvertDecimalToStringCurrency(existingLoyaltyCustomer.RPoints);

                        txtRemark.Text = existingLoyaltyCustomer.Remark;

                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        Common.EnableTextBox(false, txtCustomerCode, txtNicNo, txtCardNo);
                    }
                    else
                    {
                        Toast.Show("Nic No - " + txtNicNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtNicNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    LoyalityCustomerCode = string.Empty;

                    List<LoyaltyCustomer> loyaltyCustomerList = new List<LoyaltyCustomer>();
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

                    loyaltyCustomerList = loyaltyCustomerService.GetLoyalityCustomerlistByNicNo(txtNicNo.Text.Trim());

                    if (loyaltyCustomerList.Count > 1)
                    {
                        FrmLoyaltyCustomerGrid frmLoyaltyCustomerGrid = new FrmLoyaltyCustomerGrid(loyaltyCustomerList);
                        frmLoyaltyCustomerGrid.ShowDialog();

                        txtCustomerCode.Text = LoyalityCustomerCode;

                        if (!string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
                        {
                            txtCustomerCode_Validated(this, e);
                        }
                        else { txtNicNo.Focus(); return; }
                    }
                    else
                    {
                        txtNewCustomerCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void SetCustomerCode(string customerCode)
        {
            LoyalityCustomerCode = customerCode;
        }


        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtNewCardNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCardNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtCardNo.Text.Trim() != string.Empty)
                {
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                    CardMaster cardMaster = new CardMaster();
                    CardMasterService cardMasterService = new CardMasterService();

                    existingLoyaltyCustomer = loyaltyCustomerService.GetLoyaltyCustomerByCardNo(txtCardNo.Text.Trim());
                    if (existingLoyaltyCustomer != null)
                    {
                        txtCustomerCode.Text = existingLoyaltyCustomer.CustomerCode;
                        txtCardNo.Text = existingLoyaltyCustomer.CardNo;
                        txtCustomerName.Text = existingLoyaltyCustomer.CustomerName.Trim();
                        txtNicNo.Text = existingLoyaltyCustomer.NicNo.Trim();
                        txtNameOnCard.Text = existingLoyaltyCustomer.NameOnCard.Trim();
                        dtpIssuedDate.Value = existingLoyaltyCustomer.IssuedOn;

                        cardMaster = cardMasterService.GetCardMasterById(existingLoyaltyCustomer.CardMasterID);
                        if (cardMaster != null) { txtCardType.Text = cardMaster.CardName.Trim(); }

                        lblCPoints.Text = Common.ConvertDecimalToStringCurrency(existingLoyaltyCustomer.CPoints);
                        lblEPoints.Text = Common.ConvertDecimalToStringCurrency(existingLoyaltyCustomer.EPoints);
                        lblRPoints.Text = Common.ConvertDecimalToStringCurrency(existingLoyaltyCustomer.RPoints);

                        txtRemark.Text = existingLoyaltyCustomer.Remark;

                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        Common.EnableTextBox(false, txtCustomerCode, txtNicNo, txtCardNo);
                    }
                    else
                    {
                        Toast.Show("Card No  - " + txtCardNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpRenewedDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                txtRemark.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationCardNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoCompleationCardNo.Checked)
                {
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                    Common.SetAutoComplete(txtCardNo, loyaltyCustomerService.GetAllCustomersCardNos(), chkAutoCompleationCardNo.Checked);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtNewCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtNewCustomerCode_Validated(this, e);
                }
            }
            catch (Exception ex)
            {
               Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtNewCustomerCode_Validated(object sender, EventArgs e)
        {
            try
            {
                LostAndRenewService lostAndRenewService = new LostAndRenewService();

                if (!string.IsNullOrEmpty(txtNewCustomerCode.Text.Trim()))
                {
                    if (txtNewCustomerCode.TextLength == 7)
                    {
                        if (lostAndRenewService.CheckNewCodeExists(txtNewCustomerCode.Text.Trim()))
                        {
                            Toast.Show("This Code already exists", Toast.messageType.Information, Toast.messageAction.General);
                            txtNewCustomerCode.SelectAll();
                            txtNewCustomerCode.Focus();
                            return;
                        }
                        else
                        {
                            txtNewCardNo.Focus();
                        }
                    }
                    else
                    {
                        Toast.Show("Code length must be 7", Toast.messageType.Information, Toast.messageAction.General);
                        txtNewCustomerCode.Focus();
                        return;
                    }
                }
                else
                {
                    txtNewCardNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtNewCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtNewCardNo_Validated(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtNewCardNo_Validated(object sender, EventArgs e)
        {
            try
            {
                LostAndRenewService lostAndRenewService = new LostAndRenewService();

                if (!string.IsNullOrEmpty(txtNewCardNo.Text.Trim()))
                {
                    if (lostAndRenewService.CheckNewCardNoExists(txtNewCardNo.Text.Trim()))
                    {
                        Toast.Show("This Card no already exists ", Toast.messageType.Information, Toast.messageAction.General);
                        txtNewCardNo.SelectAll();
                        txtNewCardNo.Focus();
                        return;
                    }
                    else
                    {
                        dtpRenewedDate.Focus();
                    }
                }
                else
                {
                    dtpRenewedDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
        
    }
}
