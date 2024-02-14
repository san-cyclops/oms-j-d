using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using CrystalDecisions.Shared;
using Domain;
using Utility;
using Service;
using Report.Inventory.Transactions.Reports;
using Report.CRM.Reports;
using System.Collections;

namespace Report.CRM
{
    public class CrmReportGenerator
    {
        string groupingFields = string.Empty;
        string strFieldName = string.Empty;

        class ReportSources
        {
            public DataTable reportData { get; set; }
            public ArrayList newSumFieldsIndexes { get; set; }
        }

        public void GenearateReferenceReport(AutoGenerateInfo autoGenerateInfo, string referenceNo)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            Cursor.Current = Cursors.WaitCursor;
            DataTable reportData = new DataTable();

            string[] stringField = new string[] { };
            string[] stringHeaderField = new string[] { };
            

            switch (autoGenerateInfo.FormName)
            {
                #region Loyalty Customer
                case "FrmLoyaltyCustomer": // Loyalty Customer
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                    CrmRptReferenceTemplate crmRptReferenceTemplate = new CrmRptReferenceTemplate();
                    reportData = loyaltyCustomerService.GetLoyaltyCustomerDataTable(referenceNo);
                    
                    // Set field text
                    stringField = new string[] { "Card Type", "Card No", "Code", "Since", "Name", "Name on Card", 
                                            "Date of Birth", "Gender", "NIC", "Reference No", "Age", "Civil Status", 
                                            "Nationality", "Religion", "Address", "Mobile", "Telephone", "Fax", "Email", "Created Date",
                                            "Send Updates via SMS", "Send Updates via Email",
                                            "Organization", "Occupation", "Company Address", "Telephone", "Email", "Mobile", "Fax", 
                                            "", "Card Issued", "Issued Date", "Expiry Date", "Renewed Date", "Credit Allowed", 
                                            "Current Points", "Earned Points", "Redeemed Points", "Suspended", "Black Listed",
                                            "Active", "Active Date", "Remark", "", "Created User", "", "Sinhala & Hindu New Year", "Wesak", 
                                            "Ramazan", "Thai Pongal", "Haj Festival", "X Mas","Race"
                                            };

                    stringHeaderField = new string[] { "CUSTOMER PROFILE", "COMPANY PROFILE", "DETAILS OF CARD", "DETAILS OF CARD ACTIVITY", "DETAILS OF PROMOTION"};

                    crmRptReferenceTemplate.SetDataSource(reportData);
                    // Assign formula and summary field values
                    crmRptReferenceTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    crmRptReferenceTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    crmRptReferenceTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    crmRptReferenceTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    crmRptReferenceTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    crmRptReferenceTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        crmRptReferenceTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }

                    for (int i = 0; i < stringHeaderField.Length; i++)
                    {
                        string stHr = "hr" + (i + 1);
                        crmRptReferenceTemplate.DataDefinition.FormulaFields["hr" + (i + 1).ToString()].Text = "'" + stringHeaderField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = crmRptReferenceTemplate;
                    break;
                #endregion
                
                default:
                    break;
            }

            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
        }

        public void GenearateCrmReport(AutoGenerateInfo autoGenerateInfo, DataTable dtReportData, DataTable dtReportConditions, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> groupByStructList)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            Cursor.Current = Cursors.WaitCursor;

            DataTable dtArrangedReportData = new DataTable();
            List<Common.ReportDataStruct> selectedReportStructList;
            List<Common.ReportDataStruct> selectedGroupStructList;
            string strFieldName;
            int sr = 1, dc = 12;
            int gr = 0, gp = 1;


