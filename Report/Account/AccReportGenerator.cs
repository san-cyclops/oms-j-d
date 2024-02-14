using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Domain;
using Report.Account.Transactions.Reports;
using Utility;
using Service;
using System.Collections;
using Report.Com;

namespace Report.Account
{
    public class AccReportGenerator
    {
        //public FrmReprotGenerator frmReprotGenerator { get; set; }

        string strFieldName = string.Empty;
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

            //InvRptColumn5Template invRptColumn5Template = new InvRptColumn5Template();
            string departmentText = "", categoryText = "", subCategoryText = "", subCategory2Text = "";

            string[] stringField = new string[] { };

            switch (autoGenerateInfo.FormName)
            {
                #region SalesPerson
                case "FrmSalesPerson":
                    //InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    //reportData = invSalesPersonService.GetAllInvSalesPersonsDataTable();

                    //// Set field text
                    //stringField = new string[] { "Code", "Sales Person", "Address", "Telephone", "NIC", "Rec.Date", "Type", "Remark" };
                    //invRptReferenceDetailTemplate.SetDataSource(reportData);
                    //// Assign formula and summery field values
                    //invRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    //invRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    //invRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    //invRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    //invRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    //invRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    ////cryComReferenceDetailTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + isOrg + "";

                    //for (int i = 0; i < stringField.Length; i++)
                    //{
                    //    string st = "st" + (i + 1);
                    //    invRptReferenceDetailTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringField[i].Trim() + "'";
                    //}
                    //reportViewer.crRptViewer.ReportSource = invRptReferenceDetailTemplate;
                    break;
                #endregion
                default:
                    break;
            }

            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
        }

        public void GenearateReferenceSummaryReport(AutoGenerateInfo autoGenerateInfo, DataTable dtReportData, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> groupByStructList)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            //InvRptColumn5Template invRptColumn5Template = new InvRptColumn5Template();
            Cursor.Current = Cursors.WaitCursor;
            string strFieldName = string.Empty;

