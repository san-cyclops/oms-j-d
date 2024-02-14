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
using Utility;
using Service;
using System.Threading;

using System.Drawing.Printing;

namespace UI.Windows
{
    public partial class FrmDispatchNote : UI.Windows.FrmBaseTransactionForm
    {

        /// <summary>
        /// Dispatch Note
        /// Design By - C.S.Malluwawadu
        /// Developed By - C.S.Malluwawadu
        /// Date - 30/08/2013
        /// </summary>
        /// 

        static string batchNumber;
        int documentID = 6;
        int invoiceID = 0;
        bool isInvProduct;
        decimal convertFactor = 1;
        decimal sellingPrice = 0;
        int documentState;
        private int recallStatus = 0; // 0 - dispatch, 1 - Invoice, 2 - Sales Order

        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<InvSalesDetailTemp> invSalesDetailTempList = new List<InvSalesDetailTemp>();
        InvSalesDetailTemp existingInvSalesDetailTemp = new InvSalesDetailTemp();

        InvProductMaster existingInvProductMaster;
        List<InvProductSerialNoTemp> invProductSerialNoTempList;
        List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();
        List<InvProductExpiaryTemp> invProductExpiaryTempList = new List<InvProductExpiaryTemp>();

        public FrmDispatchNote()
        {
            InitializeComponent();
        }

        public override void FormLoad()
        {
            try
            {
                dgvItemDetails.AutoGenerateColumns = false;

               
                //Load cost center name to combo
                CostCentreService costCentreService = new CostCentreService();
                List<CostCentre> costCentres = new List<CostCentre>();
                LocationService locationService = new LocationService();
                cmbCostCentre.DataSource = costCentreService.GetAllCostCentres();
                cmbCostCentre.DisplayMember = "CostCentreName";
                cmbCostCentre.ValueMember = "CostCentreID";
                cmbCostCentre.Refresh();
                cmbCostCentre.SelectedValue = locationService.GetLocationsByID(Common.LoggedLocationID).CostCentreID;

                isInvProduct = true;
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                this.Text = autoGenerateInfo.FormText;

                documentID = autoGenerateInfo.DocumentID;

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }


                base.FormLoad();

                Common.ExchangeRate = autoGenerateInfo.ExchangeRate;

                txtExchangeRate.Text = Common.ConvertDecimalToStringCurrency(autoGenerateInfo.ExchangeRate);

                RefreshDocumentNumber();
                txtExchangeRate.Enabled = false;
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
                grpBody.Enabled = false;

                Common.EnableTextBox(true, txtReferenceNo, txtDocumentNo, txtInvoiceNo, txtSalesOrderNo);
                Common.EnableButton(true, btnDocumentDetails, btnInvoiceDetails, btnSalesOrderDetails);
                Common.EnableComboBox(true, cmbCostCentre);
                Common.EnableButton(false, btnSave, btnPause);

                AutoGenerateInfo autoGenerateInvoiceInfo = new AutoGenerateInfo();
                autoGenerateInvoiceInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmInvoice");

                if (autoGenerateInvoiceInfo.IsDispatchRecall)
                {
                    txtInvoiceNo.Enabled = false;
                    chkAutoCompleationInvoiceNo.Enabled = false;
                    btnInvoiceDetails.Enabled = false;
                }
                else
                {
                    txtInvoiceNo.Enabled = true;
                    chkAutoCompleationInvoiceNo.Enabled = true;
                    btnInvoiceDetails.Enabled = true;
                }

               // if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                Common.EnableButton(false, btnPause);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

                //invSalesDetailTempList = null;
                existingInvProductMaster = null;
                //existingInvSalesDetailTemp = null;
                //invProductSerialNoTempList = null;

                sellingPrice = 0;
                recallStatus = 0;

                txtDocumentNo.Text = GetDocumentNo(true);
                this.ActiveControl = txtInvoiceNo;
                txtInvoiceNo.Focus();
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
                InvSalesServices invDispatchServices = new InvSalesServices();
                Common.SetAutoComplete(txtDocumentNo, invDispatchServices.GetAllDocumentNumbers(documentID, Common.LoggedLocationID), chkAutoCompleationDocumentNo.Checked);

                //Load Invoice Numbers
                InvSalesServices invSalesServices = new InvSalesServices();
                int invoicehDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmInvoice").DocumentID;
                Common.SetAutoComplete(txtInvoiceNo, invSalesServices.GetAllDocumentNumbersToDispatch(invoicehDocumentID, Common.LoggedLocationID), chkAutoCompleationInvoiceNo.Checked);

                //Load Sales Order Numbers
                SalesOrderService salesOrderService = new SalesOrderService();
                int salesOrderhDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSalesOrder").DocumentID;
                Common.SetAutoComplete(txtSalesOrderNo, salesOrderService.GetAllDocumentNumbersToDispatch(salesOrderhDocumentID, Common.LoggedLocationID), chkAutoCompleationSalesOrderNo.Checked);
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
                return invSalesServices.GetDocumentNo(this.Name, Common.LoggedLocationID, locationService.GetLocationsByID(Common.LoggedLocationID).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }

        }

