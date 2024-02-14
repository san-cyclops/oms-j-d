using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using Domain;
using Report.Logistic.Reference.Reports;
using Utility;
using Service;
using Report.Logistic.Transactions.Reports;
using Report.Com.Transactions.Reports;
using CrystalDecisions.Shared;

namespace Report.Logistic
{
    /// <summary>
    /// 
    /// </summary>
    class ReportSources
    {
        public DataTable reportData { get; set; }
        public ArrayList newSumFieldsIndexes { get; set; }
    }

    public class LgsReportGenerator
    {
        string strFieldName = string.Empty;
        string groupingFields = string.Empty;

        public void GenearateReport(string documentNo, string reportName, bool isOrg, string[] stringField, DataTable reportData)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            Cursor.Current = Cursors.WaitCursor;

            LgsRptReferenceDetailTemplate lgsRptReferenceDetailTemplate = new LgsRptReferenceDetailTemplate();            
            // Bind report data
            lgsRptReferenceDetailTemplate.SetDataSource(reportData);

            // Assign formula and summery field values
            lgsRptReferenceDetailTemplate.SummaryInfo.ReportTitle = reportName;
            lgsRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
            lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

            //  Set field text
            int x = 1;
            foreach (string strFieldName in stringField)
            {
                string st = "st" + x.ToString();
                lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields["st" + x.ToString()].Text = "'" + strFieldName.Trim() + "'";
                x++;
            }

            reportViewer.crRptViewer.ReportSource = lgsRptReferenceDetailTemplate;
            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
        }

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

            LgsRptColumn5Template lgsRptColumn5Template = new LgsRptColumn5Template();
            
            string departmentText = "", categoryText = "", subCategoryText = "", subCategory2Text = "";
            LgsRptReferenceDetailTemplate lgsRptReferenceDetailTemplate = new LgsRptReferenceDetailTemplate();

            string[] stringField = new string[] { };

            switch (autoGenerateInfo.FormName)
            {
                #region Supplier
                case "FrmLogisticSupplier": // Supplier
                    SupplierService supplierService = new SupplierService();
                    reportData = supplierService.GetAllSupplierDataTable();

                    // Set field text
                    stringField = new string[] { "Code", "Sup.Name", "Address", "Telephone", "NIC", "Rec.Date", "Payment Method", "Contact Person", "Remark" };

                    lgsRptReferenceDetailTemplate.SetDataSource(reportData);
                    // Assign formula and summery field values
                    lgsRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptReferenceDetailTemplate;
                    break;
                #endregion
                #region Department
                case "FrmLogisticDepartment":
                    LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                    departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticDepartment").FormText;
                    categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").FormText;
                    
                    reportData = lgsDepartmentService.GetAllLgsDepartmentDataTable();
                    stringField = new string[] { "Code", departmentText, "Remark" };
                    
                    lgsRptColumn5Template.SetDataSource(reportData);
                    // Assign formula and summery field values
                    lgsRptColumn5Template.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptColumn5Template.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptColumn5Template.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptColumn5Template.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptColumn5Template;
                    break;
                #endregion
                #region Category
                case "FrmLogisticCategory":
                    LgsCategoryService lgsCategoryService = new LgsCategoryService();
                    departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticDepartment").FormText;
                    categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").FormText;

                    if (autoGenerateInfo.IsDepend)
                    {
                        reportData = lgsCategoryService.GetAllLgsDepartmentWiseCategoryDataTable();
                        stringField = new string[] { "Code", categoryText, departmentText, "Remark" };
                    }
                    else
                    {
                        reportData = lgsCategoryService.GetAllLgsCategoryDataTable();
                        stringField = new string[] { "Code", categoryText, "Remark" };
                    }

                    lgsRptColumn5Template.SetDataSource(reportData);
                    // Assign formula and summery field values
                    lgsRptColumn5Template.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptColumn5Template.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptColumn5Template.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptColumn5Template.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptColumn5Template;
                    break;
                #endregion
                #region Sub Category
                case "FrmLogisticSubCategory":
                    LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
                    categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").FormText;
                    subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").FormText;

                    if (autoGenerateInfo.IsDepend)
                    {
                        reportData = lgsSubCategoryService.GetAllLgsCategoryWiseSubCategoryDataTable();
                        stringField = new string[] { "Code", subCategoryText, categoryText, "Remark" };
                    }
                    else
                    {
                        reportData = lgsSubCategoryService.GetAllLgsSubCategoryDataTable();
                        stringField = new string[] { "Code", subCategoryText, "Remark" };
                    }

                    lgsRptColumn5Template.SetDataSource(reportData);
                    // Assign formula and summery field values
                    lgsRptColumn5Template.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptColumn5Template.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptColumn5Template.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptColumn5Template.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptColumn5Template;
                    break;
                #endregion
                #region Sub Category 2
                case "FrmLogisticSubCategory2":
                    LgsSubCategory2Service lgsSubCategory2Service = new LgsSubCategory2Service();
                    subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").FormText;
                    subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory2").FormText;

                    if (autoGenerateInfo.IsDepend)
                    {
                        reportData = lgsSubCategory2Service.GetAllLgsSubCategoryWiseSub2CategoryDataTable();
                        stringField = new string[] { "Code", subCategory2Text, subCategoryText, "Remark" };
                    }
                    else
                    {
                        reportData = lgsSubCategory2Service.GetAllLgsSub2CategoryDataTable();
                        stringField = new string[] { "Code", subCategory2Text, "Remark" };
                    }

                    lgsRptColumn5Template.SetDataSource(reportData);
                    // Assign formula and summery field values
                    lgsRptColumn5Template.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptColumn5Template.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptColumn5Template.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptColumn5Template.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptColumn5Template;
                    break;
                #endregion
                #region Product
                //{ "Code", "Product Name", "Name on Invoice", departmentText, categoryText, subCategoryText, subCategory2Text, "Bar Code", "Re-order Qty", "Cost Price", "Selling Price", "Fixed Discount", "Batch Status", "Remark" };
                //case "FrmProduct":
                //    InvProductMasterService invProductMasterService = new InvProductMasterService();
                //    CryLgsProductTemplate cryLgsProductTemplate = new CryLgsProductTemplate();
                //    departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticDepartment").FormText;
                //    categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").FormText;
                //    subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").FormText;
                //    subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory2").FormText;

                //    reportData = invProductMasterService.GetAllProductDataTable();
                //    stringField = new string[] { "Code", "Product Name", "Name on Invoice", departmentText, categoryText, subCategoryText, subCategory2Text, "Bar Code", "Re-order Qty", "Cost Price", "Selling Price", "Fixed Discount", "Batch Status", "Promotion Status" };

                //    cryLgsProductTemplate.SetDataSource(reportData);
                //    // Assign formula and summery field values
                //    cryLgsProductTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                //    cryLgsProductTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                //    cryLgsProductTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                //    cryLgsProductTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                //    cryLgsProductTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                //    cryLgsProductTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                //    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                //    for (int i = 0; i < stringField.Length; i++)
                //    {
                //        string st = "st" + (i + 1);
                //        cryLgsProductTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                //    }
                //    reportViewer.crRptViewer.ReportSource = cryLgsProductTemplate;
                //    break;
                #endregion
                default:
                    break;
            }

            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="autoGenerateInfo"></param>
        public void GenearateReferenceSummaryReport(AutoGenerateInfo autoGenerateInfo, DataTable dtReportData, DataTable dtReportConditions, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> groupByStructList)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            LgsRptReferenceDetailTemplate lgsRptReferenceDetailTemplate = new LgsRptReferenceDetailTemplate();
            LgsRptColumn5Template lgsRptColumn5Template = new LgsRptColumn5Template();
            Cursor.Current = Cursors.WaitCursor;

            DataTable dtArrangedReportData = new DataTable();
            List<Common.ReportDataStruct> selectedReportStructList;
            List<Common.ReportDataStruct> selectedGroupStructList;
            StringBuilder sbGroupedTitle = new StringBuilder();

            string strFieldName = string.Empty;
            int sr = 1, dc = 12;
            int gr = 0, gp = 1;

