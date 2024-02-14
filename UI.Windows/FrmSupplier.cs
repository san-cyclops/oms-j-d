using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Domain;
using Report.Inventory;
using Service;
using Utility;
using System.IO;

namespace UI.Windows
{
    public partial class FrmSupplier : UI.Windows.FrmBaseMasterForm
    {
        private Supplier existingSupplier;
        private SupplierProperty existingSupplierProperty;
        private SupplierGroup existingSupplierGroup;
        private AccLedgerAccount existingLedgerAccount;
        private AccLedgerAccount existingOtherLedgerAccount;
        private ReferenceType existingReferenceType;
        private Tax existingTax;

        UserPrivileges accessRights = new UserPrivileges();
        AutoCompleteStringCollection autoCompleteSupplierCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteLegerCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteOtherLegerCode = new AutoCompleteStringCollection();

        string imagename;
        private int availableTaxCount;
        int documentID = 0;
        //int entryLevel;

        Image imgVatRegCopy = UI.Windows.Properties.Resources.Default_Document;
        Image imgPassportPhoto = UI.Windows.Properties.Resources.Default_Document;
        Image imgccdCertification = UI.Windows.Properties.Resources.Default_Document;
        Image imgbrandCertification = UI.Windows.Properties.Resources.Default_Document;
        Image imgreferenceDocumentCopy = UI.Windows.Properties.Resources.Default_Document;
        Image imglicenceToImport = UI.Windows.Properties.Resources.Default_Document;
        Image imglabCertification = UI.Windows.Properties.Resources.Default_Document;
        Image imgbrCopy = UI.Windows.Properties.Resources.Default_Document;
        Image imgdistributorOwnerCopy = UI.Windows.Properties.Resources.Default_Document;
        Image imgqcCertification = UI.Windows.Properties.Resources.Default_Document;
        Image imgslsIsoCertification = UI.Windows.Properties.Resources.Default_Document;

        public FrmSupplier()
        {
            InitializeComponent();
        }

        #region Form Events
        private void FrmSupplier_Load(object sender, EventArgs e)
        {
            
        }
        