        private bool RecallDocument(string documentNo)
        {
            try
            {
                InvSalesServices invSalesServices = new InvSalesServices();
                InvSalesHeader invSalesHeader = new InvSalesHeader();

                invSalesHeader = invSalesServices.GetPausedDispatchHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.LoggedLocationID);
                if (invSalesHeader != null)
                {
                    documentState = invSalesHeader.DocumentStatus;

                    cmbCostCentre.SelectedValue = invSalesHeader.CostCentreID;
                    dtpDispatchDate.Value = Common.FormatDate(invSalesHeader.DocumentDate);
                    txtDocumentNo.Text = invSalesHeader.DocumentNo;

                    CustomerService customerService = new CustomerService();
                    Customer customer = new Customer();

                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    InvSalesPerson invSalesPerson = new InvSalesPerson();

                    customer = customerService.GetCustomersById(invSalesHeader.CustomerID);
                    txtCustomerCode.Text = customer.CustomerCode;
                    txtCustomerName.Text = customer.CustomerName;

                    invSalesPerson = invSalesPersonService.GetInvSalesPersonByID(invSalesHeader.SalesPersonID);
                    txtSalesPersonCode.Text = invSalesPerson.SalesPersonCode;
                    txtSalesPersonName.Text = invSalesPerson.SalesPersonName;

                    txtReferenceNo.Text = invSalesHeader.ReferenceNo;
                    txtRemark.Text = invSalesHeader.Remark;

                    recallStatus = invSalesHeader.RecallDocumentStatus;

                    if (!invSalesHeader.ReferenceDocumentID.Equals(0))
                    {
                        //InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                        //txtQuotationNo.Text = invPurchaseOrderService.GetSavedDocumentDetailsByDocumentID(invPurchaseHeader.ReferenceDocumentID).DocumentNo;

                        if (invSalesHeader.RecallDocumentStatus.Equals(1))
                        {
                            InvSalesServices invInvoiceServices = new InvSalesServices();
                            txtInvoiceNo.Text = invInvoiceServices.GetSavedSalesHeaderByDocumentID(invSalesHeader.ReferenceDocumentDocumentID, invSalesHeader.ReferenceDocumentID).DocumentNo;
                            txtSalesOrderNo.Text = string.Empty;
                        }
                        else if (invSalesHeader.RecallDocumentStatus.Equals(2))
                        {
                            SalesOrderService salesOrderService = new SalesOrderService();
                            txtSalesOrderNo.Text = salesOrderService.GetSavedSalesOrderHeaderByDocumentID(invSalesHeader.ReferenceDocumentDocumentID, invSalesHeader.ReferenceDocumentID).DocumentNo;
                            txtInvoiceNo.Text = string.Empty;
                        }
                    }
                    else
                    {
                        txtSalesOrderNo.Text = string.Empty;
                        txtInvoiceNo.Text = string.Empty;
                    }

                    dgvItemDetails.DataSource = null;
                    invSalesDetailTempList = invSalesServices.GetDispatchDetail(invSalesHeader);
                    dgvItemDetails.DataSource = invSalesDetailTempList;
                    dgvItemDetails.Refresh();

                    Common.EnableTextBox(false, txtDocumentNo, txtInvoiceNo, txtSalesOrderNo);
                    Common.EnableButton(false, btnDocumentDetails, btnInvoiceDetails, btnSalesOrderDetails);

                    if (invSalesHeader.DocumentStatus.Equals(0))
                    {
                        EnableLine(false);
                        EnableProductDetails(true);
                        //if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                        Common.EnableButton(false,  btnPause);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    }
                    else
                    {
                        EnableLine(false);
                        EnableProductDetails(false);
                        Common.EnableButton(false, btnSave, btnPause);
                    }
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

        private void EnableProductDetails(bool state)
        {
            
        }

        private bool RecallInvoices(string documentNo)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                InvSalesHeader invSalesHeader = new InvSalesHeader();
                InvSalesServices invSalesServices = new Service.InvSalesServices();

                invSalesHeader = invSalesServices.getDispatchHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmInvoice").DocumentID, txtInvoiceNo.Text.Trim(), Common.LoggedLocationID);
                if (invSalesHeader != null)
                {
                    CustomerService customerService = new CustomerService();
                    Customer customer = new Customer();

                    customer = customerService.GetCustomersById(invSalesHeader.CustomerID);

                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    InvSalesPerson invSalesPerson = new InvSalesPerson();

                    invSalesPerson = invSalesPersonService.GetInvSalesPersonByID(invSalesHeader.SalesPersonID);

                    dtpDispatchDate.Value = Common.FormatDate(DateTime.Now);

                    txtDocumentNo.Text = GetDocumentNo(true);

                    txtInvoiceNo.Text = invSalesHeader.DocumentNo;

                    txtReferenceNo.Text = string.Empty;
                    txtRemark.Text = invSalesHeader.Remark;
                    customer = customerService.GetCustomersById(invSalesHeader.CustomerID);
                    txtCustomerCode.Text = customer.CustomerCode;
                    txtCustomerName.Text = customer.CustomerName;
                    txtSalesPersonCode.Text = invSalesPerson.SalesPersonCode;
                    txtSalesPersonName.Text = invSalesPerson.SalesPersonName;

                    txtInvoiceDate.Text = invSalesHeader.DocumentDate.ToString();
                    recallStatus = 1;

                    dgvItemDetails.DataSource = null;
                    invSalesDetailTempList = invSalesServices.getInvoiceDetail(invSalesHeader);
                    dgvItemDetails.DataSource = invSalesDetailTempList;
                    dgvItemDetails.Refresh();

                    Common.EnableTextBox(false, txtDocumentNo, txtInvoiceNo, txtSalesOrderNo);

                    grpBody.Enabled = true;
                    EnableLine(false);

                    Common.EnableButton(false, btnPause);
                    Common.EnableButton(true, btnSave);
                    Common.EnableButton(false, btnDocumentDetails, btnInvoiceDetails, btnSalesOrderDetails);
                  

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

        private bool RecallSalesOrders(string documentNo)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SalesOrderHeader salesOrderHeader = new SalesOrderHeader();
                InvSalesServices invSalesServices = new InvSalesServices();

                salesOrderHeader = invSalesServices.GetSalesOrderHeaderToDispatch(txtSalesOrderNo.Text.Trim(), AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSalesOrder").DocumentID);
                if (salesOrderHeader != null)
                {
                    InvSalesPerson invSalesPerson = new InvSalesPerson();
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    Customer customer = new Customer();
                    CustomerService customerService = new CustomerService();

                    invSalesPerson = invSalesPersonService.GetInvSalesPersonByID(salesOrderHeader.InvSalesPersonID);
                    customer = customerService.GetCustomersById(salesOrderHeader.CustomerID);

                    txtSalesPersonCode.Text = invSalesPerson.SalesPersonCode;
                    txtSalesPersonName.Text = invSalesPerson.SalesPersonName;

                    txtCustomerCode.Text = customer.CustomerCode;
                    txtCustomerName.Text = customer.CustomerName;

                    txtSalesOrderNo.Text = salesOrderHeader.DocumentNo;
                    
                    txtReferenceNo.Text = salesOrderHeader.ReferenceNo;
                    txtRemark.Text = salesOrderHeader.Remark;

                    recallStatus = 2;

                    dgvItemDetails.DataSource = null;
                    invSalesDetailTempList = invSalesServices.GetSalesOrderDetail(salesOrderHeader);
                    dgvItemDetails.DataSource = invSalesDetailTempList;
                    dgvItemDetails.Refresh();

                    Common.EnableTextBox(false, txtDocumentNo, txtInvoiceNo, txtSalesOrderNo);
                    Common.EnableTextBox(true, txtProductCode, txtProductName);
                    grpBody.Enabled = true;
                    EnableLine(false);

                    Common.EnableButton(false, btnPause);
                    Common.EnableButton(true, btnSave);
                    Common.EnableButton(false, btnDocumentDetails, btnInvoiceDetails, btnSalesOrderDetails);
                   
                    GetSummarizeFigures(invSalesDetailTempList);

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
            return false;
        }

        public void SetSerialNoList(List<InvProductSerialNoTemp> setInvProductSerialNoTemp, bool isValidNoOfSerialNo)
        {

            invProductSerialNoTempList = setInvProductSerialNoTemp;
            if (isValidNoOfSerialNo)
            {
                //txtFreeQty.Enabled = true;
                ////this.ActiveControl = txtFreeQty;
                //txtFreeQty.Focus();

            }
            else
            {
                txtDispatchQty.Focus();
            }
        }

        public void SetInvSerialNoList(List<InvProductSerialNoTemp> setInvProductSerialNoTemp)
        {
            invProductSerialNoTempList = setInvProductSerialNoTemp;
        }

        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtInvoiceNo.Text.Trim().Equals(string.Empty))
                    { RecallInvoices(txtInvoiceNo.Text.Trim()); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void Save()
        {
            if (!IsValidateControls(invSalesDetailTempList)) { return; }

            

                if ((Toast.Show("Dispatch Note  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
                {
                    this.Cursor = Cursors.WaitCursor;



                    string NewDocumentNo;
                    bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                    if (saveDocument.Equals(true))
                    {
                        this.Cursor = Cursors.Default;
                        Toast.Show("Dispatch Note  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);

                        RefreshDocumentNumber();
                        ClearForm();
                        FrmInvoice frmInvoice = new FrmInvoice(NewDocumentNo, 1508);
                        MdiMainRibbon mdi = null;
                        //MdiMainRibbon mdi = new MdiMainRibbon();
                        frmInvoice.MdiParent = mdi;
                        frmInvoice.Show();
                        frmInvoice.btnSave.Enabled = true;



                        //login = new FrmLoginUser(mdi);
                        //            login.MdiParent = mdi;
                        //            login.Show();

                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        Toast.Show("Dispatch Note  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                        return;
                    }
                }
             
        }

        //private void RefreshDocumentNumber()
        //{
        //    InvSalesServices invSalesServices = new InvSalesServices();
        //    Common.SetAutoComplete(txtDocumentNo, invSalesServices.GetAllDocumentNumbers(documentID, Common.LoggedLocationID), chkAutoCompleationDocumentNo.Checked);
        //}

        public override void ClearForm()
        {
            dgvItemDetails.DataSource = null;
            dgvItemDetails.Refresh();

            base.ClearForm();
        }

        private string GetDocumentNo(bool isTemporytNo, bool isInv)
        {
            try
            {
                InvSalesServices invSalesServices = new InvSalesServices();
                LocationService locationService = new LocationService();
                return invSalesServices.GetDocumentNo(this.Name, Common.LoggedLocationID, locationService.GetLocationsByID(Common.LoggedLocationID).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }
        }

        private bool    SaveDocument(int documentStatus, string documentNo, out string newDocumentNo)
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

                Location = locationService.GetLocationsByID(Common.LoggedLocationID);
                invSalesHeader = invSalesServices.getDispatchHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Location.LocationID);
                if (invSalesHeader == null)
                { invSalesHeader = new InvSalesHeader(); }

                invSalesHeader.SalesHeaderID = invSalesHeader.InvSalesHeaderID;
                invSalesHeader.CompanyID = Location.CompanyID;
                invSalesHeader.CostCentreID = Common.ConvertStringToInt(cmbCostCentre.SelectedValue.ToString());

                invSalesHeader.DocumentDate = Common.ConvertStringToDate(dtpDispatchDate.Value.ToString());

                int SalesDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmInvoice").DocumentID;

                invSalesHeader.DocumentID = SalesDocumentID;
                invSalesHeader.DocumentStatus = documentStatus;
                invSalesHeader.DocumentNo = documentNo.Trim();
                invSalesHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                invSalesHeader.LocationID = Location.LocationID;

                if (!txtSalesOrderNo.Text.Trim().Equals(string.Empty))
                {
                    SalesOrderService salesOrderService = new SalesOrderService();
                    SalesOrderHeader salesOrderHeader = new SalesOrderHeader();

                    salesOrderHeader = salesOrderService.GetSavedDocumentDetailsByDocumentNumber(txtSalesOrderNo.Text.Trim());
                    invSalesHeader.ReferenceDocumentDocumentID = salesOrderHeader.DocumentID;
                    invSalesHeader.ReferenceDocumentID = salesOrderHeader.SalesOrderHeaderID;
                    invSalesHeader.OtherChargers = salesOrderHeader.OtherCharges;
                    invSalesHeader.ExchangeRate = salesOrderHeader.DiscountAmount; // exchange rete save in sales ordrer as  discount amount
                }

                invSalesHeader.ReferenceNo = txtReferenceNo.Text.Trim();
                invSalesHeader.Remark = txtRemark.Text.Trim();
                invSalesHeader.CustomerID = customer.CustomerID;
                invSalesHeader.SalesPersonID = invSalesPerson.InvSalesPersonID;
                invSalesHeader.RecallDocumentStatus = recallStatus;
                invSalesHeader.StartTime = Common.FormatDateTime(DateTime.Now);
                invSalesHeader.EndTime = Common.FormatDateTime(DateTime.Now);
                invSalesHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtNetAmount.Text.ToString());
                invSalesHeader.GrossAmount = Common.ConvertStringToDecimalCurrency(txtNetAmount.Text.ToString());
                invSalesHeader.TransStatus = 1;


                AutoGenerateInfo autoGenerateInfoInvoice = new AutoGenerateInfo();
                autoGenerateInfoInvoice = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmInvoice");

                invoiceID = autoGenerateInfoInvoice.DocumentID;

                return invSalesServices.SaveDispatch(invSalesHeader, invSalesDetailTempList, invProductSerialNoTempList, txtSalesOrderNo.Text,Common.ConvertStringToDecimal(txtExchangeRate.Text.Trim()), out newDocumentNo, txtInvoiceNo.Text.Trim(), invoiceID, autoGenerateInfoInvoice.FormName);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                newDocumentNo = string.Empty;
                this.Cursor = Cursors.Default;

                return false;

            }
        }
        private decimal GetSummarizeQty(List<InvSalesDetailTemp> listItem)
       {

            decimal totQty = listItem.GetSummaryAmount(x => x.DispatchQty);

            return totQty;

        }
        public override void Pause()
        {
            //if ((Toast.Show("Dispatch Note  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    string NewDocumentNo;
            //    bool saveDocument = SaveDocument(0, txtDocumentNo.Text.Trim(), out NewDocumentNo);
            //    this.Cursor = Cursors.Default;
            //    if (saveDocument.Equals(true))
            //    {
            //        Toast.Show("Dispatch Note  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
            //        //RefreshDocumentNumber();
            //       // GenerateReport(NewDocumentNo.Trim(), 0);
            //        ClearForm();
            //    }
            //    else
            //    {
            //        Toast.Show("Dispatch Note  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
            //        return;
            //    }
            //}
        }

        private void EnableLine(bool enable)
        {
            Common.EnableTextBox(enable, txtProductCode, txtProductName,txtQty, txtIssuedQty, txtDispatchQty,txtSize);
        }

        public void SetBatchNumber(string batchNo)
        {
            batchNumber = batchNo;
        }

        private void CalculateLine(decimal sellingPrice)
        {
            decimal amount = 0;
            try
            {
                amount = (Common.ConvertStringToDecimalCurrency(txtDispatchQty.Text.Trim()) * sellingPrice);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void UpdateGrid(InvSalesDetailTemp invSalesDetailTemp)
        {
            try
            {
                decimal qty = 0;
                //if ((Common.ConvertStringToDecimalQty(txtDispatchQty.Text.Trim()) < 0))
               // {
                    
                    //if (isInvProduct) { invSalesDetailTemp.BaseUnitID = existingInvProductMaster.UnitOfMeasureID; }
                    //else { invSalesDetailTemp.BaseUnitID = existingInvProductMaster.UnitOfMeasureID; }

                   

                    //if (chkOverwrite.Checked)
                    //{
                    //    string productCode = txtProductCode.Text.Trim();
                    //    string unit = cmbUnit.Text.Trim();

                    //    if (dgvItemDetails["ProductCode", 0].Value == null) { }
                    //    else
                    //    {
                    //        for (int i = 0; i < dgvItemDetails.RowCount; i++)
                    //        {
                    //            if (productCode.Equals(dgvItemDetails["ProductCode", i].Value.ToString()) && unit.Equals(dgvItemDetails["Unit", i].Value.ToString()))
                    //            {
                    //                if (Toast.Show("", Toast.messageType.Question, Toast.messageAction.OverwriteQty).Equals(DialogResult.Yes)) { }
                    //                else { return; }
                    //            }
                    //        }
                    //    }

                    //}
                    //else
                    {
                        //qty = (invSalesDetailTemp.DispatchQty + Common.ConvertStringToDecimalQty(txtDispatchQty.Text));
                    }

                    qty = (Common.ConvertStringToDecimalQty(txtDispatchQty.Text));

                    invSalesDetailTemp.DispatchQty = qty;
                    invSalesDetailTemp.DocumentNo = txtDocumentNo.Text.Trim();
                    invSalesDetailTemp.ProductCode = txtProductCode.Text.Trim();
                    invSalesDetailTemp.ProductName = txtProductName.Text.Trim();
                    invSalesDetailTemp.Size = txtSize.Text.Trim();

                    //invSalesDetailTemp.Rate = Common.ConvertStringToDecimalCurrency(sellingPrice.ToString());
                    invSalesDetailTemp.NetAmount = invSalesDetailTemp.Rate * qty;
                    CalculateLine(Common.ConvertStringToDecimalCurrency(sellingPrice.ToString()));

                    InvSalesServices invSalesServices = new InvSalesServices();

                    dgvItemDetails.DataSource = null;
                    invSalesDetailTempList = invSalesServices.getUpdateSalesDetailTemp(invSalesDetailTempList, invSalesDetailTemp, existingInvProductMaster);
                    dgvItemDetails.DataSource = invSalesDetailTempList;
                    dgvItemDetails.Refresh();

                    GetSummarizeFigures(invSalesDetailTempList);
                    EnableLine(false);
                    Common.EnableTextBox(false, txtDocumentNo);
                    //if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    ClearLine();

                    txtProductCode.Enabled = true;
                    txtProductCode.Focus();
                //}
                //else
                //{
                //    Toast.Show("Invalid Operation", Toast.messageType.Information, Toast.messageAction.ValidationFailed);
                //    txtProductCode.Focus();
                //}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void GetSummarizeFigures(List<InvSalesDetailTemp> listItem)
        {
            decimal totSellingValue = listItem.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.NetAmount);

            totSellingValue = Common.ConvertStringToDecimalCurrency(totSellingValue.ToString());
            txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(totSellingValue);
            //txtTotalSellingValue.Text = Common.ConvertDecimalToStringCurrency(totSellingValue);
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode, txtProductName, txtQty, txtIssuedQty, txtDispatchQty,txtSize);
            sellingPrice = 0;
        }

        private void loadProductDetails(bool isCode, string strProduct, long unitofMeasureID, DateTime expiryDate)
        {
            try
            {
                existingInvProductMaster = new InvProductMaster();

                
                if (strProduct.Equals(string.Empty))
                { return; }

                InvProductMasterService InvProductMasterService = new InvProductMasterService();

                //if (isCode)
                //{
                //    existingInvProductMaster = InvProductMasterService.GetProductsByCode(strProduct);
                //    if (isCode && strProduct.Equals(string.Empty))
                //    {
                //        txtProductName.Focus();
                //        return;
                //    }
                //}
                //else
                //{ existingInvProductMaster = InvProductMasterService.GetProductsByName(strProduct); }

                if (strProduct != null)
                {
                    InvSalesServices invSalesServices = new InvSalesServices();
                    if (invSalesDetailTempList == null)
                    { invSalesDetailTempList = new List<InvSalesDetailTemp>(); }

                    existingInvSalesDetailTemp = invSalesServices.getSalesDetailTemp(invSalesDetailTempList, strProduct, Common.LoggedLocationID, Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID);
                    if (existingInvSalesDetailTemp != null)
                    {
                        txtProductCode.Text = existingInvSalesDetailTemp.ProductCode;
                        txtProductName.Text = existingInvSalesDetailTemp.ProductName;
                        //costPrice = Common.ConvertDecimalToStringCurrency(existingInvSalesDetailTemp.CostPrice);
                        sellingPrice = existingInvSalesDetailTemp.Rate;
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingInvSalesDetailTemp.Qty);
                        txtIssuedQty.Text = Common.ConvertDecimalToStringQty(existingInvSalesDetailTemp.BalanceQty);
                        txtDispatchQty.Text = Common.ConvertDecimalToStringQty(existingInvSalesDetailTemp.DispatchQty);
                        txtSize.Text = existingInvSalesDetailTemp.Size.ToString();

                      
                        //Common.EnableComboBox(true, cmbUnit);
                        //if (unitofMeasureID.Equals(0))
                        //    cmbUnit.Focus();
                        //txtProductCode.Enabled = true;
                        //txtProductCode.Focus();
                    }
                }
                else
                {
                    Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private bool IsValidateControls(List<InvSalesDetailTemp> invSalesDetailTempList)
        {
            bool isValidated = false;

            //string[] batchProductList = invSalesDetailTempList.Where(l => l.IsBatch == true && l.BatchNo == null).Select(l => l.DocumentNo).ToArray();


            decimal totQty = GetSummarizeQty(invSalesDetailTempList);

            if (totQty == 0)
            {
                Toast.Show("Please check Qty", Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else
            {
                isValidated = true;
            }



            return isValidated;
        }

        private void chkAutoCompleationInvoiceNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //Load Invoice Numbers
                InvSalesServices invSalesServices = new InvSalesServices();
                int invoicehDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmInvoice").DocumentID;
                Common.SetAutoComplete(txtInvoiceNo, invSalesServices.GetAllDocumentNumbersToDispatch(invoicehDocumentID, Common.LoggedLocationID), chkAutoCompleationInvoiceNo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        private void txtSalesOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
            {
                if (!txtSalesOrderNo.Text.Trim().Equals(string.Empty))
                { RecallSalesOrders(txtSalesOrderNo.Text.Trim()); }
            }
        }

      

        private void txtDispatchQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
            {
                txtProductCode.Focus();
            }
        }


        private void dgvItemDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F2))
            {
                try
                {
                    if (dgvItemDetails.CurrentCell != null && dgvItemDetails.CurrentCell.RowIndex >= 0)
                    {
                        if (Toast.Show("Product " + dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + " - " + dgvItemDetails["ProductName", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                            return;

                        InvSalesDetailTemp invSalesDetailTemp = new InvSalesDetailTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        InvProductMasterService invProductMasterService = new InvProductMasterService();

                        invSalesDetailTemp.ProductID = invProductMasterService.GetProductsByCode(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).InvProductMasterID;
                        invSalesDetailTemp.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                        InvSalesServices invSalesServices = new InvSalesServices();

                        dgvItemDetails.DataSource = null;
                        invSalesDetailTempList = invSalesServices.GetDeleteSalesDetailTemp(invSalesDetailTempList, invSalesDetailTemp);
                        dgvItemDetails.DataSource = invSalesDetailTempList;
                        dgvItemDetails.Refresh();
                        //GetSummarizeFigures(invSalesDetailTempList);
                        this.ActiveControl = txtProductCode;
                        txtProductCode.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                }
            }
        }

        private void dgvItemDetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvItemDetails.CurrentCell != null && dgvItemDetails.CurrentCell.RowIndex >= 0)
                {
                    if (dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value == null)
                    {
                        Toast.Show("No data available to display", Toast.messageType.Information, Toast.messageAction.General, "");
                        return;
                    }
                    else
                    {
                        Common.EnableTextBox(true, txtProductName, txtProductCode);

                       // UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        loadProductDetails(true, dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim(),0, DateTime.Now);

                        InvProductMasterService invProductMasterService = new InvProductMasterService();

                        InvProductMaster invProductMaster = invProductMasterService.GetProductsByCode(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString());

                        //CalculateLine();
                        if (documentState.Equals(1))
                        {
                            EnableLine(false);
                            txtProductCode.Enabled = false;
                            txtProductName.Enabled = false;
                        }
                        else
                        {
                                txtSize.Enabled = true;
                                txtDispatchQty.Enabled = true;
                                this.ActiveControl = txtSize;
                                txtSize.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtDocumentNo_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDocumentNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                { RecallDocument(txtDocumentNo.Text.Trim()); }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationSalesOrderNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //Load Sales Order Numbers
                SalesOrderService salesOrderService = new SalesOrderService();
                int salesOrderhDocumentID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSalesOrder").DocumentID;
                Common.SetAutoComplete(txtSalesOrderNo, salesOrderService.GetAllDocumentNumbersToDispatch(salesOrderhDocumentID, Common.LoggedLocationID), chkAutoCompleationSalesOrderNo.Checked);
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
                //Load Document Numbers
                InvSalesServices invDispatchServices = new InvSalesServices();
                Common.SetAutoComplete(txtDocumentNo, invDispatchServices.GetAllDocumentNumbers(documentID, Common.LoggedLocationID), chkAutoCompleationDocumentNo.Checked);
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
                    txtProductName.Enabled = true;
                    txtProductName.Focus();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductCode_Leave(object sender, EventArgs e)
        {
            try
            {
                //loadProductDetails(true, txtProductCode.Text.Trim(), 0, DateTime.Now);
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
                        txtDispatchQty.Focus();
                        txtDispatchQty.SelectAll();
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
                //loadProductDetails(false, txtProductName.Text.Trim(), 0, DateTime.Now);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

       

        private void txtDispatchQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIssuedQty_TextChanged(object sender, EventArgs e)
        {

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

                        InvStockAdjustmentService stockAdjustmentService = new InvStockAdjustmentService();
                        invProductBatchNoTempList = stockAdjustmentService.GetExpiryDetail(existingInvProductMaster);

                        if (invProductSerialNoTempList == null)
                            invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                        //FrmExpiary frmExpiry = new FrmExpiary(invProductBatchNoTempList, invProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, FrmExpiary.transactionType.StockAdjustment);
                        //frmExpiry.ShowDialog();

                        //dtpExpiry.Value = expiryDate;

                        txtDispatchQty.Enabled = true;
                        if (Common.ConvertStringToDecimal(txtDispatchQty.Text.Trim()) == 0)
                        { txtDispatchQty.Text = "1"; }
                        txtDispatchQty.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            
            }
        }

        private void btnSalesOrderDetails_Click(object sender, EventArgs e)
        {

        }

        private void txtDispatchQty_Validated(object sender, EventArgs e)
        {
            try
            {
                   if (Common.ConvertStringToDecimal(txtDispatchQty.Text.Trim()) >= 0 && Common.ConvertStringToDecimal(txtIssuedQty.Text.Trim()) >= Common.ConvertStringToDecimal(txtDispatchQty.Text.Trim()))
                    {
                        CommonService commonService = new CommonService();


                        convertFactor = 1;

                        //if (commonService.ValidateBatchStock(Common.ConvertStringToDecimalQty(txtDispatchQty.Text.Trim()), existingInvProductMaster, Common.LoggedLocationID, "", convertFactor))
                        //{
                        //    if (existingInvProductMaster.IsSerial)
                        //    {
                        InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();
                        //invProductSerialNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;


                        InvSalesServices invSalesServices = new InvSalesServices();
                        //invProductSerialNoTempList = invSalesServices.getSerialNoDetail(existingInvProductMaster);

                        //if (invProductSerialNoTempList == null)
                        //{ invProductSerialNoTempList = new List<InvProductSerialNoTemp>(); }

                        //FrmSerialCommon frmSerialCommon = new FrmSerialCommon(invProductSerialNoTempList, invProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtDispatchQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtDispatchQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.LoggedLocationID, isInvProduct, documentID, FrmSerialCommon.transactionType.Dispatch);
                        //frmSerialCommon.ShowDialog();
                        //loadProductDetails(true, txtProductCode.Text,1, DateTime.Now);
                        //CalculateLine(sellingPrice);
                        //    }
                        //    else
                        //    {
                        //        CalculateLine(sellingPrice);
                        //    }

                        UpdateGrid(existingInvSalesDetailTemp);
                    }
                   else
                   {
                       Toast.Show("Dispatch", Toast.messageType.Information, Toast.messageAction.QtyExceed);
                       this.ActiveControl = txtDispatchQty;
                       txtDispatchQty.Focus();
                       txtDispatchQty.SelectAll();
                       return;
                   }

                
            }
            

            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        private void dgvItemDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Return)
            {
                txtDispatchQty.Focus();
                txtDispatchQty.SelectAll();
            }
        }
        
    }
    }