            switch (autoGenerateInfo.FormName)
            {
                #region Supplier
                case "FrmLogisticSupplier":
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
                                lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
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

                    lgsRptReferenceDetailTemplate.SetDataSource(dtArrangedReportData);
                    //lgsRptReferenceDetailTemplate.SetDataSource(dtReportData);
                    lgsRptReferenceDetailTemplate.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    lgsRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    lgsRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    #region Header - Set
                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    if (reportDataStructList[i].IsSelectionField.Equals(true))
                    //    {
                    //        strFieldName = "st" + (i + 1);
                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //        {
                    //            lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            lgsRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
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
                    //            lgsRptReferenceDetailTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //            lgsRptReferenceDetailTemplate.SetParameterValue(i, "");
                    //    }
                    //    else
                    //    { lgsRptReferenceDetailTemplate.SetParameterValue(i, ""); }
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
                            if (gr < lgsRptReferenceDetailTemplate.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    lgsRptReferenceDetailTemplate.DataDefinition.Groups[gr].ConditionField = lgsRptReferenceDetailTemplate.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp - 1 < lgsRptReferenceDetailTemplate.DataDefinition.Groups.Count)
                            {
                                if (lgsRptReferenceDetailTemplate.ParameterFields[gp - 1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        lgsRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        lgsRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < lgsRptReferenceDetailTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (lgsRptReferenceDetailTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                lgsRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < lgsRptReferenceDetailTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (lgsRptReferenceDetailTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                lgsRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = lgsRptReferenceDetailTemplate;
                    break;
                #endregion
                #region Department
                case "FrmLogisticDepartment":
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
                                lgsRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
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

                    lgsRptColumn5Template.SetDataSource(dtArrangedReportData);
                    //lgsRptColumn5Template.SetDataSource(dtReportData);
                    lgsRptColumn5Template.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    lgsRptColumn5Template.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    lgsRptColumn5Template.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptColumn5Template.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

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
                    //            lgsRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            lgsRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
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
                    //            lgsRptColumn5Template.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //            lgsRptColumn5Template.SetParameterValue(i, "");
                    //    }
                    //    else
                    //    { lgsRptColumn5Template.SetParameterValue(i, ""); }
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
                            if (gr < lgsRptColumn5Template.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    lgsRptColumn5Template.DataDefinition.Groups[gr].ConditionField = lgsRptColumn5Template.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp - 1 < lgsRptColumn5Template.DataDefinition.Groups.Count)
                            {
                                if (lgsRptColumn5Template.ParameterFields[gp - 1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        lgsRptColumn5Template.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        lgsRptColumn5Template.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < lgsRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (lgsRptColumn5Template.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                lgsRptColumn5Template.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < lgsRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (lgsRptColumn5Template.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                lgsRptColumn5Template.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = lgsRptColumn5Template;
                    break;
                #endregion
                #region Category
                case "FrmLogisticCategory":
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
                                lgsRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
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

                    lgsRptColumn5Template.SetDataSource(dtArrangedReportData);
                    //lgsRptColumn5Template.SetDataSource(dtReportData);
                    lgsRptColumn5Template.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    lgsRptColumn5Template.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    lgsRptColumn5Template.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptColumn5Template.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

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
                    //            lgsRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            lgsRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
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
                    //            lgsRptColumn5Template.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //            lgsRptColumn5Template.SetParameterValue(i, "");
                    //    }
                    //    else
                    //    { lgsRptColumn5Template.SetParameterValue(i, ""); }
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
                            if (gr < lgsRptColumn5Template.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    lgsRptColumn5Template.DataDefinition.Groups[gr].ConditionField = lgsRptColumn5Template.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp - 1 < lgsRptColumn5Template.DataDefinition.Groups.Count)
                            {
                                if (lgsRptColumn5Template.ParameterFields[gp - 1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        lgsRptColumn5Template.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        lgsRptColumn5Template.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < lgsRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (lgsRptColumn5Template.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                lgsRptColumn5Template.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < lgsRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (lgsRptColumn5Template.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                lgsRptColumn5Template.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = lgsRptColumn5Template;
                    break;
                #endregion
                #region Sub Category
                case "FrmLogisticSubCategory":
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
                                lgsRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
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

                    lgsRptColumn5Template.SetDataSource(dtArrangedReportData);
                    //lgsRptColumn5Template.SetDataSource(dtReportData);
                    lgsRptColumn5Template.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    lgsRptColumn5Template.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    lgsRptColumn5Template.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptColumn5Template.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

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
                    //            lgsRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            lgsRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
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
                    //            lgsRptColumn5Template.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //            lgsRptColumn5Template.SetParameterValue(i, "");
                    //    }
                    //    else
                    //    { lgsRptColumn5Template.SetParameterValue(i, ""); }
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
                            if (gr < lgsRptColumn5Template.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    lgsRptColumn5Template.DataDefinition.Groups[gr].ConditionField = lgsRptColumn5Template.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp - 1 < lgsRptColumn5Template.DataDefinition.Groups.Count)
                            {
                                if (lgsRptColumn5Template.ParameterFields[gp - 1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        lgsRptColumn5Template.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        lgsRptColumn5Template.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < lgsRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (lgsRptColumn5Template.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                lgsRptColumn5Template.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < lgsRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (lgsRptColumn5Template.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                lgsRptColumn5Template.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = lgsRptColumn5Template;
                    break;
                #endregion
                #region Sub Category 2
                case "FrmLogisticSubCategory2":
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
                                lgsRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
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

                    lgsRptColumn5Template.SetDataSource(dtArrangedReportData);
                    //lgsRptColumn5Template.SetDataSource(dtReportData);
                    lgsRptColumn5Template.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    lgsRptColumn5Template.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    lgsRptColumn5Template.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptColumn5Template.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptColumn5Template.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

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
                    //            lgsRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            lgsRptColumn5Template.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
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
                    //            lgsRptColumn5Template.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //            lgsRptColumn5Template.SetParameterValue(i, "");
                    //    }
                    //    else
                    //    { lgsRptColumn5Template.SetParameterValue(i, ""); }
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
                            if (gr < lgsRptColumn5Template.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    lgsRptColumn5Template.DataDefinition.Groups[gr].ConditionField = lgsRptColumn5Template.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp - 1 < lgsRptColumn5Template.DataDefinition.Groups.Count)
                            {
                                if (lgsRptColumn5Template.ParameterFields[gp - 1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        lgsRptColumn5Template.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        lgsRptColumn5Template.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < lgsRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (lgsRptColumn5Template.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                lgsRptColumn5Template.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < lgsRptColumn5Template.DataDefinition.Groups.Count; i++)
                        {
                            if (lgsRptColumn5Template.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                lgsRptColumn5Template.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = lgsRptColumn5Template;
                    break;
                #endregion
                #region Product
                case "FrmLogisticProduct":
                    LgsRptProductTemplate lgsRptProductTemplate = new LgsRptProductTemplate();
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
                                    lgsRptProductTemplate.DataDefinition.FormulaFields[strFieldName].Text = "''";
                                    //sbGroupedTitle.Append(string.Concat(item.ReportFieldName.Trim(), ", "));
                                    groupingFields = string.IsNullOrEmpty(groupingFields) ? (item.ReportFieldName.Trim()) : (groupingFields + ", " + item.ReportFieldName.Trim());
                                }
                                else
                                {lgsRptProductTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";}
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

                    //if (groupByStructList.Count > 0)
                    //{
                    //    sbGroupedTitle.Remove(sbGroupedTitle.Length - 2, 2); // remove last ','
                    //    sbGroupedTitle.Append(" wise");
                    //}
                    //else { sbGroupedTitle.Append(" "); }
                    #endregion

                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    lgsRptProductTemplate.SetDataSource(dtArrangedReportData);
                    //lgsRptProductTemplate.SetDataSource(dtReportData);
                    lgsRptProductTemplate.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    lgsRptProductTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    lgsRptProductTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptProductTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptProductTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptProductTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptProductTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //lgsRptProductTemplate.DataDefinition.FormulaFields["GroupTitle"].Text = "'" + sbGroupedTitle + "'";
                    lgsRptProductTemplate.DataDefinition.FormulaFields["GroupNames"].Text = "'" + (string.IsNullOrEmpty(groupingFields) ? "" : groupingFields + " wise") + "'";

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
                    //            lgsRptProductTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            lgsRptProductTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        strFieldName = "st" + (i + 1);
                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //        {
                    //            lgsRptProductTemplate.DataDefinition.FormulaFields[strFieldName].Text = "''";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            lgsRptProductTemplate.DataDefinition.FormulaFields[strFieldName].Text = "''";
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
                    //            lgsRptProductTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //            lgsRptProductTemplate.SetParameterValue(i, "");
                    //    }
                    //    else
                    //    { lgsRptProductTemplate.SetParameterValue(i, ""); }
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
                            if (gr < lgsRptProductTemplate.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    lgsRptProductTemplate.DataDefinition.Groups[gr].ConditionField = lgsRptProductTemplate.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp - 1 < lgsRptProductTemplate.DataDefinition.Groups.Count)
                            {
                                if (lgsRptProductTemplate.ParameterFields[gp - 1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        lgsRptProductTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        lgsRptProductTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < lgsRptProductTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (lgsRptProductTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                lgsRptProductTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < lgsRptProductTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (lgsRptProductTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                lgsRptProductTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = lgsRptProductTemplate;
                    break;
                #endregion
                default:
                    break;
            }

            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Organize Report Generator Fields
        /// </summary>
        public FrmReprotGenerator OrganizeFormFields(AutoGenerateInfo autoGenerateInfo)
        {
            List<Common.ReportDataStruct> reportDatStructList = new List<Common.ReportDataStruct>();
            string departmentText = "", categoryText = "", subCategoryText = "", subCategory2Text = "";
            FrmReprotGenerator frmReprotGenerator;
            departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticDepartment").FormText;
            categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").FormText;
            subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").FormText;
            subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory2").FormText;
                    
            switch (autoGenerateInfo.FormName)
            {
                #region Logistic Reference
                #region Supplier
                case "FrmLogisticSupplier":
                    //string[] stringfield = { "Code", "Sup.Name", "Address", "Telephone", "NIC", "Rec.Date", "Payment Method", "Contact Person", "Remark" };

                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Code", ReportDataType = typeof(string), DbColumnName = "SupplierCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Sup.Name", ReportDataType = typeof(string), DbColumnName = "SupplierName", ValueDataType = typeof(String), IsSelectionField = true, IsConditionField = true, IsJoinField = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Address", ReportDataType = typeof(string), DbColumnName = "BillingAddress1", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Telephone", ReportDataType = typeof(string), DbColumnName = "BillingTelephone", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true,IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "NIC", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Rec.Date", ReportDataType = typeof(string), DbColumnName = "CreatedDate", ValueDataType = typeof(DateTime), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Payment Method", ReportDataType = typeof(string), DbColumnName = "PaymentMethodID", DbJoinColumnName = "PaymentMethodName", ValueDataType = typeof(int), IsConditionNameJoined = true, IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Contact Person", ReportDataType = typeof(string), DbColumnName = "ContactPersonName", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,10,0,4);
                    return frmReprotGenerator;
                #endregion
                #region Department
                case "FrmLogisticDepartment":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Code", ReportDataType = typeof(string), DbColumnName = "DepartmentCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = string.Concat(departmentText, " Name"), ReportDataType = typeof(string), DbColumnName = "DepartmentName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,5,0,4);
                    return frmReprotGenerator;
               
                    
                #endregion
                #region Category
                case "FrmLogisticCategory":
                    //departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
                    //categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
                    if (autoGenerateInfo.IsDepend)
                    {
                        reportDatStructList = new List<Common.ReportDataStruct>();
                      //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = string.Concat(categoryText, " Code"), ReportDataType = typeof(string), DbColumnName = "CategoryCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = " Code", ReportDataType = typeof(string), DbColumnName = "CategoryCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = string.Concat(categoryText, " Name"), ReportDataType = typeof(string), DbColumnName = "CategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = string.Concat(departmentText, " Name"), ReportDataType = typeof(string), DbColumnName = "LgsDepartmentID", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                        frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 5,0,4);
                        return frmReprotGenerator;
                    }
                    else
                    {
                        reportDatStructList = new List<Common.ReportDataStruct>();
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Code", ReportDataType = typeof(string), DbColumnName = "CategoryCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = string.Concat(categoryText, " Name"), ReportDataType = typeof(string), DbColumnName = "CategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                        frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,5,0,4);
                        return frmReprotGenerator;
                    }
                #endregion
                #region Sub Category
                case "FrmLogisticSubCategory":
                    categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").FormText;
                    subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").FormText;
                    if (autoGenerateInfo.IsDepend)
                    {
                        reportDatStructList = new List<Common.ReportDataStruct>();
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Code", ReportDataType = typeof(string), DbColumnName = "SubCategoryCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = string.Concat(subCategoryText, " Name"), ReportDataType = typeof(string), DbColumnName = "SubCategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = string.Concat(categoryText, " Name"), ReportDataType = typeof(string), DbColumnName = "LgsCategoryID", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                        frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,5,0,4);
                        return frmReprotGenerator;
                    }
                    else
                    {
                        reportDatStructList = new List<Common.ReportDataStruct>();
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Code", ReportDataType = typeof(string), DbColumnName = "SubCategoryCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = string.Concat(subCategoryText, " Name"), ReportDataType = typeof(string), DbColumnName = "SubCategoryName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                        frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,5,0,4);
                        return frmReprotGenerator;
                    }
                #endregion
                #region Sub Category 2
                case "FrmLogisticSubCategory2":
                    if (autoGenerateInfo.IsDepend)
                    {
                        reportDatStructList = new List<Common.ReportDataStruct>();
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Code", ReportDataType = typeof(string), DbColumnName = "SubCategory2Code", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = string.Concat(subCategory2Text, " Name"), ReportDataType = typeof(string), DbColumnName = "SubCategory2Name", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = string.Concat(subCategoryText, " Name"), ReportDataType = typeof(string), DbColumnName = "LgsSubCategoryID", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                        frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,5,0,4);
                        return frmReprotGenerator;
                    }
                    else
                    {
                        reportDatStructList = new List<Common.ReportDataStruct>();
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Code", ReportDataType = typeof(string), DbColumnName = "SubCategory2Code", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = string.Concat(subCategory2Text, " Name"), ReportDataType = typeof(string), DbColumnName = "SubCategory2Name", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                        reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                        frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList , 5, 0,4);
                        return frmReprotGenerator;
                    }
                #endregion
                #region Product
                case "FrmLogisticProduct":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = string.Concat(departmentText, " Name"), ReportDataType = typeof(string), DbColumnName = "LgsDepartmentID", DbJoinColumnName = "DepartmentName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = string.Concat(categoryText, " Name"), ReportDataType = typeof(string), DbColumnName = "LgsCategoryID", DbJoinColumnName = "CategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = string.Concat(subCategoryText, " Name"), ReportDataType = typeof(string), DbColumnName = "LgsSubCategoryID", DbJoinColumnName = "SubCategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = string.Concat(subCategory2Text, " Name"), ReportDataType = typeof(string), DbColumnName = "LgsSubCategory2ID", DbJoinColumnName = "SubCategory2Name", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Code", ReportDataType = typeof(string), DbColumnName = "ProductCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsRecordFilterByGivenOption = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Name on Invoice", ReportDataType = typeof(string), DbColumnName = "NameOnInvoice", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Bar Code", ReportDataType = typeof(string), DbColumnName = "BarCode", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Re-order Qty", ReportDataType = typeof(string), DbColumnName = "ReOrderQty", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Cost Price", ReportDataType = typeof(string), DbColumnName = "CostPrice", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Selling Price", ReportDataType = typeof(string), DbColumnName = "SellingPrice", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = "Fixed Discount", ReportDataType = typeof(string), DbColumnName = "FixedDiscount", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString13", ReportFieldName = "Batch Status", ReportDataType = typeof(string), DbColumnName = "IsBatch", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString14", ReportFieldName = "Promotion Status", ReportDataType = typeof(string), DbColumnName = "IsPromotion", ValueDataType = typeof(string), IsSelectionField = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 12,0,4);
                    return frmReprotGenerator;
                #endregion
                #endregion

                #region Logistic transactions

                #region FrmLogisticPurchaseOrder
                case "FrmLogisticPurchaseOrder":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Supplier", ReportDataType = typeof(string), DbColumnName = "LgsSupplierID", DbJoinColumnName = "SupplierName", ValueDataType = typeof(long), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Reference", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "LgsPurchaseHeaderID", DbJoinColumnName = "Qty", ValueDataType = typeof(decimal), IsJoinField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Disc. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "NBT(2%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount1", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "NBT(2.04%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount2", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "VAT", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount3", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                  
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                #region FrmLogisticGoodsReceivedNote
                case "FrmLogisticGoodsReceivedNote":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Supplier", ReportDataType = typeof(string), DbColumnName = "SupplierID", DbJoinColumnName = "SupplierName", ValueDataType = typeof(long), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Supp. Inv.", ReportDataType = typeof(string), DbColumnName = "SupplierInvoiceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Reference", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "P.O.", ReportDataType = typeof(string), DbColumnName = "ReferenceDocumentID", DbJoinColumnName = "DocumentNo", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "A/C", ReportDataType = typeof(string) , ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "LgsPurchaseHeaderID", DbJoinColumnName = "Qty", ValueDataType = typeof(decimal), IsJoinField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Disc. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "NBT(2%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount1", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "NBT(2.04%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount2", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "VAT", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount3", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                 
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                
                #endregion

                #region FrmLogisticPurchaseReturnNote
                case "FrmLogisticPurchaseReturnNote":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                     reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Supplier", ReportDataType = typeof(string), DbColumnName = "SupplierID", DbJoinColumnName = "SupplierName", ValueDataType = typeof(long), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Supp. Inv.", ReportDataType = typeof(string), DbColumnName = "SupplierInvoiceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Reference", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "G.R.N", ReportDataType = typeof(string), DbColumnName = "ReferenceDocumentID", DbJoinColumnName = "DocumentNo", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "A/C", ReportDataType = typeof(string) , ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "LgsPurchaseHeaderID", DbJoinColumnName = "Qty", ValueDataType = typeof(decimal), IsJoinField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Disc. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "NBT(2%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount1", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "NBT(2.04%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount2", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "VAT", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount3", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                  
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    #endregion

                #region FrmLogisticTransferOfGoodsNote 
                case "FrmLogisticTransferOfGoodsNote":
                    
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Transfer Type", ReportDataType = typeof(string), DbColumnName = "TransferTypeID", DbJoinColumnName = "TransferType", ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });                                       
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "From Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "To Location", ReportDataType = typeof(string), DbColumnName = "ToLocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Reference", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "InvTransferNoteHeaderID", DbJoinColumnName = "Qty", ValueDataType = typeof(decimal), IsJoinField = true, IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Net Amount", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    #endregion

                #region FrmLogisticStockAdjustment 
                case "FrmLogisticStockAdjustment":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Adjustment Mode", ReportDataType = typeof(string), DbColumnName = "StockAdjustmentMode", ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Ref. No", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    #endregion
                    
                #region FrmLogisticQuotation
                case "FrmLogisticQuotation":
                  
                    return null;
                #endregion

                #region FrmMaintenanceJobRequisitionNote
                case "FrmMaintenanceJobRequisitionNote":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Exp. Date", ReportDataType = typeof(string), DbColumnName = "ExpectedDate", ValueDataType = typeof(DateTime), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Ref. No", ReportDataType = typeof(string), DbColumnName = "ReferenceDocumentNo", ValueDataType = typeof(string), IsSelectionField = true });
                    
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    #endregion

                #region FrmMaintenanceJobAssignNote
                case "FrmMaintenanceJobAssignNote":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Dilivery Date", ReportDataType = typeof(string), DbColumnName = "DiliveryDate", ValueDataType = typeof(DateTime), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Ref. No", ReportDataType = typeof(string), DbColumnName = "ReferenceDocumentNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    #endregion

                #region FrmMaterialAllocationNote
                case "FrmMaterialAllocationNote":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Alloc. Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Ref. No", ReportDataType = typeof(string), DbColumnName = "ReferenceDocumentNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "LgsMaterialRequestHeaderID", DbJoinColumnName = "Qty", ValueDataType = typeof(decimal), IsJoinField = true, IsSelectionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    #endregion

                #region FrmMaterialRequestNote
                case "FrmMaterialRequestNote":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Req. Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Delivery Date", ReportDataType = typeof(string), DbColumnName = "DiliveryDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Reference", ReportDataType = typeof(string), DbColumnName = "ReferenceDocumentNo", ValueDataType = typeof(string), IsSelectionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "LgsMaterialRequestHeaderID", DbJoinColumnName = "OrderQty", ValueDataType = typeof(decimal), IsJoinField = true, IsSelectionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                #endregion

                #region stock reports

                #region Opening Stock
                case "LgsRptOpeningBalanceRegister":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "LgsDepartmentID", DbJoinColumnName = "DepartmentName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "LgsCategoryID", DbJoinColumnName = "CategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "LgsSubCategoryID", DbJoinColumnName = "SubCategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "LgsSubCategory2ID", DbJoinColumnName = "SubCategory2Name", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "LgsProductMasterID", DbJoinColumnName = "ProductCode", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "LgsProductMasterID", DbJoinColumnName = "ProductName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Opening Stock Type", ReportDataType = typeof(string), DbColumnName = "OpeningStockType", ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Opening Stock Type", ReportDataType = typeof(string), DbColumnName = "OpeningStockType", DbJoinColumnName = "LookupValue", ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Selling Value", ReportDataType = typeof(decimal), DbColumnName = "SellingValue", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

                #endregion
                #region stock balance
                case "LgsRptStockBalance":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "LgsDepartmentID", DbJoinColumnName = "DepartmentName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "LgsCategoryID", DbJoinColumnName = "CategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "LgsSubCategoryID", DbJoinColumnName = "SubCategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "LgsSubCategory2ID", DbJoinColumnName = "SubCategory2Name", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "LgsProductMasterID", DbJoinColumnName = "ProductCode", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "LgsProductMasterID", DbJoinColumnName = "ProductName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Stock", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Sale Price", ReportDataType = typeof(decimal), DbColumnName = "SellingPrice", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Cost Price", ReportDataType = typeof(decimal), DbColumnName = "CostPrice", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Selling Value", ReportDataType = typeof(decimal), DbColumnName = "", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Cost Value", ReportDataType = typeof(decimal), DbColumnName = "", ValueDataType = typeof(decimal), IsSelectionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

                #endregion
                #region LgsRptTransferRegister
                case "LgsRptTransferRegister":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Transfer Type", ReportDataType = typeof(string), DbColumnName = "TransferTypeID", DbJoinColumnName = "TransferType", ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "From Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "To Location", ReportDataType = typeof(string), DbColumnName = "ToLocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "LgsDepartmentID", DbJoinColumnName = "DepartmentName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "LgsCategoryID", DbJoinColumnName = "CategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "LgsSubCategoryID", DbJoinColumnName = "SubCategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "LgsSubCategory2ID", DbJoinColumnName = "SubCategory2Name", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "LgsProductMasterID", DbJoinColumnName = "ProductCode", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "LgsProductMasterID", DbJoinColumnName = "ProductName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
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

                default:
                    return null;
            }
        }


        /// <summary>
        /// Get selection data for Combo boxes
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSelectionData(Common.ReportDataStruct reportDatStruct, AutoGenerateInfo autoGenerateInfo)
        {
            switch (autoGenerateInfo.FormName)
            {
                #region Logistic Reference
                #region Supplier
                case "FrmLogisticSupplier":
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    return lgsSupplierService.GetSelectionData(reportDatStruct);
                #endregion
                #region Department
                case "FrmLogisticDepartment":
                    bool isDependCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").IsDepend;
                    LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                    return lgsDepartmentService.GetSelectionData(isDependCategory, reportDatStruct);
                #endregion
                #region Category
                case "FrmLogisticCategory":

                    LgsCategoryService lgsCategoryService = new LgsCategoryService();
                    return lgsCategoryService.GetSelectionData(reportDatStruct);
                #endregion
                #region Sub Category
                case "FrmLogisticSubCategory":
                    LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
                    return lgsSubCategoryService.GetSelectionData(reportDatStruct);
                #endregion
                #region Sub Category 2
                case "FrmLogisticSubCategory2":
                    LgsSubCategory2Service lgsSubCategory2Service = new LgsSubCategory2Service();
                    return lgsSubCategory2Service.GetSelectionData(reportDatStruct);
                #endregion
                #region Product
                case "FrmLogisticProduct":
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    return lgsProductMasterService.GetSelectionData(reportDatStruct);
                #endregion
                #endregion

                #region Logistic transactions

                case "FrmLogisticPurchaseOrder":
                    LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                    return lgsPurchaseOrderService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmLogisticGoodsReceivedNote":
                    LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                    return lgsPurchaseService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmLogisticPurchaseReturnNote":
                    LgsPurchaseService lgsPurchaseServiceRe = new LgsPurchaseService();
                    return lgsPurchaseServiceRe.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmLogisticTransferOfGoodsNote":
                    LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();
                    return lgsTransferOfGoodsService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmLogisticStockAdjustment":
                    LgsStockAdjustmentService lgsStockAdjustmentService = new LgsStockAdjustmentService();
                    return lgsStockAdjustmentService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmOpeningStock":
                    OpeningStockService openingStockService = new OpeningStockService();
                    return openingStockService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmMaintenanceJobRequisitionNote":
                    LgsMaintenanceJobRequisitionService lgsMaintenanceJobRequisitionService = new LgsMaintenanceJobRequisitionService();
                    return lgsMaintenanceJobRequisitionService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmMaintenanceJobAssignNote":
                    LgsMaintenanceJobAssignService lgsMaintenanceJobAssignService = new LgsMaintenanceJobAssignService();
                    return lgsMaintenanceJobAssignService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmMaterialAllocationNote":
                    LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();
                    return lgsMaterialAllocationService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmMaterialRequestNote":
                    LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();
                    return lgsMaterialRequestService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                #endregion

                #region stock reports
                case "LgsRptOpeningBalanceRegister":
                    OpeningStockService openingStockServicer = new OpeningStockService();
                    return openingStockServicer.GetSelectionData(reportDatStruct, autoGenerateInfo);
                
                case "LgsRptStockBalance":
                    LgsProductStockMasterService lgsProductStockMasterService = new LgsProductStockMasterService();
                    return lgsProductStockMasterService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "LgsRptTransferRegister":
                    LgsTransferOfGoodsService lgsTransferOfGoodsService1 = new LgsTransferOfGoodsService();
                    return lgsTransferOfGoodsService1.GetSelectionData(reportDatStruct, autoGenerateInfo);

                #endregion
                default:
                    return null;
            }
        }

        public DataTable GetResultData(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            switch (autoGenerateInfo.FormName)
            {
                #region Logistic Reference
                #region Supplier
                case "FrmLogisticSupplier":
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    return lgsSupplierService.GetAllLgsSupplierDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                #endregion
                #region Department
                case "FrmLogisticDepartment":
                    bool isDependCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").IsDepend;

                    LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                    return lgsDepartmentService.GetAllLgsDepartmentDataTable(isDependCategory, reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                    
                    break;
                #endregion
                #region Category
                case "FrmLogisticCategory":
                    if (autoGenerateInfo.IsDepend)
                    {
                        LgsCategoryService lgsCategoryService = new LgsCategoryService();
                        return lgsCategoryService.GetAllLgsDepartmentWiseCategoryDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                    }
                    else
                    {
                        LgsCategoryService lgsCategoryService = new LgsCategoryService();
                        return lgsCategoryService.GetAllLgsCategoryDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                    }
                    break;
                #endregion
                #region Sub Category
                case "FrmLogisticSubCategory":
                    LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
                    if (autoGenerateInfo.IsDepend)
                    {
                        return lgsSubCategoryService.GetAllLgsCategoryWiseSubCategoryDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                    }
                    else
                    {
                        return lgsSubCategoryService.GetAllLgsSubCategoryDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                    }
                    break;
                #endregion
                #region Sub Category 2
                case "FrmLogisticSubCategory2":
                    LgsSubCategory2Service lgsSubCategory2Service = new LgsSubCategory2Service();
                    if (autoGenerateInfo.IsDepend)
                    {
                        return lgsSubCategory2Service.GetAllLgsSubCategoryWiseSub2CategoryDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                    }
                    else
                    {
                        return lgsSubCategory2Service.GetAllLgsSub2CategoryDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                    }
                    break;
                #endregion
                #region Product
                case "FrmLogisticProduct":
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    return lgsProductMasterService.GetAllLgsProductDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                    break;
                #endregion
                #endregion

                #region Logistic transactions

                case "FrmLogisticPurchaseOrder":
                    LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                    return lgsPurchaseOrderService.GetPurchaseOrdersDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);

                case "FrmLogisticGoodsReceivedNote":
                    LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                    return lgsPurchaseService.GetPurchaseDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);

                case "FrmLogisticPurchaseReturnNote":
                    LgsPurchaseService lgsPurchaseServiceRe = new LgsPurchaseService();
                    return lgsPurchaseServiceRe.GetPurchaseReturnDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);

                case "FrmLogisticTransferOfGoodsNote":
                    LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();
                    return lgsTransferOfGoodsService.GetTransferOfGoodsDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);

                case "FrmLogisticStockAdjustment":
                    LgsStockAdjustmentService lgsStockAdjustmentService = new LgsStockAdjustmentService();
                    return lgsStockAdjustmentService.GetStockAdjustmentDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);
                
                case "FrmOpeningStock":
                    OpeningStockService openingStockService = new OpeningStockService();
                    return openingStockService.GetOpeningStockDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);

                case "FrmMaintenanceJobRequisitionNote":
                    LgsMaintenanceJobRequisitionService lgsMaintenanceJobRequisitionService = new LgsMaintenanceJobRequisitionService();
                    return lgsMaintenanceJobRequisitionService.GetMaintenanceJobRequisitionDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);

                case "FrmMaintenanceJobAssignNote":
                    LgsMaintenanceJobAssignService lgsMaintenanceJobAssignService = new LgsMaintenanceJobAssignService();
                    return lgsMaintenanceJobAssignService.GetMaintenanceJobAssignDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);

                case "FrmMaterialAllocationNote":
                    LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();
                    return lgsMaterialAllocationService.GetMaterialAllocationDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);

                case "FrmMaterialRequestNote":
                    LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();
                    return lgsMaterialRequestService.GetMaterialRequestDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);

                #endregion

                #region stock reports
                case "LgsRptOpeningBalanceRegister":
                    OpeningStockService openingStockServiceReg = new OpeningStockService();
                    return openingStockServiceReg.GetOpeningBalancesRegisterDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);

                case "LgsRptStockBalance":
                    LgsProductStockMasterService lgsProductStockMasterService = new LgsProductStockMasterService();
                    return lgsProductStockMasterService.GetStockBalancesDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);

                case "LgsRptTransferRegister":
                    LgsTransferOfGoodsService lgsTransferOfGoodsServicer = new LgsTransferOfGoodsService();
                    return lgsTransferOfGoodsServicer.GetProductTransferRegisterDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, autoGenerateInfo);
            
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
                case "LgsSupplierID":
                case "SupplierID":
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    conditionValue = lgsSupplierService.GetLgsSupplierByName(dataValue.Trim()).SupplierName.Trim();
                    break;
                case "ProductID":
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    conditionValue = lgsProductMasterService.GetProductsByCode(dataValue.Trim()).LgsProductMasterID.ToString();
                    break;
                case "LgsDepartmentID":
                    LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                    bool isDepend = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticDepartment").IsDepend;
                    if (!reportDataStruct.IsConditionNameJoined)
                    {conditionValue = lgsDepartmentService.GetLgsDepartmentsByCode(dataValue.Trim(), isDepend).LgsDepartmentID.ToString();}
                    else
                    { conditionValue = lgsDepartmentService.GetLgsDepartmentsByName(dataValue.Trim(), isDepend).DepartmentName.ToString(); }
                    break;
                case "DepartmentID":
                    LgsDepartmentService lgsProductDepartmentService = new LgsDepartmentService();
                    bool isDependProduct = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticDepartment").IsDepend;
                    if (!reportDataStruct.IsConditionNameJoined)
                    {conditionValue = lgsProductDepartmentService.GetLgsDepartmentsByCode(dataValue.Trim(), isDependProduct).LgsDepartmentID.ToString();}
                    else
                    { conditionValue = lgsProductDepartmentService.GetLgsDepartmentsByName(dataValue.Trim(), isDependProduct).DepartmentName.ToString(); }
                    break;
                case "LgsCategoryID":
                    LgsCategoryService lgsCategoryService = new LgsCategoryService();
                    bool isDependDepartment = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").IsDepend;
                    if (!reportDataStruct.IsConditionNameJoined)
                    {conditionValue = lgsCategoryService.GetLgsCategoryByCode(dataValue.Trim(), isDependDepartment).LgsCategoryID.ToString();}
                    else
                    { conditionValue = lgsCategoryService.GetLgsCategoryByName(dataValue.Trim(), isDependDepartment).CategoryName.ToString(); }
                    break;
                case "CategoryID":
                    LgsCategoryService lgsProductCategoryService = new LgsCategoryService();
                    bool isDependProductDepartment = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").IsDepend;
                    if (!reportDataStruct.IsConditionNameJoined)
                    {conditionValue = lgsProductCategoryService.GetLgsCategoryByCode(dataValue.Trim(), isDependProductDepartment).LgsCategoryID.ToString();}
                    else
                    {conditionValue = lgsProductCategoryService.GetLgsCategoryByName(dataValue.Trim(), isDependProductDepartment).CategoryName.ToString();}
                    break;
                case "LgsSubCategoryID":
                    LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
                    bool isDependCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").IsDepend;
                    if (!reportDataStruct.IsConditionNameJoined)
                    {conditionValue = lgsSubCategoryService.GetLgsSubCategoryByCode(dataValue.Trim(), isDependCategory).LgsCategoryID.ToString();}
                    else 
                    { conditionValue = lgsSubCategoryService.GetLgsSubCategoryByName(dataValue.Trim(), isDependCategory).SubCategoryName.ToString(); }
                    break;
                case "SubCategoryID":
                    LgsSubCategoryService lgsProductSubCategoryService = new LgsSubCategoryService();
                    bool isDependProductCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").IsDepend;
                    if (!reportDataStruct.IsConditionNameJoined)
                    {conditionValue = lgsProductSubCategoryService.GetLgsSubCategoryByCode(dataValue.Trim(), isDependProductCategory).LgsCategoryID.ToString();}
                    else 
                    { conditionValue = lgsProductSubCategoryService.GetLgsSubCategoryByName(dataValue.Trim(), isDependProductCategory).SubCategoryName.ToString(); }
                    break;
                case "LgsSubCategory2ID":
                    LgsSubCategory2Service lgsSubCategory2Service = new LgsSubCategory2Service();
                    if (!reportDataStruct.IsConditionNameJoined)
                    { conditionValue = lgsSubCategory2Service.GetLgsSubCategory2ByCode(dataValue.Trim()).LgsSubCategoryID.ToString(); }
                    else
                    { conditionValue = lgsSubCategory2Service.GetLgsSubCategory2ByName(dataValue.Trim()).SubCategory2Name.ToString(); }
                    break;
                case "PaymentMethodID":
                    PaymentMethodService paymentMethodService = new PaymentMethodService();
                    if (string.Equals(reportDataStruct.DbJoinColumnName, "PaymentMethodName"))
                    { conditionValue = paymentMethodService.GetPaymentMethodsByName(dataValue.Trim()).PaymentMethodName.ToString(); }
                    else
                    {conditionValue = paymentMethodService.GetPaymentMethodsByName(dataValue.Trim()).PaymentMethodID.ToString();}
                    break;
                case "LocationID":
                case "ToLocationID":
                    LocationService locationService = new LocationService();
                    conditionValue = locationService.GetLocationsByName(dataValue.Trim()).LocationName.ToString();
                    break;

                default:
                    break;
            }

            return conditionValue;
        }

        public void GenearateTransactionReport(AutoGenerateInfo autoGenerateInfo, string documentNo, int printStatus)
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

            switch (autoGenerateInfo.FormName)
            {

                #region Logistic transactions

                #region FrmLogisticPurchaseOrder
                case "FrmLogisticPurchaseOrder": // Purchase Order
                    LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                    reportData = lgsPurchaseOrderService.GetPurchaseOrderTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldPo = { "Document", "Date", "Remark", "Supplier", "Validity Period", "Reference No", "VAT No", "Supp. Adress", "Pmt. Expected Date", "Payment Terms", 
                                            "Expected Date", "Location", "Pro.Code", "Pro.Name", "Unit", "P.Size", "Or.Qty", "F.Qty", "C.Price", "Dis.%", "Dis.Amt.", 
                                            "Net Amt.", "Gross Amt.", "Dis.%", "Dis.Amt.", "NBT Amt", "VAT Amt", "Oth. Charges", "Net Amt.", "" };

                    LgsRptPOTransactionTemplate lgsRptTransactionTemplatePo = new LgsRptPOTransactionTemplate();                    
                    lgsRptTransactionTemplatePo.SetDataSource(reportData);
                    // Assign formula and summery field values
                    lgsRptTransactionTemplatePo.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransactionTemplatePo.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionTemplatePo.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionTemplatePo.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionTemplatePo.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionTemplatePo.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionTemplatePo.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldPo.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptTransactionTemplatePo.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldPo[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionTemplatePo;

                    break;
                #endregion

                #region FrmLogisticGoodsReceivedNote
                case "FrmLogisticGoodsReceivedNote":

                    LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                    DataSet dsReportDataGrn = new DataSet();
                    dsReportDataGrn = lgsPurchaseService.GetPurchaseTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldGrn = { "Document", "Date", "Remark", "Supplier", "P.O. Number", "Reference No", "VAT No", "Supp. Adress", "Supp. Invoice", "Payment Terms", "",
                                                  "Location", "Pro.Code", "Pro.Name", "Unit", "Or.Qty", "Qty", "F.Qty", "C.Price",
                                                  "Dis.%", "Dis.Amt.", "Net Amt.", "Gross Amt.", "Dis.%", "Dis.Amt.", "NBT Amt", "VAT Amt", "Oth. Charges", "Net Amt.", "" };

                    LgsRptGRNTransactionTemplate lgsRptTransactionTemplateGrn = new LgsRptGRNTransactionTemplate();
                    lgsRptTransactionTemplateGrn.SetDataSource(dsReportDataGrn.Tables[0]);                                       
                    // Assign formula and summery field values
                    lgsRptTransactionTemplateGrn.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransactionTemplateGrn.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionTemplateGrn.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionTemplateGrn.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionTemplateGrn.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionTemplateGrn.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionTemplateGrn.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    //  Set field text
                    for (int i = 0; i < stringFieldGrn.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptTransactionTemplateGrn.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldGrn[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionTemplateGrn;

                    // Get Balances report of reference PO
                    if (dsReportDataGrn.Tables.Count > 1)
                    {
                        if (dsReportDataGrn.Tables[1].Rows.Count > 0)
                        {
                            FrmReportViewer reportViewerPoBal = new FrmReportViewer();
                            string[] stringFieldPoBal = { "Document", "Date", "Supplier", "Remark", "P Req. No", "Validity Period", "", "", "Reference No", "Payment terms", 
                                            "Expected Date", "Location", "Pro.Code", "Pro.Name", "Unit", "B. Qty", "Or.Qty", "F.Qty", "C.Price", "Dis.%", "Dis.Amt.", 
                                            "Net Amt.", "Gross Amt.", "Dis.%", "Dis.Amt.", "Tax Amt.", "Oth. Charges", "Net Amt." };

                            LgsRptTransactionTemplate lgsRptTransactionTemplatePoBal = new LgsRptTransactionTemplate();
                            lgsRptTransactionTemplatePoBal.SetDataSource(dsReportDataGrn.Tables[1]);
                            // Assign formula and summery field values
                            lgsRptTransactionTemplatePoBal.SummaryInfo.ReportTitle = "Purchase Order Balances";
                            lgsRptTransactionTemplatePoBal.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                            lgsRptTransactionTemplatePoBal.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                            lgsRptTransactionTemplatePoBal.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                            lgsRptTransactionTemplatePoBal.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                            lgsRptTransactionTemplatePoBal.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                            lgsRptTransactionTemplatePoBal.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                            //  Set field text
                            for (int i = 0; i < stringFieldPoBal.Length; i++)
                            {
                                string st = "st" + (i + 1);
                                lgsRptTransactionTemplatePoBal.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldPoBal[i].Trim() + "'";
                            }

                            //Common.SetMenu(reportViewerPoBal, UI.Windows);
                            reportViewerPoBal.crRptViewer.ReportSource = lgsRptTransactionTemplatePoBal;
                            reportViewerPoBal.WindowState = FormWindowState.Maximized;
                            reportViewerPoBal.Show();
                        }
                    }
                    break;
                    #endregion 
                    
                #region FrmLogisticPurchaseReturnNote
                case "FrmLogisticPurchaseReturnNote":
                    LgsPurchaseService lgsPurchaseServiceRe = new LgsPurchaseService();
                    reportData = lgsPurchaseServiceRe.GetPurchaseReturnTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldPrn = { "Document", "Date", "Remark", "Supplier", "G.R.N No", "Reference No", "Return Type", "Supp. Adress", "Supp. Invoice", "", "", "Location", 
                                                "Pro.Code", "Pro.Name", "Unit", "", "Qty", "F.Qty", "C.Price", "Dis.%", "Dis.Amt.", "Net Amt.", "Gross Amt.",
                                                "Dis.%", "Dis.Amt.", "Tax Amt.", "Oth. Charges", "Net Amt." };

                    LgsRptPRNTransactionTemplate lgsRptTransactionTemplatePrn = new LgsRptPRNTransactionTemplate();
                    lgsRptTransactionTemplatePrn.SetDataSource(reportData);
                    // Assign formula and summery field values
                    lgsRptTransactionTemplatePrn.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransactionTemplatePrn.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionTemplatePrn.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionTemplatePrn.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionTemplatePrn.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionTemplatePrn.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionTemplatePrn.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    //  Set field text
                    for (int i = 0; i < stringFieldPrn.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptTransactionTemplatePrn.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldPrn[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionTemplatePrn;
                    break;
                #endregion

                #region FrmLogisticTransferOfGoodsNote
                case "FrmLogisticTransferOfGoodsNote":
                    LgsTransferOfGoodsService lgsTransferOfGoodsService = new LgsTransferOfGoodsService();
                    reportData = lgsTransferOfGoodsService.GetTransferofGoodsTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldTog = { "Document", "Date", "", "Remark", "", "", "", "", "Reference No", "", "From Location", "To Location",
                                                "Pro.Code", "Pro.Name", "Unit", "", "Qty", "", "C.Price", "", "", "Net Amt.", "Gross Amt.", "Net Amt.", "", "", "", "" };


                    LgsRptTOGTransactionTemplate lgsRptTransactionTemplateTog = new LgsRptTOGTransactionTemplate();
                    lgsRptTransactionTemplateTog.SetDataSource(reportData);
                    // Assign formula and summery field values
                    lgsRptTransactionTemplateTog.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransactionTemplateTog.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionTemplateTog.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionTemplateTog.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionTemplateTog.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionTemplateTog.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionTemplateTog.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    //  Set field text
                    for (int i = 0; i < stringFieldTog.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptTransactionTemplateTog.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldTog[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionTemplateTog;
                    break;
                #endregion

                #region FrmLogisticStockAdjustment
                
                case "FrmLogisticStockAdjustment":
                     LgsStockAdjustmentService lgsStockAdjustmentService = new LgsStockAdjustmentService();
                     reportData = lgsStockAdjustmentService.GetStockAdjustmentTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldSto = { "Document", "Date", "Narration", "Remark", "", "", "", "", "Reference No", "Adjustment Mode", 
                                            "", "Location", "Pro.Code", "Pro.Name", "Unit", "", "Qty", "S.Price", "C.Price", "", "S.Value", 
                                            "C.Value", "Tot. S.Value", "Tot. C.Value", "", "", "", "" };

                    LgsRptTransactionTemplate lgsRptTransactionTemplateSto = new LgsRptTransactionTemplate();
                    lgsRptTransactionTemplateSto.SetDataSource(reportData);
                    // Assign formula and summery field values
                    lgsRptTransactionTemplateSto.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransactionTemplateSto.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionTemplateSto.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionTemplateSto.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionTemplateSto.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionTemplateSto.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionTemplateSto.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldSto.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptTransactionTemplateSto.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldSto[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionTemplateSto;                    
                    break;

                #endregion

                #region FrmSampleOut
                case "FrmSampleOut":
                    SampleOutService sampleOutService = new SampleOutService();
                    reportData = sampleOutService.GetSampleOutTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID, 2);

                    string[] stringFieldSao = { "Document", "Date", "Issued To", "Dilivery Person", "Remark", "", "", "Reference No", "", "Sample Out Type", 
                                            "Location", "", "Pro.Code", "Pro.Name", "Unit", "", "Or.Qty", "", "C.Price", "", "", 
                                            "Net Amt.", "Net Amt.", "", "", "", "", "" };

                    LgsRptTransactionTemplate lgsRptTransactionTemplateSao = new LgsRptTransactionTemplate();
                    lgsRptTransactionTemplateSao.SetDataSource(reportData);
                    // Assign formula and summery field values
                    lgsRptTransactionTemplateSao.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransactionTemplateSao.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionTemplateSao.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionTemplateSao.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionTemplateSao.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionTemplateSao.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionTemplateSao.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldSao.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptTransactionTemplateSao.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldSao[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionTemplateSao;
                    break;
                #endregion

                #region FrmSampleIn
                case "FrmSampleIn":
                     SampleInService sampleInService = new SampleInService();
                     reportData = sampleInService.GetSampleInTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID, 2);

                    string[] stringFieldSai = { "Document", "Date", "Issued To", "Dilivery Person", "Remark", "", "", "Reference No", "", "Sample Out Type", 
                                            "Location", "", "Pro.Code", "Pro.Name", "Unit", "", "Or.Qty", "", "C.Price", "", "", 
                                            "Net Amt.", "Net Amt.", "", "", "", "", "" };

                    LgsRptTransactionTemplate lgsRptTransactionTemplateSai = new LgsRptTransactionTemplate();
                    lgsRptTransactionTemplateSai.SetDataSource(reportData);
                    // Assign formula and summery field values
                    lgsRptTransactionTemplateSai.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransactionTemplateSai.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionTemplateSai.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionTemplateSai.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionTemplateSai.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionTemplateSai.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionTemplateSai.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldSai.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptTransactionTemplateSai.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldSai[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionTemplateSai;

                    break;
                #endregion

                #region FrmOpeningStock
                case "FrmOpeningStock":
                     OpeningStockService openingStockService = new OpeningStockService();
                     reportData = openingStockService.GetOpeningStockTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID, 2);

                    string[] stringFieldOpst = { "Document", "Date", "Narration", "Remark", "", "", "", "", "Reference No", "Opening Stock Type", 
                                            "", "Location", "Pro.Code", "Pro.Name", "Unit", "Batch No", "Expiry Date", "Qty", "S.Price", "C.Price", "S.Value", 
                                            "C.Value", "Tot. S.Value", "Tot. C.Value", "", "", "", "" };

                    LgsRptOpeningStockSmallPaper lgsRptTransactionTemplateOpst = new LgsRptOpeningStockSmallPaper();
                    lgsRptTransactionTemplateOpst.SetDataSource(reportData);
                    // Assign formula and summery field values
                    lgsRptTransactionTemplateOpst.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransactionTemplateOpst.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionTemplateOpst.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionTemplateOpst.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionTemplateOpst.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionTemplateOpst.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionTemplateOpst.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldOpst.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptTransactionTemplateOpst.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldOpst[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionTemplateOpst;
                    break;
                #endregion

                #region FrmMaterialRequestNote
                case "FrmMaterialRequestNote":
                    LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();
                    reportData = lgsMaterialRequestService.GetMaterialRequestTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldMrn = { "Document", "Date", "Dilivery Date", "Remark", "Reference No", "", "", "", "", "", 
                                            "", "Location", "Pro.Code", "Pro.Name", "Unit", "", "", "", "", "", "", 
                                            "Req. Qty", "", "", "", "", "", "" };

                    LgsRptMRNTransactionTemplate lgsRptTransactionTemplateMrn = new LgsRptMRNTransactionTemplate();
                    lgsRptTransactionTemplateMrn.SetDataSource(reportData);
                    // Assign formula and summery field values
                    lgsRptTransactionTemplateMrn.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransactionTemplateMrn.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionTemplateMrn.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionTemplateMrn.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionTemplateMrn.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionTemplateMrn.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionTemplateMrn.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldMrn.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptTransactionTemplateMrn.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldMrn[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionTemplateMrn;
                    break;
                #endregion

                #region FrmMaterialAllocationNote
                case "FrmMaterialAllocationNote":
                    LgsMaterialAllocationService lgsMaterialAllocationService = new LgsMaterialAllocationService();
                    DataSet dsMANReportData = lgsMaterialAllocationService.GetMaterialAllocationTransactionData(documentNo, documentSataus, autoGenerateInfo.DocumentID);    
                
                    string[] stringFieldMan = { "Document", "Date", "Reference No", "Remark", "M.R.N. No", "M.R.N. Created By", "M.R.N. Date", "", "M.R.N. Location", "", 
                                            "", "Location", "Pro.Code", "Pro.Name", "Unit", "", "Qty", "Location", "", "Request", "C.Price", 
                                            "Amount", "", "", "", "", "", "" };

                    LgsRptMANTransactionTemplate lgsRptTransactionTemplateMan = new LgsRptMANTransactionTemplate();
                    lgsRptTransactionTemplateMan.SetDataSource(dsMANReportData.Tables[0]);
                    // Assign formula and summery field values
                    lgsRptTransactionTemplateMan.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransactionTemplateMan.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionTemplateMan.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionTemplateMan.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionTemplateMan.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionTemplateMan.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionTemplateMan.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldMan.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptTransactionTemplateMan.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldMan[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionTemplateMan;

                     #region MRN Balance

                    // Get Balances report of reference PO
                    if (dsMANReportData.Tables.Count > 1)
                    {
                        if (dsMANReportData.Tables[1].Rows.Count > 0)
                        {
                            FrmReportViewer reportViewerMRNBal = new FrmReportViewer();

                            string[] stringFieldMrnBal = { "Document", "Date", "Dilivery Date", "Remark", "Reference No", "", "", "", "", "", 
                                            "", "Location", "Pro.Code", "Pro.Name", "Unit", "", "", "", "", "", "Bal. Qty", 
                                            "Req. Qty", "", "", "", "", "", "" };

                            LgsRptMRNTransactionTemplate lgsRptTransactionTemplateMrnBal = new LgsRptMRNTransactionTemplate();
                            lgsRptTransactionTemplateMrnBal.SetDataSource(dsMANReportData.Tables[1]);
                            // Assign formula and summery field values
                            lgsRptTransactionTemplateMrnBal.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " - Balances";
                            lgsRptTransactionTemplateMrnBal.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                            lgsRptTransactionTemplateMrnBal.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                            lgsRptTransactionTemplateMrnBal.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                            lgsRptTransactionTemplateMrnBal.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                            lgsRptTransactionTemplateMrnBal.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                            lgsRptTransactionTemplateMrnBal.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                            for (int i = 0; i < stringFieldMrnBal.Length; i++)
                            {
                                string st = "st" + (i + 1);
                                lgsRptTransactionTemplateMrnBal.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldMrnBal[i].Trim() + "'";
                            }

                            reportViewerMRNBal.crRptViewer.ReportSource = lgsRptTransactionTemplateMrnBal;
                            reportViewerMRNBal.WindowState = FormWindowState.Maximized;
                            reportViewerMRNBal.Show();
                    
                        }
                    }

                    #endregion

                    break;
                #endregion

                #region FrmMaintenanceJobRequisitionNote
                case "FrmMaintenanceJobRequisitionNote":
                    LgsMaintenanceJobRequisitionService lgsMaintenanceJobRequisitionService = new LgsMaintenanceJobRequisitionService();
                    reportData = lgsMaintenanceJobRequisitionService.GetMaintenanceJobRequisitionTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldMjr = { "Document", "Date", "Exp. Datee", "", "Reference No", "", "", "", "", "", 
                                            "", "Location", "Job Desc.", "", "", "", "", "", "", "", "", 
                                            "", "", "", "", "", "", "" };

                    LgsRptTransactionTemplate1 lgsRptTransactionTemplateMjr = new LgsRptTransactionTemplate1();
                    lgsRptTransactionTemplateMjr.SetDataSource(reportData);
                    // Assign formula and summery field values
                    lgsRptTransactionTemplateMjr.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransactionTemplateMjr.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionTemplateMjr.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionTemplateMjr.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionTemplateMjr.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionTemplateMjr.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionTemplateMjr.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldMjr.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptTransactionTemplateMjr.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldMjr[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionTemplateMjr;
                    break;
                #endregion

                #region FrmMaintenanceJobAssignNote
                case "FrmMaintenanceJobAssignNote":
                    LgsMaintenanceJobAssignService lgsMaintenanceJobAssignService = new LgsMaintenanceJobAssignService();
                    DataSet dsReportDataMja = lgsMaintenanceJobAssignService.GetMaintenanceJobAssignTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldMja =  { "Document", "Date", "Dilivery Date", "Remark", "Reference No", "", "", "", "", "", 
                                            "", "Location", "", "", "", "", "", "", "", "", "", 
                                            "", "", "", "", "", "", "" };
                    string[] stringFieldMjaProducts =  { "Pro.Code", "Pro.Name", "Unit", "B. Qty", "Or.Qty", "F.Qty", "C.Price", "Dis.%", "Dis.Amt.", 
                                            "Net Amt."};

                    /////////////////////////////////////////
                    //LgsRptTransactionTemplateProductDetailsSub lgsRptTransactionTemplateProductDetailsSub = new LgsRptTransactionTemplateProductDetailsSub();
                    //lgsRptTransactionTemplateProductDetailsSub.SetDataSource(dsReportDataMja.Tables[1]);

                    ////////////////////////////////////
                    LgsRptTransactionTemplateSubReport lgsRptTransactionTemplateMja = new LgsRptTransactionTemplateSubReport();
                  
                    
                    //lgsRptTransactionTemplateMja.Subreports[""].SetDataSource(dsReportDataMja.Tables[2]);
                    //// Assign formula and summery field values
                    lgsRptTransactionTemplateMja.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransactionTemplateMja.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionTemplateMja.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionTemplateMja.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionTemplateMja.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionTemplateMja.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionTemplateMja.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";
                    lgsRptTransactionTemplateMja.SetDataSource(dsReportDataMja.Tables[0]);

                    for (int i = 0; i < stringFieldMja.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptTransactionTemplateMja.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldMja[i].Trim() + "'";
                    }

                    // Sub report products (FieldString13 to FieldString22)
                    for (int i = 0; i < stringFieldMjaProducts.Length; i++)
                    {
                        string st = "st" + (i + 13);
                        lgsRptTransactionTemplateMja.Subreports["LgsRptTransactionTemplateProductDetailsSub.rpt"].DataDefinition.FormulaFields["st" + (i + 12).ToString()].Text = "'" + stringFieldMjaProducts[i].Trim() + "'";
                        ///////////
                        //lgsRptTransactionTemplateProductDetailsSub.DataDefinition.FormulaFields["st" + (i + 12).ToString()].Text = "'" + stringFieldMjaProducts[i].Trim() + "'";
                        ///////////
                    }
                    lgsRptTransactionTemplateMja.Subreports["LgsRptTransactionTemplateProductDetailsSub.rpt"].SetDataSource(dsReportDataMja.Tables[1]);
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionTemplateMja;
                    break;

                #endregion

                #region FrmServiceOut
                case "FrmServiceOut":
                    ServiceOutService serviceOutService = new ServiceOutService();
                    reportData = serviceOutService.GetServiceOutTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldSOut = { "Document", "Date", "Supplier", "Employee", "Remark", "Reference No", "", "", "", "", 
                                            "", "Location", "Pro.Code", "Pro.Name", "Unit", "", "Batch No", "", "Qty", "B. Qty", "Rate", 
                                            "Amount", "", "", "", "", "", "" };

                    LgsRptTransactionTemplate lgsRptTransactionTemplateSOut = new LgsRptTransactionTemplate();
                    lgsRptTransactionTemplateSOut.SetDataSource(reportData);
                    // Assign formula and summery field values
                    lgsRptTransactionTemplateSOut.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransactionTemplateSOut.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionTemplateSOut.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionTemplateSOut.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionTemplateSOut.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionTemplateSOut.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionTemplateSOut.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldSOut.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptTransactionTemplateSOut.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldSOut[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionTemplateSOut;

                    break;
                #endregion

                #region FrmServiceIn
                case "FrmServiceIn":
                    ServiceInService serviceInService = new ServiceInService();
                    reportData = serviceInService.GetServiceInTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldSIn = { "Document", "Date", "Supplier", "Employee", "Remark", "", "", "", "", "", 
                                            "", "Location", "Pro.Code", "Pro.Name", "Unit", "", "Batch No", "", "Qty", "", "Rate", 
                                            "Amount", "", "", "", "", "", "" };

                    LgsRptTransactionTemplate lgsRptTransactionTemplateSIn = new LgsRptTransactionTemplate();
                    lgsRptTransactionTemplateSIn.SetDataSource(reportData);
                    // Assign formula and summery field values
                    lgsRptTransactionTemplateSIn.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransactionTemplateSIn.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionTemplateSIn.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionTemplateSIn.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionTemplateSIn.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionTemplateSIn.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionTemplateSIn.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldSIn.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        lgsRptTransactionTemplateSIn.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldSIn[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionTemplateSIn;

                    break;
                #endregion

                #endregion

                #region purchasing reports
                #endregion

                #region stock reports
                #endregion

                default:
                    break;
            }

            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Refresh();
            reportViewer.Show();            
            Cursor.Current = Cursors.Default;

        }

        public void GenearateTransactionSummeryReport(AutoGenerateInfo autoGenerateInfo, DataTable dtReportData, DataTable dtReportConditions, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> groupByStructList, bool viewGroupDetails)
        {
            DataTable dtArrangedReportData = new DataTable();
            FrmReportViewer reportViewer = new FrmReportViewer();
            Cursor.Current = Cursors.WaitCursor;
            string group1, group2, group3, group4;
            int yx = 0;
            switch (autoGenerateInfo.FormName)
            {

                #region Logistic transactions

                #region FrmPurchaseOrder
                case "FrmLogisticPurchaseOrder":
                    LgsRptPOTransactionSummeryTemplateLandscape LgsRptTransactionSummeryTemplatePo = new LgsRptPOTransactionSummeryTemplateLandscape();

                    LgsRptTransactionSummeryTemplatePo.SetDataSource(dtReportData);
                    LgsRptTransactionSummeryTemplatePo.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    LgsRptTransactionSummeryTemplatePo.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    LgsRptTransactionSummeryTemplatePo.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    LgsRptTransactionSummeryTemplatePo.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    LgsRptTransactionSummeryTemplatePo.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    LgsRptTransactionSummeryTemplatePo.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    LgsRptTransactionSummeryTemplatePo.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    LgsRptTransactionSummeryTemplatePo.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    strFieldName = string.Empty;
                    int srpos = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            if (reportDataStructList[i].IsSelectionField)
                            { LgsRptTransactionSummeryTemplatePo.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { LgsRptTransactionSummeryTemplatePo.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (7 + srpos);
                            if (reportDataStructList[i].IsSelectionField)
                            { LgsRptTransactionSummeryTemplatePo.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { LgsRptTransactionSummeryTemplatePo.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                            srpos++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 10; i++)
                    {
                        if (LgsRptTransactionSummeryTemplatePo.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.StringParameter))
                        {
                            LgsRptTransactionSummeryTemplatePo.SetParameterValue(i, "");

                            if (groupByStructList.Count > i)
                            {
                                if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                                {
                                    LgsRptTransactionSummeryTemplatePo.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                                }
                                else
                                {
                                    LgsRptTransactionSummeryTemplatePo.SetParameterValue(i, "");
                                }
                            }
                        }
                    }
                    #endregion

                    reportViewer.crRptViewer.ReportSource = LgsRptTransactionSummeryTemplatePo;


                    break;
                #endregion

                #region FrmLogisticGoodsReceivedNote
                case "FrmLogisticGoodsReceivedNote":
                    LgsRptGRNTransactionSummeryTemplateLandscape lgsRptTransactionSummeryTemplateGrn = new LgsRptGRNTransactionSummeryTemplateLandscape();

                    lgsRptTransactionSummeryTemplateGrn.SetDataSource(dtReportData);
                    lgsRptTransactionSummeryTemplateGrn.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    lgsRptTransactionSummeryTemplateGrn.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    lgsRptTransactionSummeryTemplateGrn.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionSummeryTemplateGrn.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionSummeryTemplateGrn.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionSummeryTemplateGrn.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionSummeryTemplateGrn.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionSummeryTemplateGrn.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    strFieldName = string.Empty;
                    int srgrns = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            if (reportDataStructList[i].IsSelectionField)
                            { lgsRptTransactionSummeryTemplateGrn.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { lgsRptTransactionSummeryTemplateGrn.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (10 + srgrns);
                            if (reportDataStructList[i].IsSelectionField)
                            { lgsRptTransactionSummeryTemplateGrn.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { lgsRptTransactionSummeryTemplateGrn.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                            srgrns++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 10; i++)
                    {
                        if (lgsRptTransactionSummeryTemplateGrn.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.StringParameter))
                        {
                            lgsRptTransactionSummeryTemplateGrn.SetParameterValue(i, "");

                            if (groupByStructList.Count > i)
                            {
                                if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                                {
                                    lgsRptTransactionSummeryTemplateGrn.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                                }
                                else
                                {
                                    lgsRptTransactionSummeryTemplateGrn.SetParameterValue(i, "");
                                }
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionSummeryTemplateGrn;
                    break;
                #endregion

                #region FrmLogisticPurchaseReturnNote
                case "FrmLogisticPurchaseReturnNote":
                    LgsRptPRNTransactionSummeryTemplateLandscape lgsRptTransactionSummeryTemplatePrn = new LgsRptPRNTransactionSummeryTemplateLandscape();

                    lgsRptTransactionSummeryTemplatePrn.SetDataSource(dtReportData);
                    lgsRptTransactionSummeryTemplatePrn.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    lgsRptTransactionSummeryTemplatePrn.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    lgsRptTransactionSummeryTemplatePrn.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionSummeryTemplatePrn.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionSummeryTemplatePrn.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionSummeryTemplatePrn.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionSummeryTemplatePrn.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionSummeryTemplatePrn.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    strFieldName = string.Empty;
                    int srprns = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            if (reportDataStructList[i].IsSelectionField)
                            { lgsRptTransactionSummeryTemplatePrn.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { lgsRptTransactionSummeryTemplatePrn.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (10 + srprns);
                            if (reportDataStructList[i].IsSelectionField)
                            { lgsRptTransactionSummeryTemplatePrn.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { lgsRptTransactionSummeryTemplatePrn.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                            srprns++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 10; i++)
                    {
                        if (lgsRptTransactionSummeryTemplatePrn.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.StringParameter))
                        {
                            lgsRptTransactionSummeryTemplatePrn.SetParameterValue(i, "");

                            if (groupByStructList.Count > i)
                            {
                                if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                                {
                                    lgsRptTransactionSummeryTemplatePrn.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                                }
                                else
                                {
                                    lgsRptTransactionSummeryTemplatePrn.SetParameterValue(i, "");
                                }
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionSummeryTemplatePrn;
                    break;
                #endregion

                #region FrmLogisticTransferOfGoodsNote
                case "FrmLogisticTransferOfGoodsNote":
                    LgsRptTOGTransactionSummeryTemplateLandscape lgsRptTransactionSummeryTemplateTOG = new LgsRptTOGTransactionSummeryTemplateLandscape();

                    lgsRptTransactionSummeryTemplateTOG.SetDataSource(dtReportData);
                    lgsRptTransactionSummeryTemplateTOG.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    lgsRptTransactionSummeryTemplateTOG.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    lgsRptTransactionSummeryTemplateTOG.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionSummeryTemplateTOG.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionSummeryTemplateTOG.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionSummeryTemplateTOG.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionSummeryTemplateTOG.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionSummeryTemplateTOG.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    strFieldName = string.Empty;
                    int srtogs = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            if (reportDataStructList[i].IsSelectionField)
                            { lgsRptTransactionSummeryTemplateTOG.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { lgsRptTransactionSummeryTemplateTOG.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (8 + srtogs);
                            if (reportDataStructList[i].IsSelectionField)
                            { lgsRptTransactionSummeryTemplateTOG.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { lgsRptTransactionSummeryTemplateTOG.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                            srtogs++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 10; i++)
                    {
                        if (lgsRptTransactionSummeryTemplateTOG.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.StringParameter))
                        {
                            lgsRptTransactionSummeryTemplateTOG.SetParameterValue(i, "");

                            if (groupByStructList.Count > i)
                            {
                                if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                                {
                                    lgsRptTransactionSummeryTemplateTOG.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                                }
                                else
                                {
                                    lgsRptTransactionSummeryTemplateTOG.SetParameterValue(i, "");
                                }
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionSummeryTemplateTOG;
                    break;

                #endregion

                #region FrmMaterialRequestNote
                case "FrmMaterialRequestNote":
                    LgsRptTransactionSummeryTemplate2 lgsRptTransactionSummeryTemplateMrn = new LgsRptTransactionSummeryTemplate2();

                    lgsRptTransactionSummeryTemplateMrn.SetDataSource(dtReportData);
                    lgsRptTransactionSummeryTemplateMrn.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    lgsRptTransactionSummeryTemplateMrn.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransactionSummeryTemplateMrn.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionSummeryTemplateMrn.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionSummeryTemplateMrn.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionSummeryTemplateMrn.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionSummeryTemplateMrn.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionSummeryTemplateMrn.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    strFieldName = string.Empty;
                    int srmrn = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            if (reportDataStructList[i].IsSelectionField)
                            { lgsRptTransactionSummeryTemplateMrn.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { lgsRptTransactionSummeryTemplateMrn.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (7 + srmrn);
                            if (reportDataStructList[i].IsSelectionField)
                            { lgsRptTransactionSummeryTemplateMrn.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { lgsRptTransactionSummeryTemplateMrn.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                            srmrn++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 10; i++)
                    {
                        if (lgsRptTransactionSummeryTemplateMrn.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.StringParameter))
                        {
                            lgsRptTransactionSummeryTemplateMrn.SetParameterValue(i, "");

                            if (groupByStructList.Count > i)
                            {
                                if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                                {
                                    lgsRptTransactionSummeryTemplateMrn.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                                }
                                else
                                {
                                    lgsRptTransactionSummeryTemplateMrn.SetParameterValue(i, "");
                                }
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionSummeryTemplateMrn;
                    break;
                #endregion


                #region FrmMaterialAllocationNote
                case "FrmMaterialAllocationNote":
                    LgsRptMANTransactionSummeryTemplate lgsRptTransactionSummeryTemplateMan = new LgsRptMANTransactionSummeryTemplate();

                    lgsRptTransactionSummeryTemplateMan.SetDataSource(dtReportData);
                    lgsRptTransactionSummeryTemplateMan.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    lgsRptTransactionSummeryTemplateMan.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransactionSummeryTemplateMan.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransactionSummeryTemplateMan.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransactionSummeryTemplateMan.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransactionSummeryTemplateMan.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransactionSummeryTemplateMan.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptTransactionSummeryTemplateMan.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    strFieldName = string.Empty;
                    int srman = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            if (reportDataStructList[i].IsSelectionField)
                            { lgsRptTransactionSummeryTemplateMan.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { lgsRptTransactionSummeryTemplateMan.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (7 + srman);
                            if (reportDataStructList[i].IsSelectionField)
                            { lgsRptTransactionSummeryTemplateMan.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { lgsRptTransactionSummeryTemplateMan.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                            srman++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 10; i++)
                    {
                        if (lgsRptTransactionSummeryTemplateMan.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.StringParameter))
                        {
                            lgsRptTransactionSummeryTemplateMan.SetParameterValue(i, "");

                            if (groupByStructList.Count > i)
                            {
                                if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                                {
                                    lgsRptTransactionSummeryTemplateMan.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                                }
                                else
                                {
                                    lgsRptTransactionSummeryTemplateMan.SetParameterValue(i, "");
                                }
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = lgsRptTransactionSummeryTemplateMan;
                    break;
                #endregion

                #endregion

                #region stock report

                #region LgsRptOpeningBalanceRegister
                case "LgsRptOpeningBalanceRegister":
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
                #region Stock Balance
                case "LgsRptStockBalance":
                    LgsRptStockBalancesTemplateLandscape lgsRptStockBalancesTemplateLandscape = new LgsRptStockBalancesTemplateLandscape();

                    lgsRptStockBalancesTemplateLandscape.SetDataSource(dtReportData);
                    lgsRptStockBalancesTemplateLandscape.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    lgsRptStockBalancesTemplateLandscape.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptStockBalancesTemplateLandscape.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptStockBalancesTemplateLandscape.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptStockBalancesTemplateLandscape.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptStockBalancesTemplateLandscape.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptStockBalancesTemplateLandscape.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    lgsRptStockBalancesTemplateLandscape.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    strFieldName = string.Empty;
                    int srst = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            if (reportDataStructList[i].IsSelectionField)
                            { lgsRptStockBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { lgsRptStockBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (8 + srst);
                            if (reportDataStructList[i].IsSelectionField)
                            { lgsRptStockBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { lgsRptStockBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                            srst++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 10; i++)
                    {
                        if (lgsRptStockBalancesTemplateLandscape.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.StringParameter))
                        {
                            lgsRptStockBalancesTemplateLandscape.SetParameterValue(i, "");

                            if (groupByStructList.Count > i)
                            {
                                if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                                {
                                    lgsRptStockBalancesTemplateLandscape.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                                }
                                else
                                {
                                    lgsRptStockBalancesTemplateLandscape.SetParameterValue(i, "");
                                }
                            }
                            else
                            {
                                lgsRptStockBalancesTemplateLandscape.SetParameterValue(i, "");
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = lgsRptStockBalancesTemplateLandscape;
                    break;

                #endregion
                #region LgsRptTransferRegisterr
                case "LgsRptTransferRegister":
                    LgsRptTransferTemplateLandscape lgsRptTransferTemplateLandscape = new LgsRptTransferTemplateLandscape();

                    #region Set Values for report header fields
                    strFieldName = string.Empty;
                    int srtr = 1, dctr = 11;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {

                            if (item.ReportDataType.Equals(typeof(string)))
                            {
                                strFieldName = "st" + srtr;
                                lgsRptTransferTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                srtr++;
                            }

                            if (item.ReportDataType.Equals(typeof(decimal)))
                            {
                                strFieldName = "st" + dctr;
                                lgsRptTransferTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                dctr++;
                            }
                        }
                    }

                    #endregion


                    // Re arrange data table header columns for report
                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    lgsRptTransferTemplateLandscape.SetDataSource(dtArrangedReportData);
                    lgsRptTransferTemplateLandscape.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    lgsRptTransferTemplateLandscape.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    lgsRptTransferTemplateLandscape.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    lgsRptTransferTemplateLandscape.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    lgsRptTransferTemplateLandscape.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    lgsRptTransferTemplateLandscape.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    lgsRptTransferTemplateLandscape.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    #region Group By

                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        // Set report group field values
                        for (int i = 0; i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count(); i++)
                        {
                            if (i < lgsRptTransferTemplateLandscape.DataDefinition.Groups.Count)
                            {
                                lgsRptTransferTemplateLandscape.DataDefinition.Groups[i].ConditionField = lgsRptTransferTemplateLandscape.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())]; //[string.Concat("FieldString", groupStrFieldCount)];
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < lgsRptTransferTemplateLandscape.DataDefinition.Groups.Count; i++)
                        {
                            if (lgsRptTransferTemplateLandscape.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                if (i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count())
                                {
                                    lgsRptTransferTemplateLandscape.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), true);
                                }
                                else
                                {
                                    lgsRptTransferTemplateLandscape.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < lgsRptTransferTemplateLandscape.DataDefinition.Groups.Count; i++)
                        {
                            if (lgsRptTransferTemplateLandscape.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                lgsRptTransferTemplateLandscape.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion

                    reportViewer.crRptViewer.ReportSource = lgsRptTransferTemplateLandscape;
                    break;

                #endregion


                #endregion

                default:
                    break;
 
            }

            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
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
                if (col.ColumnName.Contains(stringColumnName))
                {
                    col.ColumnName = stringColumnName + (stringColIndex + 1).ToString();
                    stringColIndex++;
                }

                if (col.ColumnName.Contains(decimalColumnName))
                {
                    col.ColumnName = decimalColumnName + (decimalColIndex + 1).ToString();
                    decimalColIndex++;
                }
            }
            return reportData;
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
        /// <param name="lgsRptGroupedDetails"></param>
        /// <param name="dtReportData"></param>
        /// <param name="dtReportConditions"></param>
        /// <param name="reportDataStructList"></param>
        /// <param name="autoGenerateInfo"></param>
        /// <returns></returns>
        private LgsRptGroupedDetailsTemplate ViewGroupedReport(LgsRptGroupedDetailsTemplate lgsRptGroupedDetails, DataTable dtReportData, DataTable dtReportConditions, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
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
                        lgsRptGroupedDetails.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStruct.ReportFieldName.Trim() + "'";
                        sr++;
                        groupingFields = string.IsNullOrEmpty(groupingFields) ? (reportDataStruct.ReportFieldName.Trim()) : (groupingFields + "/ " + reportDataStruct.ReportFieldName.Trim());
                    }

                    if (reportDataStruct.ReportDataType.Equals(typeof(decimal)))
                    {
                        strFieldName = "st" + dc;
                        lgsRptGroupedDetails.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStruct.ReportFieldName.Trim() + "'";
                        dc++;
                    }
                }
            }

            #endregion

            // Re arrange data table header columns for report
            dtArrangedReportData = dtReportData.Copy();
            dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

            lgsRptGroupedDetails.SetDataSource(dtArrangedReportData);
            lgsRptGroupedDetails.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
            lgsRptGroupedDetails.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
            lgsRptGroupedDetails.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            lgsRptGroupedDetails.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            lgsRptGroupedDetails.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            lgsRptGroupedDetails.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            lgsRptGroupedDetails.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
            lgsRptGroupedDetails.DataDefinition.FormulaFields["GroupNames"].Text = "'" + (string.IsNullOrEmpty(groupingFields) ? "" : groupingFields) + "'";

            #region Group By

            // Set report group field values
            for (int i = 0; i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count(); i++)
            {
                if (i < lgsRptGroupedDetails.DataDefinition.Groups.Count)
                {
                    lgsRptGroupedDetails.DataDefinition.Groups[i].ConditionField = lgsRptGroupedDetails.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                }
            }

            // Set parameter select field values
            for (int i = 0; i < lgsRptGroupedDetails.DataDefinition.Groups.Count; i++)
            {
                if (lgsRptGroupedDetails.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                {
                    if (i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count())
                    {
                        lgsRptGroupedDetails.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), true);
                    }
                    else
                    {
                        lgsRptGroupedDetails.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                    }
                }
            }

            #endregion

            return lgsRptGroupedDetails;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lgsRptDetailsTemplate"></param>
        /// <param name="dtReportData"></param>
        /// <param name="dtReportConditions"></param>
        /// <param name="reportDataStructList"></param>
        /// <param name="autoGenerateInfo"></param>
        /// <returns></returns>
        private LgsRptDetailsTemplate ViewUnGroupedReport(LgsRptDetailsTemplate lgsRptDetailsTemplate, DataTable dtReportData, DataTable dtReportConditions, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
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
                        lgsRptDetailsTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                        sr++;
                    }

                    if (item.ReportDataType.Equals(typeof(decimal)))
                    {
                        strFieldName = "st" + dc;
                        lgsRptDetailsTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
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

            lgsRptDetailsTemplate.SetDataSource(dtArrangedReportData);
            lgsRptDetailsTemplate.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
            lgsRptDetailsTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
            lgsRptDetailsTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            lgsRptDetailsTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            lgsRptDetailsTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            lgsRptDetailsTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            lgsRptDetailsTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            // Set parameter sum fields
            for (int i = 0; i < lgsRptDetailsTemplate.ParameterFields.Count; i++)
            {
                if (lgsRptDetailsTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter) && newSumFieldsIndexesList.Contains(i + 1) && lgsRptDetailsTemplate.ParameterFields[i].Name.StartsWith("prmSumFieldDecimal"))
                {
                    lgsRptDetailsTemplate.SetParameterValue("prmSumFieldDecimal" + (i + 1).ToString(), true);
                }
                else
                {
                    lgsRptDetailsTemplate.SetParameterValue("prmSumFieldDecimal" + (i + 1).ToString(), false);
                }
            }

            return lgsRptDetailsTemplate;

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
                        newIndexesList.Add(decimalColIndex + 1);
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
    }
}
