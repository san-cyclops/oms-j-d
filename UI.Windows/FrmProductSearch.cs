using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Domain;
using Report;
using Report.Inventory;
using Report.Inventory.Reference.Reports;
using Utility;
using Service;
using System.Linq;
using System.IO;
namespace UI.Windows
{
    public partial class FrmProductSearch : Form
    {
        List<InvProductExtendedPropertyValue> invProductExtendedPropertyValueGrid = new List<InvProductExtendedPropertyValue>();
        List<InvProductExtendedProperty> invProductExtendedProperty = new List<InvProductExtendedProperty>();
        bool isDependCategory = false, isDependSubCategory = false, isDependSubCategory2 = false;

        public FrmProductSearch()
        {
            InitializeComponent();
        }

        private void FrmProductSearch_Load(object sender, EventArgs e)
        {
            try
            {
                //dgvExtendedProperties.AllowUserToAddRows = false;
                dgvExtendedProperties.AutoGenerateColumns = false;

                this.CausesValidation = false;
                lblDepartment.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText + "*";
                isDependCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend;
                lblCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText + "*";
                isDependSubCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend;
                lblSubCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText + "*";
                isDependSubCategory2 = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").IsDepend;
                lblSubCategory2.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText + "*";

                InvDepartmentService invDepartmentService = new Service.InvDepartmentService();
                Common.SetAutoComplete(txtDepartmentCode, invDepartmentService.GetInvDepartmentCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend), chkAutoCompleationDepartment.Checked);
                Common.SetAutoComplete(txtDepartmentDescription, invDepartmentService.GetInvDepartmentNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend), chkAutoCompleationDepartment.Checked);

