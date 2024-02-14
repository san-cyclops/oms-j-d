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
using Report.Inventory.Reference.Reports;

namespace UI.Windows
{
    public partial class FrmGivenDateStock : UI.Windows.FrmBaseMasterForm
    {
        int documentID = 0;
        bool isValidControls = true;
        UserPrivileges accessRights = new UserPrivileges();
        GivenDateStockService givenDateStockService = new GivenDateStockService();


        public FrmGivenDateStock()
        {
            InitializeComponent();
        }

        public override void FormLoad()
        {
            try
            {

                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;
                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);

                dtpGivenDate.Value = Common.GetSystemDate();

                cmbLocation.SelectedValue = Common.LoggedLocationID;
                

                LoadSearchCodes();

                base.FormLoad();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

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

                if (RdoCategory.Checked)
                {
                    if (ChkAutoComplteFrom.Checked) { LoadCategoryFrom(); }
                    if (ChkAutoComplteTo.Checked) { LoadCategoryTo(); }
                }

                if (RdoSubCategory.Checked)
                {
                    if (ChkAutoComplteFrom.Checked) { LoadSubCategoryFrom(); }
                    if (ChkAutoComplteTo.Checked) { LoadSubCategoryTo(); }
                }

                if (RdoSupplier.Checked)
                {
                    if (ChkAutoComplteFrom.Checked) { LoadSupplierFrom(); }
                    if (ChkAutoComplteTo.Checked) { LoadSupplierTo(); }
                }
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
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void LoadSupplierFrom()
        {
            try
            {
                SupplierService supplierService = new SupplierService();

                Common.SetAutoComplete(TxtSearchCodeFrom, supplierService.GetAllSupplierCodes(), ChkAutoComplteFrom.Checked);
                Common.SetAutoComplete(TxtSearchNameFrom, supplierService.GetAllSupplierNames(), ChkAutoComplteFrom.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadSupplierTo()
        {
            try
            {
                SupplierService supplierService = new SupplierService();

                Common.SetAutoComplete(TxtSearchCodeTo, supplierService.GetAllSupplierCodes(), ChkAutoComplteTo.Checked);
                Common.SetAutoComplete(TxtSearchNameTo, supplierService.GetAllSupplierNames(), ChkAutoComplteTo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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

                    if (RdoSupplier.Checked)
                    {
                        LoadSupplierFrom();
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

                    if (RdoSupplier.Checked)
                    {
                        LoadSupplierTo();
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

                if (RdoSupplier.Checked)
                {
                    SupplierService supplierService = new SupplierService();
                    Supplier supplier = new Supplier();

                    supplier = supplierService.GetSupplierByCode(TxtSearchCodeFrom.Text.Trim());

                    if (supplier != null)
                    {
                        TxtSearchCodeFrom.Text = supplier.SupplierCode;
                        TxtSearchNameFrom.Text = supplier.SupplierName;
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

                if (RdoSupplier.Checked)
                {
                    SupplierService supplierService = new SupplierService();
                    Supplier supplier = new Supplier();

                    supplier = supplierService.GetSupplierByCode(TxtSearchCodeTo.Text.Trim());

                    if (supplier != null)
                    {
                        TxtSearchCodeTo.Text = supplier.SupplierCode;
                        TxtSearchNameTo.Text = supplier.SupplierName;
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

                Common.ClearComboBox(cmbLocation);
                cmbLocation.Focus();

                cmbLocation.SelectedValue = Common.LoggedLocationID;
                cmbLocation.Enabled = true;

                TxtSearchCodeFrom.Text = string.Empty;
                TxtSearchCodeTo.Text = string.Empty;
                TxtSearchNameFrom.Text = string.Empty;
                TxtSearchNameTo.Text = string.Empty;

                dtpGivenDate.Value = Common.GetSystemDate();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dateGiven;

                int locationId = 0;
                long fromId = 0;
                long toId = 0;
                int uniqueId = 0;
                int typeId = 0;
                

                dateGiven = dtpGivenDate.Value;

                if (ChkAllLocations.Checked == false)
                {
                    if (ValidateLocationComboBoxes().Equals(false)) { return; }
                }

                if (ValidateControls() == false) return;


                if (ChkAllLocations.Checked == true) { locationId = 0; } else { locationId = cmbLocation.SelectedIndex + 1; }
                
                if (RdoProduct.Checked == true) { typeId = 1; }
                else if (RdoDepartment.Checked == true) { typeId = 2; }
                else if (RdoCategory.Checked == true) { typeId = 3; }
                else if (RdoSubCategory.Checked == true) { typeId = 4; }
                else if (RdoSupplier.Checked == true) { typeId = 5; }


                if (givenDateStockService.View(typeId, locationId, dateGiven, fromId, toId, uniqueId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()) == true)
                {
                    ViewReport(locationId);
                }          
               
            }

            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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

        private void ViewReport(int locationId)
        {

            FrmReportViewer objReportView = new FrmReportViewer();
            Cursor.Current = Cursors.WaitCursor;

            if (ChkAllLocations.Checked == true)
            {
                InvRptGivenDateStockProductWise1 invRptGivenDateStockProductWise1 = new InvRptGivenDateStockProductWise1();


                ConnectionInfo crconnectioninfo = new ConnectionInfo();
                ReportDocument cryrpt = new ReportDocument();
                TableLogOnInfos crtablelogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtablelogoninfo = new TableLogOnInfo();

                Tables CrTables;

                string serverName; string databaseName; string userName; string password;
                CommonService.GetConnectionProperties(out serverName, out databaseName, out userName, out password);

                crconnectioninfo.ServerName = serverName;
                crconnectioninfo.DatabaseName = databaseName;
                crconnectioninfo.UserID = userName;
                crconnectioninfo.Password = password;



                CrTables = invRptGivenDateStockProductWise1.Database.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtablelogoninfo = CrTable.LogOnInfo;
                    crtablelogoninfo.ConnectionInfo = crconnectioninfo;
                    CrTable.ApplyLogOnInfo(crtablelogoninfo);
                }




                invRptGivenDateStockProductWise1.SummaryInfo.ReportTitle = "Given Date Stock - Department Wise All Location Report";
                invRptGivenDateStockProductWise1.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                if (ChkAllLocations.Checked == false)
                    invRptGivenDateStockProductWise1.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";
                else
                    invRptGivenDateStockProductWise1.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";
                invRptGivenDateStockProductWise1.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invRptGivenDateStockProductWise1.DataDefinition.FormulaFields["GivenDate"].Text = "'" + dtpGivenDate.Value + "'";
                invRptGivenDateStockProductWise1.DataDefinition.FormulaFields["CodeFrom"].Text = "'" + TxtSearchCodeFrom.Text.Trim() + " " + TxtSearchNameFrom.Text.Trim() + "'";
                invRptGivenDateStockProductWise1.DataDefinition.FormulaFields["CodeTo"].Text = "'" + TxtSearchCodeTo.Text.Trim() + " " + TxtSearchNameTo.Text.Trim() + "'";

                invRptGivenDateStockProductWise1.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invRptGivenDateStockProductWise1.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invRptGivenDateStockProductWise1.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invRptGivenDateStockProductWise1.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                invRptGivenDateStockProductWise1.DataDefinition.FormulaFields["UserID"].Text = "'" + Common.LoggedUserId.ToString().Trim() + "'";

                objReportView.crRptViewer.ReportSource = invRptGivenDateStockProductWise1;
            }
            else if (RdoProduct.Checked == true)
            {
                InvRptGivenDateStockProductWise invRptGivenDateStockProductWise = new InvRptGivenDateStockProductWise();
                if (ChkAllLocations.Checked == true)
                {
                    invRptGivenDateStockProductWise.SetDataSource(givenDateStockService.GetGivenDateStockAllLocation(TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }
                else
                {
                    invRptGivenDateStockProductWise.SetDataSource(givenDateStockService.GetGivenDateStock(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }

               
                invRptGivenDateStockProductWise.SetDataSource(givenDateStockService.DsGetGivenDateStock.Tables["GivenDateStock"]);

                invRptGivenDateStockProductWise.SummaryInfo.ReportTitle = "Given Date Stock - Product Wise Report";
                invRptGivenDateStockProductWise.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                if (ChkAllLocations.Checked == false)
                    invRptGivenDateStockProductWise.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";
                else
                    invRptGivenDateStockProductWise.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";
                invRptGivenDateStockProductWise.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invRptGivenDateStockProductWise.DataDefinition.FormulaFields["GivenDate"].Text = "'" + dtpGivenDate.Value + "'";
                invRptGivenDateStockProductWise.DataDefinition.FormulaFields["CodeFrom"].Text = "'" + TxtSearchCodeFrom.Text.Trim() + " " + TxtSearchNameFrom.Text.Trim() + "'";
                invRptGivenDateStockProductWise.DataDefinition.FormulaFields["CodeTo"].Text = "'" + TxtSearchCodeTo.Text.Trim() + " " + TxtSearchNameTo.Text.Trim() + "'";
                
                invRptGivenDateStockProductWise.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invRptGivenDateStockProductWise.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invRptGivenDateStockProductWise.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invRptGivenDateStockProductWise.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                objReportView.crRptViewer.ReportSource = invRptGivenDateStockProductWise;

            }

            else if (RdoDepartment.Checked == true)
            {
                InvRptGivenDateStockDepartmentWise invRptGivenDateStockDepartmentWise = new InvRptGivenDateStockDepartmentWise();
                if (ChkAllLocations.Checked == true)
                {
                    invRptGivenDateStockDepartmentWise.SetDataSource(givenDateStockService.GetGivenDateStockDepartmentWiseAllLocation(TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }
                else
                {
                    invRptGivenDateStockDepartmentWise.SetDataSource(givenDateStockService.GetGivenDateStockDepartmentWise(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }


                invRptGivenDateStockDepartmentWise.SetDataSource(givenDateStockService.DsGetGivenDateStock.Tables["GivenDateStock"]);

                invRptGivenDateStockDepartmentWise.SummaryInfo.ReportTitle = "Given Date Stock - Department Wise Report";
                invRptGivenDateStockDepartmentWise.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                if (ChkAllLocations.Checked == false)
                    invRptGivenDateStockDepartmentWise.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";
                else
                    invRptGivenDateStockDepartmentWise.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";
                invRptGivenDateStockDepartmentWise.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invRptGivenDateStockDepartmentWise.DataDefinition.FormulaFields["GivenDate"].Text = "'" + dtpGivenDate.Value + "'";
                invRptGivenDateStockDepartmentWise.DataDefinition.FormulaFields["CodeFrom"].Text = "'" + TxtSearchCodeFrom.Text.Trim() + " " + TxtSearchNameFrom.Text.Trim() + "'";
                invRptGivenDateStockDepartmentWise.DataDefinition.FormulaFields["CodeTo"].Text = "'" + TxtSearchCodeTo.Text.Trim() + " " + TxtSearchNameTo.Text.Trim() + "'";

                invRptGivenDateStockDepartmentWise.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invRptGivenDateStockDepartmentWise.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invRptGivenDateStockDepartmentWise.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invRptGivenDateStockDepartmentWise.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                objReportView.crRptViewer.ReportSource = invRptGivenDateStockDepartmentWise;
            }

            else if (RdoCategory.Checked == true)
            {
                InvRptGivenDateStockCategoryWise invRptGivenDateStockCategoryWise = new InvRptGivenDateStockCategoryWise();
                if (ChkAllLocations.Checked == true)
                {
                    invRptGivenDateStockCategoryWise.SetDataSource(givenDateStockService.GetGivenDateStockCategoryWiseAllLocation(TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }
                else
                {
                    invRptGivenDateStockCategoryWise.SetDataSource(givenDateStockService.GetGivenDateStockCategoryWise(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }


                invRptGivenDateStockCategoryWise.SetDataSource(givenDateStockService.DsGetGivenDateStock.Tables["GivenDateStock"]);

                invRptGivenDateStockCategoryWise.SummaryInfo.ReportTitle = "Given Date Stock - Category Wise Report";
                invRptGivenDateStockCategoryWise.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                if (ChkAllLocations.Checked == false)
                    invRptGivenDateStockCategoryWise.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";
                else
                    invRptGivenDateStockCategoryWise.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";
                invRptGivenDateStockCategoryWise.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invRptGivenDateStockCategoryWise.DataDefinition.FormulaFields["GivenDate"].Text = "'" + dtpGivenDate.Value + "'";
                invRptGivenDateStockCategoryWise.DataDefinition.FormulaFields["CodeFrom"].Text = "'" + TxtSearchCodeFrom.Text.Trim() + " " + TxtSearchNameFrom.Text.Trim() + "'";
                invRptGivenDateStockCategoryWise.DataDefinition.FormulaFields["CodeTo"].Text = "'" + TxtSearchCodeTo.Text.Trim() + " " + TxtSearchNameTo.Text.Trim() + "'";

                invRptGivenDateStockCategoryWise.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invRptGivenDateStockCategoryWise.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invRptGivenDateStockCategoryWise.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invRptGivenDateStockCategoryWise.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                objReportView.crRptViewer.ReportSource = invRptGivenDateStockCategoryWise;
            }

            else if (RdoSubCategory.Checked == true)
            {
                InvRptGivenDateStockSubCategoryWise invRptGivenDateStockSubCategoryWise = new InvRptGivenDateStockSubCategoryWise();
                if (ChkAllLocations.Checked == true)
                {
                    invRptGivenDateStockSubCategoryWise.SetDataSource(givenDateStockService.GetGivenDateStockSubCategoryWiseAllLocation(TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }
                else
                {
                    invRptGivenDateStockSubCategoryWise.SetDataSource(givenDateStockService.GetGivenDateStockSubCategoryWise(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }


                invRptGivenDateStockSubCategoryWise.SetDataSource(givenDateStockService.DsGetGivenDateStock.Tables["GivenDateStock"]);

                invRptGivenDateStockSubCategoryWise.SummaryInfo.ReportTitle = "Given Date Stock - Sub Category Wise Report";
                invRptGivenDateStockSubCategoryWise.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                if (ChkAllLocations.Checked == false)
                    invRptGivenDateStockSubCategoryWise.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";
                else
                    invRptGivenDateStockSubCategoryWise.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";
                invRptGivenDateStockSubCategoryWise.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invRptGivenDateStockSubCategoryWise.DataDefinition.FormulaFields["GivenDate"].Text = "'" + dtpGivenDate.Value + "'";
                invRptGivenDateStockSubCategoryWise.DataDefinition.FormulaFields["CodeFrom"].Text = "'" + TxtSearchCodeFrom.Text.Trim() + " " + TxtSearchNameFrom.Text.Trim() + "'";
                invRptGivenDateStockSubCategoryWise.DataDefinition.FormulaFields["CodeTo"].Text = "'" + TxtSearchCodeTo.Text.Trim() + " " + TxtSearchNameTo.Text.Trim() + "'";

                invRptGivenDateStockSubCategoryWise.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invRptGivenDateStockSubCategoryWise.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invRptGivenDateStockSubCategoryWise.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invRptGivenDateStockSubCategoryWise.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                objReportView.crRptViewer.ReportSource = invRptGivenDateStockSubCategoryWise;
            }

            else if (RdoSupplier.Checked == true)
            {
                InvRptGivenDateStockSupplierWise invRptGivenDateStockSupplierWise = new InvRptGivenDateStockSupplierWise();
                if (ChkAllLocations.Checked == true)
                {
                    invRptGivenDateStockSupplierWise.SetDataSource(givenDateStockService.GetGivenDateStockSupplierWiseAllLocation(TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }
                else
                {
                    invRptGivenDateStockSupplierWise.SetDataSource(givenDateStockService.GetGivenDateStockSupplierWise(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }


                invRptGivenDateStockSupplierWise.SetDataSource(givenDateStockService.DsGetGivenDateStock.Tables["GivenDateStock"]);

                invRptGivenDateStockSupplierWise.SummaryInfo.ReportTitle = "Given Date Stock - Supplier Wise Report";
                invRptGivenDateStockSupplierWise.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                if (ChkAllLocations.Checked == false)
                    invRptGivenDateStockSupplierWise.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";
                else
                    invRptGivenDateStockSupplierWise.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";
                invRptGivenDateStockSupplierWise.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invRptGivenDateStockSupplierWise.DataDefinition.FormulaFields["GivenDate"].Text = "'" + dtpGivenDate.Value + "'";
                invRptGivenDateStockSupplierWise.DataDefinition.FormulaFields["CodeFrom"].Text = "'" + TxtSearchCodeFrom.Text.Trim() + " " + TxtSearchNameFrom.Text.Trim() + "'";
                invRptGivenDateStockSupplierWise.DataDefinition.FormulaFields["CodeTo"].Text = "'" + TxtSearchCodeTo.Text.Trim() + " " + TxtSearchNameTo.Text.Trim() + "'";

                invRptGivenDateStockSupplierWise.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invRptGivenDateStockSupplierWise.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invRptGivenDateStockSupplierWise.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invRptGivenDateStockSupplierWise.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                objReportView.crRptViewer.ReportSource = invRptGivenDateStockSupplierWise;
            }

            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void RdoProduct_CheckedChanged(object sender, EventArgs e)
        {

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

        private void RdoSupplier_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RdoSupplier.Checked)
                {
                    LoadSupplierFrom();
                    LoadSupplierTo();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        
        
    }
}
