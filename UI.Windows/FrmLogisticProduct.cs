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
using Report.Logistic;
using Report.Logistic.Reference.Reports;
using Utility;
using Service;
using System.Linq;
using System.IO;
namespace UI.Windows
{
    public partial class FrmLogisticProduct : UI.Windows.FrmBaseMasterForm
    {
        private LgsProductMaster existingProduct;
        List<LgsProductUnitConversion> lgsProductUnitConversionsGrid = new List<LgsProductUnitConversion>();
        List<LgsProductLink> lgsProductLinkGrid = new List<LgsProductLink>();
        List<LgsProductExtendedPropertyValue> lgsProductExtendedPropertyValueGrid = new List<LgsProductExtendedPropertyValue>();
        List<LgsProductSupplierLink> lgsProductSupplierLinkGrid = new List<LgsProductSupplierLink>();
        List<ProductLocationInfoTemp> productLocationInfoTempList = new List<ProductLocationInfoTemp>();

        UserPrivileges accessRights = new UserPrivileges();
        int documentID;

        string image;
        bool isDependCategory = false, isDependSubCategory = false, isDependSubCategory2 = false;
        bool isValidControls = true;
        bool codeDependOnDepartment, codeDependOnCategory, codeDependOnSubCategory, codeDependOnSubCategory2;

        public FrmLogisticProduct()
        {
            InitializeComponent();
        }

        private void FrmLogisticProduct_Load(object sender, EventArgs e)
        {

        }

        public override void FormLoad()
        {

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

            documentID = autoGenerateInfo.DocumentID;

            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);


            chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;

            dgvUnitConversion.AutoGenerateColumns = false;
            dgvLocationInfo.AutoGenerateColumns = false;
            dgvExtendedProperties.AutoGenerateColumns = false;
            dgvDisplayInfo.AutoGenerateColumns = false;

            dgvUnitConversion.AllowUserToAddRows = true;
            dgvLocationInfo.AllowUserToAddRows = false;
            dgvLocationInfo.AllowUserToDeleteRows = false;
            dgvPluLink.AllowUserToAddRows = true;
            dgvPluLink.AutoGenerateColumns = false;
            dgvMultiSupplier.AutoGenerateColumns = false;
            dgvExtendedProperties.AllowUserToAddRows = false;

            this.CausesValidation = false;
            lblDepartment.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticDepartment").FormText + "*";
            isDependCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").IsDepend;
            lblCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").FormText + "*";
            isDependSubCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").IsDepend;
            lblSubCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").FormText + "*";
            isDependSubCategory2 = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory2").IsDepend;
            lblSubCategory2.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory2").FormText + "*";

