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
using Report.Com.Reference.Reports;
using Utility;
using Service;
using Report.Inventory.Transactions.Reports;
using Report.Com.Transactions.Reports;
using CrystalDecisions.Shared;
using Report.Inventory;
using Report.Logistic;

namespace Report.Com
{
    public class ComReportGenerator
    {
        string strFieldName = string.Empty;

        public void GenearateReport(string documentNo, string reportName, bool isOrg, string[] stringField, DataTable reportData)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            Cursor.Current = Cursors.WaitCursor;

            ComRptReferenceDetailTemplate comRptReferenceDetailTemplate = new ComRptReferenceDetailTemplate();            
            // Bind report data
            comRptReferenceDetailTemplate.SetDataSource(reportData);

            // Assign formula and summery field values
            comRptReferenceDetailTemplate.SummaryInfo.ReportTitle = reportName;
            comRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            comRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            comRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
            comRptReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

            //  Set field text
            int x = 1;
            foreach (string strFieldName in stringField)
            {
                string st = "st" + x.ToString();
                comRptReferenceDetailTemplate.DataDefinition.FormulaFields["st" + x.ToString()].Text = "'" + strFieldName.Trim() + "'";
                x++;
            }

            reportViewer.crRptViewer.ReportSource = comRptReferenceDetailTemplate;
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

            ComRptReferenceDetailTemplate comRptReferenceDetailTemplate = new ComRptReferenceDetailTemplate();

            string[] stringField =  new string[] { };

