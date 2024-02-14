using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Domain;
using Report.GV.Transactions.Reports;
using Service;
using Utility;

namespace Report.GV
{
    public class GvReportGenerator
    {
        public void GenearateReferenceReport(AutoGenerateInfo autoGenerateInfo, string documentNo, bool isOrg)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            Cursor.Current = Cursors.WaitCursor;
            DataTable reportData = new DataTable();
            
            string[] stringField = new string[] { };
            

            switch (autoGenerateInfo.FormName)
            {
               
                default:
                    break;
            }

            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
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
                case "FrmGiftVoucherPurchaseOrder": // GiftVoucher Purchase Order
                    InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                    reportData = invGiftVoucherPurchaseOrderService.GetPurchaseOrderTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldPo = { "Document", "Date", "Supplier", "Remark", "Expected Date", "Payment Terms", "", "", "Type", "Reference No", 
                                            "", "Location", "Group", "Book", "Serial Range", "Value", "Count", "Total Value", "", "", "", 
                                            "", "Total Amt.", "Gross Amt.", "Dis.%", "Dis.Amt.", "Tax Amt.", "Oth. Charges", "Net Amt." };

                    GvRptTransactionTemplate gvRptTransactionTemplate = new GvRptTransactionTemplate();
                    gvRptTransactionTemplate.SetDataSource(reportData);
                    // Assign formula and summery field values
                    gvRptTransactionTemplate.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    gvRptTransactionTemplate.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    gvRptTransactionTemplate.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    gvRptTransactionTemplate.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    gvRptTransactionTemplate.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    gvRptTransactionTemplate.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    gvRptTransactionTemplate.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    for (int i = 0; i < stringFieldPo.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        gvRptTransactionTemplate.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldPo[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = gvRptTransactionTemplate;
                    break;

                case "FrmGiftVoucherGoodsReceivedNote":
                    InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                    DataSet dsReportData = new DataSet();
                    dsReportData = invGiftVoucherPurchaseService.GetPurchaseTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldGrn = { "Document", "Date", "Supplier", "Remark", "Expected Date", "Payment Terms", "", "", "Type", "Reference No", 
                                            "", "Location", "Group", "Book", "Serial Range", "Value", "Count", "Total Value", "", "", "", 
                                            "", "Total Amt.", "Gross Amt.", "Dis.%", "Dis.Amt.", "Tax Amt.", "Oth. Charges", "Net Amt."  };

                    GvRptTransactionTemplate gvRptTransactionTemplateGRN = new GvRptTransactionTemplate();
                    gvRptTransactionTemplateGRN.SetDataSource(dsReportData.Tables[0]);
                    // Assign formula and summery field values
                    gvRptTransactionTemplateGRN.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    gvRptTransactionTemplateGRN.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    gvRptTransactionTemplateGRN.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    gvRptTransactionTemplateGRN.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    gvRptTransactionTemplateGRN.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    gvRptTransactionTemplateGRN.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    gvRptTransactionTemplateGRN.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    //  Set field text
                    for (int i = 0; i < stringFieldGrn.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        gvRptTransactionTemplateGRN.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldGrn[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = gvRptTransactionTemplateGRN;

                    // Get Balances report of reference PO
                    if (dsReportData.Tables.Count > 1)
                    {
                        if (dsReportData.Tables[1].Rows.Count > 0)
                        {
                            FrmReportViewer reportViewerPoBal = new FrmReportViewer();
                            string[] stringFieldPoBal = { "Document", "Date", "Supplier", "Remark", "Expected Date", "Payment Terms", "", "", "Type", "Reference No", 
                                            "", "Location", "Group", "Book", "Serial Range", "Value", "Count", "Bal. Count", "", "", "", 
                                            "", "Total Amt.", "Gross Amt.", "Dis.%", "Dis.Amt.", "Tax Amt.", "Oth. Charges", "Net Amt."  };

                            GvRptTransactionTemplate gvRptTransactionTemplatePoBal = new GvRptTransactionTemplate();
                            gvRptTransactionTemplatePoBal.SetDataSource(dsReportData.Tables[1]);
                            // Assign formula and summery field values
                            gvRptTransactionTemplatePoBal.SummaryInfo.ReportTitle = "Purchase Order Balances";
                            gvRptTransactionTemplatePoBal.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                            gvRptTransactionTemplatePoBal.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                            gvRptTransactionTemplatePoBal.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                            gvRptTransactionTemplatePoBal.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                            gvRptTransactionTemplatePoBal.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                            gvRptTransactionTemplatePoBal.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                            //  Set field text
                            for (int i = 0; i < stringFieldPoBal.Length; i++)
                            {
                                string st = "st" + (i + 1);
                                gvRptTransactionTemplatePoBal.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldPoBal[i].Trim() + "'";
                            }

                            //Common.SetMenu(reportViewerPoBal, UI.Windows);
                            reportViewerPoBal.crRptViewer.ReportSource = gvRptTransactionTemplatePoBal;
                            reportViewerPoBal.WindowState = FormWindowState.Maximized;
                            reportViewerPoBal.Show();
                        }
                    }
                    break;
                case "FrmGiftVoucherTransfer":
                    InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                    dsReportData = invGiftVoucherTransferNoteService.GetTransferofGoodsTransactionDataTable(documentNo, documentSataus, autoGenerateInfo.DocumentID);

                    string[] stringFieldTog = { "Document", "Date", "", "Remark", "", "", "", "", "Type", "Reference No", "From Location", 
                                                  "To Location", "Group", "Book", "Serial Range", "Value", "Count", "", "", "", "", 
                                                  "", "Total Amt.", "", "", "", "", "", "" };
                    
                    GvRptTransactionTemplate gvRptTransactionTemplateTOG = new GvRptTransactionTemplate();
                    gvRptTransactionTemplateTOG.SetDataSource(dsReportData.Tables[0]);
                    // Assign formula and summery field values
                    gvRptTransactionTemplateTOG.SummaryInfo.ReportTitle = autoGenerateInfo.FormText;
                    gvRptTransactionTemplateTOG.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    gvRptTransactionTemplateTOG.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    gvRptTransactionTemplateTOG.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    gvRptTransactionTemplateTOG.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    gvRptTransactionTemplateTOG.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                    gvRptTransactionTemplateTOG.DataDefinition.FormulaFields["isOrg"].Text = "" + printStatus + "";

                    //  Set field text
                    for (int i = 0; i < stringFieldTog.Length; i++)
                    {
                        string st = "st" + (i + 1);
                        gvRptTransactionTemplateTOG.DataDefinition.FormulaFields["st" + (i + 1).ToString()].Text = "'" + stringFieldTog[i].Trim() + "'";
                    }
                    reportViewer.crRptViewer.ReportSource = gvRptTransactionTemplateTOG;
                    break;
                default:
                    break;
            }
            
            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.BringToFront();
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
        }

        public void GenerateTransactionSummaryReport(AutoGenerateInfo autoGenerateInfo, DataTable dtReportData, DataTable dtReportConditions, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> groupByStructList, List<Common.ReportDataStruct> orderByStructList)
        {
            FrmReportViewer reportViewer = new FrmReportViewer();
            Cursor.Current = Cursors.WaitCursor;

            List<Common.ReportDataStruct> printDataStructList;
            List<Common.ReportDataStruct> printGroupByStructList;
            DataTable dtPrintReportData = new DataTable();
            DataTable dtArrangedReportData = new DataTable();
            List<Common.ReportDataStruct> selectedReportStructList;
            List<Common.ReportDataStruct> selectedGroupStructList;

            
            string strFieldName = string.Empty;
            string group1, group2, group3, group4;
            int yx = 0;
            int rc = 0, sr = 1, dc = 12;
            int gr = 0, gp = 1;

            switch (autoGenerateInfo.FormName)
            {
                #region Transaction Reports
                #region GiftVoucherPurchaseOrder
                case "FrmGiftVoucherPurchaseOrder":
                    GvRptTransactionSummaryTemplateLandscape gvRptTransactionSummaryTemplatePo = new GvRptTransactionSummaryTemplateLandscape();

                    #region Set Values for report header Fields
                    strFieldName = string.Empty;
                    sr = 1; dc = 8;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {
                            if (item.ReportDataType.Equals(typeof(string)))
                            {
                                strFieldName = "st" + sr;
                                gvRptTransactionSummaryTemplatePo.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                sr++;
                            }

                            if (item.ReportDataType.Equals(typeof(decimal)))
                            {
                                strFieldName = "st" + dc;
                                gvRptTransactionSummaryTemplatePo.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                dc++;
                            }
                        }
                    }

                    #endregion

                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    gvRptTransactionSummaryTemplatePo.SetDataSource(dtArrangedReportData);

                    //gvRptTransactionSummaryTemplatePo.SetDataSource(dtReportData);
                    gvRptTransactionSummaryTemplatePo.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    gvRptTransactionSummaryTemplatePo.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    gvRptTransactionSummaryTemplatePo.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    gvRptTransactionSummaryTemplatePo.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    gvRptTransactionSummaryTemplatePo.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    gvRptTransactionSummaryTemplatePo.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    gvRptTransactionSummaryTemplatePo.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

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
                    //        gvRptTransactionSummaryTemplatePo.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //    }

                    //    if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //    {
                    //        strFieldName = "st" + (17 - i);
                    //        gvRptTransactionSummaryTemplatePo.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //    }
                    //}
                    #endregion
                    #region Group By - old

                    //for (int i = 0; i <= 3; i++)
                    //{
                    //    gvRptTransactionSummaryTemplatePo.SetParameterValue(i, "");

                    //    if (groupByStructList.Count > i)
                    //    {
                    //        if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    //        {
                    //            string strGroup = groupByStructList[i].ReportField.ToString();
                    //            gvRptTransactionSummaryTemplatePo.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //        {

                    //            gvRptTransactionSummaryTemplatePo.SetParameterValue(i, "");
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
                            if (i < gvRptTransactionSummaryTemplatePo.DataDefinition.Groups.Count)
                            {
                                gvRptTransactionSummaryTemplatePo.DataDefinition.Groups[i].ConditionField = gvRptTransactionSummaryTemplatePo.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())]; //[string.Concat("FieldString", groupStrFieldCount)];
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < gvRptTransactionSummaryTemplatePo.DataDefinition.Groups.Count; i++)
                        {
                            if (gvRptTransactionSummaryTemplatePo.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                if (i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count())
                                {
                                    gvRptTransactionSummaryTemplatePo.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), true);
                                }
                                else
                                {
                                    gvRptTransactionSummaryTemplatePo.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < gvRptTransactionSummaryTemplatePo.DataDefinition.Groups.Count; i++)
                        {
                            if (gvRptTransactionSummaryTemplatePo.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                gvRptTransactionSummaryTemplatePo.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = gvRptTransactionSummaryTemplatePo;
                    break;
                #endregion
                #region GiftVoucherGoodsReceivedNote
                case "FrmGiftVoucherGoodsReceivedNote":
                    GvRptTransactionSummaryTemplateLandscape gvRptTransactionSummaryTemplateGrn = new GvRptTransactionSummaryTemplateLandscape();

                    #region Set Values for report header Fields
                    strFieldName = string.Empty;
                    sr = 1; dc = 8;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {

                            if (item.ReportDataType.Equals(typeof(string)))
                            {
                                strFieldName = "st" + sr;
                                gvRptTransactionSummaryTemplateGrn.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                sr++;
                            }

                            if (item.ReportDataType.Equals(typeof(decimal)))
                            {
                                strFieldName = "st" + dc;
                                gvRptTransactionSummaryTemplateGrn.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                dc++;
                            }
                        }
                    }
                    #endregion

                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    gvRptTransactionSummaryTemplateGrn.SetDataSource(dtArrangedReportData);

                    //gvRptTransactionSummaryTemplateGrn.SetDataSource(dtReportData);
                    gvRptTransactionSummaryTemplateGrn.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    gvRptTransactionSummaryTemplateGrn.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    gvRptTransactionSummaryTemplateGrn.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    gvRptTransactionSummaryTemplateGrn.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    gvRptTransactionSummaryTemplateGrn.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    gvRptTransactionSummaryTemplateGrn.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    gvRptTransactionSummaryTemplateGrn.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

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
                    //        gvRptTransactionSummaryTemplateGrn.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //    }

                    //    if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //    {
                    //        strFieldName = "st" + (17 - i);
                    //        gvRptTransactionSummaryTemplateGrn.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //    }
                    //}
                    #endregion
                    #region Group By - old

                    //for (int i = 0; i <= 3; i++)
                    //{
                    //    gvRptTransactionSummaryTemplateGrn.SetParameterValue(i, "");

                    //    if (groupByStructList.Count > i)
                    //    {
                    //        if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    //        {
                    //            string strGroup = groupByStructList[i].ReportField.ToString();
                    //            gvRptTransactionSummaryTemplateGrn.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //        {

                    //            gvRptTransactionSummaryTemplateGrn.SetParameterValue(i, "");
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
                            if (i < gvRptTransactionSummaryTemplateGrn.DataDefinition.Groups.Count)
                            {
                                gvRptTransactionSummaryTemplateGrn.DataDefinition.Groups[i].ConditionField = gvRptTransactionSummaryTemplateGrn.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())]; //[string.Concat("FieldString", groupStrFieldCount)];
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < gvRptTransactionSummaryTemplateGrn.DataDefinition.Groups.Count; i++)
                        {
                            if (gvRptTransactionSummaryTemplateGrn.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                if (i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count())
                                {
                                    gvRptTransactionSummaryTemplateGrn.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), true);
                                }
                                else
                                {
                                    gvRptTransactionSummaryTemplateGrn.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < gvRptTransactionSummaryTemplateGrn.DataDefinition.Groups.Count; i++)
                        {
                            if (gvRptTransactionSummaryTemplateGrn.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                gvRptTransactionSummaryTemplateGrn.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = gvRptTransactionSummaryTemplateGrn;
                    break;
                #endregion
                #region GiftVoucherTransfer
                case "FrmGiftVoucherTransfer":
                    GvRptTransactionSummaryTemplateLandscape gvRptTransactionSummaryTemplateTOG = new GvRptTransactionSummaryTemplateLandscape();

                    #region Set Values for report header Fields
                    strFieldName = string.Empty;
                    sr = 1; dc = 8;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {

                            if (item.ReportDataType.Equals(typeof(string)))
                            {
                                strFieldName = "st" + sr;
                                gvRptTransactionSummaryTemplateTOG.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                sr++;
                            }

                            if (item.ReportDataType.Equals(typeof(decimal)))
                            {
                                strFieldName = "st" + dc;
                                gvRptTransactionSummaryTemplateTOG.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                dc++;
                            }
                        }
                    }

                    #endregion

                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    gvRptTransactionSummaryTemplateTOG.SetDataSource(dtArrangedReportData);

                    //gvRptTransactionSummaryTemplateTOG.SetDataSource(dtReportData);
                    gvRptTransactionSummaryTemplateTOG.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    gvRptTransactionSummaryTemplateTOG.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    gvRptTransactionSummaryTemplateTOG.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    gvRptTransactionSummaryTemplateTOG.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    gvRptTransactionSummaryTemplateTOG.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    gvRptTransactionSummaryTemplateTOG.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    gvRptTransactionSummaryTemplateTOG.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    ///////////////////////////////////////////////////////////////////////////
                    // Use some other way to add values into formula fields DateFrom and  DateTo ()
                    //goReport.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFrom.Value.ToShortDateString() + "'";
                    //goReport.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpTo.Value.ToShortDateString() + "'";
                    ///////////////////////////////////////////////////////////////////////////
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                    //cryRptReportTemplate.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";

                    #region set Header - old
                    //strFieldName = string.Empty;
                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    strFieldName = "st" + (i + 1);
                    //    if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //    {
                    //        gvRptTransactionSummaryTemplateTOG.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //    }

                    //    if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //    {
                    //        strFieldName = "st" + (17 - i);
                    //        gvRptTransactionSummaryTemplateTOG.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //    }
                    //}
                    #endregion
                    #region Group By - old

                    //for (int i = 0; i <= 3; i++)
                    //{
                    //    gvRptTransactionSummaryTemplateTOG.SetParameterValue(i, "");

                    //    if (groupByStructList.Count > i)
                    //    {
                    //        if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    //        {
                    //            string strGroup = groupByStructList[i].ReportField.ToString();
                    //            gvRptTransactionSummaryTemplateTOG.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //        {
                    //            gvRptTransactionSummaryTemplateTOG.SetParameterValue(i, "");
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
                            if (i < gvRptTransactionSummaryTemplateTOG.DataDefinition.Groups.Count)
                            {
                                gvRptTransactionSummaryTemplateTOG.DataDefinition.Groups[i].ConditionField = gvRptTransactionSummaryTemplateTOG.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())]; //[string.Concat("FieldString", groupStrFieldCount)];
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < gvRptTransactionSummaryTemplateTOG.DataDefinition.Groups.Count; i++)
                        {
                            if (gvRptTransactionSummaryTemplateTOG.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                if (i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count())
                                {
                                    gvRptTransactionSummaryTemplateTOG.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), true);
                                }
                                else
                                {
                                    gvRptTransactionSummaryTemplateTOG.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < gvRptTransactionSummaryTemplateTOG.DataDefinition.Groups.Count; i++)
                        {
                            if (gvRptTransactionSummaryTemplateTOG.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                gvRptTransactionSummaryTemplateTOG.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion
                    reportViewer.crRptViewer.ReportSource = gvRptTransactionSummaryTemplateTOG;
                    break;
                #endregion
                #endregion

                #region Summary Reports
                #region Gift Voucher Register
                case "RptGiftVoucherRegister":
                    //GvRptTransactionTemplateLandscape gvRptTransactionTemplateLandscape = new GvRptTransactionTemplateLandscape();
                    GvRptTransactionTemplateLandscape1 gvRptTransactionTemplateLandscape = new GvRptTransactionTemplateLandscape1();

                    #region Set Values for report header Fields
                    strFieldName = string.Empty;
                    sr = 1; dc = 13;

                    foreach (var item in reportDataStructList)
                    {
                        if (dtReportData.Columns.Contains(item.ReportField.Trim()))
                        {
                            if (item.ReportDataType.Equals(typeof(string)))
                            {
                                strFieldName = "st" + sr;
                                gvRptTransactionTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                sr++;
                            }

                            if (item.ReportDataType.Equals(typeof(decimal)))
                            {
                                strFieldName = "st" + dc;
                                gvRptTransactionTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + item.ReportFieldName.Trim() + "'";
                                dc++;
                            }
                        }
                    }

                    #endregion

                    dtArrangedReportData = dtReportData.Copy();
                    dtArrangedReportData = SetReportDataTableHeadersForReport(dtArrangedReportData);

                    gvRptTransactionTemplateLandscape.SetDataSource(dtArrangedReportData);

                    //gvRptTransactionTemplateLandscape.SetDataSource(dtReportData);
                    gvRptTransactionTemplateLandscape.Subreports["RptReportConditions.rpt"].SetDataSource(dtReportConditions);
                    gvRptTransactionTemplateLandscape.SummaryInfo.ReportTitle = autoGenerateInfo.FormText + " Summary";
                    gvRptTransactionTemplateLandscape.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                    gvRptTransactionTemplateLandscape.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                    gvRptTransactionTemplateLandscape.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                    gvRptTransactionTemplateLandscape.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                    gvRptTransactionTemplateLandscape.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                    #region Set Header - old
                    //strFieldName = string.Empty;
                    //int sr = 0;
                    //for (int i = 0; i < reportDataStructList.Count; i++)
                    //{
                    //    if (reportDataStructList[i].IsSelectionField.Equals(true))
                    //    {
                    //        strFieldName = "st" + (i + 1);
                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(string)))
                    //        {
                    //            gvRptTransactionTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //        }

