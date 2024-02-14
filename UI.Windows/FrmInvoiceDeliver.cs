using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using System.Transactions;

using Domain;
using Report.Inventory;
using Utility;
using Service;
using System.Threading;
using System.Drawing.Printing;


namespace UI.Windows
{
    public partial class FrmInvoiceDeliver : UI.Windows.FrmBaseTransactionForm
    {


        int documentID = 4;
        int paymentType;
        bool isInvProduct;
        static string batchNumber;
        static DateTime expiryDate;
        private int recallStatus = 0; // 0 - Invoice, 1 - Quotation, 2 - Performa Invoice, 3 - Dispatch
        int documentState;

        int autoRecall =0;

        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();
        
        List<InvSalesDetailTemp> invSalesDetailTempList = new List<InvSalesDetailTemp>();
        InvProductMaster existingInvProductMaster;
        InvSalesDetailTemp existingInvSalesDetailTemp = new InvSalesDetailTemp();

        //List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();
        List<InvProductSerialNoTemp> invProductSerialNoTempList;
        List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();
        List<InvProductExpiaryTemp> invProductExpiaryTempList = new List<InvProductExpiaryTemp>();
        List<OtherExpenseTemp> otherExpenceTempList;

        List<PaymentTemp> paymentTempList;

        public FrmInvoiceDeliver()
        {
            InitializeComponent();
        }

        public FrmInvoiceDeliver(string DocumentNo, int documentID)
        {
            InitializeComponent();
            btnSave.Enabled = true;
            invSalesDetailTempList = null;
            existingInvProductMaster = null;
            existingInvSalesDetailTemp = null;
            invProductSerialNoTempList = null;
            dgvItemDetails.AutoGenerateColumns = false;
            txtDocumentNo.Text = DocumentNo;
            this.documentID = documentID;
            LocationService locationService = new LocationService();
            Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
            cmbLocation.SelectedValue = Common.LoggedLocationID;
            cmbLocation.SelectedIndex = -1;
            autoRecall = 1;
            RecallDocument(DocumentNo);
        }

        #region Form Events

        private void FrmInvoice_Load(object sender, EventArgs e)
        {

        }

