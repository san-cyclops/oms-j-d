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
    public partial class FrmSalesReturnNote : UI.Windows.FrmBaseTransactionForm
    {
        /// <summary>
        /// Sales Return Note
        /// Design By - C.S.Malluwawadu
        /// Developed By - C.S.Malluwawadu
        /// Date - 29/08/2013
        /// </summary>
        /// 

        int documentID = 5;
        int paymentType;
        bool isInvProduct;
        static string batchNumber;

        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<InvSalesDetailTemp> invSalesDetailTempList = new List<InvSalesDetailTemp>();
        InvProductMaster existingInvProductMaster;
        InvSalesDetailTemp existingInvSalesDetailTemp = new InvSalesDetailTemp();

        List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();
        List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();
        List<InvProductExpiaryTemp> invProductExpiaryTempList = new List<InvProductExpiaryTemp>();

        List<OtherExpenseTemp> otherExpenceTempList;
        List<PaymentTemp> paymentTempList;

        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;

        public FrmSalesReturnNote()
        {
            InitializeComponent();
        }

        #region Methods
        public override void FormLoad()
        {
            try
            {
                dgvItemDetails.AutoGenerateColumns = false;
                dgvPaymentDetails.AutoGenerateColumns = false;
                dgvAdvanced.AutoGenerateColumns = false;

                // Load Payment methods
                PaymentMethodService paymentMethodService = new PaymentMethodService();
                Common.LoadPaymentMethods(cmbPaymentMethod, paymentMethodService.GetAllPaymentMethods());

                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                List<AccLedgerAccount> accLedgerAccount = new List<AccLedgerAccount>();
                accLedgerAccount = accLedgerAccountService.GetBankList();
                cmbBankCode.DataSource = accLedgerAccount;
                cmbBankCode.DisplayMember = "LedgerCode";
                cmbBankCode.ValueMember = "AccLedgerAccountID";
                cmbBankCode.Refresh();

                cmbBankName.DataSource = accLedgerAccount;
                cmbBankName.DisplayMember = "LedgerName";
                cmbBankName.ValueMember = "AccLedgerAccountID";
                cmbBankName.Refresh();

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                this.Text = autoGenerateInfo.FormText;

                documentID = autoGenerateInfo.DocumentID;

                // Load Unit of measures
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                Common.LoadUnitOfMeasures(cmbUnit, unitOfMeasureService.GetAllUnitOfMeasures());

                // Load Locations
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());

                // Load Customers
                CustomerService customerService = new CustomerService();
                Common.SetAutoComplete(txtCustomerCode, customerService.GetAllCustomerCodes(), chkAutoCompleationCustomer.Checked);
                Common.SetAutoComplete(txtCustomerName, customerService.GetAllCustomerNames(), chkAutoCompleationCustomer.Checked);

                // Load SalesPersons
                InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                Common.SetAutoComplete(txtSalesPersonCode, invSalesPersonService.GetAllInvSalesPersonCodes(), chkAutoCompleationSalesPerson.Checked);
                Common.SetAutoComplete(txtSalesPersonName, invSalesPersonService.GetAllInvSalesPersonNames(), chkAutoCompleationSalesPerson.Checked);

                //Load Return Types
                InvReturnTypeService returnTypeService = new InvReturnTypeService();
                Common.LoadInvReturnTypes(cmbReturnType, returnTypeService.GetAllInvReturnTypes(documentID));

                /////Load cost center name to combo
                CostCentreService costCentreService = new CostCentreService();
                List<CostCentre> costCentres = new List<CostCentre>();
                cmbCostCentre.DataSource = costCentreService.GetAllCostCentres();
                cmbCostCentre.DisplayMember = "CostCentreName";
                cmbCostCentre.ValueMember = "CostCentreID";
                cmbCostCentre.Refresh();
                cmbCostCentre.SelectedValue = locationService.GetLocationsByID(Common.LoggedLocationID).CostCentreID;

                isInvProduct = true;
                GetPrintingDetails();

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }

                if (Common.tStatus == true) { chkTStatus.Visible = true; } else { chkTStatus.Visible = false; }

                base.FormLoad();

                RefreshDocumentNumber();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void RefreshDocumentNumber()
        {
            try
            {
                //Load Document Numbers
                InvSalesServices invSalesServices = new InvSalesServices();
                Common.SetAutoComplete(txtDocumentNo, invSalesServices.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

                //Load Invoice Numbers
                InvSalesServices invInvoiceServices = new InvSalesServices();
                int invoiceDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmInvoice").DocumentID;
                Common.SetAutoComplete(txtInvoiceNo, invInvoiceServices.GetAllDocumentNumbersToInvoice(invoiceDocumentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationInvoiceNo.Checked);
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
                // Disable product details controls
                tbBody.Enabled = false;
                grpFooter.Enabled = false;
                EnableLine(false);
                Common.EnableTextBox(true, txtCustomerCode, txtCustomerName, txtDocumentNo, txtInvoiceNo);
                Common.EnableButton(true, btnDocumentDetails, btnInvoiceDetails);
                Common.EnableComboBox(true, cmbLocation);
                Common.EnableButton(false, btnSave, btnPause);
                dtpExpiry.Value = DateTime.Now;

                invSalesDetailTempList = null;
                existingInvProductMaster = null;
                existingInvSalesDetailTemp = null;
                invProductSerialNoTempList = null;

                cmbLocation.SelectedValue = Common.LoggedLocationID;

                resetPayment();
                LoadTransactionEntryLedgers();

                selectedRowIndex = 0;
                isUpdateGrid = false;
                rowCount = 0;

                txtDocumentNo.Text = GetDocumentNo(true);
                this.ActiveControl = txtCustomerCode;
                txtCustomerCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadTransactionEntryLedgers()
        {
            AccTransactionTypeService accTransactionTypeDetailService = new AccTransactionTypeService();
            AccTransactionTypeDetail accTransactionTypeDetail = new AccTransactionTypeDetail();

            AccLedgerAccount accLedgerAccount = new AccLedgerAccount();
            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();

            accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionIDDefinition(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSalesReturnNote").DocumentID, 1);
            if (accTransactionTypeDetail != null)
            {
                string[] entryLedgerCodes = new string[] { };
                string[] entryLedgerNames = new string[] { };

                accLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByID(accTransactionTypeDetail.AccLedgerAccountID);
                entryLedgerCodes[1] = accLedgerAccount.LedgerCode;
                entryLedgerNames[1] = accLedgerAccount.LedgerName;

                Common.SetAutoComplete(txtCustomerCode, entryLedgerCodes, chkAutoCompleationCustomer.Checked);
                Common.SetAutoComplete(txtCustomerName, entryLedgerNames, chkAutoCompleationCustomer.Checked);
            }
        }

        private void EnableLine(bool enable)
        {
            Common.EnableTextBox(enable, txtQty, txtFreeQty, txtRate, txtProductDiscountAmount, txtProductDiscountPercentage, txtProductAmount, txtBatchNo);
            Common.EnableComboBox(enable, cmbUnit);
            dtpExpiry.Enabled = enable;
        }

        public void SetBatchNumber(string batchNo)
        {
            batchNumber = batchNo;
        }

        private void resetPayment()
        {
            txtCardChequeNo.Text = string.Empty;
            txtCardChequeNo.Enabled = false;
            dtpChequeDate.Value = Common.FormatDate(DateTime.Now);
            dtpChequeDate.Enabled = false;

            Common.EnableComboBox(false, cmbBankCode, cmbBankName);
            Common.ClearComboBox(cmbBankCode, cmbBankName, cmbPaymentMethod);

            txtPayingAmount.Text = "0.00";
            txtPayingAmount.Enabled = false;
        }

        private string GetDocumentNo(bool isTemporytNo)
        {
            try
            {
                InvSalesServices invSalesServices = new InvSalesServices();
                LocationService locationService = new LocationService();
                return invSalesServices.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }
        }

        private void LoadProducts()
        {
            try
            {
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                Common.SetAutoComplete(txtProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool RecallInvoices(string documentNo)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                InvSalesHeader invSalesHeader = new InvSalesHeader();
                InvSalesServices invSalesServices = new Service.InvSalesServices();

                invSalesHeader = invSalesServices.getSalesHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmInvoice").DocumentID, txtInvoiceNo.Text.Trim(), Common.LoggedLocationID);
                if (invSalesHeader != null)
                {
                    CustomerService customerService = new CustomerService();
                    Customer customer = new Customer();

                    customer = customerService.GetCustomersById(invSalesHeader.CustomerID);

                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    InvSalesPerson invSalesPerson = new InvSalesPerson();

                    invSalesPerson = invSalesPersonService.GetInvSalesPersonByID(invSalesHeader.SalesPersonID);

                    cmbLocation.SelectedValue = invSalesHeader.LocationID;
                    cmbLocation.Refresh();
                    cmbCostCentre.SelectedValue = invSalesHeader.CostCentreID;

                    //if (!invSalesHeader.DiscountPercentage.Equals(0))
                    //{
                    //    txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(invSalesHeader.DiscountPercentage);
                    //    chkSubTotalDiscountPercentage.Checked = true;
                    //}
                    //else
                    //{
                    //    txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(0);
                    //    chkSubTotalDiscountPercentage.Checked = false;
                    //}

                    //txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(invSalesHeader.DiscountAmount);
                    //txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(invSalesHeader.OtherChargers);

                    //?????????????????? check with net amt
                    txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(0);
                    chkSubTotalDiscountPercentage.Checked = false;

                    txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(0);
                    txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(0);

                    dtpReturnDate.Value = Common.FormatDate(DateTime.Now);

                    txtDocumentNo.Text = GetDocumentNo(true);
                    txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(invSalesHeader.GrossAmount);
                    txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(invSalesHeader.NetAmount);

                    txtInvoiceNo.Text = invSalesHeader.DocumentNo;

                    txtReferenceNo.Text = string.Empty;
                    txtRemark.Text = invSalesHeader.Remark;
                    customer = customerService.GetCustomersById(invSalesHeader.CustomerID);
                    txtCustomerCode.Text = customer.CustomerCode;
                    txtCustomerName.Text = customer.CustomerName;

                    txtSalesPersonCode.Text = invSalesPerson.SalesPersonCode;
                    txtSalesPersonName.Text = invSalesPerson.SalesPersonName;
                    
                    //txtSupplierInvoiceNo.Text = string.Empty;

                    if (!invSalesHeader.TaxAmount.Equals(0))
                    {
                        chkTaxEnable.Checked = true;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(invSalesHeader.TaxAmount);
                    }
                    else
                    {
                        chkTaxEnable.Checked = false;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    }

                    dgvItemDetails.DataSource = null;
                    invSalesDetailTempList = invSalesServices.getInvoiceDetail(invSalesHeader);
                    dgvItemDetails.DataSource = invSalesDetailTempList;
                    dgvItemDetails.Refresh();

                    dgvAdvanced.DataSource = null;
                    dgvAdvanced.Refresh();

                    dgvPaymentDetails.DataSource = null;
                    dgvPaymentDetails.Refresh();

                    Common.EnableTextBox(false, txtCustomerCode, txtCustomerName, txtSalesPersonCode, txtSalesPersonName, txtDocumentNo, txtInvoiceNo);
                    Common.EnableComboBox(false, cmbLocation);

                    tbBody.Enabled = true;
                    grpFooter.Enabled = true;
                    AssignTempPaymentMode();
                    EnableLine(true);
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    Common.EnableButton(false, btnDocumentDetails, btnInvoiceDetails);
                    this.ActiveControl = txtProductCode;
                    txtProductCode.Focus();

                    dtpExpiry.Value = DateTime.Now;

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
        //            existingInvProductMaster = InvProductMasterService.GetProductsByRefCodes(strProduct);
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
        //                invSalesDetailTempList = new List<InvSalesDetailTemp>();
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
        private void CalculateLine(decimal qty = 0)
        {
            try
            {
                if (qty == 0)
                {
                    if (Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()) > 0)
                        txtProductDiscountAmount.Text = (Common.GetDiscountAmount(true, (Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim())) * Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()), Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()))).ToString();
                    else
                        txtProductDiscountAmount.Text = Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim()).ToString();

                    txtProductAmount.Text = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim())).ToString();
                }
                else
                {
                    if (Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()) > 0)
                        txtProductDiscountAmount.Text = (Common.GetDiscountAmount(true, (Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim())) * Common.ConvertDecimalToDecimalQty(qty), Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()))).ToString();
                    else
                        txtProductDiscountAmount.Text = Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim()).ToString();

                    txtProductAmount.Text = (Common.ConvertDecimalToDecimalQty(qty) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim())).ToString();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void CalculateLine()
        {
            try
            {
                if (Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()) > 0)
                {txtProductDiscountAmount.Text = (Common.GetDiscountAmount(true, (Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim())) * Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()), Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()))).ToString();}
                else
                {txtProductDiscountAmount.Text = Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim()).ToString();}

                txtProductAmount.Text = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim())).ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void GetPrintingDetails()
        {
            try
            {
                foreach (string printname in PrinterSettings.InstalledPrinters)
                {
                    cmbPrinter.Items.Add(printname);
                }
                
                PrinterSettings settings = new PrinterSettings();
                foreach (PaperSize size in settings.PaperSizes)
                {
                    cmbPaperSize.Items.Add(size);
                }

                rdoPortrait.Checked = true;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool RecallDocument(string documentNo)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                InvSalesServices invSalesServices = new InvSalesServices();
                InvSalesHeader invSalesHeader = new InvSalesHeader();

                invSalesHeader = invSalesServices.getSalesHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                if (invSalesHeader != null)
                {
                    CustomerService customerService = new CustomerService();
                    Customer customer = new Customer();

                    customer = customerService.GetCustomersByCode(txtCustomerCode.Text.Trim());

                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    InvSalesPerson invSalesPerson = new InvSalesPerson();

                    invSalesPerson = invSalesPersonService.GetInvSalesPersonByCode(txtSalesPersonCode.Text.Trim());

                    cmbLocation.SelectedValue = invSalesHeader.LocationID;
                    //cmbLocation.Refresh();
                    cmbCostCentre.SelectedValue = invSalesHeader.CostCentreID;

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
                    txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(invSalesHeader.OtherChargers);

                    dtpReturnDate.Value = Common.FormatDate(invSalesHeader.DocumentDate);

                    txtDocumentNo.Text = invSalesHeader.DocumentNo;
                    txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(invSalesHeader.GrossAmount);
                    txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(invSalesHeader.NetAmount);

                    if (!invSalesHeader.ReferenceDocumentID.Equals(0))
                    {
                        if (invSalesHeader.RecallDocumentStatus.Equals(3))
                        {
                            InvSalesServices invInvoiceServices = new InvSalesServices();
                            txtInvoiceNo.Text = invInvoiceServices.GetSavedSalesHeaderByDocumentID(invSalesHeader.ReferenceDocumentDocumentID, invSalesHeader.ReferenceDocumentID).DocumentNo;
                        }
                    }
                    else
                    {
                        txtInvoiceNo.Text = string.Empty;
                    }

                    txtReferenceNo.Text = invSalesHeader.ReferenceNo;
                    txtRemark.Text = invSalesHeader.Remark;
                    customer = customerService.GetCustomersById(invSalesHeader.CustomerID);
                    txtCustomerCode.Text = customer.CustomerCode;
                    txtCustomerName.Text = customer.CustomerName;

                    invSalesPerson = invSalesPersonService.GetInvSalesPersonByID(invSalesHeader.SalesPersonID);
                    txtSalesPersonCode.Text = invSalesPerson.SalesPersonCode;
                    txtSalesPersonName.Text = invSalesPerson.SalesPersonName;

                    if (!invSalesHeader.TaxAmount.Equals(0))
                    {
                        chkTaxEnable.Checked = true;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(invSalesHeader.TaxAmount);
                    }
                    else
                    {
                        chkTaxEnable.Checked = false;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    }

                    dgvItemDetails.DataSource = null;
                    invSalesDetailTempList = invSalesServices.GetPausedSalesReturnDetail(invSalesHeader);
                    dgvItemDetails.DataSource = invSalesDetailTempList;
                    dgvItemDetails.Refresh();

                    dgvAdvanced.DataSource = null;
                    otherExpenceTempList = invSalesServices.getPausedExpence(invSalesHeader);
                    dgvAdvanced.DataSource = otherExpenceTempList;
                    dgvAdvanced.Refresh();

                    dgvPaymentDetails.DataSource = null;
                    paymentTempList = invSalesServices.getPausedPayment(invSalesHeader);
                    dgvPaymentDetails.DataSource = paymentTempList;
                    dgvPaymentDetails.Refresh();

                    invProductSerialNoTempList = invSalesServices.getPausedSalesSerialNoDetail(invSalesHeader);

                    Common.EnableTextBox(false, txtCustomerCode, txtCustomerName, txtSalesPersonCode, txtSalesPersonName, txtDocumentNo, txtInvoiceNo);
                    Common.EnableComboBox(false, cmbLocation);

                    if (invSalesHeader.DocumentStatus.Equals(0))
                    {
                        tbBody.Enabled = true;
                        grpFooter.Enabled = true;
                        AssignTempPaymentMode();
                        EnableLine(true);
                        if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        Common.EnableButton(false, btnDocumentDetails, btnInvoiceDetails);
                        this.ActiveControl = txtProductCode;
                        txtProductCode.Focus();
                    }
                    else
                    {
                        Common.EnableTextBox(false, txtRemark, txtReferenceNo);
                        Common.EnableComboBox(false, cmbReturnType, cmbCostCentre);
                        dtpReturnDate.Enabled = false;
                    }

                    dtpExpiry.Value = DateTime.Now;

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
            dgvPaymentDetails.DataSource = null;
            dgvPaymentDetails.Refresh();
            dgvAdvanced.DataSource = null;
            dgvAdvanced.Refresh();
            tbBody.SelectedTab = tbpGeneral;
            base.ClearForm();
        }

        private void UpdateGrid(InvSalesDetailTemp invSalesDetailTemp)
        {
            try
            {
                decimal qty = 0;
                decimal freeQty = 0;

                if (((Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) + Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim())) > 0) && (Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()) > 0))
                {

                    if (existingInvProductMaster.IsExpiry.Equals(true))
                    {invSalesDetailTemp.ExpiryDate = Common.ConvertStringToDate(dtpExpiry.Value.ToString());}
                    invSalesDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                    invSalesDetailTemp.BaseUnitID = existingInvProductMaster.UnitOfMeasureID;
                    invSalesDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();

                    //if (chkOverwrite.Checked)
                    //{
                    //    if (Toast.Show("", Toast.messageType.Question, Toast.messageAction.OverwriteQty).Equals(DialogResult.Yes)) { }
                    //    else { return; }
                    //}
                    //else
                    //{
                    //    txtQty.Text = (invSalesDetailTemp.Qty + Common.ConvertStringToDecimalQty(txtQty.Text)).ToString();
                    //}

                    if (chkOverwrite.Checked)
                    {
                        string productCode = txtProductCode.Text.Trim();
                        string unit = cmbUnit.Text.Trim();

                        if (dgvItemDetails["ProductCode", 0].Value == null) { }
                        else
                        {
                            for (int i = 0; i < dgvItemDetails.RowCount; i++)
                            {
                                if (productCode.Equals(dgvItemDetails["ProductCode", i].Value.ToString()) && unit.Equals(dgvItemDetails["Unit", i].Value.ToString()))
                                {
                                    if (Toast.Show("", Toast.messageType.Question, Toast.messageAction.OverwriteQty).Equals(DialogResult.Yes)) { }
                                    else { return; }
                                }
                            }
                        }
                    }
                    else
                    {
                        qty = (invSalesDetailTemp.Qty + Common.ConvertStringToDecimalQty(txtQty.Text));
                        freeQty = (invSalesDetailTemp.FreeQty + Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim()));
                    }

                    if (!chkOverwrite.Checked)
                    {
                        CalculateLine(qty);
                        invSalesDetailTemp.Qty = Common.ConvertDecimalToDecimalQty(qty);
                        invSalesDetailTemp.FreeQty = Common.ConvertDecimalToDecimalQty(freeQty);
                        invSalesDetailTemp.GrossAmount = (Common.ConvertDecimalToDecimalQty(qty) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()));
                    }
                    else
                    {
                        CalculateLine();
                        invSalesDetailTemp.Qty = Common.ConvertStringToDecimalQty(txtQty.Text);
                        invSalesDetailTemp.FreeQty = Common.ConvertStringToDecimalQty(txtFreeQty.Text);
                        invSalesDetailTemp.GrossAmount = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()));
                    }

                    invSalesDetailTemp.BatchNo = txtBatchNo.Text.Trim();

                    invSalesDetailTemp.Rate = Common.ConvertStringToDecimalCurrency(txtRate.Text);                
                    invSalesDetailTemp.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim());
                    invSalesDetailTemp.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text);
                    invSalesDetailTemp.NetAmount = Common.ConvertStringToDecimalCurrency(txtProductAmount.Text);

                    InvSalesServices invSalesServices = new Service.InvSalesServices();

                    dgvItemDetails.DataSource = null;
                    invSalesDetailTempList = invSalesServices.getUpdateSalesDetailTemp(invSalesDetailTempList, invSalesDetailTemp, existingInvProductMaster);
                    dgvItemDetails.DataSource = invSalesDetailTempList;
                    dgvItemDetails.Refresh();

                    foreach (DataGridViewRow row in dgvItemDetails.Rows)
                    {
                        if (string.Equals(txtProductCode.Text.Trim(), dgvItemDetails["ProductCode", row.Index].Value.ToString()))
                        {
                            isUpdateGrid = true;
                            selectedRowIndex = row.Index;
                            break;
                        }
                    }

                    if (isUpdateGrid)
                    {
                        dgvItemDetails.CurrentCell = dgvItemDetails.Rows[selectedRowIndex].Cells[0];
                        isUpdateGrid = false;
                    }
                    else
                    {
                        rowCount = dgvItemDetails.Rows.Count;
                        dgvItemDetails.CurrentCell = dgvItemDetails.Rows[rowCount - 1].Cells[0];
                    }

                    GetSummarizeFigures(invSalesDetailTempList);
                    EnableLine(false);
                    Common.EnableTextBox(false, txtCustomerCode, txtCustomerName, txtSalesPersonCode, txtSalesPersonName, txtDocumentNo, txtInvoiceNo);
                    Common.EnableComboBox(false, cmbLocation);
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    Common.EnableButton(false, btnDocumentDetails, btnInvoiceDetails);
                    ClearLine();
                    if (invSalesDetailTempList.Count > 0)
                    {
                        grpFooter.Enabled = true;
                        this.AssignTempPaymentMode();
                    }

                    txtProductCode.Enabled = true;
                    txtProductCode.Focus();
                }
                else
                {
                    Toast.Show("Operation", Toast.messageType.Information, Toast.messageAction.Invalid);
                    txtProductCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void AssignTempPaymentMode()
        {
            cmbPaymentMethod.SelectedIndex = 0;
            cmbPaymentMethod.Enabled = false;

            SetPaymentMethodProperties();
            //txtPayingAmount.Text = txtNetAmount.Text;
        }

        private void SetPaymentMethodProperties()
        {
            PaymentMethodService paymentMethodService = new PaymentMethodService();

            paymentType = paymentMethodService.GetPaymentTypeByID(Common.ConvertStringToInt(cmbPaymentMethod.SelectedValue.ToString()));

            dtpChequeDate.Enabled = false;
            Common.EnableTextBox(false, txtCardChequeNo);
            Common.EnableComboBox(false, cmbBankCode, cmbBankName, cmbBranchCode, cmbBranchName);

            // cmbBankCode.SelectedIndex = -1;

            Common.ClearTextBox(txtCardChequeNo);
            Common.ClearComboBox(cmbBankCode, cmbBankName, cmbBranchCode, cmbBranchName);

            if (paymentType.Equals(0)) 
            {
                Common.EnableTextBox(true, txtPayingAmount);
                txtPayingAmount.Focus();
                return;
            }
            else if (paymentType.Equals(1))
            {
                Common.EnableTextBox(true, txtPayingAmount);
                txtPayingAmount.Focus();
                return;
            }
            else if (paymentType.Equals(2))
            {
                Common.EnableTextBox(true, txtPayingAmount, txtCardChequeNo);
                Common.EnableComboBox(true, cmbBankCode, cmbBankName, cmbBranchCode, cmbBranchName);

                dtpChequeDate.Enabled = true;
                txtCardChequeNo.Focus();
                return;
            }
            else if (paymentType.Equals(3))
            {
                Common.EnableTextBox(true, txtPayingAmount, txtCardChequeNo);
                Common.EnableComboBox(true, cmbBankCode, cmbBankName);
                txtCardChequeNo.Focus();
                return;
            }
        }

        private void GetSummarizeFigures(List<InvSalesDetailTemp> listItem)
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
            decimal grossAmount = listItem.GetSummaryAmount(x => x.NetAmount);

            grossAmount = Common.ConvertStringToDecimalCurrency(grossAmount.ToString());

            decimal otherChargersAmount = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.ToString());

            //Get Discount Amount
            bool isSubDiscount = chkSubTotalDiscountPercentage.Checked;

            decimal subDiscount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());
            decimal discountAmount = Common.ConvertStringToDecimalCurrency(Common.GetDiscountAmount(isSubDiscount, grossAmount, subDiscount).ToString());

            //Read from Tax
            decimal taxAmount = 0;
            if (chkTaxEnable.Checked)
            {
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(1, grossAmount, customerService.GetCustomersByCode(txtCustomerCode.Text.ToString()).CustomerID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
            }

            //Get Net Amount
            decimal netAmount = Common.ConvertStringToDecimalCurrency((Common.GetTotalAmount(grossAmount, otherChargersAmount, taxAmount) - Common.GetTotalAmount(discountAmount)).ToString());

            //Assign calculated values
            txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(grossAmount);
            txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(otherChargersAmount);
            txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(discountAmount);
            txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(taxAmount);
            txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(netAmount);
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode, txtProductName, txtBatchNo, txtQty, txtFreeQty, txtRate, txtProductDiscountAmount, txtProductDiscountPercentage, txtProductAmount);
            Common.ClearComboBox(cmbUnit);
            dtpExpiry.Value = DateTime.Now;
            txtProductCode.Focus();
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

            decimal otherChargersAmount = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.ToString());

            //Get Discount Amount
            bool isSubDiscount = chkSubTotalDiscountPercentage.Checked;

            decimal subDiscount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());
            decimal discountAmount = Common.ConvertStringToDecimalCurrency(Common.GetDiscountAmount(isSubDiscount, grossAmount, subDiscount).ToString());

            //Read from Tax
            decimal taxAmount = 0;
            if (chkTaxEnable.Checked)
            {
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(1, grossAmount, customerService.GetCustomersByCode(txtCustomerCode.Text.ToString()).CustomerID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
            }

            //Get Net Amount
            decimal netAmount = Common.ConvertStringToDecimalCurrency((Common.GetTotalAmount(grossAmount, otherChargersAmount, taxAmount) - Common.GetTotalAmount(discountAmount)).ToString());

            //Assign calculated values
            txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(grossAmount);
            txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(otherChargersAmount);
            txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(discountAmount);
            txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(taxAmount);
            txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(netAmount);
        }

        public override void Save()
        {
            if ((Toast.Show("Sales Return Note  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                this.Cursor = Cursors.WaitCursor;
                string NewDocumentNo;
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                this.Cursor = Cursors.Default;
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Sales Return Note  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 1);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Sales Return Note  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        public override void Pause()
        {
            if ((Toast.Show("Sales Return Note  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                this.Cursor = Cursors.WaitCursor;
                string NewDocumentNo;
                bool saveDocument = SaveDocument(0, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                this.Cursor = Cursors.Default;
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Sales Return Note  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 0);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Sales Return Note  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        private bool SaveDocument(int documentStatus, string documentNo, out string newDocumentNo)
        {
            try
            {
                GetSummarizeSubFigures();

                InvSalesServices invSalesServices = new InvSalesServices();
                InvSalesHeader invSalesHeader = new InvSalesHeader();
                LocationService locationService = new LocationService();
                InvReturnTypeService invReturnTypeService = new InvReturnTypeService();
                Location Location = new Location();
                CustomerService customerService = new CustomerService();
                Customer customer = new Customer();

                customer = customerService.GetCustomersByCode(txtCustomerCode.Text.Trim());

                InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                InvSalesPerson invSalesPerson = new InvSalesPerson();

                invSalesPerson = invSalesPersonService.GetInvSalesPersonByCode(txtSalesPersonCode.Text.Trim());

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                invSalesHeader = invSalesServices.getSalesHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Location.LocationID);
                if (invSalesHeader == null)
                {invSalesHeader = new InvSalesHeader();}

                invSalesHeader.SalesHeaderID = invSalesHeader.InvSalesHeaderID;
                invSalesHeader.CompanyID = Location.CompanyID;
                invSalesHeader.CostCentreID = Common.ConvertStringToInt(cmbCostCentre.SelectedValue.ToString());
                invSalesHeader.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscount.Text.ToString());
                if (chkSubTotalDiscountPercentage.Checked)
                {invSalesHeader.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());}
                invSalesHeader.OtherChargers = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.ToString());
                invSalesHeader.DocumentDate = Common.ConvertStringToDate(dtpReturnDate.Value.ToString());
                invSalesHeader.DocumentID = documentID;
                invSalesHeader.DocumentStatus = documentStatus;
                invSalesHeader.DocumentNo = documentNo.Trim();
                invSalesHeader.GrossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.ToString());
                invSalesHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                invSalesHeader.LineDiscountTotal = GetLineDiscountTotal(invSalesDetailTempList);
                invSalesHeader.LocationID = Location.LocationID;
                invSalesHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtNetAmount.Text.ToString());
                invSalesHeader.InvReturnTypeID = invReturnTypeService.GetInvReturnTypesByID(Common.ConvertStringToInt(cmbReturnType.SelectedValue.ToString())).InvReturnTypeID;

                ////if (!txtQuotationNo.Text.Trim().Equals(string.Empty))
                ////{
                ////    InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                ////    InvPurchaseOrderHeader invPurchaseOrderHeader = new InvPurchaseOrderHeader();

                ////    invPurchaseOrderHeader = invPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());
                ////    invSalesHeader.ReferenceDocumentDocumentID = invPurchaseOrderHeader.DocumentID;
                ////    invSalesHeader.ReferenceDocumentID = invPurchaseOrderHeader.InvPurchaseOrderHeaderID;
                ////}

                invSalesHeader.ReferenceNo = txtReferenceNo.Text.Trim();
                invSalesHeader.Remark = txtRemark.Text.Trim();
                invSalesHeader.CustomerID = customer.CustomerID;
                invSalesHeader.SalesPersonID = invSalesPerson.InvSalesPersonID;
                invSalesHeader.StartTime = Common.FormatDateTime(DateTime.Now);
                invSalesHeader.EndTime = Common.FormatDateTime(DateTime.Now);

                if (chkTaxEnable.Checked)
                {
                    decimal tax1 = 0;
                    decimal tax2 = 0;
                    decimal tax3 = 0;
                    decimal tax4 = 0;
                    decimal tax5 = 0;

                    CommonService commonService = new CommonService();
                    invSalesHeader.TaxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(1, invSalesHeader.GrossAmount, customer.CustomerID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());

                    invSalesHeader.TaxAmount1 = tax1;
                    invSalesHeader.TaxAmount2 = tax2;
                    invSalesHeader.TaxAmount3 = tax3;
                    invSalesHeader.TaxAmount4 = tax4;
                    invSalesHeader.TaxAmount5 = tax5;
                }

                if (invSalesDetailTempList == null)
                {invSalesDetailTempList = new List<InvSalesDetailTemp>();}

                if (invProductSerialNoTempList == null)
                {invProductSerialNoTempList = new List<InvProductSerialNoTemp>();}

                if (otherExpenceTempList == null)
                {otherExpenceTempList = new List<OtherExpenseTemp>();}

                if (paymentTempList == null)
                {paymentTempList = new List<PaymentTemp>();}

                return invSalesServices.SaveSalesReturn(invSalesHeader, invSalesDetailTempList, invProductSerialNoTempList, out newDocumentNo, otherExpenceTempList, paymentTempList, Common.ConvertStringToDecimalCurrency(txtPaidAmount.Text.Trim()), this.Name);
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
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            invReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
        }

        private decimal GetLineDiscountTotal(List<InvSalesDetailTemp> listItem)
        {
            return listItem.GetSummaryAmount(x => x.DiscountAmount);
        }

        private void LoadCustomer(bool isCode, string strCustomer)
        {
            try
            {
                CustomerService customerService = new CustomerService();
                Customer existingCustomer = new Customer();

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
                    txtCreditLimit.Text = (existingCustomer.CreditLimit + existingCustomer.TemporaryLimit).ToString();
                    txtPaymentTerms.Text = (paymentMethodService.GetPaymentMethodsByID(existingCustomer.PaymentMethodID)).PaymentMethodName;
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
        #endregion

        #region Form Events

        private void btnTaxBreakdown_Click(object sender, EventArgs e)
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

        private void FrmSalesReturnNote_Load(object sender, EventArgs e)
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

        private void txtCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {txtCustomerName.Focus();}

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

        private void txtSalesPersonName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                { txtRemark.Focus(); }

                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    DataView dvAllReferenceData = new DataView(invSalesPersonService.GetAllActiveSalesPersonsDataTable());
                    if (dvAllReferenceData.Count > 0)
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

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {txtReferenceNo.Focus();}
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
                {cmbReturnType.Focus();}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbReturnType_KeyDown(object sender, KeyEventArgs e)
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

        private void cmbLocation_Validated(object sender, EventArgs e)
        {
            try
            {
                LocationService locationService = new LocationService();
                if (cmbLocation.SelectedValue == null || locationService.IsExistsLocation(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim())).Equals(false))
                {
                    Toast.Show("Location - " + cmbLocation.Text.Trim().Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    cmbLocation.Focus();
                    return;
                }
                else
                {
                    accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbLocation.Text.Trim()).LocationID, documentID);
                    if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }

                    cmbCostCentre.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbCostCentre_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    tbBody.Enabled = true;
                    Common.EnableTextBox(true, txtProductCode, txtProductName);

                    LoadProducts();
                    txtProductCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtProductCode.Text.Trim().Equals(string.Empty))
                    {
                        txtProductName.Enabled = true;
                        txtProductName.Focus();
                    }
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

        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {

                    if (!txtProductName.Text.Trim().Equals(string.Empty))
                    {
                        cmbUnit.Enabled = true;
                        cmbUnit.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

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

        private void cmbUnit_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbUnit.SelectedValue == null)
                {return;}

                if (!existingInvProductMaster.UnitOfMeasureID.Equals(Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())))
                {
                    InvProductUnitConversionService invProductUnitConversionService = new InvProductUnitConversionService();
                    if (invProductUnitConversionService.GetProductUnitByProductCode(existingInvProductMaster.InvProductMasterID, Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())) == null)
                    {
                        Toast.Show("Unit - " + cmbUnit.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "Product - " + txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "");
                        dtpExpiry.Enabled = false;
                        cmbUnit.SelectedValue = existingInvProductMaster.UnitOfMeasureID;
                        cmbUnit.Focus();
                        return;
                    }
                }
                //loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()), dtpExpiry.Value);
                if (existingInvProductMaster.IsExpiry.Equals(true))
                {
                    txtBatchNo.Enabled = true;
                    txtBatchNo.Focus();
                }
                else
                {
                    txtQty.Enabled = true;
                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                    { txtQty.Text = "1";}
                    txtQty.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtFreeQty.Enabled = true;
                    txtFreeQty.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            {
                CalculateLine();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
        
        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    cmbUnit_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtProductName.Text.Trim().Equals(string.Empty))
                    {
                        //InvProductBatchNoTemp invProductBatchNoTemp = new InvProductBatchNoTemp();
                        ////invProductBatchNoTemp.DocumentID = documentID;
                        //invProductBatchNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                        //invProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        //InvSalesServices invSalesServices = new InvSalesServices();
                        //invProductBatchNoTempList = invSalesServices.getBatchNoDetail(existingInvProductMaster);

                        //if (invProductSerialNoTempList == null)
                        //    invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                        //FrmBatchNumber frmBatchNumber = new FrmBatchNumber(invProductBatchNoTempList, invProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, FrmBatchNumber.transactionType.Invoice, existingInvProductMaster.InvProductMasterID);
                        //frmBatchNumber.ShowDialog();

                        //if (existingInvProductMaster.IsExpiry)
                        //{
                        //    dtpExpiry.Enabled = true;
                        //    dtpExpiry.Focus();
                        //}

                        InvProductBatchNoTemp invProductBatchNoTemp = new InvProductBatchNoTemp();
                        //invProductBatchNoTemp.DocumentID = documentID;
                        invProductBatchNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                        invProductBatchNoTemp.UnitOfMeasureID =
                            Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        LocationService locationService = new LocationService();

                        if (txtBatchNo.Text.Trim() != "")
                        {
                            InvProductBatchNoExpiaryDetail invProductBatchNo = new InvProductBatchNoExpiaryDetail();

                            CommonService commonService = new CommonService();
                            invProductBatchNo = commonService.CheckInvBatchNumber(txtBatchNo.Text.Trim(),
                                                                                  existingInvProductMaster
                                                                                      .InvProductMasterID,
                                                                                  locationService.GetLocationsByName(
                                                                                      cmbLocation.Text).LocationID,
                                                                                  existingInvProductMaster
                                                                                      .UnitOfMeasureID);

                            if (invProductBatchNo == null)
                            {
                                if (
                                    (Toast.Show("This Batch No", Toast.messageType.Error,
                                                Toast.messageAction.NotExists).Equals(DialogResult.OK)))
                                    return;
                            }
                            else
                            {
                                if (existingInvProductMaster.IsExpiry)
                                {
                                    dtpExpiry.Enabled = true;
                                    dtpExpiry.Value =
                                        Common.ConvertStringToDateTime(invProductBatchNo.ExpiryDate.ToString());
                                    dtpExpiry.Focus();

                                }
                                else
                                {
                                    txtQty.Enabled = true;
                                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                                        txtQty.Text = "1";
                                    txtQty.Focus();
                                    dtpExpiry.Enabled = true;
                                }
                                return;
                            }
                        }
                        else
                        {
                            InvSalesServices invSalesServices = new InvSalesServices();
                            invProductBatchNoTempList = invSalesServices.GetBatchNoDetail(existingInvProductMaster, locationService.GetLocationsByName(cmbLocation.Text).LocationID);

                            if (invProductSerialNoTempList == null)
                            {
                                invProductSerialNoTempList = new List<InvProductSerialNoTemp>();
                            }

                            FrmBatchNumber frmBatchNumber = new FrmBatchNumber(invProductBatchNoTempList, invProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, FrmBatchNumber.transactionType.SalesReturn, existingInvProductMaster.InvProductMasterID);
                            frmBatchNumber.ShowDialog();

                            txtBatchNo.Text = batchNumber;

                            InvProductBatchNoExpiaryDetail invProductBatchNo = new InvProductBatchNoExpiaryDetail();
                            CommonService commonService = new CommonService();

                            invProductBatchNo = commonService.CheckInvBatchNumber(txtBatchNo.Text.Trim(), existingInvProductMaster.InvProductMasterID, locationService.GetLocationsByName(cmbLocation.Text).LocationID, existingInvProductMaster.UnitOfMeasureID);

                            if (invProductBatchNo == null)
                            {
                                if ((Toast.Show("This Batch No", Toast.messageType.Error, Toast.messageAction.NotExists).Equals(DialogResult.OK)))
                                { return; }
                            }
                            else
                            {
                                if (existingInvProductMaster.IsExpiry)
                                {
                                    dtpExpiry.Enabled = true;
                                    dtpExpiry.Value = Common.ConvertStringToDateTime(invProductBatchNo.ExpiryDate.ToString());
                                    dtpExpiry.Focus();
                                }
                                else
                                {
                                    txtQty.Enabled = true;
                                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                                    {
                                        txtQty.Text = "1";
                                    }
                                    txtQty.Focus();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtProductName.Text.Trim().Equals(string.Empty))
                    {
                        InvProductBatchNoTemp invProductBatchNoTemp = new InvProductBatchNoTemp();
                        //invProductBatchNoTemp.DocumentID = documentID;
                        invProductBatchNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                        invProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        InvSalesServices invSalesServices = new InvSalesServices();
                        invProductBatchNoTempList = invSalesServices.getExpiryDetail(existingInvProductMaster);

                        if (invProductSerialNoTempList == null)
                            invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                        //FrmExpiry frmExpiry = new FrmExpiry(invProductBatchNoTempList, invProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, FrmExpiry.transactionType.SalesReturnNote);
                        //frmExpiry.ShowDialog();
                        
                        txtQty.Enabled = true;
                        if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                        {txtQty.Text = "1";}
                        txtQty.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtFreeQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {

                    if ((Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim())) > 0)
                    {
                        if (existingInvProductMaster.IsSerial)
                        {
                            InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();
                            invProductSerialNoTemp.DocumentID = documentID;
                            if (existingInvProductMaster.IsExpiry.Equals(true))
                            {invProductSerialNoTemp.ExpiryDate = Common.ConvertStringToDate(dtpExpiry.Value.ToString());}
                            invProductSerialNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                            invProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                            if (invProductSerialNoTempList == null)
                            {invProductSerialNoTempList = new List<InvProductSerialNoTemp>();}

                            InvPurchaseService invPurchaseServices = new InvPurchaseService();

                            if (invPurchaseServices.IsValidNoOfSerialNo(invProductSerialNoTempList, invProductSerialNoTemp, (Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim()))))
                            {
                                txtRate.Enabled = true;
                                txtRate.Focus();
                            }
                            else
                            {
                                //FrmSerial frmSerial = new FrmSerial(invProductSerialNoTempList, invProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()),FrmSerial.transactionType.LogisticGoodReceivedNote);
                                //frmSerial.ShowDialog();

                                txtRate.Enabled = true;
                                txtRate.Focus();
                            }
                        }
                        else
                        {
                            txtRate.Enabled = true;
                            txtRate.Focus();
                        }
                    }
                }
                else if (e.KeyCode.Equals(Keys.F3))
                {
                    if (existingInvProductMaster.IsSerial)
                    {
                        InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();
                        invProductSerialNoTemp.DocumentID = documentID;
                        if (existingInvProductMaster.IsExpiry.Equals(true))
                        {invProductSerialNoTemp.ExpiryDate = Common.ConvertStringToDate(dtpExpiry.Value.ToString());}
                        invProductSerialNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                        invProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        if (invProductSerialNoTempList == null)
                        {invProductSerialNoTempList = new List<InvProductSerialNoTemp>();}

                        FrmSerial frmSerial = new FrmSerial(invProductSerialNoTempList, invProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()),FrmSerial.transactionType.LogisticGoodReceivedNote);
                        frmSerial.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtProductDiscountPercentage.Enabled = true;
                    txtProductDiscountPercentage.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductDiscountPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtProductDiscountAmount.Enabled = true;
                    txtProductDiscountAmount.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductDiscountPercentage_Leave(object sender, EventArgs e)
        {
            try
            {
                CalculateLine();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductDiscountAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtProductAmount.Enabled = true;
                    txtProductAmount.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductDiscountAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                CalculateLine();
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

        private void txtSalesPersonCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                { txtSalesPersonName.Focus(); }

                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    DataView dvAllReferenceData = new DataView(invSalesPersonService.GetAllActiveSalesPersonsDataTable());
                    if (dvAllReferenceData.Count > 0)
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

        private void chkAutoCompleationDocumentNo_CheckedChanged(object sender, EventArgs e)
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

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                    {RecallDocument(txtDocumentNo.Text.Trim());}
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

        

        private void txtProductAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {UpdateGrid(existingInvSalesDetailTemp);}
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
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    SetPaymentMethodProperties();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCardChequeNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab)) && cmbPaymentMethod.Text.Trim() != string.Empty)
                {
                    if (paymentType.Equals(0))
                    {
                        txtPayingAmount.Focus();
                        return;
                    }
                    else if (paymentType.Equals(1))
                    {
                        txtPayingAmount.Enabled = true;
                        txtPayingAmount.Focus();
                        return;
                    }
                    else if (paymentType.Equals(2))
                    {
                        txtPayingAmount.Enabled = true;
                        dtpChequeDate.Enabled = true;
                        dtpChequeDate.Focus();
                        return;
                    }
                    else if (paymentType.Equals(3))
                    {
                        txtPayingAmount.Enabled = true;
                        cmbBankCode.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpChequeDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab)) && cmbPaymentMethod.Text.Trim() != string.Empty)
                {
                    cmbBankCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbBankCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {

                    if (!cmbBankCode.Text.Trim().Equals(string.Empty))
                    {
                        cmbBranchCode.Enabled = true;
                        cmbBranchCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbBankName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!cmbBankCode.Text.Trim().Equals(string.Empty))
                    {
                        cmbBranchCode.Enabled = true;
                        cmbBranchCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbBranchCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {

                    if (!cmbBranchCode.Text.Trim().Equals(string.Empty))
                    {
                        txtPayingAmount.Enabled = true;
                        txtPayingAmount.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbBranchName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {

                    if (!cmbBranchCode.Text.Trim().Equals(string.Empty))
                    {
                        txtPayingAmount.Enabled = true;
                        txtPayingAmount.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPayingAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    try
                    {
                        PaymentTemp paymentTemp = new PaymentTemp();
                        paymentTemp.AccLedgerAccountID = Common.ConvertStringToLong(cmbPaymentMethod.SelectedValue.ToString());
                        paymentTemp.PayAmount = Common.ConvertStringToDecimal(txtPayingAmount.Text.Trim());
                        paymentTemp.CardCheqNo = txtCardChequeNo.Text.Trim();
                        if (paymentType.Equals(2))
                        {paymentTemp.ChequeDate = dtpChequeDate.Value;}
                        else
                        {paymentTemp.ChequeDate = null;}
                        paymentTemp.PaymentMethod = cmbPaymentMethod.Text.Trim();
                        paymentTemp.PaymentMethodID = paymentType; ;

                        paymentTemp.BankCode = cmbBankCode.Text.ToString();
                        paymentTemp.BankName = cmbBankName.Text.ToString();

                        BankService bankService = new BankService();
                        Bank bank = new Bank();
                        bank = bankService.GetBankByCode(paymentTemp.BankCode);

                        if (bank != null)
                        {paymentTemp.BankID = bank.BankID;}
                        else
                        {paymentTemp.BankID = 0;}

                        if (paymentTempList == null)
                        {paymentTempList = new List<PaymentTemp>();}


                        if (Common.ConvertStringToDecimalCurrency(txtNetAmount.Text.Trim()) < ((Common.ConvertStringToDecimalCurrency(txtPayingAmount.Text.Trim())) + (Common.ConvertStringToDecimalCurrency(txtPaidAmount.Text.Trim()))))
                        {
                            if (Toast.Show("You are trying to enter over payment\nDo you want to continue?", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.No))
                            {return;}

                        }

                        PaymentTemp paymentTempRemove = new PaymentTemp();

                        paymentTempRemove = paymentTempList.Where(p => p.CardCheqNo.Equals(txtCardChequeNo.Text.Trim()) && p.PaymentMethodID.Equals(paymentType)).FirstOrDefault();


                        if (paymentTempRemove != null)
                        {paymentTempList.Remove(paymentTempRemove);}
                        paymentTempList.Add(paymentTemp);
                        dgvPaymentDetails.DataSource = null;
                        dgvPaymentDetails.DataSource = paymentTempList;
                        dgvPaymentDetails.Refresh();

                        txtPaidAmount.Text = Common.ConvertDecimalToStringCurrency(paymentTempList.Sum(p => p.PayAmount));

                        cmbPaymentMethod.SelectedValue = 0;
                        resetPayment();
                        cmbPaymentMethod.Focus();
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtInvoiceNo.Text.Trim().Equals(string.Empty))
                    { RecallInvoices(txtInvoiceNo.Text.Trim());}
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationInvoiceNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvSalesServices invSalesServices = new InvSalesServices();
                Common.SetAutoComplete(txtInvoiceNo, invSalesServices.GetAllDocumentNumbers(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmInvoice").DocumentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
        #endregion

        private void txtSubTotalDiscountPercentage_KeyUp(object sender, KeyEventArgs e)
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
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvItemDetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                isUpdateGrid = true;
                if (dgvItemDetails.CurrentCell != null && dgvItemDetails.CurrentCell.RowIndex >= 0)
                {
                    selectedRowIndex = dgvItemDetails.CurrentCell.RowIndex;
                    UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                    //loadProductDetails(true, dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim(), unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID, Common.ConvertStringToDate((dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value == null ? DateTime.Now.ToString() : dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value.ToString())));
                    CalculateLine();
                    txtQty.Enabled = true;
                    this.ActiveControl = txtQty;
                    txtQty.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
    }
}
