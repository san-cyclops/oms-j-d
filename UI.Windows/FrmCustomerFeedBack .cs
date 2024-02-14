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
using Domain;
using Service;
using Utility;

namespace UI.Windows
{
    public partial class FrmCustomerFeedBack  : FrmBaseTransactionForm
    {
        int documentID;

        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        public FrmCustomerFeedBack()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return ; }
                
                LoadCmbCustomer();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        private void tpGeneral_Click(object sender, EventArgs e)
        {

        }

        private bool ValidateControls()
        {
            return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtDocNo,cmbCustomerType,cmbCustomerName);
        }

        public override void Pause()
        {

            if ((Toast.Show("Customer Feed Back DocNo - " + txtDocNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                if (SaveDocument(0, txtDocNo.Text.Trim()).Equals(true))
                {
                    Toast.Show("Customer Feed Back DocNo - " + txtDocNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    ClearForm();
                }
                else
                {
                    Toast.Show("Customer Feed Back DocNo - " + txtDocNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }

        }

        public override void Save()
        {

            if ((Toast.Show("Customer Feed Back DocNo - " + txtDocNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                bool saveDocument = SaveDocument(1, txtDocNo.Text.Trim());
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Customer Feed Back DocNo - " + txtDocNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    ClearForm();
                }
                else
                {
                    Toast.Show("Customer Feed Back DocNo - " + txtDocNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }

        }

        private bool SaveDocument(int documentStatus, string documentNo)
        {
            try
            {

                CustomerFeedBackService customerFeedBackService = new CustomerFeedBackService();
                CustomerFeedBack customerFeed = new CustomerFeedBack();
                LocationService locationService = new LocationService();
                Location Location = new Location();
                LoyaltyCustomerService LoyaltyCustomerService = new LoyaltyCustomerService();

                LoyaltyCustomer loyaltyCustomer = new LoyaltyCustomer();

                loyaltyCustomer = LoyaltyCustomerService.GetLoyaltyCustomerByCode(cmbCustomerName.Text.Trim());

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                
                customerFeed = customerFeedBackService.getCustomerFeedBackByDocumentNo(documentID, txtDocNo.Text.Trim(), Location.LocationID);
                if (customerFeed == null)
                    customerFeed = new CustomerFeedBack();
                if (documentStatus.Equals(1)) // update paused document
                {
                    documentNo = GetDocumentNo(false);
                    txtDocNo.Text = documentNo;
                }

                customerFeed.DocumentDate = dtpDate.Value;
                customerFeed.CustomerName = cmbCustomerName.Text;
                customerFeed.Nic = txtNic.Text.Trim();
                customerFeed.Age = cmbAge.Text.Trim();
                customerFeed.Gender = cmbGender.Text.Trim();
                customerFeed.Occupation = txtOccupation.Text.Trim();
                customerFeed.Ethinicity = cmbEthnicity.Text.Trim();
                customerFeed.CardType = cmbCardType.Text.Trim();
                customerFeed.Address1 = txtAddress1.Text.Trim();
                customerFeed.Address2 = txtAddress2.Text.Trim();
                customerFeed.Address3 = txtAddress3.Text.Trim();
                customerFeed.ContactPerson = txtContactPerson.Text.Trim();
                customerFeed.Province = cmbProvince.Text.Trim();
                customerFeed.District = cmbDistrict.Text.Trim();
                customerFeed.Email = txtEmail.Text.Trim();
                customerFeed.Telephone = txtTelephone.Text.Trim();
                customerFeed.Mobile = txtMobile.Text.Trim();
                customerFeed.Fax = txtFax.Text.Trim();
                customerFeed.RefPerson = txtRefPerson.Text.Trim();
                customerFeed.RefAddress1 = txtRefAddress1.Text.Trim();
                customerFeed.RefAddress2 = txtRefAddress2.Text.Trim();
                customerFeed.RefAddress3 = txtRefAddress3.Text.Trim();
                customerFeed.HabitualShopping = cmbHabitualShopping.Text.Trim();
                customerFeed.ItemPrice = cmbItemPrice.Text.Trim();
                customerFeed.Quality = cmbQuality.Text.Trim();
                customerFeed.Service = cmbService.Text.Trim();
                customerFeed.SatisfiedWithAvailability = cmbSatisfiedWithAvailability.Text.Trim();
                customerFeed.ShoppingAt = cmbShopingAtOther.Text.Trim();
                customerFeed.SatisfiedWithVariety = cmbSatisfiedWiththeVariety.Text.Trim();
                customerFeed.StaffsQuality = cmbStaffsQuality.Text.Trim();
                customerFeed.StaffsKnowledge = cmbStaffsKnowledge.Text.Trim();
                customerFeed.InfluencedBy = cmbInfluencedBy.Text.Trim();
                customerFeed.Comfortable = cmbComfortable.Text.Trim();
                customerFeed.FactQuality = chkFactorQuality.Checked;
                customerFeed.FactService = chkFactorService.Checked;
                customerFeed.FactPrice = chkFactorPrice.Checked;
                customerFeed.FactCollections = chkFactorCollection.Checked;
                customerFeed.FactMerchandising = chkFactorMerchandising.Checked;
                customerFeed.FactAppearance = chkFactorAppearance.Checked;
                customerFeed.KnowAbtPromotion = cmbKnowAbountPromo.Text.Trim();
                customerFeed.Regularity = cmbRegularity.Text.Trim();
                customerFeed.ShoppingFor = cmbShoppingFor.Text.Trim();
                customerFeed.Motivates = cmbMotivates.Text.Trim();
                customerFeed.Recommend4Others = cmbRecommendForOthers.Text.Trim();
                customerFeed.YourChanges = txtChange.Text.Trim();
                customerFeed.Recommendations = txtRecommendations.Text.Trim();
                customerFeed.RateService = cmbRateService.Text.Trim();

                return customerFeedBackService.Save(customerFeed);
                //   }

                //using (transactionScope)
                //{

                //}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return false;

            }


        }

        private void LoadCmbCustomer()
        {
            try
            {
                LoyaltyCustomerService LoyaltyCustomerService = new LoyaltyCustomerService();
                cmbCustomerName.DataSource = LoyaltyCustomerService.GetAllLoyaltyCustomerNames();
                cmbCustomerName.DisplayMember = "CustomerName";
                cmbCustomerName.ValueMember = "CustomerId";
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void FrmCustomerFeedBack_Load(object sender, EventArgs e)
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                // Load Locations
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
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
                LoyaltyCustomerService LoyaltyCustomerService = new LoyaltyCustomerService();

                CustomerService customerService = new CustomerService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                CardMasterService cardMasterService = new CardMasterService();


                Common.EnableTextBox(true);
                Common.EnableComboBox(true, cmbLocation);
                if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

                SetAutoBindRecords(cmbGender, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.GenderType).ToString()));

                SetAutoBindRecords(cmbDistrict, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.District).ToString()));

                SetAutoBindRecords(cmbProvince, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.District).ToString()));

                SetAutoBindRecords(cmbCardType, cardMasterService.GetAllCardNames());

                //SetAutoBindRecords(cmbCustomerName, LoyaltyCustomerService.GetAllLoyaltyCustomerNames());

                documentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).DocumentID;

                // Load Locations            
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());

                cmbLocation.SelectedValue = Common.LoggedLocationID;

                txtDocNo.Text = GetDocumentNo(true);

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

        private string GetDocumentNo(bool isTemporytNo)
        {

            try
            {
                CustomerFeedBackService customerFeedBackService = new CustomerFeedBackService();
                LocationService locationService = new LocationService();
                return customerFeedBackService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }

        }

        private void cmbLocation_Validated(object sender, EventArgs e)
        {
            try
            {
                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbLocation.Text.Trim()).LocationID, documentID);
                if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
