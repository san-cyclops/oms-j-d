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
using Report.Account;
using Report.CRM;
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


namespace Report
{
    public partial class FrmReprotGenerator : FrmBaseReportsForm
    {
        string group1, group2, group3, group4;
        string SelectFormula, rptName = "";
        int maxStringFields = 0; // maximum string fields can be displayed in the report
        int maxDecimalFields = 0; // maximum decimal fields can be displayed in the report
        int maxGroups = 0; // maximum groups can be displayed in the report
        //private bool isMdiChild = true;
        //private bool isCrossTabReport = false;

        ArrayList stringField= new ArrayList();           // Eg:- {"Doc.No", "Date", "Pro.Code","Pro.Name"};
        ArrayList decimalField = new ArrayList();         //Eg:-  {"Net Amt","P.Size","F.Qty","C.Price", "Or.Qty"};    
        ArrayList groupbyField = new ArrayList();
        ArrayList columnTotalField = new ArrayList();
        ArrayList rowTotalField = new ArrayList();
        ArrayList conditionField = new ArrayList();

        Common.ReportDataStruct reportDataStructTemp;

        List<Common.ReportDataStruct> reportDatStructList = new List<Common.ReportDataStruct>();
        AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();

        UserPrivileges showCostPrice = new UserPrivileges();
        UserPrivileges showSellingPrice = new UserPrivileges();
        UserPrivileges showQty = new UserPrivileges();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pautoGenerateInfo"></param>
        /// <param name="pReportDatStructList"></param>
        public FrmReprotGenerator(AutoGenerateInfo pautoGenerateInfo, List<Common.ReportDataStruct> pReportDatStructList)
        {
            InitializeComponent();
        
            for (int i = 0; i < pReportDatStructList.Count; i++)
            {
                if (pReportDatStructList[i].IsSelectionField.Equals(true) && pReportDatStructList[i].ReportDataType.Equals(typeof(string)))
                { stringField.Add(pReportDatStructList[i].ReportFieldName.ToString().Trim()); }

                if (pReportDatStructList[i].IsSelectionField.Equals(true) && pReportDatStructList[i].ReportDataType.Equals(typeof(decimal)))
                { decimalField.Add(pReportDatStructList[i].ReportFieldName.ToString().Trim()); }

                if (pReportDatStructList[i].IsConditionField.Equals(true))
                { conditionField.Add(pReportDatStructList[i].ReportFieldName.ToString().Trim()); }

                if (pReportDatStructList[i].IsGroupBy.Equals(true))
                { groupbyField.Add(pReportDatStructList[i].ReportFieldName.ToString().Trim()); }

                if (pReportDatStructList[i].IsColumnTotal.Equals(true))
                { columnTotalField.Add(pReportDatStructList[i].ReportFieldName.ToString().Trim()); }

                if (pReportDatStructList[i].IsRowTotal.Equals(true))
                { rowTotalField.Add(pReportDatStructList[i].ReportFieldName.ToString().Trim()); }
            }


            reportDatStructList = pReportDatStructList;
            autoGenerateInfo = pautoGenerateInfo;

            Text = Text + " - " + pautoGenerateInfo.FormText;

            cmbValueFrom.BringToFront();
            cmbValueTo.BringToFront();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pautoGenerateInfo"></param>
        /// <param name="pReportDatStructList"></param>
        /// <param name="pMaxStringFields"></param> maximum string fields can be displayed in the report
        /// <param name="pMaxDecimalFields"></param> maximum decimal fields can be displayed in the report
        public FrmReprotGenerator(AutoGenerateInfo pautoGenerateInfo, List<Common.ReportDataStruct> pReportDatStructList, int pMaxStringFields, int pMaxDecimalFields, int pMaxGroups)
        {
            InitializeComponent();

            for (int i = 0; i < pReportDatStructList.Count; i++)
            {
                if (pReportDatStructList[i].IsSelectionField.Equals(true) && pReportDatStructList[i].ReportDataType.Equals(typeof(string)))
                { stringField.Add(pReportDatStructList[i].ReportFieldName.ToString().Trim()); }

                if (pReportDatStructList[i].IsSelectionField.Equals(true) && pReportDatStructList[i].ReportDataType.Equals(typeof(decimal)))
                { decimalField.Add(pReportDatStructList[i].ReportFieldName.ToString().Trim()); }

                if (pReportDatStructList[i].IsConditionField.Equals(true))
                { conditionField.Add(pReportDatStructList[i].ReportFieldName.ToString().Trim()); }

                if (pReportDatStructList[i].IsGroupBy.Equals(true))
                { groupbyField.Add(pReportDatStructList[i].ReportFieldName.ToString().Trim()); }

                if (pReportDatStructList[i].IsColumnTotal.Equals(true))
                { columnTotalField.Add(pReportDatStructList[i].ReportFieldName.ToString().Trim()); }

                if (pReportDatStructList[i].IsRowTotal.Equals(true))
                { rowTotalField.Add(pReportDatStructList[i].ReportFieldName.ToString().Trim()); }
            }

            reportDatStructList = pReportDatStructList;
            autoGenerateInfo = pautoGenerateInfo;
            maxStringFields = pMaxStringFields;
            maxDecimalFields = pMaxDecimalFields;
            maxGroups = pMaxGroups;

            Text = Text + " - " + pautoGenerateInfo.FormText;

            cmbValueFrom.BringToFront();
            cmbValueTo.BringToFront();
        }

        public override void InitializeForm()
        {
            try
            {
                LoadFieldS();
                CheckUserRights();
            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }        
        }

        private void CheckUserRights()
        {
            try
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                showCostPrice = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, 19002);
                showSellingPrice = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, 19003);
                showQty = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, 19004);
            
            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }  
        }

        private void LoadFieldS()
        {

            ArrayList allValueType = new ArrayList();
            allValueType.AddRange(stringField);
            allValueType.AddRange(decimalField);

            //ArrayList allGroupBy = new ArrayList();
            //allGroupBy.AddRange(groupbyField);

            ArrayList allOrderBy = new ArrayList();
            allOrderBy.AddRange(allValueType);

            //ArrayList allRowTotal = new ArrayList();
            //allRowTotal.AddRange(rowTotalField);

            //ArrayList allColumnTotal = new ArrayList();
            //allColumnTotal.AddRange(columnTotalField);

            chkLstFieldSelectionStr.DataSource = stringField;
            chkLstFieldSelectionDes.DataSource = decimalField;
            chkLstGroupBy.DataSource = groupbyField;
            chkLstOrderBy.DataSource = allOrderBy;
            chkRowTotal.DataSource = rowTotalField;
            chklstColumnTotal.DataSource = columnTotalField;

            this.cmbValueType.SelectedIndexChanged -= new System.EventHandler(this.cmbValueType_SelectedIndexChanged);
            cmbValueType.DataSource = conditionField;
            cmbValueType.SelectedIndex = -1;
            this.cmbValueType.SelectedIndexChanged += new System.EventHandler(this.cmbValueType_SelectedIndexChanged);

            // Loop through and set all to checked.
            this.chkLstFieldSelectionDes.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstFieldSelectionDes_ItemCheck);
            for (int x = 0; x < ((maxDecimalFields <= chkLstFieldSelectionDes.Items.Count) ? maxDecimalFields : chkLstFieldSelectionDes.Items.Count); x++)
            {
                //chkLstFieldSelectionDes.SetItemChecked(x, true);
                var c = GetReportCheckedStatusByDataStruct(chkLstFieldSelectionDes.Items[x].ToString().Trim());
                chkLstFieldSelectionDes.SetItemChecked(x, GetReportCheckedStatusByDataStruct(chkLstFieldSelectionDes.Items[x].ToString().Trim()));
            }
            this.chkLstFieldSelectionDes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstFieldSelectionDes_ItemCheck);

            this.chkLstFieldSelectionStr.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstFieldSelectionStr_ItemCheck);
            for (int x = 0; x < ((maxStringFields <= chkLstFieldSelectionStr.Items.Count) ? maxStringFields : chkLstFieldSelectionStr.Items.Count); x++)
            {
                //chkLstFieldSelectionStr.SetItemChecked(x, true);
                chkLstFieldSelectionStr.SetItemChecked(x, GetReportCheckedStatusByDataStruct(chkLstFieldSelectionStr.Items[x].ToString().Trim()));
            }
            this.chkLstFieldSelectionStr.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstFieldSelectionStr_ItemCheck);

            // Loop through and set first field to checked.
            this.chkLstOrderBy.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstOrderBy_ItemCheck);
            if (chkLstOrderBy.Items.Count >= 0)
            { chkLstOrderBy.SetItemChecked(0, true); }
            this.chkLstOrderBy.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstOrderBy_ItemCheck);

            //SetUntickFields();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvResult.Rows.Count < 1)
                { return; }

                #region View
                switch (autoGenerateInfo.ModuleType)
                {
                    case 1: //Common Summary Report
                        ComReportGenerator comReportGenerator = new ComReportGenerator();
                        switch (autoGenerateInfo.ReportType)
                        {
                            case 1: // Reference
                                comReportGenerator.GenearateReferenceSummaryReport(autoGenerateInfo,
                                                                                dgvResult.DataSource as DataTable, GetConditionsDataTable(),
                                                                                GetReportFields(), GetGroupByFields());
                                break;
                            case 2: // Transaction
                                comReportGenerator.GenearateTransactionSummeryReport(autoGenerateInfo,
                                                                                dgvResult.DataSource as DataTable, GetConditionsDataTable(),
                                                                                GetReportFields(), GetGroupByFields(), chkLstGroupBy.CheckedIndices.Count < 1 ? true : chkViewGroupDetails.Checked);
                               
                                break;
                            default:

                                break;
                        }

                        break;
                    case 2: //Inventory Summary Report
                        InvReportGenerator invReportGenerator = new InvReportGenerator();
                        switch (autoGenerateInfo.ReportType)
                        {
                            case 1: // Reference
                                invReportGenerator.GenearateReferenceSummaryReport(autoGenerateInfo,
                                                                                dgvResult.DataSource as DataTable, GetConditionsDataTable(),
                                                                                GetReportFields(), GetGroupByFields());
                                break;
                            case 2: // Transaction
                                invReportGenerator.GenearateTransactionSummeryReport(autoGenerateInfo,
                                                                                dgvResult.DataSource as DataTable, GetConditionsDataTable(),
                                                                                GetReportFields(), GetGroupByFields(), chkLstGroupBy.CheckedIndices.Count < 1 ? true : chkViewGroupDetails.Checked);
                               
                                break;
                            default:

                                break;
                        }

                        break;
                    case 3: //Logistic Summary Report
                        LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();    
                        switch (autoGenerateInfo.ReportType)
                            {
                                case 1: // Reference
                                    lgsReportGenerator.GenearateReferenceSummaryReport(autoGenerateInfo,
                                                                                    dgvResult.DataSource as DataTable, GetConditionsDataTable(),
                                                                                    GetReportFields(), GetGroupByFields());
                                    break;
                                case 2: // Transaction
                                    lgsReportGenerator.GenearateTransactionSummeryReport(autoGenerateInfo,
                                                                                    dgvResult.DataSource as DataTable, GetConditionsDataTable(),
                                                                                    GetReportFields(), GetGroupByFields(), chkLstGroupBy.CheckedIndices.Count < 1 ? true : chkViewGroupDetails.Checked);
                               
                                    break;
                                default:

                                    break;
                            }
                        break;
                    case 4: //CRM Summary Report
                        CrmReportGenerator crmReportGenerator;
                        switch (autoGenerateInfo.ReportType)
                        {

                            case 1: // Reference
                                crmReportGenerator = new CrmReportGenerator();
                                crmReportGenerator.GenearateCrmReport(autoGenerateInfo,
                                                                                dgvResult.DataSource as DataTable, GetConditionsDataTable(),
                                                                                GetReportFields(), GetGroupByFields());
                                break;
                            case 2: // Transaction
                                crmReportGenerator = new CrmReportGenerator();
                                crmReportGenerator.GenearateCrmReport(autoGenerateInfo,
                                                                                dgvResult.DataSource as DataTable, GetConditionsDataTable(),
                                                                                GetReportFields(), GetGroupByFields());
                                break;
                            default:

                                break;
                        }
                        break;
                    case 5: //Accounts Summary Report
                        AccReportGenerator accReportGenerator = new AccReportGenerator();
                        switch (autoGenerateInfo.ReportType)
                        {
                            case 1: // Reference
                                accReportGenerator.GenearateReferenceSummaryReport(autoGenerateInfo,
                                                                                dgvResult.DataSource as DataTable,
                                                                                GetReportFields(), GetGroupByFields());
                                break;
                            case 2: // Transaction
                                accReportGenerator.GenearateTransactionSummaryReport(autoGenerateInfo,
                                                                                dgvResult.DataSource as DataTable, GetConditionsDataTable(),
                                                                                GetReportFields(), GetGroupByFields(), chkLstGroupBy.CheckedIndices.Count < 1 ? true : chkViewGroupDetails.Checked);
                               
                                break;
                            default:

                                break;
                        }

                        break;
                    case 6: //Gift Voucher Summary Report
                        GvReportGenerator gvReportGenerator = new GvReportGenerator();
                        switch (autoGenerateInfo.ReportType)
                        {
                            case 1: // Reference

                                break;
                            case 2: // Transaction

                                gvReportGenerator.GenerateTransactionSummaryReport(autoGenerateInfo,
                                                                                dgvResult.DataSource as DataTable, GetConditionsDataTable(),
                                                                                GetReportFields(), GetGroupByFields(), GetOrderByFields());
                                break;
                            default:

                                break;
                        }
                        break;
                    default:
                        break;
                }
                #endregion
               
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
           
        }

        private void btnValueAdd_Click(object sender, EventArgs e)
        {
            try
            { 
                if (GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()).ValueDataType.Equals(typeof(DateTime)))
                { AddConditions(); }
                else if (GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()).ValueDataType.Equals(typeof(decimal)))
                {
                    if (string.IsNullOrEmpty(txtValueFrom.Text.Trim()) || string.IsNullOrEmpty(txtValueTo.Text.Trim()))// SelectedIndex < 0 || txtValueTo.SelectedIndex < 0)
                    { return; }
                    AddConditions();
                }
                else if (reportDataStructTemp.IsManualRecordFilter.Equals(true))
                {
                    if (string.IsNullOrEmpty(txtValue.Text.Trim()))
                    { return; }
                    AddConditions();
                }
                else
                {
                    if (cmbValueFrom.SelectedIndex < 0 || cmbValueTo.SelectedIndex < 0)
                    { return; }
                    else
                    {
                        AddConditions();
                    }
                }


            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }           
        }

        private void AddConditions()
        {
            foreach (DataGridViewRow drItem in dgvValueRange.Rows)
            {
                if (cmbValueType.Text.Trim().Equals(drItem.Cells["ValueType"].Value.ToString().Trim()))
                { dgvValueRange.Rows.Remove(drItem); }
            }

            dgvValueRange.Rows.Add();
            int row = dgvValueRange.Rows.Count - 1;

            if (GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()).ValueDataType.Equals(typeof(DateTime)))
            {
                dgvValueRange.Rows[row].Cells["ValueType"].Value = cmbValueType.Text.Trim();
                dgvValueRange.Rows[row].Cells["ValueFrom"].Value = dtpDateFrom.Value.ToShortDateString().Trim();
                dgvValueRange.Rows[row].Cells["ValueTo"].Value = dtpDateTo.Value.ToShortDateString().Trim();
            }
            else if (GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()).ValueDataType.Equals(typeof(decimal)))
            {
                dgvValueRange.Rows[row].Cells["ValueType"].Value = cmbValueType.Text.Trim();
                dgvValueRange.Rows[row].Cells["ValueFrom"].Value = txtValueFrom.Text.Trim();
                dgvValueRange.Rows[row].Cells["ValueTo"].Value = txtValueTo.Text.Trim();
            }
            else if (reportDataStructTemp.IsManualRecordFilter.Equals(true))
            {
                dgvValueRange.Rows[row].Cells["ValueType"].Value = cmbValueType.Text.Trim();
                dgvValueRange.Rows[row].Cells["ValueFrom"].Value = txtValue.Text.Trim();
                dgvValueRange.Rows[row].Cells["ValueTo"].Value = txtValue.Text.Trim();
            }
            else
            {
                dgvValueRange.Rows[row].Cells["ValueType"].Value = cmbValueType.Text.Trim();
                dgvValueRange.Rows[row].Cells["ValueFrom"].Value = cmbValueFrom.Text.Trim();
                dgvValueRange.Rows[row].Cells["ValueTo"].Value = cmbValueTo.Text.Trim();
            }
        }                

        private void cmbValueType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbValueType.SelectedIndex < 0)
                { return; }

                LoadSelectionData();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void LoadSelectionData()
        {
          
            cmbValueFrom.DataSource = null;
            cmbValueTo.DataSource = null;
            lblValue.Text = "Value Between";

            reportDataStructTemp.IsManualRecordFilter = false;
            if (GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()).IsRecordFilterByGivenOption.Equals(true))
            {
                if (Toast.Show("Filter - By entered value ", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.Yes))
                {
                    reportDataStructTemp.IsManualRecordFilter = true;
                }
            }

            // Set Datetime Picker for date ranges
            if (GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()).ValueDataType.Equals(typeof(DateTime)))
            {
                cmbValueFrom.Visible = false;
                cmbValueTo.Visible = false;
                txtValueFrom.Text = string.Empty;
                txtValueTo.Text = string.Empty;
                txtValueFrom.Visible = false;
                txtValueTo.Visible = false;

                dtpDateFrom.Visible = true;
               // dtpDateFrom.Size = new System.Drawing.Size(128, 21);
                dtpDateFrom.TabIndex = 5;

                dtpDateTo.Visible = true;
               // dtpDateTo.Size = new System.Drawing.Size(128, 21);
                dtpDateTo.TabIndex = 6;

            }
            else if (GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()).ValueDataType.Equals(typeof(decimal)))
            {
                cmbValueFrom.Visible = false;
                cmbValueTo.Visible = false;
                dtpDateFrom.Visible = false;
                dtpDateTo.Visible = false;

                txtValueFrom.Visible = true;
                //txtValueFrom.Size = new System.Drawing.Size(128, 21);
                txtValueFrom.TabIndex = 5;

                txtValueTo.Visible = true;
                //txtValueTo.Size = new System.Drawing.Size(128, 21);
                txtValueTo.TabIndex = 6;
            }
            else if (reportDataStructTemp.IsManualRecordFilter)
            {
                cmbValueFrom.Visible = false;
                cmbValueTo.Visible = false;
                dtpDateFrom.Visible = false;
                dtpDateTo.Visible = false;
                txtValueFrom.Visible = false;
                txtValueTo.Visible = false;

                lblValue.Text = "Value";
                txtValue.Visible = true;
                //txtValueFrom.Size = new System.Drawing.Size(128, 21);
                txtValue.TabIndex = 5;
                
                //txtValueTo.Size = new System.Drawing.Size(128, 21);
                //txtValueTo.TabIndex = 6;
            }
            else
            {
                dtpDateFrom.Visible = false;
                dtpDateTo.Visible = false;
                txtValueFrom.Text = string.Empty;
                txtValueTo.Text = string.Empty;
                txtValueFrom.Visible = false;
                txtValueTo.Visible = false;
                cmbValueFrom.Visible = true;
                cmbValueTo.Visible = true;

                #region Load selected data
                switch (autoGenerateInfo.ModuleType)
                {
                    case 1: //Common Summary Report
                        ComReportGenerator comReportGenerator = new ComReportGenerator();
                        cmbValueFrom.DataSource = comReportGenerator.GetSelectionData(GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()), autoGenerateInfo);
                        cmbValueTo.DataSource = comReportGenerator.GetSelectionData(GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()), autoGenerateInfo);
                        break;
                    case 2: //Inventory Summary Report
                        InvReportGenerator invReportGenerator = new InvReportGenerator();
                        cmbValueTo.DataSource = invReportGenerator.GetSelectionData(GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()), autoGenerateInfo);
                        cmbValueFrom.DataSource = invReportGenerator.GetSelectionData(GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()), autoGenerateInfo);
                        break;
                    case 3: //Logistic Summary Report
                        LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
                        cmbValueFrom.DataSource = lgsReportGenerator.GetSelectionData(GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()), autoGenerateInfo);
                        cmbValueTo.DataSource = lgsReportGenerator.GetSelectionData(GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()), autoGenerateInfo);
                        break;
                    case 4: //CRM Summary Report
                        CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
                        cmbValueFrom.DataSource = crmReportGenerator.GetSelectionData(GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()), autoGenerateInfo);
                        cmbValueTo.DataSource = crmReportGenerator.GetSelectionData(GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()), autoGenerateInfo);
                        break;
                    case 5: //Accounts Summary Report
                        AccReportGenerator accReportGenerator = new AccReportGenerator();
                        cmbValueTo.DataSource = accReportGenerator.GetSelectionData(GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()), autoGenerateInfo);
                        cmbValueFrom.DataSource = accReportGenerator.GetSelectionData(GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()), autoGenerateInfo);
                        break;
                    case 6: //Gift Voucher Summary Report
                        GvReportGenerator gvReportGenerator = new GvReportGenerator();
                        cmbValueTo.DataSource = gvReportGenerator.GetSelectionData(GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()), autoGenerateInfo);
                        cmbValueFrom.DataSource = gvReportGenerator.GetSelectionData(GetSelectedReportDataStruct(cmbValueType.SelectedItem.ToString().Trim()), autoGenerateInfo);
                        break;
                    default:
                        break;
                }
                #endregion
            }
        }

        /// <summary>
        ///  Get ReportDataStruct of the selected report field
        /// </summary>
        /// <param name="selectedRepoertFieldName"></param>
        /// <returns></returns>
        private Common.ReportDataStruct GetSelectedReportDataStruct(string selectedRepoertFieldName)
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
        ///  Get ReportDataStruct of the selected report field
        /// </summary>
        /// <param name="selectedRepoertFieldName"></param>
        /// <returns></returns>
        private Common.ReportDataStruct GetReportGroupDataStruct(string selectedRepoertFieldName, bool groupStatus)
        {
            Common.ReportDataStruct reportDataStruct = new Common.ReportDataStruct();

            foreach (Common.ReportDataStruct reportDataStructItem in reportDatStructList)
            {
                if (reportDataStructItem.ReportFieldName.Trim().Equals(selectedRepoertFieldName.Trim()))
                {
                    reportDataStruct = reportDataStructItem;
                    reportDataStruct.IsResultGroupBy = groupStatus;
                    return reportDataStruct;
                }
            }

            return reportDataStruct;
        }

        /// <summary>
        ///  Get ReportDataStruct of the selected orderby report field
        /// </summary>
        /// <param name="selectedRepoertFieldName"></param>
        /// <returns></returns>
        private Common.ReportDataStruct GetReportOrderDataStruct(string selectedRepoertFieldName, bool orderStatus)
        {
            Common.ReportDataStruct reportDataStruct = new Common.ReportDataStruct();

            foreach (Common.ReportDataStruct reportDataStructItem in reportDatStructList)
            {
                if (reportDataStructItem.ReportFieldName.Trim().Equals(selectedRepoertFieldName.Trim()))
                {
                    reportDataStruct = reportDataStructItem;
                    reportDataStruct.IsResultOrderBy = orderStatus;
                    return reportDataStruct;
                }
            }

            return reportDataStruct;
        }

        /// <summary>
        ///  Get status of the default un-tick report field
        /// </summary>
        /// <param name="selectedRepoertFieldName"></param>
        /// <returns></returns>
        private bool GetReportCheckedStatusByDataStruct(string selectedRepoertFieldName)
        {
            bool fieldStatus = true;

            foreach (Common.ReportDataStruct reportDataStructItem in reportDatStructList)
            {
                if (reportDataStructItem.ReportFieldName.Trim().Equals(selectedRepoertFieldName.Trim()) && reportDataStructItem.IsUntickedField.Equals(true))
                {
                    fieldStatus = false;
                    return fieldStatus;
                }
            }

            return fieldStatus;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (dgvValueRange.Rows.Count < 1)
                { return; }
                LoadResult();
                Cursor.Current = Cursors.Default;                     
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        /// <summary>
        ///  Load Result data into dgvResult
        /// </summary>
        private void LoadResult()
        {            
            List<Common.ReportConditionsDataStruct> reportConditionsDataStructList = new List<Common.ReportConditionsDataStruct>();
            List<Common.ReportDataStruct> reportDataStructList = new List<Common.ReportDataStruct>();
            List<Common.ReportDataStruct> reportGroupDataStructList = new List<Common.ReportDataStruct>();
            List<Common.ReportDataStruct> reportOrderByDataStructList = new List<Common.ReportDataStruct>();
            
            reportConditionsDataStructList = GetReportConditions();
            reportDataStructList = GetReportFields();
            reportGroupDataStructList = GetGroupByFields();
            reportOrderByDataStructList = GetOrderByFields();
            dgvResult.DataSource = null;
            dgvResult.Rows.Clear();
            dgvResult.Refresh();
            
            
            #region View
            switch (autoGenerateInfo.ModuleType)
            {
                case 1: //Common Summary Report
                    ComReportGenerator comReportGenerator = new ComReportGenerator();
                    dgvResult.DataSource = comReportGenerator.GetResultData(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);
                    break;
                case 2: //Inventory Summary Report
                    InvReportGenerator invReportGenerator = new InvReportGenerator();
                    dgvResult.DataSource = invReportGenerator.GetResultData(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);
                    break;
                case 3: //Logistic Summary Report
                    LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
                    dgvResult.DataSource = lgsReportGenerator.GetResultData(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);
                    break;
                case 4: //CRM Summary Report
                    CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
                    dgvResult.DataSource = crmReportGenerator.GetResultData(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);
                    break;
                case 5: //Accounts Summary Report
                    AccReportGenerator accReportGenerator = new AccReportGenerator();
                    dgvResult.DataSource = accReportGenerator.GetResultData(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);
                    break;
                case 6: //Gift Voucher Summary Report
                    GvReportGenerator gvReportGenerator = new GvReportGenerator();
                    dgvResult.DataSource = gvReportGenerator.GetResultData(reportConditionsDataStructList, reportDataStructList, reportGroupDataStructList, reportOrderByDataStructList, autoGenerateInfo);
                    break;
                default:
                    break;
            }
            
            SetUpdgvResultColumns(reportDataStructList);
            
            
            #endregion
        }

        /// <summary>
        /// Set up dgvResult Columns
        /// </summary>
        /// <param name="reportDataStructList"></param>
        private void SetUpdgvResultColumns(List<Common.ReportDataStruct> reportDataStructList)
        {
            for (int i = 0; i < reportDataStructList.Count; i++)
            {
                if (dgvResult.Columns.Contains(reportDataStructList[i].ReportField.Trim()))
                {
                    dgvResult.Columns[reportDataStructList[i].ReportField.Trim()].HeaderText = reportDataStructList[i].ReportFieldName.Trim();
                    dgvResult.Columns[reportDataStructList[i].ReportField.Trim()].Visible = reportDataStructList[i].IsSelectionField;
                    //dgvResult.Columns[i].Visible = reportDataStructList[i].IsSelectionField;
                }
            }
        }


        /// <summary>
        /// Get report conditions
        /// </summary>
        /// <returns></returns>
        private List<Common.ReportConditionsDataStruct> GetReportConditions()
         {
            string conditionFrom="";
            string conditionTo="";
            List<Common.ReportConditionsDataStruct> reportConditionsDataStructList = new List<Common.ReportConditionsDataStruct>();
            // Fill reportConditionsDataStructList
            foreach (DataGridViewRow drValueRange in dgvValueRange.Rows)
            {
                Common.ReportDataStruct reportDataStruct = GetSelectedReportDataStruct(drValueRange.Cells["ValueType"].Value.ToString().Trim());
                
                    #region View
                    switch (autoGenerateInfo.ModuleType)
                    {
                        case 1: //Common Summary Report
                            ComReportGenerator comReportGenerator = new ComReportGenerator();
                            conditionFrom = reportDataStruct.IsJoinField.Equals(true) ? comReportGenerator.GetConditionValue(reportDataStruct, drValueRange.Cells["ValueFrom"].Value.ToString().Trim()) : drValueRange.Cells["ValueFrom"].Value.ToString().Trim();
                            conditionTo = reportDataStruct.IsJoinField.Equals(true) ? comReportGenerator.GetConditionValue(reportDataStruct, drValueRange.Cells["ValueTo"].Value.ToString().Trim()) : drValueRange.Cells["ValueTo"].Value.ToString().Trim();
                            break;
                        case 2: //Inventory Summary Report
                            InvReportGenerator invReportGenerator = new InvReportGenerator();
                            conditionFrom = reportDataStruct.IsJoinField.Equals(true) ? invReportGenerator.GetConditionValue(reportDataStruct, drValueRange.Cells["ValueFrom"].Value.ToString().Trim()) : drValueRange.Cells["ValueFrom"].Value.ToString().Trim();
                            conditionTo = reportDataStruct.IsJoinField.Equals(true) ? invReportGenerator.GetConditionValue(reportDataStruct, drValueRange.Cells["ValueTo"].Value.ToString().Trim()) : drValueRange.Cells["ValueTo"].Value.ToString().Trim();
                            break;
                        case 3: //Logistic Summary Report
                            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
                            conditionFrom = reportDataStruct.IsJoinField.Equals(true) ? lgsReportGenerator.GetConditionValue(reportDataStruct, drValueRange.Cells["ValueFrom"].Value.ToString().Trim()) : drValueRange.Cells["ValueFrom"].Value.ToString().Trim();
                            conditionTo = reportDataStruct.IsJoinField.Equals(true) ? lgsReportGenerator.GetConditionValue(reportDataStruct, drValueRange.Cells["ValueTo"].Value.ToString().Trim()) : drValueRange.Cells["ValueTo"].Value.ToString().Trim();
                            break;
                        case 4: //CRM Summary Report
                            CrmReportGenerator CrmReportGenerator = new CrmReportGenerator();
                            conditionFrom = reportDataStruct.IsJoinField.Equals(true) ? CrmReportGenerator.GetConditionValue(reportDataStruct, drValueRange.Cells["ValueFrom"].Value.ToString().Trim()) : drValueRange.Cells["ValueFrom"].Value.ToString().Trim();
                            conditionTo = reportDataStruct.IsJoinField.Equals(true) ? CrmReportGenerator.GetConditionValue(reportDataStruct, drValueRange.Cells["ValueTo"].Value.ToString().Trim()) : drValueRange.Cells["ValueTo"].Value.ToString().Trim();
                            break;
                        case 5: //Accounts Summary Report
                            AccReportGenerator accReportGenerator = new AccReportGenerator();
                            conditionFrom = reportDataStruct.IsJoinField.Equals(true) ? accReportGenerator.GetConditionValue(reportDataStruct, drValueRange.Cells["ValueFrom"].Value.ToString().Trim()) : drValueRange.Cells["ValueFrom"].Value.ToString().Trim();
                            conditionTo = reportDataStruct.IsJoinField.Equals(true) ? accReportGenerator.GetConditionValue(reportDataStruct, drValueRange.Cells["ValueTo"].Value.ToString().Trim()) : drValueRange.Cells["ValueTo"].Value.ToString().Trim();
                            break;
                        case 6: //Gift Voucher Summary Report
                            GvReportGenerator gvReportGenerator = new GvReportGenerator();
                            conditionFrom = reportDataStruct.IsJoinField.Equals(true) ? gvReportGenerator.GetConditionValue(reportDataStruct, drValueRange.Cells["ValueFrom"].Value.ToString().Trim()) : drValueRange.Cells["ValueFrom"].Value.ToString().Trim();
                            conditionTo = reportDataStruct.IsJoinField.Equals(true) ? gvReportGenerator.GetConditionValue(reportDataStruct, drValueRange.Cells["ValueTo"].Value.ToString().Trim()) : drValueRange.Cells["ValueTo"].Value.ToString().Trim();
                            break;
                        default:
                            break;
                    }
                    #endregion

                                    
                reportConditionsDataStructList.Add(new Common.ReportConditionsDataStruct() { ReportDataStruct = reportDataStruct, ConditionFrom = conditionFrom, ConditionTo = conditionTo});
            }
            return reportConditionsDataStructList;
        }

        private List<Common.ReportDataStruct> GetReportFields()
        {
            List<Common.ReportDataStruct> reportDataStructFielsList = new List<Common.ReportDataStruct>();

            for (int itemIndex = 0; itemIndex <  chkLstFieldSelectionStr.Items.Count; itemIndex++)
            {                                                        
                if (chkLstFieldSelectionStr.GetItemCheckState(itemIndex).Equals(CheckState.Checked))
                {                    
                    Common.ReportDataStruct reportDataStructField = GetSelectedReportDataStruct(chkLstFieldSelectionStr.Items[itemIndex].ToString().Trim()); 
                    reportDataStructField.IsSelectionField = true;
                    reportDataStructFielsList.Add(reportDataStructField);
                }
                else
                {
                    Common.ReportDataStruct reportDataStructField = GetSelectedReportDataStruct(chkLstFieldSelectionStr.Items[itemIndex].ToString().Trim()); 
                    reportDataStructField.IsSelectionField = false;
                    reportDataStructFielsList.Add(reportDataStructField);
                }
            }

             for (int itemIndex = 0; itemIndex <  chkLstFieldSelectionDes.Items.Count; itemIndex++)
            {
                if (chkLstFieldSelectionDes.GetItemCheckState(itemIndex).Equals(CheckState.Checked))
                {
                    Common.ReportDataStruct reportDataStructField = GetSelectedReportDataStruct(chkLstFieldSelectionDes.Items[itemIndex].ToString().Trim());
                    reportDataStructField.IsSelectionField = true;
                    reportDataStructFielsList.Add(reportDataStructField);
                }
                else
                {
                    Common.ReportDataStruct reportDataStructField = GetSelectedReportDataStruct(chkLstFieldSelectionDes.Items[itemIndex].ToString().Trim());
                    reportDataStructField.IsSelectionField = false;
                    reportDataStructFielsList.Add(reportDataStructField);
                }
            }

            return reportDataStructFielsList;
        }

        private List<Common.ReportDataStruct> GetGroupByFields()
        {
            List<Common.ReportDataStruct> reportDataStructList = new List<Common.ReportDataStruct>();
            // Fill reportDataStructList

            for (int i = 0; i < chkLstGroupBy.Items.Count; i++)
            {
                reportDataStructList.Add(GetReportGroupDataStruct(chkLstGroupBy.Items[i].ToString().Trim(), chkLstGroupBy.GetItemChecked(i)));   
            }

            return reportDataStructList;
        }


        private List<Common.ReportDataStruct> GetOrderByFields()
        {
            List<Common.ReportDataStruct> reportDataStructList = new List<Common.ReportDataStruct>();
            
            for (int i = 0; i < chkLstOrderBy.Items.Count; i++)
            {
                reportDataStructList.Add(GetReportOrderDataStruct(chkLstOrderBy.Items[i].ToString().Trim(), chkLstOrderBy.GetItemChecked(i)));
            }

            return reportDataStructList;
        }

        //private List<Common.ReportDataStruct> GetGroupByFields(List<Common.ReportDataStruct> reportDataStructList)
        //{
        //    for (int i = 0; i < reportDataStructList.Count; i++)
        //    {
        //        if (chkLstGroupBy.CheckedItems.OfType<string>().Any(p => p.ToString() == reportDataStructList[i].ReportFieldName))
        //        {
        //            reportDataStructList[i].ReportFieldName = "true";
        //        }
        //        //reportDataStructList[i].IsGroupBy = 
        //    }

        //    foreach (var item in reportDataStructList)
        //    {
        //        //var rr = reportDataStructList.AsEnumerable().Where(p => p.ReportFieldName.Equals(item.ToString())).Select(p=> p.i);
                
        //    }

        //    return reportDataStructList;
        //}

        ///// <summary>
        ///// Set up column visibilities
        ///// </summary>
        ///// <param name="dtData"></param>
        //private void SetUpdgvResultColumnVisibilities(DataTable dtData)
        //{            
        //    foreach (var column in dtData.Columns.Cast<DataColumn>().ToArray())
        //    {
        //        if (dtData.AsEnumerable().All(dr => dr.IsNull(column)))
        //        {
        //            dgvResult.Columns[column.ColumnName].Visible = false;
        //        }
        //        else
        //        {
        //            if (dtData.AsEnumerable().All(dr => dr[column.ColumnName].ToString() == string.Empty))
        //            {
        //                dgvResult.Columns[column.ColumnName].Visible = false;
        //            }
        //            else
        //            {
        //                dgvResult.Columns[column.ColumnName].Visible = true;
        //            }
        //        }
        //    }            
        //}

        private void dgvResult_DoubleClick(object sender, EventArgs e)
        {            
            try
            {
                
                if (dgvResult.CurrentCell != null && dgvResult.CurrentCell.RowIndex >= 0)
                {
                    int documentNoColumnIndex = -1;

                    foreach (DataGridViewColumn col in dgvResult.Columns)
                    {
                        if (string.Equals(col.HeaderText.Trim(), "Document No"))
                        {
                            documentNoColumnIndex = col.Index;
                        }
                    }

                    //documentNoColumnIndex = dgvResult.Columns.OfType<DataGridViewColumn>().ToList()
                                        //.Where(col => col.HeaderText == "Document No").Select(col => col.Index).First();// FirstOrDefault();

                    if (int.Equals(documentNoColumnIndex, -1))
                    { return; }

                    #region View
                    switch (autoGenerateInfo.ModuleType)
                    {
                        case 1: //Common Summary Report
                            ComReportGenerator comReportGenerator = new ComReportGenerator();
                            switch (autoGenerateInfo.ReportType)
                            {
                                case 1: // Reference
                                    comReportGenerator.GenearateReferenceReport(autoGenerateInfo, dgvResult[0, dgvResult.CurrentCell.RowIndex].Value.ToString().Trim(), false);
                                    break;
                                case 2: // Transaction
                                    if (int.Equals(documentNoColumnIndex, -1))
                                    { return; }
                                    comReportGenerator.GenearateTransactionReport(autoGenerateInfo, dgvResult[documentNoColumnIndex, dgvResult.CurrentCell.RowIndex].Value.ToString().Trim(), 2);
                                    break;
                                default:
                                    break;
                            }

                            break;
                        case 2: //Inventory Summary Report
                            InvReportGenerator invReportGenerator = new InvReportGenerator();
                            switch (autoGenerateInfo.ReportType)
                            {
                                case 1: // Reference
                                    invReportGenerator.GenearateReferenceReport(autoGenerateInfo, dgvResult[0, dgvResult.CurrentCell.RowIndex].Value.ToString().Trim(), false);
                                    break;
                                case 2: // Transaction
                                    if (int.Equals(documentNoColumnIndex, -1))
                                    { return; }
                                    invReportGenerator.GenearateTransactionReport(autoGenerateInfo, dgvResult[documentNoColumnIndex, dgvResult.CurrentCell.RowIndex].Value.ToString().Trim(), 2);
                                    break;
                                default:
                                    break;
                            }

                            break;
                        case 3: //Logistic Summary Report
                            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
                            switch (autoGenerateInfo.ReportType)
                            {
                                case 1: // Reference
                                    lgsReportGenerator.GenearateReferenceReport(autoGenerateInfo, dgvResult[0, dgvResult.CurrentCell.RowIndex].Value.ToString().Trim(), false);
                                    break;
                                case 2: // Transaction
                                    if(int.Equals(documentNoColumnIndex, -1))
                                    { return; }
                                    lgsReportGenerator.GenearateTransactionReport(autoGenerateInfo, dgvResult[documentNoColumnIndex, dgvResult.CurrentCell.RowIndex].Value.ToString().Trim(), 2);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 4: //CRM Summary Report
                            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
                            switch (autoGenerateInfo.ReportType)
                            {
                                case 1: // Reference
                                    if (dgvResult.Columns.Contains("CustomerCode"))
                                    {crmReportGenerator.GenearateReferenceReport(autoGenerateInfo, dgvResult[2, dgvResult.CurrentCell.RowIndex].Value.ToString().Trim());}
                                    else
                                    { crmReportGenerator.GenearateReferenceReport(autoGenerateInfo, dgvResult[0, dgvResult.CurrentCell.RowIndex].Value.ToString().Trim()); }
                                    break;
                                case 2: // Transaction
                                    
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 5: //Accounts Summary Report
                            AccReportGenerator accReportGenerator = new AccReportGenerator();
                            switch (autoGenerateInfo.ReportType)
                            {
                                case 1: // Reference
                                    accReportGenerator.GenearateReferenceReport(autoGenerateInfo, dgvResult[0, dgvResult.CurrentCell.RowIndex].Value.ToString().Trim(), false);
                                    break;
                                case 2: // Transaction
                                    if (int.Equals(documentNoColumnIndex, -1))
                                    { return; }
                                    accReportGenerator.GenerateTransactionReport(autoGenerateInfo, dgvResult[documentNoColumnIndex, dgvResult.CurrentCell.RowIndex].Value.ToString().Trim(), 2);
                                    break;
                                default:
                                    break;
                            }

                            break;
                        case 6: //Gift Voucher Summary Report
                            GvReportGenerator gvReportGenerator = new GvReportGenerator();
                            switch (autoGenerateInfo.ReportType)
                            {
                                case 1: // Reference
                                    gvReportGenerator.GenearateReferenceReport(autoGenerateInfo, dgvResult[0, dgvResult.CurrentCell.RowIndex].Value.ToString().Trim(), false);
                                    break;
                                case 2: // Transaction
                                    if (int.Equals(documentNoColumnIndex, -1))
                                    { return; }
                                    gvReportGenerator.GenearateTransactionReport(autoGenerateInfo, dgvResult[documentNoColumnIndex, dgvResult.CurrentCell.RowIndex].Value.ToString().Trim(), 2);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dgvValueRange_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgvResult.Rows.Count < 0 || !e.KeyCode.Equals(Keys.F2))
                { return; }

                if (Toast.Show("Filter - " + dgvValueRange[0, dgvValueRange.CurrentCell.RowIndex].Value.ToString(), Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                { return; }

                dgvValueRange.Rows.RemoveAt(dgvValueRange.CurrentCell.RowIndex);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkLstFieldSelectionStr_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (chkLstGroupBy.CheckedIndices.Count > 0 && int.Equals(autoGenerateInfo.ReportType, 2))
                {
                    e.NewValue = e.CurrentValue;
                    return;
                }

                #region Validate Selection Filed Count
                if (maxStringFields > 0)  
                {
                    if (!ValidateStringSelectionFiledCount(maxStringFields, e.NewValue))
                    {
                        e.NewValue = e.CurrentValue;
                        Toast.Show("Maxium number of fields can be seleted is " + maxStringFields + ".", Toast.messageType.Information, Toast.messageAction.General);
                        return;
                    }
                }
                #endregion

                #region Disable unchecking mandatory fields
                foreach (var mandField in reportDatStructList.AsEnumerable().Where(i => i.IsMandatoryField.Equals(true)).ToList())
                {
                    if (string.Equals(GetSelectedReportDataStruct(chkLstFieldSelectionStr.Items[e.Index].ToString().Trim()).ReportFieldName, mandField.ReportFieldName))
                    {
                        this.chkLstFieldSelectionStr.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstFieldSelectionStr_ItemCheck);
                        e.NewValue = e.CurrentValue;
                        this.chkLstFieldSelectionStr.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstFieldSelectionStr_ItemCheck);
                        return;
                    }
                }
                #endregion

                if (e.NewValue.Equals(CheckState.Unchecked))
                {
                    SetUpDependingFieldsStatuses(GetSelectedReportDataStruct(chkLstFieldSelectionStr.Items[e.Index].ToString().Trim()));
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkLstFieldSelectionDes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                #region Validate Selection Filed Count
                if (maxDecimalFields > 0)
                {
                    if (!ValidateDecimalSelectionFiledCount(maxDecimalFields, e.NewValue))
                    {
                        e.NewValue = e.CurrentValue;
                        Toast.Show("Maxium number of fields can be seleted is " + maxDecimalFields.ToString() + ".", Toast.messageType.Information, Toast.messageAction.General);
                        return;
                    }
                }
                #endregion

                #region Disable unchecking mandatory fields

                foreach (var mandField in reportDatStructList.AsEnumerable().Where(i => i.IsMandatoryField.Equals(true)).ToList())
                {
                    if (string.Equals(GetSelectedReportDataStruct(chkLstFieldSelectionDes.Items[e.Index].ToString().Trim()).ReportFieldName, mandField.ReportFieldName))
                    {
                        this.chkLstFieldSelectionDes.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstFieldSelectionDes_ItemCheck);
                        e.NewValue = e.CurrentValue;
                        this.chkLstFieldSelectionDes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstFieldSelectionDes_ItemCheck);
                        return;
                    }
                }

                #endregion

                if (e.NewValue.Equals(CheckState.Unchecked))
                {
                    SetUpDependingFieldsStatuses(GetSelectedReportDataStruct(chkLstFieldSelectionDes.Items[e.Index].ToString().Trim()));
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }        
        }

        private void chkLstGroupBy_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                #region Validate Selection Filed Count
                if (maxGroups > 0)
                {
                    if (!ValidateGroupSelectionFiledCount(maxGroups, e.NewValue))
                    {
                        e.NewValue = e.CurrentValue;
                        Toast.Show("Maxium number of group fields can be seleted is " + maxGroups + ".", Toast.messageType.Information, Toast.messageAction.General);
                        return;
                    }

                    //if (int.Equals(autoGenerateInfo.ReportType, 2))
                    //{
                    //    if (!ValidateSelectionFiledCount(maxGroups, e.NewValue))
                    //    {
                    //        e.NewValue = e.CurrentValue;
                    //        Toast.Show("Maxium number of fields can be seleted is " + maxGroups + ".", Toast.messageType.Information, Toast.messageAction.General);
                    //        return;
                    //    }
                    //}
                }
                #endregion

                SetUpGroupDependingFieldsStatuses(e);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }        
        }

        private void chkLstOrderBy_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (e.NewValue.Equals(CheckState.Checked))
                {
                    if (!CheckDependingFieldsStatuses(GetSelectedReportDataStruct(chkLstOrderBy.Items[e.Index].ToString().Trim())))
                    {
                        e.NewValue = e.CurrentValue;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }        
        }

        private void chklstColumnTotal_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (e.NewValue.Equals(CheckState.Checked))
                {
                    if (!CheckDependingFieldsStatuses(GetSelectedReportDataStruct(chklstColumnTotal.Items[e.Index].ToString().Trim())))
                    {
                        e.NewValue = e.CurrentValue;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }        
        }

        private void chkRowTotal_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (e.NewValue.Equals(CheckState.Checked))
                {
                    if (!CheckDependingFieldsStatuses(GetSelectedReportDataStruct(chkRowTotal.Items[e.Index].ToString().Trim())))
                    {
                        e.NewValue = e.CurrentValue;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }        
        }

        /// <summary>
        /// Setup string Fields Statuses depending on selected groups
        /// </summary>
        private void SetUpGroupDependingFieldsStatuses(ItemCheckEventArgs e)
        {
            if (int.Equals(autoGenerateInfo.ReportType, 1)) // && (chkLstGroupBy.CheckedIndices.Count - 1) <= 0) //&& (e.NewValue.Equals(CheckState.Checked)))
            {
                return;
            }

            this.chkLstFieldSelectionStr.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstFieldSelectionStr_ItemCheck);
            this.chkLstFieldSelectionDes.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstFieldSelectionDes_ItemCheck);
            ArrayList checkedGroupsList = new ArrayList();
            
            foreach (var chkLstGrpItem in chkLstGroupBy.CheckedItems)
            {
                checkedGroupsList.Add(chkLstGrpItem.ToString());
            }

            if (e.NewValue.Equals(CheckState.Checked))
            {
                checkedGroupsList.Add(chkLstGroupBy.Items[e.Index].ToString());
            }
            if (e.NewValue.Equals(CheckState.Unchecked))
            {
                checkedGroupsList.Remove(chkLstGroupBy.Items[e.Index].ToString());
            }

            // uncheck all items in chkLstFieldSelectionStr
            for (int i = 0; i < chkLstFieldSelectionStr.Items.Count; i++)
            {
                chkLstFieldSelectionStr.SetItemCheckState(i, CheckState.Unchecked);
            }

            foreach (var grpItem in checkedGroupsList)
            {
                for (int i = 0; i < chkLstFieldSelectionStr.Items.Count; i++)
                {
                    if (string.Equals(chkLstFieldSelectionStr.Items[i].ToString(), grpItem.ToString()) && chkLstFieldSelectionStr.GetItemCheckState(i).Equals(CheckState.Unchecked))
                    {
                        chkLstFieldSelectionStr.SetItemCheckState(i, CheckState.Checked);
                    }
                }    
            }


            if (chkLstGroupBy.CheckedItems.Count - 1 <= 0 && e.NewValue.Equals(CheckState.Unchecked))
            {
                // Check mandatory fields
                foreach (var mandField in reportDatStructList.AsEnumerable().Where(i => i.IsMandatoryField.Equals(true)).ToList())
                {
                    for (int i = 0; i < chkLstFieldSelectionStr.Items.Count; i++)
                    {
                        if (string.Equals(mandField.ReportFieldName, chkLstFieldSelectionStr.Items[i].ToString()) && chkLstFieldSelectionStr.GetItemCheckState(i).Equals(CheckState.Unchecked))
                        {
                            chkLstFieldSelectionStr.SetItemCheckState(i, CheckState.Checked);
                        }
                    }

                    for (int i = 0; i < chkLstFieldSelectionDes.Items.Count; i++)
                    {
                        if (string.Equals(mandField.ReportFieldName, chkLstFieldSelectionDes.Items[i].ToString()) && chkLstFieldSelectionDes.GetItemCheckState(i).Equals(CheckState.Unchecked))
                        {
                            chkLstFieldSelectionDes.SetItemCheckState(i, CheckState.Checked);
                        }
                    }
                }
            }

            this.chkLstFieldSelectionStr.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstFieldSelectionStr_ItemCheck);
            this.chkLstFieldSelectionDes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstFieldSelectionDes_ItemCheck);
         }

        /// <summary>
        /// Change checked statuses of depending fields in chkLstGroupBy, chkLstOrderBy, chklstColumnTotal and chkRowTotal grid views
        /// </summary>
        /// <param name="reportDataStruct"></param>
        private void SetUpDependingFieldsStatuses(Common.ReportDataStruct reportDataStruct)
        {
            //chkLstGroupBy
            foreach (int itemIndex in chkLstGroupBy.CheckedIndices)
            {
                if (string.Equals(chkLstGroupBy.Items[itemIndex].ToString().Trim(), reportDataStruct.ReportFieldName))
                {
                    this.chkLstGroupBy.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstGroupBy_ItemCheck);
                    chkLstGroupBy.SetItemCheckState(itemIndex, CheckState.Unchecked);
                    this.chkLstGroupBy.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstGroupBy_ItemCheck);
                }
            }

            //chkLstOrderBy
            foreach (int itemIndex in chkLstOrderBy.CheckedIndices)
            {
                if (string.Equals(chkLstOrderBy.Items[itemIndex].ToString().Trim(), reportDataStruct.ReportFieldName))
                {
                    this.chkLstOrderBy.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.chkLstOrderBy_ItemCheck);
                    chkLstOrderBy.SetItemCheckState(itemIndex, CheckState.Unchecked);
                    this.chkLstOrderBy.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstOrderBy_ItemCheck);
                }
            }

            //chklstColumnTotal
            foreach (int itemIndex in chklstColumnTotal.CheckedIndices)
            {
                if (string.Equals(chklstColumnTotal.Items[itemIndex].ToString().Trim(), reportDataStruct.ReportFieldName))
                {
                    chklstColumnTotal.SetItemCheckState(itemIndex, CheckState.Unchecked);
                }
            }

            //chkRowTotal
            foreach (int itemIndex in chkRowTotal.CheckedIndices)
            {
                if (string.Equals(chkRowTotal.Items[itemIndex].ToString().Trim(), reportDataStruct.ReportFieldName))
                {
                    chkRowTotal.SetItemCheckState(itemIndex, CheckState.Unchecked);
                }
            }
        }

        /// <summary>
        /// Check the stateses of chkLstFieldSelectionStr and chkLstFieldSelectionDes before change the current state of given field
        /// </summary>
        /// <param name="reportDataStruct"></param>
        /// <returns></returns>
        private bool CheckDependingFieldsStatuses(Common.ReportDataStruct reportDataStruct)
        {
            foreach (int itemIndex in chkLstFieldSelectionStr.CheckedIndices)
            {
                if (string.Equals(chkLstFieldSelectionStr.Items[itemIndex].ToString().Trim(), reportDataStruct.ReportFieldName))
                {
                    return true;
                }   
            }

            foreach (int itemIndex in chkLstFieldSelectionDes.CheckedIndices)
            {
                if (string.Equals(chkLstFieldSelectionDes.Items[itemIndex].ToString().Trim(), reportDataStruct.ReportFieldName))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Validate maximum nuber of string fields can be selected for the report
        /// </summary>
        /// <param name="maxStringFields"></param> number of maximum string fields can be selected
        /// <param name="checkState"></param>
        /// <returns></returns>
        private bool ValidateStringSelectionFiledCount(int maxStringFields, CheckState checkState)
        {
            if (checkState.Equals(CheckState.Checked) && (chkLstFieldSelectionStr.CheckedIndices.Count + 1) > maxStringFields)
            { return false; }
            else
            { return true; }
        }

        /// <summary>
        /// Validate maximum nuber of decimal fields can be selected for the report
        /// </summary>
        /// <param name="maxDecimalFields"></param> number of maximum deciaml fields can be selected
        /// <param name="checkState"></param>
        /// <returns></returns>
        private bool ValidateDecimalSelectionFiledCount(int maxDecimalFields, CheckState checkState)
        {
            if (checkState.Equals(CheckState.Checked) && (chkLstFieldSelectionDes.CheckedIndices.Count + 1) > maxDecimalFields)
            { return false; }
            else
            { return true; }
        }

        /// <summary>
        /// Validate maximum nuber of groups can be selected for the report
        /// </summary>
        /// <param name="maxGroupFields"></param> number of maximum groups can be selected
        /// <param name="checkState"></param>
        /// <returns></returns>
        private bool ValidateGroupSelectionFiledCount(int maxGroupFields, CheckState checkState)
        {
            if (checkState.Equals(CheckState.Checked) && (chkLstGroupBy.CheckedIndices.Count + 1) > maxGroupFields)
            { return false; }
            else
            { return true; }
        }

        private void cmbValueFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbValueFrom.Text))
                {
                   if (cmbValueFrom.FindStringExact(cmbValueFrom.Text.Trim()) < 0)
                    {
                        Toast.Show("Entered Value ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        cmbValueFrom.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbValueTo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbValueTo.Text))
                {
                    if (cmbValueTo.FindStringExact(cmbValueTo.Text.Trim()) < 0)
                    {
                        Toast.Show("Entered Value ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        cmbValueTo.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            //GetConditionsDataTable();
            //DateTime dttr = Common.FormatDate(dtpDateFrom.Value);

        }

        private void chkViewGroupDetails_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkLstGroupBy.CheckedIndices.Count < 1)
                {
                    chkViewGroupDetails.Checked = false;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkLstGroupBy_Leave(object sender, EventArgs e)
        {
            try
            {
                if (chkLstGroupBy.CheckedIndices.Count < 1)
                {
                    chkViewGroupDetails.Checked = false;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        
        }

        private void btnGroupByUp_Click(object sender, EventArgs e)
        {
            try
            {
                ReArrangeCheckedListBox(chkLstGroupBy, groupbyField, true);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void btnGroupByDown_Click(object sender, EventArgs e)
        {
            try
            {
                ReArrangeCheckedListBox(chkLstGroupBy, groupbyField, false);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        /// <summary>
        /// Re arrange the order of the items of the checked list box
        /// </summary>
        /// <param name="chkLstBox"></param>
        /// <param name="move"></param> true = move up, false = move down
        private void ReArrangeCheckedListBox(CheckedListBox chkLstBox, ArrayList sourceList, bool move)
        { 
            if (chkLstBox.Items.Count <= 0)
            { return; }

            if (bool.Equals(move, true))
            {
                if (int.Equals(chkLstBox.SelectedIndex, 0))
                { return; }
            }
            else
            {
                if (int.Equals((chkLstBox.SelectedIndex), chkLstBox.Items.Count-1))
                { return; }
            }

            //chkLstGroupBy.CheckedItems.OfType<string>().Any(p => p.ToString() == reportDataStructList[i].ReportFieldName))
            
            // Get checked items
            ArrayList checkedItems = new ArrayList();
            foreach (var item in chkLstBox.CheckedItems)
            {
                checkedItems.Add(item.ToString());
            }

            // Re arranage source list
            for (int i = 0; i < sourceList.Count; i++)
            {
                if (string.Equals(sourceList[i].ToString().Trim(), chkLstBox.SelectedItem.ToString().Trim()))
                {
                    sourceList.Insert(bool.Equals(move, true) ? (i - 1) : (i + 2), chkLstBox.SelectedItem.ToString());
                    sourceList.RemoveAt(bool.Equals(move, true) ? (i + 1) : (i));
                    break;
                }
            }

            // Reset data source
            chkLstBox.DataSource = null;
            chkLstBox.Items.Clear();
            chkLstBox.DataSource = sourceList;

            // Set checked items
            for (int i = 0; i < chkLstBox.Items.Count; i++)
            {
                foreach (var item in checkedItems)
                {
                    if (string.Equals(item.ToString(), chkLstBox.Items[i].ToString()))
                    {
                        chkLstBox.SetItemChecked(i, true);
                    }
                }
            }

            // Reset selected item
            chkLstBox.SetSelected(bool.Equals(move, true) ? (chkLstBox.SelectedIndex - 1) : (chkLstBox.SelectedIndex + 1), true);
            
        }

        /// <summary>
        /// Get report condtions and convert it into data table
        /// </summary>
        /// <returns></returns>
        private DataTable GetConditionsDataTable()
        {
            DataTable dtCondtions = new DataTable();
            dtCondtions.Columns.Add("ValueType", typeof(string));
            dtCondtions.Columns.Add("ValueFrom", typeof(string));
            dtCondtions.Columns.Add("ValueTo", typeof(string));

            foreach (DataGridViewRow dgvRow in dgvValueRange.Rows)
            {
                dtCondtions.Rows.Add(dgvRow.Cells["ValueType"].Value.ToString(), dgvRow.Cells["ValueFrom"].Value.ToString(), dgvRow.Cells["ValueTo"].Value.ToString());
            }

            return dtCondtions;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvResult.DataSource = null;
            dgvValueRange.DataSource = null;

        }

    }
}