        private void btnTaxBreakdown_Click(object sender, EventArgs e)
        {

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

        private void txtCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                { txtCustomerName.Focus(); }

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
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCustomerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtCustomerCode.Text.Trim().Equals(string.Empty))
                {LoadCustomer(true, txtCustomerCode.Text.Trim());}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadCustomer(bool isCode, string strCustomer)
        {
            try
            {
                CustomerService customerService = new CustomerService();
                Customer existingCustomer  = new Customer();

                if (isCode)
                {
                    existingCustomer = customerService.GetCustomersByCode(strCustomer);
                    if (isCode && strCustomer.Equals(string.Empty))
                    {
                        txtCustomerName.Focus();
                        return;
                    }
                }
                else
                {existingCustomer = customerService.GetCustomersByName(strCustomer);}

                if (existingCustomer != null)
                {
                    PaymentMethodService paymentMethodService = new PaymentMethodService();
                    txtCustomerCode.Text = existingCustomer.CustomerCode;
                    txtCustomerName.Text = existingCustomer.CustomerName;
                    //txtCreditLimit.Text = (existingCustomer.CreditLimit + existingCustomer.TemporaryLimit).ToString();
                    //txtPaymentTerms.Text = (paymentMethodService.GetPaymentMethodsByID(existingCustomer.PaymentMethodID)).PaymentMethodName;
                    txtSalesPersonCode.Focus();
                }
                else
                {
                    Toast.Show("Customer - " + strCustomer.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    return;
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
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                { txtSalesPersonCode.Focus(); }

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

        private void txtCustomerName_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadCustomer(false, txtCustomerName.Text.Trim());
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationSalesPerson_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                Common.SetAutoComplete(txtSalesPersonCode, invSalesPersonService.GetAllInvSalesPersonCodes(), chkAutoCompleationSalesPerson.Checked);
                Common.SetAutoComplete(txtSalesPersonName, invSalesPersonService.GetAllInvSalesPersonNames(), chkAutoCompleationSalesPerson.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesPersonCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {txtSalesPersonName.Focus();}

                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    DataView dvAllReferenceData = new DataView(invSalesPersonService.GetAllActiveSalesPersonsDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtSalesPersonCode);
                        txtSalesPersonCode_Leave(this, e);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesPersonCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtSalesPersonCode.Text.Trim().Equals(string.Empty))
                {LoadSalesPerson(true, txtSalesPersonCode.Text.Trim());}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadSalesPerson(bool isCode, string strSalesPerson)
        {
            try
            {
                InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                InvSalesPerson existingSalesPerson = new InvSalesPerson();

                if (isCode)
                {
                    existingSalesPerson = invSalesPersonService.GetInvSalesPersonByCode(strSalesPerson);
                    if (isCode && strSalesPerson.Equals(string.Empty))
                    {
                        txtSalesPersonName.Focus();
                        return;
                    }
                }
                else
                { existingSalesPerson = invSalesPersonService.GetInvSalesPersonByName(strSalesPerson); }

                if (existingSalesPerson != null)
                {
                    txtSalesPersonCode.Text = existingSalesPerson.SalesPersonCode;
                    txtSalesPersonName.Text = existingSalesPerson.SalesPersonName;
                    txtRemark.Focus();
                }
                else
                {
                    Toast.Show("SalesPerson - " + strSalesPerson.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesPersonName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {txtRemark.Focus();}

                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    DataView dvAllReferenceData = new DataView(invSalesPersonService.GetAllActiveSalesPersonsDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtSalesPersonCode);
                        txtSalesPersonCode_Leave(this, e);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesPersonName_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadSalesPerson(false, txtSalesPersonName.Text.Trim());
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
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {cmbLocation.Focus();}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {cmbLocation_Validated(this, e);}
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
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                { txtReferenceNo.Focus(); }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbLocation_Validated(object sender, EventArgs e)
        {
            try
            {
                LocationService locationService = new LocationService();
            
                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbLocation.Text.Trim()).LocationID, documentID);
                if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }


                if (cmbLocation.SelectedValue == null || locationService.IsExistsLocation(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim())).Equals(false))
                {
                    Toast.Show("Location - " + cmbLocation.Text.Trim().Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    cmbLocation.Focus();
                    return;
                }
                else
                {
                    //cmbCostCentre.Focus();
                    //cmbDispatchLocation.SelectedIndex = cmbLocation.SelectedIndex;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

      
        private void txtProductCode_Leave(object sender, EventArgs e)
        {
            try
            {
                //loadProductDetails(true, txtProductCode.Text.Trim(), 0, dtpExpiry.Value);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        //private void loadProductDetails(bool isCode, string strProduct, long unitofMeasureID, DateTime expiryDate)
        //{
        //    try
        //    {
        //        existingInvProductMaster = new InvProductMaster();

        //        if (strProduct.Equals(string.Empty))
        //        {return;}

        //        InvProductMasterService InvProductMasterService = new InvProductMasterService();
                
        //        if (isCode)
        //        {
        //            existingInvProductMaster = InvProductMasterService.GetProductsByCode(strProduct);
        //            if (isCode && strProduct.Equals(string.Empty))
        //            {
        //                txtProductName.Focus();
        //                return;
        //            }
        //        }
        //        else
        //        {existingInvProductMaster = InvProductMasterService.GetProductsByName(strProduct);} 

        //        if (existingInvProductMaster != null)
        //        {
        //            InvSalesServices invSalesServices = new InvSalesServices();
        //            if (invSalesDetailTempList == null)
        //            {invSalesDetailTempList = new List<InvSalesDetailTemp>();}
        //            existingInvSalesDetailTemp = invSalesServices.getSalesDetailTemp(invSalesDetailTempList, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID);
        //            if (existingInvSalesDetailTemp != null)
        //            {
        //                txtProductCode.Text = existingInvSalesDetailTemp.ProductCode;
        //                txtProductName.Text = existingInvSalesDetailTemp.ProductName;
        //                cmbUnit.SelectedValue = existingInvSalesDetailTemp.UnitOfMeasureID;
        //                txtRate.Text = Common.ConvertDecimalToStringCurrency(existingInvSalesDetailTemp.Rate);
        //                txtQty.Text = Common.ConvertDecimalToStringQty(existingInvSalesDetailTemp.Qty);
        //                txtFreeQty.Text = Common.ConvertDecimalToStringQty(existingInvSalesDetailTemp.FreeQty);
        //                txtProductDiscountAmount.Text = Common.ConvertDecimalToStringCurrency(existingInvSalesDetailTemp.DiscountAmount);
        //                txtProductDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(existingInvSalesDetailTemp.DiscountPercentage);
        //                if (existingInvProductMaster.IsExpiry)
        //                {
        //                    dtpExpiry.Value = Common.ConvertStringToDate((existingInvSalesDetailTemp.ExpiryDate == null ? dtpExpiry.Value.ToString() : existingInvSalesDetailTemp.ExpiryDate.ToString()));
        //                    dtpExpiry.Enabled = true;
        //                }
        //                else
        //                {
        //                    dtpExpiry.Value = Common.ConvertDateTimeToDate(DateTime.Now);
        //                    dtpExpiry.Enabled = false;
        //                }
        //                Common.EnableComboBox(true, cmbUnit);
        //                if (unitofMeasureID.Equals(0))
        //                {cmbUnit.Focus();}
        //            }
        //        }
        //        else
        //        {
        //            Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
        //    }
        //}

        private void txtProductName_Leave(object sender, EventArgs e)
        {
            try
            {
                //loadProductDetails(false, txtProductName.Text.Trim(), 0, dtpExpiry.Value);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

      

        public void SetBatchNumber(string batchNo)
        {
            batchNumber = batchNo;
        }

 
       
     


        private void txtSubTotalDiscountPercentage_Leave(object sender, EventArgs e)
        {
            try
            {
                if (chkSubTotalDiscountPercentage.Checked)
                {
                    decimal value = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.Trim());
                    if (value >= 101)
                    {
                        Toast.Show("", Toast.messageType.Information, Toast.messageAction.SubTotalDiscountPercentageExceed);
                        txtSubTotalDiscountPercentage.Focus();
                    }
                }
                else
                {
                    decimal value = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.Trim());
                    decimal grossAmt = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.Trim());

                    if (value > grossAmt)
                    {
                        Toast.Show("", Toast.messageType.Information, Toast.messageAction.SubTotalDiscountAmountExceed);
                        txtSubTotalDiscountPercentage.Focus();
                    }
                }
                //txtNetAmount.Text= Common.ConvertDecimalToStringCurrency(Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.Trim())- Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.Trim())) ;
                GetSummarizeSubFigures();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void GetSummarizeFiguresD(List<InvSalesDetailTemp> listItem)
        {
            decimal totSellingValue = listItem.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.NetAmount);

            totSellingValue = Common.ConvertStringToDecimalCurrency(totSellingValue.ToString());
            txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(totSellingValue);
            txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(totSellingValue);
            //txtTotalSellingValue.Text = Common.ConvertDecimalToStringCurrency(totSellingValue);
        }
 

        private void GetSummarizeSubFigures()
        {
            

            CommonService commonService = new CommonService();
            CustomerService customerService = new CustomerService();

            decimal tax1 = 0;
            decimal tax2 = 0;
            decimal tax3 = 0;
            decimal tax4 = 0;
            decimal tax5 = 0;

            //Get Gross Amount
            //Note: apply this for first para- && x.DocumentID == '<required Transaction Id>' && x.LocationID ==  'Transaction Location' && '<userID>' && 'machine id'
            decimal grossAmount = 0;

            grossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.ToString());

            decimal otherChargersAmount = 0;

            //Get Discount Amount
            bool isSubDiscount = chkSubTotalDiscountPercentage.Checked;

            decimal subDiscount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());
            decimal discountAmount = Common.ConvertStringToDecimalCurrency(Common.GetDiscountAmount(isSubDiscount, grossAmount, subDiscount).ToString());

            //Read from Tax
            decimal taxAmount = 0;
            if (chkTaxEnable.Checked)
            {
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(2, grossAmount, customerService.GetCustomersByCode(txtCustomerCode.Text.ToString()).CustomerID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
            }

            //Get Net Amount
            decimal netAmount = Common.ConvertStringToDecimalCurrency((Common.GetTotalAmount(grossAmount, otherChargersAmount, taxAmount) - Common.GetTotalAmount(discountAmount)).ToString());

            //Assign calculated values
            txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(grossAmount);
             
            txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(discountAmount);
            txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(taxAmount);
            txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(netAmount);
        }

  

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                        RecallDocument(txtDocumentNo.Text.Trim());
                    else
                    {
                        txtDocumentNo.Text = GetDocumentNo(true);
                        txtCustomerCode.Focus();
                    }
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

        private void chkAutoCompleationInvoiceNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvSalesServices invSalesServices = new InvSalesServices();
                Common.SetAutoComplete(txtDocumentNo, invSalesServices.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationInvoiceNo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
 

        private void chkAutoCompleationPerformaNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationDispatchNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //Load dispatch document numbers
                InvSalesServices invSalesServices = new InvSalesServices();
                int dispatchDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDispatchNote").DocumentID;
                Common.SetAutoComplete(txtDispatchNo, invSalesServices.GetAllDocumentNumbersToInvoice(dispatchDocumentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDispatchNo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

       
        #endregion

        #region Methods
        public override void FormLoad()
        {
            try
            {
                dgvItemDetails.AutoGenerateColumns = false;
               


                // Load Payment methods
                PaymentMethodService paymentMethodService = new PaymentMethodService();
              

                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                List<AccLedgerAccount> accLedgerAccount = new List<AccLedgerAccount>();
                accLedgerAccount = accLedgerAccountService.GetBankList();
               

              
                // Load Customers
                CustomerService customerService = new CustomerService();
                Common.SetAutoComplete(txtCustomerCode, customerService.GetAllCustomerCodes(), chkAutoCompleationCustomer.Checked);
                Common.SetAutoComplete(txtCustomerName, customerService.GetAllCustomerNames(), chkAutoCompleationCustomer.Checked);

                // Load SalesPersons
                InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                Common.SetAutoComplete(txtSalesPersonCode, invSalesPersonService.GetAllInvSalesPersonCodes(), chkAutoCompleationSalesPerson.Checked);
                Common.SetAutoComplete(txtSalesPersonName, invSalesPersonService.GetAllInvSalesPersonNames(), chkAutoCompleationSalesPerson.Checked);
            
                isInvProduct = true;

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmInvoice");
                this.Text = autoGenerateInfo.FormText;

                Common.ExchangeRate = autoGenerateInfo.ExchangeRate;

                txtExchangeRate.Text = Common.ConvertDecimalToStringCurrency(autoGenerateInfo.ExchangeRate);
                //if (autoGenerateInfo.IsDispatchRecall)
                //{
                    txtDispatchNo.Enabled = true;
                    chkAutoCompleationDispatchNo.Enabled = true;
                    btnDispatchDetails.Enabled = true;
                
                //}
                //else
                //{
                //    txtDispatchNo.Enabled = false;
                //    chkAutoCompleationDispatchNo.Enabled = false;
                //    btnDispatchDetails.Enabled = false;

                //    chkSaveWithDispatch.Enabled = true;
                //}

             
                documentID = autoGenerateInfo.DocumentID;

                //accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                //if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }

             
                base.FormLoad();

                RefreshDocumentNumber();
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

                if (autoRecall == 0)
                {
                    // Disable product details controls
                    tbBody.Enabled = false;
                    grpFooter.Enabled = false;

                    Common.EnableTextBox(true, txtCustomerCode, txtCustomerName, txtSalesPersonCode, txtSalesPersonName, txtDocumentNo);
                    Common.EnableButton(true, btnDocumentDetails);
                    Common.EnableComboBox(true, cmbLocation);
                    Common.EnableButton(false, btnSave, btnPause, btnView);

                    //if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                    LocationService locationService = new LocationService();
                    Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
                    cmbLocation.SelectedValue = Common.LoggedLocationID;
                    cmbLocation.SelectedIndex = -1;

                    AutoGenerateInfo autoGenerateInitInfo = new AutoGenerateInfo();
                    autoGenerateInitInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmInvoice");

                    //if (autoGenerateInitInfo.IsDispatchRecall)
                    //{
                    txtDispatchNo.Enabled = true;
                    chkAutoCompleationDispatchNo.Enabled = true;
                    btnDispatchDetails.Enabled = true;


                    //}
                    //else
                    //{
                    //    txtDispatchNo.Enabled = false;
                    //    chkAutoCompleationDispatchNo.Enabled = false;
                    //    btnDispatchDetails.Enabled = false;

                    //    chkSaveWithDispatch.Enabled = true;
                    //}

                    invSalesDetailTempList = null;
                    existingInvProductMaster = null;
                    existingInvSalesDetailTemp = null;
                    invProductSerialNoTempList = null;

                    recallStatus = 0;

                    cmbLocation.SelectedValue = Common.LoggedLocationID;

                    txtDocumentNo.Text = GetDocumentNo(true);
                    this.ActiveControl = txtCustomerCode;
                    txtCustomerCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

       
       

        

        private string GetDocumentNo(bool isTemporytNo)
        {
            try
            {
                InvSalesServices invSalesServices = new InvSalesServices();
                LocationService locationService = new LocationService();
                return invSalesServices.GetDocumentNo("FrmInvoice",1, locationService.GetLocationsByID(1).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }
        }

        
  
        
        private void RefreshDocumentNumber()
        {
            try
            {
                                 //Load Document Numbers
                InvSalesServices invSalesServices = new InvSalesServices();
                Common.SetAutoComplete(txtDocumentNo, invSalesServices.GetAllDocumentNumbers(documentID, 1), chkAutoCompleationInvoiceNo.Checked);

                //Load dispatch document numbers
               //int dispatchDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDispatchNote").DocumentID;
               // Common.SetAutoComplete(txtDispatchNo, invSalesServices.GetAllDocumentNumbersToInvoice(dispatchDocumentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDispatchNo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void Save()
        {
            if ((Toast.Show("Invoice  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {


                 foreach (DataGridViewRow row in dgvItemDetails.Rows)
                    {
                        if (dgvItemDetails["ProductCode", row.Index].Value.ToString()!= string.Empty )
                        {
                            bool isdeliver = Common.ConvertStringToBool(dgvItemDetails["IsDeliver", row.Index].Value.ToString());
                            int isdeliverint = isdeliver== true? 1 :0;

                            string sqlst = "update InvSalesDetail set Waight=  " + Common.ConvertStringToDecimal(dgvItemDetails["Waight", row.Index].Value.ToString()) + " ,IsDeliver = " + isdeliverint + " where InvSalesDetailID = " + Common.ConvertStringToLong(dgvItemDetails["InvSalesDetailID", row.Index].Value.ToString()) + " ";
                            CommonService.ExecuteSqlstatement(sqlst);
                            
                        }
                    }
                bool saveDocument = true;
                this.Cursor = Cursors.WaitCursor;
                //string NewDocumentNo;
                 
                 this.Cursor = Cursors.Default;
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Invoice  - " + txtDocumentNo + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    //GenerateReport(NewDocumentNo.Trim(), 1);
                    //RefreshDocumentNumber();
                    ClearForm();
                    this.CloseForm();
                }
                else
                {
                    Toast.Show("Invoice  - " + txtDocumentNo + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        public override void Pause()
        {
            //if ((Toast.Show("Invoice  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    string NewDocumentNo;
            //    bool saveDocument = SaveDocument(0, txtDocumentNo.Text.Trim(), out NewDocumentNo);
            //    this.Cursor = Cursors.Default;
            //    if (saveDocument.Equals(true))
            //    {
            //        Toast.Show("Invoice  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
            //        GenerateReport(NewDocumentNo.Trim(), 0);
            //        RefreshDocumentNumber();
            //        ClearForm();
            //    }
            //    else
            //    {
            //        Toast.Show("Invoice  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
            //        return;
            //    }
            //}
        }

        private bool SaveDocument(int documentStatus, string documentNo, out string newDocumentNo)
        {
            try
            {
                InvSalesServices invSalesServices = new InvSalesServices();
                InvSalesHeader invSalesHeader = new InvSalesHeader();
                LocationService locationService = new LocationService();
                Location Location = new Location();
                CustomerService customerService = new CustomerService();
                Customer customer = new Customer();

                customer = customerService.GetCustomersByCode(txtCustomerCode.Text.Trim());

                InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                InvSalesPerson invSalesPerson = new InvSalesPerson();

                invSalesPerson = invSalesPersonService.GetInvSalesPersonByCode(txtSalesPersonCode.Text.Trim());

                //Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                invSalesHeader = invSalesServices.getSalesHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(),Common.LoggedLocationID);
                if (invSalesHeader == null)
                { invSalesHeader = new InvSalesHeader();}

                invSalesHeader.SalesHeaderID = invSalesHeader.InvSalesHeaderID;
                invSalesHeader.CompanyID = Common.LoggedCompanyID;
                invSalesHeader.CostCentreID = 1;
                invSalesHeader.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscount.Text.ToString());
                if (chkSubTotalDiscountPercentage.Checked)
                {invSalesHeader.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());}
                invSalesHeader.DocumentDate = Common.FormatDateTime(dtpInvoiceDate.Value);
                invSalesHeader.DocumentID = documentID;
                invSalesHeader.DocumentStatus = documentStatus;
                invSalesHeader.DocumentNo = documentNo.Trim();
                invSalesHeader.GrossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.ToString());
                invSalesHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                invSalesHeader.LineDiscountTotal = GetLineDiscountTotal(invSalesDetailTempList);
                invSalesHeader.LocationID = Common.LoggedLocationID;
                invSalesHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtNetAmount.Text.ToString());
                
                invSalesHeader.RecallDocumentStatus = recallStatus;
                invSalesHeader.IsDispatch = true;

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                this.Text = autoGenerateInfo.FormText;
                
                invSalesHeader.ExchangeRate = autoGenerateInfo.ExchangeRate;

                invSalesHeader.PaymentMethodID = customer.PaymentMethodID; //Common.ConvertStringToInt(cmbPaymentMethod.SelectedValue.ToString());

                ////if (!txtQuotationNo.Text.Trim().Equals(string.Empty))
                ////{
                ////    InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                ////    InvPurchaseOrderHeader invPurchaseOrderHeader = new InvPurchaseOrderHeader();

                ////    invPurchaseOrderHeader = invPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());
                ////    invSalesHeader.ReferenceDocumentDocumentID = invPurchaseOrderHeader.DocumentID;
                ////    invSalesHeader.ReferenceDocumentID = invPurchaseOrderHeader.InvPurchaseOrderHeaderID;
                ////}

                invSalesHeader.TransStatus = 1;
                invSalesHeader.ReferenceNo = txtReferenceNo.Text.Trim();
                invSalesHeader.Remark = txtRemark.Text.Trim();
                invSalesHeader.CustomerID = customer.CustomerID;
                invSalesHeader.SalesPersonID = invSalesPerson.InvSalesPersonID;
                invSalesHeader.StartTime = Common.FormatDateTime(DateTime.Now);
                invSalesHeader.EndTime = Common.FormatDateTime(DateTime.Now);

              

                if (invSalesDetailTempList == null)
                {invSalesDetailTempList = new List<InvSalesDetailTemp>();}

                if (invProductSerialNoTempList == null)
                {invProductSerialNoTempList = new List<InvProductSerialNoTemp>();}

                if (otherExpenceTempList == null)
                {otherExpenceTempList = new List<OtherExpenseTemp>();}

                if (paymentTempList == null)
                {paymentTempList = new List<PaymentTemp>();}


                return invSalesServices.Save(invSalesHeader, invSalesDetailTempList, invProductSerialNoTempList, out newDocumentNo, otherExpenceTempList, paymentTempList, 0, Common.ConvertStringToDecimalCurrency(txtExchangeRate.Text.Trim()), this.Name);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                newDocumentNo = string.Empty;
                this.Cursor = Cursors.Default;

                return false;
            }
        }

        private void GenerateReport(string documentNo, int documentStatus)
        {
            try
            {
                InvReportGenerator invReportGenerator = new InvReportGenerator();
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                invReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private decimal GetLineDiscountTotal(List<InvSalesDetailTemp> listItem)
        {
            return listItem.GetSummaryAmount(x => x.DiscountAmount);
        }

        private bool RecallDocument(string documentNo)
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;
                InvSalesServices invSalesServices = new InvSalesServices();
                InvSalesHeader invSalesHeader = new InvSalesHeader();

                invSalesHeader = invSalesServices.getSalesHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(),1);
                if (invSalesHeader != null)
                {


                    if (invSalesHeader.IsDispatch==true)
                    {
                        grpFooter.Enabled = true;
                        Common.EnableTextBox(false, txtCustomerCode, txtCustomerName, txtSalesPersonCode, txtSalesPersonName, txtDocumentNo);
                        Common.EnableTextBox(false, txtGrossAmount, txtSubTotalDiscount, txtSubTotalDiscountPercentage);
                        Common.EnableComboBox(false, cmbLocation);
                        Common.EnableButton(true, btnSave, btnPause);

                    }
                    else
                    {
                        grpFooter.Enabled = true;
                        Common.EnableTextBox(false, txtCustomerCode, txtCustomerName, txtSalesPersonCode, txtSalesPersonName, txtDocumentNo);
                        Common.EnableTextBox(false, txtGrossAmount, txtSubTotalDiscount, txtSubTotalDiscountPercentage);
                        Common.EnableComboBox(false, cmbLocation);
                        Common.EnableButton(true, btnSave, btnPause);
                        btnSave.Enabled = true;
                        
                    }

                    CustomerService customerService = new CustomerService();
                    Customer customer = new Customer();

                    customer = customerService.GetCustomersByCode(txtCustomerCode.Text.Trim());

                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    InvSalesPerson invSalesPerson = new InvSalesPerson();

                    invSalesPerson = invSalesPersonService.GetInvSalesPersonByCode(txtSalesPersonCode.Text.Trim());

                    cmbLocation.SelectedValue = invSalesHeader.LocationID;
                    cmbCostCentre.SelectedValue = invSalesHeader.CostCentreID;
                    //cmbPaymentMethod.SelectedValue = invSalesHeader.PaymentMethodID;

                    cmbCostCentre.Refresh();

                    documentState = invSalesHeader.DocumentStatus;

                    if (!invSalesHeader.DiscountPercentage.Equals(0))
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(invSalesHeader.DiscountPercentage);
                        chkSubTotalDiscountPercentage.Checked = true;
                    }
                    else
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(0);
                        chkSubTotalDiscountPercentage.Checked = false;
                    }

                    txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(invSalesHeader.DiscountAmount);
                  

                    dtpInvoiceDate.Value = Common.FormatDate(invSalesHeader.DocumentDate);

                    txtDocumentNo.Text = invSalesHeader.DocumentNo;
                    txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(invSalesHeader.GrossAmount);
                    txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(invSalesHeader.NetAmount);

                    recallStatus = invSalesHeader.RecallDocumentStatus;

                    

                    txtReferenceNo.Text = invSalesHeader.ReferenceNo;
                    txtRemark.Text = invSalesHeader.Remark;
                    customer = customerService.GetCustomersById(invSalesHeader.CustomerID);
                    txtCustomerCode.Text = customer.CustomerCode;
                    txtCustomerName.Text = customer.CustomerName;

                    invSalesPerson = invSalesPersonService.GetInvSalesPersonByID(invSalesHeader.SalesPersonID);
                    txtSalesPersonCode.Text = invSalesPerson.SalesPersonCode;
                    txtSalesPersonName.Text = invSalesPerson.SalesPersonName;

                     
                    dgvItemDetails.DataSource = null;
                    invSalesDetailTempList = invSalesServices.GetInvoceDetailEdit(invSalesHeader);
                    dgvItemDetails.DataSource = invSalesDetailTempList;
                    dgvItemDetails.Refresh();


                    //invSalesDetailTempList = new List<InvSalesDetailTemp>();
                    //dgvItemDetails.DataSource = null;
                    //invSalesDetailTempList = invSalesServices.GetInvoceDetailEdit(invSalesHeader);
                    //dgvItemDetails.DataSource = invSalesDetailTempList;
                    //dgvItemDetails.Refresh();

                    GetSummarizeFiguresD(invSalesDetailTempList);

                    
                    //Common.EnableButton(false, btnDocumentDetails, btnQuotationDetails, btnPerformaDetails, btnDispatchDetails);
                    
                    //txtGrossAmount.Focus();
                    ///txtGrossAmount.SelectAll();
                    ///
                    dgvItemDetails.Enabled = true;
                    dgvItemDetails.ReadOnly = false;
                    tbBody.Enabled = true;

                    this.Cursor = Cursors.Default;
                    return true;
                }
                else
                {
                    this.Cursor = Cursors.Default;

                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                this.Cursor = Cursors.Default;

                return false;
            }
        }

        public override void ClearForm()
        {
            dgvItemDetails.DataSource = null;
            dgvItemDetails.Refresh();
            autoRecall = 0;
            tbBody.SelectedTab = tbpGeneral;
            base.ClearForm();
        }

       
        private bool ReCallDispatch(string dispatchNo)
        {
            try
            {
                InvSalesHeader invDispatchHeader = new InvSalesHeader();
                InvSalesServices invSalesServices = new InvSalesServices();

                int dispatchDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDispatchNote").DocumentID;
                invDispatchHeader = invSalesServices.GetDispatchHeaderToInvoice(txtDispatchNo.Text.Trim(), dispatchDocumentID);
                if (invDispatchHeader != null)
                {
                    InvSalesPerson invSalesPerson = new InvSalesPerson();
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    Customer customer = new Customer();
                    CustomerService customerService = new CustomerService();

                    invSalesPerson = invSalesPersonService.GetInvSalesPersonByID(invDispatchHeader.SalesPersonID);
                    customer = customerService.GetCustomersById(invDispatchHeader.CustomerID);

                    txtSalesPersonCode.Text = invSalesPerson.SalesPersonCode;
                    txtSalesPersonName.Text = invSalesPerson.SalesPersonName;

                    txtCustomerCode.Text = customer.CustomerCode;
                    txtCustomerName.Text = customer.CustomerName;

                    cmbLocation.SelectedValue = invDispatchHeader.LocationID;
                    cmbLocation.Refresh();

                    if (!invDispatchHeader.DiscountPercentage.Equals(0))
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(invDispatchHeader.DiscountPercentage);
                        chkSubTotalDiscountPercentage.Checked = true;
                    }
                    else
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(0);
                        chkSubTotalDiscountPercentage.Checked = false;
                    }

                    txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(invDispatchHeader.DiscountAmount);

                    txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(invDispatchHeader.GrossAmount);
                    txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(invDispatchHeader.NetAmount);

                    txtReferenceNo.Text = invDispatchHeader.ReferenceNo;
                    txtRemark.Text = invDispatchHeader.Remark;

                   

                    recallStatus = 3;

                    dgvItemDetails.DataSource = null;
                    invSalesDetailTempList = invSalesServices.GetDispatchDetail(invDispatchHeader);
                    dgvItemDetails.DataSource = invSalesDetailTempList;
                    dgvItemDetails.Refresh();

                    Common.EnableTextBox(false, txtCustomerCode, txtCustomerName, txtSalesPersonCode, txtSalesPersonName, txtDocumentNo,txtDispatchNo, txtReferenceNo);
                    Common.EnableComboBox(false, cmbLocation);
                    Common.EnableButton(false,btnDocumentDetails, btnDispatchDetails);

                    grpFooter.Enabled = true;
                    if (accessRights.IsPause == true) { Common.EnableButton(true, btnPause); }
                    if (accessRights.IsSave == true) { Common.EnableButton(true, btnSave); }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return false;
            }
        }
        #endregion

        private void btnDispatchDetails_Click(object sender, EventArgs e)
        {
            ReCallDispatch(txtDispatchNo.Text.Trim());
        }

        private void txtGrossAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                txtSubTotalDiscountPercentage.Focus();
                txtSubTotalDiscountPercentage.SelectAll();
            }
        }

        private void txtSubTotalDiscountPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {   
                
                btnSave.Focus();
            }
        }

        private void btnTaxBreakdown_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerCode.Text.Trim() != "")
                {
                    CustomerService customerService = new CustomerService();

                    FrmTaxBreakdown frmTaxBreakdown = new FrmTaxBreakdown(customerService.GetCustomersByCode(txtCustomerCode.Text.Trim()).CustomerID, 2, Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.Trim()));
                    frmTaxBreakdown.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTaxEnable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GetSummarizeSubFigures();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (txtDocumentNo.Text != string.Empty)
            {
                GenerateReport(txtDocumentNo.Text.Trim(), 1);
                ClearForm();
                //RefreshDocumentNumber();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDocumentNo.Text != string.Empty)
                {

                    if (Toast.Show("" + this.Text + " - " + txtDocumentNo.Text, Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                    { return; }

                    InvSalesServices invSalesServices = new InvSalesServices();
                    invSalesServices.DeleteSales(txtDocumentNo.Text.Trim());

                    Toast.Show("" + this.Text + " - " + txtDocumentNo.Text, Toast.messageType.Information, Toast.messageAction.Delete);
                    ClearForm();
                     

                }
            }

            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtExchangeRate_TextChanged(object sender, EventArgs e)
        {

        }

        
        

    }
}
