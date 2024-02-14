using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using Report;
using Report.Com;
using Report.GV;
using Report.Inventory.Transactions.Reports;
using Report.Logistic;
using Service;
using Domain;
using UI.Windows;
using UI.Windows.Reports;
using Utility;
using System.Collections;
using Report.Inventory;
using System.Reflection;


namespace UI.Windows
{
    public partial class FrmCurrentSales : UI.Windows.FrmBaseMasterForm
    {

        bool isValidControls = true;
        int documentID = 0;
        UserPrivileges accessRights = new UserPrivileges();
        PosSalesSummeryService posSalesSummeryService = new PosSalesSummeryService();

        public FrmCurrentSales()
        {
            InitializeComponent();
        }

        public override void FormLoad()
        {
            try
            {

                // Load Locations
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;
                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);

                RdoProduct.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmProduct").FormText;
                RdoDepartment.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
                RdoCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
                RdoSubCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
               

                dtpFromDate.Value = Common.GetSystemDate();
                dtpToDate.Value = Common.GetSystemDate();

                cmbLocation.SelectedValue = Common.LoggedLocationID;
                cmbLocation.Enabled = false;

                LoadSearchCodes();

                base.FormLoad();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void ChkAllTerminals_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllTerminals.Checked == true)
            {
                Common.ClearComboBox(cmbTerminal);
                cmbTerminal.Enabled = false;
            }
            else
            {
                cmbTerminal.Enabled = true;
            }
        }


