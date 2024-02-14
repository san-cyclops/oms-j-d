using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using CrystalDecisions.CrystalReports.Engine;
using Domain;
using Report.Inventory.Reference.Reports;
using Utility;
using Service;
using Report.Inventory.Transactions.Reports;
using System.Collections;
using Report.Com.Transactions.Reports;
using CrystalDecisions.Shared;

namespace Report.Inventory 
{
    /// <summary>
    /// 
    /// </summary>
    class ReportSources
    {
        public DataTable reportData { get; set; }
        public ArrayList newSumFieldsIndexes { get; set; }
    }

    public class InvReportGenerator
    {
        //public FrmReprotGenerator frmReprotGenerator { get; set; }

        string strFieldName = string.Empty;
        string groupingFields = string.Empty;

        /// <summary>
        /// Generate Reference Report 
        /// </summary>
        /// <param name="autoGenerateInfo"></param>
        /// <param name="documentNo"></param>
        /// <param name="isOrg"></param>
        public void GenearateReferenceReport(AutoGenerateInfo autoGenerateInfo, string documentNo, bool isOrg)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            Cursor.Current = Cursors.WaitCursor;
            DataTable reportData = new DataTable();

            InvRptColumn5Template invRptColumn5Template = new InvRptColumn5Template();
            InvRptReferenceDetailTemplate invRptReferenceDetailTemplate = new InvRptReferenceDetailTemplate();
            string departmentText = "", categoryText = "", subCategoryText = "", subCategory2Text = "";

            string[] stringField = new string[] { };

            switch (autoGenerateInfo.FormName)
            {
                #region Department
                case "FrmDepartment":
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
                    categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
                    bool isDependCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend;

                    reportData = invDepartmentService.GetAllInvDepartmentDataTable(isDependCategory);
                    stringField = new string[] { "Code", departmentText, "Remark" };

                    invRptColumn5Template.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptColumn5Template.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptColumn5Template.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptColumn5Template.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptColumn5Template.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptColumn5Template;
                    break;
                #endregion
                #region Category
                case "FrmCategory":
                    InvCategoryService invCategoryService = new InvCategoryService();
                    departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
                    categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;

                    if (autoGenerateInfo.IsDepend)
                    {
                        reportData = invCategoryService.GetAllDepartmentWiseCategoryDataTable();
                        stringField = new string[] { "Code", categoryText, departmentText, "Remark" };
                    }
                    else
                    {
                        reportData = invCategoryService.GetAllCategoryDataTable();
                        stringField = new string[] { "Code", categoryText, "Remark" };
                    }
                    
                    invRptColumn5Template.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptColumn5Template.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptColumn5Template.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptColumn5Template.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptColumn5Template.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptColumn5Template;
                    break;
                #endregion
                #region Sub Category
                case "FrmSubCategory":
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
                    subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;

                    if (autoGenerateInfo.IsDepend)
                    {
                        reportData = invSubCategoryService.GetAllCategoryWiseSubCategoryDataTable();
                        stringField = new string[] { "Code", subCategoryText, categoryText, "Remark" };
                    }
                    else
                    {
                        reportData = invSubCategoryService.GetAllSubCategoryDataTable();
                        stringField = new string[] { "Code", subCategoryText, "Remark" };
                    }

                    invRptColumn5Template.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptColumn5Template.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptColumn5Template.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptColumn5Template.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptColumn5Template.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptColumn5Template;
                    break;
                #endregion
                #region Sub Category 2
                case "FrmSubCategory2":
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
                    subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;

                    if (autoGenerateInfo.IsDepend)
                    {
                        reportData = invSubCategory2Service.GetAllSubCategoryWiseSub2CategoryDataTable();
                        stringField = new string[] { "Code", subCategory2Text, subCategoryText, "Remark" };
                    }
                    else
                    {
                        reportData = invSubCategory2Service.GetAllSub2CategoryDataTable();
                        stringField = new string[] { "Code", subCategory2Text, "Remark" };
                    }

                    invRptColumn5Template.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptColumn5Template.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptColumn5Template.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptColumn5Template.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptColumn5Template.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptColumn5Template;
                    break;
                #endregion
                #region Product
                    //{ "Code", "Product Name", "Name on Invoice", departmentText, categoryText, subCategoryText, subCategory2Text, "Bar Code", "Re-order Qty", "Cost Price", "Selling Price", "Fixed Discount", "Batch Status", "Remark" };
                case "FrmProduct":
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    InvRptProductTemplate invRptProductTemplate = new InvRptProductTemplate();
                    departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
                    categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
                    subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
                    subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;

                    reportData = invProductMasterService.GetAllProductDataTable();
                    stringField = new string[] { "Code", "Product Name", "Name on Invoice", departmentText, categoryText, subCategoryText, subCategory2Text, "Bar Code", "Re-order Qty", "Cost Price", "Selling Price", "Fixed Discount", "Batch Status", "Promotion Status" };

                    invRptProductTemplate.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptProductTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptProductTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptProductTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptProductTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptProductTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptProductTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptProductTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptProductTemplate;
                    break;
                #endregion
                #region Supplier
                case "FrmSupplier": // Supplier
                    SupplierService supplierService = new SupplierService();
                    reportData = supplierService.GetAllSupplierDataTable();

                    // Set field text
                    stringField = new string[] { "Code", "Sup.Name", "Billing Address", "Telephone", "NIC", "Rec.Date", "Payment Method", "Contact Person", "Delivery Address", "Remark" };
                    invRptReferenceDetailTemplate.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptReferenceDetailTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptReferenceDetailTemplate;
                    break;
                #endregion
                #region SalesPerson
                case "FrmSalesPerson": // SalesPerson
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    reportData = invSalesPersonService.GetAllInvSalesPersonsDataTable();

                    // Set field text
                    stringField = new string[] { "Code", "Sales Person", "Address", "Telephone", "NIC", "Rec.Date", "Type", "Remark" };
                    invRptReferenceDetailTemplate.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptReferenceDetailTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptReferenceDetailTemplate;
                    break;
                #endregion

                #region Employee
                case "FrmEmployee": // Employee
                    EmployeeService employeeService = new EmployeeService();
                    reportData = employeeService.GetAllEmployeeDataTable();

                    // Set field text
                    stringField = new string[] { "Code", "Name", "Mobile", "Telephone", "NIC", "Address", "Remark" };
                    invRptReferenceDetailTemplate.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptReferenceDetailTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptReferenceDetailTemplate;
                    break;
                #endregion
                default:
                    break;
            }

            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
        }

        public void GenearateReferenceSummaryReport(AutoGenerateInfo autoGenerateInfo, DataTable dtReportData, DataTable dtReportConditions, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> groupByStructList)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            InvRptColumn5Template invRptColumn5Template = new InvRptColumn5Template();
            InvRptReferenceDetailTemplate invRptReferenceDetailTemplate = new InvRptReferenceDetailTemplate();
            Cursor.Current = Cursors.WaitCursor;

            DataTable dtArrangedReportData = new DataTable();
            List<Common.ReportDataStruct> selectedReportStructList;
            List<Common.ReportDataStruct> selectedGroupStructList;
            string strFieldName = string.Empty, strReportTitle = string.Empty;
            StringBuilder sbGroupedTitle = new StringBuilder();
            int sr = 1, dc = 12;
            int gr = 0, gp = 1;

            switch (autoGenerateInfo.FormName)
            {
                #region Department
                case "FrmDepartment":
                    #region Set Values for report header Fields
                    strFieldName = string.Empty;
                    sr = 1; dc = 12;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {
                            if (item.ReportDataType.Equals(typeof(string)))
                            {
                                strFieldName = "st" + sr;
                                invRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                sr++;
                            }

                            //if (item.ReportDataType.Equals(typeof(decimal)))
                            //{
                            //    strFieldName = "st" + dc;
                            //    comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                            //    dc++;
                            //}
                        }
                    }

                    #endregion

                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    invRptColumn5Template.SetDataSource(dtArrangedReportData);
                    //invRptColumn5Template.SetDataSource(dtReportData);
                    invRptColumn5Template.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    invRptColumn5Template.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    invRptColumn5Template.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptColumn5Template.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    #region Set Header - old
                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    if (reportDataStructList[i].IsSelectionField.Equals(true))
                    //    {
                    //        strFieldName = "st" + (i + 1);
                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //        {
                    //            invRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            invRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }
                    //    }
                    //}
                    #endregion
                    #region Group By - old

                    //for (int i = 0; i <= 3; i++)
                    //{

                    //    if (groupByStructList.Count > i)
                    //    {
                    //        if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    //        {
                    //            string strGroup = groupByStructList[i].ReportField.ToString();
                    //            invRptColumn5Template.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //            invRptColumn5Template.SetParameterValue(i, "");
                    //    }
                    //    else
                    //    { invRptColumn5Template.SetParameterValue(i, ""); }
                    //}
                    #endregion

                    #region Group By

                    selectedReportStructList = reportDataStructList.Where(c => c.IsSelectionField.Equals(true)).ToList();
                    selectedGroupStructList = groupByStructList.Where(c => c.IsResultGroupBy.Equals(true)).ToList();

