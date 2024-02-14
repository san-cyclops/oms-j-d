using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Domain;
using Service;
using Utility;
using System.IO;
using System.Reflection;

namespace UI.Windows
{
    public partial class FrmCustomer : UI.Windows.FrmBaseMasterForm
    {
        /// <summary>
        /// Sanjeewa
        /// </summary>

        Customer existingCustomer;
        string imagename;
        Area existingArea;
        Territory existingTerritory;
        CustomerGroup existingCustomerGroup;
        Broker existingBroker;
        AccLedgerAccount existingAccLedgerAccount;
        AccLedgerAccount existingOtherLedgerAccount;
        PaymentMethod existingPaymentMethod;
        private Tax existingTax;
        private int taxCount;
        private ReferenceType existingReferenceType;
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;


        public FrmCustomer()
        {
            InitializeComponent();
        }

        public override void FormLoad()
        {
            try
            {
                if (Common.EntryLevel.Equals(1))
                {
                    lblLedger.Text = lblLedger.Text + "*";
                    lblOtherLedger.Text = lblOtherLedger.Text + "*";
                }

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

        public override void InitializeForm()
        {
            try
            {
                Customer customer = new Customer();
                CustomerService customerService = new CustomerService();

                PaymentMethodService paymentMethodService = new PaymentMethodService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                CustomerGroupService customerGroupService = new CustomerGroupService();


                AreaService areaService = new AreaService();
                TerritoryService territoryService = new TerritoryService();
                BrokerService brokerService = new BrokerService();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();

                Common.SetAutoComplete(txtCustomerCode, customerService.GetAllCustomerCodes(), chkAutoCompleationCustomer.Checked);
                Common.SetAutoComplete(txtCustomerName, customerService.GetAllCustomerNames(), chkAutoCompleationCustomer.Checked);

                Common.SetAutoComplete(txtAreaCode, areaService.GetAllAreaCodes(), chkAutoCompleationArea.Checked);
                Common.SetAutoComplete(txtTerritoryCode, territoryService.GetAllTerritoryCodes(), chkAutoCompleationTerritory.Checked);
                Common.SetAutoComplete(txtBrokerCode, brokerService.GetAllBrokerCodes(), chkAutoCompleationBroker.Checked);
                Common.SetAutoComplete(txtLedgerCode, accLedgerAccountService.GetLedgerCodes(), chkAutoCompleationLedger.Checked);
                Common.SetAutoComplete(txtOtherLedgerCode, accLedgerAccountService.GetLedgerCodes(), chkAutoCompleationOtherLedger.Checked);

                Common.SetAutoComplete(txtAreaName, areaService.GetAllAreaNames(), chkAutoCompleationArea.Checked);
                Common.SetAutoComplete(txtTerritoryName, territoryService.GetAllTerritoryNames(), chkAutoCompleationTerritory.Checked);
                Common.SetAutoComplete(txtBrokerName, brokerService.GetAllBrokerNames(), chkAutoCompleationBroker.Checked);
                Common.SetAutoComplete(txtLedgerDescription, accLedgerAccountService.GetLedgerNames(), chkAutoCompleationLedger.Checked);
                Common.SetAutoComplete(txtOtherLedgerDescription, accLedgerAccountService.GetLedgerNames(), chkAutoCompleationOtherLedger.Checked);


                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete, btnView);
                Common.EnableTextBox(true, txtCustomerCode);

                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                LoadTaxes();

                SetAutoBindRecords(cmbTitle, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.TitleType).ToString()));
                SetAutoBindRecords(cmbGender, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.GenderType).ToString()));
                SetAutoBindRecords(cmbCustomerType, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.CustomerType).ToString()));

                SetAutoBindRecords(cmbPaymentMethod, paymentMethodService.GetAllPaymentMethodNames());
                SetAutoBindRecords(cmbCustomerGroup, customerGroupService.GetAllCustomerGroupNames());

                txtTax1No.Enabled = false;
                txtTax2No.Enabled = false;
                txtTax3No.Enabled = false;
                txtTax4No.Enabled = false;
                txtTax5No.Enabled = false;

                Common.ClearForm(this);

                pbCustomer.Image = UI.Windows.Properties.Resources.Default_Customer;
                tabCustomer.SelectedTab = tpGeneral;

                //tabCustomer.TabPages.Remove(tpDelivery);
                //tabCustomer.TabPages.Remove(tpFinancial);
                //tabCustomer.TabPages.Remove(tpTaxDetails);
                //tabCustomer.TabPages.Remove(tpOther);
                


       
 

                ActiveControl = txtCustomerCode;
                txtCustomerCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public static void SetAutoBindRecords(ComboBox comboBox, string[] stringCollection)
        {
            try
            {
                AutoCompleteStringCollection autoCompleteCode = new AutoCompleteStringCollection();
                autoCompleteCode.AddRange(stringCollection);
                comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox.AutoCompleteCustomSource = autoCompleteCode;
                comboBox.DataSource = autoCompleteCode;
                comboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), "FrmCustomer", Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
 
        private bool ValidateControls()
        {
             return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtCustomerCode, txtCustomerName,txtAreaCode,txtTerritoryCode,cmbCustomerType,cmbCustomerGroup,cmbPaymentMethod,cmbTitle,cmbGender);
        }

        private bool ValidateLedger()
        {
            return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtLedgerCode, txtLedgerDescription, txtOtherLedgerCode, txtOtherLedgerDescription);
        }

        private Customer FillCustomer()
        {
            CustomerGroupService customerGroupService = new CustomerGroupService();
            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
            LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
            PaymentMethodService paymentMethodService = new PaymentMethodService();
            AreaService areaService = new AreaService();
            BrokerService brokerService = new BrokerService();
            TerritoryService territoryService=new TerritoryService();
            TaxService taxService = new TaxService();
        
            //existingCustomer = new Customer();
            existingCustomer.CustomerCode = txtCustomerCode.Text.Trim();
            existingCustomer.CustomerName = txtCustomerName.Text.Trim();

            existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.TitleType).ToString(), cmbTitle.Text.Trim());
            if (existingReferenceType != null)
            {
                existingCustomer.CustomerTitle = existingReferenceType.LookupKey;
            }
            else
            {
                existingCustomer.CustomerTitle = 0;
            }

            //Read Gender Type
            existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.GenderType).ToString(), cmbGender.Text.Trim());
            if (existingReferenceType != null)
            {
                existingCustomer.Gender = existingReferenceType.LookupKey;
            }
            else
            {
                existingCustomer.Gender = 0;
            }

            existingCustomerGroup = customerGroupService.GetCustomerGroupByName(cmbCustomerGroup.Text.Trim());
            if (existingCustomerGroup != null)
                existingCustomer.CustomerGroupID = existingCustomerGroup.CustomerGroupID;

            existingArea = areaService.GetAreasByCode(txtAreaCode.Text.Trim());
            if (existingArea != null)
                existingCustomer.AreaID = existingArea.AreaID;

            existingBroker = brokerService.GetBrokersByCode(txtBrokerCode.Text.Trim());
            if (existingBroker != null)
            { existingCustomer.BrokerID = existingBroker.BrokerID; }
            else
            { 
                existingCustomer.BrokerID = 0;
            }


            existingTerritory = territoryService.GetTerritoryByCode(txtTerritoryCode.Text.Trim());
            if (existingTerritory != null)
                existingCustomer.TerritoryID = existingTerritory.TerritoryID;


            existingCustomer.ReferenceNo = txtReferenceNo.Text.Trim();
            existingCustomer.RepresentativeName = txtRepresentativeName.Text.Trim();
            existingCustomer.RepresentativeMobileNo = txtRepresentativeMoblie.Text.Trim();
            existingCustomer.RepresentativeNICNo = txtRNic.Text.Trim();
            existingCustomer.Remark = txtRemark.Text.Trim();
            existingCustomer.BillingAddress1 = txtBillingAddress1.Text.Trim();
            existingCustomer.BillingAddress2 = txtBillingAddress2.Text.Trim();
            existingCustomer.BillingAddress3 = txtBillingAddress3.Text.Trim();
            existingCustomer.BillingFax = txtBillingFax.Text.Trim();
            existingCustomer.BillingMobile = txtBillingMobile.Text.Trim();
            existingCustomer.BillingTelephone = txtBillingTelephone.Text.Trim();
            existingCustomer.Email = txtEmail.Text.Trim();
            existingCustomer.ContactPersonName = txtContactPerson.Text.Trim();
            existingCustomer.DeliveryAddress1 = txtDeliveryAddress1.Text.Trim();
            existingCustomer.DeliveryAddress2 = txtDeliveryAddress2.Text.Trim();
            existingCustomer.DeliveryAddress3 = txtDeliveryAddress3.Text.Trim();
            existingCustomer.DeliveryFax = txtDeliveryFax.Text.Trim();
            existingCustomer.DeliveryMobile = txtDeliveryMobile.Text.Trim();
            existingCustomer.DeliveryTelephone = txtDeliveryTelephone.Text.Trim();
            existingCustomer.NICNo = txtNic.Text.Trim();

            

            //Read Ledger ID
            existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtLedgerCode.Text.Trim());
            if (existingAccLedgerAccount != null)
            {
                existingCustomer.LedgerID = existingAccLedgerAccount.AccLedgerAccountID;
            }
            else
            {
                existingCustomer.LedgerID = 0;
            }

            //Read Other Ledegr ID
            existingOtherLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtOtherLedgerCode.Text.Trim());
            if (existingOtherLedgerAccount != null)
            {
                existingCustomer.OtherLedgerID = existingOtherLedgerAccount.AccLedgerAccountID;
            }
            else
            {
                existingCustomer.OtherLedgerID = 0;
            }


            existingCustomer.CustomerType = cmbCustomerType.Text.Trim();
            existingCustomer.CreditLimit = Common.ConvertStringToDecimal(txtCreditLimit.Text.Trim());
            existingCustomer.CreditPeriod = Common.ConvertStringToInt(txtCreditPeriod.Text.Trim());
            existingCustomer.ChequeLimit = Common.ConvertStringToDecimal(txtChequeLimit.Text.Trim());
            existingCustomer.ChequePeriod = Common.ConvertStringToInt(txtChequePeriod.Text.Trim());
            existingCustomer.MaximumCashDiscount = Common.ConvertStringToDecimal(txtMaxCashDiscount.Text.Trim());
            existingCustomer.MaximumCreditDiscount = Common.ConvertStringToDecimal(txtMaxCreditDiscount.Text.Trim());
            
            existingPaymentMethod = paymentMethodService.GetPaymentMethodsByName(cmbPaymentMethod.Text);
            if (existingPaymentMethod != null)
                existingCustomer.PaymentMethodID = existingPaymentMethod.PaymentMethodID;
            
            
            
            existingCustomer.BankDraft = Common.ConvertStringToDecimal(txtBankDraft.Text.Trim());
            existingCustomer.TemporaryLimit = Common.ConvertStringToDecimal(txtTemporallyLimit.Text.Trim());

            existingTax = taxService.GetTaxByName(lblTax1.Text.Trim());
            if (chkTax1.Checked) { existingCustomer.TaxID1 = existingTax.TaxID; }
            else { existingCustomer.TaxID1 = 0; }

            existingTax = taxService.GetTaxByName(lblTax2.Text.Trim());
            if (chkTax2.Checked) { existingCustomer.TaxID2 = existingTax.TaxID; }
            else { existingCustomer.TaxID2 = 0; }

            existingTax = taxService.GetTaxByName(lblTax3.Text.Trim());
            if (chkTax3.Checked) { existingCustomer.TaxID3 = existingTax.TaxID; }
            else { existingCustomer.TaxID3 = 0; }

            existingTax = taxService.GetTaxByName(lblTax4.Text.Trim());
            if (chkTax4.Checked) { existingCustomer.TaxID4 = existingTax.TaxID; }
            else { existingCustomer.TaxID4 = 0; }

            existingTax = taxService.GetTaxByName(lblTax5.Text.Trim());
            if (chkTax5.Checked) { existingCustomer.TaxID5 = existingTax.TaxID; }
            else { existingCustomer.TaxID5 = 0; }

            existingCustomer.TaxNo1 = txtTax1No.Text.Trim();
            existingCustomer.TaxNo2 = txtTax2No.Text.Trim();
            existingCustomer.TaxNo3 = txtTax3No.Text.Trim();
            existingCustomer.TaxNo4 = txtTax4No.Text.Trim();
            existingCustomer.TaxNo5 = txtTax5No.Text.Trim();
            existingCustomer.IsLoyalty = chkLoyalty.Checked;
            existingCustomer.IsSuspended = chkSuspended.Checked;
            existingCustomer.IsBlackListed = chkBlackListed.Checked;
            existingCustomer.IsCreditAllowed = chkCreditAllowed.Checked;

            MemoryStream stream = new MemoryStream();
            pbCustomer.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] pic = stream.ToArray();
            existingCustomer.CustomerImage = pic;
            return existingCustomer;   
        }

        public override void Save()
        {
            try
            {
                if (ValidateControls() == false) return;

                if (Common.EntryLevel.Equals(1))
                {
                    if (!ValidateLedger()) { return; }
                }

                CustomerService customerService = new CustomerService();
                bool isNew = false;
                existingCustomer = customerService.GetCustomersByCode(txtCustomerCode.Text.Trim());
                if (existingCustomer == null || existingCustomer.CustomerID == 0)
                {
                    existingCustomer = new Customer();
                    isNew = true;
                }
                
                FillCustomer();

                if (existingCustomer.CustomerID == 0)
                {
                    if ((Toast.Show("Customer  - " + existingCustomer.CustomerCode + " - " + existingCustomer.CustomerName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    customerService.AddCustomer(existingCustomer);

                    if ((Toast.Show("Customer  - " + existingCustomer.CustomerCode + " - " + existingCustomer.CustomerName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        if ((Toast.Show("Customer  - " + existingCustomer.CustomerCode + " - " + existingCustomer.CustomerName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingCustomer.CustomerCode + " - " + existingCustomer.CustomerName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("Customer  - " + existingCustomer.CustomerCode + " - " + existingCustomer.CustomerName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    customerService.UpdateCustomer(existingCustomer);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    Toast.Show("Customer  - " + existingCustomer.CustomerCode + " - " + existingCustomer.CustomerName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }
                txtCustomerCode.Focus();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadTaxes()
        {
            try
            {
                Tax tax = new Tax();
                TaxService taxService = new TaxService();

                List<Tax> taxes = new List<Tax>();
                taxes = taxService.GetAllTaxes();

                taxCount = taxes.Count;

                for (int i = 0; i < taxCount; i++)
                {

                    switch (i)
                    {
                        case 0:
                            lblTax1.Text = taxes[i].TaxName.Trim();
                            break;
                        case 1:
                            lblTax2.Text = taxes[i].TaxName.Trim();
                            break;
                        case 2:
                            lblTax3.Text = taxes[i].TaxName.Trim();
                            break;
                        case 3:
                            lblTax4.Text = taxes[i].TaxName.Trim();
                            break;
                        case 4:
                            lblTax5.Text = taxes[i].TaxName.Trim();
                            break;
                    }
                }

                existingTax = taxService.GetTaxByName(lblTax1.Text.Trim());
                if (existingTax == null)
                {
                    lblTax1.Visible = false;
                    chkTax1.Visible = false;
                    txtTax1No.Visible = false;
                }

                existingTax = taxService.GetTaxByName(lblTax2.Text.Trim());
                if (existingTax == null)
                {
                    lblTax2.Visible = false;
                    chkTax2.Visible = false;
                    txtTax2No.Visible = false;
                }

                existingTax = taxService.GetTaxByName(lblTax3.Text.Trim());
                if (existingTax == null)
                {
                    lblTax3.Visible = false;
                    chkTax3.Visible = false;
                    txtTax3No.Visible = false;
                }

                existingTax = taxService.GetTaxByName(lblTax4.Text.Trim());
                if (existingTax == null)
                {
                    lblTax4.Visible = false;
                    chkTax4.Visible = false;
                    txtTax4No.Visible = false;
                }

                existingTax = taxService.GetTaxByName(lblTax5.Text.Trim());
                if (existingTax == null)
                {
                    lblTax5.Visible = false;
                    chkTax5.Visible = false;
                    txtTax5No.Visible = false;
                }
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
                if (Toast.Show("Customer  - " + existingCustomer.CustomerCode + " - " + existingCustomer.CustomerName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                    return;

                CustomerService customerService = new CustomerService();
                existingCustomer= customerService.GetCustomersByCode(txtCustomerCode.Text.Trim());

                if (existingCustomer != null && existingCustomer.CustomerID != 0)
                {
                    existingCustomer.IsDelete = true;
                    customerService.UpdateCustomer(existingCustomer);
                    ClearForm();
                    Toast.Show("Customer  - " + existingCustomer.CustomerCode + " - " + existingCustomer.CustomerName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    txtCustomerCode.Focus();
                }
                else
                    Toast.Show("Customer  - " + existingCustomer.CustomerCode + " - " + existingCustomer.CustomerName + "", Toast.messageType.Warning, Toast.messageAction.NotExists);


            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
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
                    pbCustomer.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbCustomer.Image = (Image)newimg;
                }
                fldlg = null;
            }

            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                MessageBox.Show(ae.Message.ToString());
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
                    CustomerService customerService = new CustomerService();
                    DataView dvAllReferenceData = new DataView(customerService.GetAllActiveCustomersDataTable());
                    if (dvAllReferenceData.Count != 0)
                    { 
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtCustomerCode);
                        txtCustomerCode_Leave(this, e);
                    }
                }
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                Common.SetFocus(e, cmbTitle);
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

        private void LoadCustomer(Customer existingCustomer)
        {
            AccLedgerAccount ledgerAccount = new AccLedgerAccount();
            AccLedgerAccount otherLedgerAccount = new AccLedgerAccount();
            Broker broker = new Broker();
            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
            LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();           
            BrokerService brokerService = new BrokerService();

            txtCustomerCode.Text = existingCustomer.CustomerCode;
            txtCustomerName.Text = existingCustomer.CustomerName;

            existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.TitleType).ToString(), existingCustomer.CustomerTitle);
            if (existingReferenceType != null)
            {
                cmbTitle.Text = existingReferenceType.LookupValue;
            }
            else
            {
                cmbTitle.SelectedIndex = -1;
            }

            //Read Gender Type
            existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.GenderType).ToString(), existingCustomer.Gender);
            if (existingReferenceType != null)
            {
                cmbGender.Text = existingReferenceType.LookupValue;
            }
            else
            {
                cmbGender.SelectedIndex = -1;
            }

            CustomerGroupService customerGroupService = new CustomerGroupService();

            CustomerGroup customerGroup = new CustomerGroup();
            customerGroup = customerGroupService.GetCustomerGroupByID(existingCustomer.CustomerGroupID);

            cmbCustomerGroup.Text = customerGroup.CustomerGroupName;
            txtReferenceNo.Text = existingCustomer.ReferenceNo;
            txtRepresentativeName.Text = existingCustomer.RepresentativeName;
            txtRepresentativeMoblie.Text = existingCustomer.RepresentativeMobileNo;
            txtRNic.Text = existingCustomer.RepresentativeNICNo;
            txtRemark.Text = existingCustomer.Remark;
            txtBillingAddress1.Text = existingCustomer.BillingAddress1;
            txtBillingAddress2.Text = existingCustomer.BillingAddress2;
            txtBillingAddress3.Text = existingCustomer.BillingAddress3;
            txtBillingFax.Text = existingCustomer.BillingFax;
            txtBillingMobile.Text = existingCustomer.BillingMobile;
            txtBillingTelephone.Text = existingCustomer.BillingTelephone;
            txtEmail.Text = existingCustomer.Email;
            txtDeliveryAddress1.Text = existingCustomer.DeliveryAddress1;
            txtDeliveryAddress2.Text = existingCustomer.DeliveryAddress2.Trim();
            txtDeliveryAddress3.Text = existingCustomer.DeliveryAddress3.Trim();
            txtDeliveryFax.Text = existingCustomer.DeliveryFax.Trim();
            txtDeliveryMobile.Text = existingCustomer.DeliveryMobile.Trim();
            txtDeliveryTelephone.Text = existingCustomer.DeliveryTelephone.Trim();
            txtContactPerson.Text = existingCustomer.ContactPersonName.Trim();
            txtNic.Text = existingCustomer.NICNo.Trim();

            ledgerAccount = accLedgerAccountService.GetAccLedgerAccountByID(existingCustomer.LedgerID);
            if (ledgerAccount != null)
            {
                txtLedgerCode.Text = ledgerAccount.LedgerCode.Trim();
                txtLedgerDescription.Text = ledgerAccount.LedgerName.Trim();
            }

            otherLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByID(existingCustomer.OtherLedgerID);
            if (ledgerAccount != null)
            {
                txtOtherLedgerCode.Text = otherLedgerAccount.LedgerCode.Trim();
                txtOtherLedgerDescription.Text = otherLedgerAccount.LedgerName.Trim();
            }

            //AreaService areaService = new AreaService();
           // Area area = new Area();
            //area = areaService.GetAreasByID(existingCustomer.AreaID);


            txtAreaCode.Text = "01"; //area.AreaCode;
            txtAreaName.Text = ""; //area.AreaName;

            txtTerritoryCode.Text = "01"; // existingCustomer.Territory.TerritoryCode;
            txtTerritoryName.Text = ""; // existingCustomer.Territory.TerritoryName;
           
           // broker = brokerService.GetBrokersByID(existingCustomer.BrokerID);
           // if (broker != null)
           // {
            txtBrokerCode.Text = "0"; //broker.BrokerCode.Trim();
            txtBrokerName.Text = ""; // broker.BrokerName.Trim();
            //}
           
            cmbCustomerType.Text = existingCustomer.CustomerType.Trim();
            txtCreditLimit.Text = existingCustomer.CreditLimit.ToString();
            cmbPaymentMethod.Text = existingCustomer.PaymentMethod.PaymentMethodName;

            txtCreditPeriod.Text = existingCustomer.CreditPeriod.ToString();
            txtChequePeriod.Text = existingCustomer.ChequePeriod.ToString();
            txtChequeLimit.Text = existingCustomer.ChequeLimit.ToString();
            txtMaxCreditDiscount.Text = existingCustomer.MaximumCreditDiscount.ToString();
            txtBankDraft.Text = existingCustomer.BankDraft.ToString();
            txtMaxCashDiscount.Text = existingCustomer.MaximumCashDiscount.ToString();

            txtTemporallyLimit.Text = existingCustomer.TemporaryLimit.ToString();

            txtTax1No.Text = existingCustomer.TaxNo1.Trim();
            if (string.IsNullOrEmpty(txtTax1No.Text.Trim())) { chkTax1.Checked = false; } else { chkTax1.Checked = true; }

            txtTax2No.Text = existingCustomer.TaxNo2.Trim();
            if (string.IsNullOrEmpty(txtTax2No.Text.Trim())) { chkTax2.Checked = false; } else { chkTax2.Checked = true; }

            txtTax3No.Text = existingCustomer.TaxNo3.Trim();
            if (string.IsNullOrEmpty(txtTax3No.Text.Trim())) { chkTax3.Checked = false; } else { chkTax3.Checked = true; }

            txtTax4No.Text = existingCustomer.TaxNo4.Trim();
            if (string.IsNullOrEmpty(txtTax4No.Text.Trim())) { chkTax4.Checked = false; } else { chkTax4.Checked = true; }

            txtTax5No.Text = existingCustomer.TaxNo5.Trim();
            if (string.IsNullOrEmpty(txtTax5No.Text.Trim())) { chkTax5.Checked = false; } else { chkTax5.Checked = true; }

            chkLoyalty.Checked = existingCustomer.IsLoyalty;
            chkSuspended.Checked = existingCustomer.IsSuspended;
            chkBlackListed.Checked = existingCustomer.IsBlackListed;
            chkCreditAllowed.Checked = existingCustomer.IsCreditAllowed;
            if (existingCustomer.CustomerImage != null)
            {
                MemoryStream ms = new MemoryStream((byte[])existingCustomer.CustomerImage);
                pbCustomer.Image = Image.FromStream(ms);
                pbCustomer.SizeMode = PictureBoxSizeMode.StretchImage;
                pbCustomer.Refresh();
            }
            else
            {
                pbCustomer.Image = UI.Windows.Properties.Resources.Default_Customer;
                pbCustomer.Refresh();
            }
            if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
            if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
            Common.EnableTextBox(false, txtCustomerCode);
        }

        private void txtCustomerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerCode.Text.Trim() != string.Empty)
                {
                    CustomerService customerService = new CustomerService();

                    existingCustomer = customerService.GetCustomersByCode(txtCustomerCode.Text.Trim());

                    if (existingCustomer != null)
                    {
                        LoadCustomer(existingCustomer);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                    }
                    else
                    {
                        if (btnNew.Enabled)
                        {
                            if (Toast.Show("Customer  - " + txtCustomerCode.Text.Trim() + " ", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                            {
                                btnNew.PerformClick();
                                cmbTitle.Focus();
                            }
                        }
                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtCustomerCode);
                    }

                }
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

                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    CustomerService customerService = new CustomerService();
                    txtCustomerCode.Text = customerService.GetNewCode(this.Name);
                    Common.EnableTextBox(false, txtCustomerCode);
                    cmbTitle.Focus();
                }
                else
                {
                    Common.EnableTextBox(true, txtCustomerCode);
                    txtCustomerCode.Focus();
                }
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
                if (e.KeyCode.Equals(Keys.F3))
                {
                    CustomerService customerService = new CustomerService();
                    DataView dvAllReferenceData = new DataView(customerService.GetAllActiveCustomersDataTable());
                    if (dvAllReferenceData.Count != 0)
                    { 
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtCustomerCode);
                        txtCustomerCode_Leave(this, e);
                    }
                }
                Common.SetFocus(e, tabCustomer, tpGeneral);
                Common.SetFocus(e, cmbGender);
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
                pbCustomer.Image = UI.Windows.Properties.Resources.Default_Customer;
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

        private void cmbGender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbCustomerGroup);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbCustomerGroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtReferenceNo);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtReferenceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtRepresentativeName);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtRepresentativeName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtRepresentativeMoblie);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtRepresentativeMoblie_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtNic);
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
                Common.SetFocus(e, txtRNic);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtRNic_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabCustomer, tpContact);
                Common.SetFocus(e, txtBillingAddress1);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBillingAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtBillingAddress2);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBillingAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtBillingAddress3);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBillingAddress3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtContactPerson);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtContactPerson_KeyDown(object sender, KeyEventArgs e)
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

        private void txtBillingTelephone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtBillingMobile);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBillingMobile_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtBillingFax);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBillingFax_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabCustomer, tpDelivery);
                Common.SetFocus(e, txtDeliveryAddress1);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDeliveryAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtDeliveryAddress2);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDeliveryAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtDeliveryAddress3);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDeliveryAddress3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtDeliveryTelephone);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDeliveryTelephone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtDeliveryMobile);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDeliveryMobile_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtDeliveryFax);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDeliveryFax_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabCustomer, tpFinancial);
                Common.SetFocus(e, txtLedgerCode);
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
                Common.SetFocus(e, txtOtherLedgerCode);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherLedgerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbCustomerType);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbCustomerType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtCreditLimit);
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
                Common.SetFocus(e, txtChequeLimit);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtChequeLimit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtMaxCashDiscount);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMaxCashDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbPaymentMethod);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbPaymentMethod_KeyDown(object sender, KeyEventArgs e)
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
                Common.SetFocus(e, txtChequePeriod);
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
                Common.SetFocus(e, txtBillingTelephone);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTax1.Checked) { txtTax1No.Enabled = true; txtTax1No.Focus(); }
                else { txtTax1No.Enabled = false; txtTax1No.Text = string.Empty; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTax2.Checked) { txtTax2No.Enabled = true; txtTax2No.Focus(); }
                else { txtTax2No.Enabled = false; txtTax2No.Text = string.Empty; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTax3.Checked) { txtTax3No.Enabled = true; txtTax3No.Focus(); }
                else { txtTax3No.Enabled = false; txtTax3No.Text = string.Empty; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTax4.Checked) { txtTax4No.Enabled = true; txtTax4No.Focus(); }
                else { txtTax4No.Enabled = false; txtTax4No.Text = string.Empty; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTax5.Checked) { txtTax5No.Enabled = true; txtTax5No.Focus(); }
                else { txtTax5No.Enabled = false; txtTax5No.Text = string.Empty; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtChequePeriod_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtMaxCreditDiscount);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMaxCreditDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtBankDraft);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBankDraft_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtTemporallyLimit);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTemporallyLimit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (taxCount.Equals(0))
                {
                    Common.SetFocus(e, tabCustomer, tpOther);
                    Common.SetFocus(e, txtAreaCode);
                }
                else
                {
                    Common.SetFocus(e, tabCustomer, tpTaxDetails);
                    Common.SetFocus(e, txtTax1No);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAreaCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtTerritoryCode);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAreaCode_Leave(object sender, EventArgs e)
        {
            try
            {
                AreaService areaService = new AreaService();
                existingArea = areaService.GetAreasByCode(txtAreaCode.Text.Trim());
                if (existingArea != null)
                    txtAreaName.Text = existingArea.AreaName;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTerritoryCode_Leave(object sender, EventArgs e)
        {
            try
            {
                TerritoryService territoryService = new TerritoryService();
                existingTerritory = territoryService.GetTerritoryByCode(txtTerritoryCode.Text.Trim());
                if (existingTerritory != null)
                    txtTerritoryName.Text = existingTerritory.TerritoryName;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBrokerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                BrokerService brokerService = new BrokerService();
                existingBroker = brokerService.GetBrokersByCode(txtBrokerCode.Text.Trim());
                if (existingBroker != null)
                    txtBrokerName.Text = existingBroker.BrokerName;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTerritoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtBrokerCode);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBrokerCode_KeyDown(object sender, KeyEventArgs e)
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

        private void txtLedgerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtLedgerCode.Text);
                if (existingAccLedgerAccount != null)
                    txtLedgerDescription.Text = existingAccLedgerAccount.LedgerName;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherLedgerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtOtherLedgerCode.Text);
                if (existingAccLedgerAccount != null)
                    txtOtherLedgerDescription.Text = existingAccLedgerAccount.LedgerName;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCustomerName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                {
                    return;
                }
                if (txtCustomerName.Text.Trim() != string.Empty)
                {
                    CustomerService customerService = new CustomerService();

                    existingCustomer = customerService.GetCustomersByName(txtCustomerName.Text.Trim());

                    if (existingCustomer != null)
                    {
                        LoadCustomer(existingCustomer);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                    }
                    else
                    {
                        Toast.Show("" + this.Text + " - " + txtCustomerName.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtCustomerName.Focus();
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLedgerDescription_Leave(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByName(txtLedgerDescription.Text);
                if (existingAccLedgerAccount != null)
                    txtLedgerCode.Text = existingAccLedgerAccount.LedgerCode;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherLedgerDescription_Leave(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByName(txtOtherLedgerDescription.Text);
                if (existingAccLedgerAccount != null)
                    txtOtherLedgerCode.Text = existingAccLedgerAccount.LedgerCode;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAreaName_Leave(object sender, EventArgs e)
        {
            try
            {
                AreaService areaService = new AreaService();
                existingArea = areaService.GetAreasByName(txtAreaName.Text.Trim());
                if (existingArea != null)
                    txtAreaCode.Text = existingArea.AreaCode;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTerritoryName_Leave(object sender, EventArgs e)
        {
            try
            {
                TerritoryService territoryService = new TerritoryService();
                existingTerritory = territoryService.GetTerritoryByName(txtTerritoryName.Text.Trim());
                if (existingTerritory != null)
                    txtTerritoryCode.Text = existingTerritory.TerritoryCode;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBrokerName_Leave(object sender, EventArgs e)
        {
            try
            {
                BrokerService brokerService = new BrokerService();
                existingBroker = brokerService.GetBrokersByName(txtBrokerName.Text.Trim());
                if (existingBroker != null)
                    txtBrokerCode.Text = existingBroker.BrokerCode;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTax1No_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                else
                {
                    if (taxCount.Equals(1))
                    {
                        tabCustomer.SelectedIndex = (tabCustomer.SelectedIndex + 1) % tabCustomer.TabCount;
                        ActiveControl = txtAreaCode;
                        txtAreaCode.Focus();
                    }
                    else if (taxCount >= 2)
                    {
                        ActiveControl = chkTax2;
                        chkTax2.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTax2No_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                else
                {
                    if (taxCount.Equals(2))
                    {
                        tabCustomer.SelectedIndex = (tabCustomer.SelectedIndex + 1) % tabCustomer.TabCount;
                        ActiveControl = txtAreaCode;
                        txtAreaCode.Focus();
                    }
                    else if (taxCount >= 3)
                    {
                        ActiveControl = chkTax3;
                        chkTax3.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTax3No_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                else
                {
                    if (taxCount.Equals(3))
                    {
                        tabCustomer.SelectedIndex = (tabCustomer.SelectedIndex + 1) % tabCustomer.TabCount;
                        ActiveControl = txtAreaCode;
                        txtAreaCode.Focus();
                    }
                    else if (taxCount >= 4)
                    {
                        ActiveControl = chkTax4;
                        chkTax4.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTax4No_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                else
                {
                    if (taxCount.Equals(4))
                    {
                        tabCustomer.SelectedIndex = (tabCustomer.SelectedIndex + 1) % tabCustomer.TabCount;
                        ActiveControl = txtAreaCode;
                        txtAreaCode.Focus();
                    }
                    else if (taxCount >= 5)
                    {
                        ActiveControl = chkTax5;
                        chkTax5.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTax5No_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabCustomer, tpOther);
                Common.SetFocus(e, txtAreaCode);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationArea_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AreaService areaService = new AreaService();
                Common.SetAutoComplete(txtAreaCode, areaService.GetAllAreaCodes(), chkAutoCompleationArea.Checked);
                Common.SetAutoComplete(txtAreaName, areaService.GetAllAreaNames(), chkAutoCompleationArea.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationTerritory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TerritoryService territoryService = new TerritoryService();

                Common.SetAutoComplete(txtTerritoryCode, territoryService.GetAllTerritoryCodes(), chkAutoCompleationTerritory.Checked);
                Common.SetAutoComplete(txtTerritoryName, territoryService.GetAllTerritoryNames(), chkAutoCompleationTerritory.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationBroker_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                BrokerService brokerService = new BrokerService();
                Common.SetAutoComplete(txtBrokerCode, brokerService.GetAllBrokerCodes(), chkAutoCompleationBroker.Checked);
                Common.SetAutoComplete(txtBrokerName, brokerService.GetAllBrokerNames(), chkAutoCompleationBroker.Checked);
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
                Common.SetAutoComplete(txtLedgerDescription, accLedgerAccountService.GetLedgerNames(), chkAutoCompleationLedger.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationOtherLedger_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();

                Common.SetAutoComplete(txtOtherLedgerCode, accLedgerAccountService.GetLedgerCodes(), chkAutoCompleationOtherLedger.Checked);
                Common.SetAutoComplete(txtOtherLedgerDescription, accLedgerAccountService.GetLedgerNames(), chkAutoCompleationOtherLedger.Checked);
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
                CustomerService customerService = new CustomerService();
                Common.SetAutoComplete(txtCustomerCode, customerService.GetAllCustomerCodes(), chkAutoCompleationCustomer.Checked);
                Common.SetAutoComplete(txtCustomerName, customerService.GetAllCustomerNames(), chkAutoCompleationCustomer.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAreaName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtAreaCode);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTerritoryName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtTerritoryCode);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBrokerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtBrokerCode);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLedgerDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtLedgerCode);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherLedgerDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtOtherLedgerCode);
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

        private void txtBillingTelephone_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtBillingMobile_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtBillingFax_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDeliveryTelephone_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDeliveryMobile_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDeliveryFax_KeyPress(object sender, KeyPressEventArgs e)
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

        

 
    }
}