                    //        if (reportDataStructList[i].ReportDataType.Equals(typeof(decimal)))
                    //        {
                    //            strFieldName = "st" + (14 + sr);
                    //            gvRptTransactionTemplateLandscape.DataDefinition.FormulaFields[strFieldName].Text = "'" + reportDataStructList[i].ReportFieldName.Trim() + "'";
                    //            sr++;
                    //        }
                    //    }
                    //}
                    #endregion
                    #region Group By - Old

                    //for (int i = 0; i <= 9; i++)
                    //{
                    //    gvRptTransactionTemplateLandscape.SetParameterValue(i, "");

                    //    if (groupByStructList.Count > i)
                    //    {
                    //        if (groupByStructList[i].ReportField.Trim() != string.Empty && groupByStructList[i].IsResultGroupBy == true)
                    //        {
                    //            string strGroup = groupByStructList[i].ReportField.ToString();
                    //            gvRptTransactionTemplateLandscape.SetParameterValue(i, groupByStructList[i].ReportField.Trim());
                    //        }
                    //        else
                    //        {

                    //            gvRptTransactionTemplateLandscape.SetParameterValue(i, "");
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
                            if (i < gvRptTransactionTemplateLandscape.DataDefinition.Groups.Count)
                            {
                                gvRptTransactionTemplateLandscape.DataDefinition.Groups[i].ConditionField = gvRptTransactionTemplateLandscape.Database.Tables[0].Fields[string.Concat("FieldString", (i + 1).ToString())]; //[string.Concat("FieldString", groupStrFieldCount)];
                            }
                        }

                        // Set parameter field values
                        for (int i = 0; i < gvRptTransactionTemplateLandscape.DataDefinition.Groups.Count; i++)
                        {
                            if (gvRptTransactionTemplateLandscape.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                if (i < dtArrangedReportData.Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith("FieldString")).Count())
                                {
                                    gvRptTransactionTemplateLandscape.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), true);
                                }
                                else
                                {
                                    gvRptTransactionTemplateLandscape.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Set parameter field values
                        for (int i = 0; i < gvRptTransactionTemplateLandscape.DataDefinition.Groups.Count; i++)
                        {
                            if (gvRptTransactionTemplateLandscape.ParameterFields[i].ParameterValueType.Equals(ParameterValueKind.BooleanParameter))
                            {
                                gvRptTransactionTemplateLandscape.SetParameterValue("prmSelectGroup" + (i + 1).ToString(), false);
                            }
                        }
                    }
                    #endregion

                    reportViewer.crRptViewer.ReportSource = gvRptTransactionTemplateLandscape;
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

        public FrmReprotGenerator OrganizeFormFields(AutoGenerateInfo autoGenerateInfo)
        {
            List<Common.ReportDataStruct> reportDatStructList = new List<Common.ReportDataStruct>();
            FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList);

            switch (autoGenerateInfo.FormName)
            {
                #region GV transactions
                #region GiftVoucherPurchaseOrder
                case "FrmGiftVoucherPurchaseOrder":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true }); //, IsGroupBy = true
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Book Name", ReportDataType = typeof(string), DbColumnName = "InvGiftVoucherBookCodeID", DbJoinColumnName = "BookName", ValueDataType = typeof(int), IsSelectionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Supplier", ReportDataType = typeof(string), DbColumnName = "LgsSupplierID", DbJoinColumnName = "SupplierName", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Ref. No", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Serial Range", ReportDataType = typeof(string), DbColumnName = "Serial", ValueDataType = typeof(string), IsSelectionField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Expected Date", ReportDataType = typeof(string), DbColumnName = "ExpectedDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Discount Percentage", ReportDataType = typeof(decimal), DbColumnName = "DiscountPercentage", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Discount Amount", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "Tax Amount", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Total Amt.", ReportDataType = typeof(decimal), DbColumnName = "TotalAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,7,5,4);
                #endregion
                #region GiftVoucherGoodsReceivedNote
                case "FrmGiftVoucherGoodsReceivedNote":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true }); //, IsGroupBy = true
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Book Name", ReportDataType = typeof(string), DbColumnName = "InvGiftVoucherBookCodeID", DbJoinColumnName = "BookName", ValueDataType = typeof(int), IsSelectionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Supplier", ReportDataType = typeof(string), DbColumnName = "LgsSupplierID", DbJoinColumnName = "SupplierName", ValueDataType = typeof(long), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Ref. No", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Serial Range", ReportDataType = typeof(string), DbColumnName = "Serial", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Net Amt.", ReportDataType = typeof(decimal), DbColumnName = "NetAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Gross Amt.", ReportDataType = typeof(decimal), DbColumnName = "GrossAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Discount Percentage", ReportDataType = typeof(decimal), DbColumnName = "DiscountPercentage", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Discount Amount", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal6", ReportFieldName = "Tax Amount", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Consignment Basis.", ReportDataType = typeof(decimal), DbColumnName = "IsConsignmentBasis", ValueDataType = typeof(bool) });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal7", ReportFieldName = "Total Amt.", ReportDataType = typeof(decimal), DbColumnName = "TotalAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,7,5,4);
                #endregion
                #region GiftVoucherTransfer
                case "FrmGiftVoucherTransfer":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "From Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", IsConditionNameJoined = true, ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "To Location", ReportDataType = typeof(string), DbColumnName = "ToLocationID", DbJoinColumnName = "LocationName", IsConditionNameJoined = true, ValueDataType = typeof(int), IsJoinField = true, IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Date", ReportDataType = typeof(string), DbColumnName = "DocumentDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true}); //, IsGroupBy = true 
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Document No", ReportDataType = typeof(string), DbColumnName = "DocumentNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Book Name", ReportDataType = typeof(string), DbColumnName = "InvGiftVoucherBookCodeID", DbJoinColumnName = "BookName", ValueDataType = typeof(int), IsSelectionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Ref. No", ReportDataType = typeof(string), DbColumnName = "ReferenceNo", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Remark", ReportDataType = typeof(string), DbColumnName = "Remark", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Serial Range", ReportDataType = typeof(string), DbColumnName = "Serial", ValueDataType = typeof(string), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Total Amt.", ReportDataType = typeof(decimal), DbColumnName = "TotalAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Discount Percentage", ReportDataType = typeof(decimal), DbColumnName = "DiscountPercentage", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal4", ReportFieldName = "Discount Amount", ReportDataType = typeof(decimal), DbColumnName = "DiscountAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal5", ReportFieldName = "Tax Amount", ReportDataType = typeof(decimal), DbColumnName = "TaxAmount", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsColumnTotal = true, IsRowTotal = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal3", ReportFieldName = "Consignment Basis.", ReportDataType = typeof(decimal), DbColumnName = "IsConsignmentBasis", ValueDataType = typeof(bool) });

                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,7,5,4);
                #endregion
                #endregion

                #region GV Summary Reports
                case "RptGiftVoucherRegister":
                    reportDatStructList = new List<Common.ReportDataStruct>();
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString1", ReportFieldName = "Voucher Serial", ReportDataType = typeof(string), DbColumnName = "VoucherSerial", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsRecordFilterByGivenOption = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Book Code", ReportDataType = typeof(string), DbColumnName = "InvGiftVoucherBookCodeID", DbJoinColumnName = "BookCode", ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString2", ReportFieldName = "Book Name", ReportDataType = typeof(string), DbColumnName = "InvGiftVoucherBookCodeID", DbJoinColumnName = "BookName", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Group Code", ReportDataType = typeof(string), DbColumnName = "InvGiftVoucherGroupID", DbJoinColumnName = "GiftVoucherGroupCode", ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                   
                    // ?? reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Issued Date", ReportDataType = typeof(string), DbColumnName = "SoldDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString3", ReportFieldName = "Issued Date", ReportDataType = typeof(string), DbColumnName = "SoldDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString4", ReportFieldName = "Issued Unit", ReportDataType = typeof(string), DbColumnName = "SoldUnitID", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString5", ReportFieldName = "Issued Receipt No", ReportDataType = typeof(string), DbColumnName = "SoldReceiptNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString6", ReportFieldName = "Issued Cashier", ReportDataType = typeof(string), DbColumnName = "EmployeeID", DbJoinColumnName = "EmployeeName", ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString7", ReportFieldName = "Issued Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });

                    // ?? reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Redeemed Date", ReportDataType = typeof(string), DbColumnName = "RedeemedDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString8", ReportFieldName = "Redeemed Date", ReportDataType = typeof(string), DbColumnName = "RedeemedDate", ValueDataType = typeof(DateTime), IsSelectionField = true, IsConditionField = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString9", ReportFieldName = "Redeemed Unit", ReportDataType = typeof(string), DbColumnName = "RedeemedUnitID", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString10", ReportFieldName = "Redeemed Receipt No", ReportDataType = typeof(string), DbColumnName = "RedeemedReceiptNo", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString11", ReportFieldName = "Redeemed Cashier", ReportDataType = typeof(string), DbColumnName = "EmployeeID", DbJoinColumnName = "EmployeeName", ValueDataType = typeof(long), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString12", ReportFieldName = "Redeemed Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString13", ReportFieldName = "Group Name", ReportDataType = typeof(string), DbColumnName = "InvGiftVoucherGroupID", DbJoinColumnName = "GiftVoucherGroupName", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString14", ReportFieldName = "Type", ReportDataType = typeof(string), DbColumnName = "VoucherType", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString15", ReportFieldName = "Status", ReportDataType = typeof(string), DbColumnName = "VoucherStatus", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString16", ReportFieldName = "Transfered Location", ReportDataType = typeof(string), DbColumnName = "LocationID", DbJoinColumnName = "LocationName", ValueDataType = typeof(int), IsSelectionField = true, IsConditionField = true, IsGroupBy = true, IsJoinField = true });
                    //reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldString17", ReportFieldName = "Sales Person", ReportDataType = typeof(string), DbColumnName = "VoucherStatus", ValueDataType = typeof(string), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });

                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal1", ReportFieldName = "Qty", ReportDataType = typeof(decimal), DbColumnName = "Qty", ValueDataType = typeof(decimal), IsSelectionField = true });
                    reportDatStructList.Add(new Common.ReportDataStruct() { ReportField = "FieldDecimal2", ReportFieldName = "Value", ReportDataType = typeof(decimal), DbColumnName = "GiftVoucherValue", ValueDataType = typeof(decimal), IsSelectionField = true, IsConditionField = true, IsGroupBy = true });
                    
                    return frmReprotGenerator = new FrmReprotGenerator(autoGenerateInfo, reportDatStructList,12,2,10);
                #endregion
                default:
                    return null;
            }
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDatStruct, AutoGenerateInfo autoGenerateInfo)
        {
            switch (autoGenerateInfo.FormName)
            {
                #region transactions

                case "FrmGiftVoucherPurchaseOrder":
                    InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                    return invGiftVoucherPurchaseOrderService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmGiftVoucherGoodsReceivedNote":
                    InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                    return invGiftVoucherPurchaseService.GetSelectionData(reportDatStruct, autoGenerateInfo);

                case "FrmGiftVoucherTransfer":
                    InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                    return invGiftVoucherTransferNoteService.GetSelectionData(reportDatStruct, autoGenerateInfo);
                #endregion

                #region Summary
                case "RptGiftVoucherRegister":
                    InvGiftVoucherMasterService invGiftVoucherMasterRegisterService = new InvGiftVoucherMasterService();
                    return invGiftVoucherMasterRegisterService.GetSelectionData(reportDatStruct, autoGenerateInfo);
                
                #endregion
                    
                default:
                    return null;
            }
        }

        public DataTable GetResultData(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            switch (autoGenerateInfo.FormName)
            {
                #region transactions


                case "FrmGiftVoucherPurchaseOrder":
                    InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                    return invGiftVoucherPurchaseOrderService.GetPurchaseOrdersDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);

                case "FrmGiftVoucherGoodsReceivedNote":
                    InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                    return invGiftVoucherPurchaseService.GetPurchaseDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);

                case "FrmGiftVoucherTransfer":
                    InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                    return invGiftVoucherTransferNoteService.GetTransferofGiftVouchersDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);
                #endregion

                #region Summary
                case "RptGiftVoucherRegister":
                    InvGiftVoucherMasterService invGiftVoucherMasterRegisterService = new InvGiftVoucherMasterService();
                    return invGiftVoucherMasterRegisterService.GetGiftVoucherRegisterDataTable(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);
                    
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
                    LgsSupplierService lgssupplierService = new LgsSupplierService();
                    if (reportDataStruct.DbJoinColumnName == "SupplierName")
                    {conditionValue = lgssupplierService.GetSupplierByName(dataValue.Trim()).SupplierName.ToString();}
                    else if (reportDataStruct.DbJoinColumnName == "SupplierCode")
                    { conditionValue = lgssupplierService.GetSupplierByCode(dataValue.Trim()).SupplierCode.ToString(); }
                    else
                    { conditionValue = lgssupplierService.GetSupplierByID(int.Parse(dataValue.Trim())).LgsSupplierID.ToString(); }
                    break;
                case "LookupKey":
                    LookUpReferenceService lookUpReferenceTitleTypeService = new LookUpReferenceService();
                    string titleType = ((int)LookUpReference.TitleType).ToString();
                    conditionValue = lookUpReferenceTitleTypeService.GetLookUpReferenceByKey(titleType, int.Parse(dataValue.Trim())).LookupKey.ToString();
                    break;
                case "PaymentMethodID":
                    PaymentMethodService paymentMethodService = new PaymentMethodService();
                    conditionValue = paymentMethodService.GetPaymentMethodsByName(dataValue.Trim()).PaymentMethodName.ToString();
                    break;
                case "LocationID":
                case "ToLocationID":
                    LocationService locationService = new LocationService();
                    if (reportDataStruct.DbJoinColumnName == "LocationName")
                    {conditionValue = locationService.GetLocationsByName(dataValue.Trim()).LocationName.ToString();}
                    else if (reportDataStruct.DbJoinColumnName == "LocationCode")
                    {conditionValue = locationService.GetLocationsByCode(dataValue.Trim()).LocationCode.ToString();}
                    else
                    { conditionValue = locationService.GetLocationsByID(int.Parse(dataValue.Trim())).LocationID.ToString(); }
                    break;
                case "EmployeeID":
                    EmployeeService employeeService = new EmployeeService();
                    if (reportDataStruct.DbJoinColumnName == "EmployeeName")
                    {conditionValue = employeeService.GetEmployeesByName(dataValue.Trim()).EmployeeName.ToString();}
                    else if (reportDataStruct.DbJoinColumnName == "EmployeeCode")
                    {conditionValue = employeeService.GetEmployeesByCode(dataValue.Trim()).EmployeeCode.ToString();}
                    else
                    { conditionValue = employeeService.GetEmployeesByID(int.Parse(dataValue.Trim())).EmployeeID.ToString(); }
                    break;
                case "InvGiftVoucherGroupID":
                    InvGiftVoucherGroupService invGiftVoucherGroupService = new InvGiftVoucherGroupService();

                    if (reportDataStruct.DbJoinColumnName == "GiftVoucherGroupName")
                    { conditionValue = invGiftVoucherGroupService.GetInvGiftVoucherGroupByName(dataValue.Trim()).GiftVoucherGroupName.ToString(); }
                    else if (reportDataStruct.DbJoinColumnName == "GiftVoucherGroupCode")
                    { conditionValue = invGiftVoucherGroupService.GetInvGiftVoucherGroupByCode(dataValue.Trim()).GiftVoucherGroupCode.ToString(); }
                    else
                    { conditionValue = invGiftVoucherGroupService.GetInvGiftVoucherGroupByID(int.Parse(dataValue.Trim())).InvGiftVoucherGroupID.ToString(); }

                    break;
                case "InvGiftVoucherBookCodeID":
                    InvGiftVoucherBookCodeGenerationService invGiftVoucherBookCodeGenerationService = new InvGiftVoucherBookCodeGenerationService();

                    if (reportDataStruct.DbJoinColumnName == "BookName")
                    { conditionValue = invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByName(dataValue.Trim()).BookName.ToString(); }
                    else if (reportDataStruct.DbJoinColumnName == "BookCode")
                    { conditionValue = invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByCode(dataValue.Trim()).BookCode.ToString(); }
                    else
                    { conditionValue = invGiftVoucherBookCodeGenerationService.GetInvGiftVoucherMasterBookByID(int.Parse(dataValue.Trim())).InvGiftVoucherGroupID.ToString(); }
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
