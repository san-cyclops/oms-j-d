using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Domain;
using Report.Com;
using Utility;
using Service;

namespace UI.Windows
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public partial class FrmVehicle : UI.Windows.FrmBaseMasterForm
    {
        private Vehicle existingVehicle;
        AutoCompleteStringCollection autoCompleteRegistrationNo;
        AutoCompleteStringCollection autocompleteVehicleName; 

        public FrmVehicle()
        {
            InitializeComponent();
        }

        private void FrmVehicle_Load(object sender, EventArgs e)
        {
           
        }

        #region Button Click Events....

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Override Methods....

        public override void FormLoad()
        {
            chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
            base.FormLoad();
        }

        public override void InitializeForm()
        {
            VehicleService vehicleService = new VehicleService();
 
            ////auto complete vehicle registration number
            autoCompleteRegistrationNo = new AutoCompleteStringCollection();
            autoCompleteRegistrationNo.AddRange(vehicleService.GetAllRegistrationNumbers());
            Common.SetAutoComplete(txtRegistrationNo, autoCompleteRegistrationNo, chkAutoCompleationRegistrationNo.Checked);
            ////

            cmbEngineCapacity.SelectedIndex = -1;
            cmbFuelType.SelectedIndex = -1;
            cmbVehicleType.SelectedIndex = -1;
            cmbSeatingCapacity.SelectedIndex = -1;

            Common.EnableButton(false, btnSave, btnDelete);
            Common.EnableTextBox(true, txtRegistrationNo);
            Common.ClearTextBox(txtRegistrationNo);

            ActiveControl = txtRegistrationNo;
            txtRegistrationNo.Focus();
        }

        public override void Save()
        {
            try
            {
                if (ValidateTextbox().Equals(false))
                {
                    return;
                }
                if (ValidateCombobox().Equals(false))
                {
                    return;
                }

                VehicleService vehicleService = new VehicleService();
                existingVehicle = vehicleService.GetVehicleByRegistrationNo(txtRegistrationNo.Text.Trim());

                if (existingVehicle == null || existingVehicle.VehicleID == 0)
                {
                    existingVehicle = new Vehicle();
                }

                existingVehicle.RegistrationNo = txtRegistrationNo.Text.Trim();
                existingVehicle.VehicleName = txtVehicleName.Text.Trim();
                existingVehicle.EngineNo = txtEngineNo.Text.Trim();
                existingVehicle.ChassesNo = txtChassesNo.Text.Trim();
                existingVehicle.VehicleType = cmbVehicleType.Text.Trim();
                existingVehicle.FuelType = cmbFuelType.Text.Trim();
                existingVehicle.Make = txtMake.Text.Trim();
                existingVehicle.Model = txtModel.Text.Trim();
                existingVehicle.EngineCapacity = cmbEngineCapacity.Text.Trim();
                existingVehicle.SeatingCapacity = cmbSeatingCapacity.Text.Trim();

                if (string.IsNullOrEmpty(txtWeight.Text.Trim()) || txtWeight.Text.Equals(0)) { existingVehicle.Weight = 0; }
                else { existingVehicle.Weight = Convert.ToInt32(txtWeight.Text.Trim()); }

                existingVehicle.Remark = txtRemark.Text.Trim();

                if (existingVehicle.VehicleID.Equals(0))
                {
                    if ((Toast.Show("Vehicle - " + existingVehicle.RegistrationNo + " - " + existingVehicle.VehicleName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }

                    vehicleService.AddVehicle(existingVehicle);
                    
                    if ((Toast.Show("Vehicle - " + existingVehicle.RegistrationNo + " - " + existingVehicle.VehicleName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
                    {
                        txtRegistrationNo.Focus();
                        if (chkAutoClear.Checked)
                        {
                            ClearForm();
                        }
                        else
                        {
                            InitializeForm();
                        }
                    }
                }
                else
                {
                    if ((Toast.Show("Vehicle - " + existingVehicle.RegistrationNo + " - " + existingVehicle.VehicleName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    vehicleService.UpdateVehicle(existingVehicle);

                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }

                    Toast.Show("Vehicle - " + existingVehicle.RegistrationNo + " - " + existingVehicle.VehicleName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override void Delete()
        {
            try
            {
                if (Toast.Show("Vehicle - " + existingVehicle.RegistrationNo + " - " + existingVehicle.VehicleName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                {
                    return;
                }

                VehicleService vehicleService = new VehicleService();
                existingVehicle= vehicleService.GetVehicleByRegistrationNo(txtRegistrationNo.Text.Trim());

                if (existingVehicle != null || existingVehicle.VehicleID != 0)
                {
                    vehicleService.DeleteVehicle(existingVehicle);
                    
                    Toast.Show("Vehicle - " + existingVehicle.RegistrationNo + " - " + existingVehicle.VehicleName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    ClearForm();
                    txtRegistrationNo.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override void View()
        {
            try
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmVehicle");
                ComReportGenerator comReportGenerator = new ComReportGenerator();
                comReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region KeyDown and Leave Events....

        private void txtRegistrationNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    VehicleService vehicleService = new VehicleService();
                    DataView dvAllReferenceData = new DataView(vehicleService.GetAllVehiclesDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtRegistrationNo);
                        txtRegistrationNo_Leave(this, e);
                    }
                }
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtVehicleName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtRegistrationNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtRegistrationNo.Text.Trim() != string.Empty)
                {
                    VehicleService vehicleService = new VehicleService();
                    existingVehicle = vehicleService.GetVehicleByRegistrationNo(txtRegistrationNo.Text.Trim());

                    if (existingVehicle != null)
                    {
                        txtVehicleName.Text = existingVehicle.VehicleName.Trim();
                        txtVehicleName.Text = existingVehicle.VehicleName.Trim();
                        txtEngineNo.Text = existingVehicle.EngineNo.Trim();
                        txtChassesNo.Text = existingVehicle.ChassesNo;
                        cmbVehicleType.Text = existingVehicle.VehicleType;
                        cmbFuelType.Text = existingVehicle.FuelType;
                        txtMake.Text = existingVehicle.Make;
                        txtModel.Text = existingVehicle.Model;
                        cmbEngineCapacity.Text = existingVehicle.EngineCapacity;
                        cmbSeatingCapacity.Text = existingVehicle.SeatingCapacity;
                        txtWeight.Text = Convert.ToString(existingVehicle.Weight);
                        txtRemark.Text = existingVehicle.Remark;

                        Common.EnableButton(true, btnSave, btnDelete);
                        txtVehicleName.Focus();
                    }
                    else
                    {
                        if (Toast.Show("Vehicle - " + txtRegistrationNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                        {
                            CreateNewVehicle();
                        }
                    }
                    txtVehicleName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtVehicleName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtEngineNo.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtVehicleName_Leave(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtEngineNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtChassesNo.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtEngineNo_Leave(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtChassesNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                cmbVehicleType.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtChassesNo_Leave(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbVehicleType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                cmbFuelType.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbVehicleType_Leave(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbFuelType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtMake.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbFuelType_Leave(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtMake_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtModel.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtMake_Leave(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                cmbEngineCapacity.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtModel_Leave(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbEngineCapacity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                cmbSeatingCapacity.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbEngineCapacity_Leave(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbSeatingCapacity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtWeight.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbSeatingCapacity_Leave(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtWeight_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtRemark.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtWeight_Leave(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Methods....

        private bool ValidateTextbox()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtRegistrationNo, txtVehicleName);
        }

        private bool ValidateCombobox()
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbFuelType);
        }

        private void chkAutoCompleationRegistrationNo_CheckedChanged(object sender, EventArgs e)
        {
            Common.SetAutoComplete(txtRegistrationNo, autoCompleteRegistrationNo, chkAutoCompleationRegistrationNo.Checked);
        }

        private void CreateNewVehicle()
        {
            try
            {
                Common.EnableButton(true, btnSave, btnDelete);
                Common.EnableTextBox(false, txtRegistrationNo);
                if (chkAutoClear.Checked)
                {
                    Common.ClearTextBox(txtVehicleName, txtEngineNo, txtChassesNo, txtMake, txtModel, txtWeight, txtRemark);
                    Common.ClearComboBox(cmbVehicleType, cmbFuelType, cmbEngineCapacity, cmbSeatingCapacity);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Load Reference Search form on demand
        /// </summary>
        private void LoadReferenceSearchForm(DataView dvAllReferenceData, string parentOfSearch, string searchFormCaption, string searchText, Control focusControl)
        {
            FrmReferenceSearch referenceSearch = new FrmReferenceSearch();
            referenceSearch.DvResults = dvAllReferenceData;
            referenceSearch.ParentOfSearch = parentOfSearch.Trim();
            referenceSearch.FormCaption = searchFormCaption.Trim();
            referenceSearch.SearchText = searchText.Trim();
            referenceSearch.FocusControl = focusControl;

            if (referenceSearch.IsDisposed)
            { referenceSearch = new FrmReferenceSearch(); }

            foreach (Form frm in Application.OpenForms)
            {
                if (frm is FrmReferenceSearch)
                {
                    FrmReferenceSearch masterSearch2 = (FrmReferenceSearch)frm;
                    if (string.Equals(masterSearch2.ParentOfSearch.Trim(), this.Name.Trim()))
                    { return; }
                }
            }

            referenceSearch.ShowDialog();
        }

        #endregion 

    }
}