            switch (autoGenerateInfo.FormName)
            {
                #region SalesPerson
                case "FrmSalesPerson":
                    //invRptReferenceDetailTemplate.SetDataSource(dtReportData);
                    //invRptReferenceDetailTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    //invRptReferenceDetailTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    //invRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    //invRptReferenceDetailTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    //invRptReferenceDetailTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    //invRptReferenceDetailTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    /////////////////////////////////////////////////////////////////////////////
                    //// Use some other way to add values into formula fields DateFrom and  DateTo ()
                    ////goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    ////goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    /////////////////////////////////////////////////////////////////////////////
                    ////cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    ////cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";


                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    strFieldName = "st" + (i + 1);
                    //    if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //    {
                    //        invRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //    }

                    //    if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //    {
                    //        strFieldName = "st" + (12 - i);
                    //        invRptReferenceDetailTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //    }
                    //}

                    //#region Group By

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
                    //#endregion
                    //reportViewer.crRptViewer.ReportSource = invRptReferenceDetailTemplate;
                    break;
                #endregion
                default:
                    break;
            }

            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
        }

        public void GenerateTransactionReport(AutoGenerateInfo autoGenerateInfo, string documentNo, int printStatus)
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
                #region Petty Cash
                case "FrmPettyCashReimbursement":
                    AccPettyCashReimbursementService accPettyCashReimbursementService = new AccPettyCashReimbursementService();
                    reportData = accPettyCashReimbursementService.GetPettyCashReimbursementTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldRI = { "Document", "Date", "Petty Cash", "Remark", "Employee", "Payee", "", "", "Reference No", "", 
                                            "", "Location", "Ledger", "", "Date", "Card No", "Imprest Amount", "Issued Amount", "", "", "", 
                                            "", "", "", "", "", "", "", "" };

                    AccRptTransactionTemplate accRptTransactionTemplate = new AccRptTransactionTemplate();
                    accRptTransactionTemplate.SetDataSource(reportData);
                    // Assign formula and summary field values
                    accRptTransactionTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    accRptTransactionTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    accRptTransactionTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    accRptTransactionTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    accRptTransactionTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    accRptTransactionTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    accRptTransactionTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldRI.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        accRptTransactionTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldRI[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = accRptTransactionTemplate;
                    break;

                case "FrmPettyCashIOU":
                    AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                    reportData = accPettyCashIOUService.GetPettyCashIOUTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldIOU = { "Document", "Date", "Petty Cash", "Remark", "Employee", "Payee", "", "", "Reference No", "", 
                                            "", "Location", "Ledger", "", "", "", "", "", "Value", "", "", 
                                            "", "Total Amt.", "", "", "", "", "", "" };

                    AccRptTransactionTemplate accRptPettyIOUTransactionTemplate = new AccRptTransactionTemplate();
                    accRptPettyIOUTransactionTemplate.SetDataSource(reportData);
                    // Assign formula and summery field values
                    accRptPettyIOUTransactionTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    accRptPettyIOUTransactionTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    accRptPettyIOUTransactionTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    accRptPettyIOUTransactionTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    accRptPettyIOUTransactionTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    accRptPettyIOUTransactionTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    accRptPettyIOUTransactionTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldIOU.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        accRptPettyIOUTransactionTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldIOU[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = accRptPettyIOUTransactionTemplate;
                    break;

                case "FrmPettyCashBillEntry":
                    AccPettyCashBillService accPettyCashBillService = new AccPettyCashBillService();
                    reportData = accPettyCashBillService.GetPettyCashBillTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldBill = { "Document", "Date", "Petty Cash", "Remark", "Employee", "Payee", "", "", "Reference No", "", 
                                            "", "Location", "Ledger", "", "", "", "", "", "Value", "", "", 
                                            "", "Total Amt.", "", "", "", "", "", "" };

                    AccRptTransactionTemplate accRptPettyBillTransactionTemplate = new AccRptTransactionTemplate();
                    accRptPettyBillTransactionTemplate.SetDataSource(reportData);
                    // Assign formula and summery field values
                    accRptPettyBillTransactionTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    accRptPettyBillTransactionTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    accRptPettyBillTransactionTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    accRptPettyBillTransactionTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    accRptPettyBillTransactionTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    accRptPettyBillTransactionTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    accRptPettyBillTransactionTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldBill.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        accRptPettyBillTransactionTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldBill[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = accRptPettyBillTransactionTemplate;
                    break;

                case "FrmPettyCashPayment":
                    AccPettyCashPaymentService accPettyCashPaymentService = new AccPettyCashPaymentService();
                    reportData = accPettyCashPaymentService.GetPettyCashPaymentTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldPettyPMT = { "Document", "Date", "Petty Cash", "Remark", "Employee", "Payee", "", "", "Reference No", "", 
                                            "", "Location", "Ledger", "", "", "", "", "", "Value", "", "", 
                                            "", "Total Amt.", "", "", "", "", "", "" };

                    AccRptTransactionTemplate accRptPettyPMTTransactionTemplate = new AccRptTransactionTemplate();
                    accRptPettyPMTTransactionTemplate.SetDataSource(reportData);
                    // Assign formula and summery field values
                    accRptPettyPMTTransactionTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    accRptPettyPMTTransactionTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    accRptPettyPMTTransactionTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    accRptPettyPMTTransactionTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    accRptPettyPMTTransactionTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    accRptPettyPMTTransactionTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    accRptPettyPMTTransactionTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldPettyPMT.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        accRptPettyPMTTransactionTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldPettyPMT[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = accRptPettyPMTTransactionTemplate;
                    break;
#endregion
                
                default:
                    break;
            }
            
            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
        }

        public void GenearateTransactionSummaryReport(AutoGenerateInfo autoGenerateInfo, DataTable dtReportData, DataTable dtReportConditions, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> groupByStructList, bool viewGroupDetails)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            Cursor.Current = Cursors.WaitCursor;

            DataTable dtArrangedReportData = new DataTable();

            string group1, group2, group3, group4;
            int yx = 0;
            switch (autoGenerateInfo.FormName)
            {
                #region accounts transactions

                #region Petty Cash Reimbursement
                case "FrmPettyCashReimbursement":
                    AccRptTransactionSummaryTemplateLandscape accRptTransactionSummaryTemplateLandscapeReimb = new AccRptTransactionSummaryTemplateLandscape();

                    accRptTransactionSummaryTemplateLandscapeReimb.SetDataSource(dtReportData);
                    accRptTransactionSummaryTemplateLandscapeReimb.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    accRptTransactionSummaryTemplateLandscapeReimb.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    accRptTransactionSummaryTemplateLandscapeReimb.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    accRptTransactionSummaryTemplateLandscapeReimb.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    accRptTransactionSummaryTemplateLandscapeReimb.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    accRptTransactionSummaryTemplateLandscapeReimb.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    accRptTransactionSummaryTemplateLandscapeReimb.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    accRptTransactionSummaryTemplateLandscapeReimb.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    strFieldName = string.Empty;
                    int srpos = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            if (reportDataStructList[i].IsSelectionField)
                            { accRptTransactionSummaryTemplateLandscapeReimb.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { accRptTransactionSummaryTemplateLandscapeReimb.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (8 + srpos);
                            if (reportDataStructList[i].IsSelectionField)
                            { accRptTransactionSummaryTemplateLandscapeReimb.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { accRptTransactionSummaryTemplateLandscapeReimb.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                            srpos++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 4; i++)
                    {
                        if (accRptTransactionSummaryTemplateLandscapeReimb.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.StringParameter))
                        {
                            accRptTransactionSummaryTemplateLandscapeReimb.SetParameterValue(i, "");

                            if (groupByStructList.Count > i)
                            {
                                if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                                {
                                    accRptTransactionSummaryTemplateLandscapeReimb.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                                }
                                else
                                {
                                    accRptTransactionSummaryTemplateLandscapeReimb.SetParameterValue(i, "");
                                }
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = accRptTransactionSummaryTemplateLandscapeReimb;


                    break;
                #endregion
                #region Petty Cash IOU
                case "FrmPettyCashIOU":
                    AccRptTransactionSummaryTemplateLandscape accRptTransactionSummaryTemplateLandscapeIOU = new AccRptTransactionSummaryTemplateLandscape();

                    accRptTransactionSummaryTemplateLandscapeIOU.SetDataSource(dtReportData);
                    accRptTransactionSummaryTemplateLandscapeIOU.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    accRptTransactionSummaryTemplateLandscapeIOU.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    accRptTransactionSummaryTemplateLandscapeIOU.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    accRptTransactionSummaryTemplateLandscapeIOU.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    accRptTransactionSummaryTemplateLandscapeIOU.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    accRptTransactionSummaryTemplateLandscapeIOU.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    accRptTransactionSummaryTemplateLandscapeIOU.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    accRptTransactionSummaryTemplateLandscapeIOU.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    strFieldName = string.Empty;
                    int srIOUs = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            if (reportDataStructList[i].IsSelectionField)
                            { accRptTransactionSummaryTemplateLandscapeIOU.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { accRptTransactionSummaryTemplateLandscapeIOU.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (8 + srIOUs);
                            if (reportDataStructList[i].IsSelectionField)
                            { accRptTransactionSummaryTemplateLandscapeIOU.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { accRptTransactionSummaryTemplateLandscapeIOU.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                            srIOUs++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 4; i++)
                    {
                        if (accRptTransactionSummaryTemplateLandscapeIOU.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.StringParameter))
                        {
                            accRptTransactionSummaryTemplateLandscapeIOU.SetParameterValue(i, "");

                            if (groupByStructList.Count > i)
                            {
                                if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                                {
                                    accRptTransactionSummaryTemplateLandscapeIOU.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                                }
                                else
                                {
                                    accRptTransactionSummaryTemplateLandscapeIOU.SetParameterValue(i, "");
                                }
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = accRptTransactionSummaryTemplateLandscapeIOU;


                    break;
                #endregion
                #region Petty Cash Bill Entry
                case "FrmPettyCashBillEntry":
                    AccRptTransactionSummaryTemplateLandscape accRptTransactionSummaryTemplateLandscapeBill = new AccRptTransactionSummaryTemplateLandscape();

                    accRptTransactionSummaryTemplateLandscapeBill.SetDataSource(dtReportData);
                    accRptTransactionSummaryTemplateLandscapeBill.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    accRptTransactionSummaryTemplateLandscapeBill.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    accRptTransactionSummaryTemplateLandscapeBill.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    accRptTransactionSummaryTemplateLandscapeBill.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    accRptTransactionSummaryTemplateLandscapeBill.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    accRptTransactionSummaryTemplateLandscapeBill.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    accRptTransactionSummaryTemplateLandscapeBill.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    accRptTransactionSummaryTemplateLandscapeBill.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    strFieldName = string.Empty;
                    int srBills = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            if (reportDataStructList[i].IsSelectionField)
                            { accRptTransactionSummaryTemplateLandscapeBill.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { accRptTransactionSummaryTemplateLandscapeBill.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (8 + srBills);
                            if (reportDataStructList[i].IsSelectionField)
                            { accRptTransactionSummaryTemplateLandscapeBill.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { accRptTransactionSummaryTemplateLandscapeBill.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                            srBills++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 4; i++)
                    {
                        if (accRptTransactionSummaryTemplateLandscapeBill.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.StringParameter))
                        {
                            accRptTransactionSummaryTemplateLandscapeBill.SetParameterValue(i, "");

                            if (groupByStructList.Count > i)
                            {
                                if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                                {
                                    accRptTransactionSummaryTemplateLandscapeBill.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                                }
                                else
                                {
                                    accRptTransactionSummaryTemplateLandscapeBill.SetParameterValue(i, "");
                                }
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = accRptTransactionSummaryTemplateLandscapeBill;


                    break;
                #endregion
                #region Petty Cash Payment
                case "FrmPettyCashPayment":
                    AccRptTransactionSummaryTemplateLandscape accRptTransactionSummaryTemplateLandscapePymt = new AccRptTransactionSummaryTemplateLandscape();

                    accRptTransactionSummaryTemplateLandscapePymt.SetDataSource(dtReportData);
                    accRptTransactionSummaryTemplateLandscapePymt.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    accRptTransactionSummaryTemplateLandscapePymt.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    accRptTransactionSummaryTemplateLandscapePymt.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    accRptTransactionSummaryTemplateLandscapePymt.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    accRptTransactionSummaryTemplateLandscapePymt.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    accRptTransactionSummaryTemplateLandscapePymt.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    accRptTransactionSummaryTemplateLandscapePymt.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    accRptTransactionSummaryTemplateLandscapePymt.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    strFieldName = string.Empty;
                    int srPymts = 0;
                    for (int i = 0; i < reportDataStructList.Count; i++)
                    {
                        strFieldName = "st" + (i + 1);
                        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                        {
                            if (reportDataStructList[i].IsSelectionField)
                            { accRptTransactionSummaryTemplateLandscapePymt.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { accRptTransactionSummaryTemplateLandscapePymt.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (8 + srPymts);
                            if (reportDataStructList[i].IsSelectionField)
                            { accRptTransactionSummaryTemplateLandscapePymt.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                            else
                            { accRptTransactionSummaryTemplateLandscapePymt.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                            srPymts++;
                        }
                    }

                    #region Group By

                    for (int i = 0; i <= 4; i++)
                    {
                        if (accRptTransactionSummaryTemplateLandscapePymt.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.StringParameter))
                        {
                            accRptTransactionSummaryTemplateLandscapePymt.SetParameterValue(i, "");

                            if (groupByStructList.Count > i)
                            {
                                if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                                {
                                    accRptTransactionSummaryTemplateLandscapePymt.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                                }
                                else
                                {
                                    accRptTransactionSummaryTemplateLandscapePymt.SetParameterValue(i, "");
                                }
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = accRptTransactionSummaryTemplateLandscapePymt;


                    break;
                #endregion
                #endregion

                #region Account reports
                #region Petty Cash Report
                case "RptPettyCash": //RptPettyCashExpenses
                    AccRptTransactionSummaryTemplate accountPettyCashExpensesTemplate = new AccRptTransactionSummaryTemplate();

                    accountPettyCashExpensesTemplate.SetDataSource(dtReportData);
                    accountPettyCashExpensesTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    accountPettyCashExpensesTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    accountPettyCashExpensesTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    accountPettyCashExpensesTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    accountPettyCashExpensesTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    accountPettyCashExpensesTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

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
                            accountPettyCashExpensesTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                        }

                        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                        {
                            strFieldName = "st" + (9 + yx);
                            accountPettyCashExpensesTemplate.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
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
                                accountPettyCashExpensesTemplate.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                            }
                            else
                            {
                                accountPettyCashExpensesTemplate.SetParameterValue(i, "");
                            }
                        }
                        else
                        {
                            accountPettyCashExpensesTemplate.SetParameterValue(i, "");
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = accountPettyCashExpensesTemplate;
                    break;
                #endregion
                #region Receipts Report
                case "RptReceiptsRegister":
                    AccRptReceiptTransactionSummaryTemplateLandscape accRptReceiptsTemplateReceiptsRegister = new AccRptReceiptTransactionSummaryTemplateLandscape();

                    #region Set Values for report header Fields
                    strFieldName = string.Empty;
                    int sr = 1, dc = 12;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {
                            if (item.ReportDataType.Equals(typeof(string)) && sr < 12)
                            {
                                strFieldName = "st" + sr;
                                accRptReceiptsTemplateReceiptsRegister.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                sr++;
                            }

                            if (item.ReportDataType.Equals(typeof(decimal)))
                            {
                                strFieldName = "st" + dc;
                                accRptReceiptsTemplateReceiptsRegister.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                dc++;
                            }
                        }
                    }

                    #endregion

                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    accRptReceiptsTemplateReceiptsRegister.SetDataSource(dtArrangedReportData);

                    //accRptReceiptsTemplateReceiptsRegister.SetDataSource(dtReportData);
                    accRptReceiptsTemplateReceiptsRegister.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);

                    accRptReceiptsTemplateReceiptsRegister.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    accRptReceiptsTemplateReceiptsRegister.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    accRptReceiptsTemplateReceiptsRegister.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    accRptReceiptsTemplateReceiptsRegister.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    accRptReceiptsTemplateReceiptsRegister.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    accRptReceiptsTemplateReceiptsRegister.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    accRptReceiptsTemplateReceiptsRegister.SetParameterValue("prmViewGroupDetails", viewGroupDetails);

                    //strFieldName = string.Empty;
                    //int sr = 0;
                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    strFieldName = "st" + (i + 1);
                    //    if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //    {
                    //        if (reportDataStructList[i].IsSelectionField)
                    //        { accRptReceiptsTemplateReceiptsRegister.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                    //        else
                    //        { accRptReceiptsTemplateReceiptsRegister.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                    //    }

                    //    if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //    {
                    //        strFieldName = "st" + (12 + sr);
                    //        if (reportDataStructList[i].IsSelectionField)
                    //        { accRptReceiptsTemplateReceiptsRegister.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'"; }
                    //        else
                    //        { accRptReceiptsTemplateReceiptsRegister.DataDefinition.FormulaFields[strFieldName].Text = "''"; }
                    //        sr++;
                    //    }
                    //}

                    #region Group By Old

                    //for (int i = 0; i <= 9; i++)
                    //{
                    //    accRptReceiptsTemplateReceiptsRegister.SetParameterValue(i, "");

                    //    if (groupByStructList.Count > i)
                    //    {
                    //        if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    //        {
                    //            accRptReceiptsTemplateReceiptsRegister.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //        {
                    //            accRptReceiptsTemplateReceiptsRegister.SetParameterValue(i, "");
                    //        }
                    //    }
                    //}
                    #endregion

                    #region Group By

                    if (groupByStructList.Any(g => g.IsResultGroupBy.Equals(true)))
                    {
                        // Set report group field values
                        for (int i = 0; i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count(); i++)
                        {
                            if (i < accRptReceiptsTemplateReceiptsRegister.DataDefinition.Groups.Count)
                            {
                                accRptReceiptsTemplateReceiptsRegister.DataDefinition.Groups[i].ConditionField = accRptReceiptsTemplateReceiptsRegister.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())]; //[string.Concat("FieldString", groupStrFieldCount)];
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < accRptReceiptsTemplateReceiptsRegister.DataDefinition.Groups.Count; i++)
                        {
                            if (accRptReceiptsTemplateReceiptsRegister.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                if (i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count())
                                {
                                    accRptReceiptsTemplateReceiptsRegister.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), true);
                                }
                                else
                                {
                                    accRptReceiptsTemplateReceiptsRegister.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < accRptReceiptsTemplateReceiptsRegister.DataDefinition.Groups.Count; i++)
                        {
                            if (accRptReceiptsTemplateReceiptsRegister.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                accRptReceiptsTemplateReceiptsRegister.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion

                    reportViewer.crRptViewer.ReportSource = accRptReceiptsTemplateReceiptsRegister;
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
       
        /// Organize Report Generator Fields
        public FrmReprotGenerator OrganizeFormFields(AutoGenerateInfo autoGenerateInfo)
        {
            List<Common.ReportDataStruct> reportDatStructList = new List<Common.ReportDataStruct>();            
            FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

            switch (autoGenerateInfo.FormName)
            {
                #region accounts transactions
                #region Petty Cash
                #region FrmPettyCashReimbursement
                case "FrmPettyCashReimbursement":
                    reportDatStructList = new List<Common.ReportDataStruct>();

                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", IsConditionNameJoined = true, ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Petty Cash Book", ReportDataType = typeof(string), DbColumnName = "PettyCashLedgerID", DbJoinColumnName = "LedgerName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Reference", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Amount", ReportDataType = typeof(decimal), DbColumnName = "ReimburseAmount", ValueDataType = typeof(decimal), IsSelectionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Disc. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "NBT(2%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount1", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "NBT(2.04%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount2", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "VAT", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount3", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                #region FrmPettyCashIOU
                case "FrmPettyCashIOU":
                    reportDatStructList = new List<Common.ReportDataStruct>();

                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", IsConditionNameJoined = true, ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Petty Cash Book", ReportDataType = typeof(string), DbColumnName = "PettyCashLedgerID", DbJoinColumnName = "LedgerName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Reference", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Amount", ReportDataType = typeof(decimal), DbColumnName = "ReimburseAmount", ValueDataType = typeof(decimal), IsSelectionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Disc. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "NBT(2%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount1", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "NBT(2.04%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount2", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "VAT", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount3", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                #region FrmPettyCashBillEntry
                case "FrmPettyCashBillEntry":
                    reportDatStructList = new List<Common.ReportDataStruct>();

                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", IsConditionNameJoined = true, ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Petty Cash Book", ReportDataType = typeof(string), DbColumnName = "PettyCashLedgerID", DbJoinColumnName = "LedgerName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Reference", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Amount", ReportDataType = typeof(decimal), DbColumnName = "ReimburseAmount", ValueDataType = typeof(decimal), IsSelectionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Disc. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "NBT(2%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount1", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "NBT(2.04%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount2", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "VAT", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount3", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion

                #region FrmPettyCashPayment
                case "FrmPettyCashPayment":
                    reportDatStructList = new List<Common.ReportDataStruct>();

                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", IsConditionNameJoined = true, ValueDataType = typeof(int), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "CreatedUser", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Petty Cash Book", ReportDataType = typeof(string), DbColumnName = "PettyCashLedgerID", DbJoinColumnName = "LedgerName", IsConditionNameJoined = true, ValueDataType = typeof(long), IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Reference", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Amount", ReportDataType = typeof(decimal), DbColumnName = "ReimburseAmount", ValueDataType = typeof(decimal), IsSelectionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Disc. Amt.", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "NBT(2%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount1", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "NBT(2.04%)", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount2", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "VAT", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount3", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion
                #endregion
                #endregion

                #region Account Detail reports
                #region Petty Cash
                case "RptPettyCash":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", ValueDataType = typeof(int), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DepartmentID", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = categoryText, ReportDataType = typeof(string), DbColumnName = "CategoryID", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = subCategoryText, ReportDataType = typeof(string), DbColumnName = "SubCategoryID", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Product Code", ReportDataType = typeof(string), DbColumnName = "ProductID", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Product Name", ReportDataType = typeof(string), DbColumnName = "ProductName", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Unit", ReportDataType = typeof(string), DbColumnName = "UnitOfMeasureID", ValueDataType = typeof(long), IsSelectionField = true, IsGroupBy = true, IsJoinField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "ExcessQty", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Percentage", ReportDataType = typeof(decimal), DbColumnName = "OrderQty", ValueDataType = typeof(decimal), IsSelectionField = true, IsRowTotal = true });
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
                #endregion
                #region Receipt
                case "RptReceiptsRegister":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsConditionNameJoined = true, IsJoinField = true, IsGroupBy = true, IsConditionField = true, IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Sale Type", ReportDataType = typeof(string), DbColumnName = "SaleTypeID", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Bill Type", ReportDataType = typeof(string), DbColumnName = "BillTypeID", ValueDataType = typeof(int), IsSelectionField = true, IsGroupBy = true }); // IsConditionField = true,
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "SDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "SDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "Receipt", ValueDataType = typeof(string), IsSelectionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Pay Type", ReportDataType = typeof(string), DbColumnName = "PaymentID", DbJoinColumnName = "PrintDescrip", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Customer Type", ReportDataType = typeof(string), DbColumnName = "CustomerType", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Customer", ReportDataType = typeof(string), DbColumnName = "EnCodeName", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Bank", ReportDataType = typeof(string), DbColumnName = "BankPosID", DbJoinColumnName = "Bank", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Cheque Date", ReportDataType = typeof(string), DbColumnName = "ChequeDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Reference No", ReportDataType = typeof(string), DbColumnName = "RefNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = "Created User", ReportDataType = typeof(string), DbColumnName = "UpdatedBy", DbJoinColumnName = "EmployeeName", IsJoinField = true, ValueDataType = typeof(long), IsConditionNameJoined = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true }); // IsSelectionField = true,
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString13", ReportFieldName = "Cashier", ReportDataType = typeof(string), DbColumnName = "CashierID", DbJoinColumnName = "EmployeeName", IsJoinField = true, ValueDataType = typeof(long), IsConditionNameJoined = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Rcpt. Amt.", ReportDataType = typeof(decimal), DbColumnName = "Amount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true });
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);
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

                case "FrmPettyCashReimbursement":
                    AccPettyCashReimbursementService accPettyCashReimbursementService = new AccPettyCashReimbursementService();
                    return accPettyCashReimbursementService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmPettyCashIOU":
                    AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                    return accPettyCashIOUService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmPettyCashBillEntry":
                    AccPettyCashBillService accPettyCashBillService = new AccPettyCashBillService();
                    return accPettyCashBillService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmPettyCashPayment":
                    AccPettyCashPaymentService accPettyCashPaymentService = new AccPettyCashPaymentService();
                    return accPettyCashPaymentService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                #endregion

                #region Account reports
                case "RptPettyCash":
                    //InvStockAdjustmentService invStockExcessAdjustmentService = new InvStockAdjustmentService();
                    //return invStockExcessAdjustmentService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "RptReceiptsRegister":
                    AccPaymentService accReceiptsServiceReceiptsRegister = new AccPaymentService();
                    return accReceiptsServiceReceiptsRegister.GetSelectionReceiptsDataSummary(reportDatStruct);
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
                    
                case "FrmPettyCashReimbursement":
                    AccPettyCashReimbursementService accPettyCashReimbursementService = new AccPettyCashReimbursementService();
                    return accPettyCashReimbursementService.GetPettyCashReimbursementsDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);

                case "FrmPettyCashIOU":
                    AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                    return accPettyCashIOUService.GetPettyCashIOUsDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);

                case "FrmPettyCashBillEntry":
                    AccPettyCashBillService accPettyCashBillService = new AccPettyCashBillService();
                    return accPettyCashBillService.GetPettyCashBillsDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);

                case "FrmPettyCashPayment":
                    AccPettyCashPaymentService accPettyCashPaymentService = new AccPettyCashPaymentService();
                    return accPettyCashPaymentService.GetPettyCashPaymentsDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);
                #endregion

                #region Account reports
                case "RptPettyCash":
                    //InvStockAdjustmentService invStockExcessAdjustmentService = new InvStockAdjustmentService();
                    //return invStockExcessAdjustmentService.GetStockAdjustmentDetailsDataTable(reportConditionsDataStructList, reportDataStructList, autoGenerateInfo);

                case "RptReceiptsRegister":
                    AccPaymentService accReceiptsServiceReceiptsRegister = new AccPaymentService();
                    return accReceiptsServiceReceiptsRegister.GetReceiptsRegisterDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, autoGenerateInfo);
                    
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
                case "LocationID":
                    LocationService locationService = new LocationService();
                    if (!reportDataStruct.IsConditionNameJoined)
                    {conditionValue = locationService.GetLocationsByCode(dataValue.Trim()).LocationID.ToString();}
                    else
                    {conditionValue = locationService.GetLocationsByName(dataValue.Trim()).LocationName.ToString();}
                    break;
                case "PaymentID":
                    PayTypeService payTypeService = new PayTypeService();
                    conditionValue = payTypeService.GetPayTypeByName(dataValue.Trim()).PrintDescrip.ToString();
                    break;
                case "BankPosID":
                    BankPosService bankPosService = new BankPosService();
                    conditionValue = bankPosService.GetBankPosByName(dataValue.Trim()).Bank.ToString();
                    break;
                case "EmployeeID":
                case "CashierID":
                case "UpdatedBy":
                    EmployeeService employeeService = new EmployeeService();
                    if (!reportDataStruct.IsConditionNameJoined)
                    { conditionValue = employeeService.GetEmployeesByCode(dataValue.Trim()).EmployeeID.ToString(); }
                    else
                    { conditionValue = employeeService.GetEmployeesByName(dataValue.Trim()).EmployeeName.ToString(); }
                    break;
                case "LoyaltyCustomerID":
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                    if (!reportDataStruct.IsConditionNameJoined)
                    { conditionValue = loyaltyCustomerService.GetLoyaltyCustomerByCode(dataValue.Trim()).CustomerId.ToString(); }
                    else
                    { conditionValue = loyaltyCustomerService.GetLoyaltyCustomerByName(dataValue.Trim()).CustomerName.ToString(); }
                    break;

                case "PettyCashLedgerID":
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    if (!reportDataStruct.IsConditionNameJoined)
                    { conditionValue = accLedgerAccountService.GetAccLedgerPettyCashAccountByCode(dataValue.Trim()).AccLedgerAccountID.ToString(); }
                    else
                    { conditionValue = accLedgerAccountService.GetAccLedgerPettyCashAccountByName(dataValue.Trim()).LedgerName.ToString(); }
                    break;
                default:
                    break;
            }
        
            return conditionValue;
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
