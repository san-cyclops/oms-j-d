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

namespace UI.Windows
{
    public partial class FrmPromotionMaster : UI.Windows.FrmBaseMasterForm
    {

        /// <summary>
        /// Promotion Master
        /// Design By - C.S.Malluwawadu
        /// Developed By - C.S.Malluwawadu
        /// Date - 07/08/2013
        /// </summary>
        /// 

        ErrorMessage errorMessage = new ErrorMessage();

        private InvPromotionMaster existingPromotion;
        private InvDepartment existingInvDeparment;
        private InvCategory existingInvCategory;
        private InvSubCategory existingInvSubCategory;
        private InvProductMaster existingInvProductMaster;

        private InvPromotionMaster existingPromotionMaster;
        private InvPromotionDetailsBuyXProduct existingPromotionDetailBuyXProduct;
        private InvPromotionDetailsBuyXDepartment existingPromotionDetailBuyXDepartment;
        private InvPromotionDetailsBuyXCategory existingPromotionDetailBuyXCategory;
        private InvPromotionDetailsBuyXSubCategory existingPromotionDetailBuyXSubCategory;


        List<InvPromotionDetailsBuyXProduct> invPromotionBuyXDetailTempList = new List<InvPromotionDetailsBuyXProduct>();
        InvPromotionDetailsBuyXProduct existingBuyXDetailTemp = new InvPromotionDetailsBuyXProduct();

        List<InvPromotionDetailsGetYDetails> invPromotionGetYDetailTempList = new List<InvPromotionDetailsGetYDetails>();
        InvPromotionDetailsGetYDetails existingGetYDetailTemp = new InvPromotionDetailsGetYDetails();

        List<InvPromotionDetailsProductDis> invPromotionProductDiscountDetailTempList = new List<InvPromotionDetailsProductDis>();
        InvPromotionDetailsProductDis existingProductDiscountDetailTemp = new InvPromotionDetailsProductDis();

        List<InvPromotionDetailsDepartmentDis> invPromotionDepartmentDetailTempList = new List<InvPromotionDetailsDepartmentDis>();
        InvPromotionDetailsDepartmentDis existingDepartmentDetailTemp = new InvPromotionDetailsDepartmentDis();

        List<InvPromotionDetailsCategoryDis> invPromotionCategoryDetailTempList = new List<InvPromotionDetailsCategoryDis>();
        InvPromotionDetailsCategoryDis existingCategoryDetailTemp = new InvPromotionDetailsCategoryDis();

        List<InvPromotionDetailsSubCategoryDis> invPromotionSubCategoryDetailTempList = new List<InvPromotionDetailsSubCategoryDis>();
        InvPromotionDetailsSubCategoryDis existingSubCategoryDetailTemp = new InvPromotionDetailsSubCategoryDis();

        List<InvPromotionDetailsSubCategory2Dis> invPromotionSubCategory2DetailTempList = new List<InvPromotionDetailsSubCategory2Dis>();
        InvPromotionDetailsSubCategory2Dis existingSubCategory2DetailTemp = new InvPromotionDetailsSubCategory2Dis();


        List<InvPromotionDetailsBuyXDepartment> invPromotionBuyXDepartmentDetailTempList = new List<InvPromotionDetailsBuyXDepartment>();
        InvPromotionDetailsBuyXDepartment existingBuyXDepartmentDetailTemp = new InvPromotionDetailsBuyXDepartment();

        List<InvPromotionDetailsBuyXCategory> invPromotionBuyXCategoryDetailTempList = new List<InvPromotionDetailsBuyXCategory>();
        InvPromotionDetailsBuyXCategory existingBuyXCategoryDetailTemp = new InvPromotionDetailsBuyXCategory();

        List<InvPromotionDetailsBuyXSubCategory> invPromotionBuyXSubCategoryDetailsTempDetailsList = new List<InvPromotionDetailsBuyXSubCategory>();
        InvPromotionDetailsBuyXSubCategory existingBuyXSubCategoryDetailTemp = new InvPromotionDetailsBuyXSubCategory();

        List<InvPromotionDetailsBuyXSubCategory2> invPromotionBuyXSubCategory2DetailsTempDetailsList = new List<InvPromotionDetailsBuyXSubCategory2>();
        InvPromotionDetailsBuyXSubCategory2 existingBuyXSubCategory2DetailTemp = new InvPromotionDetailsBuyXSubCategory2();

       
        List<InvPromotionDetailsAllowLocations> invPromotionAllowLocationList = new List<InvPromotionDetailsAllowLocations>();
        InvPromotionDetailsAllowLocations existingAllowLocationTemp = new InvPromotionDetailsAllowLocations();

        List<InvPromotionDetailsAllowTypes> invPromotionAllowTypeList = new List<InvPromotionDetailsAllowTypes>();
        InvPromotionDetailsAllowTypes existingAllowTypeTemp = new InvPromotionDetailsAllowTypes();


        bool isDepend = false, isDependCategory = false, isDependSubCategory = false, isDependSubCategory2 = false;
        bool isValidControls = true;
        bool isCheckedAllLocations = true;
        bool isCheckedAllTypes = true;
        int documentID;

        public FrmPromotionMaster()
        {
            InitializeComponent();
        }

        private void FrmPromotionMaster_Load(object sender, EventArgs e)
        {

        }

        public override void FormLoad()
        {
            this.CausesValidation = false;

            isDependCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend;
            isDependSubCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend;
            isDependSubCategory2 = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").IsDepend;

            InvProductMasterService invProductMasterService = new InvProductMasterService();
            Common.SetAutoComplete(txtProductProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationPdisProduct.Checked);
            Common.SetAutoComplete(txtProductProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationPdisProduct.Checked);

            Common.SetAutoComplete(txtBuyProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationBuyProduct.Checked);
            Common.SetAutoComplete(txtBuyProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationBuyProduct.Checked);

            Common.SetAutoComplete(txtGetProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationGetProduct.Checked);
            Common.SetAutoComplete(txtGetProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationGetProduct.Checked);


            InvDepartmentService invDepartmentService = new Service.InvDepartmentService();
            Common.SetAutoComplete(txtDepartmentCode, invDepartmentService.GetInvDepartmentCodes(isDependCategory), chkAutoCompleationDepartment.Checked);
            Common.SetAutoComplete(txtDepartmentDescription, invDepartmentService.GetInvDepartmentNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend), chkAutoCompleationDepartment.Checked);
            Common.SetAutoComplete(txtBuyDepartmentCode, invDepartmentService.GetInvDepartmentCodes(isDependCategory), chkAutoCompleationDepartment.Checked);
            Common.SetAutoComplete(txtBuyDepartmentName, invDepartmentService.GetInvDepartmentNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend), chkAutoCompleationBuyDepartment.Checked);

