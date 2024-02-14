using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;
using Domain;
using Service;
using System.Reflection;

namespace UI.Windows
{
    public partial class FrmManualPointsAdd : FrmBaseMasterForm
    {
        int documentID = 0;

        public FrmManualPointsAdd()
        {
            InitializeComponent();
        }

        public override void FormLoad()
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            documentID = autoGenerateInfo.DocumentID;

            LocationService locationService = new LocationService();
            Common.LoadLocations(cmbLocation, locationService.GetAllInventoryLocations());

            LoadEmployees();

            LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
            Common.LoadLoyltyTypes(cmbCustomerType, lookUpReferenceService.LoadLoyltyTypes(((int)LookUpReference.LoyaltyType).ToString()));

            base.FormLoad();   
        }

        public override void InitializeForm()
        {
            cmbCustomerType.SelectedIndex = -1;
            Common.EnableTextBox(true, txtCustomerCode, txtCustomerName, txtCardNo, txtEmployeeCode, txtEmployeeName);
            Common.ClearTextBox(txtCustomerCode, txtCustomerName, txtCardNo, txtEmployeeCode, txtEmployeeName);

            Common.EnableButton(false, btnDelete, btnSave);

            this.ActiveControl = txtCustomerCode;
            txtCustomerCode.Focus(); 

            base.InitializeForm();
        }

