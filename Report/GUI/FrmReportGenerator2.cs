using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Windows;
using Utility;
using System.Reflection;
using Domain;
using Service;
using Report.Inventory.Transactions.Reports;

namespace Report.GUI
{
    public partial class FrmReportGenerator2 : FrmBaseReportsForm
    {
        ///// <summary>
        ///// Store location prefixes and their Columns in table InvTmpReportDetail
        ///// </summary>
        //private enum reportHeaderLocations
        //{
        //    ST = 01, // ST-Location prefix, 01-Table Column (Qty01/Value01)
        //    ML = 02,
        //    NG = 03,
        //    MG = 04,
        //    DG = 05,
        //    HP = 06,
        //    WW = 07,
        //    KY = 08,
        //    MP = 09,
        //    KG = 10,
        //    NB = 11,
        //    NL = 12,
        //    BR = 13,
        //    DL = 14,
        //    RT = 15,
        //    RP = 16,
        //    WP = 17,
        //    CP = 18,
        //    KL = 19,
        //    YP = 20,
        //    KC = 21,
        //    PD = 22,
        //    KK = 23,
        //    ON = 24,
        //    OG = 25
        //    //= 26,
        //    //= 27,
        //    //= 28,
        //    //= 29,
        //    //= 30
        //}

        //AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();

        Color selectedColor = Color.LightSteelBlue;
        Color defaultColor = SystemColors.Control;

        List<Common.CheckedListBoxSelection> locationList = new List<Common.CheckedListBoxSelection>();
        string[] locationNames;

        string[] productNames;
        string[] productCodes;

        List<Common.CheckedListBoxSelection> departmentList = new List<Common.CheckedListBoxSelection>();
        string[] departmentNames; 

        List<Common.CheckedListBoxSelection> categoryList = new List<Common.CheckedListBoxSelection>();
        string[] categoryNames;

        List<Common.CheckedListBoxSelection> subCategoryList = new List<Common.CheckedListBoxSelection>();
        string[] subCategoryNames; 

        List<Common.CheckedListBoxSelection> subCategory2List = new List<Common.CheckedListBoxSelection>();
        string[] subCategory2Names;

        List<Common.CheckedListBoxSelection> productExtendedPropertiesList = new List<Common.CheckedListBoxSelection>();

        List<Common.CheckedListBoxSelection> suppliersList = new List<Common.CheckedListBoxSelection>();
        string[] supplierNames;

        List<Common.CheckedListBoxSelection> customersList = new List<Common.CheckedListBoxSelection>();
        string[] customerNames;

        public FrmReportGenerator2()
        {
            InitializeComponent();
        }

        public override void InitializeForm()
        {
            try
            {
                InitializeSelectionLists();
                OrganiseFormControls();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void InitializeSelectionLists()
        {

            //Locations
            LocationService locationService = new LocationService();
            locationList = CreateSelectionList(locationService.GetAllInventoryLocationNames());
            locationNames = locationService.GetAllInventoryLocationNames();

            InvProductMasterService invProductMasterService = new InvProductMasterService();
            productNames = invProductMasterService.GetAllProductNames();
            productCodes = invProductMasterService.GetAllProductCodes();

            // Departments
            InvDepartmentService invDepartmentService = new InvDepartmentService();
            departmentList = CreateSelectionList(invDepartmentService.GetAllDepartmentNames());
            departmentNames = invDepartmentService.GetAllDepartmentNames();    

            // Categories
            InvCategoryService invCategoryService = new InvCategoryService();
            categoryList = CreateSelectionList(invCategoryService.GetAllCategoryNames());
            categoryNames = invCategoryService.GetAllCategoryNames();     

            // Sub Categories
            InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
            subCategoryList = CreateSelectionList(invSubCategoryService.GetAllSubCategoryNames());
            subCategoryNames = invSubCategoryService.GetAllSubCategoryNames();     

            // Sub Categories2
            InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
            subCategory2List = CreateSelectionList(invSubCategory2Service.GetInvSubCategory2Names());
            subCategory2Names = invSubCategory2Service.GetInvSubCategory2Names();

            //Product Extended Properties
            InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
            productExtendedPropertiesList = CreateSelectionList(invProductExtendedPropertyService.GetInvProductExtendedPropertyNames());
            
            // Suppliers
            SupplierService supplierService = new SupplierService();
            suppliersList = CreateSelectionList(supplierService.GetSupplierNames());
            supplierNames = supplierService.GetSupplierNames();

            // Customers
            CustomerService customerService = new CustomerService();
            customersList = CreateSelectionList(customerService.GetAllCustomerNames());
            customerNames = customerService.GetAllCustomerNames();


        }

        private void OrganiseFormControls()
        {

            // Apply Product Property Types
            
            grpDepartment.Text = "     " + (AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText);
            grpCategory.Text = "     " + (AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText);
            grpSubCategory.Text = "     " + (AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText);
            grpSubCategory2.Text = "     " + (AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText);

            // Location
            Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstLocations, locationList, "", "");
            
            // Product

            // Department
            Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstDepartment, departmentList, "", "");

            // Category
            Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstCategory, categoryList, "", "");
            
            // Sub Category
            Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstSubCategory, subCategoryList, "", "");
            