                InvCategoryService invCategoryService = new Service.InvCategoryService();
                Common.SetAutoComplete(txtCategoryCode, invCategoryService.GetInvCategoryCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend), chkAutoCompleationCategory.Checked);
                Common.SetAutoComplete(txtCategoryDescription, invCategoryService.GetInvCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend), chkAutoCompleationCategory.Checked);

                InvSubCategoryService invSubCategoryService = new Service.InvSubCategoryService();
                Common.SetAutoComplete(txtSubCategoryCode, invSubCategoryService.GetInvSubCategoryCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").IsDepend), chkAutoCompleationSubCategory.Checked);
                Common.SetAutoComplete(txtSubCategoryDescription, invSubCategoryService.GetInvSubCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").IsDepend), chkAutoCompleationSubCategory.Checked);

                InvSubCategory2Service invSubCategory2Service = new Service.InvSubCategory2Service();
                Common.SetAutoComplete(txtSubCategory2Code, invSubCategory2Service.GetInvSubCategory2Codes(), chkAutoCompleationSubCategory2.Checked);
                Common.SetAutoComplete(txtSubCategory2Description, invSubCategory2Service.GetInvSubCategory2Names(), chkAutoCompleationSubCategory2.Checked);

                SupplierService supplierService = new Service.SupplierService();
                Common.SetAutoComplete(txtMainSupplierCode, supplierService.GetSupplierCodes(), chkAutoCompleationMainSupplier.Checked);
                Common.SetAutoComplete(txtMainSupplierDescription, supplierService.GetSupplierNames(), chkAutoCompleationMainSupplier.Checked);

                InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                Common.SetAutoComplete(txtPropertyName, invProductExtendedPropertyService.GetInvProductExtendedPropertyNames(), true);

                //InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                //invProductExtendedProperty = invProductExtendedPropertyService.GetAllActiveInvProductExtendedProperties();
                //dgvExtendedProperties.DataSource = invProductExtendedProperty;
                //dgvExtendedProperties.Refresh();

                Common.EnableTextBox(true, txtDepartmentCode, txtCategoryCode, txtSubCategoryCode, txtSubCategory2Code);
                Common.EnableTextBox(true, txtDepartmentDescription, txtCategoryDescription, txtSubCategoryDescription, txtSubCategory2Description);
                tabProduct.SelectedTab = tbpGneral;
                ActiveControl = txtDepartmentCode;
                txtDepartmentCode.Focus();

                

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void chkAutoCompleationDepartment_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvDepartmentService invDepartmentService = new Service.InvDepartmentService();
                Common.SetAutoComplete(txtDepartmentCode, invDepartmentService.GetInvDepartmentCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend), chkAutoCompleationDepartment.Checked);
                Common.SetAutoComplete(txtDepartmentDescription, invDepartmentService.GetInvDepartmentNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend), chkAutoCompleationDepartment.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationCategory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvCategoryService invCategoryService = new Service.InvCategoryService();
                Common.SetAutoComplete(txtCategoryCode, invCategoryService.GetInvCategoryCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend), chkAutoCompleationCategory.Checked);
                Common.SetAutoComplete(txtCategoryDescription, invCategoryService.GetInvCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend), chkAutoCompleationCategory.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationMainSupplier_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SupplierService supplierService = new Service.SupplierService();
                Common.SetAutoComplete(txtSubCategoryCode, supplierService.GetSupplierCodes(), chkAutoCompleationMainSupplier.Checked);
                Common.SetAutoComplete(txtSubCategoryDescription, supplierService.GetSupplierNames(), chkAutoCompleationMainSupplier.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationSubCategory2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvSubCategory2Service invSubCategory2Service = new Service.InvSubCategory2Service();
                Common.SetAutoComplete(txtSubCategory2Code, invSubCategory2Service.GetInvSubCategory2Codes(), chkAutoCompleationSubCategory2.Checked);
                Common.SetAutoComplete(txtSubCategory2Description, invSubCategory2Service.GetInvSubCategory2Names(), chkAutoCompleationSubCategory2.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMainSupplierCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtMainSupplierCode.Text.Trim() != string.Empty)
                {
                    SupplierService supplierService = new SupplierService();
                    Supplier existingSupplier = new Supplier();
                    existingSupplier = supplierService.GetSupplierByCode(txtMainSupplierCode.Text.Trim());

                    if (existingSupplier != null)
                    {
                        txtMainSupplierDescription.Text = existingSupplier.SupplierName;
                        txtCostPrice.Focus();
                    }
                    else
                    {
                        Toast.Show("Supplier " + txtMainSupplierCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtMainSupplierCode);
                        txtMainSupplierCode.Focus();

                    }
                }
                }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void txtMainSupplierDescription_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtMainSupplierDescription.Text.Trim() != string.Empty)
                {
                    SupplierService supplierService = new SupplierService();
                    Supplier existingSupplier = new Supplier();
                    existingSupplier = supplierService.GetSupplierByName(txtMainSupplierDescription.Text.Trim());

                    if (existingSupplier != null)
                    {
                        txtMainSupplierCode.Text = existingSupplier.SupplierCode;
                        txtCostPrice.Focus();
                    }
                    else
                    {
                        Toast.Show("Supplier " + txtMainSupplierDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtMainSupplierDescription);
                        txtMainSupplierDescription.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void dgvExtendedProperties_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                ComboBox combo = e.Control as ComboBox;
                if (combo != null)
                {
                   
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        /// <summary>
        /// Load Reference Search form on demand
        /// </summary>
        private void LoadReferenceSearchForm(DataView dvAllReferenceData, string parentOfSearch, string searchFormCaption, string searchText, Control focusControl)
        {
            try
            {
                FrmReferenceSearch referenceSearch = new FrmReferenceSearch();
                referenceSearch.ParentOfSearch = parentOfSearch.Trim();
                referenceSearch.FormCaption = searchFormCaption.Trim();
                referenceSearch.SearchText = searchText.Trim();
                referenceSearch.DvResults = dvAllReferenceData;
                referenceSearch.FocusControl = focusControl;

                if (referenceSearch.IsDisposed)
                {
                    referenceSearch = new FrmReferenceSearch();
                }

                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is FrmReferenceSearch)
                    {
                        FrmReferenceSearch masterSearch2 = (FrmReferenceSearch)frm;
                        if (string.Equals(masterSearch2.ParentOfSearch.Trim(), this.Name.Trim()))
                        {
                            return;
                        }
                    }
                }

                referenceSearch.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMainSupplierCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtMainSupplierCode_Validated(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMainSupplierDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtMainSupplierDescription_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void txtCostPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtCostPrice_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCostPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                txtSellingPrice.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }



        private void txtSellingPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSellingPrice_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSellingPrice_Leave(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void txtUnitCostPrice_Leave(object sender, EventArgs e)
        {

        }


        private void txtDepartmentCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtDepartmentCode_Leave(this, e);
                    txtCategoryCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtDepartmentCode.Text.Trim() != string.Empty)
                {
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    InvDepartment existingInvDepartment = new InvDepartment();
                    existingInvDepartment = invDepartmentService.GetInvDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDependCategory);

                    if (existingInvDepartment != null)
                    {
                        txtDepartmentCode.Text = existingInvDepartment.DepartmentCode;
                        txtDepartmentDescription.Text = existingInvDepartment.DepartmentName;
                    }
                    else
                    {
                        Toast.Show(lblDepartment.Text + " " + txtDepartmentCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtDepartmentCode);
                        txtDepartmentCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtDepartmentDescription_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentDescription_Leave(object sender, EventArgs e)
        {
            if (txtDepartmentDescription.Text.Trim() != string.Empty)
            {
                InvDepartmentService invDepartmentService = new InvDepartmentService();
                InvDepartment existingInvDepartment = new InvDepartment();
                existingInvDepartment = invDepartmentService.GetInvDepartmentsByName(txtDepartmentDescription.Text.Trim(), isDependCategory);

                if (existingInvDepartment != null)
                {
                    txtDepartmentCode.Text = existingInvDepartment.DepartmentCode;
                    txtCategoryCode.Focus();
                }
                else
                {
                    Toast.Show(lblDepartment.Text + " " + txtDepartmentDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    Common.ClearTextBox(txtDepartmentDescription);
                    txtDepartmentDescription.Focus();
                }
            }
        }

        private void txtCategoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtCategoryCode_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryCode.Text.Trim() != string.Empty)
                {
                    InvCategoryService invCategoryService = new InvCategoryService();
                    InvCategory existingInvCategory = new InvCategory();

                    existingInvCategory = invCategoryService.GetInvCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory);

                    if (existingInvCategory != null)
                    {
                        if (!isDependCategory)
                        {
                            txtCategoryDescription.Text = existingInvCategory.CategoryName;
                            txtSubCategoryCode.Focus();
                        }
                        else
                        {
                            InvDepartmentService invDepartmentService = new Service.InvDepartmentService();

                            if (invDepartmentService.GetInvDepartmentsByID(existingInvCategory.InvDepartmentID, isDependCategory).DepartmentCode.Equals(txtDepartmentCode.Text.Trim()))
                            {
                                txtCategoryDescription.Text = existingInvCategory.CategoryName;
                                txtSubCategoryCode.Focus();
                            }
                            else
                            {
                                Toast.Show(lblCategory.Text + " " + txtCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblDepartment.Text + " - " + txtDepartmentCode.Text.Trim() + " - " + txtDepartmentDescription.Text.Trim());
                                Common.ClearTextBox(txtCategoryCode);
                                txtCategoryCode.Focus();
                            }
                        }
                    }
                    else
                    {
                        Toast.Show(lblCategory.Text + " " + txtCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtCategoryCode);
                        txtCategoryCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtCategoryDescription_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryDescription_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryDescription.Text.Trim() != string.Empty)
                {
                    InvCategoryService invCategoryService = new InvCategoryService();
                    InvCategory existingInvCategory = new InvCategory();

                    existingInvCategory = invCategoryService.GetInvCategoryByName(txtCategoryDescription.Text.Trim(), isDependSubCategory);


                    if (existingInvCategory != null)
                    {
                        if (!isDependCategory)
                        {
                            txtCategoryCode.Text = existingInvCategory.CategoryCode;
                            txtSubCategoryCode.Focus();
                        }
                        else
                        {
                            InvDepartmentService invDepartmentService = new Service.InvDepartmentService();

                            if (invDepartmentService.GetInvDepartmentsByID(existingInvCategory.InvDepartmentID, isDependCategory).DepartmentCode.Equals(txtDepartmentCode.Text.Trim()))
                            {
                                txtCategoryCode.Text = existingInvCategory.CategoryCode;
                                txtSubCategoryCode.Focus();
                            }
                            else
                            {
                                Toast.Show(lblCategory.Text + " " + txtCategoryDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblDepartment.Text + " - " + txtDepartmentCode.Text.Trim() + " - " + txtDepartmentDescription.Text.Trim());
                                Common.ClearTextBox(txtCategoryDescription);
                                txtCategoryDescription.Focus();
                            }
                        }
                    }
                    else
                    {
                        Toast.Show(lblCategory.Text + " " + txtCategoryDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtCategoryDescription);
                        txtCategoryDescription.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSubCategoryCode_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSubCategoryCode.Text.Trim() != string.Empty)
                {
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    InvSubCategory existingInvSubCategory = new InvSubCategory();

                    existingInvSubCategory = invSubCategoryService.GetInvSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2);

                    if (existingInvSubCategory != null)
                    {
                        if (!isDependCategory)
                        {
                            txtSubCategoryDescription.Text = existingInvSubCategory.SubCategoryName;
                            txtSubCategory2Code.Focus();
                        }
                        else
                        {
                            InvCategoryService invCategoryService = new Service.InvCategoryService();

                            if (invCategoryService.GetInvCategoryByID(existingInvSubCategory.InvCategoryID, isDependSubCategory2).CategoryCode.Equals(txtCategoryCode.Text.Trim()))
                            {
                                txtSubCategoryDescription.Text = existingInvSubCategory.SubCategoryName;
                                txtSubCategory2Code.Focus();
                            }
                            else
                            {
                                Toast.Show(lblSubCategory.Text + " " + txtSubCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblCategory.Text + " - " + txtCategoryCode.Text.Trim() + " - " + txtCategoryDescription.Text.Trim());
                                Common.ClearTextBox(txtSubCategoryCode);
                                txtSubCategoryCode.Focus();
                            }
                        }
                    }
                    else
                    {
                        Toast.Show(lblSubCategory.Text + " " + txtSubCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtSubCategoryCode);
                        txtSubCategoryCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void txtSubCategoryDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSubCategoryDescription_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryDescription_Leave(object sender, EventArgs e)
        {
            if (txtSubCategoryDescription.Text.Trim() != string.Empty)
            {
                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                InvSubCategory existingInvSubCategory = new InvSubCategory();

                existingInvSubCategory = invSubCategoryService.GetInvSubCategoryByName(txtSubCategoryDescription.Text.Trim(), isDependSubCategory2);

                if (existingInvSubCategory != null)
                {
                    if (!isDependCategory)
                    {
                        txtSubCategoryCode.Text = existingInvSubCategory.SubCategoryCode;
                        txtSubCategory2Code.Focus();
                    }
                    else
                    {
                        InvCategoryService invCategoryService = new Service.InvCategoryService();

                        if (invCategoryService.GetInvCategoryByID(existingInvSubCategory.InvCategoryID, isDependSubCategory2).CategoryCode.Equals(txtCategoryCode.Text.Trim()))
                        {
                            txtSubCategoryCode.Text = existingInvSubCategory.SubCategoryCode;
                            txtSubCategory2Code.Focus();
                        }
                        else
                        {
                            Toast.Show(lblSubCategory.Text + " " + txtSubCategoryDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblCategory.Text + " - " + txtCategoryCode.Text.Trim() + " - " + txtCategoryDescription.Text.Trim());
                            Common.ClearTextBox(txtSubCategoryDescription);
                            txtSubCategoryDescription.Focus();
                        }
                    }
                }
                else
                {
                    Toast.Show(lblSubCategory.Text + " " + txtSubCategoryDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    Common.ClearTextBox(txtSubCategoryDescription);
                    txtSubCategoryDescription.Focus();
                }
            }
        }

        private void txtSubCategory2Code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSubCategory2Code_Leave(this, e);
                    txtMainSupplierCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategory2Code_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSubCategory2Code.Text.Trim() != string.Empty)
                {
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    InvSubCategory2 existingInvSubCategory2 = new InvSubCategory2();

                    existingInvSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByCode(txtSubCategory2Code.Text.Trim());

                    if (existingInvSubCategory2 != null)
                    {
                        if (!isDependSubCategory2)
                        {
                            txtSubCategory2Description.Text = existingInvSubCategory2.SubCategory2Name;
                        }
                        else
                        {
                            InvSubCategoryService InvSubCategoryService = new Service.InvSubCategoryService();

                            if (InvSubCategoryService.GetInvSubCategoryByID(existingInvSubCategory2.InvSubCategoryID, isDependSubCategory2).SubCategoryCode.Equals(txtSubCategoryCode.Text.Trim()))
                            {
                                txtSubCategory2Description.Text = existingInvSubCategory2.SubCategory2Name;
                                txtMainSupplierCode.Focus();
                            }
                            else
                            {
                                Toast.Show(lblSubCategory2.Text + " " + txtSubCategory2Code.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblSubCategory.Text + " - " + txtSubCategoryCode.Text.Trim() + " - " + txtSubCategoryDescription.Text.Trim());
                                Common.ClearTextBox(txtSubCategory2Code);
                                txtSubCategory2Code.Focus();
                            }
                        }
                    }
                    else
                    {
                        Toast.Show(lblSubCategory2.Text + " " + txtSubCategory2Code.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtSubCategory2Code);
                        txtSubCategory2Code.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategory2Description_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSubCategory2Description_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategory2Description_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSubCategory2Description.Text.Trim() != string.Empty)
                {
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    InvSubCategory2 existingInvSubCategory2 = new InvSubCategory2();

                    existingInvSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByName(txtSubCategory2Description.Text.Trim());


                    if (existingInvSubCategory2 != null)
                    {
                        if (!isDependSubCategory2)
                        {
                            txtSubCategory2Code.Text = existingInvSubCategory2.SubCategory2Code;
                            txtMainSupplierCode.Focus();
                        }
                        else
                        {
                            InvSubCategoryService InvSubCategoryService = new Service.InvSubCategoryService();

                            if (InvSubCategoryService.GetInvSubCategoryByID(existingInvSubCategory2.InvSubCategoryID, isDependSubCategory2).SubCategoryCode.Equals(txtSubCategoryCode.Text.Trim()))
                            {
                                txtSubCategory2Code.Text = existingInvSubCategory2.SubCategory2Code;
                                txtMainSupplierCode.Focus();
                            }
                            else
                            {
                                Toast.Show(lblSubCategory2.Text + " " + txtSubCategory2Description.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblSubCategory.Text + " - " + txtSubCategoryCode.Text.Trim() + " - " + txtSubCategoryDescription.Text.Trim());
                                Common.ClearTextBox(txtSubCategory2Description);
                                txtSubCategory2Description.Focus();
                            }
                        }

                    }
                    else
                    {

                        Toast.Show(lblSubCategory2.Text + " " + txtSubCategory2Description.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtSubCategory2Description);
                        txtSubCategory2Description.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (Toast.Show("Do you want to Close this form?", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.Yes))
                {
                    this.Close();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        { 
            try
            {
                List<InvProductProperty> invProductPropertyList = new List<InvProductProperty>();
                InvProductMasterService invProductMasterService = new InvProductMasterService();

                long departmentID = 0, CategoryID = 0, SubCategoryID = 0, subCategory2ID = 0, SupplierID = 0;
                string productFeature = "", country = "", cut = "", sleeve = "", heel = "", embelishment = "", fit = "", length = "", material = "", txture = "", neck = "",
                        collar = "", size = "", colour = "", patternNo = "", Brand = "", shop = ""; 
                decimal costPrice = 0, sellingPrice = 0;


                //foreach (DataGridViewRow item in dgvExtendedProperties.Rows)
                //{
                //    if (item.Index == 0) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { productFeature = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //    if (item.Index == 1) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { country = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //    if (item.Index == 2) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { cut = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //    if (item.Index == 3) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { sleeve = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //    if (item.Index == 4) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { heel = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //    if (item.Index == 5) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { embelishment = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //    if (item.Index == 6) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { fit = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //    if (item.Index == 7) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { length = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //    if (item.Index == 8) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { material = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //    if (item.Index == 9) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { txture = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //    if (item.Index == 10) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { neck = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //    if (item.Index == 11) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { collar = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //    if (item.Index == 12) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { size = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //    if (item.Index == 13) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { colour = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //    if (item.Index == 14) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { patternNo = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //    if (item.Index == 15) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { Brand = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //    if (item.Index == 15) { if (dgvExtendedProperties["ValueData", item.Index].Value != null) { shop = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); } }
                //}

                foreach (DataGridViewRow item in dgvExtendedProperties.Rows)
                {
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "PRODUCTFEATURE") { productFeature = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "COUNTRY") { country = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "CUT") { cut = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "SLEEVE") { sleeve = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "HEEL") { heel = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "EMBELISHMENT") { embelishment = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "FIT") { fit = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "LENGTH") { length = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "MATERIAL") { material = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "TEXTURE") { txture = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "NECK") { neck = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "COLLAR") { collar = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "SIZE") { size = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "COLOUR") { colour = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "PATTERNNO") { patternNo = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "BRAND") { Brand = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                    if (dgvExtendedProperties["ExtendedPropertyName", item.Index].Value.ToString().Trim() == "SHOP") { shop = dgvExtendedProperties["ValueData", item.Index].Value.ToString().Trim(); }
                }

                if (!string.IsNullOrEmpty(txtDepartmentCode.Text.Trim()))
                {
                    InvDepartment invDepartment = new InvDepartment();
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    invDepartment = invDepartmentService.GetInvDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDependCategory);
                    departmentID = invDepartment.InvDepartmentID;
                }
                if (!string.IsNullOrEmpty(txtCategoryCode.Text.Trim()))
                {
                    InvCategoryService invCategoryService = new InvCategoryService();
                    InvCategory invCategory = new InvCategory();
                    invCategory = invCategoryService.GetInvCategoryByCode(txtCategoryCode.Text.Trim(), isDependSubCategory);
                    CategoryID = invCategory.InvCategoryID;
                }
                if (!string.IsNullOrEmpty(txtSubCategoryCode.Text.Trim()))
                {
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    InvSubCategory invSubCategory = new InvSubCategory();
                    invSubCategory = invSubCategoryService.GetInvSubCategoryByCode(txtSubCategoryCode.Text.Trim(), isDependSubCategory2);
                    SubCategoryID = invSubCategory.InvSubCategoryID;
                }
                if (!string.IsNullOrEmpty(txtSubCategory2Code.Text.Trim()))
                {
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    InvSubCategory2 invSubCategory2 = new InvSubCategory2();
                    invSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByCode(txtSubCategory2Code.Text.Trim());
                    subCategory2ID = invSubCategory2.InvSubCategory2ID;
                }
                if (!string.IsNullOrEmpty(txtMainSupplierCode.Text.Trim()))
                {
                    SupplierService supplierService = new SupplierService();
                    Supplier supplier = new Supplier();
                    supplier = supplierService.GetSupplierByCode(txtMainSupplierCode.Text.Trim());
                    SupplierID = supplier.SupplierID;
                }
                if (!string.IsNullOrEmpty(txtCostPrice.Text.Trim()))
                {
                    costPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtSellingPrice.Text.Trim()))
                {
                    sellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text.Trim());
                }


                invProductPropertyList = invProductMasterService.GetSearchProducts(departmentID, CategoryID, SubCategoryID, subCategory2ID, SupplierID,
                                                            costPrice, sellingPrice, productFeature, country, cut, sleeve,
                                                            heel, embelishment, fit, length, material, txture, neck,
                                                            collar, size, colour, patternNo, Brand, shop);
                dgvExistingProducts.DataSource = null;
                dgvExistingProducts.DataSource = invProductPropertyList;
                dgvExistingProducts.Refresh();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationSubCategory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvSubCategoryService invSubCategoryService = new Service.InvSubCategoryService();
                Common.SetAutoComplete(txtSubCategory2Code, invSubCategoryService.GetInvSubCategoryCodes(false), chkAutoCompleationSubCategory.Checked);
                Common.SetAutoComplete(txtSubCategory2Description, invSubCategoryService.GetInvSubCategoryNames(false), chkAutoCompleationSubCategory.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Common.ClearForm(this);
                dgvExtendedProperties.DataSource = null;
                invProductExtendedPropertyValueGrid = new List<InvProductExtendedPropertyValue>();

                //InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
                //invProductExtendedProperty = invProductExtendedPropertyService.GetAllActiveInvProductExtendedProperties();
                //dgvExtendedProperties.DataSource = invProductExtendedProperty;
                //dgvExtendedProperties.Refresh();

                txtDepartmentCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvExistingProducts_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvExistingProducts.CurrentCell != null && dgvExistingProducts.CurrentCell.RowIndex >= 0)
                {
                    string productCode = dgvExistingProducts["ProductCode", dgvExistingProducts.CurrentCell.RowIndex].Value.ToString().Trim();
                    
                }
            }
            catch (Exception ex)
            {
               Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPropertyName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    InvProductExtendedProperty invProductExtendedProperty = new InvProductExtendedProperty();
                    InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();

                    invProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByName(txtPropertyName.Text.Trim());

                    if (invProductExtendedProperty != null)
                    {
                        InvProductExtendedPropertyValueService invProductExtendedPropertyValueService = new InvProductExtendedPropertyValueService();
                        Common.SetAutoComplete(txtPropertyValue, invProductExtendedPropertyValueService.GetExtendedPropertyValuesAccordingToPropertyName(invProductExtendedProperty), true);
                        txtPropertyValue.Focus();
                    }
                    else
                    {
                        Toast.Show("Invalid property name", Toast.messageType.Information, Toast.messageAction.General);
                        txtPropertyName.Focus();
                        txtPropertyName.SelectAll();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPropertyValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                if (string.IsNullOrEmpty(txtPropertyName.Text.Trim()))
                {
                    Toast.Show("Please select valid property", Toast.messageType.Information, Toast.messageAction.General);
                    txtPropertyName.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPropertyValue.Text.Trim()))
                { return; }

                InvProductExtendedProperty invProductExtendedProperty = new InvProductExtendedProperty();
                InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();

                invProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByName(txtPropertyName.Text.Trim());

                if (!IsExtendedValueByNameByProperty(txtPropertyValue.Text, invProductExtendedProperty.InvProductExtendedPropertyID))
                {
                    txtPropertyValue.Focus();
                    return;
                }

                AssignProductExtendedProperties();
                txtPropertyName.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool IsExtendedValueByNameByProperty(string valueName, long extendedPropertyID)
        {
            bool recodFound = false;
            InvProductExtendedValueService invProductExtendedValueService = new InvProductExtendedValueService();
            InvProductExtendedValue invProductExtendedValue = new InvProductExtendedValue();
            invProductExtendedValue = invProductExtendedValueService.GetInvProductExtendedValueByValueDataByExtendedPropertyID(valueName, extendedPropertyID);

            if (invProductExtendedValue != null)
            {
                recodFound = true;
                txtPropertyValue.Text = invProductExtendedValue.ValueData;
            }
            else
            {
                recodFound = false;
                Toast.Show("Product Extended Value ", Toast.messageType.Information, Toast.messageAction.NotExists);
                txtPropertyValue.Focus();
                txtPropertyValue.SelectAll();
            }

            return recodFound;
        }

        private void AssignProductExtendedProperties()
        {
            InvProductExtendedPropertyValue invProductExtendedPropertyValue = new InvProductExtendedPropertyValue();
            InvProductExtendedProperty invProductExtendedProperty = new InvProductExtendedProperty();
            InvProductExtendedValue invProductExtendedValue = new InvProductExtendedValue();
            InvProductMasterService invProductMasterService = new InvProductMasterService();
            InvProductExtendedPropertyService invProductExtendedPropertyService = new InvProductExtendedPropertyService();
            InvProductExtendedValueService invProductExtendedValueService = new InvProductExtendedValueService();
            InvProductExtendedPropertyValueService invProductExtendedPropertyValueService = new InvProductExtendedPropertyValueService();

            invProductExtendedProperty = invProductExtendedPropertyService.GetInvProductExtendedPropertyByName(txtPropertyName.Text.Trim());
            invProductExtendedValue = invProductExtendedValueService.GetInvProductExtendedValueByValueData(txtPropertyValue.Text.Trim());

            invProductExtendedPropertyValue.ProductID = 0;
            invProductExtendedPropertyValue.InvProductExtendedPropertyID = invProductExtendedProperty.InvProductExtendedPropertyID;
            invProductExtendedPropertyValue.ExtendedPropertyName = invProductExtendedProperty.ExtendedPropertyName;
            invProductExtendedPropertyValue.InvProductExtendedValueID = invProductExtendedValue.InvProductExtendedValueID;
            invProductExtendedPropertyValue.ValueData = invProductExtendedValue.ValueData;

            invProductExtendedPropertyValueGrid = invProductExtendedPropertyValueService.GetInvProductExtendedPropertyValueTempList(invProductExtendedPropertyValueGrid, invProductExtendedPropertyValue);

            dgvExtendedProperties.DataSource = null;
            dgvExtendedProperties.DataSource = invProductExtendedPropertyValueGrid;
            dgvExtendedProperties.Refresh();
            Common.ClearTextBox(txtPropertyName, txtPropertyValue);
            txtPropertyName.Focus();
        }

    }
}