        private void ChkAllLocations_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllLocations.Checked == true)
            {
                Common.ClearComboBox(cmbLocation);
                cmbLocation.Enabled = false;
            }
            else
            {
                cmbLocation.Enabled = true;
            }
        }


        public override void CloseForm()
        {
            try
            {
                base.CloseForm();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

       
        private bool ValidateTerminalComboBoxes()
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbTerminal);
        }

        private bool ValidateLocationComboBoxes()
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbLocation);
        }

        private bool ValidateControls()
        {
            if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, TxtSearchCodeFrom, TxtSearchCodeTo))
            { return false; }
                       
            else
            {
                isValidControls = true;
                this.ValidateChildren();

                return isValidControls;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dateFrom;
                DateTime dateTo;

                int locationId = 0;
                int terminalId = 0;
                string offlineTerminals = "";

                FrmReportViewer objReportView = new FrmReportViewer();
                Cursor.Current = Cursors.WaitCursor;

                dateFrom = dtpFromDate.Value;
                dateTo = dtpToDate.Value;                

                if (ChkAllTerminals.Checked == false)
                {
                    if (ValidateTerminalComboBoxes().Equals(false)) { return; }
                }

                if (ChkAllLocations.Checked == false)
                {
                    if (ValidateLocationComboBoxes().Equals(false)) { return; }
                }

                if (ValidateControls() == false) return;

                if (dateFrom > dateTo)
                {
                    Toast.Show("", Toast.messageType.Information, Toast.messageAction.InvalidDateRange);
                    return;
                }


                if (ChkAllLocations.Checked == true) { locationId = 0; } else { locationId = cmbLocation.SelectedIndex + 1; }
                if (ChkAllTerminals.Checked == true) { terminalId = 0; } else { terminalId = cmbTerminal.SelectedIndex + 1; }


                if (RdoProduct.Checked == true)
                {
                    posSalesSummeryService.SqlStatement = " SELECT SD.ProductCode Productcode, (SD.descrip) ProductDescription, " +
                        " SD.Price UnitPrice, SD.Cost CostPrice, (CASE SD.DocumentId WHEN '1' THEN SD.Qty WHEN '2' THEN -SD.Qty " + //WHEN '1'  " +
                        " ELSE 0 END) Qty, (CASE SD.DocumentId WHEN '1' THEN SD.nett  " +
                        " WHEN '2' THEN -SD.nett ELSE 0 END) Amount, (CASE SD.DocumentId   " +
                        " WHEN '1' THEN (IDiscount1+IDiscount2+IDiscount3+IDiscount4+IDiscount5) WHEN '2' THEN " +
                        " -(+IDiscount1+IDiscount2+IDiscount3+IDiscount4+IDiscount5) ELSE 0 END) Discount, (CASE SD.DocumentId WHEN '6' THEN SD.SDiscount  " +
                        "  ELSE 0 END)  " +
                        " SDiscount, SD.LocationId LocationCode, SD.Receipt DocumentNo, SD.RecDate SaleDate, '" + dtpFromDate.Text + "'  " +
                        " DateFrom,  '" + dtpToDate.Text + "' DateTo, '' AccCode, '' AccName  " +
                        " FROM TransactionDet SD  " +
                        " WHERE SD.SaleTypeID = 1 AND SD.BillTypeID = 1 AND SD.Status = 1 AND SD.TransStatus = 1 AND SD.DocumentId in (1,2,6) AND  SD.LocationId = '" + locationId + "' AND ((SD.ProductCode BETWEEN '" + TxtSearchCodeFrom.Text.Trim() + "' AND '" + TxtSearchCodeTo.Text.Trim() + "') OR SD.ProductCode = '')  " +
                        " AND CAST(SD.RECDATE as date) BETWEEN '" + String.Format("{0:yyyy/MM/dd}", dateFrom) + "' AND '" + String.Format("{0:yyyy/MM/dd}", dateTo) + "' ";



                    InvRpt_CurrentSale invRpt_CurrentSale = new InvRpt_CurrentSale();

                    if (ChkAllTerminals.Checked == false)
                        posSalesSummeryService.GetCurrentSales(terminalId, true, locationId, out offlineTerminals);
                    else
                        posSalesSummeryService.GetCurrentSales(terminalId, false, locationId, out offlineTerminals);

                    invRpt_CurrentSale.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";

                    if (ChkAllTerminals.Checked == false)
                        invRpt_CurrentSale.DataDefinition.FormulaFields["UnitNo"].Text = "'" + cmbTerminal.Text.Trim() + "'";
                    else
                        invRpt_CurrentSale.DataDefinition.FormulaFields["UnitNo"].Text = "'All Terminals'";

                    if (ChkAllLocations.Checked == false)
                        invRpt_CurrentSale.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";
                    else
                        invRpt_CurrentSale.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";

                    invRpt_CurrentSale.DataDefinition.FormulaFields["OffLine"].Text = "'" + offlineTerminals.ToString() + "'";

                    //invRpt_CurrentSale.SummaryInfo.ReportTitle = "Current Sales Product Wise Report";
                    invRpt_CurrentSale.SummaryInfo.ReportTitle = "Current Sales " +  RdoProduct.Text.Trim() + " Wise Report";
                    invRpt_CurrentSale.SetDataSource(posSalesSummeryService.DsGetCurrentsales.Tables["inv_productsalesdetails"]);


                    invRpt_CurrentSale.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRpt_CurrentSale.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                    invRpt_CurrentSale.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRpt_CurrentSale.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRpt_CurrentSale.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    objReportView.crRptViewer.ReportSource = invRpt_CurrentSale;

                }

                else if (RdoDepartment.Checked == true)
                {                  

                    posSalesSummeryService.SqlStatement = " SELECT DP.DepartmentCode, DP.DepartmentName, SD.ProductCode Productcode, (SD.descrip) ProductDescription, " +
                        " SD.Price UnitPrice, SD.Cost CostPrice, (CASE SD.DocumentId WHEN '1' THEN SD.Qty WHEN '2' THEN -SD.Qty " + //WHEN '1'  " +
                        " ELSE 0 END) Qty, (CASE SD.DocumentId  WHEN '1' THEN SD.nett  " +
                        " WHEN '2' THEN -SD.nett ELSE 0 END) Amount, (CASE SD.DocumentId   " +
                        " WHEN '1' THEN (IDiscount1+IDiscount2+IDiscount3+IDiscount4+IDiscount5) WHEN '2' THEN " +
                        " -(+IDiscount1+IDiscount2+IDiscount3+IDiscount4+IDiscount5) ELSE 0 END) Discount, (CASE SD.DocumentId WHEN '6' THEN SD.SDiscount  " +
                        "  ELSE 0 END)  " +
                        " SDiscount, SD.LocationId LocationCode, SD.Receipt DocumentNo, SD.RecDate SaleDate, '" + dtpFromDate.Text + "'  " +
                        " DateFrom,  '" + dtpToDate.Text + "' DateTo, '' AccCode, '' AccName  " +
                        " FROM TransactionDet SD left outer JOIN (SELECT ProductId,DepartmentId FROM PRODUCTMASTER GROUP BY  ProductId,DepartmentId) PM ON SD.ProductId = PM.ProductId left outer join InvDepartment DP ON PM.DepartmentId = DP.InvDepartmentId " +
                        " WHERE SD.SaleTypeID = 1 AND SD.BillTypeID = 1 AND SD.Status = 1 AND SD.TransStatus = 1 AND SD.DocumentId in (1,2,6) AND   SD.LocationId = '" + locationId + "' AND ((DP.DepartmentCode BETWEEN '" + TxtSearchCodeFrom.Text.Trim() + "' AND '" + TxtSearchCodeTo.Text.Trim() + "') OR DP.DepartmentCode is null) " +
                        " AND CAST(SD.RECDATE as date) BETWEEN '" + String.Format("{0:yyyy/MM/dd}", dateFrom) + "' AND '" + String.Format("{0:yyyy/MM/dd}", dateTo) + "' ";


                    InvRpt_CurrentSaleDepartmentWise invRpt_CurrentSaleDepartmentWise = new InvRpt_CurrentSaleDepartmentWise();



                    if (ChkAllTerminals.Checked == false)
                        posSalesSummeryService.GetCurrentSales(terminalId, true, locationId, out offlineTerminals);
                    else
                        posSalesSummeryService.GetCurrentSales(terminalId, false, locationId, out offlineTerminals);

                    invRpt_CurrentSaleDepartmentWise.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";

                    if (ChkAllTerminals.Checked == false)
                        invRpt_CurrentSaleDepartmentWise.DataDefinition.FormulaFields["UnitNo"].Text = "'" + cmbTerminal.Text.Trim() + "'";
                    else
                        invRpt_CurrentSaleDepartmentWise.DataDefinition.FormulaFields["UnitNo"].Text = "'All Terminals'";

                    if (ChkAllLocations.Checked == false)
                        invRpt_CurrentSaleDepartmentWise.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";
                    else
                        invRpt_CurrentSaleDepartmentWise.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";

                    invRpt_CurrentSaleDepartmentWise.DataDefinition.FormulaFields["OffLine"].Text = "'" + offlineTerminals.ToString() + "'";

                    //invRpt_CurrentSaleDepartmentWise.SummaryInfo.ReportTitle = "Current Sales Department Wise Report";
                    invRpt_CurrentSaleDepartmentWise.SummaryInfo.ReportTitle = "Current Sales " + RdoDepartment.Text.Trim() + " Wise Report";

                    invRpt_CurrentSaleDepartmentWise.SetDataSource(posSalesSummeryService.DsGetCurrentsales.Tables["inv_productsalesdetails"]);


                    invRpt_CurrentSaleDepartmentWise.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRpt_CurrentSaleDepartmentWise.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                    invRpt_CurrentSaleDepartmentWise.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRpt_CurrentSaleDepartmentWise.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRpt_CurrentSaleDepartmentWise.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    

                    objReportView.crRptViewer.ReportSource = invRpt_CurrentSaleDepartmentWise;
                }

                else if (RdoCategory.Checked == true)
                {

                    posSalesSummeryService.SqlStatement = " SELECT PM.CategoryId, SD.ProductCode Productcode, (SD.descrip) ProductDescription, " +
                        " SD.Price UnitPrice, SD.Cost CostPrice, (CASE SD.DocumentId WHEN '1' THEN SD.Qty WHEN '2' THEN -SD.Qty " + //WHEN '1'  " +
                        " ELSE 0 END) Qty, (CASE SD.DocumentId  WHEN '1' THEN SD.nett  " +
                        " WHEN '2' THEN -SD.nett ELSE 0 END) Amount, (CASE SD.DocumentId   " +
                        " WHEN '1' THEN (IDiscount1+IDiscount2+IDiscount3+IDiscount4+IDiscount5) WHEN '2' THEN " +
                        " -(+IDiscount1+IDiscount2+IDiscount3+IDiscount4+IDiscount5) ELSE 0 END) Discount, (CASE SD.DocumentId WHEN '6' THEN SD.SDiscount  " +
                        "  ELSE 0 END)  " +
                        " SDiscount, SD.LocationId LocationCode, SD.Receipt DocumentNo, SD.RecDate SaleDate, '" + dtpFromDate.Text + "'  " +
                        " DateFrom,  '" + dtpToDate.Text + "' DateTo, '' AccCode, '' AccName  " +
                        " FROM TransactionDet SD left outer JOIN (SELECT ProductId, CategoryId FROM PRODUCTMASTER GROUP BY  ProductId,CategoryId) PM ON SD.ProductId = PM.ProductId " +
                        " WHERE SD.SaleTypeID = 1 AND SD.BillTypeID = 1 AND SD.Status = 1 AND SD.TransStatus = 1 AND SD.DocumentId in (1,2,6) AND   SD.LocationId = '" + locationId + "' " +
                        " AND CAST(SD.RECDATE as date) BETWEEN '" + String.Format("{0:yyyy/MM/dd}", dateFrom) + "' AND '" + String.Format("{0:yyyy/MM/dd}", dateTo) + "' ";


                   InvRpt_CurrentSaleCategoryWise invRpt_CurrentSaleCategoryWise = new InvRpt_CurrentSaleCategoryWise();


                    if (ChkAllTerminals.Checked == false)
                        posSalesSummeryService.GetCurrentSales(terminalId, true, locationId, out offlineTerminals);
                    else
                        posSalesSummeryService.GetCurrentSales(terminalId, false, locationId, out offlineTerminals);

                    invRpt_CurrentSaleCategoryWise.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";

                    if (ChkAllTerminals.Checked == false)
                        invRpt_CurrentSaleCategoryWise.DataDefinition.FormulaFields["UnitNo"].Text = "'" + cmbTerminal.Text.Trim() + "'";
                    else
                        invRpt_CurrentSaleCategoryWise.DataDefinition.FormulaFields["UnitNo"].Text = "'All Terminals'";

                    if (ChkAllLocations.Checked == false)
                        invRpt_CurrentSaleCategoryWise.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";
                    else
                        invRpt_CurrentSaleCategoryWise.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";

                    invRpt_CurrentSaleCategoryWise.DataDefinition.FormulaFields["OffLine"].Text = "'" + offlineTerminals.ToString() + "'";

                    //invRpt_CurrentSaleCategoryWise.SummaryInfo.ReportTitle = "Current Sales Category Wise Report";
                    invRpt_CurrentSaleCategoryWise.SummaryInfo.ReportTitle = "Current Sales " + RdoCategory.Text.Trim() + " Wise Report";


                    posSalesSummeryService.GetCurrentSalesCategory(TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim());
                    invRpt_CurrentSaleCategoryWise.SetDataSource(posSalesSummeryService.DsGetCurrentsales);


                    invRpt_CurrentSaleCategoryWise.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRpt_CurrentSaleCategoryWise.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                    invRpt_CurrentSaleCategoryWise.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRpt_CurrentSaleCategoryWise.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRpt_CurrentSaleCategoryWise.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";


                    objReportView.crRptViewer.ReportSource = invRpt_CurrentSaleCategoryWise;
                }

                else if (RdoSubCategory.Checked == true)
                {

                    posSalesSummeryService.SqlStatement = " SELECT PM.SubCategoryId, SD.ProductCode Productcode, (SD.descrip) ProductDescription, " +
                        " SD.Price UnitPrice, SD.Cost CostPrice, (CASE SD.DocumentId WHEN '1' THEN SD.Qty WHEN '2' THEN -SD.Qty " + //WHEN '1'  " +
                        " ELSE 0 END) Qty, (CASE SD.DocumentId  WHEN '1' THEN SD.nett  " +
                        " WHEN '2' THEN -SD.nett ELSE 0 END) Amount, (CASE SD.DocumentId   " +
                        " WHEN '1' THEN (IDiscount1+IDiscount2+IDiscount3+IDiscount4+IDiscount5) WHEN '2' THEN " +
                        " -(+IDiscount1+IDiscount2+IDiscount3+IDiscount4+IDiscount5) ELSE 0 END) Discount, (CASE SD.DocumentId WHEN '6' THEN SD.SDiscount  " +
                        "  ELSE 0 END)  " +
                        " SDiscount, SD.LocationId LocationCode, SD.Receipt DocumentNo, SD.RecDate SaleDate, '" + dtpFromDate.Text + "'  " +
                        " DateFrom,  '" + dtpToDate.Text + "' DateTo, '' AccCode, '' AccName  " +
                        " FROM TransactionDet SD left outer JOIN (SELECT ProductId, SubCategoryId FROM PRODUCTMASTER GROUP BY  ProductId, SubCategoryId) PM ON SD.ProductId = PM.ProductId " +
                        " WHERE SD.SaleTypeID = 1 AND SD.BillTypeID = 1 AND SD.Status = 1 AND SD.TransStatus = 1 AND SD.DocumentId in (1,2,6) AND   SD.LocationId = '" + locationId + "' " +
                        " AND CAST(SD.RECDATE as date) BETWEEN '" + String.Format("{0:yyyy/MM/dd}", dateFrom) + "' AND '" + String.Format("{0:yyyy/MM/dd}", dateTo) + "' ";


                    InvRpt_CurrentSaleSubCategoryWise invRpt_CurrentSaleSubCategoryWise = new InvRpt_CurrentSaleSubCategoryWise();


                    if (ChkAllTerminals.Checked == false)
                        posSalesSummeryService.GetCurrentSales(terminalId, true, locationId, out offlineTerminals);
                    else
                        posSalesSummeryService.GetCurrentSales(terminalId, false, locationId, out offlineTerminals);

                    invRpt_CurrentSaleSubCategoryWise.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";

                    if (ChkAllTerminals.Checked == false)
                        invRpt_CurrentSaleSubCategoryWise.DataDefinition.FormulaFields["UnitNo"].Text = "'" + cmbTerminal.Text.Trim() + "'";
                    else
                        invRpt_CurrentSaleSubCategoryWise.DataDefinition.FormulaFields["UnitNo"].Text = "'All Terminals'";

                    if (ChkAllLocations.Checked == false)
                        invRpt_CurrentSaleSubCategoryWise.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";
                    else
                        invRpt_CurrentSaleSubCategoryWise.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";

                    invRpt_CurrentSaleSubCategoryWise.DataDefinition.FormulaFields["OffLine"].Text = "'" + offlineTerminals.ToString() + "'";

                    //invRpt_CurrentSaleSubCategoryWise.SummaryInfo.ReportTitle = "Current Sales Sub Category Wise Report";
                    invRpt_CurrentSaleSubCategoryWise.SummaryInfo.ReportTitle = "Current Sales " + RdoSubCategory.Text.Trim() + " Wise Report";

                    posSalesSummeryService.GetCurrentSalesSubCategory(TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim());
                    invRpt_CurrentSaleSubCategoryWise.SetDataSource(posSalesSummeryService.DsGetCurrentsales);


                    invRpt_CurrentSaleSubCategoryWise.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRpt_CurrentSaleSubCategoryWise.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                    invRpt_CurrentSaleSubCategoryWise.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRpt_CurrentSaleSubCategoryWise.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRpt_CurrentSaleSubCategoryWise.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";


                    objReportView.crRptViewer.ReportSource = invRpt_CurrentSaleSubCategoryWise;
                }

                objReportView.WindowState = FormWindowState.Maximized;
                objReportView.Show();
                Cursor.Current = Cursors.Default;
            }

            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadProductsFrom() 
        {
            try
            {
                InvProductMasterService invProductMasterService = new InvProductMasterService();

                Common.SetAutoComplete(TxtSearchCodeFrom, invProductMasterService.GetAllProductCodes(), ChkAutoComplteFrom.Checked);
                Common.SetAutoComplete(TxtSearchNameFrom, invProductMasterService.GetAllProductNames(), ChkAutoComplteFrom.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadProductsTo()
        {
            try
            {
                InvProductMasterService invProductMasterService = new InvProductMasterService();

                Common.SetAutoComplete(TxtSearchCodeTo, invProductMasterService.GetAllProductCodes(), ChkAutoComplteTo.Checked);
                Common.SetAutoComplete(TxtSearchNameTo, invProductMasterService.GetAllProductNames(), ChkAutoComplteTo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadDepartmentsFrom() 
        {
            try
            {
                InvDepartmentService invDepartmentService = new InvDepartmentService();

                Common.SetAutoComplete(TxtSearchCodeFrom, invDepartmentService.GetAllDepartmentCodes(), ChkAutoComplteFrom.Checked);
                Common.SetAutoComplete(TxtSearchNameFrom, invDepartmentService.GetAllDepartmentNames(), ChkAutoComplteFrom.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadDepartmentsTo() 
        {
            try
            {
                InvDepartmentService invDepartmentService = new InvDepartmentService();

                Common.SetAutoComplete(TxtSearchCodeTo, invDepartmentService.GetAllDepartmentCodes(), ChkAutoComplteTo.Checked);
                Common.SetAutoComplete(TxtSearchNameTo, invDepartmentService.GetAllDepartmentNames(), ChkAutoComplteTo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadCategoryFrom()
        {
            try
            {
                InvCategoryService invCategoryService = new InvCategoryService();

                Common.SetAutoComplete(TxtSearchCodeFrom, invCategoryService.GetAllCategoryCodes(), ChkAutoComplteFrom.Checked);
                Common.SetAutoComplete(TxtSearchNameFrom, invCategoryService.GetAllCategoryNames(), ChkAutoComplteFrom.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadCategoryTo()
        {
            try
            {
                InvCategoryService invCategoryService = new InvCategoryService();

                Common.SetAutoComplete(TxtSearchCodeTo, invCategoryService.GetAllCategoryCodes(), ChkAutoComplteTo.Checked);
                Common.SetAutoComplete(TxtSearchNameTo, invCategoryService.GetAllCategoryNames(), ChkAutoComplteTo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadSubCategoryFrom()
        {
            try
            {
                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();

                Common.SetAutoComplete(TxtSearchCodeFrom, invSubCategoryService.GetAllSubCategoryCodes(), ChkAutoComplteFrom.Checked);
                Common.SetAutoComplete(TxtSearchNameFrom, invSubCategoryService.GetAllSubCategoryNames(), ChkAutoComplteFrom.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadSubCategoryTo()
        {
            try
            {
                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();

                Common.SetAutoComplete(TxtSearchCodeTo, invSubCategoryService.GetAllSubCategoryCodes(), ChkAutoComplteTo.Checked);
                Common.SetAutoComplete(TxtSearchNameTo, invSubCategoryService.GetAllSubCategoryNames(), ChkAutoComplteTo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadSearchCodes()
        {
            try
            {
                if (RdoProduct.Checked)
                {
                    if (ChkAutoComplteFrom.Checked) { LoadProductsFrom(); }
                    if (ChkAutoComplteTo.Checked) { LoadProductsTo(); }
                }
                
                if (RdoDepartment.Checked)
                {
                    if (ChkAutoComplteFrom.Checked) { LoadDepartmentsFrom(); }
                    if (ChkAutoComplteTo.Checked) { LoadDepartmentsTo(); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void RdoProduct_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RdoProduct.Checked)
                {
                    LoadProductsFrom();
                    LoadProductsTo();
                }
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void RdoDepartment_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RdoDepartment.Checked)
                {
                    LoadDepartmentsFrom();
                    LoadDepartmentsTo();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void ChkAutoComplteFrom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkAutoComplteFrom.Checked)
                {
                    if (RdoProduct.Checked)
                    {
                        LoadProductsFrom();
                    }
                    if (RdoDepartment.Checked)
                    {
                        LoadDepartmentsFrom();
                    }
                    if (RdoCategory.Checked)
                    {
                        LoadCategoryFrom();
                    }
                    if (RdoSubCategory.Checked)
                    {
                        LoadSubCategoryFrom();
                    }
                }
                else
                {
                    LoadProductsFrom();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void ChkAutoComplteTo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkAutoComplteTo.Checked)
                {
                    if (RdoProduct.Checked)
                    {
                        LoadProductsTo();
                    }

                    if (RdoDepartment.Checked)
                    {
                        LoadDepartmentsTo();
                    }

                    if (RdoCategory.Checked)
                    {
                        LoadCategoryTo();
                    }

                    if (RdoSubCategory.Checked)
                    {
                        LoadSubCategoryTo();
                    }
                }
                else
                {
                    LoadProductsTo();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void TxtSearchCodeFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (!string.IsNullOrEmpty(TxtSearchCodeFrom.Text.Trim())) { TxtSearchNameFrom.Focus(); }
                    TxtSearchCodeTo.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void TxtSearchCodeFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtSearchCodeFrom.Text.Trim())) { return; }
                if (RdoProduct.Checked)
                {
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    InvProductMaster invProductMaster = new InvProductMaster();

                    invProductMaster = invProductMasterService.GetProductsByCode(TxtSearchCodeFrom.Text.Trim());

                    if (invProductMaster != null)
                    {
                        TxtSearchCodeFrom.Text = invProductMaster.ProductCode;
                        TxtSearchNameFrom.Text = invProductMaster.ProductName;
                    }
                }

                if (RdoDepartment.Checked)
                {
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    InvDepartment invDepartment = new InvDepartment();

                    invDepartment = invDepartmentService.GetInvDepartmentsByCode(TxtSearchCodeFrom.Text.Trim(), false);

                    if (invDepartment != null)
                    {
                        TxtSearchCodeFrom.Text = invDepartment.DepartmentCode;
                        TxtSearchNameFrom.Text = invDepartment.DepartmentName;
                    }
                }

                if (RdoCategory.Checked)
                {
                    InvCategoryService invCategoryService = new InvCategoryService();
                    InvCategory invCategory = new InvCategory();

                    invCategory = invCategoryService.GetInvCategoryByCode(TxtSearchCodeFrom.Text.Trim(), false);

                    if (invCategory != null)
                    {
                        TxtSearchCodeFrom.Text = invCategory.CategoryCode;
                        TxtSearchNameFrom.Text = invCategory.CategoryName;
                    }
                }

                if (RdoSubCategory.Checked)
                {
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    InvSubCategory invSubCategory = new InvSubCategory();

                    invSubCategory = invSubCategoryService.GetInvSubCategoryByCode(TxtSearchCodeFrom.Text.Trim(), false);

                    if (invSubCategory != null)
                    {
                        TxtSearchCodeFrom.Text = invSubCategory.SubCategoryCode;
                        TxtSearchNameFrom.Text = invSubCategory.SubCategoryName;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void TxtSearchCodeTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (!string.IsNullOrEmpty(TxtSearchCodeTo.Text.Trim())) { TxtSearchNameTo.Focus(); }
                    TxtSearchNameTo.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void TxtSearchCodeTo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtSearchCodeTo.Text.Trim())) { return; }
                if (RdoProduct.Checked)
                {
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    InvProductMaster invProductMaster = new InvProductMaster();

                    invProductMaster = invProductMasterService.GetProductsByCode(TxtSearchCodeTo.Text.Trim());

                    if (invProductMaster != null)
                    {
                        TxtSearchCodeTo.Text = invProductMaster.ProductCode;
                        TxtSearchNameTo.Text = invProductMaster.ProductName;
                    }
                }

                if (RdoDepartment.Checked)
                {
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    InvDepartment invDepartment = new InvDepartment();

                    invDepartment = invDepartmentService.GetInvDepartmentsByCode(TxtSearchCodeTo.Text.Trim(), false);

                    if (invDepartment != null)
                    {
                        TxtSearchCodeTo.Text = invDepartment.DepartmentCode;
                        TxtSearchNameTo.Text = invDepartment.DepartmentName;
                    }
                }

                if (RdoCategory.Checked)
                {
                    InvCategoryService invCategoryService = new InvCategoryService();
                    InvCategory invCategory = new InvCategory();

                    invCategory = invCategoryService.GetInvCategoryByCode(TxtSearchCodeTo.Text.Trim(), false);

                    if (invCategory != null)
                    {
                        TxtSearchCodeTo.Text = invCategory.CategoryCode;
                        TxtSearchNameTo.Text = invCategory.CategoryName;
                    }
                }

                if (RdoSubCategory.Checked)
                {
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    InvSubCategory invSubCategory = new InvSubCategory();

                    invSubCategory = invSubCategoryService.GetInvSubCategoryByCode(TxtSearchCodeTo.Text.Trim(), false);

                    if (invSubCategory != null)
                    {
                        TxtSearchCodeTo.Text = invSubCategory.SubCategoryCode;
                        TxtSearchNameTo.Text = invSubCategory.SubCategoryName;
                    }
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
                base.ClearForm();

                Common.ClearComboBox(cmbTerminal, cmbLocation);
                cmbLocation.Focus();

                ChkAllTerminals.Checked = false;
                //ChkAllLocations.Checked = false;

                cmbLocation.SelectedValue = Common.LoggedLocationID;
                cmbLocation.Enabled = false;

                TxtSearchCodeFrom.Text = string.Empty;
                TxtSearchCodeTo.Text = string.Empty;
                TxtSearchNameFrom.Text = string.Empty;
                TxtSearchNameTo.Text = string.Empty;

                dtpFromDate.Value = Common.GetSystemDate();
                dtpToDate.Value = Common.GetSystemDate();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void RdoCategory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
               if (RdoCategory.Checked)
                {
                    LoadCategoryFrom();
                    LoadCategoryTo();
                }
               
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void RdoSubCategory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
               if (RdoSubCategory.Checked)
                {
                    LoadSubCategoryFrom();
                    LoadSubCategoryTo();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
    }
}
