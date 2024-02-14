using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Domain;
using Report;
using Report.Inventory;
using Report.Inventory.Reference.Reports;
using Utility;
using Service;
using System.Linq;
using System.IO;
namespace UI.Windows
{
    public partial class FrmProduct : UI.Windows.FrmBaseMasterForm
    {
        private InvProductMaster existingProduct;
        List<InvProductUnitConversion> invProductUnitConversionsGrid = new List<InvProductUnitConversion>();
        List<InvProductLink> invProductLinkGrid = new List<InvProductLink>();
        List<InvProductExtendedPropertyValue> invProductExtendedPropertyValueGrid = new List<InvProductExtendedPropertyValue>();
        List<InvProductSupplierLink> invProductSupplierLinkGrid = new List<InvProductSupplierLink>();
        List<ProductLocationInfoTemp> productLocationInfoTempList = new List<ProductLocationInfoTemp>();
        UserPrivileges accessRights = new UserPrivileges();
        UserPrivileges changeSellingPrice = new UserPrivileges();
        UserPrivileges showCostPrice = new UserPrivileges();

        int documentID = 0;
        string image;
        bool isDependCategory = false, isDependSubCategory = false, isDependSubCategory2 = false;
        bool isValidControls = true;
        bool codeDependOnDepartment, codeDependOnCategory, codeDependOnSubCategory, codeDependOnSubCategory2;

        public FrmProduct()
        {
            InitializeComponent();
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {

        }

        public override void FormLoad()
        {
            try
            {
                SystemFeature systemFeature = new SystemFeature();
                //systemFeature=
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;

                dgvUnitConversion.AutoGenerateColumns = false;
                dgvLocationInfo.AutoGenerateColumns = false;
                dgvExtendedProperties.AutoGenerateColumns = false;
                dgvDisplayInfo.AutoGenerateColumns = false;
                dgvExistingProducts.AutoGenerateColumns = false;

                dgvUnitConversion.AllowUserToAddRows = true;
                dgvLocationInfo.AllowUserToAddRows = false;
                dgvLocationInfo.AllowUserToDeleteRows = false;
                dgvPluLink.AllowUserToAddRows = true;
                dgvPluLink.AutoGenerateColumns = false;
                dgvMultiSupplier.AutoGenerateColumns = false;
                dgvExtendedProperties.AllowUserToAddRows = false;

                this.CausesValidation = false;
                lblDepartment.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText + "*";
                isDependCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend;
                lblCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText + "*";
                isDependSubCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend;
                lblSubCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText + "*";
                isDependSubCategory2 = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").IsDepend;
                lblSubCategory2.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText + "*";

                InvDepartmentService invDepartmentService = new Service.InvDepartmentService();
                Common.SetAutoComplete(txtDepartmentCode, invDepartmentService.GetInvDepartmentCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend), chkAutoCompleationDepartment.Checked);
                Common.SetAutoComplete(txtDepartmentDescription, invDepartmentService.GetInvDepartmentNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend), chkAutoCompleationDepartment.Checked);

                InvCategoryService invCategoryService = new Service.InvCategoryService();
                Common.SetAutoComplete(txtCategoryCode, invCategoryService.GetInvCategoryCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend), chkAutoCompleationCategory.Checked);
                Common.SetAutoComplete(txtCategoryDescription, invCategoryService.GetInvCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend), chkAutoCompleationCategory.Checked);

                InvSubCategoryService invSubCategoryService = new Service.InvSubCategoryService();
                Common.SetAutoComplete(txtSubCategoryCode, invSubCategoryService.GetInvSubCategoryCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").IsDepend), chkAutoCompleationSubCategory.Checked);
                Common.SetAutoComplete(txtSubCategoryDescription, invSubCategoryService.GetInvSubCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").IsDepend), chkAutoCompleationSubCategory.Checked);

                InvSubCategory2Service invSubCategory2Service = new Service.InvSubCategory2Service();
                Common.SetAutoComplete(txtSubCategory2Code, invSubCategory2Service.GetInvSubCategory2Codes(), chkAutoCompleationSubCategory2.Checked);
                Common.SetAutoComplete(txtSubCategory2Description, invSubCategory2Service.GetInvSubCategory2Names(), chkAutoCompleationSubCategory2.Checked);

                SupplierService supplierService = new Service.SupplierService();
                Common.SetAutoComplete(txtMainSupplierCode, supplierService.GetSupplierCodes(), chkAutoCompleationMainSupplier.Checked);
                Common.SetAutoComplete(txtMainSupplierDescription, supplierService.GetSupplierNames(), chkAutoCompleationMainSupplier.Checked);

                Common.SetAutoComplete(txtSupplierLinkSupplierCode, supplierService.GetSupplierCodes(), true);
                Common.SetAutoComplete(txtSupplierLinkSupplierName, supplierService.GetSupplierNames(), true);

                //AccLedgerAccountService AccLedgerAccountService = new Service.AccLedgerAccountService();
                //Common.SetAutoComplete(txtPurchaseLedgerCode, AccLedgerAccountService.GetLedgerCodes(), chkAutoCompleationPurchaseLedger.Checked);
                //Common.SetAutoComplete(txtPurchaseLedgerName, AccLedgerAccountService.GetLedgerNames(), chkAutoCompleationPurchaseLedger.Checked);

                //Common.SetAutoComplete(txtOtherPurchaseLedgerCode, AccLedgerAccountService.GetLedgerCodes(), chkAutoCompleationOtherPurchaseLedger.Checked);
                //Common.SetAutoComplete(txtOtherPurchaseLedgerName, AccLedgerAccountService.GetLedgerNames(), chkAutoCompleationOtherPurchaseLedger.Checked);

                //Common.SetAutoComplete(txtSalesLedgerCode, AccLedgerAccountService.GetLedgerCodes(), chkAutoCompleationSalesLedger.Checked);
                //Common.SetAutoComplete(txtSalesLedgerName, AccLedgerAccountService.GetLedgerNames(), chkAutoCompleationSalesLedger.Checked);

                //Common.SetAutoComplete(txtOtherSalesLedgerCode, AccLedgerAccountService.GetLedgerCodes(), chkAutoCompleationOtherSalesLedger.Checked);
                //Common.SetAutoComplete(txtOtherSalesLedgerName, AccLedgerAccountService.GetLedgerNames(), chkAutoCompleationOtherSalesLedger.Checked);

                UnitOfMeasureService unitOfMeasureService = new Service.UnitOfMeasureService();
                Common.LoadUnitOfMeasures(cmbUnit, unitOfMeasureService.GetAllUnitOfMeasures());
                Common.LoadUnitOfMeasures(cmbPackSizeUnit, unitOfMeasureService.GetAllUnitOfMeasures());
                Common.LoadUnitOfMeasures(cmbUnitTbpUnit, unitOfMeasureService.GetAllUnitOfMeasures());

                cmbUnit.SelectedIndex = 0;
                cmbPackSizeUnit.SelectedIndex = 0;
                cmbUnitTbpUnit.SelectedIndex = 0;

                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                Common.SetAutoBindRecords(cmbCostingMethod, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.CostingMethod).ToString()));

                ProductCodeDependancy productCodeDependancy = new ProductCodeDependancy();
                ProductCodeDependancyService productCodeDependancyService = new ProductCodeDependancyService();
                productCodeDependancy = productCodeDependancyService.GetProductCodeDependancyByForm(this.Name);

                codeDependOnDepartment = productCodeDependancy.DependOnDepartment;
                codeDependOnCategory = productCodeDependancy.DependOnCategory;
                codeDependOnSubCategory = productCodeDependancy.DependOnSubCategory;
                codeDependOnSubCategory2 = productCodeDependancy.DependOnSubCategory2;

                //AddCustomCoulmnstoGridUnitConversion();
                //AddCustomCoulmnPropertyNametoGridExtendedProperties();

                InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                Common.SetAutoComplete(txtPropertyName, invProductExtendedPropertyService.GetInvProductExtendedPropertyNames(), true);


                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;
                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                //changeSellingPrice = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, 19001);
                //showCostPrice = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, 19002);

                //if (showCostPrice.IsAccess == true) { lblHideCostPrice.Visible = false; } else { lblHideCostPrice.Visible = true; }

                base.FormLoad();

