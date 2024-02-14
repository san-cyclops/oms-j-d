using Domain;
using Service;
using UI.Windows;
using Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Report.Inventory.Transactions.Reports;

namespace Report.GUI
{
    public partial class FrmExtendedPropertyReports : FrmBaseReportsForm
    {

        AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();

        List<Common.CheckedListBoxSelection> locationList = new List<Common.CheckedListBoxSelection>();
        List<Common.CheckedListBoxSelection> productExtendedPropertiesList = new List<Common.CheckedListBoxSelection>();
        List<Common.CheckedListBoxSelection> productPropertyValuesList = new List<Common.CheckedListBoxSelection>();
        List<Common.CheckedListBoxSelection> suppliersList = new List<Common.CheckedListBoxSelection>();

        public FrmExtendedPropertyReports(AutoGenerateInfo pautoGenerateInfo)
        {
            InitializeComponent();
            autoGenerateInfo = pautoGenerateInfo;
        }

        /// <summary>
        /// Setup GUI contols (Set visibility, data binding, auto loading)
        /// </summary>
        public override void InitializeForm()
        {
            try
            {
                InitializeSelectionLists();

                this.chkLstLocations.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstLocations_ItemCheck);
                this.chkLstProductExtendedProperties.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstProductExtendedProperties_ItemCheck);
                this.chkLstProductPropertyValues.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstProductPropertyValues_ItemCheck);
                this.chkLstSuppliers.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstSuppliers_ItemCheck);
                
                OrganiseFormControls();
                
                this.chkLstLocations.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstLocations_ItemCheck);
                this.chkLstProductExtendedProperties.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstProductExtendedProperties_ItemCheck);
                this.chkLstProductPropertyValues.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstProductPropertyValues_ItemCheck);
                this.chkLstSuppliers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstSuppliers_ItemCheck);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private List<Common.CheckedListBoxSelection> CreateSelectionList(string[] inArray)
        {
            List<Common.CheckedListBoxSelection> selectionDataStruct = new List<Common.CheckedListBoxSelection>();

            foreach (var item in inArray)
            {
                selectionDataStruct.Add(new Common.CheckedListBoxSelection() { Value = item.ToString().Trim(), isChecked = CheckState.Unchecked }); 
            }
            return selectionDataStruct;
        }

        private void InitializeSelectionLists()
        {
            //Product Extended Properties
            InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
            productExtendedPropertiesList = CreateSelectionList(invProductExtendedPropertyService.GetInvProductExtendedPropertyNames());
            
            //Locations
            LocationService locationService = new LocationService();
            locationList = CreateSelectionList(locationService.GetAllInventoryLocationNames());

            switch (autoGenerateInfo.FormName)
            {
                case "RptExtendedPropertySalesRegister":
                    //Suppliers
                    SupplierService supplierService = new SupplierService();
                    suppliersList = CreateSelectionList(supplierService.GetSupplierNames());
                    break;

                case "RptExtendedPropertyStockBalance":

                    break;
                default:
                    break;
            }
        }

        private void OrganiseFormControls()
        {
            
            // Apply Product Property Types
            rbtnDepartment.Text = (AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText);
            rbtnCategory.Text = (AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText);
            rbtnSubCategory.Text = (AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText);
            rbtnSubCategory2.Text = (AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText);

            Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstProductExtendedProperties, productExtendedPropertiesList, "", "");
            Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstLocations, locationList, "", "");
            
             //Load Dynamic Property 1
            switch (autoGenerateInfo.FormName)
            {
                case "RptExtendedPropertySalesRegister":
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstSuppliers, suppliersList, "", "");
                    break;
                case "RptExtendedPropertyStockBalance":

                    break;
                default:
                    break;
            }


            this.Text = autoGenerateInfo.FormText;
            //ActiveControl = ;
            //txtDepartmentCode.Focus();
        }

        private void rbtnDepartment_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnDepartment.Checked)
                {
                    LoadProductPropertyValues(rbtnDepartment.Text);
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void rbtnCategory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnCategory.Checked)
                {
                    LoadProductPropertyValues(rbtnCategory.Text);
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void rbtnSubCategory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnSubCategory.Checked)
                {
                    LoadProductPropertyValues(rbtnSubCategory.Text);
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void rbtnSubCategory2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnSubCategory2.Checked)
                {
                    LoadProductPropertyValues(rbtnSubCategory2.Text);
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void LoadProductPropertyValues(string productProperty)
        {
            chkProductProperties.CheckState = CheckState.Unchecked;
            ReSetAllItemsCheckedStatuses(productPropertyValuesList, CheckState.Unchecked);
            RefreshCheckedList(chkLstProductPropertyValues, productPropertyValuesList);
            
            string departmentText, categoryText, subCategoryText, subCategory2Text;
            departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
            categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
            subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
            subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;

            if (string.Equals(productProperty.Trim(), departmentText.Trim()))
            {
                //Departments               
                InvDepartmentService invDepartmentService = new InvDepartmentService();
                productPropertyValuesList = CreateSelectionList(invDepartmentService.GetAllDepartmentNames());
                Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstProductPropertyValues, productPropertyValuesList, "", "");
            }
            else if (string.Equals(productProperty.Trim(), categoryText.Trim()))
            {
                //Categories
                InvCategoryService invCategoryService = new InvCategoryService();
                productPropertyValuesList = CreateSelectionList(invCategoryService.GetAllCategoryNames());
                Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstProductPropertyValues, productPropertyValuesList, "", "");
            }
            else if (string.Equals(productProperty.Trim(), subCategoryText.Trim()))
            {
                //Sub Categories
                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                productPropertyValuesList = CreateSelectionList(invSubCategoryService.GetAllSubCategoryNames());
                Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstProductPropertyValues, productPropertyValuesList, "", "");            
            }
            else if (string.Equals(productProperty.Trim(), subCategory2Text.Trim()))
            {
                //Sub Categories 2
                InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                productPropertyValuesList = CreateSelectionList(invSubCategory2Service.GetInvSubCategory2Names());
                Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstProductPropertyValues, productPropertyValuesList, "", "");            
            }
            else
            {
                // Clear Product Property Values
                productPropertyValuesList.Clear();
                chkLstProductPropertyValues.Items.Clear();
                chkLstProductPropertyValues.DataSource = null;
            }
        }

        public override void View()//(object sender, EventArgs e)
        {
            try
            {
                if (bgWReport.IsBusy != true)
                {
                    bgWReport.RunWorkerAsync();
                    btnView.Enabled = false;
                    pBoxPreLoader.Visible = true;
                }

                //while (this.bgWReport.IsBusy)
                //{
                //    //pBarReportProgress.Increment(1);
                //    // Keep UI messages moving, so the form remains 
                //    // responsive during the asynchronous operation.
                //    //Application.DoEvents();
                //}


            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void bgWReport_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DataSet dsReportDatSet = GetReportData();
                e.Result = dsReportDatSet;
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void bgWReport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                //pBarReportProgress.Value = e.ProgressPercentage;
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
                        btnView.Enabled = true;
                        pBoxPreLoader.Visible = false;
                        return;
                    }
                    ViewReport((DataSet)e.Result);
                    btnView.Enabled = true;
                    pBoxPreLoader.Visible = false;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        
        }

        private void ViewReport(DataSet dtReportData)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            switch (autoGenerateInfo.FormName)
            {
                case "RptExtendedPropertySalesRegister":
                    InvSalesServices invSalesServicesSalesRegisterEx = new InvSalesServices();
            ArrayList productExtendedPropertyList = GetProductExtendedPropertyList();
            InvRptExtendedPropertySales invRptExtendedPropertySales = new InvRptExtendedPropertySales();

            invRptExtendedPropertySales.SetDataSource(dtReportData.Tables[0]);
            invRptExtendedPropertySales.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
            invRptExtendedPropertySales.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            invRptExtendedPropertySales.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            invRptExtendedPropertySales.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            invRptExtendedPropertySales.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            invRptExtendedPropertySales.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            // Set values to extended property header formulas
            for (int i = 0; i < productExtendedPropertyList.Count; i++)
            {
                invRptExtendedPropertySales.DataDefinition.FormulaFields["FExpr" + (i + 1).ToString()].Text = "'" + productExtendedPropertyList[i].ToString().Trim() + "'";
            }

            reportViewer.crRptViewer.ReportSource = invRptExtendedPropertySales;
            break;
                default:
            break;
            }

            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
        }

        private DataSet GetReportData()
        {
            DateTime dateFrom = dtpDateFrom.Value, dateTo = dtpDateTo.Value;
            DataSet dsReportData = new DataSet();

            ArrayList productExtendedPropertyList = GetProductExtendedPropertyList();
            if (productExtendedPropertyList.Count < 1)
            {
                Toast.Show("Product Extended Property ", Toast.messageType.Warning, Toast.messageAction.NotExists);
                return null;
            }

            string[] productPropertyValuesList = GetProductPropertyValuesList();
            if (productPropertyValuesList.Length < 1)
            {
                Toast.Show("Product Property Values ", Toast.messageType.Warning, Toast.messageAction.NotExists);
                return null;
            }

            string productPropertyType = GetProductPropertyType();
            if (string.IsNullOrEmpty(productPropertyType))
            {
                Toast.Show("Product Property Type ", Toast.messageType.Warning, Toast.messageAction.NotExists);
                return null;
            }

            string[] locationList = GetLocationList();
            if (locationList.Length < 1)
            {
                Toast.Show("Locations ", Toast.messageType.Warning, Toast.messageAction.NotExists);
                return null;
            }

            switch (autoGenerateInfo.FormName)
            {
                case "RptExtendedPropertySalesRegister":
                    InvSalesServices invSalesServicesSalesRegisterEx = new InvSalesServices();
                    InvRptExtendedPropertySales invRptExtendedPropertySales = new InvRptExtendedPropertySales();
                    dsReportData.Tables.Add(invSalesServicesSalesRegisterEx.GetExtendedPropertySalesRegisterDataTable(autoGenerateInfo, dateFrom, dateTo, productExtendedPropertyList, productPropertyType, productPropertyValuesList, locationList, GetSuppliersList()));
                    break;
                default:
                    break;
            }
            return dsReportData;
        }

        private string[] GetLocationList()
        {
            ArrayList tmpList = new ArrayList();
            //foreach (var item in chkLstLocations.CheckedItems)
            //{
            //    tmpList.Add(item.ToString().Trim());
            //}
            foreach (var item in locationList.Where(l=> l.isChecked.Equals(CheckState.Checked)))
            {
                tmpList.Add(item.Value.ToString().Trim());
            }
            return (string[])tmpList.ToArray(typeof(string));
        }

        private ArrayList GetProductExtendedPropertyList()
        {

            ArrayList productExtendedPropertyList = new ArrayList();
            //foreach (var item in chkLstProductExtendedProperties.CheckedItems)
            //{
            //    productExtendedPropertyList.Add(item.ToString().Trim());
            //}
            foreach (var item in productExtendedPropertiesList.Where(l => l.isChecked.Equals(CheckState.Checked)))
            {
                productExtendedPropertyList.Add(item.Value.ToString().Trim());
            }
            return productExtendedPropertyList;
        }

        private string[] GetProductPropertyValuesList()
        {
            ArrayList tmpList = new ArrayList();
            //foreach (var item in chkLstProductPropertyValues.CheckedItems)
            //{
            //    tmpList.Add(item.ToString().Trim());
            //}
            foreach (var item in productPropertyValuesList.Where(l => l.isChecked.Equals(CheckState.Checked)))
            {
                tmpList.Add(item.Value.ToString().Trim());
            }
            return (string[])tmpList.ToArray(typeof(string));
        }

        private string GetProductPropertyType()
        {
            if (!grpProductProperties.Controls.OfType<RadioButton>().Any(r => r.Checked))
            {
                Toast.Show("Product Property ", Toast.messageType.Warning, Toast.messageAction.NotExists);
                return null;
            }
            else
            {
                return grpProductProperties.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text.Trim();
            }
        }

        private string[] GetSuppliersList()
        {
            ArrayList tmpList = new ArrayList();
            //foreach (var item in chkLstSuppliers.CheckedItems)
            //{
            //    tmpList.Add(item.ToString().Trim());
            //}
            foreach (var item in suppliersList.Where(l => l.isChecked.Equals(CheckState.Checked)))
            {
                tmpList.Add(item.Value.ToString().Trim());
            }
            return (string[])tmpList.ToArray(typeof(string));
        }

        private void chkProductExtendedProperties_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.chkLstProductExtendedProperties.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstProductExtendedProperties_ItemCheck);
                if (chkProductExtendedProperties.Checked)
                {
                    for (int i = 0; i < chkLstProductExtendedProperties.Items.Count; i++)
                    {
                        chkLstProductExtendedProperties.SetItemChecked(i, true);
                    }
                    ReSetAllItemsCheckedStatuses(productExtendedPropertiesList, CheckState.Checked);
                }
                else
                {
                    for (int i = 0; i < chkLstProductExtendedProperties.Items.Count; i++)
                    {
                        chkLstProductExtendedProperties.SetItemChecked(i, false);
                    }
                    ReSetAllItemsCheckedStatuses(productExtendedPropertiesList, CheckState.Unchecked);
                }
                this.chkLstProductExtendedProperties.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstProductExtendedProperties_ItemCheck);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkProductProperties_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkLstProductPropertyValues.Items.Count < 1)
                {
                    chkProductProperties.CheckState = CheckState.Unchecked;
                    return;
                }

                this.chkLstProductPropertyValues.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstProductPropertyValues_ItemCheck);
                if (chkProductProperties.Checked)
                {
                    for (int i = 0; i < chkLstProductPropertyValues.Items.Count; i++)
                    {
                        chkLstProductPropertyValues.SetItemChecked(i, true);
                    }
                    ReSetAllItemsCheckedStatuses(productPropertyValuesList, CheckState.Checked);
                }
                else
                {
                    for (int i = 0; i < chkLstProductPropertyValues.Items.Count; i++)
                    {
                        chkLstProductPropertyValues.SetItemChecked(i, false);
                    }
                    ReSetAllItemsCheckedStatuses(productPropertyValuesList, CheckState.Unchecked);
                }
                this.chkLstProductPropertyValues.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstProductPropertyValues_ItemCheck);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkLocations_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.chkLstLocations.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstLocations_ItemCheck);
                if (chkLocations.Checked)
                {
                    for (int i = 0; i < chkLstLocations.Items.Count; i++)
                    {
                        chkLstLocations.SetItemChecked(i, true);
                    }
                    ReSetAllItemsCheckedStatuses(locationList, CheckState.Checked);
                }
                else
                {
                    for (int i = 0; i < chkLstLocations.Items.Count; i++)
                    {
                        chkLstLocations.SetItemChecked(i, false);
                    }
                    ReSetAllItemsCheckedStatuses(locationList, CheckState.Unchecked);
                }
                this.chkLstLocations.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstLocations_ItemCheck);
                
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkSuppliers_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.chkLstSuppliers.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstSuppliers_ItemCheck);
                if (chkSuppliers.Checked)
                {
                    for (int i = 0; i < chkLstSuppliers.Items.Count; i++)
                    {
                        chkLstSuppliers.SetItemChecked(i, true);
                    }
                    ReSetAllItemsCheckedStatuses(suppliersList, CheckState.Checked);
                }
                else
                {
                    for (int i = 0; i < chkLstSuppliers.Items.Count; i++)
                    {
                        chkLstSuppliers.SetItemChecked(i, false);
                    }
                    ReSetAllItemsCheckedStatuses(suppliersList, CheckState.Unchecked);
                }
                this.chkLstSuppliers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstSuppliers_ItemCheck);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        //private void txtSuppliers_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(txtSuppliers.Text.Trim()))
        //        { return; }

        //        //dynamic CheckedList = (from Item in chkLstSuppliers.Items.Cast<string>()
        //        //                           .Where((xItem, Index) => chkLstSuppliers.GetItemChecked(Index))Item).ToList();
                
        //        //var result = chkLstSuppliers.CheckedItems.Cast<object>().Aggregate(string.Empty, (current, item) => current + item.ToString()); 
        //        var result = chkLstSuppliers.Items.Cast<string>().Where(c => c.ToLower().StartsWith(txtSuppliers.Text.ToLower().Trim())).ToList();
                
        //        //chkLstSuppliers.

        //    }
        //    catch (Exception ex)
        //    { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        //}

        //private void chkLstProductExtendedProperties_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (int.Equals(chkLstProductExtendedProperties.CheckedItems.Count, chkLstProductExtendedProperties.Items.Count))
        //        {
        //            chkProductExtendedProperties.CheckState = CheckState.Checked;
        //        }
        //        else
        //        {
        //            this.chkProductExtendedProperties.CheckedChanged -= new System.EventHandler(this.chkProductExtendedProperties_CheckedChanged);
        //            chkProductExtendedProperties.CheckState = CheckState.Unchecked;
        //            this.chkProductExtendedProperties.CheckedChanged += new System.EventHandler(this.chkProductExtendedProperties_CheckedChanged);
        //        }
        //    }
        //    catch (Exception ex)
        //    { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        //}

        //private void chkLstProductPropertyValues_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (int.Equals(chkLstProductPropertyValues.CheckedItems.Count, chkLstProductPropertyValues.Items.Count))
        //        {
        //            chkProductProperties.CheckState = CheckState.Checked;
        //        }
        //        else
        //        {
        //            this.chkProductProperties.CheckedChanged -= new System.EventHandler(this.chkProductProperties_CheckedChanged);
        //            chkProductProperties.CheckState = CheckState.Unchecked;
        //            this.chkProductProperties.CheckedChanged += new System.EventHandler(this.chkProductProperties_CheckedChanged);
        //        }
        //    }
        //    catch (Exception ex)
        //    { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        //}

        //private void chkLstLocations_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //if (int.Equals(chkLstLocations.CheckedItems.Count, chkLstLocations.Items.Count))
        //        //{
        //        //    //if (chkLstLocations.Items.Count.Equals(locationList.Count))
        //        //    //{
        //        //        chkLocations.CheckState = CheckState.Checked;
        //        //    //}
        //        //}
        //        //else
        //        //{
        //        //    this.chkLocations.CheckedChanged -= new System.EventHandler(this.chkLocations_CheckedChanged);
        //        //    chkLocations.CheckState = CheckState.Unchecked;
        //        //    this.chkLocations.CheckedChanged += new System.EventHandler(this.chkLocations_CheckedChanged);
        //        //}
        //}
        //    catch (Exception ex)
        //    { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        //}

        //private void chkLstSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (int.Equals(chkLstSuppliers.CheckedItems.Count, chkLstSuppliers.Items.Count))
        //        {
        //            chkSuppliers.CheckState = CheckState.Checked;
        //        }
        //        else
        //        {
        //            this.chkSuppliers.CheckedChanged -= new System.EventHandler(this.chkSuppliers_CheckedChanged);
        //            chkSuppliers.CheckState = CheckState.Unchecked;
        //            this.chkSuppliers.CheckedChanged += new System.EventHandler(this.chkSuppliers_CheckedChanged);
        //        }
        //    }
        //    catch (Exception ex)
        //    { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        //}

        private List<Common.CheckedListBoxSelection> SearchLocation(List<Common.CheckedListBoxSelection> locations, string searchString)
        {
            return locations.Where(c => c.Value.ToLower().StartsWith(searchString.ToLower().Trim())).ToList();
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
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstLocations, SearchLocation(locationList, txtLocations.Text.Trim()), "", "");
                }
                RefreshCheckedList(chkLstLocations, locationList);
                this.chkLstLocations.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstLocations_ItemCheck);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void SetItemCheckedStatus(List<Common.CheckedListBoxSelection> allValuesList, string checkedListBoxItem, CheckState checkState)
        {
            foreach (var item in allValuesList.Where(v => v.Value == checkedListBoxItem.Trim()))
            {
                item.isChecked = checkState;
            }
        }

        private void ReSetAllItemsCheckedStatuses(List<Common.CheckedListBoxSelection> allValuesList, CheckState checkState)
        {
            foreach (var item in allValuesList)
            {
                item.isChecked = checkState;
            }
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

        private void ReSetCheckedList(CheckedListBox checkedListBox, CheckState checkState)
        {
            bool status = checkState.Equals(CheckState.Checked) ? true : false;
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                checkedListBox.SetItemChecked(i, status);
            }
        }

        //private void ReSetSelectAllCheckedBox(CheckBox selectAllCheckBox, List<Common.CheckedListBoxSelection> allValuesList)
        //{
        //    if (allValuesList.Any(a => a.isChecked.Equals(CheckState.Unchecked)))
        //    {
        //        selectAllCheckBox.CheckState = CheckState.Unchecked;
        //    }
        //    else
        //    {
        //        selectAllCheckBox.CheckState = CheckState.Checked;
        //    }
        //}

        private void chkLstLocations_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var item = (CheckedListBox)sender;
                SetItemCheckedStatus(locationList, item.SelectedItem.ToString().Trim(), e.CurrentValue.Equals(CheckState.Checked) ? CheckState.Unchecked : CheckState.Checked);

                this.chkLocations.CheckedChanged -= new System.EventHandler(this.chkLocations_CheckedChanged);
                if (locationList.Any(a => a.isChecked.Equals(CheckState.Unchecked)))
                {
                    chkLocations.CheckState = CheckState.Unchecked;
                }
                else
                {
                    chkLocations.CheckState = CheckState.Checked;
                }
                this.chkLocations.CheckedChanged += new System.EventHandler(this.chkLocations_CheckedChanged);
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

                this.chkProductExtendedProperties.CheckedChanged -= new System.EventHandler(this.chkProductExtendedProperties_CheckedChanged);
                if (productExtendedPropertiesList.Any(a => a.isChecked.Equals(CheckState.Unchecked)))
                {
                    chkProductExtendedProperties.CheckState = CheckState.Unchecked;
                }
                else
                {
                    chkProductExtendedProperties.CheckState = CheckState.Checked;
                }
                this.chkProductExtendedProperties.CheckedChanged += new System.EventHandler(this.chkProductExtendedProperties_CheckedChanged);

            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkLstProductPropertyValues_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var item = (CheckedListBox)sender;
                SetItemCheckedStatus(productPropertyValuesList, item.SelectedItem.ToString().Trim(), e.CurrentValue.Equals(CheckState.Checked) ? CheckState.Unchecked : CheckState.Checked);

                this.chkProductProperties.CheckedChanged -= new System.EventHandler(this.chkProductProperties_CheckedChanged);
                if (productPropertyValuesList.Any(a => a.isChecked.Equals(CheckState.Unchecked)))
                {
                    chkProductProperties.CheckState = CheckState.Unchecked;
                }
                else
                {
                    chkProductProperties.CheckState = CheckState.Checked;
                }
                this.chkProductProperties.CheckedChanged += new System.EventHandler(this.chkProductProperties_CheckedChanged);

            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        
        }

        private void chkLstSuppliers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var item = (CheckedListBox)sender;
                SetItemCheckedStatus(suppliersList, item.SelectedItem.ToString().Trim(), e.CurrentValue.Equals(CheckState.Checked) ? CheckState.Unchecked : CheckState.Checked);

                this.chkSuppliers.CheckedChanged -= new System.EventHandler(this.chkSuppliers_CheckedChanged);
                if (suppliersList.Any(a => a.isChecked.Equals(CheckState.Unchecked)))
                {
                    chkSuppliers.CheckState = CheckState.Unchecked;
                }
                else
                {
                    chkSuppliers.CheckState = CheckState.Checked;
                }
                this.chkSuppliers.CheckedChanged += new System.EventHandler(this.chkSuppliers_CheckedChanged);
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
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstProductExtendedProperties, SearchLocation(productExtendedPropertiesList, txtProductExtendedProperties.Text.Trim()), "", "");
                }
                RefreshCheckedList(chkLstProductExtendedProperties, productExtendedPropertiesList);
                this.chkLstProductExtendedProperties.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstProductExtendedProperties_ItemCheck);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductPropertyValue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.chkLstProductPropertyValues.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstProductPropertyValues_ItemCheck);
                if (string.IsNullOrEmpty(txtProductPropertyValue.Text.Trim()))
                {
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstProductPropertyValues, productPropertyValuesList, "", "");
                }
                else
                {
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstProductPropertyValues, SearchLocation(productPropertyValuesList, txtProductPropertyValue.Text.Trim()), "", "");
                }
                RefreshCheckedList(chkLstProductPropertyValues, productPropertyValuesList);
                this.chkLstProductPropertyValues.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstProductPropertyValues_ItemCheck);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        
        }

        private void txtSuppliers_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.chkLstSuppliers.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstSuppliers_ItemCheck);
                if (string.IsNullOrEmpty(txtSuppliers.Text.Trim()))
                {
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstSuppliers, suppliersList, "", "");
                }
                else
                {
                    Common.LoadItemsToCheckListt<Common.CheckedListBoxSelection>(chkLstSuppliers, SearchLocation(suppliersList, txtSuppliers.Text.Trim()), "", "");
                }
                RefreshCheckedList(chkLstSuppliers, suppliersList);
                this.chkLstSuppliers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstSuppliers_ItemCheck);
                
            }
            catch (Exception ex)
            { Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        
        }

        


    }
}