            switch (autoGenerateInfo.FormName)
            {
                #region Driver
                case "FrmDriver": // Driver
                    DriverService driverService = new DriverService();
                    reportData = driverService.GetAllDriverDataTable();

                    // Set field text
                    stringField = new string[] { "Code", "Driver Name", "Address", "Telephone", "NIC", "Rec.Date" };
                    comRptReferenceDetailTemplate.SetDataSource(reportData);
                    // Assign formula and summery field values
                    comRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    comRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        comRptReferenceDetailTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = comRptReferenceDetailTemplate;
                    break;
                #endregion
                #region Helper
                case "FrmHelper": // Helper
                    HelperService helperService = new HelperService();
                    reportData = helperService.GetAllHelperDataTable();

                    // Set field text
                    stringField = new string[] { "Code", "Helper Name", "Address", "Telephone", "NIC", "Rec.Date" };
                    comRptReferenceDetailTemplate.SetDataSource(reportData);
                    // Assign formula and summery field values
                    comRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    comRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        comRptReferenceDetailTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = comRptReferenceDetailTemplate;
                    break;
                #endregion
                #region Vehicle
                case "FrmVehicle":
                    VehicleService vehicleService = new VehicleService();
                    reportData = vehicleService.GetAllVehicleDataTable();

                    // Set field text
                    stringField = new string[] { "Registration No", "Vehicle Name", "Engine No", "Chasses No", "Make", "Model", "Vehicle Type", "Fuel Type", "Weight", "Engine Capacity" , "Seating Capacity", "Rec.Date", "Remark" };
                    comRptReferenceDetailTemplate.SetDataSource(reportData);
                    // Assign formula and summery field values
                    comRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    comRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        comRptReferenceDetailTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = comRptReferenceDetailTemplate;
                    break;
                #endregion
                #region Location
                case "FrmLocation": // Location
                    LocationService locationService = new LocationService();
                    reportData = locationService.GetAllLocationDataTable();

                    // Set field text
                    stringField = new string[] { "Code", "Loca.Name", "Company", "Address", "Telephone", "Contact Person", "Other Business Name", "Date", "Costing Method", "Type of Business", "Cost Centre Name" };

                    comRptReferenceDetailTemplate.SetDataSource(reportData);
                    // Assign formula and summery field values
                    comRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    comRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        comRptReferenceDetailTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = comRptReferenceDetailTemplate;
                    break;
                #endregion
                #region Payment Method
                case "FrmPaymentMethod":
                    PaymentMethodService paymentMethodService = new PaymentMethodService();
                    reportData = paymentMethodService.GetAllPaymentMethodDataTable();

                    // Set field text
                    stringField = new string[] { "Code", "Method.Name", "Commission Rate"};

                    comRptReferenceDetailTemplate.SetDataSource(reportData);
                    // Assign formula and summery field values
                    comRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    comRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    for (int i = 0; i < stringField.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        comRptReferenceDetailTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = comRptReferenceDetailTemplate;
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
        /// 
        /// </summary>
        /// <param name="autoGenerateInfo"></param>
        public void GenearateReferenceSummaryReport(AutoGenerateInfo autoGenerateInfo, DataTable dtReportData, DataTable dtReportConditions, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> groupByStructList)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            ComRptReferenceDetailTemplate comRptReferenceDetailTemplate = new ComRptReferenceDetailTemplate();
            Cursor.Current = Cursors.WaitCursor;

            DataTable dtArrangedReportData = new DataTable();
            List<Common.ReportDataStruct> selectedReportStructList;
            List<Common.ReportDataStruct> selectedGroupStructList;

            string strFieldName = string.Empty;
            int sr = 1, dc = 12;
            int gr = 0, gp = 1;

            switch (autoGenerateInfo.FormName)
            {
                #region Driver
                case "FrmDriver":
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
                                comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
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

                    comRptReferenceDetailTemplate.SetDataSource(dtArrangedReportData);
                    //comRptReferenceDetailTemplate.SetDataSource(dtReportData);
                    comRptReferenceDetailTemplate.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    comRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    comRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

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
                    //            comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
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
                    //            comRptReferenceDetailTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //            comRptReferenceDetailTemplate.SetParameterValue(i, "");
                    //    }
                    //    { comRptReferenceDetailTemplate.SetParameterValue(i, ""); }
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
                            if (gr < comRptReferenceDetailTemplate.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    comRptReferenceDetailTemplate.DataDefinition.Groups[gr].ConditionField = comRptReferenceDetailTemplate.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp-1 < comRptReferenceDetailTemplate.DataDefinition.Groups.Count)
                            {
                                if (comRptReferenceDetailTemplate.ParameterFields[gp-1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < comRptReferenceDetailTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (comRptReferenceDetailTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < comRptReferenceDetailTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (comRptReferenceDetailTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = comRptReferenceDetailTemplate;
                    break;
               #endregion
                #region Helper
                case "FrmHelper":
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
                                comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
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

                    comRptReferenceDetailTemplate.SetDataSource(dtArrangedReportData);

                    //comRptReferenceDetailTemplate.SetDataSource(dtReportData);
                    comRptReferenceDetailTemplate.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    comRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    comRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

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
                    //            comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
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
                    //            comRptReferenceDetailTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //            comRptReferenceDetailTemplate.SetParameterValue(i, "");
                    //    }
                    //    { comRptReferenceDetailTemplate.SetParameterValue(i, ""); }
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
                            if (gr < comRptReferenceDetailTemplate.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    comRptReferenceDetailTemplate.DataDefinition.Groups[gr].ConditionField = comRptReferenceDetailTemplate.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp-1 < comRptReferenceDetailTemplate.DataDefinition.Groups.Count)
                            {
                                if (comRptReferenceDetailTemplate.ParameterFields[gp-1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < comRptReferenceDetailTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (comRptReferenceDetailTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < comRptReferenceDetailTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (comRptReferenceDetailTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = comRptReferenceDetailTemplate;
                    break;
                #endregion
                #region Vehicle
                case "FrmVehicle":
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
                                comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
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

                    comRptReferenceDetailTemplate.SetDataSource(dtArrangedReportData);

                    //comRptReferenceDetailTemplate.SetDataSource(dtReportData);
                    comRptReferenceDetailTemplate.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    comRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    comRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    #region Set Header
                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    if (reportDataStructList[i].IsSelectionField.Equals(true))
                    //    {
                    //        strFieldName = "st" + (i + 1);
                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //        {
                    //            comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
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
                    //            comRptReferenceDetailTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //            comRptReferenceDetailTemplate.SetParameterValue(i, "");
                    //    }
                    //    { comRptReferenceDetailTemplate.SetParameterValue(i, ""); }
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
                            if (gr < comRptReferenceDetailTemplate.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    comRptReferenceDetailTemplate.DataDefinition.Groups[gr].ConditionField = comRptReferenceDetailTemplate.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp-1 < comRptReferenceDetailTemplate.DataDefinition.Groups.Count)
                            {
                                if (comRptReferenceDetailTemplate.ParameterFields[gp-1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < comRptReferenceDetailTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (comRptReferenceDetailTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < comRptReferenceDetailTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (comRptReferenceDetailTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = comRptReferenceDetailTemplate;
                    break;
                #endregion
                #region Location
                case "FrmLocation":
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
                                comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
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

                    comRptReferenceDetailTemplate.SetDataSource(dtArrangedReportData);

                    //comRptReferenceDetailTemplate.SetDataSource(dtReportData);
                    comRptReferenceDetailTemplate.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    comRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    comRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    //string strFieldName = string.Empty;
                    #region Set Header - old
                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    if (reportDataStructList[i].IsSelectionField.Equals(true))
                    //    {
                    //        strFieldName = "st" + (i + 1);
                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //        {
                    //            comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }
                    //    }
                    //}
                    #endregion

                    #region Group By - old

                    //for (int i = 0; i <= 3; i++)
                    //{
                    //    comRptReferenceDetailTemplate.SetParameterValue(i, "");

                    //    if (groupByStructList.Count > i)
                    //    {
                    //        if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    //        {
                    //            string strGroup = groupByStructList[i].ReportField.ToString();
                    //            comRptReferenceDetailTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //        {comRptReferenceDetailTemplate.SetParameterValue(i, "");}
                    //    }
                    //    //{ comRptReferenceDetailTemplate.SetParameterValue(i, ""); }
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
                            if (gr < comRptReferenceDetailTemplate.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    comRptReferenceDetailTemplate.DataDefinition.Groups[gr].ConditionField = comRptReferenceDetailTemplate.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp-1 < comRptReferenceDetailTemplate.DataDefinition.Groups.Count)
                            {
                                if (comRptReferenceDetailTemplate.ParameterFields[gp-1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < comRptReferenceDetailTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (comRptReferenceDetailTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < comRptReferenceDetailTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (comRptReferenceDetailTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = comRptReferenceDetailTemplate;
                    break;
                #endregion
                #region Payment Method
                case "FrmPaymentMethod":
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
                                comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
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

                    comRptReferenceDetailTemplate.SetDataSource(dtArrangedReportData);
                    //comRptReferenceDetailTemplate.SetDataSource(dtReportData);
                    comRptReferenceDetailTemplate.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    comRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " List";
                    comRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    comRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    //string strFieldName = string.Empty;
                    #region Set Header - old
                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    if (reportDataStructList[i].IsSelectionField.Equals(true))
                    //    {
                    //        strFieldName = "st" + (i + 1);
                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //        {
                    //            comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (12 - i);
                    //            comRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
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
                    //            comRptReferenceDetailTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //        { comRptReferenceDetailTemplate.SetParameterValue(i, ""); }
                    //    }
                    //    else
                    //    { comRptReferenceDetailTemplate.SetParameterValue(i, ""); }
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
                            if (gr < comRptReferenceDetailTemplate.DataDefinition.Groups.Count)
                            {
                                if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                {
                                    comRptReferenceDetailTemplate.DataDefinition.Groups[gr].ConditionField = comRptReferenceDetailTemplate.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())];
                                    gr++;
                                }
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < selectedReportStructList.Count(); i++)
                        {
                            if (gp-1 < comRptReferenceDetailTemplate.DataDefinition.Groups.Count)
                            {
                                if (comRptReferenceDetailTemplate.ParameterFields[gp-1].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                                {
                                    if (selectedGroupStructList.ToList().Any(c => c.IsResultGroupBy.Equals(true) && c.DbColumnName == selectedReportStructList[i].DbColumnName.Trim()))
                                    {
                                        comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), true);
                                        gp++;
                                    }
                                    else
                                    {
                                        comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (gp).ToString(), false);
                                    }
                                }
                            }
                        }

                        for (int i = gp; i < comRptReferenceDetailTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (comRptReferenceDetailTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < comRptReferenceDetailTemplate.DataDefinition.Groups.Count; i++)
                        {
                            if (comRptReferenceDetailTemplate.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                comRptReferenceDetailTemplate.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = comRptReferenceDetailTemplate;
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
            FrmReprotGenerator frmReprotGenerator;

            string departmentText = "", categoryText = "", subCategoryText = "", subCategory2Text = "";
            departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
            categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
            subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
            subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;
            
            switch (autoGenerateInfo.FormName)
            {
                #region Driver
                case "FrmDriver":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Code", ReportDataType = typeof(string), DbColumnName = "DriverCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true});
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Driver Name", ReportDataType = typeof(string), DbColumnName = "DriverName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Address", ReportDataType = typeof(string), DbColumnName = "Address", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Telephone", ReportDataType = typeof(string), DbColumnName = "Telephone", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "NIC", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Rec.Date", ReportDataType = typeof(string), DbColumnName = "CreatedDate", ValueDataType = typeof(DateTime), IsSelectionField = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,14,0,4);
                    return frmReprotGenerator;
                #endregion
                #region Helper
                case "FrmHelper":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Code", ReportDataType = typeof(string), DbColumnName = "HelperCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Helper Name", ReportDataType = typeof(string), DbColumnName = "HelperName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Address", ReportDataType = typeof(string), DbColumnName = "Address", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Telephone", ReportDataType = typeof(string), DbColumnName = "Telephone", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "NIC", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Rec.Date", ReportDataType = typeof(string), DbColumnName = "CreatedDate", ValueDataType = typeof(DateTime), IsSelectionField = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList, 14, 0 ,4);
                    return frmReprotGenerator;
                #endregion
                #region Vehicle
                case "FrmVehicle":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Registration No", ReportDataType = typeof(string), DbColumnName = "RegistrationNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Vehicle Name", ReportDataType = typeof(string), DbColumnName = "VehicleName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Engine No", ReportDataType = typeof(string), DbColumnName = "EngineNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Chasses No", ReportDataType = typeof(string), DbColumnName = "ChassesNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Make", ReportDataType = typeof(string), DbColumnName = "Make", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Model", ReportDataType = typeof(string), DbColumnName = "Model", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Vehicle Type", ReportDataType = typeof(string), DbColumnName = "VehicleType", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Fuel Type", ReportDataType = typeof(string), DbColumnName = "FuelType", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Weight", ReportDataType = typeof(string), DbColumnName = "Weight", ValueDataType = typeof(int), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Engine Capacity", ReportDataType = typeof(string), DbColumnName = "EngineCapacity", ValueDataType = typeof(int), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Seating Capacity", ReportDataType = typeof(string), DbColumnName = "SeatingCapacity", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "CreatedDate", ValueDataType = typeof(DateTime), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString13", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,14,0,4);
                    return frmReprotGenerator;
                #endregion
                #region Location
                case "FrmLocation":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Code", ReportDataType = typeof(string), DbColumnName = "LocationCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true }); //, IsGroupBy = true
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Loca.Name", ReportDataType = typeof(string), DbColumnName = "LocationName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Company", ReportDataType = typeof(string), DbColumnName = "CompanyID", DbJoinColumnName = "CompanyName", IsConditionNameJoined = true, ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Address", ReportDataType = typeof(string), DbColumnName = "Address1", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Telephone", ReportDataType = typeof(string), DbColumnName = "Telephone", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Contact Person", ReportDataType = typeof(string), DbColumnName = "ContactPersonName", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Other Business Name", ReportDataType = typeof(string), DbColumnName = "OtherBusinessName", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "CreatedDate", ValueDataType = typeof(DateTime), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Costing Method", ReportDataType = typeof(string), DbColumnName = "CostingMethod", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Type of Business", ReportDataType = typeof(string), DbColumnName = "TypeOfBusiness", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Cost Centre Name", ReportDataType = typeof(string), DbColumnName = "CostCentreID", ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = "Prefix Code", ReportDataType = typeof(string), DbColumnName = "LocationPrefixCode", ValueDataType = typeof(string), IsJoinField = true, IsSelectionField = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,14,0,4);
                    return frmReprotGenerator;
                #endregion
                #region Payment Method
                case "FrmPaymentMethod":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Method Code", ReportDataType = typeof(string), DbColumnName = "PaymentMethodCode", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsMandatoryField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Method Name", ReportDataType = typeof(string), DbColumnName = "PaymentMethodName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "CreatedDate", ValueDataType = typeof(DateTime), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Commission Rate", ReportDataType = typeof(string), DbColumnName = "CommissionRate", ValueDataType = typeof(decimal), IsSelectionField = true });
                    frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,14,0,4);
                    return frmReprotGenerator;
                #endregion

                #region Opening Stock
                case "FrmOpeningStock":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Opening Stock Type", ReportDataType = typeof(string), DbColumnName = "OpeningStockType", DbJoinColumnName = "LookupValue", ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Ref. No", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                #region opening balances reports
                //case "RptOpeningBalanceRegister":
                //    reportDatStructList = new List<Common.ReportDataStruct>();
                //    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(string), IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                //    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = departmentText, ReportDataType = typeof(string), DbColumnName = "InvDepartmentID", DbJoinColumnName = "DepartmentName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true});
                //    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "InvCategoryID", DbJoinColumnName = "CategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                //    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "InvSubCategoryID", DbJoinColumnName = "SubCategoryName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                //    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = subCategory2Text, ReportDataType = typeof(string), DbColumnName = "InvSubCategory2ID", DbJoinColumnName = "SubCategory2Name", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                //    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductCode", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                //    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "InvProductMasterID", DbJoinColumnName = "ProductName", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true, IsJoinField = true, IsConditionNameJoined = true });
                //    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                //    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Opening Stock Type", ReportDataType = typeof(string), DbColumnName = "OpeningStockType", ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                //    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Opening Stock Type", ReportDataType = typeof(string), DbColumnName = "OpeningStockType", DbJoinColumnName = "LookupValue", ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                //    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                //    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                //    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                //    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                //    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Selling Value", ReportDataType = typeof(decimal), DbColumnName = "SellingValue", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    
                //    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

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
                case "FrmDriver":
                    DriverService driverService = new DriverService();
                    return driverService.GetSelectionData(reportDatStruct);
                case "FrmHelper":
                    HelperService helperService = new HelperService();
                    return helperService.GetSelectionData(reportDatStruct);
                case "FrmVehicle":
                    VehicleService vehicleService = new VehicleService();
                    return vehicleService.GetSelectionData(reportDatStruct);
                case "FrmLocation":
                    LocationService locationService = new LocationService();
                    return locationService.GetSelectionData(reportDatStruct);
                case "FrmPaymentMethod":
                    PaymentMethodService paymentMethodService = new PaymentMethodService();
                    return paymentMethodService.GetSelectionData(reportDatStruct);
                case "FrmOpeningStock":
                //case "RptOpeningBalanceRegister":
                    OpeningStockService openingStockService = new OpeningStockService();
                    return openingStockService.GetSelectionData(reportDatStruct, autoGenerateInfo);
                
                default:
                    return null;
            }
        }

        public DataTable GetResultData(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            switch (autoGenerateInfo.FormName)
            {
                case "FrmDriver":
                    DriverService driverService = new DriverService();
                    return driverService.GetAllDriverDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                case "FrmHelper":
                    HelperService helperService = new HelperService();
                    return helperService.GetAllHelperDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                case "FrmVehicle":
                    VehicleService vehicleService = new VehicleService();
                    return vehicleService.GetAllVehicleDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                case "FrmLocation":
                    LocationService locationService = new LocationService();
                    return locationService.GetAllLocationDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList);
                case "FrmPaymentMethod":
                    PaymentMethodService paymentMethodService = new PaymentMethodService();
                    return paymentMethodService.GetAllPaymentMethodDataTable(reportConditionsDataStructList, reportDataStructList, reportOrderByDataStructList);
                case "FrmOpeningStock":
                    OpeningStockService openingStockService = new OpeningStockService();
                    return openingStockService.GetOpeningStockDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);
                //case "RptOpeningBalanceRegister":
                //    OpeningStockService openingStockServiceReg = new OpeningStockService();
                //    return openingStockServiceReg.GetOpeningBalancesRegisterDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);
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
                    conditionValue = supplierService.GetSupplierByCode(dataValue.Trim()).SupplierID.ToString();
                    break;
                case "LookupKey":
                    LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                    string titleType = ((int)LookUpReference.TitleType).ToString();
                    conditionValue = lookUpReferenceService.GetLookUpReferenceByKey(titleType, int.Parse(titleType.Trim())).LookupKey.ToString();
                    break;
                case "PaymentMethodID":
                    PaymentMethodService paymentMethodService = new PaymentMethodService();
                    conditionValue = paymentMethodService.GetPaymentMethodsByName(dataValue.Trim()).PaymentMethodID.ToString();
                    break;
                case "CompanyID":
                    CompanyService companyService = new CompanyService();
                    if (!reportDataStruct.IsConditionNameJoined)
                    { conditionValue = companyService.GetCompaniesByName(dataValue.Trim()).CompanyID.ToString(); }
                    else
                    { conditionValue = companyService.GetCompaniesByName(dataValue.Trim()).CompanyName.ToString(); }
                    break;
                case "CostCentreID":
                    CostCentreService costCentreService = new CostCentreService();
                    conditionValue = costCentreService.GetCostCentresByName(dataValue.Trim()).CostCentreID.ToString();
                    break;
                case "InvDepartmentID":
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    bool isDepend = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").IsDepend;
                    if (!reportDataStruct.IsConditionNameJoined)
                    { conditionValue = invDepartmentService.GetInvDepartmentsByCode(dataValue.Trim(), isDepend).InvDepartmentID.ToString(); }
                    else
                    { conditionValue = invDepartmentService.GetInvDepartmentsByName(dataValue.Trim(), isDepend).DepartmentName.ToString(); }

                    break;
                case "InvCategoryID":
                    InvCategoryService invCategoryService = new InvCategoryService();
                    bool isDependDepartment = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend;
                    if (!reportDataStruct.IsConditionNameJoined)
                    { conditionValue = invCategoryService.GetInvCategoryByCode(dataValue.Trim(), isDependDepartment).InvCategoryID.ToString(); }
                    else
                    { conditionValue = invCategoryService.GetInvCategoryByName(dataValue.Trim(), isDependDepartment).CategoryName.ToString(); }
                    break;
                case "InvSubCategoryID":
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    bool isDependCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend;
                    if (!reportDataStruct.IsConditionNameJoined)
                    { conditionValue = invSubCategoryService.GetInvSubCategoryByCode(dataValue.Trim(), isDependCategory).InvCategoryID.ToString(); }
                    else
                    { conditionValue = invSubCategoryService.GetInvSubCategoryByName(dataValue.Trim(), isDependCategory).SubCategoryName.ToString(); }
                    break;
                case "InvSubCategory2ID":
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    if (!reportDataStruct.IsConditionNameJoined)
                    { conditionValue = invSubCategory2Service.GetInvSubCategory2ByCode(dataValue.Trim()).InvSubCategoryID.ToString(); }
                    else
                    { conditionValue = invSubCategory2Service.GetInvSubCategory2ByName(dataValue.Trim()).SubCategory2Name.ToString(); }
                    break;

                case "OpeningStockType":
                    LookUpReferenceService lookUpReferenceService2 = new LookUpReferenceService();
                    string titleType2 = ((int)LookUpReference.ModuleType).ToString();
                    conditionValue = lookUpReferenceService2.GetLookUpReferenceByValue(titleType2, dataValue.Trim()).LookupValue.ToString();
                    break;

                //case "LocationID":
                //    LocationService locationService = new LocationService();
                //    conditionValue = locationService.GetLocationsByCode(dataValue.Trim()).LocationID.ToString();
                //    break;
                //case "LocationId":
                //    locationService = new LocationService();
                //    conditionValue = locationService.GetLocationsByName(dataValue.Trim()).LocationID.ToString();
                //    break;
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

                case "FrmOpeningStock":
                    OpeningStockService openingStockService = new OpeningStockService();
                    //reportData = openingStockService.GetOpeningStockTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID, 1);

                    if (int.Equals(openingStockService.GetOpeningStockTransactionType(documentNo, documentSataus, autoGenerateInfo.DocumentID), 1))
                    {
                        InvReportGenerator invReportGenerator = new InvReportGenerator();
                        invReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo, printStatus);
                    }

                    if (int.Equals(openingStockService.GetOpeningStockTransactionType(documentNo, documentSataus, autoGenerateInfo.DocumentID), 2))
                    {
                        LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
                        lgsReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo, printStatus);
                    }

                    //string[] stringFieldOpst = { "Document", "Date", "Narration", "Remark", "", "", "", "", "Reference No", "Opening Stock Type", 
                    //                        "", "Location", "Pro.Code", "Pro.Name", "Unit", "Batch No", "Expiry Date", "Qty", "S.Price", "C.Price", "S.Value", 
                    //                        "C.Value", "Tot. S.Value", "Tot. C.Value", "", "", "", "" };

                    ////InvRptTransactionTemplateLandscape1 invRptTransactionTemplateOpst = new InvRptTransactionTemplateLandscape1();
                    //InvRptOpeningStockSmallPaper invRptTransactionTemplateOpst = new InvRptOpeningStockSmallPaper();
                    //invRptTransactionTemplateOpst.SetDataSource(reportData);

                    //// Assign formula and summery field values
                    //invRptTransactionTemplateOpst.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    //invRptTransactionTemplateOpst.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    //invRptTransactionTemplateOpst.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    //invRptTransactionTemplateOpst.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    //invRptTransactionTemplateOpst.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    //invRptTransactionTemplateOpst.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    //invRptTransactionTemplateOpst.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    //for (int i = 0; i < stringFieldOpst.Length; i++)
                    //{
                    //    string st = "st" + (i + 1);
                    //    invRptTransactionTemplateOpst.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldOpst[i].Trim() + "'";
                    //}
                    //reportViewer.crRptViewer.ReportSource = invRptTransactionTemplateOpst;
                    break;
                default:
                    break;
            }

            //reportViewer.WindowState = FormWindowState.Maximized;
            //reportViewer.Show();
            //Cursor.Current = Cursors.Default;
        }

        public void GenearateTransactionSummeryReport(AutoGenerateInfo autoGenerateInfo, DataTable dtReportData, DataTable dtReportConditions,  List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> groupByStructList, bool viewGroupDetails)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            Cursor.Current = Cursors.WaitCursor;
            string group1, group2, group3, group4;
            int yx = 0;
            switch (autoGenerateInfo.FormName)
            {
                //case "RptOpeningBalanceRegister":
                //    ComRptOpeningBalancesTemplateLandscape comRptOpeningBalancesTemplateLandscape = new ComRptOpeningBalancesTemplateLandscape();

                //    comRptOpeningBalancesTemplateLandscape.SetDataSource(dtReportData);
                //    comRptOpeningBalancesTemplateLandscape.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                //    comRptOpeningBalancesTemplateLandscape.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summery";
                //    comRptOpeningBalancesTemplateLandscape.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                //    comRptOpeningBalancesTemplateLandscape.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                //    comRptOpeningBalancesTemplateLandscape.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                //    comRptOpeningBalancesTemplateLandscape.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                //    comRptOpeningBalancesTemplateLandscape.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                //    comRptOpeningBalancesTemplateLandscape.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                //    strFieldName = string.Empty;
                //    int sr = 0;
                //    for (int i = 0; i < reportDataStructList.Count; i++)
                //    {
                //        strFieldName = "st" + (i + 1);
                //        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                //        {
                //            if (reportDataStructList[i].IsSelectionField)
                //            { comRptOpeningBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                //            else
                //            { comRptOpeningBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                //        }

                //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                //        {
                //            strFieldName = "st" + (11 + sr);
                //            if (reportDataStructList[i].IsSelectionField)
                //            { comRptOpeningBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                //            else
                //            { comRptOpeningBalancesTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                //            sr++;
                //        }
                //    }

                //    #region Group By

                //    for (int i = 0; i <= 10; i++)
                //    {
                //        if (comRptOpeningBalancesTemplateLandscape.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.StringParameter))
                //        {
                //            comRptOpeningBalancesTemplateLandscape.SetParameterValue(i, "");

                //            if (groupByStructList.Count > i)
                //            {
                //                if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                //                {
                //                    comRptOpeningBalancesTemplateLandscape.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                //                }
                //                else
                //                {
                //                    comRptOpeningBalancesTemplateLandscape.SetParameterValue(i, "");
                //                }
                //            }
                //        }
                //    }
                //    #endregion
                //    reportViewer.crRptViewer.ReportSource = comRptOpeningBalancesTemplateLandscape;
                //    break;

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
       
    }
}