                    gr = 0; gp = 1;
                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        // Set report group field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gr < invRptColumn5Template.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    invRptColumn5Template.DataDefinition.Groups[gr].ConditionField = invRptColumn5Template.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp - 1 < invRptColumn5Template.DataDefinition.Groups.Count)
                            {
                                if (invRptColumn5Template.ParameterFields[gp - 1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        invRptColumn5Template.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        invRptColumn5Template.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < invRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptColumn5Template.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptColumn5Template.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < invRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptColumn5Template.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptColumn5Template.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptColumn5Template;
                    break;
                #endregion
                #region Category
                case "FrmCategory":
                    #region Set Values for report header Fields
                    strFieldName = string.Empty;
                    sr = 1; dc = 12;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {
                            if (item.ReportDataType.Equals(typeof(string)))
                            {
                                strFieldName = "st" + sr;
                                invRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                sr++;
                            }

                            //if (item.ReportDataType.Equals(typeof(decimal)))
                            //{
                            //    strFieldName = "st" + dc;
                            //    comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                            //    dc++;
                            //}
                        }
                    }

                    #endregion

                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    invRptColumn5Template.SetDataSource(dtArrangedReportData);
                    //invRptColumn5Template.SetDataSource(dtReportData);
                    invRptColumn5Template.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    invRptColumn5Template.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    invRptColumn5Template.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptColumn5Template.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    #region Set Header - old
                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    if (reportDataStructList[i].IsSelectionField.Equals(true))
                    //    {
                    //        strFieldName = "st" + (i + 1);
                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //        {
                    //            invRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            invRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }
                    //    }
                    //}
                    #endregion
                    #region Group By - old

                    //for (int i = 0; i <= 3; i++)
                    //{
                    //    invRptColumn5Template.SetParameterValue(i, "");
                    //    if (groupByStructList.Count > i)
                    //    {
                    //        if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    //        {
                    //            string strGroup = groupByStructList[i].ReportField.ToString();
                    //            invRptColumn5Template.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //        {

                    //            switch (groupByStructList[i].ValueDataType.ToString())
                    //            {
                    //                case "System.String":
                    //                    invRptColumn5Template.SetParameterValue(i, "");
                    //                    break;
                    //                case "System.DateTime":
                    //                    invRptColumn5Template.SetParameterValue(i, "02-02-2013");
                    //                    break;
                    //                case "System.bool":
                    //                    invRptColumn5Template.SetParameterValue(i, true);
                    //                    break;
                    //                case "System.Int32":
                    //                    invRptColumn5Template.SetParameterValue(i, 0);
                    //                    break;
                    //                case "System.Int64":
                    //                    invRptColumn5Template.SetParameterValue(i, 0);
                    //                    break;
                    //                default:

                    //                    break;
                    //            }
                    //        }
                    //    }


                    //}
                    #endregion

                    #region Group By

                    selectedReportStructList = reportDataStructList.Where(c => c.IsSelectionField.Equals(true)).ToList();
                    selectedGroupStructList = groupByStructList.Where(c => c.IsResultGroupBy.Equals(true)).ToList();

                    gr = 0; gp = 1;
                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        // Set report group field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gr < invRptColumn5Template.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    invRptColumn5Template.DataDefinition.Groups[gr].ConditionField = invRptColumn5Template.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp - 1 < invRptColumn5Template.DataDefinition.Groups.Count)
                            {
                                if (invRptColumn5Template.ParameterFields[gp - 1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        invRptColumn5Template.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        invRptColumn5Template.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < invRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptColumn5Template.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptColumn5Template.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < invRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptColumn5Template.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptColumn5Template.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptColumn5Template;
                    break;
                    #endregion
                #region Sub Category
                case "FrmSubCategory":
                    #region Set Values for report header Fields
                    strFieldName = string.Empty;
                    sr = 1; dc = 12;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {
                            if (item.ReportDataType.Equals(typeof(string)))
                            {
                                strFieldName = "st" + sr;
                                invRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                sr++;
                            }

                            //if (item.ReportDataType.Equals(typeof(decimal)))
                            //{
                            //    strFieldName = "st" + dc;
                            //    comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                            //    dc++;
                            //}
                        }
                    }

                    #endregion

                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    invRptColumn5Template.SetDataSource(dtArrangedReportData);
                    //invRptColumn5Template.SetDataSource(dtReportData);
                    invRptColumn5Template.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    invRptColumn5Template.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    invRptColumn5Template.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptColumn5Template.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    #region Set Header - old
                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    if (reportDataStructList[i].IsSelectionField.Equals(true))
                    //    {
                    //        strFieldName = "st" + (i + 1);
                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //        {
                    //            invRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            invRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }
                    //    }
                    //}
                    #endregion
                    #region Group By - old

                    //for (int i = 0; i <= 3; i++)
                    //{

                    //    if (groupByStructList.Count > i)
                    //    {
                    //        if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    //        {
                    //            string strGroup = groupByStructList[i].ReportField.ToString();
                    //            invRptColumn5Template.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //            invRptColumn5Template.SetParameterValue(i, "");
                    //    }
                    //    else
                    //    { invRptColumn5Template.SetParameterValue(i, ""); }
                    //}
                    #endregion

                    #region Group By

                    selectedReportStructList = reportDataStructList.Where(c => c.IsSelectionField.Equals(true)).ToList();
                    selectedGroupStructList = groupByStructList.Where(c => c.IsResultGroupBy.Equals(true)).ToList();

                    gr = 0; gp = 1;
                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        // Set report group field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gr < invRptColumn5Template.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    invRptColumn5Template.DataDefinition.Groups[gr].ConditionField = invRptColumn5Template.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp - 1 < invRptColumn5Template.DataDefinition.Groups.Count)
                            {
                                if (invRptColumn5Template.ParameterFields[gp - 1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        invRptColumn5Template.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        invRptColumn5Template.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < invRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptColumn5Template.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptColumn5Template.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < invRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptColumn5Template.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptColumn5Template.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptColumn5Template;
                    break;
                #endregion
                #region Sub Category 2
                case "FrmSubCategory2":
                    #region Set Values for report header Fields
                    strFieldName = string.Empty;
                    sr = 1; dc = 12;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {
                            if (item.ReportDataType.Equals(typeof(string)))
                            {
                                strFieldName = "st" + sr;
                                invRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                sr++;
                            }

                            //if (item.ReportDataType.Equals(typeof(decimal)))
                            //{
                            //    strFieldName = "st" + dc;
                            //    comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                            //    dc++;
                            //}
                        }
                    }

                    #endregion

                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    invRptColumn5Template.SetDataSource(dtArrangedReportData);
                    //invRptColumn5Template.SetDataSource(dtReportData);
                    invRptColumn5Template.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    invRptColumn5Template.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    invRptColumn5Template.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptColumn5Template.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptColumn5Template.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    #region Set Header - old
                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    if (reportDataStructList[i].IsSelectionField.Equals(true))
                    //    {
                    //        strFieldName = "st" + (i + 1);
                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //        {
                    //            invRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            invRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }
                    //    }
                    //}
                    #endregion
                    #region Group By - old

                    //for (int i = 0; i <= 3; i++)
                    //{

                    //    if (groupByStructList.Count > i)
                    //    {
                    //        if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    //        {
                    //            string strGroup = groupByStructList[i].ReportField.ToString();
                    //            invRptColumn5Template.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //            invRptColumn5Template.SetParameterValue(i, "");
                    //    }
                    //    else
                    //    { invRptColumn5Template.SetParameterValue(i, ""); }
                    //}
                    #endregion

                    #region Group By

                    selectedReportStructList = reportDataStructList.Where(c => c.IsSelectionField.Equals(true)).ToList();
                    selectedGroupStructList = groupByStructList.Where(c => c.IsResultGroupBy.Equals(true)).ToList();

                    gr = 0; gp = 1;
                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        // Set report group field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gr < invRptColumn5Template.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    invRptColumn5Template.DataDefinition.Groups[gr].ConditionField = invRptColumn5Template.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp - 1 < invRptColumn5Template.DataDefinition.Groups.Count)
                            {
                                if (invRptColumn5Template.ParameterFields[gp - 1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        invRptColumn5Template.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        invRptColumn5Template.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < invRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptColumn5Template.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptColumn5Template.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < invRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptColumn5Template.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptColumn5Template.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptColumn5Template;
                    break;
                #endregion
                #region Product
                case "FrmProduct":
                    InvRptProductTemplate invRptProductTemplate = new InvRptProductTemplate();
                    #region Set Values for report header Fields
                    strFieldName = string.Empty;
                    sr = 1; dc = 12;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {
                            if (item.ReportDataType.Equals(typeof(string)))
                            {
                                strFieldName = "st" + sr;
                                if (GetSelectedReportDataStruct(groupByStructList, item.ReportFieldName.Trim()).IsResultGroupBy)
                                { 
                                    invRptProductTemplate.DataDefinition.FormulaFields[strFieldName].Text = "''";
                                    //sbGroupedTitle.Append(string.Concat(item.ReportFieldName.Trim(), ", "));
                                    groupingFields = string.IsNullOrEmpty(groupingFields) ? (item.ReportFieldName.Trim()) : (groupingFields + ", " + item.ReportFieldName.Trim());
                                }
                                else
                                {invRptProductTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";}
                                sr++;
                            }

                            //if (item.ReportDataType.Equals(typeof(decimal)))
                            //{
                            //    strFieldName = "st" + dc;
                            //    comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                            //    dc++;
                            //}
                        }

                        if(item.IsUntickedField.Equals(true) && item.IsSelectionField.Equals(true) && item.DbColumnName.Equals("StockEntry"))
                        {
                            strReportTitle = autoGenerateInfo.FormText + " List - Stock Entry Sheet";
                        }
                        else
                        {
                            strReportTitle = autoGenerateInfo.FormText + " List";
                        }
                    }

                    //if (groupByStructList.Count > 0)
                    //{
                    //    sbGroupedTitle.Remove(sbGroupedTitle.Length - 2, 2); // remove last ','
                    //    sbGroupedTitle.Append(" wise");
                    //}
                    //else { sbGroupedTitle.Append(" "); }

                    #endregion

                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    invRptProductTemplate.SetDataSource(dtArrangedReportData);

                    //invRptProductTemplate.SetDataSource(dtReportData);
                    invRptProductTemplate.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    invRptProductTemplate.SummaryInfo.ReportTitle = strReportTitle;

                    invRptProductTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptProductTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptProductTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptProductTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptProductTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //invRptProductTemplate.DataDefinition.FormulaFields["GroupTitle"].Text = "'" + sbGroupedTitle + "'";
                    invRptProductTemplate.DataDefinition.FormulaFields["GroupNames"].Text = "'" + (string.IsNullOrEmpty(groupingFields) ? "" : groupingFields + " wise") + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    #region Set Header - old
                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    if (reportDataStructList[i].IsSelectionField.Equals(true))
                    //    {
                    //        strFieldName = "st" + (i + 1);
                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //        {
                    //            invRptProductTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            invRptProductTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        strFieldName = "st" + (i + 1);
                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //        {
                    //            invRptProductTemplate.DataDefinition.FormulaFields[strFieldName].Text = "''";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            invRptProductTemplate.DataDefinition.FormulaFields[strFieldName].Text = "''";
                    //        }
                    //    }
                    //}
                    #endregion
                    #region Group By - old

                    //for (int i = 0; i <= 3; i++)
                    //{
                    //    invRptReferenceDetailTemplate.SetParameterValue(i, "");
                    //    if (groupByStructList.Count > i)
                    //    {
                    //        if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    //        {
                    //            string strGroup = groupByStructList[i].ReportField.ToString();
                    //            invRptProductTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //            invRptProductTemplate.SetParameterValue(i, "");
                    //    }
                    //    else
                    //    { invRptProductTemplate.SetParameterValue(i, ""); }
                    //}
                    #endregion

                    #region Group By
                    selectedReportStructList = reportDataStructList.Where(c => c.IsSelectionField.Equals(true)).ToList();
                    selectedGroupStructList = groupByStructList.Where(c => c.IsResultGroupBy.Equals(true)).ToList();

                    gr = 0; gp = 1;
                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        // Set report group field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gr < invRptProductTemplate.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    invRptProductTemplate.DataDefinition.Groups[gr].ConditionField = invRptProductTemplate.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp - 1 < invRptProductTemplate.DataDefinition.Groups.Count)
                            {
                                if (invRptProductTemplate.ParameterFields[gp - 1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        invRptProductTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        invRptProductTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < invRptProductTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptProductTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptProductTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < invRptProductTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptProductTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptProductTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptProductTemplate;
                    break;
                #endregion
                #region Supplier
                case "FrmSupplier":
                    #region Set Values for report header Fields
                    strFieldName = string.Empty;
                    sr = 1; dc = 12;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {
                            if (item.ReportDataType.Equals(typeof(string)))
                            {
                                strFieldName = "st" + sr;
                                invRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                sr++;
                            }

                            //if (item.ReportDataType.Equals(typeof(decimal)))
                            //{
                            //    strFieldName = "st" + dc;
                            //    comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                            //    dc++;
                            //}
                        }
                    }

                    #endregion

                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    invRptReferenceDetailTemplate.SetDataSource(dtArrangedReportData);
                    //invRptReferenceDetailTemplate.SetDataSource(dtReportData);
                    invRptReferenceDetailTemplate.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    invRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    invRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    #region Set Header - old
                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    if (reportDataStructList[i].IsSelectionField.Equals(true))
                    //    {
                    //        strFieldName = "st" + (i + 1);
                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //        {
                    //            invRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            invRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }
                    //    }
                    //}
                    #endregion
                    #region Group By - old

                    //for (int i = 0; i <= 3; i++)
                    //{

                    //    if (groupByStructList.Count > i)
                    //    {
                    //        if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    //        {
                    //            string strGroup = groupByStructList[i].ReportField.ToString();
                    //            invRptReferenceDetailTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //            invRptReferenceDetailTemplate.SetParameterValue(i, "");
                    //    }
                    //    else
                    //    { invRptReferenceDetailTemplate.SetParameterValue(i, ""); }
                    //}
                    #endregion

                    #region Group By

                    selectedReportStructList = reportDataStructList.Where(c => c.IsSelectionField.Equals(true)).ToList();
                    selectedGroupStructList = groupByStructList.Where(c => c.IsResultGroupBy.Equals(true)).ToList();

                    gr = 0; gp = 1;
                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        // Set report group field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gr < invRptReferenceDetailTemplate.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    invRptReferenceDetailTemplate.DataDefinition.Groups[gr].ConditionField = invRptReferenceDetailTemplate.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp - 1 < invRptReferenceDetailTemplate.DataDefinition.Groups.Count)
                            {
                                if (invRptReferenceDetailTemplate.ParameterFields[gp - 1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        invRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        invRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < invRptReferenceDetailTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptReferenceDetailTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < invRptReferenceDetailTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptReferenceDetailTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptReferenceDetailTemplate;
                    break;
                #endregion
                #region SalesPerson
                case "FrmSalesPerson": // SalesPerson"
                    #region Set Values for report header Fields
                    strFieldName = string.Empty;
                    sr = 1; dc = 12;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {
                            if (item.ReportDataType.Equals(typeof(string)))
                            {
                                strFieldName = "st" + sr;
                                invRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                sr++;
                            }

                            //if (item.ReportDataType.Equals(typeof(decimal)))
                            //{
                            //    strFieldName = "st" + dc;
                            //    comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                            //    dc++;
                            //}
                        }
                    }

                    #endregion

                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    invRptReferenceDetailTemplate.SetDataSource(dtArrangedReportData);
                    //invRptReferenceDetailTemplate.SetDataSource(dtReportData);
                    invRptReferenceDetailTemplate.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    invRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    invRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    #region Set Header - old
                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    if (reportDataStructList[i].IsSelectionField.Equals(true))
                    //    {
                    //        strFieldName = "st" + (i + 1);
                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //        {
                    //            invRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            invRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }
                    //    }
                    //}
                    #endregion
                    #region Group By - old

                    //for (int i = 0; i <= 3; i++)
                    //{

                    //    if (groupByStructList.Count > i)
                    //    {
                    //        if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    //        {
                    //            string strGroup = groupByStructList[i].ReportField.ToString();
                    //            invRptReferenceDetailTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //            invRptReferenceDetailTemplate.SetParameterValue(i, "");
                    //    }
                    //    else
                    //    { invRptReferenceDetailTemplate.SetParameterValue(i, ""); }
                    //}
                    #endregion

                    #region Group By

                    selectedReportStructList = reportDataStructList.Where(c => c.IsSelectionField.Equals(true)).ToList();
                    selectedGroupStructList = groupByStructList.Where(c => c.IsResultGroupBy.Equals(true)).ToList();

                    gr = 0; gp = 1;
                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        // Set report group field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gr < invRptReferenceDetailTemplate.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    invRptReferenceDetailTemplate.DataDefinition.Groups[gr].ConditionField = invRptReferenceDetailTemplate.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp - 1 < invRptReferenceDetailTemplate.DataDefinition.Groups.Count)
                            {
                                if (invRptReferenceDetailTemplate.ParameterFields[gp - 1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        invRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        invRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < invRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptReferenceDetailTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < invRptReferenceDetailTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptReferenceDetailTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptReferenceDetailTemplate;
                    break;
                #endregion
                default:
                    return;
            }

            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
        }

        public void GenearatePOSSalesSummaryReport(DataTable dtReportData)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            InvRptColumn5Template invRptColumn5Template = new InvRptColumn5Template();
            InvRptReferenceDetailTemplate invRptReferenceDetailTemplate = new InvRptReferenceDetailTemplate();
            Cursor.Current = Cursors.WaitCursor;
            string strFieldName = string.Empty;

            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
        }

        public void GenearateTransactionReport(AutoGenerateInfo autoGenerateInfo, string documentNo, int printStatus,string supplier="", string supplierRemark="", string paymentMethod="", string poDocumentNo="",bool isTempDocument=false)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            Cursor.Current = Cursors.WaitCursor;
            DataTable reportData = new DataTable();
            int documentSataus = 1;
            if (printStatus.Equals(0))
            {
                documentSataus = 0;
            }
            else if (printStatus.Equals(1))
            {
                documentSataus = 1;
            }
            else if (printStatus.Equals(2))
            {
                documentSataus = 1;
            }

            string departmentText, categoryText, subCategoryText, subCategory2Text;
            departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
            categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
            subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
            subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;


            switch (autoGenerateInfo.FormName)
            {

                #region inventory transactions
                case "FrmLogisticProduct": // Purchase Order
                    SalesOrderService salesOrderService = new SalesOrderService();
                    reportData = salesOrderService.GetReorderTransactionDataTable(documentNo, documentSataus, AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSalesOrder").DocumentID);

                    string[] stringFieldReorder = { "Document", "PO No", "Invoice No", "Customer", "Deli.Address", "Unit", "Currency", "Company", "Doc.Date", "", 
                                            "Colour", "CreatedUser", "Pro.Code", "Pro.Name", "Size", "Gauge", "", "Qty","Item","Kind Of Material","Making Type",""};

                    InvRptTransactionTemplateSalesInvDis invRptTransactionTemplateInvD = new InvRptTransactionTemplateSalesInvDis();
                    //InvRptTransactionTemplateTmp invRptTransactionTemplatePo = new InvRptTransactionTemplateTmp();
                    invRptTransactionTemplateInvD.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptTransactionTemplateInvD.SummaryInfo.ReportTitle = "ADVICE OF DISPATCH";
                    invRptTransactionTemplateInvD.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptTransactionTemplateInvD.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptTransactionTemplateInvD.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptTransactionTemplateInvD.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptTransactionTemplateInvD.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldReorder.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptTransactionTemplateInvD.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldReorder[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptTransactionTemplateInvD;
                    break;
                #region Redispatch


                #endregion


                #region PurchaseOrder
                case "FrmPurchaseOrder": // Purchase Order
                //case "RptPendingPurchaseOrders":
                //    AutoGenerateInfo autoGenerateInfoPo = new AutoGenerateInfo();
                //    autoGenerateInfoPo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseOrder");
                    InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                    DataSet dsReportDataPo = invPurchaseOrderService.GetPurchaseOrderTransactionDataSet(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    #region Purchase Order
                    FrmReportViewer reportViewerPo = new FrmReportViewer();
                    string[] stringFieldPo = { "Document", "Date", "Remark", "Supplier", "P Req. No", "Validity Period", "Vat No", "Pmt. Expected Date", "Reference No", "Payment terms", 
                                            "Expected Date", "Location", "Pro.Code", "Pro.Name", "Unit", "P.Size", "Or.Qty", "F.Qty", "C.Price", "Dis.%", "Dis.Amt.", 
                                            "Net Amt.", "Gross Amt.", "Dis.%", "Dis.Amt.", "VAT Amt.", "Oth. Charges", "Net Amt.", "", "NBT Amt.", "Old Supp. Code" };

                    InvRptPurchaseOrderTemplate invRptPurchaseOrderTemplate = new InvRptPurchaseOrderTemplate();
                    invRptPurchaseOrderTemplate.SetDataSource(dsReportDataPo.Tables[0]);
                    // Assign formula and summery field values
                    invRptPurchaseOrderTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptPurchaseOrderTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptPurchaseOrderTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptPurchaseOrderTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptPurchaseOrderTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptPurchaseOrderTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptPurchaseOrderTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldPo.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptPurchaseOrderTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldPo[i].Trim() + "'";
                    }

                    reportViewerPo.crRptViewer.ReportSource = invRptPurchaseOrderTemplate;
                    reportViewerPo.WindowState = FormWindowState.Maximized;
                    reportViewerPo.Show();
                    #endregion

                    #region Purchase Order - Product Details

                    string[] stringFieldPoProducts = { "Document", "Date", "Supplier", "Location", "Pro.Code", "", "", categoryText, subCategoryText, subCategory2Text, 
                                            "id", "", "", "", "", "", "", "", "", "", "", "", "", "", ""};

                    InvRptPurchaseOrderProductTemplate invRptPurchaseOrderProductTemplate = new InvRptPurchaseOrderProductTemplate();
                    invRptPurchaseOrderProductTemplate.SetDataSource(dsReportDataPo.Tables[1]);
                    // Assign formula and summery field values
                    invRptPurchaseOrderProductTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptPurchaseOrderProductTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptPurchaseOrderProductTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptPurchaseOrderProductTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptPurchaseOrderProductTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptPurchaseOrderProductTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptPurchaseOrderProductTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < 11; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptPurchaseOrderProductTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldPoProducts[i].Trim() + "'";
                    }

                    if (dsReportDataPo.Tables[2].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsReportDataPo.Tables[2].Rows.Count; i++)
                        {
                            if (i < 14) // Because report has ony 14 fields for product extended properties
                            {
                                invRptPurchaseOrderProductTemplate.DataDefinition.FormulaFields["st" + (i + 12).ToString()].Text = "'" + dsReportDataPo.Tables[2].Rows[i][0].ToString().Trim() + "'";    
                            }
                        }
                    }

                    reportViewer.crRptViewer.ReportSource = invRptPurchaseOrderProductTemplate;


                    #endregion
                    break;

                #endregion

                #region RptPendingPurchaseOrders
                case "RptPendingPurchaseOrders":
                    AutoGenerateInfo autoGenerateInfoPo = new AutoGenerateInfo();
                    autoGenerateInfoPo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseOrder");
                    InvPurchaseOrderService invPurchaseOrderServicePending = new InvPurchaseOrderService();
                    DataSet dsReportDataPendingPo = invPurchaseOrderServicePending.GetPendingPurchaseOrderTransactionDataSet(documentNo, documentSataus, autoGenerateInfoPo.DocumentID);

                    #region Purchase Order
                    FrmReportViewer reportViewerPendingPo = new FrmReportViewer();
                    string[] stringFieldPendingPo = { "Document", "Date", "Remark", "Supplier", "P Req. No", "Validity Period", "Vat No", "Pmt. Expected Date", "Reference No", "Payment terms", 
                                            "Expected Date", "Location", "Pro.Code", "Pro.Name", "Unit", "P.Size", "Bal.Qty", "F.Qty", "C.Price", "Dis.%", "Dis.Amt.", 
                                            "Net Amt.", "Gross Amt.", "Dis.%", "Dis.Amt.", "VAT Amt.", "Oth. Charges", "Net Amt.", "", "NBT Amt.", "Old Supp. Code" };

                    InvRptPurchaseOrderTemplate invRptPendingPurchaseOrderTemplate = new InvRptPurchaseOrderTemplate();
                    invRptPendingPurchaseOrderTemplate.SetDataSource(dsReportDataPendingPo.Tables[0]);
                    // Assign formula and summery field values
                    invRptPendingPurchaseOrderTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptPendingPurchaseOrderTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptPendingPurchaseOrderTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptPendingPurchaseOrderTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptPendingPurchaseOrderTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptPendingPurchaseOrderTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptPendingPurchaseOrderTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldPendingPo.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptPendingPurchaseOrderTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldPendingPo[i].Trim() + "'";
                    }

                    reportViewerPendingPo.crRptViewer.ReportSource = invRptPendingPurchaseOrderTemplate;
                    reportViewerPendingPo.WindowState = FormWindowState.Maximized;
                    reportViewerPendingPo.Show();

                    #endregion

                    #region Purchase Order - Product Details

                    string[] stringFieldPendingPoProducts = { "Document", "Date", "Supplier", "Location", "Pro.Code", "", "", categoryText, subCategoryText, subCategory2Text, 
                                            "id", "", "", "", "", "", "", "", "", "", "", "", "", "", ""};

                    InvRptPurchaseOrderProductTemplate invRptPendingPurchaseOrderProductTemplate = new InvRptPurchaseOrderProductTemplate();
                    invRptPendingPurchaseOrderProductTemplate.SetDataSource(dsReportDataPendingPo.Tables[1]);
                    // Assign formula and summery field values
                    invRptPendingPurchaseOrderProductTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptPendingPurchaseOrderProductTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptPendingPurchaseOrderProductTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptPendingPurchaseOrderProductTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptPendingPurchaseOrderProductTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptPendingPurchaseOrderProductTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptPendingPurchaseOrderProductTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < 11; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptPendingPurchaseOrderProductTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldPendingPoProducts[i].Trim() + "'";
                    }

                    if (dsReportDataPendingPo.Tables[2].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsReportDataPendingPo.Tables[2].Rows.Count; i++)
                        {
                            if (i < 14) // Because report has ony 14 fields for product extended properties
                            {
                                invRptPendingPurchaseOrderProductTemplate.DataDefinition.FormulaFields["st" + (i + 12).ToString()].Text = "'" + dsReportDataPendingPo.Tables[2].Rows[i][0].ToString().Trim() + "'";
                            }
                        }
                    }

                    reportViewer.crRptViewer.ReportSource = invRptPendingPurchaseOrderProductTemplate;


                    #endregion

                    break;
                #endregion

                #region Goods Received Note
                case "FrmGoodsReceivedNote":
                    InvPurchaseService invPurchaseService = new InvPurchaseService();
                    DataSet dsReportData = new DataSet();

                    if (isTempDocument)
                    {
                        dsReportData = invPurchaseService.GetPurchaseTransactionDataTableGrnTemp(documentNo, documentSataus, autoGenerateInfo.DocumentID, supplier, supplierRemark, paymentMethod, poDocumentNo);
                    }
                    else
                    {
                        dsReportData = invPurchaseService.GetPurchaseTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    }
                    string[] stringFieldGrn = { "Document", "Date", "Remark", "Supplier", "P.O. Number", "", "", "", "Reference No", "Payment terms", "Supp. Invoice", 
                                                  "Location", "Pro.Code", "Pro.Name", "Unit", "Other%", "Tot.Weight", "WAvg", "C.Price", 
                                                  "Net Weight", "", "Net Amt.", "Gross Amt.", "Tot.Ded.%", "Ded.Amt.", "NBT Amt.", "VAT Amt", "Oth. Charges", "Net Amt.", "CreatedUser", "Old Supp. Code" };

                    InvRptPurchaseTemplate1 invRptPurchaseTemplateGrn = new InvRptPurchaseTemplate1();
                    invRptPurchaseTemplateGrn.SetDataSource(dsReportData.Tables[0]);
                    // Assign formula and summery field values
                    invRptPurchaseTemplateGrn.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptPurchaseTemplateGrn.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptPurchaseTemplateGrn.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptPurchaseTemplateGrn.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptPurchaseTemplateGrn.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptPurchaseTemplateGrn.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptPurchaseTemplateGrn.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    //  Set field text
                    for (int i = 0; i < stringFieldGrn.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptPurchaseTemplateGrn.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldGrn[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptPurchaseTemplateGrn;

                #region PO Balance

                    // Get Balances report of reference PO
                    if (dsReportData.Tables.Count > 1)
                    {
                        if (dsReportData.Tables[1].Rows.Count > 0)
                        {
                            FrmReportViewer reportViewerPoBal = new FrmReportViewer();
                            //string[] stringFieldPoBal = { "Document", "Date", "Supplier", "Remark", "", "Validity Period", "", "", "Reference No", "Payment terms", 
                            //                "Expected Date", "Location", "Pro.Code", "Pro.Name", "Unit", "Or.Qty", "B. Qty", "F.Qty", "C.Price", "Dis.%", "Dis.Amt.", 
                            //                "Net Amt.", "Gross Amt.", "Dis.%", "Dis.Amt.", "Tax Amt.", "Oth. Charges", "Net Amt.", "CreatedUser", "Old Supp. Code" };

                            string[] stringFieldPoBal = { "Document", "Date", "Supplier", "Remark", "G.R.N.", "Validity Period", "Vat No", "Pmt. Expected Date", "Reference No", "Payment terms", 
                                            "Expected Date", "Location", "Pro.Code", "Pro.Name", "Unit", "Or.Qty", "Bal. Qty", "F.Qty", "C.Price", "Dis.%", "Dis.Amt.", 
                                            "Net Amt.", "Gross Amt.", "Dis.%", "Dis.Amt.", "Vat Amt.", "Oth. Charges", "Net Amt.", "", "NBT Amt.", "Old Supp. Code" };

                            //InvRptTransactionTemplate invRptTransactionTemplatePoBal = new InvRptTransactionTemplate();
                            InvRptPurchaseOrderTemplate invRptTransactionTemplatePoBal = new InvRptPurchaseOrderTemplate();
                            invRptTransactionTemplatePoBal.SetDataSource(dsReportData.Tables[1]);
                            // Assign formula and summery field values
                            invRptTransactionTemplatePoBal.SummaryInfo.ReportTitle = "Purchase Order Balances";
                            invRptTransactionTemplatePoBal.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                            invRptTransactionTemplatePoBal.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                            invRptTransactionTemplatePoBal.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                            invRptTransactionTemplatePoBal.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                            invRptTransactionTemplatePoBal.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                            invRptTransactionTemplatePoBal.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                            //  Set field text
                            for (int i = 0; i < stringFieldPoBal.Length; i++)
                            {
                                string st = "st" + (i + 1);
                                invRptTransactionTemplatePoBal.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldPoBal[i].Trim() + "'";
                            }

                            //Common.SetMenu(reportViewerPoBal, UI.Windows);
                            reportViewerPoBal.crRptViewer.ReportSource = invRptTransactionTemplatePoBal;
                            reportViewerPoBal.WindowState = FormWindowState.Maximized;
                            reportViewerPoBal.Show();
                        }
                    }

                #endregion
                    break;
                #endregion

                #region Purchase Return Note
                case "FrmPurchaseReturnNote":
                    InvPurchaseService invPurchaseServiceRe = new InvPurchaseService();
                    reportData = invPurchaseServiceRe.GetPurchaseReturnTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldPrn = { "Document", "Date", "Supplier", "Remark", "Return Type", "", "", "", "Reference No", "", "", "Location", "Pro.Code", 
                                                  "Pro.Name", "Unit", "", "Qty", "F.Qty", "C.Price", "Dis.%", "Dis.Amt.", "Net Amt.", "Gross Amt.", "Dis.%", 
                                                  "Dis.Amt.", "Tax Amt.", "", "Net Amt.", "", "", "Old Supp. Code" };

                    InvRptPRNTransactionTemplate invRptTransactionTemplatePrn = new InvRptPRNTransactionTemplate();
                    invRptTransactionTemplatePrn.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptTransactionTemplatePrn.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptTransactionTemplatePrn.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptTransactionTemplatePrn.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptTransactionTemplatePrn.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptTransactionTemplatePrn.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptTransactionTemplatePrn.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptTransactionTemplatePrn.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    //  Set field text
                    for (int i = 0; i < stringFieldPrn.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptTransactionTemplatePrn.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldPrn[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptTransactionTemplatePrn;
                    break;
                #endregion

                #region Transfer Of Goods Note
                case "FrmTransferOfGoodsNote":
                    InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                    reportData = invTransferOfGoodsService.GetTransferofGoodsTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);


                    string[] stringFieldTog = { "Document", "Date", "", "Remark", "", "", "", "", "Reference No", "", "From Location", "To Location", 
                                                  "Pro.Code", "Pro.Name", "Unit", "Brand", "Size", "Or.Qty", "Rec.Qty", "", "S.Price", "Net Amt.", 
                                                  "", "", "", "", "", "Net Amt." };
                    
                    InvRptTOGTransactionTemplate invRptTransactionTemplateTog = new InvRptTOGTransactionTemplate();
                    invRptTransactionTemplateTog.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptTransactionTemplateTog.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptTransactionTemplateTog.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptTransactionTemplateTog.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptTransactionTemplateTog.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptTransactionTemplateTog.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptTransactionTemplateTog.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptTransactionTemplateTog.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";
                    invRptTransactionTemplateTog.DataDefinition.FormulaFields["isOtherSum"].Text = "'" + 1 + "'";

                    //  Set field text
                    for (int i = 0; i < stringFieldTog.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptTransactionTemplateTog.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldTog[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptTransactionTemplateTog;
                    break;
                #endregion

                #region Stock Adjustment
                case "FrmStockAdjustment":
                    InvStockAdjustmentService invStockAdjustmentService = new InvStockAdjustmentService();
                    reportData = invStockAdjustmentService.GetStockAdjustmentTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldSto = { "Document", "Date", "Narration", "Remark", "", "", "", "", "Reference No", "Adjustment Mode", 
                                            "", "Location", "Pro.Code", "Pro.Name", "Unit", "", "Qty", "S.Price", "C.Price", "", "S.Value", 
                                            "C.Value", "Tot. S.Value", "Tot. C.Value", "", "", "", "" };

                    InvRptSTATransactionTemplate invRptTransactionTemplateSta = new InvRptSTATransactionTemplate();
                    invRptTransactionTemplateSta.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptTransactionTemplateSta.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptTransactionTemplateSta.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptTransactionTemplateSta.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptTransactionTemplateSta.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptTransactionTemplateSta.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptTransactionTemplateSta.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptTransactionTemplateSta.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";
                    

                    for (int i = 0; i < stringFieldSto.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptTransactionTemplateSta.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldSto[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptTransactionTemplateSta;
                    break;
                #endregion

                #region Sales Order
                case "FrmSalesOrder":
                    salesOrderService = new SalesOrderService();
                    reportData = salesOrderService.GetSalesOrderTransactionOldDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldSo = { "Document", "Reference No", "Customer", "Salesman", "Deli.Address", "Unit", "", "Company", "Doc.Date", "Deliver.Date", 
                                            "Colour", "CreatedUser", "Pro.Code", "Pro.Name", "Size", "Gauge", "", "Qty","Item","Kind Of Material","Making Type","Special Option"};

                    InvRptTransactionTemplateSalesOrder invRptTransactionTemplateSo = new InvRptTransactionTemplateSalesOrder();
                    //InvRptTransactionTemplateTmp invRptTransactionTemplatePo = new InvRptTransactionTemplateTmp();
                    invRptTransactionTemplateSo.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptTransactionTemplateSo.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptTransactionTemplateSo.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptTransactionTemplateSo.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptTransactionTemplateSo.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptTransactionTemplateSo.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptTransactionTemplateSo.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptTransactionTemplateSo.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldSo.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptTransactionTemplateSo.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldSo[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptTransactionTemplateSo;
                    break;
                #endregion

                #region Sample Out
                case "FrmSampleOut":
                    SampleOutService sampleOutService = new SampleOutService();
                    reportData = sampleOutService.GetSampleOutTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID, 1);

                    string[] stringFieldSao = { "Document", "Date", "Issued To", "Dilivery Person", "Remark", "", "", "Reference No", "", "Sample Out Type", 
                                            "Location", "", "Pro.Code", "Pro.Name", "Unit", "", "Or.Qty", "", "C.Price", "", "", 
                                            "Net Amt.", "Net Amt.", "", "", "", "", "" };

                    InvRptTransactionTemplate invRptTransactionTemplateSao = new InvRptTransactionTemplate();
                    invRptTransactionTemplateSao.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptTransactionTemplateSao.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptTransactionTemplateSao.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptTransactionTemplateSao.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptTransactionTemplateSao.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptTransactionTemplateSao.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptTransactionTemplateSao.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptTransactionTemplateSao.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldSao.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptTransactionTemplateSao.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldSao[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptTransactionTemplateSao;
                    break;
                #endregion

                #region Sample In
                case "FrmSampleIn":
                    SampleInService sampleInService = new SampleInService();
                    reportData = sampleInService.GetSampleInTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID, 1);

                    string[] stringFieldSai = { "Document", "Date", "Issued To", "Dilivery Person", "Remark", "", "", "Reference No", "", "Sample Out Type", 
                                            "Location", "", "Pro.Code", "Pro.Name", "Unit", "", "Or.Qty", "", "C.Price", "", "", 
                                            "Net Amt.", "Net Amt.", "", "", "", "", "" };

                    InvRptTransactionTemplate invRptTransactionTemplateSai = new InvRptTransactionTemplate();
                    invRptTransactionTemplateSai.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptTransactionTemplateSai.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptTransactionTemplateSai.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptTransactionTemplateSai.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptTransactionTemplateSai.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptTransactionTemplateSai.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptTransactionTemplateSai.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptTransactionTemplateSai.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldSai.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptTransactionTemplateSai.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldSai[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptTransactionTemplateSai;
                    break;
                #endregion

                #region Opening Stock
                case "FrmOpeningStock":
                    OpeningStockService openingStockService = new OpeningStockService();
                    reportData = openingStockService.GetOpeningStockTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID,1);

                    string[] stringFieldOpst = { "Document", "Date", "Narration", "Remark", "", "", "", "", "Reference No", "Opening Stock Type", 
                                            "", "Location", "Pro.Code", "Pro.Name", "Unit", "Batch No", "Expiry Date", "Qty", "S.Price", "C.Price", "S.Value", 
                                            "C.Value", "Tot. S.Value", "Tot. C.Value", "Tot.Qty", "", "", "" };

                    InvRptTransactionTemplateLandscape1 invRptTransactionTemplateOpst = new InvRptTransactionTemplateLandscape1();
                    ////InvRptOpeningStockSmallPaper invRptTransactionTemplateOpst = new InvRptOpeningStockSmallPaper();
                    invRptTransactionTemplateOpst.SetDataSource(reportData);

                    // Assign formula and summery field values
                    invRptTransactionTemplateOpst.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptTransactionTemplateOpst.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptTransactionTemplateOpst.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptTransactionTemplateOpst.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptTransactionTemplateOpst.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptTransactionTemplateOpst.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptTransactionTemplateOpst.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldOpst.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptTransactionTemplateOpst.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldOpst[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptTransactionTemplateOpst;
                    break;
                #endregion

                #region Quotation
                case "FrmQuotation":
                    InvQuotationService invQuotationService = new InvQuotationService();
                    reportData = invQuotationService.GetQuotationTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldQtn = { "Document", "Date", "Customer", "Remark", "", "", "", "", "Reference No", "", "", "Location", "Pro.Code", 
                                                  "Pro.Name", "Unit", "", "Qty", "", "C.Price", "Dis.%", "Dis.Amt.", "Net Amt.", "Gross Amt.", "Dis.%", 
                                                  "Dis.Amt.", "Tax Amt.", "Oth. Charges", "Net Amt." };

                    InvRptTransactionTemplate invRptTransactionTemplateQtn = new InvRptTransactionTemplate();
                    invRptTransactionTemplateQtn.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptTransactionTemplateQtn.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptTransactionTemplateQtn.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptTransactionTemplateQtn.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptTransactionTemplateQtn.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptTransactionTemplateQtn.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptTransactionTemplateQtn.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptTransactionTemplateQtn.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";
                    invRptTransactionTemplateQtn.DataDefinition.FormulaFields["isOtherSum"].Text = "'" + 0 + "'";

                    //  Set field text
                    for (int i = 0; i < stringFieldQtn.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptTransactionTemplateQtn.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldQtn[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptTransactionTemplateQtn;
                    break;
                #endregion

                #region Sales
                case "FrmInvoice":

                    FrmReportViewer reportViewerI = new FrmReportViewer();

                    salesOrderService = new SalesOrderService();
                    DataTable dtinv = new DataTable();
                    dtinv = salesOrderService.GetSalesOrderTransactionInvoiceDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);
                     InvRptTransactionTemplateSalesInv invRptTransactionTemplateInv = new InvRptTransactionTemplateSalesInv();

                    string[] stringFieldInv = { "Document", "Order NO", "Reference No","Customer","SVat","Vat","Currency","","Doc.Date", "", "Exchange Rate", 
                                            "CreatedUser", "Pro.Code", "Pro.Name", "Unit Price", "Qty","", "Amount","Net Value","Vat","Suspended Vat Lkr :","Equivalent Rupee Amount : "};

                    //InvRptTransactionTemplateTmp invRptTransactionTemplatePo = new InvRptTransactionTemplateTmp();
                    invRptTransactionTemplateInv.SetDataSource(dtinv);
                    // Assign formula and summery field values
                    invRptTransactionTemplateInv.SummaryInfo.ReportTitle = "SUSPENDED TAX INVOICE";
                    invRptTransactionTemplateInv.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptTransactionTemplateInv.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptTransactionTemplateInv.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptTransactionTemplateInv.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptTransactionTemplateInv.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptTransactionTemplateInv.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";
                    //invRptTransactionTemplateInv.DataDefinition.FormulaFields["rupee"].Text = "" + rupee + "";
                    for (int i = 0; i < stringFieldInv.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptTransactionTemplateInv.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldInv[i].Trim() + "'";
                    }
                    reportViewerI.crRptViewer.ReportSource = invRptTransactionTemplateInv;
                    reportViewerI.WindowState = FormWindowState.Normal;
                    //reportViewerI.TopMost = true;
                    //reportViewerI.BringToFront();
                    //reportViewerI.TopMost = false;
                    reportViewerI.Show();

                    salesOrderService = new SalesOrderService();
                    reportData = salesOrderService.GetSalesOrderTransactionDataTable(documentNo, documentSataus, AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSalesOrder").DocumentID);

                    string[] stringFieldDisp = { "Document", "PO No", "Invoice No", "Customer", "Deli.Address", "Unit", "Currency", "Company", "Doc.Date", "", 
                                            "Colour", "CreatedUser", "Pro.Code", "Pro.Name", "Size", "Gauge", "", "Qty","Item","Kind Of Material","Making Type",""};

                    invRptTransactionTemplateInvD = new InvRptTransactionTemplateSalesInvDis();
                    //InvRptTransactionTemplateTmp invRptTransactionTemplatePo = new InvRptTransactionTemplateTmp();
                    invRptTransactionTemplateInvD.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptTransactionTemplateInvD.SummaryInfo.ReportTitle = "ADVICE OF DISPATCH";
                    invRptTransactionTemplateInvD.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptTransactionTemplateInvD.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptTransactionTemplateInvD.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptTransactionTemplateInvD.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptTransactionTemplateInvD.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldDisp.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptTransactionTemplateInvD.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldDisp[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptTransactionTemplateInvD;
                    //reportViewerSO.WindowState = FormWindowState;
                    //reportViewerSO.Show();


                    
       
                                        
                    break;

                                
        
                #endregion

                #region Sales Return
                case "FrmSalesReturnNote":
                    InvSalesServices invSalesReturnServices = new InvSalesServices();
                    reportData = invSalesReturnServices.GetSalesReturnTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldSaleRet = { "Document", "Date", "Customer", "Remark", "", "", "", "", "Reference No", "", "", "Location", "Pro.Code", 
                                                  "Pro.Name", "Unit", "", "Qty", "", "C.Price", "Dis.%", "Dis.Amt.", "Net Amt.", "Gross Amt.", "Dis.%", 
                                                  "Dis.Amt.", "Tax Amt.", "Oth. Charges", "Net Amt." };

                    InvRptTransactionTemplate invRptTransactionTemplateSaleRet = new InvRptTransactionTemplate();
                    invRptTransactionTemplateSaleRet.SetDataSource(reportData);
                    // Assign formula and summery field values
                    invRptTransactionTemplateSaleRet.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptTransactionTemplateSaleRet.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptTransactionTemplateSaleRet.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptTransactionTemplateSaleRet.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptTransactionTemplateSaleRet.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptTransactionTemplateSaleRet.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptTransactionTemplateSaleRet.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";
                    invRptTransactionTemplateSaleRet.DataDefinition.FormulaFields["isOtherSum"].Text = "'" + 0 + "'";

                    //  Set field text
                    for (int i = 0; i < stringFieldSaleRet.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptTransactionTemplateSaleRet.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldSaleRet[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = invRptTransactionTemplateSaleRet;
                    break;
                #endregion

                #region Price Change
                case "FrmProductPriceChange":

                    InvProductPriceChangeService invProductPriceChangeService = new InvProductPriceChangeService();
                    dsReportDataPo = new DataSet();
                    dsReportDataPo = invProductPriceChangeService.GetPriceChangeTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);


                    string[] stringFieldPoProduct = {"Doc_No", "Advice Date", "Effect Date", "RefNo","Pro.Code","Pro.Name", "Office IT","Batch No",categoryText, subCategoryText, subCategory2Text, 
                                            "id", "", "", "", "", "", "", "", "", "Old Price", "New Price", "St Stock", "Price Change Loca", "", ""};

                    InvRptPriceChangeProductTemplate invRptPriceChangeProductTemplate = new InvRptPriceChangeProductTemplate();
                    invRptPriceChangeProductTemplate.SetDataSource(dsReportDataPo.Tables[1]);
                    // Assign formula and summery field values
                    invRptPriceChangeProductTemplate.SummaryInfo.ReportTitle = "Price Change Details";
                    invRptPriceChangeProductTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptPriceChangeProductTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptPriceChangeProductTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptPriceChangeProductTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptPriceChangeProductTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptPriceChangeProductTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < 12; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptPriceChangeProductTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldPoProduct[i].Trim() + "'";
                    }

                    if (dsReportDataPo.Tables[2].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsReportDataPo.Tables[2].Rows.Count; i++)
                        {
                            if (i < 8) // Because report has ony 14 fields for product extended properties
                            {
                                invRptPriceChangeProductTemplate.DataDefinition.FormulaFields["st" + (i + 13).ToString()].Text = "'" + dsReportDataPo.Tables[2].Rows[i][0].ToString().Trim() + "'";
                            }
                        }
                    }

                    for (int i = 20; i < 23; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptPriceChangeProductTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldPoProduct[i].Trim() + "'";
                    }

                    reportViewer.crRptViewer.ReportSource = invRptPriceChangeProductTemplate;
                    break;

                case "FrmProductPriceChangeDamage":

                    InvProductPriceChangeDamageService invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
                    dsReportDataPo = new DataSet();
                    dsReportDataPo = invProductPriceChangeDamageService.GetPriceChangeTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);


                    string[] stringFieldPoProductX = {"Doc_No", "Advice Date", "Effect Date", "RefNo","Pro.Code","Pro.Name", "Office IT","New P.Code","New P.Name", "TOG NO", subCategory2Text, 
                                            "id", "", "", "Old Price", "New Price", "Qty", "", "", "", "","","", "Price Change Loca", "", ""};

                    InvRptPriceChangeProductDamageTemplate invRptPriceChangeProductDamageTemplate = new InvRptPriceChangeProductDamageTemplate();
                    invRptPriceChangeProductDamageTemplate.SetDataSource(dsReportDataPo.Tables[1]);
                    // Assign formula and summery field values
                    invRptPriceChangeProductDamageTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptPriceChangeProductDamageTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptPriceChangeProductDamageTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptPriceChangeProductDamageTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptPriceChangeProductDamageTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptPriceChangeProductDamageTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptPriceChangeProductDamageTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < 18; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptPriceChangeProductDamageTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldPoProductX[i].Trim() + "'";
                    }

                    if (dsReportDataPo.Tables[2].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsReportDataPo.Tables[2].Rows.Count; i++)
                        {
                            if (i < 8) // Because report has ony 14 fields for product extended properties
                            {
                                invRptPriceChangeProductDamageTemplate.DataDefinition.FormulaFields["st" + (i + 13).ToString()].Text = "'" + dsReportDataPo.Tables[2].Rows[i][0].ToString().Trim() + "'";
                            }
                        }
                    }

                    for (int i = 20; i < 23; i++)
                    {
                        string st = "st" + (i + 1);
                        invRptPriceChangeProductDamageTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldPoProductX[i].Trim() + "'";
                    }

                    reportViewer.crRptViewer.ReportSource = invRptPriceChangeProductDamageTemplate;
                    break;


                #endregion

                #endregion

                default:
                    return;
                    //break;
            }
            
            reportViewer.WindowState = FormWindowState.Normal;
            //reportViewer.TopMost = true;
            //reportViewer.BringToFront();
            //reportViewer.TopMost = false;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
        }

        public void GenearateTransactionSummeryReport(AutoGenerateInfo autoGenerateInfo, DataTable dtReportData, DataTable dtReportConditions, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> groupByStructList, bool viewGroupDetails)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            Cursor.Current = Cursors.WaitCursor;
            DataTable dtArrangedReportData = new DataTable();

            int yx = 0;
            switch (autoGenerateInfo.FormName)
            {
                #region inventory transactions

                #region Purchase Order
                case "FrmPurchaseOrder":
                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        InvRptGroupedDetailsTemplate invRptGroupedDetails = new InvRptGroupedDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewGroupedReport(invRptGroupedDetails, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    else
                    {
                        InvRptDetailsTemplate invRptDetailsTemplate = new InvRptDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewUnGroupedReport(invRptDetailsTemplate, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    break;
                #endregion
                #region GoodsReceivedNote
                case "FrmGoodsReceivedNote":

                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        InvRptGroupedDetailsTemplate invRptGroupedDetails = new InvRptGroupedDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewGroupedReport(invRptGroupedDetails, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    else
                    {
                        InvRptDetailsTemplate invRptDetailsTemplate = new InvRptDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewUnGroupedReport(invRptDetailsTemplate, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    break;
                #endregion
                #region PurchaseReturnNote
                case "FrmPurchaseReturnNote":
                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        InvRptGroupedDetailsTemplate invRptGroupedDetails = new InvRptGroupedDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewGroupedReport(invRptGroupedDetails, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    else
                    {
                        InvRptDetailsTemplate invRptDetailsTemplate = new InvRptDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewUnGroupedReport(invRptDetailsTemplate, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    break;
                #endregion
                #region TransferOfGoodsNote
                case "FrmTransferOfGoodsNote":
                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        InvRptGroupedDetailsTemplate invRptGroupedDetails = new InvRptGroupedDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewGroupedReport(invRptGroupedDetails, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    else
                    {
                        InvRptDetailsTemplate invRptDetailsTemplate = new InvRptDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewUnGroupedReport(invRptDetailsTemplate, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    //InvRptTOGTransactionSummeryTemplateLandscape invRptTransactionSummeryTemplateTOG = new InvRptTOGTransactionSummeryTemplateLandscape();

                    //#region Set Values for report header fields
                    //strFieldName = string.Empty;
                    //int srpo = 1, dcpo = 8;

                    //foreach (var item in reportDataStructList)
                    //{
                    //    if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                    //    {
                           
                    //        if (item.ReportDataType.Equals(typeof(string)))
                    //        {
                    //            strFieldName = "st" + srpo;
                    //            invRptTransactionSummeryTemplateTOG.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                    //            srpo++;
                    //        }

                    //        if (item.ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + dcpo;
                    //            invRptTransactionSummeryTemplateTOG.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                    //            dcpo++;
                    //        }
                    //    }
                    //}

                    //#endregion
                    
                    //// Re arrange data table header columns for report
                    //dtArrangedReportData = dtReportData.Copy();
                    //dtReportData.Dispose();
                    //dtReportData = null;
                    //dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    //invRptTransactionSummeryTemplateTOG.SetDataSource(dtArrangedReportData);
                    //invRptTransactionSummeryTemplateTOG.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    //invRptTransactionSummeryTemplateTOG.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    //invRptTransactionSummeryTemplateTOG.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    //invRptTransactionSummeryTemplateTOG.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    //invRptTransactionSummeryTemplateTOG.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    //invRptTransactionSummeryTemplateTOG.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    //invRptTransactionSummeryTemplateTOG.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //invRptTransactionSummeryTemplateTOG.SetParameterValue("prmViewGroupDetails", viewGroupDetails);


                    //#region Group By
                    //if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    //{
                    //    // Set report group field values
                    //    for (int i = 0; i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count(); i++)
                    //    {
                    //        if (i < invRptTransactionSummeryTemplateTOG.DataDefinition.Groups.Count)
                    //        {
                    //            invRptTransactionSummeryTemplateTOG.DataDefinition.Groups[i].ConditionField = invRptTransactionSummeryTemplateTOG.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())]; //[string.Concat("FieldString", groupStrFieldCount)];
                    //        }
                    //    }

                    //    // Set parameter field values
                    //    for (int i = 0; i < invRptTransactionSummeryTemplateTOG.DataDefinition.Groups.Count; i++)
                    //    {
                    //        if (invRptTransactionSummeryTemplateTOG.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                    //        {
                    //            if (i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count())
                    //            {
                    //                invRptTransactionSummeryTemplateTOG.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), true);
                    //            }
                    //            else
                    //            {
                    //                invRptTransactionSummeryTemplateTOG.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    // Set parameter field values
                    //    for (int i = 0; i < invRptTransactionSummeryTemplateTOG.DataDefinition.Groups.Count; i++)
                    //    {
                    //        if (invRptTransactionSummeryTemplateTOG.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                    //        {
                    //            invRptTransactionSummeryTemplateTOG.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                    //        }
                    //    }
                    //}
                    //#endregion
                    //reportViewer.crRptViewer.ReportSource = invRptTransactionSummeryTemplateTOG;
                    break;

                #endregion
                #region Invoice
                case "FrmInvoice":
                    InvRptTransactionSummeryTemplate invRptTransactionSummeryTemplateInv = new InvRptTransactionSummeryTemplate();

                    invRptTransactionSummeryTemplateInv.SetDataSource(dtReportData);
                    invRptTransactionSummeryTemplateInv.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    invRptTransactionSummeryTemplateInv.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptTransactionSummeryTemplateInv.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptTransactionSummeryTemplateInv.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptTransactionSummeryTemplateInv.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptTransactionSummeryTemplateInv.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    strFieldName = string.Empty;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            invRptTransactionSummeryTemplateInv.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (12 - i);
                            invRptTransactionSummeryTemplateInv.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }
                    }
                    reportViewer.crRptViewer.ReportSource = invRptTransactionSummeryTemplateInv;
                    break;
                #endregion
                #region PriceChange
                case "RptPriceChange":
                    InvRptTransactionPriceChange invRptTransactionPriceChange = new InvRptTransactionPriceChange();

                    invRptTransactionPriceChange.SetDataSource(dtReportData);
                    invRptTransactionPriceChange.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    invRptTransactionPriceChange.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptTransactionPriceChange.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptTransactionPriceChange.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptTransactionPriceChange.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptTransactionPriceChange.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";
                    int xx = 0;
                    strFieldName = string.Empty;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            invRptTransactionPriceChange.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (14 - xx);
                            invRptTransactionPriceChange.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                            xx++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 3; i++)
                    {
                        invRptTransactionPriceChange.SetParameterValue(i, "");
                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                string strGroup = groupByStructList[i].ReportField.ToString();
                                invRptTransactionPriceChange.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {

                                switch (groupByStructList[i].ValueDataType.ToString())
                                {
                                    case "System.String":
                                        invRptTransactionPriceChange.SetParameterValue(i, "");
                                        break;
                                    case "System.DateTime":
                                        invRptTransactionPriceChange.SetParameterValue(i, "02-02-2013");
                                        break;
                                    case "System.bool":
                                        invRptTransactionPriceChange.SetParameterValue(i, true);
                                        break;
                                    case "System.Int32":
                                        invRptTransactionPriceChange.SetParameterValue(i, 0);
                                        break;
                                    case "System.Int64":
                                        invRptTransactionPriceChange.SetParameterValue(i, 0);
                                        break;
                                    default:

                                        break;
                                }
                            }
                        }


                    }
                    #endregion

                    reportViewer.crRptViewer.ReportSource = invRptTransactionPriceChange;
                    break;
                #endregion
                #endregion

                #region purchasing reports

                #region Purchase Register
                case "RptPurchaseRegister":

                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        InvRptGroupedDetailsTemplate invRptGroupedDetails = new InvRptGroupedDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewGroupedReport(invRptGroupedDetails, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    else
                    {
                        InvRptDetailsTemplate invRptDetailsTemplate = new InvRptDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewUnGroupedReport(invRptDetailsTemplate, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    break;
                    #endregion
                #region Pending Purchase Orders
                case "RptPendingPurchaseOrders":
                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        InvRptGroupedDetailsTemplate invRptGroupedDetails = new InvRptGroupedDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewGroupedReport(invRptGroupedDetails, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    else
                    {
                        InvRptDetailsTemplate invRptDetailsTemplate = new InvRptDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewUnGroupedReport(invRptDetailsTemplate, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    break;

                #endregion
                #region Purchase Return Register
                case "RptPurchaseReturnRegister":
                    InvPurchaseTransactionTemplate invRptPurchaseReturnRegistryTemplate = new InvPurchaseTransactionTemplate();

                    invRptPurchaseReturnRegistryTemplate.SetDataSource(dtReportData);
                    invRptPurchaseReturnRegistryTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    invRptPurchaseReturnRegistryTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptPurchaseReturnRegistryTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptPurchaseReturnRegistryTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptPurchaseReturnRegistryTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptPurchaseReturnRegistryTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    strFieldName = string.Empty;
                    yx = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            invRptPurchaseReturnRegistryTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (8 + yx);
                            invRptPurchaseReturnRegistryTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                            yx++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 6; i++)
                    {

                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                string strGroup = groupByStructList[i].ReportField.ToString();
                                invRptPurchaseReturnRegistryTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {

                                invRptPurchaseReturnRegistryTemplate.SetParameterValue(i, "");
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptPurchaseReturnRegistryTemplate;
                    break;
                #endregion    
                   
                case "RptProductWisePurchase":
                    InvRptPurchaseSummeryTemplate invRptProductPurchaseSummeryTemplate = new InvRptPurchaseSummeryTemplate();

                    invRptProductPurchaseSummeryTemplate.SetDataSource(dtReportData);
                    invRptProductPurchaseSummeryTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    invRptProductPurchaseSummeryTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptProductPurchaseSummeryTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptProductPurchaseSummeryTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptProductPurchaseSummeryTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptProductPurchaseSummeryTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    strFieldName = string.Empty;
                    xx = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            invRptProductPurchaseSummeryTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (8 + xx);
                            invRptProductPurchaseSummeryTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                            xx++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 12; i++)
                    {

                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                string strGroup = groupByStructList[i].ReportField.ToString();
                                invRptProductPurchaseSummeryTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {

                                invRptProductPurchaseSummeryTemplate.SetParameterValue(i, "");
                            }
                        }
                    }
                    #endregion

                    reportViewer.crRptViewer.ReportSource = invRptProductPurchaseSummeryTemplate;
                    break;

                case "RptSupplierWisePurchase":
                    InvRptPurchaseSummeryTemplate invRptSupplierPurchaseSummeryTemplate = new InvRptPurchaseSummeryTemplate();

                    invRptSupplierPurchaseSummeryTemplate.SetDataSource(dtReportData);
                    invRptSupplierPurchaseSummeryTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    invRptSupplierPurchaseSummeryTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptSupplierPurchaseSummeryTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptSupplierPurchaseSummeryTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptSupplierPurchaseSummeryTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptSupplierPurchaseSummeryTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    strFieldName = string.Empty;
                    int yy = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            invRptSupplierPurchaseSummeryTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (8 + yy);
                            invRptSupplierPurchaseSummeryTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                            yy++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 12; i++)
                    {

                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                string strGroup = groupByStructList[i].ReportField.ToString();
                                invRptSupplierPurchaseSummeryTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {

                                invRptSupplierPurchaseSummeryTemplate.SetParameterValue(i, "");
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptSupplierPurchaseSummeryTemplate;
                    break;
                case "RptSupplierWisePerformanceAnalysis":
                    invRptSupplierPurchaseSummeryTemplate = new InvRptPurchaseSummeryTemplate();

                    invRptSupplierPurchaseSummeryTemplate.SetDataSource(dtReportData);
                    invRptSupplierPurchaseSummeryTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    invRptSupplierPurchaseSummeryTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptSupplierPurchaseSummeryTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptSupplierPurchaseSummeryTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptSupplierPurchaseSummeryTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptSupplierPurchaseSummeryTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    strFieldName = string.Empty;
                    yy = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            invRptSupplierPurchaseSummeryTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (8 + yy);
                            invRptSupplierPurchaseSummeryTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                            yy++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 12; i++)
                    {

                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                string strGroup = groupByStructList[i].ReportField.ToString();
                                invRptSupplierPurchaseSummeryTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {

                                invRptSupplierPurchaseSummeryTemplate.SetParameterValue(i, "");
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptSupplierPurchaseSummeryTemplate;
                    break;
                #endregion

                #region sales reports
                #region ProductWiseSales
                case "RptProductWiseSales":
                    InvRptSalesSummeryTemplate invRptProductSalesSummeryTemplate = new InvRptSalesSummeryTemplate();

                    invRptProductSalesSummeryTemplate.SetDataSource(dtReportData);
                    invRptProductSalesSummeryTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptProductSalesSummeryTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptProductSalesSummeryTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptProductSalesSummeryTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptProductSalesSummeryTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptProductSalesSummeryTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'"; 
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    strFieldName = string.Empty;
                    int aa = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            invRptProductSalesSummeryTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (8 + aa);
                            invRptProductSalesSummeryTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                            aa++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 6; i++)
                    {

                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                string strGroup = groupByStructList[i].ReportField.ToString();
                                invRptProductSalesSummeryTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {

                                invRptProductSalesSummeryTemplate.SetParameterValue(i, "");
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptProductSalesSummeryTemplate;
                    break;
                #endregion
                #region SupplierWiseSales
                case "RptSupplierWiseSales":
                    InvRptSalesSummeryTemplate invRptSupplierSalesSummeryTemplate = new InvRptSalesSummeryTemplate();

                    invRptSupplierSalesSummeryTemplate.SetDataSource(dtReportData);
                    invRptSupplierSalesSummeryTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptSupplierSalesSummeryTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptSupplierSalesSummeryTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptSupplierSalesSummeryTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptSupplierSalesSummeryTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptSupplierSalesSummeryTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    strFieldName = string.Empty;
                    int mm = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            invRptSupplierSalesSummeryTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (8 + mm);
                            invRptSupplierSalesSummeryTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                            mm++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 6; i++)
                    {

                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                string strGroup = groupByStructList[i].ReportField.ToString();
                                invRptSupplierSalesSummeryTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {

                                invRptSupplierSalesSummeryTemplate.SetParameterValue(i, "");
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptSupplierSalesSummeryTemplate;
                    break;
                #endregion
                #region Sales Register
                case "RptSalesRegister":
                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        InvRptGroupedDetailsTemplate invRptGroupedDetails = new InvRptGroupedDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewGroupedReport(invRptGroupedDetails, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    else
                    {
                        InvRptDetailsTemplate invRptDetailsTemplate = new InvRptDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewUnGroupedReport(invRptDetailsTemplate, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    break;
                #endregion
                #region ExtendedPropertySalesRegister
                case "RptExtendedPropertySalesRegister":
                    InvRptExtendedPropertySalesTemplateLandscape invRptExtendedPropertySalesTemplateLandscape = new InvRptExtendedPropertySalesTemplateLandscape();

                    invRptExtendedPropertySalesTemplateLandscape.SetDataSource(dtReportData);
                    invRptExtendedPropertySalesTemplateLandscape.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    invRptExtendedPropertySalesTemplateLandscape.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptExtendedPropertySalesTemplateLandscape.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptExtendedPropertySalesTemplateLandscape.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptExtendedPropertySalesTemplateLandscape.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptExtendedPropertySalesTemplateLandscape.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptExtendedPropertySalesTemplateLandscape.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptExtendedPropertySalesTemplateLandscape.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    strFieldName = string.Empty;
                    int srex = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            if (reportDataStructList[i].IsSelectionField)
                            { invRptExtendedPropertySalesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { invRptExtendedPropertySalesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (11 + srex);
                            if (reportDataStructList[i].IsSelectionField)
                            { invRptExtendedPropertySalesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { invRptExtendedPropertySalesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                            srex++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 9; i++)
                    {
                        invRptExtendedPropertySalesTemplateLandscape.SetParameterValue(i, "");

                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                invRptExtendedPropertySalesTemplateLandscape.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {
                                invRptExtendedPropertySalesTemplateLandscape.SetParameterValue(i, "");
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptExtendedPropertySalesTemplateLandscape;
                    break;

                #endregion
                #region LocationSales
                case "RptLocationSales":
                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        InvRptGroupedDetailsTemplate invRptGroupedDetails = new InvRptGroupedDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewGroupedReport(invRptGroupedDetails, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    else
                    {
                        InvRptDetailsTemplate invRptDetailsTemplate = new InvRptDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewUnGroupedReport(invRptDetailsTemplate, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    break;
                #endregion
                #region Price Change

                #region PriceChangeDetail
                case "RptProductPriceChangeDetail":
                    InvRptPriceChangeTransactionSummaryTemplateLandscape invRptTransactionPriceChangeDetail = new InvRptPriceChangeTransactionSummaryTemplateLandscape();

                    #region Create Print Data Table using selected Fields
                    ////printDataStructList = reportDataStructList.Where(a => a.IsSelectionField.Equals(true)).ToList();
                    ////printGroupByStructList = groupByStructList.Where(a => a.IsResultGroupBy.Equals(true)).ToList();

                    ////for (int i = 0; i < printDataStructList.Count; i++)
                    ////{
                    ////    if (printDataStructList[i].ReportDataType.Equals(typeof(string)))
                    ////    {
                    ////        dtPrintReportData.Columns.Add(new DataColumn(string.Concat("FieldString", colCount), (Type)(printDataStructList[i].ReportDataType)));
                    ////        colCount++;
                    ////    }

                    ////    if (printDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    ////    {
                    ////        dtPrintReportData.Columns.Add(new DataColumn(string.Concat("FieldDecimal", decColCount), (Type)(printDataStructList[i].ReportDataType)));
                    ////        decColCount++;
                    ////    }
                    ////}
                   
                    //foreach (DataRow sourceRow in dtReportData.Rows)
                    //{
                    //    DataRow destinationRow = dtPrintReportData.Rows.Add();
                    //    columnCount = 0;
                    //    foreach (DataColumn destinationColumn in dtPrintReportData.Columns)
                    //    {
                    //        columnName = destinationColumn.ColumnName;
                    //        columnName2 = printDataStructList[columnCount].ReportField.Trim();
                                    
                    //        if (dtReportData.Columns.Contains(columnName2))
                    //        {
                    //            destinationRow[columnCount] = sourceRow[columnName2];
                    //            columnCount++;
                    //        }
                    //    }
                    //}
                    #endregion

                    //invRptTransactionPriceChangeDetail.SetDataSource(dtPrintReportData);
                    #region Set Values for report header Fields
                    strFieldName = string.Empty;
                    int srpc = 1; int dcpc = 9;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {
                            if (item.ReportDataType.Equals(typeof(string)))
                            {
                                strFieldName = "st" + srpc;
                                invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                srpc++;
                            }

                            if (item.ReportDataType.Equals(typeof(decimal)))
                            {
                                strFieldName = "st" + dcpc;
                                invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                dcpc++;
                            }
                        }
                    }

                    #endregion

                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    invRptTransactionPriceChangeDetail.SetDataSource(dtArrangedReportData);
                    //invRptTransactionPriceChangeDetail.SetDataSource(dtReportData);
                    invRptTransactionPriceChangeDetail.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    invRptTransactionPriceChangeDetail.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    invRptTransactionPriceChangeDetail.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptTransactionPriceChangeDetail.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";
                    int ix = 0;
                    strFieldName = string.Empty;
                    #region Fixed Properties load into report - old
                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    strFieldName = "st" + (i + 1);
                    //    if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //    {
                    //        invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //    }

                    //    if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //    {
                    //        strFieldName = "st" + (10 + ix);
                    //        invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        ix++;
                    //    }
                    //}
                    #endregion

                    //sr = 0;
                    //for (int i = 0; i < printDataStructList.Count; i++)
                    //{
                    //    strFieldName = "st" + (i + 1);
                    //    if (printDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //    {
                    //        invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "'" + printDataStructList[i].ReportFieldName.Trim() + "'";
                    //    }

                    //    if (printDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //    {
                    //        strFieldName = "st" + (11 + sr);
                    //        invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "'" + printDataStructList[i].ReportFieldName.Trim() + "'";
                    //        sr++;
                    //    }
                    //}

                    #region Group By -old

                    //for (int i = 0; i <= 9; i++)
                    //{
                    //    if (invRptTransactionPriceChangeDetail.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.StringParameter))
                    //    {
                    //        invRptTransactionPriceChangeDetail.SetParameterValue(i, "");

                    //        if (groupByStructList.Count > i)
                    //        {
                    //            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    //            {
                    //                invRptTransactionPriceChangeDetail.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //            }
                    //            else
                    //            {
                    //                invRptTransactionPriceChangeDetail.SetParameterValue(i, "");
                    //            }
                    //        }
                    //    }
                    //    else
                    //    { invRptTransactionPriceChangeDetail.SetParameterValue(i, ""); }
                    //}

                    //int printGroupCount = (printGroupByStructList.Count>9?9:printGroupByStructList.Count),
                    //groupStrFieldCount = 0, groupDecFieldCount = 0;

                    //if (printGroupCount > 0)
                    //{
                    //    for (int x = 0; x < printDataStructList.Count; x++)
                    //    {
                    //        if (printDataStructList[x].ReportDataType.Equals(typeof (string)))
                    //        {
                    //            groupStrFieldCount++;
                    //        }

                    //        if (printDataStructList[x].ReportDataType.Equals(typeof (decimal)))
                    //        {
                    //            groupDecFieldCount++;
                    //        }

                    //        for (int i = 0; i < printGroupCount; i++)
                    //        {
                    //            strFieldName = "frmGroup" + (i + 1);
                    //            if (printDataStructList[i].ReportDataType.Equals(typeof (string)))
                    //            {

                    //                if (printGroupByStructList.ToList().Any(c => c.DbColumnName == printDataStructList[x].DbColumnName.Trim()))
                    //                {
                    //                    invRptTransactionPriceChangeDetail.DataDefinition.Groups[x].ConditionField = invRptTransactionPriceChangeDetail.Database.Tables[0].Fields[string.Concat("FieldString", groupStrFieldCount)];
                    //                    invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "'" + invRptTransactionPriceChangeDetail.Database.Tables[0].Fields[string.Concat("FieldString", groupStrFieldCount)] + "'";
                    //                    break;
                    //                }
                    //            }

                    //            if (printDataStructList[i].ReportDataType.Equals(typeof (decimal)))
                    //            {
                    //                if (printGroupByStructList.ToList().Any(c => c.DbColumnName == printDataStructList[x].DbColumnName.Trim()))
                    //                {
                    //                    invRptTransactionPriceChangeDetail.DataDefinition.Groups[x].ConditionField = invRptTransactionPriceChangeDetail.Database.Tables[0].Fields[string.Concat("FieldDecimal", groupDecFieldCount)];
                    //                    invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "'" + invRptTransactionPriceChangeDetail.Database.Tables[0].Fields[string.Concat("FieldDecimal", groupDecFieldCount)] + "'";
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{


                    //    for (int i = 0; i < 10; i++)
                    //    {
                    //        strFieldName = "frmGroup" + (i + 1);

                    //        invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "''";
                    //    }
                    //}
                    #endregion
                    #region Group By

                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        // Set report group field values
                        for (int i = 0; i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count(); i++)
                        {
                            if (i < invRptTransactionPriceChangeDetail.DataDefinition.Groups.Count)
                            {
                                invRptTransactionPriceChangeDetail.DataDefinition.Groups[i].ConditionField = invRptTransactionPriceChangeDetail.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())]; //[string.Concat("FieldString", groupStrFieldCount)];
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < invRptTransactionPriceChangeDetail.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptTransactionPriceChangeDetail.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                if (i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count())
                                {
                                    invRptTransactionPriceChangeDetail.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), true);
                                }
                                else
                                {
                                    invRptTransactionPriceChangeDetail.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < invRptTransactionPriceChangeDetail.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptTransactionPriceChangeDetail.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptTransactionPriceChangeDetail.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptTransactionPriceChangeDetail;
                    break;
                case "RptProductPriceChangeDamageDetail":
                    invRptTransactionPriceChangeDetail = new InvRptPriceChangeTransactionSummaryTemplateLandscape();

                    #region Create Print Data Table using selected Fields
                    ////printDataStructList = reportDataStructList.Where(a => a.IsSelectionField.Equals(true)).ToList();
                    ////printGroupByStructList = groupByStructList.Where(a => a.IsResultGroupBy.Equals(true)).ToList();

                    ////for (int i = 0; i < printDataStructList.Count; i++)
                    ////{
                    ////    if (printDataStructList[i].ReportDataType.Equals(typeof(string)))
                    ////    {
                    ////        dtPrintReportData.Columns.Add(new DataColumn(string.Concat("FieldString", colCount), (Type)(printDataStructList[i].ReportDataType)));
                    ////        colCount++;
                    ////    }

                    ////    if (printDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    ////    {
                    ////        dtPrintReportData.Columns.Add(new DataColumn(string.Concat("FieldDecimal", decColCount), (Type)(printDataStructList[i].ReportDataType)));
                    ////        decColCount++;
                    ////    }
                    ////}

                    //foreach (DataRow sourceRow in dtReportData.Rows)
                    //{
                    //    DataRow destinationRow = dtPrintReportData.Rows.Add();
                    //    columnCount = 0;
                    //    foreach (DataColumn destinationColumn in dtPrintReportData.Columns)
                    //    {
                    //        columnName = destinationColumn.ColumnName;
                    //        columnName2 = printDataStructList[columnCount].ReportField.Trim();

                    //        if (dtReportData.Columns.Contains(columnName2))
                    //        {
                    //            destinationRow[columnCount] = sourceRow[columnName2];
                    //            columnCount++;
                    //        }
                    //    }
                    //}
                    #endregion

                    //invRptTransactionPriceChangeDetail.SetDataSource(dtPrintReportData);
                    #region Set Values for report header Fields
                    strFieldName = string.Empty;
                    srpc = 1;  dcpc = 9;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {
                            if (item.ReportDataType.Equals(typeof(string)))
                            {
                                strFieldName = "st" + srpc;
                                invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                srpc++;
                            }

                            if (item.ReportDataType.Equals(typeof(decimal)))
                            {
                                strFieldName = "st" + dcpc;
                                invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                dcpc++;
                            }
                        }
                    }

                    #endregion

                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    invRptTransactionPriceChangeDetail.SetDataSource(dtArrangedReportData);
                    //invRptTransactionPriceChangeDetail.SetDataSource(dtReportData);
                    invRptTransactionPriceChangeDetail.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    invRptTransactionPriceChangeDetail.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    invRptTransactionPriceChangeDetail.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptTransactionPriceChangeDetail.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";
                    ix = 0;
                    strFieldName = string.Empty;
                    #region Fixed Properties load into report - old
                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    strFieldName = "st" + (i + 1);
                    //    if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //    {
                    //        invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //    }

                    //    if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //    {
                    //        strFieldName = "st" + (10 + ix);
                    //        invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        ix++;
                    //    }
                    //}
                    #endregion

                    //sr = 0;
                    //for (int i = 0; i < printDataStructList.Count; i++)
                    //{
                    //    strFieldName = "st" + (i + 1);
                    //    if (printDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //    {
                    //        invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "'" + printDataStructList[i].ReportFieldName.Trim() + "'";
                    //    }

                    //    if (printDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //    {
                    //        strFieldName = "st" + (11 + sr);
                    //        invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "'" + printDataStructList[i].ReportFieldName.Trim() + "'";
                    //        sr++;
                    //    }
                    //}

                    #region Group By -old

                    //for (int i = 0; i <= 9; i++)
                    //{
                    //    if (invRptTransactionPriceChangeDetail.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.StringParameter))
                    //    {
                    //        invRptTransactionPriceChangeDetail.SetParameterValue(i, "");

                    //        if (groupByStructList.Count > i)
                    //        {
                    //            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    //            {
                    //                invRptTransactionPriceChangeDetail.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //            }
                    //            else
                    //            {
                    //                invRptTransactionPriceChangeDetail.SetParameterValue(i, "");
                    //            }
                    //        }
                    //    }
                    //    else
                    //    { invRptTransactionPriceChangeDetail.SetParameterValue(i, ""); }
                    //}

                    //int printGroupCount = (printGroupByStructList.Count>9?9:printGroupByStructList.Count),
                    //groupStrFieldCount = 0, groupDecFieldCount = 0;

                    //if (printGroupCount > 0)
                    //{
                    //    for (int x = 0; x < printDataStructList.Count; x++)
                    //    {
                    //        if (printDataStructList[x].ReportDataType.Equals(typeof (string)))
                    //        {
                    //            groupStrFieldCount++;
                    //        }

                    //        if (printDataStructList[x].ReportDataType.Equals(typeof (decimal)))
                    //        {
                    //            groupDecFieldCount++;
                    //        }

                    //        for (int i = 0; i < printGroupCount; i++)
                    //        {
                    //            strFieldName = "frmGroup" + (i + 1);
                    //            if (printDataStructList[i].ReportDataType.Equals(typeof (string)))
                    //            {

                    //                if (printGroupByStructList.ToList().Any(c => c.DbColumnName == printDataStructList[x].DbColumnName.Trim()))
                    //                {
                    //                    invRptTransactionPriceChangeDetail.DataDefinition.Groups[x].ConditionField = invRptTransactionPriceChangeDetail.Database.Tables[0].Fields[string.Concat("FieldString", groupStrFieldCount)];
                    //                    invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "'" + invRptTransactionPriceChangeDetail.Database.Tables[0].Fields[string.Concat("FieldString", groupStrFieldCount)] + "'";
                    //                    break;
                    //                }
                    //            }

                    //            if (printDataStructList[i].ReportDataType.Equals(typeof (decimal)))
                    //            {
                    //                if (printGroupByStructList.ToList().Any(c => c.DbColumnName == printDataStructList[x].DbColumnName.Trim()))
                    //                {
                    //                    invRptTransactionPriceChangeDetail.DataDefinition.Groups[x].ConditionField = invRptTransactionPriceChangeDetail.Database.Tables[0].Fields[string.Concat("FieldDecimal", groupDecFieldCount)];
                    //                    invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "'" + invRptTransactionPriceChangeDetail.Database.Tables[0].Fields[string.Concat("FieldDecimal", groupDecFieldCount)] + "'";
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{


                    //    for (int i = 0; i < 10; i++)
                    //    {
                    //        strFieldName = "frmGroup" + (i + 1);

                    //        invRptTransactionPriceChangeDetail.DataDefinition.FormulaFields[strFieldName].Text = "''";
                    //    }
                    //}
                    #endregion
                    #region Group By

                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        // Set report group field values
                        for (int i = 0; i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count(); i++)
                        {
                            if (i < invRptTransactionPriceChangeDetail.DataDefinition.Groups.Count)
                            {
                                invRptTransactionPriceChangeDetail.DataDefinition.Groups[i].ConditionField = invRptTransactionPriceChangeDetail.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())]; //[string.Concat("FieldString", groupStrFieldCount)];
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < invRptTransactionPriceChangeDetail.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptTransactionPriceChangeDetail.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                if (i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count())
                                {
                                    invRptTransactionPriceChangeDetail.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), true);
                                }
                                else
                                {
                                    invRptTransactionPriceChangeDetail.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < invRptTransactionPriceChangeDetail.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptTransactionPriceChangeDetail.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptTransactionPriceChangeDetail.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptTransactionPriceChangeDetail;
                    break;

                #endregion
                #endregion
                #endregion

                #region Staff Sales

                case "RptStaffCreditSales":

                    InvRptSalesTemplateStaffPurchase invRptSalesTemplateStaffPurchase = new InvRptSalesTemplateStaffPurchase();
                    invRptSalesTemplateStaffPurchase.SetDataSource(dtReportData);
                    invRptSalesTemplateStaffPurchase.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    invRptSalesTemplateStaffPurchase.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptSalesTemplateStaffPurchase.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    strFieldName = string.Empty;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (7 +(i-5));
                            invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 4; i++)
                    {
                        invRptSalesTemplateStaffPurchase.SetParameterValue(i, "");
                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                string strGroup = groupByStructList[i].ReportField.ToString();
                                invRptSalesTemplateStaffPurchase.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {
                                invRptSalesTemplateStaffPurchase.SetParameterValue(i, "");
                            }
                        }
                    }
                    #endregion

                    reportViewer.crRptViewer.ReportSource = invRptSalesTemplateStaffPurchase;
                    break;
                case "RptStaffSalaryAdvance":

                    invRptSalesTemplateStaffPurchase = new InvRptSalesTemplateStaffPurchase();
                    invRptSalesTemplateStaffPurchase.SetDataSource(dtReportData);
                    invRptSalesTemplateStaffPurchase.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    invRptSalesTemplateStaffPurchase.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptSalesTemplateStaffPurchase.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    strFieldName = string.Empty;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (7 + (i - 5));
                            invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 4; i++)
                    {
                        invRptSalesTemplateStaffPurchase.SetParameterValue(i, "");
                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                string strGroup = groupByStructList[i].ReportField.ToString();
                                invRptSalesTemplateStaffPurchase.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {
                                invRptSalesTemplateStaffPurchase.SetParameterValue(i, "");
                            }
                        }
                    }
                    #endregion

                    reportViewer.crRptViewer.ReportSource = invRptSalesTemplateStaffPurchase;
                    break;

                case "RptStaffCashCrediCard":

                    invRptSalesTemplateStaffPurchase = new InvRptSalesTemplateStaffPurchase();
                    invRptSalesTemplateStaffPurchase.SetDataSource(dtReportData);
                    invRptSalesTemplateStaffPurchase.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    invRptSalesTemplateStaffPurchase.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptSalesTemplateStaffPurchase.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    strFieldName = string.Empty;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (7 + (i - 5));
                            invRptSalesTemplateStaffPurchase.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 4; i++)
                    {
                        invRptSalesTemplateStaffPurchase.SetParameterValue(i, "");
                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                string strGroup = groupByStructList[i].ReportField.ToString();
                                invRptSalesTemplateStaffPurchase.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {
                                invRptSalesTemplateStaffPurchase.SetParameterValue(i, "");
                            }
                        }
                    }
                    #endregion

                    reportViewer.crRptViewer.ReportSource = invRptSalesTemplateStaffPurchase;
                    break;
                    
              

                #endregion 

                #region stock reports
                #region Excess Report
                case "RptExcessStockAdjustment":
                    InvRptStockTemplate invRptStockExcessTemplate = new InvRptStockTemplate();

                    invRptStockExcessTemplate.SetDataSource(dtReportData);
                    invRptStockExcessTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    invRptStockExcessTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptStockExcessTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptStockExcessTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptStockExcessTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptStockExcessTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    strFieldName = string.Empty;
                    yx = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            invRptStockExcessTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (10 + yx);
                            invRptStockExcessTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                            yx++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 7; i++)
                    {
                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                string strGroup = groupByStructList[i].ReportField.ToString();
                                invRptStockExcessTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {
                                invRptStockExcessTemplate.SetParameterValue(i, "");
                            }
                        }
                        else
                        {
                            invRptStockExcessTemplate.SetParameterValue(i, "");
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptStockExcessTemplate;
                    break;
                #endregion
                #region Shortage Report
                case "RptShortageStockAdjustment":
                    InvRptStockTemplate invRptStockShortageTemplate = new InvRptStockTemplate();

                    invRptStockShortageTemplate.SetDataSource(dtReportData);
                    invRptStockShortageTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    invRptStockShortageTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptStockShortageTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptStockShortageTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptStockShortageTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptStockShortageTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    strFieldName = string.Empty;
                    yx = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            invRptStockShortageTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (10 + yx);
                            invRptStockShortageTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                            yx++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 7; i++)
                    {

                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                string strGroup = groupByStructList[i].ReportField.ToString();
                                invRptStockShortageTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {
                                invRptStockShortageTemplate.SetParameterValue(i, "");
                            }
                        }
                        else
                        {
                            invRptStockShortageTemplate.SetParameterValue(i, "");
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptStockShortageTemplate;
                    break;
                #endregion
                #region Stock Adjustment
                case "RptStockAdjustmentPercentage":
                    InvRptStockTemplate invRptStockAdjustmentPercentageTemplate = new InvRptStockTemplate();

                    invRptStockAdjustmentPercentageTemplate.SetDataSource(dtReportData);
                    invRptStockAdjustmentPercentageTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    invRptStockAdjustmentPercentageTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptStockAdjustmentPercentageTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptStockAdjustmentPercentageTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptStockAdjustmentPercentageTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptStockAdjustmentPercentageTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    strFieldName = string.Empty;
                    yx = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            invRptStockAdjustmentPercentageTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (10 + yx);
                            invRptStockAdjustmentPercentageTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                            yx++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 7; i++)
                    {

                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                string strGroup = groupByStructList[i].ReportField.ToString();
                                invRptStockAdjustmentPercentageTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {
                                invRptStockAdjustmentPercentageTemplate.SetParameterValue(i, "");
                            }
                        }
                        else
                        {
                            invRptStockAdjustmentPercentageTemplate.SetParameterValue(i, "");
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptStockAdjustmentPercentageTemplate;
                    break;
                #endregion
                #region Bin Card
                case "RptBinCard":
                    InvRptStockTemplate invRptBinCardTemplate = new InvRptStockTemplate();

                    invRptBinCardTemplate.SetDataSource(dtReportData);
                    invRptBinCardTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    invRptBinCardTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptBinCardTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptBinCardTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptBinCardTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptBinCardTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    strFieldName = string.Empty;
                    yx = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            invRptBinCardTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (10 + yx);
                            invRptBinCardTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                            yx++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 7; i++)
                    {
                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                string strGroup = groupByStructList[i].ReportField.ToString();
                                invRptBinCardTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {
                                invRptBinCardTemplate.SetParameterValue(i, "");
                            }
                        }
                        else
                        {
                            invRptBinCardTemplate.SetParameterValue(i, "");
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptBinCardTemplate;
                    break;
                #endregion
                #region Opening stock
                case "InvRptOpeningBalanceRegister":
                    ComRptOpeningBalancesTemplateLandscape comRptOpeningBalancesTemplateLandscape = new ComRptOpeningBalancesTemplateLandscape();

                    comRptOpeningBalancesTemplateLandscape.SetDataSource(dtReportData);
                    comRptOpeningBalancesTemplateLandscape.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    comRptOpeningBalancesTemplateLandscape.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    comRptOpeningBalancesTemplateLandscape.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    comRptOpeningBalancesTemplateLandscape.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    comRptOpeningBalancesTemplateLandscape.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    comRptOpeningBalancesTemplateLandscape.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    comRptOpeningBalancesTemplateLandscape.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    comRptOpeningBalancesTemplateLandscape.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    strFieldName = string.Empty;
                    int sro = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            if (reportDataStructList[i].IsSelectionField)
                            { comRptOpeningBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { comRptOpeningBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (11 + sro);
                            if (reportDataStructList[i].IsSelectionField)
                            { comRptOpeningBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { comRptOpeningBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                            sro++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 10; i++)
                    {
                        if (comRptOpeningBalancesTemplateLandscape.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.StringParameter))
                        {
                            comRptOpeningBalancesTemplateLandscape.SetParameterValue(i, "");

                            if (groupByStructList.Count > i)
                            {
                                if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                                {
                                    comRptOpeningBalancesTemplateLandscape.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                                }
                                else
                                {
                                    comRptOpeningBalancesTemplateLandscape.SetParameterValue(i, "");
                                }
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = comRptOpeningBalancesTemplateLandscape;
                    break;

                #endregion
                #region InvRptStockBalance
                case "InvRptStockBalance":
                    InvRptStockBalancesTemplateLandscape1 invRptStockBalancesTemplateLandscape = new InvRptStockBalancesTemplateLandscape1();

                      #region Set Values for report header sields
                    strFieldName = string.Empty;
                    int srst = 1, dcst = 11;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {
                           
                            if (item.ReportDataType.Equals(typeof(string)))
                            {
                                strFieldName = "st" + srst;
                                invRptStockBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                srst++;
                            }

                            if (item.ReportDataType.Equals(typeof(decimal)))
                            {
                                strFieldName = "st" + dcst;
                                invRptStockBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                dcst++;
                            }
                        }
                    }

                #endregion

                    // Re arrange data table header columns for report
                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    invRptStockBalancesTemplateLandscape.SetDataSource(dtArrangedReportData);
                    invRptStockBalancesTemplateLandscape.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    invRptStockBalancesTemplateLandscape.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptStockBalancesTemplateLandscape.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptStockBalancesTemplateLandscape.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptStockBalancesTemplateLandscape.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptStockBalancesTemplateLandscape.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptStockBalancesTemplateLandscape.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptStockBalancesTemplateLandscape.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    #region Group By

                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        // Set report group field values
                        for (int i = 0; i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count(); i++)
                        {
                            if (i < invRptStockBalancesTemplateLandscape.DataDefinition.Groups.Count)
                            {
                                invRptStockBalancesTemplateLandscape.DataDefinition.Groups[i].ConditionField = invRptStockBalancesTemplateLandscape.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())]; //[string.Concat("FieldString", groupStrFieldCount)];
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < invRptStockBalancesTemplateLandscape.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptStockBalancesTemplateLandscape.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                if (i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count())
                                {
                                    invRptStockBalancesTemplateLandscape.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), true);
                                }
                                else
                                {
                                    invRptStockBalancesTemplateLandscape.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < invRptStockBalancesTemplateLandscape.DataDefinition.Groups.Count; i++)
                        {
                            if (invRptStockBalancesTemplateLandscape.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                invRptStockBalancesTemplateLandscape.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptStockBalancesTemplateLandscape;
                    dtReportData.Dispose();
                    dtReportData = null;
                    dtReportConditions.Dispose();
                    dtReportConditions = null;
                    break;

                #endregion
                #region InvRptBatchStockBalance
                case "InvRptBatchStockBalance":
                    InvRptBatchStockBalancesTemplateLandscape invRptBatchStockBalancesTemplateLandscape = new InvRptBatchStockBalancesTemplateLandscape();

                    invRptBatchStockBalancesTemplateLandscape.SetDataSource(dtReportData);
                    invRptBatchStockBalancesTemplateLandscape.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    invRptBatchStockBalancesTemplateLandscape.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    invRptBatchStockBalancesTemplateLandscape.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    invRptBatchStockBalancesTemplateLandscape.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    invRptBatchStockBalancesTemplateLandscape.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    invRptBatchStockBalancesTemplateLandscape.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    invRptBatchStockBalancesTemplateLandscape.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    invRptBatchStockBalancesTemplateLandscape.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    strFieldName = string.Empty;
                    int srbst = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            if (reportDataStructList[i].IsSelectionField)
                            { invRptBatchStockBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { invRptBatchStockBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (9 + srbst);
                            if (reportDataStructList[i].IsSelectionField)
                            { invRptBatchStockBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { invRptBatchStockBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                            srbst++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 9; i++)
                    {
                        if (invRptBatchStockBalancesTemplateLandscape.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.StringParameter))
                        {
                            invRptBatchStockBalancesTemplateLandscape.SetParameterValue(i, "");

                            if (groupByStructList.Count > i)
                            {
                                if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                                {
                                    invRptBatchStockBalancesTemplateLandscape.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                                }
                                else
                                {
                                    invRptBatchStockBalancesTemplateLandscape.SetParameterValue(i, "");
                                }
                            }
                            else
                            {
                                invRptBatchStockBalancesTemplateLandscape.SetParameterValue(i, "");
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = invRptBatchStockBalancesTemplateLandscape;
                    break;

                #endregion
                #region InvRptTransferRegisterr
                case "InvRptTransferRegister":
                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        InvRptGroupedDetailsTemplate invRptGroupedDetails = new InvRptGroupedDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewGroupedReport(invRptGroupedDetails, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    else
                    {
                        InvRptDetailsTemplate invRptDetailsTemplate = new InvRptDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewUnGroupedReport(invRptDetailsTemplate, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    break;

                #endregion

                #endregion

                

                default:
                    return;
            }

            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
           // reportViewer.crRptViewer.ReportSource = null;

            Cursor.Current = Cursors.Default;
        }
       
        /// Organize Report Generator Fields
        public FrmReprotGenerator OrganizeFormFields(AutoGenerateInfo autoGenerateInfo)
        {
            List<Common.ReportDataStruct> reportDatStructList = new List<Common.ReportDataStruct>();            
            FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

            string departmentText = "", categoryText = "", subCategoryText = "", subCategory2Text = "";
            departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
            categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
            subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
            subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;
                    
            switch (autoGenerateInfo.FormName)
            {
                #region inventory transactions

                #region FrmPurchaseOrder
                case "FrmPurchaseOrder":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                   
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Supplier", ReportDataType = typeof(string), DbColumnName = "SupplierID", DbJoinColumnName = "SupplierName", ValueDataType = typeof(long), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Reference", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "InvPurchaseHeaderID", DbJoinColumnName = "Qty", ValueDataType = typeof(decimal), IsJoinField = true, IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Disc. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "NBT(2%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount1", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "NBT(2.04%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount2", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "VAT", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount3", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 7, 4, 8);
                #endregion

                #region FrmGoodsReceivedNote
                case "FrmGoodsReceivedNote":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Supplier", ReportDataType = typeof(string), DbColumnName = "SupplierID", DbJoinColumnName = "SupplierName", ValueDataType = typeof(long), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Supp. Inv.", ReportDataType = typeof(string), DbColumnName = "SupplierInvoiceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Reference", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "P.O.", ReportDataType = typeof(string), DbColumnName = "ReferenceDocumentID", DbJoinColumnName = "DocumentNo", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "A/C", ReportDataType = typeof(string) , ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "InvPurchaseHeaderID", DbJoinColumnName = "Qty", ValueDataType = typeof(decimal), IsJoinField = true, IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true,});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Disc. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "NBT(2%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount1", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "NBT(2.04%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount2", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "VAT", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount3", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 7, 4, 8);
                #endregion

                #region FrmPurchaseReturnNote
                case "FrmPurchaseReturnNote":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Supplier", ReportDataType = typeof(string), DbColumnName = "SupplierID", DbJoinColumnName = "SupplierName", ValueDataType = typeof(long), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Supp. Inv.", ReportDataType = typeof(string), DbColumnName = "SupplierInvoiceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Reference", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "G.R.N", ReportDataType = typeof(string), DbColumnName = "ReferenceDocumentID", DbJoinColumnName = "DocumentNo", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "A/C", ReportDataType = typeof(string) , ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "InvPurchaseHeaderID", DbJoinColumnName = "Qty", ValueDataType = typeof(decimal), IsJoinField = true, IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Disc. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "NBT(2%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount1", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "NBT(2.04%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount2", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "VAT", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount3", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 7, 4, 8);
                #endregion

                #region FrmInvoice
                case "FrmInvoice": 
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Customer", ReportDataType = typeof(string), DbColumnName = "CustomerID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Sales Person", ReportDataType = typeof(string), DbColumnName = "SalesPersonID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Ref. No", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                #region FrmSalesReturnNote
                case "FrmSalesReturnNote":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Customer", ReportDataType = typeof(string), DbColumnName = "CustomerID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Sales Person", ReportDataType = typeof(string), DbColumnName = "SalesPersonID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Ref. No", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true, });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Return Type", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string) });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                #region FrmQuotation
                case "FrmQuotation":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Customer", ReportDataType = typeof(string), DbColumnName = "CustomerID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Sales Person", ReportDataType = typeof(string), DbColumnName = "InvSalesPersonID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Ref. No", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                #region FrmDispatchNote
                case "FrmDispatchNote":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Customer", ReportDataType = typeof(string), DbColumnName = "CustomerID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Sales Person", ReportDataType = typeof(string), DbColumnName = "SalesPersonID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Ref. No", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });                    

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                #region FrmSalesOrder
                case "FrmSalesOrders":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Customer", ReportDataType = typeof(string), DbColumnName = "CustomerID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Sales Person", ReportDataType = typeof(string), DbColumnName = "InvSalesPersonID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Ref. No", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Tax Amt.", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                #region FrmTransferOfGoodsNote
                case "FrmTransferOfGoodsNote":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "From Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "To Location", ReportDataType = typeof(string), DbColumnName = "ToLocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Transfer Type", ReportDataType = typeof(string), DbColumnName = "TransferTypeID", DbJoinColumnName = "TransferType", ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });                                       
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Reference", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });                        
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "InvTransferNoteHeaderID", DbJoinColumnName = "Qty", ValueDataType = typeof(decimal), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Net Amount", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 7, 4, 8);

                #endregion

                #region FrmStockAdjustment
                case "FrmStockAdjustment":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Adjustment Mode", ReportDataType = typeof(string), DbColumnName = "StockAdjustmentMode", ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Ref. No", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 7, 4, 8);
                    
                #endregion

                #region FrmSampleOut
                case "FrmSampleOut":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Issued To", ReportDataType = typeof(string), DbColumnName = "IssuedTo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Delivery Person", ReportDataType = typeof(string), DbColumnName = "DeliveryPerson", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Ref. No", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                #region FrmSampleIn
                case "FrmSampleIn":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Issued To", ReportDataType = typeof(string), DbColumnName = "IssuedTo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Delivery Person", ReportDataType = typeof(string), DbColumnName = "DeliveryPerson", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                #region RptPriceChange
                case "RptPriceChange":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "CreatedDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Department", ReportDataType = typeof(string), DbColumnName = "DepartmentID", ValueDataType = typeof(long),IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Category", ReportDataType = typeof(string), DbColumnName = "CategoryID", ValueDataType = typeof(long),IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Pro.Code", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(string), IsSelectionField = true,IsConditionField =true  });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Pro.Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "CostPrice", ReportDataType = typeof(decimal), DbColumnName = "NewCostPrice", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "SellingPrice", ReportDataType = typeof(decimal), DbColumnName = "NewSellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                case "FrmOpeningStock":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Opening Stock Type", ReportDataType = typeof(string), DbColumnName = "OpeningStockType", ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Ref. No", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
               
                #endregion

                #region POS Audit reports

                case "RptUserWiseDiscount":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "User Name", ReportDataType = typeof(string), DbColumnName = "UserName", ValueDataType = typeof(string) , IsSelectionField = true, IsGroupBy = true, IsConditionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(DateTime), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Location Code", ReportDataType = typeof(string), DbColumnName = "LocationID", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Location Name", ReportDataType = typeof(string), DbColumnName = "LocationID", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Discount", ReportDataType = typeof(decimal), DbColumnName = "Discount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Over Ride Amount", ReportDataType = typeof(decimal), DbColumnName = "OverRide", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                  
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);


                case "RptUserMediaTerminal":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Payment Mode", ReportDataType = typeof(string), DbColumnName = "UserName", ValueDataType = typeof(string) , IsSelectionField = true, IsGroupBy = true, IsConditionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(DateTime), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Terminal No", ReportDataType = typeof(long), DbColumnName = "UnitNo", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Shift No", ReportDataType = typeof(long), DbColumnName = "ShiftNo", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Discount", ReportDataType = typeof(decimal), DbColumnName = "Discount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Over Ride Amount", ReportDataType = typeof(decimal), DbColumnName = "OverRide", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

                case "RptUserWiseSales":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "User Name", ReportDataType = typeof(string), DbColumnName = "UserName", ValueDataType = typeof(string) , IsSelectionField = true, IsGroupBy = true, IsConditionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(DateTime), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Return Amount", ReportDataType = typeof(decimal), DbColumnName = "ReturnAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amount", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Net Amount", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                   
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

                case "RptDetailMediaSales":

                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "User Name", ReportDataType = typeof(string), DbColumnName = "UserName", ValueDataType = typeof(string) , IsSelectionField = true, IsGroupBy = true, IsConditionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Payment Methode", ReportDataType = typeof(string), DbColumnName = "PaymentID", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(DateTime), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amount", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Return Amount", ReportDataType = typeof(decimal), DbColumnName = "ReturnAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Total Sale Amount", ReportDataType = typeof(decimal), DbColumnName = "TotalSale", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

                case "RptMediaSalesSummary":

                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location Code", ReportDataType = typeof(string), DbColumnName = "LocationCode", ValueDataType = typeof(string) , IsSelectionField = true, IsGroupBy = true, IsConditionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Location Name", ReportDataType = typeof(string), DbColumnName = "LocationName", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(DateTime), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "User Name", ReportDataType = typeof(string), DbColumnName = "UserName", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                                        
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Sale Amount", ReportDataType = typeof(decimal), DbColumnName = "SaleAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Return Amount", ReportDataType = typeof(decimal), DbColumnName = "ReturnAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Exchange Amount", ReportDataType = typeof(decimal), DbColumnName = "ExchangeAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Total Sale", ReportDataType = typeof(decimal), DbColumnName = "TotalSale", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

                case "RptSalesDiscount":

                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Bill No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string) , IsSelectionField = true, IsGroupBy = true, IsConditionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "User Name", ReportDataType = typeof(string), DbColumnName = "UserName", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(DateTime), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "TerminalID", ReportDataType = typeof(long), DbColumnName = "TerminalID", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                                        
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Discount Amount", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Net Amount", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

                case "RptCreditCardCollection":


                #endregion

                #region purchasing reports

                #region Purchase Register
                case "RptPurchaseRegister":
                    reportDatStructList = new List<Common.ReportDataStruct>(); 
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Supplier", ReportDataType = typeof(string), DbColumnName = "SupplierID", DbJoinColumnName = "SupplierName", ValueDataType = typeof(string), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "DepartmentName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "CategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "SubCategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "SubCategory2Name", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Purchase Type", ReportDataType = typeof(string), DbColumnName = "DocumentID", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Cost Price", ReportDataType = typeof(decimal), DbColumnName = "CostPrice", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 7, 4, 8);
                #endregion

                #region RptPendingPurchaseOrders
                case "RptPendingPurchaseOrders":
                    reportDatStructList = new List<Common.ReportDataStruct>();

                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Expiry Date", ReportDataType = typeof(string), DbColumnName = "ExpiryDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Supplier", ReportDataType = typeof(string), DbColumnName = "SupplierID", DbJoinColumnName = "SupplierName", ValueDataType = typeof(long), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });                  
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Document Status", ReportDataType = typeof(string), DbColumnName = "DocumentID", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Reference", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Balance Qty", ReportDataType = typeof(decimal), DbColumnName = "InvPurchaseHeaderID", DbJoinColumnName = "BalanceQty", ValueDataType = typeof(decimal), IsJoinField = true, IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Disc. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "NBT(2%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount1", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "NBT(2.04%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount2", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "VAT", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount3", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 7, 4, 8);
                #endregion

                #region Purchase Return Register

                case "RptPurchaseReturnRegister":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "EAN Code", ReportDataType = typeof(string), DbColumnName = "BarCode", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Unit", ReportDataType = typeof(string), DbColumnName = "Unit", ValueDataType = typeof(string) });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Supplier Code", ReportDataType = typeof(string), DbColumnName = "SupplierCode", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Supplier Name", ReportDataType = typeof(string), DbColumnName = "SupplierName", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Cost Price", ReportDataType = typeof(decimal), DbColumnName = "CostPrice", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Disc %", ReportDataType = typeof(decimal), DbColumnName = "DiscPer", ValueDataType = typeof(decimal) });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Disc Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                #region  RptProductWisePurchase
                case "RptProductWisePurchase":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "InvPurchaseHeaderID", ValueDataType = typeof(long), IsGroupBy = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "EAN Code", ReportDataType = typeof(string), DbColumnName = "BarCode", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Unit", ReportDataType = typeof(string), DbColumnName = "Unit", ValueDataType = typeof(string), IsSelectionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Department Code", ReportDataType = typeof(string), DbColumnName = "DepartmentCode", ValueDataType = typeof(string), IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Department Name", ReportDataType = typeof(string), DbColumnName = "DepartmentName", ValueDataType = typeof(string), IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Category Code", ReportDataType = typeof(string), DbColumnName = "CategoryCode", ValueDataType = typeof(string), IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Category Name", ReportDataType = typeof(string), DbColumnName = "CategoryName", ValueDataType = typeof(string), IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "SubCategory Code", ReportDataType = typeof(string), DbColumnName = "SubCategoryCode", ValueDataType = typeof(string), IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = "SubCategory Name", ReportDataType = typeof(string), DbColumnName = "SubCategoryName", ValueDataType = typeof(string), IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString13", ReportFieldName = "Location Code", ReportDataType = typeof(string), DbColumnName = "LocationCode", ValueDataType = typeof(string),IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString14", ReportFieldName = "Location Name", ReportDataType = typeof(string), DbColumnName = "LocationName", ValueDataType = typeof(string), IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString15", ReportFieldName = "Supplier Code", ReportDataType = typeof(string), DbColumnName = "SupplierCode", ValueDataType = typeof(string), IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString16", ReportFieldName = "Supplier Name", ReportDataType = typeof(string), DbColumnName = "SupplierName", ValueDataType = typeof(string), IsGroupBy = true, });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Purchase Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Return Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Cost Price", ReportDataType = typeof(decimal), DbColumnName = "CostPrice", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Disc. Amount.", ReportDataType = typeof(decimal), DbColumnName = "DiscAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Net Amount.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

                #endregion

                #region RptSupplierWisePurchase
                case "RptSupplierWisePurchase":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Supplier", ReportDataType = typeof(string), DbColumnName = "SupplierID", DbJoinColumnName = "SupplierName", ValueDataType = typeof(string), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Purchase Type", ReportDataType = typeof(string), DbColumnName = "DocumentID", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "DepartmentName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "CategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "SubCategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "SubCategory2Name", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Cost Price", ReportDataType = typeof(decimal), DbColumnName = "CostPrice", ValueDataType = typeof(decimal), IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Stock Qty", ReportDataType = typeof(decimal), DbColumnName = "ProductId", DbJoinColumnName = "Stock", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Dis. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "NBT Amt.", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "VAT Amt.", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Net. Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    
                #endregion

                #region RptSupplierWisePerformanceAnalysis
                case "RptSupplierWisePerformanceAnalysis":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Supplier", ReportDataType = typeof(string), DbColumnName = "SupplierID", DbJoinColumnName = "SupplierName", ValueDataType = typeof(string), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Item Code", ReportDataType = typeof(string), DbColumnName = "DocumentID", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "DepartmentName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "CategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "SubCategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "SubCategory2Name", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Cost Price", ReportDataType = typeof(decimal), DbColumnName = "CostPrice", ValueDataType = typeof(decimal), IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Stock Qty", ReportDataType = typeof(decimal), DbColumnName = "ProductId", DbJoinColumnName = "Stock", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Dis. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "NBT Amt.", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "VAT Amt.", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Net. Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

                #endregion




                #endregion

                #region Staff Sales

                case "RptStaffCashCrediCard":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Loca.Code", ReportDataType = typeof(string), DbColumnName = "LocationCode", ValueDataType = typeof(string), IsGroupBy = true, IsSelectionField = true,IsJoinField=true  });
                    // reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Location Name", ReportDataType = typeof(string), DbColumnName = "LocationName", ValueDataType = typeof(string), IsGroupBy = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Loca.Name", ReportDataType = typeof(string), DbColumnName = "LocationId", DbJoinColumnName = "LocationName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "Date", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Receipt", ReportDataType = typeof(string), DbColumnName = "Receipt", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Emp.Code", ReportDataType = typeof(string), DbColumnName = "EmployeeCode", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Emp.Code", ReportDataType = typeof(string), DbColumnName = "EmployeeId", DbJoinColumnName = "EmployeeCode", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Emp.Name", ReportDataType = typeof(string), DbColumnName = "EmployeeId", DbJoinColumnName = "EmployeeName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = false });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Terminal", ReportDataType = typeof(string), DbColumnName = "TerminalNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Amount", ReportDataType = typeof(decimal), DbColumnName = "Amount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "DiscAmt", ReportDataType = typeof(decimal), DbColumnName = "DiscAmt", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Dis. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Net. Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

                case "RptStaffCreditSales":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Loca.Code", ReportDataType = typeof(string), DbColumnName = "LocationCode", ValueDataType = typeof(string), IsGroupBy = true, IsSelectionField = true,IsJoinField=true  });
                    // reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Location Name", ReportDataType = typeof(string), DbColumnName = "LocationName", ValueDataType = typeof(string), IsGroupBy = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Loca.Name", ReportDataType = typeof(string), DbColumnName = "LocationId", DbJoinColumnName = "LocationName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "Date", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Receipt", ReportDataType = typeof(string), DbColumnName = "Receipt", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Emp.Code", ReportDataType = typeof(string), DbColumnName = "EmployeeCode", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Emp.Code", ReportDataType = typeof(string), DbColumnName = "EmployeeId", DbJoinColumnName = "EmployeeCode", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Emp.Name", ReportDataType = typeof(string), DbColumnName = "EmployeeId", DbJoinColumnName = "EmployeeName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = false });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Terminal", ReportDataType = typeof(string), DbColumnName = "TerminalNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Amount", ReportDataType = typeof(decimal), DbColumnName = "Amount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "DiscAmt", ReportDataType = typeof(decimal), DbColumnName = "DiscAmt", ValueDataType = typeof(decimal), IsSelectionField = false, IsConditionField = false });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Dis. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Net. Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);


                case "RptStaffSalaryAdvance":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Loca.Code", ReportDataType = typeof(string), DbColumnName = "LocationCode", ValueDataType = typeof(string), IsGroupBy = true, IsSelectionField = true,IsJoinField=true  });
                    // reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Location Name", ReportDataType = typeof(string), DbColumnName = "LocationName", ValueDataType = typeof(string), IsGroupBy = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Loca.Name", ReportDataType = typeof(string), DbColumnName = "LocationId", DbJoinColumnName = "LocationName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "Date", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Receipt", ReportDataType = typeof(string), DbColumnName = "Receipt", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Emp.Code", ReportDataType = typeof(string), DbColumnName = "EmployeeId", DbJoinColumnName = "EmployeeCode", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Emp.Name", ReportDataType = typeof(string), DbColumnName = "EmployeeId", DbJoinColumnName = "EmployeeName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = false });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Terminal", ReportDataType = typeof(string), DbColumnName = "TerminalNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Amount", ReportDataType = typeof(decimal), DbColumnName = "Amount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "DiscAmt", ReportDataType = typeof(decimal), DbColumnName = "DiscAmt", ValueDataType = typeof(decimal), IsSelectionField = false, IsConditionField = false });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Dis. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Net. Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                #region sales reports

                case "RptProductWiseSales":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "InvPurchaseHeaderID", ValueDataType = typeof(long), IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "EAN Code", ReportDataType = typeof(string), DbColumnName = "BarCode", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Unit", ReportDataType = typeof(string), DbColumnName = "Unit", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Department Code", ReportDataType = typeof(string), DbColumnName = "DepartmentCode", ValueDataType = typeof(string), IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "DepartmentName", ValueDataType = typeof(string), IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Category Code", ReportDataType = typeof(string), DbColumnName = "CategoryCode", ValueDataType = typeof(string), IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "CategoryName", ValueDataType = typeof(string), IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "SubCategory Code", ReportDataType = typeof(string), DbColumnName = "SubCategoryCode", ValueDataType = typeof(string), IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "SubCategoryName", ValueDataType = typeof(string), IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString13", ReportFieldName = "Location Code", ReportDataType = typeof(string), DbColumnName = "LocationCode", ValueDataType = typeof(string), IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString14", ReportFieldName = "Location Name", ReportDataType = typeof(string), DbColumnName = "LocationName", ValueDataType = typeof(string), IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString15", ReportFieldName = "Supplier Code", ReportDataType = typeof(string), DbColumnName = "SupplierCode", ValueDataType = typeof(string), IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString16", ReportFieldName = "Supplier Name", ReportDataType = typeof(string), DbColumnName = "SupplierName", ValueDataType = typeof(string), IsGroupBy = true, });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Sales Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Return Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Cost Price", ReportDataType = typeof(decimal), DbColumnName = "CostPrice", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Disc. Amount.", ReportDataType = typeof(decimal), DbColumnName = "DiscAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Net Amount.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);


                case "RptSupplierWiseSales":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "InvPurchaseHeaderID", ValueDataType = typeof(long), IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "EAN Code", ReportDataType = typeof(string), DbColumnName = "BarCode", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Unit", ReportDataType = typeof(string), DbColumnName = "Unit", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Department Code", ReportDataType = typeof(string), DbColumnName = "DepartmentCode", ValueDataType = typeof(string), IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "DepartmentName", ValueDataType = typeof(string), IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Category Code", ReportDataType = typeof(string), DbColumnName = "CategoryCode", ValueDataType = typeof(string), IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "CategoryName", ValueDataType = typeof(string), IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "SubCategory Code", ReportDataType = typeof(string), DbColumnName = "SubCategoryCode", ValueDataType = typeof(string), IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "SubCategoryName", ValueDataType = typeof(string), IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString13", ReportFieldName = "Location Code", ReportDataType = typeof(string), DbColumnName = "LocationCode", ValueDataType = typeof(string), IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString14", ReportFieldName = "Location Name", ReportDataType = typeof(string), DbColumnName = "LocationName", ValueDataType = typeof(string), IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString15", ReportFieldName = "Supplier Code", ReportDataType = typeof(string), DbColumnName = "SupplierCode", ValueDataType = typeof(string), IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString16", ReportFieldName = "Supplier Name", ReportDataType = typeof(string), DbColumnName = "SupplierName", ValueDataType = typeof(string), IsGroupBy = true, });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Sales Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Return Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Sale Price", ReportDataType = typeof(decimal), DbColumnName = "SellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Disc Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

                case "RptSalesRegister":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Prod. Code", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationName", ValueDataType = typeof(string), IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "DepartmentName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "CategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "SubCategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "SubCategory2Name", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Sale Type", ReportDataType = typeof(string), DbColumnName = "DocumentID", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Terminal", ReportDataType = typeof(string), DbColumnName = "TerminalNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Customer Type", ReportDataType = typeof(string), DbColumnName = "CustomerType", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = "Customer", ReportDataType = typeof(string), DbColumnName = "CustomerCode", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString13", ReportFieldName = "Cashier", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString14", ReportFieldName = "Supplier", ReportDataType = typeof(string), DbColumnName = "SupplierName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Dis. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "S. Disc. Amount.", ReportDataType = typeof(decimal), DbColumnName = "SubTotalDiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = false, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Net. Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "Sel. Price", ReportDataType = typeof(decimal), DbColumnName = "SellingPrice", ValueDataType = typeof(decimal), IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Cost Price", ReportDataType = typeof(decimal), DbColumnName = "CostPrice", ValueDataType = typeof(decimal), IsConditionField = true });
                    
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 7, 4, 4);
                    //return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

                case "RptLocationSales":
                    reportDatStructList = new List<Common.ReportDataStruct>();

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Prod. Code", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Company", ReportDataType = typeof(string), DbColumnName = "Company", ValueDataType = typeof(string), IsGroupBy = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Cancel", ReportDataType = typeof(string), DbColumnName = "Cancel", ValueDataType = typeof(bool), IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Pending", ReportDataType = typeof(string), DbColumnName = "Pending", ValueDataType = typeof(bool), IsConditionField = true, IsSelectionField = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Del.Date", ReportDataType = typeof(string), DbColumnName = "DeliverDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Customer", ReportDataType = typeof(string), DbColumnName = "CustomerID", ValueDataType = typeof(int), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "SalesPerson", ReportDataType = typeof(string), DbColumnName = "InvSalesPersonID", ValueDataType = typeof(int), IsSelectionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "DepartmentName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "CategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString13", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "SubCategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString14", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "SubCategory2Name", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString15", ReportFieldName = "Size", ReportDataType = typeof(string), DbColumnName = "Size", ValueDataType = typeof(string),  IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString16", ReportFieldName = "Gauge", ReportDataType = typeof(string), DbColumnName = "Gauge", ValueDataType = typeof(string), IsSelectionField = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "OrderQty", ReportDataType = typeof(decimal), DbColumnName = "OrderQty", ValueDataType = typeof(decimal), IsSelectionField = true,  IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "BalaceQty", ReportDataType = typeof(decimal), DbColumnName = "BalanceQty", ValueDataType = typeof(decimal), IsSelectionField = true,  IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Sel. Price", ReportDataType = typeof(decimal), DbColumnName = "SellingPrice", ValueDataType = typeof(decimal), IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Net. Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                                                            
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 7, 4, 4);



                case "RptExtendedPropertySalesRegister":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "DepartmentName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "CategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "SubCategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "SubCategory2Name", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Sale Type", ReportDataType = typeof(string), DbColumnName = "DocumentID", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Prod. Code", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "", ReportDataType = typeof(string), DbColumnName = "", ValueDataType = typeof(string) });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = "", ReportDataType = typeof(string), DbColumnName = "", ValueDataType = typeof(string) });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString13", ReportFieldName = "", ReportDataType = typeof(string), DbColumnName = "", ValueDataType = typeof(string) });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString14", ReportFieldName = "", ReportDataType = typeof(string), DbColumnName = "", ValueDataType = typeof(string) });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString15", ReportFieldName = "", ReportDataType = typeof(string), DbColumnName = "", ValueDataType = typeof(string) });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString16", ReportFieldName = "", ReportDataType = typeof(string), DbColumnName = "", ValueDataType = typeof(string) });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString17", ReportFieldName = "", ReportDataType = typeof(string), DbColumnName = "", ValueDataType = typeof(string) });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString18", ReportFieldName = "", ReportDataType = typeof(string), DbColumnName = "", ValueDataType = typeof(string) });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString19", ReportFieldName = "", ReportDataType = typeof(string), DbColumnName = "", ValueDataType = typeof(string) });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString20", ReportFieldName = "", ReportDataType = typeof(string), DbColumnName = "", ValueDataType = typeof(string) });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString21", ReportFieldName = "", ReportDataType = typeof(string), DbColumnName = "", ValueDataType = typeof(string) });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString22", ReportFieldName = "", ReportDataType = typeof(string), DbColumnName = "", ValueDataType = typeof(string) });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString23", ReportFieldName = "", ReportDataType = typeof(string), DbColumnName = "", ValueDataType = typeof(string) });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString24", ReportFieldName = "", ReportDataType = typeof(string), DbColumnName = "", ValueDataType = typeof(string) });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Dis. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Net. Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Sel. Price", ReportDataType = typeof(decimal), DbColumnName = "SellingPrice", ValueDataType = typeof(decimal), IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "Cost Price", ReportDataType = typeof(decimal), DbColumnName = "CostPrice", ValueDataType = typeof(decimal), IsConditionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);


                case "RptSummarySalesBook":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Loca. Code", ReportDataType = typeof(string), DbColumnName = "LocationCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Dep. Code", ReportDataType = typeof(string), DbColumnName = "DepartmentCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Cat. Code", ReportDataType = typeof(string), DbColumnName = "CategoryCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "SubCat. Code", ReportDataType = typeof(string), DbColumnName = "SubCategoryCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "SubCat2. Code", ReportDataType = typeof(string), DbColumnName = "SubCategory2Code", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Prod. Code", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Cust. Code", ReportDataType = typeof(string), DbColumnName = "CustomerCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Sales Person", ReportDataType = typeof(string), DbColumnName = "SalesPersonCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Dis. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Net. Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });


                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

              
 


                #region RptProductPriceChangeDetail
                case "RptProductPriceChangeDetail":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", IsConditionNameJoined = true, ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "CreatedDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Batch No", ReportDataType = typeof(string), DbColumnName = "PriceBatchNo", DbJoinColumnName = "BatchNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "DepartmentID", DbJoinColumnName = "DepartmentName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "CategoryID", DbJoinColumnName = "CategoryName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "SubCategoryID", DbJoinColumnName = "SubCategoryName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "SubCategory2ID", DbJoinColumnName = "SubCategory2Name", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Pro.Code", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductCode", ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Pro.Name", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Unit", ReportDataType = typeof(string), DbColumnName = "UnitOfMeasureID", DbJoinColumnName = "UnitOfMeasureName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString13", ReportFieldName = "Effect Date", ReportDataType = typeof(string), DbColumnName = "EffectFromDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Stock", ReportDataType = typeof(decimal), DbColumnName = "CurrentStock", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Current Price", ReportDataType = typeof(decimal), DbColumnName = "OldSellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Revised Price", ReportDataType = typeof(decimal), DbColumnName = "NewSellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Cost Price", ReportDataType = typeof(decimal), DbColumnName = "OldCostPrice", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Previous Sales Value", ReportDataType = typeof(decimal), DbColumnName = "OldSellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "Revised Sales Value", ReportDataType = typeof(decimal), DbColumnName = "NewSellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Loss/Gain", ReportDataType = typeof(decimal), DbColumnName = "NewSellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal8", ReportFieldName = "Item Loss/Gain", ReportDataType = typeof(decimal), DbColumnName = "NewSellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 7, 4, 4);
                    //return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                case "RptProductPriceChangeDamageDetail":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", IsConditionNameJoined = true, ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "CreatedDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "DepartmentID", DbJoinColumnName = "DepartmentName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "CategoryID", DbJoinColumnName = "CategoryName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "SubCategoryID", DbJoinColumnName = "SubCategoryName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "SubCategory2ID", DbJoinColumnName = "SubCategory2Name", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Pro.Code", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductCode", ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Pro.Name", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID",DbJoinColumnName = "ProductName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "New Pro.Code", ReportDataType = typeof(string), DbColumnName = "NewProductID",DbJoinColumnName = "NewProductCode", ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "New Pro.Name", ReportDataType = typeof(string), DbColumnName = "NewProductName", DbJoinColumnName = "ProductName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", DbJoinColumnName = "CreatedUser", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString13", ReportFieldName = "Damage Type", ReportDataType = typeof(string), DbColumnName = "DamageType", DbJoinColumnName = "DamageType", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString14", ReportFieldName = "From Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString15", ReportFieldName = "TogNo", ReportDataType = typeof(string), DbColumnName = "TogNo", DbJoinColumnName = "TogNo", IsConditionNameJoined = true, ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString16", ReportFieldName = "Supplier", ReportDataType = typeof(string), DbColumnName = "SupplierID", DbJoinColumnName = "SupplierName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "QTY", ReportDataType = typeof(decimal), DbColumnName = "CurrentStock", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal8", ReportFieldName = "Current Price", ReportDataType = typeof(decimal), DbColumnName = "OldSellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal9", ReportFieldName = "Revised Price", ReportDataType = typeof(decimal), DbColumnName = "NewSellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecima20", ReportFieldName = "Cost Price", ReportDataType = typeof(decimal), DbColumnName = "OldCostPrice", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecima21", ReportFieldName = "Previous Sales Value", ReportDataType = typeof(decimal), DbColumnName = "OldSellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecima22", ReportFieldName = "Revised Sales Value", ReportDataType = typeof(decimal), DbColumnName = "NewSellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecima23", ReportFieldName = "Loss/Gain", ReportDataType = typeof(decimal), DbColumnName = "NewSellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecima24", ReportFieldName = "Item Loss/Gain", ReportDataType = typeof(decimal), DbColumnName = "NewSellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true });
                     
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 7, 4, 4);
                    //return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);




                #endregion

                #endregion

                #region stock reports
                #region Excess Stock
                case "RptExcessStockAdjustment":
                    departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
                    categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
                    subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
                    subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;

                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "DepartmentID", DbJoinColumnName = "DepartmentName", ValueDataType = typeof(long), IsConditionNameJoined = true, IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "CategoryID", DbJoinColumnName = "CategoryName", ValueDataType = typeof(long), IsConditionNameJoined = true, IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "SubCategoryID", DbJoinColumnName = "SubCategoryName", ValueDataType = typeof(long), IsConditionNameJoined = true, IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductCode", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductName", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Unit", ReportDataType = typeof(string), DbColumnName = "UnitOfMeasureID", DbJoinColumnName = "UnitOfMeasureName", ValueDataType = typeof(long), IsConditionNameJoined = true, IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "ExcessQty", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Percentage", ReportDataType = typeof(decimal), DbColumnName = "OrderQty", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion
                #region Shortage Stock // 
                case "RptShortageStockAdjustment":
                    departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
                    categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
                    subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
                    subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;

                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "DepartmentID", DbJoinColumnName = "DepartmentName", ValueDataType = typeof(long), IsConditionNameJoined = true, IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "CategoryID", DbJoinColumnName = "CategoryName", ValueDataType = typeof(long), IsConditionNameJoined = true, IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "SubCategoryID", DbJoinColumnName = "SubCategoryName", ValueDataType = typeof(long), IsConditionNameJoined = true, IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductCode", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductName", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Unit", ReportDataType = typeof(string), DbColumnName = "UnitOfMeasureID", DbJoinColumnName = "UnitOfMeasureName", ValueDataType = typeof(long), IsConditionNameJoined = true, IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "ShortageQty", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Percentage", ReportDataType = typeof(decimal), DbColumnName = "OrderQty", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion
                #region Stock Adjustment
                case "RptStockAdjustmentPercentage":
                    departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
                    categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
                    subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
                    subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;

                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsConditionNameJoined = true, IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "DepartmentID", DbJoinColumnName = "DepartmentName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "CategoryID", DbJoinColumnName = "CategoryName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "SubCategoryID", DbJoinColumnName = "SubCategoryName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductCode", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductName", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Unit", ReportDataType = typeof(string), DbColumnName = "UnitOfMeasureID", DbJoinColumnName = "UnitOfMeasureName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Excess Qty", ReportDataType = typeof(decimal), DbColumnName = "ExcessQty", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Shortage Qty", ReportDataType = typeof(decimal), DbColumnName = "ShortageQty", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Overwrite Qty", ReportDataType = typeof(decimal), DbColumnName = "OverWriteQty", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Percentage", ReportDataType = typeof(decimal), DbColumnName = "OrderQty", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Cost Value", ReportDataType = typeof(decimal), DbColumnName = "CostValue", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "Selling Value", ReportDataType = typeof(decimal), DbColumnName = "SellingValue", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion
                #region Bin Card
                case "RptBinCard":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", ValueDataType = typeof(int), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Description", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Reference No", ReportDataType = typeof(string), DbColumnName = "SubCategoryID", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Customer/Supplier", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "", ReportDataType = typeof(string), DbColumnName = "UnitOfMeasureID", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Price", ReportDataType = typeof(decimal), DbColumnName = "ExcessQty", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "In Qty", ReportDataType = typeof(decimal), DbColumnName = "ShortageQty", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Out Qty", ReportDataType = typeof(decimal), DbColumnName = "OverWriteQty", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Percentage", ReportDataType = typeof(decimal), DbColumnName = "OrderQty", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Cost Value", ReportDataType = typeof(decimal), DbColumnName = "CostValue", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "Selling Value", ReportDataType = typeof(decimal), DbColumnName = "SellingValue", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion
                #region Opening Stock
                case "InvRptOpeningBalanceRegister":
                     reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "InvDepartmentID", DbJoinColumnName = "DepartmentName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "InvCategoryID", DbJoinColumnName = "CategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "InvSubCategoryID", DbJoinColumnName = "SubCategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "InvSubCategory2ID", DbJoinColumnName = "SubCategory2Name", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductCode", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Opening Stock Type", ReportDataType = typeof(string), DbColumnName = "OpeningStockType", ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Opening Stock Type", ReportDataType = typeof(string), DbColumnName = "OpeningStockType", DbJoinColumnName = "LookupValue", ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Selling Value", ReportDataType = typeof(decimal), DbColumnName = "SellingValue", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 10 ,2,10);
                //return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion
                #region InvRptStockBalance
                case "InvRptStockBalance":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Supplier", ReportDataType = typeof(string), DbColumnName = "SupplierID", DbJoinColumnName = "SupplierName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "InvDepartmentID", DbJoinColumnName = "DepartmentName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "InvCategoryID", DbJoinColumnName = "CategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "InvSubCategoryID", DbJoinColumnName = "SubCategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "InvSubCategory2ID", DbJoinColumnName = "SubCategory2Name", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductCode", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Stock", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Sale Price", ReportDataType = typeof(decimal), DbColumnName = "SellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Cost Price", ReportDataType = typeof(decimal), DbColumnName = "CostPrice", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Selling Value", ReportDataType = typeof(decimal), DbColumnName = "", ValueDataType = typeof(decimal), IsSelectionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Cost Value", ReportDataType = typeof(decimal), DbColumnName = "", ValueDataType = typeof(decimal), IsSelectionField = true});
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 7, 4, 8);
                    //return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion
                #region InvRptBatchStockBalance
                case "InvRptBatchStockBalance":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Supplier", ReportDataType = typeof(string), DbColumnName = "SupplierID", DbJoinColumnName = "SupplierName", ValueDataType = typeof(long), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Batch No", ReportDataType = typeof(string), DbColumnName = "BatchNo", ValueDataType = typeof(string), IsGroupBy = true, IsConditionField = true, IsSelectionField = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "InvDepartmentID", DbJoinColumnName = "DepartmentName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "InvCategoryID", DbJoinColumnName = "CategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "InvSubCategoryID", DbJoinColumnName = "SubCategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "InvSubCategory2ID", DbJoinColumnName = "SubCategory2Name", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductCode", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Balance Qty", ReportDataType = typeof(decimal), DbColumnName = "BalanceQty", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });    
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });                    
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Sale Price", ReportDataType = typeof(decimal), DbColumnName = "SellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Cost Price", ReportDataType = typeof(decimal), DbColumnName = "CostPrice", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Selling Value", ReportDataType = typeof(decimal), DbColumnName = "", ValueDataType = typeof(decimal), IsSelectionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "Cost Value", ReportDataType = typeof(decimal), DbColumnName = "", ValueDataType = typeof(decimal), IsSelectionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

                #endregion
                #region InvRptTransferRegister
                case "InvRptTransferRegister":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Transfer Type", ReportDataType = typeof(string), DbColumnName = "TransferTypeID", DbJoinColumnName = "TransferType", ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "From Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "To Location", ReportDataType = typeof(string), DbColumnName = "ToLocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "InvDepartmentID", DbJoinColumnName = "DepartmentName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "InvCategoryID", DbJoinColumnName = "CategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "InvSubCategoryID", DbJoinColumnName = "SubCategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "InvSubCategory2ID", DbJoinColumnName = "SubCategory2Name", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductCode", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Sel. Price", ReportDataType = typeof(decimal), DbColumnName = "SellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Cost Price", ReportDataType = typeof(decimal), DbColumnName = "CostPrice", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Sel Value.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Cost Value.", ReportDataType = typeof(decimal), DbColumnName = "", ValueDataType = typeof(decimal), IsSelectionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                #endregion

                #region inventory references
                #region Department
                case "FrmDepartment":
                    departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
                    categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;

                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = string.Concat(departmentText, " Code"), ReportDataType = typeof(string), DbColumnName = "DepartmentCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = string.Concat(departmentText, " Name"), ReportDataType = typeof(string), DbColumnName = "DepartmentName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 5, 0, 4);
                    return frmReprotGenerator;

                #endregion
                #region Category
                case "FrmCategory":
                    departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
                    categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
                    if (autoGenerateInfo.IsDepend)
                    {
                        reportDatStructList = new List<Common.ReportDataStruct>();
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = string.Concat(categoryText, " Code"), ReportDataType = typeof(string), DbColumnName = "CategoryCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = string.Concat(categoryText, " Name"), ReportDataType = typeof(string), DbColumnName = "CategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = string.Concat(departmentText , " Name"), ReportDataType = typeof(string), DbColumnName = "InvDepartmentID", DbJoinColumnName = "DepartmentName", ValueDataType = typeof(long), IsConditionNameJoined = true, IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                        frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 5, 0, 4);
                        return frmReprotGenerator;
                    }
                    else
                    {
                        reportDatStructList = new List<Common.ReportDataStruct>();
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = string.Concat(categoryText, " Code"), ReportDataType = typeof(string), DbColumnName = "CategoryCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = string.Concat(categoryText, " Name"), ReportDataType = typeof(string), DbColumnName = "CategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                        frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 5, 0, 4);
                        return frmReprotGenerator;
                    }
                #endregion
                #region Sub Category
                case "FrmSubCategory":
                    categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
                    subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
                    if (autoGenerateInfo.IsDepend)
                    {
                        reportDatStructList = new List<Common.ReportDataStruct>();
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = string.Concat(subCategoryText, " Code"), ReportDataType = typeof(string), DbColumnName = "SubCategoryCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = string.Concat(subCategoryText, " Name"), ReportDataType = typeof(string), DbColumnName = "SubCategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = string.Concat(categoryText, " Name"), ReportDataType = typeof(string), DbColumnName = "InvCategoryID", DbJoinColumnName = "CategoryName", ValueDataType = typeof(string), IsConditionNameJoined = true, IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                        frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 5, 0, 4);
                        return frmReprotGenerator;
                    }
                    else
                    {
                        reportDatStructList = new List<Common.ReportDataStruct>();
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = string.Concat(subCategoryText, " Code"), ReportDataType = typeof(string), DbColumnName = "SubCategoryCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = string.Concat(subCategoryText, " Name"), ReportDataType = typeof(string), DbColumnName = "SubCategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                        frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,5,0,4);
                        return frmReprotGenerator;
                    }
                #endregion
                #region Sub Category 2
                case "FrmSubCategory2":
                    subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
                    subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;
                    if (autoGenerateInfo.IsDepend)
                    {
                        reportDatStructList = new List<Common.ReportDataStruct>();
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = string.Concat(subCategory2Text, " Code"), ReportDataType = typeof(string), DbColumnName = "SubCategory2Code", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = string.Concat(subCategory2Text, " Name"), ReportDataType = typeof(string), DbColumnName = "SubCategory2Name", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = string.Concat(subCategoryText, " Name"), ReportDataType = typeof(string), DbColumnName = "InvSubCategoryID", DbJoinColumnName = "SubCategoryName", ValueDataType = typeof(string), IsConditionNameJoined =  true, IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                        frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 5, 0, 4);
                        return frmReprotGenerator;
                    }
                    else
                    {
                        reportDatStructList = new List<Common.ReportDataStruct>();
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = string.Concat(subCategory2Text, " Code"), ReportDataType = typeof(string), DbColumnName = "SubCategory2Code", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = string.Concat(subCategory2Text, " Name"), ReportDataType = typeof(string), DbColumnName = "SubCategory2Name", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                        frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 5, 0, 4);
                        return frmReprotGenerator;
                    }
                #endregion
                #region Product
                case "FrmProduct":
                    departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
                    categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
                    subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
                    subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "InvDepartmentID", DbJoinColumnName = "DepartmentName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "InvCategoryID", DbJoinColumnName = "CategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "InvSubCategoryID", DbJoinColumnName = "SubCategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "InvSubCategory2ID", DbJoinColumnName = "SubCategory2Name", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Code", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsRecordFilterByGivenOption = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Name on Invoice", ReportDataType = typeof(string), DbColumnName = "NameOnInvoice", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Bar Code", ReportDataType = typeof(string), DbColumnName = "BarCode", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "   Re-order Qty", ReportDataType = typeof(string), DbColumnName = "ReOrderQty", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "    Cost Price", ReportDataType = typeof(string), DbColumnName = "CostPrice", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "  Selling Price", ReportDataType = typeof(string), DbColumnName = "SellingPrice", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = " Fixed Discount", ReportDataType = typeof(string), DbColumnName = "FixedDiscount", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString13", ReportFieldName = "   Batch Status", ReportDataType = typeof(string), DbColumnName = "IsBatch", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString14", ReportFieldName = "Promotion Status", ReportDataType = typeof(string), DbColumnName = "IsPromotion", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString15", ReportFieldName = "Stock Entry", ReportDataType = typeof(string), DbColumnName = "StockEntry", ValueDataType = typeof(string), IsSelectionField = true, IsUntickedField = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 14, 0, 4);
                    return frmReprotGenerator;
                #endregion
                #region Supplier
                case "FrmSupplier":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Code", ReportDataType = typeof(string), DbColumnName = "SupplierCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true,IsRecordFilterByGivenOption=true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Sup.Name", ReportDataType = typeof(string), DbColumnName = "SupplierName", ValueDataType = typeof(long), IsSelectionField = true, IsJoinField = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Billing Address", ReportDataType = typeof(string), DbColumnName = "BillingAddress1", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Telephone", ReportDataType = typeof(string), DbColumnName = "BillingTelephone", ValueDataType = typeof(string), IsSelectionField = true,IsConditionField = true,IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "NIC", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true,IsConditionField = true,IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Rec.Date", ReportDataType = typeof(string), DbColumnName = "CreatedDate", ValueDataType = typeof(DateTime), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Payment Method", ReportDataType = typeof(string), DbColumnName = "PaymentMethodID", DbJoinColumnName = "PaymentMethodName", ValueDataType = typeof(int), IsConditionNameJoined = true, IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Contact Person", ReportDataType = typeof(string), DbColumnName = "ContactPersonName", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Delivery Address", ReportDataType = typeof(string), DbColumnName = "DeliveryAddress1", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Reference No", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Supplier Group", ReportDataType = typeof(string), DbColumnName = "SupplierGroupID", DbJoinColumnName = "SupplierGroupName", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsJoinField = true,IsGroupBy = true});

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,10,0,4);
                    return frmReprotGenerator;
                #endregion
                #region SalesPerson
                case "FrmSalesPerson":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Code", ReportDataType = typeof(string), DbColumnName = "SalesPersonCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Sales Person", ReportDataType = typeof(string), DbColumnName = "SalesPersonName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Address", ReportDataType = typeof(string), DbColumnName = "Address1", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Telephone", ReportDataType = typeof(string), DbColumnName = "Telephone", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "NIC", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Rec.Date", ReportDataType = typeof(string), DbColumnName = "CreatedDate", ValueDataType = typeof(DateTime), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Type", ReportDataType = typeof(string), DbColumnName = "SalesPersonType", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion
                #region Employee
                case "FrmEmployee":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Code", ReportDataType = typeof(string), DbColumnName = "EmployeeCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Employee Name", ReportDataType = typeof(string), DbColumnName = "EmployeeName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Designation", ReportDataType = typeof(string), DbColumnName = "Designation", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Mobile", ReportDataType = typeof(string), DbColumnName = "Mobile", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Telephone", ReportDataType = typeof(string), DbColumnName = "Telephone", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "NIC", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Address", ReportDataType = typeof(string), DbColumnName = "Address1", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 7, 4, 8);
                #endregion


                #endregion

                default:
                    return null;
            }            
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDatStruct, AutoGenerateInfo autoGenerateInfo)
        {
            switch (autoGenerateInfo.FormName)
            {
                #region inventory transactions

                case "FrmPurchaseOrder":
                    InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                    return invPurchaseOrderService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmGoodsReceivedNote":
                    InvPurchaseService invPurchaseService = new InvPurchaseService();
                    return invPurchaseService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmPurchaseReturnNote":
                    InvPurchaseService invPurchaseServiceRe = new InvPurchaseService();
                    return invPurchaseServiceRe.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmInvoice":
                    InvSalesServices invSalesServices = new InvSalesServices();
                    return invSalesServices.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmSalesReturnNote":
                    InvSalesServices invSalesServicesRe = new InvSalesServices();
                    return invSalesServicesRe.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmQuotation":
                    InvQuotationService invQuotationService = new InvQuotationService();
                    return invQuotationService.GetSelectionData(reportDatStruct, autoGenerateInfo);
                
                case "FrmDispatchNote":
                    InvSalesServices invSalesServicesDis = new InvSalesServices();
                    return invSalesServicesDis.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "RptLocationSales":
                    SalesOrderService salesOrderService = new SalesOrderService();
                    return salesOrderService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmTransferOfGoodsNote":
                    InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                    return invTransferOfGoodsService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmStockAdjustment":
                    InvStockAdjustmentService invStockAdjustmentService = new InvStockAdjustmentService();
                    return invStockAdjustmentService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmSampleOut":
                    SampleOutService sampleOutService = new SampleOutService();
                    return sampleOutService.GetSelectionData(reportDatStruct, autoGenerateInfo, 1);

                case "FrmSampleIn":
                    SampleInService sampleInService = new SampleInService();
                    return sampleInService.GetSelectionData(reportDatStruct, autoGenerateInfo, 1);

                case "RptPriceChange":
                    InvProductPriceChangeService invProductPriceChangeService = new InvProductPriceChangeService();
                    return invProductPriceChangeService.GetSelectionDataLC(reportDatStruct);

                
                #endregion

                #region purchasing reports
                case "RptPurchaseRegister":               
                    InvPurchaseService invPurchaseRegistryService = new InvPurchaseService();
                    return invPurchaseRegistryService.GetSelectionData(reportDatStruct, autoGenerateInfo);
                case "RptPurchaseReturnRegister":
                    InvPurchaseService invPurchaseReturnRegistryService = new InvPurchaseService();
                    return invPurchaseReturnRegistryService.GetSelectionData(reportDatStruct, autoGenerateInfo);
                case "RptProductWisePurchase":
                    InvPurchaseService invProductPurchaseService = new InvPurchaseService();
                    return invProductPurchaseService.GetSelectionPurchaseDataSummary(reportDatStruct);
                case "RptSupplierWisePurchase":
                    InvPurchaseService invSupplierPurchaseService = new InvPurchaseService();
                    return invSupplierPurchaseService.GetSelectionData(reportDatStruct, autoGenerateInfo);
                case "RptSupplierWisePerformanceAnalysis":
                    invSupplierPurchaseService = new InvPurchaseService();
                    return invSupplierPurchaseService.GetSelectionData(reportDatStruct, autoGenerateInfo);
                case "RptPendingPurchaseOrders":
                     InvPurchaseOrderService invPurchaseOrderServicePendig = new InvPurchaseOrderService();
                     return invPurchaseOrderServicePendig.GetSelectionData(reportDatStruct, autoGenerateInfo);

                #endregion

                #region sales reports
                case "RptProductWiseSales":
                    //InvSalesServices invProductSalesService = new InvSalesServices();
                    //return invProductSalesService.GetSelectionSalesDataSummary(reportDatStruct);

                case "RptSupplierWiseSales":
                    //InvSalesServices invSupplierSalesService = new InvSalesServices();
                    //return invSupplierSalesService.GetSelectionSalesDataSummary(reportDatStruct);

                case "RptSalesRegister":
                case "RptExtendedPropertySalesRegister":
                    InvSalesServices invSalesServiceSalesRegister = new InvSalesServices();
                    return invSalesServiceSalesRegister.GetSelectionSalesDataSummary(reportDatStruct);


                case "SalesOrderDetail":
                    salesOrderService = new SalesOrderService();
                    return salesOrderService.GetSelectionData(reportDatStruct, autoGenerateInfo);


                //case "RptLocationSales":
                //    InvSalesServices invSalesServiceSalesRegister = new InvSalesServices();
                //    return invSalesServiceSalesRegister.GetSelectionSalesDataSummary(reportDatStruct);

                case "RptProductPriceChangeDetail":
                    InvProductPriceChangeService invProductPriceChangeDeatilService = new InvProductPriceChangeService();
                    return invProductPriceChangeDeatilService.GetSelectionDataSummary(reportDatStruct);

                case "RptProductPriceChangeDamageDetail":
                    InvProductPriceChangeDamageService invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
                    return invProductPriceChangeDamageService.GetSelectionDataSummary(reportDatStruct);
                #endregion

                #region Staff Sales
                case "RptStaffCreditSales":
                    PosSalesSummeryService posSalesSummeryService = new PosSalesSummeryService();
                    return posSalesSummeryService.GetSelectionDataStaffSalesCredit(reportDatStruct,2);

                case "RptStaffSalaryAdvance":
                    posSalesSummeryService = new PosSalesSummeryService();
                    return posSalesSummeryService.GetSelectionDataStaffSalesCredit(reportDatStruct,3);

                case "RptStaffCashCrediCard":
                    posSalesSummeryService = new PosSalesSummeryService();
                    return posSalesSummeryService.GetSelectionDataStaffSalesCredit(reportDatStruct,1);

                #endregion

                #region stock reports
                case "RptExcessStockAdjustment":
                    InvStockAdjustmentService invStockExcessAdjustmentService = new InvStockAdjustmentService();
                    return invStockExcessAdjustmentService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "RptShortageStockAdjustment":
                    InvStockAdjustmentService invStockShortageAdjustmentService = new InvStockAdjustmentService();
                    return invStockShortageAdjustmentService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "RptStockAdjustmentPercentage":
                    InvStockAdjustmentService invStockAdjustmentPercentageService = new InvStockAdjustmentService();
                    return invStockAdjustmentPercentageService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "RptBinCard":
                    InvReportService invReportService = new InvReportService();
                    return invReportService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "InvRptOpeningBalanceRegister":
                    OpeningStockService openingStockService = new OpeningStockService();
                    return openingStockService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "InvRptStockBalance":
                    InvProductStockMasterService invProductStockMasterService = new InvProductStockMasterService();
                    return invProductStockMasterService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "InvRptBatchStockBalance":
                    InvBatchNoExpiaryDetailService invBatchNoExpiaryDetailService = new InvBatchNoExpiaryDetailService();
                    return invBatchNoExpiaryDetailService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "InvRptTransferRegister":
                    InvTransferOfGoodsService invTransferOfGoodsService1 = new InvTransferOfGoodsService();
                    return invTransferOfGoodsService1.GetSelectionData(reportDatStruct, autoGenerateInfo);

                #endregion

                #region inventory references

                #region Department
                case "FrmDepartment":
                    bool isDependCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend;
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    return invDepartmentService.GetSelectionData(isDependCategory, reportDatStruct);
                #endregion
                #region Category
                case "FrmCategory":
                    InvCategoryService invCategoryService = new InvCategoryService();
                    return invCategoryService.GetSelectionData(reportDatStruct);
                #endregion
                #region Sub Category
                case "FrmSubCategory":
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    return invSubCategoryService.GetSelectionData(reportDatStruct);
                #endregion
                #region Sub Category 2
                case "FrmSubCategory2":
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    return invSubCategory2Service.GetSelectionData(reportDatStruct);
                #endregion
                #region Product
                case "FrmProduct":
                    InvProductMasterService invProductMasterServiceP = new InvProductMasterService();
                    return invProductMasterServiceP.GetSelectionData(reportDatStruct);
                #endregion
                #region Supplier
                case "FrmSupplier":
                    SupplierService supplierService = new SupplierService();
                    return supplierService.GetSelectionData(reportDatStruct);
                #endregion
                #region SalesPerson
                case "FrmSalesPerson":
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    return invSalesPersonService.GetSelectionData(reportDatStruct,autoGenerateInfo);
                #endregion
                #region SalesPerson
                case "FrmEmployee":
                    EmployeeService employeeService = new EmployeeService();
                    return employeeService.GetSelectionData(reportDatStruct, autoGenerateInfo);
                #endregion


                #endregion

                default:
                    return null;
            }
        }

        public DataTable GetResultData(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            switch (autoGenerateInfo.FormName)
            {
                #region inventory transactions

                
                case "FrmPurchaseOrder":
                    InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                    return invPurchaseOrderService.GetPurchaseOrdersDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);

                case "FrmGoodsReceivedNote":
                    InvPurchaseService invPurchaseService = new InvPurchaseService();
                    return invPurchaseService.GetPurchaseDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);

                case "FrmPurchaseReturnNote":
                    InvPurchaseService invPurchaseServiceRe = new InvPurchaseService();
                    return invPurchaseServiceRe.GetPurchaseReturnDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);

                case "FrmInvoice":
                    InvSalesServices invSalesServices = new InvSalesServices();
                    return invSalesServices.GetInvoicesDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);
                