        private void chkAutoCompleationCustomer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoCompleationCustomer.Checked)
                {
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                    Common.SetAutoComplete(txtCustomerCode, loyaltyCustomerService.GetAllLoyaltyCustomerCodes(), chkAutoCompleationCustomer.Checked);
                    Common.SetAutoComplete(txtCustomerName, loyaltyCustomerService.GetAllLoyaltyCustomerNames(), chkAutoCompleationCustomer.Checked);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationCardNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoCompleationCardNo.Checked)
                {
                    LoyaltyCardGeneratrionService loyaltyCardGeneratrionService = new LoyaltyCardGeneratrionService();
                    Common.SetAutoComplete(txtCardNo, loyaltyCardGeneratrionService.GetIssuedCardNos(), chkAutoCompleationCardNo.Checked);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void LoadEmployees()
        {
            EmployeeService employeeService = new EmployeeService();

            Common.SetAutoComplete(txtEmployeeCode, employeeService.GetAllActiveEmployeeCodesForCashier(), chkAutoCompleationEmployee.Checked);
            Common.SetAutoComplete(txtEmployeeName, employeeService.GetAllActiveEmployeeNamesForCashier(), chkAutoCompleationEmployee.Checked);
        }

        private void chkAutoCompleationEmployee_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoCompleationEmployee.Checked)
                {
                    LoadEmployees();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCustomerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
                {
                    CardMasterService cardMasterService = new CardMasterService();
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                    LoyaltyCustomer loyaltyCustomer = new LoyaltyCustomer();
                    loyaltyCustomer = loyaltyCustomerService.GetLoyaltyCustomerByCode(txtCustomerCode.Text.Trim());

                    if (loyaltyCustomer != null)
                    {
                        txtCustomerName.Text = loyaltyCustomer.CustomerName.Trim();
                        cmbCustomerType.SelectedValue = loyaltyCustomer.LoyaltyType;
                        txtCardNo.Text = loyaltyCustomer.CardNo.Trim();
                        lblLoyltyTyp.Text = cmbCustomerType.Text;
                        lblCardType.Text = cardMasterService.GetCardMasterById(loyaltyCustomer.CardMasterID).CardName;

                        Common.EnableButton(true, btnDelete, btnSave);
                        Common.EnableTextBox(false, txtCustomerCode, txtCustomerName, txtCardNo);
                    }
                    else
                    {
                        Toast.Show("Invalid Customer Code", Toast.messageType.Information, Toast.messageAction.General);
                        txtCustomerCode.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter)) { txtEmployeeCode.Focus(); }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter)) { txtEmployeeCode.Focus(); }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCardNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCardNo.Text.Trim()))
                {
                    LoyaltyCustomerService loyaltyCustomerervice = new LoyaltyCustomerService();
                    CardMasterService cardMasterService = new CardMasterService();
                    LoyaltyCustomer loyaltyCustomer = new LoyaltyCustomer();
                    loyaltyCustomer = loyaltyCustomerervice.GetLoyaltyCustomerByCardNo(txtCardNo.Text.Trim());

                    if (loyaltyCustomer != null)
                    {
                        txtCustomerCode.Text = loyaltyCustomer.CustomerCode.Trim();
                        txtCustomerName.Text = loyaltyCustomer.CustomerName.Trim();
                        cmbCustomerType.SelectedValue = loyaltyCustomer.LoyaltyType;
                        txtCardNo.Text = loyaltyCustomer.CardNo.Trim();
                        lblLoyltyTyp.Text = cmbCustomerType.Text;
                        lblCardType.Text = cardMasterService.GetCardMasterById(loyaltyCustomer.CardMasterID).CardName;

                        Common.EnableButton(true, btnDelete, btnSave);
                        Common.EnableTextBox(false, txtCustomerCode, txtCustomerName, txtCardNo);
                    }
                    else
                    {
                        Toast.Show("Invalid card no", Toast.messageType.Information, Toast.messageAction.General);
                        txtCardNo.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmployeeCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEmployeeCode.Text.Trim()))
                {
                    EmployeeService employeeService = new EmployeeService();
                    Employee employee = new Employee();

                    employee = employeeService.GetActiveEmployeesByCode(txtEmployeeCode.Text.Trim());

                    if (employee != null)
                    {
                        txtEmployeeCode.Text = employee.EmployeeCode;
                        txtEmployeeName.Text = employee.EmployeeName;

                        Common.EnableTextBox(false, txtEmployeeCode, txtEmployeeName);
                    }
                    else
                    {
                        Toast.Show("Employee  - " + txtEmployeeCode.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtEmployeeCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmployeeCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtEmployeeName.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmployeeName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    cmbLocation.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmployeeName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEmployeeName.Text.Trim()))
                {
                    EmployeeService employeeService = new EmployeeService();
                    Employee employee = new Employee();

                    employee = employeeService.GetActiveEmployeesByName(txtEmployeeName.Text.Trim());

                    if (employee != null)
                    {
                        txtEmployeeCode.Text = employee.EmployeeCode;
                        txtEmployeeName.Text = employee.EmployeeName;

                        Common.EnableTextBox(false, txtEmployeeCode, txtEmployeeName);
                    }
                    else
                    {
                        Toast.Show("Employee  - " + txtEmployeeCode.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtEmployeeName.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        public override void Save()
        {
            try
            {
                if (Toast.Show("points for customer " + txtCustomerCode.Text.Trim(), Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.Yes))
                {
                    LoyaltyCustomer loyaltyCustomer = new LoyaltyCustomer();
                    string recept;
                    decimal amount;
                    decimal points;
                    int locationID;
                    int unitNo;
                    Employee employee = new Employee();
                    DateTime billDate;
                    DateTime addedDate;
                    ManualPointsAddService manualPointsAddService = new ManualPointsAddService();

                    if (ValidateControls() == false) { return; }
                    if (ValidateCombos() == false) { return; }

                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                    loyaltyCustomer = loyaltyCustomerService.GetLoyaltyCustomerByCode(txtCustomerCode.Text.Trim());
                    if (loyaltyCustomer == null) { Toast.Show("Invalid Customer", Toast.messageType.Information, Toast.messageAction.General); return; }

                    recept = TxtReceptNo.Text.Trim();
                    amount = Common.ConvertStringToDecimalCurrency(txtAmount.Text.Trim());
                    points = Common.ConvertStringToDecimalCurrency(txtPoints.Text.Trim());
                    locationID = Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString());
                    unitNo = Common.ConvertStringToInt(txtUnitNo.Text.Trim());

                    EmployeeService employeeService = new EmployeeService();
                    employee = employeeService.GetActiveEmployeesByName(txtEmployeeName.Text.Trim());
                    if (employee == null) { Toast.Show("Invalid Employee", Toast.messageType.Information, Toast.messageAction.General); return; }

                    billDate = dtpBillDate.Value;
                    addedDate = dtpAddedDate.Value;

                    if (!manualPointsAddService.AddManualPoints(loyaltyCustomer, recept, amount, points, locationID, unitNo, employee, billDate, addedDate))
                    {
                        Toast.Show("Error found", Toast.messageType.Information, Toast.messageAction.General);
                        return;
                    }
                    else
                    {
                        Toast.Show("Points added successfully", Toast.messageType.Information, Toast.messageAction.General);
                        ClearForm();
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void ClearForm()
        {
            dtpAddedDate.Value = Common.GetSystemDate();
            dtpBillDate.Value = Common.GetSystemDate();
            base.ClearForm();
        }

        private bool ValidateControls()
        {
            return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtCustomerCode, txtCardNo, TxtReceptNo, txtAmount, txtUnitNo, txtPoints);
        }

        private bool ValidateCombos() 
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbLocation, cmbCustomerType);
        }

        private void cmbCustomerType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblLoyltyTyp.Text = cmbCustomerType.Text;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter)) { txtEmployeeName.Focus(); }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter)) { txtUnitNo.Focus(); }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtUnitNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter)) { TxtReceptNo.Focus(); }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void TxtReceptNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter)) { dtpBillDate.Focus(); }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpBillDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter)) { dtpAddedDate.Focus(); }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpAddedDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter)) { txtAmount.Focus(); }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter)) { txtPoints.Focus(); }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void View()
        {
            Report.GUI.FrmCrmManuallyAddedPoints frmCrmManuallyAddedPoints = new Report.GUI.FrmCrmManuallyAddedPoints("Manually Added Points Details");
            frmCrmManuallyAddedPoints.ShowDialog();
        }


    }
}