                LoadDefaultLocation();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void InitializeForm()
        {
            InvProductMasterService invProductMasterService = new InvProductMasterService();

            Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
            Common.SetAutoComplete(txtProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
            Common.EnableButton(true, btnNew);
            Common.EnableButton(false, btnSave, btnDelete, btnView);
            Common.EnableTextBox(true, txtNameOnInvoice);
            Common.EnableTextBox(true, txtProductCode, txtDepartmentCode, txtCategoryCode, txtSubCategoryCode, txtSubCategory2Code);
            Common.EnableTextBox(true, txtDepartmentDescription, txtCategoryDescription, txtSubCategoryDescription, txtSubCategory2Description);
            Common.ClearTextBox(txtProductCode);
            //cmbCostingMethod.SelectedIndex = -1;

            if (accessRights.IsView == true) Common.EnableButton(true, btnView);

            lblCreatedDate.Text = string.Empty;
            lblCreatedBy.Text = string.Empty;
            lblModifiedDate.Text = string.Empty;
            lblModifiedBy.Text = string.Empty;

            txtSellingPrice.Enabled = true;

            //invProductUnitConversionsGrid = null;
            //invProductLinkGrid = null;
            //invProductExtendedPropertyValueGrid = null;
            //invProductSupplierLinkGrid = null;
            //productLocationInfoTempList = null;

            //invProductUnitConversionsGrid = new List<InvProductUnitConversion>();
            //invProductLinkGrid = new List<InvProductLink>();
            //invProductExtendedPropertyValueGrid = new List<InvProductExtendedPropertyValue>();
            //invProductSupplierLinkGrid = new List<InvProductSupplierLink>();
            //productLocationInfoTempList = new List<ProductLocationInfoTemp>();

            pbProductImage.Image = UI.Windows.Properties.Resources.Default_Product;

            CommonService commonService = new CommonService();
            cmbCostingMethod.Text = commonService.GetCostingMethodByLocation(Common.LoggedLocationID);

            tabProduct.SelectedTab = tbpGneral;
            
            ActiveControl = txtProductCode;
            txtProductCode.Focus();
        }

        private void chkAutoCompleationProduct_CheckedChanged(object sender, EventArgs e)
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

        private void chkAutoCompleationDepartment_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvDepartmentService invDepartmentService = new Service.InvDepartmentService();
                Common.SetAutoComplete(txtDepartmentCode, invDepartmentService.GetInvDepartmentCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend), chkAutoCompleationDepartment.Checked);
                Common.SetAutoComplete(txtDepartmentDescription, invDepartmentService.GetInvDepartmentNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend), chkAutoCompleationDepartment.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationCategory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvCategoryService invCategoryService = new Service.InvCategoryService();
                Common.SetAutoComplete(txtCategoryCode, invCategoryService.GetInvCategoryCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend), chkAutoCompleationCategory.Checked);
                Common.SetAutoComplete(txtCategoryDescription, invCategoryService.GetInvCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend), chkAutoCompleationCategory.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationMainSupplier_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SupplierService supplierService = new Service.SupplierService();
                Common.SetAutoComplete(txtSubCategoryCode, supplierService.GetSupplierCodes(), chkAutoCompleationMainSupplier.Checked);
                Common.SetAutoComplete(txtSubCategoryDescription, supplierService.GetSupplierNames(), chkAutoCompleationMainSupplier.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationPurchaseLedger_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService AccLedgerAccountService = new Service.AccLedgerAccountService();
                Common.SetAutoComplete(txtPurchaseLedgerCode, AccLedgerAccountService.GetLedgerCodes(), chkAutoCompleationPurchaseLedger.Checked);
                Common.SetAutoComplete(txtPurchaseLedgerName, AccLedgerAccountService.GetLedgerNames(), chkAutoCompleationPurchaseLedger.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationSalesLedger_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService AccLedgerAccountService = new Service.AccLedgerAccountService();
                Common.SetAutoComplete(txtSalesLedgerCode, AccLedgerAccountService.GetLedgerCodes(), chkAutoCompleationSalesLedger.Checked);
                Common.SetAutoComplete(txtSalesLedgerName, AccLedgerAccountService.GetLedgerNames(), chkAutoCompleationSalesLedger.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationOtherPurchaseLedger_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService AccLedgerAccountService = new Service.AccLedgerAccountService();
                Common.SetAutoComplete(txtOtherPurchaseLedgerCode, AccLedgerAccountService.GetLedgerCodes(), chkAutoCompleationOtherPurchaseLedger.Checked);
                Common.SetAutoComplete(txtOtherPurchaseLedgerName, AccLedgerAccountService.GetLedgerNames(), chkAutoCompleationOtherPurchaseLedger.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationOtherSalesLedger_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService AccLedgerAccountService = new Service.AccLedgerAccountService();
                Common.SetAutoComplete(txtOtherSalesLedgerCode, AccLedgerAccountService.GetLedgerCodes(), chkAutoCompleationOtherSalesLedger.Checked);
                Common.SetAutoComplete(txtOtherSalesLedgerName, AccLedgerAccountService.GetLedgerNames(), chkAutoCompleationOtherSalesLedger.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateControls()
        {
            if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtProductCode, txtProductName, txtNameOnInvoice, txtDepartmentCode, txtCategoryCode, txtSubCategoryCode, txtSubCategory2Code, txtMainSupplierCode, txtCostPrice, txtSellingPrice, txtMaximumPrice, txtMinimumPrice))
            { return false; }
            else if (!Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbUnit))
            { return false; }
            //else if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Zero, txtPackSize))
            //{ return false; }

            else
            {
                isValidControls = true;
                this.ValidateChildren();

                return isValidControls;
            }
        }


        private void FillProduct()
        {
            InvDepartmentService invDepartmentService = new Service.InvDepartmentService();
            InvCategoryService invCategoryService = new Service.InvCategoryService();
            InvSubCategoryService invSubCategoryService = new Service.InvSubCategoryService();
            InvSubCategory2Service invSubCategory2Service = new Service.InvSubCategory2Service();
            SupplierService supplierService = new Service.SupplierService();

            existingProduct.ProductCode = txtProductCode.Text.Trim();
            existingProduct.BarCode = txtBarCode.Text.Trim();
            existingProduct.ReferenceCode1 = txtReferenceCode1.Text.Trim();
            existingProduct.ReferenceCode2 = txtReferenceCode2.Text.Trim();
            existingProduct.ProductName = txtProductName.Text.Trim();
            existingProduct.NameOnInvoice = txtNameOnInvoice.Text.Trim();
            existingProduct.DepartmentID = invDepartmentService.GetInvDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDependCategory).InvDepartmentID;
            existingProduct.CategoryID = invCategoryService.GetInvCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory).InvCategoryID;
            existingProduct.SubCategoryID = invSubCategoryService.GetInvSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2).InvSubCategoryID;
            existingProduct.SubCategory2ID = invSubCategory2Service.GetInvSubCategory2ByCode(txtSubCategory2Code.Text.Trim()).InvSubCategory2ID;
            existingProduct.SupplierID = supplierService.GetSupplierByCode(txtMainSupplierCode.Text.Trim()).SupplierID;
            

            existingProduct.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
            existingProduct.PackSize = txtPackSize.Text.Trim();

            //if (@image != null)
            //{
            //    FileStream fs;
            //    fs = new FileStream(@image, FileMode.Open, FileAccess.Read);

            //    byte[] picbyte = new byte[fs.Length];
            //    fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
            //    fs.Close();
            //    existingProduct.ProductImage = picbyte;
            //}
            //else
            //{
            //    existingProduct.ProductImage = null;
            //}

            MemoryStream stream = new MemoryStream();
            pbProductImage.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] pic = stream.ToArray();
            existingProduct.ProductImage = pic;

            existingProduct.CostingMethod = cmbCostingMethod.Text.Trim();
            existingProduct.CostPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim());
            if (existingProduct.InvProductMasterID.Equals(0))
                existingProduct.AverageCost = existingProduct.CostPrice;
            existingProduct.SellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text.Trim());
            existingProduct.WholesalePrice = Common.ConvertStringToDecimalCurrency(txtWholesalePrice.Text.Trim());
            existingProduct.MinimumPrice = Common.ConvertStringToDecimalCurrency(txtMinimumPrice.Text.Trim());
            existingProduct.FixedDiscount = Common.ConvertStringToDecimalCurrency(txtFixedDiscount.Text.Trim());
            existingProduct.MaximumDiscount = Common.ConvertStringToDecimalCurrency(txtMaximumDiscount.Text.Trim());
            existingProduct.MaximumPrice = Common.ConvertStringToDecimalCurrency(txtMaximumPrice.Text.Trim());
            existingProduct.FixedDiscountPercentage = Common.ConvertStringToDecimal(txtFixedDiscountPercentage.Text.Trim());
            existingProduct.MaximumDiscountPercentage = Common.ConvertStringToDecimal(txtMaximumDiscountPercentage.Text.Trim());
            existingProduct.ReOrderLevel = Common.ConvertStringToDecimalQty(txtReOrderLevel.Text.Trim());
            existingProduct.ReOrderQty = Common.ConvertStringToDecimalQty(txtReOrderQty.Text.Trim());
            existingProduct.ReOrderPeriod = Common.ConvertStringToDecimalQty(txtReOrderPeriod.Text.Trim());
            existingProduct.IsActive = chkActive.Checked;
            existingProduct.IsBatch = chkBatch.Checked;
            existingProduct.IsBundle = chkBundle.Checked;
            existingProduct.IsConsignment = chkConsignment.Checked;
            existingProduct.IsCountable = chkCountable.Checked;
            //existingProduct.IsDCS = chkdActive.Checked;
            existingProduct.IsDrayage = chkDrayage.Checked;
            existingProduct.IsExpiry = chkExpiry.Checked;
            existingProduct.IsFreeIssue = chkFreeIssue.Checked;
            existingProduct.IsPromotion = chkPromotion.Checked;
            existingProduct.IsSerial = chkSerial.Checked;
            existingProduct.IsTax = chkTax.Checked;

            existingProduct.OrderPrice = Common.ConvertStringToDecimalCurrency(txtOrderPrice.Text.Trim());

            existingProduct.CostingMethod = cmbCostingMethod.Text;
            existingProduct.DrayagePercentage = Convert.ToDecimal(txtDrayagePercentage.Text.Trim());
            existingProduct.PackSizeUnitOfMeasureID = Common.ConvertStringToLong(cmbPackSizeUnit.SelectedValue.ToString());
            existingProduct.Margin = Common.ConvertStringToDecimal(txtMargin.Text.Trim());
            existingProduct.WholesaleMargin = Common.ConvertStringToDecimal(txtWholesaleMargin.Text.Trim());
            existingProduct.FixedGP = Common.ConvertStringToDecimal(txtFixedGP.Text.Trim());

            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
            AccLedgerAccount PurchaseLedger = new AccLedgerAccount();
            AccLedgerAccount SalesLedger = new AccLedgerAccount();
            AccLedgerAccount OtherPurchaseLedger = new AccLedgerAccount();
            AccLedgerAccount OtherSalesLedger = new AccLedgerAccount();

            PurchaseLedger = accLedgerAccountService.GetAccLedgerAccountByCode(txtPurchaseLedgerCode.Text.Trim());
            if (PurchaseLedger != null) { existingProduct.PurchaseLedgerID = PurchaseLedger.AccLedgerAccountID; }
            else { existingProduct.PurchaseLedgerID = 0; }

            SalesLedger = accLedgerAccountService.GetAccLedgerAccountByCode(txtSalesLedgerCode.Text.Trim());
            if (SalesLedger != null) { existingProduct.SalesLedgerID = SalesLedger.AccLedgerAccountID; }
            else { existingProduct.SalesLedgerID = 0; }

            OtherPurchaseLedger = accLedgerAccountService.GetAccLedgerAccountByCode(txtOtherPurchaseLedgerCode.Text.Trim());
            if (OtherPurchaseLedger != null) { existingProduct.OtherPurchaseLedgerID = OtherPurchaseLedger.AccLedgerAccountID; }
            else { existingProduct.OtherPurchaseLedgerID = 0; }

            OtherSalesLedger = accLedgerAccountService.GetAccLedgerAccountByCode(txtOtherSalesLedgerCode.Text.Trim());
            if (OtherPurchaseLedger != null) { existingProduct.OtherSalesLedgerID = OtherSalesLedger.AccLedgerAccountID; }
            else { existingProduct.OtherSalesLedgerID = 0; }
            
        }

        private void SaveProductExtendedValues()
        {
            InvProductExtendedPropertyValueService invProductExtendedPropertyValueService = new InvProductExtendedPropertyValueService();
            InvProductMasterService InvProductMasterService = new InvProductMasterService();
            existingProduct = InvProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

            //List<InvProductExtendedPropertyValue> extendedPropertyValueList = new List<InvProductExtendedPropertyValue>();

            if (existingProduct != null)
            {
                invProductExtendedPropertyValueService.UpdateProductExtendedPropertyValueStatus(existingProduct.InvProductMasterID);

                foreach (InvProductExtendedPropertyValue temp in invProductExtendedPropertyValueGrid)
                {
                    InvProductExtendedPropertyValue existingInvProductExtendedPropertyValue = new InvProductExtendedPropertyValue();

                    existingInvProductExtendedPropertyValue.ProductID = existingProduct.InvProductMasterID;
                    existingInvProductExtendedPropertyValue.InvProductExtendedPropertyID = temp.InvProductExtendedPropertyID;
                    existingInvProductExtendedPropertyValue.InvProductExtendedValueID = temp.InvProductExtendedValueID;
                    existingInvProductExtendedPropertyValue.IsDelete = false;

                    invProductExtendedPropertyValueService.AddInvProductExtendedPropertyValues(existingInvProductExtendedPropertyValue);
                }
            }
            
        }


        private void SaveUnitConversion()
        {
            InvProductUnitConversionService invProductUnitConversionService = new InvProductUnitConversionService();
            InvProductMasterService InvProductMasterService = new InvProductMasterService();
            existingProduct = InvProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

            if (existingProduct != null)
            {
                invProductUnitConversionService.UpdateProductUnitConversionStatus(existingProduct.InvProductMasterID);

                foreach (InvProductUnitConversion temp in invProductUnitConversionsGrid)
                {
                    InvProductUnitConversion existinginvProductUnitConversion = new InvProductUnitConversion();
                    existinginvProductUnitConversion = invProductUnitConversionService.GetProductUnitWithDeletedByProductCode(existingProduct.InvProductMasterID, temp.UnitOfMeasureID);

                    if (existinginvProductUnitConversion == null || existinginvProductUnitConversion.InvProductUnitConversionID == 0)
                    {
                        existinginvProductUnitConversion = new InvProductUnitConversion();
                    }

                    existinginvProductUnitConversion.ProductID = existingProduct.InvProductMasterID;
                    existinginvProductUnitConversion.Description = temp.Description;
                    existinginvProductUnitConversion.UnitOfMeasureID = temp.UnitOfMeasureID;
                    existinginvProductUnitConversion.ConvertFactor = temp.ConvertFactor;
                    existinginvProductUnitConversion.SellingPrice = temp.SellingPrice;
                    existinginvProductUnitConversion.MinimumPrice = temp.SellingPrice;
                    existinginvProductUnitConversion.CostPrice = temp.CostPrice;
                    existinginvProductUnitConversion.IsDelete = false;

                    if (existinginvProductUnitConversion.InvProductUnitConversionID.Equals(0))
                    {
                        invProductUnitConversionService.AddProductUnitConversion(existinginvProductUnitConversion);
                    }
                    else
                    {
                        invProductUnitConversionService.AddProductUnitConversion(existinginvProductUnitConversion);
                    }
                }
            }
        }

        //private void SaveUnitConversion()
        //{
        //    InvProductMasterService InvProductMasterService = new InvProductMasterService();

        //    existingProduct = InvProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

        //    if (existingProduct != null)
        //    {
        //        InvProductUnitConversionService InvProductUnitConversionService = new InvProductUnitConversionService();
        //        InvProductUnitConversion existinginvProductUnitConversion = new InvProductUnitConversion();

        //        InvProductUnitConversionService.UpdateProductUnitConversionStatus(existingProduct.InvProductMasterID);
        //        if (dgvUnitConversion.Rows.Count > 0)
        //        {
        //            foreach (DataGridViewRow row in dgvUnitConversion.Rows)
        //            {
        //                if (dgvUnitConversion["tbpUnitConversionFactor", row.Index].Value != null && !Common.ConvertStringToDecimal(dgvUnitConversion["tbpUnitConversionFactor", row.Index].Value.ToString().Trim()).Equals(0))
        //                {
        //                    existinginvProductUnitConversion = InvProductUnitConversionService.GetProductUnitWithDeletedByProductCode(existingProduct.InvProductMasterID, Common.ConvertStringToLong(dgvUnitConversion[1, row.Index].Value.ToString().Trim()));

        //                    if (existinginvProductUnitConversion == null || existinginvProductUnitConversion.InvProductUnitConversionID == 0)
        //                    {
        //                        existinginvProductUnitConversion = new InvProductUnitConversion();
        //                    }

        //                    existinginvProductUnitConversion.ProductID = existingProduct.InvProductMasterID;
        //                    existinginvProductUnitConversion.Description = dgvUnitConversion["tbpUnitConversionDescription", row.Index].Value.ToString().Trim();
        //                    existinginvProductUnitConversion.UnitOfMeasureID = Common.ConvertStringToLong(dgvUnitConversion["tbpUnitConversionUnit", row.Index].Value.ToString().Trim());
        //                    existinginvProductUnitConversion.ConvertFactor = Common.ConvertStringToDecimal(dgvUnitConversion["tbpUnitConversionFactor", row.Index].Value.ToString().Trim());
        //                    existinginvProductUnitConversion.SellingPrice = Common.ConvertStringToDecimal(dgvUnitConversion["tbpUnitConversionSellingPrice", row.Index].Value.ToString().Trim());
        //                    existinginvProductUnitConversion.MinimumPrice = Common.ConvertStringToDecimal(dgvUnitConversion["tbpUnitConversionMinimumPrice", row.Index].Value.ToString().Trim());
        //                    existinginvProductUnitConversion.CostPrice = Common.ConvertStringToDecimal(dgvUnitConversion["tbUnitConversionCostPrice", row.Index].Value.ToString().Trim());
        //                    existinginvProductUnitConversion.IsDelete = false;

        //                    if (existinginvProductUnitConversion.InvProductUnitConversionID.Equals(0))
        //                    {
        //                        InvProductUnitConversionService.AddProductUnitConversion(existinginvProductUnitConversion);
        //                    }
        //                    else
        //                    {
        //                        InvProductUnitConversionService.UpdateProductUnitConversion(existinginvProductUnitConversion);
        //                    }

        //                }
        //            }
        //        }
        //    }
        //}

        private void SaveProductLink()
        {
            if (existingProduct != null)
            {
                InvProductLinkService invProductLinkService = new InvProductLinkService();
                InvProductLink existingInvProductLink = new InvProductLink();

                invProductLinkService.UpdateProductLinkStatus(existingProduct.InvProductMasterID);
                if (dgvPluLink.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvPluLink.Rows)
                    {

                        if (dgvPluLink["tbpPluLinkCode", row.Index].Value != null )
                        {

                            existingInvProductLink = invProductLinkService.GetProductLinkWithDeletedByProductCode(existingProduct.InvProductMasterID, dgvPluLink["tbpPluLinkCode", row.Index].Value.ToString().Trim());



                            if (existingInvProductLink == null || existingInvProductLink.InvProductLinkID == 0)
                            { existingInvProductLink = new InvProductLink(); }



                            existingInvProductLink.ProductID = existingProduct.InvProductMasterID;
                            existingInvProductLink.ProductLinkCode = dgvPluLink["tbpPluLinkCode", row.Index].Value.ToString().Trim();
                            existingInvProductLink.ProductLinkName = dgvPluLink["tbpPluLinkDescription", row.Index].Value.ToString().Trim();

                            existingInvProductLink.IsDelete = false;

                            if (existingInvProductLink.InvProductLinkID.Equals(0))
                            {

                                invProductLinkService.AddProductLink(existingInvProductLink);

                            }
                            else
                            {

                                invProductLinkService.UpdateProductLink(existingInvProductLink);

                            }
                        }
                    }

                }
            }
        }


        //private void SaveSupplierLink()
        //{
        //    InvProductSupplierLinkService invProductSupplierLinkService = new InvProductSupplierLinkService();
        //    InvProductSupplierLink existingInvProductSupplierLink = new InvProductSupplierLink();
        //    SupplierService supplierService = new SupplierService();
        //    Supplier existingSupplier = new Supplier();

        //    InvProductMasterService InvProductMasterService = new InvProductMasterService();
        //    existingProduct = InvProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

        //    //invProductSupplierLinkService.UpdateInvProductSupplierLinkStatus(existingProduct.InvProductMasterID);

        //    if (existingInvProductSupplierLink == null || existingInvProductSupplierLink.InvProductSupplierLinkID == 0)
        //    {
        //        existingInvProductSupplierLink = new InvProductSupplierLink();
        //    }

        //    foreach (InvProductSupplierLink temp in invProductSupplierLinkGrid)  
        //    {
        //        existingInvProductSupplierLink.ProductID = existingProduct.InvProductMasterID;
        //        existingInvProductSupplierLink.SupplierID = temp.SupplierID;
        //        existingInvProductSupplierLink.CostPrice = temp.CostPrice;
        //        existingInvProductSupplierLink.FixedGP = temp.FixedGP;
        //        existingInvProductSupplierLink.ReferenceDocumentNo = string.Empty;
        //        existingInvProductSupplierLink.DocumentDate = DateTime.Now;
        //        existingInvProductSupplierLink.IsDelete = false;
        //    }

        //    if (existingInvProductSupplierLink.InvProductSupplierLinkID.Equals(0))
        //    {
        //        invProductSupplierLinkService.AddInvProductSupplierLink(existingInvProductSupplierLink);
        //    }
        //    else
        //    {
        //        invProductSupplierLinkService.UpdateInvProductSupplierLink(existingInvProductSupplierLink);
        //    }
        //}

        private void SaveSupplierLink()
        {
            if (existingProduct != null)
            {
                InvProductSupplierLinkService invProductSupplierLinkService = new InvProductSupplierLinkService();
                InvProductSupplierLink existingInvProductSupplierLink = new InvProductSupplierLink();
                SupplierService supplierService = new SupplierService();
                Supplier existingSupplier = new Supplier();
                invProductSupplierLinkService.UpdateInvProductSupplierLinkStatus(existingProduct.InvProductMasterID);
                if (dgvMultiSupplier.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvMultiSupplier.Rows)
                    {
                        if (dgvMultiSupplier["tbpMultiSupplierCode", row.Index].Value != null)
                        {
                            existingSupplier = supplierService.GetSupplierByCode(dgvMultiSupplier["tbpMultiSupplierCode", row.Index].Value.ToString().Trim());
                            existingInvProductSupplierLink = invProductSupplierLinkService.GetInvProductSupplierLinkWithDeletedByProductCode(existingProduct.InvProductMasterID, existingSupplier.SupplierID);

                            if (existingInvProductSupplierLink == null || existingInvProductSupplierLink.InvProductSupplierLinkID == 0)
                            { existingInvProductSupplierLink = new InvProductSupplierLink(); }

                            existingInvProductSupplierLink.ProductID = existingProduct.InvProductMasterID;
                            existingInvProductSupplierLink.SupplierID = existingSupplier.SupplierID;
                            existingInvProductSupplierLink.CostPrice = Common.ConvertStringToDecimalCurrency(dgvMultiSupplier["tbsupplinkCostPrice", row.Index].Value.ToString().Trim());
                            existingInvProductSupplierLink.FixedGP = Common.ConvertStringToDecimal(dgvMultiSupplier["FixedGP", row.Index].Value.ToString().Trim());
                            existingInvProductSupplierLink.ReferenceDocumentNo = string.Empty;
                            existingInvProductSupplierLink.DocumentDate = DateTime.Now;
                            existingInvProductSupplierLink.IsDelete = false;

                            if (existingInvProductSupplierLink.InvProductSupplierLinkID.Equals(0))
                            {
                                invProductSupplierLinkService.AddInvProductSupplierLink(existingInvProductSupplierLink);
                            }
                            else
                            {
                                invProductSupplierLinkService.UpdateInvProductSupplierLink(existingInvProductSupplierLink);
                            }
                        }
                    }
                }
            }
        }

        private void SaveLocationInfo()
        {
            if (existingProduct != null)
            {
                InvProductStockMasterService invProductStockMasterService = new InvProductStockMasterService();
                InvProductStockMaster existinginvInvProductStockMaster = new InvProductStockMaster();

                if (dgvLocationInfo.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvLocationInfo.Rows)
                    {
                        if (dgvLocationInfo["Location", row.Index].Value != null && !dgvLocationInfo["Location", row.Index].Value.ToString().Trim().Equals(string.Empty))
                        {
                            existinginvInvProductStockMaster = invProductStockMasterService.GetProductStockMasterWithDeletedByProductID(existingProduct.InvProductMasterID, Common.ConvertStringToInt(dgvLocationInfo["LocationID", row.Index].Value.ToString().Trim()));

                            if (existinginvInvProductStockMaster == null || existinginvInvProductStockMaster.InvProductStockMasterID == 0)
                            { existinginvInvProductStockMaster = new InvProductStockMaster(); }

                            Location location = new Location();
                            LocationService locationService = new LocationService();
                            location = locationService.GetLocationsByID(Common.LoggedLocationID);

                            if (location != null) { existinginvInvProductStockMaster.CostCentreID = location.CostCentreID; }
                            else { existinginvInvProductStockMaster.CostCentreID = 0; }

                            existinginvInvProductStockMaster.CompanyID = Common.LoggedCompanyID;
                            existinginvInvProductStockMaster.LocationID = Common.ConvertStringToInt(dgvLocationInfo["LocationID", row.Index].Value.ToString().Trim());
                            
                            existinginvInvProductStockMaster.ProductID = existingProduct.InvProductMasterID;

                            if (dgvLocationInfo["Selection", row.Index].Value == null || Common.ConvertStringToBool(dgvLocationInfo["Selection", row.Index].Value.ToString().Trim()).Equals(false))
                            {
                                existinginvInvProductStockMaster.IsDelete = true;
                                existinginvInvProductStockMaster.ReOrderLevel = 0;
                                existinginvInvProductStockMaster.ReOrderQuantity = 0;
                                existinginvInvProductStockMaster.ReOrderPeriod = 0;

                                existinginvInvProductStockMaster.CostPrice = 0;
                                existinginvInvProductStockMaster.SellingPrice = 0;
                                existinginvInvProductStockMaster.MinimumPrice = 0;
                            }
                            else
                            {
                                existinginvInvProductStockMaster.IsDelete = false;

                                if (dgvLocationInfo["ReLevel", row.Index].Value == null) { existinginvInvProductStockMaster.ReOrderLevel = Common.ConvertStringToDecimal("0.00"); }
                                else
                                {
                                    if (dgvLocationInfo["ReLevel", row.Index].Value.ToString() != string.Empty)
                                        existinginvInvProductStockMaster.ReOrderLevel = Common.ConvertStringToDecimal(dgvLocationInfo["ReLevel", row.Index].Value.ToString().Trim());
                                    else
                                        existinginvInvProductStockMaster.ReOrderLevel = Common.ConvertStringToDecimal("0.00");
                                }

                                if (dgvLocationInfo["ReQty", row.Index].Value == null) { existinginvInvProductStockMaster.ReOrderQuantity = Common.ConvertStringToDecimal("0.00"); }
                                else
                                {
                                    if (dgvLocationInfo["ReQty", row.Index].Value.ToString().Trim() != string.Empty)
                                        existinginvInvProductStockMaster.ReOrderQuantity = Common.ConvertStringToDecimal(dgvLocationInfo["ReQty", row.Index].Value.ToString().Trim());
                                    else
                                        existinginvInvProductStockMaster.ReOrderQuantity = Common.ConvertStringToDecimal("0.00");
                                }

                                if (dgvLocationInfo["RePeriod", row.Index].Value == null) { existinginvInvProductStockMaster.ReOrderPeriod = Common.ConvertStringToDecimal("0.00"); }
                                else
                                {
                                    if (dgvLocationInfo["RePeriod", row.Index].Value.ToString() != string.Empty)
                                        existinginvInvProductStockMaster.ReOrderPeriod = Common.ConvertStringToDecimal(dgvLocationInfo["RePeriod", row.Index].Value.ToString().Trim());
                                    else
                                        existinginvInvProductStockMaster.ReOrderPeriod = Common.ConvertStringToDecimal("0.00");
                                }

                                if (dgvLocationInfo["CostPrice", row.Index].Value == null) { existinginvInvProductStockMaster.CostPrice = Common.ConvertStringToDecimal("0.00"); }
                                else
                                {
                                    if (dgvLocationInfo["CostPrice", row.Index].Value.ToString() != null)
                                        existinginvInvProductStockMaster.CostPrice = Common.ConvertStringToDecimal(dgvLocationInfo["CostPrice", row.Index].Value.ToString().Trim());
                                    else
                                        existinginvInvProductStockMaster.CostPrice = Common.ConvertStringToDecimal("0.00");
                                }

                                if (dgvLocationInfo["SellingPrice", row.Index].Value == null) { existinginvInvProductStockMaster.SellingPrice = Common.ConvertStringToDecimal("0.00"); }
                                else
                                {
                                    if (dgvLocationInfo["SellingPrice", row.Index].Value.ToString() != string.Empty)
                                        existinginvInvProductStockMaster.SellingPrice = Common.ConvertStringToDecimal(dgvLocationInfo["SellingPrice", row.Index].Value.ToString().Trim());
                                    else
                                        existinginvInvProductStockMaster.SellingPrice = Common.ConvertStringToDecimal("0.00");
                                }

                                if (dgvLocationInfo["MinimumPrice", row.Index].Value == null) { existinginvInvProductStockMaster.MinimumPrice = Common.ConvertStringToDecimal("0.00"); }
                                else
                                {
                                    if (dgvLocationInfo["MinimumPrice", row.Index].Value.ToString() != string.Empty)
                                        existinginvInvProductStockMaster.MinimumPrice = Common.ConvertStringToDecimal(dgvLocationInfo["MinimumPrice", row.Index].Value.ToString().Trim());
                                    else
                                        existinginvInvProductStockMaster.MinimumPrice = Common.ConvertStringToDecimal("0.00");
                                }

                                //if (string.IsNullOrEmpty(dgvLocationInfo["MinimumPrice", row.Index].Value.ToString().Trim())) { existinginvInvProductStockMaster.MinimumPrice = Common.ConvertStringToDecimal("0.00"); }
                                //else { existinginv


                                //if (string.IsNullOrEmpty(dgvLocationInfo["ReLevel", row.Index].Value.ToString().Trim())) { existinginvInvProductStockMaster.ReOrderLevel = Common.ConvertStringToDecimal("0.00"); }
                                //else { existinginvInvProductStockMaster.ReOrderLevel = Common.ConvertStringToDecimal(dgvLocationInfo["ReLevel", row.Index].Value.ToString().Trim()); }

                                //if (string.IsNullOrEmpty(dgvLocationInfo["ReQty", row.Index].Value.ToString().Trim())) { existinginvInvProductStockMaster.ReOrderQuantity = Common.ConvertStringToDecimal("0.00"); }
                                //else { existinginvInvProductStockMaster.ReOrderQuantity = Common.ConvertStringToDecimal(dgvLocationInfo["ReQty", row.Index].Value.ToString().Trim()); }

                                //if (string.IsNullOrEmpty(dgvLocationInfo["RePeriod", row.Index].Value.ToString().Trim())) { existinginvInvProductStockMaster.ReOrderPeriod = Common.ConvertStringToDecimal("0.00"); }
                                //else { existinginvInvProductStockMaster.ReOrderPeriod = Common.ConvertStringToDecimal(dgvLocationInfo["RePeriod", row.Index].Value.ToString().Trim()); }

                                //if (string.IsNullOrEmpty(dgvLocationInfo["CostPrice", row.Index].Value.ToString().Trim())) { existinginvInvProductStockMaster.CostPrice = Common.ConvertStringToDecimal("0.00"); }
                                //else { existinginvInvProductStockMaster.CostPrice = Common.ConvertStringToDecimal(dgvLocationInfo["CostPrice", row.Index].Value.ToString().Trim()); }

                                //if (string.IsNullOrEmpty(dgvLocationInfo["SellingPrice", row.Index].Value.ToString().Trim())) { existinginvInvProductStockMaster.SellingPrice = Common.ConvertStringToDecimal("0.00"); }
                                //else { existinginvInvProductStockMaster.SellingPrice = Common.ConvertStringToDecimal(dgvLocationInfo["SellingPrice", row.Index].Value.ToString().Trim()); }

                                //if (string.IsNullOrEmpty(dgvLocationInfo["MinimumPrice", row.Index].Value.ToString().Trim())) { existinginvInvProductStockMaster.MinimumPrice = Common.ConvertStringToDecimal("0.00"); }
                                //else { existinginvInvProductStockMaster.MinimumPrice = Common.ConvertStringToDecimal(dgvLocationInfo["MinimumPrice", row.Index].Value.ToString().Trim()); }
                            }

                            if (existinginvInvProductStockMaster.InvProductStockMasterID.Equals(0))
                            {
                                invProductStockMasterService.AddProductStockMaster(existinginvInvProductStockMaster);
                            }
                            else
                            {
                                invProductStockMasterService.UpdateProductStockMaster(existinginvInvProductStockMaster);
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
                if (ValidateControls() == false) return;

                InvProductMasterService invProductMasterService = new InvProductMasterService();
                bool isNew = false;
                existingProduct = invProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

                if (existingProduct == null || existingProduct.InvProductMasterID == 0)
                {
                    existingProduct = new InvProductMaster();
                    isNew = true;
                }

                //Tab General
                FillProduct();


                //Tab Unit Conversion

                if (existingProduct.InvProductMasterID == 0)
                {
                    if ((Toast.Show("" + this.Text + " - " + existingProduct.ProductCode + " - " + existingProduct.ProductName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    invProductMasterService.AddProduct(existingProduct);
                    SaveSubDetails();
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    if ((Toast.Show("" + this.Text + " - " + existingProduct.ProductCode + " - " + existingProduct.ProductName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
                    {
                        btnNew.PerformClick();
                    }
                }
                else
                {
                    if (isNew)
                    {
                        if ((Toast.Show("" + this.Text + " - " + existingProduct.ProductCode + " - " + existingProduct.ProductName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingProduct.ProductCode + " - " + existingProduct.ProductName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("" + this.Text + " - " + existingProduct.ProductCode + " - " + existingProduct.ProductName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    invProductMasterService.UpdateProduct(existingProduct);

                    SaveSubDetails();
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    Toast.Show("" + this.Text + " - " + existingProduct.ProductCode + " - " + existingProduct.ProductName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }
                txtProductCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void SaveSubDetails()
        {
            SaveUnitConversion();
            SaveLocationInfo();
            SaveProductLink();
            SaveSupplierLink();
            SaveProductExtendedValues();
        }

        public override void View()
        {
            try
            {
                //string[] decimalfield = { };
                //MdiService mdiService = new MdiService();
                //string ReprotName = "Product";
                //CryInvProductTemplate cryInvTemplate = new CryInvProductTemplate();
                //int x;

                //string departmentText="", categoryText ="", subCategoryText="", subCategory2Text = "";

                //departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
                //categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
                //subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
                //subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;

                //string[] stringfield = { "Code", "Product Name", "Name on Invoice", departmentText, categoryText, subCategoryText, subCategory2Text, "Bar Code", "Re-order Qty", "Cost Price", "Selling Price", "Fixed Discount", "Batch Status", "Remark" };
                //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
                //                                                                mdiService.GetAllProductDataTable(),
                //                                                                0, cryInvTemplate);
                //frmReprotGenerator.ShowDialog();
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmProduct");
                InvReportGenerator invReportGenerator = new InvReportGenerator();
                invReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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
                    image = fldlg.FileName;
                    Bitmap newimg = new Bitmap(image);
                    pbProductImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbProductImage.Image = (Image)newimg;
                }
                fldlg = null;
            }

            catch (System.ArgumentException ae)
            {
                image = string.Empty;
                Logger.WriteLog(ae, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnClearPicture_Click(object sender, EventArgs e)
        {
            try
            {
                pbProductImage.Image = UI.Windows.Properties.Resources.Default_Product;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                bool generateDirect;
                string productCode = "";

                if (!codeDependOnDepartment && !codeDependOnCategory && !codeDependOnSubCategory && !codeDependOnSubCategory2)
                {
                    generateDirect = true;
                }
                else
                {
                    generateDirect = false;
                }

                if (generateDirect)
                {
                    Common.EnableButton(false, btnNew, btnDelete);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    if (chkAutoClear.Checked)
                        Common.ClearTextBox(txtProductName);
                    if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                    {
                        InvProductMasterService invProductMasterService = new InvProductMasterService();
                        txtProductCode.Text = invProductMasterService.GetDirectNewCode(this.Name);
                        Common.EnableTextBox(false, txtProductCode);
                        txtBarCode.Focus();
                    }
                    //else
                    //    txtProductCode.Focus();
                }
                else
                {
                    if (codeDependOnDepartment)
                    {
                        if (string.IsNullOrEmpty(txtDepartmentCode.Text.Trim()))
                        {
                            Toast.Show("Invalid " + lblDepartment.Text.Trim(), Toast.messageType.Information, Toast.messageAction.General);
                            txtDepartmentCode.Focus();
                            return;
                        }
                        else
                        {
                            InvDepartment invDepartment = new InvDepartment();
                            InvDepartmentService invDepartmentService = new InvDepartmentService();
                            invDepartment = invDepartmentService.GetInvDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDependCategory);

                            if (invDepartment != null)
                            {
                                productCode = txtDepartmentCode.Text.Trim();
                            }
                            else
                            {
                                Toast.Show("Invalid " + lblDepartment.Text.Trim(), Toast.messageType.Information, Toast.messageAction.General);
                                txtDepartmentCode.Focus();
                                return;
                            }
                        }
                    }
                    if (codeDependOnCategory)
                    {
                        if (string.IsNullOrEmpty(txtCategoryCode.Text.Trim()))
                        {
                            Toast.Show("Invalid " + lblCategory.Text.Trim(), Toast.messageType.Information, Toast.messageAction.General);
                            txtCategoryCode.Focus();
                            return;
                        }
                        else
                        {
                            InvCategory invCategory = new InvCategory();
                            InvCategoryService invCategoryService = new InvCategoryService();
                            invCategory = invCategoryService.GetInvCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory);

                            if (invCategory != null)
                            {
                                productCode = productCode + txtCategoryCode.Text.Trim();
                            }
                            else
                            {
                                Toast.Show("Invalid " + lblCategory.Text.Trim(), Toast.messageType.Information, Toast.messageAction.General);
                                txtCategoryCode.Focus();
                                return;
                            }
                        }
                    }
                    if (codeDependOnSubCategory)
                    {
                        if (string.IsNullOrEmpty(txtSubCategoryCode.Text.Trim()))
                        {
                            Toast.Show("Invalid " + lblSubCategory.Text.Trim(), Toast.messageType.Information, Toast.messageAction.General);
                            txtSubCategoryCode.Focus();
                            return;
                        }
                        else
                        {
                            InvSubCategory invSubCategory = new InvSubCategory();
                            InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                            invSubCategory = invSubCategoryService.GetInvSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2);

                            if (invSubCategory != null)
                            {
                                productCode = productCode + txtSubCategoryCode.Text.Trim();
                            }
                            else
                            {
                                Toast.Show("Invalid " + lblSubCategory.Text.Trim(), Toast.messageType.Information, Toast.messageAction.General);
                                txtSubCategoryCode.Focus();
                                return;
                            }
                        }
                    }
                    if (codeDependOnSubCategory2)
                    {
                        if (string.IsNullOrEmpty(txtSubCategory2Code.Text.Trim()))
                        {
                            Toast.Show("Invalid " + lblSubCategory2.Text.Trim(), Toast.messageType.Information, Toast.messageAction.General);
                            txtSubCategory2Code.Focus();
                            return;
                        }
                        else
                        {
                            InvSubCategory2 invSubCategory2 = new InvSubCategory2();
                            InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                            invSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByCode(txtSubCategory2Code.Text.Trim());

                            if (invSubCategory2 != null)
                            {
                                productCode = productCode + txtSubCategory2Code.Text.Trim();
                            }
                            else
                            {
                                Toast.Show("Invalid " + lblSubCategory2.Text.Trim(), Toast.messageType.Information, Toast.messageAction.General);
                                txtSubCategory2Code.Focus();
                                return;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(txtSubCategory2Description.Text.Trim()))
                    {
                        Toast.Show("Invalid " + lblSubCategory2.Text.Trim(), Toast.messageType.Information, Toast.messageAction.General);
                        txtSubCategory2Description.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtSubCategoryDescription.Text.Trim()))
                    {
                        Toast.Show("Invalid " + lblSubCategory.Text.Trim(), Toast.messageType.Information, Toast.messageAction.General);
                        txtSubCategory2Description.Focus();
                        return;
                    }

                    Common.EnableButton(false, btnNew, btnDelete);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    if (chkAutoClear.Checked) { Common.ClearTextBox(txtProductName); }

                    /////
                    InvDepartment dpt = new InvDepartment();
                    InvDepartmentService dptService = new InvDepartmentService();
                    dpt = dptService.GetInvDepartmentsByCode(txtDepartmentCode.Text.Trim(), true);

                    InvCategory cat = new InvCategory();
                    InvCategoryService catService = new InvCategoryService(); 
                    cat = catService.GetInvCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory);

                    InvSubCategory subCat = new InvSubCategory();
                    InvSubCategoryService subCatService = new InvSubCategoryService(); 
                    subCat = subCatService.GetInvSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2);
                    /////

                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    txtProductCode.Text = invProductMasterService.GetNewCode(this.Name, productCode, dpt.InvDepartmentID, cat.InvCategoryID, subCat.InvSubCategoryID);

                    txtNameOnInvoice.Text = txtSubCategory2Description.Text.Trim() + " " + txtSubCategoryDescription.Text.Trim();

                    Common.EnableTextBox(false, txtProductCode);
                    Common.EnableTextBox(false, txtNameOnInvoice);

                    if (codeDependOnDepartment) { txtDepartmentCode.Enabled = false; txtDepartmentDescription.Enabled = false; }
                    if (codeDependOnCategory) { txtCategoryCode.Enabled = false; txtCategoryDescription.Enabled = false; }
                    if (codeDependOnSubCategory) { txtSubCategoryCode.Enabled = false; txtSubCategoryDescription.Enabled = false; }
                    if (codeDependOnSubCategory2) { txtSubCategory2Code.Enabled = false; txtSubCategory2Description.Enabled = false; }

                    txtBarCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void FrmProduct_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void FrmProduct_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter && (this.ActiveControl != txtNameOnInvoice && this.ActiveControl != txtPackSize && this.ActiveControl != txtReOrderPeriod))
            //    SendKeys.Send("{TAB}");

        }

        private void txtNameOnInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    DataView dvAllReferenceData = new DataView(invProductMasterService.GetProductsDataTable());
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), this.ActiveControl);
                }
                if (e.KeyCode == Keys.Enter)
                {
                    tabProduct.SelectedTab = tbpGneral;
                    txtDepartmentCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
        }


        private void chkAutoCompleationSubCategory2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvSubCategory2Service invSubCategory2Service = new Service.InvSubCategory2Service();
                Common.SetAutoComplete(txtSubCategory2Code, invSubCategory2Service.GetInvSubCategory2Codes(), chkAutoCompleationSubCategory2.Checked);
                Common.SetAutoComplete(txtSubCategory2Description, invSubCategory2Service.GetInvSubCategory2Names(), chkAutoCompleationSubCategory2.Checked);
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
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    DataView dvAllReferenceData = new DataView(invProductMasterService.GetProductsDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtProductCode);
                        txtProductCode_Leave(this, e);
                    }
                }
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    //txtProductCode_Leave(this, e);
                    txtBarCode.Focus();
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
                if (txtProductCode.Text.Trim() != string.Empty)
                {
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    InvProductMaster existingProduct;

                    existingProduct = invProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());

                    if (existingProduct != null)
                    {
                        LoadProductMaster(existingProduct);
                        if (changeSellingPrice.IsAccess == false) { Common.EnableTextBox(false, txtSellingPrice); } else { Common.EnableTextBox(true, txtSellingPrice); }
                        
                    }
                    else
                    {
                        if (chkAutoClear.Checked) 
                        { 
                            //Common.ClearForm(this, txtProductCode);
                        }
                        
                        if (btnNew.Enabled)
                        {
                            if (Toast.Show("Product - " + txtProductCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes)) { btnNew.PerformClick(); } 
                        }
                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtProductCode);
                    }

                    //txtBarCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void LoadProductMaster(InvProductMaster existingProduct)
        {
            //Tab General

            InvDepartmentService invDepartmentService = new InvDepartmentService();
            InvCategoryService invCategoryService = new InvCategoryService();
            InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
            SupplierService supplierService = new SupplierService();
            InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
            InvDepartment invDepartment;
            InvCategory invCategory;
            InvSubCategory invSubCategory;
            InvSubCategory2 invSubCategory2;
            Supplier supplier;

            txtProductCode.Text = existingProduct.ProductCode;
            txtBarCode.Text = existingProduct.BarCode;
            txtReferenceCode1.Text = existingProduct.ReferenceCode1;
            txtReferenceCode2.Text = existingProduct.ReferenceCode2;
            txtProductName.Text = existingProduct.ProductName;
            txtNameOnInvoice.Text = existingProduct.NameOnInvoice;
            invDepartment = invDepartmentService.GetInvDepartmentsByID(existingProduct.DepartmentID, isDependCategory);
            if (invDepartment != null)
            {
                txtDepartmentCode.Text = invDepartment.DepartmentCode;
                txtDepartmentDescription.Text = invDepartment.DepartmentName;
            }
            else
            {
                Common.ClearTextBox(txtDepartmentCode, txtDepartmentDescription);
                Toast.Show(lblDepartment.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }

            invCategory = invCategoryService.GetInvCategoryByID(existingProduct.CategoryID, isDependSubCategory);
            if (invCategory != null)
            {
                txtCategoryCode.Text = invCategory.CategoryCode;
                txtCategoryDescription.Text = invCategory.CategoryName;
            }
            else
            {
                Common.ClearTextBox(txtCategoryCode, txtCategoryDescription);
                Toast.Show(lblCategory.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }

            invSubCategory = invSubCategoryService.GetInvSubCategoryByID(existingProduct.SubCategoryID, isDependSubCategory2);
            if (invSubCategory != null)
            {
                txtSubCategoryCode.Text = invSubCategory.SubCategoryCode;
                txtSubCategoryDescription.Text = invSubCategory.SubCategoryName;
            }
            else
            {
                Common.ClearTextBox(txtSubCategoryCode, txtSubCategoryDescription);
                Toast.Show(lblSubCategory.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }

            invSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByID(existingProduct.SubCategory2ID);
            if (invSubCategory2 != null)
            {
                txtSubCategory2Code.Text = invSubCategory2.SubCategory2Code;
                txtSubCategory2Description.Text = invSubCategory2.SubCategory2Name;
            }
            else
            {
                Common.ClearTextBox(txtSubCategory2Code, txtSubCategory2Description);
                Toast.Show(lblSubCategory2.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }

            if (codeDependOnDepartment) { txtDepartmentCode.Enabled = false; txtDepartmentDescription.Enabled = false; }
            if (codeDependOnCategory) { txtCategoryCode.Enabled = false; txtCategoryDescription.Enabled = false; }
            if (codeDependOnSubCategory) { txtSubCategoryCode.Enabled = false; txtSubCategoryDescription.Enabled = false; }
            if (codeDependOnSubCategory2) { txtSubCategory2Code.Enabled = false; txtSubCategory2Description.Enabled = false; }

            supplier = supplierService.GetSupplierByID(existingProduct.SupplierID);
            if (supplier != null)
            {
                txtMainSupplierCode.Text = supplier.SupplierCode;
                txtMainSupplierDescription.Text = supplier.SupplierName;
            }
            else
            {
                Common.ClearTextBox(txtMainSupplierCode, txtMainSupplierDescription);
                Toast.Show("Supplier ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }

            cmbUnit.SelectedValue = existingProduct.UnitOfMeasureID;

            txtPackSize.Text = existingProduct.PackSize;

            if (existingProduct.ProductImage != null)
            {
                MemoryStream ms = new MemoryStream((byte[])existingProduct.ProductImage);
                pbProductImage.Image = Image.FromStream(ms);
                pbProductImage.SizeMode = PictureBoxSizeMode.StretchImage;
                pbProductImage.Refresh();
            }
            else
            {
                pbProductImage.Image = UI.Windows.Properties.Resources.Default_Product;
                pbProductImage.Refresh();
            }

            cmbCostingMethod.Text = existingProduct.CostingMethod;
            txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingProduct.CostPrice);
            txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingProduct.SellingPrice);
            txtWholesalePrice.Text = Common.ConvertDecimalToStringCurrency(existingProduct.WholesalePrice);
            txtMinimumPrice.Text = Common.ConvertDecimalToStringCurrency(existingProduct.MinimumPrice);
            txtFixedDiscount.Text = Common.ConvertDecimalToStringCurrency(existingProduct.FixedDiscount);
            txtMaximumDiscount.Text = Common.ConvertDecimalToStringCurrency(existingProduct.MaximumDiscount);
            txtMaximumPrice.Text = Common.ConvertDecimalToStringCurrency(existingProduct.MaximumPrice);
            txtFixedDiscountPercentage.Text = existingProduct.FixedDiscountPercentage.ToString();
            txtMaximumDiscountPercentage.Text = existingProduct.MaximumDiscountPercentage.ToString();
            txtReOrderLevel.Text = Common.ConvertDecimalToStringQty(existingProduct.ReOrderLevel);
            txtReOrderQty.Text = Common.ConvertDecimalToStringQty(existingProduct.ReOrderQty);
            txtReOrderPeriod.Text = Common.ConvertDecimalToStringQty(existingProduct.ReOrderPeriod);
            chkActive.Checked = existingProduct.IsActive;
            chkBatch.Checked = existingProduct.IsBatch;
            chkBundle.Checked = existingProduct.IsBundle;
            chkConsignment.Checked = existingProduct.IsConsignment;
            chkCountable.Checked = existingProduct.IsCountable;
            //existingProduct.IsDCS = chkdActive.Checked;
            chkDrayage.Checked = existingProduct.IsDrayage;
            chkExpiry.Checked = existingProduct.IsExpiry;
            chkFreeIssue.Checked = existingProduct.IsFreeIssue;
            chkPromotion.Checked = existingProduct.IsPromotion;
            chkTax.Checked = existingProduct.IsTax;
            chkSerial.Checked = existingProduct.IsSerial;

            txtOrderPrice.Text = Common.ConvertDecimalToStringCurrency(existingProduct.OrderPrice);

            lblCreatedDate.BackColor = Color.LightBlue;
            lblCreatedBy.BackColor = Color.LightBlue;
            lblModifiedDate.BackColor = Color.LightBlue;
            lblModifiedBy.BackColor = Color.LightBlue;

            lblCreatedDate.Text = Common.FormatDate(existingProduct.CreatedDate).ToShortDateString();
            lblCreatedBy.Text = existingProduct.CreatedUser;
            lblModifiedDate.Text = Common.FormatDate(existingProduct.ModifiedDate).ToShortDateString();
            lblModifiedBy.Text = existingProduct.ModifiedUser;

            cmbCostingMethod.Text = existingProduct.CostingMethod;
            cmbPackSizeUnit.SelectedValue = existingProduct.PackSizeUnitOfMeasureID;
            txtMargin.Text = Common.ConvertDecimalToStringQty(existingProduct.Margin);
            txtWholesaleMargin.Text = Common.ConvertDecimalToStringQty(existingProduct.WholesaleMargin);
            txtFixedGP.Text = Common.ConvertDecimalToStringQty(existingProduct.FixedGP);
            //txtProductName.Focus();

            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
            AccLedgerAccount purchaseLedger = new AccLedgerAccount();
            AccLedgerAccount salesLedger = new AccLedgerAccount();
            AccLedgerAccount otherPurchaseLedger = new AccLedgerAccount();
            AccLedgerAccount otherSalesLedger = new AccLedgerAccount();

            purchaseLedger = accLedgerAccountService.GetAccLedgerAccountByID(existingProduct.PurchaseLedgerID);
            if (purchaseLedger != null)
            {
                txtPurchaseLedgerCode.Text = purchaseLedger.LedgerCode;
                txtPurchaseLedgerName.Text = purchaseLedger.LedgerName;
            }
            else
            {
                txtPurchaseLedgerCode.Text = string.Empty;
                txtPurchaseLedgerName.Text = string.Empty;
            }

            salesLedger = accLedgerAccountService.GetAccLedgerAccountByID(existingProduct.SalesLedgerID);
            if (salesLedger != null)
            {
                txtSalesLedgerCode.Text = salesLedger.LedgerCode;
                txtSalesLedgerName.Text = salesLedger.LedgerName;
            }
            else
            {
                txtSalesLedgerCode.Text = string.Empty;
                txtSalesLedgerName.Text = string.Empty;
            }

            otherPurchaseLedger = accLedgerAccountService.GetAccLedgerAccountByID(existingProduct.OtherPurchaseLedgerID);
            if (otherPurchaseLedger != null)
            {
                txtOtherPurchaseLedgerCode.Text = otherPurchaseLedger.LedgerCode;
                txtOtherPurchaseLedgerName.Text = otherPurchaseLedger.LedgerName;
            }
            else
            {
                txtOtherPurchaseLedgerCode.Text = string.Empty;
                txtOtherPurchaseLedgerName.Text = string.Empty;
            }

            otherSalesLedger = accLedgerAccountService.GetAccLedgerAccountByID(existingProduct.OtherSalesLedgerID);
            if (otherSalesLedger != null)
            {
                txtOtherSalesLedgerCode.Text = otherSalesLedger.LedgerCode;
                txtOtherSalesLedgerName.Text = otherSalesLedger.LedgerName;
            }
            else
            {
                txtOtherSalesLedgerCode.Text = string.Empty;
                txtOtherSalesLedgerName.Text = string.Empty;
            }
            
            //existingProduct.PurchaseLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtPurchaseLedgerCode.Text.Trim()).AccLedgerAccountID;
            //existingProduct.SalesLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtSalesLedgerCode.Text.Trim()).AccLedgerAccountID;
            //existingProduct.OtherPurchaseLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtOtherPurchaseLedgerCode.Text.Trim()).AccLedgerAccountID;
            //existingProduct.OtherSalesLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtOtherSalesLedgerCode.Text.Trim()).AccLedgerAccountID;

            LoadUnitConversion(existingProduct.InvProductMasterID);
            LoadLocationInfo(existingProduct.InvProductMasterID);
            LoadProductLink(existingProduct.InvProductMasterID);
            LoadSupplierLink(existingProduct.InvProductMasterID);
            LoadInfo(existingProduct.InvProductMasterID);
            LoadProductExtendedPropertyValue(existingProduct.InvProductMasterID);

            if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
            if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
            if (changeSellingPrice.IsAccess == true) { };
            Common.EnableButton(false, btnNew);

            tabProduct.SelectedTab = tbpGneral;
        }

        private void LoadProductExtendedPropertyValue(long productID)
        {
            InvProductExtendedPropertyValueService invProductExtendedPropertyValueService = new InvProductExtendedPropertyValueService();

            invProductExtendedPropertyValueGrid = invProductExtendedPropertyValueService.GetProductExtendedPropertyValuesByProductID(productID);

            if (invProductExtendedPropertyValueGrid.Count > 0) { dgvExtendedProperties.DataSource = invProductExtendedPropertyValueGrid; }
            else { dgvExtendedProperties.DataSource = null; }
            
            dgvExtendedProperties.Refresh();
        }


        private void LoadUnitConversion(long productID)
        {
            InvProductUnitConversionService invProductUnitConversionService = new InvProductUnitConversionService();       
            invProductUnitConversionsGrid = invProductUnitConversionService.GetAllProductUnitConversionByProductCode(productID);
            if (invProductUnitConversionsGrid.Count > 0)
                dgvUnitConversion.DataSource = invProductUnitConversionsGrid;
            else
                dgvUnitConversion.DataSource = null;
            dgvUnitConversion.Refresh();
            
           
        }
        private void LoadProductLink(long productID)
        {
            InvProductLinkService invProductLinkService = new InvProductLinkService();
            invProductLinkGrid = invProductLinkService.GetAllProductLinkByProductCode(productID);
            if (invProductLinkGrid.Count > 0)
                dgvPluLink.DataSource = invProductLinkGrid;
            else
                dgvPluLink.DataSource = null;
            dgvPluLink.Refresh();

        }

        private void LoadSupplierLink(long productID)
        {
            InvProductSupplierLinkService invProductSupplierLinkService = new InvProductSupplierLinkService();
            invProductSupplierLinkGrid = invProductSupplierLinkService.GetSupplierLink(productID);

            if (invProductSupplierLinkGrid.Count > 0) { dgvMultiSupplier.DataSource = invProductSupplierLinkGrid; }
            else { dgvMultiSupplier.DataSource = null; }
                
            dgvMultiSupplier.Refresh();

        }

        private void AddNewRowToUnitConversion()
        {
            InvProductUnitConversion invProductUnitConversion = new InvProductUnitConversion();
            invProductUnitConversion.UnitOfMeasureID = 1;
            invProductUnitConversionsGrid.Add(invProductUnitConversion);
            dgvUnitConversion.DataSource = null;
            dgvUnitConversion.DataSource = invProductUnitConversionsGrid;
            dgvUnitConversion.Refresh();
        }

        private void AddNewRowToExtendedProperties()
        {
            InvProductExtendedPropertyValue invProductExtendedPropertyValue = new InvProductExtendedPropertyValue();
            invProductExtendedPropertyValue.InvProductExtendedPropertyID = 1;
            invProductExtendedPropertyValueGrid.Add(invProductExtendedPropertyValue);
            dgvExtendedProperties.DataSource = null;
            dgvExtendedProperties.DataSource = invProductExtendedPropertyValueGrid;

            dgvExtendedProperties.Refresh();
        }

        private void AddNewRowToPLULink()
        {
            InvProductLink invProductLink = new InvProductLink();
            invProductLinkGrid.Add(invProductLink);
            dgvPluLink.DataSource = null;
            dgvPluLink.DataSource = invProductLinkGrid;

            dgvPluLink.Refresh();
        }

      

        private void txtBarCode_Validated(object sender, EventArgs e)
        {
            try
            {
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                InvProductMaster existingProduct;

                if (txtBarCode.Text.Trim() != string.Empty && (txtProductCode.Enabled))
                {


                    existingProduct = invProductMasterService.GetProductsByRefCodes(txtBarCode.Text.Trim());

                    if (existingProduct != null)
                    {
                        LoadProductMaster(existingProduct);
                    }
                    else
                    {

                        Toast.Show("Barcode - " + txtBarCode.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);

                    }


                }
                else if (txtBarCode.Text.Trim() != string.Empty)
                {
                    existingProduct = invProductMasterService.GetProductsByRefCodes(txtBarCode.Text.Trim());

                    if (existingProduct != null && existingProduct.ProductCode != txtProductCode.Text.Trim())
                    {
                        Toast.Show("Bar Code - " + txtBarCode.Text.Trim() + "\nis already exists with Product - " + existingProduct.ProductCode + " - " + existingProduct.ProductName + "", Toast.messageType.Error, Toast.messageAction.General);
                        //txtBarCode.Text = string.Empty;
                        txtBarCode.Focus();
                        isValidControls = false;

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMainSupplierCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtMainSupplierCode.Text.Trim() != string.Empty)
                {
                    SupplierService supplierService = new SupplierService();
                    Supplier existingSupplier = new Supplier();
                    existingSupplier = supplierService.GetSupplierByCode(txtMainSupplierCode.Text.Trim());

                    if (existingSupplier != null)
                    {
                        txtMainSupplierDescription.Text = existingSupplier.SupplierName;
                        cmbUnit.Focus();
                    }
                    else
                    {
                        Toast.Show("Supplier " + txtMainSupplierCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtMainSupplierCode);
                        txtMainSupplierCode.Focus();
                        isValidControls = false;

                    }
                }
                }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

   
        private void txtReferenceCode1_Validated(object sender, EventArgs e)
        {
            try
            {
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                InvProductMaster existingProduct;

                if (txtReferenceCode1.Text.Trim() != string.Empty && (txtProductCode.Enabled))
                {


                    existingProduct = invProductMasterService.GetProductsByRefCodes(txtReferenceCode1.Text.Trim());

                    if (existingProduct != null)
                    {
                        LoadProductMaster(existingProduct);
                    }
                    else
                    {

                        Toast.Show("Reference Code 1 - " + txtReferenceCode1.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);

                    }


                }
                else if (txtReferenceCode1.Text.Trim() != string.Empty)
                {
                    existingProduct = invProductMasterService.GetProductsByRefCodes(txtReferenceCode1.Text.Trim());

                    if (existingProduct != null && existingProduct.ProductCode != txtProductCode.Text.Trim())
                    {
                        Toast.Show("Reference Code 1 - " + txtBarCode.Text.Trim() + "\nis already exists with Product - " + existingProduct.ProductCode + " - " + existingProduct.ProductName + "", Toast.messageType.Error, Toast.messageAction.General);
                        //txtReferenceCode1.Text = string.Empty;
                        txtReferenceCode1.Focus();
                        isValidControls = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtReferenceCode2_Validated(object sender, EventArgs e)
        {
            try
            {
            InvProductMasterService invProductMasterService = new InvProductMasterService();
            InvProductMaster existingProduct;

            if (txtReferenceCode2.Text.Trim() != string.Empty && (txtProductCode.Enabled))
            {


                existingProduct = invProductMasterService.GetProductsByRefCodes(txtReferenceCode2.Text.Trim());

                if (existingProduct != null)
                {
                    LoadProductMaster(existingProduct);
                }
                else
                {

                    Toast.Show("Reference Code 2 - " + txtReferenceCode2.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);

                }


            }
            else if (txtReferenceCode2.Text.Trim() != string.Empty)
            {
                existingProduct = invProductMasterService.GetProductsByRefCodes(txtReferenceCode2.Text.Trim());

                if (existingProduct != null && existingProduct.ProductCode != txtProductCode.Text.Trim())
                {
                    Toast.Show("Reference Code 2 - " + txtReferenceCode2.Text.Trim() + "\nis already exists with Product - " + existingProduct.ProductCode + " - " + existingProduct.ProductName + "", Toast.messageType.Error, Toast.messageAction.General);
                    txtReferenceCode2.Focus();
                    isValidControls = false;
                }
            }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void AddCustomCoulmnstoGridUnitConversion()
        {
            UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();

            dgvUnitConversion.AllowUserToAddRows = true;
            dgvUnitConversion.Columns.Remove("tbpUnitConversionUnit");
           
            DataGridViewComboBoxColumn comboboxColumnParentValues = new DataGridViewComboBoxColumn();

            // Bind data to DataGridViewComboBoxColumn, set properties and add into DataGridView
            comboboxColumnParentValues.DataSource = unitOfMeasureService.GetAllUnitOfMeasures();
            comboboxColumnParentValues.DataPropertyName = "UnitOfMeasureID";
            comboboxColumnParentValues.Name = "tbpUnitConversionUnit";
            comboboxColumnParentValues.DisplayMember = "UnitOfMeasureName";
            comboboxColumnParentValues.ValueMember = "UnitOfMeasureID";
            comboboxColumnParentValues.HeaderText = "Unit";
            comboboxColumnParentValues.MaxDropDownItems = 20;
            comboboxColumnParentValues.FlatStyle = FlatStyle.Flat;
            dgvUnitConversion.Columns.Insert(1, comboboxColumnParentValues);
        }

        private void AddCustomCoulmnPropertyValuetoGridExtendedProperties(long invProductExtendedPropertyID)
        {
            InvProductExtendedValueService invProductExtendedValueService = new InvProductExtendedValueService();

            dgvExtendedProperties.AllowUserToAddRows = true;
            dgvExtendedProperties.Columns.Remove("tbpExtendedPropertyValue");

            DataGridViewComboBoxColumn comboboxColumnPropertyValue = new DataGridViewComboBoxColumn();

            // Bind data to DataGridViewComboBoxColumn, set properties and add into DataGridView
            comboboxColumnPropertyValue.DataSource = invProductExtendedValueService.GetAllActiveInvProductExtendedValuesByProductExtendedPropertyID(invProductExtendedPropertyID).ToList();
            comboboxColumnPropertyValue.DataPropertyName = "InvProductExtendedValueID";
            comboboxColumnPropertyValue.Name = "tbpExtendedPropertyValue";
            comboboxColumnPropertyValue.DisplayMember = "ValueData";
            comboboxColumnPropertyValue.ValueMember = "InvProductExtendedValueID";
            comboboxColumnPropertyValue.HeaderText = "Property Value    ";
            comboboxColumnPropertyValue.MaxDropDownItems = 20;
            comboboxColumnPropertyValue.FlatStyle = FlatStyle.Flat;
            dgvExtendedProperties.Columns.Insert(1, comboboxColumnPropertyValue);

        }

        public override void ClearForm()
        {
            LoadDefaultLocation();
            dgvUnitConversion.DataSource = null;
            if (dgvUnitConversion.Rows.Count > 1)
            {
                foreach(DataGridViewRow dr in dgvUnitConversion.Rows)
                    dgvUnitConversion.Rows.Remove(dr);
            }

            dgvUnitConversion.Refresh();
            dgvPluLink.DataSource = null;
            if (dgvPluLink.Rows.Count > 1)
            {
                foreach (DataGridViewRow dr in dgvPluLink.Rows)
                    dgvPluLink.Rows.Remove(dr);
            }
            dgvPluLink.Refresh();

            dgvMultiSupplier.DataSource = null;
            if (dgvMultiSupplier.Rows.Count > 1)
            {
                foreach (DataGridViewRow dr in dgvMultiSupplier.Rows)
                    dgvMultiSupplier.Rows.Remove(dr);
            }
            dgvMultiSupplier.Refresh();

            dgvDisplayInfo.DataSource = null;
            dgvDisplayInfo.Refresh();

            dgvExtendedProperties.DataSource = null;
            dgvExtendedProperties.Refresh();

            dgvExistingProducts.DataSource = null;
            dgvExistingProducts.Refresh();

            txtSellingPrice.Enabled = true;

            base.ClearForm();
        }

        private void LoadDefaultLocation()
        {
            LocationService locationService = new LocationService();
            List<Location> locations = new List<Location>();

            locations = locationService.GetAllLocations();
            dgvLocationInfo.DataSource = locations;
            dgvLocationInfo.Refresh();

            for (int i = 0; i < dgvLocationInfo.RowCount; i++)
            {
                dgvLocationInfo.Rows[i].Cells["Selection"].Value = true;

                dgvLocationInfo.Rows[i].Cells["ReLevel"].Value = "0.00";
                dgvLocationInfo.Rows[i].Cells["ReQty"].Value = "0.00";
                dgvLocationInfo.Rows[i].Cells["RePeriod"].Value = "0.00";
                dgvLocationInfo.Rows[i].Cells["CostPrice"].Value = "0.00";
                dgvLocationInfo.Rows[i].Cells["MinimumPrice"].Value = "0.00";
                dgvLocationInfo.Rows[i].Cells["SellingPrice"].Value = "0.00";
            }
        }

        private void LoadLocationInfo(long productID)
        {
            InvProductStockMasterService invProductStockMasterService = new InvProductStockMasterService();
            if (invProductStockMasterService.GetProductStockMasterByProductID(productID) == null)
            {
                LoadDefaultLocation();
            }
            else
            {
                dgvLocationInfo.DataSource = null;
                dgvLocationInfo.DataSource = invProductStockMasterService.GetLocationInfo(productID);
                dgvLocationInfo.Refresh();
            }
        }

        private void LoadInfo(long productID)
        {
            InvProductStockMasterService invProductStockMasterService = new InvProductStockMasterService();
            if (!(invProductStockMasterService.GetProductStockMasterByProductID(productID) == null))          
            {
                dgvDisplayInfo.DataSource = null;
                dgvDisplayInfo.DataSource = invProductStockMasterService.GetInfo(productID);
                dgvDisplayInfo.Refresh();
            }
        }

        private void dgvUnitConversion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvUnitConversion.Rows.Count > 0)
            //{
            //    if (dgvUnitConversion.Rows.Count - dgvUnitConversion.CurrentCell.RowIndex == 1 && (dgvUnitConversion["tbpUnitConversionFactor", dgvUnitConversion.CurrentCell.RowIndex].Value != null && !Common.ConvertStringToDecimal(dgvUnitConversion["tbpUnitConversionFactor", dgvUnitConversion.CurrentCell.RowIndex].Value.ToString().Trim()).Equals(0)))
            //    {
            //        AddNewRowToUnitConversion();
            //    }
            //}
        }

        

        private void dgvUnitConversion_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //if (e.ColumnIndex != dgvUnitConversion.Columns["tbpUnitConversionDescription"].Index  &&  e.ColumnIndex != dgvUnitConversion.Columns["tbpUnitConversionUnit"].Index) 
            //{
            //    dgvUnitConversion.Rows[e.RowIndex].ErrorText = "";
            //    decimal newdecimal;

            //    if (dgvUnitConversion.Rows[e.RowIndex].IsNewRow) { return; }
            //    if ((!decimal.TryParse(e.FormattedValue.ToString(), out newdecimal) || newdecimal <= 0) && e.ColumnIndex == dgvUnitConversion.Columns["tbpUnitConversionFactor"].Index)
            //    {
            //        e.Cancel = true;
            //        dgvUnitConversion.Rows[e.RowIndex].ErrorText =  dgvUnitConversion.Columns[e.ColumnIndex].HeaderText.Trim() +" must be a positive value";
            //    }
            //    else if (!decimal.TryParse(e.FormattedValue.ToString(), out newdecimal) || newdecimal < 0)
            //    {
            //        e.Cancel = true;
            //        dgvUnitConversion.Rows[e.RowIndex].ErrorText = dgvUnitConversion.Columns[e.ColumnIndex].HeaderText.Trim() + " must be a positive value";
            //    }
            //}
        }

        private void dgvLocationInfo_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != dgvLocationInfo.Columns["Selection"].Index && e.ColumnIndex != dgvLocationInfo.Columns["Location"].Index)
                {
                    dgvLocationInfo.Rows[e.RowIndex].ErrorText = "";
                    decimal newdecimal;


                    if (dgvLocationInfo.Rows[e.RowIndex].IsNewRow) { return; }
                    if (!decimal.TryParse(e.FormattedValue.ToString(),
                        out newdecimal) || newdecimal < 0)
                    {
                        e.Cancel = true;
                        dgvLocationInfo.Rows[e.RowIndex].ErrorText = dgvLocationInfo.Columns[e.ColumnIndex].HeaderText.Trim() + " must be a positive value";

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvPluLink_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvPluLink.Rows.Count > 0)
            //{
            //    if (dgvPluLink.Rows.Count - dgvPluLink.CurrentCell.RowIndex == 1 && (dgvPluLink["tbpPluLinkCode", dgvPluLink.CurrentCell.RowIndex].Value != null))
            //    {
            //        AddNewRowToPLULink();
            //    }
            //}
        }

        private void txtMainSupplierDescription_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtMainSupplierDescription.Text.Trim() != string.Empty)
                {
                    SupplierService supplierService = new SupplierService();
                    Supplier existingSupplier = new Supplier();
                    existingSupplier = supplierService.GetSupplierByName(txtMainSupplierDescription.Text.Trim());

                    if (existingSupplier != null)
                    {
                        txtMainSupplierCode.Text = existingSupplier.SupplierCode;
                        cmbUnit.Focus();
                    }
                    else
                    {
                        Toast.Show("Supplier " + txtMainSupplierDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtMainSupplierDescription);
                        txtMainSupplierDescription.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvUnitConversion_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void dgvUnitConversion_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void dgvMultiSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F2))
            {
                try
                {
                    if (Toast.Show("Supplier Link", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No)) { return; }
                        
                    InvProductSupplierLinkService invProductSupplierLinkService = new InvProductSupplierLinkService();
                    InvProductSupplierLink existingInvProductSupplierLink = new InvProductSupplierLink();
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    
                    existingProduct = invProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());
                    
                    SupplierService supplierService = new SupplierService();

                    if (existingProduct != null && dgvMultiSupplier["tbpMultiSupplierCode", dgvMultiSupplier.CurrentCell.RowIndex].Value != null)
                    {
                        existingInvProductSupplierLink = invProductSupplierLinkService.GetInvProductSupplierLinkWithDeletedByProductCode(existingProduct.InvProductMasterID, supplierService.GetSupplierByCode(dgvMultiSupplier["tbpMultiSupplierCode", dgvMultiSupplier.CurrentCell.RowIndex].Value.ToString().Trim()).SupplierID);
                    }

                    if (existingInvProductSupplierLink != null && !existingInvProductSupplierLink.SupplierID.Equals(0))
                    {
                        var existingInvProductSupplierLinkToDelete = invProductSupplierLinkGrid.First(s => s.ProductID == existingProduct.InvProductMasterID && s.SupplierID == supplierService.GetSupplierByCode(dgvMultiSupplier["tbpMultiSupplierCode", dgvMultiSupplier.CurrentCell.RowIndex].Value.ToString().Trim()).SupplierID);
                        invProductSupplierLinkGrid.Remove(existingInvProductSupplierLinkToDelete);
                        dgvMultiSupplier.DataSource = null;
                        dgvMultiSupplier.DataSource = invProductSupplierLinkGrid;
                    }
                    else
                    {
                        var existingInvProductSupplierLinkToDelete = invProductSupplierLinkGrid.First(s => s.SupplierID  == supplierService.GetSupplierByCode(dgvMultiSupplier["tbpMultiSupplierCode", dgvMultiSupplier.CurrentCell.RowIndex].Value.ToString().Trim()).SupplierID);
                        invProductSupplierLinkGrid.Remove(existingInvProductSupplierLinkToDelete);
                        dgvMultiSupplier.DataSource = null;
                        dgvMultiSupplier.DataSource = invProductSupplierLinkGrid;
                    }

                    dgvMultiSupplier.Refresh();
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                }
            }
        }

        private void dgvUnitConversion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F2))
            {
                try
                {
                    if (Toast.Show("Unit", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No)) { return; }
                        
                    InvProductUnitConversionService invProductUnitConversionService = new InvProductUnitConversionService();
                    InvProductUnitConversion existingInvProductUnitConversion = new InvProductUnitConversion();
                    InvProductMasterService invProductMasterService = new InvProductMasterService();

                    existingProduct = invProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());

                    if (existingProduct != null && dgvUnitConversion["tbpUnitConversionUnit", dgvUnitConversion.CurrentCell.RowIndex].Value != null)
                    {
                        UnitOfMeasure unitOfMeasure = new UnitOfMeasure();
                        UnitOfMeasureService UnitOfMeasureService = new UnitOfMeasureService();
                        unitOfMeasure = UnitOfMeasureService.GetUnitOfMeasureByName(dgvUnitConversion["tbpUnitConversionUnit", dgvUnitConversion.CurrentCell.RowIndex].Value.ToString().Trim());

                        existingInvProductUnitConversion = invProductUnitConversionService.GetProductUnitByProductCode(existingProduct.InvProductMasterID, unitOfMeasure.UnitOfMeasureID);
                    }
                    if (existingInvProductUnitConversion != null)
                    {
                        //var existingInvProductUnitConversionToDelete = invProductUnitConversionsGrid.First(u => u.ProductID == existingProduct.InvProductMasterID && u.UnitOfMeasureID == Common.ConvertStringToLong(dgvUnitConversion["tbpUnitConversionUnit", dgvUnitConversion.CurrentCell.RowIndex].Value.ToString().Trim()));
                        var existingInvProductUnitConversionToDelete = invProductUnitConversionsGrid.First(u => u.ProductID == existingProduct.InvProductMasterID && u.UnitOfMeasureID == existingInvProductUnitConversion.UnitOfMeasureID);
                        invProductUnitConversionsGrid.Remove(existingInvProductUnitConversionToDelete);
                        dgvUnitConversion.DataSource = null;
                        dgvUnitConversion.DataSource = invProductUnitConversionsGrid;
                    }
                    else
                    {
                        var existingInvProductUnitConversionToDelete = invProductUnitConversionsGrid.First(u => u.UnitOfMeasureName == dgvUnitConversion["tbpUnitConversionUnit", dgvUnitConversion.CurrentCell.RowIndex].Value.ToString().Trim());
                        invProductUnitConversionsGrid.Remove(existingInvProductUnitConversionToDelete);
                        dgvUnitConversion.DataSource = null;
                        dgvUnitConversion.DataSource = invProductUnitConversionsGrid;
                    }

                    dgvUnitConversion.Refresh();
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                }
            }
        }

        private void dgvExtendedProperties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F2))
            {
                try
                {
                    if (Toast.Show("Extended property", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No)) { return; }

                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    InvProductExtendedPropertyValue existinginvProductExtendedPropertyValue = new InvProductExtendedPropertyValue(); 
                    InvProductExtendedPropertyValueService invProductExtendedPropertyValueService = new InvProductExtendedPropertyValueService();

                    existingProduct = invProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());

                    if (existingProduct != null && dgvExtendedProperties["ExtendedPropertyName", dgvExtendedProperties.CurrentCell.RowIndex].Value != null)
                    {
                        existinginvProductExtendedPropertyValue = invProductExtendedPropertyValueService.GetProductExtendedPropertyValueByProductID(existingProduct.InvProductMasterID);
                    }
                    if (existinginvProductExtendedPropertyValue != null)
                    {
                        var existinginvProductExtendedPropertyValueToDelete = invProductExtendedPropertyValueGrid.First(u => u.ExtendedPropertyName == dgvExtendedProperties["ExtendedPropertyName", dgvExtendedProperties.CurrentCell.RowIndex].Value.ToString().Trim());
                        invProductExtendedPropertyValueGrid.Remove(existinginvProductExtendedPropertyValueToDelete);
                        dgvExtendedProperties.DataSource = null;
                        dgvExtendedProperties.DataSource = invProductExtendedPropertyValueGrid;
                    }
                    else
                    {
                        var existinginvProductExtendedPropertyValueToDelete = invProductExtendedPropertyValueGrid.First(u => u.ExtendedPropertyName == dgvExtendedProperties["ExtendedPropertyName", dgvExtendedProperties.CurrentCell.RowIndex].Value.ToString().Trim());
                        invProductExtendedPropertyValueGrid.Remove(existinginvProductExtendedPropertyValueToDelete);
                        dgvExtendedProperties.DataSource = null;
                        dgvExtendedProperties.DataSource = invProductExtendedPropertyValueGrid;
                    }

                    dgvExtendedProperties.Refresh();
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                }
            }
        }

        private void dgvPluLink_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F2))
            {
                try
                {
                    if (Toast.Show("PLU Link", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                        return;

                    InvProductLinkService invProductLinkService = new InvProductLinkService();
                    InvProductLink existingInvProductLink = new InvProductLink();
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    existingProduct = invProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());

                    if (existingProduct != null && dgvPluLink["tbpPluLinkCode", dgvPluLink.CurrentCell.RowIndex].Value != null)
                        existingInvProductLink = invProductLinkService.GetProductLinkByProductCode(existingProduct.InvProductMasterID, (dgvPluLink["tbpPluLinkCode", dgvPluLink.CurrentCell.RowIndex].Value.ToString().Trim()));

                    //&& dgvUnitConversion["tbpUnitConversionUnit", dgvUnitConversion.CurrentCell.RowIndex].Value != null

                    if (existingInvProductLink!=null)
                    {
                        var existingInvProductLinkToDelete = invProductLinkGrid.First(p => p.ProductID == existingProduct.InvProductMasterID && p.ProductLinkCode == (dgvPluLink["tbpPluLinkCode", dgvPluLink.CurrentCell.RowIndex].Value.ToString().Trim()));

                        invProductLinkGrid.Remove(existingInvProductLinkToDelete);
                        dgvPluLink.DataSource = null;
                        dgvPluLink.DataSource = invProductLinkGrid;
                    }
                    else
                    {
                        var existingInvProductLinkToDelete = invProductLinkGrid.First(p => p.ProductLinkCode == (dgvPluLink["tbpPluLinkCode", dgvPluLink.CurrentCell.RowIndex].Value.ToString().Trim()));

                        invProductLinkGrid.Remove(existingInvProductLinkToDelete);
                        dgvPluLink.DataSource = null;
                        dgvPluLink.DataSource = invProductLinkGrid;
                    }

                    dgvPluLink.Refresh();
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                }
            }
        }

        private void dgvMultiSupplier_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvMultiSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void dgvExtendedProperties_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (dgvExtendedProperties.Rows.Count > 0)
                //{
                //    if (dgvExtendedProperties["tbpExtendedPropertyName", dgvExtendedProperties.CurrentCell.RowIndex].Value == null)
                //    {
                //        return;
                //    }
                //    //string st1 = dgvExtendedProperties["tbpExtendedPropertyName", dgvExtendedProperties.CurrentCell.RowIndex].Value.ToString();
                //    //string st2 = (Common.ConvertStringToDecimal(dgvExtendedProperties["tbpExtendedPropertyName", dgvExtendedProperties.CurrentCell.RowIndex].Value.ToString().Trim())).ToString();

                //    if (!Common.ConvertStringToDecimal(dgvExtendedProperties["tbpExtendedPropertyName", dgvExtendedProperties.CurrentCell.RowIndex].Value.ToString().Trim()).Equals(0))
                //    {
                //        AddCustomCoulmnPropertyValuetoGridExtendedProperties(Common.ConvertStringToLong(dgvExtendedProperties["tbpExtendedPropertyName", dgvExtendedProperties.CurrentCell.RowIndex].Value.ToString()));
                //    }
                    
                //    //if (dgvExtendedProperties.Rows.Count - dgvExtendedProperties.CurrentCell.RowIndex == 1 && (dgvExtendedProperties["tbpExtendedPropertyName", dgvExtendedProperties.CurrentCell.RowIndex].Value != null && !Common.ConvertStringToDecimal(dgvExtendedProperties["tbpExtendedPropertyName", dgvExtendedProperties.CurrentCell.RowIndex].Value.ToString().Trim()).Equals(0)))
                //    //{
                //    //    AddNewRowToExtendedProperties();
                //    //}
                //}            
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void dgvExtendedProperties_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                ComboBox combo = e.Control as ComboBox;
                if (combo != null)
                {
                    // Remove an existing event-handler, if present, to avoid  
                    // adding multiple handlers when the editing control is reused.
                    combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                   
                    // Add the event handler. 
                    combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
                   
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
            //    if (((ComboBox)sender).SelectedValue == null)
            //    {
            //        return;
            //    }               

            //    int selectedIndex = ((ComboBox)sender).SelectedIndex;
            //    string selectedValue = ((ComboBox)sender).SelectedValue.ToString();
            //    string selectedText = ((ComboBox)sender).Text;

            //    var currentcell = dgvExtendedProperties.CurrentCellAddress;
            //    var sendingCB = sender as DataGridViewComboBoxEditingControl;
            //    //DataGridViewTextBoxCell cel = (DataGridViewTextBoxCell)dgvExtendedProperties.Rows[currentcell.Y].Cells[0];
            //    DataGridViewComboBoxCell cel = (DataGridViewComboBoxCell)dgvExtendedProperties.Rows[currentcell.Y].Cells[1];
            //    //cel.Value = "gg";// sendingCB.EditingControlFormattedValue.ToString();
            //    InvProductExtendedValueService  invProductExtendedValueService = new InvProductExtendedValueService();
            //    cel.DataSource = invProductExtendedValueService.GetAllActiveInvProductExtendedValuesByProductExtendedPrpertyID(long.Parse(((ComboBox)sender).SelectedValue.ToString())).ToList();
            //    cel.DisplayMember = "ValueData";
            //    cel.ValueMember = "InvProductExtendedValueID";
            //    //comboboxColumnPropertyValue.DataPropertyName = "InvProductExtendedValueID";
            //    //comboboxColumnPropertyValue.Name = "tbpExtendedPropertyValue";
            //    //comboboxColumnPropertyValue.DisplayMember = "ValueData";
            //    //comboboxColumnPropertyValue.ValueMember = "InvProductExtendedValueID";
                
                //AddCustomCoulmnPropertyValuetoGridExtendedProperties(long.Parse(((ComboBox)sender).SelectedValue.ToString()));

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }



       

        //private bool IsExtendedPropertyByName(string propertyName)
        //{
        //    bool recodFound = false;
        //    InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
        //    InvProductExtendedProperty invProductExtendedProperty = new InvProductExtendedProperty();
            
        //    invProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByName(propertyName);
            
        //    if (invProductExtendedProperty != null) 
        //    {
        //        recodFound = true;
        //        cmbExtendedProperty.Text = invProductExtendedProperty.ExtendedPropertyName;
        //        LoadProductExtendedValues(invProductExtendedProperty.InvProductExtendedPropertyID);
        //    }
        //    else
        //    {
        //        cmbExtendedPropertyValue.DataSource = null;
        //        recodFound = false;
        //        Common.ClearComboBox(cmbExtendedProperty);
        //        Toast.Show("Product Extended Property ", Toast.messageType.Information, Toast.messageAction.NotExists);
        //    }

        //    return recodFound;
        //}

        private bool IsExtendedValueByNameByProperty(string valueName, long extendedPropertyID)
        {
            bool recodFound = false;
            InvProductExtendedValueService invProductExtendedValueService = new InvProductExtendedValueService();
            InvProductExtendedValue invProductExtendedValue = new InvProductExtendedValue();
            invProductExtendedValue = invProductExtendedValueService.GetInvProductExtendedValueByValueDataByExtendedPropertyID(valueName, extendedPropertyID);
            
            if (invProductExtendedValue != null)
            {
                recodFound = true;
                txtPropertyValue.Text = invProductExtendedValue.ValueData;                
            }
            else
            {
                recodFound = false;
                Toast.Show("Product Extended Value ", Toast.messageType.Information, Toast.messageAction.NotExists);
                txtPropertyValue.Focus();
                txtPropertyValue.SelectAll();
            }

            return recodFound;
        }


        /// <summary>
        /// Assign product properties before adding into grid view
        /// </summary>
        private void AssignProductExtendedProperties()
        {
            List<InvProductMaster> invProductMasterList = new List<InvProductMaster>();
            InvProductMaster invProductMaster = new InvProductMaster();
            InvProductExtendedPropertyValue invProductExtendedPropertyValue = new InvProductExtendedPropertyValue();
            InvProductExtendedProperty invProductExtendedProperty = new InvProductExtendedProperty();
            InvProductExtendedValue invProductExtendedValue = new InvProductExtendedValue();
            InvProductMasterService invProductMasterService = new InvProductMasterService();
            InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
            InvProductExtendedValueService invProductExtendedValueService = new InvProductExtendedValueService();
            InvProductExtendedPropertyValueService invProductExtendedPropertyValueService = new InvProductExtendedPropertyValueService();

            invProductMaster = invProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

            if (invProductMaster != null)
            {
                invProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByName(txtPropertyName.Text.Trim());
                invProductExtendedValue = invProductExtendedValueService.GetInvProductExtendedValueByValueData(txtPropertyValue.Text.Trim());

                invProductExtendedPropertyValue.ProductID = invProductMaster.InvProductMasterID;
                invProductExtendedPropertyValue.InvProductExtendedPropertyID = invProductExtendedProperty.InvProductExtendedPropertyID;
                invProductExtendedPropertyValue.ExtendedPropertyName = invProductExtendedProperty.ExtendedPropertyName;
                invProductExtendedPropertyValue.InvProductExtendedValueID = invProductExtendedValue.InvProductExtendedValueID;
                invProductExtendedPropertyValue.ValueData = invProductExtendedValue.ValueData;

                invProductExtendedPropertyValueGrid = invProductExtendedPropertyValueService.GetInvProductExtendedPropertyValueTempList(invProductExtendedPropertyValueGrid, invProductExtendedPropertyValue);
                UpdatedgvExtendedProperties();
            }
            else
            {
                invProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByName(txtPropertyName.Text.Trim());
                invProductExtendedValue = invProductExtendedValueService.GetInvProductExtendedValueByValueData(txtPropertyValue.Text.Trim());

                invProductExtendedPropertyValue.ProductID = 0;
                invProductExtendedPropertyValue.InvProductExtendedPropertyID = invProductExtendedProperty.InvProductExtendedPropertyID;
                invProductExtendedPropertyValue.ExtendedPropertyName = invProductExtendedProperty.ExtendedPropertyName;
                invProductExtendedPropertyValue.InvProductExtendedValueID = invProductExtendedValue.InvProductExtendedValueID;
                invProductExtendedPropertyValue.ValueData = invProductExtendedValue.ValueData;

                invProductExtendedPropertyValueGrid = invProductExtendedPropertyValueService.GetInvProductExtendedPropertyValueTempList(invProductExtendedPropertyValueGrid, invProductExtendedPropertyValue);
                UpdatedgvExtendedProperties();
            }

            dgvExistingProducts.DataSource = null;
            invProductMasterList = invProductMasterService.GetAllProductsFromPatternNumber(invProductExtendedValue.InvProductExtendedValueID);
            dgvExistingProducts.DataSource = invProductMasterList;
            dgvExistingProducts.Refresh();
        }

        private void UpdatedgvExtendedProperties()
        {
            dgvExtendedProperties.DataSource = null;
            dgvExtendedProperties.DataSource = invProductExtendedPropertyValueGrid.ToList(); //OrderBy(pr => pr.).ToList();
            dgvExtendedProperties.FirstDisplayedScrollingRowIndex = dgvExtendedProperties.RowCount - 1;
            Common.ClearTextBox(txtPropertyName, txtPropertyValue);
            txtPropertyName.Focus();
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

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    DataView dvAllReferenceData = new DataView(invProductMasterService.GetProductsDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtProductCode);
                        txtProductCode_Leave(this, e);
                    }
                }
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtBarCode_Validated(this, e);
                    txtReferenceCode1.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtReferenceCode1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    DataView dvAllReferenceData = new DataView(invProductMasterService.GetProductsDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtProductCode);
                        txtProductCode_Leave(this, e);
                    }
                }
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtReferenceCode1_Validated(this, e);
                    txtReferenceCode2.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtReferenceCode2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    DataView dvAllReferenceData = new DataView(invProductMasterService.GetProductsDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtProductCode);
                        txtProductCode_Leave(this, e);
                    }
                }
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtReferenceCode2_Validated(this, e);
                    txtProductName.Focus();
                }
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
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    DataView dvAllReferenceData = new DataView(invProductMasterService.GetProductsDataTable());
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), this.ActiveControl);
                }
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtNameOnInvoice.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMainSupplierCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtMainSupplierCode_Validated(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMainSupplierDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtMainSupplierDescription_Leave(this, e);
                }
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
                    txtPackSize.Focus();
                    txtPackSize.SelectAll();
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
                txtPackSize.Text = "0.00";
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbPackSizeUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    cmbPackSizeUnit_Leave(this, e);
                    txtCostPrice.Focus();
                    txtCostPrice.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbPackSizeUnit_Leave(object sender, EventArgs e)
        {
            try
            {
                //txtCostPrice.Text = "0.00";
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCostPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtCostPrice_Leave(this, e);
                    txtMinimumPrice.Focus();
                    txtMinimumPrice.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCostPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMinimumPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtMinimumPrice_Leave(this, e);
                    txtMaximumPrice.Focus();
                    txtMaximumPrice.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMinimumPrice_Leave(object sender, EventArgs e)
        {
            //if (!Validater.ValidateTextBoxWithCustomerMessage("Invalid minimum price", errorProvider, Validater.ValidateType.Empty, txtMinimumPrice))
            //{
            //    txtMinimumPrice.Focus();
            //}
            //else if (!Validater.ValidateTextBoxWithCustomerMessage("Invalid minimum price", errorProvider, Validater.ValidateType.Zero, txtMinimumPrice))
            //{
            //    txtMinimumPrice.Focus();
            //}
        }

        private void txtMaximumPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtMaximumPrice_Leave(this, e);
                    txtSellingPrice.Focus();
                    txtSellingPrice.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMaximumPrice_Leave(object sender, EventArgs e)
        {
            //txtSellingPrice.Text = "0.00";
            //if (!Validater.ValidateTextBoxWithCustomerMessage("Invalid maximum price", errorProvider, Validater.ValidateType.Empty, txtMaximumPrice))
            //{
            //    txtMaximumPrice.Focus();
            //}
            //else if (!Validater.ValidateTextBoxWithCustomerMessage("Invalid maximum price", errorProvider, Validater.ValidateType.Zero, txtMaximumPrice))
            //{
            //    txtMaximumPrice.Focus();
            //}
        }

        private void txtSellingPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSellingPrice_Leave(this, e);
                    txtMargin.Focus();
                    txtMargin.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSellingPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                //if (!Validater.ValidateTextBoxWithCustomerMessage("Invalid selling price", errorProvider, Validater.ValidateType.Empty, txtSellingPrice))
                //{
                //    txtSellingPrice.Focus();
                //}
                decimal costPrice, sellingPrise;
                if (string.IsNullOrEmpty(txtCostPrice.Text.Trim())) { costPrice = 0; }
                else { costPrice = Convert.ToDecimal(txtCostPrice.Text.Trim()); }
                if (string.IsNullOrEmpty(txtSellingPrice.Text.Trim())) { sellingPrise = 0; }
                else { sellingPrise = Convert.ToDecimal(txtSellingPrice.Text.Trim()); }

                txtMargin.Text = String.Format("{0:0.00}", GetMarginByCostAndSellingOrWholesalePrice(costPrice, sellingPrise).ToString("#0.00"));
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private decimal GetMarginByCostAndSellingOrWholesalePrice(decimal costPrice, decimal sellingPriceOrWholesalePrice)  
        {
            decimal value;
            decimal margin = 0;

            value = sellingPriceOrWholesalePrice - costPrice;

            if (costPrice != 0)
            {
                margin = (value / costPrice) * 100;
            }

            return margin;
        }

        private decimal GetSellingOrWholesalePriceByMargin(decimal costPrice, decimal marginOrWholesaleMargin)
        {
            decimal sellingOrWholesalePrice = 0;
            sellingOrWholesalePrice = Math.Round(((costPrice * marginOrWholesaleMargin) / 100) + costPrice, 2, MidpointRounding.AwayFromZero);
            return sellingOrWholesalePrice;
        }

        private void txtMargin_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtMargin_Leave(this, e);
                    txtFixedDiscountPercentage.Focus();
                    txtFixedDiscountPercentage.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMargin_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal costPrice, margin;
                if (string.IsNullOrEmpty(txtCostPrice.Text.Trim())) { costPrice = 0; }
                else { costPrice = Convert.ToDecimal(txtCostPrice.Text.Trim()); }
                if (string.IsNullOrEmpty(txtMargin.Text.Trim())) { margin = 0; }
                else { margin = Convert.ToDecimal(txtMargin.Text.Trim()); }

                if (chkMargin.Checked)
                {
                    txtSellingPrice.Text = String.Format("{0:0.00}", GetSellingOrWholesalePriceByMargin(costPrice, margin).ToString("#0.00"));
                }

                //txtFixedDiscountPercentage.Text = "0.00";
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtWholesalePrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtWholesalePrice_Leave(this, e);
                    txtWholesaleMargin.Focus();
                    txtWholesaleMargin.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtWholesalePrice_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal costPrice, wholesalePrise;
                if (string.IsNullOrEmpty(txtCostPrice.Text.Trim())) { costPrice = 0; }
                else { costPrice = Convert.ToDecimal(txtCostPrice.Text.Trim()); }
                if (string.IsNullOrEmpty(txtWholesalePrice.Text.Trim())) { wholesalePrise = 0; }
                else { wholesalePrise = Convert.ToDecimal(txtWholesalePrice.Text.Trim()); }

                txtWholesaleMargin.Text = String.Format("{0:0.00}", GetMarginByCostAndSellingOrWholesalePrice(costPrice, wholesalePrise).ToString("#0.00"));
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtWholesaleMargin_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtWholesaleMargin_Leave(this, e);
                    txtMaximumDiscountPercentage.Focus();
                    txtMaximumDiscountPercentage.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtWholesaleMargin_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal costPrice, margin;
                if (string.IsNullOrEmpty(txtCostPrice.Text.Trim())) { costPrice = 0; }
                else { costPrice = Convert.ToDecimal(txtCostPrice.Text.Trim()); }
                if (string.IsNullOrEmpty(txtWholesaleMargin.Text.Trim())) { margin = 0; }
                else { margin = Convert.ToDecimal(txtWholesaleMargin.Text.Trim()); }

                if (chkWholeSaleMargin.Checked)
                {
                    txtWholesalePrice.Text = String.Format("{0:0.00}", GetSellingOrWholesalePriceByMargin(costPrice, margin).ToString("#0.00"));
                }

                //txtMaximumDiscountPercentage.Text = "0.00";
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void txtFixedDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtFixedDiscount_Leave(this, e);
                    txtMaximumDiscount.Focus();
                    txtMaximumDiscount.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtFixedDiscount_Leave(object sender, EventArgs e)
        {
            //txtMaximumDiscount.Text = "0.00";
        }

        private void txtMaximumDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtMaximumDiscount_Leave(this, e);
                    txtFixedGP.Focus();
                    txtFixedGP.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMaximumDiscount_Leave(object sender, EventArgs e)
        {
            //txtFixedGP.Text = "0.00";
        }

        private void txtFixedGP_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtFixedGP_Leave(this, e);
                    txtReOrderLevel.Focus();
                    txtReOrderLevel.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtFixedGP_Leave(object sender, EventArgs e)
        {
            //txtReOrderLevel.Text = "0.00";
        }

        private void txtReOrderLevel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtReOrderLevel_Leave(this, e);
                    txtReOrderQty.Focus();
                    txtReOrderQty.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtReOrderLevel_Leave(object sender, EventArgs e)
        {
            //txtReOrderQty.Text = "0.00";
        }

        private void txtReOrderQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtReOrderQty_Leave(this, e);
                    txtReOrderPeriod.Focus();
                    txtReOrderPeriod.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtReOrderQty_Leave(object sender, EventArgs e)
        {
            //txtReOrderPeriod.Text = "0.00";
        }

        private void txtReOrderPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtReOrderPeriod_Leave(this, e);
                    tabProduct.SelectedTab = tbpExtendedProperties;
                    this.ActiveControl = txtPropertyName;
                    txtPropertyName.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtReOrderPeriod_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtPackSize_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbPackSizeUnit.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPackSize_Leave(object sender, EventArgs e)
        {
            //if (!Validater.ValidateTextBoxWithCustomerMessage("Invalid Pack Size", errorProvider, Validater.ValidateType.Zero, txtPackSize))
            //{
            //    txtPackSize.Focus();
            //}
        }

        private void chkDrayage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDrayage.Checked)
                {
                    tabProduct.SelectedTab = tbpOther;
                    this.ActiveControl = txtDrayagePercentage;
                    txtDrayagePercentage.Enabled = true;
                    txtDrayagePercentage.Focus();
                }
                else
                {
                    tabProduct.SelectedTab = tbpGneral;
                    txtDrayagePercentage.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtUnitDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    cmbUnitTbpUnit.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtUnitDescription_Leave(object sender, EventArgs e)
        {
            
        }

        private void cmbUnitTbpUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtUnitConvertFactor.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbUnitTbpUnit_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.Equals(cmbUnitTbpUnit.Text.Trim(), cmbUnit.Text.Trim()))
                {
                    Toast.Show("Cant select base unit as unit conversions", Toast.messageType.Information, Toast.messageAction.General);
                    cmbUnitTbpUnit.Focus();
                }
                else
                {
                    txtUnitConvertFactor.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtUnitConvertFactor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtUnitConvertFactor_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateUnit()
        {
            bool value;
            if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Zero, txtUnitConvertFactor)) { value = false; }
            else { value = true; }

            return value;
        }

        private void txtUnitConvertFactor_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUnitConvertFactor.Text.Trim())) { return; }
                if (ValidateUnit())
                {
                    txtUnitCostPrice.Focus();
                    txtUnitCostPrice.SelectAll();
                }
                else
                {
                    txtUnitConvertFactor.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtUnitCostPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtUnitCostPrice_Leave(this, e);
                    txtUnitMinimumPrice.Focus();
                    txtUnitMinimumPrice.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtUnitCostPrice_Leave(object sender, EventArgs e)
        {

        }

        private void txtUnitMinimumPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtUnitMinimumPrice_Leave(this, e);
                    txtUnitSellingPrice.Focus();
                    txtUnitSellingPrice.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtUnitMinimumPrice_Leave(object sender, EventArgs e)
        {

        }

        private void UnitConversionLineClear()
        {
            txtUnitDescription.Text = string.Empty;
            cmbUnitTbpUnit.SelectedIndex = -1;
            txtUnitConvertFactor.Text = string.Empty;
            txtUnitCostPrice.Text = string.Empty;
            txtUnitSellingPrice.Text = string.Empty;
            txtUnitMinimumPrice.Text = string.Empty;
        }

        private void txtUnitSellingPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (string.IsNullOrEmpty(cmbUnitTbpUnit.Text.Trim())) 
                    {
                        Toast.Show("Invalid unit", Toast.messageType.Information, Toast.messageAction.General);
                        cmbUnitTbpUnit.Focus();
                        return;
                    }
                    
                    if (ValidateUnitConversionLine())
                    {
                        UpdateUnitConversionGrid();
                        UnitConversionLineClear();
                        txtUnitDescription.Focus();
                    }
                    //else
                    //{
                    //    Toast.Show("Please fill required fields", Toast.messageType.Error, Toast.messageAction.General);
                    //}
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateUnitConversionLine()
        {
            bool value;

            if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtUnitDescription, txtUnitConvertFactor, txtUnitCostPrice, txtUnitSellingPrice, txtUnitMinimumPrice)) { value = false; }
            else { value = true; }

            return value;
        }

        private void txtUnitSellingPrice_Leave(object sender, EventArgs e)
        {

        }

        private void UpdateUnitConversionGrid()
        {
            InvProductMasterService InvProductMasterService = new InvProductMasterService();
            existingProduct = InvProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

            if (existingProduct != null)
            {
                InvProductUnitConversion invProductUnitConversion = new InvProductUnitConversion();

                invProductUnitConversion.Description = txtUnitDescription.Text.Trim();
                invProductUnitConversion.ProductID = existingProduct.InvProductMasterID;
                invProductUnitConversion.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnitTbpUnit.SelectedValue.ToString());
                invProductUnitConversion.UnitOfMeasureName = cmbUnitTbpUnit.Text.Trim();
                invProductUnitConversion.ConvertFactor = Common.ConvertStringToDecimal(txtUnitConvertFactor.Text.Trim());
                invProductUnitConversion.CostPrice = Common.ConvertStringToDecimalCurrency(txtUnitCostPrice.Text.Trim());
                invProductUnitConversion.SellingPrice = Common.ConvertStringToDecimalCurrency(txtUnitSellingPrice.Text.Trim());
                invProductUnitConversion.MinimumPrice = Common.ConvertStringToDecimalCurrency(txtUnitMinimumPrice.Text.Trim());
                //invProductUnitConversionsGrid

                invProductUnitConversionsGrid = InvProductMasterService.GetUpdateProductUnitConversionTemp(invProductUnitConversionsGrid, invProductUnitConversion, existingProduct);
                dgvUnitConversion.DataSource = invProductUnitConversionsGrid;
                dgvUnitConversion.Refresh();
            }
            else
            {
                InvProductUnitConversion invProductUnitConversion = new InvProductUnitConversion();

                invProductUnitConversion.Description = txtUnitDescription.Text.Trim();
                invProductUnitConversion.ProductID = 0;
                invProductUnitConversion.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnitTbpUnit.SelectedValue.ToString());
                invProductUnitConversion.UnitOfMeasureName = cmbUnitTbpUnit.Text.Trim();
                invProductUnitConversion.ConvertFactor = Common.ConvertStringToDecimal(txtUnitConvertFactor.Text.Trim());
                invProductUnitConversion.CostPrice = Common.ConvertStringToDecimalCurrency(txtUnitCostPrice.Text.Trim());
                invProductUnitConversion.SellingPrice = Common.ConvertStringToDecimalCurrency(txtUnitSellingPrice.Text.Trim());
                invProductUnitConversion.MinimumPrice = Common.ConvertStringToDecimalCurrency(txtUnitMinimumPrice.Text.Trim());
                //invProductUnitConversionsGrid

                invProductUnitConversionsGrid = InvProductMasterService.GetUpdateProductUnitConversionTemp(invProductUnitConversionsGrid, invProductUnitConversion, existingProduct);
                dgvUnitConversion.DataSource = invProductUnitConversionsGrid;
                dgvUnitConversion.Refresh();
            }

        }

        private void txtSupplierLinkSupplierCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSupplierLinkSupplierCode_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierLinkSupplierCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSupplierLinkSupplierCode.Text)) { return; }

                SupplierService supplierService = new SupplierService();
                Supplier supplier = new Supplier();

                supplier = supplierService.GetSupplierByCode(txtSupplierLinkSupplierCode.Text.Trim());

                if (supplier != null)
                {
                    txtSupplierLinkSupplierCode.Text = supplier.SupplierCode;
                    txtSupplierLinkSupplierName.Text = supplier.SupplierName;
                    txtSupplierLinkCostPrice.Focus();
                }
                else
                {
                    //Toast.Show("Supplier", Toast.messageType.Error, Toast.messageAction.NotExists);
                    txtSupplierLinkSupplierName.Focus();
                }

                Common.SetAutoComplete(txtMainSupplierDescription, supplierService.GetSupplierNames(), chkAutoCompleationMainSupplier.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierLinkSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSupplierLinkSupplierName_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierLinkSupplierName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSupplierLinkSupplierName.Text)) { return; }

                SupplierService supplierService = new SupplierService();
                Supplier supplier = new Supplier();

                supplier = supplierService.GetSupplierByName(txtSupplierLinkSupplierName.Text.Trim());

                if (supplier != null)
                {
                    txtSupplierLinkSupplierCode.Text = supplier.SupplierCode;
                    txtSupplierLinkSupplierName.Text = supplier.SupplierName;
                    txtSupplierLinkCostPrice.Focus();
                    txtSupplierLinkCostPrice.SelectAll();
                }
                else
                {
                    Toast.Show("Supplier", Toast.messageType.Error, Toast.messageAction.NotExists);
                    txtSupplierLinkSupplierName.Focus();
                }

                Common.SetAutoComplete(txtMainSupplierDescription, supplierService.GetSupplierNames(), chkAutoCompleationMainSupplier.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierLinkCostPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSupplierLinkFixedGP.Focus();
                    txtSupplierLinkFixedGP.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierLinkCostPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void SupplierLinkGridClear()
        {
            txtSupplierLinkSupplierCode.Text = string.Empty;
            txtSupplierLinkSupplierName.Text = string.Empty;
            txtSupplierLinkCostPrice.Text = string.Empty;
            txtSupplierLinkFixedGP.Text = string.Empty;
        }

        private void txtSupplierLinkFixedGP_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    UpdateSupplierLinkGrid();
                    SupplierLinkGridClear();
                    txtSupplierLinkSupplierCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierLinkFixedGP_Leave(object sender, EventArgs e)
        {

        }

        private void UpdateSupplierLinkGrid()  
        {
            InvProductMasterService InvProductMasterService = new InvProductMasterService();
            existingProduct = InvProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

            if (existingProduct != null)
            {
                InvProductSupplierLink invProductSupplierLink = new InvProductSupplierLink();

                SupplierService supplierService = new SupplierService();
                Supplier supplier = new Supplier();

                supplier = supplierService.GetSupplierByCode(txtSupplierLinkSupplierCode.Text.Trim());

                invProductSupplierLink.ProductID = existingProduct.InvProductMasterID;
                invProductSupplierLink.SupplierID = supplier.SupplierID;
                invProductSupplierLink.SupplierCode = supplier.SupplierCode;
                invProductSupplierLink.SupplierName = supplier.SupplierName;
                invProductSupplierLink.CostPrice = Common.ConvertStringToDecimalCurrency(txtSupplierLinkCostPrice.Text.Trim());
                invProductSupplierLink.FixedGP = Common.ConvertStringToDecimalCurrency(txtSupplierLinkFixedGP.Text.Trim());
                //invProductUnitConversionsGrid

                invProductSupplierLinkGrid = InvProductMasterService.GetUpdateProductSupplierLinkTemp(invProductSupplierLinkGrid, invProductSupplierLink, existingProduct);
                dgvMultiSupplier.DataSource = invProductSupplierLinkGrid;
                dgvMultiSupplier.Refresh();
            }
            else
            {
                InvProductSupplierLink invProductSupplierLink = new InvProductSupplierLink();

                SupplierService supplierService = new SupplierService();
                Supplier supplier = new Supplier();

                supplier = supplierService.GetSupplierByCode(txtSupplierLinkSupplierCode.Text.Trim());

                invProductSupplierLink.ProductID = 0;
                invProductSupplierLink.SupplierID = supplier.SupplierID;
                invProductSupplierLink.SupplierCode = supplier.SupplierCode;
                invProductSupplierLink.SupplierName = supplier.SupplierName;
                invProductSupplierLink.CostPrice = Common.ConvertStringToDecimalCurrency(txtSupplierLinkCostPrice.Text.Trim());
                invProductSupplierLink.FixedGP = Common.ConvertStringToDecimalCurrency(txtSupplierLinkFixedGP.Text.Trim());

                //invProductUnitConversionsGrid

                invProductSupplierLinkGrid = InvProductMasterService.GetUpdateProductSupplierLinkTemp(invProductSupplierLinkGrid, invProductSupplierLink, existingProduct);
                dgvMultiSupplier.DataSource = invProductSupplierLinkGrid;
                dgvMultiSupplier.Refresh();
            }

        }

        private void txtPurchaseLedgerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtPurchaseLedgerName);
                txtPurchaseLedgerName.SelectionStart = txtPurchaseLedgerName.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPurchaseLedgerCode_Leave(object sender, EventArgs e)
        {
           try
            {
                if (txtPurchaseLedgerCode.Text.Trim() != string.Empty)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    AccLedgerAccount existingLedgerAccount = new AccLedgerAccount();
                    existingLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtPurchaseLedgerCode.Text.Trim());
                    if (existingLedgerAccount != null)
                    {
                        txtPurchaseLedgerCode.Text = existingLedgerAccount.LedgerCode;
                        txtPurchaseLedgerName.Text = existingLedgerAccount.LedgerName;
                        //txtPurchaseLedgerName.Focus();
                    }
                    else
                    {
                        Toast.Show("Purchase ledger" + " - " + txtPurchaseLedgerCode.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists).Equals(DialogResult.Yes);
                        txtPurchaseLedgerCode.Text = string.Empty;
                        txtPurchaseLedgerCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPurchaseLedgerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtSalesLedgerCode);
                txtSalesLedgerCode.SelectionStart = txtSalesLedgerCode.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPurchaseLedgerName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPurchaseLedgerName.Text.Trim() != string.Empty)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    AccLedgerAccount existingLedgerAccount = new AccLedgerAccount();
                    existingLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByName(txtPurchaseLedgerName.Text.Trim());
                    if (existingLedgerAccount != null)
                    {
                        txtPurchaseLedgerCode.Text = existingLedgerAccount.LedgerCode;
                        txtPurchaseLedgerName.Text = existingLedgerAccount.LedgerName;
                        //txtPurchaseLedgerName.Focus();
                    }
                    else
                    {
                        Toast.Show("Purchase ledger" + " - " + txtPurchaseLedgerName.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists).Equals(DialogResult.Yes);
                        txtPurchaseLedgerName.Text = string.Empty;
                        txtPurchaseLedgerName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesLedgerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtSalesLedgerName);
                txtSalesLedgerName.SelectionStart = txtSalesLedgerName.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesLedgerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSalesLedgerCode.Text.Trim() != string.Empty)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    AccLedgerAccount existingLedgerAccount = new AccLedgerAccount();
                    existingLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtSalesLedgerCode.Text.Trim());
                    if (existingLedgerAccount != null)
                    {
                        txtSalesLedgerCode.Text = existingLedgerAccount.LedgerCode;
                        txtSalesLedgerName.Text = existingLedgerAccount.LedgerName;
                        //txtPurchaseLedgerName.Focus();
                    }
                    else
                    {
                        Toast.Show("Sales ledger" + " - " + txtSalesLedgerCode.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists).Equals(DialogResult.Yes);
                        txtSalesLedgerCode.Text = string.Empty;
                        txtSalesLedgerCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesLedgerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtOtherPurchaseLedgerCode);
                txtOtherPurchaseLedgerCode.SelectionStart = txtOtherPurchaseLedgerCode.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesLedgerName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSalesLedgerName.Text.Trim() != string.Empty)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    AccLedgerAccount existingLedgerAccount = new AccLedgerAccount();
                    existingLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByName(txtSalesLedgerName.Text.Trim());
                    if (existingLedgerAccount != null)
                    {
                        txtSalesLedgerCode.Text = existingLedgerAccount.LedgerCode;
                        txtSalesLedgerName.Text = existingLedgerAccount.LedgerName;
                        //txtPurchaseLedgerName.Focus();
                    }
                    else
                    {
                        Toast.Show("Sales ledger" + " - " + txtSalesLedgerName.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists).Equals(DialogResult.Yes);
                        txtSalesLedgerCode.Text = string.Empty;
                        txtSalesLedgerCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherPurchaseLedgerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtOtherPurchaseLedgerName);
                txtOtherPurchaseLedgerName.SelectionStart = txtOtherPurchaseLedgerName.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherPurchaseLedgerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtOtherPurchaseLedgerCode.Text.Trim() != string.Empty)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    AccLedgerAccount existingLedgerAccount = new AccLedgerAccount();
                    existingLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtOtherPurchaseLedgerCode.Text.Trim());
                    if (existingLedgerAccount != null)
                    {
                        txtOtherPurchaseLedgerCode.Text = existingLedgerAccount.LedgerCode;
                        txtOtherPurchaseLedgerName.Text = existingLedgerAccount.LedgerName;
                        //txtPurchaseLedgerName.Focus();
                    }
                    else
                    {
                        Toast.Show("Other purchase ledger" + " - " + txtOtherPurchaseLedgerCode.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists).Equals(DialogResult.Yes);
                        txtOtherPurchaseLedgerCode.Text = string.Empty;
                        txtOtherPurchaseLedgerCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherPurchaseLedgerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtOtherSalesLedgerCode);
                txtOtherSalesLedgerCode.SelectionStart = txtOtherSalesLedgerCode.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherPurchaseLedgerName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtOtherPurchaseLedgerName.Text.Trim() != string.Empty)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    AccLedgerAccount existingLedgerAccount = new AccLedgerAccount();
                    existingLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByName(txtOtherPurchaseLedgerName.Text.Trim());
                    if (existingLedgerAccount != null)
                    {
                        txtOtherPurchaseLedgerCode.Text = existingLedgerAccount.LedgerCode;
                        txtOtherPurchaseLedgerName.Text = existingLedgerAccount.LedgerName;
                        //txtPurchaseLedgerName.Focus();
                    }
                    else
                    {
                        Toast.Show("Other purchase ledger" + " - " + txtOtherPurchaseLedgerName.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists).Equals(DialogResult.Yes);
                        txtOtherPurchaseLedgerName.Text = string.Empty;
                        txtOtherPurchaseLedgerName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherSalesLedgerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtOtherSalesLedgerName);
                txtOtherSalesLedgerName.SelectionStart = txtOtherSalesLedgerName.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherSalesLedgerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtOtherSalesLedgerCode.Text.Trim() != string.Empty)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    AccLedgerAccount existingLedgerAccount = new AccLedgerAccount();
                    existingLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtOtherSalesLedgerCode.Text.Trim());
                    if (existingLedgerAccount != null)
                    {
                        txtOtherSalesLedgerCode.Text = existingLedgerAccount.LedgerCode;
                        txtOtherSalesLedgerName.Text = existingLedgerAccount.LedgerName;
                        //txtPurchaseLedgerName.Focus();
                    }
                    else
                    {
                        Toast.Show("Other sales ledger" + " - " + txtOtherSalesLedgerCode.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists).Equals(DialogResult.Yes);
                        txtOtherSalesLedgerCode.Text = string.Empty;
                        txtOtherSalesLedgerCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherSalesLedgerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    tabProduct.SelectedTab = tbpDisplay;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherSalesLedgerName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtOtherSalesLedgerName.Text.Trim() != string.Empty)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    AccLedgerAccount existingLedgerAccount = new AccLedgerAccount();
                    existingLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByName(txtOtherSalesLedgerName.Text.Trim());
                    if (existingLedgerAccount != null)
                    {
                        txtOtherSalesLedgerCode.Text = existingLedgerAccount.LedgerCode;
                        txtOtherSalesLedgerName.Text = existingLedgerAccount.LedgerName;
                        //txtPurchaseLedgerName.Focus();
                    }
                    else
                    {
                        Toast.Show("Other sales ledger" + " - " + txtOtherSalesLedgerName.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists).Equals(DialogResult.Yes);
                        txtOtherSalesLedgerName.Text = string.Empty;
                        txtOtherSalesLedgerName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtDepartmentCode_Leave(this, e);
                    txtCategoryCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtDepartmentCode.Text.Trim() != string.Empty)
                {
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    InvDepartment existingInvDepartment = new InvDepartment();
                    existingInvDepartment = invDepartmentService.GetInvDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDependCategory);

                    if (existingInvDepartment != null)
                    {
                        txtDepartmentCode.Text = existingInvDepartment.DepartmentCode;
                        txtDepartmentDescription.Text = existingInvDepartment.DepartmentName;
                    }
                    else
                    {
                        Toast.Show(lblDepartment.Text + " " + txtDepartmentCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtDepartmentCode);
                        txtDepartmentCode.Focus();
                        isValidControls = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtDepartmentDescription_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentDescription_Leave(object sender, EventArgs e)
        {
            if (txtDepartmentDescription.Text.Trim() != string.Empty)
            {
                InvDepartmentService invDepartmentService = new InvDepartmentService();
                InvDepartment existingInvDepartment = new InvDepartment();
                existingInvDepartment = invDepartmentService.GetInvDepartmentsByName(txtDepartmentDescription.Text.Trim(), isDependCategory);

                if (existingInvDepartment != null)
                {
                    txtDepartmentCode.Text = existingInvDepartment.DepartmentCode;
                    txtCategoryCode.Focus();
                }
                else
                {
                    Toast.Show(lblDepartment.Text + " " + txtDepartmentDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    Common.ClearTextBox(txtDepartmentDescription);
                    txtDepartmentDescription.Focus();
                }
            }
        }

        private void txtCategoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtCategoryCode_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryCode.Text.Trim() != string.Empty)
                {
                    InvCategoryService invCategoryService = new InvCategoryService();
                    InvCategory existingInvCategory = new InvCategory();

                    existingInvCategory = invCategoryService.GetInvCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory);

                    if (existingInvCategory != null)
                    {
                        if (!isDependCategory)
                        {
                            txtCategoryDescription.Text = existingInvCategory.CategoryName;
                            txtSubCategoryCode.Focus();
                        }
                        else
                        {
                            InvDepartmentService invDepartmentService = new Service.InvDepartmentService();

                            if (invDepartmentService.GetInvDepartmentsByID(existingInvCategory.InvDepartmentID, isDependCategory).DepartmentCode.Equals(txtDepartmentCode.Text.Trim()))
                            {
                                txtCategoryDescription.Text = existingInvCategory.CategoryName;
                                txtSubCategoryCode.Focus();
                            }
                            else
                            {
                                Toast.Show(lblCategory.Text + " " + txtCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblDepartment.Text + " - " + txtDepartmentCode.Text.Trim() + " - " + txtDepartmentDescription.Text.Trim());
                                Common.ClearTextBox(txtCategoryCode);
                                txtCategoryCode.Focus();
                                isValidControls = false;
                            }
                        }
                    }
                    else
                    {
                        Toast.Show(lblCategory.Text + " " + txtCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtCategoryCode);
                        txtCategoryCode.Focus();
                        isValidControls = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtCategoryDescription_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryDescription_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryDescription.Text.Trim() != string.Empty)
                {
                    InvCategoryService invCategoryService = new InvCategoryService();
                    InvCategory existingInvCategory = new InvCategory();

                    existingInvCategory = invCategoryService.GetInvCategoryByName(txtCategoryDescription.Text.Trim(), isDependSubCategory);


                    if (existingInvCategory != null)
                    {
                        if (!isDependCategory)
                        {
                            txtCategoryCode.Text = existingInvCategory.CategoryCode;
                            txtSubCategoryCode.Focus();
                        }
                        else
                        {
                            InvDepartmentService invDepartmentService = new Service.InvDepartmentService();

                            if (invDepartmentService.GetInvDepartmentsByID(existingInvCategory.InvDepartmentID, isDependCategory).DepartmentCode.Equals(txtDepartmentCode.Text.Trim()))
                            {
                                txtCategoryCode.Text = existingInvCategory.CategoryCode;
                                txtSubCategoryCode.Focus();
                            }
                            else
                            {
                                Toast.Show(lblCategory.Text + " " + txtCategoryDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblDepartment.Text + " - " + txtDepartmentCode.Text.Trim() + " - " + txtDepartmentDescription.Text.Trim());
                                Common.ClearTextBox(txtCategoryDescription);
                                txtCategoryDescription.Focus();
                            }
                        }
                    }
                    else
                    {
                        Toast.Show(lblCategory.Text + " " + txtCategoryDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtCategoryDescription);
                        txtCategoryDescription.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSubCategoryCode_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSubCategoryCode.Text.Trim() != string.Empty)
                {
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    InvSubCategory existingInvSubCategory = new InvSubCategory();

                    existingInvSubCategory = invSubCategoryService.GetInvSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2);

                    if (existingInvSubCategory != null)
                    {
                        if (!isDependCategory)
                        {
                            txtSubCategoryDescription.Text = existingInvSubCategory.SubCategoryName;
                            txtSubCategory2Code.Focus();
                        }
                        else
                        {
                            InvCategoryService invCategoryService = new Service.InvCategoryService();

                            if (invCategoryService.GetInvCategoryByID(existingInvSubCategory.InvCategoryID, isDependSubCategory2).CategoryCode.Equals(txtCategoryCode.Text.Trim()))
                            {
                                txtSubCategoryDescription.Text = existingInvSubCategory.SubCategoryName;
                                txtSubCategory2Code.Focus();
                            }
                            else
                            {
                                Toast.Show(lblSubCategory.Text + " " + txtSubCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblCategory.Text + " - " + txtCategoryCode.Text.Trim() + " - " + txtCategoryDescription.Text.Trim());
                                Common.ClearTextBox(txtSubCategoryCode);
                                txtSubCategoryCode.Focus();
                                isValidControls = false;
                            }
                        }
                    }
                    else
                    {
                        Toast.Show(lblSubCategory.Text + " " + txtSubCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtSubCategoryCode);
                        txtSubCategoryCode.Focus();
                        isValidControls = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void txtSubCategoryDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSubCategoryDescription_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryDescription_Leave(object sender, EventArgs e)
        {
            if (txtSubCategoryDescription.Text.Trim() != string.Empty)
            {
                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                InvSubCategory existingInvSubCategory = new InvSubCategory();

                existingInvSubCategory = invSubCategoryService.GetInvSubCategoryByName(txtSubCategoryDescription.Text.Trim(), isDependSubCategory2);

                if (existingInvSubCategory != null)
                {
                    if (!isDependCategory)
                    {
                        txtSubCategoryCode.Text = existingInvSubCategory.SubCategoryCode;
                        txtSubCategory2Code.Focus();
                    }
                    else
                    {
                        InvCategoryService invCategoryService = new Service.InvCategoryService();

                        if (invCategoryService.GetInvCategoryByID(existingInvSubCategory.InvCategoryID, isDependSubCategory2).CategoryCode.Equals(txtCategoryCode.Text.Trim()))
                        {
                            txtSubCategoryCode.Text = existingInvSubCategory.SubCategoryCode;
                            txtSubCategory2Code.Focus();
                        }
                        else
                        {
                            Toast.Show(lblSubCategory.Text + " " + txtSubCategoryDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblCategory.Text + " - " + txtCategoryCode.Text.Trim() + " - " + txtCategoryDescription.Text.Trim());
                            Common.ClearTextBox(txtSubCategoryDescription);
                            txtSubCategoryDescription.Focus();
                        }
                    }
                }
                else
                {
                    Toast.Show(lblSubCategory.Text + " " + txtSubCategoryDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    Common.ClearTextBox(txtSubCategoryDescription);
                    txtSubCategoryDescription.Focus();
                }
            }
        }

        private void txtSubCategory2Code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSubCategory2Code_Leave(this, e);
                    txtMainSupplierCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategory2Code_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSubCategory2Code.Text.Trim() != string.Empty)
                {
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    InvSubCategory2 existingInvSubCategory2 = new InvSubCategory2();

                    existingInvSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByCode(txtSubCategory2Code.Text.Trim());

                    if (existingInvSubCategory2 != null)
                    {
                        if (!isDependSubCategory2)
                        {
                            txtSubCategory2Description.Text = existingInvSubCategory2.SubCategory2Name;
                        }
                        else
                        {
                            InvSubCategoryService InvSubCategoryService = new Service.InvSubCategoryService();

                            if (InvSubCategoryService.GetInvSubCategoryByID(existingInvSubCategory2.InvSubCategoryID, isDependSubCategory2).SubCategoryCode.Equals(txtSubCategoryCode.Text.Trim()))
                            {
                                txtSubCategory2Description.Text = existingInvSubCategory2.SubCategory2Name;
                                txtMainSupplierCode.Focus();
                            }
                            else
                            {
                                Toast.Show(lblSubCategory2.Text + " " + txtSubCategory2Code.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblSubCategory.Text + " - " + txtSubCategoryCode.Text.Trim() + " - " + txtSubCategoryDescription.Text.Trim());
                                Common.ClearTextBox(txtSubCategory2Code);
                                txtSubCategory2Code.Focus();
                                isValidControls = false;
                            }
                        }
                    }
                    else
                    {
                        Toast.Show(lblSubCategory2.Text + " " + txtSubCategory2Code.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtSubCategory2Code);
                        txtSubCategory2Code.Focus();
                        isValidControls = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategory2Description_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSubCategory2Description_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategory2Description_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSubCategory2Description.Text.Trim() != string.Empty)
                {
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    InvSubCategory2 existingInvSubCategory2 = new InvSubCategory2();

                    existingInvSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByName(txtSubCategory2Description.Text.Trim());


                    if (existingInvSubCategory2 != null)
                    {
                        if (!isDependSubCategory2)
                        {
                            txtSubCategory2Code.Text = existingInvSubCategory2.SubCategory2Code;
                            txtMainSupplierCode.Focus();
                        }
                        else
                        {
                            InvSubCategoryService InvSubCategoryService = new Service.InvSubCategoryService();

                            if (InvSubCategoryService.GetInvSubCategoryByID(existingInvSubCategory2.InvSubCategoryID, isDependSubCategory2).SubCategoryCode.Equals(txtSubCategoryCode.Text.Trim()))
                            {
                                txtSubCategory2Code.Text = existingInvSubCategory2.SubCategory2Code;
                                txtMainSupplierCode.Focus();
                            }
                            else
                            {
                                Toast.Show(lblSubCategory2.Text + " " + txtSubCategory2Description.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblSubCategory.Text + " - " + txtSubCategoryCode.Text.Trim() + " - " + txtSubCategoryDescription.Text.Trim());
                                Common.ClearTextBox(txtSubCategory2Description);
                                txtSubCategory2Description.Focus();
                            }
                        }

                    }
                    else
                    {

                        Toast.Show(lblSubCategory2.Text + " " + txtSubCategory2Description.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtSubCategory2Description);
                        txtSubCategory2Description.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMaximumDiscountPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtMaximumDiscountPercentage_Leave(this, e);
                    txtFixedDiscount.Focus();
                    txtFixedDiscount.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMaximumDiscountPercentage_Leave(object sender, EventArgs e)
        {
            //txtFixedDiscount.Text = "0.00";
        }

        private void txtFixedDiscountPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtFixedDiscountPercentage_Leave(this, e);
                    txtWholesalePrice.Focus();
                    txtWholesalePrice.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtFixedDiscountPercentage_Leave(object sender, EventArgs e)
        {
            //txtWholesalePrice.Text = "0.00";
            //if (!Validater.ValidateTextBoxWithCustomerMessage("Invalid selling price", errorProvider, Validater.ValidateType.Empty, txtFixedDiscountPercentage))
            //{
            //    txtFixedDiscountPercentage.Focus();
            //}
        }

        private void AddValuesToLocationInfoGrid()
        {
            try
            {
                for (int i = 0; i < dgvLocationInfo.RowCount; i++)
                {
                    if (string.IsNullOrEmpty(txtReOrderLevel.Text.Trim())) { dgvLocationInfo.Rows[i].Cells["ReLevel"].Value = "0.00"; }
                    else { dgvLocationInfo.Rows[i].Cells["ReLevel"].Value = Convert.ToDecimal(txtReOrderLevel.Text.Trim()); } 

                    if (string.IsNullOrEmpty(txtReOrderQty.Text.Trim())) { dgvLocationInfo.Rows[i].Cells["ReQty"].Value = "0.00"; }
                    else { dgvLocationInfo.Rows[i].Cells["ReQty"].Value = Convert.ToDecimal(txtReOrderQty.Text.Trim()); }

                    if (string.IsNullOrEmpty(txtReOrderPeriod.Text.Trim())) { dgvLocationInfo.Rows[i].Cells["RePeriod"].Value = "0.00"; }
                    else { dgvLocationInfo.Rows[i].Cells["RePeriod"].Value = Convert.ToDecimal(txtReOrderPeriod.Text.Trim()); }

                    if (string.IsNullOrEmpty(txtCostPrice.Text.Trim())) { dgvLocationInfo.Rows[i].Cells["CostPrice"].Value = "0.00"; }
                    else { dgvLocationInfo.Rows[i].Cells["CostPrice"].Value = Convert.ToDecimal(txtCostPrice.Text.Trim()); }

                    if (string.IsNullOrEmpty(txtMinimumPrice.Text.Trim())) { dgvLocationInfo.Rows[i].Cells["MinimumPrice"].Value = "0.00"; }
                    else { dgvLocationInfo.Rows[i].Cells["MinimumPrice"].Value = Convert.ToDecimal(txtMinimumPrice.Text.Trim()); }

                    if (string.IsNullOrEmpty(txtSellingPrice.Text.Trim())) { dgvLocationInfo.Rows[i].Cells["SellingPrice"].Value = "0.00"; }
                    else { dgvLocationInfo.Rows[i].Cells["SellingPrice"].Value = Convert.ToDecimal(txtSellingPrice.Text.Trim()); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void AddDefaultValuesToLocationInfoGrid() 
        {
            try
            {
                for (int i = 0; i < dgvLocationInfo.RowCount; i++)
                {
                    dgvLocationInfo.Rows[i].Cells["ReLevel"].Value = "0.00";
                    dgvLocationInfo.Rows[i].Cells["ReQty"].Value = "0.00";
                    dgvLocationInfo.Rows[i].Cells["RePeriod"].Value = "0.00";
                    dgvLocationInfo.Rows[i].Cells["CostPrice"].Value = "0.00";
                    dgvLocationInfo.Rows[i].Cells["MinimumPrice"].Value = "0.00";
                    dgvLocationInfo.Rows[i].Cells["SellingPrice"].Value = "0.00";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAddDetails_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAddDetails.Checked)
                {
                    AddValuesToLocationInfoGrid();
                }
                else
                {
                    AddDefaultValuesToLocationInfoGrid();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkMargin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCostPrice.Text.Trim()))
                {
                    Toast.Show("Please enter cost price", Toast.messageType.Error, Toast.messageAction.General);
                    txtCostPrice.Focus();
                    return;
                }
                else
                {
                    txtSellingPrice.Text = string.Empty;
                    txtMargin.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkWholeSaleMargin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCostPrice.Text.Trim()))
                {
                    Toast.Show("Please enter cost price", Toast.messageType.Error, Toast.messageAction.General);
                    txtCostPrice.Focus();
                    return;
                }
                else
                {
                    txtWholesalePrice.Text = string.Empty;
                    txtWholesaleMargin.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void tabProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabProduct.SelectedIndex.Equals(3))
                {
                    if (!string.IsNullOrEmpty(txtNameOnInvoice.Text.Trim()))
                    {
                        txtUnitDescription.Text = txtNameOnInvoice.Text.Trim();
                        txtUnitDescription.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void UpdateProductLinkGrid()
        {
            InvProductMasterService InvProductMasterService = new InvProductMasterService();
            existingProduct = InvProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

            if (existingProduct != null)
            {
                InvProductLink invProductLink = new InvProductLink();

                invProductLink.ProductID = existingProduct.InvProductMasterID;
                invProductLink.ProductLinkCode = txtPluCode.Text.Trim();
                invProductLink.ProductLinkName = txtPluName.Text.Trim();

                invProductLinkGrid = InvProductMasterService.GetUpdateProductLinkTemp(invProductLinkGrid, invProductLink, existingProduct);
                dgvPluLink.DataSource = invProductLinkGrid;
                dgvPluLink.Refresh();
            }
            else
            {
                InvProductLink invProductLink = new InvProductLink();

                invProductLink.ProductID = 0;
                invProductLink.ProductLinkCode = txtPluCode.Text.Trim();
                invProductLink.ProductLinkName = txtPluName.Text.Trim();

                invProductLinkGrid = InvProductMasterService.GetUpdateProductLinkTemp(invProductLinkGrid, invProductLink, existingProduct);
                dgvPluLink.DataSource = invProductLinkGrid;
                dgvPluLink.Refresh();
            }

            txtPluCode.Text = string.Empty;
            txtPluName.Text = string.Empty;
        }


        private void txtPluCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    InvProductMaster existingProduct;

                    existingProduct = invProductMasterService.GetProductsByRefCodes(txtPluCode.Text.Trim());

                    if (existingProduct == null)
                    {
                        txtPluName.Focus();
                    }
                    else
                    {
                        Toast.Show("This code already exists", Toast.messageType.Information, Toast.messageAction.General);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPluName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtPluCode, txtPluName)) { return; }

                    UpdateProductLinkGrid();
                    txtPluCode.Focus();
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
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                InvProductMaster existingProduct;
                existingProduct = invProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());

                if (Toast.Show("Product - " + existingProduct.ProductCode + " - " + existingProduct.ProductName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                {
                    return;
                }

                existingProduct = invProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

                if (existingProduct != null && existingProduct.InvProductMasterID != 0)
                {
                    invProductMasterService.DeleteProduct(existingProduct);

                    Toast.Show("Product - " + existingProduct.ProductCode + " - " + existingProduct.ProductName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    ClearForm();
                    txtProductCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBatchBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    txtProductCode.Text = invProductMasterService.GetProductCodeFromBatchBarcode(Common.ConvertStringToLong(txtBatchBarcode.Text.Trim()));
                    txtProductCode_Leave(this, e);
                    tabProduct.SelectedTab = tbpOther;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPropertyName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    InvProductExtendedProperty invProductExtendedProperty = new InvProductExtendedProperty();
                    InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();

                    invProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByName(txtPropertyName.Text.Trim());

                    if (invProductExtendedProperty != null)
                    {
                        InvProductExtendedPropertyValueService invProductExtendedPropertyValueService = new InvProductExtendedPropertyValueService();
                        Common.SetAutoComplete(txtPropertyValue, invProductExtendedPropertyValueService.GetExtendedPropertyValuesAccordingToPropertyName(invProductExtendedProperty), true);
                        txtPropertyValue.Focus();
                    }
                    else
                    {
                        Toast.Show("Invalid property name", Toast.messageType.Information, Toast.messageAction.General);
                        txtPropertyName.Focus();
                        txtPropertyName.SelectAll();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPropertyValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                if (string.IsNullOrEmpty(txtPropertyName.Text.Trim()))
                {
                    Toast.Show("Please select valid property", Toast.messageType.Information, Toast.messageAction.General);
                    txtPropertyName.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPropertyValue.Text.Trim()))
                { return; }

                InvProductExtendedProperty invProductExtendedProperty = new InvProductExtendedProperty();
                InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();

                invProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByName(txtPropertyName.Text.Trim());

                if (!IsExtendedValueByNameByProperty(txtPropertyValue.Text, invProductExtendedProperty.InvProductExtendedPropertyID))
                {
                    txtPropertyValue.Focus();
                    return;
                }

                AssignProductExtendedProperties();
                txtPropertyName.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvExistingProducts_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvExistingProducts.CurrentCell != null && dgvExistingProducts.CurrentCell.RowIndex >= 0)
                {
                    txtProductCode.Text = dgvExistingProducts["ProductCode", dgvExistingProducts.CurrentCell.RowIndex].Value.ToString().Trim();
                    txtProductCode_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchProduct(e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationSubCategory_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void SearchProduct(EventArgs e)
        {
            List<InvProductProperty> invProductPropertyList = new List<InvProductProperty>();
            InvProductMasterService invProductMasterService = new InvProductMasterService();

            long departmentID = 0, CategoryID = 0, SubCategoryID = 0, subCategory2ID = 0, SupplierID = 0;
            string productFeature = "", country = "", cut = "", sleeve = "", heel = "", embelishment = "", fit = "", length = "", material = "", txture = "", neck = "",
                    collar = "", size = "", colour = "", patternNo = "", Brand = "", shop = "";
            decimal costPrice = 0, sellingPrice = 0;

            foreach (DataGridViewRow item in dgvExtendedProperties.Rows)
            {
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "PRODUCTFEATURE") { productFeature = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "COUNTRY") { country = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "CUT") { cut = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "SLEEVE") { sleeve = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "HEEL") { heel = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "EMBELISHMENT") { embelishment = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "FIT") { fit = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "LENGTH") { length = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "MATERIAL") { material = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "TEXTURE") { txture = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "NECK") { neck = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "COLLAR") { collar = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "SIZE") { size = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "COLOUR") { colour = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "PATTERNNO") { patternNo = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "BRAND") { Brand = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "SHOP") { shop = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
            }

            if (!string.IsNullOrEmpty(txtDepartmentCode.Text.Trim()))
            {
                InvDepartment invDepartment = new InvDepartment();
                InvDepartmentService invDepartmentService = new InvDepartmentService();
                invDepartment = invDepartmentService.GetInvDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDependCategory);
                departmentID = invDepartment.InvDepartmentID;
            }
            if (!string.IsNullOrEmpty(txtCategoryCode.Text.Trim()))
            {
                InvCategoryService invCategoryService = new InvCategoryService();
                InvCategory invCategory = new InvCategory();
                invCategory = invCategoryService.GetInvCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory);
                CategoryID = invCategory.InvCategoryID;
            }
            if (!string.IsNullOrEmpty(txtSubCategoryCode.Text.Trim()))
            {
                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                InvSubCategory invSubCategory = new InvSubCategory();
                invSubCategory = invSubCategoryService.GetInvSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2);
                SubCategoryID = invSubCategory.InvSubCategoryID;
            }
            if (!string.IsNullOrEmpty(txtSubCategory2Code.Text.Trim()))
            {
                InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                InvSubCategory2 invSubCategory2 = new InvSubCategory2();
                invSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByCode(txtSubCategory2Code.Text.Trim());
                subCategory2ID = invSubCategory2.InvSubCategory2ID;
            }
            if (!string.IsNullOrEmpty(txtMainSupplierCode.Text.Trim()))
            {
                SupplierService supplierService = new SupplierService();
                Supplier supplier = new Supplier();
                supplier = supplierService.GetSupplierByCode(txtMainSupplierCode.Text.Trim());
                SupplierID = supplier.SupplierID;
            }
            if (!string.IsNullOrEmpty(txtCostPrice.Text.Trim()))
            {
                costPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtSellingPrice.Text.Trim()))
            {
                sellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text.Trim());
            }

            DataView dvAllReferenceData = new DataView(invProductMasterService.GetSearchProductsDatatable(departmentID, CategoryID, SubCategoryID, subCategory2ID, SupplierID,
                                                            costPrice, sellingPrice, productFeature, country, cut, sleeve,
                                                            heel, embelishment, fit, length, material, txture, neck,
                                                            collar, size, colour, patternNo, Brand, shop));
            if (dvAllReferenceData.Count != 0)
            {
                LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), "", txtProductCode);
                txtProductCode_Leave(this, e);
            }

        }

    }
}