            // Sub Category 2
            Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstSubCategory2, subCategory2List, "", "");
            
            //Product Extended Properties
            Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstProductExtendedProperties, productExtendedPropertiesList, "", "");

            //ActiveControl = ;
            //.Focus();
        }

        private void LoadAutoList(TextBox sourceTextBox, bool status,string[] autoCompleteList)
        {
            Common.SetAutoComplete(sourceTextBox, autoCompleteList, status);
        }

        //private void LoadProductsAuto()
        //{
        //    Common.SetAutoComplete(txtProductFrom, productNames, chkAutoComplteProductFrom.Checked);
        //    Common.SetAutoComplete(txtProductTo, productNames, chkAutoComplteProductTo.Checked);
        //}

        //private void LoadDepartmentsAuto()
        //{
        //    Common.SetAutoComplete(txtDepartmentFrom, departmentNames, chkAutoComplteDepartmentFrom.Checked);
        //    Common.SetAutoComplete(txtDepartmentTo, departmentNames, chkAutoComplteDepartmentTo.Checked);
        //}

        //private void LoadCategoriesAuto()
        //{
        //    Common.SetAutoComplete(txtCategoryFrom, categoryNames, chkAutoComplteCategoryFrom.Checked);
        //    Common.SetAutoComplete(txtCategoryTo, categoryNames, chkAutoComplteCategoryTo.Checked);
        //}

        //private void LoadSubCategoriesAuto()
        //{
        //    Common.SetAutoComplete(txtSubCategoryFrom, subCategoryNames, chkAutoComplteSubCategoryFrom.Checked);
        //    Common.SetAutoComplete(txtSubCategoryTo, subCategoryNames, chkAutoComplteSubCategoryTo.Checked);
        //}

        //private void LoadSubCategories2Auto()
        //{
        //    Common.SetAutoComplete(txtSubCategory2From, subCategory2Names, chkAutoComplteSubCategory2From.Checked);
        //    Common.SetAutoComplete(txtSubCategory2To, subCategory2Names, chkAutoComplteSubCategory2To.Checked);
        //}

        private List<Common.CheckedListBoxSelection> CreateSelectionList(string[] inArray)
        {
            List<Common.CheckedListBoxSelection> selectionDataStruct = new List<Common.CheckedListBoxSelection>();

            foreach (var item in inArray)
            {
                selectionDataStruct.Add(new Common.CheckedListBoxSelection() { Value = item.ToString().Trim(), isChecked = CheckState.Unchecked });
            }
            return selectionDataStruct;
        }

        private void SetItemCheckedStatus(List<Common.CheckedListBoxSelection> allValuesList, string checkedListBoxItem, CheckState checkState)
        {
            foreach (var item in allValuesList.Where(v => v.Value == checkedListBoxItem.Trim()))
            {
                item.isChecked = checkState;
            }
        }

        private List<Common.CheckedListBoxSelection> SearchList(List<Common.CheckedListBoxSelection> inList, string searchString)
        {
            return inList.Where(c => c.Value.ToLower().StartsWith(searchString.ToLower().Trim())).ToList();
        }

        private void RefreshCheckedList(CheckedListBox checkedListBox, List<Common.CheckedListBoxSelection> allValuesList)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                if (allValuesList.Any(a => a.Value.Equals(checkedListBox.Items[i].ToString().Trim()) && a.isChecked.Equals(CheckState.Checked)))
                {
                    checkedListBox.SetItemChecked(i, true);
                }
                else
                {
                    checkedListBox.SetItemChecked(i, false);
                }
            }
        }

        private string GetProductCodeFrom()
        {
            string productCodeFrom;
            if (string.Equals(txtProductCodeFrom.Text.Trim(), "0"))
            {
                productCodeFrom = "0";
                return productCodeFrom;
            }

            InvProductMaster invProductMasterFrom = new InvProductMaster();
            if (!string.IsNullOrEmpty(txtProductCodeFrom.Text.Trim()))
            {
                invProductMasterFrom = GetInvProductMasterByProductCode(txtProductCodeFrom.Text.Trim());
                if (invProductMasterFrom == null)
                {
                    productCodeFrom = string.Empty;
                }
                else
                {
                    productCodeFrom = invProductMasterFrom.ProductCode.Trim();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtProductNameFrom.Text.Trim()))
                {
                    invProductMasterFrom = GetInvProductMasterByProductName(txtProductNameFrom.Text.Trim());
                    if (invProductMasterFrom == null)
                    {
                        productCodeFrom = string.Empty;
                    }
                    else
                    {
                        productCodeFrom = invProductMasterFrom.ProductCode.Trim();
                    }
                }
                else
                {
                    productCodeFrom = string.Empty;
                }
            }
            return productCodeFrom;
        }

        private string GetProductCodeTo()
        {
            string productCodeTo;
            if (string.Equals(txtProductCodeTo.Text.Trim().ToLower(), "z"))
            {
                productCodeTo = "z";
                return productCodeTo;
            }
            InvProductMaster invProductMasterTo = new InvProductMaster();
            if (!string.IsNullOrEmpty(txtProductCodeTo.Text.Trim()))
            {
                invProductMasterTo = GetInvProductMasterByProductCode(txtProductCodeTo.Text.Trim());
                if (invProductMasterTo == null)
                {
                    productCodeTo = string.Empty;
                }
                else
                {
                    productCodeTo = invProductMasterTo.ProductCode.Trim();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtProductNameTo.Text.Trim()))
                {
                    invProductMasterTo = GetInvProductMasterByProductName(txtProductNameTo.Text.Trim());
                    if (invProductMasterTo == null)
                    {
                        productCodeTo = string.Empty;
                    }
                    else
                    {
                        productCodeTo = invProductMasterTo.ProductCode.Trim();
                    }
                }
                else
                {
                    productCodeTo = string.Empty;
                }
            }
            return productCodeTo;
        }

        private string GetReport()
        {
            if (rbtnPurchases.Checked)
            { return "PU"; }
            if (rbtnSales.Checked)
            { return "SL"; }
            if (rbtnStock.Checked)
            { return "ST"; }
            return string.Empty;
        }

        private string GetReportType()
        {
            if (rbtnSupplier.Checked)
            { return "SUP"; }
            if (rbtnCustomer.Checked)
            { return "CUS"; }
            return string.Empty;
        }

        private string GetDisplayType()
        {
            if (rbtnDetails.Checked)
            { return "DTL"; }
            if (rbtnSummery.Checked)
            { return "SMR"; }
            return null;
        }

        private bool IsAmount()
        {
            if (rbtnAmount.Checked)
            { return true; }
            else
            { return false; }
        }

        private bool IsQuantity()
        {
            if (rbtnQuantity.Checked)
            { return true; }
            else
            { return false; }
        }

        private bool IsCoatValue()
        {
            if (rbtnCostValue.Checked)
            { return true; }
            else
            { return false; }
        }

        private bool IsSaleValue()
        {
            if (rbtnSaleValue.Checked)
            { return true; }
            else
            { return false; }
        }

        private bool isValidStringRangeValues(string fromValue, string toValue)
        {
            if (string.Compare(fromValue.Trim(), toValue.Trim()).Equals(1))
            { return false; }
            else
            { return true; }
        }

        private DataTable GetLocations()
        {
            DataTable dtLocations = new DataTable();
            dtLocations.Columns.Add("LocationID", typeof(int));

            if (rbtnLocationSelection.Checked)
            {
                if (locationList.Any(l => l.isChecked.Equals(CheckState.Checked)))
                {
                    LocationService locationService = new LocationService();
                    foreach (var item in locationList.Where(l => l.isChecked.Equals(CheckState.Checked)))
                    {
                        dtLocations.Rows.Add(locationService.GetLocationsByName(item.Value.Trim()).LocationID);
                    }
                }
            }

            if (rbtnLocationRange.Checked)
            {
                LocationService locationService = new LocationService();
                int[] locationIds = locationService.GetInventoryLocationsIdRangeByLocationNames(txtLocationFrom.Text.Trim(), txtLocationTo.Text.Trim());

                foreach (var item in locationIds)
                {
                    dtLocations.Rows.Add(item);
                }
            }
            DataView dvLocations = dtLocations.DefaultView;
            dvLocations.Sort = "LocationID ASC";
            return dvLocations.ToTable();
        }

        private DataTable GetDepartments()
        {
            DataTable dtDepartments = new DataTable();
            dtDepartments.Columns.Add("InvDepartmentID", typeof(long));
            if (chkDepartment.Checked)
            {
                if (rbtnDepartmentSelection.Checked)
                {
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    bool isDepend = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").IsDepend;

                    if (departmentList.Any(l => l.isChecked.Equals(CheckState.Checked)))
                    {
                        foreach (var item in departmentList.Where(l => l.isChecked.Equals(CheckState.Checked)))
                        {
                            dtDepartments.Rows.Add(invDepartmentService.GetInvDepartmentsByName(item.Value.Trim(), isDepend).InvDepartmentID);
                        }
                    }
                }

                if (rbtnDepartmentRange.Checked)
                {
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    long[] departmentIds = invDepartmentService.GetInvDepartmentIdRangeByDepartmentNames(txtDepartmentFrom.Text.Trim(), txtDepartmentTo.Text.Trim());

                    foreach (var item in departmentIds)
                    {
                        dtDepartments.Rows.Add(item);
                    }
                }
            }

            return dtDepartments;
        }

        private DataTable GetCategories()
        {
            DataTable dtCategories = new DataTable();
            dtCategories.Columns.Add("InvCategoryID", typeof(long));

            if (chkCategory.Checked)
            {
                if (rbtnCategorySelection.Checked)
                {
                    InvCategoryService invCategoryService = new InvCategoryService();
                    bool isDepend = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend;

                    if (categoryList.Any(l => l.isChecked.Equals(CheckState.Checked)))
                    {
                        foreach (var item in categoryList.Where(l => l.isChecked.Equals(CheckState.Checked)))
                        {
                            dtCategories.Rows.Add(invCategoryService.GetInvCategoryByName(item.Value.Trim(), isDepend).InvCategoryID);
                        }
                    }
                }

                if (rbtnCategoryRange.Checked)
                {
                    InvCategoryService invCategoryService = new InvCategoryService();
                    long[] categoryIds = invCategoryService.GetInvCategoryIdRangeByCategoryNames(txtCategoryFrom.Text.Trim(), txtCategoryTo.Text.Trim());

                    foreach (var item in categoryIds)
                    {
                        dtCategories.Rows.Add(item);
                    }
                }
            }
            return dtCategories;
        }

        private DataTable GetSubCategories()
        {
            DataTable dtSubCategories = new DataTable();
            dtSubCategories.Columns.Add("InvSubCategoryID", typeof(long));
            if (chkSubCategory.Checked)
            {
                if (rbtnSubCategorySelection.Checked)
                {
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    bool isDepend = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend;

                    if (subCategoryList.Any(l => l.isChecked.Equals(CheckState.Checked)))
                    {
                        foreach (var item in subCategoryList.Where(l => l.isChecked.Equals(CheckState.Checked)))
                        {
                            dtSubCategories.Rows.Add(invSubCategoryService.GetInvSubCategoryByName(item.Value.Trim(), isDepend).InvSubCategoryID);
                        }
                    }
                }

                if (rbtnSubCategoryRange.Checked)
                {
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    long[] subCategoryIds = invSubCategoryService.GetInvSubCategoryIdRangeBySubCategoryNames(txtSubCategoryFrom.Text.Trim(), txtSubCategoryTo.Text.Trim());

                    foreach (var item in subCategoryIds)
                    {
                        dtSubCategories.Rows.Add(item);
                    }
                }
            }
            return dtSubCategories;
        }

        private DataTable GetSubCategories2()
        {
            DataTable dtSubCategories2 = new DataTable();
            dtSubCategories2.Columns.Add("InvSubCategory2ID", typeof(long));

            if (chkSubCategory2.Checked)
            {
                if (rbtnSubCategory2Selection.Checked)
                {
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();

                    if (subCategory2List.Any(l => l.isChecked.Equals(CheckState.Checked)))
                    {
                        foreach (var item in subCategory2List.Where(l => l.isChecked.Equals(CheckState.Checked)))
                        {
                            dtSubCategories2.Rows.Add(invSubCategory2Service.GetInvSubCategory2ByName(item.Value.Trim()).InvSubCategory2ID);
                        }
                    }
                }

                if (rbtnSubCategory2Range.Checked)
                {
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    long[] subCategory2Ids = invSubCategory2Service.GetInvSubCategory2IdRangeBySubCategory2Names(txtSubCategory2From.Text.Trim(), txtSubCategory2To.Text.Trim());

                    foreach (var item in subCategory2Ids)
                    {
                        dtSubCategories2.Rows.Add(item);
                    }
                }
            }
            return dtSubCategories2;
        }

        private DataTable GetSupplier()
        {
            DataTable dtSupplier = new DataTable();
            dtSupplier.Columns.Add("SupplierID", typeof(long));

            if (rbtnSupplier.Checked)
            {
                if (rbtnReportTypeSelection.Checked)
                {
                    SupplierService supplierService = new SupplierService();

                    if (suppliersList.Any(l => l.isChecked.Equals(CheckState.Checked)))
                    {
                        foreach (var item in suppliersList.Where(l => l.isChecked.Equals(CheckState.Checked)))
                        {
                            dtSupplier.Rows.Add(supplierService.GetSupplierByName(item.Value.Trim()).SupplierName);
                        }
                    }
                }

                if (rbtnReportTypeRange.Checked)
                {
                    SupplierService supplierService = new SupplierService();
                    long[] supplierIds = supplierService.GetSupplierIdRangeBySupplierNames(txtReportTypeFrom.Text.Trim(), txtReportTypeTo.Text.Trim());

                    foreach (var item in supplierIds)
                    {
                        dtSupplier.Rows.Add(item);
                    }
                }
            }
            return dtSupplier;
        }

        private DataTable GetProductExtendedProperties()
        {
            DataTable dtExtendedProperties = new DataTable();
            dtExtendedProperties.Columns.Add("InvProductExtendedPropertyID", typeof(long));

            if (chkExtendedProperty.Checked)
            {
                InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                if (productExtendedPropertiesList.Any(l => l.isChecked.Equals(CheckState.Checked)))
                {
                    foreach (var item in productExtendedPropertiesList.Where(l => l.isChecked.Equals(CheckState.Checked)))
                    {
                        dtExtendedProperties.Rows.Add(invProductExtendedPropertyService.GetInvProductExtendedPropertyByName(item.Value.Trim()).InvProductExtendedPropertyID);
                    }
                }
            }
            return dtExtendedProperties;
        }

        private DataTable GetReportTypeData()
        {
            DataTable dtReportType = new DataTable();
            if (rbtnSupplier.Checked)
            {
                dtReportType.Columns.Add("SupplierID", typeof(long));
                if (chkReportType.Checked)
                {
                    if (rbtnReportTypeSelection.Checked)
                    {
                        if (suppliersList.Any(l => l.isChecked.Equals(CheckState.Checked)))
                        {
                            SupplierService supplierService = new SupplierService();
                            foreach (var item in suppliersList.Where(l => l.isChecked.Equals(CheckState.Checked)))
                            {
                                dtReportType.Rows.Add(supplierService.GetSupplierByName(item.Value.Trim()).SupplierID);
                            }
                        }
                    }

                    if (rbtnReportTypeRange.Checked)
                    {
                        SupplierService supplierService = new SupplierService();
                        long[] supplierIds = supplierService.GetSupplierIdRangeBySupplierNames(txtReportTypeFrom.Text.Trim(), txtReportTypeTo.Text.Trim());

                        foreach (var item in supplierIds)
                        {
                            dtReportType.Rows.Add(item);
                        }
                    }
                }
            }
            if (rbtnCustomer.Checked)
            {
                dtReportType.Columns.Add("CustomerID", typeof(long));
                if (chkReportType.Checked)
                {
                    if (rbtnReportTypeSelection.Checked)
                    {
                        if (customersList.Any(l => l.isChecked.Equals(CheckState.Checked)))
                        {
                            CustomerService customerService = new CustomerService();
                            foreach (var item in customersList.Where(l => l.isChecked.Equals(CheckState.Checked)))
                            {
                                dtReportType.Rows.Add(customerService.GetCustomersByName(item.Value.Trim()).CustomerID);
                            }
                        }
                    }

                    if (rbtnReportTypeRange.Checked)
                    {
                        CustomerService customerService = new CustomerService();
                        long[] customerIds = customerService.GetCustomerIdRangeByCustomerNames(txtReportTypeFrom.Text.Trim(), txtReportTypeTo.Text.Trim());

                        foreach (var item in customerIds)
                        {
                            dtReportType.Rows.Add(item);
                        }
                    }
                }
            }

            return dtReportType;
        }

        private InvProductMaster GetInvProductMasterByProductCode(string productCode)
        {
            InvProductMasterService invProductMasterService = new InvProductMasterService();
            InvProductMaster invProductMaster = new InvProductMaster();
            invProductMaster = invProductMasterService.GetProductsByCode(productCode.Trim());

            return invProductMaster;
        }

        private InvProductMaster GetInvProductMasterByProductName(string productName)
        {
            InvProductMasterService invProductMasterService = new InvProductMasterService();
            InvProductMaster invProductMaster = new InvProductMaster();
            invProductMaster = invProductMasterService.GetProductsByName(productName.Trim());

            return invProductMaster;
        }

        public override void View()
        {
            try
            {
                string report = string.Empty, reportType = string.Empty, displayType = string.Empty, productCodeFrom = string.Empty, productCodeTo = string.Empty;
                bool isAmt, isQty, isCval, isSval;
                DateTime dateFrom, dateTo;
                DataTable dtLocations, dtDepartments = new DataTable(), dtCategories = new DataTable(), dtSubCategories = new DataTable(),
                    dtSubCategories2 = new DataTable(), dtSupplier = new DataTable(), dtProductExtendedProperties = new DataTable(), dtReportType = new DataTable();

                #region product range
                if (string.IsNullOrEmpty(txtProductCodeFrom.Text.Trim()) && string.IsNullOrEmpty(txtProductNameFrom.Text.Trim()) && string.IsNullOrEmpty(txtProductCodeTo.Text.Trim()) && string.IsNullOrEmpty(txtProductNameTo.Text.Trim()))
                {
                    productCodeFrom = string.Empty;
                    productCodeTo = string.Empty;
                }
                else
                {
                    productCodeFrom = GetProductCodeFrom();
                    productCodeTo = GetProductCodeTo();

                    if (string.IsNullOrEmpty(productCodeFrom.Trim()) && string.IsNullOrEmpty(productCodeTo.Trim()))
                    {
                        Toast.Show("Invalid products range.", Toast.messageType.Information, Toast.messageAction.General);
                        return;
                    }

                    // Validate selected order
                    if (!isValidStringRangeValues(txtProductCodeFrom.Text.Trim(), txtProductCodeTo.Text.Trim()))
                    {
                        Toast.Show("Invalid product range.\nProduct range not in ascending order.", Toast.messageType.Information, Toast.messageAction.General);
                        return;
                    }
                }

                #endregion

                #region report
                report = GetReport();
                if (string.IsNullOrEmpty(report))
                {
                    Toast.Show("Report ", Toast.messageType.Information, Toast.messageAction.NotSelected);
                    return;
                }
                #endregion

                #region display type
                displayType = GetDisplayType();
                if (string.IsNullOrEmpty(displayType))
                {
                    Toast.Show("Display Type", Toast.messageType.Information, Toast.messageAction.NotSelected);
                    return;
                }
                #endregion

                #region date range
                dateFrom = dtpDateFrom.Value;
                dateTo = dtpDateTo.Value;
                #endregion

                #region output values
                if (rbtnAmount.Checked.Equals(false) && rbtnQuantity.Checked.Equals(false) && rbtnSaleValue.Checked.Equals(false) && rbtnCostValue.Checked.Equals(false))
                {
                    Toast.Show("Output Values ", Toast.messageType.Information, Toast.messageAction.NotSelected);
                    return;
                }
                else
                {
                    isAmt = IsAmount();
                    isQty = IsQuantity();
                    isSval = IsSaleValue();
                    isCval = IsSaleValue();
                }
                #endregion

                #region locations

                if (rbtnLocationSelection.Checked)
                {
                    if (!locationList.Any(l => l.isChecked.Equals(CheckState.Checked)))
                    {
                        Toast.Show("Locations ", Toast.messageType.Information, Toast.messageAction.Empty);
                        return;
                    }
                }

                if (rbtnLocationRange.Checked)
                {
                    if (string.IsNullOrEmpty(txtLocationFrom.Text.Trim()) || string.IsNullOrEmpty(txtLocationTo.Text.Trim()))
                    {
                        Toast.Show("Locations ", Toast.messageType.Information, Toast.messageAction.Empty);
                        return;
                    }
                    if (!isValidStringRangeValues(txtLocationFrom.Text.Trim(), txtLocationTo.Text.Trim()))
                    {
                        Toast.Show("Invalid location range.\nLocation range not in ascending order.", Toast.messageType.Information, Toast.messageAction.General);
                        return;
                    }
                }

                dtLocations = GetLocations();
                #endregion

                #region product properties
                dtDepartments = GetDepartments();
                dtCategories = GetCategories();
                dtSubCategories = GetSubCategories();
                dtSubCategories2 = GetSubCategories2();
                dtSupplier = GetSupplier();
                #endregion

                #region product extended properties
                dtProductExtendedProperties = GetProductExtendedProperties();
                #endregion

                #region report type
                reportType = GetReportType();
                dtReportType = GetReportTypeData();
                #endregion

                ReportService reportService = new ReportService();
                if (reportService.ExecuteReportStoredProcedure(productCodeFrom, productCodeTo, report, reportType, isAmt, isQty, dtLocations, dtDepartments, dtCategories,
                    dtSubCategories, dtSubCategories2,dtSupplier, dtProductExtendedProperties, displayType, dateFrom, dateTo))
                {
                    ViewReport(dtLocations, dtProductExtendedProperties, report, isAmt, isQty, isCval, isSval, dateFrom, dateTo);
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void ViewReport(DataTable dtLocations,DataTable dtExtendedProperty, string report, bool isAmt, bool isQty, bool isCval, bool isSval, DateTime dateFrom, DateTime dateTo)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            Cursor.Current = Cursors.WaitCursor;
            DataTable dtReportData = new DataTable();

            string stValues = string.Empty;
            string stQtys = string.Empty;
            string reportTitle = (string.Equals(report, "PU") ? "Purcahse " : (string.Equals(report, "SL") ? "Sales " : (string.Equals(report, "ST") ? "Stock " : "")));  

            if (isAmt)
            {
                //for (int i = 0; i < dtLocations.Rows.Count; i++)
                //{
                //    if (i >= 25) // limit Value columns to 25
                //    { break; }
                //    stValues = stValues + ", Value" + (i < 9 ? "0" : "") + (i + 1).ToString();
                //}

                stValues = " ,Value01, Value02, Value03, Value04, Value05, Value06, Value07, Value08, Value09, Value10, Value11, Value12, Value13, Value14, Value15, Value16, Value17, Value18, Value19, Value20, Value21, Value22, Value23, Value24, Value25, Value26 ";
                reportTitle = reportTitle + " (Amount)";
            }

            if (isQty)
            {
                //for (int i = 0; i < dtLocations.Rows.Count; i++)
                //{
                //    if (i >= 25) // limit Value columns to 25
                //    { break; }
                //    stQtys = stQtys + ", Qty" + (i < 9 ? "0" : "") + (i + 1).ToString();
                //}

                stQtys = " ,Qty01, Qty02, Qty03, Qty04, Qty05, Qty06, Qty07, Qty08, Qty09, Qty10, Qty11, Qty12, Qty13, Qty14, Qty15, Qty16, Qty17, Qty18, Qty19, Qty20, Qty21, Qty22, Qty23, Qty24, Qty25, Qty26 ";
                reportTitle = reportTitle + " (Quantity)";
            }

            #region Supplier
            if (rbtnSupplier.Checked.Equals(true)) 
            {
                dtReportData = CommonService.ExecuteSqlQuery("SELECT  DocumentDate, SupplierCode, SupplierName, CustomerCode, CustomerName, ProductCode, ProductName,"
                + " DepartmentCode, DepartmentName, CategoryCode, CategoryName, SubCategoryCode, SubCategoryName, SubCategory2ID, SubCategory2Code, SubCategory2Name,"
                + " Ext1, Ext2, Ext3, Ext4, Ext5,SupplierCode,SupplierName"
                + " CostPrice, SellingPrice" + stValues + stQtys
                + " FROM InvTmpReportDetail WHERE UserId =" + Common.LoggedUserId.ToString().Trim() + "");

               if (rbtnAmount.Checked)
                {
                    InvRptSupplierWiseAmount invRptSupplierWiseAmount = new InvRptSupplierWiseAmount();
                    invRptSupplierWiseAmount.SetDataSource(dtReportData);
                    invRptSupplierWiseAmount.SummaryInfo.ReportTitle = "Supplier Wise " + reportTitle;
                    invRptSupplierWiseAmount.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptSupplierWiseAmount.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptSupplierWiseAmount.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptSupplierWiseAmount.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptSupplierWiseAmount.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptSupplierWiseAmount.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                    invRptSupplierWiseAmount.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                    invRptSupplierWiseAmount.DataDefinition.FormulaFields["FromRange"].Text = "''";
                    invRptSupplierWiseAmount.DataDefinition.FormulaFields["ToRange"].Text = "''";
                    // Arrange report header locations
                    LocationService locationService = new LocationService();
                    for (int i = 0; i < dtLocations.Rows.Count; i++)
                    {
                        if (i >= 25) // limit Value columns to 25
                        { break; }
                        var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                        invRptSupplierWiseAmount.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                    }
                    //InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                    //for (int i = 0; i < dtExtendedProperty.Rows.Count; i++)
                    //{
                    //    if (i >= 5) // limit Value columns to 5
                    //    { break; }
                    //    var ExtendedPropertyName = object.Equals(invProductExtendedPropertyService.GetInvProductExtendedPropertyById(int.Parse(dtExtendedProperty.Rows[i]["InvProductExtendedPropertyID"].ToString())).ExtendedPropertyName.Trim(),null)?"":invProductExtendedPropertyService.GetInvProductExtendedPropertyById(int.Parse(dtExtendedProperty.Rows[i]["InvProductExtendedPropertyID"].ToString())).ExtendedPropertyName.Trim();
                    //    invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["Ext" + (i + 1).ToString()].Text = "'" + ExtendedPropertyName.ToString() + "'";


                    //}

                    reportViewer.crRptViewer.ReportSource = invRptSupplierWiseAmount;
                }
                if (rbtnQuantity.Checked)
                {
                    InvRptSupplierWiseQty invRptSupplierWiseQty = new InvRptSupplierWiseQty();
                    invRptSupplierWiseQty.SetDataSource(dtReportData);
                    invRptSupplierWiseQty.SummaryInfo.ReportTitle = "Supplier Wise " + reportTitle;
                    invRptSupplierWiseQty.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptSupplierWiseQty.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptSupplierWiseQty.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptSupplierWiseQty.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptSupplierWiseQty.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptSupplierWiseQty.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                    invRptSupplierWiseQty.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                    invRptSupplierWiseQty.DataDefinition.FormulaFields["FromRange"].Text = "''";
                    invRptSupplierWiseQty.DataDefinition.FormulaFields["ToRange"].Text = "''";
                    // Arrange report header locations
                    LocationService locationService = new LocationService();
                    for (int i = 0; i < dtLocations.Rows.Count; i++)
                    {
                        if (i >= 25) // limit Value columns to 25
                        { break; }
                        var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                        invRptSupplierWiseQty.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                    }


                    reportViewer.crRptViewer.ReportSource = invRptSupplierWiseQty;
                }
            }
            #endregion

            #region Depertment wise
            if (chkDepartment.Checked.Equals(true))
            {
                dtReportData = CommonService.ExecuteSqlQuery("SELECT  DocumentDate, SupplierCode, SupplierName, CustomerCode, CustomerName, ProductCode, ProductName,"
                + " DepartmentCode, DepartmentName, CategoryCode, CategoryName, SubCategoryCode, SubCategoryName, SubCategory2ID, SubCategory2Code, SubCategory2Name,"
                + " CostPrice, SellingPrice" + stValues + stQtys
                + " FROM InvTmpReportDetail WHERE UserId =" + Common.LoggedUserId.ToString().Trim() + "");

                if (chkCategory.Checked.Equals(true)) // Dept and Cat
                {
                    if (rbtnAmount.Checked)
                    {
                        InvRptDepartmentWiseCategoryWiseAmount invRptDepartmentWiseCategoryWiseAmount = new InvRptDepartmentWiseCategoryWiseAmount();
                        invRptDepartmentWiseCategoryWiseAmount.SetDataSource(dtReportData);
                        invRptDepartmentWiseCategoryWiseAmount.SummaryInfo.ReportTitle = "Department Wise Category Wise " + reportTitle;
                        invRptDepartmentWiseCategoryWiseAmount.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptDepartmentWiseCategoryWiseAmount.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptDepartmentWiseCategoryWiseAmount.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptDepartmentWiseCategoryWiseAmount.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptDepartmentWiseCategoryWiseAmount.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptDepartmentWiseCategoryWiseAmount.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptDepartmentWiseCategoryWiseAmount.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptDepartmentWiseCategoryWiseAmount.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptDepartmentWiseCategoryWiseAmount.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptDepartmentWiseCategoryWiseAmount.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";

                        }

                        reportViewer.crRptViewer.ReportSource = invRptDepartmentWiseCategoryWiseAmount;
                    }
                    if (rbtnQuantity.Checked)
                    {
                        InvRptDepartmentWiseCategoryWiseQty invRptDepartmentWiseCategoryWiseQty = new InvRptDepartmentWiseCategoryWiseQty();
                        invRptDepartmentWiseCategoryWiseQty.SetDataSource(dtReportData);
                        invRptDepartmentWiseCategoryWiseQty.SummaryInfo.ReportTitle = "Department Wise Category Wise " + reportTitle;
                        invRptDepartmentWiseCategoryWiseQty.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptDepartmentWiseCategoryWiseQty.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptDepartmentWiseCategoryWiseQty.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptDepartmentWiseCategoryWiseQty.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptDepartmentWiseCategoryWiseQty.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptDepartmentWiseCategoryWiseQty.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptDepartmentWiseCategoryWiseQty.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptDepartmentWiseCategoryWiseQty.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptDepartmentWiseCategoryWiseQty.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptDepartmentWiseCategoryWiseQty.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";

                        }
                        
                        reportViewer.crRptViewer.ReportSource = invRptDepartmentWiseCategoryWiseQty;
                    }
                }
                else if (chkSubCategory.Checked.Equals(true)) // Dept and SubCat
                {
                    if (rbtnAmount.Checked)
                    {
                        InvRptDepartmentWiseSubCategoryWiseAmount invRptDepartmentWiseSubCategoryWiseAmount = new InvRptDepartmentWiseSubCategoryWiseAmount();
                        invRptDepartmentWiseSubCategoryWiseAmount.SetDataSource(dtReportData);
                        invRptDepartmentWiseSubCategoryWiseAmount.SummaryInfo.ReportTitle = "Department Wise Sub Category Wise " + reportTitle;
                        invRptDepartmentWiseSubCategoryWiseAmount.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptDepartmentWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptDepartmentWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptDepartmentWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptDepartmentWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptDepartmentWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptDepartmentWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptDepartmentWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptDepartmentWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptDepartmentWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";

                        }
                        reportViewer.crRptViewer.ReportSource = invRptDepartmentWiseSubCategoryWiseAmount;
                    }
                    if (rbtnQuantity.Checked)
                    {
                        InvRptDepartmentWiseSubCategoryWiseQty invRptDepartmentWiseSubCategoryWiseQty = new InvRptDepartmentWiseSubCategoryWiseQty();
                        invRptDepartmentWiseSubCategoryWiseQty.SetDataSource(dtReportData);
                        invRptDepartmentWiseSubCategoryWiseQty.SummaryInfo.ReportTitle = "Department Wise Sub Category Wise " + reportTitle;
                        invRptDepartmentWiseSubCategoryWiseQty.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptDepartmentWiseSubCategoryWiseQty.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptDepartmentWiseSubCategoryWiseQty.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptDepartmentWiseSubCategoryWiseQty.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptDepartmentWiseSubCategoryWiseQty.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptDepartmentWiseSubCategoryWiseQty.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptDepartmentWiseSubCategoryWiseQty.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptDepartmentWiseSubCategoryWiseQty.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptDepartmentWiseSubCategoryWiseQty.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptDepartmentWiseSubCategoryWiseQty.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";

                        }
                        reportViewer.crRptViewer.ReportSource = invRptDepartmentWiseSubCategoryWiseQty;
                    }
                }
                else if (chkSubCategory2.Checked.Equals(true)) // Dept and SubCat2
                {
                    if (rbtnAmount.Checked)
                    {
                        InvRptDepartmentWiseSubCategory2WiseAmount invRptDepartmentWiseSubCategory2WiseAmount = new InvRptDepartmentWiseSubCategory2WiseAmount();
                        invRptDepartmentWiseSubCategory2WiseAmount.SetDataSource(dtReportData);
                        invRptDepartmentWiseSubCategory2WiseAmount.SummaryInfo.ReportTitle = "Department Wise Sub Category 2 Wise " + reportTitle;
                        invRptDepartmentWiseSubCategory2WiseAmount.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptDepartmentWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptDepartmentWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptDepartmentWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptDepartmentWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptDepartmentWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptDepartmentWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptDepartmentWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptDepartmentWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptDepartmentWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";

                        }
                        reportViewer.crRptViewer.ReportSource = invRptDepartmentWiseSubCategory2WiseAmount;
                    }
                    if (rbtnQuantity.Checked)
                    {
                        InvRptDepartmentWiseSubCategory2WiseQty invRptDepartmentWiseSubCategory2WiseQty = new InvRptDepartmentWiseSubCategory2WiseQty();
                        invRptDepartmentWiseSubCategory2WiseQty.SetDataSource(dtReportData);
                        invRptDepartmentWiseSubCategory2WiseQty.SummaryInfo.ReportTitle = "Department Wise Sub Category 2 Wise " + reportTitle;
                        invRptDepartmentWiseSubCategory2WiseQty.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptDepartmentWiseSubCategory2WiseQty.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptDepartmentWiseSubCategory2WiseQty.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptDepartmentWiseSubCategory2WiseQty.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptDepartmentWiseSubCategory2WiseQty.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptDepartmentWiseSubCategory2WiseQty.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptDepartmentWiseSubCategory2WiseQty.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptDepartmentWiseSubCategory2WiseQty.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptDepartmentWiseSubCategory2WiseQty.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptDepartmentWiseSubCategory2WiseQty.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }
                        reportViewer.crRptViewer.ReportSource = invRptDepartmentWiseSubCategory2WiseQty;
                    }
                }
                #region ExtendedProperty
                else if (chkExtendedProperty.Checked.Equals(true)) // SubCat only
                {
                    dtReportData = CommonService.ExecuteSqlQuery("SELECT  DocumentDate, SupplierCode, SupplierName, CustomerCode, CustomerName, ProductCode, ProductName,"
                    + " DepartmentCode, DepartmentName, CategoryCode, CategoryName, SubCategoryCode, SubCategoryName, SubCategory2ID, SubCategory2Code, SubCategory2Name,"
                    + " Ext1, Ext2, Ext3, Ext4, Ext5,"
                    + " CostPrice, SellingPrice" + stValues + stQtys
                    + " FROM InvTmpReportDetail WHERE UserId =" + Common.LoggedUserId.ToString().Trim() + "");

                    if (rbtnAmount.Checked)
                    {
                        InvRptDepartmentWiseAmountExt invRptDepartmentWiseAmountExt = new InvRptDepartmentWiseAmountExt();
                        invRptDepartmentWiseAmountExt.SetDataSource(dtReportData);
                        invRptDepartmentWiseAmountExt.SummaryInfo.ReportTitle = "Department Wise Extended Property Wise " + reportTitle;
                        invRptDepartmentWiseAmountExt.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }
                        //InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                        //for (int i = 0; i < dtExtendedProperty.Rows.Count; i++)
                        //{
                        //    if (i >= 5) // limit Value columns to 5
                        //    { break; }
                        //    var ExtendedPropertyName = object.Equals(invProductExtendedPropertyService.GetInvProductExtendedPropertyById(int.Parse(dtExtendedProperty.Rows[i]["InvProductExtendedPropertyID"].ToString())).ExtendedPropertyName.Trim(),null)?"":invProductExtendedPropertyService.GetInvProductExtendedPropertyById(int.Parse(dtExtendedProperty.Rows[i]["InvProductExtendedPropertyID"].ToString())).ExtendedPropertyName.Trim();
                        //    invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["Ext" + (i + 1).ToString()].Text = "'" + ExtendedPropertyName.ToString() + "'";


                        //}

                        reportViewer.crRptViewer.ReportSource = invRptDepartmentWiseAmountExt;
                    }
                    if (rbtnQuantity.Checked)
                    {
                        InvRptDepartmentWiseQtyExt invRptDepartmentWiseQtyExt = new InvRptDepartmentWiseQtyExt();
                        invRptDepartmentWiseQtyExt.SetDataSource(dtReportData);
                        invRptDepartmentWiseQtyExt.SummaryInfo.ReportTitle = "Department Wise Extended Property Wise " + reportTitle;
                        invRptDepartmentWiseQtyExt.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }


                        reportViewer.crRptViewer.ReportSource = invRptDepartmentWiseQtyExt;
                    }
                }
                #endregion


                else // Dept only
                {
                    if (rbtnAmount.Checked)
                    {
                        InvRptDepartmentWiseAmount invRptDepartmentWiseAmount = new InvRptDepartmentWiseAmount();
                        invRptDepartmentWiseAmount.SetDataSource(dtReportData);
                        invRptDepartmentWiseAmount.SummaryInfo.ReportTitle = "Department Wise " + reportTitle;
                        invRptDepartmentWiseAmount.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptDepartmentWiseAmount.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptDepartmentWiseAmount.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptDepartmentWiseAmount.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptDepartmentWiseAmount.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptDepartmentWiseAmount.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptDepartmentWiseAmount.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptDepartmentWiseAmount.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptDepartmentWiseAmount.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptDepartmentWiseAmount.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";

                        }
                        reportViewer.crRptViewer.ReportSource = invRptDepartmentWiseAmount;
                    }
                    if (rbtnQuantity.Checked)
                    {
                        InvRptDepartmentWiseQty invRptDepartmentWiseQty = new InvRptDepartmentWiseQty();
                        invRptDepartmentWiseQty.SetDataSource(dtReportData);
                        invRptDepartmentWiseQty.SummaryInfo.ReportTitle = "Department Wise " + reportTitle;
                        invRptDepartmentWiseQty.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptDepartmentWiseQty.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptDepartmentWiseQty.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptDepartmentWiseQty.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptDepartmentWiseQty.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptDepartmentWiseQty.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptDepartmentWiseQty.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptDepartmentWiseQty.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptDepartmentWiseQty.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptDepartmentWiseQty.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                            
                        }
                        
                        reportViewer.crRptViewer.ReportSource = invRptDepartmentWiseQty;
                    }
                }
            }
            #endregion

            #region Category wise
            if (chkCategory.Checked.Equals(true))
            {
                dtReportData = CommonService.ExecuteSqlQuery("SELECT  DocumentDate, SupplierCode, SupplierName, CustomerCode, CustomerName, ProductCode, ProductName,"
                + " CategoryCode, CategoryName, SubCategoryCode, SubCategoryName, SubCategory2ID, SubCategory2Code, SubCategory2Name,"
                + " CostPrice, SellingPrice" + stValues + stQtys
                + " FROM InvTmpReportDetail WHERE UserId =" + Common.LoggedUserId.ToString().Trim() + "");

                if (chkSubCategory.Checked.Equals(true)) // Cat and SubCat
                {
                    if (rbtnAmount.Checked)
                    {
                        InvRptCategoryWiseSubCategoryWiseAmount invRptCategoryWiseSubCategoryWiseAmount = new InvRptCategoryWiseSubCategoryWiseAmount();
                        invRptCategoryWiseSubCategoryWiseAmount.SetDataSource(dtReportData);
                        invRptCategoryWiseSubCategoryWiseAmount.SummaryInfo.ReportTitle = "Category Wise Sub Category Wise " + reportTitle;
                        invRptCategoryWiseSubCategoryWiseAmount.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptCategoryWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptCategoryWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptCategoryWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptCategoryWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptCategoryWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptCategoryWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptCategoryWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptCategoryWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptCategoryWiseSubCategoryWiseAmount.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";

                        }
                        reportViewer.crRptViewer.ReportSource = invRptCategoryWiseSubCategoryWiseAmount;
                    }
                    if (rbtnQuantity.Checked)
                    {
                        InvRptCategoryWiseSubCategoryWiseQty invRptCategoryWiseSubCategoryWiseQty = new InvRptCategoryWiseSubCategoryWiseQty();
                        invRptCategoryWiseSubCategoryWiseQty.SetDataSource(dtReportData);
                        invRptCategoryWiseSubCategoryWiseQty.SummaryInfo.ReportTitle = "Category Wise Sub Category Wise " + reportTitle;
                        invRptCategoryWiseSubCategoryWiseQty.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptCategoryWiseSubCategoryWiseQty.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptCategoryWiseSubCategoryWiseQty.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptCategoryWiseSubCategoryWiseQty.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptCategoryWiseSubCategoryWiseQty.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptCategoryWiseSubCategoryWiseQty.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptCategoryWiseSubCategoryWiseQty.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptCategoryWiseSubCategoryWiseQty.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptCategoryWiseSubCategoryWiseQty.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptCategoryWiseSubCategoryWiseQty.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";

                        }
                        reportViewer.crRptViewer.ReportSource = invRptCategoryWiseSubCategoryWiseQty;
                    }
                }
                else if (chkSubCategory2.Checked.Equals(true)) // Cat and SubCat2
                {
                    if (rbtnAmount.Checked)
                    {
                        InvRptCategoryWiseSubCategory2WiseAmount invRptCategoryWiseSubCategory2WiseAmount = new InvRptCategoryWiseSubCategory2WiseAmount();
                        invRptCategoryWiseSubCategory2WiseAmount.SetDataSource(dtReportData);
                        invRptCategoryWiseSubCategory2WiseAmount.SummaryInfo.ReportTitle = "Category Wise Sub Category 2 Wise " + reportTitle;
                        invRptCategoryWiseSubCategory2WiseAmount.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";

                        }
                        reportViewer.crRptViewer.ReportSource = invRptCategoryWiseSubCategory2WiseAmount;
                    }
                    if (rbtnQuantity.Checked)
                    {
                        InvRptCategoryWiseSubCategory2WiseQty invRptCategoryWiseSubCategory2WiseQty = new InvRptCategoryWiseSubCategory2WiseQty();
                        invRptCategoryWiseSubCategory2WiseQty.SetDataSource(dtReportData);
                        invRptCategoryWiseSubCategory2WiseQty.SummaryInfo.ReportTitle = "Category Wise Sub Category 2 Wise " + reportTitle;
                        invRptCategoryWiseSubCategory2WiseQty.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";

                        }
                        reportViewer.crRptViewer.ReportSource = invRptCategoryWiseSubCategory2WiseQty;
                    }
                }

                #region ExtendedProperty
                else if (chkExtendedProperty.Checked.Equals(true)) // SubCat only
                {
                    dtReportData = CommonService.ExecuteSqlQuery("SELECT  DocumentDate, SupplierCode, SupplierName, CustomerCode, CustomerName, ProductCode, ProductName,"
                    + " DepartmentCode, DepartmentName, CategoryCode, CategoryName, SubCategoryCode, SubCategoryName, SubCategory2ID, SubCategory2Code, SubCategory2Name,"
                    + " Ext1, Ext2, Ext3, Ext4, Ext5,"
                    + " CostPrice, SellingPrice" + stValues + stQtys
                    + " FROM InvTmpReportDetail WHERE UserId =" + Common.LoggedUserId.ToString().Trim() + "");

                    if (rbtnAmount.Checked)
                    {
                        InvRptCategoryWiseAmountExt invRptCategoryWiseAmountExt = new InvRptCategoryWiseAmountExt();
                        invRptCategoryWiseAmountExt.SetDataSource(dtReportData);
                        invRptCategoryWiseAmountExt.SummaryInfo.ReportTitle = "Category Wise Extended Property Wise " + reportTitle;
                        invRptCategoryWiseAmountExt.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptCategoryWiseAmountExt.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptCategoryWiseAmountExt.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptCategoryWiseAmountExt.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptCategoryWiseAmountExt.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptCategoryWiseAmountExt.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptCategoryWiseAmountExt.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptCategoryWiseAmountExt.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptCategoryWiseAmountExt.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptCategoryWiseAmountExt.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }
                        //InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                        //for (int i = 0; i < dtExtendedProperty.Rows.Count; i++)
                        //{
                        //    if (i >= 5) // limit Value columns to 5
                        //    { break; }
                        //    var ExtendedPropertyName = object.Equals(invProductExtendedPropertyService.GetInvProductExtendedPropertyById(int.Parse(dtExtendedProperty.Rows[i]["InvProductExtendedPropertyID"].ToString())).ExtendedPropertyName.Trim(),null)?"":invProductExtendedPropertyService.GetInvProductExtendedPropertyById(int.Parse(dtExtendedProperty.Rows[i]["InvProductExtendedPropertyID"].ToString())).ExtendedPropertyName.Trim();
                        //    invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["Ext" + (i + 1).ToString()].Text = "'" + ExtendedPropertyName.ToString() + "'";


                        //}

                        reportViewer.crRptViewer.ReportSource = invRptCategoryWiseAmountExt;
                    }
                    if (rbtnQuantity.Checked)
                    {
                        InvRptCategoryWiseQtyExt invRptCategoryWiseQtyExt = new InvRptCategoryWiseQtyExt();
                        invRptCategoryWiseQtyExt.SetDataSource(dtReportData);
                        invRptCategoryWiseQtyExt.SummaryInfo.ReportTitle = "Category Wise Extended Property Wise " + reportTitle;
                        invRptCategoryWiseQtyExt.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptCategoryWiseQtyExt.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptCategoryWiseQtyExt.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptCategoryWiseQtyExt.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptCategoryWiseQtyExt.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptCategoryWiseQtyExt.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptCategoryWiseQtyExt.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptCategoryWiseQtyExt.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptCategoryWiseQtyExt.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptCategoryWiseQtyExt.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }


                        reportViewer.crRptViewer.ReportSource = invRptCategoryWiseQtyExt;
                    }
                }
                #endregion


                else // Cat only
                {
                    if (rbtnAmount.Checked)
                    {
                        InvRptCategoryWiseAmount invRptCategoryWiseAmount = new InvRptCategoryWiseAmount();
                        invRptCategoryWiseAmount.SetDataSource(dtReportData);
                        invRptCategoryWiseAmount.SummaryInfo.ReportTitle = "Category Wise " + reportTitle;
                        invRptCategoryWiseAmount.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptCategoryWiseAmount.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptCategoryWiseAmount.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptCategoryWiseAmount.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptCategoryWiseAmount.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptCategoryWiseAmount.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptCategoryWiseAmount.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptCategoryWiseAmount.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptCategoryWiseAmount.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptCategoryWiseAmount.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }
                        reportViewer.crRptViewer.ReportSource = invRptCategoryWiseAmount;
                    }
                    if (rbtnQuantity.Checked)
                    {
                        InvRptCategoryWiseQty invRptCategoryWiseQty = new InvRptCategoryWiseQty();
                        invRptCategoryWiseQty.SetDataSource(dtReportData);
                        invRptCategoryWiseQty.SummaryInfo.ReportTitle = "Category Wise " + reportTitle;
                        invRptCategoryWiseQty.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptCategoryWiseQty.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptCategoryWiseQty.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptCategoryWiseQty.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptCategoryWiseQty.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptCategoryWiseQty.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptCategoryWiseQty.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptCategoryWiseQty.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptCategoryWiseQty.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptCategoryWiseQty.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }
                        reportViewer.crRptViewer.ReportSource = invRptCategoryWiseQty;
                    }
                }
            }
            #endregion

            #region Sub Category wise
            if (chkSubCategory.Checked.Equals(true))
            {
                dtReportData = CommonService.ExecuteSqlQuery("SELECT  DocumentDate, SupplierCode, SupplierName, CustomerCode, CustomerName, ProductCode, ProductName,"
                + " SubCategoryCode, SubCategoryName, SubCategory2ID, SubCategory2Code, SubCategory2Name,"
                + " CostPrice, SellingPrice" + stValues + stQtys
                + " FROM InvTmpReportDetail WHERE UserId =" + Common.LoggedUserId.ToString().Trim() + "");

                if (chkSubCategory2.Checked.Equals(true)) // SubCat and SubCat2
                {
                    if (rbtnAmount.Checked)
                    {
                        InvRptSubCategoryWiseSubCategory2WiseAmount invRptSubCategoryWiseSubCategory2WiseAmount = new InvRptSubCategoryWiseSubCategory2WiseAmount();
                        invRptSubCategoryWiseSubCategory2WiseAmount.SetDataSource(dtReportData);
                        invRptSubCategoryWiseSubCategory2WiseAmount.SummaryInfo.ReportTitle = "Sub Category Wise Sub Category 2 Wise " + reportTitle;
                        invRptSubCategoryWiseSubCategory2WiseAmount.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptSubCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptSubCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptSubCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptSubCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptSubCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptSubCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptSubCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptSubCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptSubCategoryWiseSubCategory2WiseAmount.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }
                        reportViewer.crRptViewer.ReportSource = invRptSubCategoryWiseSubCategory2WiseAmount;
                    }
                    if (rbtnQuantity.Checked)
                    {
                        InvRptSubCategoryWiseSubCategory2WiseQty invRptSubCategoryWiseSubCategory2WiseQty = new InvRptSubCategoryWiseSubCategory2WiseQty();
                        invRptSubCategoryWiseSubCategory2WiseQty.SetDataSource(dtReportData);
                        invRptSubCategoryWiseSubCategory2WiseQty.SummaryInfo.ReportTitle = "Sub Category Wise Sub Category 2 Wise " + reportTitle;
                        invRptSubCategoryWiseSubCategory2WiseQty.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptSubCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptSubCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptSubCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptSubCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptSubCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptSubCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptSubCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptSubCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptSubCategoryWiseSubCategory2WiseQty.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }
                        reportViewer.crRptViewer.ReportSource = invRptSubCategoryWiseSubCategory2WiseQty;
                    }
                }
                #region ExtendedProperty
                else if (chkExtendedProperty.Checked.Equals(true)) // SubCat only
                {
                    dtReportData = CommonService.ExecuteSqlQuery("SELECT  DocumentDate, SupplierCode, SupplierName, CustomerCode, CustomerName, ProductCode, ProductName,"
                    + " DepartmentCode, DepartmentName, CategoryCode, CategoryName, SubCategoryCode, SubCategoryName, SubCategory2ID, SubCategory2Code, SubCategory2Name,"
                    + " Ext1, Ext2, Ext3, Ext4, Ext5,"
                    + " CostPrice, SellingPrice" + stValues + stQtys
                    + " FROM InvTmpReportDetail WHERE UserId =" + Common.LoggedUserId.ToString().Trim() + "");

                    if (rbtnAmount.Checked)
                    {
                        InvRptSubCategoryWiseAmountExt invRptSubCategoryWiseAmountExt = new InvRptSubCategoryWiseAmountExt();
                        invRptSubCategoryWiseAmountExt.SetDataSource(dtReportData);
                        invRptSubCategoryWiseAmountExt.SummaryInfo.ReportTitle = "SubCategory Wise Extended Property Wise " + reportTitle;
                        invRptSubCategoryWiseAmountExt.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptSubCategoryWiseAmountExt.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptSubCategoryWiseAmountExt.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptSubCategoryWiseAmountExt.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptSubCategoryWiseAmountExt.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptSubCategoryWiseAmountExt.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptSubCategoryWiseAmountExt.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptSubCategoryWiseAmountExt.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptSubCategoryWiseAmountExt.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptSubCategoryWiseAmountExt.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }
                        //InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                        //for (int i = 0; i < dtExtendedProperty.Rows.Count; i++)
                        //{
                        //    if (i >= 5) // limit Value columns to 5
                        //    { break; }
                        //    var ExtendedPropertyName = object.Equals(invProductExtendedPropertyService.GetInvProductExtendedPropertyById(int.Parse(dtExtendedProperty.Rows[i]["InvProductExtendedPropertyID"].ToString())).ExtendedPropertyName.Trim(),null)?"":invProductExtendedPropertyService.GetInvProductExtendedPropertyById(int.Parse(dtExtendedProperty.Rows[i]["InvProductExtendedPropertyID"].ToString())).ExtendedPropertyName.Trim();
                        //    invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["Ext" + (i + 1).ToString()].Text = "'" + ExtendedPropertyName.ToString() + "'";


                        //}

                        reportViewer.crRptViewer.ReportSource = invRptSubCategoryWiseAmountExt;
                    }
                    if (rbtnQuantity.Checked)
                    {
                        InvRptSubCategoryWiseQtyExt invRptSubCategoryWiseQtyExt = new InvRptSubCategoryWiseQtyExt();
                        invRptSubCategoryWiseQtyExt.SetDataSource(dtReportData);
                        invRptSubCategoryWiseQtyExt.SummaryInfo.ReportTitle = "SubCategory Wise Extended Property Wise " + reportTitle;
                        invRptSubCategoryWiseQtyExt.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptSubCategoryWiseQtyExt.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptSubCategoryWiseQtyExt.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptSubCategoryWiseQtyExt.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptSubCategoryWiseQtyExt.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptSubCategoryWiseQtyExt.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptSubCategoryWiseQtyExt.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptSubCategoryWiseQtyExt.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptSubCategoryWiseQtyExt.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptSubCategoryWiseQtyExt.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }


                        reportViewer.crRptViewer.ReportSource = invRptSubCategoryWiseQtyExt;
                    }
                }
                #endregion

                else // SubCat only
                {
                    if (rbtnAmount.Checked)
                    {
                        InvRptSubCategoryWiseAmount invRptSubCategoryWiseAmount = new InvRptSubCategoryWiseAmount();
                        invRptSubCategoryWiseAmount.SetDataSource(dtReportData);
                        invRptSubCategoryWiseAmount.SummaryInfo.ReportTitle = "Sub Category Wise " + reportTitle;
                        invRptSubCategoryWiseAmount.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptSubCategoryWiseAmount.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptSubCategoryWiseAmount.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptSubCategoryWiseAmount.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptSubCategoryWiseAmount.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptSubCategoryWiseAmount.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptSubCategoryWiseAmount.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptSubCategoryWiseAmount.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptSubCategoryWiseAmount.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptSubCategoryWiseAmount.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }
                        reportViewer.crRptViewer.ReportSource = invRptSubCategoryWiseAmount;
                    }
                    if (rbtnQuantity.Checked)
                    {
                        InvRptSubCategoryWiseQty invRptSubCategoryWiseQty = new InvRptSubCategoryWiseQty();
                        invRptSubCategoryWiseQty.SetDataSource(dtReportData);
                        invRptSubCategoryWiseQty.SummaryInfo.ReportTitle = "Sub Category Wise " + reportTitle;
                        invRptSubCategoryWiseQty.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptSubCategoryWiseQty.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptSubCategoryWiseQty.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptSubCategoryWiseQty.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptSubCategoryWiseQty.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptSubCategoryWiseQty.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptSubCategoryWiseQty.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptSubCategoryWiseQty.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptSubCategoryWiseQty.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptSubCategoryWiseQty.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }
                        reportViewer.crRptViewer.ReportSource = invRptSubCategoryWiseQty;
                    }
                }
            }
            #endregion

            #region Sub Category 2 wise
            if (chkSubCategory2.Checked.Equals(true)) // SubCat only
            {
                #region ExtendedProperty
                if (chkExtendedProperty.Checked.Equals(true)) // SubCat only
                {
                    dtReportData = CommonService.ExecuteSqlQuery("SELECT  DocumentDate, SupplierCode, SupplierName, CustomerCode, CustomerName, ProductCode, ProductName,"
                    + " DepartmentCode, DepartmentName, CategoryCode, CategoryName, SubCategoryCode, SubCategoryName, SubCategory2ID, SubCategory2Code, SubCategory2Name,"
                    + " Ext1, Ext2, Ext3, Ext4, Ext5,"
                    + " CostPrice, SellingPrice" + stValues + stQtys
                    + " FROM InvTmpReportDetail WHERE UserId =" + Common.LoggedUserId.ToString().Trim() + "");

                    if (rbtnAmount.Checked)
                    {
                        InvRptSubCategory2WiseAmountExt invRptSubCategory2WiseAmountExt = new InvRptSubCategory2WiseAmountExt();
                        invRptSubCategory2WiseAmountExt.SetDataSource(dtReportData);
                        invRptSubCategory2WiseAmountExt.SummaryInfo.ReportTitle = "SubCategory2 Wise Extended Property Wise " + reportTitle;
                        invRptSubCategory2WiseAmountExt.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptSubCategory2WiseAmountExt.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptSubCategory2WiseAmountExt.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptSubCategory2WiseAmountExt.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptSubCategory2WiseAmountExt.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptSubCategory2WiseAmountExt.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptSubCategory2WiseAmountExt.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptSubCategory2WiseAmountExt.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptSubCategory2WiseAmountExt.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptSubCategory2WiseAmountExt.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }
                        //InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                        //for (int i = 0; i < dtExtendedProperty.Rows.Count; i++)
                        //{
                        //    if (i >= 5) // limit Value columns to 5
                        //    { break; }
                        //    var ExtendedPropertyName = object.Equals(invProductExtendedPropertyService.GetInvProductExtendedPropertyById(int.Parse(dtExtendedProperty.Rows[i]["InvProductExtendedPropertyID"].ToString())).ExtendedPropertyName.Trim(),null)?"":invProductExtendedPropertyService.GetInvProductExtendedPropertyById(int.Parse(dtExtendedProperty.Rows[i]["InvProductExtendedPropertyID"].ToString())).ExtendedPropertyName.Trim();
                        //    invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["Ext" + (i + 1).ToString()].Text = "'" + ExtendedPropertyName.ToString() + "'";


                        //}

                        reportViewer.crRptViewer.ReportSource = invRptSubCategory2WiseAmountExt;
                    }
                    if (rbtnQuantity.Checked)
                    {
                        InvRptSubCategory2WiseQtyExt invRptSubCategory2WiseQtyExt = new InvRptSubCategory2WiseQtyExt();
                        invRptSubCategory2WiseQtyExt.SetDataSource(dtReportData);
                        invRptSubCategory2WiseQtyExt.SummaryInfo.ReportTitle = "SubCategory2 Wise Extended Property Wise " + reportTitle;
                        invRptSubCategory2WiseQtyExt.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptSubCategory2WiseQtyExt.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptSubCategory2WiseQtyExt.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptSubCategory2WiseQtyExt.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptSubCategory2WiseQtyExt.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptSubCategory2WiseQtyExt.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptSubCategory2WiseQtyExt.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptSubCategory2WiseQtyExt.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptSubCategory2WiseQtyExt.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptSubCategory2WiseQtyExt.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }


                        reportViewer.crRptViewer.ReportSource = invRptSubCategory2WiseQtyExt;
                    }
                }
                #endregion

                else
                {

                    dtReportData = CommonService.ExecuteSqlQuery("SELECT  DocumentDate, SupplierCode, SupplierName, CustomerCode, CustomerName, ProductCode, ProductName,"
                    + " SubCategory2ID, SubCategory2Code, SubCategory2Name,"
                    + " CostPrice, SellingPrice" + stValues + stQtys
                    + " FROM InvTmpReportDetail WHERE UserId =" + Common.LoggedUserId.ToString().Trim() + "");

                    if (rbtnAmount.Checked)
                    {
                        InvRptSubCategory2WiseAmount invRptSubCategory2WiseAmount = new InvRptSubCategory2WiseAmount();
                        invRptSubCategory2WiseAmount.SetDataSource(dtReportData);
                        invRptSubCategory2WiseAmount.SummaryInfo.ReportTitle = "Sub Category 2 Wise " + reportTitle;
                        invRptSubCategory2WiseAmount.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptSubCategory2WiseAmount.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptSubCategory2WiseAmount.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptSubCategory2WiseAmount.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptSubCategory2WiseAmount.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptSubCategory2WiseAmount.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptSubCategory2WiseAmount.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptSubCategory2WiseAmount.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptSubCategory2WiseAmount.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptSubCategory2WiseAmount.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }
                        reportViewer.crRptViewer.ReportSource = invRptSubCategory2WiseAmount;
                    }
                    if (rbtnQuantity.Checked)
                    {
                        InvRptSubCategory2WiseQty invRptSubCategory2WiseQty = new InvRptSubCategory2WiseQty();
                        invRptSubCategory2WiseQty.SetDataSource(dtReportData);
                        invRptSubCategory2WiseQty.SummaryInfo.ReportTitle = "Sub Category 2 Wise " + reportTitle;
                        invRptSubCategory2WiseQty.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                        invRptSubCategory2WiseQty.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                        invRptSubCategory2WiseQty.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                        invRptSubCategory2WiseQty.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                        invRptSubCategory2WiseQty.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                        invRptSubCategory2WiseQty.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                        invRptSubCategory2WiseQty.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                        invRptSubCategory2WiseQty.DataDefinition.FormulaFields["FromRange"].Text = "''";
                        invRptSubCategory2WiseQty.DataDefinition.FormulaFields["ToRange"].Text = "''";
                        // Arrange report header locations
                        LocationService locationService = new LocationService();
                        for (int i = 0; i < dtLocations.Rows.Count; i++)
                        {
                            if (i >= 25) // limit Value columns to 25
                            { break; }
                            var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                            invRptSubCategory2WiseQty.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                        }
                        reportViewer.crRptViewer.ReportSource = invRptSubCategory2WiseQty;
                    }
                }
            }
            #endregion

            #region ExtendedProperty
            if (chkExtendedProperty.Checked.Equals(true) && chkReportType.Checked.Equals(false) && chkDepartment.Checked.Equals(false) && chkCategory.Checked.Equals(false) && chkSubCategory.Checked.Equals(false) && chkSubCategory2.Checked.Equals(false)) // SubCat only
            {
                dtReportData = CommonService.ExecuteSqlQuery("SELECT  DocumentDate, SupplierCode, SupplierName, CustomerCode, CustomerName, ProductCode, ProductName,"
                + " DepartmentCode, DepartmentName, CategoryCode, CategoryName, SubCategoryCode, SubCategoryName, SubCategory2ID, SubCategory2Code, SubCategory2Name,"
                + " Ext1, Ext2, Ext3, Ext4, Ext5,"
                + " CostPrice, SellingPrice" + stValues + stQtys
                + " FROM InvTmpReportDetail WHERE UserId =" + Common.LoggedUserId.ToString().Trim() + "");

                if (rbtnAmount.Checked)
                {
                    InvRptDepartmentWiseAmountExt invRptDepartmentWiseAmountExt = new InvRptDepartmentWiseAmountExt();
                    invRptDepartmentWiseAmountExt.SetDataSource(dtReportData);
                    invRptDepartmentWiseAmountExt.SummaryInfo.ReportTitle = "Extended Property Wise " + reportTitle;
                    invRptDepartmentWiseAmountExt.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                    invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                    invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["FromRange"].Text = "''";
                    invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["ToRange"].Text = "''";
                    // Arrange report header locations
                    LocationService locationService = new LocationService();
                    for (int i = 0; i < dtLocations.Rows.Count; i++)
                    {
                        if (i >= 25) // limit Value columns to 25
                        { break; }
                        var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                        invRptDepartmentWiseAmountExt.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                    }
                    //InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                    //for (int i = 0; i < dtExtendedProperty.Rows.Count; i++)
                    //{
                    //    if (i >= 5) // limit Value columns to 5
                    //    { break; }
                    //    var ExtendedPropertyName = object.Equals(invProductExtendedPropertyService.GetInvProductExtendedPropertyById(int.Parse(dtExtendedProperty.Rows[i]["InvProductExtendedPropertyID"].ToString())).ExtendedPropertyName.Trim(),null)?"":invProductExtendedPropertyService.GetInvProductExtendedPropertyById(int.Parse(dtExtendedProperty.Rows[i]["InvProductExtendedPropertyID"].ToString())).ExtendedPropertyName.Trim();
                    //    invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["Ext" + (i + 1).ToString()].Text = "'" + ExtendedPropertyName.ToString() + "'";

                     
                    //}

                    reportViewer.crRptViewer.ReportSource = invRptDepartmentWiseAmountExt;
                }
                if (rbtnQuantity.Checked)
                {
                    InvRptDepartmentWiseQtyExt invRptDepartmentWiseQtyExt = new InvRptDepartmentWiseQtyExt();
                    invRptDepartmentWiseQtyExt.SetDataSource(dtReportData);
                    invRptDepartmentWiseQtyExt.SummaryInfo.ReportTitle = "Extended Property Wise " + reportTitle;
                    invRptDepartmentWiseQtyExt.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom.ToShortDateString() + "'";
                    invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo.ToShortDateString() + "'";
                    invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["FromRange"].Text = "''";
                    invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["ToRange"].Text = "''";
                    // Arrange report header locations
                    LocationService locationService = new LocationService();
                    for (int i = 0; i < dtLocations.Rows.Count; i++)
                    {
                        if (i >= 25) // limit Value columns to 25
                        { break; }
                        var locationPrefixCode = object.Equals(locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode, null) ? "" : locationService.GetLocationsByID(int.Parse(dtLocations.Rows[i]["LocationID"].ToString())).LocationPrefixCode.Trim();
                        invRptDepartmentWiseQtyExt.DataDefinition.FormulaFields["Loca" + (i + 1).ToString()].Text = "'" + locationPrefixCode.ToString() + "'";
                    }
 

                    reportViewer.crRptViewer.ReportSource = invRptDepartmentWiseQtyExt;
                }
            }
            #endregion

            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;

            // Delete Temp Data
            ReportService reportService = new ReportService();
            if (!reportService.DeleteTempData())
            {
                Toast.Show("Error on deleting temp data", Toast.messageType.Information, Toast.messageAction.General);
                return;
            }
        }
        
        private bool ValidateSelection()
        {
            int deptSelect = 0, catSelect = 0, subCatSelect = 0, subCat2Select = 0, extendedPropSelect = 0, reportTypeSelect = 0;
            if (chkDepartment.Checked.Equals(true))
            { deptSelect = 1; }
            else { deptSelect = 0; }

            if (chkCategory.Checked.Equals(true))
            { catSelect = 1; }
            else { catSelect = 0; }

            if (chkSubCategory.Checked.Equals(true))
            { subCatSelect = 1; }
            else { subCatSelect = 0; }

            if (chkSubCategory2.Checked.Equals(true))
            { subCat2Select = 1; }
            else { subCat2Select = 0; }

            if (chkExtendedProperty.Checked.Equals(true))
            { extendedPropSelect = 1; }
            else { extendedPropSelect = 0; }

            if (chkReportType.Checked.Equals(true))
            { reportTypeSelect = 1; }
            else { reportTypeSelect = 0; }

            if ((deptSelect + catSelect + subCatSelect + subCat2Select + extendedPropSelect + reportTypeSelect) <= 2)
            { return true; }
            else { return false; }
        }

        private void bgWReport_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //InvProductMasterService invProductMasterService = new InvProductMasterService();
                //productNames = invProductMasterService.GetAllProductNames();
                //e.Result = productNames;
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }

        }

        private void bgWReport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void bgWReport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if ((e.Cancelled == true))
                {//Canceled 
                }
                else if (!(e.Error == null))
                {//Error
                }
                else
                {//Done
                    if (object.Equals(e.Result, null))
                    {
                        //pBoxProductsPreLoader.Visible = false;
                        return;
                    }
                    
                    //Common.SetAutoComplete(txtProductFrom, (string[])e.Result, true);
                    //Common.SetAutoComplete(txtProductTo, (string[])e.Result, true);
                    //Common.SetAutoBindRecords(cmbProductFrom, (string[])e.Result);
                    //Common.SetAutoBindRecords(cmbProductTo, (string[])e.Result);
                    //cmbProductFrom.DataSource = (string[])e.Result;
                    //cmbProductFrom.DisplayMember = "ProductName";
                    //pBoxProductsPreLoader.Visible = false;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void rbtnDetails_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnDetails.Checked)
                {
                    rbtnCostValue.Visible = true;
                    rbtnSaleValue.Visible = true;
                }
                else
                {
                    rbtnCostValue.Visible = false;
                    rbtnSaleValue.Visible = false;
                    rbtnCostValue.Checked = false;
                    rbtnSaleValue.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void rbtnSummery_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnSummery.Checked)
                {
                    rbtnCostValue.Visible = false;
                    rbtnSaleValue.Visible = false;
                    rbtnCostValue.Checked = false;
                    rbtnSaleValue.Checked = false;
                }
                else
                {
                    rbtnCostValue.Visible = true;
                    rbtnSaleValue.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void rbtnLocationSelection_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnLocationSelection.Checked)
                {
                    chkLstLocations.Enabled = true;
                    txtLocations.Enabled = true;
                }
                else
                {
                    chkLstLocations.Enabled = false;
                    txtLocations.Enabled = false;
                }
            }
            catch (Exception ex)
            {Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void rbtnLocationRange_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnLocationRange.Checked)
                {
                    txtLocationFrom.Enabled = true;
                    txtLocationTo.Enabled = true;
                    chkAutoComplteLocationFrom.Enabled = true;
                    chkAutoComplteLocationTo.Enabled = true;
                }
                else
                {
                    txtLocationFrom.Enabled = false;
                    txtLocationTo.Enabled = false;
                    chkAutoComplteLocationFrom.Enabled = false;
                    chkAutoComplteLocationTo.Enabled = false;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkLstLocations_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var item = (CheckedListBox)sender;
                SetItemCheckedStatus(locationList, item.SelectedItem.ToString().Trim(), e.CurrentValue.Equals(CheckState.Checked) ? CheckState.Unchecked : CheckState.Checked);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtLocations_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.chkLstLocations.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstLocations_ItemCheck);
                if (string.IsNullOrEmpty(txtLocations.Text.Trim()))
                {
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstLocations, locationList, "", "");
                }
                else
                {
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstLocations, SearchList(locationList, txtLocations.Text.Trim()), "", "");
                }
                RefreshCheckedList(chkLstLocations, locationList);
                this.chkLstLocations.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstLocations_ItemCheck);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        
        }

        private void rbtnDepartmentSelection_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnDepartmentSelection.Checked)
                {
                    chkLstDepartment.Enabled = true;
                    txtDepartment.Enabled = true;
                }
                else
                {
                    chkLstDepartment.Enabled = false;
                    txtDepartment.Enabled = false;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkLstDepartment_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var item = (CheckedListBox)sender;
                SetItemCheckedStatus(departmentList, item.SelectedItem.ToString().Trim(), e.CurrentValue.Equals(CheckState.Checked) ? CheckState.Unchecked : CheckState.Checked);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtDepartment_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.chkLstDepartment.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstDepartment_ItemCheck);
                if (string.IsNullOrEmpty(txtDepartment.Text.Trim()))
                {
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstDepartment, departmentList, "", "");
                }
                else
                {
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstDepartment, SearchList(departmentList, txtDepartment.Text.Trim()), "", "");
                }
                RefreshCheckedList(chkLstDepartment, departmentList);
                this.chkLstDepartment.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstDepartment_ItemCheck);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        
        }

        private void rbtnDepartmentRange_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnDepartmentRange.Checked)
                {
                    txtDepartmentFrom.Enabled = true;
                    txtDepartmentTo.Enabled = true;
                    chkAutoComplteDepartmentFrom.Enabled = true;
                    chkAutoComplteDepartmentTo.Enabled = true;
                }
                else
                {
                    txtDepartmentFrom.Enabled = false;
                    txtDepartmentTo.Enabled = false;
                    chkAutoComplteDepartmentFrom.Enabled = false;
                    chkAutoComplteDepartmentTo.Enabled = false;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void rbtnCategorySelection_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnCategorySelection.Checked)
                {
                    chkLstCategory.Enabled = true;
                    txtCategory.Enabled = true;
                }
                else
                {
                    chkLstCategory.Enabled = false;
                    txtCategory.Enabled = false;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkLstCategory_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var item = (CheckedListBox)sender;
                SetItemCheckedStatus(categoryList, item.SelectedItem.ToString().Trim(), e.CurrentValue.Equals(CheckState.Checked) ? CheckState.Unchecked : CheckState.Checked);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtCategory_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.chkLstCategory.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstCategory_ItemCheck);
                if (string.IsNullOrEmpty(txtCategory.Text.Trim()))
                {
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstCategory, categoryList, "", "");
                }
                else
                {
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstCategory, SearchList(categoryList, txtCategory.Text.Trim()), "", "");
                }
                RefreshCheckedList(chkLstCategory, categoryList);
                this.chkLstCategory.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstCategory_ItemCheck);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        
        }

        private void rbtnCategoryRange_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnCategoryRange.Checked)
                {
                    txtCategoryFrom.Enabled = true;
                    txtCategoryTo.Enabled = true;
                    chkAutoComplteCategoryFrom.Enabled = true;
                    chkAutoComplteCategoryTo.Enabled = true;
                }
                else
                {
                    txtCategoryFrom.Enabled = false;
                    txtCategoryTo.Enabled = false;
                    chkAutoComplteCategoryFrom.Enabled = false;
                    chkAutoComplteCategoryTo.Enabled = false;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        
        }

        private void rbtnSubCategorySelection_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnSubCategorySelection.Checked)
                {
                    chkLstSubCategory.Enabled = true;
                    txtSubCategory.Enabled = true;
                }
                else
                {
                    chkLstSubCategory.Enabled = false;
                    txtSubCategory.Enabled = false;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkLstSubCategory_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var item = (CheckedListBox)sender;
                SetItemCheckedStatus(subCategoryList, item.SelectedItem.ToString().Trim(), e.CurrentValue.Equals(CheckState.Checked) ? CheckState.Unchecked : CheckState.Checked);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        
        }

        private void txtSubCategory_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.chkLstSubCategory.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstSubCategory_ItemCheck);
                if (string.IsNullOrEmpty(txtSubCategory.Text.Trim()))
                {
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstSubCategory, subCategoryList, "", "");
                }
                else
                {
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstSubCategory, SearchList(subCategoryList, txtSubCategory.Text.Trim()), "", "");
                }
                RefreshCheckedList(chkLstSubCategory, subCategoryList);
                this.chkLstSubCategory.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstSubCategory_ItemCheck);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void rbtnSubCategoryRange_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnSubCategoryRange.Checked)
                {
                    txtSubCategoryFrom.Enabled = true;
                    txtSubCategoryTo.Enabled = true;
                    chkAutoComplteSubCategoryFrom.Enabled = true;
                    chkAutoComplteSubCategoryTo.Enabled = true;
                }
                else
                {
                    txtSubCategoryFrom.Enabled = false;
                    txtSubCategoryTo.Enabled = false;
                    chkAutoComplteSubCategoryFrom.Enabled = false;
                    chkAutoComplteSubCategoryTo.Enabled = false;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        
        }

        private void rbtnSubCategory2Selection_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnSubCategory2Selection.Checked)
                {
                    chkLstSubCategory2.Enabled = true;
                    txtSubcategory2.Enabled = true;
                }
                else
                {
                    chkLstSubCategory2.Enabled = false;
                    txtSubcategory2.Enabled = false;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkLstSubCategory2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var item = (CheckedListBox)sender;
                SetItemCheckedStatus(subCategory2List, item.SelectedItem.ToString().Trim(), e.CurrentValue.Equals(CheckState.Checked) ? CheckState.Unchecked : CheckState.Checked);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        
        }

        private void txtSubcategory2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.chkLstSubCategory2.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstSubCategory2_ItemCheck);
                if (string.IsNullOrEmpty(txtSubcategory2.Text.Trim()))
                {
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstSubCategory2, subCategory2List, "", "");
                }
                else
                {
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstSubCategory2, SearchList(subCategory2List, txtSubcategory2.Text.Trim()), "", "");
                }
                RefreshCheckedList(chkLstSubCategory2, subCategory2List);
                this.chkLstSubCategory2.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstSubCategory2_ItemCheck);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void rbtnSubCategory2Range_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnSubCategory2Range.Checked)
                {
                    txtSubCategory2From.Enabled = true;
                    txtSubCategory2To.Enabled = true;
                    chkAutoComplteSubCategory2From.Enabled = true;
                    chkAutoComplteSubCategory2To.Enabled = true;
                }
                else
                {
                    txtSubCategory2From.Enabled = false;
                    txtSubCategory2To.Enabled = false;
                    chkAutoComplteSubCategory2From.Enabled = false;
                    chkAutoComplteSubCategory2To.Enabled = false;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        
        }

        private void chkLstProductExtendedProperties_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var item = (CheckedListBox)sender;
                SetItemCheckedStatus(productExtendedPropertiesList, item.SelectedItem.ToString().Trim(), e.CurrentValue.Equals(CheckState.Checked) ? CheckState.Unchecked : CheckState.Checked);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductExtendedProperties_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.chkLstProductExtendedProperties.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstProductExtendedProperties_ItemCheck);
                if (string.IsNullOrEmpty(txtProductExtendedProperties.Text.Trim()))
                {
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstProductExtendedProperties, productExtendedPropertiesList, "", "");
                }
                else
                {
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstProductExtendedProperties, SearchList(productExtendedPropertiesList, txtProductExtendedProperties.Text.Trim()), "", "");
                }
                RefreshCheckedList(chkLstProductExtendedProperties, productExtendedPropertiesList);
                this.chkLstProductExtendedProperties.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstProductExtendedProperties_ItemCheck);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoComplteProductFrom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAutoList(txtProductCodeFrom, chkAutoComplteProductFrom.Checked, productCodes);
                LoadAutoList(txtProductNameFrom, chkAutoComplteProductFrom.Checked, productNames);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoComplteProductTo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAutoList(txtProductCodeTo, chkAutoComplteProductTo.Checked, productCodes);
                LoadAutoList(txtProductNameTo, chkAutoComplteProductTo.Checked, productNames);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void rbtnReportTypeRange_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnReportTypeRange.Checked)
                {
                    txtReportTypeFrom.Enabled = true;
                    txtReportTypeTo.Enabled = true;
                    chkAutoComplteReportTypeFrom.Enabled = true;
                    chkAutoComplteReportTypeTo.Enabled = true;
                }
                else
                {
                    txtReportTypeFrom.Enabled = false;
                    txtReportTypeTo.Enabled = false;
                    chkAutoComplteReportTypeFrom.Enabled = false;
                    chkAutoComplteReportTypeTo.Enabled = false;
                }
                
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void rbtnReportTypeSelection_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnReportTypeSelection.Checked)
                {
                    chkLstReportType.Enabled = true;
                    txtReportType.Enabled = true;
                }
                else
                {
                    chkLstReportType.Enabled = false;
                    txtReportType.Enabled = false;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); } 
        }

        private void chkAutoComplteLocationFrom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAutoList(txtLocationFrom, chkAutoComplteLocationFrom.Checked, locationNames);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoComplteLocationTo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAutoList(txtLocationTo, chkAutoComplteLocationTo.Checked, locationNames);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoComplteDepartmentFrom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAutoList(txtDepartmentFrom, chkAutoComplteDepartmentFrom.Checked, departmentNames);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoComplteDepartmentTo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAutoList(txtDepartmentTo, chkAutoComplteDepartmentTo.Checked, departmentNames);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoComplteCategoryFrom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAutoList(txtCategoryFrom, chkAutoComplteCategoryFrom.Checked, categoryNames);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoComplteCategoryTo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAutoList(txtCategoryTo, chkAutoComplteCategoryTo.Checked, categoryNames);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoComplteSubCategoryFrom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAutoList(txtSubCategoryFrom, chkAutoComplteSubCategoryFrom.Checked, subCategoryNames);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoComplteSubCategoryTo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAutoList(txtSubCategoryTo, chkAutoComplteSubCategoryTo.Checked, subCategoryNames);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoComplteSubCategory2From_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAutoList(txtSubCategory2From, chkAutoComplteSubCategory2From.Checked, subCategory2Names);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoComplteSubCategory2To_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAutoList(txtSubCategory2To, chkAutoComplteSubCategory2To.Checked, subCategory2Names);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoComplteReportTypeFrom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnSupplier.Checked)
                {
                    LoadAutoList(txtReportTypeFrom, chkAutoComplteReportTypeFrom.Checked, supplierNames);
                }
                if (rbtnCustomer.Checked)
                {
                    LoadAutoList(txtReportTypeFrom, chkAutoComplteReportTypeFrom.Checked, customerNames);
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoComplteReportTypeTo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnSupplier.Checked)
                {
                    LoadAutoList(txtReportTypeTo, chkAutoComplteReportTypeTo.Checked, supplierNames);
                }
                if (rbtnCustomer.Checked)
                {
                    LoadAutoList(txtReportTypeTo, chkAutoComplteReportTypeTo.Checked, customerNames);
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductCodeFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProductCodeFrom.Text.Trim()))
                { return; }

                if (chkAutoComplteProductFrom.Checked.Equals(false) && string.Equals(txtProductCodeFrom.Text.Trim(), "0"))
                {
                    return;
                }

                InvProductMasterService invProductMasterService = new InvProductMasterService();
                InvProductMaster invProductMaster = new InvProductMaster();

                invProductMaster = invProductMasterService.GetProductsByCode(txtProductCodeFrom.Text.Trim());
                if (invProductMaster != null)
                {
                    txtProductCodeFrom.Text = invProductMaster.ProductCode.Trim();
                    txtProductNameFrom.Text = invProductMaster.ProductName.Trim();
                }
                else
                {
                    Toast.Show("Product from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    txtProductNameFrom.Text = string.Empty;
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductCodeFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                if (string.IsNullOrEmpty(txtProductCodeFrom.Text.Trim()))
                { return; }

                if (chkAutoComplteProductFrom.Checked.Equals(false) && string.Equals(txtProductCodeFrom.Text.Trim(), "0"))
                {
                    return;
                }

                InvProductMasterService invProductMasterService = new InvProductMasterService();
                InvProductMaster invProductMaster = new InvProductMaster();

                invProductMaster = invProductMasterService.GetProductsByCode(txtProductCodeFrom.Text.Trim());
                if (invProductMaster != null)
                {
                    txtProductCodeFrom.Text = invProductMaster.ProductCode.Trim();
                    txtProductNameFrom.Text = invProductMaster.ProductName.Trim();
                }
                else
                {
                    Toast.Show("Product from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    txtProductNameFrom.Text = string.Empty;
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductNameFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProductNameFrom.Text.Trim()))
                { return; }

                InvProductMasterService invProductMasterService = new InvProductMasterService();
                InvProductMaster invProductMaster = new InvProductMaster();

                invProductMaster = invProductMasterService.GetProductsByName(txtProductNameFrom.Text.Trim());
                if (invProductMaster != null)
                {
                    txtProductCodeFrom.Text = invProductMaster.ProductCode.Trim();
                    txtProductNameFrom.Text = invProductMaster.ProductName.Trim();
                }
                else
                {
                    Toast.Show("Product from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    txtProductCodeFrom.Text = string.Empty;
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductNameFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.Equals(Keys.Enter))
            { return; }

            if (string.IsNullOrEmpty(txtProductNameFrom.Text.Trim()))
            { return; }

            InvProductMasterService invProductMasterService = new InvProductMasterService();
            InvProductMaster invProductMaster = new InvProductMaster();

            invProductMaster = invProductMasterService.GetProductsByName(txtProductNameFrom.Text.Trim());
            if (invProductMaster != null)
            {
                txtProductCodeFrom.Text = invProductMaster.ProductCode.Trim();
                txtProductNameFrom.Text = invProductMaster.ProductName.Trim();
            }
            else
            {
                Toast.Show("Product from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                txtProductCodeFrom.Text = string.Empty;
                return;
            }
        }

        private void txtProductCodeTo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProductCodeTo.Text.Trim()))
                { return; }

                if (chkAutoComplteProductTo.Checked.Equals(false) && string.Equals(txtProductCodeTo.Text.Trim().ToLower(), "z"))
                {
                    return;
                }

                InvProductMasterService invProductMasterService = new InvProductMasterService();
                InvProductMaster invProductMaster = new InvProductMaster();

                invProductMaster = invProductMasterService.GetProductsByCode(txtProductCodeTo.Text.Trim());
                if (invProductMaster != null)
                {
                    txtProductCodeTo.Text = invProductMaster.ProductCode.Trim();
                    txtProductNameTo.Text = invProductMaster.ProductName.Trim();
                }
                else
                {
                    Toast.Show("Product to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    txtProductNameTo.Text = string.Empty;
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductCodeTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.Equals(Keys.Enter))
            { return; }

            if (string.IsNullOrEmpty(txtProductCodeTo.Text.Trim()))
            { return; }

            if (chkAutoComplteProductTo.Checked.Equals(false) && string.Equals(txtProductCodeTo.Text.Trim().ToLower(), "z"))
            {
                return;
            }

            InvProductMasterService invProductMasterService = new InvProductMasterService();
            InvProductMaster invProductMaster = new InvProductMaster();

            invProductMaster = invProductMasterService.GetProductsByCode(txtProductCodeTo.Text.Trim());
            if (invProductMaster != null)
            {
                txtProductCodeTo.Text = invProductMaster.ProductCode.Trim();
                txtProductNameTo.Text = invProductMaster.ProductName.Trim();
            }
            else
            {
                Toast.Show("Product to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                txtProductNameTo.Text = string.Empty;
                return;
            }
        }

        private void txtProductNameTo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProductNameTo.Text.Trim()))
                { return; }

                InvProductMasterService invProductMasterService = new InvProductMasterService();
                InvProductMaster invProductMaster = new InvProductMaster();

                invProductMaster = invProductMasterService.GetProductsByName(txtProductNameTo.Text.Trim());
                if (invProductMaster != null)
                {
                    txtProductCodeTo.Text = invProductMaster.ProductCode.Trim();
                    txtProductNameTo.Text = invProductMaster.ProductName.Trim();
                }
                else
                {
                    Toast.Show("Product to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    txtProductCodeTo.Text = string.Empty;
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductNameTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.Equals(Keys.Enter))
            { return; }

            if (string.IsNullOrEmpty(txtProductNameTo.Text.Trim()))
            { return; }

            InvProductMasterService invProductMasterService = new InvProductMasterService();
            InvProductMaster invProductMaster = new InvProductMaster();

            invProductMaster = invProductMasterService.GetProductsByName(txtProductNameTo.Text.Trim());
            if (invProductMaster != null)
            {
                txtProductCodeTo.Text = invProductMaster.ProductCode.Trim();
                txtProductNameTo.Text = invProductMaster.ProductName.Trim();
            }
            else
            {
                Toast.Show("Product to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                txtProductCodeTo.Text = string.Empty;
                return;
            }
        }

        private void txtLocationFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                if (string.IsNullOrEmpty(txtLocationFrom.Text.Trim()))
                { return; }

                if (chkAutoComplteLocationFrom.Checked.Equals(false) && string.Equals(txtLocationFrom.Text.Trim(), "0"))
                {
                    return;
                }

                LocationService locationService = new LocationService();
                Location location = new Location();

                location = locationService.GetLocationsByName(txtLocationFrom.Text.Trim());
                if (location != null)
                {
                    txtLocationFrom.Text = location.LocationName.Trim();
                }
                else
                {
                    Toast.Show("Location from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtLocationFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLocationFrom.Text.Trim()))
                { return; }

                if (chkAutoComplteLocationFrom.Checked.Equals(false) && string.Equals(txtLocationFrom.Text.Trim(), "0"))
                {
                    return;
                }

                LocationService locationService = new LocationService();
                Location location = new Location();

                location = locationService.GetLocationsByName(txtLocationFrom.Text.Trim());
                if (location != null)
                {
                    txtLocationFrom.Text = location.LocationName.Trim();
                }
                else
                {
                    Toast.Show("Location from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtLocationTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.Equals(Keys.Enter))
            { return; }

            if (string.IsNullOrEmpty(txtLocationTo.Text.Trim()))
            { return; }

            if (chkAutoComplteLocationTo.Checked.Equals(false) && string.Equals(txtLocationTo.Text.Trim().ToLower(), "z"))
            { return; }

            LocationService locationService = new LocationService();
            Location location = new Location();

            location = locationService.GetLocationsByName(txtLocationTo.Text.Trim());
            if (location != null)
            {
                txtLocationTo.Text = location.LocationName.Trim();
            }
            else
            {
                Toast.Show("Location to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                return;
            }
        }

        private void txtLocationTo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLocationTo.Text.Trim()))
            { return; }

            if (chkAutoComplteLocationTo.Checked.Equals(false) && string.Equals(txtLocationTo.Text.Trim().ToLower(), "z"))
            {
                return;
            }

            LocationService locationService = new LocationService();
            Location location = new Location();

            location = locationService.GetLocationsByName(txtLocationTo.Text.Trim());
            if (location != null)
            {
                txtLocationTo.Text = location.LocationName.Trim();
            }
            else
            {
                Toast.Show("Location to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                return;
            }
        }

        private void txtDepartmentFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                if (string.IsNullOrEmpty(txtDepartmentFrom.Text.Trim()))
                { return; }

                if (chkAutoComplteDepartmentFrom.Checked.Equals(false) && string.Equals(txtDepartmentFrom.Text.Trim(), "0"))
                {
                    return;
                }

                InvDepartmentService invDepartmentService = new InvDepartmentService();
                InvDepartment invDepartment = new InvDepartment();

                invDepartment = invDepartmentService.GetInvDepartmentsByName(txtDepartmentFrom.Text.Trim(), AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").IsDepend);
                if (invDepartment != null)
                {
                    txtDepartmentFrom.Text = invDepartment.DepartmentName.Trim();
                }
                else
                {
                    Toast.Show("Department from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void txtDepartmentFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDepartmentFrom.Text.Trim()))
                { return; }

                if (chkAutoComplteDepartmentFrom.Checked.Equals(false) && string.Equals(txtDepartmentFrom.Text.Trim(), "0"))
                {
                    return;
                }

                InvDepartmentService invDepartmentService = new InvDepartmentService();
                InvDepartment invDepartment = new InvDepartment();

                invDepartment = invDepartmentService.GetInvDepartmentsByName(txtDepartmentFrom.Text.Trim(), AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").IsDepend);
                if (invDepartment != null)
                {
                    txtDepartmentFrom.Text = invDepartment.DepartmentName.Trim();
                }
                else
                {
                    Toast.Show("Department from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void txtDepartmentTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                if (string.IsNullOrEmpty(txtDepartmentTo.Text.Trim()))
                { return; }

                if (chkAutoComplteDepartmentTo.Checked.Equals(false) && string.Equals(txtDepartmentTo.Text.Trim().ToLower(), "z"))
                {
                    return;
                }

                InvDepartmentService invDepartmentService = new InvDepartmentService();
                InvDepartment invDepartment = new InvDepartment();

                invDepartment = invDepartmentService.GetInvDepartmentsByName(txtDepartmentTo.Text.Trim(), AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").IsDepend);
                if (invDepartment != null)
                {
                    txtDepartmentTo.Text = invDepartment.DepartmentName.Trim();
                }
                else
                {
                    Toast.Show("Department to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void txtDepartmentTo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDepartmentTo.Text.Trim()))
                { return; }

                if (chkAutoComplteDepartmentTo.Checked.Equals(false) && string.Equals(txtDepartmentTo.Text.Trim().ToLower(), "z"))
                {
                    return;
                }

                InvDepartmentService invDepartmentService = new InvDepartmentService();
                InvDepartment invDepartment = new InvDepartment();

                invDepartment = invDepartmentService.GetInvDepartmentsByName(txtDepartmentTo.Text.Trim(), AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").IsDepend);
                if (invDepartment != null)
                {
                    txtDepartmentTo.Text = invDepartment.DepartmentName.Trim();
                }
                else
                {
                    Toast.Show("Department to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void txtCategoryFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                if (string.IsNullOrEmpty(txtCategoryFrom.Text.Trim()))
                { return; }

                if (chkAutoComplteCategoryFrom.Checked.Equals(false) && string.Equals(txtCategoryFrom.Text.Trim(), "0"))
                { return; }

                InvCategoryService invCategoryService = new InvCategoryService();
                InvCategory invCategory = new InvCategory();

                invCategory = invCategoryService.GetInvCategoryByName(txtCategoryFrom.Text.Trim(), AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend);
                if (invCategory != null)
                {
                    txtCategoryFrom.Text = invCategory.CategoryName.Trim();
                }
                else
                {
                    Toast.Show("Category to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void txtCategoryFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCategoryFrom.Text.Trim()))
                { return; }

                if (chkAutoComplteCategoryFrom.Checked.Equals(false) && string.Equals(txtCategoryFrom.Text.Trim(), "0"))
                { return; }

                InvCategoryService invCategoryService = new InvCategoryService();
                InvCategory invCategory = new InvCategory();

                invCategory = invCategoryService.GetInvCategoryByName(txtCategoryFrom.Text.Trim(), AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend);
                if (invCategory != null)
                {
                    txtCategoryFrom.Text = invCategory.CategoryName.Trim();
                }
                else
                {
                    Toast.Show("Category from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void txtCategoryTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                if (string.IsNullOrEmpty(txtCategoryTo.Text.Trim()))
                { return; }

                if (chkAutoComplteCategoryTo.Checked.Equals(false) && string.Equals(txtCategoryTo.Text.Trim().ToLower(), "z"))
                { return; }

                InvCategoryService invCategoryService = new InvCategoryService();
                InvCategory invCategory = new InvCategory();

                invCategory = invCategoryService.GetInvCategoryByName(txtCategoryTo.Text.Trim(), AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend);
                if (invCategory != null)
                {
                    txtCategoryTo.Text = invCategory.CategoryName.Trim();
                }
                else
                {
                    Toast.Show("Category to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void txtCategoryTo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCategoryTo.Text.Trim()))
                { return; }

                if (chkAutoComplteCategoryTo.Checked.Equals(false) && string.Equals(txtCategoryTo.Text.Trim().ToLower(), "z"))
                { return; }

                InvCategoryService invCategoryService = new InvCategoryService();
                InvCategory invCategory = new InvCategory();

                invCategory = invCategoryService.GetInvCategoryByName(txtCategoryTo.Text.Trim(), AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend);
                if (invCategory != null)
                {
                    txtCategoryTo.Text = invCategory.CategoryName.Trim();
                }
                else
                {
                    Toast.Show("Category to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void txtSubCategoryFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                if (string.IsNullOrEmpty(txtSubCategoryFrom.Text.Trim()))
                { return; }

                if (chkAutoComplteSubCategoryFrom.Checked.Equals(false) && string.Equals(txtSubCategoryFrom.Text.Trim(), "0"))
                { return; }

                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                InvSubCategory invSubCategory = new InvSubCategory();

                invSubCategory = invSubCategoryService.GetInvSubCategoryByName(txtSubCategoryFrom.Text.Trim(), AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend);
                if (invSubCategory != null)
                {
                    txtSubCategoryFrom.Text = invSubCategory.SubCategoryName.Trim();
                }
                else
                {
                    Toast.Show("Sub Category from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void txtSubCategoryFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSubCategoryFrom.Text.Trim()))
                { return; }

                if (chkAutoComplteSubCategoryFrom.Checked.Equals(false) && string.Equals(txtSubCategoryFrom.Text.Trim(), "0"))
                { return; }

                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                InvSubCategory invSubCategory = new InvSubCategory();

                invSubCategory = invSubCategoryService.GetInvSubCategoryByName(txtSubCategoryFrom.Text.Trim(), AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend);
                if (invSubCategory != null)
                {
                    txtSubCategoryFrom.Text = invSubCategory.SubCategoryName.Trim();
                }
                else
                {
                    Toast.Show("Sub Category from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void txtSubCategoryTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                if (string.IsNullOrEmpty(txtSubCategoryTo.Text.Trim()))
                { return; }

                if (chkAutoComplteSubCategoryTo.Checked.Equals(false) && string.Equals(txtSubCategoryTo.Text.Trim().ToLower(), "z"))
                { return; }

                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                InvSubCategory invSubCategory = new InvSubCategory();

                invSubCategory = invSubCategoryService.GetInvSubCategoryByName(txtSubCategoryTo.Text.Trim(), AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend);
                if (invSubCategory != null)
                {
                    txtSubCategoryTo.Text = invSubCategory.SubCategoryName.Trim();
                }
                else
                {
                    Toast.Show("Sub Category to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void txtSubCategoryTo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSubCategoryTo.Text.Trim()))
                { return; }

                if (chkAutoComplteSubCategoryTo.Checked.Equals(false) && string.Equals(txtSubCategoryTo.Text.Trim().ToLower(), "z"))
                { return; }

                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                InvSubCategory invSubCategory = new InvSubCategory();

                invSubCategory = invSubCategoryService.GetInvSubCategoryByName(txtSubCategoryTo.Text.Trim(), AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend);
                if (invSubCategory != null)
                {
                    txtSubCategoryTo.Text = invSubCategory.SubCategoryName.Trim();
                }
                else
                {
                    Toast.Show("Sub Category to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtSubCategory2From_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                if (string.IsNullOrEmpty(txtSubCategory2From.Text.Trim()))
                { return; }

                if (chkAutoComplteSubCategory2From.Checked.Equals(false) && string.Equals(txtSubCategory2From.Text.Trim(), "0"))
                { return; }

                InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                InvSubCategory2 invSubCategory2 = new InvSubCategory2();

                invSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByName(txtSubCategory2From.Text.Trim());
                if (invSubCategory2 != null)
                {
                    txtSubCategory2From.Text = invSubCategory2.SubCategory2Name.Trim();
                }
                else
                {
                    Toast.Show("Sub Category 2 from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtSubCategory2From_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSubCategory2From.Text.Trim()))
                { return; }

                if (chkAutoComplteSubCategory2From.Checked.Equals(false) && string.Equals(txtSubCategory2From.Text.Trim(), "0"))
                { return; }

                InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                InvSubCategory2 invSubCategory2 = new InvSubCategory2();

                invSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByName(txtSubCategory2From.Text.Trim());
                if (invSubCategory2 != null)
                {
                    txtSubCategory2From.Text = invSubCategory2.SubCategory2Name.Trim();
                }
                else
                {
                    Toast.Show("Sub Category 2 from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtSubCategory2To_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                if (string.IsNullOrEmpty(txtSubCategory2To.Text.Trim()))
                { return; }

                if (chkAutoComplteSubCategory2To.Checked.Equals(false) && string.Equals(txtSubCategory2To.Text.Trim().ToLower(), "z"))
                { return; }

                InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                InvSubCategory2 invSubCategory2 = new InvSubCategory2();

                invSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByName(txtSubCategory2To.Text.Trim());
                if (invSubCategory2 != null)
                {
                    txtSubCategory2To.Text = invSubCategory2.SubCategory2Name.Trim();
                }
                else
                {
                    Toast.Show("Sub Category 2 to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
      
        }

        private void txtSubCategory2To_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSubCategory2To.Text.Trim()))
                { return; }

                if (chkAutoComplteSubCategory2To.Checked.Equals(false) && string.Equals(txtSubCategory2To.Text.Trim().ToLower(), "z"))
                { return; }

                InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                InvSubCategory2 invSubCategory2 = new InvSubCategory2();

                invSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByName(txtSubCategory2To.Text.Trim());
                if (invSubCategory2 != null)
                {
                    txtSubCategory2To.Text = invSubCategory2.SubCategory2Name.Trim();
                }
                else
                {
                    Toast.Show("Sub Category 2 to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
      
        }

        private void rbtnSupplier_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnSupplier.Checked)
                {
                    if (rbtnReportTypeSelection.Checked)
                    {
                        Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstReportType, suppliersList, "", "");
                    }
                    else
                    {
                        chkLstReportType.DataSource = null;
                        chkLstReportType.Items.Clear();
                    }

                    if (rbtnReportTypeRange.Checked)
                    {
                        txtReportTypeFrom.Enabled = true;
                        txtReportTypeTo.Enabled = true;
                        chkAutoComplteReportTypeFrom.Enabled = true;
                        chkAutoComplteReportTypeTo.Enabled = true;
                    }
                    else
                    {
                        txtReportTypeFrom.Enabled = false;
                        txtReportTypeTo.Enabled = false;
                        chkAutoComplteReportTypeFrom.Enabled = false;
                        chkAutoComplteReportTypeTo.Enabled = false;
                    }
                }
                else
                {
                    chkLstReportType.DataSource = null;
                    chkLstReportType.Items.Clear();
                    txtReportTypeFrom.Enabled = false;
                    txtReportTypeTo.Enabled = false;
                    chkAutoComplteReportTypeFrom.Enabled = false;
                    chkAutoComplteReportTypeTo.Enabled = false;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
      
        }

        private void rbtnCustomer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnCustomer.Checked)
                {
                    if (rbtnReportTypeSelection.Checked)
                    {
                        Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstReportType, customersList, "", "");
                    }
                    else
                    {
                        chkLstReportType.DataSource = null;
                        chkLstReportType.Items.Clear();
                    }

                    if (rbtnReportTypeRange.Checked)
                    {
                        txtReportTypeFrom.Enabled = true;
                        txtReportTypeTo.Enabled = true;
                        chkAutoComplteReportTypeFrom.Enabled = true;
                        chkAutoComplteReportTypeTo.Enabled = true;
                    }
                    else
                    {
                        txtReportTypeFrom.Enabled = false;
                        txtReportTypeTo.Enabled = false;
                        chkAutoComplteReportTypeFrom.Enabled = false;
                        chkAutoComplteReportTypeTo.Enabled = false;
                    }
                }
                else
                {
                    chkLstReportType.DataSource = null;
                    chkLstReportType.Items.Clear();
                    txtReportTypeFrom.Enabled = false;
                    txtReportTypeTo.Enabled = false;
                    chkAutoComplteReportTypeFrom.Enabled = false;
                    chkAutoComplteReportTypeTo.Enabled = false;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
      
        }

        private void chkLstReportType_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var item = (CheckedListBox)sender;
                if (rbtnSupplier.Checked)
                {
                    SetItemCheckedStatus(suppliersList, item.SelectedItem.ToString().Trim(), e.CurrentValue.Equals(CheckState.Checked) ? CheckState.Unchecked : CheckState.Checked);
                }
                if (rbtnCustomer.Checked)
                {
                    SetItemCheckedStatus(customersList, item.SelectedItem.ToString().Trim(), e.CurrentValue.Equals(CheckState.Checked) ? CheckState.Unchecked : CheckState.Checked);
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        
        }

        private void txtReportType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.chkLstSubCategory2.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstSubCategory2_ItemCheck);
                if (string.IsNullOrEmpty(txtReportType.Text.Trim()))
                {
                    if (rbtnSupplier.Checked)
                    {
                        Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstReportType, suppliersList, "", "");
                    }
                    if (rbtnCustomer.Checked)
                    {
                        Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstReportType, customersList, "", "");
                    }
                }
                else
                {
                    if (rbtnSupplier.Checked)
                    {
                        Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstReportType, SearchList(suppliersList, txtReportType.Text.Trim()), "", "");
                    }
                    if (rbtnCustomer.Checked)
                    {
                        Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstReportType, SearchList(customersList, txtReportType.Text.Trim()), "", ""); 
                    }
                }
                if (rbtnSupplier.Checked)
                {
                    RefreshCheckedList(chkLstReportType, suppliersList); 
                }
                if (rbtnCustomer.Checked)
                {
                    RefreshCheckedList(chkLstReportType, customersList); 
                }
                
                this.chkLstSubCategory2.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstSubCategory2_ItemCheck);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtReportTypeFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                if (string.IsNullOrEmpty(txtReportTypeFrom.Text.Trim()))
                { return; }

                if (chkAutoComplteReportTypeFrom.Checked.Equals(false) && string.Equals(txtReportTypeFrom.Text.Trim(), "0"))
                { return; }

                if (rbtnSupplier.Checked)
                {
                    SupplierService supplierService = new SupplierService();
                    Supplier supplier = new Supplier();

                    supplier = supplierService.GetSupplierByName(txtReportTypeFrom.Text.Trim());
                    if (supplier != null)
                    {
                        txtReportTypeFrom.Text = supplier.SupplierName.Trim();
                    }
                    else
                    {
                        Toast.Show("Supplier from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        return;
                    }
                }
                if (rbtnCustomer.Checked)
                {
                    CustomerService customerService = new CustomerService();
                    Customer customer = new Customer();

                    customer = customerService.GetCustomersByName(txtReportTypeFrom.Text.Trim());
                    if (customer != null)
                    {
                        txtReportTypeFrom.Text = customer.CustomerName.Trim();
                    }
                    else
                    {
                        Toast.Show("Customer from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        return;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtReportTypeFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtReportTypeFrom.Text.Trim()))
                { return; }

                if (chkAutoComplteReportTypeFrom.Checked.Equals(false) && string.Equals(txtReportTypeFrom.Text.Trim(), "0"))
                { return; }

                if (rbtnSupplier.Checked)
                {
                    SupplierService supplierService = new SupplierService();
                    Supplier supplier = new Supplier();

                    supplier = supplierService.GetSupplierByName(txtReportTypeFrom.Text.Trim());
                    if (supplier != null)
                    {
                        txtReportTypeFrom.Text = supplier.SupplierName.Trim();
                    }
                    else
                    {
                        Toast.Show("Supplier from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        return;
                    }
                }
                if (rbtnCustomer.Checked)
                {
                    CustomerService customerService = new CustomerService();
                    Customer customer = new Customer();

                    customer = customerService.GetCustomersByName(txtReportTypeFrom.Text.Trim());
                    if (customer != null)
                    {
                        txtReportTypeFrom.Text = customer.CustomerName.Trim();
                    }
                    else
                    {
                        Toast.Show("Customer from ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        return;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
      
        }

        private void txtReportTypeTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                if (string.IsNullOrEmpty(txtReportTypeTo.Text.Trim()))
                { return; }

                if (chkAutoComplteReportTypeTo.Checked.Equals(false) && string.Equals(txtReportTypeTo.Text.Trim().ToLower(), "z"))
                { return; }

                if (rbtnSupplier.Checked)
                {
                    SupplierService supplierService = new SupplierService();
                    Supplier supplier = new Supplier();

                    supplier = supplierService.GetSupplierByName(txtReportTypeTo.Text.Trim());
                    if (supplier != null)
                    {
                        txtReportTypeTo.Text = supplier.SupplierName.Trim();
                    }
                    else
                    {
                        Toast.Show("Supplier to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        return;
                    }
                }
                if (rbtnCustomer.Checked)
                {
                    CustomerService customerService = new CustomerService();
                    Customer customer = new Customer();

                    customer = customerService.GetCustomersByName(txtReportTypeTo.Text.Trim());
                    if (customer != null)
                    {
                        txtReportTypeTo.Text = customer.CustomerName.Trim();
                    }
                    else
                    {
                        Toast.Show("Customer to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        return;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtReportTypeTo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtReportTypeTo.Text.Trim()))
                { return; }

                if (chkAutoComplteReportTypeTo.Checked.Equals(false) && string.Equals(txtReportTypeTo.Text.Trim().ToLower(), "z"))
                { return; }

                if (rbtnSupplier.Checked)
                {
                    SupplierService supplierService = new SupplierService();
                    Supplier supplier = new Supplier();

                    supplier = supplierService.GetSupplierByName(txtReportTypeTo.Text.Trim());
                    if (supplier != null)
                    {
                        txtReportTypeTo.Text = supplier.SupplierName.Trim();
                    }
                    else
                    {
                        Toast.Show("Supplier to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        return;
                    }
                }
                if (rbtnCustomer.Checked)
                {
                    CustomerService customerService = new CustomerService();
                    Customer customer = new Customer();

                    customer = customerService.GetCustomersByName(txtReportTypeTo.Text.Trim());
                    if (customer != null)
                    {
                        txtReportTypeTo.Text = customer.CustomerName.Trim();
                    }
                    else
                    {
                        Toast.Show("Customer to ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        return;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        
        }

        private void grpDepartment_Enter(object sender, EventArgs e)
        {
            //try
            //{
            //    if (ValidateSelection())
            //    {
            //        grpDepartment.BackColor = selectedColor;
            //    }
            //    else
            //    { grpDepartment.BackColor = SystemColors.Control; }
                
            //}
            //catch (Exception ex)
            //{ Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void grpCategory_Enter(object sender, EventArgs e)
        {
            //try
            //{
            //    if (ValidateSelection())
            //    {
            //        grpCategory.BackColor = selectedColor;
            //    }
            //    else
            //    { grpCategory.BackColor = SystemColors.Control; }

            //}
            //catch (Exception ex)
            //{ Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void grpSubCategory_Enter(object sender, EventArgs e)
        {
            //try
            //{
            //    if (ValidateSelection())
            //    {
            //        grpSubCategory.BackColor = selectedColor;
            //    }
            //    else
            //    { grpSubCategory.BackColor = SystemColors.Control; }

            //}
            //catch (Exception ex)
            //{ Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkDepartment_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDepartment.Checked)
                {
                    if (ValidateSelection())
                    {
                        grpDepartment.BackColor = selectedColor;
                    }
                    else
                    { 
                        grpDepartment.BackColor = defaultColor;
                        chkDepartment.CheckState = CheckState.Unchecked;
                    }
                }
                else
                {
                    grpDepartment.BackColor = defaultColor; 
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void chkCategory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCategory.Checked)
                {
                    if (ValidateSelection())
                    {
                        grpCategory.BackColor = selectedColor;
                    }
                    else
                    { 
                        grpCategory.BackColor = defaultColor;
                        chkCategory.CheckState = CheckState.Unchecked;
                    }
                }
                else
                {
                    grpCategory.BackColor = defaultColor;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkSubCategory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSubCategory.Checked)
                {
                    if (ValidateSelection())
                    {
                        grpSubCategory.BackColor = selectedColor;
                    }
                    else
                    {
                        grpSubCategory.BackColor = defaultColor;
                        chkSubCategory.CheckState = CheckState.Unchecked;
                    }
                }
                else
                {
                    grpSubCategory.BackColor = defaultColor;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void chkSubCategory2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSubCategory2.Checked)
                {
                    if (ValidateSelection())
                    {
                        grpSubCategory2.BackColor = selectedColor;
                    }
                    else
                    {
                        grpSubCategory2.BackColor = defaultColor;
                        chkSubCategory2.CheckState = CheckState.Unchecked;
                    }
                }
                else
                {
                    grpSubCategory2.BackColor = defaultColor;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void chkExtendedProperty_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkExtendedProperty.Checked)
                {
                    if (ValidateSelection())
                    {
                        grpExtendedProperty.BackColor = selectedColor;
                    }
                    else
                    {
                        grpExtendedProperty.BackColor = defaultColor;
                        chkExtendedProperty.CheckState = CheckState.Unchecked;
                    }
                }
                else
                {
                    grpExtendedProperty.BackColor = defaultColor;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void chkReportType_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkReportType.Checked)
                {
                    if (ValidateSelection())
                    {
                        grpReportType.BackColor = selectedColor;
                    }
                    else
                    {
                        grpReportType.BackColor = defaultColor;
                        chkReportType.CheckState = CheckState.Unchecked;
                    }
                }
                else
                {
                    grpReportType.BackColor = defaultColor;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

    }
}
