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
using Report.Inventory;
using Service;
using Utility;

namespace UI.Windows
{
    public partial class FrmProductPriceChangeDamage : FrmBaseTransactionForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        private int tagwz = 0;
        private InvProductPriceChangeDamageService invProductPriceChangeDamageService;
        private InvProductPriceChangeDetailDamage existingInvProductPriceChangeDamage;
        InvProductMaster existingInvProductMaster;
 
        PriceChangeTempDamage priceChangeTempDamage = new PriceChangeTempDamage();
        static List<PriceChangeTempDamage> PriceChangeTempDamagelst = new List<PriceChangeTempDamage>();

        bool isInvProduct;
      
        int datarow = 0;
        int documentID = 0;
        string documentNo = "";
        static string batchNumber;
        static int  locationId;
        //static int DocPcType;
        static int DocumentStatus=0;
        public FrmProductPriceChangeDamage()
        {
            InitializeComponent();
        }

        //public FrmProductPriceChangeKK(int PcType)
        //{
        //    InitializeComponent();
        //    DocPcType = PcType;
        //}

        public override void InitializeForm()
        {
            try
            {
                Common.EnableTextBox(true, txtProductCode, txtProductName,txtNewProductCode,txtNewProductName);
                cmbFromLocation.Enabled = true;
                existingInvProductMaster = null;
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                documentID = autoGenerateInfo.DocumentID;
                txtDocumentNo.Text = GetDocumentNo(true);
                this.ActiveControl = txtProductCode;
                txtProductCode.Focus();
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
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

 
                documentID = autoGenerateInfo.DocumentID;
                 

                //accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                //if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }

               
                pnlUpdateCostPrice.Enabled = false;
                pnlUpdateSellingPrice.Enabled = false;
 

                dgvItemDetails.AutoGenerateColumns = false;
                dgvItemDetails.AllowUserToAddRows = false;
                dgvItemDetails.AllowUserToDeleteRows = false;
                LoadProducts();
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbFromLocation, locationService.GetAllLocations());
                
                InvDepartmentService invDepartmentService = new InvDepartmentService();
                Common.LoadDepartmentNames(cmbDepartment, invDepartmentService.GetAllDepartmentNamesList());

                invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
                Common.LoadDamageType(cmbDamageType, invProductPriceChangeDamageService.GetAllDamageTypesList());

                tabCritaria.SelectedTab = tpCritaria;
                cmbFromLocation.SelectedValue = Common.LoggedLocationID;
               

 
                base.FormLoad();

                // Load Unit of measures
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                Common.LoadUnitOfMeasures(cmbUnit, unitOfMeasureService.GetAllUnitOfMeasures());

                

                invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
                Common.SetAutoComplete(txtDocumentNo, invProductPriceChangeDamageService.GetPausedDocumentNumbers(), chkAutoCompleationPoNo.Checked);
                cmbUnit.SelectedIndex = -1;
                //cmbDepartment.SelectedIndex = 9;
               
                
                this.ActiveControl = cmbFromLocation;
                txtProductCode.Focus();
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        # region Load Grids ----

        


        #endregion

       
        private void UpdateDiscount()
        {
            try
            {

               

                invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
                dgvItemDetails.DataSource = null;

                PriceChangeTempDamagelst = invProductPriceChangeDamageService.getUpdatePriceDiscount(PriceChangeTempDamagelst, Common.ConvertStringToDecimal(txtSellingDiscount.Text.Trim()), txtNewUProduct.Text.Trim());   // (chkSellingPercentage.Checked ? true : false)
                dgvItemDetails.AutoGenerateColumns = false;
                dgvItemDetails.DataSource = PriceChangeTempDamagelst;
                dgvItemDetails.Refresh();




            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            if (!txtSellingDiscount.Text.Trim().Equals(string.Empty) && txtSellingDiscount.Text.Trim() != null)
            {
                UpdateDiscount();
                Common.ClearTextBox(txtNewUProduct, txtNewUProName, txtSellingDiscount);
            }
        }

        private bool ValidateControls()
        {
            return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtSellingDiscount);
        }

        public override void Pause()
        {
            if ((Toast.Show("Price Change  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                this.Cursor = Cursors.WaitCursor;
                string NewDocumentNo;
                bool saveDocument = SaveDocument(0, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                this.Cursor = Cursors.Default;
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Price Change  -  " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 0);
                    RefreshDocumentNumbers();
                    ClearForm();
                }
                else
                {
                    Toast.Show("Price Change  -  " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        private void RefreshDocumentNumbers()
        {
            ////Load PO Document Numbers
            InvProductPriceChangeDamageService invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
            Common.SetAutoComplete(txtDocumentNo, invProductPriceChangeDamageService.GetPausedDocumentNumbers(), chkAutoCompleationPoNo.Checked);
        }

        private void GenerateReport(string documentNo, int documentStatus)
        {
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            invReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
        }

        private bool SaveDocument(int documentStatus, string documentNo, out string newDocumentNo)
        {
            try
            {
                invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();

                InvProductPriceChangeHeaderDamage invProductPriceChangeHeaderDamage = new InvProductPriceChangeHeaderDamage();
                invProductPriceChangeHeaderDamage = invProductPriceChangeDamageService.GetPausedInvProductPriceChangeHeaderDamageByDocumentNo(documentID, txtDocumentNo.Text.Trim());
                if (invProductPriceChangeHeaderDamage == null)
                    invProductPriceChangeHeaderDamage = new InvProductPriceChangeHeaderDamage();


                invProductPriceChangeHeaderDamage.CompanyID = Common.LoggedCompanyID;
                invProductPriceChangeHeaderDamage.DocumentDate = Common.FormatDate(DateTime.Now);
                invProductPriceChangeHeaderDamage.EffectFromDate = Common.FormatDate(DateTime.Now);
                invProductPriceChangeHeaderDamage.DocumentID = documentID;
                invProductPriceChangeHeaderDamage.DocumentStatus = documentStatus;
                invProductPriceChangeHeaderDamage.DocumentNo = documentNo.Trim();
                invProductPriceChangeHeaderDamage.GroupOfCompanyID = Common.GroupOfCompanyID;
                invProductPriceChangeHeaderDamage.LocationID = Common.LoggedLocationID;
                invProductPriceChangeHeaderDamage.ReferenceNo = txtReferenceNo.Text.Trim();
                invProductPriceChangeHeaderDamage.TogNO = cmbTogDocument.Text.Trim();

                invProductPriceChangeHeaderDamage.IsUpLoad = chkTStatus.Checked;
                return invProductPriceChangeDamageService.Save(invProductPriceChangeHeaderDamage, PriceChangeTempDamagelst, out newDocumentNo, this.Name);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                newDocumentNo = string.Empty;
                this.Cursor = Cursors.Default;

                return false;
            }
        }

        private bool ValidateTextBoxes()
        {
            bool isValidatedBalnk = false;
            bool isValidatedZero = false;
            isValidatedBalnk = Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo);

            if (isValidatedZero.Equals(true) && isValidatedBalnk.Equals(true))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public override void Save()
        {
            try
            {

                if ((Toast.Show("Price Change  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
                {
                   // if (ValidateTextBoxes().Equals(false)) { return; }

                    this.Cursor = Cursors.WaitCursor;
                    string NewDocumentNo;
                    bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                    this.Cursor = Cursors.Default;
                    if (saveDocument.Equals(true))
                    {
                        Toast.Show("Purchase Order  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                        GenerateReport(NewDocumentNo.Trim(), 1);
                        RefreshDocumentNumbers();
                        ClearForm();
                    }
                    else
                    {
                        Toast.Show("Purchase Order  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void View()
        {
            base.View();
            GenerateReport(txtDocumentNo.Text.Trim(), DocumentStatus);
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
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        loadProductDetails(true, dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim(), Common.ConvertStringToLong(dgvItemDetails["UOMID", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()), Common.ConvertStringToInt(dgvItemDetails["LocationIdx", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()), Common.ConvertStringToInt(dgvItemDetails["LineNo", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()));

                        Common.EnableTextBox(true, txtNewCostPrice, txtNewSellingPrice,txtQty);
                        Common.EnableTextBox(false, txtProductCode, txtProductName, txtNewProductCode);
                        Common.EnableComboBox(false, cmbUnit);
                        txtNewSellingPrice.SelectAll();
                        txtNewSellingPrice.Focus();
                        txtNewSellingPrice.SelectAll();
                        

                      
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void loadProductDetails(bool isCode, string strProduct, long unitofMeasureID,int locationId,int lineNo)
        {
            try
            {
                existingInvProductMaster = new InvProductMaster();

                if (strProduct.Equals(string.Empty))
                    return;

                InvProductMasterService InvProductMasterService = new InvProductMasterService();

                if (isCode)
                {

                    existingInvProductMaster = InvProductMasterService.GetProductsByCode(strProduct);
                    if (isCode && strProduct.Equals(string.Empty))
                    {
                        txtProductName.Focus();
                        return;
                    }
                }
                else
                    existingInvProductMaster = InvProductMasterService.GetProductsByName(strProduct); ;

                if (existingInvProductMaster != null)
                {
                    invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
                    if (PriceChangeTempDamagelst == null)
                        PriceChangeTempDamagelst = new List<PriceChangeTempDamage>();
                        priceChangeTempDamage = invProductPriceChangeDamageService.getPriceChangeTemp(PriceChangeTempDamagelst, existingInvProductMaster, locationId, unitofMeasureID,lineNo);
                    if (priceChangeTempDamage != null)
                    {
                        txtProductCode.Text = priceChangeTempDamage.ProductCode;
                        txtProductName.Text = priceChangeTempDamage.ProductName;
                        cmbUnit.SelectedValue = priceChangeTempDamage.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(priceChangeTempDamage.CostPrice);
                        txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(priceChangeTempDamage.SellingPrice);
                        txtQty.Text = Common.ConvertDecimalToStringQty(priceChangeTempDamage.Qty);
                        txtNewProductCode.Text = priceChangeTempDamage.NewProductCode;
                        txtNewProductName.Text = priceChangeTempDamage.NewProductName;
                        txtMrp.Text = Common.ConvertDecimalToStringCurrency(priceChangeTempDamage.MRP);
                        //datarow = lineNo;

                        //priceChangeTempDamage.LineNo = lineNo;

                        
                        Common.EnableComboBox(true, cmbUnit);
                        if (unitofMeasureID.Equals(0))
                            cmbUnit.Focus();

 
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

        

        public override void ClearForm()
        {
            try
            {
                Common.ClearForm(this);
                errorProvider.Clear();
                InitializeForm();
                dtpEffectivDate.Value = DateTime.Now;
                DocumentStatus = 0;
                PriceChangeTempDamagelst.Clear();
                pnlUpdateSellingPrice.Enabled = true;
                Common.ClearTextBox(txtTotalQty, txtProductCode, txtProductName, txtNewProductCode, txtNewCostPrice, txtNewSellingPrice, txtCostPrice, txtSellingPrice, txtSellingDiscount, txtCostDiscount, txtSellingDiscount, txtNewUProduct, txtNewUProName);
                Common.EnableTextBox(false, txtQty, txtSellingPrice, txtCostPrice, txtNewProductCode, txtNewProductCode, txtNewCostPrice, txtNewSellingPrice,txtMrp);
                Common.EnableTextBox(true, txtProductCode, txtProductName,txtDocumentNo);
                Common.EnableComboBox(false, cmbUnit);
                pnlUpdateSellingPrice.Enabled = false;
                invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
                //Common.SetAutoComplete(txtDocumentNo, invProductPriceChangeService.GetPausedDocumentNumbers(), chkAutoCompleationPoNo.Checked);
 
                dgvItemDetails.DataSource = null;
                do
                {
                    foreach (DataGridViewRow row in dgvItemDetails.Rows)
                    {
                        try
                        {
                            dgvItemDetails.Rows.Remove(row);
                        }
                        catch (Exception) { }
                    }
                } while (dgvItemDetails.Rows.Count > 1);

                InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                LocationService locationService = new LocationService();
                Common.LoadTOGDocuments(cmbTogDocument, invTransferOfGoodsService.GetTOGDocuments(locationService.GetLocationsByName(cmbFromLocation.Text.Trim()).LocationID));
                cmbTogDocument.SelectedIndex = -1;

                cmbTogDocument.Enabled = true;
                cmbFromLocation.SelectedValue = Common.LoggedLocationID;
                txtProductCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadProducts()
        {
            try
            {
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                //if (DocPcType == 0)
                //{
                //    Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodes(), true);
                //}
                //else
                //{
                Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodes(), true);
                                //}

                //Common.SetAutoComplete(txtProductName, invProductMasterService.GetAllProductNames(),true);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void chkUpdateSellingPrice_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void chkUpdateCostPrice_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void btnUpdateCostPrice_Click(object sender, EventArgs e)
        {
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        

        private void pnlItemWise_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

       
        private void CheckedGrdCheckBox(DataGridView c, string checkstr, bool status)
        {
            for (int i = 0; i < c.RowCount; i++)
            {
                c.Rows[i].Cells[checkstr].Value = status;
            }
                 
           
        }
  
 

        private void txtNewSellingPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Return))
                {
                    if (!string.IsNullOrEmpty(txtNewSellingPrice.Text.Trim()))
                    {
                        if (!string.IsNullOrEmpty(cmbDepartment.Text.ToString()))
                        {
                            InvProductMasterService invProductMasterService = new InvProductMasterService();
                            InvDepartmentService invDepartmentService = new InvDepartmentService();
                            InvDepartment invDepartment = new InvDepartment();
                            invDepartment = invDepartmentService.GetInvDepartmentsByName(cmbDepartment.Text.ToString(), false);

                            if (invProductMasterService.GetAllProductCodesCount(invDepartment.InvDepartmentID, Common.ConvertStringToDecimal(txtNewSellingPrice.Text.Trim())) != 1)
                            {
                                Common.SetAutoComplete(txtNewProductCode, invProductMasterService.GetAllProductCodes(invDepartment.InvDepartmentID, Common.ConvertStringToDecimal(txtNewSellingPrice.Text.Trim())), true);
                                Common.EnableTextBox(true, txtNewProductCode);
                                txtNewProductCode.Focus();
                            }
                            else
                            {
                                InvProductMaster invProductMaster = new InvProductMaster();
                                invProductMaster = invProductMasterService.GetProductCode(invDepartment.InvDepartmentID, Common.ConvertStringToDecimal(txtNewSellingPrice.Text.Trim()));
                                txtNewProductCode.Text = invProductMaster.ProductCode;
                                txtNewProductName.Text = invProductMaster.ProductName;
                                Common.EnableTextBox(true, txtQty);
                                txtQty.SelectAll();
                                txtQty.Focus();
                                txtQty.SelectAll();
                            }
                        }
                        else
                        {
                            Toast.Show("Department Not Selected.", Toast.messageType.Warning,Toast.messageAction.AccessDenied);
                            txtNewSellingPrice.SelectAll();
                            cmbDepartment.Focus();

                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);      
            }
        }

        private void UpdateToGrid(PriceChangeTempDamage priceChangeTempDamage)
        {
            try
            {
                
                 
                if (txtProductCode.Text != string.Empty && (txtNewCostPrice.Text != string.Empty || txtNewSellingPrice.Text != string.Empty))
                {
                    string productCode = txtProductCode.Text.Trim();
                    string unit = cmbUnit.Text.Trim();

                    if (dgvItemDetails.RowCount>0)  
                    {
                        for (int i = 0; i < dgvItemDetails.RowCount; i++)
                        {
                            if (locationId.Equals(productCode.Equals(dgvItemDetails["LocationIdx", i].Value.ToString())) && batchNumber.Equals(productCode.Equals(dgvItemDetails["BatchNo", i].Value.ToString())) && productCode.Equals(dgvItemDetails["ProductCode", i].Value.ToString()) && unit.Equals(dgvItemDetails["UnitOfMeasureName", i].Value.ToString()))
                            {
                                if (Toast.Show("", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.Yes)) { }
                                else { return; }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(cmbDamageType.Text.Trim()))
                    
                    {

                        invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
                        dgvItemDetails.DataSource = null;
                        InvProductMaster invProductMaster = new InvProductMaster();
                        InvProductMaster invProductMasterOld = new InvProductMaster();
                        InvProductMasterService invProductMasterService = new InvProductMasterService();
                        invProductMaster = invProductMasterService.GetProductsByCode(txtNewProductCode.Text.Trim());
                        invProductMasterOld= invProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());
                        priceChangeTempDamage.NewProductCode = invProductMaster.ProductCode;
                        priceChangeTempDamage.NewProductID = invProductMaster.InvProductMasterID;
                        priceChangeTempDamage.NewProductName = invProductMaster.ProductName;
                        priceChangeTempDamage.ProductCode = invProductMasterOld.ProductCode;
                        priceChangeTempDamage.ProductID = invProductMasterOld.InvProductMasterID;
                        priceChangeTempDamage.ProductName = invProductMasterOld.ProductName;
                        priceChangeTempDamage.CostPrice = Common.ConvertStringToDecimal(txtCostPrice.Text.Trim());
                        priceChangeTempDamage.SellingPrice = Common.ConvertStringToDecimal(txtSellingPrice.Text.Trim());
                        priceChangeTempDamage.NewSellingPrice = invProductMaster.SellingPrice;
                        priceChangeTempDamage.NewCostPrice = invProductMaster.CostPrice;
                        priceChangeTempDamage.DamageType = cmbDamageType.Text.Trim();
                        priceChangeTempDamage.UnitOfMeasureID = invProductMaster.UnitOfMeasureID;
                        priceChangeTempDamage.UnitOfMeasureName = invProductMaster.UnitOfMeasure.UnitOfMeasureName;

                        priceChangeTempDamage.Qty = Common.ConvertStringToDecimal(txtQty.Text.Trim());
                        dgvItemDetails.DataSource = null;
                        PriceChangeTempDamagelst = invProductPriceChangeDamageService.getUpdatePriceChangeTemp(PriceChangeTempDamagelst, priceChangeTempDamage, invProductMaster.InvProductMasterID, Common.ConvertStringToDecimal(txtNewSellingPrice.Text.Trim()));
                        dgvItemDetails.AutoGenerateColumns = false;
                        dgvItemDetails.DataSource = PriceChangeTempDamagelst;
                        dgvItemDetails.Refresh();
                        txtTotalQty.Text = Common.ConvertDecimalToStringQty(PriceChangeTempDamagelst.GetSummaryAmount(x => x.Qty));
                    }
                    else
                    {
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
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                InvProductPriceChangeService invProductPriceChangeService = new InvProductPriceChangeService();
                InvProductBatchNoExpiaryDetail existingProBatchNo;
                UnitOfMeasure existingUOM;

                existingInvProductMaster = new InvProductMaster();
                if (!string.IsNullOrEmpty(txtProductCode.Text))
                {
                    existingInvProductMaster = invProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());

                    if (existingInvProductMaster != null)
                    {


                        txtProductCode.Text = existingInvProductMaster.ProductCode;
                        txtProductName.Text = existingInvProductMaster.ProductName;
                        existingUOM = unitOfMeasureService.GetUnitOfMeasureById(existingInvProductMaster.UnitOfMeasureID);
                        cmbUnit.SelectedValue = existingUOM.UnitOfMeasureID;

                        //txtUom.Text = existingUOM.UnitOfMeasureName.Trim();
                        existingProBatchNo = invProductPriceChangeService.GetProductBatchNo(existingInvProductMaster.InvProductMasterID);
                        //txtBatchNo.Text = existingProBatchNo.BatchNo.Trim();
                        //txtLocation.Text = existingInvProductMaster.Location.LocationName;
                        txtCostPrice.Text = existingProBatchNo.CostPrice.ToString();
                        txtSellingPrice.Text = existingProBatchNo.SellingPrice.ToString();
                        InvProductMasterService invProductMasterServiceNew = new InvProductMasterService();
                        decimal sumqty = invProductMasterServiceNew.GetProductsQtyByProductCodes(existingInvProductMaster.InvProductMasterID, Convert.ToInt32(cmbFromLocation.SelectedValue));
                        txtQty.Text = sumqty.ToString();
                        priceChangeTempDamage = new PriceChangeTempDamage();
                        Common.EnableComboBox(true, cmbUnit);
                        Common.EnableTextBox(true, txtNewSellingPrice,txtNewProductCode);
                        txtNewSellingPrice.Focus();


                    }
                }
                
                
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
                    return;

                if (!existingInvProductMaster.UnitOfMeasureID.Equals(Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())))
                {
                    InvProductUnitConversionService invProductUnitConversionService = new InvProductUnitConversionService();
                    if (invProductUnitConversionService.GetProductUnitByProductCode(existingInvProductMaster.InvProductMasterID, Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())) == null)
                    {
                        Toast.Show("Unit - " + cmbUnit.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "Product - " + txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "");
                        cmbUnit.SelectedValue = existingInvProductMaster.UnitOfMeasureID;
                        cmbUnit.Focus();
                        return;
                    }
                }

                //loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()));

                if (existingInvProductMaster.IsBatch)
                {
                    txtNewProductCode.Enabled = true;
                    txtNewProductCode.Focus();
                }
                else
                {
                    txtNewProductCode.Enabled = true;
                    txtNewProductCode.Focus();
                    //commonService = new CommonService();
                    //InvProductMaster invProductMaster = new InvProductMaster();
                    //invProductMaster = commonService.CheckInvProductCode(existingInvProductMaster.InvProductMasterID, existingInvProductMaster.UnitOfMeasureID);
 
                    //    txtCostPrice.Text = invProductMaster.CostPrice.ToString();
                    //    txtSellingPrice.Text = invProductMaster.SellingPrice.ToString();
                    //    priceChangeTemp.SellingPrice = invProductMaster.SellingPrice;
                    //    priceChangeTemp.CostPrice = invProductMaster.CostPrice;


                    //priceChangeTemp = new PriceChangeTemp();
                    //priceChangeTemp.BatchNo = "";
                    //priceChangeTemp.ProductID = invProductMaster.InvProductMasterID
                    //priceChangeTemp.UnitOfMeasureID = invProductMaster.UnitOfMeasureID;
                    //priceChangeTemp.LocationId = invProductMaster
                    //priceChangeTemp.Qty = Common.ConvertStringToLong(dgvSearchDetails["Qty", i].Value.ToString());
                    //PriceChangeTempList = commonService.UpdatePriceChangeTemp(PriceChangeTempList, priceChangeTemp);
                    //FrmProductPriceChangeKK.SetBatchNoList(PriceChangeTempList);




                    //    txtBatchNo.Enabled = false;
                    //    Common.EnableTextBox(true, txtNewCostPrice, txtNewSellingPrice);
                    //    txtNewCostPrice.Focus();

                 }
 
  
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
             
            
        }
       
        public void SetBatchNumber(string batchNo,int LocationId)
        {
            batchNumber = batchNo;
            locationId = LocationId;
        }
        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                cmbUnit_Leave(this, e);
            }
        }

        

        private void txtProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                txtProductCode_Leave(this, e);
            }
        }
 
        private void chkIsIncrementSelling_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void chkIsIncrementCost_CheckedChanged(object sender, EventArgs e)
        {
 
        }

        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                //txtProductName_Leave(this, e);
            }
        }

        private void txtProductName_Leave(object sender, EventArgs e)
        {
            try
            {
                //InvProductMasterService invProductMasterService = new InvProductMasterService();
                //UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                //InvProductPriceChangeService invProductPriceChangeService = new InvProductPriceChangeService();
                //InvProductBatchNoExpiaryDetail existingProBatchNo;
                //UnitOfMeasure existingUOM;

                //existingInvProductMaster = new InvProductMaster();
                //existingInvProductMaster = invProductMasterService.GetProductsByName(txtProductName.Text.Trim());

                //if (existingInvProductMaster != null)
                //{


                //    txtProductCode.Text = existingInvProductMaster.ProductCode;
                //    txtProductName.Text = existingInvProductMaster.ProductName;
                //    // existingUOM = unitOfMeasureService.GetUnitOfMeasureById(existingInvProductMaster.UnitOfMeasureID);
                //    //txtUom.Text = existingUOM.UnitOfMeasureName.Trim();
                //    //existingProBatchNo = invProductPriceChangeService.GetProductBatchNo(existingInvProductMaster.InvProductMasterID);
                //    //txtBatchNo.Text = existingProBatchNo.BatchNo.Trim();
                //    //txtLocation.Text = existingInvProductMaster.Location.LocationName;
                //    //txtCostPrice.Text = existingProBatchNo.CostPrice.ToString();
                //    //txtSellingPrice.Text = existingProBatchNo.SellingPrice.ToString();
                //    priceChangeTemp = new PriceChangeTemp();
                //    Common.EnableComboBox(true, cmbUnit);
                //    cmbUnit.Focus();
                //}
                //else
                //{
                //   // txtProductCode.Focus();
                //   // txtProductCode.SelectAll();
                //}

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDocumentNo_Leave(object sender, EventArgs e)
        {
            if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
            {
                RecallDocumentPriceChangeDamage(txtDocumentNo.Text.Trim());
            }
            else
            {
                txtDocumentNo.Text = GetDocumentNo(true);
                dtpEffectivDate.Focus();
            }
        }


        private bool RecallDocumentPriceChangeDamage(string documentNo)
        {
            try
            {
                invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
                existingInvProductPriceChangeDamage = new InvProductPriceChangeDetailDamage();

                InvProductPriceChangeHeaderDamage invProductPriceChangeHeaderDamage = new InvProductPriceChangeHeaderDamage();
                invProductPriceChangeHeaderDamage = invProductPriceChangeDamageService.GetPausedInvProductPriceChangeHeaderDamageByDocumentNo(documentID, txtDocumentNo.Text.Trim());

                if (invProductPriceChangeHeaderDamage.DocumentStatus == 1)
                {
                    Common.EnableButton(false, btnSave, btnPause);
                    Common.EnableComboBox(false, cmbDamageType,cmbTogDocument,cmbFromLocation);
                    dgvItemDetails.Enabled = false;
                    pnlUpdateSellingPrice.Enabled = false;
                }
                else
                {
                    Common.EnableButton(true, btnSave, btnPause);
                    Common.EnableComboBox(true, cmbDamageType, cmbTogDocument, cmbFromLocation);
                    dgvItemDetails.Enabled = true;
                    pnlUpdateSellingPrice.Enabled =true;
                }

                txtReferenceNo.Text = invProductPriceChangeHeaderDamage.ReferenceNo;
                cmbTogDocument.Text = invProductPriceChangeHeaderDamage.TogNO;
                DocumentStatus = invProductPriceChangeHeaderDamage.DocumentStatus;
                PriceChangeTempDamagelst = invProductPriceChangeDamageService.GetSavedDocumentDetailsByDocumentNumber(documentNo.Trim());
                dgvItemDetails.AutoGenerateColumns = false;
                dgvItemDetails.DataSource = PriceChangeTempDamagelst;
                dgvItemDetails.Refresh();
                txtTotalQty.Text = Common.ConvertDecimalToStringQty(PriceChangeTempDamagelst.GetSummaryAmount(x => x.Qty));
                if (dgvItemDetails.Rows.Count > 0)
                {
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

        private string GetDocumentNo(bool isTemporytNo)
        {
            try
            {
                InvProductPriceChangeDamageService invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
                LocationService locationService = new LocationService();
                return invProductPriceChangeDamageService.GetDocumentNo(this.Name, Common.LoggedLocationID,locationService.GetLocationsByID(Common.LoggedLocationID).LocationCode, documentID, isTemporytNo).Trim();
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
                 InvProductPriceChangeDamageService invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
                 PriceChangeTempDamagelst = invProductPriceChangeDamageService.GetSavedTogByDocumentNumber(documentNo.Trim());
                 dgvItemDetails.AutoGenerateColumns = false;
                 dgvItemDetails.DataSource = PriceChangeTempDamagelst;
                 dgvItemDetails.Refresh();

                 if (dgvItemDetails.Rows.Count > 0)
                 {
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

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtDocumentNo_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadPausedDocuments()
        {
            invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
            Common.SetAutoComplete(txtDocumentNo, invProductPriceChangeDamageService.GetPausedDocumentNumbers(), chkAutoCompleationPoNo.Checked);
        }

        private void chkAutoCompleationPoNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadPausedDocuments();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDocumentNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnlProduct_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkPriceDecreas_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtNewSellingPrice_ImeModeChanged(object sender, EventArgs e)
        {

        }
 

        private void btnGetReturnDocuments_Click(object sender, EventArgs e)
        {
             
        }

        private void txtNewProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
            {

                 InvProductMasterService invProductMasterService = new InvProductMasterService();
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                InvProductPriceChangeService invProductPriceChangeService = new InvProductPriceChangeService();
                InvProductBatchNoExpiaryDetail existingProBatchNo;
                UnitOfMeasure existingUOM;

                existingInvProductMaster = new InvProductMaster();
                if (!string.IsNullOrEmpty(txtNewProductCode.Text))
                {
                    existingInvProductMaster = invProductMasterService.GetProductsByRefCodes(txtNewProductCode.Text.Trim());

                    if (existingInvProductMaster != null)
                    {
                        txtNewProductCode.Text = existingInvProductMaster.ProductCode;
                        txtNewProductName.Text = existingInvProductMaster.ProductName;
                        txtQty.SelectAll();
                        txtQty.Focus();
                        txtQty.SelectAll();
                    }
                }
            }
        }

        private bool RecallReturnDocument(string documentNo)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                InvProductPriceChangeDamageService invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
                LocationService locationService = new Service.LocationService();
                InvTransferOfGoodsService invTransferOfGoodsService = new Service.InvTransferOfGoodsService();

 
                txtDocumentNo.Text = GetDocumentNo(true);

                dgvItemDetails.DataSource = null;

                PriceChangeTempDamagelst = invProductPriceChangeDamageService.GetSavedTogByDocumentNumber(cmbTogDocument.Text.Trim());
                txtTotalQty.Text = Common.ConvertDecimalToStringQty(PriceChangeTempDamagelst.GetSummaryAmount(x => x.Qty));

                dgvItemDetails.AutoGenerateColumns = false;
                dgvItemDetails.DataSource = PriceChangeTempDamagelst;
                dgvItemDetails.Refresh();

                Common.EnableTextBox(false, txtDocumentNo, txtProductCode, txtProductName);
                Common.EnableComboBox(false, cmbFromLocation);
 
                 
                //if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                //if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

                this.ActiveControl = txtProductCode;
                txtProductCode.Focus();
 

                this.Cursor = Cursors.Default;
                return true;

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                this.Cursor = Cursors.Default;
                return false;
            }
        }

        private void btnTogDetails_Click(object sender, EventArgs e)
        {
            if (!cmbTogDocument.Text.Trim().Equals(string.Empty))
            {
                if (RecallReturnDocument(cmbTogDocument.Text.Trim()))
                { cmbFromLocation.Enabled = false; pnlUpdateSellingPrice.Enabled = true; }
            }
        }

        private void cmbFromLocation_Validated(object sender, EventArgs e)
        {

        }

        private void cmbFromLocation_Leave(object sender, EventArgs e)
        {
          
        }

        private void cmbFromLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                LocationService locationService = new LocationService();
                Common.LoadTOGDocuments(cmbTogDocument, invTransferOfGoodsService.GetTOGDocuments(locationService.GetLocationsByName(cmbFromLocation.Text.Trim()).LocationID));
                cmbTogDocument.SelectedIndex = -1;

                cmbTogDocument.Enabled = true;
                 
            }
        }

        private void cmbFromLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void dgvItemDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F2))
            {
                try
                {
                    int currentRow = dgvItemDetails.CurrentCell.RowIndex;
                    if (dgvItemDetails.CurrentCell != null && dgvItemDetails.CurrentCell.RowIndex >= 0)
                    {
                        if (Toast.Show("Product " + dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + " - " + dgvItemDetails["ProductName", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                            return;

                        PriceChangeTempDamage priceChangeTempDamage = new PriceChangeTempDamage();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        InvProductMasterService invProductMasterService = new InvProductMasterService();

                        priceChangeTempDamage.ProductID = invProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).InvProductMasterID;
                        existingInvProductMaster = invProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim());
                        priceChangeTempDamage.UnitOfMeasureID = Common.ConvertStringToLong(dgvItemDetails["UOMID", dgvItemDetails.CurrentCell.RowIndex].Value.ToString());
                        priceChangeTempDamage.LineNo = Common.ConvertStringToLong(dgvItemDetails["LineNo", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim());
                        InvProductPriceChangeDamageService invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();


                        dgvItemDetails.DataSource = null;
                        PriceChangeTempDamagelst = invProductPriceChangeDamageService.GetDeletePriceChangeTemp(PriceChangeTempDamagelst,priceChangeTempDamage);
                        dgvItemDetails.DataSource = PriceChangeTempDamagelst;
                        dgvItemDetails.Refresh();

                        if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                        else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }

                        
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

        private void cmbDamageType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {


                if (!string.IsNullOrEmpty(cmbDamageType.Text))
                {
                    UpdateToGrid(priceChangeTempDamage);
                    Common.ClearCheckBox(chkUpdateCostPrice);
                    Common.ClearTextBox(txtProductCode,txtNewProductName, txtProductName, txtQty, txtSellingPrice, txtNewProductCode, txtNewSellingPrice, txtNewCostPrice, txtCostPrice, txtMrp);
                    Common.ClearComboBox(cmbUnit,cmbDamageType);
                    Common.EnableTextBox(false, txtQty, txtSellingPrice, txtCostPrice, txtNewProductCode, txtNewCostPrice, txtNewSellingPrice, txtMrp);
                    Common.EnableTextBox(true, txtProductCode, txtProductName);
                    txtProductCode.Focus();
                    return;
                }
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (priceChangeTempDamage.Qty < Common.ConvertStringToDecimal(txtQty.Text.Trim()))
                {
                    //txtQty.SelectAll();
                    //txtQty.Focus();
                    //txtQty.SelectAll();
                    cmbDamageType.Focus();
                }
                else
                {
                    cmbDamageType.Focus();
 
                }
 
            }
        }

        private void txtSellingDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Return))
                {
                    if (!string.IsNullOrEmpty(txtSellingDiscount.Text.Trim()))
                    {
                        if (!string.IsNullOrEmpty(cmbDepartment.Text.ToString()))
                        {
                            InvProductMasterService invProductMasterService = new InvProductMasterService();
                            InvDepartmentService invDepartmentService = new InvDepartmentService();
                            InvDepartment invDepartment = new InvDepartment();
                            invDepartment = invDepartmentService.GetInvDepartmentsByName(cmbDepartment.Text.ToString(), false);

                            if (invProductMasterService.GetAllProductCodesCount(invDepartment.InvDepartmentID, Common.ConvertStringToDecimal(txtSellingDiscount.Text.Trim())) != 1)
                            {
                                Common.SetAutoComplete(txtNewUProduct, invProductMasterService.GetAllProductCodes(invDepartment.InvDepartmentID, Common.ConvertStringToDecimal(txtSellingDiscount.Text.Trim())), true);
                                Common.EnableTextBox(true, txtNewProductCode);
                                txtNewProductCode.Focus();
                            }
                            else
                            {
                                InvProductMaster invProductMaster = new InvProductMaster();
                                invProductMaster = invProductMasterService.GetProductCode(invDepartment.InvDepartmentID, Common.ConvertStringToDecimal(txtNewSellingPrice.Text.Trim()));
                                txtNewUProduct.Text = invProductMaster.ProductCode;
                                txtNewUProName.Text = invProductMaster.ProductName;
                                 
                            }
                        }
                        else
                        {
                            Toast.Show("Department Not Selected.", Toast.messageType.Warning, Toast.messageAction.AccessDenied);
                            txtNewSellingPrice.SelectAll();
                            cmbDepartment.Focus();

                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

 


        }
 
    }
 