            LgsDepartmentService lgsDepartmentService = new Service.LgsDepartmentService();
            Common.SetAutoComplete(txtDepartmentCode, lgsDepartmentService.GetAllLgsDepartmentCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").IsDepend), chkAutoCompleationDepartment.Checked);
            Common.SetAutoComplete(txtDepartmentDescription, lgsDepartmentService.GetAllLgsDepartmentNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").IsDepend), chkAutoCompleationDepartment.Checked);

            LgsCategoryService lgsCategoryService = new Service.LgsCategoryService();
            Common.SetAutoComplete(txtCategoryCode, lgsCategoryService.GetAllLgsCategoryCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").IsDepend), chkAutoCompleationCategory.Checked);
            Common.SetAutoComplete(txtCategoryDescription, lgsCategoryService.GetAllLgsCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").IsDepend), chkAutoCompleationCategory.Checked);

            LgsSubCategoryService lgsSubCategoryService = new Service.LgsSubCategoryService();
            Common.SetAutoComplete(txtSubCategoryCode, lgsSubCategoryService.GetAllLgsSubCategoryCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory2").IsDepend), chkAutoCompleationSubCategory.Checked);
            Common.SetAutoComplete(txtSubCategoryDescription, lgsSubCategoryService.GetAllLgsSubCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory2").IsDepend), chkAutoCompleationSubCategory.Checked);

            LgsSubCategory2Service lgsSubCategory2Service = new Service.LgsSubCategory2Service();
            Common.SetAutoComplete(txtSubCategory2Code, lgsSubCategory2Service.GetAllLgsSubCategory2Codes(), chkAutoCompleationSubCategory2.Checked);
            Common.SetAutoComplete(txtSubCategory2Description, lgsSubCategory2Service.GetAllLgsSubCategory2Names(), chkAutoCompleationSubCategory2.Checked);

            LgsSupplierService supplierService = new Service.LgsSupplierService();
            Common.SetAutoComplete(txtMainSupplierCode, supplierService.GetAllLgsSupplierCodes(), chkAutoCompleationMainSupplier.Checked);
            Common.SetAutoComplete(txtMainSupplierDescription, supplierService.GetAllLgsSupplierNames(), chkAutoCompleationMainSupplier.Checked);

            Common.SetAutoComplete(txtSupplierLinkSupplierCode, supplierService.GetAllLgsSupplierCodes(), true);
            Common.SetAutoComplete(txtSupplierLinkSupplierName, supplierService.GetAllLgsSupplierNames(), true);

            AccLedgerAccountService AccLedgerAccountService = new Service.AccLedgerAccountService();
            Common.SetAutoComplete(txtPurchaseLedgerCode, AccLedgerAccountService.GetLedgerCodes(), chkAutoCompleationPurchaseLedger.Checked);
            Common.SetAutoComplete(txtPurchaseLedgerName, AccLedgerAccountService.GetLedgerNames(), chkAutoCompleationPurchaseLedger.Checked);

            Common.SetAutoComplete(txtOtherPurchaseLedgerCode, AccLedgerAccountService.GetLedgerCodes(), chkAutoCompleationOtherPurchaseLedger.Checked);
            Common.SetAutoComplete(txtOtherPurchaseLedgerName, AccLedgerAccountService.GetLedgerNames(), chkAutoCompleationOtherPurchaseLedger.Checked);

            Common.SetAutoComplete(txtSalesLedgerCode, AccLedgerAccountService.GetLedgerCodes(), chkAutoCompleationSalesLedger.Checked);
            Common.SetAutoComplete(txtSalesLedgerName, AccLedgerAccountService.GetLedgerNames(), chkAutoCompleationSalesLedger.Checked);

            Common.SetAutoComplete(txtOtherSalesLedgerCode, AccLedgerAccountService.GetLedgerCodes(), chkAutoCompleationOtherSalesLedger.Checked);
            Common.SetAutoComplete(txtOtherSalesLedgerName, AccLedgerAccountService.GetLedgerNames(), chkAutoCompleationOtherSalesLedger.Checked);

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

            InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
            Common.SetAutoComplete(txtPropertyName, invProductExtendedPropertyService.GetInvProductExtendedPropertyNames(), true);
            
            //AddCustomCoulmnstoGridUnitConversion();
            //AddCustomCoulmnPropertyNametoGridExtendedProperties();


            

            base.FormLoad();

            LoadDefaultLocation();
        }

        public override void InitializeForm()
        {
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

            Common.SetAutoComplete(txtProductCode, lgsProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
            Common.SetAutoComplete(txtProductName, lgsProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
            Common.EnableButton(true, btnNew);
            Common.EnableButton(false, btnSave, btnDelete);
            Common.EnableTextBox(true, txtProductCode, txtDepartmentCode, txtCategoryCode, txtSubCategoryCode, txtSubCategory2Code);
            Common.EnableTextBox(true, txtDepartmentDescription, txtCategoryDescription, txtSubCategoryDescription, txtSubCategory2Description);
            Common.ClearTextBox(txtProductCode);

            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
            if (accessRights.IsView == true) Common.EnableButton(true, btnView);

            //cmbCostingMethod.SelectedIndex = -1;

            lblCreatedDate.Text = string.Empty;
            lblCreatedBy.Text = string.Empty;
            lblModifiedDate.Text = string.Empty;
            lblModifiedBy.Text = string.Empty;

            lgsProductUnitConversionsGrid = null;
            lgsProductLinkGrid = null;
            lgsProductExtendedPropertyValueGrid = null;
            lgsProductSupplierLinkGrid = null;
            productLocationInfoTempList = null;

            lgsProductUnitConversionsGrid = new List<LgsProductUnitConversion>();
            lgsProductLinkGrid = new List<LgsProductLink>();
            lgsProductExtendedPropertyValueGrid = new List<LgsProductExtendedPropertyValue>();
            lgsProductSupplierLinkGrid = new List<LgsProductSupplierLink>();
            productLocationInfoTempList = new List<ProductLocationInfoTemp>();

            pbProductImage.Image = UI.Windows.Properties.Resources.Default_Product;

            CommonService commonService = new CommonService();
            cmbCostingMethod.Text = commonService.GetCostingMethodByLocation(Common.LoggedLocationID);

            tabProduct.SelectedTab = tbpGneral;

            ActiveControl = txtProductCode;
            txtProductCode.Focus();
        }

        private void chkAutoCompleationProduct_CheckedChanged(object sender, EventArgs e)
        {
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

            Common.SetAutoComplete(txtProductCode, lgsProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
            Common.SetAutoComplete(txtProductName, lgsProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
        }

        private void chkAutoCompleationDepartment_CheckedChanged(object sender, EventArgs e)
        {
            LgsDepartmentService lgsDepartmentService = new Service.LgsDepartmentService();
            Common.SetAutoComplete(txtDepartmentCode, lgsDepartmentService.GetAllLgsDepartmentCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").IsDepend), chkAutoCompleationDepartment.Checked);
            Common.SetAutoComplete(txtDepartmentDescription, lgsDepartmentService.GetAllLgsDepartmentNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").IsDepend), chkAutoCompleationDepartment.Checked);

        }

        private void chkAutoCompleationCategory_CheckedChanged(object sender, EventArgs e)
        {
            LgsCategoryService lgsCategoryService = new Service.LgsCategoryService();
            Common.SetAutoComplete(txtCategoryCode, lgsCategoryService.GetAllLgsCategoryCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").IsDepend), chkAutoCompleationCategory.Checked);
            Common.SetAutoComplete(txtCategoryDescription, lgsCategoryService.GetAllLgsCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").IsDepend), chkAutoCompleationCategory.Checked);

        }

        private void chkAutoCompleationMainSupplier_CheckedChanged(object sender, EventArgs e)
        {
            LgsSupplierService supplierService = new Service.LgsSupplierService();
            Common.SetAutoComplete(txtSubCategoryCode, supplierService.GetAllLgsSupplierCodes(), chkAutoCompleationMainSupplier.Checked);
            Common.SetAutoComplete(txtSubCategoryDescription, supplierService.GetAllLgsSupplierNames(), chkAutoCompleationMainSupplier.Checked);

        }

        private void chkAutoCompleationPurchaseLedger_CheckedChanged(object sender, EventArgs e)
        {
            AccLedgerAccountService AccLedgerAccountService = new Service.AccLedgerAccountService();
            Common.SetAutoComplete(txtPurchaseLedgerCode, AccLedgerAccountService.GetLedgerCodes(), chkAutoCompleationPurchaseLedger.Checked);
            Common.SetAutoComplete(txtPurchaseLedgerName, AccLedgerAccountService.GetLedgerNames(), chkAutoCompleationPurchaseLedger.Checked);

        }

        private void chkAutoCompleationSalesLedger_CheckedChanged(object sender, EventArgs e)
        {
            AccLedgerAccountService AccLedgerAccountService = new Service.AccLedgerAccountService();
            Common.SetAutoComplete(txtSalesLedgerCode, AccLedgerAccountService.GetLedgerCodes(), chkAutoCompleationSalesLedger.Checked);
            Common.SetAutoComplete(txtSalesLedgerName, AccLedgerAccountService.GetLedgerNames(), chkAutoCompleationSalesLedger.Checked);

        }

        private void chkAutoCompleationOtherPurchaseLedger_CheckedChanged(object sender, EventArgs e)
        {
            AccLedgerAccountService AccLedgerAccountService = new Service.AccLedgerAccountService();
            Common.SetAutoComplete(txtOtherPurchaseLedgerCode, AccLedgerAccountService.GetLedgerCodes(), chkAutoCompleationOtherPurchaseLedger.Checked);
            Common.SetAutoComplete(txtOtherPurchaseLedgerName, AccLedgerAccountService.GetLedgerNames(), chkAutoCompleationOtherPurchaseLedger.Checked);

        }

        private void chkAutoCompleationOtherSalesLedger_CheckedChanged(object sender, EventArgs e)
        {
            AccLedgerAccountService AccLedgerAccountService = new Service.AccLedgerAccountService();
            Common.SetAutoComplete(txtOtherSalesLedgerCode, AccLedgerAccountService.GetLedgerCodes(), chkAutoCompleationOtherSalesLedger.Checked);
            Common.SetAutoComplete(txtOtherSalesLedgerName, AccLedgerAccountService.GetLedgerNames(), chkAutoCompleationOtherSalesLedger.Checked);

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
            LgsDepartmentService lgsDepartmentService = new Service.LgsDepartmentService();
            LgsCategoryService lgsCategoryService = new Service.LgsCategoryService();
            LgsSubCategoryService lgsSubCategoryService = new Service.LgsSubCategoryService();
            LgsSubCategory2Service lgsSubCategory2Service = new Service.LgsSubCategory2Service();
            LgsSupplierService supplierService = new Service.LgsSupplierService();

            existingProduct.ProductCode = txtProductCode.Text.Trim();
            existingProduct.BarCode = txtBarCode.Text.Trim();
            existingProduct.ReferenceCode1 = txtReferenceCode1.Text.Trim();
            existingProduct.ReferenceCode2 = txtReferenceCode2.Text.Trim();
            existingProduct.ProductName = txtProductName.Text.Trim();
            existingProduct.NameOnInvoice = txtNameOnInvoice.Text.Trim();
            existingProduct.DepartmentID = lgsDepartmentService.GetLgsDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDependCategory).LgsDepartmentID;
            existingProduct.CategoryID = lgsCategoryService.GetLgsCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory).LgsCategoryID;
            existingProduct.SubCategoryID = lgsSubCategoryService.GetLgsSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2).LgsSubCategoryID;
            existingProduct.SubCategory2ID = lgsSubCategory2Service.GetLgsSubCategory2ByCode(txtSubCategory2Code.Text.Trim()).LgsSubCategory2ID;
            existingProduct.LgsSupplierID = supplierService.GetLgsSupplierByCode(txtMainSupplierCode.Text.Trim()).LgsSupplierID;
            

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
            if (existingProduct.LgsProductMasterID.Equals(0))
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
            existingProduct.IsTax = chkTax.Checked;
            existingProduct.IsSerial = chkSerial.Checked;
            existingProduct.IsService = chkIsService.Checked;

            existingProduct.OrderPrice = Common.ConvertStringToDecimalCurrency(txtOrderPrice.Text.Trim());

            existingProduct.Remarks = txtRemarks.Text.Trim();

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
            LgsProductExtendedPropertyValueService lgsProductExtendedPropertyValueService = new LgsProductExtendedPropertyValueService();
            LgsProductMasterService LgsProductMasterService = new LgsProductMasterService();
            existingProduct = LgsProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

            //List<InvProductExtendedPropertyValue> extendedPropertyValueList = new List<InvProductExtendedPropertyValue>();

            if (existingProduct != null)
            {
                lgsProductExtendedPropertyValueService.UpdateProductExtendedPropertyValueStatus(existingProduct.LgsProductMasterID);

                foreach (LgsProductExtendedPropertyValue temp in lgsProductExtendedPropertyValueGrid)
                {
                    LgsProductExtendedPropertyValue existingInvProductExtendedPropertyValue = new LgsProductExtendedPropertyValue();

                    existingInvProductExtendedPropertyValue.ProductID = existingProduct.LgsProductMasterID;
                    existingInvProductExtendedPropertyValue.LgsProductExtendedPropertyID = temp.LgsProductExtendedPropertyID;
                    existingInvProductExtendedPropertyValue.LgsProductExtendedValueID = temp.LgsProductExtendedValueID;
                    existingInvProductExtendedPropertyValue.IsDelete = false;

                    lgsProductExtendedPropertyValueService.AddLgsProductExtendedPropertyValues(existingInvProductExtendedPropertyValue);
                }
            }
            
        }


        private void SaveUnitConversion()
        {
            LgsProductUnitConversionService lgsProductUnitConversionService = new LgsProductUnitConversionService();
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
            existingProduct = lgsProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

            if (existingProduct != null)
            {
                lgsProductUnitConversionService.UpdateProductUnitConversionStatus(existingProduct.LgsProductMasterID);

                foreach (LgsProductUnitConversion temp in lgsProductUnitConversionsGrid)
                {
                    LgsProductUnitConversion existingLgsProductUnitConversion = new LgsProductUnitConversion();
                    existingLgsProductUnitConversion = lgsProductUnitConversionService.GetProductUnitWithDeletedByProductCode(existingProduct.LgsProductMasterID, temp.UnitOfMeasureID);

                    if (existingLgsProductUnitConversion == null || existingLgsProductUnitConversion.LgsProductUnitConversionID == 0)
                    {
                        existingLgsProductUnitConversion = new LgsProductUnitConversion();
                    }

                    existingLgsProductUnitConversion.ProductID = existingProduct.LgsProductMasterID;
                    existingLgsProductUnitConversion.Description = temp.Description;
                    existingLgsProductUnitConversion.UnitOfMeasureID = temp.UnitOfMeasureID;
                    existingLgsProductUnitConversion.ConvertFactor = temp.ConvertFactor;
                    existingLgsProductUnitConversion.SellingPrice = temp.SellingPrice;
                    existingLgsProductUnitConversion.MinimumPrice = temp.SellingPrice;
                    existingLgsProductUnitConversion.CostPrice = temp.CostPrice;
                    existingLgsProductUnitConversion.IsDelete = false;

                    if (existingLgsProductUnitConversion.LgsProductUnitConversionID.Equals(0))
                    {
                        lgsProductUnitConversionService.AddProductUnitConversion(existingLgsProductUnitConversion);
                    }
                    else
                    {
                        lgsProductUnitConversionService.AddProductUnitConversion(existingLgsProductUnitConversion);
                    }
                }
            }
        }


        private void SaveProductLink()
        {
            if (existingProduct != null)
            {
                LgsProductLinkService lgsProductLinkService = new LgsProductLinkService();
                LgsProductLink existingLgsProductLink = new LgsProductLink();

                lgsProductLinkService.UpdateProductLinkStatus(existingProduct.LgsProductMasterID);
                if (dgvPluLink.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvPluLink.Rows)
                    {
                        if (dgvPluLink["tbpPluLinkCode", row.Index].Value != null )
                        {
                            existingLgsProductLink = lgsProductLinkService.GetProductLinkWithDeletedByProductCode(existingProduct.LgsProductMasterID, dgvPluLink["tbpPluLinkCode", row.Index].Value.ToString().Trim());

                            if (existingLgsProductLink == null || existingLgsProductLink.LgsProductLinkID == 0)
                            { existingLgsProductLink = new LgsProductLink(); }

                            existingLgsProductLink.ProductID = existingProduct.LgsProductMasterID;
                            existingLgsProductLink.ProductLinkCode = dgvPluLink["tbpPluLinkCode", row.Index].Value.ToString().Trim();
                            existingLgsProductLink.ProductLinkName = dgvPluLink["tbpPluLinkDescription", row.Index].Value.ToString().Trim();

                            existingLgsProductLink.IsDelete = false;

                            if (existingLgsProductLink.LgsProductLinkID.Equals(0))
                            {
                                lgsProductLinkService.AddProductLink(existingLgsProductLink);
                            }
                            else
                            {
                                lgsProductLinkService.UpdateProductLink(existingLgsProductLink);
                            }
                        }
                    }
                }
            }
        }



        private void SaveSupplierLink()
        {
            if (existingProduct != null)
            {
                LgsProductSupplierLinkService lgsProductSupplierLinkService = new LgsProductSupplierLinkService();
                LgsProductSupplierLink existingLgsProductSupplierLink = new LgsProductSupplierLink();
                LgsSupplierService supplierService = new LgsSupplierService();
                LgsSupplier existingSupplier = new LgsSupplier();
                lgsProductSupplierLinkService.UpdateLgsProductSupplierLinkStatus(existingProduct.LgsProductMasterID);
                if (dgvMultiSupplier.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvMultiSupplier.Rows)
                    {
                        if (dgvMultiSupplier["tbpMultiSupplierCode", row.Index].Value != null)
                        {
                            existingSupplier = supplierService.GetLgsSupplierByCode(dgvMultiSupplier["tbpMultiSupplierCode", row.Index].Value.ToString().Trim());
                            existingLgsProductSupplierLink = lgsProductSupplierLinkService.GetLgsProductSupplierLinkWithDeletedByProductCode(existingProduct.LgsProductMasterID, existingSupplier.LgsSupplierID);

                            if (existingLgsProductSupplierLink == null || existingLgsProductSupplierLink.LgsProductSupplierLinkID == 0)
                            { existingLgsProductSupplierLink = new LgsProductSupplierLink(); }

                            existingLgsProductSupplierLink.ProductID = existingProduct.LgsProductMasterID;
                            existingLgsProductSupplierLink.SupplierID = existingSupplier.LgsSupplierID;
                            existingLgsProductSupplierLink.CostPrice = Common.ConvertStringToDecimalCurrency(dgvMultiSupplier["tbsupplinkCostPrice", row.Index].Value.ToString().Trim());
                            existingLgsProductSupplierLink.FixedGP = Common.ConvertStringToDecimal(dgvMultiSupplier["FixedGP", row.Index].Value.ToString().Trim());
                            existingLgsProductSupplierLink.ReferenceDocumentNo = string.Empty;
                            existingLgsProductSupplierLink.DocumentDate = DateTime.Now;
                            existingLgsProductSupplierLink.IsDelete = false;

                            if (existingLgsProductSupplierLink.LgsProductSupplierLinkID.Equals(0))
                            {
                                lgsProductSupplierLinkService.AddLgsProductSupplierLink(existingLgsProductSupplierLink);
                            }
                            else
                            {
                                lgsProductSupplierLinkService.UpdateLgsProductSupplierLink(existingLgsProductSupplierLink);
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
                LgsProductStockMasterService lgsProductStockMasterService = new LgsProductStockMasterService();
                LgsProductStockMaster existingLgsProductStockMaster = new LgsProductStockMaster();

                if (dgvLocationInfo.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvLocationInfo.Rows)
                    {
                        if (dgvLocationInfo["Location", row.Index].Value != null && !dgvLocationInfo["Location", row.Index].Value.ToString().Trim().Equals(string.Empty))
                        {
                            existingLgsProductStockMaster = lgsProductStockMasterService.GetProductStockMasterWithDeletedByProductID(existingProduct.LgsProductMasterID, Common.ConvertStringToInt(dgvLocationInfo["LocationID", row.Index].Value.ToString().Trim()));

                            if (existingLgsProductStockMaster == null || existingLgsProductStockMaster.LgsProductStockMasterID == 0)
                            { existingLgsProductStockMaster = new LgsProductStockMaster(); }

                            Location location = new Location();
                            LocationService locationService = new LocationService();
                            location = locationService.GetLocationsByID(Common.LoggedLocationID);

                            if (location != null) { existingLgsProductStockMaster.CostCentreID = location.CostCentreID; }
                            else { existingLgsProductStockMaster.CostCentreID = 0; }

                            existingLgsProductStockMaster.CompanyID = Common.LoggedCompanyID;
                            existingLgsProductStockMaster.LocationID = Common.ConvertStringToInt(dgvLocationInfo["LocationID", row.Index].Value.ToString().Trim());
                            
                            existingLgsProductStockMaster.ProductID = existingProduct.LgsProductMasterID;

                            if (dgvLocationInfo["Selection", row.Index].Value == null || Common.ConvertStringToBool(dgvLocationInfo["Selection", row.Index].Value.ToString().Trim()).Equals(false))
                            {
                                existingLgsProductStockMaster.IsDelete = true;
                                existingLgsProductStockMaster.ReOrderLevel = 0;
                                existingLgsProductStockMaster.ReOrderQuantity = 0;
                                existingLgsProductStockMaster.ReOrderPeriod = 0;

                                existingLgsProductStockMaster.CostPrice = 0;
                                existingLgsProductStockMaster.SellingPrice = 0;
                                existingLgsProductStockMaster.MinimumPrice = 0;
                            }
                            else
                            {
                                existingLgsProductStockMaster.IsDelete = false;

                                if (dgvLocationInfo["ReLevel", row.Index].Value == null) { existingLgsProductStockMaster.ReOrderLevel = Common.ConvertStringToDecimal("0.00"); }
                                else
                                {
                                    if (dgvLocationInfo["ReLevel", row.Index].Value.ToString() != string.Empty)
                                        existingLgsProductStockMaster.ReOrderLevel = Common.ConvertStringToDecimal(dgvLocationInfo["ReLevel", row.Index].Value.ToString().Trim());
                                    else
                                        existingLgsProductStockMaster.ReOrderLevel = Common.ConvertStringToDecimal("0.00");
                                }

                                if (dgvLocationInfo["ReQty", row.Index].Value == null) { existingLgsProductStockMaster.ReOrderQuantity = Common.ConvertStringToDecimal("0.00"); }
                                else
                                {
                                    if (dgvLocationInfo["ReQty", row.Index].Value.ToString().Trim() != string.Empty)
                                        existingLgsProductStockMaster.ReOrderQuantity = Common.ConvertStringToDecimal(dgvLocationInfo["ReQty", row.Index].Value.ToString().Trim());
                                    else
                                        existingLgsProductStockMaster.ReOrderQuantity = Common.ConvertStringToDecimal("0.00");
                                }

                                if (dgvLocationInfo["RePeriod", row.Index].Value == null) { existingLgsProductStockMaster.ReOrderPeriod = Common.ConvertStringToDecimal("0.00"); }
                                else
                                {
                                    if (dgvLocationInfo["RePeriod", row.Index].Value.ToString() != string.Empty)
                                        existingLgsProductStockMaster.ReOrderPeriod = Common.ConvertStringToDecimal(dgvLocationInfo["RePeriod", row.Index].Value.ToString().Trim());
                                    else
                                        existingLgsProductStockMaster.ReOrderPeriod = Common.ConvertStringToDecimal("0.00");
                                }

                                if (dgvLocationInfo["CostPrice", row.Index].Value == null) { existingLgsProductStockMaster.CostPrice = Common.ConvertStringToDecimal("0.00"); }
                                else
                                {
                                    if (dgvLocationInfo["CostPrice", row.Index].Value.ToString() != null)
                                        existingLgsProductStockMaster.CostPrice = Common.ConvertStringToDecimal(dgvLocationInfo["CostPrice", row.Index].Value.ToString().Trim());
                                    else
                                        existingLgsProductStockMaster.CostPrice = Common.ConvertStringToDecimal("0.00");
                                }

                                if (dgvLocationInfo["SellingPrice", row.Index].Value == null) { existingLgsProductStockMaster.SellingPrice = Common.ConvertStringToDecimal("0.00"); }
                                else
                                {
                                    if (dgvLocationInfo["SellingPrice", row.Index].Value.ToString() != string.Empty)
                                        existingLgsProductStockMaster.SellingPrice = Common.ConvertStringToDecimal(dgvLocationInfo["SellingPrice", row.Index].Value.ToString().Trim());
                                    else
                                        existingLgsProductStockMaster.SellingPrice = Common.ConvertStringToDecimal("0.00");
                                }

                                if (dgvLocationInfo["MinimumPrice", row.Index].Value == null) { existingLgsProductStockMaster.MinimumPrice = Common.ConvertStringToDecimal("0.00"); }
                                else
                                {
                                    if (dgvLocationInfo["MinimumPrice", row.Index].Value.ToString() != string.Empty)
                                        existingLgsProductStockMaster.MinimumPrice = Common.ConvertStringToDecimal(dgvLocationInfo["MinimumPrice", row.Index].Value.ToString().Trim());
                                    else
                                        existingLgsProductStockMaster.MinimumPrice = Common.ConvertStringToDecimal("0.00");
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

                            if (existingLgsProductStockMaster.LgsProductStockMasterID.Equals(0))
                            {
                                lgsProductStockMasterService.AddProductStockMaster(existingLgsProductStockMaster);
                            }
                            else
                            {
                                lgsProductStockMasterService.UpdateProductStockMaster(existingLgsProductStockMaster);
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

                LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                bool isNew = false;
                existingProduct = lgsProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

                if (existingProduct == null || existingProduct.LgsProductMasterID == 0)
                {
                    existingProduct = new LgsProductMaster();
                    isNew = true;
                }

                //Tab General
                FillProduct();


                //Tab Unit Conversion

                if (existingProduct.LgsProductMasterID == 0)
                {
                    if ((Toast.Show("" + this.Text + " - " + existingProduct.ProductCode + " - " + existingProduct.ProductName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    lgsProductMasterService.AddProduct(existingProduct);
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
                    lgsProductMasterService.UpdateProduct(existingProduct);

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
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticProduct");
                LgsReportGenerator invReportGenerator = new LgsReportGenerator();
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
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    txtProductCode.Text = lgsProductMasterService.GetDirectNewCode(this.Name);
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
                        LgsDepartment lgsDepartment = new LgsDepartment();
                        LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                        lgsDepartment = lgsDepartmentService.GetLgsDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDependCategory);

                        if (lgsDepartment != null)
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
                        LgsCategory lgsCategory = new LgsCategory();
                        LgsCategoryService lgsCategoryService = new LgsCategoryService();
                        lgsCategory = lgsCategoryService.GetLgsCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory);

                        if (lgsCategory != null)
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
                        LgsSubCategory lgsSubCategory = new LgsSubCategory();
                        LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
                        lgsSubCategory = lgsSubCategoryService.GetLgsSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2);

                        if (lgsSubCategory != null)
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
                        LgsSubCategory2 lgsSubCategory2 = new LgsSubCategory2();
                        LgsSubCategory2Service lgsSubCategory2Service = new LgsSubCategory2Service();
                        lgsSubCategory2 = lgsSubCategory2Service.GetLgsSubCategory2ByCode(txtSubCategory2Code.Text.Trim());

                        if (lgsSubCategory2 != null)
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
                LgsDepartment dpt = new LgsDepartment();
                LgsDepartmentService dptService = new LgsDepartmentService();
                dpt = dptService.GetLgsDepartmentsByCode(txtDepartmentCode.Text.Trim(), true);

                LgsCategory cat = new LgsCategory();
                LgsCategoryService catService = new LgsCategoryService();
                cat = catService.GetLgsCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory);

                LgsSubCategory subCat = new LgsSubCategory();
                LgsSubCategoryService subCatService = new LgsSubCategoryService();
                subCat = subCatService.GetLgsSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2);
                /////

                LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                txtProductCode.Text = lgsProductMasterService.GetNewCode(this.Name, productCode, dpt.LgsDepartmentID, cat.LgsCategoryID, subCat.LgsSubCategoryID);

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

        private void FrmLogisticProduct_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void FrmLogisticProduct_KeyDown(object sender, KeyEventArgs e)
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
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    DataView dvAllReferenceData = new DataView(lgsProductMasterService.GetProductsDataTable());
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
            LgsSubCategory2Service lgsSubCategory2Service = new Service.LgsSubCategory2Service();
            Common.SetAutoComplete(txtSubCategory2Code, lgsSubCategory2Service.GetAllLgsSubCategory2Codes(), chkAutoCompleationSubCategory2.Checked);
            Common.SetAutoComplete(txtSubCategory2Description, lgsSubCategory2Service.GetAllLgsSubCategory2Names(), chkAutoCompleationSubCategory2.Checked);
        }

        private void txtProductCode_Leave(object sender, EventArgs e)
        {
            if (txtProductCode.Text.Trim() != string.Empty)
            {
                LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                LgsProductMaster existingProduct;

                existingProduct = lgsProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());

                if (existingProduct != null)
                {
                    LoadProductMaster(existingProduct);
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

                txtBarCode.Focus();
            }
        }


        private void LoadProductMaster(LgsProductMaster existingProduct)
        {
            //Tab General

            LgsDepartmentService lgsDepartmentService = new Service.LgsDepartmentService();
            LgsCategoryService lgsCategoryService = new Service.LgsCategoryService();
            LgsSubCategoryService lgsSubCategoryService = new Service.LgsSubCategoryService();
            LgsSupplierService supplierService = new Service.LgsSupplierService();
            LgsSubCategory2Service lgsSubCategory2Service = new LgsSubCategory2Service();
            LgsDepartment lgsDepartment;
            LgsCategory lgsCategory;
            LgsSubCategory lgsSubCategory;
            LgsSubCategory2 lgsSubCategory2;
            LgsSupplier supplier;

            txtProductCode.Text = existingProduct.ProductCode;
            txtBarCode.Text = existingProduct.BarCode;
            txtReferenceCode1.Text = existingProduct.ReferenceCode1;
            txtReferenceCode2.Text = existingProduct.ReferenceCode2;
            txtProductName.Text = existingProduct.ProductName;
            txtNameOnInvoice.Text = existingProduct.NameOnInvoice;
            lgsDepartment = lgsDepartmentService.GetLgsDepartmentsByID(existingProduct.DepartmentID, isDependCategory);
            if (lgsDepartment != null)
            {
                txtDepartmentCode.Text = lgsDepartment.DepartmentCode;
                txtDepartmentDescription.Text = lgsDepartment.DepartmentName;
            }
            else
            {
                Common.ClearTextBox(txtDepartmentCode, txtDepartmentDescription);
                Toast.Show(lblDepartment.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }

            lgsCategory = lgsCategoryService.GetLgsCategoryByID(existingProduct.CategoryID, isDependSubCategory);
            if (lgsCategory != null)
            {
                txtCategoryCode.Text = lgsCategory.CategoryCode;
                txtCategoryDescription.Text = lgsCategory.CategoryName;
            }
            else
            {
                Common.ClearTextBox(txtCategoryCode, txtCategoryDescription);
                Toast.Show(lblCategory.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }

            lgsSubCategory = lgsSubCategoryService.GetLgsSubCategoryByID(existingProduct.SubCategoryID, isDependSubCategory2);
            if (lgsSubCategory != null)
            {
                txtSubCategoryCode.Text = lgsSubCategory.SubCategoryCode;
                txtSubCategoryDescription.Text = lgsSubCategory.SubCategoryName;
            }
            else
            {
                Common.ClearTextBox(txtSubCategoryCode, txtSubCategoryDescription);
                Toast.Show(lblSubCategory.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }

            lgsSubCategory2 = lgsSubCategory2Service.GetLgsSubCategory2ByID(existingProduct.SubCategory2ID);
            if (lgsSubCategory2 != null)
            {
                txtSubCategory2Code.Text = lgsSubCategory2.SubCategory2Code;
                txtSubCategory2Description.Text = lgsSubCategory2.SubCategory2Name;
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


            supplier = supplierService.GetLgsSupplierByID(existingProduct.LgsSupplierID);
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
            chkIsService.Checked = existingProduct.IsService;

            txtOrderPrice.Text = Common.ConvertDecimalToStringCurrency(existingProduct.OrderPrice);

            txtRemarks.Text = existingProduct.Remarks;

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

            LoadUnitConversion(existingProduct.LgsProductMasterID);
            LoadLocationInfo(existingProduct.LgsProductMasterID);
            LoadProductLink(existingProduct.LgsProductMasterID);
            LoadSupplierLink(existingProduct.LgsProductMasterID);
            LoadInfo(existingProduct.LgsProductMasterID);
            LoadProductExtendedPropertyValue(existingProduct.LgsProductMasterID);
            if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
            if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
            Common.EnableButton(false, btnNew);

            tabProduct.SelectedTab = tbpGneral;
        }

        private void LoadProductExtendedPropertyValue(long productID)
        {
            LgsProductExtendedPropertyValueService lgsProductExtendedPropertyValueService = new LgsProductExtendedPropertyValueService();

            lgsProductExtendedPropertyValueGrid = lgsProductExtendedPropertyValueService.GetProductExtendedPropertyValuesByProductID(productID);

            if (lgsProductExtendedPropertyValueGrid.Count > 0) { dgvExtendedProperties.DataSource = lgsProductExtendedPropertyValueGrid; }
            else { dgvExtendedProperties.DataSource = null; }
            
            dgvExtendedProperties.Refresh();
        }


        private void LoadUnitConversion(long productID)
        {
            LgsProductUnitConversionService lgsProductUnitConversionService = new LgsProductUnitConversionService();       
            lgsProductUnitConversionsGrid = lgsProductUnitConversionService.GetAllProductUnitConversionByProductCode(productID);
            if (lgsProductUnitConversionsGrid.Count > 0)
                dgvUnitConversion.DataSource = lgsProductUnitConversionsGrid;
            else
                dgvUnitConversion.DataSource = null;
            dgvUnitConversion.Refresh();
            
           
        }
        private void LoadProductLink(long productID)
        {
            LgsProductLinkService lgsProductLinkService = new LgsProductLinkService();
            lgsProductLinkGrid = lgsProductLinkService.GetAllProductLinkByProductCode(productID);
            if (lgsProductLinkGrid.Count > 0)
                dgvPluLink.DataSource = lgsProductLinkGrid;
            else
                dgvPluLink.DataSource = null;
            dgvPluLink.Refresh();

        }

        private void LoadSupplierLink(long productID)
        {
            LgsProductSupplierLinkService lgsProductSupplierLinkService = new LgsProductSupplierLinkService();
            lgsProductSupplierLinkGrid = lgsProductSupplierLinkService.GetSupplierLink(productID);

            if (lgsProductSupplierLinkGrid.Count > 0) { dgvMultiSupplier.DataSource = lgsProductSupplierLinkGrid; }
            else { dgvMultiSupplier.DataSource = null; }
                
            dgvMultiSupplier.Refresh();

        }

        private void AddNewRowToUnitConversion()
        {
            LgsProductUnitConversion lgsProductUnitConversion = new LgsProductUnitConversion();
            lgsProductUnitConversion.UnitOfMeasureID = 1;
            lgsProductUnitConversionsGrid.Add(lgsProductUnitConversion);
            dgvUnitConversion.DataSource = null;
            dgvUnitConversion.DataSource = lgsProductUnitConversionsGrid;
            dgvUnitConversion.Refresh();
        }

        private void AddNewRowToExtendedProperties()
        {
            LgsProductExtendedPropertyValue lgsProductExtendedPropertyValue = new LgsProductExtendedPropertyValue();
            lgsProductExtendedPropertyValue.LgsProductExtendedPropertyID = 1;
            lgsProductExtendedPropertyValueGrid.Add(lgsProductExtendedPropertyValue);
            dgvExtendedProperties.DataSource = null;
            dgvExtendedProperties.DataSource = lgsProductExtendedPropertyValueGrid;

            dgvExtendedProperties.Refresh();
        }

        private void AddNewRowToPLULink()
        {
            LgsProductLink lgsProductLink = new LgsProductLink();
            lgsProductLinkGrid.Add(lgsProductLink);
            dgvPluLink.DataSource = null;
            dgvPluLink.DataSource = lgsProductLinkGrid;

            dgvPluLink.Refresh();
        }

      

        private void txtBarCode_Validated(object sender, EventArgs e)
        {
            LgsProductMasterService invProductMasterService = new LgsProductMasterService();
            LgsProductMaster existingProduct;

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

        private void txtMainSupplierCode_Validated(object sender, EventArgs e)
        {
            if (txtMainSupplierCode.Text.Trim() != string.Empty)
            {
                LgsSupplierService supplierService = new LgsSupplierService();
                LgsSupplier existingSupplier = new LgsSupplier();
                existingSupplier = supplierService.GetLgsSupplierByCode(txtMainSupplierCode.Text.Trim());

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

   
        private void txtReferenceCode1_Validated(object sender, EventArgs e)
        {
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
            LgsProductMaster existingProduct;

            if (txtReferenceCode1.Text.Trim() != string.Empty && (txtProductCode.Enabled))
            {
                existingProduct = lgsProductMasterService.GetProductsByRefCodes(txtReferenceCode1.Text.Trim());

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
                existingProduct = lgsProductMasterService.GetProductsByRefCodes(txtReferenceCode1.Text.Trim());

                if (existingProduct != null && existingProduct.ProductCode != txtProductCode.Text.Trim())
                {
                    Toast.Show("Reference Code 1 - " + txtBarCode.Text.Trim() + "\nis already exists with Product - " + existingProduct.ProductCode + " - " + existingProduct.ProductName + "", Toast.messageType.Error, Toast.messageAction.General);
                    //txtReferenceCode1.Text = string.Empty;
                    txtReferenceCode1.Focus();
                    isValidControls = false;
                }
            }
        }

        private void txtReferenceCode2_Validated(object sender, EventArgs e)
        {
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
            LgsProductMaster existingProduct;

            if (txtReferenceCode2.Text.Trim() != string.Empty && (txtProductCode.Enabled))
            {
                existingProduct = lgsProductMasterService.GetProductsByRefCodes(txtReferenceCode2.Text.Trim());

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
                existingProduct = lgsProductMasterService.GetProductsByRefCodes(txtReferenceCode2.Text.Trim());

                if (existingProduct != null && existingProduct.ProductCode != txtProductCode.Text.Trim())
                {
                    Toast.Show("Reference Code 2 - " + txtReferenceCode2.Text.Trim() + "\nis already exists with Product - " + existingProduct.ProductCode + " - " + existingProduct.ProductName + "", Toast.messageType.Error, Toast.messageAction.General);
                    txtReferenceCode2.Focus();
                    isValidControls = false;
                }
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
            LgsProductStockMasterService lgsProductStockMasterService = new LgsProductStockMasterService();
            if (lgsProductStockMasterService.GetProductStockMasterByProductID(productID) == null)
            {
                LoadDefaultLocation();
            }
            else
            {
                dgvLocationInfo.DataSource = null;
                dgvLocationInfo.DataSource = lgsProductStockMasterService.GetLocationInfo(productID);
                dgvLocationInfo.Refresh();
            }
        }

        private void LoadInfo(long productID)
        {
            LgsProductStockMasterService lgsProductStockMasterService = new LgsProductStockMasterService();
            if (!(lgsProductStockMasterService.GetProductStockMasterByProductID(productID) == null))          
            {
                dgvDisplayInfo.DataSource = null;
                dgvDisplayInfo.DataSource = lgsProductStockMasterService.GetInfo(productID);
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
            if (txtMainSupplierDescription.Text.Trim() != string.Empty)
            {
                LgsSupplierService supplierService = new LgsSupplierService();
                LgsSupplier existingSupplier = new LgsSupplier();
                existingSupplier = supplierService.GetLgsSupplierByName(txtMainSupplierDescription.Text.Trim());

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

                    LgsProductSupplierLinkService lgsProductSupplierLinkService = new LgsProductSupplierLinkService();
                    LgsProductSupplierLink existingLgsProductSupplierLink = new LgsProductSupplierLink();
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    
                    existingProduct = lgsProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());

                    LgsSupplierService supplierService = new LgsSupplierService();

                    if (existingProduct != null && dgvMultiSupplier["tbpMultiSupplierCode", dgvMultiSupplier.CurrentCell.RowIndex].Value != null)
                    {
                        existingLgsProductSupplierLink = lgsProductSupplierLinkService.GetLgsProductSupplierLinkWithDeletedByProductCode(existingProduct.LgsProductMasterID, supplierService.GetLgsSupplierByCode(dgvMultiSupplier["tbpMultiSupplierCode", dgvMultiSupplier.CurrentCell.RowIndex].Value.ToString().Trim()).LgsSupplierID);
                    }

                    if (existingLgsProductSupplierLink != null && !existingLgsProductSupplierLink.SupplierID.Equals(0))
                    {
                        var existingLgsProductSupplierLinkToDelete = lgsProductSupplierLinkGrid.First(s => s.ProductID == existingProduct.LgsProductMasterID && s.SupplierID == supplierService.GetLgsSupplierByCode(dgvMultiSupplier["tbpMultiSupplierCode", dgvMultiSupplier.CurrentCell.RowIndex].Value.ToString().Trim()).LgsSupplierID);
                        lgsProductSupplierLinkGrid.Remove(existingLgsProductSupplierLinkToDelete);
                        dgvMultiSupplier.DataSource = null;
                        dgvMultiSupplier.DataSource = lgsProductSupplierLinkGrid;
                    }
                    else
                    {
                        var existingInvProductSupplierLinkToDelete = lgsProductSupplierLinkGrid.First(s => s.SupplierID  == supplierService.GetLgsSupplierByCode(dgvMultiSupplier["tbpMultiSupplierCode", dgvMultiSupplier.CurrentCell.RowIndex].Value.ToString().Trim()).LgsSupplierID);
                        lgsProductSupplierLinkGrid.Remove(existingInvProductSupplierLinkToDelete);
                        dgvMultiSupplier.DataSource = null;
                        dgvMultiSupplier.DataSource = lgsProductSupplierLinkGrid;
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

                    LgsProductUnitConversionService lgsProductUnitConversionService = new LgsProductUnitConversionService();
                    LgsProductUnitConversion existingLgsProductUnitConversion = new LgsProductUnitConversion();
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                    existingProduct = lgsProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());

                    if (existingProduct != null && dgvUnitConversion["tbpUnitConversionUnit", dgvUnitConversion.CurrentCell.RowIndex].Value != null)
                    {
                        UnitOfMeasure unitOfMeasure = new UnitOfMeasure();
                        UnitOfMeasureService UnitOfMeasureService = new UnitOfMeasureService();
                        unitOfMeasure = UnitOfMeasureService.GetUnitOfMeasureByName(dgvUnitConversion["tbpUnitConversionUnit", dgvUnitConversion.CurrentCell.RowIndex].Value.ToString().Trim());

                        existingLgsProductUnitConversion = lgsProductUnitConversionService.GetProductUnitByProductCode(existingProduct.LgsProductMasterID, unitOfMeasure.UnitOfMeasureID);
                    }
                    if (existingLgsProductUnitConversion != null)
                    {
                        //var existingInvProductUnitConversionToDelete = invProductUnitConversionsGrid.First(u => u.ProductID == existingProduct.InvProductMasterID && u.UnitOfMeasureID == Common.ConvertStringToLong(dgvUnitConversion["tbpUnitConversionUnit", dgvUnitConversion.CurrentCell.RowIndex].Value.ToString().Trim()));
                        var existingLgsProductUnitConversionToDelete = lgsProductUnitConversionsGrid.First(u => u.ProductID == existingProduct.LgsProductMasterID && u.UnitOfMeasureID == existingLgsProductUnitConversion.UnitOfMeasureID);
                        lgsProductUnitConversionsGrid.Remove(existingLgsProductUnitConversionToDelete);
                        dgvUnitConversion.DataSource = null;
                        dgvUnitConversion.DataSource = lgsProductUnitConversionsGrid;
                    }
                    else
                    {
                        var existingLgsProductUnitConversionToDelete = lgsProductUnitConversionsGrid.First(u => u.UnitOfMeasureName == dgvUnitConversion["tbpUnitConversionUnit", dgvUnitConversion.CurrentCell.RowIndex].Value.ToString().Trim());
                        lgsProductUnitConversionsGrid.Remove(existingLgsProductUnitConversionToDelete);
                        dgvUnitConversion.DataSource = null;
                        dgvUnitConversion.DataSource = lgsProductUnitConversionsGrid;
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

                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    LgsProductExtendedPropertyValue existinglgsProductExtendedPropertyValue = new LgsProductExtendedPropertyValue();
                    LgsProductExtendedPropertyValueService lgsProductExtendedPropertyValueService = new LgsProductExtendedPropertyValueService();

                    existingProduct = lgsProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());

                    if (existingProduct != null && dgvExtendedProperties["ExtendedPropertyName", dgvExtendedProperties.CurrentCell.RowIndex].Value != null)
                    {
                        existinglgsProductExtendedPropertyValue = lgsProductExtendedPropertyValueService.GetProductExtendedPropertyValueByProductID(existingProduct.LgsProductMasterID);
                    }
                    if (existinglgsProductExtendedPropertyValue != null)
                    {
                        var existinginvProductExtendedPropertyValueToDelete = lgsProductExtendedPropertyValueGrid.First(u => u.ExtendedPropertyName == dgvExtendedProperties["ExtendedPropertyName", dgvExtendedProperties.CurrentCell.RowIndex].Value.ToString().Trim());
                        lgsProductExtendedPropertyValueGrid.Remove(existinginvProductExtendedPropertyValueToDelete);
                        dgvExtendedProperties.DataSource = null;
                        dgvExtendedProperties.DataSource = lgsProductExtendedPropertyValueGrid;
                    }
                    else
                    {
                        var existinginvProductExtendedPropertyValueToDelete = lgsProductExtendedPropertyValueGrid.First(u => u.ExtendedPropertyName == dgvExtendedProperties["ExtendedPropertyName", dgvExtendedProperties.CurrentCell.RowIndex].Value.ToString().Trim());
                        lgsProductExtendedPropertyValueGrid.Remove(existinginvProductExtendedPropertyValueToDelete);
                        dgvExtendedProperties.DataSource = null;
                        dgvExtendedProperties.DataSource = lgsProductExtendedPropertyValueGrid;
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

                    LgsProductLinkService lgsProductLinkService = new LgsProductLinkService();
                    LgsProductLink existingLgsProductLink = new LgsProductLink();
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    existingProduct = lgsProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());

                    if (existingProduct != null && dgvPluLink["tbpPluLinkCode", dgvPluLink.CurrentCell.RowIndex].Value != null)
                        existingLgsProductLink = lgsProductLinkService.GetProductLinkByProductCode(existingProduct.LgsProductMasterID, (dgvPluLink["tbpPluLinkCode", dgvPluLink.CurrentCell.RowIndex].Value.ToString().Trim()));

                    //&& dgvUnitConversion["tbpUnitConversionUnit", dgvUnitConversion.CurrentCell.RowIndex].Value != null

                    if (existingLgsProductLink!=null)
                    {
                        var existingLgsProductLinkToDelete = lgsProductLinkGrid.First(p => p.ProductID == existingProduct.LgsProductMasterID && p.ProductLinkCode == (dgvPluLink["tbpPluLinkCode", dgvPluLink.CurrentCell.RowIndex].Value.ToString().Trim()));

                        lgsProductLinkGrid.Remove(existingLgsProductLinkToDelete);
                        dgvPluLink.DataSource = null;
                        dgvPluLink.DataSource = lgsProductLinkGrid;
                    }
                    else
                    {
                        var existingLgsProductLinkToDelete = lgsProductLinkGrid.First(p => p.ProductLinkCode == (dgvPluLink["tbpPluLinkCode", dgvPluLink.CurrentCell.RowIndex].Value.ToString().Trim()));

                        lgsProductLinkGrid.Remove(existingLgsProductLinkToDelete);
                        dgvPluLink.DataSource = null;
                        dgvPluLink.DataSource = lgsProductLinkGrid;
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
            LgsProductMaster lgsProductMaster = new LgsProductMaster();
            LgsProductExtendedPropertyValue lgsProductExtendedPropertyValue = new LgsProductExtendedPropertyValue();
            InvProductExtendedProperty invProductExtendedProperty = new InvProductExtendedProperty();
            InvProductExtendedValue invProductExtendedValue = new InvProductExtendedValue();
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
            InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
            InvProductExtendedValueService invProductExtendedValueService = new InvProductExtendedValueService();
            LgsProductExtendedPropertyValueService lgsProductExtendedPropertyValueService = new LgsProductExtendedPropertyValueService();

            lgsProductMaster = lgsProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

            if (lgsProductMaster != null)
            {
                invProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByName(txtPropertyName.Text.Trim());
                invProductExtendedValue = invProductExtendedValueService.GetInvProductExtendedValueByValueData(txtPropertyValue.Text.Trim());

                lgsProductExtendedPropertyValue.ProductID = lgsProductMaster.LgsProductMasterID;
                lgsProductExtendedPropertyValue.LgsProductExtendedPropertyID = invProductExtendedProperty.InvProductExtendedPropertyID;
                lgsProductExtendedPropertyValue.ExtendedPropertyName = invProductExtendedProperty.ExtendedPropertyName;
                lgsProductExtendedPropertyValue.LgsProductExtendedValueID = invProductExtendedValue.InvProductExtendedValueID;
                lgsProductExtendedPropertyValue.ValueData = invProductExtendedValue.ValueData;

                lgsProductExtendedPropertyValueGrid = lgsProductExtendedPropertyValueService.GetLgsProductExtendedPropertyValueTempList(lgsProductExtendedPropertyValueGrid, lgsProductExtendedPropertyValue);
                UpdatedgvExtendedProperties();
            }
            else
            {
                invProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByName(txtPropertyName.Text.Trim());
                invProductExtendedValue = invProductExtendedValueService.GetInvProductExtendedValueByValueData(txtPropertyValue.Text.Trim());

                lgsProductExtendedPropertyValue.ProductID = 0;
                lgsProductExtendedPropertyValue.LgsProductExtendedPropertyID = invProductExtendedProperty.InvProductExtendedPropertyID;
                lgsProductExtendedPropertyValue.ExtendedPropertyName = invProductExtendedProperty.ExtendedPropertyName;
                lgsProductExtendedPropertyValue.LgsProductExtendedValueID = invProductExtendedValue.InvProductExtendedValueID;
                lgsProductExtendedPropertyValue.ValueData = invProductExtendedValue.ValueData;

                lgsProductExtendedPropertyValueGrid = lgsProductExtendedPropertyValueService.GetLgsProductExtendedPropertyValueTempList(lgsProductExtendedPropertyValueGrid, lgsProductExtendedPropertyValue);
                UpdatedgvExtendedProperties();
            }
        }

        private void UpdatedgvExtendedProperties()
        {
            dgvExtendedProperties.DataSource = null;
            dgvExtendedProperties.DataSource = lgsProductExtendedPropertyValueGrid.ToList(); //OrderBy(pr => pr.).ToList();
            dgvExtendedProperties.FirstDisplayedScrollingRowIndex = dgvExtendedProperties.RowCount - 1;
            Common.ClearTextBox(txtPropertyName, txtPropertyValue);
            txtPropertyName.Focus();
        }

        private void txtProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    DataView dvAllReferenceData = new DataView(lgsProductMasterService.GetProductsDataTable());
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
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    DataView dvAllReferenceData = new DataView(lgsProductMasterService.GetProductsDataTable());
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
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    DataView dvAllReferenceData = new DataView(lgsProductMasterService.GetProductsDataTable());
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
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    DataView dvAllReferenceData = new DataView(lgsProductMasterService.GetProductsDataTable());
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
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    DataView dvAllReferenceData = new DataView(lgsProductMasterService.GetProductsDataTable());
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
            margin = (value / costPrice) * 100;

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
            if (e.KeyCode == Keys.Enter)
            {
                cmbPackSizeUnit.Focus();
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
            LgsProductMasterService LgsProductMasterService = new LgsProductMasterService();
            existingProduct = LgsProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

            if (existingProduct != null)
            {
                LgsProductUnitConversion lgsProductUnitConversion = new LgsProductUnitConversion();

                lgsProductUnitConversion.Description = txtUnitDescription.Text.Trim();
                lgsProductUnitConversion.ProductID = existingProduct.LgsProductMasterID;
                lgsProductUnitConversion.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnitTbpUnit.SelectedValue.ToString());
                lgsProductUnitConversion.UnitOfMeasureName = cmbUnitTbpUnit.Text.Trim();
                lgsProductUnitConversion.ConvertFactor = Common.ConvertStringToDecimal(txtUnitConvertFactor.Text.Trim());
                lgsProductUnitConversion.CostPrice = Common.ConvertStringToDecimalCurrency(txtUnitCostPrice.Text.Trim());
                lgsProductUnitConversion.SellingPrice = Common.ConvertStringToDecimalCurrency(txtUnitSellingPrice.Text.Trim());
                lgsProductUnitConversion.MinimumPrice = Common.ConvertStringToDecimalCurrency(txtUnitMinimumPrice.Text.Trim());
                //invProductUnitConversionsGrid

                lgsProductUnitConversionsGrid = LgsProductMasterService.GetUpdateProductUnitConversionTemp(lgsProductUnitConversionsGrid, lgsProductUnitConversion, existingProduct);
                dgvUnitConversion.DataSource = lgsProductUnitConversionsGrid;
                dgvUnitConversion.Refresh();
            }
            else
            {
                LgsProductUnitConversion lgsProductUnitConversion = new LgsProductUnitConversion();

                lgsProductUnitConversion.Description = txtUnitDescription.Text.Trim();
                lgsProductUnitConversion.ProductID = 0;
                lgsProductUnitConversion.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnitTbpUnit.SelectedValue.ToString());
                lgsProductUnitConversion.UnitOfMeasureName = cmbUnitTbpUnit.Text.Trim();
                lgsProductUnitConversion.ConvertFactor = Common.ConvertStringToDecimal(txtUnitConvertFactor.Text.Trim());
                lgsProductUnitConversion.CostPrice = Common.ConvertStringToDecimalCurrency(txtUnitCostPrice.Text.Trim());
                lgsProductUnitConversion.SellingPrice = Common.ConvertStringToDecimalCurrency(txtUnitSellingPrice.Text.Trim());
                lgsProductUnitConversion.MinimumPrice = Common.ConvertStringToDecimalCurrency(txtUnitMinimumPrice.Text.Trim());
                //invProductUnitConversionsGrid

                lgsProductUnitConversionsGrid = LgsProductMasterService.GetUpdateProductUnitConversionTemp(lgsProductUnitConversionsGrid, lgsProductUnitConversion, existingProduct);
                dgvUnitConversion.DataSource = lgsProductUnitConversionsGrid;
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

                LgsSupplierService supplierService = new LgsSupplierService();
                LgsSupplier supplier = new LgsSupplier();

                supplier = supplierService.GetLgsSupplierByCode(txtSupplierLinkSupplierCode.Text.Trim());

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

                Common.SetAutoComplete(txtMainSupplierDescription, supplierService.GetAllLgsSupplierNames(), chkAutoCompleationMainSupplier.Checked);
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

                LgsSupplierService supplierService = new LgsSupplierService();
                LgsSupplier supplier = new LgsSupplier();

                supplier = supplierService.GetLgsSupplierByName(txtSupplierLinkSupplierName.Text.Trim());

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

                Common.SetAutoComplete(txtMainSupplierDescription, supplierService.GetAllLgsSupplierNames(), chkAutoCompleationMainSupplier.Checked);
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
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
            existingProduct = lgsProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

            if (existingProduct != null)
            {
                LgsProductSupplierLink lgsProductSupplierLink = new LgsProductSupplierLink();

                LgsSupplierService supplierService = new LgsSupplierService();
                LgsSupplier supplier = new LgsSupplier();

                supplier = supplierService.GetLgsSupplierByCode(txtSupplierLinkSupplierCode.Text.Trim());

                lgsProductSupplierLink.ProductID = existingProduct.LgsProductMasterID;
                lgsProductSupplierLink.SupplierID = supplier.LgsSupplierID;
                lgsProductSupplierLink.SupplierCode = supplier.SupplierCode;
                lgsProductSupplierLink.SupplierName = supplier.SupplierName;
                lgsProductSupplierLink.CostPrice = Common.ConvertStringToDecimalCurrency(txtSupplierLinkCostPrice.Text.Trim());
                lgsProductSupplierLink.FixedGP = Common.ConvertStringToDecimalCurrency(txtSupplierLinkFixedGP.Text.Trim());
                //invProductUnitConversionsGrid

                lgsProductSupplierLinkGrid = lgsProductMasterService.GetUpdateProductSupplierLinkTemp(lgsProductSupplierLinkGrid, lgsProductSupplierLink, existingProduct);
                dgvMultiSupplier.DataSource = lgsProductSupplierLinkGrid;
                dgvMultiSupplier.Refresh();
            }
            else
            {
                LgsProductSupplierLink lgsProductSupplierLink = new LgsProductSupplierLink();

                LgsSupplierService supplierService = new LgsSupplierService();
                LgsSupplier supplier = new LgsSupplier();

                supplier = supplierService.GetLgsSupplierByCode(txtSupplierLinkSupplierCode.Text.Trim());

                lgsProductSupplierLink.ProductID = 0;
                lgsProductSupplierLink.SupplierID = supplier.LgsSupplierID;
                lgsProductSupplierLink.SupplierCode = supplier.SupplierCode;
                lgsProductSupplierLink.SupplierName = supplier.SupplierName;
                lgsProductSupplierLink.CostPrice = Common.ConvertStringToDecimalCurrency(txtSupplierLinkCostPrice.Text.Trim());
                lgsProductSupplierLink.FixedGP = Common.ConvertStringToDecimalCurrency(txtSupplierLinkFixedGP.Text.Trim());

                //invProductUnitConversionsGrid

                lgsProductSupplierLinkGrid = lgsProductMasterService.GetUpdateProductSupplierLinkTemp(lgsProductSupplierLinkGrid, lgsProductSupplierLink, existingProduct);
                dgvMultiSupplier.DataSource = lgsProductSupplierLinkGrid;
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
                    LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                    LgsDepartment existingLgsDepartment = new LgsDepartment();
                    existingLgsDepartment = lgsDepartmentService.GetLgsDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDependCategory);

                    if (existingLgsDepartment != null)
                    {
                        txtDepartmentCode.Text = existingLgsDepartment.DepartmentCode;
                        txtDepartmentDescription.Text = existingLgsDepartment.DepartmentName;
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
                LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                LgsDepartment existingLgsDepartment = new LgsDepartment();
                existingLgsDepartment = lgsDepartmentService.GetLgsDepartmentsByName(txtDepartmentDescription.Text.Trim(), isDependCategory);

                if (existingLgsDepartment != null)
                {
                    txtDepartmentCode.Text = existingLgsDepartment.DepartmentCode;
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
            if (txtCategoryCode.Text.Trim() != string.Empty)
            {
                LgsCategoryService lgsCategoryService = new LgsCategoryService();
                LgsCategory existingLgsCategory = new LgsCategory();

                existingLgsCategory = lgsCategoryService.GetLgsCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory);

                if (existingLgsCategory != null)
                {
                    if (!isDependCategory)
                    {
                        txtCategoryDescription.Text = existingLgsCategory.CategoryName;
                        txtSubCategoryCode.Focus();
                    }
                    else
                    {
                        LgsDepartmentService lgsDepartmentService = new Service.LgsDepartmentService();

                        if (lgsDepartmentService.GetLgsDepartmentsByID(existingLgsCategory.LgsDepartmentID, isDependCategory).DepartmentCode.Equals(txtDepartmentCode.Text.Trim()))
                        {
                            txtCategoryDescription.Text = existingLgsCategory.CategoryName;
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
            if (txtCategoryDescription.Text.Trim() != string.Empty)
            {
                LgsCategoryService lgsCategoryService = new LgsCategoryService();
                LgsCategory existingLgsCategory = new LgsCategory();

                existingLgsCategory = lgsCategoryService.GetLgsCategoryByName(txtCategoryDescription.Text.Trim(), isDependSubCategory);


                if (existingLgsCategory != null)
                {
                    if (!isDependCategory)
                    {
                        txtCategoryCode.Text = existingLgsCategory.CategoryCode;
                        txtSubCategoryCode.Focus();
                    }
                    else
                    {
                        LgsDepartmentService lgsDepartmentService = new Service.LgsDepartmentService();

                        if (lgsDepartmentService.GetLgsDepartmentsByID(existingLgsCategory.LgsDepartmentID, isDependCategory).DepartmentCode.Equals(txtDepartmentCode.Text.Trim()))
                        {
                            txtCategoryCode.Text = existingLgsCategory.CategoryCode;
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
                    LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
                    LgsSubCategory existingLgsSubCategory = new LgsSubCategory();

                    existingLgsSubCategory = lgsSubCategoryService.GetLgsSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2);

                    if (existingLgsSubCategory != null)
                    {
                        if (!isDependCategory)
                        {
                            txtSubCategoryDescription.Text = existingLgsSubCategory.SubCategoryName;
                            txtSubCategory2Code.Focus();
                        }
                        else
                        {
                            LgsCategoryService invCategoryService = new Service.LgsCategoryService();

                            if (invCategoryService.GetLgsCategoryByID(existingLgsSubCategory.LgsCategoryID, isDependSubCategory2).CategoryCode.Equals(txtCategoryCode.Text.Trim()))
                            {
                                txtSubCategoryDescription.Text = existingLgsSubCategory.SubCategoryName;
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
                LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
                LgsSubCategory existingLgsSubCategory = new LgsSubCategory();

                existingLgsSubCategory = lgsSubCategoryService.GetLgsSubCategoryByName(txtSubCategoryDescription.Text.Trim(), isDependSubCategory2);

                if (existingLgsSubCategory != null)
                {
                    if (!isDependCategory)
                    {
                        txtSubCategoryCode.Text = existingLgsSubCategory.SubCategoryCode;
                        txtSubCategory2Code.Focus();
                    }
                    else
                    {
                        LgsCategoryService lgsCategoryService = new Service.LgsCategoryService();

                        if (lgsCategoryService.GetLgsCategoryByID(existingLgsSubCategory.LgsCategoryID, isDependSubCategory2).CategoryCode.Equals(txtCategoryCode.Text.Trim()))
                        {
                            txtSubCategoryCode.Text = existingLgsSubCategory.SubCategoryCode;
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
                    LgsSubCategory2Service lgsSubCategory2Service = new LgsSubCategory2Service();
                    LgsSubCategory2 existingLgsSubCategory2 = new LgsSubCategory2();

                    existingLgsSubCategory2 = lgsSubCategory2Service.GetLgsSubCategory2ByCode(txtSubCategory2Code.Text.Trim());

                    if (existingLgsSubCategory2 != null)
                    {
                        if (!isDependSubCategory2)
                        {
                            txtSubCategory2Description.Text = existingLgsSubCategory2.SubCategory2Name;
                        }
                        else
                        {
                            LgsSubCategoryService LgsSubCategoryService = new Service.LgsSubCategoryService();

                            if (LgsSubCategoryService.GetLgsSubCategoryByID(existingLgsSubCategory2.LgsSubCategoryID, isDependSubCategory2).SubCategoryCode.Equals(txtSubCategoryCode.Text.Trim()))
                            {
                                txtSubCategory2Description.Text = existingLgsSubCategory2.SubCategory2Name;
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
            if (txtSubCategory2Description.Text.Trim() != string.Empty)
            {
                LgsSubCategory2Service lgsSubCategory2Service = new LgsSubCategory2Service();
                LgsSubCategory2 existingLgsSubCategory2 = new LgsSubCategory2();

                existingLgsSubCategory2 = lgsSubCategory2Service.GetLgsSubCategory2ByName(txtSubCategory2Description.Text.Trim());


                if (existingLgsSubCategory2 != null)
                {
                    if (!isDependSubCategory2)
                    {
                        txtSubCategory2Code.Text = existingLgsSubCategory2.SubCategory2Code;
                        txtMainSupplierCode.Focus();
                    }
                    else
                    {
                        LgsSubCategoryService LgsSubCategoryService = new Service.LgsSubCategoryService();

                        if (LgsSubCategoryService.GetLgsSubCategoryByID(existingLgsSubCategory2.LgsSubCategoryID, isDependSubCategory2).SubCategoryCode.Equals(txtSubCategoryCode.Text.Trim()))
                        {
                            txtSubCategory2Code.Text = existingLgsSubCategory2.SubCategory2Code;
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
            LgsProductMasterService LgsProductMasterService = new LgsProductMasterService();
            existingProduct = LgsProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

            if (existingProduct != null)
            {
                LgsProductLink lgsProductLink = new LgsProductLink();

                lgsProductLink.ProductID = existingProduct.LgsProductMasterID;
                lgsProductLink.ProductLinkCode = txtPluCode.Text.Trim();
                lgsProductLink.ProductLinkName = txtPluName.Text.Trim();

                lgsProductLinkGrid = LgsProductMasterService.GetUpdateProductLinkTemp(lgsProductLinkGrid, lgsProductLink, existingProduct);
                dgvPluLink.DataSource = lgsProductLinkGrid;
                dgvPluLink.Refresh();
            }
            else
            {
                LgsProductLink lgsProductLink = new LgsProductLink();

                lgsProductLink.ProductID = 0;
                lgsProductLink.ProductLinkCode = txtPluCode.Text.Trim();
                lgsProductLink.ProductLinkName = txtPluName.Text.Trim();

                lgsProductLinkGrid = LgsProductMasterService.GetUpdateProductLinkTemp(lgsProductLinkGrid, lgsProductLink, existingProduct);
                dgvPluLink.DataSource = lgsProductLinkGrid;
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
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    LgsProductMaster existingProduct;

                    existingProduct = lgsProductMasterService.GetProductsByRefCodes(txtPluCode.Text.Trim());

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
                LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                LgsProductMaster existingProduct;
                existingProduct = lgsProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());

                if (Toast.Show("Product - " + existingProduct.ProductCode + " - " + existingProduct.ProductName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                {
                    return;
                }

                existingProduct = lgsProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());

                if (existingProduct != null && existingProduct.LgsProductMasterID != 0)
                {
                    lgsProductMasterService.DeleteProduct(existingProduct);

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

        private void txtProductName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtProductName.Text.Trim() != string.Empty)
                {
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    LgsProductMaster existingProduct = new LgsProductMaster();

                    existingProduct = lgsProductMasterService.GetProductsByName(txtProductName.Text.Trim());

                    if (existingProduct != null)
                    {
                        LoadProductMaster(existingProduct);
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

                    txtBarCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                long departmentID = 0, CategoryID = 0, SubCategoryID = 0, subCategory2ID = 0, SupplierID = 0;
                decimal costPrice = 0, sellingPrice = 0;

                if (!string.IsNullOrEmpty(txtDepartmentCode.Text.Trim()))
                {
                    LgsDepartment lgsDepartment = new LgsDepartment();
                    LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                    lgsDepartment = lgsDepartmentService.GetLgsDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDependCategory);
                    departmentID = lgsDepartment.LgsDepartmentID;
                }
                if (!string.IsNullOrEmpty(txtCategoryCode.Text.Trim()))
                {
                    LgsCategoryService lgsCategoryService = new LgsCategoryService();
                    LgsCategory invCategory = new LgsCategory();
                    invCategory = lgsCategoryService.GetLgsCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory);
                    CategoryID = invCategory.LgsCategoryID;
                }
                if (!string.IsNullOrEmpty(txtSubCategoryCode.Text.Trim()))
                {
                    LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
                    LgsSubCategory lgsSubCategory = new LgsSubCategory();
                    lgsSubCategory = lgsSubCategoryService.GetLgsSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2);
                    SubCategoryID = lgsSubCategory.LgsSubCategoryID;
                }
                if (!string.IsNullOrEmpty(txtSubCategory2Code.Text.Trim()))
                {
                    LgsSubCategory2Service lgsSubCategory2Service = new LgsSubCategory2Service();
                    LgsSubCategory2 lgsSubCategory2 = new LgsSubCategory2();
                    lgsSubCategory2 = lgsSubCategory2Service.GetLgsSubCategory2ByCode(txtSubCategory2Code.Text.Trim());
                    subCategory2ID = lgsSubCategory2.LgsSubCategory2ID;
                }
                if (!string.IsNullOrEmpty(txtMainSupplierCode.Text.Trim()))
                {
                    LgsSupplierService supplierService = new LgsSupplierService();
                    LgsSupplier supplier = new LgsSupplier();
                    supplier = supplierService.GetSupplierByCode(txtMainSupplierCode.Text.Trim());
                    SupplierID = supplier.LgsSupplierID;
                }
                if (!string.IsNullOrEmpty(txtCostPrice.Text.Trim()))
                {
                    costPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtSellingPrice.Text.Trim()))
                {
                    sellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text.Trim());
                }

                LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                DataView dvAllReferenceData = new DataView(lgsProductMasterService.GetProductsSearchAccordingToCombination(departmentID, CategoryID, SubCategoryID, subCategory2ID, SupplierID, costPrice, sellingPrice));
                if (dvAllReferenceData.Count != 0)
                {
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), "", txtProductCode);
                    txtProductCode_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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


    }
}