            InvCategoryService invCategoryService = new Service.InvCategoryService();
            Common.SetAutoComplete(txtCategoryCode, invCategoryService.GetInvCategoryCodes(isDependSubCategory), chkAutoCompleationCategory.Checked);
            Common.SetAutoComplete(txtCategoryDescription, invCategoryService.GetInvCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend), chkAutoCompleationCategory.Checked);
            Common.SetAutoComplete(txtBuyCategoryCode, invCategoryService.GetInvCategoryCodes(isDependSubCategory), chkAutoCompleationBuyCaytegory.Checked);
            Common.SetAutoComplete(txtBuyCategoryName, invCategoryService.GetInvCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend), chkAutoCompleationBuyCaytegory.Checked);

            InvSubCategoryService invSubCategoryService = new Service.InvSubCategoryService();
            Common.SetAutoComplete(txtSubCategoryCode, invSubCategoryService.GetInvSubCategoryCodes(isDependSubCategory2), chkAutoCompleationSubCategory.Checked);
            Common.SetAutoComplete(txtSubCategoryDescription, invSubCategoryService.GetInvSubCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").IsDepend), chkAutoCompleationSubCategory.Checked);
            Common.SetAutoComplete(txtBuySubCategoryCode, invSubCategoryService.GetInvSubCategoryCodes(isDependSubCategory2), chkAutoCompleationBuySubCategory.Checked);
            Common.SetAutoComplete(txtBuySubCategoryName, invSubCategoryService.GetInvSubCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").IsDepend), chkAutoCompleationBuySubCategory.Checked);

            InvSubCategory2Service invSubCategory2Service = new Service.InvSubCategory2Service();
            Common.SetAutoComplete(txtBuySubCategory2Code, invSubCategory2Service.GetInvSubCategory2Codes(), chkAutoCompleationBuySubCategory2.Checked);
            Common.SetAutoComplete(txtBuySubCategory2Name, invSubCategory2Service.GetInvSubCategory2Names(), chkAutoCompleationBuySubCategory2.Checked);
            Common.SetAutoComplete(txtSubCategory2Code, invSubCategory2Service.GetInvSubCategory2Codes(), ChkSubCategory2Dis.Checked);
            Common.SetAutoComplete(txtSubCategory2Description, invSubCategory2Service.GetInvSubCategory2Names(), ChkSubCategory2Dis.Checked);

           // Load Unit of measures            
            UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
            Common.LoadUnitOfMeasures(cmbBuyUnit, unitOfMeasureService.GetAllUnitOfMeasures());
            Common.LoadUnitOfMeasures(cmbGetUnit, unitOfMeasureService.GetAllUnitOfMeasures());
            Common.LoadUnitOfMeasures(cmbProductUnit, unitOfMeasureService.GetAllUnitOfMeasures());

            // Load Banks
            BankService bankService = new BankService();
            Common.LoadAllBanks(cmbProvider, bankService.GetAllBanks());

            // Load Promotion Types
            InvPromotionMasterService promotionTypes = new InvPromotionMasterService();
            Common.LoadAllPromotionTypes(cmbPromotionType, promotionTypes.GetAllPromotionTypes());

            // Load Locations
            dgvLocationInfo.AutoGenerateColumns = false;
            LoadAllLocations();

            //Load Customer Groups
            dgvCustomerGroup.AutoGenerateColumns = false;
            LoadCustomerGroups();

            LoadAllPaymentCardTypes();

            ResetTimePeriod();

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

            dgvBuyDetails.AutoGenerateColumns = false;
            dgvGetDetails.AutoGenerateColumns = false;



            base.FormLoad();
            txtPromotionCode.Focus();
        }

        public override void ClearForm()
        {

            base.ClearForm();

            dgvBuyDetails.DataSource = null;
            dgvBuyDetails.Refresh();
            dgvGetDetails.DataSource = null;
            dgvGetDetails.Refresh();
            dgvBuyDepartment.DataSource = null;
            dgvBuyDepartment.Refresh();
            dgvBuyCategory.DataSource = null;
            dgvBuyCategory.Refresh();
            dgvBuySubCategory.DataSource = null;
            dgvBuySubCategory.Refresh();
            dgvBuySubCategory2.DataSource = null;
            dgvBuySubCategory2.Refresh();
            dgvSubCategory2.DataSource = null;
            dgvSubCategory2.Refresh();

            dgvProductDiscount.DataSource = null;
            dgvProductDiscount.Refresh();
            dgvDepartmentDiscount.DataSource = null;
            dgvDepartmentDiscount.Refresh();
            dgvCategoryDiscount.DataSource = null;
            dgvCategoryDiscount.Refresh();
            dgvSubCategory.DataSource = null;
            dgvSubCategory.Refresh();


            txtSubTotalDiscountPercentage.Text = "";
            txtSubTotalDiscountValue.Text = "";
            txtPoints.Text = "";

            ChkCombined.Checked = false;
            ChkGiftVoucher.Checked = false;
                                  
            
        }

        private void ResetTimePeriod()
        {
            dtpMondayFrom.Text = "12:00:00 AM";
            dtpTuesdayFrom.Text = "12:00:00 AM";
            dtpWednesdayFrom.Text = "12:00:00 AM";
            dtpThuresdayFrom.Text = "12:00:00 AM";
            dtpFridayFrom.Text = "12:00:00 AM";
            dtpSaturdayFrom.Text = "12:00:00 AM";
            dtpSundayFrom.Text = "12:00:00 AM";

            dtpMondayTo.Text = "11:59:59 PM";
            dtpTuesdayTo.Text = "11:59:59 PM";
            dtpWednesdayTo.Text = "11:59:59 PM";
            dtpThuresdayTo.Text = "11:59:59 PM";
            dtpFridayTo.Text = "11:59:59 PM";
            dtpSaturdayTo.Text = "11:59:59 PM";
            dtpSundayTo.Text = "11:59:59 PM";
        }

        private void LoadAllLocations()
        {
            LocationService locationService = new LocationService();
            List<Location> locations = new List<Location>();

            locations = locationService.GetAllLocations();
            dgvLocationInfo.DataSource = locations;
            dgvLocationInfo.Refresh();

        }

        private void LoadCustomerGroups()
        {
            CustomerGroupService customerGroupService = new CustomerGroupService();
            List<CustomerGroup> customerGroups = new List<CustomerGroup>();

            customerGroups = customerGroupService.GetAllCustomerGroups();
            dgvCustomerGroup.DataSource = customerGroups;
            dgvCustomerGroup.Refresh();

        }

        //private void LoadPaymentCardTypes()
        //{
        //    PaymentCardMasterService paymentCardMasterService = new PaymentCardMasterService();
        //    List<InvPaymentCardType> invPaymentCardType = new List<InvPaymentCardType>();
        //    if (cmbProvider.SelectedIndex != -1) { invPaymentCardType = paymentCardMasterService.GetAllCardMastersByProvider(Common.ConvertStringToLong(cmbProvider.SelectedValue.ToString())); }
        //    dgvType.AutoGenerateColumns = false;
        //    dgvType.DataSource = null;
        //    dgvType.DataSource = invPaymentCardType;
        //    dgvType.Refresh();

        //}

        private void LoadAllPaymentCardTypes()
        {
            PaymentCardMasterService paymentCardMasterService = new PaymentCardMasterService();
            List<InvPaymentCardType> invPaymentCardType = new List<InvPaymentCardType>();
            invPaymentCardType = paymentCardMasterService.GetAllCards(); 
            dgvType.AutoGenerateColumns = false;
            dgvType.DataSource = null;
            dgvType.DataSource = invPaymentCardType;
            dgvType.Refresh();

        }

        private void CheckedAllLocations()
        {
            for (int i = 0; i < dgvLocationInfo.RowCount; i++)
             {
                 dgvLocationInfo.Rows[i].Cells["Selection"].Value = true;
             }

        }

        private void UnCheckedAllLocations()
        {
            for (int i = 0; i < dgvLocationInfo.RowCount; i++)
            {
                dgvLocationInfo.Rows[i].Cells["Selection"].Value = false;
            }

        }
        
        private void CheckCheckedStatus()
        {
            for (int i = 0; i < dgvLocationInfo.RowCount; i++)
            {
                if (Convert.ToBoolean(dgvLocationInfo.Rows[i].Cells["Selection"].Value) == false) { chkAllLocations.Checked = false; return; } else { chkAllLocations.Checked = true; }
            }

        }

        private void CheckedAllTypes()
        {
            for (int i = 0; i < dgvType.RowCount; i++)
            {
                dgvType.Rows[i].Cells["TypeSelection"].Value = true;
            }

        }

        private void UnCheckedAllTypes()
        {
            for (int i = 0; i < dgvType.RowCount; i++)
            {
                dgvType.Rows[i].Cells["TypeSelection"].Value = false;
            }

        }

        private void CheckCheckedStatusTypes()
        {
            for (int i = 0; i < dgvType.RowCount; i++)
            {
                if (Convert.ToBoolean(dgvType.Rows[i].Cells["TypeSelection"].Value) == false) { chkAllType.Checked = false; return; } else { chkAllType.Checked = true; }
            }

        }
        

        private void ChangePaymentMethod()
        {
            if (rdoAll.Checked || rdoCash.Checked) { cmbProvider.Enabled = false; lblProvider.Enabled = false; chkConsiderProvider.Checked = false; chkConsiderProvider.Enabled = false; lblType.Enabled = false; dgvType.Enabled = false; chkAllType.Checked = false; chkAllType.Enabled = false; }
            else if (rdoCard.Checked) { lblProvider.Enabled = true; chkConsiderProvider.Enabled = true; cmbProvider.SelectedIndex = -1; chkAllType.Enabled = true; lblType.Enabled = true; dgvType.Enabled = true; }

        }

        private void SelectValueRange()
        {
            if (chkConsiderValue.Checked) { txtMinValue.Enabled = true; txtMaxValue.Enabled = true; lblTo.Enabled = true; }
            else { txtMinValue.Text = string.Empty; txtMaxValue.Text = string.Empty; txtMinValue.Enabled = false; txtMaxValue.Enabled = false; lblTo.Enabled = false; }
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

        private void chkAutoCompleationPdisProduct_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                Common.SetAutoComplete(txtProductProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationPdisProduct.Checked);
                Common.SetAutoComplete(txtProductProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationPdisProduct.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationBuyProduct_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                Common.SetAutoComplete(txtBuyProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationBuyProduct.Checked);
                Common.SetAutoComplete(txtBuyProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationBuyProduct.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationGetProduct_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                Common.SetAutoComplete(txtGetProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationGetProduct.Checked);
                Common.SetAutoComplete(txtGetProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationGetProduct.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        /// <summary>
        /// Validate form controls
        /// </summary>
        /// <returns></returns>
        private bool ValidateControls()
        {
            return true; //Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDepartmentCode, txtDepartmentDescription);
        }

        /// <summary>
        /// Load Reference Search form on demand
        /// </summary>
        private void LoadReferenceSearchForm(DataView dvAllReferenceData, string parentOfSearch, string searchFormCaption, string searchText, Control focusControl)
        {
            try
            {
                FrmReferenceSearch referenceSearch = new FrmReferenceSearch();
                referenceSearch.DvResults = dvAllReferenceData;
                referenceSearch.ParentOfSearch = parentOfSearch.Trim();
                referenceSearch.FormCaption = searchFormCaption.Trim();
                referenceSearch.SearchText = searchText.Trim();
                referenceSearch.FocusControl = focusControl;

                if (referenceSearch.IsDisposed)
                { referenceSearch = new FrmReferenceSearch(); }

                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is FrmReferenceSearch)
                    {
                        FrmReferenceSearch masterSearch2 = (FrmReferenceSearch)frm;
                        if (string.Equals(masterSearch2.ParentOfSearch.Trim(), this.Name.Trim()))
                        { return; }
                    }
                }

                referenceSearch.Show();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentCode_Validated(object sender, EventArgs e)
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
                        txtDepartmentDescription.Text = existingInvDepartment.DepartmentName;
                        txtDepartmentQty.Focus();
                    }
                    else
                    {
                        Toast.Show("Department " + txtDepartmentCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
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

        private void txtDepartmentCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtDepartmentQty.Focus();
                txtDepartmentQty.SelectionStart = txtDepartmentQty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentDescription_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtDepartmentDescription.Text.Trim() != string.Empty)
                {
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    InvDepartment existingInvDepartment = new InvDepartment();
                    existingInvDepartment = invDepartmentService.GetInvDepartmentsByName(txtDepartmentDescription.Text.Trim(), isDependCategory);

                    if (existingInvDepartment != null)
                    {
                        txtDepartmentCode.Text = existingInvDepartment.DepartmentCode;
                        txtDepartmentQty.Focus();
                    }
                    else
                    {
                        Toast.Show("Department " + txtDepartmentDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtDepartmentDescription);
                        txtDepartmentDescription.Focus();
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
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtDepartmentQty.Focus();
                txtDepartmentQty.SelectionStart = txtDepartmentQty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryCode_Validated(object sender, EventArgs e)
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
                        txtCategoryDescription.Text = existingInvCategory.CategoryName;
                        txtSubCategoryCode.Focus();
                    }
                    else
                    {
                        Toast.Show("Category " + txtCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
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

        private void txtCategoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtCategoryQty.Focus();
                txtCategoryQty.SelectionStart = txtCategoryQty.Text.Length;
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
                        txtCategoryCode.Text = existingInvCategory.CategoryCode;
                        txtCategoryQty.Focus();

                    }
                    else
                    {
                        Toast.Show("Category " + txtCategoryDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
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

        private void txtCategoryDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtCategoryQty.Focus();
                txtCategoryQty.SelectionStart = txtCategoryQty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryCode_Validated(object sender, EventArgs e)
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
                        txtSubCategoryDescription.Text = existingInvSubCategory.SubCategoryName;
                        txtSubCategoryQty.Focus();

                    }
                    else
                    {
                        Toast.Show("SubCategory " + txtSubCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
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

        private void txtSubCategoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtSubCategoryQty.Focus();
                txtSubCategoryQty.SelectionStart = txtSubCategoryQty.Text.Length;
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
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtSubCategoryQty.Focus();
                txtSubCategoryQty.SelectionStart = txtSubCategoryQty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryDescription_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSubCategoryDescription.Text.Trim() != string.Empty)
                {
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    InvSubCategory existingInvSubCategory = new InvSubCategory();

                    existingInvSubCategory = invSubCategoryService.GetInvSubCategoryByName(txtSubCategoryDescription.Text.Trim(), isDependSubCategory2);


                    if (existingInvSubCategory != null)
                    {
                        txtSubCategoryCode.Text = existingInvSubCategory.SubCategoryCode;
                        txtSubCategoryQty.Focus();

                    }
                    else
                    {
                        Toast.Show("SubCategory " + txtSubCategoryDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtSubCategoryDescription);
                        txtSubCategoryDescription.Focus();
                    }
            }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationSubCategory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvSubCategoryService invCategoryService = new Service.InvSubCategoryService();
                Common.SetAutoComplete(txtSubCategoryCode, invCategoryService.GetInvSubCategoryCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").IsDepend), chkAutoCompleationSubCategory.Checked);
                Common.SetAutoComplete(txtSubCategoryDescription, invCategoryService.GetInvSubCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").IsDepend), chkAutoCompleationSubCategory.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void txtBuyProductCode_Validated(object sender, EventArgs e)
        {
            try
            {
            if (txtBuyProductCode.Text.Trim() != string.Empty)
            {
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                InvProductMaster existingInvProductMaster = new InvProductMaster();
                existingInvProductMaster = invProductMasterService.GetProductsByCode(txtBuyProductCode.Text.Trim());

                if (existingInvProductMaster != null)
                {
                    txtBuyProductName.Text = existingInvProductMaster.ProductName;
                    cmbBuyUnit.SelectedValue = existingInvProductMaster.UnitOfMeasureID;
                    txtBuyRate.Text = Common.ConvertDecimalToStringCurrency(existingInvProductMaster.SellingPrice);

                    if (ChkAutoSelectGetProduct.Checked == true)
                    {
                        txtGetProductCode.Text = txtBuyProductCode.Text.Trim();
                        txtGetProductName.Text = existingInvProductMaster.ProductName;
                        cmbGetUnit.SelectedValue = existingInvProductMaster.UnitOfMeasureID;
                        txtGetRate.Text = Common.ConvertDecimalToStringCurrency(existingInvProductMaster.SellingPrice);
                        txtGetPoints.Text = "0";
                        txtGetQty.Text = "1";
                        txtGetDiscPercentage.Text = "100";
                        txtGetDiscAmount.Text = txtGetRate.Text.Trim();
                    }
                    txtBuyQty.Focus();
                }
                else
                {
                    Toast.Show("Buy Product " + txtBuyProductCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    Common.ClearTextBox(txtBuyProductCode);
                    txtBuyProductCode.Focus();
                    isValidControls = false;

                }
            }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGetProductCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtGetProductCode.Text.Trim() != string.Empty)
                {
                    if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 10)
                    {
                        InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                        InvSubCategory2 existingInvSubCategory2 = new InvSubCategory2();

                        existingInvSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByCode(txtGetProductCode.Text.Trim());


                        if (existingInvSubCategory2 != null)
                        {
                            txtGetProductName.Text = existingInvSubCategory2.SubCategory2Name;
                            txtGetQty.Focus();

                        }
                        else
                        {
                            Toast.Show("SubCategory2 " + txtBuySubCategory2Code.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                            Common.ClearTextBox(txtGetProductCode);
                            txtGetProductCode.Focus();
                            isValidControls = false;

                        }
                    }
                    else
                    {
                        InvProductMasterService invProductMasterService = new InvProductMasterService();
                        InvProductMaster existingInvProductMaster = new InvProductMaster();
                        existingInvProductMaster = invProductMasterService.GetProductsByCode(txtGetProductCode.Text.Trim());

                        if (existingInvProductMaster != null)
                        {
                            txtGetProductName.Text = existingInvProductMaster.ProductName;
                            cmbGetUnit.SelectedValue = existingInvProductMaster.UnitOfMeasureID;
                            txtGetRate.Text = Common.ConvertDecimalToStringCurrency(existingInvProductMaster.SellingPrice);
                            txtGetQty.Focus();
                        }
                        else
                        {
                            Toast.Show("Get Product " + txtGetProductCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                            Common.ClearTextBox(txtGetProductCode);
                            txtGetProductCode.Focus();
                            isValidControls = false;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductDisProductCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtProductProductCode.Text.Trim() != string.Empty)
                {
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    InvProductMaster existingInvProductMaster = new InvProductMaster();
                    existingInvProductMaster = invProductMasterService.GetProductsByCode(txtProductProductCode.Text.Trim());

                    if (existingInvProductMaster != null)
                    {
                        txtProductProductName.Text = existingInvProductMaster.ProductName;
                        cmbProductUnit.SelectedValue = existingInvProductMaster.UnitOfMeasureID;
                        txtProductRate.Text = Common.ConvertDecimalToStringCurrency(existingInvProductMaster.SellingPrice);
                        txtProductQty.Focus();

                    }
                    else
                    {
                        Toast.Show("Product " + txtProductProductCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtProductProductCode);
                        txtProductProductCode.Focus();
                        isValidControls = false;

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductDisProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtProductQty.Focus();
                txtProductQty.SelectionStart = txtProductQty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuyProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtBuyQty.Focus();
                txtBuyQty.SelectionStart = txtBuyQty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGetProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtGetQty.Focus();
                txtGetQty.SelectionStart = txtGetQty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductDisProductName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtProductQty.Focus();
                txtProductQty.SelectionStart = txtProductQty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuyProductName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtBuyQty.Focus();
                txtBuyQty.SelectionStart = txtBuyQty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGetProductName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtGetQty.Focus();
                txtGetQty.SelectionStart = txtGetQty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductDisProductName_Leave(object sender, EventArgs e)
        {
            try
                {
                if (txtProductProductName.Text.Trim() != string.Empty)
                {
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    InvProductMaster existingInvProductMaster = new InvProductMaster();
                    existingInvProductMaster = invProductMasterService.GetProductsByName(txtProductProductName.Text.Trim());

                    if (existingInvProductMaster != null)
                    {
                        txtProductProductCode.Text = existingInvProductMaster.ProductCode;
                        cmbProductUnit.SelectedValue = existingInvProductMaster.UnitOfMeasureID;
                        txtProductRate.Text = Common.ConvertDecimalToStringCurrency(existingInvProductMaster.SellingPrice);
                        txtProductQty.Focus();
                    }
                    else
                    {
                        Toast.Show("Product " + txtProductProductName.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtProductProductName);
                        txtProductProductName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuyProductName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtBuyProductName.Text.Trim() != string.Empty)
                {
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    InvProductMaster existingInvProductMaster = new InvProductMaster();
                    existingInvProductMaster = invProductMasterService.GetProductsByName(txtBuyProductName.Text.Trim());

                    if (existingInvProductMaster != null)
                    {
                        txtBuyProductCode.Text = existingInvProductMaster.ProductCode;
                        cmbBuyUnit.SelectedValue = existingInvProductMaster.UnitOfMeasureID;
                        txtBuyRate.Text = Common.ConvertDecimalToStringCurrency(existingInvProductMaster.SellingPrice);
                        txtBuyQty.Focus();
                    }
                    else
                    {
                        Toast.Show("Buy Product " + txtBuyProductName.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtBuyProductName);
                        txtBuyProductName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGetProductName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtGetProductName.Text.Trim() != string.Empty)
                {
                    if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 10)
                    {
                        InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                        InvSubCategory2 existingInvSubCategory2 = new InvSubCategory2();

                        existingInvSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByName(txtBuySubCategory2Name.Text.Trim());


                        if (existingInvSubCategory2 != null)
                        {
                            txtGetProductCode.Text = existingInvSubCategory2.SubCategory2Code;
                            txtGetQty.Focus();

                        }
                        else
                        {
                            Toast.Show("SubCategory2 " + txtBuySubCategory2Name.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                            Common.ClearTextBox(txtGetProductName);
                            txtGetProductName.Focus();
                        }

                    }
                    else
                    {
                        
                        InvProductMasterService invProductMasterService = new InvProductMasterService();
                        InvProductMaster existingInvProductMaster = new InvProductMaster();
                        existingInvProductMaster = invProductMasterService.GetProductsByName(txtGetProductName.Text.Trim());

                        if (existingInvProductMaster != null)
                        {
                            txtGetProductCode.Text = existingInvProductMaster.ProductCode;
                            cmbGetUnit.SelectedValue = existingInvProductMaster.UnitOfMeasureID;
                            txtGetRate.Text = Common.ConvertDecimalToStringCurrency(existingInvProductMaster.SellingPrice);
                            txtGetQty.Focus();
                        }
                        else
                        {
                            Toast.Show("Get Product " + txtGetProductName.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                            Common.ClearTextBox(txtGetProductName);
                            txtGetProductName.Focus();
                        }
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
                Common.EnableButton(true, btnSave);
                Common.EnableComboBox(true, cmbPromotionType);

                Common.ClearTextBox(txtPromotionCode, txtPromotionDescription, txtRemark);

                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    InvPromotionMasterService promotionMasterService = new InvPromotionMasterService();
                    txtPromotionCode.Text = promotionMasterService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength);
                    Common.EnableTextBox(false, txtPromotionCode);
                    txtPromotionDescription.Focus();
                }
                else
                {
                    txtPromotionCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void UpdateBuyGrid(InvPromotionDetailsBuyXProduct invPromotionTempDetails)
        {
            try
            {
                existingInvProductMaster = new InvProductMaster();
                InvProductMasterService InvProductMasterService = new InvProductMasterService();
                existingInvProductMaster = InvProductMasterService.GetProductsByCode(txtBuyProductCode.Text);

                invPromotionTempDetails = new InvPromotionDetailsBuyXProduct();

                invPromotionTempDetails.BuyProductID = existingInvProductMaster.InvProductMasterID;
                invPromotionTempDetails.BuyProductCode = txtBuyProductCode.Text.Trim();
                invPromotionTempDetails.BuyProductName = txtBuyProductName.Text.Trim();
                invPromotionTempDetails.BuyUnitOfMeasureID = Common.ConvertStringToLong(cmbBuyUnit.SelectedValue.ToString());
                invPromotionTempDetails.UnitOfMeasure = cmbBuyUnit.Text.ToString().Trim();
                invPromotionTempDetails.BuyQty = Common.ConvertStringToDecimalQty(txtBuyQty.Text);
                invPromotionTempDetails.BuyRate = Common.ConvertStringToDecimalCurrency(txtBuyRate.Text);

                InvPromotionMasterService invPromotionMasterServices = new Service.InvPromotionMasterService();

                dgvBuyDetails.DataSource = null;
                invPromotionBuyXDetailTempList = invPromotionMasterServices.getUpdatePromotionDetailBuyXTemp(invPromotionBuyXDetailTempList, invPromotionTempDetails);
                dgvBuyDetails.AutoGenerateColumns = false;
                dgvBuyDetails.DataSource = invPromotionBuyXDetailTempList;
                dgvBuyDetails.Refresh();

                ClearBuyLine();

                txtBuyProductCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        
        private void UpdateGetSubCategory2Grid(InvPromotionDetailsGetYDetails invPromotionTempDetails)
        {
            try
            {
                //existingInvProductMaster = new InvProductMaster();
                //InvProductMasterService InvProductMasterService = new InvProductMasterService();
                InvSubCategory2 existingInvSubCategory2 = new InvSubCategory2();
                InvSubCategory2Service InvSubCategory2Service = new InvSubCategory2Service();

                //existingInvProductMaster = InvProductMasterService.GetProductsByCode(txtGetProductCode.Text);

                existingInvSubCategory2 = InvSubCategory2Service.GetInvSubCategory2ByCode(txtGetProductCode.Text);

                invPromotionTempDetails = new InvPromotionDetailsGetYDetails();

                invPromotionTempDetails.GetProductID = existingInvSubCategory2.InvSubCategory2ID;
                invPromotionTempDetails.ProductCode = txtGetProductCode.Text.Trim();
                invPromotionTempDetails.ProductName = txtGetProductName.Text.Trim();
                invPromotionTempDetails.GetUnitOfMeasureID = Common.ConvertStringToLong(cmbGetUnit.SelectedValue.ToString());
                invPromotionTempDetails.UnitOfMeasure = cmbGetUnit.Text.ToString().Trim();
                invPromotionTempDetails.GetQty = Common.ConvertStringToDecimalQty(txtGetQty.Text);
                invPromotionTempDetails.GetRate = Common.ConvertStringToDecimalCurrency(txtGetRate.Text);
                invPromotionTempDetails.GetDiscountPercentage = Common.ConvertStringToDecimal(txtGetDiscPercentage.Text);
                invPromotionTempDetails.GetDiscountAmount = Common.ConvertStringToDecimal(txtGetDiscAmount.Text);
                invPromotionTempDetails.GetPoints = Common.ConvertStringToInt(txtGetPoints.Text);

                InvPromotionMasterService invPromotionMasterServices = new Service.InvPromotionMasterService();

                dgvGetDetails.DataSource = null;
                invPromotionGetYDetailTempList = invPromotionMasterServices.getUpdatePromotionDetailGetYTemp(invPromotionGetYDetailTempList, invPromotionTempDetails);
                dgvGetDetails.AutoGenerateColumns = false;
                dgvGetDetails.DataSource = invPromotionGetYDetailTempList;
                dgvGetDetails.Refresh();


                ClearGetLine();

                txtGetProductCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void UpdateGetGrid(InvPromotionDetailsGetYDetails invPromotionTempDetails)
        {
            try
            {
                existingInvProductMaster = new InvProductMaster();
                InvProductMasterService InvProductMasterService = new InvProductMasterService();
                existingInvProductMaster = InvProductMasterService.GetProductsByCode(txtGetProductCode.Text);

                invPromotionTempDetails = new InvPromotionDetailsGetYDetails();

                invPromotionTempDetails.GetProductID = existingInvProductMaster.InvProductMasterID;
                invPromotionTempDetails.ProductCode = txtGetProductCode.Text.Trim();
                invPromotionTempDetails.ProductName = txtGetProductName.Text.Trim();
                invPromotionTempDetails.GetUnitOfMeasureID = Common.ConvertStringToLong(cmbGetUnit.SelectedValue.ToString());
                invPromotionTempDetails.UnitOfMeasure = cmbGetUnit.Text.ToString().Trim();
                invPromotionTempDetails.GetQty = Common.ConvertStringToDecimalQty(txtGetQty.Text);
                invPromotionTempDetails.GetRate = Common.ConvertStringToDecimalCurrency(txtGetRate.Text);
                invPromotionTempDetails.GetDiscountPercentage = Common.ConvertStringToDecimal(txtGetDiscPercentage.Text);
                invPromotionTempDetails.GetDiscountAmount = Common.ConvertStringToDecimal(txtGetDiscAmount.Text);
                invPromotionTempDetails.GetPoints = Common.ConvertStringToInt(txtGetPoints.Text);

                InvPromotionMasterService invPromotionMasterServices = new Service.InvPromotionMasterService();

                dgvGetDetails.DataSource = null;
                invPromotionGetYDetailTempList = invPromotionMasterServices.getUpdatePromotionDetailGetYTemp(invPromotionGetYDetailTempList, invPromotionTempDetails);
                dgvGetDetails.AutoGenerateColumns = false;
                dgvGetDetails.DataSource = invPromotionGetYDetailTempList;
                dgvGetDetails.Refresh();


                ClearGetLine();

                txtGetProductCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void UpdateProductDiscountGrid(InvPromotionDetailsProductDis invPromotionTempDetails)
        {
            try
            {
                existingInvProductMaster = new InvProductMaster();
                InvProductMasterService InvProductMasterService = new InvProductMasterService();
                existingInvProductMaster = InvProductMasterService.GetProductsByCode(txtProductProductCode.Text);

                invPromotionTempDetails = new InvPromotionDetailsProductDis();

                invPromotionTempDetails.ProductID = existingInvProductMaster.InvProductMasterID;
                invPromotionTempDetails.ProductCode = txtProductProductCode.Text.Trim();
                invPromotionTempDetails.ProductName = txtProductProductName.Text.Trim();
                invPromotionTempDetails.UnitOfMeasureID = Common.ConvertStringToLong(cmbProductUnit.SelectedValue.ToString());
                invPromotionTempDetails.UnitOfMeasure = cmbProductUnit.Text.ToString().Trim();
                invPromotionTempDetails.Qty = Common.ConvertStringToDecimalQty(txtProductQty.Text);
                invPromotionTempDetails.Rate = Common.ConvertStringToDecimalCurrency(txtProductRate.Text);
                invPromotionTempDetails.DiscountPercentage = Common.ConvertStringToDecimal(txtProductDiscPercentage.Text);
                invPromotionTempDetails.DiscountAmount = Common.ConvertStringToDecimal(txtProductDiscAmount.Text);
                invPromotionTempDetails.Points = Common.ConvertStringToInt(txtProductPoints.Text);

                InvPromotionMasterService invPromotionMasterServices = new Service.InvPromotionMasterService();

                dgvProductDiscount.DataSource = null;
                invPromotionProductDiscountDetailTempList = invPromotionMasterServices.getUpdatePromotionDetailProductDiscountTemp(invPromotionProductDiscountDetailTempList, invPromotionTempDetails);
                dgvProductDiscount.AutoGenerateColumns = false;
                dgvProductDiscount.DataSource = invPromotionProductDiscountDetailTempList;
                dgvProductDiscount.Refresh();

                ClearProductDiscountLine();

                txtProductProductCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void UpdateDepartmentGrid(InvPromotionDetailsDepartmentDis invPromotionTempDetails)
        {
            try
            {
                existingInvDeparment = new InvDepartment();
                InvDepartmentService InvDepartmentService = new InvDepartmentService();
                existingInvDeparment = InvDepartmentService.GetInvDepartmentsByCode(txtDepartmentCode.Text, true);

                invPromotionTempDetails = new InvPromotionDetailsDepartmentDis();

                invPromotionTempDetails.DepartmentID = existingInvDeparment.InvDepartmentID;
                invPromotionTempDetails.DepartmentCode = txtDepartmentCode.Text.Trim();
                invPromotionTempDetails.DepartmentName = txtDepartmentDescription.Text.Trim();
                invPromotionTempDetails.Qty = Common.ConvertStringToDecimalQty(txtDepartmentQty.Text);
                invPromotionTempDetails.DiscountPercentage = Common.ConvertStringToDecimal(txtDepartmentDiscPercentage.Text);
                invPromotionTempDetails.DiscountAmount= Common.ConvertStringToDecimal(txtDepartmentDiscAmount.Text);
                invPromotionTempDetails.Points = Common.ConvertStringToInt(txtDepartmentPoints.Text);

                InvPromotionMasterService invPromotionMasterServices = new Service.InvPromotionMasterService();

                dgvDepartmentDiscount.DataSource = null;
                invPromotionDepartmentDetailTempList = invPromotionMasterServices.getUpdatePromotionDetailDepartmentTemp(invPromotionDepartmentDetailTempList, invPromotionTempDetails);
                dgvDepartmentDiscount.AutoGenerateColumns = false;
                dgvDepartmentDiscount.DataSource = invPromotionDepartmentDetailTempList;
                dgvDepartmentDiscount.Refresh();

                ClearDepartmentLine();

                txtDepartmentCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void UpdateBuyDepartmentGrid(InvPromotionDetailsBuyXDepartment invPromotionTempDetails)
        {
            try
            {
                existingInvDeparment = new InvDepartment();
                InvDepartmentService InvDepartmentService = new InvDepartmentService();
                existingInvDeparment = InvDepartmentService.GetInvDepartmentsByCode(txtBuyDepartmentCode.Text, true);

                invPromotionTempDetails = new InvPromotionDetailsBuyXDepartment();

                invPromotionTempDetails.BuyDepartmentID = existingInvDeparment.InvDepartmentID;
                invPromotionTempDetails.DepartmentCode = txtBuyDepartmentCode.Text.Trim();
                invPromotionTempDetails.DepartmentName = txtBuyDepartmentName.Text.Trim();
                invPromotionTempDetails.BuyQty = Common.ConvertStringToDecimalQty(txtBuyDepartmentQty.Text);
                

                InvPromotionMasterService invPromotionMasterServices = new Service.InvPromotionMasterService();

                dgvBuyDepartment.DataSource = null;
                invPromotionBuyXDepartmentDetailTempList = invPromotionMasterServices.getUpdatePromotionDetailBuyXDepartmentTemp(invPromotionBuyXDepartmentDetailTempList, invPromotionTempDetails);
                dgvBuyDepartment.AutoGenerateColumns = false;
                dgvBuyDepartment.DataSource = invPromotionBuyXDepartmentDetailTempList;
                dgvBuyDepartment.Refresh();

                ClearBuyDepartmentLine();

                txtBuyDepartmentCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void UpdateCategoryGrid(InvPromotionDetailsCategoryDis invPromotionTempDetails)
        {
            try
            {
                existingInvCategory = new InvCategory();
                InvCategoryService InvCategoryService = new InvCategoryService();
                existingInvCategory = InvCategoryService.GetInvCategoryByCode(txtCategoryCode.Text, true);

                invPromotionTempDetails = new InvPromotionDetailsCategoryDis();

                invPromotionTempDetails.CategoryID = existingInvCategory.InvCategoryID;
                invPromotionTempDetails.CategoryCode = txtCategoryCode.Text.Trim();
                invPromotionTempDetails.CategoryName = txtCategoryDescription.Text.Trim();
                invPromotionTempDetails.Qty = Common.ConvertStringToDecimalQty(txtCategoryQty.Text);
                invPromotionTempDetails.DiscountPercentage = Common.ConvertStringToDecimal(txtCategoryDiscPercentage.Text);
                invPromotionTempDetails.DiscountAmount = Common.ConvertStringToDecimal(txtCategoryDiscAmount.Text);
                invPromotionTempDetails.Points = Common.ConvertStringToInt(txtCategoryPoints.Text);

                InvPromotionMasterService invPromotionMasterServices = new Service.InvPromotionMasterService();

                dgvCategoryDiscount.DataSource = null;
                invPromotionCategoryDetailTempList = invPromotionMasterServices.getUpdatePromotionDetailCategoryTemp(invPromotionCategoryDetailTempList, invPromotionTempDetails);
                dgvCategoryDiscount.AutoGenerateColumns = false;
                dgvCategoryDiscount.DataSource = invPromotionCategoryDetailTempList;
                dgvCategoryDiscount.Refresh();

                ClearCategoryLine();

                txtCategoryCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void UpdateBuyCategoryGrid(InvPromotionDetailsBuyXCategory invPromotionTempDetails)
        {
            try
            {
                existingInvCategory = new InvCategory();
                InvCategoryService InvCategoryService = new InvCategoryService();
                existingInvCategory = InvCategoryService.GetInvCategoryByCode(txtBuyCategoryCode.Text, true);

                invPromotionTempDetails = new InvPromotionDetailsBuyXCategory();

                invPromotionTempDetails.BuyCategoryID = existingInvCategory.InvCategoryID;
                invPromotionTempDetails.CategoryCode = txtBuyCategoryCode.Text.Trim();
                invPromotionTempDetails.CategoryName = txtBuyCategoryName.Text.Trim();
                invPromotionTempDetails.BuyQty = Common.ConvertStringToDecimalQty(txtBuyCategoryQty.Text);
                
                InvPromotionMasterService invPromotionMasterServices = new Service.InvPromotionMasterService();

                dgvBuyCategory.DataSource = null;
                invPromotionBuyXCategoryDetailTempList = invPromotionMasterServices.getUpdatePromotionBuyXDetailCategoryTemp(invPromotionBuyXCategoryDetailTempList, invPromotionTempDetails);
                dgvBuyCategory.AutoGenerateColumns = false;
                dgvBuyCategory.DataSource = invPromotionBuyXCategoryDetailTempList;
                dgvBuyCategory.Refresh();

                ClearBuyCategoryLine();

                txtBuyCategoryCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void UpdateSubCategoryGrid(InvPromotionDetailsSubCategoryDis invPromotionTempDetails)
        {
            try
            {
                existingInvSubCategory = new InvSubCategory();
                InvSubCategoryService InvSubCategoryService = new InvSubCategoryService();
                existingInvSubCategory = InvSubCategoryService.GetInvSubCategoryByCode(txtSubCategoryCode.Text, true);

                invPromotionTempDetails = new InvPromotionDetailsSubCategoryDis();

                invPromotionTempDetails.SubCategoryID = existingInvSubCategory.InvSubCategoryID;
                invPromotionTempDetails.SubCategoryCode = txtSubCategoryCode.Text.Trim();
                invPromotionTempDetails.SubCategoryName = txtSubCategoryDescription.Text.Trim();
                invPromotionTempDetails.Qty = Common.ConvertStringToDecimalQty(txtSubCategoryQty.Text);
                invPromotionTempDetails.DiscountPercentage = Common.ConvertStringToDecimal(txtSubCategoryDiscPercentage.Text);
                invPromotionTempDetails.DiscountAmount = Common.ConvertStringToDecimal(txtSubCategoryDiscAmount.Text);
                invPromotionTempDetails.Points = Common.ConvertStringToInt(txtSubCategoryPoints.Text);

                InvPromotionMasterService invPromotionMasterServices = new Service.InvPromotionMasterService();

                dgvSubCategory.DataSource = null;
                invPromotionSubCategoryDetailTempList = invPromotionMasterServices.getUpdatePromotionDetailSubCategoryTemp(invPromotionSubCategoryDetailTempList, invPromotionTempDetails);
                dgvSubCategory.AutoGenerateColumns = false;
                dgvSubCategory.DataSource = invPromotionSubCategoryDetailTempList;
                dgvSubCategory.Refresh();

                ClearSubCategoryLine();

                txtSubCategoryCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void UpdateSubCategory2Grid(InvPromotionDetailsSubCategory2Dis invPromotionTempDetails)
        {
            try
            {
                InvSubCategory2 existingInvSubCategory2Dis = new InvSubCategory2();
                InvSubCategory2Service InvSubCategory2Service = new InvSubCategory2Service();
                existingInvSubCategory2Dis = InvSubCategory2Service.GetInvSubCategory2ByCode(txtSubCategory2Code.Text);

                invPromotionTempDetails = new InvPromotionDetailsSubCategory2Dis();

                invPromotionTempDetails.SubCategory2ID = existingInvSubCategory2Dis.InvSubCategory2ID;
                invPromotionTempDetails.SubCategory2Code = txtSubCategory2Code.Text.Trim();
                invPromotionTempDetails.SubCategory2Name = txtSubCategory2Description.Text.Trim();
                invPromotionTempDetails.Qty = Common.ConvertStringToDecimalQty(txtSubCategory2Qty.Text);
                invPromotionTempDetails.DiscountPercentage = Common.ConvertStringToDecimal(txtSubCategory2DiscPercentage.Text);
                invPromotionTempDetails.DiscountAmount = Common.ConvertStringToDecimal(txtSubCategory2DiscAmount.Text);
                invPromotionTempDetails.Points = Common.ConvertStringToInt(txtSubCategory2Points.Text);

                InvPromotionMasterService invPromotionMasterServices = new Service.InvPromotionMasterService();

                dgvSubCategory2.DataSource = null;
                invPromotionSubCategory2DetailTempList = invPromotionMasterServices.getUpdatePromotionDetailSubCategory2Temp(invPromotionSubCategory2DetailTempList, invPromotionTempDetails);
                dgvSubCategory2.AutoGenerateColumns = false;
                dgvSubCategory2.DataSource = invPromotionSubCategory2DetailTempList;
                dgvSubCategory2.Refresh();

                ClearSubCategory2Line();

                txtSubCategory2Code.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }


        private void UpdateBuySubCategoryGrid(InvPromotionDetailsBuyXSubCategory invPromotionTempDetails)
        {
            try
            {
                InvSubCategory existingInvSubCategory = new InvSubCategory();
                InvSubCategoryService InvSubCategoryService = new InvSubCategoryService();
                existingInvSubCategory = InvSubCategoryService.GetInvSubCategoryByCode(txtBuySubCategoryCode.Text, true);

                invPromotionTempDetails = new InvPromotionDetailsBuyXSubCategory();

                invPromotionTempDetails.BuySubCategoryID = existingInvSubCategory.InvSubCategoryID;
                invPromotionTempDetails.SubCategoryCode = txtBuySubCategoryCode.Text.Trim();
                invPromotionTempDetails.SubCategoryName = txtBuySubCategoryName.Text.Trim();
                invPromotionTempDetails.BuyQty = Common.ConvertStringToDecimalQty(txtBuySubCategoryQty.Text);               

                InvPromotionMasterService invPromotionMasterServices = new Service.InvPromotionMasterService();

                dgvBuySubCategory.DataSource = null;
                
                invPromotionBuyXSubCategoryDetailsTempDetailsList = invPromotionMasterServices.getUpdatePromotionBuyXDetailSubCategoryTemp(invPromotionBuyXSubCategoryDetailsTempDetailsList, invPromotionTempDetails);
                dgvBuySubCategory.AutoGenerateColumns = false;
                dgvBuySubCategory.DataSource = invPromotionBuyXSubCategoryDetailsTempDetailsList;
                dgvBuySubCategory.Refresh();

                ClearBuySubCategoryLine();

                txtBuySubCategoryCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }


        private void UpdateBuySubCategory2Grid(InvPromotionDetailsBuyXSubCategory2 invPromotionTempDetails)
        {
            try
            {
                InvSubCategory2 existingInvSubCategory2 = new InvSubCategory2();
                InvSubCategory2Service InvSubCategory2Service = new InvSubCategory2Service();
                existingInvSubCategory2 = InvSubCategory2Service.GetInvSubCategory2ByCode(txtBuySubCategory2Code.Text);

                invPromotionTempDetails = new InvPromotionDetailsBuyXSubCategory2();

                invPromotionTempDetails.BuySubCategory2ID = existingInvSubCategory2.InvSubCategory2ID;
                invPromotionTempDetails.SubCategory2Code = txtBuySubCategory2Code.Text.Trim();
                invPromotionTempDetails.SubCategory2Name = txtBuySubCategory2Name.Text.Trim();
                invPromotionTempDetails.BuyQty = Common.ConvertStringToDecimalQty(txtBuySubCategory2Qty.Text);

                InvPromotionMasterService invPromotionMasterServices = new Service.InvPromotionMasterService();

                dgvBuySubCategory2.DataSource = null;

                invPromotionBuyXSubCategory2DetailsTempDetailsList = invPromotionMasterServices.getUpdatePromotionBuyXDetailSubCategory2Temp(invPromotionBuyXSubCategory2DetailsTempDetailsList, invPromotionTempDetails);
                dgvBuySubCategory2.AutoGenerateColumns = false;
                dgvBuySubCategory2.DataSource = invPromotionBuyXSubCategory2DetailsTempDetailsList;
                dgvBuySubCategory2.Refresh();

                ClearBuySubCategory2Line();

                txtBuySubCategory2Code.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        

        private void SelectDays()
        {
            if (chkMonday.Checked) { chkMondayTime.Enabled = true; } else { chkMondayTime.Checked = false; chkMondayTime.Enabled = false; }
            if (chkTuesday.Checked) { chkTuesdayTime.Enabled = true; } else { chkTuesdayTime.Checked = false; chkTuesdayTime.Enabled = false; }
            if (chkWednesday.Checked) { chkWednesdayTime.Enabled = true; } else { chkWednesdayTime.Checked = false; chkWednesdayTime.Enabled = false; }
            if (chkThuresday.Checked) { chkThuresdayTime.Enabled = true; } else { chkThuresdayTime.Checked = false; chkThuresdayTime.Enabled = false; }
            if (chkFriday.Checked) { chkFridayTime.Enabled = true; } else { chkFridayTime.Checked = false; chkFridayTime.Enabled = false; }
            if (chkSaturday.Checked) { chkSaturdayTime.Enabled = true; } else { chkSaturdayTime.Checked = false; chkSaturdayTime.Enabled = false; }
            if (chkSunday.Checked) { chkSundayTime.Enabled = true; } else { chkSundayTime.Checked = false; chkSundayTime.Enabled = false; }

        }

        private void SelectTimePeriod()
        {
            if (chkMondayTime.Checked) { dtpMondayFrom.Enabled = true; dtpMondayTo.Enabled = true; } else { dtpMondayFrom.Enabled = false; dtpMondayTo.Enabled = false; }
            if (chkTuesdayTime.Checked) { dtpTuesdayFrom.Enabled = true; dtpTuesdayTo.Enabled = true; } else { dtpTuesdayFrom.Enabled = false; dtpTuesdayTo.Enabled = false; }
            if (chkWednesdayTime.Checked) { dtpWednesdayFrom.Enabled = true; dtpWednesdayTo.Enabled = true; } else { dtpWednesdayFrom.Enabled = false; dtpWednesdayTo.Enabled = false; }
            if (chkThuresdayTime.Checked) { dtpThuresdayFrom.Enabled = true; dtpThuresdayTo.Enabled = true; } else { dtpThuresdayFrom.Enabled = false; dtpThuresdayTo.Enabled = false; }
            if (chkFridayTime.Checked) { dtpFridayFrom.Enabled = true; dtpFridayTo.Enabled = true; } else { dtpFridayFrom.Enabled = false; dtpFridayTo.Enabled = false; }
            if (chkSaturdayTime.Checked) { dtpSaturdayFrom.Enabled = true; dtpSaturdayTo.Enabled = true; } else { dtpSaturdayFrom.Enabled = false; dtpSaturdayTo.Enabled = false; }
            if (chkSundayTime.Checked) { dtpSundayFrom.Enabled = true; dtpSundayTo.Enabled = true; } else { dtpSundayFrom.Enabled = false; dtpSundayTo.Enabled = false; }

        }

        private void ClearBuyLine()
        {
            Common.ClearTextBox(txtBuyProductCode, txtBuyProductName, txtBuyQty, txtBuyRate);
            Common.ClearComboBox(cmbBuyUnit);

        }

        private void ClearGetLine()
        {
            Common.ClearTextBox(txtGetProductCode, txtGetProductName, txtGetQty, txtGetRate, txtGetDiscPercentage, txtGetDiscAmount, txtGetPoints);
            Common.ClearComboBox(cmbGetUnit);

        }

        private void ClearProductDiscountLine()
        {
            Common.ClearTextBox(txtProductProductCode, txtProductProductName, txtProductQty, txtProductRate, txtProductDiscPercentage, txtProductDiscAmount, txtProductPoints);
            Common.ClearComboBox(cmbProductUnit);

        }

        private void ClearDepartmentLine()
        {
            Common.ClearTextBox(txtDepartmentCode, txtDepartmentDescription, txtDepartmentQty, txtDepartmentDiscPercentage, txtDepartmentDiscAmount, txtDepartmentPoints);

        }

        private void ClearBuyDepartmentLine()
        {
            Common.ClearTextBox(txtBuyDepartmentCode, txtBuyDepartmentName, txtBuyDepartmentQty);

        }

        private void ClearCategoryLine()
        {
            Common.ClearTextBox(txtCategoryCode, txtCategoryDescription, txtCategoryQty, txtCategoryDiscPercentage, txtCategoryDiscAmount, txtCategoryPoints);

        }

        private void ClearBuyCategoryLine()
        {
            Common.ClearTextBox(txtBuyCategoryCode, txtBuyCategoryName, txtBuyCategoryQty);

        }

        private void ClearSubCategoryLine()
        {
            Common.ClearTextBox(txtSubCategoryCode, txtSubCategoryDescription, txtSubCategoryQty, txtSubCategoryDiscPercentage, txtSubCategoryDiscAmount, txtSubCategoryPoints);

        }

        private void ClearSubCategory2Line()
        {
            Common.ClearTextBox(txtSubCategory2Code, txtSubCategory2Description, txtSubCategory2Qty, txtSubCategory2DiscPercentage, txtSubCategory2DiscAmount, txtSubCategory2Points);

        }

        private void ClearBuySubCategoryLine()
        {
            Common.ClearTextBox(txtBuySubCategoryCode, txtBuySubCategoryName, txtBuySubCategoryQty);

        }

        private void ClearBuySubCategory2Line()
        {
            Common.ClearTextBox(txtBuySubCategory2Code, txtBuySubCategory2Name, txtBuySubCategory2Qty);

        }

        private void txtBuyQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtBuyQty.Text) > 0) { UpdateBuyGrid(existingBuyXDetailTemp); }
                    if (ChkAutoSelectGetProduct.Checked == true)
                    {
                       
                        if (Common.ConvertStringToDecimal(txtGetDiscAmount.Text) > 0) { UpdateGetGrid(existingGetYDetailTemp); } else { txtGetDiscAmount.Focus(); }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkMonday_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectDays();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkMondayTime_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectTimePeriod();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTuesdayTime_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectTimePeriod();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkWednesdayTime_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectTimePeriod();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkThuresdayTime_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                 SelectTimePeriod();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkFridayTime_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectTimePeriod();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkSaturdayTime_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectTimePeriod();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkSundayTime_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectTimePeriod();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTuesday_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectDays();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkWednesday_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectDays();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkThuresday_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectDays();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkFriday_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectDays();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkSaturday_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectDays();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkSunday_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectDays();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGetQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {           
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimalQty(txtGetQty.Text) > 0) 
                    {
                        if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 10)
                        {
                            txtGetDiscPercentage.Focus();
                        }
                        else
                        {
                            txtGetPoints.Focus();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtGetPoints_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtGetDiscPercentage.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGetDiscPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtGetDiscPercentage.Text) <= 100)
                    {
                        if (Common.ConvertStringToDecimal(txtGetDiscPercentage.Text) > 0)
                        {
                            if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 10)
                            {
                                UpdateGetSubCategory2Grid(existingGetYDetailTemp);
                            }
                            else
                            {
                                UpdateGetGrid(existingGetYDetailTemp);
                            }
                        }
                        else
                        {
                            txtGetDiscAmount.Focus();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGetDiscAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtGetDiscAmount.Text) > 0) 
                    {
                        if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 10)
                        {
                            UpdateGetSubCategory2Grid(existingGetYDetailTemp);
                        }
                        else
                        {
                            UpdateGetGrid(existingGetYDetailTemp);
                        }
                    } 
                    else 
                    { 
                        txtGetDiscAmount.Focus();
                    }                   

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimalQty(txtProductQty.Text) > 0) { txtProductPoints.Focus(); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductPoints_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtProductDiscPercentage.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductDiscPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtProductDiscPercentage.Text) <= 100)
                    {
                        if (Common.ConvertStringToDecimal(txtProductDiscPercentage.Text) > 0) { UpdateProductDiscountGrid(existingProductDiscountDetailTemp); } else { txtProductDiscAmount.Focus(); }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductDiscAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtProductDiscAmount.Text) > 0) { UpdateProductDiscountGrid(existingProductDiscountDetailTemp); } else { txtProductDiscAmount.Focus(); }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimalQty(txtDepartmentQty.Text) > 0) { txtDepartmentPoints.Focus(); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentPoints_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtDepartmentDiscPercentage.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentDiscPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtDepartmentDiscPercentage.Text) <= 100)
                    {
                        if (Common.ConvertStringToDecimal(txtDepartmentDiscPercentage.Text) > 0) { UpdateDepartmentGrid(existingDepartmentDetailTemp); } else { txtDepartmentDiscAmount.Focus(); }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentDiscAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtDepartmentDiscAmount.Text) > 0) { UpdateDepartmentGrid(existingDepartmentDetailTemp); } else { txtProductDiscAmount.Focus(); }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimalQty(txtCategoryQty.Text) > 0) { txtCategoryPoints.Focus(); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryPoints_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtCategoryDiscPercentage.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryDiscPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtCategoryDiscPercentage.Text) <= 100)
                    {
                        if (Common.ConvertStringToDecimal(txtCategoryDiscPercentage.Text) > 0) { UpdateCategoryGrid(existingCategoryDetailTemp); } else { txtCategoryDiscAmount.Focus(); }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryDiscAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtCategoryDiscAmount.Text) > 0) { UpdateCategoryGrid(existingCategoryDetailTemp); } else { txtCategoryDiscAmount.Focus(); }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimalQty(txtSubCategoryQty.Text) > 0) { txtSubCategoryPoints.Focus(); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryPoints_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtSubCategoryDiscPercentage.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryDiscPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtSubCategoryDiscPercentage.Text) <= 100)
                    {
                        if (Common.ConvertStringToDecimal(txtSubCategoryDiscPercentage.Text) > 0) { UpdateSubCategoryGrid(existingSubCategoryDetailTemp); } else { txtSubCategoryDiscAmount.Focus(); }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryDiscAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtSubCategoryDiscAmount.Text) > 0) { UpdateSubCategoryGrid(existingSubCategoryDetailTemp); } else { txtSubCategoryDiscAmount.Focus(); }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void rdoCash_Click(object sender, EventArgs e)
        {
            try
            {
                 ChangePaymentMethod();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void rdoCard_Click(object sender, EventArgs e)
        {
            try
            {
                ChangePaymentMethod();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void rdoAll_Click(object sender, EventArgs e)
        {
            try
            {
                ChangePaymentMethod();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkConsiderValue_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectValueRange();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        public override void InitializeForm()
        {
            try
            {                
                cmbProvider.SelectedIndex = -1;
                cmbRecipient.SelectedIndex = -1;
                SelectDays();
                ResetTimePeriod();
                cmbBuyUnit.SelectedIndex = -1;
                cmbGetUnit.SelectedIndex = -1;
                cmbProductUnit.SelectedIndex = -1;

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtPromotionCode, txtPromotionDescription);
                Common.ClearTextBox(txtPromotionCode, txtPromotionDescription);
                Common.ClearComboBox(cmbPromotionType);

                existingPromotion = null;
                existingInvDeparment = null;
                existingInvCategory = null;
                existingInvSubCategory = null;
                existingInvProductMaster = null;
                existingAllowLocationTemp = null;
                existingCategoryDetailTemp = null;
                existingDepartmentDetailTemp = null;
                existingInvProductMaster = null;
                existingSubCategoryDetailTemp = null;
                existingGetYDetailTemp = null;
                existingBuyXDetailTemp = null;

                invPromotionBuyXDetailTempList = new List<InvPromotionDetailsBuyXProduct>();
                invPromotionGetYDetailTempList = new List<InvPromotionDetailsGetYDetails>();
                invPromotionCategoryDetailTempList = new List<InvPromotionDetailsCategoryDis>();
                invPromotionDepartmentDetailTempList = new List<InvPromotionDetailsDepartmentDis>();
                invPromotionSubCategoryDetailTempList = new List<InvPromotionDetailsSubCategoryDis>();
                invPromotionProductDiscountDetailTempList = new List<InvPromotionDetailsProductDis>();
                invPromotionDepartmentDetailTempList = new List<InvPromotionDetailsDepartmentDis>();
                invPromotionCategoryDetailTempList = new List<InvPromotionDetailsCategoryDis>();
                invPromotionSubCategoryDetailTempList = new List<InvPromotionDetailsSubCategoryDis>();
                invPromotionAllowLocationList = new List<InvPromotionDetailsAllowLocations>();
                invPromotionBuyXSubCategory2DetailsTempDetailsList = new List<InvPromotionDetailsBuyXSubCategory2>();
                invPromotionSubCategory2DetailTempList = new List<InvPromotionDetailsSubCategory2Dis>();
 

                InvPromotionMasterService invPromotionMasterService = new InvPromotionMasterService();

                Common.SetAutoComplete(txtPromotionCode, invPromotionMasterService.GetAllInvPromotionCodes(), chkAutoCompleationPromotion.Checked);
                Common.SetAutoComplete(txtPromotionDescription, invPromotionMasterService.GetAllInvPromotionNames(), chkAutoCompleationPromotion.Checked);
                
               // UnCheckedAllLocations();
                CheckedAllLocations();
                chkAutoCompleationPromotion.Checked = true;
                txtPromotionCode.Focus();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

       
        private void chkConsiderProvider_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkConsiderProvider.Checked) { cmbProvider.Enabled = true; }
                else { cmbProvider.Enabled = false; cmbProvider.SelectedIndex = -1; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        

        private void chkAutoCompleationBuyProduct_CheckedChanged_1(object sender, EventArgs e)
        {
            try
           {
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                Common.SetAutoComplete(txtBuyProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationBuyProduct.Checked);
                Common.SetAutoComplete(txtBuyProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationBuyProduct.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationBuyDepartment_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvDepartmentService invDepartmentService = new Service.InvDepartmentService();
                Common.SetAutoComplete(txtBuyDepartmentCode, invDepartmentService.GetInvDepartmentCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend), chkAutoCompleationBuyDepartment.Checked);
                Common.SetAutoComplete(txtBuyDepartmentName, invDepartmentService.GetInvDepartmentNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend), chkAutoCompleationBuyDepartment.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationBuyCaytego_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvCategoryService invCategoryService = new Service.InvCategoryService();
                Common.SetAutoComplete(txtBuyCategoryCode, invCategoryService.GetInvCategoryCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend), chkAutoCompleationBuyCaytegory.Checked);
                Common.SetAutoComplete(txtBuyCategoryName, invCategoryService.GetInvCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend), chkAutoCompleationBuyCaytegory.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationBuySubCategory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvSubCategoryService invCategoryService = new Service.InvSubCategoryService();
                Common.SetAutoComplete(txtBuySubCategoryCode, invCategoryService.GetInvSubCategoryCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").IsDepend), chkAutoCompleationBuySubCategory.Checked);
                Common.SetAutoComplete(txtBuySubCategoryName, invCategoryService.GetInvSubCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").IsDepend), chkAutoCompleationBuySubCategory.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuySubCategoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtBuySubCategoryQty.Focus();
                txtBuySubCategoryQty.SelectionStart = txtBuySubCategoryQty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuySubCategoryCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtBuySubCategoryCode.Text.Trim() != string.Empty)
                {
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    InvSubCategory existingInvSubCategory = new InvSubCategory();

                    existingInvSubCategory = invSubCategoryService.GetInvSubCategoryByCode(txtBuySubCategoryCode.Text.Trim(), isDependSubCategory2);


                    if (existingInvSubCategory != null)
                    {
                        txtBuySubCategoryName.Text = existingInvSubCategory.SubCategoryName;
                        txtBuySubCategoryQty.Focus();

                    }
                    else
                    {
                        Toast.Show("SubCategory " + txtBuySubCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtBuySubCategoryCode);
                        txtBuySubCategoryCode.Focus();
                        isValidControls = false;

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuySubCategoryName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtBuySubCategoryQty.Focus();
                txtBuySubCategoryQty.SelectionStart = txtBuySubCategoryQty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuySubCategoryName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtBuySubCategoryName.Text.Trim() != string.Empty)
                {
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    InvSubCategory existingInvSubCategory = new InvSubCategory();

                    existingInvSubCategory = invSubCategoryService.GetInvSubCategoryByName(txtBuySubCategoryName.Text.Trim(), isDependSubCategory2);


                    if (existingInvSubCategory != null)
                    {
                        txtBuySubCategoryCode.Text = existingInvSubCategory.SubCategoryCode;
                        txtBuySubCategoryQty.Focus();

                    }
                    else
                    {
                        Toast.Show("SubCategory " + txtBuySubCategoryName.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtBuySubCategoryName);
                        txtBuySubCategoryName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuyCategoryCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtBuyCategoryCode.Text.Trim() != string.Empty)
                {
                    InvCategoryService invCategoryService = new InvCategoryService();
                    InvCategory existingInvCategory = new InvCategory();

                    existingInvCategory = invCategoryService.GetInvCategoryByCode(txtBuyCategoryCode.Text.Trim(), isDependSubCategory);


                    if (existingInvCategory != null)
                    {
                        txtBuyCategoryName.Text = existingInvCategory.CategoryName;
                        txtBuyCategoryQty.Focus();
                    }
                    else
                    {
                        Toast.Show("Category " + txtBuyCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtBuyCategoryCode);
                        txtBuyCategoryCode.Focus();
                        isValidControls = false;

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuyCategoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtBuyCategoryQty.Focus();
                txtBuyCategoryQty.SelectionStart = txtBuyCategoryQty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuyCategoryName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtBuyCategoryQty.Focus();
                txtBuyCategoryQty.SelectionStart = txtBuyCategoryQty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuyCategoryName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtBuyCategoryName.Text.Trim() != string.Empty)
                {
                    InvCategoryService invCategoryService = new InvCategoryService();
                    InvCategory existingInvCategory = new InvCategory();

                    existingInvCategory = invCategoryService.GetInvCategoryByName(txtBuyCategoryName.Text.Trim(), isDependSubCategory);


                    if (existingInvCategory != null)
                    {
                        txtBuyCategoryCode.Text = existingInvCategory.CategoryCode;
                        txtBuyCategoryQty.Focus();

                    }
                    else
                    {
                        Toast.Show("Category " + txtBuyCategoryName.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtBuyCategoryName);
                        txtBuyCategoryName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuyDepartmentCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtBuyDepartmentCode.Text.Trim() != string.Empty)
                {
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    InvDepartment existingInvDepartment = new InvDepartment();
                    existingInvDepartment = invDepartmentService.GetInvDepartmentsByCode(txtBuyDepartmentCode.Text.Trim(), isDependCategory);

                    if (existingInvDepartment != null)
                    {
                        txtBuyDepartmentName.Text = existingInvDepartment.DepartmentName;
                        txtBuyDepartmentQty.Focus();
                    }
                    else
                    {
                        Toast.Show("Department " + txtBuyDepartmentCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtBuyDepartmentCode);
                        txtBuyDepartmentCode.Focus();
                        isValidControls = false;

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuyDepartmentCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtBuyDepartmentQty.Focus();
                txtBuyDepartmentQty.SelectionStart = txtBuyDepartmentQty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuyDepartmentName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtBuyDepartmentName.Text.Trim() != string.Empty)
                {
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    InvDepartment existingInvDepartment = new InvDepartment();
                    existingInvDepartment = invDepartmentService.GetInvDepartmentsByName(txtBuyDepartmentName.Text.Trim(), isDependCategory);

                    if (existingInvDepartment != null)
                    {
                        txtBuyDepartmentCode.Text = existingInvDepartment.DepartmentCode;
                        txtBuyDepartmentQty.Focus();
                    }
                    else
                    {
                        Toast.Show("Department " + txtBuyDepartmentName.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtBuyDepartmentName);
                        txtBuyDepartmentName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuyDepartmentName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtBuyDepartmentQty.Focus();
                txtBuyDepartmentQty.SelectionStart = txtBuyDepartmentQty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuyDepartmentQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtBuyDepartmentQty.Text) > 0) { UpdateBuyDepartmentGrid(existingBuyXDepartmentDetailTemp); } else { txtBuyDepartmentQty.Focus(); }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuyCategoryQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtBuyCategoryQty.Text) > 0) { UpdateBuyCategoryGrid(existingBuyXCategoryDetailTemp); } else { txtBuyCategoryQty.Focus(); }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAllLocations_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (isCheckedAllLocations == true)
                {
                    if (chkAllLocations.Checked) { CheckedAllLocations(); } else { UnCheckedAllLocations(); }
                }
                }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvLocationInfo_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                isCheckedAllLocations = false;
                CheckCheckedStatus();
                isCheckedAllLocations = true;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBody_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                lblCount.Text = txtBody.TextLength.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void chkAllType_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (isCheckedAllTypes == true)
                {
                    if (chkAllType.Checked) { CheckedAllTypes(); } else { UnCheckedAllTypes(); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvType_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                isCheckedAllTypes = false;
                CheckCheckedStatusTypes();
                isCheckedAllTypes = true;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void  FillPromotion()
        {
            try
            {
                #region Header
                Location LogingLocation = new Location();

                existingPromotion = new InvPromotionMaster();
                existingPromotion.CompanyID = Common.LoggedCompanyID;
                existingPromotion.LocationID = Common.LoggedLocationID;
                existingPromotion.PromotionCode = txtPromotionCode.Text.Trim();
                existingPromotion.PromotionName = txtPromotionDescription.Text.Trim();
                existingPromotion.PromotionTypeID = Common.ConvertStringToInt(cmbPromotionType.SelectedValue.ToString());                
                existingPromotion.StartDate = dtpStartDate.Value;
                existingPromotion.EndDate = dtpEndDate.Value;
                existingPromotion.IsAutoApply = chkAutoApply.Checked;
                existingPromotion.CashierMessage = txtCashierMessage.Text.Trim();
                existingPromotion.DisplayMessage = txtBillDisplayMessage.Text.Trim();
                existingPromotion.IsMonday = chkMonday.Checked;
                existingPromotion.IsTuesday = chkTuesday.Checked;
                existingPromotion.IsWednesday = chkWednesday.Checked;
                existingPromotion.IsThuresday = chkThuresday.Checked;
                existingPromotion.IsFriday = chkFriday.Checked;
                existingPromotion.IsSaturday = chkSaturday.Checked;
                existingPromotion.IsSunday = chkSunday.Checked;

                existingPromotion.IsMondayTime = chkMondayTime.Checked;
                existingPromotion.IsTuesdayTime = chkTuesdayTime.Checked;
                existingPromotion.IsWednesdayTime = chkWednesdayTime.Checked;
                existingPromotion.IsThuresdayTime = chkThuresdayTime.Checked;
                existingPromotion.IsFridayTime = chkFridayTime.Checked;
                existingPromotion.IsSaturdayTime = chkSaturdayTime.Checked;
                existingPromotion.IsSundayTime = chkSundayTime.Checked;

                existingPromotion.Remark = txtRemark.Text.Trim();
                existingPromotion.IsProvider = chkConsiderProvider.Checked;
                existingPromotion.PaymentMethodID = 1;
                existingPromotion.IsValueRange = chkConsiderValue.Checked;
                existingPromotion.IsAllType = chkAllType.Checked;
                existingPromotion.MondayStartTime = dtpMondayFrom.Value;
                existingPromotion.MondayEndTime = dtpMondayTo.Value;
                existingPromotion.TuesdayStartTime = dtpTuesdayFrom.Value;
                existingPromotion.TuesdayEndTime = dtpTuesdayTo.Value;
                existingPromotion.WednesdayStartTime = dtpWednesdayFrom.Value;
                existingPromotion.WednesdayEndTime = dtpWednesdayTo.Value;
                existingPromotion.ThuresdayStartTime = dtpThuresdayFrom.Value;
                existingPromotion.ThuresdayEndTime = dtpThuresdayTo.Value;
                existingPromotion.FridayStartTime = dtpFridayFrom.Value;
                existingPromotion.FridayEndTime = dtpFridayTo.Value;
                existingPromotion.SaturdayStartTime = dtpSaturdayFrom.Value;
                existingPromotion.SaturdayEndTime = dtpSaturdayTo.Value;
                existingPromotion.SundayStartTime = dtpSundayFrom.Value;
                existingPromotion.SundayEndTime = dtpSundayTo.Value;
                existingPromotion.MinimumValue = Common.ConvertStringToDecimalCurrency(txtMinValue.Text.ToString());
                existingPromotion.MaximumValue = Common.ConvertStringToDecimalCurrency(txtMaxValue.Text.ToString());
                existingPromotion.DiscountPercentage = 0;
                existingPromotion.DiscountValue = 0;
                existingPromotion.Points = 0;
                existingPromotion.IsDelete = false;
                #endregion

                #region Details                

                #endregion

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

       
        public override void Save()
        {
            try
            {
                if (ValidateControls().Equals(false))
                {
                    return;
                }
                InvPromotionMasterService invPromotionMasterService = new InvPromotionMasterService();
                bool isNew = false;
                existingPromotion = invPromotionMasterService.GetPromotionByCode(txtPromotionCode.Text.Trim());

                if (existingPromotion == null || existingPromotion.InvPromotionMasterID == 0)
                {
                    existingPromotion = new InvPromotionMaster();
                    isNew = true;
                }

                FillPromotion();

                int x=0;

                for (int i = 0; i < dgvLocationInfo.RowCount; i++)
                {

                    if (Convert.ToBoolean(dgvLocationInfo.Rows[i].Cells["Selection"].Value) == true)
                    {
                        x = Convert.ToInt32(dgvLocationInfo.Rows[i].Cells["LocationId"].Value);
                    }
                    else
                    {
                        x = 0;
                    }

                    var t = new InvPromotionDetailsAllowLocations()
                    {
                        CompanyID = Common.LoggedCompanyID,
                        LocationID = Common.LoggedLocationID,
                        PromotionLocationID=x
                        
                    };

                    invPromotionAllowLocationList.Add(t);                   
                    
                }


                existingPromotion.InvPromotionDetailsAllowLocations = invPromotionAllowLocationList;
                existingPromotion.InvPromotionDetailsAllowTypes = invPromotionAllowTypeList;

                if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 0)
                {
                    existingPromotion.InvPromotionDetailsBuyXProduct = invPromotionBuyXDetailTempList;
                    existingPromotion.InvPromotionDetailsGetYDetails = invPromotionGetYDetailTempList;
                }
                else if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 1)
                {
                    existingPromotion.InvPromotionDetailsBuyXDepartment = invPromotionBuyXDepartmentDetailTempList;
                    existingPromotion.InvPromotionDetailsGetYDetails = invPromotionGetYDetailTempList;
                }
                else if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 2)
                {
                    existingPromotion.InvPromotionDetailsBuyXCategory = invPromotionBuyXCategoryDetailTempList;
                    existingPromotion.InvPromotionDetailsGetYDetails = invPromotionGetYDetailTempList;
                }
                else if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 3)
                {
                    existingPromotion.InvPromotionDetailsBuyXSubCategory = invPromotionBuyXSubCategoryDetailsTempDetailsList;
                    existingPromotion.InvPromotionDetailsGetYDetails = invPromotionGetYDetailTempList;
                }
                else if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 4)
                {
                    existingPromotion.InvPromotionDetailsProductsDis = invPromotionProductDiscountDetailTempList;
                }
                else if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 5)
                {
                    existingPromotion.InvPromotionDetailsDepartmentDis = invPromotionDepartmentDetailTempList;
                }
                else if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 6)
                {
                    existingPromotion.InvPromotionDetailsCategoryDis = invPromotionCategoryDetailTempList;
                }
                else if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 7)
                {
                    existingPromotion.InvPromotionDetailsSubCategoryDis = invPromotionSubCategoryDetailTempList;
                }
                else if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 8)
                {

                    List<InvPromotionDetailsSubTotal> invPromotionDetailsSubTotalList=new List<InvPromotionDetailsSubTotal>();
                    InvPromotionDetailsSubTotal invPromotionDetailsSubTotal=new Domain.InvPromotionDetailsSubTotal();

                    invPromotionDetailsSubTotal.DiscountPercentage=Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());
                    invPromotionDetailsSubTotal.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountValue.Text.ToString());
                    invPromotionDetailsSubTotal.Points = Common.ConvertStringToDecimalCurrency(txtPoints.Text.ToString());
                    invPromotionDetailsSubTotal.LocationID = existingPromotion.LocationID;
                    invPromotionDetailsSubTotal.CompanyID = existingPromotion.CompanyID;
                    invPromotionDetailsSubTotalList.Add(invPromotionDetailsSubTotal);
                    existingPromotion.InvPromotionDetailsSubTotal = invPromotionDetailsSubTotalList;

                }
                else if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 9)
                {
                    existingPromotion.InvPromotionDetailsBuyXProduct = invPromotionBuyXDetailTempList;
                    existingPromotion.InvPromotionDetailsGetYDetails = invPromotionGetYDetailTempList;
                }

                else if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 10)
                {
                    existingPromotion.InvPromotionDetailsBuyXSubCategory2 = invPromotionBuyXSubCategory2DetailsTempDetailsList;
                    existingPromotion.InvPromotionDetailsGetYDetails = invPromotionGetYDetailTempList;
                }

                else if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 11)
                {
                    existingPromotion.InvPromotionDetailsBuyXSubCategory2 = invPromotionBuyXSubCategory2DetailsTempDetailsList;
                    existingPromotion.InvPromotionDetailsGetYDetails = invPromotionGetYDetailTempList;
                }
                else if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 13)
                {
                    existingPromotion.InvPromotionDetailsSubCategory2Dis = invPromotionSubCategory2DetailTempList;
                }

                if (existingPromotion.InvPromotionMasterID == 0)
                {
                    if ((Toast.Show("Promotion - " + existingPromotion.PromotionCode + " - " + existingPromotion.PromotionName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }

                    invPromotionMasterService.AddPromotion(existingPromotion);

                   
                    //InitializeForm();

                    if ((Toast.Show("Promotion - " + existingPromotion.PromotionCode + " - " + existingPromotion.PromotionName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
                    {
                        ClearForm();
                        btnNew.PerformClick();
                    }


                }
                else
                {
                    if (isNew)
                    {
                        if ((Toast.Show("Promotion - " + existingPromotion.PromotionCode + " - " + existingPromotion.PromotionName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if ((Toast.Show("Promotion - " + existingPromotion.PromotionCode + " - " + existingPromotion.PromotionName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    invPromotionMasterService.UpdatePromotion(existingPromotion);                 

                    Toast.Show("Promotion - " + existingPromotion.PromotionCode + " - " + existingPromotion.PromotionName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                    ClearForm();
                }

                ClearForm();
                txtPromotionCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

       // public List<InvPromotionDetailsBuyXSubCategory> invPromotionSubCategoryBuyXTempDetailsList { get; set; }


        private void txtBuySubCategoryQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtBuySubCategoryQty.Text) > 0) { UpdateBuySubCategoryGrid(existingBuyXSubCategoryDetailTemp); } else { txtBuySubCategoryQty.Focus(); }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPromotionCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtPromotionCode.Text.Trim().Equals(string.Empty))
                        RecallPromotionHeader(true);

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool RecallPromotionHeader(bool isCode)
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;
                InvPromotionMaster invPromotionMaster = new InvPromotionMaster();
                InvPromotionMasterService invPromotionMasterService = new Service.InvPromotionMasterService();
                if (isCode == true)
                {
                    invPromotionMaster = invPromotionMasterService.getPromotionHeaderByPromotionCode(txtPromotionCode.Text.Trim(), Common.LoggedLocationID);

                }
                else
                {
                    invPromotionMaster = invPromotionMasterService.getPromotionHeaderByPromotionName(txtPromotionDescription.Text.Trim(), Common.LoggedLocationID);

                }
                if (invPromotionMaster != null)
                {
                    
                    txtPromotionCode.Text = invPromotionMaster.PromotionCode;
                    txtPromotionDescription.Text = invPromotionMaster.PromotionName;
                    cmbPromotionType.SelectedIndex = invPromotionMaster.PromotionTypeID-1;
                    dtpStartDate.Value = invPromotionMaster.StartDate.Value;
                    dtpEndDate.Value = invPromotionMaster.EndDate.Value;

                    txtRemark.Text = invPromotionMaster.Remark;
                    txtCashierMessage.Text = invPromotionMaster.CashierMessage;
                    txtBillDisplayMessage.Text = invPromotionMaster.DisplayMessage;

                    chkConsiderValue.Checked = invPromotionMaster.IsValueRange;
                    txtMinValue.Text = invPromotionMaster.MinimumValue.ToString();
                    txtMaxValue.Text = invPromotionMaster.MaximumValue.ToString();

                    chkAllType.Checked = invPromotionMaster.IsAllType;
                    chkAllLocations.Checked = invPromotionMaster.IsAllLocations;

                    chkMondayTime.Checked = invPromotionMaster.IsMondayTime;
                    chkTuesdayTime.Checked = invPromotionMaster.IsTuesdayTime;
                    chkWednesdayTime.Checked = invPromotionMaster.IsWednesdayTime;
                    chkThuresdayTime.Checked = invPromotionMaster.IsThuresdayTime;
                    chkFridayTime.Checked = invPromotionMaster.IsFridayTime;
                    chkSaturdayTime.Checked = invPromotionMaster.IsSaturdayTime;
                    chkSundayTime.Checked = invPromotionMaster.IsSundayTime;

                    chkMonday.Checked = invPromotionMaster.IsMonday;
                    chkTuesday.Checked = invPromotionMaster.IsTuesday;
                    chkWednesday.Checked = invPromotionMaster.IsWednesday;
                    chkThuresday.Checked = invPromotionMaster.IsThuresday;
                    chkFriday.Checked = invPromotionMaster.IsFriday;
                    chkSaturday.Checked = invPromotionMaster.IsSaturday;
                    chkSunday.Checked = invPromotionMaster.IsSunday;

                    chkMondayTime.Checked = invPromotionMaster.IsMondayTime;
                    chkTuesdayTime.Checked = invPromotionMaster.IsTuesdayTime;
                    chkWednesdayTime.Checked = invPromotionMaster.IsWednesdayTime;
                    chkThuresdayTime.Checked = invPromotionMaster.IsThuresdayTime;
                    chkFridayTime.Checked = invPromotionMaster.IsFridayTime;
                    chkSaturdayTime.Checked = invPromotionMaster.IsSaturdayTime;
                    chkSundayTime.Checked = invPromotionMaster.IsSundayTime;

                    dtpMondayFrom.Value = invPromotionMaster.MondayStartTime.Value;
                    dtpMondayTo.Value = invPromotionMaster.MondayEndTime.Value;
                    dtpTuesdayFrom.Value = invPromotionMaster.TuesdayStartTime.Value;
                    dtpTuesdayTo.Value = invPromotionMaster.TuesdayEndTime.Value;
                    dtpWednesdayFrom.Value = invPromotionMaster.WednesdayStartTime.Value;
                    dtpWednesdayTo.Value = invPromotionMaster.WednesdayEndTime.Value;
                    dtpThuresdayFrom.Value = invPromotionMaster.ThuresdayStartTime.Value;
                    dtpThuresdayTo.Value = invPromotionMaster.ThuresdayEndTime.Value;                    
                    dtpFridayFrom.Value = invPromotionMaster.FridayStartTime.Value;
                    dtpFridayTo.Value = invPromotionMaster.FridayEndTime.Value;
                    dtpSaturdayFrom.Value = invPromotionMaster.SaturdayStartTime.Value;
                    dtpSaturdayTo.Value = invPromotionMaster.SaturdayEndTime.Value;
                    dtpSundayFrom.Value = invPromotionMaster.SundayStartTime.Value;
                    dtpSundayTo.Value = invPromotionMaster.SundayEndTime.Value;

                    dgvLocationInfo.DataSource = null;
                    //invPromotionAllowLocationList = existingPromotion.InvPromotionDetailsAllowLocations;
                    invPromotionAllowLocationList = invPromotionMasterService.getPromotionDetailLocations(invPromotionMaster, invPromotionAllowLocationList);
                    dgvLocationInfo.DataSource = invPromotionAllowLocationList;
                    dgvLocationInfo.Refresh();

                    if (invPromotionMaster.PromotionTypeID == 1)
                    {
                        dgvBuyDetails.AutoGenerateColumns = false;
                        dgvBuyDetails.DataSource = null;
                        invPromotionBuyXDetailTempList = invPromotionMasterService.getBuyXProductDetail(invPromotionMaster);
                        dgvBuyDetails.DataSource = invPromotionBuyXDetailTempList;
                        dgvBuyDetails.Refresh();

                        dgvGetDetails.DataSource = null;
                        invPromotionGetYDetailTempList = invPromotionMasterService.getGetYProductDetail(invPromotionMaster);
                        dgvGetDetails.DataSource = invPromotionGetYDetailTempList;
                        dgvGetDetails.Refresh();
                    }
                    else if (invPromotionMaster.PromotionTypeID == 2)
                    {
                        dgvBuyDepartment.AutoGenerateColumns = false;
                        dgvBuyDepartment.DataSource = null;
                        invPromotionBuyXDepartmentDetailTempList = invPromotionMasterService.BuyDepartmentDetail(invPromotionMaster);
                        dgvBuyDepartment.DataSource = invPromotionBuyXDepartmentDetailTempList;
                        dgvBuyDepartment.Refresh();

                        dgvGetDetails.DataSource = null;
                        invPromotionGetYDetailTempList = invPromotionMasterService.getGetYProductDetail(invPromotionMaster);
                        dgvGetDetails.DataSource = invPromotionGetYDetailTempList;
                        dgvGetDetails.Refresh();
                    }
                    else if (invPromotionMaster.PromotionTypeID == 3)
                    {
                        dgvBuyCategory.AutoGenerateColumns = false;
                        dgvBuyCategory.DataSource = null;
                        invPromotionBuyXCategoryDetailTempList = invPromotionMasterService.BuyCategoryDetail(invPromotionMaster);
                        dgvBuyCategory.DataSource = invPromotionBuyXCategoryDetailTempList;
                        dgvBuyCategory.Refresh();

                        dgvGetDetails.AutoGenerateColumns = false;
                        dgvGetDetails.DataSource = null;
                        invPromotionGetYDetailTempList = invPromotionMasterService.getGetYProductDetail(invPromotionMaster);
                        dgvGetDetails.DataSource = invPromotionGetYDetailTempList;
                        dgvGetDetails.Refresh();
                    }
                    else if (invPromotionMaster.PromotionTypeID == 4)
                    {
                        dgvBuySubCategory.AutoGenerateColumns = false;
                        dgvBuySubCategory.DataSource = null;
                        invPromotionBuyXSubCategoryDetailsTempDetailsList = invPromotionMasterService.BuySubCategoryDetail(invPromotionMaster);
                        dgvBuySubCategory.DataSource = invPromotionBuyXSubCategoryDetailsTempDetailsList;
                        dgvBuySubCategory.Refresh();

                        dgvGetDetails.AutoGenerateColumns = false;
                        dgvGetDetails.DataSource = null;
                        invPromotionGetYDetailTempList = invPromotionMasterService.getGetYProductDetail(invPromotionMaster);
                        dgvGetDetails.DataSource = invPromotionGetYDetailTempList;
                        dgvGetDetails.Refresh();
                    }
                    else if (invPromotionMaster.PromotionTypeID == 5)
                    {
                        dgvProductDiscount.AutoGenerateColumns = false;
                        dgvProductDiscount.DataSource = null;
                        invPromotionProductDiscountDetailTempList = invPromotionMasterService.getProductDisDetail(invPromotionMaster);
                        dgvProductDiscount.DataSource = invPromotionProductDiscountDetailTempList;
                        dgvProductDiscount.Refresh();
                    }
                    else if (invPromotionMaster.PromotionTypeID == 6)
                    {
                        dgvDepartmentDiscount.AutoGenerateColumns = false;
                        dgvDepartmentDiscount.DataSource = null;
                        invPromotionDepartmentDetailTempList = invPromotionMasterService.getGetDepartmentDisDetail(invPromotionMaster);
                        dgvDepartmentDiscount.DataSource = invPromotionDepartmentDetailTempList;
                        dgvDepartmentDiscount.Refresh();
                    }
                    else if (invPromotionMaster.PromotionTypeID == 7)
                    {
                        dgvCategoryDiscount.AutoGenerateColumns = false;
                        dgvCategoryDiscount.DataSource = null;
                        invPromotionCategoryDetailTempList = invPromotionMasterService.getGetCategoryDisDetail(invPromotionMaster);
                        dgvCategoryDiscount.DataSource = invPromotionCategoryDetailTempList;
                        dgvCategoryDiscount.Refresh();
                    }
                    else if (invPromotionMaster.PromotionTypeID == 8)
                    {
                        dgvSubCategory.AutoGenerateColumns = false;
                        dgvSubCategory.DataSource = null;
                        invPromotionSubCategoryDetailTempList = invPromotionMasterService.getGetSubCategoryDisDetail(invPromotionMaster);
                        dgvSubCategory.DataSource = invPromotionSubCategoryDetailTempList;
                        dgvSubCategory.Refresh();
                    }
                    else if (invPromotionMaster.PromotionTypeID == 9)
                    {
                        InvPromotionDetailsSubTotal invPromotionDetailsSubTotal = new InvPromotionDetailsSubTotal();
                        txtSubTotalDiscountPercentage.Text = invPromotionDetailsSubTotal.DiscountPercentage.ToString();
                        txtSubTotalDiscountValue.Text = invPromotionDetailsSubTotal.DiscountAmount.ToString();
                        txtPoints.Text = invPromotionDetailsSubTotal.Points.ToString();

                    }
                    else if (invPromotionMaster.PromotionTypeID == 10)
                    {
                        dgvBuyDetails.AutoGenerateColumns = false;
                        dgvBuyDetails.DataSource = null;
                        invPromotionBuyXDetailTempList = invPromotionMasterService.getBuyXProductDetail(invPromotionMaster);
                        dgvBuyDetails.DataSource = invPromotionBuyXDetailTempList;
                        dgvBuyDetails.Refresh();

                        dgvGetDetails.DataSource = null;
                        invPromotionGetYDetailTempList = invPromotionMasterService.getGetYProductDetail(invPromotionMaster);
                        dgvGetDetails.DataSource = invPromotionGetYDetailTempList;
                        dgvGetDetails.Refresh();
                    }
                    else if (invPromotionMaster.PromotionTypeID == 11)
                    {
                        dgvBuySubCategory2.AutoGenerateColumns = false;
                        dgvBuySubCategory2.DataSource = null;
                        invPromotionBuyXSubCategory2DetailsTempDetailsList = invPromotionMasterService.BuySubCategory2Detail(invPromotionMaster);
                        dgvBuySubCategory2.DataSource = invPromotionBuyXSubCategory2DetailsTempDetailsList;
                        dgvBuySubCategory2.Refresh();

                        dgvGetDetails.AutoGenerateColumns = false;
                        dgvGetDetails.DataSource = null;
                        invPromotionGetYDetailTempList = invPromotionMasterService.GetSubCategory2Detail(invPromotionMaster);
                        dgvGetDetails.DataSource = invPromotionGetYDetailTempList;
                        dgvGetDetails.Refresh();
                    }
                    else if (invPromotionMaster.PromotionTypeID == 12)
                    {
                        dgvBuySubCategory2.AutoGenerateColumns = false;
                        dgvBuySubCategory2.DataSource = null;
                        invPromotionBuyXSubCategory2DetailsTempDetailsList = invPromotionMasterService.BuySubCategory2Detail(invPromotionMaster);
                        dgvBuySubCategory2.DataSource = invPromotionBuyXSubCategory2DetailsTempDetailsList;
                        dgvBuySubCategory2.Refresh();

                        dgvGetDetails.AutoGenerateColumns = false;
                        dgvGetDetails.DataSource = null;
                        invPromotionGetYDetailTempList = invPromotionMasterService.getGetYProductDetail(invPromotionMaster);
                        dgvGetDetails.DataSource = invPromotionGetYDetailTempList;
                        dgvGetDetails.Refresh();
                    }
                    else if (invPromotionMaster.PromotionTypeID == 14)
                    {
                        dgvSubCategory2.AutoGenerateColumns = false;
                        dgvSubCategory2.DataSource = null;
                        invPromotionSubCategory2DetailTempList = invPromotionMasterService.getGetSubCategory2DisDetail(invPromotionMaster);
                        dgvSubCategory2.DataSource = invPromotionSubCategory2DetailTempList;
                        dgvSubCategory2.Refresh();
                    }

                    Common.EnableTextBox(false, txtPromotionCode, txtPromotionDescription);
                    Common.EnableComboBox(false, cmbPromotionType);
                    Common.EnableButton(true, btnSave);

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

        private void txtMinValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtMaxValue.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPromotionDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    cmbPromotionType.Enabled = true;
                    cmbPromotionType.Focus();
                    cmbPromotionType.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMaxValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtRemark.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void CheckValueRange()
        {
            if (txtMinValue.Text.Trim() != "" && txtMaxValue.Text.Trim() != "")
            {
                if ((Common.ConvertStringToDecimalCurrency(txtMaxValue.Text.Trim())) < (Common.ConvertStringToDecimalCurrency(txtMinValue.Text.Trim())))
                {
                    Toast.Show("Maximum value should be grater than minimum value", Toast.messageType.Information, Toast.messageAction.General);
                    txtMinValue.Focus();
                    return;
                }
            }
        }

        private void txtCashierMessage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtBillDisplayMessage.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMaxValue_Leave(object sender, EventArgs e)
        {
            CheckValueRange();
        }

        private void chkAutoCompleationPromotion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvPromotionMasterService invPromotionMasterService = new InvPromotionMasterService();

                Common.SetAutoComplete(txtPromotionCode, invPromotionMasterService.GetAllInvPromotionCodes(), chkAutoCompleationPromotion.Checked);
                Common.SetAutoComplete(txtPromotionDescription, invPromotionMasterService.GetAllInvPromotionNames(), chkAutoCompleationPromotion.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPromotionDescription_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtPromotionDescription.Text.Trim().Equals(string.Empty))
                    RecallPromotionHeader(false);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationBuySubCategory2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvSubCategory2Service invCategory2Service = new Service.InvSubCategory2Service();
                Common.SetAutoComplete(txtBuySubCategory2Code, invCategory2Service.GetInvSubCategory2Codes(), chkAutoCompleationBuySubCategory2.Checked);
                Common.SetAutoComplete(txtBuySubCategory2Name, invCategory2Service.GetInvSubCategory2Names(), chkAutoCompleationBuySubCategory2.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuySubCategory2Code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtBuySubCategory2Qty.Focus();
                txtBuySubCategory2Qty.SelectionStart = txtBuySubCategory2Qty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuySubCategory2Code_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtBuySubCategory2Code.Text.Trim() != string.Empty)
                {
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    InvSubCategory2 existingInvSubCategory2 = new InvSubCategory2();

                    existingInvSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByCode(txtBuySubCategory2Code.Text.Trim());


                    if (existingInvSubCategory2 != null)
                    {
                        txtBuySubCategory2Name.Text = existingInvSubCategory2.SubCategory2Name;
                        txtBuySubCategory2Qty.Focus();

                    }
                    else
                    {
                        Toast.Show("SubCategory2 " + txtBuySubCategory2Code.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtBuySubCategory2Code);
                        txtBuySubCategory2Code.Focus();
                        isValidControls = false;

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuySubCategory2Name_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtBuySubCategory2Qty.Focus();
                txtBuySubCategory2Qty.SelectionStart = txtBuySubCategory2Qty.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuySubCategory2Name_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtBuySubCategory2Name.Text.Trim() != string.Empty)
                {
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    InvSubCategory2 existingInvSubCategory2 = new InvSubCategory2();

                    existingInvSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByName(txtBuySubCategory2Name.Text.Trim());


                    if (existingInvSubCategory2 != null)
                    {
                        txtBuySubCategory2Code.Text = existingInvSubCategory2.SubCategory2Code;
                        txtBuySubCategory2Qty.Focus();

                    }
                    else
                    {
                        Toast.Show("SubCategory2 " + txtBuySubCategory2Name.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtBuySubCategory2Name);
                        txtBuySubCategory2Name.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBuySubCategory2Qty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtBuySubCategory2Qty.Text) > 0) { UpdateBuySubCategory2Grid(existingBuyXSubCategory2DetailTemp); } else { txtBuySubCategory2Qty.Focus(); }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbPromotionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvProductMasterService invProductMasterService = new InvProductMasterService();
            InvSubCategory2Service invSubCategory2Service = new Service.InvSubCategory2Service();

            if (Common.ConvertStringToInt(cmbPromotionType.SelectedIndex.ToString()) == 10)
            {
                
                Common.SetAutoComplete(txtGetProductCode, invSubCategory2Service.GetInvSubCategory2Codes(), chkAutoCompleationBuySubCategory2.Checked);
                Common.SetAutoComplete(txtGetProductName, invSubCategory2Service.GetInvSubCategory2Names(), chkAutoCompleationBuySubCategory2.Checked);

                cmbGetUnit.Text = "Nos";
                cmbGetUnit.Enabled = false;
                txtGetRate.Text = "0.00";
                txtGetRate.Enabled = false;
                txtGetPoints.Text = "0";
                txtGetPoints.Enabled = false;
            }
            else
            {                
                Common.SetAutoComplete(txtGetProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationGetProduct.Checked);
                Common.SetAutoComplete(txtGetProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationGetProduct.Checked);

                cmbGetUnit.Enabled = true;
                txtGetRate.Enabled = true;
                txtGetPoints.Enabled = true;

            }
        }

        private void ChkSubCategory2Dis_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvSubCategory2Service invCategory2Service = new Service.InvSubCategory2Service();
                Common.SetAutoComplete(txtSubCategory2Code, invCategory2Service.GetInvSubCategory2Codes(), ChkSubCategory2Dis.Checked);
                Common.SetAutoComplete(txtSubCategory2Description, invCategory2Service.GetInvSubCategory2Names(), ChkSubCategory2Dis.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategory2Code_Validated(object sender, EventArgs e)
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
                        txtSubCategory2Description.Text = existingInvSubCategory2.SubCategory2Name;
                        txtSubCategory2Qty.Focus();

                    }
                    else
                    {
                        Toast.Show("SubCategory2 " + txtBuySubCategory2Code.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
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

        private void txtSubCategory2Code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtSubCategory2Qty.Focus();
                txtSubCategory2Qty.SelectionStart = txtSubCategory2Qty.Text.Length;
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
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtSubCategory2Qty.Focus();
                txtSubCategory2Qty.SelectionStart = txtSubCategory2Qty.Text.Length;
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
                        txtSubCategory2Code.Text = existingInvSubCategory2.SubCategory2Code;
                        txtSubCategory2Qty.Focus();

                    }
                    else
                    {
                        Toast.Show("SubCategory2 " + txtSubCategory2Description.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
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

        private void txtSubCategory2Qty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimalQty(txtSubCategory2Qty.Text) > 0) { txtSubCategory2Points.Focus(); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategory2Points_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtSubCategory2DiscPercentage.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategory2DiscPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtSubCategory2DiscPercentage.Text) <= 100)
                    {
                        if (Common.ConvertStringToDecimal(txtSubCategory2DiscPercentage.Text) > 0) { UpdateSubCategory2Grid(existingSubCategory2DetailTemp); } else { txtSubCategory2DiscAmount.Focus(); }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategory2DiscAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimal(txtSubCategory2DiscAmount.Text) > 0) { UpdateSubCategory2Grid(existingSubCategory2DetailTemp); } else { txtSubCategory2DiscAmount.Focus(); }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGetDiscPercentage_Validated(object sender, EventArgs e)
        {
            if (Common.ConvertStringToDecimal(txtGetDiscPercentage.Text) > 100) { txtGetDiscPercentage.Text = "0.00"; txtGetDiscPercentage.Focus(); }

        }

        private void txtProductDiscPercentage_Validated(object sender, EventArgs e)
        {
            if (Common.ConvertStringToDecimal(txtProductDiscPercentage.Text) > 100) { txtProductDiscPercentage.Text = "0.00"; txtProductDiscPercentage.Focus(); }
        }

        private void txtDepartmentDiscPercentage_Validated(object sender, EventArgs e)
        {
            if (Common.ConvertStringToDecimal(txtDepartmentDiscPercentage.Text) > 100) { txtDepartmentDiscPercentage.Text = "0.00"; txtDepartmentDiscPercentage.Focus(); }
        }

        private void txtCategoryDiscPercentage_Validated(object sender, EventArgs e)
        {
            if (Common.ConvertStringToDecimal(txtCategoryDiscPercentage.Text) > 100) { txtCategoryDiscPercentage.Text = "0.00"; txtCategoryDiscPercentage.Focus(); }
        }

        private void txtSubCategoryDiscPercentage_Validated(object sender, EventArgs e)
        {
            if (Common.ConvertStringToDecimal(txtSubCategoryDiscPercentage.Text) > 100) { txtSubCategoryDiscPercentage.Text = "0.00"; txtSubCategoryDiscPercentage.Focus(); }

        }

        private void txtSubCategory2DiscPercentage_Validated(object sender, EventArgs e)
        {
            if (Common.ConvertStringToDecimal(txtSubCategory2DiscPercentage.Text) > 100) { txtSubCategory2DiscPercentage.Text = "0.00"; txtSubCategory2DiscPercentage.Focus(); }

        }

        
    }
}