                case "FrmQuotation":
                    InvQuotationService invQuotationService = new InvQuotationService();
                    return invQuotationService.GetQuotationsDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);

                case "RptLocationSales":
                    SalesOrderService salesOrderService = new SalesOrderService();
                    return salesOrderService.GetSalesOrderDtlDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);
 
                case "FrmTransferOfGoodsNote":
                    InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                    return invTransferOfGoodsService.GetTransferOfGoodsDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);
                    
                case "FrmStockAdjustment":
                    InvStockAdjustmentService invStockAdjustmentService = new InvStockAdjustmentService();
                    return invStockAdjustmentService.GetStockAdjustmentDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);

                case "FrmSampleOut":
                    SampleOutService sampleOutService = new SampleOutService();
                    return sampleOutService.GetSamleOutDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo, 1);

                case "FrmSampleIn":
                    SampleInService sampleInService = new SampleInService();
                    return sampleInService.GetSamleInDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo, 1);

                case "RptPriceChange":
                      InvProductPriceChangeService invProductPriceChangeService = new InvProductPriceChangeService();
                      return invProductPriceChangeService.GetPriceChangeDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);


                case "FrmOpeningStock":
                      OpeningStockService openingStockService = new OpeningStockService();
                      return openingStockService.GetOpeningStockDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);
                
                #endregion

                #region purchasing reports
               
                case "RptPurchaseRegister":
                    InvPurchaseService invPurchaseRegistryService = new InvPurchaseService();
                    return invPurchaseRegistryService.GetPurchaseRegisterDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);
                case "RptPurchaseReturnRegister":
                    InvPurchaseService invPurchaseReturnRegistryService = new InvPurchaseService();
                    return invPurchaseReturnRegistryService.GetPurchaseReturnRegistryDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);
                case "RptProductWisePurchase":
                    InvPurchaseService invProductPurchaseService = new InvPurchaseService();
                    return invProductPurchaseService.GetPurchaseDetailsSummaryDataTable(reportConditionsDataStructList, reportDataStructList);               
                case "RptSupplierWisePurchase":
                    InvPurchaseService invSupplierPurchaseService = new InvPurchaseService();
                    return invSupplierPurchaseService.GetSupplierPurchaseDetailsDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptSupplierWisePerformanceAnalysis":
                    invSupplierPurchaseService = new InvPurchaseService();
                    return invSupplierPurchaseService.GetSupplierPurchaseDetailsDataTable(reportConditionsDataStructList, reportDataStructList);
  
                case "RptPendingPurchaseOrders":
                    InvPurchaseOrderService invPurchaseOrderServicePendigPo = new InvPurchaseOrderService();
                    return invPurchaseOrderServicePendigPo.GetPendingPurchaseOrdersDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);
                
                #endregion

                #region sales reports

                case "RptSalesRegister":
                    InvSalesServices invSalesServicesSalesRegister = new InvSalesServices();
                    return invSalesServicesSalesRegister.GetSalesRegisterDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);
                case "SalesOrderDetail":
                    salesOrderService = new SalesOrderService();
                    return salesOrderService.GetSalesOrderDtlDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);
                //case "RptLocationSales":
                //    InvSalesServices invSalesServicesLocationSales = new InvSalesServices();
                //    return invSalesServicesLocationSales.GetLocationSalesDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);
                case "RptProductPriceChangeDetail":
                    InvProductPriceChangeService invProductPriceChangeDetailService = new InvProductPriceChangeService();
                    return invProductPriceChangeDetailService.GetPriceChangeDetailDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);
                case "RptProductPriceChangeDamageDetail":
                    InvProductPriceChangeDamageService invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
                    return invProductPriceChangeDamageService.GetPriceChangeDamageDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);

                #endregion

                #region Staff Sales
                case "RptStaffCreditSales":
                    PosSalesSummeryService posSalesSummeryService = new PosSalesSummeryService();
                    return posSalesSummeryService.GetStaffPurchaseCreditSalesDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo,2);
                case "RptStaffSalaryAdvance":
                    posSalesSummeryService = new PosSalesSummeryService();
                    return posSalesSummeryService.GetStaffPurchaseCreditSalesDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo,3);
                case "RptStaffCashCrediCard":
                    posSalesSummeryService = new PosSalesSummeryService();
                    return posSalesSummeryService.GetStaffPurchaseCreditSalesDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo,1);

                    
      
                #endregion

                #region stock reports
                case "RptExcessStockAdjustment":
                    InvStockAdjustmentService invStockExcessAdjustmentService = new InvStockAdjustmentService();
                    return invStockExcessAdjustmentService.GetStockAdjustmentDetailsDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);
                case "RptShortageStockAdjustment":
                    InvStockAdjustmentService invStockShortageAdjustmentService = new InvStockAdjustmentService();
                    return invStockShortageAdjustmentService.GetStockAdjustmentDetailsDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);
                case "RptStockAdjustmentPercentage":
                    InvStockAdjustmentService invStockAdjustmentPercentageService = new InvStockAdjustmentService();
                    return invStockAdjustmentPercentageService.GetStockAdjustmentDetailsDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);
                case "RptBinCard":
                    InvReportService invReportService = new InvReportService();
                    return invReportService.GetBinCardDetailsDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);
                case "InvRptOpeningBalanceRegister":
                    OpeningStockService openingStockServiceReg = new OpeningStockService();
                    return openingStockServiceReg.GetOpeningBalancesRegisterDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);
                case "InvRptStockBalance":
                    InvProductStockMasterService invProductStockMasterService = new InvProductStockMasterService();
                    return invProductStockMasterService.GetStockBalancesDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, autoGenerateInfo);
                case "InvRptBatchStockBalance":
                    InvBatchNoExpiaryDetailService invBatchNoExpiaryDetailService = new InvBatchNoExpiaryDetailService();
                    return invBatchNoExpiaryDetailService.GetBatchStockBalancesDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);
                case "InvRptTransferRegister":
                    InvTransferOfGoodsService invTransferOfGoodsServicer = new InvTransferOfGoodsService();
                    return invTransferOfGoodsServicer.GetProductTransferRegisterDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);
            
                #endregion

                #region inventory references
                #region Department
                case "FrmDepartment":
                    bool isDependCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend;

                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    return invDepartmentService.GetAllInvDepartmentDataTable(isDependCategory, reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);

                    break;
                #endregion
                #region Category
                case "FrmCategory":
                    if (autoGenerateInfo.IsDepend)
                    {
                        InvCategoryService invCategoryService = new InvCategoryService();
                        return invCategoryService.GetAllDepartmentWiseCategoryDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList); 
                    }
                    else
                    {
                        InvCategoryService invCategoryService = new InvCategoryService();
                        return invCategoryService.GetAllCategoryDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList); 
                    }
                    break;
                #endregion
                #region Sub Category
                case "FrmSubCategory":
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    if (autoGenerateInfo.IsDepend)
                    {
                        return invSubCategoryService.GetAllCategoryWiseSubCategoryDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList); 
                    }
                    else
                    {
                        return invSubCategoryService.GetAllSubCategoryDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList); 
                    }
                    break;
                #endregion
                #region Sub Category 2
                case "FrmSubCategory2":
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    if (autoGenerateInfo.IsDepend)
                    {
                        return invSubCategory2Service.GetAllSubCategoryWiseSub2CategoryDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                    }
                    else
                    {
                        return invSubCategory2Service.GetAllSub2CategoryDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                    }
                    break;
                #endregion
                #region Product
                case "FrmProduct":
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    return invProductMasterService.GetAllProductDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList);
                  
                #endregion
                #region Supplier
                case "FrmSupplier":
                    SupplierService supplierService = new SupplierService();
                    return supplierService.GetAllSupplierDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                #endregion
                #region SalesPerson
                case "FrmSalesPerson":
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    return invSalesPersonService.GetAllInvSalesPersonsDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                #endregion
                #region SalesPerson
                case "FrmEmployee":
                    EmployeeService employeeService = new EmployeeService();
                    return employeeService.GetAllEmployeeDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                #endregion
                #endregion

                default:
                    return null;
            }
        }

        public string GetConditionValue(Common.ReportDataStruct reportDataStruct, string dataValue)
        {
            string conditionValue = string.Empty;
            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "SupplierID":
                    SupplierService supplierService = new SupplierService();
                    conditionValue = supplierService.GetSupplierByName(dataValue.Trim()).SupplierName.ToString();
                    break;
                case "LookupKey":
                    LookUpReferenceService lookUpReferenceTitleTypeService = new LookUpReferenceService();
                    string titleType = ((int)LookUpReference.TitleType).ToString();
                    conditionValue = lookUpReferenceTitleTypeService.GetLookUpReferenceByKey(titleType, int.Parse(dataValue.Trim())).LookupKey.ToString();
                    break;
                case "PaymentMethodID":
                    PaymentMethodService paymentMethodService = new PaymentMethodService();
                    if (string.Equals(reportDataStruct.DbJoinColumnName, "PaymentMethodName"))
                    {conditionValue =paymentMethodService.GetPaymentMethodsByName(dataValue.Trim()).PaymentMethodName.ToString();}
                    else
                    {conditionValue = paymentMethodService.GetPaymentMethodsByName(dataValue.Trim()).PaymentMethodID.ToString();}
                    break;

                case "SupplierGroupID":
                    SupplierGroupService supplierGroupService = new SupplierGroupService();
                    if (string.Equals(reportDataStruct.DbJoinColumnName, "SupplierGroupName"))
                    { conditionValue = supplierGroupService.GetSupplierGroupsByName(dataValue.Trim()).SupplierGroupName.ToString(); }
                    else if (string.Equals(reportDataStruct.DbJoinColumnName, "SupplierGroupCode"))
                    { conditionValue = supplierGroupService.GetSupplierGroupsByName(dataValue.Trim()).SupplierGroupCode.ToString(); }
                    else
                    { conditionValue = supplierGroupService.GetSupplierGroupsByCode(dataValue.Trim()).SupplierGroupID.ToString(); }
                    break;
                //case "ProductID":
                //    InvProductMasterService invProductMasterService = new InvProductMasterService();
                //    conditionValue = invProductMasterService.GetProductDetailsByID(dataValue.Trim()).InvProductMasterID.ToString();
                //    break;
                case "CustomerID":
                    CustomerService customerService = new CustomerService();
                    conditionValue = customerService.GetCustomersByCode(dataValue.Trim()).CustomerID.ToString();
                    break;
                case "InvSalesPersonID":
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    conditionValue = invSalesPersonService.GetInvSalesPersonByCode(dataValue.Trim()).InvSalesPersonID.ToString();
                    break;
                case "ProductID":
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    conditionValue = invProductMasterService.GetProductsByCode(dataValue.Trim()).InvProductMasterID.ToString();
                    break;
                case "InvProductMasterID":
                    InvProductMasterService invProductMasterServicePm = new InvProductMasterService();
                    if (string.Equals(reportDataStruct.DbJoinColumnName, "ProductCode"))
                    {
                        conditionValue = invProductMasterServicePm.GetProductsByCode(dataValue.Trim()).ProductCode.ToString();
                    }
                    if (string.Equals(reportDataStruct.DbJoinColumnName, "ProductName"))
                    {
                        conditionValue = invProductMasterServicePm.GetProductsByName(dataValue.Trim()).ProductName.ToString();
                    }
                    
                    break;
                case "InvPurchaseHeaderID":
                    AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                    autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote");
                    InvPurchaseService invPurchaseService = new InvPurchaseService();
                    conditionValue = invPurchaseService.GetInvPurchaseHeaderByDocumentNo(autoGenerateInfo.DocumentID, dataValue.Trim(), Common.LoggedLocationID).InvPurchaseHeaderID.ToString();
                    break;
                case "InvDepartmentID":
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    bool isDepend = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").IsDepend;
                    if (!reportDataStruct.IsConditionNameJoined)
                    {conditionValue = invDepartmentService.GetInvDepartmentsByCode(dataValue.Trim(), isDepend).InvDepartmentID.ToString();}
                    else
                    { conditionValue = invDepartmentService.GetInvDepartmentsByName(dataValue.Trim(), isDepend).DepartmentName.ToString(); }
                    
                    break;
                case "DepartmentID":
                    InvDepartmentService invProductDepartmentService = new InvDepartmentService();
                    bool isDependProduct = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").IsDepend;
                    if (!reportDataStruct.IsConditionNameJoined)
                    {conditionValue = invProductDepartmentService.GetInvDepartmentsByCode(dataValue.Trim(), isDependProduct).InvDepartmentID.ToString();}
                    else
                    { conditionValue = invProductDepartmentService.GetInvDepartmentsByName(dataValue.Trim(), isDependProduct).DepartmentName.ToString(); }
                    break;
                case "InvCategoryID":
                    InvCategoryService invCategoryService = new InvCategoryService();
                    bool isDependDepartment = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend;
                    if (!reportDataStruct.IsConditionNameJoined)
                    {conditionValue = invCategoryService.GetInvCategoryByCode(dataValue.Trim(), isDependDepartment).InvCategoryID.ToString();}
                    else
                    { conditionValue = invCategoryService.GetInvCategoryByName(dataValue.Trim(), isDependDepartment).CategoryName.ToString(); }
                    break;
                case "CategoryID":
                    InvCategoryService invProductCategoryService = new InvCategoryService();
                    bool isDependProductDepartment = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend;
                    if (!reportDataStruct.IsConditionNameJoined)
                    {conditionValue = invProductCategoryService.GetInvCategoryByCode(dataValue.Trim(), isDependProductDepartment).InvCategoryID.ToString();}
                    else
                    { conditionValue = invProductCategoryService.GetInvCategoryByName(dataValue.Trim(), isDependProductDepartment).CategoryName.ToString(); }
                    break;
                case "InvSubCategoryID":
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    bool isDependCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend;
                    if (!reportDataStruct.IsConditionNameJoined)
                    {conditionValue = invSubCategoryService.GetInvSubCategoryByCode(dataValue.Trim(), isDependCategory).InvCategoryID.ToString();}
                    else
                    {conditionValue = invSubCategoryService.GetInvSubCategoryByName(dataValue.Trim(), isDependCategory).SubCategoryName.ToString();}
                    break;
                case "SubCategoryID":
                    InvSubCategoryService invProductSubCategoryService = new InvSubCategoryService();
                    bool isDependProductCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend;
                    if (!reportDataStruct.IsConditionNameJoined)
                    {conditionValue = invProductSubCategoryService.GetInvSubCategoryByCode(dataValue.Trim(), isDependProductCategory).InvCategoryID.ToString();}
                    else
                    { conditionValue = invProductSubCategoryService.GetInvSubCategoryByName(dataValue.Trim(), isDependProductCategory).SubCategoryName.ToString(); }
                    break;
                case "InvSubCategory2ID":
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    if (!reportDataStruct.IsConditionNameJoined)
                    { conditionValue = invSubCategory2Service.GetInvSubCategory2ByCode(dataValue.Trim()).InvSubCategoryID.ToString(); }
                    else
                    { conditionValue = invSubCategory2Service.GetInvSubCategory2ByName(dataValue.Trim()).SubCategory2Name.ToString(); }
                    break;
                case "SubCategory2ID":
                    InvSubCategory2Service invProductSubCategory2Service = new InvSubCategory2Service();
                    if (!reportDataStruct.IsConditionNameJoined)
                    { conditionValue = invProductSubCategory2Service.GetInvSubCategory2ByCode(dataValue.Trim()).InvSubCategoryID.ToString(); }
                    else
                    { conditionValue = invProductSubCategory2Service.GetInvSubCategory2ByName(dataValue.Trim()).SubCategory2Name.ToString(); }
                    break;
                case "LocationID":
                case "ToLocationID":
                    LocationService locationService = new LocationService();
                    conditionValue = locationService.GetLocationsByName(dataValue.Trim()).LocationName.ToString();
                    break;
                case "LocationId":
                    locationService = new LocationService();
                    if (!reportDataStruct.IsConditionNameJoined)
                    { conditionValue = locationService.GetLocationsByName(dataValue.Trim()).LocationID.ToString(); }
                    else
                    { conditionValue = locationService.GetLocationsByName(dataValue.Trim()).LocationName.ToString(); }
                    break;
                case "UnitOfMeasureID":
                    UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                    if (!reportDataStruct.IsConditionNameJoined)
                    { conditionValue = unitOfMeasureService.GetUnitOfMeasureByName(dataValue.Trim()).UnitOfMeasureID.ToString(); }
                    else
                    { conditionValue = unitOfMeasureService.GetUnitOfMeasureByName(dataValue.Trim()).UnitOfMeasureName.ToString(); }
                    break;
                case "StockAdjustmentMode":
                    LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                    conditionValue = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.StockAdjustmentMode).ToString(), dataValue.Trim()).LookupKey.ToString();
                    break;
                case "TransferTypeID":
                    InvTransferTypeService invTransferTypeService = new InvTransferTypeService();
                    conditionValue = invTransferTypeService.GetTransferTypesByName(dataValue.Trim()).TransferType.Trim();
                    break;
                case "EmployeeId":
                    EmployeeService employeeService = new EmployeeService();
                   // conditionValue = employeeService.GetEmployeesByName(dataValue.Trim()).EmployeeID.ToString();
                    if (!reportDataStruct.IsConditionNameJoined)
                    { conditionValue = employeeService.GetEmployeesByName(dataValue.Trim()).EmployeeID.ToString(); }
                    else
                    { conditionValue = employeeService.GetEmployeesByCode(dataValue.Trim()).EmployeeID.ToString(); }
                    break;
                case "OpeningStockType":
                    LookUpReferenceService lookUpReferenceService2 = new LookUpReferenceService();
                    string titleType2 = ((int)LookUpReference.ModuleType).ToString();
                    conditionValue = lookUpReferenceService2.GetLookUpReferenceByValue(titleType2, dataValue.Trim()).LookupValue.ToString();
                    break;
                case "PriceBatchNo":
                    conditionValue = dataValue.ToString();
                    break;
                case "LocationName":
                    locationService = new LocationService();
                    if (!reportDataStruct.IsConditionNameJoined)
                    { conditionValue = locationService.GetLocationsByName(dataValue.Trim()).LocationID.ToString(); }
                    else
                    { conditionValue = locationService.GetLocationsByName(dataValue.Trim()).LocationID.ToString(); }
                    break;
                default:
                    break;
            }
        
            return conditionValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtData"></param>
        /// <returns></returns>
        private DataTable SetDateFormat(DataTable dtData)
        {
            DataTable dtNewData = dtData.Clone();
            dtNewData.Columns[7].DataType = typeof(string);

            foreach (DataRow row in dtData.Rows)
            { dtNewData.ImportRow(row); }

            foreach (DataRow row in dtNewData.Rows)
            {
                DateTime dt = DateTime.Parse(row[7].ToString());
                row[7] = dt.ToShortDateString();
            }            

            return dtNewData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportData"></param>
        /// <returns></returns>
        private DataTable SetReportDataTableHeadersForReport(DataTable reportData)
        {

            int stringColIndex = 0, decimalColIndex = 0;
            string stringColumnName = "FieldString";
            string decimalColumnName = "FieldDecimal";

            foreach (DataColumn col in reportData.Columns)
            {
                // Reset all column headers before arrange them according to report fields  
                if (col.ColumnName.Contains(stringColumnName))
                {
                    col.ColumnName = stringColumnName + (stringColIndex + 1 + "S").ToString();
                    stringColIndex++;
                }

                if (col.ColumnName.Contains(decimalColumnName))
                {
                    col.ColumnName = decimalColumnName + (decimalColIndex + 1 + "D").ToString();
                    decimalColIndex++;
                }
            }

            // Rearrange column headers for the report
            foreach (DataColumn col in reportData.Columns)
            {
                if (col.ColumnName.Contains(stringColumnName))
                {
                    col.ColumnName = col.ColumnName.Remove(col.ColumnName.Length - 1, 1);
                }

                if (col.ColumnName.Contains(decimalColumnName))
                {
                    col.ColumnName = col.ColumnName.Remove(col.ColumnName.Length - 1, 1);
                }
            }

            return reportData;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="reportData"></param>
       /// <param name="reportDataStructList"></param>
       /// <returns></returns>
        private ReportSources SetReportSourcesForReport(DataTable reportData, List<Common.ReportDataStruct> reportDataStructList)
        {
            ReportSources reportSources = new ReportSources();
            ArrayList originalSumFields = new ArrayList();
            ArrayList newIndexesList = new ArrayList();

            int stringColIndex = 0, decimalColIndex = 0;
            string stringColumnName = "FieldString";
            string decimalColumnName = "FieldDecimal";
            
            originalSumFields.AddRange(reportDataStructList.Where(d => d.ReportDataType.Equals(typeof(decimal)) && d.IsColumnTotal.Equals(true)).Select(d => d.ReportField).ToList());

            foreach (DataColumn col in reportData.Columns)
            {
                // Reset all column headers before arrange them according to report fields  
                if (col.ColumnName.Contains(stringColumnName))
                {
                    col.ColumnName = stringColumnName + (stringColIndex + 1 + "S").ToString();
                    stringColIndex++;
                }

                if (col.ColumnName.Contains(decimalColumnName))
                {
                    if (originalSumFields.Contains(col.ColumnName))
                    { 
                        newIndexesList.Add(decimalColIndex+1); 
                    }
                    col.ColumnName = decimalColumnName + (decimalColIndex + 1 + "D").ToString();
                    decimalColIndex++;
                }
            }
            
            // Rearrange column headers for the report
            foreach (DataColumn col in reportData.Columns)
            {
                if (col.ColumnName.Contains(stringColumnName))
                {
                    col.ColumnName = col.ColumnName.Remove(col.ColumnName.Length - 1, 1);
                }

                if (col.ColumnName.Contains(decimalColumnName))
                {
                    col.ColumnName = col.ColumnName.Remove(col.ColumnName.Length - 1, 1);
                }
            }

            reportSources.reportData = reportData;
            reportData = null;
            reportSources.newSumFieldsIndexes = newIndexesList;
            return reportSources;
        }

        private Common.ReportDataStruct GetSelectedReportDataStruct(List<Common.ReportDataStruct> reportDatStructList, string selectedRepoertFieldName)
        {
            Common.ReportDataStruct reportDataStruct = new Common.ReportDataStruct();

            foreach (Common.ReportDataStruct reportDataStructItem in reportDatStructList)
            {
                if (reportDataStructItem.ReportFieldName.Trim().Equals(selectedRepoertFieldName.Trim()))
                {
                    reportDataStruct = reportDataStructItem;
                    return reportDataStruct;
                }
            }

            return reportDataStruct;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invRptGroupedDetails"></param>
        /// <param name="dtReportData"></param>
        /// <param name="dtReportConditions"></param>
        /// <param name="reportDataStructList"></param>
        /// <param name="autoGenerateInfo"></param>
        /// <returns></returns>
        private InvRptGroupedDetailsTemplate ViewGroupedReport(InvRptGroupedDetailsTemplate invRptGroupedDetails, DataTable dtReportData, DataTable dtReportConditions, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            DataTable dtArrangedReportData = new DataTable();
            
            #region Set Values for report header fields
            strFieldName = string.Empty;
            int sr = 1, dc = 11;

            foreach (var col in dtReportData.Columns)
            {
                if (reportDataStructList.Any(c => c.ReportField.Equals(col.ToString())))
                {
                    Common.ReportDataStruct reportDataStruct = reportDataStructList.Where(c => c.ReportField.Equals(col.ToString())).FirstOrDefault();
                    if (reportDataStruct.ReportDataType.Equals(typeof(string)))
                    {
                        strFieldName = "st" + sr;
                        invRptGroupedDetails.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStruct.ReportFieldName.Trim() + "'";
                        sr++;
                        groupingFields = string.IsNullOrEmpty(groupingFields) ? (reportDataStruct.ReportFieldName.Trim()) : (groupingFields + "/ " + reportDataStruct.ReportFieldName.Trim());
                    }

                    if (reportDataStruct.ReportDataType.Equals(typeof(decimal)))
                    {
                        strFieldName = "st" + dc;
                        invRptGroupedDetails.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStruct.ReportFieldName.Trim() + "'";
                        dc++;
                    }
                }
            }

            #endregion

            // Re arrange data table header columns for report
            dtArrangedReportData = dtReportData.Copy();
            dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

            invRptGroupedDetails.SetDataSource(dtArrangedReportData);
            invRptGroupedDetails.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
            invRptGroupedDetails.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
            invRptGroupedDetails.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            invRptGroupedDetails.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            invRptGroupedDetails.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            invRptGroupedDetails.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            invRptGroupedDetails.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
            invRptGroupedDetails.DataDefinition.FormulaFields["GroupNames"].Text = "'" + (string.IsNullOrEmpty(groupingFields) ? "" : groupingFields) + "'";

            #region Group By

            // Set report group field values
            for (int i = 0; i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count(); i++)
            {
                if (i < invRptGroupedDetails.DataDefinition.Groups.Count)
                {
                    invRptGroupedDetails.DataDefinition.Groups[i].ConditionField = invRptGroupedDetails.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                }
            }

            // Set parameter select field values
            for (int i = 0; i < invRptGroupedDetails.DataDefinition.Groups.Count; i++)
            {
                if (invRptGroupedDetails.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                {
                    if (i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count())
                    {
                        invRptGroupedDetails.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), true);
                    }
                    else
                    {
                        invRptGroupedDetails.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                    }
                }
            }

            #endregion

            return invRptGroupedDetails;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invRptDetailsTemplate"></param>
        /// <param name="dtReportData"></param>
        /// <param name="dtReportConditions"></param>
        /// <param name="reportDataStructList"></param>
        /// <param name="autoGenerateInfo"></param>
        /// <returns></returns>
        private InvRptDetailsTemplate ViewUnGroupedReport(InvRptDetailsTemplate invRptDetailsTemplate, DataTable dtReportData, DataTable dtReportConditions, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            DataTable dtArrangedReportData = new DataTable();
            #region Set Values for report header fields
            strFieldName = string.Empty;
            int sr = 1, dc = 11;

            foreach (var item in reportDataStructList)
            {
                if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                {

                    if (item.ReportDataType.Equals(typeof(string)))
                    {
                        strFieldName = "st" + sr;
                        invRptDetailsTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                        sr++;
                    }

                    if (item.ReportDataType.Equals(typeof(decimal)))
                    {
                        strFieldName = "st" + dc;
                        invRptDetailsTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                        dc++;
                    }
                }
            }

            #endregion

            // Re arrange data table header columns for report
            dtArrangedReportData = dtReportData.Copy();
            dtReportData = null;
            ReportSources reportSources = new ReportSources();
            reportSources = SetReportSourcesForReport(dtArrangedReportData, reportDataStructList);
            dtArrangedReportData = reportSources.reportData;
            ArrayList newSumFieldsIndexesList = reportSources.newSumFieldsIndexes;

            invRptDetailsTemplate.SetDataSource(dtArrangedReportData);
            invRptDetailsTemplate.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
            invRptDetailsTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
            invRptDetailsTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            invRptDetailsTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            invRptDetailsTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            invRptDetailsTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            invRptDetailsTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            // Set parameter sum fields
            for (int i = 0; i < invRptDetailsTemplate.ParameterFields.Count; i++)
            {
                if (invRptDetailsTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter) && newSumFieldsIndexesList.Contains(i + 1) && invRptDetailsTemplate.ParameterFields[i].Name.StartsWith("prmSumFieldDecimal"))
                {
                    invRptDetailsTemplate.SetParameterValue("prmSumFieldDecimal" + (i + 1).ToString(), true);
                }
                else
                {
                    invRptDetailsTemplate.SetParameterValue("prmSumFieldDecimal" + (i + 1).ToString(), false);
                }
            }

            return invRptDetailsTemplate;

        }
    }
    
}
