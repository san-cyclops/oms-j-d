using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Domain;
using Report.Inventory;
using Service;
using Utility;
using System.IO;
using System.Reflection;
using System.Linq;

namespace UI.Windows
{
    public partial class FrmCashierGroup : UI.Windows.FrmBaseMasterForm
    {
        /// <summary>
        /// Nuwan
        /// </summary>

        List<CashierGroup> initialCashierFunctionList = new List<CashierGroup>();
        List<CashierGroup> existingCashierGroupList = new List<CashierGroup>();
        List<Location> existingLocationList = new List<Location>();
        long orderNo;
        decimal value;
        bool isAccess;

        public FrmCashierGroup()
        {
            InitializeComponent();
        }

        public void GetInitialFunctionList()
        {
            CashierGroupService cashierGroupService = new CashierGroupService();


            //CashierPermissionService cashierPermissionService = new CashierPermissionService();
            //existingCashierFunctionList = cashierPermissionService.GetAllCashierFunctionsForCashierPermission();
            //dgvCashier.DataSource = existingCashierPermissionList;
            //dgvCashier.Refresh();
        }

        public override void FormLoad()
        {
            try
            {
                dgvCashier.AutoGenerateColumns = false;
                dgvLocationInfo.AutoGenerateColumns = false;

                grpLocation.Enabled = false;
                //InitializeForm();

                // Load Employee Designations
                CashierGroupService cashierGroupService = new CashierGroupService();
                Common.LoadEmployeeDesignations(cmbGroup, cashierGroupService.GetAllEmployeeDesignationTypes());

                chkUpdateFunction.Text = "Update Above Function To \nSelected locations";

                base.FormLoad();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        public override void InitializeForm()
        {
            try
            {
                Employee employee = new Employee();
                EmployeeService employeeService = new EmployeeService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();

                CashierGroupService cashierGroupService = new CashierGroupService();
                //initialCashierFunctionList = cashierGroupService.GetInitialFunctions();
                //dgvCashier.DataSource = initialCashierFunctionList;
                existingCashierGroupList = cashierGroupService.GetInitialFunctions();
                dgvCashier.DataSource = existingCashierGroupList;
                dgvCashier.Refresh();

                existingLocationList = new List<Location>();

                orderNo = 0;
                value = 0;
                isAccess = false;
                grpLocation.Enabled = false;

                LoadAllLocations();

                cmbGroup.SelectedIndex = -1;
                chkUpdateAllCashiers.Enabled = false;

                Common.EnableButton(true, btnSave);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public static void SetAutoBindRecords(ComboBox comboBox, string[] stringCollection)
        {
            try
            {
                AutoCompleteStringCollection autoCompleteCode = new AutoCompleteStringCollection();
                autoCompleteCode.AddRange(stringCollection);
                comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox.AutoCompleteCustomSource = autoCompleteCode;
                comboBox.DataSource = autoCompleteCode;
                comboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), "FrmCustomer", Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void UpdateList()
        {
            //existingCashierGroupList = initialCashierFunctionList;
            //existingCashierGroupList = new List<CashierGroup>();

            foreach (DataGridViewRow row in dgvCashier.Rows)
            {
                CashierGroupService cashierGroupService = new CashierGroupService();
                CashierGroup cashierGroup = new CashierGroup();

                if (dgvCashier["FunctionName", row.Index].Value.ToString() != string.Empty) { cashierGroup.FunctionName = dgvCashier["FunctionName", row.Index].Value.ToString().Trim(); }
                else { cashierGroup.FunctionName = string.Empty; }

                if (dgvCashier["FunctionDescription", row.Index].Value.ToString() != string.Empty) { cashierGroup.FunctionDescription = dgvCashier["FunctionDescription", row.Index].Value.ToString().Trim(); }
                else { cashierGroup.FunctionDescription = string.Empty; }

                if (Convert.ToBoolean(dgvCashier["Access", row.Index].Value) == true) { cashierGroup.IsAccess = true; }
                else { cashierGroup.IsAccess = false; }

                if (dgvCashier["Value", row.Index].Value.ToString() != string.Empty) { cashierGroup.Value = Common.ConvertStringToDecimalCurrency(dgvCashier["Value", row.Index].Value.ToString().Trim()); }
                else { cashierGroup.Value = Common.ConvertStringToDecimalCurrency("0"); }

                cashierGroup.IsValue = Convert.ToBoolean(dgvCashier["IsValue", row.Index].Value);

                //long order = Common.ConvertStringToLong(dgvCashier["RowNo", row.Index].Value.ToString().Trim()) + 1;
                //cashierGroup.Order = order;
                long order = Common.ConvertStringToLong(dgvCashier["RowNo", row.Index].Value.ToString().Trim());
                cashierGroup.Order = order;

                cashierGroup.EmployeeDesignationTypeID = Common.ConvertStringToInt(cmbGroup.SelectedValue.ToString());

                existingCashierGroupList = cashierGroupService.UpdateCashierGroup(existingCashierGroupList, cashierGroup);
            }

            value = existingCashierGroupList.Where(cg => cg.Order.Equals(orderNo)).Select(cg => cg.Value).FirstOrDefault();
            isAccess = existingCashierGroupList.Where(cg => cg.Order.Equals(orderNo)).Select(cg => cg.IsAccess).FirstOrDefault();
        }

        public void GetSelectedLocations()
        {
            for (int i = 0; i < dgvLocationInfo.RowCount; i++)
            {
                if (Convert.ToBoolean(dgvLocationInfo.Rows[i].Cells["Selection"].Value) == true)
                {
                    Location location = new Location();
                    LocationService locationService = new LocationService();
                    int locID = Common.ConvertStringToInt(dgvLocationInfo.Rows[i].Cells["LocationID"].Value.ToString().Trim());

                    location = locationService.GetLocationsByID(locID);
                    existingLocationList.Add(location);
                }
            }
        }


        public override void Save()
        {
            try
            {
                if ((Toast.Show("Cashier Group" + cmbGroup.Text.Trim(), Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                {
                    return;
                }

                UpdateList();
                GetSelectedLocations();

                CashierGroupService cashierGroupService = new CashierGroupService();
                cashierGroupService.SaveCashierGroup(existingCashierGroupList, Common.ConvertStringToInt(cmbGroup.SelectedValue.ToString()), chkUpdateAllCashiers.Checked, chkUpdateFunction.Checked, orderNo, value, isAccess, existingLocationList);

                Toast.Show("Cashier Group" + cmbGroup.Text.Trim(), Toast.messageType.Information, Toast.messageAction.Saved);
                ClearForm();
                ClearObjects();
                InitializeForm();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        public void ClearObjects()
        {

        }

        public override void View()
        {
            
        }

       
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelectAll.Checked)
                {
                    for (int i = 0; i < dgvCashier.RowCount; i++)
                    {
                        dgvCashier.Rows[i].Cells["Access"].Value = true;
                    }
                }
                else
                {
                    for (int i = 0; i < dgvCashier.RowCount; i++)
                    {
                        dgvCashier.Rows[i].Cells["Access"].Value = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvCashier_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(dgvCashier.Rows[e.RowIndex].Cells["IsValue"].Value) == true)
                {
                    //dgvCashier.Rows[e.RowIndex].Cells["Value"].Style.ForeColor = Color.Red;
                    dgvCashier.Rows[e.RowIndex].Cells["Value"].Style.BackColor = Color.SkyBlue;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        //public void paintRowColor()
        //{
        //    foreach (DataGridViewRow row in dgvCashier.Rows)
        //    {
        //        if (Convert.ToBoolean(dgvCashier["IsValue", row.Index + 1].Value) == true)
        //        {
        //            row.DefaultCellStyle.BackColor = Color.PowderBlue;
        //            dgvCashier.Refresh();
        //        }
        //    }
        //}


        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbGroup.SelectedIndex != -1) { chkUpdateAllCashiers.Enabled = true; }
                CashierGroupService cashierGroupService = new CashierGroupService();
                if (cmbGroup.SelectedValue == null) { return; }
                else
                {
                    existingCashierGroupList = cashierGroupService.GetGroupFunctionsByDesignationIDForCashierGroup(Common.ConvertStringToInt(cmbGroup.SelectedValue.ToString()));
                    dgvCashier.DataSource = existingCashierGroupList;
                    dgvCashier.Refresh();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void RowPaint()
        {
            var tempList = existingCashierGroupList;
            foreach (var item in tempList)
            {
                if (item.IsValue == true)
                {
                    dgvCashier.Rows[((int)item.Order) - 1].Cells["Value"].Style.BackColor = Color.SkyBlue;
                }
            }
        }

        private void dgvCashier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvCashier.CurrentCell.ColumnIndex == 3)
                {
                    if (Convert.ToBoolean(dgvCashier["Access", dgvCashier.CurrentCell.RowIndex].Value) == true)
                    {
                        dgvCashier["Value", dgvCashier.CurrentCell.RowIndex].Value = Convert.ToDecimal(0.00);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkUpdateAllCashiers_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkUpdateAllCashiers.Checked)
                {
                    if ((Toast.Show("Are you sure you want to update all\ncashiers in cashier group " + cmbGroup.Text.Trim(), Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.Yes)))
                    { chkUpdateAllCashiers.Checked = true; }
                    else
                    { chkUpdateAllCashiers.Checked = false; }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvCashier_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                orderNo = 0;
                value = 0;
                isAccess = false;
                if (dgvCashier.CurrentCell != null && dgvCashier.CurrentCell.RowIndex >= 0)
                {
                    txtFunction.Text = dgvCashier["FunctionDescription", dgvCashier.CurrentCell.RowIndex].Value.ToString();
                    orderNo = Common.ConvertStringToLong(dgvCashier["RowNo", dgvCashier.CurrentCell.RowIndex].Value.ToString());

                    if (orderNo != 0)
                    {
                        grpLocation.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadAllLocations()
        {
            LocationService locationService = new LocationService();
            List<Location> locations = new List<Location>();

            locations = locationService.GetAllInventoryLocations();
            dgvLocationInfo.DataSource = locations;
            dgvLocationInfo.Refresh();

        }

        private void dgvLocationInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!chkUpdateFunction.Checked)
                {
                    if (Convert.ToBoolean(dgvLocationInfo["Selection", dgvLocationInfo.CurrentCell.RowIndex].Value) == false)
                    {
                        chkUpdateFunction.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvCashier_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dgvCashier.CurrentCell.ColumnIndex == 4)
                {
                    if (Convert.ToBoolean((dgvCashier["IsValue", dgvCashier.CurrentCell.RowIndex].Value)) == false)
                    {
                        dgvCashier["Value", dgvCashier.CurrentCell.RowIndex].ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvCashier_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvCashier.CurrentCell.ColumnIndex == 4)
                {
                    if (Convert.ToBoolean((dgvCashier["IsValue", dgvCashier.CurrentCell.RowIndex].Value)) == false)
                    {
                        dgvCashier["Value", dgvCashier.CurrentCell.RowIndex].ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        
    }
}