        /// <summary>
        /// Get new code on user demand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Common.EnableButton(false, btnNew, btnDelete);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    SupplierService supplierService = new SupplierService();
                    txtSupplierCode.Text = supplierService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength);
                    Common.EnableTextBox(false, txtSupplierCode);
                    txtSupplierName.Focus();
                }
                else
                {
                    Common.EnableTextBox(true, txtSupplierCode);
                    txtSupplierCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                //specify your own initial directory
                fldlg.InitialDirectory = @":D\";
                //this will allow only those file extensions to be added
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imagename = fldlg.FileName;
                    Bitmap newimg = new Bitmap(imagename);
                    pbSupplier.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbSupplier.Image = (Image)newimg;
                }
                fldlg = null;
            }
            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                Logger.WriteLog(ae, MethodBase.GetCurrentMethod().Name.ToString(), this.Name);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnClearImage_Click(object sender, EventArgs e)
        {
            pbSupplier.Image = UI.Windows.Properties.Resources.Default_Supplier;
        }

        private void btnCollector1Browse_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                //specify your own initial directory
                fldlg.InitialDirectory = @":D\";
                //this will allow only those file extensions to be added
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imagename = fldlg.FileName;
                    Bitmap newimg = new Bitmap(imagename);
                    pbCollector1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbCollector1.Image = (Image)newimg;
                }
                fldlg = null;
            }
            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                Logger.WriteLog(ae, MethodBase.GetCurrentMethod().Name.ToString(), this.Name);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnCollector1Clear_Click(object sender, EventArgs e)
        {
            try
            {
                pbCollector1.Image = UI.Windows.Properties.Resources.Default_Collector1;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnCollector2Browse_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                //specify your own initial directory
                fldlg.InitialDirectory = @":D\";
                //this will allow only those file extensions to be added
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imagename = fldlg.FileName;
                    Bitmap newimg = new Bitmap(imagename);
                    pbCollector2.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbCollector2.Image = (Image)newimg;
                }
                fldlg = null;
            }
            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                Logger.WriteLog(ae.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnCollector2Clear_Click(object sender, EventArgs e)
        {
            try
            {
                pbCollector2.Image = UI.Windows.Properties.Resources.Default_Collector2;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    SupplierService supplierService = new SupplierService();
                    DataView dvAllReferenceData = new DataView(supplierService.GetAllActiveSuppliersDataTable());
                    if (dvAllReferenceData.Count>0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtSupplierCode);
                        txtSupplierCode_Leave(this, e);
                    }
                }
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                Common.SetFocus(e, cmbTitle);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbTitle_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtSupplierName);
                txtSupplierName.SelectionStart = txtSupplierName.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    SupplierService supplierService = new SupplierService();
                    DataView dvAllReferenceData = new DataView(supplierService.GetAllActiveSuppliersDataTable());
                    if (dvAllReferenceData.Count > 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtSupplierCode);
                        txtSupplierCode_Leave(this, e);
                    }
                }
                Common.SetFocus(e, tabSupplier, tbpGeneral);
                Common.SetFocus(e, cmbGender);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbGender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbSupplierGroup);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbSupplierGroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtReferenceNo);
                txtReferenceNo.SelectionStart = txtReferenceNo.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtReferenceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtRepresentative);
                txtRepresentative.SelectionStart = txtRepresentative.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtRepresentative_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtRepresentativeNicNo);
                txtRepresentativeNicNo.SelectionStart = txtRepresentativeNicNo.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtRepresentativeNicNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabSupplier, tbpContactDetails);
                Common.SetFocus(e, txtBillingAddress1);
                txtBillingAddress1.SelectionStart = txtBillingAddress1.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBillingAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtBillingAddress2);
                txtBillingAddress2.SelectionStart = txtBillingAddress2.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBillingAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtBillingAddress3);
                txtBillingAddress3.SelectionStart = txtBillingAddress3.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBillingAddress3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtContactPerson);
                txtContactPerson.SelectionStart = txtContactPerson.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtContactPerson_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtEmail);
                txtEmail.SelectionStart = txtEmail.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtBillingTelephone);
                txtBillingTelephone.SelectionStart = txtBillingTelephone.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBillingTelephone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtBillingMobile);
                txtBillingMobile.SelectionStart = txtBillingMobile.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBillingMobile_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtBillingFax);
                txtBillingFax.SelectionStart = txtBillingFax.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBillingFax_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabSupplier, tbpDeliveryDetails);
                Common.SetFocus(e, txtDeliveryAddress1);
                txtDeliveryAddress1.SelectionStart = txtDeliveryAddress1.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDeliveryAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtDeliveryAddress2);
                txtDeliveryAddress2.SelectionStart = txtDeliveryAddress2.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDeliveryAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtDeliveryAddress3);
                txtDeliveryAddress3.SelectionStart = txtDeliveryAddress3.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDeliveryAddress3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtDeliveryTelephone);
                txtDeliveryTelephone.SelectionStart = txtDeliveryTelephone.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDeliveryTelephone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtDeliveryMobile);
                txtDeliveryMobile.SelectionStart = txtDeliveryMobile.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDeliveryMobile_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtDeliveryFax);
                txtDeliveryFax.SelectionStart = txtDeliveryFax.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDeliveryFax_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabSupplier, tbpFinancialDetails);
                Common.SetFocus(e, txtLedgerCode);
                txtLedgerCode.SelectionStart = txtLedgerCode.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLedgerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtOtherLedgerCode);
                txtOtherLedgerCode.SelectionStart = txtOtherLedgerCode.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLedgerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtLedgerCode);
                txtLedgerCode.SelectionStart = txtLedgerCode.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherLedgerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbSupplierType);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherLedgerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtOtherLedgerCode);
                txtOtherLedgerCode.SelectionStart = txtOtherLedgerCode.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbSupplierType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                else
                {
                    cmbPaymentMethod.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtTax1No);
                txtTax1No.SelectionStart = txtTax1No.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTax1No_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                else
                {
                    if (availableTaxCount.Equals(1))
                    {
                        tabSupplier.SelectedIndex = (tabSupplier.SelectedIndex + 1) % tabSupplier.TabCount;
                        ActiveControl = txtDealingPeriod;
                        txtDealingPeriod.Focus();
                    }
                    else if (availableTaxCount >= 2)
                    {
                        ActiveControl = chkTax2;
                        chkTax2.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtTax2No);
                txtTax2No.SelectionStart = txtTax2No.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTax2No_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                else
                {
                    if (availableTaxCount.Equals(2))
                    {
                        tabSupplier.SelectedIndex = (tabSupplier.SelectedIndex + 1) % tabSupplier.TabCount;
                        ActiveControl = txtDealingPeriod;
                        txtDealingPeriod.Focus();
                    }
                    else if (availableTaxCount >= 3)
                    {
                        ActiveControl = chkTax3;
                        chkTax3.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtTax3No);
                txtTax3No.SelectionStart = txtTax3No.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTax3No_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                else
                {
                    if (availableTaxCount.Equals(3))
                    {
                        tabSupplier.SelectedIndex = (tabSupplier.SelectedIndex + 1) % tabSupplier.TabCount;
                        ActiveControl = txtDealingPeriod;
                        txtDealingPeriod.Focus();
                    }
                    else if (availableTaxCount >= 4)
                    {
                        ActiveControl = chkTax4;
                        chkTax4.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax4_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtTax4No);
                txtTax4No.SelectionStart = txtTax4No.Text.Length;
            }
            catch (Exception ex) 
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTax4No_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                else
                {
                    if (availableTaxCount.Equals(4))
                    {
                        tabSupplier.SelectedIndex = (tabSupplier.SelectedIndex + 1) % tabSupplier.TabCount;
                        ActiveControl = txtDealingPeriod;
                        txtDealingPeriod.Focus();
                    }
                    else if (availableTaxCount >= 5)
                    {
                        ActiveControl = chkTax5;
                        chkTax5.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax5_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtTax5No);
                txtTax5No.SelectionStart = txtTax5No.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTax5No_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                tabSupplier.SelectedIndex = (tabSupplier.SelectedIndex + 1) % tabSupplier.TabCount;
                ActiveControl = txtDealingPeriod;
                txtDealingPeriod.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDealingPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, dtpYearOfBusinessCommencement);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpYearOfBusinessCommencement_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtNoOfEmployees);
                txtNoOfEmployees.SelectionStart = txtNoOfEmployees.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtNoOfEmployees_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtNoOfMachines);
                txtNoOfMachines.SelectionStart = txtNoOfMachines.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtNoOfMachines_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtPercentageOfWht);
                txtPercentageOfWht.SelectionStart = txtPercentageOfWht.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPercentageOfWht_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtIncomeTaxFileNo);
                txtIncomeTaxFileNo.SelectionStart = txtIncomeTaxFileNo.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtIncomeTaxFileNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtPurchasingDepartmentRemark);
                txtPurchasingDepartmentRemark.SelectionStart = txtPurchasingDepartmentRemark.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPurchasingDepartmentRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtAccountsDepartmentRemarks);
                txtAccountsDepartmentRemarks.SelectionStart = txtAccountsDepartmentRemarks.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAccountsDepartmentRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkVatRegistrationCopy);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkVatRegistrationCopy_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkPassportPhotos);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkPassportPhotos_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkCddCertification);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkCddCertification_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkBrandCertification);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkBrandCertification_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkReferenceDocumentCopy);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkReferenceDocumentCopy_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkLicenseToImport);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkLicenseToImport_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkLabCertification);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkLabCertification_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkBrCopy);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkBrCopy_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkDistributorOwnerCopy);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkDistributorOwnerCopy_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkQcCertification);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkQcCertification_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, chkSLSCertification);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkSLSCertification_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabSupplier, tbpCollector1);
                Common.SetFocus(e, txtCollector1Name);
                txtCollector1Name.SelectionStart = txtCollector1Name.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCollector1Name_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtCollector1Designation);
                txtCollector1Designation.SelectionStart = txtCollector1Designation.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCollector1Designation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtCollector1ReferenceNo);
                txtCollector1ReferenceNo.SelectionStart = txtCollector1ReferenceNo.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCollector1ReferenceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtCollector1Telephone);
                txtCollector1Telephone.SelectionStart = txtCollector1Telephone.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCollector1Telephone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabSupplier, tbpCollector2);
                Common.SetFocus(e, txtCollector2Name);
                txtCollector2Name.SelectionStart = txtCollector2Name.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCollector2Name_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtCollector2Designation);
                txtCollector2Designation.SelectionStart = txtCollector2Designation.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCollector2Designation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtCollector2ReferenceNo);
                txtCollector2ReferenceNo.SelectionStart = txtCollector2ReferenceNo.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCollector2ReferenceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtCollector2Telephone);
                txtCollector2Telephone.SelectionStart = txtCollector2Telephone.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCollector2Telephone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabSupplier, tbpIntroducedPerson1);
                Common.SetFocus(e, txtPerson1Name);
                txtPerson1Name.SelectionStart = txtPerson1Name.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPerson1Name_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtPerson1Address1);
                txtPerson1Address1.SelectionStart = txtPerson1Address1.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPerson1Address1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtPerson1Address2);
                txtPerson1Address2.SelectionStart = txtPerson1Address2.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPerson1Address2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtPerson1Address3);
                txtPerson1Address3.SelectionStart = txtPerson1Address3.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPerson1Address3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtPerson1Telephone);
                txtPerson1Telephone.SelectionStart = txtPerson1Telephone.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPerson1Telephone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabSupplier, tbpIntroducedPerson2);
                Common.SetFocus(e, txtPerson2Name);
                txtPerson2Name.SelectionStart = txtPerson2Name.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPerson2Name_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtPerson2Address1);
                txtPerson2Address1.SelectionStart = txtPerson2Address1.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPerson2Address1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtPerson2Address2);
                txtPerson2Address2.SelectionStart = txtPerson2Address2.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPerson2Address2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtPerson2Address3);
                txtPerson2Address3.SelectionStart = txtPerson2Address3.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPerson2Address3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtPerson2Telephone);
                txtPerson2Telephone.SelectionStart = txtPerson2Telephone.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
        
        private void txtPerson2Telephone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabSupplier, tbpOtherDetails);
                Common.SetFocus(e, txtReferenceSerialNo);
                txtReferenceSerialNo.SelectionStart = txtReferenceSerialNo.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtReferenceSerialNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtOrderCircle);
                txtOrderCircle.SelectionStart = txtOrderCircle.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void txtOrderCircle_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtRemark);
                txtRemark.SelectionStart = txtRemark.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, btnSave);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplierCode.Text.Trim() != string.Empty)
                {
                    SupplierService supplierService = new SupplierService();

                    existingSupplier = supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());

                    if (existingSupplier != null)
                    {
                        LoadSupplier(existingSupplier);

                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                    }
                    else
                    {
                        if (chkAutoClear.Checked)
                        {
                            Common.ClearTextBox(txtSupplierName, txtRemark, txtReferenceNo, txtRepresentative, txtReferenceNo,
                                                txtBillingAddress1, txtBillingAddress2, txtBillingAddress3, txtBillingTelephone, txtBillingMobile, txtBillingFax, txtContactPerson,
                                                txtEmail, txtDeliveryAddress1, txtDeliveryAddress2, txtDeliveryAddress3, txtDeliveryTelephone, txtDeliveryMobile, txtDeliveryFax,
                                                txtLedgerCode, txtLedgerName, txtOtherLedgerCode, txtOtherLedgerName, txtReferenceSerialNo);
                        }
                        if (btnNew.Enabled)
                        {
                            if (Toast.Show(this.Text + " - " + txtSupplierCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                            {
                                btnNew.PerformClick();
                            }
                        }
                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtSupplierCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbSupplierGroup_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbSupplierGroup.Text.Trim() != string.Empty)
                {
                    SupplierGroupService supplierGroupService = new SupplierGroupService();
                    existingSupplierGroup = supplierGroupService.GetSupplierGroupsByName(cmbSupplierGroup.Text.Trim());
                    if (existingSupplierGroup != null)
                    {
                        cmbSupplierGroup.Text = existingSupplierGroup.SupplierGroupName;
                        txtReferenceNo.Focus();
                    }
                    else
                    {
                        Toast.Show(lblSupplierGroup.Text + "  - " + cmbSupplierGroup.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists).Equals(DialogResult.Yes);
                        cmbSupplierGroup.Text = string.Empty;
                        cmbSupplierGroup.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLedgerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtLedgerCode.Text.Trim() != string.Empty)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    existingLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtLedgerCode.Text.Trim());
                    if (existingLedgerAccount != null)
                    {
                        txtLedgerCode.Text = existingLedgerAccount.LedgerCode;
                        txtLedgerName.Text = existingLedgerAccount.LedgerName;
                        txtOtherLedgerCode.Focus();
                    }
                    else
                    {
                        Toast.Show(lblLedger.Text + "  - " + txtLedgerCode.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists).Equals(DialogResult.Yes);
                        txtLedgerCode.Text = string.Empty;
                        txtLedgerCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherLedgerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtOtherLedgerCode.Text.Trim() != string.Empty)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    existingOtherLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtOtherLedgerCode.Text.Trim());
                    if (existingOtherLedgerAccount != null)
                    {
                        txtOtherLedgerCode.Text = existingOtherLedgerAccount.LedgerCode;
                        txtOtherLedgerName.Text = existingOtherLedgerAccount.LedgerName;
                        cmbSupplierType.Focus();
                    }
                    else
                    {
                        Toast.Show(lblOtherLedger.Text + "  - " + txtOtherLedgerCode.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists).Equals(DialogResult.Yes);
                        txtOtherLedgerCode.Text = string.Empty;
                        txtOtherLedgerCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLedgerName_Leave(object sender, EventArgs e)
        {
            try
            {
            if (txtLedgerName.Text.Trim() != string.Empty)
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                existingLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByName(txtLedgerName.Text.Trim());
                if (existingLedgerAccount != null)
                {
                    txtLedgerCode.Text = existingLedgerAccount.LedgerCode;
                    txtLedgerName.Text = existingLedgerAccount.LedgerName;
                    txtLedgerCode.Focus();
                }
                else
                {
                    Toast.Show(lblLedger.Text + "  - " + txtLedgerName.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists).Equals(DialogResult.Yes);
                    txtLedgerName.Text = string.Empty;
                    txtLedgerName.Focus();
                }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherLedgerName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtOtherLedgerName.Text.Trim() != string.Empty)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    existingOtherLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByName(txtOtherLedgerName.Text.Trim());
                    if (existingOtherLedgerAccount != null)
                    {
                        txtOtherLedgerCode.Text = existingOtherLedgerAccount.LedgerCode;
                        txtOtherLedgerName.Text = existingOtherLedgerAccount.LedgerName;
                        txtOtherLedgerCode.Focus();
                    }
                    else
                    {
                        Toast.Show(lblLedger.Text + "  - " + txtOtherLedgerName.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists).Equals(DialogResult.Yes);
                        txtOtherLedgerName.Text = string.Empty;
                        txtOtherLedgerName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationSupplier_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SupplierService supplierService = new SupplierService();
                Common.SetAutoComplete(txtSupplierCode, supplierService.GetSupplierCodes(), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, supplierService.GetSupplierNames(), chkAutoCompleationSupplier.Checked); 
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTax1.Checked) { txtTax1No.Enabled = true; txtTax1No.Focus(); }
                else 
                { 
                    txtTax1No.Enabled = false;
                    txtTax1No.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTax2.Checked) { txtTax2No.Enabled = true; txtTax2No.Focus(); }
                else { txtTax2No.Enabled = false; txtTax2No.Text = string.Empty; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTax3.Checked) { txtTax3No.Enabled = true; txtTax3No.Focus(); }
                else { txtTax3No.Enabled = false; txtTax3No.Text = string.Empty; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTax4.Checked) { txtTax4No.Enabled = true; txtTax4No.Focus(); }
                else { txtTax4No.Enabled = false; txtTax4No.Text = string.Empty; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTax5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTax5.Checked) { txtTax5No.Enabled = true; txtTax5No.Focus(); }
                else { txtTax5No.Enabled = false; txtTax5No.Text = string.Empty; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnVatRegCopy_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = @":D\";
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imgVatRegCopy = Image.FromFile(fldlg.FileName);
                }
                fldlg = null;

            }
            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                Logger.WriteLog(ae, MethodBase.GetCurrentMethod().Name.ToString(), this.Name);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnPassport_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = @":D\";
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imgPassportPhoto = Image.FromFile(fldlg.FileName);
                }
                fldlg = null;
            }
            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                Logger.WriteLog(ae, MethodBase.GetCurrentMethod().Name.ToString(), this.Name);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnCcdCertification_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = @":D\";
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imgccdCertification = Image.FromFile(fldlg.FileName);
                }
                fldlg = null;
            }
            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                Logger.WriteLog(ae, MethodBase.GetCurrentMethod().Name.ToString(), this.Name);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnBrandCertification_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = @":D\";
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imgbrandCertification = Image.FromFile(fldlg.FileName);
                }
                fldlg = null;
            }
            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                Logger.WriteLog(ae, MethodBase.GetCurrentMethod().Name.ToString(), this.Name);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnRefDocument_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = @":D\";
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imgreferenceDocumentCopy = Image.FromFile(fldlg.FileName);
                }
                fldlg = null;
            }
            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                Logger.WriteLog(ae, MethodBase.GetCurrentMethod().Name.ToString(), this.Name);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnLicenceToImport_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = @":D\";
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imglicenceToImport = Image.FromFile(fldlg.FileName);
                }
                fldlg = null;
            }
            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                Logger.WriteLog(ae, MethodBase.GetCurrentMethod().Name.ToString(), this.Name);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnLabCertification_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = @":D\";
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imglabCertification = Image.FromFile(fldlg.FileName);
                }
                fldlg = null;
            }
            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                Logger.WriteLog(ae, MethodBase.GetCurrentMethod().Name.ToString(), this.Name);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnBRCopy_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = @":D\";
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imgbrCopy = Image.FromFile(fldlg.FileName);
                }
                fldlg = null;
            }
            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                Logger.WriteLog(ae, MethodBase.GetCurrentMethod().Name.ToString(), this.Name);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnDistributorOwner_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = @":D\";
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imgdistributorOwnerCopy = Image.FromFile(fldlg.FileName);
                }
                fldlg = null;
            }
            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                Logger.WriteLog(ae, MethodBase.GetCurrentMethod().Name.ToString(), this.Name);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnQcCertification_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = @":D\";
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imgqcCertification = Image.FromFile(fldlg.FileName);
                }
                fldlg = null;
            }
            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                Logger.WriteLog(ae, MethodBase.GetCurrentMethod().Name.ToString(), this.Name);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSlsIsoCertification_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = @":D\";
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imgslsIsoCertification = Image.FromFile(fldlg.FileName);
                }
                fldlg = null;
            }
            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                Logger.WriteLog(ae, MethodBase.GetCurrentMethod().Name.ToString(), this.Name);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkVatRegistrationCopy_MouseHover(object sender, EventArgs e)
        {
            try
            {
                pbDocument.Image = imgVatRegCopy;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkPassportPhotos_MouseHover(object sender, EventArgs e)
        {
            try
            {
                pbDocument.Image = imgPassportPhoto;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkCddCertification_MouseHover(object sender, EventArgs e)
        {
            try
            {
                pbDocument.Image = imgccdCertification;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkBrandCertification_MouseHover(object sender, EventArgs e)
        {
            try
            {
                pbDocument.Image = imgbrandCertification;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkReferenceDocumentCopy_MouseHover(object sender, EventArgs e)
        {
            try
            {
                pbDocument.Image = imgreferenceDocumentCopy;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkLicenseToImport_MouseHover(object sender, EventArgs e)
        {
            try
            {
                pbDocument.Image = imglicenceToImport;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkLabCertification_MouseHover(object sender, EventArgs e)
        {
            try
            {
                pbDocument.Image = imglabCertification;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkBrCopy_MouseHover(object sender, EventArgs e)
        {
            try
            {
                pbDocument.Image = imgbrCopy;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkDistributorOwnerCopy_MouseHover(object sender, EventArgs e)
        {
            try
            {
                pbDocument.Image = imgdistributorOwnerCopy;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkQcCertification_MouseHover(object sender, EventArgs e)
        {
            try
            {
                pbDocument.Image = imgqcCertification;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkSLSCertification_MouseHover(object sender, EventArgs e)
        {
            try
            {
                pbDocument.Image = imgslsIsoCertification;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            try
            {
                if (pbDocument.Image != null)
                {
                    FrmDocumentViewer frmPictureView = new FrmDocumentViewer(pbDocument.Image);
                    frmPictureView.ShowDialog();
                }
                else
                {
                    Toast.Show("No image to display", Toast.messageType.Information, Toast.messageAction.General);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void pbxDocument_DoubleClick(object sender, EventArgs e)
        {
            if (pbDocument.Image != null)
            {
                FrmDocumentViewer frmPictureView = new FrmDocumentViewer(pbDocument.Image);
                frmPictureView.ShowDialog();
            }
            else
            {
                Toast.Show("No image to display", Toast.messageType.Information, Toast.messageAction.General);
            }
        }

        private void chkVatRegistrationCopy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVatRegistrationCopy.Checked)
            {
                btnVatRegCopy.Enabled = true;
                imgVatRegCopy = UI.Windows.Properties.Resources.Default_Document;
            }
            else
            {
                btnVatRegCopy.Enabled = false;
                imgVatRegCopy = UI.Windows.Properties.Resources.Default_Document;
            }
        }

        private void chkPassportPhotos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPassportPhotos.Checked)
            {
                btnPassport.Enabled = true;
                imgPassportPhoto = UI.Windows.Properties.Resources.Default_Document;
            }
            else
            {
                btnPassport.Enabled = false;
                imgPassportPhoto = UI.Windows.Properties.Resources.Default_Document;
            }
        }

        private void chkCddCertification_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCddCertification.Checked)
            {
                btnCcdCertification.Enabled = true;
                imgccdCertification = UI.Windows.Properties.Resources.Default_Document;
            }
            else
            {
                btnCcdCertification.Enabled = false;
                imgccdCertification = UI.Windows.Properties.Resources.Default_Document;
            }
        }

        private void chkBrandCertification_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBrandCertification.Checked)
            {
                btnBrandCertification.Enabled = true;
                imgbrandCertification = UI.Windows.Properties.Resources.Default_Document;
            }
            else
            {
                btnBrandCertification.Enabled = false;
                imgbrandCertification = UI.Windows.Properties.Resources.Default_Document;
            }
        }

        private void chkReferenceDocumentCopy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReferenceDocumentCopy.Checked)
            {
                btnRefDocument.Enabled = true;
                imgreferenceDocumentCopy = UI.Windows.Properties.Resources.Default_Document;
            }
            else
            {
                btnRefDocument.Enabled = false;
                imgreferenceDocumentCopy = UI.Windows.Properties.Resources.Default_Document;
            }
        }

        private void chkLicenseToImport_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLicenseToImport.Checked)
            {
                btnLicenceToImport.Enabled = true;
                imglicenceToImport = UI.Windows.Properties.Resources.Default_Document;
            }
            else
            {
                btnLicenceToImport.Enabled = false;
                imglicenceToImport = UI.Windows.Properties.Resources.Default_Document;
            }
        }

        private void chkLabCertification_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLabCertification.Checked)
            {
                btnLabCertification.Enabled = true;
                imglabCertification = UI.Windows.Properties.Resources.Default_Document;
            }
            else
            {
                btnLabCertification.Enabled = false;
                imglabCertification = UI.Windows.Properties.Resources.Default_Document;
            }
        }

        private void chkBrCopy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBrCopy.Checked)
            {
                btnBRCopy.Enabled = true;
                imgbrCopy = UI.Windows.Properties.Resources.Default_Document;
            }
            else
            {
                btnBRCopy.Enabled = false;
                imgbrCopy = UI.Windows.Properties.Resources.Default_Document;
            }
        }

        private void chkDistributorOwnerCopy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDistributorOwnerCopy.Checked)
            {
                btnDistributorOwner.Enabled = true;
                imgdistributorOwnerCopy = UI.Windows.Properties.Resources.Default_Document;
            }
            else
            {
                btnDistributorOwner.Enabled = false;
                imgdistributorOwnerCopy = UI.Windows.Properties.Resources.Default_Document;
            }

        }

        private void chkQcCertification_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQcCertification.Checked)
            {
                btnQcCertification.Enabled = true;
                imgqcCertification = UI.Windows.Properties.Resources.Default_Document;
            }
            else
            {
                btnQcCertification.Enabled = false;
                imgqcCertification = UI.Windows.Properties.Resources.Default_Document;
            }
        }

        private void chkSLSCertification_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSLSCertification.Checked)
            {
                btnSlsIsoCertification.Enabled = true;
                imgslsIsoCertification = UI.Windows.Properties.Resources.Default_Document;
            }
            else
            {
                btnSlsIsoCertification.Enabled = false;
                imgslsIsoCertification = UI.Windows.Properties.Resources.Default_Document;
            }
        }
        #endregion

        #region Methods

        public override void InitializeForm()
        {
            try
            {
                SupplierService supplierService = new SupplierService();

                autoCompleteSupplierCode.AddRange(supplierService.GetSupplierCodes());
                Common.SetAutoComplete(txtSupplierCode, supplierService.GetSupplierCodes(), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, supplierService.GetSupplierNames(), chkAutoCompleationSupplier.Checked); 

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete, btnView);
                Common.EnableTextBox(true, txtSupplierCode);
                Common.EnableTextBox(false, txtTax1No, txtTax2No, txtTax3No, txtTax4No, txtTax5No);
                Common.ClearTextBox(txtSupplierCode);

                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                pbSupplier.Image = UI.Windows.Properties.Resources.Default_Supplier;
                pbCollector1.Image = UI.Windows.Properties.Resources.Default_Collector1;
                pbCollector2.Image = UI.Windows.Properties.Resources.Default_Collector2;
                pbDocument.Image = UI.Windows.Properties.Resources.Default_Document;

                tabSupplier.SelectedTab = tbpGeneral;

                cmbTitle.SelectedIndex = -1;
                cmbGender.SelectedIndex = -1;
                cmbSupplierGroup.SelectedIndex = -1;
                cmbSupplierType.SelectedIndex = -1;

                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;

                tabSupplier.TabPages.Remove(tbpCollector1);
                tabSupplier.TabPages.Remove(tbpCollector2);
                tabSupplier.TabPages.Remove(tbpCompany);
                tabSupplier.TabPages.Remove(tbpDeliveryDetails);
                tabSupplier.TabPages.Remove(tbpFinancialDetails);
                tabSupplier.TabPages.Remove(tbpIntroducedPerson1);
                tabSupplier.TabPages.Remove(tbpIntroducedPerson2);
                tabSupplier.TabPages.Remove(tbpOtherDetails);


                ActiveControl = txtSupplierCode;
                txtSupplierCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void FormLoad()
        {
            try
            {
                if (Common.EntryLevel.Equals(1))
                {
                    lblLedger.Text = lblLedger.Text + "*";
                    lblOtherLedger.Text = lblOtherLedger.Text + "*";
                }

                ResetComponents();
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                
                base.FormLoad();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Save()
        {
            try
            {
                if (!ValidateControls()) { return; }

                //if (Common.EntryLevel.Equals(1)) 
                //{
                //    if (!ValidateLedger()) { return; }
                //}

                SupplierService supplierService = new SupplierService();
                SupplierPropertyService supplierPropertyService = new SupplierPropertyService();

                bool isNew = false;
                existingSupplier = supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());

                if (existingSupplier == null || existingSupplier.SupplierID == 0)
                {
                    existingSupplier = new Supplier();
                    existingSupplierProperty = new SupplierProperty();
                    isNew = true;
                }
                else
                {
                    existingSupplierProperty = existingSupplier.SupplierProperty; 
                    //supplierPropertyService.GetSupplierPropertyByID(existingSupplier.SupplierID);
                }

                FillSupplier();

                if (existingSupplier.SupplierID == 0)
                {
                    if ((Toast.Show(this.Text + " - " + existingSupplier.SupplierCode + " - " + existingSupplier.SupplierName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    
                    supplierService.AddSupplier(existingSupplier);

                    if ((Toast.Show(this.Text + " - " + existingSupplier.SupplierCode + " - " + existingSupplier.SupplierName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
                    {
                        if (chkAutoClear.Checked)
                        {
                            ClearForm();
                        }
                        btnNew.Enabled = true;
                        btnNew.PerformClick();
                    }
                    else
                    {
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
                    if (isNew)
                    {
                        if ((Toast.Show(this.Text + " - " + existingSupplier.SupplierCode + " - " + existingSupplier.SupplierName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingSupplier.SupplierCode + " - " + existingSupplier.SupplierName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show(this.Text + " - " + existingSupplier.SupplierCode + " - " + existingSupplier.SupplierName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    supplierService.UpdateSupplier(existingSupplier);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    Toast.Show(this.Text + " - " + existingSupplier.SupplierCode + " - " + existingSupplier.SupplierName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }
                txtSupplierCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private Supplier FillSupplier()
        {
            try
            {
                #region Supplier
                SupplierGroupService supplierGroupService = new SupplierGroupService();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                TaxService taxService = new TaxService();
                PaymentMethod paymentMethod = new PaymentMethod();
                PaymentMethodService paymentMethodService = new PaymentMethodService();

                existingSupplier.SupplierCode = txtSupplierCode.Text.Trim();
                existingSupplier.SupplierName = txtSupplierName.Text.Trim();
                existingSupplier.Remark = txtRemark.Text.Trim();
                existingSupplier.ReferenceNo = txtReferenceNo.Text.Trim();
                existingSupplier.RepresentativeName = txtRepresentative.Text.Trim();
                existingSupplier.RepresentativeNICNo = txtRepresentativeNicNo.Text.Trim();
                existingSupplier.BillingAddress1 = txtBillingAddress1.Text.Trim();
                existingSupplier.BillingAddress2 = txtBillingAddress2.Text.Trim();
                existingSupplier.BillingAddress3 = txtBillingAddress3.Text.Trim();
                existingSupplier.PostalCode = "";
                existingSupplier.BillingTelephone = txtBillingTelephone.Text.Trim();
                existingSupplier.BillingMobile = txtBillingMobile.Text.Trim();
                existingSupplier.BillingFax = txtBillingFax.Text.Trim();
                existingSupplier.Email = txtEmail.Text.Trim();
                existingSupplier.ContactPersonName = txtContactPerson.Text.Trim();
                existingSupplier.DeliveryAddress1 = txtDeliveryAddress1.Text.Trim();
                existingSupplier.DeliveryAddress2 = txtDeliveryAddress2.Text.Trim();
                existingSupplier.DeliveryAddress3 = txtDeliveryAddress3.Text.Trim();
                existingSupplier.DeliveryTelephone = txtDeliveryTelephone.Text.Trim();
                existingSupplier.DeliveryMobile = txtDeliveryMobile.Text.Trim();
                existingSupplier.DeliveryFax = txtDeliveryFax.Text.Trim();
                existingSupplier.ReferenceSerial = txtReferenceSerialNo.Text.Trim();
                existingSupplier.OrderCircle = Common.ConvertStringToInt(txtOrderCircle.Text.Trim());

                existingTax = taxService.GetTaxByName(lblTax1.Text.Trim());
                if (chkTax1.Checked)
                { existingSupplier.TaxID1 = existingTax.TaxID; }
                else { existingSupplier.TaxID1 = 0; }

                existingTax = taxService.GetTaxByName(lblTax2.Text.Trim());
                if (chkTax2.Checked) { existingSupplier.TaxID2 = existingTax.TaxID; }
                else { existingSupplier.TaxID2 = 0; }

                existingTax = taxService.GetTaxByName(lblTax3.Text.Trim());
                if (chkTax3.Checked) { existingSupplier.TaxID3 = existingTax.TaxID; }
                else { existingSupplier.TaxID3 = 0; }

                existingTax = taxService.GetTaxByName(lblTax4.Text.Trim());
                if (chkTax4.Checked) { existingSupplier.TaxID4 = existingTax.TaxID; }
                else { existingSupplier.TaxID4 = 0; }

                existingTax = taxService.GetTaxByName(lblTax5.Text.Trim());
                if (chkTax5.Checked) { existingSupplier.TaxID5 = existingTax.TaxID; }
                else { existingSupplier.TaxID5 = 0; }

                existingSupplier.TaxNo1 = txtTax1No.Text.Trim();
                existingSupplier.TaxNo2 = txtTax2No.Text.Trim();
                existingSupplier.TaxNo3 = txtTax3No.Text.Trim();
                existingSupplier.TaxNo4 = txtTax4No.Text.Trim();
                existingSupplier.TaxNo5 = txtTax5No.Text.Trim();

                if (chkTStatus.Checked) { existingSupplier.IsUpload = true; } else { existingSupplier.IsUpload = false; }

                existingSupplier.CreditLimit = Common.ConvertStringToDecimalCurrency(txtCreditLimit.Text.Trim());
                existingSupplier.CreditPeriod = Common.ConvertStringToInt(txtCreditPeriod.Text.Trim());
                existingSupplier.ChequeLimit = Common.ConvertStringToDecimalCurrency(txtChequeLimit.Text.Trim());
                existingSupplier.ChequePeriod = Common.ConvertStringToInt(txtChequePeriod.Text.Trim());

                paymentMethod = paymentMethodService.GetPaymentMethodsByName(cmbPaymentMethod.Text.Trim());
                if (paymentMethod != null) { existingSupplier.PaymentMethod = paymentMethod.PaymentMethodID; }
                else existingSupplier.PaymentMethod = 0;

                //Read Supplier Title
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.TitleType).ToString(), cmbTitle.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingSupplier.SupplierTitle = existingReferenceType.LookupKey;
                }
                else
                {
                    existingSupplier.SupplierTitle = 0;
                }

                //Read Gender Type
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.GenderType).ToString(), cmbGender.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingSupplier.Gender = existingReferenceType.LookupKey;
                }
                else
                {
                    existingSupplier.Gender = 0;
                }

                //Read Supplier Type
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.SupplierType).ToString(), cmbSupplierType.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingSupplier.SupplierType = existingReferenceType.LookupKey;
                }
                else
                {
                    existingSupplier.SupplierType = 0;
                }

                //Read Supplier Group ID
                existingSupplierGroup = supplierGroupService.GetSupplierGroupsByName(cmbSupplierGroup.Text.Trim());
                if (existingSupplierGroup != null)
                {
                    existingSupplier.SupplierGroupID = existingSupplierGroup.SupplierGroupID;
                }
                else
                {
                    existingSupplier.SupplierGroupID = 0;
                }

                //Read Ledger ID
                existingLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtLedgerCode.Text.Trim());
                if (existingLedgerAccount != null)
                {
                    existingSupplier.LedgerID = existingLedgerAccount.AccLedgerAccountID;
                }
                else
                {
                    existingSupplier.LedgerID = 0;
                }

                //Read Other Ledegr ID
                existingOtherLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtOtherLedgerCode.Text.Trim());
                if (existingOtherLedgerAccount != null)
                {
                    existingSupplier.OtherLedgerID = existingOtherLedgerAccount.AccLedgerAccountID;
                }
                else
                {
                    existingSupplier.OtherLedgerID = 0;
                }

                // update image
                //if (@imagename != null)
                //{
                //    FileStream fs;
                //    fs = new FileStream(@imagename, FileMode.Open, FileAccess.Read);
                //    //a byte array to read the image
                //    byte[] picbyte = new byte[fs.Length];
                //    fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
                //    fs.Close();
                //    existingSupplier.SupplierImage = picbyte;
                //}
                //else
                //{
                //    existingSupplier.SupplierImage = null;
                //}

                MemoryStream stream = new MemoryStream();
                pbSupplier.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] pic = stream.ToArray();
                existingSupplier.SupplierImage = pic;
                #endregion

                #region Supplier Property

                existingSupplierProperty.SupplierID = existingSupplier.SupplierID;
                existingSupplierProperty.SupplierCode = existingSupplier.SupplierCode.Trim();

                //Company Details
                existingSupplierProperty.DealingPeriod = Common.ConvertStringToLong(txtDealingPeriod.Text.ToString());
                existingSupplierProperty.YearOfBusinessCommencement = dtpYearOfBusinessCommencement.Value.Year;
                existingSupplierProperty.NoOfEmployees = Common.ConvertStringToInt(txtNoOfEmployees.Text.ToString().Trim());
                existingSupplierProperty.NoOfMachines = Common.ConvertStringToInt(txtNoOfMachines.Text.ToString().Trim());
                existingSupplierProperty.PercentageOfWHT = Common.ConvertStringToInt(txtPercentageOfWht.Text.ToString().Trim());
                existingSupplierProperty.IncomeTaxFileNo = txtIncomeTaxFileNo.Text.Trim();
                existingSupplierProperty.RemarkByPurchasingDepartment = txtPurchasingDepartmentRemark.Text.Trim();
                existingSupplierProperty.RemarkByAccountDepartment = txtAccountsDepartmentRemarks.Text.Trim();

                ImageConverter imageConverter = new ImageConverter(); 

                existingSupplierProperty.IsVatRegistrationCopy = chkVatRegistrationCopy.Checked;
                byte[] vatRegCopy = (byte[])imageConverter.ConvertTo(imgVatRegCopy, typeof(byte[]));
                existingSupplierProperty.VatRegistrationCopy = vatRegCopy;

                existingSupplierProperty.IsPassportPhotos = chkPassportPhotos.Checked;
                byte[] passportPhoto = (byte[])imageConverter.ConvertTo(imgPassportPhoto, typeof(byte[]));
                existingSupplierProperty.PassportPhotos = passportPhoto;

                existingSupplierProperty.IsCDDCertification = chkCddCertification.Checked;
                byte[] ccdCerticate = (byte[])imageConverter.ConvertTo(imgccdCertification, typeof(byte[]));
                existingSupplierProperty.CDDCertificate = ccdCerticate;

                existingSupplierProperty.IsBrandCertification = chkBrandCertification.Checked;
                byte[] brandCerticate = (byte[])imageConverter.ConvertTo(imgbrandCertification, typeof(byte[]));
                existingSupplierProperty.BrandCertificate = brandCerticate;

                existingSupplierProperty.IsRefeneceDocumentCopy = chkReferenceDocumentCopy.Checked;
                byte[] refDoc = (byte[])imageConverter.ConvertTo(imgreferenceDocumentCopy, typeof(byte[]));
                existingSupplierProperty.RefeneceDocumentCopy = refDoc;

                existingSupplierProperty.IsLicenceToImport = chkLicenseToImport.Checked;
                byte[] licence = (byte[])imageConverter.ConvertTo(imglicenceToImport, typeof(byte[]));
                existingSupplierProperty.LicenceToImport = licence;

                existingSupplierProperty.IsLabCertification = chkLabCertification.Checked;
                byte[] labCertificate = (byte[])imageConverter.ConvertTo(imglabCertification, typeof(byte[]));
                existingSupplierProperty.LabCertificate = labCertificate;

                existingSupplierProperty.IsBRCCopy = chkBrCopy.Checked;
                byte[] brCopy = (byte[])imageConverter.ConvertTo(imgbrCopy, typeof(byte[]));
                existingSupplierProperty.BRCCopy = brCopy;

                existingSupplierProperty.IsDistributorOwnerCopy = chkDistributorOwnerCopy.Checked;
                byte[] distributerOwner = (byte[])imageConverter.ConvertTo(imgdistributorOwnerCopy, typeof(byte[]));
                existingSupplierProperty.DistributorOwnerCopy = distributerOwner;

                existingSupplierProperty.IsQCCertification = chkQcCertification.Checked;
                byte[] qcCertificate = (byte[])imageConverter.ConvertTo(imgqcCertification, typeof(byte[]));
                existingSupplierProperty.QCCertificate = qcCertificate;

                existingSupplierProperty.IsSLSISOCertiification = chkSLSCertification.Checked;
                byte[] slsIso = (byte[])imageConverter.ConvertTo(imgslsIsoCertification, typeof(byte[]));
                existingSupplierProperty.SLSISOCertiificate = slsIso;

                //Collector 1
                existingSupplierProperty.PaymentCollector1Name = txtCollector1Name.Text.Trim();
                existingSupplierProperty.PaymentCollector1Designation = txtCollector1Designation.Text.Trim();
                existingSupplierProperty.PaymentCollector1ReferenceNo = txtCollector1ReferenceNo.Text.Trim();
                existingSupplierProperty.PaymentCollector1TelephoneNo = txtCollector1Telephone.Text.Trim();

                stream = new MemoryStream();
                pbCollector1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                pic = stream.ToArray();
                existingSupplierProperty.PaymentCollector1Image = pic;

                //Collector 2
                existingSupplierProperty.PaymentCollector2Name = txtCollector2Name.Text.Trim();
                existingSupplierProperty.PaymentCollector2Designation = txtCollector2Designation.Text.Trim();
                existingSupplierProperty.PaymentCollector2ReferenceNo = txtCollector2ReferenceNo.Text.Trim();
                existingSupplierProperty.PaymentCollector2TelephoneNo = txtCollector2Telephone.Text.Trim();

                stream = new MemoryStream();
                pbCollector2.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                pic = stream.ToArray();
                existingSupplierProperty.PaymentCollector2Image = pic;

                //Introducer 1
                existingSupplierProperty.IntroducedPerson1Name = txtPerson1Name.Text.Trim();
                existingSupplierProperty.IntroducedPerson1Address1 = txtPerson1Address1.Text.Trim();
                existingSupplierProperty.IntroducedPerson1Address2 = txtPerson1Address2.Text.Trim();
                existingSupplierProperty.IntroducedPerson1Address3 = txtPerson1Address3.Text.Trim();
                existingSupplierProperty.IntroducedPerson1TelephoneNo = txtPerson1Telephone.Text.Trim();

                //Introducer 2
                existingSupplierProperty.IntroducedPerson2Name = txtPerson2Name.Text.Trim();
                existingSupplierProperty.IntroducedPerson2Address1 = txtPerson2Address1.Text.Trim();
                existingSupplierProperty.IntroducedPerson2Address2 = txtPerson2Address2.Text.Trim();
                existingSupplierProperty.IntroducedPerson2Address3 = txtPerson2Address3.Text.Trim();
                existingSupplierProperty.IntroducedPerson2TelephoneNo = txtPerson2Telephone.Text.Trim();
                #endregion

                existingSupplier.SupplierProperty = existingSupplierProperty;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            return existingSupplier;
        }

        public override void Delete()
        {
            try
            {
                if (Toast.Show(this.Text + " - " + existingSupplier.SupplierCode + " - " + existingSupplier.SupplierName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                { return; }

                SupplierService supplierService = new SupplierService();
                SupplierPropertyService supplierPropertyService = new SupplierPropertyService();

                existingSupplier = supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());

                if (existingSupplier != null && existingSupplier.SupplierID != 0)
                {
                    existingSupplierProperty = existingSupplier.SupplierProperty; //supplierPropertyService.GetSupplierPropertyByID(existingSupplier.SupplierID);

                    existingSupplier.IsDelete = true;
                    existingSupplierProperty.IsDelete = true;
                    existingSupplier.SupplierProperty = existingSupplierProperty;
                    supplierService.UpdateSupplier(existingSupplier);
                    ClearForm();
                    Toast.Show(this.Text + " - " + existingSupplier.SupplierCode + " - " + existingSupplier.SupplierName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    txtSupplierCode.Focus();
                }
                else
                {
                    Toast.Show(this.Text + " - " + existingSupplier.SupplierCode + " - " + existingSupplier.SupplierName + "", Toast.messageType.Warning, Toast.messageAction.NotExists);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void Clear()
        {
            try
            {
                ResetComponents();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void View()
        {
            try
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSupplier");
                InvReportGenerator invReportGenerator = new InvReportGenerator();
                //invReportGenerator.OrganizeFormFields(autoGenerateInfo).MdiParent = this.MdiParent;
                //invReportGenerator.OrganizeFormFields(autoGenerateInfo).IsMdiContainer = true;
                //invReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();
                Form mdiMainRibbon = new MdiMainRibbon();
                
                invReportGenerator.OrganizeFormFields(autoGenerateInfo).MdiParent = mdiMainRibbon;
                invReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();

                //foreach (Form child in this.MdiParent.MdiChildren)
                //{
                //    child.Enabled = false;

                //}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadSupplier(Supplier existingSupplierMaster)
        {
            try
            
            {
                #region Supplier
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                AccLedgerAccount ledgerAccount = new AccLedgerAccount();
                AccLedgerAccount otherLedgerAccount = new AccLedgerAccount(); 
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                TaxService taxService = new TaxService();
                PaymentMethod paymentMethod = new PaymentMethod();
                PaymentMethodService paymentMethodService = new PaymentMethodService();

                txtSupplierCode.Text = existingSupplierMaster.SupplierCode.Trim();
                txtSupplierName.Text = existingSupplierMaster.SupplierName.Trim();
                txtRemark.Text = existingSupplierMaster.Remark.Trim();
                txtReferenceNo.Text = existingSupplierMaster.ReferenceNo.Trim();
                txtRepresentative.Text = existingSupplierMaster.RepresentativeName.Trim();
                txtRepresentativeNicNo.Text = existingSupplierMaster.RepresentativeNICNo.Trim();
                txtBillingAddress1.Text = existingSupplierMaster.BillingAddress1.Trim();
                txtBillingAddress2.Text = existingSupplierMaster.BillingAddress2.Trim();
                txtBillingAddress3.Text = existingSupplierMaster.BillingAddress3.Trim();
                txtBillingTelephone.Text = existingSupplierMaster.BillingTelephone.Trim();
                txtBillingMobile.Text = existingSupplierMaster.BillingMobile.Trim();
                txtBillingFax.Text = existingSupplierMaster.BillingFax.Trim();
                txtEmail.Text = existingSupplierMaster.Email.Trim();
                txtContactPerson.Text = existingSupplierMaster.ContactPersonName.Trim();
                txtDeliveryAddress1.Text = existingSupplierMaster.DeliveryAddress1.Trim();
                txtDeliveryAddress2.Text = existingSupplierMaster.DeliveryAddress2.Trim();
                txtDeliveryAddress3.Text = existingSupplierMaster.DeliveryAddress3.Trim();
                txtDeliveryTelephone.Text = existingSupplierMaster.DeliveryTelephone.Trim();
                txtDeliveryMobile.Text = existingSupplierMaster.DeliveryMobile.Trim();
                txtDeliveryFax.Text = existingSupplierMaster.DeliveryFax.Trim();
                txtReferenceSerialNo.Text = existingSupplierMaster.ReferenceSerial.Trim();
                txtOrderCircle.Text = existingSupplierMaster.OrderCircle.ToString();

                ledgerAccount = accLedgerAccountService.GetAccLedgerAccountByID(existingSupplierMaster.LedgerID);
                if (ledgerAccount != null)
                {
                    txtLedgerCode.Text = ledgerAccount.LedgerCode.Trim();
                    txtLedgerName.Text = ledgerAccount.LedgerName.Trim();
                }

                otherLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByID(existingSupplierMaster.OtherLedgerID);
                if (ledgerAccount != null)
                {
                    txtOtherLedgerCode.Text = otherLedgerAccount.LedgerCode.Trim();
                    txtOtherLedgerName.Text = otherLedgerAccount.LedgerName.Trim();
                }

                cmbSupplierGroup.Text = existingSupplierMaster.SupplierGroup.SupplierGroupName;
                txtReferenceSerialNo.Text = existingSupplierMaster.ReferenceSerial.Trim();

                txtTax1No.Text = existingSupplierMaster.TaxNo1.Trim();
                existingTax = taxService.GetTaxByID(existingSupplier.TaxID1);
                if (existingTax != null) { chkTax1.Checked = true; }
                else { chkTax1.Checked = false; }

                txtTax2No.Text = existingSupplierMaster.TaxNo2.Trim();
                existingTax = taxService.GetTaxByID(existingSupplier.TaxID2);
                if (existingTax != null) { chkTax2.Checked = true; }
                else { chkTax2.Checked = false; }

                txtTax3No.Text = existingSupplierMaster.TaxNo3.Trim();
                existingTax = taxService.GetTaxByID(existingSupplier.TaxID3);
                if (existingTax != null) { chkTax3.Checked = true; }
                else { chkTax3.Checked = false; }

                txtTax4No.Text = existingSupplierMaster.TaxNo4.Trim();
                existingTax = taxService.GetTaxByID(existingSupplier.TaxID4);
                if (existingTax != null) { chkTax4.Checked = true; }
                else { chkTax4.Checked = false; }

                txtTax5No.Text = existingSupplierMaster.TaxNo5.Trim();
                existingTax = taxService.GetTaxByID(existingSupplier.TaxID5);
                if (existingTax != null) { chkTax5.Checked = true; }
                else { chkTax5.Checked = false; }

                paymentMethod = paymentMethodService.GetPaymentMethodsByID(existingSupplierMaster.PaymentMethod);
                if (paymentMethod != null) { cmbPaymentMethod.Text = paymentMethod.PaymentMethodName; }
                else { cmbPaymentMethod.Text = string.Empty; }

                txtCreditLimit.Text = Common.ConvertDecimalToStringCurrency(existingSupplierMaster.CreditLimit);
                txtCreditPeriod.Text = existingSupplierMaster.CreditPeriod.ToString();
                txtChequeLimit.Text = Common.ConvertDecimalToStringCurrency(existingSupplierMaster.ChequeLimit);
                txtChequePeriod.Text = existingSupplierMaster.ChequePeriod.ToString();

                //Read Supplier Title
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.TitleType).ToString(), existingSupplierMaster.SupplierTitle);
                if (existingReferenceType != null)
                {
                    cmbTitle.Text = existingReferenceType.LookupValue;
                }
                else
                {
                    cmbTitle.SelectedIndex = -1;
                }

                //Read Gender Type
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.GenderType).ToString(), existingSupplierMaster.Gender);
                if (existingReferenceType != null)
                {
                    cmbGender.Text = existingReferenceType.LookupValue;
                }
                else
                {
                    cmbGender.SelectedIndex = -1;
                }

                //Read Supplier Type
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.SupplierType).ToString(), existingSupplierMaster.SupplierType);
                if (existingReferenceType != null)
                {
                    cmbSupplierType.Text = existingReferenceType.LookupValue;
                }
                else
                {
                    cmbSupplierType.SelectedIndex = -1;
                }

                if (existingSupplierMaster.SupplierImage != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingSupplierMaster.SupplierImage);
                    pbSupplier.Image = Image.FromStream(ms);
                    pbSupplier.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbSupplier.Refresh();
                }
                else
                {
                    pbSupplier.Image = UI.Windows.Properties.Resources.Default_Supplier;
                    pbSupplier.Refresh();
                }
                #endregion

                #region Supplier Property

                //Tab Company Details
                txtDealingPeriod.Text = existingSupplierMaster.SupplierProperty.DealingPeriod.ToString().Trim();
                
                //change with suitable code
                dtpYearOfBusinessCommencement.Text = new DateTime(existingSupplierMaster.SupplierProperty.YearOfBusinessCommencement, dtpYearOfBusinessCommencement.Value.Month, dtpYearOfBusinessCommencement.Value.Day).ToString();
                txtNoOfEmployees.Text = existingSupplierMaster.SupplierProperty.NoOfEmployees.ToString().Trim();
                txtNoOfMachines.Text = existingSupplierMaster.SupplierProperty.NoOfMachines.ToString().Trim();
                txtPercentageOfWht.Text = existingSupplierMaster.SupplierProperty.PercentageOfWHT.ToString().Trim();
                txtIncomeTaxFileNo.Text = existingSupplierMaster.SupplierProperty.IncomeTaxFileNo.Trim();
                txtPurchasingDepartmentRemark.Text = existingSupplierMaster.SupplierProperty.RemarkByPurchasingDepartment.Trim();
                txtAccountsDepartmentRemarks.Text = existingSupplierMaster.SupplierProperty.RemarkByAccountDepartment.Trim();

                chkVatRegistrationCopy.Checked = existingSupplierMaster.SupplierProperty.IsVatRegistrationCopy;
                if (existingSupplierMaster.SupplierProperty.VatRegistrationCopy != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingSupplierMaster.SupplierProperty.VatRegistrationCopy);
                    imgVatRegCopy = Image.FromStream(ms);
                }

                chkPassportPhotos.Checked = existingSupplierMaster.SupplierProperty.IsPassportPhotos;
                if (existingSupplierMaster.SupplierProperty.PassportPhotos != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingSupplierMaster.SupplierProperty.PassportPhotos);
                    imgPassportPhoto = Image.FromStream(ms);
                }

                chkCddCertification.Checked = existingSupplierMaster.SupplierProperty.IsCDDCertification;
                if (existingSupplierMaster.SupplierProperty.CDDCertificate != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingSupplierMaster.SupplierProperty.CDDCertificate);
                    imgccdCertification = Image.FromStream(ms);
                }

                chkBrandCertification.Checked = existingSupplierMaster.SupplierProperty.IsBrandCertification;
                if (existingSupplierMaster.SupplierProperty.BrandCertificate != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingSupplierMaster.SupplierProperty.BrandCertificate);
                    imgbrandCertification = Image.FromStream(ms);
                }

                chkReferenceDocumentCopy.Checked = existingSupplierMaster.SupplierProperty.IsRefeneceDocumentCopy;
                if (existingSupplierMaster.SupplierProperty.RefeneceDocumentCopy != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingSupplierMaster.SupplierProperty.RefeneceDocumentCopy);
                    imgreferenceDocumentCopy = Image.FromStream(ms);
                }

                chkLicenseToImport.Checked = existingSupplierMaster.SupplierProperty.IsLicenceToImport;
                if (existingSupplierMaster.SupplierProperty.LicenceToImport != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingSupplierMaster.SupplierProperty.LicenceToImport);
                    imglicenceToImport = Image.FromStream(ms);
                }

                chkLabCertification.Checked = existingSupplierMaster.SupplierProperty.IsLabCertification;
                if (existingSupplierMaster.SupplierProperty.LabCertificate != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingSupplierMaster.SupplierProperty.LabCertificate);
                    imglabCertification = Image.FromStream(ms);
                }

                chkBrCopy.Checked = existingSupplierMaster.SupplierProperty.IsBRCCopy;
                if (existingSupplierMaster.SupplierProperty.BRCCopy != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingSupplierMaster.SupplierProperty.BRCCopy);
                    imgbrCopy = Image.FromStream(ms);
                }

                chkDistributorOwnerCopy.Checked = existingSupplierMaster.SupplierProperty.IsDistributorOwnerCopy;
                if (existingSupplierMaster.SupplierProperty.DistributorOwnerCopy != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingSupplierMaster.SupplierProperty.DistributorOwnerCopy);
                    imgdistributorOwnerCopy = Image.FromStream(ms);
                }

                chkQcCertification.Checked = existingSupplierMaster.SupplierProperty.IsQCCertification;
                if (existingSupplierMaster.SupplierProperty.QCCertificate != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingSupplierMaster.SupplierProperty.QCCertificate);
                    imgqcCertification = Image.FromStream(ms);
                }

                chkSLSCertification.Checked = existingSupplierMaster.SupplierProperty.IsSLSISOCertiification;
                if (existingSupplierMaster.SupplierProperty.SLSISOCertiificate != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingSupplierMaster.SupplierProperty.SLSISOCertiificate);
                    imgslsIsoCertification = Image.FromStream(ms);
                }

                //Tab Collector 1
                txtCollector1Name.Text = existingSupplierMaster.SupplierProperty.PaymentCollector1Name.Trim();
                txtCollector1Designation.Text = existingSupplierMaster.SupplierProperty.PaymentCollector1Designation.Trim();
                txtCollector1ReferenceNo.Text = existingSupplierMaster.SupplierProperty.PaymentCollector1ReferenceNo.Trim();
                txtCollector1Telephone.Text = existingSupplierMaster.SupplierProperty.PaymentCollector1TelephoneNo.Trim();

                if (existingSupplierMaster.SupplierProperty.PaymentCollector1Image != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingSupplierMaster.SupplierProperty.PaymentCollector1Image);
                    pbCollector1.Image = Image.FromStream(ms);
                    pbCollector1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbCollector1.Refresh();
                }
                else
                {
                    pbCollector1.Image = UI.Windows.Properties.Resources.Default_Collector1;
                    pbCollector1.Refresh();
                }

                //Tab Collector 2
                txtCollector2Name.Text = existingSupplierMaster.SupplierProperty.PaymentCollector2Name.Trim();
                txtCollector2Designation.Text = existingSupplierMaster.SupplierProperty.PaymentCollector2Designation.Trim();
                txtCollector2ReferenceNo.Text = existingSupplierMaster.SupplierProperty.PaymentCollector2ReferenceNo.Trim();
                txtCollector2Telephone.Text = existingSupplierMaster.SupplierProperty.PaymentCollector2TelephoneNo.Trim();

                if (existingSupplierMaster.SupplierProperty.PaymentCollector2Image != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingSupplierMaster.SupplierProperty.PaymentCollector2Image);
                    pbCollector2.Image = Image.FromStream(ms);
                    pbCollector2.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbCollector2.Refresh();
                }
                else
                {
                    pbCollector2.Image = UI.Windows.Properties.Resources.Default_Collector2;
                    pbCollector2.Refresh();
                }

                //Tab Introducer 1
                txtPerson1Name.Text = existingSupplierMaster.SupplierProperty.IntroducedPerson1Name.Trim();
                txtPerson1Address1.Text = existingSupplierMaster.SupplierProperty.IntroducedPerson1Address1.Trim();
                txtPerson1Address2.Text = existingSupplierMaster.SupplierProperty.IntroducedPerson1Address2.Trim();
                txtPerson1Address3.Text = existingSupplierMaster.SupplierProperty.IntroducedPerson1Address3.Trim();
                txtPerson1Telephone.Text = existingSupplierMaster.SupplierProperty.IntroducedPerson1TelephoneNo.Trim();

                //Tab Introducer 2
                txtPerson2Name.Text = existingSupplierMaster.SupplierProperty.IntroducedPerson2Name.Trim();
                txtPerson2Address1.Text = existingSupplierMaster.SupplierProperty.IntroducedPerson2Address1.Trim();
                txtPerson2Address2.Text = existingSupplierMaster.SupplierProperty.IntroducedPerson2Address2.Trim();
                txtPerson2Address3.Text = existingSupplierMaster.SupplierProperty.IntroducedPerson2Address3.Trim();
                txtPerson2Telephone.Text = existingSupplierMaster.SupplierProperty.IntroducedPerson2TelephoneNo.Trim();
                #endregion
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadTaxes()
        {
            try
            {
                int taxCount;

                Tax tax = new Tax();
                TaxService taxService = new TaxService();

                List<Tax> taxes = new List<Tax>();
                taxes = taxService.GetAllTaxes();

                taxCount = taxes.Count;
            
                for (int i = 0; i < taxCount; i++)
                {

                    switch (i)
                    {
                        case 0:
                            lblTax1.Text = taxes[i].TaxName.Trim();
                            break;
                        case 1:
                            lblTax2.Text = taxes[i].TaxName.Trim();
                            break;
                        case 2:
                            lblTax3.Text = taxes[i].TaxName.Trim();
                            break;
                        case 3:
                            lblTax4.Text = taxes[i].TaxName.Trim();
                            break;
                        case 4:
                            lblTax5.Text = taxes[i].TaxName.Trim();
                            break;
                    }
                }

                existingTax = taxService.GetTaxByName(lblTax1.Text.Trim());
                if (existingTax == null)
                {
                    lblTax1.Visible = false;
                    chkTax1.Visible = false;
                    txtTax1No.Visible = false;
                }

                existingTax = taxService.GetTaxByName(lblTax2.Text.Trim());
                if (existingTax == null)
                {
                    lblTax2.Visible = false;
                    chkTax2.Visible = false;
                    txtTax2No.Visible = false;
                }

                existingTax = taxService.GetTaxByName(lblTax3.Text.Trim());
                if (existingTax == null)
                {
                    lblTax3.Visible = false;
                    chkTax3.Visible = false;
                    txtTax3No.Visible = false;
                }

                existingTax = taxService.GetTaxByName(lblTax4.Text.Trim());
                if (existingTax == null)
                {
                    lblTax4.Visible = false;
                    chkTax4.Visible = false;
                    txtTax4No.Visible = false;
                }

                existingTax = taxService.GetTaxByName(lblTax5.Text.Trim());
                if (existingTax == null)
                {
                    lblTax5.Visible = false;
                    chkTax5.Visible = false;
                    txtTax5No.Visible = false;
                }

                availableTaxCount = taxCount;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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

        private bool ValidateControls()
        {
            return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtSupplierCode, txtSupplierName, cmbTitle);
        }

        private bool ValidateLedger()
        {
            return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtLedgerCode, txtLedgerName, txtOtherLedgerCode, txtOtherLedgerName);
        }

        private void ResetComponents()
        {
            #region Initialize Records

            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                SupplierGroupService supplierGroupService = new SupplierGroupService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                PaymentMethodService paymentMethodService = new PaymentMethodService();

                autoCompleteLegerCode.AddRange(accLedgerAccountService.GetLedgerCodes());
                Common.SetAutoComplete(txtLedgerCode, accLedgerAccountService.GetLedgerCodes(), chkAutoCompleationLedger.Checked);
                Common.SetAutoComplete(txtLedgerName, accLedgerAccountService.GetLedgerNames(), chkAutoCompleationLedger.Checked);

                autoCompleteOtherLegerCode.AddRange(accLedgerAccountService.GetLedgerCodes());
                Common.SetAutoComplete(txtOtherLedgerCode, accLedgerAccountService.GetLedgerCodes(), chkAutoCompleationOtherLedger.Checked);
                Common.SetAutoComplete(txtOtherLedgerName, accLedgerAccountService.GetLedgerNames(), chkAutoCompleationOtherLedger.Checked);

                Common.SetAutoBindRecords(cmbSupplierGroup, supplierGroupService.GetAllSupplierGroupNames());
                Common.SetAutoBindRecords(cmbPaymentMethod, paymentMethodService.GetAllPaymentMethodNames());

                Common.SetAutoBindRecords(cmbTitle, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.TitleType).ToString()));
                Common.SetAutoBindRecords(cmbGender, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.GenderType).ToString()));
                Common.SetAutoBindRecords(cmbSupplierType, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.SupplierType).ToString()));

                LoadTaxes();

                dtpYearOfBusinessCommencement.Value = DateTime.Now;

                ActiveControl = txtSupplierCode;
                txtSupplierCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

            #endregion
        }

        private void LoadDefaultImages()
        {
            imgVatRegCopy = UI.Windows.Properties.Resources.Default_Document;
            imgPassportPhoto = UI.Windows.Properties.Resources.Default_Document;
            imgccdCertification = UI.Windows.Properties.Resources.Default_Document;
            imgbrandCertification = UI.Windows.Properties.Resources.Default_Document;
            imgreferenceDocumentCopy = UI.Windows.Properties.Resources.Default_Document;
            imglicenceToImport = UI.Windows.Properties.Resources.Default_Document;
            imglabCertification = UI.Windows.Properties.Resources.Default_Document;
            imgbrCopy = UI.Windows.Properties.Resources.Default_Document;
            imgdistributorOwnerCopy = UI.Windows.Properties.Resources.Default_Document;
            imgqcCertification = UI.Windows.Properties.Resources.Default_Document;
            imgslsIsoCertification = UI.Windows.Properties.Resources.Default_Document;
        }
        
        #endregion

        private void txtLedgerCode_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void txtBillingTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBillingMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtBillingFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDeliveryTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDeliveryMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDeliveryFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateEmailAddress(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCollector1Telephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCollector2Telephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPerson1Telephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPerson2Telephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                {
                    return;
                }
                if (txtSupplierName.Text.Trim() != string.Empty)
                {
                    SupplierService supplierService = new SupplierService();

                    existingSupplier = supplierService.GetSupplierByName(txtSupplierName.Text.Trim());

                    if (existingSupplier != null)
                    {
                        LoadSupplier(existingSupplier);

                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                    }
                    else
                    {
                        Toast.Show("" + this.Text + " - " + txtSupplierName.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtSupplierName.Focus();
                        return;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtChequePeriod_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                else
                {
                    if (availableTaxCount.Equals(0))
                    {
                        tabSupplier.SelectedIndex = (tabSupplier.SelectedIndex + 1) % tabSupplier.TabCount;
                        ActiveControl = txtDealingPeriod;
                        txtDealingPeriod.Focus();
                    }
                    else
                    {
                        ActiveControl = chkTax1;
                        chkTax1.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbPaymentMethod_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                else
                {
                    txtCreditLimit.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCreditLimit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                else
                {
                    txtCreditPeriod.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCreditPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                else
                {
                    txtChequeLimit.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtChequeLimit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter)) { return; }
                else
                {
                    txtChequePeriod.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        

        

    }
}
