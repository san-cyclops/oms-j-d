using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using Domain;
using Report.CRM;
using Service;
using Utility;
using System.IO;
using Report;
using Report.CRM.Reports;

namespace UI.Windows
{
    public partial class FrmLoyaltyCustomer : UI.Windows.FrmBaseMasterForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        LoyaltyCustomer existingLoyaltyCustomer;
        LoyaltySuplimentary existingSuplimentary;
        string imagename;
        Customer existingCustomer;
        AccLedgerAccount existingLedgerAccount;
        ReferenceType existingReferenceType;
        CardMaster existingCard;
        AccLedgerAccount existingAccLedgerAccount;
        DataTable dtSuplimentaryGrid = new DataTable();
        int documentID = 0;

        static string LoyalityCustomerCode;

        public FrmLoyaltyCustomer()
        {
            InitializeComponent();
        }

        private void btnBrows_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                //specify your own initial directory
                fldlg.InitialDirectory = @":D\";
                //this will allow only those file extensions to be added
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imagename = fldlg.FileName;
                    Bitmap newimg = new Bitmap(imagename);
                    pbLgsCustomer.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbLgsCustomer.Image = (Image)newimg;
                }
                fldlg = null;
            }

            catch (Exception ex)
            {
                if (ex is System.ArgumentException)
                {
                    imagename = string.Empty;
                    MessageBox.Show(ex.ToString());
                }
                if (ex is System.Exception)
                {
                    Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                }
            }
            
        }

        private void LoadSupplementary(long loyalityId)
        {
            LoyaltySuplimentaryService loyaltySuplimentaryService = new LoyaltySuplimentaryService();
            dgvSupplementaryCard.DataSource = null;
            dtSuplimentaryGrid = loyaltySuplimentaryService.GetLoyaltySuplimentarys(loyalityId);
            if (dtSuplimentaryGrid.Rows.Count > 0)
                dgvSupplementaryCard.DataSource = dtSuplimentaryGrid;
            else
                dgvSupplementaryCard.DataSource = null;
            dgvSupplementaryCard.Refresh();

        }

        private void txtCustomerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerCode.Text.Trim() != string.Empty)
                {
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

                    existingLoyaltyCustomer = loyaltyCustomerService.GetLoyaltyCustomerByCode(txtCustomerCode.Text.Trim());

                    if (existingLoyaltyCustomer != null)
                    {
                        LoadCustomer(existingLoyaltyCustomer);
                        LoadSupplementary(existingLoyaltyCustomer.LoyaltyCustomerID);
                        txtCardNo.Text = existingLoyaltyCustomer.CardNo.Trim();                        
                        Common.EnableButton(true, btnDelete, btnSave);
                        Common.EnableButton(false, btnNew);
                        //Common.EnableTextBox(false, txtCustomerCode, txtNic);
                        Common.EnableTextBox(false, txtCustomerCode);
                    }
                    else
                    {
                        //if (chkAutoClear.Checked)
                        //{ Common.ClearTextBox(txtCustomerCode, txtRemark); }

                        if (btnNew.Enabled)
                        {
                            if (Toast.Show("" + this.Text + " - " + txtCustomerCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                            { btnNew.PerformClick(); }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        public override void FormLoad()
        {            
            chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
            dgvSupplementaryCard.AutoGenerateColumns = false;
            dgvSupplementaryCard.AllowUserToAddRows = false;
            AddCustomCoulmnstoGridCardType();
            AddCustomCoulmnstoGridRelationship();
            AddCustomCoulmnstoGridStatus();

            if (Common.EntryLevel.Equals(1))
            {
                lblLedger.Text = lblLedger.Text + "*";
                lblOtherLedger.Text = lblOtherLedger.Text + "*";
            }

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

            documentID = autoGenerateInfo.DocumentID;
            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);


            base.FormLoad();

            this.ActiveControl = txtCustomerCode;
            txtCustomerCode.Focus();            
         
        }

        public override void InitializeForm()
        {
            try
            {                
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                CardMasterService cardMasterService = new CardMasterService();
             
                chkAutoCompleationCustomer.Checked = true;
                chkAutoCompleationCardNo.Checked = true;
                chkAutoCompleationNic.Checked = true;
                chkAutoCompleationCreditCustomer.Checked = true;
                chkAutoCompleationLedger.Checked = true;
                chkAutoCompleationLedger2.Checked = true;

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete, btnView);
                Common.EnableTextBox(true, txtCustomerCode);

                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                SetAutoBindRecords(cmbTitle,lookUpReferenceService.GetLookUpReferenceValues(
                                       ((int) LookUpReference.TitleType).ToString()));
                SetAutoBindRecords(cmbGender,
                                   lookUpReferenceService.GetLookUpReferenceValues(
                                       ((int) LookUpReference.GenderType).ToString()));
                SetAutoBindRecords(cmbNationality,
                                   lookUpReferenceService.GetLookUpReferenceValues(
                                       ((int) LookUpReference.Nationality).ToString()));
                SetAutoBindRecords(cmbReligion,
                                   lookUpReferenceService.GetLookUpReferenceValues(
                                       ((int) LookUpReference.Religion).ToString()));

                SetAutoBindRecords(cmbRace,
                                   lookUpReferenceService.GetLookUpReferenceValues(
                                       ((int)LookUpReference.Race).ToString()));

                SetAutoBindRecords(cmbDistrict,
                                   lookUpReferenceService.GetLookUpReferenceValues(
                                       ((int) LookUpReference.District).ToString()));
                SetAutoBindRecords(cmbCivilStatus,
                                   lookUpReferenceService.GetLookUpReferenceValues(
                                       ((int) LookUpReference.CivilStatus).ToString()));
                SetAutoBindRecords(cmbTvChannel,
                                   lookUpReferenceService.GetLookUpReferenceValues(
                                       ((int) LookUpReference.TvChannel).ToString()));
                SetAutoBindRecords(cmbNewsPaper,
                                   lookUpReferenceService.GetLookUpReferenceValues(
                                       ((int) LookUpReference.NewsPaper).ToString()));
                SetAutoBindRecords(cmbRadioChannel,
                                   lookUpReferenceService.GetLookUpReferenceValues(
                                       ((int) LookUpReference.RadioChannel).ToString()));
                SetAutoBindRecords(cmbMagazine,
                                   lookUpReferenceService.GetLookUpReferenceValues(
                                       ((int) LookUpReference.Magazine).ToString()));
                SetAutoBindRecords(cmbDeliverTo,
                                   lookUpReferenceService.GetLookUpReferenceValues(
                                       ((int) LookUpReference.DeliverTo).ToString()));
                SetAutoBindRecords(cmbCardType, cardMasterService.GetAllCardNames());

                cmbDeliverTo.SelectedIndex = 0;

                dtpIssuedOn.Enabled = false;
                dtpExpiryDate.Enabled = false;
                dtpRenewedOn.Enabled = false;
                //dtpDateOfBirth.MaxDate = DateTime.Today;

                lblCPoints.Text = string.Empty;
                lblEPoints.Text = string.Empty;
                lblRPoints.Text = string.Empty;
                lblCardTypeDisplay.Text = string.Empty;
                lblNameOnCardDisplay.Text = string.Empty;
                lblCreatedByDisplay.Text = string.Empty;

                Common.ClearForm(this);

                pbLgsCustomer.Image = UI.Windows.Properties.Resources.Default_Loyalty_Customer;
                tabLgsCustomer.SelectedTab = tbpGeneral;

                txtCustomerCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public static void SetAutoBindRecords(ComboBox comboBox, string[] stringCollection)
        {
            AutoCompleteStringCollection autoCompleteCode = new AutoCompleteStringCollection();
            autoCompleteCode.AddRange(stringCollection);
            comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBox.AutoCompleteCustomSource = autoCompleteCode;
            comboBox.DataSource = autoCompleteCode;
            comboBox.SelectedIndex = -1;
        }

        private bool ValidateControls()
        {
            return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtCustomerCode, txtCustomerName, txtCardNo, cmbTitle, cmbGender, txtAddress1, txtAddress2, txtMobile);         
        }

        private bool ValidateLedger()
        {
            return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtLedgerCode, txtLedgerName, txtLedgerCode2, txtLedgerName2);
        }

        private LoyaltyCustomer FillLoyaltyCustomer()
        {            
                
                CustomerService customerService = new CustomerService();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                CardMasterService cardMasterService = new CardMasterService();

                existingLoyaltyCustomer.CustomerCode = txtCustomerCode.Text.Trim();
                existingLoyaltyCustomer.CustomerName = txtCustomerName.Text.Trim();
                existingLoyaltyCustomer.CardNo = txtCardNo.Text.Trim();

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.TitleType).ToString(), cmbTitle.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingLoyaltyCustomer.CustomerTitle = existingReferenceType.LookupKey;
                }
                else
                {
                    existingLoyaltyCustomer.CustomerTitle = 0;
                }

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.GenderType).ToString(), cmbGender.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingLoyaltyCustomer.Gender = existingReferenceType.LookupKey;
                }
                else
                {
                    existingLoyaltyCustomer.Gender = 0;
                }

                existingLoyaltyCustomer.NicNo = txtNic.Text.Trim();
                existingLoyaltyCustomer.ReferenceNo = txtReferenceNo.Text.Trim();

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.Nationality).ToString(), cmbNationality.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingLoyaltyCustomer.Nationality = existingReferenceType.LookupKey;
                }
                else
                {
                    existingLoyaltyCustomer.Nationality = 0;
                }
  
                existingCustomer = customerService.GetCustomersByCode(txtCreditCustomerCode.Text);
                if (existingCustomer != null)
                    existingLoyaltyCustomer.CustomerId = existingCustomer.CustomerID;

                existingLoyaltyCustomer.DateOfBirth = dtpDateOfBirth.Value;
                existingLoyaltyCustomer.Age = Common.ConvertStringToInt(txtAge.Text);

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.Religion).ToString(), cmbReligion.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingLoyaltyCustomer.Religion = existingReferenceType.LookupKey;
                }
                else
                {
                    existingLoyaltyCustomer.Religion = 0;
                }

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.Race).ToString(), cmbRace.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingLoyaltyCustomer.Race = existingReferenceType.LookupKey;
                }
                else
                {
                    existingLoyaltyCustomer.Race = 0;
                }

                existingLoyaltyCustomer.Address1 = txtAddress1.Text.Trim();
                existingLoyaltyCustomer.Address2 = txtAddress2.Text.Trim();
                existingLoyaltyCustomer.Address3 = txtAddress3.Text.Trim();

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.District).ToString(), cmbDistrict.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingLoyaltyCustomer.District = existingReferenceType.LookupKey;
                }
                else
                {
                    existingLoyaltyCustomer.District = 0;
                }

                existingLoyaltyCustomer.LandMark = txtLandMark.Text.Trim();
                existingLoyaltyCustomer.Email = txtEmail.Text.Trim();
                existingLoyaltyCustomer.Telephone = txtTelephone.Text.Trim();
                existingLoyaltyCustomer.Mobile = txtMobile.Text.Trim();
                existingLoyaltyCustomer.Fax = txtFax.Text.Trim();
                existingLoyaltyCustomer.Organization = txtOrganization.Text.Trim();
                existingLoyaltyCustomer.Occupation = txtOccupation.Text.Trim();
                existingLoyaltyCustomer.WorkAddres1 = txtWorkAddress1.Text.Trim();
                existingLoyaltyCustomer.WorkAddres2 = txtWorkAddress2.Text.Trim();
                existingLoyaltyCustomer.WorkAddres3 = txtWorkAddress3.Text.Trim();
                existingLoyaltyCustomer.WorkEmail = txtWorkEmail.Text.Trim();
                existingLoyaltyCustomer.WorkTelephone = txtWorkTelephone.Text.Trim();
                existingLoyaltyCustomer.WorkMobile = txtWorkMobile.Text.Trim();
                existingLoyaltyCustomer.WorkFax = txtWorkFax.Text.Trim();
                existingLoyaltyCustomer.NameOnCard = txtNameOnCard.Text.Trim();

                existingCard = cardMasterService.GetCardMasterByName(cmbCardType.Text);
                if (existingCard != null)
                {
                    existingLoyaltyCustomer.CardMasterID = existingCard.CardMasterID;
                    existingLoyaltyCustomer.LoyaltyType = existingCard.CardType;
                }

                existingLoyaltyCustomer.CardIssued = chkIssued.Checked;
                existingLoyaltyCustomer.IssuedOn = dtpIssuedOn.Value;
                existingLoyaltyCustomer.ExpiryDate = dtpExpiryDate.Value;
                existingLoyaltyCustomer.RenewedOn = dtpRenewedOn.Value;
                existingLoyaltyCustomer.SpouseName = txtSpouseName.Text.Trim();

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.CivilStatus).ToString(), cmbCivilStatus.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingLoyaltyCustomer.CivilStatus = existingReferenceType.LookupKey;
                }
                else
                {
                    existingLoyaltyCustomer.CivilStatus = 0;
                }
   
                existingLoyaltyCustomer.FemaleAdults = Common.ConvertStringToInt(txtFemaleAdults.Text.Trim());
                existingLoyaltyCustomer.MaleAdults = Common.ConvertStringToInt(txtMaleAdults.Text.Trim());
                existingLoyaltyCustomer.Childrens = Common.ConvertStringToInt(txtChildrens.Text.Trim());
                existingLoyaltyCustomer.SpouseDateOfBirth = dtpSpouseDateOfBirth.Value;
                existingLoyaltyCustomer.Anniversary = dtpAnniversary.Value;
                existingLoyaltyCustomer.SinhalaHinduNewYear = chkSinhala.Checked;
                existingLoyaltyCustomer.ThaiPongal = chkThai.Checked;
                existingLoyaltyCustomer.Wesak = chkWesak.Checked;
                existingLoyaltyCustomer.HajFestival = chkHaj.Checked;
                existingLoyaltyCustomer.Ramazan = chkRamazan.Checked;
                existingLoyaltyCustomer.Xmas = chkXmas.Checked;

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.TvChannel).ToString(), cmbTvChannel.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingLoyaltyCustomer.FavoriteTvChannel = existingReferenceType.LookupKey;
                }
                else
                {
                    existingLoyaltyCustomer.FavoriteTvChannel = 0;
                }

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.RadioChannel).ToString(), cmbRadioChannel.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingLoyaltyCustomer.FavoriteRadioChannels = existingReferenceType.LookupKey;
                }
                else
                {
                    existingLoyaltyCustomer.FavoriteRadioChannels = 0;
                }

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.NewsPaper).ToString(), cmbNewsPaper.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingLoyaltyCustomer.FavoriteNewsPapers = existingReferenceType.LookupKey;
                }
                else
                {
                    existingLoyaltyCustomer.FavoriteNewsPapers = 0;
                }

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.Magazine).ToString(), cmbMagazine.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingLoyaltyCustomer.FavoriteMagazines = existingReferenceType.LookupKey;
                }
                else
                {
                    existingLoyaltyCustomer.FavoriteNewsPapers = 0;
                }
                
                existingLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtLedgerCode.Text);
                if (existingLedgerAccount != null)
                    existingLoyaltyCustomer.LedgerId = existingLedgerAccount.AccLedgerAccountID;

                existingLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtLedgerCode2.Text);
                if(existingLedgerAccount != null)
                    existingLoyaltyCustomer.LedgerId2 = existingLedgerAccount.AccLedgerAccountID;

                existingLoyaltyCustomer.CreditLimit = Common.ConvertStringToDecimal(txtCreditLimit.Text);
                existingLoyaltyCustomer.CreditPeriod = Common.ConvertStringToInt(txtCreditPeriod.Text);

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.DeliverTo).ToString(), cmbDeliverTo.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingLoyaltyCustomer.DeliverTo = existingReferenceType.LookupKey;
                }
                else
                {
                    existingLoyaltyCustomer.DeliverTo = 0;
                }

                existingLoyaltyCustomer.DeliverToAddress = txtDeliverToAddress.Text.Trim();
                existingLoyaltyCustomer.CustomerSince = dtpCustomerSince.Value;
                existingLoyaltyCustomer.SendUpdatesViaEmail = chkSendEmail.Checked;
                existingLoyaltyCustomer.SendUpdatesViaSms = chkSendSms.Checked;
                existingLoyaltyCustomer.IsDelete = false;
                existingLoyaltyCustomer.IsSuspended = chkLost.Checked;
                existingLoyaltyCustomer.IsBlackListed = chkBlackListed.Checked;
                existingLoyaltyCustomer.IsCreditAllowed = chkCreditAllowed.Checked;
                existingLoyaltyCustomer.Active = chkActive.Checked;
                existingLoyaltyCustomer.AcitiveDate = dtpActivateDate.Value;
                existingLoyaltyCustomer.Remark = txtRemark.Text.Trim();

                existingLoyaltyCustomer.CPoints = Common.ConvertStringToDecimal(lblCPoints.Text);
                existingLoyaltyCustomer.EPoints = Common.ConvertStringToDecimal(lblEPoints.Text);
                existingLoyaltyCustomer.RPoints = Common.ConvertStringToDecimal(lblRPoints.Text);

                existingLoyaltyCustomer.IsReDimm = true;

                MemoryStream stream = new MemoryStream();
                pbLgsCustomer.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] pic = stream.ToArray();
                existingLoyaltyCustomer.CustomerImage = pic;

                return existingLoyaltyCustomer;           
           
        }

        public override void ClearForm()
        {
            try
            {
                txtNic.Enabled = true;
                txtCardNo.Enabled = true;

                dgvSupplementaryCard.DataSource = null;
                if (dgvSupplementaryCard.Rows.Count > 1)
                {
                    foreach (DataGridViewRow dr in dgvSupplementaryCard.Rows)
                        dgvSupplementaryCard.Rows.Remove(dr);
                }

                base.ClearForm();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
           
        }

        private void Savesuplimenty()
        {
            // suplimenty
            if (existingCustomer != null)
            {

               CardMasterService cardMasterService = new CardMasterService();
               LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
               LoyaltySuplimentaryService loyaltySuplimentaryService = new LoyaltySuplimentaryService();
                //existingSuplimentary = new LoyaltySuplimentary();
                if (dgvSupplementaryCard.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvSupplementaryCard.Rows)
                    {

                        if (dgvSupplementaryCard["CardNo", row.Index].Value != null && !dgvSupplementaryCard["CardNo", row.Index].Value.ToString().Trim().Equals(string.Empty))
                        {

                            existingSuplimentary = loyaltySuplimentaryService.GetLoyaltySuplimentaryById(existingLoyaltyCustomer.LoyaltyCustomerID, Common.ConvertStringToLong(dgvSupplementaryCard["LcId", row.Index].Value.ToString()));
                            if (existingSuplimentary == null)
                            {
                                existingSuplimentary = new LoyaltySuplimentary();
                            }

                            existingSuplimentary.CardTypeId = 1;// Common.ConvertStringToLong(dgvSupplementaryCard["CardType", row.Index].Value.ToString());
                            existingSuplimentary.CardNo =dgvSupplementaryCard["CardNo", row.Index].Value.ToString().Trim();
                            existingSuplimentary.Name = dgvSupplementaryCard["SName", row.Index].Value.ToString().Trim();

                            existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.Relationship).ToString(),dgvSupplementaryCard["Relationship", row.Index].Value.ToString());
                            if (existingReferenceType != null)
                            {
                                existingSuplimentary.RelationShipId= existingReferenceType.LookupKey;
                            }

                            existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.Status).ToString(), dgvSupplementaryCard["Status", row.Index].Value.ToString());
                            if (existingReferenceType != null)
                            {
                                existingSuplimentary.Status = existingReferenceType.LookupKey;
                            }
                              
                            
                            existingSuplimentary.LoyaltyCustomerID = existingLoyaltyCustomer.LoyaltyCustomerID;
                            existingSuplimentary.IsDelete = false;


                          
                            if (existingSuplimentary.LoyaltySuplimentaryID.Equals(0))
                            {
                                loyaltySuplimentaryService.AddLoyaltySuplimentarys(existingSuplimentary);
                            }
                            else
                            {
                                loyaltySuplimentaryService.UpdateLoyaltySuplimentarys(existingSuplimentary);
                            }
                        }
                    }

                }
            }
        }

        public override void Save()
        {
            try
            {
                LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();


                if (ValidateControls() == false) { return; }
                
                if (Common.EntryLevel.Equals(1))
                {
                    if (!ValidateLedger()) { return; }
                }

                bool isNew = false;

               //existingLgsCustomer = lgsCustomerervice.GetLgsCustomerByCode(txtCustomerCode.Text.Trim());
                existingLoyaltyCustomer = loyaltyCustomerService.GetLoyaltyCustomerByCustomerCodeOrCardNo(txtCustomerCode.Text.Trim(), txtCardNo.Text.Trim());
                if (existingLoyaltyCustomer == null || existingLoyaltyCustomer.LoyaltyCustomerID == 0)
                {
                    existingLoyaltyCustomer = new LoyaltyCustomer();
                    isNew = true;
                }

                FillLoyaltyCustomer();

                if (existingLoyaltyCustomer.LoyaltyCustomerID == 0)
                {
                    if ((Toast.Show("Loyalty Customer  - " + existingLoyaltyCustomer.CustomerCode + " - " + existingLoyaltyCustomer.CustomerName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    { return;}

                    if (loyaltyCustomerService.CheckExistingNic(txtNic.Text.Trim())) 
                    {
                        Toast.Show("NIC No", Toast.messageType.Information, Toast.messageAction.AlreadyExists);
                        txtNic.Focus();
                        txtNic.SelectAll();
                        return; 
                    }

                    if (loyaltyCustomerService.CheckExistingCardNo(txtCardNo.Text.Trim()))
                    {
                        Toast.Show("NIC No", Toast.messageType.Information, Toast.messageAction.AlreadyExists);
                        txtCardNo.Focus();
                        txtCardNo.SelectAll();
                        return;
                    }


                    loyaltyCustomerService.AddLoyaltyCustomer(existingLoyaltyCustomer);
                    Savesuplimenty();

                    if (chkAutoClear.Checked)
                    { ClearForm(); }
                    else
                    { InitializeForm(); }
                    
                    if ((Toast.Show("Loyalty Customer  - " + existingLoyaltyCustomer.CustomerCode  + " - " + existingLoyaltyCustomer.CustomerCode + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
                    { btnNew.PerformClick();}
                }
                else
                {
                    if (isNew)
                    {
                        if ((Toast.Show("Loyalty Customer  - " + existingLoyaltyCustomer.CustomerCode + " - " + existingLoyaltyCustomer.CustomerName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        { return;}
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingLoyaltyCustomer.CustomerCode + " - " + existingLoyaltyCustomer.CustomerName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("Loyalty Customer  - " + existingLoyaltyCustomer.CustomerCode + " - " + existingLoyaltyCustomer.CustomerName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        { return;}
                    }

                    //if (loyaltyCustomerService.CheckExistingNic(txtNic.Text.Trim()))
                    //{
                    //    Toast.Show("NIC No", Toast.messageType.Information, Toast.messageAction.AlreadyExists);
                    //    txtNic.Focus();
                    //    txtNic.SelectAll();
                    //    return;
                    //}

                    //if (loyaltyCustomerService.CheckExistingCardNo(txtCardNo.Text.Trim()))
                    //{
                    //    Toast.Show("NIC No", Toast.messageType.Information, Toast.messageAction.AlreadyExists);
                    //    txtCardNo.Focus();
                    //    txtCardNo.SelectAll();
                    //    return;
                    //}

                    loyaltyCustomerService.UpdateLoyaltyCustomer(existingLoyaltyCustomer);
                    Savesuplimenty();

                    if (chkAutoClear.Checked)
                    { ClearForm();}
                    else
                    { InitializeForm(); }

                    Toast.Show("Loyalty Customer  - " + existingLoyaltyCustomer.CustomerCode + " - " + existingLoyaltyCustomer.CustomerName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }
                txtCustomerCode.Focus();

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
                if (Toast.Show("Loyalty Customer  - " + existingLoyaltyCustomer.CustomerCode + " - " + existingLoyaltyCustomer.CustomerName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                    return;

                LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                existingLoyaltyCustomer = loyaltyCustomerService.GetLoyaltyCustomerByCode(txtCustomerCode.Text.Trim());

                if (existingLoyaltyCustomer != null && existingLoyaltyCustomer.LoyaltyCustomerID != 0)
                {
                    existingLoyaltyCustomer.IsDelete = true;
                    loyaltyCustomerService.UpdateLoyaltyCustomer(existingLoyaltyCustomer);
                    ClearForm();
                    Toast.Show("Loyalty Customer  - " + existingLoyaltyCustomer.CustomerCode + " - " + existingLoyaltyCustomer.CustomerName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    txtCustomerCode.Focus();
                }
                else
                {Toast.Show("Loyalty Customer  - " + existingLoyaltyCustomer.CustomerCode + " - " + existingLoyaltyCustomer.CustomerName + "", Toast.messageType.Warning, Toast.messageAction.NotExists);}


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void View()
        {
            try
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
                
                if (!string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
                {
                    crmReportGenerator.GenearateReferenceReport(autoGenerateInfo, txtCustomerCode.Text.Trim());  
                }
                else
                {
                    Form mdiMainRibbon = new MdiMainRibbon();

                    crmReportGenerator.OrganizeFormFields(autoGenerateInfo).MdiParent = mdiMainRibbon;
                    crmReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnClearImage_Click(object sender, EventArgs e)
        {
            try
            {
                pbLgsCustomer.Image = UI.Windows.Properties.Resources.Default_Customer;
                pbLgsCustomer.Refresh();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void LoadCustomer(LoyaltyCustomer existingLoyaltyCustomer)
        {
            try
            {
                txtCustomerCode.Text = existingLoyaltyCustomer.CustomerCode;
                txtCustomerName.Text = existingLoyaltyCustomer.CustomerName;

                CustomerService customerService = new CustomerService();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                CardMasterService cardMasterService = new CardMasterService();

                txtCardNo.Text = existingLoyaltyCustomer.CardNo.Trim();

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.TitleType).ToString(), existingLoyaltyCustomer.CustomerTitle);
                if (existingReferenceType != null)
                {
                    cmbTitle.Text = existingReferenceType.LookupValue;
                }

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.GenderType).ToString(), existingLoyaltyCustomer.Gender);
                if (existingReferenceType != null)
                {
                    cmbGender.Text = existingReferenceType.LookupValue;
                }
               
                txtNic.Text=existingLoyaltyCustomer.NicNo.Trim();
                txtReferenceNo.Text=existingLoyaltyCustomer.ReferenceNo;

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.Nationality).ToString(), existingLoyaltyCustomer.Nationality);
                if (existingReferenceType != null)
                {cmbNationality.Text= existingReferenceType.LookupValue;}

                existingCustomer = customerService.GetCustomersById(existingLoyaltyCustomer.CustomerId);
                if (existingCustomer != null)
                {
                    txtCreditCustomerCode.Text = existingCustomer.CustomerCode;
                    txtCreditCustomerName.Text = existingCustomer.CustomerName;
                }

 
                dtpDateOfBirth.Value = existingLoyaltyCustomer.DateOfBirth;

                txtAge.Text = existingLoyaltyCustomer.Age.ToString();

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.Religion).ToString(),existingLoyaltyCustomer.Religion);
                if (existingReferenceType != null)
                {
                    cmbReligion.Text = existingReferenceType.LookupValue;
                }

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.Race).ToString(), existingLoyaltyCustomer.Race);
                if (existingReferenceType != null)
                {
                    cmbRace.Text = existingReferenceType.LookupValue;
                }

                txtAddress1.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.Address1) ? string.Empty : existingLoyaltyCustomer.Address1;
                txtAddress2.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.Address2) ? string.Empty : existingLoyaltyCustomer.Address2;
                txtAddress3.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.Address3) ? string.Empty : existingLoyaltyCustomer.Address3;

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.District).ToString(),existingLoyaltyCustomer.District);
                if (existingReferenceType != null)
                {
                    cmbDistrict.Text  = existingReferenceType.LookupValue;
                }

                txtLandMark.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.LandMark) ? string.Empty : existingLoyaltyCustomer.LandMark;
                txtEmail.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.Email) ? string.Empty : existingLoyaltyCustomer.Email;
                txtTelephone.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.Telephone) ? string.Empty : existingLoyaltyCustomer.Telephone;
                txtMobile.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.Mobile) ? string.Empty : existingLoyaltyCustomer.Mobile;
                txtFax.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.Fax) ? string.Empty : existingLoyaltyCustomer.Fax;
                txtOrganization.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.Organization) ? string.Empty : existingLoyaltyCustomer.Organization;
                txtWorkAddress1.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.WorkAddres1) ? string.Empty : existingLoyaltyCustomer.WorkAddres1;
                txtWorkAddress2.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.WorkAddres2) ? string.Empty : existingLoyaltyCustomer.WorkAddres2;
                txtWorkAddress3.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.WorkAddres3) ? string.Empty : existingLoyaltyCustomer.WorkAddres3;
                txtWorkEmail.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.WorkEmail) ? string.Empty : existingLoyaltyCustomer.WorkEmail;
                txtWorkTelephone.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.WorkTelephone) ? string.Empty : existingLoyaltyCustomer.WorkTelephone;
                txtWorkMobile.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.WorkMobile) ? string.Empty : existingLoyaltyCustomer.WorkMobile;
                txtWorkFax.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.WorkFax) ? string.Empty : existingLoyaltyCustomer.WorkFax;

                txtNameOnCard.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.NameOnCard) ? string.Empty : existingLoyaltyCustomer.NameOnCard;
                lblNameOnCardDisplay.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.NameOnCard) ? string.Empty : existingLoyaltyCustomer.NameOnCard;

                txtOccupation.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.Occupation) ? string.Empty : existingLoyaltyCustomer.Occupation;

                existingCard = cardMasterService.GetCardMasterById(existingLoyaltyCustomer.CardMasterID);
                
                if (existingCard != null)
                { 
                    cmbCardType.Text = existingCard.CardName;
                    lblCardTypeDisplay.Text = existingCard.CardName;
                }

                lblCPoints.Text = existingLoyaltyCustomer.CPoints.ToString();
                lblEPoints.Text = existingLoyaltyCustomer.EPoints.ToString();
                lblRPoints.Text = existingLoyaltyCustomer.RPoints.ToString();

                chkIssued.Checked=existingLoyaltyCustomer.CardIssued;
                dtpIssuedOn.Value=existingLoyaltyCustomer.IssuedOn;
                dtpExpiryDate.Value=existingLoyaltyCustomer.ExpiryDate;
                dtpRenewedOn.Value=existingLoyaltyCustomer.RenewedOn;
                dtpActivateDate.Value = existingLoyaltyCustomer.AcitiveDate;   

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.CivilStatus).ToString(), existingLoyaltyCustomer.CivilStatus);
                if (existingReferenceType != null)
                {
                    cmbCivilStatus.Text = existingReferenceType.LookupValue;
                }

                txtSpouseName.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.SpouseName) ? string.Empty : existingLoyaltyCustomer.SpouseName;
                txtFemaleAdults.Text = existingLoyaltyCustomer.FemaleAdults.ToString();
                txtMaleAdults.Text = existingLoyaltyCustomer.MaleAdults.ToString();
                txtChildrens.Text = existingLoyaltyCustomer.Childrens.ToString();
                dtpSpouseDateOfBirth.Value=existingLoyaltyCustomer.SpouseDateOfBirth;
                dtpAnniversary.Value=existingLoyaltyCustomer.Anniversary;
                chkSinhala.Checked=existingLoyaltyCustomer.SinhalaHinduNewYear;
                chkThai.Checked=existingLoyaltyCustomer.ThaiPongal;
                chkWesak.Checked = existingLoyaltyCustomer.Wesak;
                chkHaj.Checked=existingLoyaltyCustomer.HajFestival;
                chkRamazan.Checked=existingLoyaltyCustomer.Ramazan;
                chkXmas.Checked=existingLoyaltyCustomer.Xmas;

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.TvChannel).ToString(),existingLoyaltyCustomer.FavoriteTvChannel);
                if (existingReferenceType != null)
                {
                     cmbTvChannel.Text=existingReferenceType.LookupValue;
                }
               

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.RadioChannel).ToString(),existingLoyaltyCustomer.FavoriteRadioChannels);
                if (existingReferenceType != null)
                {
                     cmbRadioChannel.Text= existingReferenceType.LookupValue;
                }
                 
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.NewsPaper).ToString(), existingLoyaltyCustomer.FavoriteNewsPapers);
                if (existingReferenceType != null)
                {
                    cmbNewsPaper.Text= existingReferenceType.LookupValue;
                }
 
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.Magazine).ToString(), existingLoyaltyCustomer.FavoriteMagazines);
                if (existingReferenceType != null)
                {
                    cmbMagazine.Text = existingReferenceType.LookupValue;
                }
          
                existingLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByID(existingLoyaltyCustomer.LedgerId);
                if (existingLedgerAccount != null)
                {
                    txtLedgerCode.Text = existingLedgerAccount.LedgerCode;
                    txtLedgerName.Text = existingLedgerAccount.LedgerName;
                }
                existingLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByID(existingLoyaltyCustomer.LedgerId2);
                if (existingLedgerAccount != null)
                {
                    txtLedgerCode2.Text = existingLedgerAccount.LedgerCode;
                    txtLedgerName2.Text = existingLedgerAccount.LedgerName;
                }
                txtCreditLimit.Text  = existingLoyaltyCustomer.CreditLimit.ToString();
                txtCreditPeriod.Text  = existingLoyaltyCustomer.CreditPeriod.ToString();

                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.DeliverTo).ToString(),existingLoyaltyCustomer.DeliverTo);
                if (existingReferenceType != null)
                {
                    cmbDeliverTo.Text = existingReferenceType.LookupValue;
                }


                txtDeliverToAddress.Text = string.IsNullOrEmpty(existingLoyaltyCustomer.DeliverToAddress) ? string.Empty : existingLoyaltyCustomer.DeliverToAddress;

                dtpCustomerSince.Value=existingLoyaltyCustomer.CustomerSince;
                chkSendEmail.Checked=existingLoyaltyCustomer.SendUpdatesViaEmail;
                chkSendSms.Checked=existingLoyaltyCustomer.SendUpdatesViaSms;

                chkLost.Checked = existingLoyaltyCustomer.IsSuspended;
                chkBlackListed.Checked=existingLoyaltyCustomer.IsBlackListed;
                chkCreditAllowed.Checked=existingLoyaltyCustomer.IsCreditAllowed;
                chkActive.Checked = existingLoyaltyCustomer.Active;
                txtRemark.Text = existingLoyaltyCustomer.Remark;
                lblCreatedByDisplay.Text = existingLoyaltyCustomer.CreatedUser;
                lblModifiedByDisplay.Text = existingLoyaltyCustomer.ModifiedUser;

                if (existingLoyaltyCustomer.CustomerImage != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingLoyaltyCustomer.CustomerImage);
                    pbLgsCustomer.Image = Image.FromStream(ms);
                    pbLgsCustomer.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbLgsCustomer.Refresh();
                }
                else
                {
                    pbLgsCustomer.Image = UI.Windows.Properties.Resources.Default_Loyalty_Customer;
                    pbLgsCustomer.Refresh();
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #region KeyDown

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {                
               // Common.SetFocus(e, txtCustomerCode);
               Common.SetFocus(e, cmbGender);
                //if (e.KeyCode == Keys.Tab)
                //{
                   // tabLgsCustomer.Focus();
                //}
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
                if (e.KeyCode.Equals(Keys.F3))
                {
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                    DataView dvAllReferenceData = new DataView(loyaltyCustomerService.GetAllActiveLoyalityCustomerssDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtCustomerCode);
                        txtCustomerCode_Leave(this, e);
                    }
                }
                else if (e.KeyCode.Equals(Keys.Enter))
                {
                    cmbTitle.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void cmbTitle_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtCustomerName);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabLgsCustomer, tbpGeneral);
                Common.SetFocus(e, cmbGender);

                if (e.KeyCode.Equals(Keys.F3))
                {
                    //LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                    //DataView dvAllReferenceData = new DataView(loyaltyCustomerService.GetAllActiveLoyalityCustomerssDataTable());
                    //if (dvAllReferenceData.Count != 0)
                    //{
                    //    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtCustomerCode);
                    //    txtCustomerCode_Leave(this, e);
                    //}
                }
                else if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (txtCardNo.Enabled) { txtCardNo.Focus(); }
                    else { cmbGender.Focus(); }
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

        private void cmbGender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, dtpDateOfBirth);
                //if (string.IsNullOrEmpty(txtCreditCustomerName.Text))
                //{
                //    Common.SetFocus(e, txtCreditCustomerCode);
                //}
                //else
                //{
                //    Common.SetFocus(e, dtpDateOfBirth);
                //}

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
                     
            
            
            
            
         
        }


        #endregion

        private void AddCustomCoulmnstoGridCardType()
        {
            CardMasterService cardMasterService = new CardMasterService();
            dgvSupplementaryCard.AllowUserToAddRows = true;

            dgvSupplementaryCard.Columns.Remove("CardType");

            DataGridViewComboBoxColumn comboboxColumnParentValues = new DataGridViewComboBoxColumn();
            comboboxColumnParentValues.DataSource = cardMasterService.GetAllCardMasters();
            comboboxColumnParentValues.DataPropertyName = "CardType";
            comboboxColumnParentValues.Name = "CardType";
            comboboxColumnParentValues.DisplayMember = "CardName";
            comboboxColumnParentValues.ValueMember = "CardMasterId";
            comboboxColumnParentValues.HeaderText = "Card Type";
            comboboxColumnParentValues.MaxDropDownItems = 20;
            comboboxColumnParentValues.FlatStyle = FlatStyle.Flat;
            dgvSupplementaryCard.Columns.Insert(0, comboboxColumnParentValues);
        }

        private void AddCustomCoulmnstoGridRelationship()
        {
            LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
            dgvSupplementaryCard.AllowUserToAddRows = true;
            dgvSupplementaryCard.Columns.Remove("Relationship");
            DataGridViewComboBoxColumn comboboxColumnParentValues = new DataGridViewComboBoxColumn();
            comboboxColumnParentValues.DataSource = lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.Relationship).ToString());
            comboboxColumnParentValues.DataPropertyName = "Relationship";
            comboboxColumnParentValues.Name = "Relationship";
            //comboboxColumnParentValues.DisplayMember = "LookupKey";
            //comboboxColumnParentValues.ValueMember = "LookupKey";
            comboboxColumnParentValues.HeaderText = "Relationship";

            comboboxColumnParentValues.MaxDropDownItems = 20;
            comboboxColumnParentValues.FlatStyle = FlatStyle.Flat;
            dgvSupplementaryCard.Columns.Insert(3, comboboxColumnParentValues);
        }

        private void AddCustomCoulmnstoGridStatus()
        {
            LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
            dgvSupplementaryCard.AllowUserToAddRows = true;
            dgvSupplementaryCard.Columns.Remove("Status");
            DataGridViewComboBoxColumn comboboxColumnParentValues = new DataGridViewComboBoxColumn();
            comboboxColumnParentValues.DataSource = lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.Status).ToString());
            comboboxColumnParentValues.DataPropertyName = "Status";
            comboboxColumnParentValues.Name = "Status";
            //comboboxColumnParentValues.DisplayMember = "LookupKey";
            //comboboxColumnParentValues.ValueMember = "LookupKey";
            comboboxColumnParentValues.HeaderText = "Status";
            comboboxColumnParentValues.MaxDropDownItems = 20;
            comboboxColumnParentValues.FlatStyle = FlatStyle.Flat;
            dgvSupplementaryCard.Columns.Insert(4, comboboxColumnParentValues);


        }

        private void txtLedgerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtLedgerCode.Text);
                if (existingAccLedgerAccount != null)
                    txtLedgerName.Text = existingAccLedgerAccount.LedgerName;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLedgerCode2_Leave(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtLedgerCode2.Text);
                if (existingAccLedgerAccount != null)
                    txtLedgerName2.Text = existingAccLedgerAccount.LedgerName;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLedgerName_Leave(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByName(txtLedgerName.Text);
                if (existingAccLedgerAccount != null)
                    txtLedgerCode.Text = existingAccLedgerAccount.LedgerCode;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLedgerName2_Leave(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByName(txtLedgerName2.Text);
                if (existingAccLedgerAccount != null)
                    txtLedgerCode2.Text = existingAccLedgerAccount.LedgerCode;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLedgerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtLedgerName);
                //if (string.IsNullOrEmpty(txtLedgerCode.Text))
                //{
                //    Common.SetFocus(e, txtLedgerName);
                //}
                //else
                //{
                //    Common.SetFocus(e,txtLedgerCode2);
                //}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtLedgerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtLedgerCode2);
                //if (string.IsNullOrEmpty(txtLedgerName.Text))
                //{
                //    Common.SetFocus(e, txtLedgerCode2);
                //}
                //else
                //{
                //    Common.SetFocus(e, txtLedgerCode2);
                //}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtLedgerCode2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtLedgerName2);
                //if (string.IsNullOrEmpty(txtLedgerCode2.Text))
                //{
                //    Common.SetFocus(e, txtLedgerName2);
                //}
                //else
                //{
                //    Common.SetFocus(e, txtCreditLimit);
                //}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtLedgerName2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtCreditLimit);
                //if (string.IsNullOrEmpty(txtLedgerName2.Text))
                //{
                //    Common.SetFocus(e, txtLedgerCode2);
                //}
                //else
                //{
                //    Common.SetFocus(e, txtCreditLimit);
                //}
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
                // if card no exist in Card details
                LoyaltyCardGeneratrionService loyaltyCardGeneratrionService = new LoyaltyCardGeneratrionService();
                LoyaltyCardGenerationDetail loyaltyCardGenerationDetail = new LoyaltyCardGenerationDetail();
                loyaltyCardGenerationDetail = loyaltyCardGeneratrionService.GetIssuedLoyaltyCardGenerationDetailByCardNo(txtCardNo.Text.Trim());
                if (loyaltyCardGenerationDetail == null)
                {
                    Toast.Show("Card no ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    txtCardNo.Focus();
                    return;
                }

                // if card no exist in L customer
                LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();                
                if (loyaltyCustomerService.GetLoyaltyCustomerByCardNo(txtCardNo.Text.Trim()) == null)
                {
                    if (chkAutoClear.Checked)
                    { 
                        Common.ClearForm(this);
                    }
                    txtCardNo.Text = loyaltyCardGenerationDetail.CardNo.Trim();
                }

                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    LoyaltyCustomerService LoyaltyCustomerService = new LoyaltyCustomerService();
                    txtCustomerCode.Text = LoyaltyCustomerService.GetNewCode(this.Name);

                    Common.EnableTextBox(false, txtCustomerCode);
                    cmbTitle.Focus();

                }
                else
                {
                    txtCustomerCode.Focus();
                }

                Common.EnableButton(false, btnNew, btnDelete);
                Common.EnableButton(true, btnSave);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtNic_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    LoyalityCustomerCode = string.Empty;

                    List<LoyaltyCustomer> loyaltyCustomerList = new List<LoyaltyCustomer>();
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

                    loyaltyCustomerList = loyaltyCustomerService.GetLoyalityCustomerlistByNicNo(txtNic.Text.Trim());

                    if (loyaltyCustomerList.Count > 1)
                    {
                        FrmLoyaltyCustomerGrid frmLoyaltyCustomerGrid = new FrmLoyaltyCustomerGrid(loyaltyCustomerList);
                        frmLoyaltyCustomerGrid.ShowDialog();

                        txtCustomerCode.Text = LoyalityCustomerCode;

                        if (!string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
                        {
                            txtCustomerCode_Leave(this, e);
                        }
                        else { txtNic.Focus(); return; }
                    }
                    else
                    {
                        txtReferenceNo.Focus();
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

        private void txtReferenceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbNationality);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void cmbNationality_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbReligion);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtCreditCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    //txtCreditCustomerName.Focus();
                    if (string.IsNullOrEmpty(txtCreditCustomerCode.Text.Trim())) { txtCreditCustomerName.Focus(); }
                    else
                    {
                        Common.SetFocus(e, tabLgsCustomer, tbpCommunication);
                        Common.SetFocus(e, txtAddress1); //txtCreditCustomerName.Focus();
                    }

                    
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtCreditCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabLgsCustomer, tbpCommunication);
                Common.SetFocus(e, txtAddress1);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
                        
        }

        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtAddress2);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtAddress3);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtAddress3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbDistrict);
            }
            catch (Exception ex) 
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void cmbDistrict_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtLandMark);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtLandMark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtEmail);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtTelephone);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtTelephone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtMobile);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtFax);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtFax_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabLgsCustomer, tbpWork);
                Common.SetFocus(e, txtOrganization);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }            
        }

        private void txtOrganization_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtOccupation);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtOccupation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtWorkAddress1);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtWorkAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtWorkAddress3);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtWorkAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtWorkAddress2);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtWorkAddress3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtWorkEmail);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtWorkEmail_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtWorkTelephone);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtWorkTelephone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtWorkMobile);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtWorkMobile_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtWorkFax);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtWorkFax_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabLgsCustomer, tbpCard);
                Common.SetFocus(e, txtNameOnCard);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtNameOnCard_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbCardType);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void cmbCardType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkIssued);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void chkIssued_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkIssued.Checked)
                {
                    dtpIssuedOn.Enabled = true;
                    dtpExpiryDate.Enabled = true;
                    dtpRenewedOn.Enabled = true;
                }
                else
                {
                    dtpIssuedOn.Enabled = false;
                    dtpExpiryDate.Enabled = false;
                    dtpRenewedOn.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        private void chkIssued_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, dtpIssuedOn);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void dtpIssuedOn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, dtpExpiryDate);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void dtpExpiryDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, dtpRenewedOn);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void dtpRenewedOn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabLgsCustomer, tbpSuplimentary);
                Common.SetFocus(e, chkSuplimentary);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void chkSuplimentary_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabLgsCustomer, tbpFamily);
                Common.SetFocus(e, txtSpouseName);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtSpouseName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbCivilStatus);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtFemaleAdults_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtMaleAdults);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void cmbCivilStatus_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtFemaleAdults);    
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtMaleAdults_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtChildrens);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtChildrens_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, dtpSpouseDateOfBirth);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void dtpSpouseDateOfBirth_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, dtpAnniversary);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void dtpAnniversary_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabLgsCustomer, tbpPromotions);
                Common.SetFocus(e, chkSinhala);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
            
        }

        private void chkSinhala_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkThai);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void chkThai_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkWesak);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void chkWesak_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkHaj);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void chkHaj_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkRamazan);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void chkRamazan_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkXmas);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }
        

        private void cmbTvChannel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbNewsPaper);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void chkXmas_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbTvChannel);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void cmbNewsPaper_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbRadioChannel);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void cmbRadioChannel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbMagazine);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void cmbMagazine_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabLgsCustomer, tbpFinancial);
                Common.SetFocus(e, txtLedgerCode);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtCreditLimit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtCreditPeriod);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtCreditPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabLgsCustomer, tbpOther);
                Common.SetFocus(e, cmbDeliverTo);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void cmbDeliverTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtDeliverToAddress);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtDeliverToAddress_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, dtpCustomerSince);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void dtpCustomerSince_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, dtpActivateDate);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void chkSendEmail_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkSendSms);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void chkSendSms_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtRemark);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkCreditAllowed);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtCreditCustomerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCreditCustomerCode.Text.Trim() != string.Empty)
                {
                    CustomerService customerService = new CustomerService();

                    existingCustomer = customerService.GetCustomersByCode(txtCreditCustomerCode.Text.Trim());

                    if (existingCustomer != null)
                    {
                        txtCreditCustomerName.Text = existingCustomer.CustomerName.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCreditCustomerName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCreditCustomerName.Text.Trim() != string.Empty)
                {
                    CustomerService customerService = new CustomerService();

                    existingCustomer = customerService.GetCustomersByName(txtCreditCustomerName.Text.Trim());

                    if (existingCustomer != null)
                    {
                        txtCreditCustomerCode.Text = existingCustomer.CustomerCode.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpDateOfBirth_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                //if (loyaltyCustomerService.GetLoyaltyCustomerByCardNo(txtCardNo.Text.Trim()) != null)
                //{
                //    Common.SetFocus(e, txtReferenceNo);
                //}
                //else
                //{
                //    Common.SetFocus(e, txtNic);
                //}
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (txtNic.Enabled) { txtNic.Focus(); }
                    else { txtReferenceNo.Focus(); }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtAge_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbReligion);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void cmbReligion_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbRace);
               
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void dtpActivateDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkSendEmail);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void dtpDateOfBirth_Validated(object sender, EventArgs e)
        {
            try
            {
                txtAge.Text = CalculateAge(dtpDateOfBirth.Value).ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private int CalculateAge(DateTime Dob)
        {
            int age = DateTime.Today.Year - Dob.Year;
            if (Dob > DateTime.Today.AddYears(-age))
            { 
                age--; 
            }

            return age;
        }
        

        private void txtCardNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled) 
                { 
                    return; 
                }

                if (string.IsNullOrEmpty(txtCardNo.Text.Trim()))
                { return; }

                LoyaltyCardGeneratrionService loyaltyCardGeneratrionService = new LoyaltyCardGeneratrionService();
                LoyaltyCardGenerationDetail loyaltyCardGenerationDetail = new LoyaltyCardGenerationDetail();
                
                //loyaltyCardGenerationDetail = loyaltyCardGeneratrionService.GetIssuedLoyaltyCardGenerationDetailByCardNo(txtCardNo.Text.Trim());
                //if (loyaltyCardGenerationDetail == null)
                //{
                //    Toast.Show("Card no ", Toast.messageType.Information, Toast.messageAction.NotExists);
                //    return;
                //}
                
                LoyaltyCustomerService loyaltyCustomerervice = new LoyaltyCustomerService();
                existingLoyaltyCustomer = loyaltyCustomerervice.GetLoyaltyCustomerByCardNo(txtCardNo.Text.Trim());


                if (existingLoyaltyCustomer != null)
                {
                    CardMasterService cardMasterService = new CardMasterService();

                    txtCardNo.Text = existingLoyaltyCustomer.CardNo.Trim();
                    txtNic.Text = existingLoyaltyCustomer.NicNo.Trim();
                    chkIssued.Checked = existingLoyaltyCustomer.CardIssued;
                    dtpIssuedOn.Value = existingLoyaltyCustomer.IssuedOn;
                    dtpExpiryDate.Value = existingLoyaltyCustomer.ExpiryDate;

                    //txtNic.Enabled = false;
                    txtCardNo.Enabled = false;

                    existingCard = cardMasterService.GetCardMasterById(existingLoyaltyCustomer.CardMasterID);
                    if (existingCard != null)
                    { cmbCardType.Text = existingCard.CardName; }

                    if (!existingLoyaltyCustomer.IsReDimm)
                    {
                        Toast.Show("Please complete the customer details of current Card number to activate redeem facility of this loyalty card.", Toast.messageType.Information, Toast.messageAction.General);
                        Common.EnableButton(true, btnNew);
                        return;
                    }
                    LoadCustomer(existingLoyaltyCustomer);
                    LoadSupplementary(existingLoyaltyCustomer.LoyaltyCustomerID);
                    Common.EnableButton(true, btnDelete, btnSave);
                    Common.EnableButton(false, btnNew);
                    
                    //Common.EnableTextBox(false, txtCustomerCode, txtNic, txtCardNo);
                    Common.EnableTextBox(false, txtCustomerCode, txtCardNo);
                }               
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvSupplementaryCard_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {               
                //if (dgvSupplementaryCard.Columns[e.ColumnIndex].Name == "CardType")
                //{
                //    if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
                //    {
                //        dgvSupplementaryCard.Rows[e.RowIndex].ErrorText =
                //            "Card Type must not be empty";
                //        e.Cancel = true;
                //    }    
                //}

                //if (dgvSupplementaryCard.Columns[e.ColumnIndex].Name == "CardNo")
                //{
                //    if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
                //    {
                //        dgvSupplementaryCard.Rows[e.RowIndex].ErrorText =
                //            "Card No must not be empty";
                //        e.Cancel = true;
                //    }
                //}

                //if (dgvSupplementaryCard.Columns[e.ColumnIndex].Name == "SName")
                //{
                //    if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
                //    {
                //        dgvSupplementaryCard.Rows[e.RowIndex].ErrorText =
                //            "Supplementry name must not be empty";
                //        e.Cancel = true;
                //    }
                //}

                //if (dgvSupplementaryCard.Columns[e.ColumnIndex].Name == "Relationship")
                //{
                //    if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
                //    {
                //        dgvSupplementaryCard.Rows[e.RowIndex].ErrorText =
                //            "Relationship name must not be empty";
                //        e.Cancel = true;
                //    }
                //}

                //if (dgvSupplementaryCard.Columns[e.ColumnIndex].Name == "Status")
                //{
                //    if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
                //    {
                //        dgvSupplementaryCard.Rows[e.RowIndex].ErrorText =
                //            "Status name must not be empty";
                //        e.Cancel = true;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void chkSuplimentary_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dgvSupplementaryCard.Enabled = chkSuplimentary.Checked;                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvSupplementaryCard_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //int x = e.ColumnIndex;
                //int y = dgvSupplementaryCard.Columns.Count-1;
                //if (!e.ColumnIndex.Equals(dgvSupplementaryCard.Columns.Count - 1))
                //{
                //    dgvSupplementaryCard.CurrentCell = dgvSupplementaryCard.Rows[dgvSupplementaryCard.CurrentRow.Index].Cells[e.ColumnIndex +1];
                //}
                //else
                //{
                //    foreach (DataGridViewCell dc in dgvSupplementaryCard.CurrentRow.Cells)
                //    {
                //        string st = dc.ColumnIndex.ToString();                        
                //        //CardType
                //        if (dc == null)
                //        { return; }

                //        if (dc.ColumnIndex == 0)
                //        {
                //            if (String.IsNullOrEmpty(dc.Value.ToString()))
                //                {
                //                    dgvSupplementaryCard.Rows[e.RowIndex].ErrorText =
                //                        "Card Type must not be empty";
                //                    //e.Cancel = true;
                //                }
                //        }
                //        if (dc.ColumnIndex == 1)
                //        {

                //        }
                //        if (dc.ColumnIndex == 2)
                //        {

                //        }
                //        if (dc.ColumnIndex == 3)
                //        {

                //        }
                //        if (dc.ColumnIndex == 4)
                //        {

                //        }
                //        if (dc.ColumnIndex == 5)
                //        {

                //        }
                       
                //    }                    
                //}               
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void dgvSupplementaryCard_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void dgvSupplementaryCard_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.ColumnIndex.Equals(dgvSupplementaryCard.Columns.Count - 1))
            //    {
            //        dgvSupplementaryCard.CurrentCell = dgvSupplementaryCard.Rows[dgvSupplementaryCard.CurrentRow.Index].Cells[0];
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            //}
        }

        private void chkAutoCompleationCustomer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {                
                LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                Common.SetAutoComplete(txtCustomerCode, loyaltyCustomerService.GetAllLoyaltyCustomerCodes(), chkAutoCompleationCustomer.Checked);
                Common.SetAutoComplete(txtCustomerName, loyaltyCustomerService.GetAllLoyaltyCustomerNames(), chkAutoCompleationCustomer.Checked);                                    
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
                LoyaltyCardGeneratrionService loyaltyCardGeneratrionService = new LoyaltyCardGeneratrionService();
                Common.SetAutoComplete(txtCardNo, loyaltyCardGeneratrionService.GetIssuedCardNos(), chkAutoCompleationCardNo.Checked);
              
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

                LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                Common.SetAutoComplete(txtNic, loyaltyCustomerService.GetAllNicNos(), chkAutoCompleationNic.Checked);
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationCreditCustomer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {                
                CustomerService customerService = new CustomerService();
                Common.SetAutoComplete(txtCreditCustomerCode, customerService.GetAllCustomerCodes(), chkAutoCompleationCustomer.Checked);
                Common.SetAutoComplete(txtCreditCustomerName, customerService.GetAllCustomerNames(), chkAutoCompleationCustomer.Checked);                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationLedger_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                Common.SetAutoComplete(txtLedgerCode, accLedgerAccountService.GetLedgerCodes(), chkAutoCompleationLedger.Checked);
                Common.SetAutoComplete(txtLedgerName, accLedgerAccountService.GetLedgerNames(), chkAutoCompleationLedger.Checked);                                                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationLedger2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
               
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                Common.SetAutoComplete(txtLedgerCode2, accLedgerAccountService.GetLedgerCodes(), chkAutoCompleationLedger2.Checked);
                Common.SetAutoComplete(txtLedgerName2, accLedgerAccountService.GetLedgerNames(), chkAutoCompleationLedger2.Checked);
              
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtNic_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled) { return; }
                if (!string.IsNullOrEmpty(txtNic.Text.Trim()))
                {
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                    if (loyaltyCustomerService.GetLoyaltyCustomerByNicNo(txtNic.Text.Trim()) == null)
                    {
                        return;
                    }

                    existingLoyaltyCustomer = loyaltyCustomerService.GetLoyaltyCustomerByNicNo(txtNic.Text.Trim());

                    if (existingLoyaltyCustomer != null)
                    {
                        txtNic.Text = existingLoyaltyCustomer.NicNo.Trim();
                        txtCardNo.Text = existingLoyaltyCustomer.CardNo.Trim();
                        chkIssued.Checked = existingLoyaltyCustomer.CardIssued;
                        dtpIssuedOn.Value = existingLoyaltyCustomer.IssuedOn;
                        dtpExpiryDate.Value = existingLoyaltyCustomer.ExpiryDate;

                        //txtNic.Enabled = false;
                        //txtCardNo.Enabled = false;

                        if (!existingLoyaltyCustomer.IsReDimm)
                        {
                            Toast.Show("Please complete the customer details of current NIC number to activate redeem facility of this loyalty card.", Toast.messageType.Information, Toast.messageAction.General);
                        }
                        LoadCustomer(existingLoyaltyCustomer);
                        LoadSupplementary(existingLoyaltyCustomer.LoyaltyCustomerID);
                        Common.EnableButton(true, btnDelete, btnSave);
                        Common.EnableButton(false, btnNew);
                        
                        //Common.EnableTextBox(false, txtCustomerCode, txtNic);
                        Common.EnableTextBox(false, txtCustomerCode);
                    }
                    else
                    {
                        Toast.Show("NIC No ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateEmailAddress(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtWorkEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateEmailAddress(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtWorkTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtWorkMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtWorkFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpRenewedOn_Leave(object sender, EventArgs e)
        {
            try
            {
                dtpExpiryDate.Value = dtpRenewedOn.Value.AddYears(3);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpDateOfBirth_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                SendKeys.Send("{Right}");
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtWorkEmail_Leave(object sender, EventArgs e)
        {
            try
            {
                txtWorkEmail.Text = txtWorkEmail.Text.ToLower();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            try
            {
                txtEmail.Text = txtEmail.Text.ToLower();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            if (pbLgsCustomer.Image != null)
            {
                FrmDocumentViewer frmPictureView = new FrmDocumentViewer(pbLgsCustomer.Image);
                frmPictureView.ShowDialog();
            }
            else
            {
                Toast.Show("No image to display.", Toast.messageType.Information, Toast.messageAction.General);
            }
        }

        private void btnStatement_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
                {
                    Toast.Show("Customer Code", Toast.messageType.Information, Toast.messageAction.Invalid);
                    return;
                }
                else
                {
                    ViewCustomersStatement(txtCustomerCode.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void ViewCustomersStatement(string customerCode)  
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

            CrmRptCustomerStatement crmRptCustomerStatement = new CrmRptCustomerStatement();

            crmRptCustomerStatement.SetDataSource(loyaltyCustomerService.GetCustomerStatement(txtCustomerCode.Text.Trim()));

            crmRptCustomerStatement.SummaryInfo.ReportTitle = "Customer Statement";
            crmRptCustomerStatement.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptCustomerStatement.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            
            crmRptCustomerStatement.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptCustomerStatement.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptCustomerStatement.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptCustomerStatement.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptCustomerStatement;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void lblRace_Click(object sender, EventArgs e)
        {

        }

        private void cmbReligion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbRace_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtCreditCustomerCode);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkLost_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkLost.Checked)
                {
                    chkActive.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked)
                {
                    chkLost.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

      
    }
}