            switch (autoGenerateInfo.FormName)
            {

                #region Loyalty Customer

                case "FrmLoyaltyCustomer":

                    CrmRptReportTemplate crmRptReportTemplate = new CrmRptReportTemplate();

                    #region Old - Configuration
                    //List<Common.ReportDataStruct> printDataStructList = reportDataStructList.Where(a => a.IsSelectionField.Equals(true)).ToList();
                    //DataTable dtPrintReportData = new DataTable();

                    //int recordCount = reportDataStructList.Where(a => a.IsSelectionField.Equals(true)).Count();
                    //int columnCount = 0, colCount = 1;
                    //string columnName = "", columnName2 = "";

                    //for (int i = 0; i < printDataStructList.Count; i++)
                    //{
                    //    dtPrintReportData.Columns.Add(new DataColumn(string.Concat("FieldString", colCount), (Type)(printDataStructList[i].ReportDataType)));
                    //    colCount++;
                    //}
                   
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
                    //crmRptReportTemplate.SetDataSource(dtPrintReportData);
                    #endregion

                    #region Set Values for report header Fields
                    strFieldName = string.Empty;
                    sr = 1; dc = 12;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {
                            if (item.ReportDataType.Equals(typeof(string)) && sr <= 12)
                            {
                                strFieldName = "st" + sr;
                                crmRptReportTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
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

                    crmRptReportTemplate.SetDataSource(dtArrangedReportData);
                    crmRptReportTemplate.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    crmRptReportTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    crmRptReportTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    crmRptReportTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    crmRptReportTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    crmRptReportTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    crmRptReportTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    strFieldName = string.Empty;
                    
                    
                    //for (int i = 0; i < reportDataStructList.Where(a=>a.IsSelectionField.Equals(true)).Count(); i++)
                    //{
                    //    strFieldName = "st" + (i + 1);
                    //    if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //    {
                    //        crmRptReportTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //    }

                    //    if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //    {
                    //        strFieldName = "st" + (12 - i);
                    //        crmRptReportTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //    }
                    //}
                    //for (int i = 0; i < printDataStructList.Count; i++)
                    #region Set Header - old
                    //int columnHeaderCount = 0;
                    //columnHeaderCount = (printDataStructList.Count > 14 ? 14 : printDataStructList.Count);
                    //for (int i = 0; i < columnHeaderCount; i++)
                    //{
                    //    strFieldName = "st" + (i + 1);
                    //    if (printDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //    {
                    //        crmRptReportTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + printDataStructList[i].ReportFieldName.Trim() + "'";
                    //    }

                    //    if (printDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //    {
                    //        strFieldName = "st" + (12 - i);
                    //        crmRptReportTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + printDataStructList[i].ReportFieldName.Trim() + "'";
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
                    //            crmRptReportTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //        {

                    //            switch (groupByStructList[i].ReportDataType.ToString())
                    //            {
                    //                case "System.String":
                    //                    crmRptReportTemplate.SetParameterValue(i, "");
                    //                    break;
                    //                case "DateTime":
                    //                    crmRptReportTemplate.SetParameterValue(i, "01/01/2013");
                    //                    break;
                    //                case "bool":
                    //                    crmRptReportTemplate.SetParameterValue(i, true);
                    //                    break;
                    //                case "Int32":
                    //                    crmRptReportTemplate.SetParameterValue(i, 0);
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
                            if (gr < crmRptReportTemplate.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    crmRptReportTemplate.DataDefinition.Groups[gr].ConditionField = crmRptReportTemplate.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp - 1 < crmRptReportTemplate.DataDefinition.Groups.Count)
                            {
                                if (crmRptReportTemplate.ParameterFields[gp - 1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        crmRptReportTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        crmRptReportTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < crmRptReportTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (crmRptReportTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                crmRptReportTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < crmRptReportTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (crmRptReportTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                crmRptReportTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = crmRptReportTemplate;
                #endregion

                    break;


                #region CRM Register
                case "RptCRMRegister":

                   if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        CrmRptGroupedDetailsTemplate crmRptGroupedDetailsTemplate = new CrmRptGroupedDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewGroupedReport(crmRptGroupedDetailsTemplate, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    else
                    {
                        CrmRptDetailsTemplate crmRptDetailsTemplate = new CrmRptDetailsTemplate();
                        reportViewer.crRptViewer.ReportSource = ViewUnGroupedReport(crmRptDetailsTemplate, dtReportData, dtReportConditions, reportDataStructList, autoGenerateInfo);
                    }
                    break;
                #endregion



                #region CustomerHistory


                case "RptCustomerHistory":

                    crmRptReportTemplate = new CrmRptReportTemplate();

                    crmRptReportTemplate.SetDataSource(dtReportData);
                    crmRptReportTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summery";
                    crmRptReportTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    crmRptReportTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    crmRptReportTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    crmRptReportTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    crmRptReportTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

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
                            crmRptReportTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (12 - i);
                            crmRptReportTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 3; i++)
                    {
                        crmRptReportTemplate.SetParameterValue(i, "");
                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                string strGroup = groupByStructList[i].ReportField.ToString();
                                crmRptReportTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {

                                switch (groupByStructList[i].ValueDataType.ToString())
                                {
                                    case "System.String":
                                        crmRptReportTemplate.SetParameterValue(i, "");
                                        break;
                                    case "System.DateTime":
                                        crmRptReportTemplate.SetParameterValue(i, "02-02-2013");
                                        break;
                                    case "System.bool":
                                        crmRptReportTemplate.SetParameterValue(i, true);
                                        break;
                                    case "System.Int32":
                                        crmRptReportTemplate.SetParameterValue(i, 0);
                                        break;
                                    case "System.Int64":
                                        crmRptReportTemplate.SetParameterValue(i, 0);
                                        break;
                                    default:

                                        break;
                                }
                            }
                        }


                    }
                    #endregion

                    reportViewer.crRptViewer.ReportSource = crmRptReportTemplate;
                #endregion

                    break;

                #region RptInActiveCustomerDetails


                case "RptInActiveCustomerDetails":

                    CrmRptReportTemplatePoints crmRptReportTemplatePoints = new CrmRptReportTemplatePoints();

                    crmRptReportTemplatePoints.SetDataSource(dtReportData);
                    crmRptReportTemplatePoints.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summery";
                    crmRptReportTemplatePoints.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

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
                            crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (12 - i);
                            crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 3; i++)
                    {
                        crmRptReportTemplatePoints.SetParameterValue(i, "");
                        if (groupByStructList.Count > i)
                        {
                            if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                            {
                                string strGroup = groupByStructList[i].ReportField.ToString();
                                crmRptReportTemplatePoints.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {

                                switch (groupByStructList[i].ValueDataType.ToString())
                                {
                                    case "System.String":
                                        crmRptReportTemplatePoints.SetParameterValue(i, "");
                                        break;
                                    case "System.DateTime":
                                        crmRptReportTemplatePoints.SetParameterValue(i, "02-02-2013");
                                        break;
                                    case "System.bool":
                                        crmRptReportTemplatePoints.SetParameterValue(i, true);
                                        break;
                                    case "System.Int32":
                                        crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                        break;
                                    case "System.Int64":
                                        crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                        break;
                                    default:

                                        break;
                                }
                            }
                        }


                    }
                    #endregion

                    reportViewer.crRptViewer.ReportSource = crmRptReportTemplatePoints;
                    #endregion

            break;

                #region  RptBranchWiseCustomerDetails
                case "RptBranchWiseCustomerDetails":

                crmRptReportTemplate = new CrmRptReportTemplate();

                #region Set Values for report header Fields
                strFieldName = string.Empty;
                sr = 1; dc = 12;

                foreach (var item in reportDataStructList)
                {
                    if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                    {
                        if (item.ReportDataType.Equals(typeof(string)) && sr <= 12)
                        {
                            strFieldName = "st" + sr;
                            crmRptReportTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
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

                crmRptReportTemplate.SetDataSource(dtArrangedReportData);
                crmRptReportTemplate.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                //crmRptReportTemplate.SetDataSource(dtReportData);
                crmRptReportTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                crmRptReportTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                crmRptReportTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                crmRptReportTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                crmRptReportTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                crmRptReportTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                ///////////////////////////////////////////////////////////////////////////
                // Use some other way to add values into formula fields DateFrom and  DateTo ()
                //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                ///////////////////////////////////////////////////////////////////////////
                //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                #region Set Header - old
                //strFieldName = string.Empty;
                //for (int i = 0; i < reportDataStructList.Count; i++)
                //{
                //    strFieldName = "st" + (i + 1);
                //    if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                //    {
                //        crmRptReportTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                //    }

                //    if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                //    {
                //        strFieldName = "st" + (12 - i);
                //        crmRptReportTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                //    }
                //}
                #endregion

                #region Group By - old

                ////for (int i = 0; i <= 3; i++)
                ////{
                ////    crmRptReportTemplate.SetParameterValue(i, "");
                ////    if (groupByStructList.Count > i)
                ////    {
                ////        if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                ////        {
                ////            string strGroup = groupByStructList[i].ReportField.ToString();
                ////            crmRptReportTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                ////        }
                ////        else
                ////        {

                ////            switch (groupByStructList[i].ValueDataType.ToString())
                ////            {
                ////                case "System.String":
                ////                    crmRptReportTemplate.SetParameterValue(i, "");
                ////                    break;
                ////                case "System.DateTime":
                ////                    crmRptReportTemplate.SetParameterValue(i, "02-02-2013");
                ////                    break;
                ////                case "System.bool":
                ////                    crmRptReportTemplate.SetParameterValue(i, true);
                ////                    break;
                ////                case "System.Int32":
                ////                    crmRptReportTemplate.SetParameterValue(i, 0);
                ////                    break;
                ////                case "System.Int64":
                ////                    crmRptReportTemplate.SetParameterValue(i, 0);
                ////                    break;
                ////                default:

                ////                    break;
                ////            }
                ////        }
                ////    }


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
                        if (gr < crmRptReportTemplate.DataDefinition.Groups.Count)
                        {
                            if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                            {
                                crmRptReportTemplate.DataDefinition.Groups[gr].ConditionField = crmRptReportTemplate.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                gr++;
                            }
                        }
                    }

                    // Set parameter field values
                    for (int i = 0; i < selectedReportStructList.Count(); i++)
                    {
                        if (gp - 1 < crmRptReportTemplate.DataDefinition.Groups.Count)
                        {
                            if (crmRptReportTemplate.ParameterFields[gp - 1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    crmRptReportTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                    gp++;
                                }
                                else
                                {
                                    crmRptReportTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                }
                            }
                        }
                    }

                    for (int i = gp; i < crmRptReportTemplate.DataDefinition.Groups.Count; i++)
                    {
                        if (crmRptReportTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                        {
                            crmRptReportTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                        }
                    }
                }
                else
                {
                    // Set parameter field values
                    for (int i = 0; i < crmRptReportTemplate.DataDefinition.Groups.Count; i++)
                    {
                        if (crmRptReportTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                        {
                            crmRptReportTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                        }
                    }
                }
                #endregion
            reportViewer.crRptViewer.ReportSource = crmRptReportTemplate;
            #endregion

            break;

                #region  RptCustomervisitdetails
                case "RptCustomervisitdetails":

            crmRptReportTemplatePoints = new CrmRptReportTemplatePoints();
            crmRptReportTemplatePoints.SetDataSource(dtReportData);
            crmRptReportTemplatePoints.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summery";
            crmRptReportTemplatePoints.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            ///////////////////////////////////////////////////////////////////////////
            // Use some other way to add values into formula fields DateFrom and  DateTo ()
            //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
            //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
            ///////////////////////////////////////////////////////////////////////////
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

            strFieldName = string.Empty;
            int xx = 0;
            for (int i = 0; i < reportDataStructList.Count; i++)
            {
                strFieldName = "st" + (i + 1);
                if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                {
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                }

                if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                {
                    strFieldName = "st" + (14 - xx);
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    xx++;
                }
            }

            #region Group By

            for (int i = 0; i <= 3; i++)
            {
                crmRptReportTemplatePoints.SetParameterValue(i, "");
                if (groupByStructList.Count > i)
                {
                    if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    {
                        string strGroup = groupByStructList[i].ReportField.ToString();
                        crmRptReportTemplatePoints.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    }
                    else
                    {

                        switch (groupByStructList[i].ValueDataType.ToString())
                        {
                            case "System.String":
                                crmRptReportTemplatePoints.SetParameterValue(i, "");
                                break;
                            case "System.DateTime":
                                crmRptReportTemplatePoints.SetParameterValue(i, "02-02-2013");
                                break;
                            case "System.bool":
                                crmRptReportTemplatePoints.SetParameterValue(i, true);
                                break;
                            case "System.Int32":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            case "System.Int64":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            default:

                                break;
                        }
                    }
                }


            }
            #endregion

            reportViewer.crRptViewer.ReportSource = crmRptReportTemplatePoints;
                #endregion

            break;
 
                #region  RptCustomerBehavior

                case "RptCustomerBehavior":

            crmRptReportTemplatePoints = new CrmRptReportTemplatePoints();
            crmRptReportTemplatePoints.SetDataSource(dtReportData);
            crmRptReportTemplatePoints.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summery";
            crmRptReportTemplatePoints.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            ///////////////////////////////////////////////////////////////////////////
            // Use some other way to add values into formula fields DateFrom and  DateTo ()
            //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
            //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
            ///////////////////////////////////////////////////////////////////////////
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

            strFieldName = string.Empty;
            xx = 0;
            for (int i = 0; i < reportDataStructList.Count; i++)
            {
                strFieldName = "st" + (i + 1);
                if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                {
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                }

                if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                {

                    strFieldName = "st" + (14 - xx);
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    xx++;
                }
            }

            #region Group By

            for (int i = 0; i <= 3; i++)
            {
                crmRptReportTemplatePoints.SetParameterValue(i, "");
                if (groupByStructList.Count > i)
                {
                    if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    {
                        string strGroup = groupByStructList[i].ReportField.ToString();
                        crmRptReportTemplatePoints.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    }
                    else
                    {

                        switch (groupByStructList[i].ValueDataType.ToString())
                        {
                            case "System.String":
                                crmRptReportTemplatePoints.SetParameterValue(i, "");
                                break;
                            case "System.DateTime":
                                crmRptReportTemplatePoints.SetParameterValue(i, "02-02-2013");
                                break;
                            case "System.bool":
                                crmRptReportTemplatePoints.SetParameterValue(i, true);
                                break;
                            case "System.Int32":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            case "System.Int64":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            default:

                                break;
                        }
                    }
                }


            }
            #endregion

            reportViewer.crRptViewer.ReportSource = crmRptReportTemplatePoints;
                #endregion

            break;

                #region  RptNumberOfVisitsCustomerWise
                case "RptNumberOfVisitsCustomerWise":

            crmRptReportTemplatePoints = new CrmRptReportTemplatePoints();
            crmRptReportTemplatePoints.SetDataSource(dtReportData);
            crmRptReportTemplatePoints.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summery";
            crmRptReportTemplatePoints.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            ///////////////////////////////////////////////////////////////////////////
            // Use some other way to add values into formula fields DateFrom and  DateTo ()
            //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
            //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
            ///////////////////////////////////////////////////////////////////////////
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

            strFieldName = string.Empty;
            xx = 0;
            for (int i = 0; i < reportDataStructList.Count; i++)
            {
                strFieldName = "st" + (i + 1);
                if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                {
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                }

                if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                {

                    strFieldName = "st" + (14 - xx);
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    xx++;
                }
            }

            #region Group By

            for (int i = 0; i <= 3; i++)
            {
                crmRptReportTemplatePoints.SetParameterValue(i, "");
                if (groupByStructList.Count > i)
                {
                    if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    {
                        string strGroup = groupByStructList[i].ReportField.ToString();
                        crmRptReportTemplatePoints.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    }
                    else
                    {

                        switch (groupByStructList[i].ValueDataType.ToString())
                        {
                            case "System.String":
                                crmRptReportTemplatePoints.SetParameterValue(i, "");
                                break;
                            case "System.DateTime":
                                crmRptReportTemplatePoints.SetParameterValue(i, "02-02-2013");
                                break;
                            case "System.bool":
                                crmRptReportTemplatePoints.SetParameterValue(i, true);
                                break;
                            case "System.Int32":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            case "System.Int64":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            default:

                                break;
                        }
                    }
                }


            }
            #endregion

            reportViewer.crRptViewer.ReportSource = crmRptReportTemplatePoints;
                #endregion

            break;

                #region  RptCashierWiseLoyaltySummary
                case "RptCashierWiseLoyaltySummary":

            CrmRptReportTemplatePoints2Group crmRptReportTemplatePoints2G = new CrmRptReportTemplatePoints2Group();
            crmRptReportTemplatePoints2G.SetDataSource(dtReportData);
            crmRptReportTemplatePoints2G.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summery";
            crmRptReportTemplatePoints2G.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptReportTemplatePoints2G.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptReportTemplatePoints2G.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptReportTemplatePoints2G.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptReportTemplatePoints2G.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            ///////////////////////////////////////////////////////////////////////////
            // Use some other way to add values into formula fields DateFrom and  DateTo ()
            //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
            //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
            ///////////////////////////////////////////////////////////////////////////
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

            strFieldName = string.Empty;
            xx = 0;
            for (int i = 0; i < reportDataStructList.Count; i++)
            {
                strFieldName = "st" + (i + 1);
                if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                {
                    crmRptReportTemplatePoints2G.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                }

                if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                {

                    strFieldName = "st" + (14 - xx);
                    crmRptReportTemplatePoints2G.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    xx++;
                }
            }

            #region Group By

            for (int i = 0; i <= 1; i++)
            {
                crmRptReportTemplatePoints2G.SetParameterValue(i, "");
                if (groupByStructList.Count > i)
                {
                    if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    {
                        string strGroup = groupByStructList[i].ReportField.ToString();
                        crmRptReportTemplatePoints2G.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    }
                    else
                    {

                        switch (groupByStructList[i].ValueDataType.ToString())
                        {
                            case "System.String":
                                crmRptReportTemplatePoints2G.SetParameterValue(i, "");
                                break;
                            case "System.DateTime":
                                crmRptReportTemplatePoints2G.SetParameterValue(i, "02-02-2013");
                                break;
                            case "System.bool":
                                crmRptReportTemplatePoints2G.SetParameterValue(i, true);
                                break;
                            case "System.Int32":
                                crmRptReportTemplatePoints2G.SetParameterValue(i, 0);
                                break;
                            case "System.Int64":
                                crmRptReportTemplatePoints2G.SetParameterValue(i, 0);
                                break;
                            default:

                                break;
                        }
                    }
                }


            }
            #endregion

            reportViewer.crRptViewer.ReportSource = crmRptReportTemplatePoints2G;
                #endregion

            break;

                #region  RptLocationWiseSummary
                case "RptLocationWiseSummary":

            crmRptReportTemplatePoints2G = new CrmRptReportTemplatePoints2Group();
            crmRptReportTemplatePoints2G.SetDataSource(dtReportData);
            crmRptReportTemplatePoints2G.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
            crmRptReportTemplatePoints2G.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptReportTemplatePoints2G.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptReportTemplatePoints2G.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptReportTemplatePoints2G.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptReportTemplatePoints2G.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            ///////////////////////////////////////////////////////////////////////////
            // Use some other way to add values into formula fields DateFrom and  DateTo ()
            //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
            //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
            ///////////////////////////////////////////////////////////////////////////
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

            strFieldName = string.Empty;
            xx = 0;
            for (int i = 0; i < reportDataStructList.Count; i++)
            {
                strFieldName = "st" + (i + 1);
                if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                {
                    crmRptReportTemplatePoints2G.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                }

                if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                {

                    strFieldName = "st" + (17 - xx);
                    crmRptReportTemplatePoints2G.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    xx++;
                }
            }

            #region Group By

            for (int i = 0; i <= 1; i++)
            {
                crmRptReportTemplatePoints2G.SetParameterValue(i, "");
                if (groupByStructList.Count > i)
                {
                    if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    {
                        string strGroup = groupByStructList[i].ReportField.ToString();
                        crmRptReportTemplatePoints2G.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    }
                    else
                    {

                        switch (groupByStructList[i].ValueDataType.ToString())
                        {
                            case "System.String":
                                crmRptReportTemplatePoints2G.SetParameterValue(i, "");
                                break;
                            case "System.DateTime":
                                crmRptReportTemplatePoints2G.SetParameterValue(i, "02-02-2013");
                                break;
                            case "System.bool":
                                crmRptReportTemplatePoints2G.SetParameterValue(i, true);
                                break;
                            case "System.Int32":
                                crmRptReportTemplatePoints2G.SetParameterValue(i, 0);
                                break;
                            case "System.Int64":
                                crmRptReportTemplatePoints2G.SetParameterValue(i, 0);
                                break;
                            default:

                                break;
                        }
                    }
                }


            }
            #endregion

            reportViewer.crRptViewer.ReportSource = crmRptReportTemplatePoints2G;
                #endregion

            break;

                #region  RptCustomerStatement
                case "RptCustomerStatement":

            crmRptReportTemplatePoints = new CrmRptReportTemplatePoints();
            crmRptReportTemplatePoints.SetDataSource(dtReportData);
            crmRptReportTemplatePoints.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
            crmRptReportTemplatePoints.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            ///////////////////////////////////////////////////////////////////////////
            // Use some other way to add values into formula fields DateFrom and  DateTo ()
            //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
            //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
            ///////////////////////////////////////////////////////////////////////////
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

            strFieldName = string.Empty;
            xx = 0;
            for (int i = 0; i < reportDataStructList.Count; i++)
            {
                strFieldName = "st" + (i + 1);
                if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                {
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                }

                if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                {

                    strFieldName = "st" + (14 - xx);
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    xx++;
                }
            }

            #region Group By

            for (int i = 0; i <= 3; i++)
            {
                crmRptReportTemplatePoints.SetParameterValue(i, "");
                if (groupByStructList.Count > i)
                {
                    if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    {
                        string strGroup = groupByStructList[i].ReportField.ToString();
                        crmRptReportTemplatePoints.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    }
                    else
                    {

                        switch (groupByStructList[i].ValueDataType.ToString())
                        {
                            case "System.String":
                                crmRptReportTemplatePoints.SetParameterValue(i, "");
                                break;
                            case "System.DateTime":
                                crmRptReportTemplatePoints.SetParameterValue(i, "02-02-2013");
                                break;
                            case "System.bool":
                                crmRptReportTemplatePoints.SetParameterValue(i, true);
                                break;
                            case "System.Int32":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            case "System.Int64":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            default:

                                break;
                        }
                    }
                }


            }
            #endregion

            reportViewer.crRptViewer.ReportSource = crmRptReportTemplatePoints;
             #endregion
            

            break;

                #region  RptBestCustomerDetails
                case "RptBestcustomerdetails":

            crmRptReportTemplatePoints = new CrmRptReportTemplatePoints();
            crmRptReportTemplatePoints.SetDataSource(dtReportData);
            crmRptReportTemplatePoints.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summery";
            crmRptReportTemplatePoints.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            ///////////////////////////////////////////////////////////////////////////
            // Use some other way to add values into formula fields DateFrom and  DateTo ()
            //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
            //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
            ///////////////////////////////////////////////////////////////////////////
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

            strFieldName = string.Empty;
            xx = 0;
            for (int i = 0; i < reportDataStructList.Count; i++)
            {
                strFieldName = "st" + (i + 1);
                if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                {
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                }

                if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                {

                    strFieldName = "st" + (14 - xx);
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    xx++;
                }
            }

            #region Group By

            for (int i = 0; i <= 3; i++)
            {
                crmRptReportTemplatePoints.SetParameterValue(i, "");
                if (groupByStructList.Count > i)
                {
                    if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    {
                        string strGroup = groupByStructList[i].ReportField.ToString();
                        crmRptReportTemplatePoints.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    }
                    else
                    {

                        switch (groupByStructList[i].ValueDataType.ToString())
                        {
                            case "System.String":
                                crmRptReportTemplatePoints.SetParameterValue(i, "");
                                break;
                            case "System.DateTime":
                                crmRptReportTemplatePoints.SetParameterValue(i, "02-02-2013");
                                break;
                            case "System.bool":
                                crmRptReportTemplatePoints.SetParameterValue(i, true);
                                break;
                            case "System.Int32":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            case "System.Int64":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            default:

                                break;
                        }
                    }
                }


            }
            #endregion

            reportViewer.crRptViewer.ReportSource = crmRptReportTemplatePoints;
                #endregion

            break;

                #region  RptLocationWiseLoyaltyAnalysis
                case "RptLocationWiseLoyaltyAnalysis":

            crmRptReportTemplatePoints = new CrmRptReportTemplatePoints();
            crmRptReportTemplatePoints.SetDataSource(dtReportData);
            crmRptReportTemplatePoints.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summery";
            crmRptReportTemplatePoints.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            ///////////////////////////////////////////////////////////////////////////
            // Use some other way to add values into formula fields DateFrom and  DateTo ()
            //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
            //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
            ///////////////////////////////////////////////////////////////////////////
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

            strFieldName = string.Empty;
            xx = 0;
            for (int i = 0; i < reportDataStructList.Count; i++)
            {
                strFieldName = "st" + (i + 1);
                if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                {
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                }

                if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                {

                    strFieldName = "st" + (14 - xx);
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    xx++;
                }
            }

            #region Group By

            for (int i = 0; i <= 3; i++)
            {
                crmRptReportTemplatePoints.SetParameterValue(i, "");
                if (groupByStructList.Count > i)
                {
                    if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    {
                        string strGroup = groupByStructList[i].ReportField.ToString();
                        crmRptReportTemplatePoints.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    }
                    else
                    {

                        switch (groupByStructList[i].ValueDataType.ToString())
                        {
                            case "System.String":
                                crmRptReportTemplatePoints.SetParameterValue(i, "");
                                break;
                            case "System.DateTime":
                                crmRptReportTemplatePoints.SetParameterValue(i, "02-02-2013");
                                break;
                            case "System.bool":
                                crmRptReportTemplatePoints.SetParameterValue(i, true);
                                break;
                            case "System.Int32":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            case "System.Int64":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            default:

                                break;
                        }
                    }
                }


            }
            #endregion

            reportViewer.crRptViewer.ReportSource = crmRptReportTemplatePoints;
                #endregion

            break;

                #region  RptMembershipUpgradesAnalysis
                case "RptMembershipUpgradesAnalysis":

               crmRptReportTemplatePoints = new CrmRptReportTemplatePoints();
            crmRptReportTemplatePoints.SetDataSource(dtReportData);
            crmRptReportTemplatePoints.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summery";
            crmRptReportTemplatePoints.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            ///////////////////////////////////////////////////////////////////////////
            // Use some other way to add values into formula fields DateFrom and  DateTo ()
            //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
            //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
            ///////////////////////////////////////////////////////////////////////////
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

            strFieldName = string.Empty;
            xx = 0;
            for (int i = 0; i < reportDataStructList.Count; i++)
            {
                strFieldName = "st" + (i + 1);
                if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                {
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                }

                if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                {

                    strFieldName = "st" + (14 - xx);
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    xx++;
                }
            }

            #region Group By

            for (int i = 0; i <= 3; i++)
            {
                crmRptReportTemplatePoints.SetParameterValue(i, "");
                if (groupByStructList.Count > i)
                {
                    if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    {
                        string strGroup = groupByStructList[i].ReportField.ToString();
                        crmRptReportTemplatePoints.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    }
                    else
                    {

                        switch (groupByStructList[i].ValueDataType.ToString())
                        {
                            case "System.String":
                                crmRptReportTemplatePoints.SetParameterValue(i, "");
                                break;
                            case "System.DateTime":
                                crmRptReportTemplatePoints.SetParameterValue(i, "02-02-2013");
                                break;
                            case "System.bool":
                                crmRptReportTemplatePoints.SetParameterValue(i, true);
                                break;
                            case "System.Int32":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            case "System.Int64":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            default:

                                break;
                        }
                    }
                }


            }
            #endregion

            reportViewer.crRptViewer.ReportSource = crmRptReportTemplatePoints;
                #endregion

            break;

                #region  RptLostAndRenewalCardAnalysis
                case "RptLostAndRenewalCardAnalysis":

            crmRptReportTemplatePoints = new CrmRptReportTemplatePoints();
            crmRptReportTemplatePoints.SetDataSource(dtReportData);
            crmRptReportTemplatePoints.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summery";
            crmRptReportTemplatePoints.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            ///////////////////////////////////////////////////////////////////////////
            // Use some other way to add values into formula fields DateFrom and  DateTo ()
            //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
            //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
            ///////////////////////////////////////////////////////////////////////////
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

            strFieldName = string.Empty;
            xx = 0;
            for (int i = 0; i < reportDataStructList.Count; i++)
            {
                strFieldName = "st" + (i + 1);
                if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                {
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                }

                if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                {

                    strFieldName = "st" + (14 - xx);
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    xx++;
                }
            }

            #region Group By

            for (int i = 0; i <= 3; i++)
            {
                crmRptReportTemplatePoints.SetParameterValue(i, "");
                if (groupByStructList.Count > i)
                {
                    if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    {
                        string strGroup = groupByStructList[i].ReportField.ToString();
                        crmRptReportTemplatePoints.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    }
                    else
                    {

                        switch (groupByStructList[i].ValueDataType.ToString())
                        {
                            case "System.String":
                                crmRptReportTemplatePoints.SetParameterValue(i, "");
                                break;
                            case "System.DateTime":
                                crmRptReportTemplatePoints.SetParameterValue(i, "02-02-2013");
                                break;
                            case "System.bool":
                                crmRptReportTemplatePoints.SetParameterValue(i, true);
                                break;
                            case "System.Int32":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            case "System.Int64":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            default:

                                break;
                        }
                    }
                }


            }
            #endregion

            reportViewer.crRptViewer.ReportSource = crmRptReportTemplatePoints;
                #endregion

            break;

                #region  RptFreeCardIssueDetails
                case "RptFreeCardIssueDetails":

               crmRptReportTemplatePoints = new CrmRptReportTemplatePoints();
            crmRptReportTemplatePoints.SetDataSource(dtReportData);
            crmRptReportTemplatePoints.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summery";
            crmRptReportTemplatePoints.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            ///////////////////////////////////////////////////////////////////////////
            // Use some other way to add values into formula fields DateFrom and  DateTo ()
            //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
            //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
            ///////////////////////////////////////////////////////////////////////////
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

            strFieldName = string.Empty;
            xx = 0;
            for (int i = 0; i < reportDataStructList.Count; i++)
            {
                strFieldName = "st" + (i + 1);
                if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                {
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                }

                if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                {

                    strFieldName = "st" + (14 - xx);
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    xx++;
                }
            }

            #region Group By

            for (int i = 0; i <= 3; i++)
            {
                crmRptReportTemplatePoints.SetParameterValue(i, "");
                if (groupByStructList.Count > i)
                {
                    if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    {
                        string strGroup = groupByStructList[i].ReportField.ToString();
                        crmRptReportTemplatePoints.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    }
                    else
                    {

                        switch (groupByStructList[i].ValueDataType.ToString())
                        {
                            case "System.String":
                                crmRptReportTemplatePoints.SetParameterValue(i, "");
                                break;
                            case "System.DateTime":
                                crmRptReportTemplatePoints.SetParameterValue(i, "02-02-2013");
                                break;
                            case "System.bool":
                                crmRptReportTemplatePoints.SetParameterValue(i, true);
                                break;
                            case "System.Int32":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            case "System.Int64":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            default:

                                break;
                        }
                    }
                }


            }
            #endregion

            reportViewer.crRptViewer.ReportSource = crmRptReportTemplatePoints;
                #endregion

            break;

                #region  RptLocationWiseLoyaltySummary
                case "RptLocationWiseLoyaltySummary":

              crmRptReportTemplatePoints = new CrmRptReportTemplatePoints();
            crmRptReportTemplatePoints.SetDataSource(dtReportData);
            crmRptReportTemplatePoints.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summery";
            crmRptReportTemplatePoints.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            ///////////////////////////////////////////////////////////////////////////
            // Use some other way to add values into formula fields DateFrom and  DateTo ()
            //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
            //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
            ///////////////////////////////////////////////////////////////////////////
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

            strFieldName = string.Empty;
            xx = 0;
            for (int i = 0; i < reportDataStructList.Count; i++)
            {
                strFieldName = "st" + (i + 1);
                if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                {
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                }

                if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                {

                    strFieldName = "st" + (14 - xx);
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    xx++;
                }
            }

            #region Group By

            for (int i = 0; i <= 3; i++)
            {
                crmRptReportTemplatePoints.SetParameterValue(i, "");
                if (groupByStructList.Count > i)
                {
                    if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    {
                        string strGroup = groupByStructList[i].ReportField.ToString();
                        crmRptReportTemplatePoints.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    }
                    else
                    {

                        switch (groupByStructList[i].ValueDataType.ToString())
                        {
                            case "System.String":
                                crmRptReportTemplatePoints.SetParameterValue(i, "");
                                break;
                            case "System.DateTime":
                                crmRptReportTemplatePoints.SetParameterValue(i, "02-02-2013");
                                break;
                            case "System.bool":
                                crmRptReportTemplatePoints.SetParameterValue(i, true);
                                break;
                            case "System.Int32":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            case "System.Int64":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            default:

                                break;
                        }
                    }
                }


            }
            #endregion

            reportViewer.crRptViewer.ReportSource = crmRptReportTemplatePoints;
                #endregion

            break;

                #region  RptMonthVisitCustomerWise
                case "RptMonthVisitCustomerWise ":

               crmRptReportTemplatePoints = new CrmRptReportTemplatePoints();
            crmRptReportTemplatePoints.SetDataSource(dtReportData);
            crmRptReportTemplatePoints.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summery";
            crmRptReportTemplatePoints.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            ///////////////////////////////////////////////////////////////////////////
            // Use some other way to add values into formula fields DateFrom and  DateTo ()
            //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
            //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
            ///////////////////////////////////////////////////////////////////////////
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

            strFieldName = string.Empty;
            xx = 0;
            for (int i = 0; i < reportDataStructList.Count; i++)
            {
                strFieldName = "st" + (i + 1);
                if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                {
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                }

                if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                {

                    strFieldName = "st" + (14 - xx);
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    xx++;
                }
            }

            #region Group By

            for (int i = 0; i <= 3; i++)
            {
                crmRptReportTemplatePoints.SetParameterValue(i, "");
                if (groupByStructList.Count > i)
                {
                    if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    {
                        string strGroup = groupByStructList[i].ReportField.ToString();
                        crmRptReportTemplatePoints.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    }
                    else
                    {

                        switch (groupByStructList[i].ValueDataType.ToString())
                        {
                            case "System.String":
                                crmRptReportTemplatePoints.SetParameterValue(i, "");
                                break;
                            case "System.DateTime":
                                crmRptReportTemplatePoints.SetParameterValue(i, "02-02-2013");
                                break;
                            case "System.bool":
                                crmRptReportTemplatePoints.SetParameterValue(i, true);
                                break;
                            case "System.Int32":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            case "System.Int64":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            default:

                                break;
                        }
                    }
                }


            }
            #endregion

            reportViewer.crRptViewer.ReportSource = crmRptReportTemplatePoints;
                #endregion

            break;

                #region  RptMemberShipUpgradeProposalReport
                case "RptMemberShipUpgradeProposalReport":

              crmRptReportTemplatePoints = new CrmRptReportTemplatePoints();
            crmRptReportTemplatePoints.SetDataSource(dtReportData);
            crmRptReportTemplatePoints.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summery";
            crmRptReportTemplatePoints.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            ///////////////////////////////////////////////////////////////////////////
            // Use some other way to add values into formula fields DateFrom and  DateTo ()
            //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
            //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
            ///////////////////////////////////////////////////////////////////////////
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

            strFieldName = string.Empty;
            xx = 0;
            for (int i = 0; i < reportDataStructList.Count; i++)
            {
                strFieldName = "st" + (i + 1);
                if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                {
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                }

                if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                {

                    strFieldName = "st" + (14 - xx);
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    xx++;
                }
            }

            #region Group By

            for (int i = 0; i <= 3; i++)
            {
                crmRptReportTemplatePoints.SetParameterValue(i, "");
                if (groupByStructList.Count > i)
                {
                    if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    {
                        string strGroup = groupByStructList[i].ReportField.ToString();
                        crmRptReportTemplatePoints.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    }
                    else
                    {

                        switch (groupByStructList[i].ValueDataType.ToString())
                        {
                            case "System.String":
                                crmRptReportTemplatePoints.SetParameterValue(i, "");
                                break;
                            case "System.DateTime":
                                crmRptReportTemplatePoints.SetParameterValue(i, "02-02-2013");
                                break;
                            case "System.bool":
                                crmRptReportTemplatePoints.SetParameterValue(i, true);
                                break;
                            case "System.Int32":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            case "System.Int64":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            default:

                                break;
                        }
                    }
                }


            }
            #endregion

            reportViewer.crRptViewer.ReportSource = crmRptReportTemplatePoints;
                #endregion

            break;

                #region  RptCardInventory
                case "RptCardInventory":

               crmRptReportTemplatePoints = new CrmRptReportTemplatePoints();
            crmRptReportTemplatePoints.SetDataSource(dtReportData);
            crmRptReportTemplatePoints.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summery";
            crmRptReportTemplatePoints.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptReportTemplatePoints.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            ///////////////////////////////////////////////////////////////////////////
            // Use some other way to add values into formula fields DateFrom and  DateTo ()
            //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
            //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
            ///////////////////////////////////////////////////////////////////////////
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
            //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

            strFieldName = string.Empty;
            xx = 0;
            for (int i = 0; i < reportDataStructList.Count; i++)
            {
                strFieldName = "st" + (i + 1);
                if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                {
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                }

                if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                {

                    strFieldName = "st" + (14 - xx);
                    crmRptReportTemplatePoints.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    xx++;
                }
            }

            #region Group By

            for (int i = 0; i <= 3; i++)
            {
                crmRptReportTemplatePoints.SetParameterValue(i, "");
                if (groupByStructList.Count > i)
                {
                    if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    {
                        string strGroup = groupByStructList[i].ReportField.ToString();
                        crmRptReportTemplatePoints.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    }
                    else
                    {

                        switch (groupByStructList[i].ValueDataType.ToString())
                        {
                            case "System.String":
                                crmRptReportTemplatePoints.SetParameterValue(i, "");
                                break;
                            case "System.DateTime":
                                crmRptReportTemplatePoints.SetParameterValue(i, "02-02-2013");
                                break;
                            case "System.bool":
                                crmRptReportTemplatePoints.SetParameterValue(i, true);
                                break;
                            case "System.Int32":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            case "System.Int64":
                                crmRptReportTemplatePoints.SetParameterValue(i, 0);
                                break;
                            default:

                                break;
                        }
                    }
                }


            }
            #endregion

            reportViewer.crRptViewer.ReportSource = crmRptReportTemplatePoints;
                #endregion

            break;



            default:
            break;
            }
            

            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
        }
 
        public FrmReprotGenerator OrganizeFormFields(AutoGenerateInfo autoGenerateInfo)
        {
            FrmReprotGenerator frmReprotGenerator;
            List<Common.ReportDataStruct> reportDatStructList = new List<Common.ReportDataStruct>();
            // , IsJoinField = true , IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsColumnTotal = true, IsRowTotal = true
            switch (autoGenerateInfo.FormName)
            {
                #region Loyalty Customer
                case "FrmLoyaltyCustomer":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "CustomerCode", ReportDataType = typeof(string), DbColumnName = "CustomerCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "CustomerName", ReportDataType = typeof(string), DbColumnName = "CustomerName", ValueDataType = typeof(string), IsSelectionField = true, IsRecordFilterByGivenOption = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Name On Card", ReportDataType = typeof(string), DbColumnName = "NameOnCard", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Card No", ReportDataType = typeof(string), DbColumnName = "CardNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "NIC Number", ReportDataType = typeof(string), DbColumnName = "NicNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Address", ReportDataType = typeof(string), DbColumnName = "Address1", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Mobile", ReportDataType = typeof(string), DbColumnName = "Mobile", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Telephone", ReportDataType = typeof(string), DbColumnName = "Telephone", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Reference No", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Civil Status", ReportDataType = typeof(string), DbColumnName = "CivilStatus", DbJoinColumnName = "LookupValue", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Gender", ReportDataType = typeof(string), DbColumnName = "Gender", DbJoinColumnName = "LookupValue", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = "Email", ReportDataType = typeof(string), DbColumnName = "Email", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString13", ReportFieldName = "Fax", ReportDataType = typeof(string), DbColumnName = "Fax", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString14", ReportFieldName = "Date of Birth", ReportDataType = typeof(string), DbColumnName = "DateOfBirth", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString15", ReportFieldName = "Card Issued", ReportDataType = typeof(string), DbColumnName = "CardIssued", ValueDataType = typeof(bool), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString16", ReportFieldName = "Issued On", ReportDataType = typeof(string), DbColumnName = "IssuedOn", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString17", ReportFieldName = "Expiry Date", ReportDataType = typeof(string), DbColumnName = "ExpiryDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString18", ReportFieldName = "Renewed On", ReportDataType = typeof(string), DbColumnName = "RenewedOn", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString19", ReportFieldName = "Customer Since", ReportDataType = typeof(string), DbColumnName = "CustomerSince", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString20", ReportFieldName = "Card Name", ReportDataType = typeof(string), DbColumnName = "CardMasterID", DbJoinColumnName = "CardName", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString21", ReportFieldName = "Occupation", ReportDataType = typeof(string), DbColumnName = "Occupation", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString22", ReportFieldName = "Work Address", ReportDataType = typeof(string), DbColumnName = "WorkAddres1", ValueDataType = typeof(string), IsSelectionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString23", ReportFieldName = "Work Telephone", ReportDataType = typeof(string), DbColumnName = "WorkTelephone", ValueDataType = typeof(string), IsSelectionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString24", ReportFieldName = "Work Mobile", ReportDataType = typeof(string), DbColumnName = "WorkMobile", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString25", ReportFieldName = "Work Email", ReportDataType = typeof(string), DbColumnName = "WorkEmail", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString26", ReportFieldName = "Work Fax", ReportDataType = typeof(string), DbColumnName = "WorkFax", ValueDataType = typeof(string), IsSelectionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString27", ReportFieldName = "Nationality", ReportDataType = typeof(string), DbColumnName = "Nationality", DbJoinColumnName = "LookupValue", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString28", ReportFieldName = "Religion", ReportDataType = typeof(string), DbColumnName = "Religion", DbJoinColumnName = "LookupValue", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString29", ReportFieldName = "Race", ReportDataType = typeof(string), DbColumnName = "Race", DbJoinColumnName = "LookupValue", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString30", ReportFieldName = "Active Status", ReportDataType = typeof(string), DbColumnName = "Active", ValueDataType = typeof(bool), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString31", ReportFieldName = "Active Date", ReportDataType = typeof(string), DbColumnName = "AcitiveDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString32", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "CreatedDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString33", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsConditionNameJoined = true, IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString34", ReportFieldName = "Total Points", ReportDataType = typeof(string), DbColumnName = "CPoints", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString35", ReportFieldName = "Earned Points", ReportDataType = typeof(string), DbColumnName = "EPoints", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString36", ReportFieldName = "Redeemed Points", ReportDataType = typeof(string), DbColumnName = "RPoints", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString37", ReportFieldName = "Suspended Status", ReportDataType = typeof(string), DbColumnName = "IsSuspended", ValueDataType = typeof(bool), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString38", ReportFieldName = "Black Listed Status", ReportDataType = typeof(string), DbColumnName = "IsBlackListed", ValueDataType = typeof(bool), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString39", ReportFieldName = "Credit Allowed Status", ReportDataType = typeof(string), DbColumnName = "IsCreditAllowed", ValueDataType = typeof(bool), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString40", ReportFieldName = "Updates Via Email Status", ReportDataType = typeof(string), DbColumnName = "SendUpdatesViaEmail", ValueDataType = typeof(bool), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString41", ReportFieldName = "Updates Via Sms Status", ReportDataType = typeof(string), DbColumnName = "SendUpdatesViaSms", ValueDataType = typeof(bool), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString42", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,12,0,4);
                    return frmReprotGenerator;
                #endregion

                #region Loyalty Customer Register
                case "RptCRMRegister":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                     reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "CustomerCode", ReportDataType = typeof(string), DbColumnName = "CustomerCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "CustomerName", ReportDataType = typeof(string), DbColumnName = "CustomerName", ValueDataType = typeof(string), IsSelectionField = true, IsRecordFilterByGivenOption = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Name On Card", ReportDataType = typeof(string), DbColumnName = "NameOnCard", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Card No", ReportDataType = typeof(string), DbColumnName = "CardNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "NIC Number", ReportDataType = typeof(string), DbColumnName = "NicNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Address", ReportDataType = typeof(string), DbColumnName = "Address1", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Mobile", ReportDataType = typeof(string), DbColumnName = "Mobile", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Telephone", ReportDataType = typeof(string), DbColumnName = "Telephone", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Reference No", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Civil Status", ReportDataType = typeof(string), DbColumnName = "CivilStatus", DbJoinColumnName = "LookupValue", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Gender", ReportDataType = typeof(string), DbColumnName = "Gender", DbJoinColumnName = "LookupValue", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = "Email", ReportDataType = typeof(string), DbColumnName = "Email", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString13", ReportFieldName = "Fax", ReportDataType = typeof(string), DbColumnName = "Fax", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString14", ReportFieldName = "Date of Birth", ReportDataType = typeof(string), DbColumnName = "DateOfBirth", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString15", ReportFieldName = "Card Issued", ReportDataType = typeof(string), DbColumnName = "CardIssued", ValueDataType = typeof(bool), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString16", ReportFieldName = "Issued On", ReportDataType = typeof(string), DbColumnName = "IssuedOn", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString17", ReportFieldName = "Expiry Date", ReportDataType = typeof(string), DbColumnName = "ExpiryDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString18", ReportFieldName = "Renewed On", ReportDataType = typeof(string), DbColumnName = "RenewedOn", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString19", ReportFieldName = "Customer Since", ReportDataType = typeof(string), DbColumnName = "CustomerSince", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString20", ReportFieldName = "Card Name", ReportDataType = typeof(string), DbColumnName = "CardMasterID", DbJoinColumnName = "CardName", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString21", ReportFieldName = "Occupation", ReportDataType = typeof(string), DbColumnName = "Occupation", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsRecordFilterByGivenOption = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString22", ReportFieldName = "Work Address", ReportDataType = typeof(string), DbColumnName = "WorkAddres1", ValueDataType = typeof(string), IsSelectionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString23", ReportFieldName = "Work Telephone", ReportDataType = typeof(string), DbColumnName = "WorkTelephone", ValueDataType = typeof(string), IsSelectionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString24", ReportFieldName = "Work Mobile", ReportDataType = typeof(string), DbColumnName = "WorkMobile", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString25", ReportFieldName = "Work Email", ReportDataType = typeof(string), DbColumnName = "WorkEmail", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString26", ReportFieldName = "Work Fax", ReportDataType = typeof(string), DbColumnName = "WorkFax", ValueDataType = typeof(string), IsSelectionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString27", ReportFieldName = "Nationality", ReportDataType = typeof(string), DbColumnName = "Nationality", DbJoinColumnName = "LookupValue", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString28", ReportFieldName = "Religion", ReportDataType = typeof(string), DbColumnName = "Religion", DbJoinColumnName = "LookupValue", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString29", ReportFieldName = "Race", ReportDataType = typeof(string), DbColumnName = "Race", DbJoinColumnName = "LookupValue", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString30", ReportFieldName = "Active Status", ReportDataType = typeof(string), DbColumnName = "Active", ValueDataType = typeof(bool), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString31", ReportFieldName = "Active Date", ReportDataType = typeof(string), DbColumnName = "AcitiveDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString32", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "CreatedDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString33", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsConditionNameJoined = true, IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString34", ReportFieldName = "Total Points", ReportDataType = typeof(string), DbColumnName = "CPoints", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString35", ReportFieldName = "Earned Points", ReportDataType = typeof(string), DbColumnName = "EPoints", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString36", ReportFieldName = "Redeemed Points", ReportDataType = typeof(string), DbColumnName = "RPoints", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString37", ReportFieldName = "Suspended Status", ReportDataType = typeof(string), DbColumnName = "IsSuspended", ValueDataType = typeof(bool), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString38", ReportFieldName = "Black Listed Status", ReportDataType = typeof(string), DbColumnName = "IsBlackListed", ValueDataType = typeof(bool), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString39", ReportFieldName = "Credit Allowed Status", ReportDataType = typeof(string), DbColumnName = "IsCreditAllowed", ValueDataType = typeof(bool), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString40", ReportFieldName = "Updates Via Email Status", ReportDataType = typeof(string), DbColumnName = "SendUpdatesViaEmail", ValueDataType = typeof(bool), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString41", ReportFieldName = "Updates Via Sms Status", ReportDataType = typeof(string), DbColumnName = "SendUpdatesViaSms", ValueDataType = typeof(bool), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString42", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString43", ReportFieldName = "Abeyance Date", ReportDataType = typeof(string), DbColumnName = "AbeyanceDate", IsConditionField = true, ValueDataType = typeof(DateTime), IsJoinField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString44", ReportFieldName = "Using Status", ReportDataType = typeof(string), DbColumnName = "UsingStatus", IsConditionField = true, ValueDataType = typeof(string), IsJoinField = true });

                    //frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 7, 7, 7);
                    return frmReprotGenerator;
                #endregion

                #region RptCustomerHistory
                case "RptCustomerHistory":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Application Date", ReportDataType = typeof(string), DbColumnName = "CreatedDate", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Cust Code", ReportDataType = typeof(string), DbColumnName = "CustomerCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Card No", ReportDataType = typeof(string), DbColumnName = "CardNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Cust Name", ReportDataType = typeof(string), DbColumnName = "CustomerName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Nic No", ReportDataType = typeof(string), DbColumnName = "NicNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Card Issued", ReportDataType = typeof(string), DbColumnName = "CardIssued", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Sent Date", ReportDataType = typeof(string), DbColumnName = "IssueDate", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Sent By", ReportDataType = typeof(string), DbColumnName = "EmployeeName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Designation", ReportDataType = typeof(string), DbColumnName = "EmployeeTitle", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = "Card Type", ReportDataType = typeof(string), DbColumnName = "CardName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal) });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                #region RptBranchWiseCustomerDetails
                case "RptBranchWiseCustomerDetails":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Customer Code", ReportDataType = typeof(string), DbColumnName = "LoyaltyCustomerID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Card No", ReportDataType = typeof(string), DbColumnName = "CardNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsColumnTotal = true, IsRowTotal = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Name", ReportDataType = typeof(string), DbColumnName = "CustomerName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsColumnTotal = true, IsRowTotal = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Location Code", ReportDataType = typeof(string), DbColumnName = "LocationID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Location Name", ReportDataType = typeof(string), DbColumnName = "LocationID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Gender", ReportDataType = typeof(string), DbColumnName = "Gender", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Age", ReportDataType = typeof(string), DbColumnName = "Age", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "NIC number", ReportDataType = typeof(string), DbColumnName = "NicNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Telephone", ReportDataType = typeof(string), DbColumnName = "Telephone", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Mobile", ReportDataType = typeof(string), DbColumnName = "Mobile", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Office Telephone", ReportDataType = typeof(string), DbColumnName = "WorkTelephone", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = "Total Points", ReportDataType = typeof(string), DbColumnName = "CPoints", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString13", ReportFieldName = "Customer Since", ReportDataType = typeof(string), DbColumnName = "CustomerSince", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });

                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal) });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                #region RptInActiveCustomerDetails
                case "RptInActiveCustomerDetails":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Customer Code", ReportDataType = typeof(string), DbColumnName = "CustomerCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Customer Name", ReportDataType = typeof(string), DbColumnName = "CustomerName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "CardNo", ReportDataType = typeof(string), DbColumnName = "CardNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "CreatedDate", ReportDataType = typeof(DateTime), DbColumnName = "CreatedDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "NicNo", ReportDataType = typeof(string), DbColumnName = "NicNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Address1", ReportDataType = typeof(string), DbColumnName = "Address1", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Mobile", ReportDataType = typeof(string), DbColumnName = "Mobile", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "LocationName", ReportDataType = typeof(string), DbColumnName = "LocationName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal) });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                #region RptCustomervisitdetails
                case "RptCustomervisitdetails":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Customer Code", ReportDataType = typeof(string), DbColumnName = "CustomerID", ValueDataType = typeof(long ), IsJoinField = true, IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Customer Name", ReportDataType = typeof(string), DbColumnName = "CustomerID", ValueDataType = typeof(long ), IsJoinField =true , IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "LocationName", ReportDataType = typeof(string), DbColumnName = "LocationID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Receipt", ReportDataType = typeof(string), DbColumnName = "Receipt", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Amount", ReportDataType = typeof(decimal), DbColumnName = "Amount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "Points", ReportDataType = typeof(decimal), DbColumnName = "Points", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Card Status", ReportDataType = typeof(string), DbColumnName = "TransID", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Count", ReportDataType = typeof(decimal), DbColumnName = "Count", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal) });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                #region RptLocationwiseloyaltysummary
                case "RptLocationwiseloyaltysummary":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Customer Code", ReportDataType = typeof(string), DbColumnName = "CustomerCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Customer Name", ReportDataType = typeof(string), DbColumnName = "CustomerName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "CardNo", ReportDataType = typeof(string), DbColumnName = "CardNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "CreatedDate", ReportDataType = typeof(string), DbColumnName = "CreatedDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "NicNo", ReportDataType = typeof(string), DbColumnName = "NicNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Address1", ReportDataType = typeof(string), DbColumnName = "Address1", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Mobile", ReportDataType = typeof(string), DbColumnName = "Mobile", ValueDataType = typeof(string), IsSelectionField = true, });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "LocationName", ReportDataType = typeof(string), DbColumnName = "LocationName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });

                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal) });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                    #endregion      

                #region RptCustomerBehavior
                case "RptCustomerBehavior":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location Code", ReportDataType = typeof(string), DbColumnName = "LocationID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Location Name", ReportDataType = typeof(string), DbColumnName = "LocationID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Card Code", ReportDataType = typeof(string), DbColumnName = "CardCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Card Name", ReportDataType = typeof(string), DbColumnName = "CardName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Points", ReportDataType = typeof(decimal), DbColumnName = "Points", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "Swiped Count", ReportDataType = typeof(decimal), DbColumnName = "Count", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });

                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal) });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                #region RptNumberOfVisitsCustomerWise
                case "RptNumberOfVisitsCustomerWise":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Customer Code", ReportDataType = typeof(string), DbColumnName = "CustomerID", ValueDataType = typeof(long),IsJoinField =true , IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Customer Name", ReportDataType = typeof(string), DbColumnName = "CustomerID", ValueDataType = typeof(long),IsJoinField=true , IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Card No", ReportDataType = typeof(string), DbColumnName = "LocationId", ValueDataType = typeof(long), IsJoinField =true , IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Mobile", ReportDataType = typeof(string), DbColumnName = "Mobile", ValueDataType = typeof(long) , IsSelectionField = true, IsConditionField = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "Days Visited", ReportDataType = typeof(decimal), DbColumnName = "Receipt", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Total Swipe", ReportDataType = typeof(decimal), DbColumnName = "Amount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                                     
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal) });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                #region RptCashierWiseLoyaltySummary
                case "RptCashierWiseLoyaltySummary":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Cashier Code", ReportDataType = typeof(string), DbColumnName = "SalesPersonCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Cashier Name", ReportDataType = typeof(string), DbColumnName = "SalesPersonName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "R Points", ReportDataType = typeof(decimal), DbColumnName = "RPoints", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "E Points", ReportDataType = typeof(decimal), DbColumnName = "EPoints", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Cust Count", ReportDataType = typeof(decimal), DbColumnName = "CustomerID", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                                       
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal) });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                #region RptLocationWiseSummary
                case "RptLocationWiseSummary":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Cashier Code", ReportDataType = typeof(string), DbColumnName = "EmployeeCode", DbJoinColumnName = "EmployeeCode", ValueDataType = typeof(string), IsGroupBy = true,IsJoinField=true, IsSelectionField = true, IsConditionField = false });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Cashier Name", ReportDataType = typeof(string), DbColumnName = "EmployeeID", DbJoinColumnName = "EmployeeName", ValueDataType = typeof(long), IsGroupBy = true, IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Loca.Code", ReportDataType = typeof(string), DbColumnName = "LocationCode", DbJoinColumnName = "LocationCode", ValueDataType = typeof(string), IsGroupBy = true, IsSelectionField = true, IsConditionField = false });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Loca.Name", ReportDataType = typeof(string),DbColumnName = "LocationId", DbJoinColumnName = "LocationName", ValueDataType = typeof(long), IsGroupBy = true, IsJoinField=true ,IsSelectionField = true, IsConditionField = true });

                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "PointsDiff", ReportDataType = typeof(decimal), DbColumnName = "RPoints", ValueDataType = typeof(decimal), IsSelectionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal8", ReportFieldName = "PointsRedeem%", ReportDataType = typeof(decimal), DbColumnName = "EPoints", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecima17", ReportFieldName = "Tot.Points", ReportDataType = typeof(decimal), DbColumnName = "Totcount", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "RCount", ReportDataType = typeof(decimal), DbColumnName = "Rcount", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "ECount", ReportDataType = typeof(decimal), DbColumnName = "Ecount", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Points Dif", ReportDataType = typeof(decimal), DbColumnName = "Pointsdiff", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecima13", ReportFieldName = "E Points", ReportDataType = typeof(decimal), DbColumnName = "ep", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "R Points", ReportDataType = typeof(decimal), DbColumnName = "rp", ValueDataType = typeof(decimal), IsSelectionField = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                    // sanjeewa 
                #region RptCustomerStatement
                case "RptCustomerStatement":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Customer", ReportDataType = typeof(string), DbColumnName = "CustomerID", ValueDataType = typeof(long), IsJoinField = true , IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Card Number", ReportDataType = typeof(string), DbColumnName = "CardNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Bill Number", ReportDataType = typeof(string), DbColumnName = "Receipt", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "NIC Number", ReportDataType = typeof(string), DbColumnName = "NicNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Mobile", ReportDataType = typeof(string), DbColumnName = "Mobile", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", ValueDataType = typeof(long), IsJoinField = true , IsSelectionField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "LoyaltyCustomerID", ReportDataType = typeof(string), DbColumnName = "LoyaltyCustomerID", ValueDataType = typeof(long), IsJoinField = true});
                    

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Amount", ReportDataType = typeof(decimal), DbColumnName = "Amount", ValueDataType = typeof(decimal) , IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "E Points", ReportDataType = typeof(decimal), DbColumnName = "EPoints", ValueDataType = typeof(decimal) , IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "R Points", ReportDataType = typeof(decimal), DbColumnName = "RPoints", ValueDataType = typeof(decimal) , IsSelectionField = true });
              
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                #region RptBestcustomerdetails
                case "RptBestcustomerdetails":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Customer Code", ReportDataType = typeof(string), DbColumnName = "CustomerID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Customer Name", ReportDataType = typeof(string), DbColumnName = "CustomerName", ValueDataType = typeof(string),IsSelectionField=true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "CardNo", ReportDataType = typeof(string), DbColumnName = "CardNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "NicNo", ReportDataType = typeof(string), DbColumnName = "NicNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Mobile", ReportDataType = typeof(string), DbColumnName = "Mobile", ValueDataType = typeof(string), IsSelectionField = true,IsConditionField =true  });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "DocumentDate", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsJoinField = true, IsConditionField = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "R Points", ReportDataType = typeof(decimal), DbColumnName = "Amount", ValueDataType = typeof(decimal),IsSelectionField=true, IsColumnTotal =true  });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "E Points", ReportDataType = typeof(decimal), DbColumnName = "Points", ValueDataType = typeof(decimal),IsColumnTotal =true  });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Cust Count", ReportDataType = typeof(decimal), DbColumnName = "Ecount", ValueDataType = typeof(decimal) ,IsColumnTotal =true  });

                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                #region RptLocationWiseLoyaltyAnalysis
                case "RptLocationWiseLoyaltyAnalysis":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Cashier Code", ReportDataType = typeof(string), DbColumnName = "SalesPersonCode", ValueDataType = typeof(string), IsJoinField = true, IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Cashier Name", ReportDataType = typeof(string), DbColumnName = "SalesPersonName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "R Points", ReportDataType = typeof(decimal), DbColumnName = "RPoints", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "E Points", ReportDataType = typeof(decimal), DbColumnName = "EPoints", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Cust Count", ReportDataType = typeof(decimal), DbColumnName = "CustomerID", ValueDataType = typeof(decimal), IsSelectionField = true });

                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal) });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                #region RptMembershipUpgradesAnalysis
                case "RptMembershipUpgradesAnalysis":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Customer Code", ReportDataType = typeof(string), DbColumnName = "CustomerID", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Customer Name", ReportDataType = typeof(string), DbColumnName = "CustomerName", ValueDataType = typeof(string),IsSelectionField=true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "CardNo", ReportDataType = typeof(string), DbColumnName = "CardNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "NicNo", ReportDataType = typeof(string), DbColumnName = "NicNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "CustomerSince", ReportDataType = typeof(string), DbColumnName = "CustomerSince", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "CardName", ReportDataType = typeof(string), DbColumnName = "CustomerSince", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "DocumentDate", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsJoinField = true, IsConditionField = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "PurchaseAmount", ReportDataType = typeof(decimal), DbColumnName = "Amount", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "visit", ReportDataType = typeof(decimal), DbColumnName = "visit", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                #region RptLostAndRenewalCardAnalysis
                case "RptLostAndRenewalCardAnalysis":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Cust Code", ReportDataType = typeof(string), DbColumnName = "CustomerID", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Name", ReportDataType = typeof(string), DbColumnName = "CustomerID", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true,IsJoinField =true  });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "CardNo", ReportDataType = typeof(string), DbColumnName = "CardNo", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "NIC", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Reason", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsConditionField = false });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "App.Date", ReportDataType = typeof(string), DbColumnName = "RenewedOn", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "LocationName", ReportDataType = typeof(string), DbColumnName = "LocationID", ValueDataType = typeof(Int32),IsJoinField = true, IsConditionField = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                #region RptFreeCardIssueDetails
                case "RptFreeCardIssueDetails":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Cashier Code", ReportDataType = typeof(string), DbColumnName = "SalesPersonCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Cashier Name", ReportDataType = typeof(string), DbColumnName = "SalesPersonName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "R Points", ReportDataType = typeof(decimal), DbColumnName = "RPoints", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "E Points", ReportDataType = typeof(decimal), DbColumnName = "EPoints", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Cust Count", ReportDataType = typeof(decimal), DbColumnName = "CustomerID", ValueDataType = typeof(decimal), IsSelectionField = true });

                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal) });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                #region RptLocationWiseLoyaltySummary
                case "RptLocationWiseLoyaltySummary":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Cashier Code", ReportDataType = typeof(string), DbColumnName = "SalesPersonCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Cashier Name", ReportDataType = typeof(string), DbColumnName = "SalesPersonName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "R Points", ReportDataType = typeof(decimal), DbColumnName = "RPoints", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "E Points", ReportDataType = typeof(decimal), DbColumnName = "EPoints", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Cust Count", ReportDataType = typeof(decimal), DbColumnName = "CustomerID", ValueDataType = typeof(decimal), IsSelectionField = true });

                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal) });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                #region RptMonthVisitCustomerWise
                case "RptMonthVisitCustomerWise":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Cashier Code", ReportDataType = typeof(string), DbColumnName = "SalesPersonCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Cashier Name", ReportDataType = typeof(string), DbColumnName = "SalesPersonName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "R Points", ReportDataType = typeof(decimal), DbColumnName = "RPoints", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "E Points", ReportDataType = typeof(decimal), DbColumnName = "EPoints", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Cust Count", ReportDataType = typeof(decimal), DbColumnName = "CustomerID", ValueDataType = typeof(decimal), IsSelectionField = true });

                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal) });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                #region RptMemberShipUpgradeProposalReport
                case "RptMemberShipUpgradeProposalReport":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Cashier Code", ReportDataType = typeof(string), DbColumnName = "SalesPersonCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Cashier Name", ReportDataType = typeof(string), DbColumnName = "SalesPersonName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "R Points", ReportDataType = typeof(decimal), DbColumnName = "RPoints", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "E Points", ReportDataType = typeof(decimal), DbColumnName = "EPoints", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Cust Count", ReportDataType = typeof(decimal), DbColumnName = "CustomerID", ValueDataType = typeof(decimal), IsSelectionField = true });

                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal) });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                #region RptCardInventory
                case "RptCardInventory":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Cashier Code", ReportDataType = typeof(string), DbColumnName = "SalesPersonCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Cashier Name", ReportDataType = typeof(string), DbColumnName = "SalesPersonName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "R Points", ReportDataType = typeof(decimal), DbColumnName = "RPoints", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "E Points", ReportDataType = typeof(decimal), DbColumnName = "EPoints", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Cust Count", ReportDataType = typeof(decimal), DbColumnName = "CustomerID", ValueDataType = typeof(decimal), IsSelectionField = true });

                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal) });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                    return frmReprotGenerator;
                #endregion

                default:
                    return null;
            }            
        }

        // LOAD DATA FRO COMBO
        public ArrayList GetSelectionData(Common.ReportDataStruct reportDatStruct, AutoGenerateInfo autoGenerateInfo)
        {
            switch (autoGenerateInfo.FormName)
            {
                case "FrmLoyaltyCustomer":
                    LoyaltyCustomerService loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLC(reportDatStruct);
                case "RptCRMRegister":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLC(reportDatStruct);
                case "RptCustomerHistory":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLT(reportDatStruct);
                case "RptInActiveCustomerDetails":
                     loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLCInActive(reportDatStruct);
                case "RptBranchWiseCustomerDetails":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLT(reportDatStruct);
                case "RptCustomervisitdetails":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLT(reportDatStruct);
                case "RptBestcustomerdetails":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLT(reportDatStruct);
                case "RptCustomerBehavior":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLT(reportDatStruct);
                case "RptCashierWiseLoyaltySummary":
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    return invSalesPersonService.GetSelectionData(reportDatStruct, autoGenerateInfo);
                case "RptNumberOfVisitsCustomerWise":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLT(reportDatStruct);
                case "RptLocationWiseSummary":
                    invSalesPersonService = new InvSalesPersonService();
                    return invSalesPersonService.GetSelectionData(reportDatStruct, autoGenerateInfo);
                case "RptCustomerStatement":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLT(reportDatStruct);
 
                case "RptLocationWiseLoyaltyAnalysis":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLT(reportDatStruct);
                case "RptMembershipUpgradesAnalysis":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLT(reportDatStruct);
                case "RptLostAndRenewalCardAnalysis":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLT(reportDatStruct);
                case "RptFreeCardIssueDetails":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLT(reportDatStruct);
                case "RptLocationWiseLoyaltySummary":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLT(reportDatStruct);
                case "RptMonthVisitCustomerWise":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLC(reportDatStruct);
                case "RptMemberShipUpgradeProposalReport":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetSelectionDataLC(reportDatStruct);
                case "RptCardInventory":
                    InvProductPriceChangeService invProductPriceChangeService = new InvProductPriceChangeService();
                    return invProductPriceChangeService.GetSelectionDataLC(reportDatStruct);

                default:
                    return null;
            }
        }

        public DataTable GetResultData(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            switch (autoGenerateInfo.FormName)
            {
                case "FrmLoyaltyCustomer":
                    LoyaltyCustomerService loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetLoyaltyCustomerDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                case "RptCRMRegister":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetLoyaltyCustomerRegiserDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                case "RptCustomerHistory":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetCustomerHistoryDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptBranchWiseCustomerDetails":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetBranchWiseCustomerDetailsDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                case "RptInActiveCustomerDetails":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetInActiveCustomerDetailsDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptCustomervisitdetails":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetCustomervisitsDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptNumberOfVisitsCustomerWise":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetNumberOfVisitsCustomerWiseDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptBestcustomerdetails":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetBestCustomersDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptCustomerBehavior":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetCustomerBehaviorDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptCashierWiseLoyaltySummary":
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    return invSalesPersonService.GetCashierLoyaltysDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptLocationWiseSummary":
                    invSalesPersonService = new InvSalesPersonService();
                    return invSalesPersonService.GetLocationWiseSummaryDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptCustomerStatement":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetCustomerStatementDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptBestCustomerDetails":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetCustomerBehaviorDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptLocationWiseLoyaltyAnalysis":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetCustomervisitsDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptMembershipUpgradesAnalysis":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetRptMembershipUpgradesAnalysisDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptLostAndRenewalCardAnalysis":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetLostAndRenewalDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptFreeCardIssueDetails":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetCustomerBehaviorDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptLocationWiseLoyaltySummary":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetCustomerBehaviorDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptMonthVisitCustomerWise":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetCustomerBehaviorDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptMemberShipUpgradeProposalReport ":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetCustomerBehaviorDataTable(reportConditionsDataStructList, reportDataStructList);
                case "RptCardInventory":
                    loyaltyCustomerervice = new LoyaltyCustomerService();
                    return loyaltyCustomerervice.GetCustomerBehaviorDataTable(reportConditionsDataStructList, reportDataStructList);

               
                default:
 
                    return null;
            }
        }


        public string GetConditionValue(Common.ReportDataStruct reportDataStruct, string dataValue)
        {
            string conditionValue = string.Empty;
            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LocationID":
                    LocationService locationService = new LocationService();
                    if (reportDataStruct.DbJoinColumnName == "LocationName")
                    {conditionValue = locationService.GetLocationsByName(dataValue.Trim()).LocationName.ToString();}
                    else if (reportDataStruct.DbJoinColumnName == "LocationCode")
                    {conditionValue = locationService.GetLocationsByCode(dataValue.Trim()).LocationCode.ToString();}
                    else
                    { conditionValue = locationService.GetLocationsByID(int.Parse(dataValue.Trim())).LocationID.ToString(); }

                    break;
                //case "DocumentDate":
                //     LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                //    conditionValue = loyaltyCustomerService.GetLoyaltyTransactionByDate(Common.ConvertStringToDateTime(dataValue)).DocumentDate.ToString();
                //    break;
                case "CustomerID":
                     LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

                     if (reportDataStruct.DbJoinColumnName == "CustomerName")
                     { conditionValue = loyaltyCustomerService.GetLoyaltyCustomerByName(dataValue.Trim()).CustomerName.ToString(); }
                     else if (reportDataStruct.DbJoinColumnName == "CustomerCode")
                     { conditionValue = loyaltyCustomerService.GetLoyaltyCustomerByCode(dataValue.Trim()).CustomerCode.ToString(); }
                     else
                     { conditionValue = loyaltyCustomerService.GetLoyaltyCustomerById(int.Parse(dataValue.Trim())).LoyaltyCustomerID.ToString(); }
                    break;
                case "CardType":
                    CardMasterService cardMasterService = new CardMasterService();
                    if (reportDataStruct.DbJoinColumnName == "CardName")
                    { conditionValue = cardMasterService.GetCardMasterByName(dataValue.Trim()).CardName.ToString(); }
                    else if (reportDataStruct.DbJoinColumnName == "CardCode")
                    { conditionValue = cardMasterService.GetCardMasterByCode(dataValue.Trim()).CardCode.ToString(); }
                    else
                    { conditionValue = cardMasterService.GetCardMasterById(int.Parse(dataValue.Trim())).CardMasterID.ToString(); }
                    break;
                //case "SupplierID":
                //    SupplierService supplierService = new SupplierService();
                //    conditionValue = supplierService.GetSupplierByCode(dataValue.Trim()).SupplierID.ToString();
                //    break;
                //case "ProductID":
                //    InvProductMasterService invProductMasterService = new InvProductMasterService();
                //    conditionValue = invProductMasterService.GetProductsByCode(dataValue.Trim()).InvProductMasterID.ToString();
                //    break;
                //case "InvPurchaseHeaderID":
                //    AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                //    autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote");
                //    InvPurchaseService invPurchaseService = new InvPurchaseService();
                //    conditionValue = invPurchaseService.GetInvPurchaseHeaderByDocumentNo(autoGenerateInfo.DocumentID, dataValue.Trim(), Common.LoggedLocationID).InvPurchaseHeaderID.ToString();
                //    break;
                default:
                    conditionValue = dataValue.Trim();
                    break;
            }
            return conditionValue;
        }

 
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

        private CrmRptGroupedDetailsTemplate ViewGroupedReport(CrmRptGroupedDetailsTemplate crmRptGroupedDetails, DataTable dtReportData, DataTable dtReportConditions, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
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
                        crmRptGroupedDetails.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStruct.ReportFieldName.Trim() + "'";
                        sr++;
                        groupingFields = string.IsNullOrEmpty(groupingFields) ? (reportDataStruct.ReportFieldName.Trim()) : (groupingFields + "/ " + reportDataStruct.ReportFieldName.Trim());
                    }

                    if (reportDataStruct.ReportDataType.Equals(typeof(decimal)))
                    {
                        strFieldName = "st" + dc;
                        crmRptGroupedDetails.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStruct.ReportFieldName.Trim() + "'";
                        dc++;
                    }
                }
            }

            #endregion

            // Re arrange data table header columns for report
            dtArrangedReportData = dtReportData.Copy();
            dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

            crmRptGroupedDetails.SetDataSource(dtArrangedReportData);
            crmRptGroupedDetails.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
            crmRptGroupedDetails.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
            crmRptGroupedDetails.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptGroupedDetails.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptGroupedDetails.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptGroupedDetails.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptGroupedDetails.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
            crmRptGroupedDetails.DataDefinition.FormulaFields["GroupNames"].Text = "'" + (string.IsNullOrEmpty(groupingFields) ? "" : groupingFields) + "'";

            #region Group By

            // Set report group field values
            for (int i = 0; i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count(); i++)
            {
                if (i < crmRptGroupedDetails.DataDefinition.Groups.Count)
                {
                    crmRptGroupedDetails.DataDefinition.Groups[i].ConditionField = crmRptGroupedDetails.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                }
            }

            // Set parameter select field values
            for (int i = 0; i < crmRptGroupedDetails.DataDefinition.Groups.Count; i++)
            {
                if (crmRptGroupedDetails.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                {
                    if (i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count())
                    {
                        crmRptGroupedDetails.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), true);
                    }
                    else
                    {
                        crmRptGroupedDetails.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                    }
                }
            }

            #endregion

            return crmRptGroupedDetails;
        }

        private CrmRptDetailsTemplate ViewUnGroupedReport(CrmRptDetailsTemplate crmRptDetailsTemplate, DataTable dtReportData, DataTable dtReportConditions, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
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
                        crmRptDetailsTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                        sr++;
                    }

                    if (item.ReportDataType.Equals(typeof(decimal)))
                    {
                        strFieldName = "st" + dc;
                        crmRptDetailsTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
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

            crmRptDetailsTemplate.SetDataSource(dtArrangedReportData);
            crmRptDetailsTemplate.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
            crmRptDetailsTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
            crmRptDetailsTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptDetailsTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            crmRptDetailsTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptDetailsTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptDetailsTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            // Set parameter sum fields
            for (int i = 0; i < crmRptDetailsTemplate.ParameterFields.Count; i++)
            {
                if (crmRptDetailsTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter) && newSumFieldsIndexesList.Contains(i + 1) && crmRptDetailsTemplate.ParameterFields[i].Name.StartsWith("prmSumFieldDecimal"))
                {
                    crmRptDetailsTemplate.SetParameterValue("prmSumFieldDecimal" + (i + 1).ToString(), true);
                }
                else
                {
                    crmRptDetailsTemplate.SetParameterValue("prmSumFieldDecimal" + (i + 1).ToString(), false);
                }
            }

            return crmRptDetailsTemplate;

        }

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
