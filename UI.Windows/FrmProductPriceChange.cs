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
    public partial class FrmProductPriceChange : FrmBaseTransactionForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        private int tagwz = 0; 
        private InvProductPriceChangeService invProductPriceChangeService;
        private InvProductPriceChangeDetail existingInvProductPriceChange;
        InvProductMaster existingInvProductMaster;
   
        List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();
        List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

        PriceChangeTemp priceChangeTemp = new PriceChangeTemp();
        static List<PriceChangeTemp> PriceChangeTemplst = new List<PriceChangeTemp>();
        static List<PriceChangeTemp> PriceChangeBatchTempList = new List<PriceChangeTemp>();

        bool isInvProduct;
        private long[] arrDept;
        private long[] arrCategory;
        private long[] arrSubCategory; 
        private long[] arrSubCategory2;
        private string[] arrBatchNos;
        private long[] arrLocations;
        private long[] arrLocationIds;
        private long[] arrSupplier;
        int datarow = 0;
        int documentID = 103;
        string documentNo = "";
        static string batchNumber;
        static int  locationId;
        static int DocumentStatus;
        public FrmProductPriceChange()
        {
            InitializeComponent();
        }

        //public FrmProductPriceChange(int PcType)
        //{
        //    InitializeComponent();
        //    DocPcType = PcType;
        //}

        public override void InitializeForm()
        {
            try
            {
               
                Common.EnableTextBox(true, txtProductCode, txtProductName);

                //if (DocPcType != 0)
                //{
                //    dgvItemDetails.Columns[6].Name = "99Code";
                //}

                invProductBatchNoTempList = null;
                invProductSerialNoTempList = null;
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

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                //if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }

                pnlWizard.Location = new System.Drawing.Point(0, 0);
                pnlSubCategorySelection.Location = new System.Drawing.Point(5, 5);
                pnlCategorySelection.Location = new System.Drawing.Point(5, 5);
                pnlDptSelection.Location = new System.Drawing.Point(5, 5);
                //pnlLocationSelection.Location = new System.Drawing.Point(5, 5);
                pnlBatchNos.Location = new System.Drawing.Point(5, 5);
                pnlSubCategory2.Location = new System.Drawing.Point(5, 5);
               
                pnlUpdateCostPrice.Enabled = false;
                pnlUpdateSellingPrice.Enabled = false;


                dgvLocation.AutoGenerateColumns = false;
                dgvLocation.AllowUserToAddRows = false;
                dgvLocation.AllowUserToDeleteRows = false;
                btnFinished.Enabled = false;
                dgvDepartment.AutoGenerateColumns = false;
                dgvDepartment.AllowUserToAddRows = false;
                dgvDepartment.AllowUserToDeleteRows = false;

                dgvCategory.AutoGenerateColumns = false;
                dgvCategory.AllowUserToAddRows = false;
                dgvCategory.AllowUserToDeleteRows = false;

                dgvSubCategory.AutoGenerateColumns = false;
                dgvSubCategory.AllowUserToAddRows = false;
                dgvSubCategory.AllowUserToDeleteRows = false;

                dgvSubCategory2.AutoGenerateColumns = false;
                dgvSubCategory2.AllowUserToAddRows = false;
                dgvSubCategory2.AllowUserToDeleteRows = false;

                dgvItemDetails.AutoGenerateColumns = false;
                dgvItemDetails.AllowUserToAddRows = false;
                dgvItemDetails.AllowUserToDeleteRows = false;

                dgvBatchnos.AutoGenerateColumns = false;
                dgvBatchnos.AllowUserToAddRows = false;
                dgvBatchnos.AllowUserToDeleteRows = false;

                dgvSupplier.AutoGenerateColumns = false;
                dgvSupplier.AllowUserToAddRows = false;
                dgvSupplier.AllowUserToDeleteRows = false;

                //LoadAllLocations();
                //LoadAllDepartments();
                //LoadAllBatchNOs();
                //LoadAllCategorys();
                //LoadAllSubCategorys();
                //LoadAllSubCategory2s();
                LoadProducts();
                //LoadAllSupplier();

                ClearForm();
                base.FormLoad();
                // Load Unit of measures
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                Common.LoadUnitOfMeasures(cmbUnit, unitOfMeasureService.GetAllUnitOfMeasures());

                invProductPriceChangeService = new InvProductPriceChangeService();
                Common.SetAutoComplete(txtDocumentNo, invProductPriceChangeService.GetPausedDocumentNumbers(), chkAutoCompleationPoNo.Checked);
                cmbUnit.SelectedIndex = -1;
                tabCritaria.TabPages.Remove(tpBatchNos);
                tabCritaria.TabPages.Remove(tpCategory);
                tabCritaria.TabPages.Remove(tpDepartment);
                tabCritaria.TabPages.Remove(tpSubCategory);
                tabCritaria.TabPages.Remove(tpSubCategory2);
                tabCritaria.TabPages.Remove(tpSupplier);
                tabCritaria.SelectedTab = tpCritaria;

                this.ActiveControl = txtProductCode;
                txtProductCode.Focus();
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        # region Load Grids ----

        private void LoadAllLocations()
        {
            try
            {
                LocationService locationService = new LocationService();
                List<Location> locations = new List<Location>();

                locations = locationService.GetAllLocations();
                dgvLocation.DataSource = locations;
                dgvLocation.Refresh();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        private void LoadAllDepartments()
        {
            try
            {


                InvDepartmentService invDepartmentService = new InvDepartmentService();
                List<InvDepartment> departments = new List<InvDepartment>();

                departments = invDepartmentService.GetAllActiveInvDepartments(false);
                dgvDepartment.DataSource = departments;
                dgvDepartment.Refresh();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        private void LoadAllCategorys()
        {
            try
            {


                InvCategoryService categoryService = new InvCategoryService();
                List<InvCategory> categories = new List<InvCategory>();

                categories = categoryService.GetAllActiveInvCategories(false);
                dgvCategory.DataSource = categories;
                dgvCategory.Refresh();

                //if (dgvDepartment.Rows.Count > 0)
                //{
                //    arrDept = new long[dgvDepartment.Rows.Count];
                //    int x = 0;
                //    foreach (DataGridViewRow row in dgvDepartment.Rows)
                //    {
                //        if (dgvDepartment["DepartmentId", row.Index].Value != null &&
                //            !dgvDepartment["DepartmentId", row.Index].Value.ToString().Trim().Equals(string.Empty))
                //        {
                //            if (dgvDepartment["DepartmentAllow", row.Index].Value != null)
                //            {
                //                if (dgvDepartment["DepartmentAllow", row.Index].Value != null ||
                //                    Common.ConvertStringToBool(
                //                        dgvDepartment["DepartmentAllow", row.Index].Value.ToString()).Equals(true))
                //                {
                //                    var varCat = (
                //                                     categoryService.GetInvCategoryByDepartment(
                //                                         Common.ConvertStringToLong(
                //                                             dgvDepartment["DepartmentId", row.Index].Value.ToString())));
                //                    if (varCat != null)
                //                    {
                //                        categories.Add(varCat);

                //                    }
                //                    arrDept[x] = Common.ConvertStringToLong(
                //                        dgvDepartment["DepartmentId", row.Index].Value.ToString());
                //                    x++;
                //                }
                //            }
                //        }
                //    }
                //}

 
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadAllSubCategorys()
        {
            try
            {

                InvSubCategoryService subCategoryService = new InvSubCategoryService();
                List<InvSubCategory> subCategories = new List<InvSubCategory>();

                subCategories = subCategoryService.GetAllInvSubCategories(false);
 
                //if (dgvCategory.Rows.Count > 0)
                //{
                //    arrCategory = new long[dgvCategory.Rows.Count];
                //    int x = 0;
                //    foreach (DataGridViewRow row in dgvCategory.Rows)
                //    {
                //        if (dgvCategory["CategoryId", row.Index].Value != null &&
                //            !dgvCategory["CategoryId", row.Index].Value.ToString().Trim().Equals(string.Empty))
                //        {
                //            if (dgvCategory["CategoryAllow", row.Index].Value != null)
                //            {
                //                if (dgvCategory["CategoryAllow", row.Index].Value != null ||
                //                    Common.ConvertStringToBool(dgvCategory["CategoryAllow", row.Index].Value.ToString())
                //                        .Equals(true))
                //                {
                //                    var varSubCat = (subCategoryService.GetInvSubCategoryByCategory(
                //                        Common.ConvertStringToLong(dgvCategory["CategoryId", row.Index].Value.ToString())));
                //                    if (varSubCat != null)
                //                    {
                //                        subCategories.Add(varSubCat);
                //                    }
                //                    arrCategory[x] =
                //                        Common.ConvertStringToLong(dgvCategory["CategoryId", row.Index].Value.ToString());
                //                    x++;
                //                }
                //            }
                //        }
                //    }
                //}

                dgvSubCategory.DataSource = subCategories;
                dgvSubCategory.Refresh();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadAllSubCategory2s()
        {
            try
            {


                InvSubCategory2Service subCategory2Service = new InvSubCategory2Service();
                List<InvSubCategory2> subCategory2s = new List<InvSubCategory2>();

                subCategory2s = subCategory2Service.GetAllInvSubCategories2();

                //if (dgvSubCategory.Rows.Count > 0)
                //{
                //    arrSubCategory = new long[dgvSubCategory.Rows.Count];
                //    int x = 0;
                //    foreach (DataGridViewRow row in dgvSubCategory.Rows)
                //    {
                //        if (dgvSubCategory["InvSubCategoryId", row.Index].Value != null &&
                //            !dgvSubCategory["InvSubCategoryId", row.Index].Value.ToString().Trim().Equals(string.Empty))
                //        {
                //            if (dgvSubCategory["subCategoryAllow", row.Index].Value != null)
                //            {
                //                if (dgvSubCategory["subCategoryAllow", row.Index].Value != null ||
                //                    Common.ConvertStringToBool(
                //                        dgvSubCategory["subCategoryAllow", row.Index].Value.ToString()).Equals(true))
                //                {
                //                    var varSubCat2 =
                //                        (subCategory2Service.GetInvSubCategory2ByCategory(Common.ConvertStringToLong(
                //                            dgvSubCategory["InvSubCategoryId", row.Index].Value.ToString())));

                //                    if (varSubCat2 != null)
                //                    {
                //                        subCategory2s.Add(varSubCat2);
                //                    }

                //                    arrSubCategory[x] =
                //                        Common.ConvertStringToLong(
                //                            dgvSubCategory["InvSubCategoryId", row.Index].Value.ToString());
                //                    x++;
                //                }
                //            }
                //        }
                //    }
                //}


                subCategory2s = subCategory2Service.GetAllInvSubCategories2();
                dgvSubCategory2.DataSource = subCategory2s;
                dgvSubCategory2.Refresh();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        private void LoadAllBatchNOs()
        {
            try
            {


                invProductPriceChangeService = new InvProductPriceChangeService();
                List<InvProductBatchNoExpiaryDetail> batchNoes = new List<InvProductBatchNoExpiaryDetail>();

                //if (dgvSubCategory2.Rows.Count > 0)
                //{
                //    arrSubCategory2 = new long[dgvSubCategory2.Rows.Count];
                //    int x = 0;
                //    foreach (DataGridViewRow row in dgvSubCategory2.Rows)
                //    {
                //        if (dgvSubCategory2["invSubCategory2Id", row.Index].Value != null &&
                //            !dgvSubCategory2["invSubCategory2Id", row.Index].Value.ToString().Trim().Equals(string.Empty))
                //        {
                //            if (dgvSubCategory2["SubCategory2Allow", row.Index].Value != null)
                //            {
                //                if (dgvSubCategory2["SubCategory2Allow", row.Index].Value != null ||
                //                    Common.ConvertStringToBool(
                //                        dgvSubCategory2["SubCategory2Allow", row.Index].Value.ToString()).Equals(true))
                //                {
                //                    arrSubCategory2[x] = Common
                //                        .ConvertStringToLong(
                //                            dgvSubCategory2["invSubCategory2Id", row.Index].Value.ToString());
                //                    x++;
                //                }
                //            }
                //        }
                //    }
                //}

                batchNoes = invProductPriceChangeService.GetAllBatchNos();
                dgvBatchnos.DataSource = batchNoes;
                dgvBatchnos.Refresh();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadAllSupplier()
        {
            try
            {
                SupplierService supplierService = new SupplierService();
                List<Supplier> supplies = new List<Supplier>();

                supplies = supplierService.GetAllSuppliers();
                dgvSupplier.DataSource = supplies;
                dgvSupplier.Refresh();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        #endregion

        #region xxxx

        private void btnBack_Click(object sender, EventArgs e)
        {
            tagwz = tagwz - 1;

            switch (tagwz)
            {
                case 0:
                    {
                        pnlSubCategorySelection.Visible = false;
                        pnlCategorySelection.Visible = false;
                        pnlDptSelection.Visible = false;
                        pnlLocationSelection.Visible = true;
                        pnlBatchNos.Visible = false;
                        pnlSubCategory2.Visible = false;
                        btnBack.Enabled = false;
                        pnlBatchNos.Visible = false;
                        btnFinished.Enabled = false;
                        btnNext.Enabled = true;
                        break;
                    }
                case 1:
                        pnlSubCategorySelection.Visible = false;
                        pnlCategorySelection.Visible = false;
                        pnlDptSelection.Visible = true;
                        pnlLocationSelection.Visible = false;
                        pnlSubCategory2.Visible = false;
                        pnlBatchNos.Visible = false;
                        btnBack.Enabled = true;
                        btnFinished.Enabled = false;
                        btnNext.Enabled = true;
                        break;
                case 2:
                    {
                        pnlSubCategorySelection.Visible = false;
                        pnlCategorySelection.Visible = true;
                        pnlDptSelection.Visible = false;
                        pnlLocationSelection.Visible = false;
                        pnlSubCategory2.Visible = false;
                        pnlBatchNos.Visible = false;
                        btnBack.Enabled = true;
                        btnFinished.Enabled = false;
                        btnNext.Enabled = true;
                        break;
                    }
                case 3:
                    {
                        pnlSubCategorySelection.Visible = true;
                        pnlCategorySelection.Visible = false;
                        pnlDptSelection.Visible = false;
                        pnlLocationSelection.Visible = false;
                        pnlSubCategory2.Visible = false;
                        pnlBatchNos.Visible = false;
                        btnBack.Enabled = true;
                        btnFinished.Enabled = false;
                        btnNext.Enabled = true;
                        break;
                    }
                case 4:
                    {
                        pnlSubCategorySelection.Visible = false;
                        pnlCategorySelection.Visible = false;
                        pnlDptSelection.Visible = false;
                        pnlLocationSelection.Visible = false;
                        pnlSubCategory2.Visible = true;
                        pnlBatchNos.Visible = false;
                        btnBack.Enabled = true;
                        btnFinished.Enabled = false;
                        btnNext.Enabled = true;
                        break;
                    }
                case 5:
                    {
                        pnlSubCategorySelection.Visible = false;
                        pnlCategorySelection.Visible = false;
                        pnlDptSelection.Visible = false;
                        pnlLocationSelection.Visible = false;
                        pnlSubCategory2.Visible = false;
                        pnlBatchNos.Visible = true;
                        btnBack.Enabled = true;
                        btnFinished.Enabled = true;
                        btnNext.Enabled = false;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {

          

            tagwz = tagwz + 1;

            switch (tagwz)
            {
                case 0:
                    {
                        pnlSubCategorySelection.Visible = false;
                        pnlCategorySelection.Visible = false;
                        pnlDptSelection.Visible = false;
                        pnlLocationSelection.Visible = true;
                        pnlBatchNos.Visible = false;
                        pnlSubCategory2.Visible = false;
                        btnBack.Enabled = false;
                        pnlBatchNos.Visible = false;
                        btnFinished.Enabled = false;
                        btnNext.Enabled = true;
                        break;
                    }
                case 1:
                    pnlSubCategorySelection.Visible = false;
                    pnlCategorySelection.Visible = false;
                    pnlDptSelection.Visible = true;
                    pnlLocationSelection.Visible = false;
                    pnlSubCategory2.Visible = false;
                    pnlBatchNos.Visible = false;
                    btnBack.Enabled = true;
                    btnFinished.Enabled = false;
                    btnNext.Enabled = true;
                    break;
                case 2:
                    {
                        pnlSubCategorySelection.Visible = false;
                        pnlCategorySelection.Visible = true;
                        pnlDptSelection.Visible = false;
                        pnlLocationSelection.Visible = false;
                        pnlSubCategory2.Visible = false;
                        pnlBatchNos.Visible = false;
                        btnBack.Enabled = true;
                        btnFinished.Enabled = false;
                        btnNext.Enabled = true;
                        LoadAllCategorys();
                        break;
                    }
                case 3:
                    {
                        pnlSubCategorySelection.Visible = true;
                        pnlCategorySelection.Visible = false;
                        pnlDptSelection.Visible = false;
                        pnlLocationSelection.Visible = false;
                        pnlSubCategory2.Visible = false;
                        pnlBatchNos.Visible = false;
                        btnBack.Enabled = true;
                        btnFinished.Enabled = false;
                        btnNext.Enabled = true;
                        LoadAllSubCategorys();
                        break;
                    }
                case 4:
                    {
                        pnlSubCategorySelection.Visible = false;
                        pnlCategorySelection.Visible = false;
                        pnlDptSelection.Visible = false;
                        pnlLocationSelection.Visible = false;
                        pnlSubCategory2.Visible = true;
                        pnlBatchNos.Visible = false;
                        btnBack.Enabled = true;
                        btnFinished.Enabled = false;
                        btnNext.Enabled = true;
                        LoadAllSubCategory2s();
                        break;
                    }
                case 5:
                    {
                        pnlSubCategorySelection.Visible = false;
                        pnlCategorySelection.Visible = false;
                        pnlDptSelection.Visible = false;
                        pnlLocationSelection.Visible = false;
                        pnlSubCategory2.Visible = false;
                        pnlBatchNos.Visible = true;
                        btnBack.Enabled = true;
                        btnFinished.Enabled = true;
                        btnNext.Enabled = false;
                        LoadAllBatchNOs();
                        break;
                    }
                default:
                    {
                        break;
                    }
                }
            }
                  catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
     

        }

        #endregion

        private void btnFinished_Click(object sender, EventArgs e)
        {
            try
            {
                pnlWizard.Visible = true;
                pnlWizard.Left = 450;
                pnlWizard.Top = 180;
                dgvItemDetails.DataSource = null;
                tabCritaria.SelectedTab = tpCritaria;
                btnFinished.Enabled = false;
 
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void UpdateDiscount(bool IsSelling, bool isincrement)
        {
            try
            {

                bool isSubDiscount;
                decimal subDiscount = 0;

               


                if(IsSelling==true)
                {
                    isSubDiscount = chkSellingPercentage.Checked;
                    if (isincrement == false && isSubDiscount == true && Common.ConvertStringToDecimalCurrency(txtSellingDiscount.Text.ToString())>=100)
                    {
                        Toast.Show("Cannot Apply This Discount Value..", Toast.messageType.Error, Toast.messageAction.Invalid, "");
                        return;
                    }


                    subDiscount=Common.ConvertStringToDecimalCurrency(txtSellingDiscount.Text.ToString());
                    if (isincrement == false)
                    {
                        if (dgvItemDetails.RowCount > 0)
                        {
                            for (int i = 0; i < dgvItemDetails.RowCount; i++)
                            {
                                if (subDiscount > Common.ConvertStringToDecimal(dgvItemDetails["SellingPrice", i].Value.ToString()))
                                {
                                    Toast.Show("Cannot Apply This Discount Value..", Toast.messageType.Error, Toast.messageAction.Invalid, "");
                                    return;
                                }
                            }
                        }
                    }

                   
                }
                else
                {
                    isSubDiscount = chkCostPercentage.Checked;
                    if (isincrement == false && isSubDiscount == true && Common.ConvertStringToDecimalCurrency(txtCostDiscount.Text.ToString()) >= 100)
                    {
                        Toast.Show("Cannot Apply This Discount Value..", Toast.messageType.Error, Toast.messageAction.Invalid, "");
                        return;
                    }
                    subDiscount= Common.ConvertStringToDecimalCurrency(txtCostDiscount.Text.ToString());

                    if (isincrement == false)
                    {
                        if (dgvItemDetails.RowCount > 0)
                        {
                            if (dgvItemDetails.RowCount > 0)
                            {
                                for (int i = 0; i < dgvItemDetails.RowCount; i++)
                                {
                                    if (subDiscount > Common.ConvertStringToDecimal(dgvItemDetails["CostPrice", i].Value.ToString()))
                                    {
                                        Toast.Show("Cannot Apply This Discount Value..", Toast.messageType.Error, Toast.messageAction.Invalid, "");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }

                decimal discountAmount = 0;

               

                invProductPriceChangeService = new InvProductPriceChangeService();
                dgvItemDetails.DataSource = null;

                PriceChangeTemplst = invProductPriceChangeService.getUpdatePriceDiscount(PriceChangeTemplst, Common.ConvertStringToDecimal(txtCostDiscount.Text.Trim()), Common.ConvertStringToDecimal(txtSellingDiscount.Text.Trim()), isSubDiscount,isincrement);   // (chkSellingPercentage.Checked ? true : false)
                dgvItemDetails.AutoGenerateColumns = false;
                dgvItemDetails.DataSource = PriceChangeTemplst;
                dgvItemDetails.Refresh();




            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void loadItemGrid()
        {
            try
            {
                InvProductMasterService productMasterService = new InvProductMasterService();
                List<InvProductMaster> productMasters = new List<InvProductMaster>();



                loadArrDtl();
                PriceChangeTemplst = new List<PriceChangeTemp>();
                PriceChangeTemplst = productMasterService.GetAllProductRelatedToBatchNoList(arrDept, arrCategory, arrSubCategory, arrSubCategory2, arrBatchNos, arrLocations,arrSupplier);

                dgvItemDetails.DataSource = PriceChangeTemplst;
                dgvItemDetails.Refresh();

                tabCritaria.SelectedTab = tpCritaria;


            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void loadArrDtl()
        {

            #region department
            if (dgvDepartment.Rows.Count > 0)
            {
                arrDept = new long[dgvDepartment.Rows.Count];
                int x = 0;
                foreach (DataGridViewRow row in dgvDepartment.Rows)
                {
                    if (dgvDepartment["DepartmentId", row.Index].Value != null &&
                        !dgvDepartment["DepartmentId", row.Index].Value.ToString().Trim().Equals(string.Empty))
                    {
                        if (dgvDepartment["DepartmentAllow", row.Index].Value != null)
                        {
                            if (dgvDepartment["DepartmentAllow", row.Index].Value != null &&
                                Common.ConvertStringToBool(
                                    dgvDepartment["DepartmentAllow", row.Index].Value.ToString()).Equals(true))
                            {
                                arrDept[x] = Common.ConvertStringToLong(
                                    dgvDepartment["DepartmentId", row.Index].Value.ToString());
                                x++;
                            }
                        }
                    }
                }
            }
            #endregion

            #region batchno
                if (dgvBatchnos.Rows.Count > 0)
                {
                    arrBatchNos = new string[dgvBatchnos.Rows.Count];
                    int x = 0;
                    foreach (DataGridViewRow row in dgvBatchnos.Rows)
                    {
                        if (dgvBatchnos["BatchNos", row.Index].Value != null &&
                            !dgvBatchnos["BatchNos", row.Index].Value.ToString().Trim().Equals(string.Empty))
                        {
                            if (dgvBatchnos["BatchNoAllow", row.Index].Value != null)
                            {
                                if (dgvBatchnos["BatchNoAllow", row.Index].Value != null &&
                                    Common.ConvertStringToBool(dgvBatchnos["BatchNoAllow", row.Index].Value.ToString()).
                                        Equals(true))
                                {
                                    arrBatchNos[x] = dgvBatchnos["BatchNos", row.Index].Value.ToString();
                                    x++;
                                }
                            }
                        }
                    }
                }
            #endregion 

            #region subcategory2
                if (dgvSubCategory2.Rows.Count > 0)
                {
                    arrSubCategory2 = new long[dgvSubCategory2.Rows.Count];
                    int x = 0;
                    foreach (DataGridViewRow row in dgvSubCategory2.Rows)
                    {
                        if (dgvSubCategory2["invSubCategory2Id", row.Index].Value != null &&
                            !dgvSubCategory2["invSubCategory2Id", row.Index].Value.ToString().Trim().Equals(string.Empty))
                        {
                            if (dgvSubCategory2["SubCategory2Allow", row.Index].Value != null)
                            {
                                if (dgvSubCategory2["SubCategory2Allow", row.Index].Value != null &&
                                    Common.ConvertStringToBool(
                                        dgvSubCategory2["SubCategory2Allow", row.Index].Value.ToString()).Equals(true))
                                {
                                    arrSubCategory2[x] = Common
                                        .ConvertStringToLong(
                                            dgvSubCategory2["invSubCategory2Id", row.Index].Value.ToString());
                                    x++;
                                }
                            }
                        }
                    }
                }
            #endregion

            #region subcategory
               
                if (dgvSubCategory.Rows.Count > 0)
                {
                    arrSubCategory = new long[dgvSubCategory.Rows.Count];
                    int x = 0;
                    foreach (DataGridViewRow row in dgvSubCategory.Rows)
                    {
                        if (dgvSubCategory["InvSubCategoryId", row.Index].Value != null &&
                            !dgvSubCategory["InvSubCategoryId", row.Index].Value.ToString().Trim().Equals(string.Empty))
                        {
                            if (dgvSubCategory["subCategoryAllow", row.Index].Value != null)
                            {
                                if (dgvSubCategory["subCategoryAllow", row.Index].Value != null &&
                                    Common.ConvertStringToBool(
                                        dgvSubCategory["subCategoryAllow", row.Index].Value.ToString()).Equals(true))
                                {
                                    arrSubCategory[x] =
                                        Common.ConvertStringToLong(
                                            dgvSubCategory["InvSubCategoryId", row.Index].Value.ToString());
                                    x++;
                                }
                            }
                        }
                    }
                }
            #endregion

            #region Category

            if (dgvCategory.Rows.Count > 0)
                {
                    arrCategory = new long[dgvCategory.Rows.Count];
                    int x = 0;
                    foreach (DataGridViewRow row in dgvCategory.Rows)
                    {
                        if (dgvCategory["CategoryId", row.Index].Value != null &&
                            !dgvCategory["CategoryId", row.Index].Value.ToString().Trim().Equals(string.Empty))
                        {
                            if (dgvCategory["CategoryAllow", row.Index].Value != null)
                            {
                                if (dgvCategory["CategoryAllow", row.Index].Value != null &&
                                    Common.ConvertStringToBool(dgvCategory["CategoryAllow", row.Index].Value.ToString())
                                        .Equals(true))
                                {
                                        arrCategory[x] =
                                        Common.ConvertStringToLong(dgvCategory["CategoryId", row.Index].Value.ToString());
                                    x++;
                                }
                            }
                        }
                    }
                }
            #endregion

            #region Locaton

            if (dgvLocation.Rows.Count > 0)
            {
                arrLocations = new long[dgvLocation.Rows.Count];
                int x = 0;
                foreach (DataGridViewRow row in dgvLocation.Rows)
                {
                    if (dgvLocation["LocationIDy", row.Index].Value != null &&
                        !dgvLocation["LocationIDy", row.Index].Value.ToString().Trim().Equals(string.Empty))
                    {
                        if (dgvLocation["Allow", row.Index].Value != null)
                        {
                            if (dgvLocation["Allow", row.Index].Value != null &&
                                Common.ConvertStringToBool(dgvLocation["Allow", row.Index].Value.ToString())
                                    .Equals(true))
                            {
                                arrLocations[x] =
                                Common.ConvertStringToLong(dgvLocation["LocationIDy", row.Index].Value.ToString());
                                x++;
                            }
                        }
                    }
                }
            }
            #endregion

            #region Supplier

            if (dgvSupplier.Rows.Count > 0)
            {
                arrSupplier = new long[dgvSupplier.Rows.Count];
                int x = 0;
                foreach (DataGridViewRow row in dgvSupplier.Rows)
                {
                    if (dgvSupplier["SupplierId", row.Index].Value != null &&
                        !dgvSupplier["SupplierId", row.Index].Value.ToString().Trim().Equals(string.Empty))
                    {
                        if (dgvSupplier["supAllow", row.Index].Value != null)
                        {
                            if (dgvSupplier["supAllow", row.Index].Value != null &&
                                Common.ConvertStringToBool(dgvSupplier["supAllow", row.Index].Value.ToString())
                                    .Equals(true))
                            {
                                arrSupplier[x] =
                                Common.ConvertStringToLong(dgvSupplier["SupplierId", row.Index].Value.ToString());
                                x++;
                            }
                        }
                    }
                }
            }
            #endregion
        }

        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            if (!txtSellingDiscount.Text.Trim().Equals(string.Empty) && txtSellingDiscount.Text.Trim() != null)
            {
                UpdateDiscount(true, (chkIsIncrementSelling.Checked ? true : false));
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
                    Toast.Show("Price Change  -  " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 0);
                    RefreshDocumentNumbers();
                    ClearForm();
                }
                else
                {
                    Toast.Show("Price Change  -  " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        private void RefreshDocumentNumbers()
        {
            ////Load PO Document Numbers
            InvProductPriceChangeService invProductPriceChangeService = new InvProductPriceChangeService();
            Common.SetAutoComplete(txtDocumentNo, invProductPriceChangeService.GetPausedDocumentNumbers(), chkAutoCompleationPoNo.Checked);
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
                invProductPriceChangeService = new InvProductPriceChangeService();
 
                InvProductPriceChangeHeader invProductPriceChangeHeader = new InvProductPriceChangeHeader();
                invProductPriceChangeHeader = invProductPriceChangeService.GetPausedInvProductPriceChangeHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim());
                if (invProductPriceChangeHeader == null)
                {
                    invProductPriceChangeHeader = new InvProductPriceChangeHeader();
                }
 
                invProductPriceChangeHeader.CompanyID = Common.LoggedCompanyID;
                invProductPriceChangeHeader.DocumentDate = Common.FormatDate(DateTime.Now);
                invProductPriceChangeHeader.EffectFromDate = Common.FormatDate(dtpEffectivDate.Value);
                invProductPriceChangeHeader.DocumentID = documentID;
                invProductPriceChangeHeader.DocumentStatus = documentStatus;
                invProductPriceChangeHeader.DocumentNo = documentNo.Trim();
                invProductPriceChangeHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                invProductPriceChangeHeader.LocationID = Common.LoggedLocationID;
                invProductPriceChangeHeader.ReferenceNo = txtReferenceNo.Text.Trim();
                invProductPriceChangeHeader.IsUpLoad = chkTStatus.Checked;
                return invProductPriceChangeService.Save(invProductPriceChangeHeader, PriceChangeTemplst, out newDocumentNo, this.Name);

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

        public override void View()
        {

            base.View();
            GenerateReport(txtDocumentNo.Text.Trim(), DocumentStatus);
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
                        Toast.Show("Price Change  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                        GenerateReport(NewDocumentNo.Trim(), 1);
                        RefreshDocumentNumbers();
                        ClearForm();
                    }
                    else
                    {
                        Toast.Show("Price Change  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        loadProductDetails(true, dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim(), Common.ConvertStringToLong(dgvItemDetails["UOMID", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()), Common.ConvertStringToInt(dgvItemDetails["LocationIdx", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()), Common.ConvertStringToInt(dgvItemDetails["LineNo", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()));

                        Common.EnableTextBox(true, txtNewCostPrice, txtNewSellingPrice);
                        Common.EnableTextBox(false, txtProductCode, txtProductName, txtBatchNo);
                        Common.EnableComboBox(false, cmbUnit);
                        txtNewCostPrice.Focus();

                      
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
                    invProductPriceChangeService = new InvProductPriceChangeService();
                    if (PriceChangeTemplst == null)
                        PriceChangeTemplst = new List<PriceChangeTemp>();
                    priceChangeTemp = invProductPriceChangeService.getPriceChangeTemp(PriceChangeTemplst, existingInvProductMaster, locationId, unitofMeasureID);
                    PriceChangeBatchTempList = invProductPriceChangeService.getPriceChangeTempList(PriceChangeTemplst, existingInvProductMaster, locationId, unitofMeasureID,lineNo);
                    if (priceChangeTemp != null)
                    {
                        txtProductCode.Text = priceChangeTemp.ProductCode;
                        txtProductName.Text = priceChangeTemp.ProductName;
                        cmbUnit.SelectedValue = priceChangeTemp.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(priceChangeTemp.CostPrice);
                        txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(priceChangeTemp.SellingPrice);
                        txtQty.Text = Common.ConvertDecimalToStringQty(priceChangeTemp.Qty);
                        txtBatchNo.Text = priceChangeTemp.BatchNo;
                        txtLoca.Text = priceChangeTemp.LocationName;
                        txtMrp.Text = Common.ConvertDecimalToStringCurrency(priceChangeTemp.MRP);
                        datarow = Convert.ToInt16(priceChangeTemp.LineNo);
                       


                        
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

        private void loadProductDetails(bool isCode, string strProduct, long unitofMeasureID, int locationId)
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
                    invProductPriceChangeService = new InvProductPriceChangeService();
                    if (PriceChangeTemplst == null)
                        PriceChangeTemplst = new List<PriceChangeTemp>();
                    priceChangeTemp = invProductPriceChangeService.getPriceChangeTemp(PriceChangeTemplst, existingInvProductMaster, locationId, unitofMeasureID);
                    PriceChangeBatchTempList = invProductPriceChangeService.getPriceChangeTempList(PriceChangeTemplst, existingInvProductMaster, locationId, unitofMeasureID);
                    if (priceChangeTemp != null)
                    {
                        txtProductCode.Text = priceChangeTemp.ProductCode;
                        txtProductName.Text = priceChangeTemp.ProductName;
                        cmbUnit.SelectedValue = priceChangeTemp.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(priceChangeTemp.CostPrice);
                        txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(priceChangeTemp.SellingPrice);
                        txtQty.Text = Common.ConvertDecimalToStringQty(priceChangeTemp.Qty);
                        txtBatchNo.Text = priceChangeTemp.BatchNo;
                        txtLoca.Text = priceChangeTemp.LocationName;
                        txtMrp.Text = Common.ConvertDecimalToStringCurrency(priceChangeTemp.MRP);
                        datarow = Convert.ToInt16(priceChangeTemp.LineNo);




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
                PriceChangeTemplst.Clear();
                DocumentStatus = 0;
                PriceChangeBatchTempList.Clear();
                Common.ClearTextBox(txtProductCode, txtProductName, txtBatchNo, txtNewCostPrice, txtNewSellingPrice, txtCostPrice, txtSellingPrice, txtSellingDiscount, txtCostDiscount);
                Common.EnableTextBox(false, txtQty, txtSellingPrice, txtCostPrice, txtBatchNo, txtBatchNo, txtNewCostPrice, txtNewSellingPrice,txtMrp);
                Common.EnableTextBox(true, txtProductCode, txtProductName);
                Common.EnableComboBox(false, cmbUnit);
                invProductPriceChangeService = new InvProductPriceChangeService();
                Common.SetAutoComplete(txtDocumentNo, invProductPriceChangeService.GetPausedDocumentNumbers(), chkAutoCompleationPoNo.Checked);
                chkPriceDecreas.Enabled = true;

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
                    Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodes(), true);
                //}
                //else
                //{
                //    Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodes(), true);
                //    Common.SetAutoComplete(txtBatchNo, invProductMasterService.GetAllProductCodes(DocPcType, 10), true);
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
            if(chkUpdateSellingPrice.Checked==true)
            {
                pnlUpdateSellingPrice.Enabled = true;
            }
            else
            {
                pnlUpdateSellingPrice.Enabled = false;
            }
        }

        private void chkUpdateCostPrice_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUpdateCostPrice.Checked == true)
            {
                pnlUpdateCostPrice.Enabled = true;
            }
            else
            {
                pnlUpdateCostPrice.Enabled = false;
            }
        }

        private void btnUpdateCostPrice_Click(object sender, EventArgs e)
        {
            if (!txtCostDiscount.Text.Trim().Equals(string.Empty) && txtCostDiscount.Text.Trim() != null)
            {
                UpdateDiscount(false, (chkIsIncrementCost.Checked ? true : false));
            }
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

        private void chkCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCategory.Checked)
                    CheckedGrdCheckBox(dgvCategory,"CategoryAllow",true);
            else 
                    CheckedGrdCheckBox(dgvCategory,"CategoryAllow",false);

        }
        private void CheckedGrdCheckBox(DataGridView c, string checkstr, bool status)
        {
            for (int i = 0; i < c.RowCount; i++)
            {
                c.Rows[i].Cells[checkstr].Value = status;
            }
                 
           
        }
 

        private void chkSubCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubCategory.Checked)
                    CheckedGrdCheckBox(dgvSubCategory,"SubCategoryAllow",true);
            else 
                CheckedGrdCheckBox(dgvSubCategory, "SubCategoryAllow", false);
            
        }

        private void chkSubCategory2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubCategory2.Checked)
                CheckedGrdCheckBox(dgvSubCategory2, "SubCategory2Allow", true);
            else  
                    CheckedGrdCheckBox(dgvSubCategory2,"SubCategory2Allow",false);
            
        }

        private void chkBatchNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBatchNo.Checked)
                    CheckedGrdCheckBox(dgvBatchnos,"BatchNoAllow",true);
            else 
                CheckedGrdCheckBox(dgvBatchnos, "BatchNoAllow", false);
            
        }

        private void chkDepartment_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDepartment.Checked)
                    CheckedGrdCheckBox(dgvDepartment,"DepartmentAllow",true);
            else 
                CheckedGrdCheckBox(dgvDepartment, "DepartmentAllow", false);
        }

        private void txtNewCostPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {

                if (chkPriceDecreas.Checked == false)
                {
                    if (Common.ConvertStringToDecimal(txtCostPrice.Text) > Common.ConvertStringToDecimal(txtNewCostPrice.Text))
                    {
                        Toast.Show("Invalid Cost Price. Shoud be Greater Than Old Cost Price", Toast.messageType.Warning, Toast.messageAction.Invalid, "Price Change");
                        txtNewCostPrice.Focus();
                        txtNewCostPrice.SelectAll();
                        
                        return;
                    }
                }
                else
                {
                    if (Common.ConvertStringToDecimal(txtCostPrice.Text) < Common.ConvertStringToDecimal(txtNewCostPrice.Text))
                    {
                        Toast.Show("Invalid Cost Price. Shoud be Less Than Old Cost Price", Toast.messageType.Warning, Toast.messageAction.Invalid, "Price Change");
                        txtNewCostPrice.Focus();
                        txtNewCostPrice.SelectAll();
                        
                        return;
                    }

                }

                

                UpdateToGrid(priceChangeTemp);
                Common.ClearCheckBox(chkUpdateCostPrice, chkUpdateSellingPrice);
                Common.ClearTextBox(txtProductCode, txtProductName, txtQty, txtSellingPrice, txtBatchNo, txtNewSellingPrice, txtNewCostPrice, txtCostPrice, txtMrp);
                Common.ClearComboBox(cmbUnit);
                Common.EnableTextBox(false, txtQty, txtSellingPrice, txtCostPrice, txtBatchNo, txtNewCostPrice, txtNewSellingPrice, txtMrp);
                Common.EnableTextBox(true, txtProductCode, txtProductName);
                txtProductCode.Focus();
            }
            
        }

        private void txtNewSellingPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Return))
                {
                    if (chkPriceDecreas.Checked == false)
                    {
                        if (Common.ConvertStringToDecimal(txtSellingPrice.Text) > Common.ConvertStringToDecimal(txtNewSellingPrice.Text))
                        {
                            Toast.Show("Invalid Selling Price. Shoud be Greater Than Old Selling Price", Toast.messageType.Warning, Toast.messageAction.Invalid, "Price Change");
                            txtNewSellingPrice.Focus();
                            txtNewSellingPrice.SelectAll();
                            return;
                        }
                    }
                    else
                    {
                        if (Common.ConvertStringToDecimal(txtSellingPrice.Text) < Common.ConvertStringToDecimal(txtNewSellingPrice.Text))
                        {
                            Toast.Show("Invalid Selling Price. Shoud be Less Than Old Selling Price", Toast.messageType.Warning, Toast.messageAction.Invalid, "Price Change");
                            txtNewSellingPrice.Focus();
                            txtNewSellingPrice.SelectAll();
                            return;
                        }
                    }
                    UpdateToGrid(priceChangeTemp);
                    Common.ClearCheckBox(chkUpdateCostPrice, chkUpdateSellingPrice);
                    Common.ClearTextBox(txtProductCode, txtProductName, txtQty, txtSellingPrice, txtBatchNo, txtNewSellingPrice, txtNewCostPrice, txtCostPrice, txtMrp);
                    Common.ClearComboBox(cmbUnit);
                    Common.EnableTextBox(false, txtQty, txtSellingPrice, txtCostPrice, txtBatchNo, txtNewCostPrice, txtNewSellingPrice, txtMrp);
                    Common.EnableTextBox(true, txtProductCode, txtProductName);
                    txtProductCode.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void UpdateToGrid(PriceChangeTemp priceChangeTemp)
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


                    

                    
                    invProductPriceChangeService = new InvProductPriceChangeService();
                    dgvItemDetails.DataSource = null;
                    
                    PriceChangeTemplst = invProductPriceChangeService.getUpdatePriceChangeTemp( PriceChangeTemplst,PriceChangeBatchTempList,Common.ConvertStringToDecimal(txtNewCostPrice.Text.Trim()),Common.ConvertStringToDecimal(txtNewSellingPrice.Text.Trim()));
                    dgvItemDetails.AutoGenerateColumns = false;
                    dgvItemDetails.DataSource = PriceChangeTemplst;
                    dgvItemDetails.Refresh();


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
                        //existingProBatchNo = invProductPriceChangeService.GetProductBatchNo(existingInvProductMaster.InvProductMasterID);
                        //txtBatchNo.Text = existingProBatchNo.BatchNo.Trim();
                        //txtLocation.Text = existingInvProductMaster.Location.LocationName;
                        //txtCostPrice.Text = existingProBatchNo.CostPrice.ToString();
                        //txtSellingPrice.Text = existingProBatchNo.SellingPrice.ToString();
                        priceChangeTemp = new PriceChangeTemp();
                        Common.EnableComboBox(true, cmbUnit);
                        chkPriceDecreas.Enabled = false;
                        //cmbUnit.SelectedIndex = -1;
                        cmbUnit.Focus();
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
                    txtBatchNo.Enabled = true;
                    txtBatchNo.Focus();
                }
                else
                {
                    txtBatchNo.Enabled = true;
                    txtBatchNo.Focus();
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
                    //frmProductPriceChange.SetBatchNoList(PriceChangeTempList);




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
        public void SetBatchNoList(List<PriceChangeTemp> setPriceChangeTempList)
        {
            PriceChangeBatchTempList = null;
            PriceChangeBatchTempList = setPriceChangeTempList;
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

        private void txtBatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    //if (DocPcType == 0)
                    //{
                        batchNumber = null;
                        isInvProduct = true;
                        InvProductBatchNoTemp invProductBatchNoTemp = new InvProductBatchNoTemp();
                        InvProductMaster invProductMaster = new InvProductMaster();

                        LocationService locationService = new LocationService();
                        Location location = new Location();
                        invProductBatchNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                        invProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        priceChangeTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                        priceChangeTemp.ProductCode = existingInvProductMaster.ProductCode;
                        priceChangeTemp.ProductName = existingInvProductMaster.ProductName;
                        priceChangeTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                        priceChangeTemp.MRP = existingInvProductMaster.MaximumPrice;
                        txtMrp.Text = existingInvProductMaster.MaximumPrice.ToString();
                        UnitOfMeasure unitOfMeasure = new UnitOfMeasure();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        unitOfMeasure = unitOfMeasureService.GetUnitOfMeasureById(priceChangeTemp.UnitOfMeasureID);
                        priceChangeTemp.UnitOfMeasureName = unitOfMeasure.UnitOfMeasureName.ToString();


                        if (!txtProductName.Text.Trim().Equals(string.Empty))
                        {
                            CommonService commonService = new CommonService();
                            if (txtBatchNo.Text.Trim() != "")
                            {

                                txtCostPrice.Text = priceChangeTemp.CostPrice.ToString();
                                txtNewCostPrice.Text = priceChangeTemp.CostPrice.ToString();
                                txtSellingPrice.Text = priceChangeTemp.SellingPrice.ToString();
                                txtQty.Text = priceChangeTemp.Qty.ToString();
                                txtLoca.Text = priceChangeTemp.LocationId.ToString();
                                txtMrp.Text = priceChangeTemp.MRP.ToString();

                                Common.EnableTextBox(true, txtNewSellingPrice);
                                txtNewSellingPrice.Text = invProductMaster.SellingPrice.ToString();
                                txtNewSellingPrice.Focus();
                                txtNewSellingPrice.SelectAll();

                            }
                            else
                            {

                                invProductPriceChangeService = new InvProductPriceChangeService();
                                invProductBatchNoTempList = invProductPriceChangeService.getBatchNoDetailPriceChange(existingInvProductMaster, priceChangeTemp.UnitOfMeasureID);


                                if (invProductSerialNoTempList == null)
                                    invProductSerialNoTempList = new List<InvProductSerialNoTemp>();


                                InvProductMasterService invProductMasterService = new InvProductMasterService();

                                //FrmBatchNumberLocation frmBatchNumberLocation = new FrmBatchNumberLocation(invProductBatchNoTempList, invProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim(), isInvProduct, FrmBatchNumberLocation.transactionType.PriceChange, existingInvProductMaster.InvProductMasterID);
                                //frmBatchNumberLocation.ShowDialog();
                                FrmBatchNumberSelection frmBatchNumberSelection = new FrmBatchNumberSelection(invProductBatchNoTempList, invProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim(), isInvProduct, FrmBatchNumberSelection.transactionType.PriceChange, existingInvProductMaster.InvProductMasterID);
                                frmBatchNumberSelection.ShowDialog();

                                invProductMaster = new InvProductMaster();


                                txtBatchNo.Text = batchNumber;
                                commonService = new CommonService();

                                
                                invProductMaster = commonService.CheckInvProductCode(existingInvProductMaster.InvProductMasterID, existingInvProductMaster.UnitOfMeasureID);

                                if (PriceChangeBatchTempList == null || PriceChangeBatchTempList.Count == 0)
                                {
                                    if ((Toast.Show("This Batch No Not exists!", Toast.messageType.Error, Toast.messageAction.General).Equals(DialogResult.OK)))
                                        Common.ClearTextBox(txtProductCode, txtProductName, txtBatchNo, txtMrp, txtNewCostPrice, txtCostPrice, txtNewSellingPrice, txtSellingPrice);
                                    txtProductCode.Focus();
                                    return;
                                }
                                else
                                {
                                    txtCostPrice.Text = invProductMaster.CostPrice.ToString();
                                    txtSellingPrice.Text = invProductMaster.SellingPrice.ToString();
                                    priceChangeTemp.SellingPrice = invProductMaster.SellingPrice;
                                    priceChangeTemp.CostPrice = invProductMaster.CostPrice;
                                    txtBatchNo.Enabled = false;
                                    Common.EnableTextBox(true, txtNewSellingPrice);
                                    txtNewSellingPrice.Text = invProductMaster.SellingPrice.ToString();
                                    txtNewCostPrice.Text = invProductMaster.CostPrice.ToString();
                                    txtNewSellingPrice.Focus();
                                    txtNewSellingPrice.SelectAll();

                                }
                            }
                        }

                    }
                    //else
                    //{

                    //    batchNumber = null;
                    //    isInvProduct = true;
                    //    InvProductBatchNoTemp invProductBatchNoTemp = new InvProductBatchNoTemp();
                    //    InvProductMaster invProductMaster = new InvProductMaster();

                    //    LocationService locationService = new LocationService();
                    //    Location location = new Location();
                    //    invProductBatchNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                    //    invProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                    //    priceChangeTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                    //    priceChangeTemp.ProductCode = existingInvProductMaster.ProductCode;
                    //    priceChangeTemp.ProductName = existingInvProductMaster.ProductName;
                    //    priceChangeTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                    //    priceChangeTemp.MRP = existingInvProductMaster.MaximumPrice;
                    //    txtMrp.Text = existingInvProductMaster.MaximumPrice.ToString();
                    //    txtCostPrice.Text = existingInvProductMaster.CostPrice.ToString();
                    //    txtSellingPrice.Text = existingInvProductMaster.SellingPrice.ToString();
                    //    UnitOfMeasure unitOfMeasure = new UnitOfMeasure();
                    //    UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                    //    unitOfMeasure = unitOfMeasureService.GetUnitOfMeasureById(priceChangeTemp.UnitOfMeasureID);
                    //    priceChangeTemp.UnitOfMeasureName = unitOfMeasure.UnitOfMeasureName.ToString();


                    //    if (!txtProductName.Text.Trim().Equals(string.Empty))
                    //    {
                    //        CommonService commonService = new CommonService();
                    //        if (txtBatchNo.Text.Trim() != "")
                    //        {

                    //            invProductPriceChangeService = new InvProductPriceChangeService();
                    //            invProductBatchNoTempList = invProductPriceChangeService.getBatchNoDetailPriceChange(existingInvProductMaster, priceChangeTemp.UnitOfMeasureID);


                    //            txtQty.Text = priceChangeTemp.Qty.ToString();
                    //            txtLoca.Text = priceChangeTemp.LocationId.ToString();
                    //            txtMrp.Text = priceChangeTemp.MRP.ToString();

                    //            Common.EnableTextBox(true, txtNewSellingPrice);
                    //            txtNewSellingPrice.Text = invProductMaster.SellingPrice.ToString();
                    //            txtNewSellingPrice.Focus();
                    //            txtNewSellingPrice.SelectAll();

                    //        }
                    //        else
                    //        {


                    //            invProductMaster = new InvProductMaster();


                    //            txtBatchNo.Text = batchNumber;
                    //            commonService = new CommonService();

                    //            invProductMaster = commonService.CheckInvProductCode(existingInvProductMaster.InvProductMasterID, existingInvProductMaster.UnitOfMeasureID);

                    //            if (PriceChangeBatchTempList == null || PriceChangeBatchTempList.Count == 0)
                    //            {
                    //                if ((Toast.Show("This Batch No Not exists!", Toast.messageType.Error, Toast.messageAction.General).Equals(DialogResult.OK)))
                    //                    Common.ClearTextBox(txtProductCode, txtProductName, txtBatchNo, txtMrp, txtNewCostPrice, txtCostPrice, txtNewSellingPrice, txtSellingPrice);
                    //                txtProductCode.Focus();
                    //                return;
                    //            }
                    //            else
                    //            {
                    //                txtCostPrice.Text = invProductMaster.CostPrice.ToString();
                    //                txtSellingPrice.Text = invProductMaster.SellingPrice.ToString();
                    //                priceChangeTemp.SellingPrice = invProductMaster.SellingPrice;
                    //                priceChangeTemp.CostPrice = invProductMaster.CostPrice;
                    //                txtBatchNo.Enabled = false;
                    //                Common.EnableTextBox(true, txtNewSellingPrice);
                    //                txtNewSellingPrice.Text = invProductMaster.SellingPrice.ToString();
                    //                txtNewCostPrice.Text = invProductMaster.CostPrice.ToString();
                    //                txtNewSellingPrice.Focus();
                    //                txtNewSellingPrice.SelectAll();

                    //            }
                    //        }
                    //    }


                    //}
                //}
               
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                txtProductCode_Leave(this, e);
            }
        }

        private void btnLoadItemOk_Click(object sender, EventArgs e)
        {
            try
            {
                pnlWizard.Visible = false;
                loadItemGrid();
                CheckedGrdCheckBox(dgvSupplier, "supAllow", false);
                CheckedGrdCheckBox(dgvLocation, "Allow", false);
                CheckedGrdCheckBox(dgvDepartment, "DepartmentAllow", false);
                CheckedGrdCheckBox(dgvBatchnos, "BatchNoAllow", false);
                CheckedGrdCheckBox(dgvSubCategory2, "SubCategory2Allow", false);
                CheckedGrdCheckBox(dgvSubCategory, "SubCategoryAllow", false);
                CheckedGrdCheckBox(dgvCategory, "CategoryAllow", false);
                chkAllSupplier.Checked = false;
                chkAllLocation.Checked = false;
                chkCategory.Checked = false;
                chkSubCategory.Checked = false;
                chkSubCategory2.Checked = false;
                chkDepartment.Checked = false;

                Common.EnableTextBox(false, txtQty, txtSellingPrice, txtCostPrice, txtBatchNo, txtMrp, txtNewCostPrice, txtNewSellingPrice);
                Common.EnableTextBox(true, txtProductCode, txtProductName);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllLocation.Checked)
                CheckedGrdCheckBox(dgvLocation, "Allow", true);
            else
                CheckedGrdCheckBox(dgvLocation, "Allow", false);
        }

        private void chkAllSupplier_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllSupplier.Checked)
                CheckedGrdCheckBox(dgvSupplier, "supAllow", true);
            else
                CheckedGrdCheckBox(dgvSupplier, "supAllow", false);
        }

        private void chkIsIncrementSelling_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsIncrementSelling.Checked == true) chkIsIncrementSelling.Text = "Increment";
            else chkIsIncrementSelling.Text = "Decrement";
        }

        private void chkIsIncrementCost_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsIncrementCost.Checked == true) chkIsIncrementCost.Text = "Increment";
            else chkIsIncrementCost.Text = "Decrement";
        }

        private void tabCritaria_Click(object sender, EventArgs e)
        {
            if (tabCritaria.SelectedTab == tpCritaria)
            {
                btnFinished.Enabled = false;
            }
            else
            {
                btnFinished.Enabled = true;
            }

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
                RecallDocument(txtDocumentNo.Text.Trim());
            }
            else
            {
                txtDocumentNo.Text = GetDocumentNo(true);
                dtpEffectivDate.Focus();
            }
        }

         private string GetDocumentNo(bool isTemporytNo)
        {
            try
            {
                InvProductPriceChangeService invProductPriceChangeService = new InvProductPriceChangeService();
                LocationService locationService = new LocationService();
                return invProductPriceChangeService.GetDocumentNo(this.Name, Common.LoggedLocationID, locationService.GetLocationsByID(Common.LoggedLocationID).LocationCode, documentID, isTemporytNo).Trim();
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
                 InvProductPriceChangeService invProductPriceChangeService = new InvProductPriceChangeService();
                 existingInvProductPriceChange = new InvProductPriceChangeDetail();

                 InvProductPriceChangeHeader invProductPriceChangeHeader = new InvProductPriceChangeHeader();
                 invProductPriceChangeHeader = invProductPriceChangeService.GetPausedInvProductPriceChangeHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim());

                 if (invProductPriceChangeHeader.DocumentStatus == 1)
                 {
                     Common.EnableButton(false, btnSave, btnPause);
                     dtpEffectivDate.Enabled = false;
                     dgvItemDetails.Enabled = false;
                 }
                 else
                 {
                     Common.EnableButton(true, btnSave, btnPause);
                     dgvItemDetails.Enabled = true;
                 }

                 dtpEffectivDate.Value = invProductPriceChangeHeader.EffectFromDate;
                 txtReferenceNo.Text = invProductPriceChangeHeader.ReferenceNo;
                 DocumentStatus = invProductPriceChangeHeader.DocumentStatus;
                 PriceChangeTemplst = invProductPriceChangeService.GetSavedDocumentDetailsByDocumentNumber(documentNo.Trim());
                 dgvItemDetails.AutoGenerateColumns = false;
                 dgvItemDetails.DataSource = PriceChangeTemplst;
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
            invProductPriceChangeService = new InvProductPriceChangeService();
            Common.SetAutoComplete(txtDocumentNo, invProductPriceChangeService.GetPausedDocumentNumbers(), chkAutoCompleationPoNo.Checked);
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

        private void btnRecalEffective_Click(object sender, EventArgs e)
        {
            invProductPriceChangeService = new InvProductPriceChangeService();
            Common.SetAutoComplete(txtRecallDocNo, invProductPriceChangeService.GetPausedDocumentNumbers(Common.ConvertDateTimeToDate(dtpEffectivDate.Value)), chkAutoCompleationPoNo.Checked);
            txtRecallDocNo.Focus();

        }

        private void txtRecallDocNo_Leave(object sender, EventArgs e)
        {
            if (!txtRecallDocNo.Text.Trim().Equals(string.Empty))
            {
                RecallDocument(txtRecallDocNo.Text.Trim());
                txtDocumentNo.Text = txtRecallDocNo.Text.Trim();
            }
            else
            {
                Logger.WriteLog("Document No Not Found..", MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.Event, Common.LoggedLocationID);
            }
        }

        private void txtRecallDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtRecallDocNo_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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

                        PriceChangeTemp priceChangeTempDamage = new PriceChangeTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        InvProductMasterService invProductMasterService = new InvProductMasterService();

                        priceChangeTempDamage.ProductID = invProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).InvProductMasterID;
                        existingInvProductMaster = invProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim());
                        priceChangeTempDamage.UnitOfMeasureID = Common.ConvertStringToLong(dgvItemDetails["UOMID", dgvItemDetails.CurrentCell.RowIndex].Value.ToString());
                        priceChangeTempDamage.LineNo = Common.ConvertStringToLong(dgvItemDetails["LineNo", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim());
                        InvProductPriceChangeDamageService invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();


                        dgvItemDetails.DataSource = null;
                        PriceChangeTemplst = invProductPriceChangeService.GetDeletePriceChangeTemp(PriceChangeTemplst, priceChangeTemp);
                        dgvItemDetails.DataSource = PriceChangeTemplst;
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

        
        }
 
    }
 
