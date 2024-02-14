using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Domain;
using Report;
using Report.Logistic;
using Report.Logistic.Reference.Reports;
using Service;
using Utility;
using System.IO;
using System.Reflection;

namespace UI.Windows
{
    public partial class FrmLogisticSupplier : FrmBaseMasterForm
    {
        private LgsSupplier existingLgsSupplier;
        private LgsSupplierProperty existingLgsSupplierProperty;
        private SupplierGroup existingSupplierGroup;
        private AccLedgerAccount existingLedgerAccount;
        private AccLedgerAccount existingOtherLedgerAccount;
        private ReferenceType existingReferenceType;
        private Tax existingTax;

        AutoCompleteStringCollection autoCompleteSupplierCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteLegerCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteOtherLegerCode = new AutoCompleteStringCollection();
        string imagename;
        private int availableTaxCount;

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

        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        public FrmLogisticSupplier()
        {
            InitializeComponent();
        }

        private void FrmLogisticSupplier_Load(object sender, EventArgs e)
        {

        }

        #region Overrride Methods....

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

        public override void InitializeForm()
        {
            try
            {
                LgsSupplierService lgsSupplierService = new LgsSupplierService();

                autoCompleteSupplierCode.AddRange(lgsSupplierService.GetAllLgsSupplierCodes());
                Common.SetAutoComplete(txtSupplierCode, lgsSupplierService.GetAllLgsSupplierCodes(), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, lgsSupplierService.GetAllLgsSupplierNames(), chkAutoCompleationSupplier.Checked);

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtSupplierCode);
                Common.EnableTextBox(false, txtTax1No, txtTax2No, txtTax3No, txtTax4No, txtTax5No);
                Common.ClearTextBox(txtSupplierCode);

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

                ActiveControl = txtSupplierCode;
                txtSupplierCode.Focus();
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
                if (!ValidateControls()) { return; }

                if (Common.EntryLevel.Equals(1))
                {
                    if (!ValidateLedger()) { return; }
                }

                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                LgsSupplierPropertyService lgsSupplierPropertyService = new LgsSupplierPropertyService();

                bool isNew = false;
                existingLgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());

                if (existingLgsSupplier == null || existingLgsSupplier.LgsSupplierID == 0)
                {
                    existingLgsSupplier = new LgsSupplier();
                    existingLgsSupplierProperty = new LgsSupplierProperty();
                    isNew = true;
                }
                else
                {
                    existingLgsSupplierProperty = existingLgsSupplier.LgsSupplierProperty;
                    //supplierPropertyService.GetSupplierPropertyByID(existingSupplier.SupplierID);
                }

                FillSupplier();

                if (existingLgsSupplier.LgsSupplierID == 0)
                {
                    if ((Toast.Show(this.Text + " - " + existingLgsSupplier.SupplierCode + " - " + existingLgsSupplier.SupplierName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }

                    lgsSupplierService.AddSupplier(existingLgsSupplier);

                    if ((Toast.Show(this.Text + " - " + existingLgsSupplier.SupplierCode + " - " + existingLgsSupplier.SupplierName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        if ((Toast.Show(this.Text + " - " + existingLgsSupplier.SupplierCode + " - " + existingLgsSupplier.SupplierName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingLgsSupplier.SupplierCode + " - " + existingLgsSupplier.SupplierName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show(this.Text + " - " + existingLgsSupplier.SupplierCode + " - " + existingLgsSupplier.SupplierName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    lgsSupplierService.UpdateSupplier(existingLgsSupplier);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    Toast.Show(this.Text + " - " + existingLgsSupplier.SupplierCode + " - " + existingLgsSupplier.SupplierName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }
                txtSupplierCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void Delete()
        {
            try
            {
                if (Toast.Show(this.Text + " - " + existingLgsSupplier.SupplierCode + " - " + existingLgsSupplier.SupplierName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                { return; }

                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                LgsSupplierPropertyService lgsSupplierPropertyService = new LgsSupplierPropertyService();

                existingLgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());

                if (existingLgsSupplier != null && existingLgsSupplier.LgsSupplierID != 0)
                {
                    existingLgsSupplierProperty = existingLgsSupplier.LgsSupplierProperty; //supplierPropertyService.GetSupplierPropertyByID(existingSupplier.SupplierID);

                    existingLgsSupplier.IsDelete = true;
                    existingLgsSupplierProperty.IsDelete = true;
                    existingLgsSupplier.LgsSupplierProperty = existingLgsSupplierProperty;
                    lgsSupplierService.UpdateSupplier(existingLgsSupplier);
                    ClearForm();
                    Toast.Show(this.Text + " - " + existingLgsSupplier.SupplierCode + " - " + existingLgsSupplier.SupplierName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    txtSupplierCode.Focus();
                }
                else
                {
                    Toast.Show(this.Text + " - " + existingLgsSupplier.SupplierCode + " - " + existingLgsSupplier.SupplierName + "", Toast.messageType.Warning, Toast.messageAction.NotExists);
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
                //string[] stringfield = { "Code", "Sup.Name", "Address", "Telephone", "NIC", "Rec.Date", "Payment Method", "Contact Person", "Remark" };
                //string[] decimalfield = { };
                //MdiService mdiService = new MdiService();
                //string ReprotName = "Supplier";
                //CryLgsReferenceDetailTemplate cryLgsReferenceDetailTemplate = new CryLgsReferenceDetailTemplate();
                //int x;

                //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName, mdiService.GetAllLgsSupplierDataTable(), 0, cryLgsTemplate);
                //frmReprotGenerator.ShowDialog();

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSupplier");
                LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
                lgsReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region Button Click Events....

        /// <summary>
        /// Get new logistic supplier code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Common.EnableButton(false, btnNew, btnDelete);
                Common.EnableButton(true, btnSave);
                
                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    txtSupplierCode.Text = lgsSupplierService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength);
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

        /// <summary>
        /// Browse Image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = @":D\";
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
                Logger.WriteLog(ae.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnClearImage_Click(object sender, EventArgs e)
        {
            try
            {
                pbSupplier.Image = UI.Windows.Properties.Resources.Default_Supplier;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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
                Logger.WriteLog(ae.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                Logger.WriteLog(ex, "Click_Collector2Browse");
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

        #endregion

        #region key down and Leave events....

        private void txtSupplierCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    DataView dvAllReferenceData = new DataView(lgsSupplierService.GetAllActiveLgsSuppliersDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), this.ActiveControl);
                        txtSupplierCode_Leave(this, e);
                    }
                }

                Common.SetFocus(e, cmbTitle);
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
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();

                    existingLgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());

                    if (existingLgsSupplier != null)
                    {
                        LoadSupplier(existingLgsSupplier);

                        Common.EnableButton(true, btnSave, btnDelete);
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
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    DataView dvAllReferenceData = new DataView(lgsSupplierService.GetAllActiveLgsSuppliersDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), this.ActiveControl);
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

        #endregion

        #region Methods....

        private void ResetComponents()
        {
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

                Common.SetAutoBindRecords(cmbSupplierType, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.LogisticSupplierType).ToString()));

                LoadTaxes();

                ActiveControl = txtSupplierCode;
                txtSupplierCode.Focus();
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
            return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtSupplierCode, txtSupplierName, txtBillingTelephone, cmbTitle, cmbSupplierGroup, cmbSupplierType);
        }

        private bool ValidateLedger()
        {
            return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtLedgerCode, txtLedgerName, txtOtherLedgerCode, txtOtherLedgerName);
        }

        private void LoadSupplier(LgsSupplier existingLgsSupplierMaster)
        {
            try
            {
                #region Supplier
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                AccLedgerAccount ledgerAccount = new AccLedgerAccount();
                AccLedgerAccount otherLedgerAccount = new AccLedgerAccount();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                PaymentMethod paymentMethod = new PaymentMethod();
                PaymentMethodService paymentMethodService = new PaymentMethodService();

                txtSupplierCode.Text = existingLgsSupplierMaster.SupplierCode.Trim();
                txtSupplierName.Text = existingLgsSupplierMaster.SupplierName.Trim();
                txtRemark.Text = existingLgsSupplierMaster.Remark.Trim();
                txtReferenceNo.Text = existingLgsSupplierMaster.ReferenceNo.Trim();
                txtRepresentative.Text = existingLgsSupplierMaster.RepresentativeName.Trim();
                txtRepresentativeNicNo.Text = existingLgsSupplierMaster.RepresentativeNICNo.Trim();
                txtBillingAddress1.Text = existingLgsSupplierMaster.BillingAddress1.Trim();
                txtBillingAddress2.Text = existingLgsSupplierMaster.BillingAddress2.Trim();
                txtBillingAddress3.Text = existingLgsSupplierMaster.BillingAddress3.Trim();
                txtBillingTelephone.Text = existingLgsSupplierMaster.BillingTelephone.Trim();
                txtBillingMobile.Text = existingLgsSupplierMaster.BillingMobile.Trim();
                txtBillingFax.Text = existingLgsSupplierMaster.BillingFax.Trim();
                txtEmail.Text = existingLgsSupplierMaster.Email.Trim();
                txtContactPerson.Text = existingLgsSupplierMaster.ContactPersonName.Trim();
                txtDeliveryAddress1.Text = existingLgsSupplierMaster.DeliveryAddress1.Trim();
                txtDeliveryAddress2.Text = existingLgsSupplierMaster.DeliveryAddress2.Trim();
                txtDeliveryAddress3.Text = existingLgsSupplierMaster.DeliveryAddress3.Trim();
                txtDeliveryTelephone.Text = existingLgsSupplierMaster.DeliveryTelephone.Trim();
                txtDeliveryMobile.Text = existingLgsSupplierMaster.DeliveryMobile.Trim();
                txtDeliveryFax.Text = existingLgsSupplierMaster.DeliveryFax.Trim();
                txtReferenceSerialNo.Text = existingLgsSupplierMaster.ReferenceSerial.Trim();
                txtOrderCircle.Text = existingLgsSupplierMaster.OrderCircle.ToString();

                ledgerAccount = accLedgerAccountService.GetAccLedgerAccountByID(existingLgsSupplierMaster.LedgerID);
                if (ledgerAccount != null)
                {
                    txtLedgerCode.Text = ledgerAccount.LedgerCode.Trim();
                    txtLedgerName.Text = ledgerAccount.LedgerName.Trim();
                }

                otherLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByID(existingLgsSupplierMaster.OtherLedgerID);
                if (ledgerAccount != null)
                {
                    txtOtherLedgerCode.Text = otherLedgerAccount.LedgerCode.Trim();
                    txtOtherLedgerName.Text = otherLedgerAccount.LedgerName.Trim();
                }

                cmbSupplierGroup.Text = existingLgsSupplierMaster.SupplierGroup.SupplierGroupName;
                txtReferenceSerialNo.Text = existingLgsSupplierMaster.ReferenceSerial.Trim();

                txtTax1No.Text = existingLgsSupplierMaster.TaxNo1.Trim();
                if (string.IsNullOrEmpty(txtTax1No.Text.Trim())) { chkTax1.Checked = false; } else { chkTax1.Checked = true; }

                txtTax2No.Text = existingLgsSupplierMaster.TaxNo2.Trim();
                if (string.IsNullOrEmpty(txtTax2No.Text.Trim())) { chkTax2.Checked = false; } else { chkTax2.Checked = true; }

                txtTax3No.Text = existingLgsSupplierMaster.TaxNo3.Trim();
                if (string.IsNullOrEmpty(txtTax3No.Text.Trim())) { chkTax3.Checked = false; } else { chkTax3.Checked = true; }

                txtTax4No.Text = existingLgsSupplierMaster.TaxNo4.Trim();
                if (string.IsNullOrEmpty(txtTax4No.Text.Trim())) { chkTax4.Checked = false; } else { chkTax4.Checked = true; }

                txtTax5No.Text = existingLgsSupplierMaster.TaxNo5.Trim();
                if (string.IsNullOrEmpty(txtTax5No.Text.Trim())) { chkTax5.Checked = false; } else { chkTax5.Checked = true; }

                paymentMethod = paymentMethodService.GetPaymentMethodsByID(existingLgsSupplierMaster.PaymentMethod);
                if (paymentMethod != null) { cmbPaymentMethod.Text = paymentMethod.PaymentMethodName; }
                else { cmbPaymentMethod.Text = string.Empty; }

                txtCreditLimit.Text = Common.ConvertDecimalToStringCurrency(existingLgsSupplierMaster.CreditLimit);
                txtCreditPeriod.Text = existingLgsSupplierMaster.CreditPeriod.ToString();
                txtChequeLimit.Text = Common.ConvertDecimalToStringCurrency(existingLgsSupplierMaster.ChequeLimit);
                txtChequePeriod.Text = existingLgsSupplierMaster.ChequePeriod.ToString();

                //Read Supplier Title
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.TitleType).ToString(), existingLgsSupplierMaster.SupplierTitle);
                if (existingReferenceType != null)
                {
                    cmbTitle.Text = existingReferenceType.LookupValue;
                }
                else
                {
                    cmbTitle.SelectedIndex = -1;
                }

                //Read Gender Type
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.GenderType).ToString(), existingLgsSupplierMaster.Gender);
                if (existingReferenceType != null)
                {
                    cmbGender.Text = existingReferenceType.LookupValue;
                }
                else
                {
                    cmbGender.SelectedIndex = -1;
                }

                //Read Supplier Type
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.LogisticSupplierType).ToString(), existingLgsSupplierMaster.SupplierType);
                if (existingReferenceType != null)
                {
                    cmbSupplierType.Text = existingReferenceType.LookupValue;
                }
                else
                {
                    cmbSupplierType.SelectedIndex = -1;
                }

                if (existingLgsSupplierMaster.SupplierImage != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingLgsSupplierMaster.SupplierImage);
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
                txtDealingPeriod.Text = existingLgsSupplierMaster.LgsSupplierProperty.DealingPeriod.ToString().Trim();

                //change with suitable code
                dtpYearOfBusinessCommencement.Text = new DateTime(existingLgsSupplierMaster.LgsSupplierProperty.YearOfBusinessCommencement, dtpYearOfBusinessCommencement.Value.Month, dtpYearOfBusinessCommencement.Value.Day).ToString();
                txtNoOfEmployees.Text = existingLgsSupplierMaster.LgsSupplierProperty.NoOfEmployees.ToString().Trim();
                txtNoOfMachines.Text = existingLgsSupplierMaster.LgsSupplierProperty.NoOfMachines.ToString().Trim();
                txtPercentageOfWht.Text = existingLgsSupplierMaster.LgsSupplierProperty.PercentageOfWHT.ToString().Trim();
                txtIncomeTaxFileNo.Text = existingLgsSupplierMaster.LgsSupplierProperty.IncomeTaxFileNo.Trim();
                txtPurchasingDepartmentRemark.Text = existingLgsSupplierMaster.LgsSupplierProperty.RemarkByPurchasingDepartment.Trim();
                txtAccountsDepartmentRemarks.Text = existingLgsSupplierMaster.LgsSupplierProperty.RemarkByAccountDepartment.Trim();

                chkVatRegistrationCopy.Checked = existingLgsSupplierMaster.LgsSupplierProperty.IsVatRegistrationCopy;
                if (existingLgsSupplierMaster.LgsSupplierProperty.VatRegistrationCopy != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingLgsSupplierMaster.LgsSupplierProperty.VatRegistrationCopy);
                    imgVatRegCopy = Image.FromStream(ms);
                }

                chkPassportPhotos.Checked = existingLgsSupplierMaster.LgsSupplierProperty.IsPassportPhotos;
                if (existingLgsSupplierMaster.LgsSupplierProperty.PassportPhotos != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingLgsSupplierMaster.LgsSupplierProperty.PassportPhotos);
                    imgPassportPhoto = Image.FromStream(ms);
                }

                chkCddCertification.Checked = existingLgsSupplierMaster.LgsSupplierProperty.IsCDDCertification;
                if (existingLgsSupplierMaster.LgsSupplierProperty.CDDCertificate != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingLgsSupplierMaster.LgsSupplierProperty.CDDCertificate);
                    imgccdCertification = Image.FromStream(ms);
                }

                chkBrandCertification.Checked = existingLgsSupplierMaster.LgsSupplierProperty.IsBrandCertification;
                if (existingLgsSupplierMaster.LgsSupplierProperty.BrandCertificate != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingLgsSupplierMaster.LgsSupplierProperty.BrandCertificate);
                    imgbrandCertification = Image.FromStream(ms);
                }

                chkReferenceDocumentCopy.Checked = existingLgsSupplierMaster.LgsSupplierProperty.IsRefeneceDocumentCopy;
                if (existingLgsSupplierMaster.LgsSupplierProperty.RefeneceDocumentCopy != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingLgsSupplierMaster.LgsSupplierProperty.RefeneceDocumentCopy);
                    imgreferenceDocumentCopy = Image.FromStream(ms);
                }

                chkLicenseToImport.Checked = existingLgsSupplierMaster.LgsSupplierProperty.IsLicenceToImport;
                if (existingLgsSupplierMaster.LgsSupplierProperty.LicenceToImport != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingLgsSupplierMaster.LgsSupplierProperty.LicenceToImport);
                    imglicenceToImport = Image.FromStream(ms);
                }

                chkLabCertification.Checked = existingLgsSupplierMaster.LgsSupplierProperty.IsLabCertification;
                if (existingLgsSupplierMaster.LgsSupplierProperty.LabCertificate != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingLgsSupplierMaster.LgsSupplierProperty.LabCertificate);
                    imglabCertification = Image.FromStream(ms);
                }

                chkBrCopy.Checked = existingLgsSupplierMaster.LgsSupplierProperty.IsBRCCopy;
                if (existingLgsSupplierMaster.LgsSupplierProperty.BRCCopy != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingLgsSupplierMaster.LgsSupplierProperty.BRCCopy);
                    imgbrCopy = Image.FromStream(ms);
                }

                chkDistributorOwnerCopy.Checked = existingLgsSupplierMaster.LgsSupplierProperty.IsDistributorOwnerCopy;
                if (existingLgsSupplierMaster.LgsSupplierProperty.DistributorOwnerCopy != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingLgsSupplierMaster.LgsSupplierProperty.DistributorOwnerCopy);
                    imgdistributorOwnerCopy = Image.FromStream(ms);
                }

                chkQcCertification.Checked = existingLgsSupplierMaster.LgsSupplierProperty.IsQCCertification;
                if (existingLgsSupplierMaster.LgsSupplierProperty.QCCertificate != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingLgsSupplierMaster.LgsSupplierProperty.QCCertificate);
                    imgqcCertification = Image.FromStream(ms);
                }

                chkSLSCertification.Checked = existingLgsSupplierMaster.LgsSupplierProperty.IsSLSISOCertiification;
                if (existingLgsSupplierMaster.LgsSupplierProperty.SLSISOCertiificate != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingLgsSupplierMaster.LgsSupplierProperty.SLSISOCertiificate);
                    imgslsIsoCertification = Image.FromStream(ms);
                }

                //Tab Collector 1
                txtCollector1Name.Text = existingLgsSupplierMaster.LgsSupplierProperty.PaymentCollector1Name.Trim();
                txtCollector1Designation.Text = existingLgsSupplierMaster.LgsSupplierProperty.PaymentCollector1Designation.Trim();
                txtCollector1ReferenceNo.Text = existingLgsSupplierMaster.LgsSupplierProperty.PaymentCollector1ReferenceNo.Trim();
                txtCollector1Telephone.Text = existingLgsSupplierMaster.LgsSupplierProperty.PaymentCollector1TelephoneNo.Trim();

                if (existingLgsSupplierMaster.LgsSupplierProperty.PaymentCollector1Image != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingLgsSupplierMaster.LgsSupplierProperty.PaymentCollector1Image);
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
                txtCollector2Name.Text = existingLgsSupplierMaster.LgsSupplierProperty.PaymentCollector2Name.Trim();
                txtCollector2Designation.Text = existingLgsSupplierMaster.LgsSupplierProperty.PaymentCollector2Designation.Trim();
                txtCollector2ReferenceNo.Text = existingLgsSupplierMaster.LgsSupplierProperty.PaymentCollector2ReferenceNo.Trim();
                txtCollector2Telephone.Text = existingLgsSupplierMaster.LgsSupplierProperty.PaymentCollector2TelephoneNo.Trim();

                if (existingLgsSupplierMaster.LgsSupplierProperty.PaymentCollector2Image != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])existingLgsSupplierMaster.LgsSupplierProperty.PaymentCollector2Image);
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
                txtPerson1Name.Text = existingLgsSupplierMaster.LgsSupplierProperty.IntroducedPerson1Name.Trim();
                txtPerson1Address1.Text = existingLgsSupplierMaster.LgsSupplierProperty.IntroducedPerson1Address1.Trim();
                txtPerson1Address2.Text = existingLgsSupplierMaster.LgsSupplierProperty.IntroducedPerson1Address2.Trim();
                txtPerson1Address3.Text = existingLgsSupplierMaster.LgsSupplierProperty.IntroducedPerson1Address3.Trim();
                txtPerson1Telephone.Text = existingLgsSupplierMaster.LgsSupplierProperty.IntroducedPerson1TelephoneNo.Trim();

                //Tab Introducer 2
                txtPerson2Name.Text = existingLgsSupplierMaster.LgsSupplierProperty.IntroducedPerson2Name.Trim();
                txtPerson2Address1.Text = existingLgsSupplierMaster.LgsSupplierProperty.IntroducedPerson2Address1.Trim();
                txtPerson2Address2.Text = existingLgsSupplierMaster.LgsSupplierProperty.IntroducedPerson2Address2.Trim();
                txtPerson2Address3.Text = existingLgsSupplierMaster.LgsSupplierProperty.IntroducedPerson2Address3.Trim();
                txtPerson2Telephone.Text = existingLgsSupplierMaster.LgsSupplierProperty.IntroducedPerson2TelephoneNo.Trim();
                #endregion
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
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                Common.SetAutoComplete(txtSupplierCode, lgsSupplierService.GetAllLgsSupplierCodes(), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, lgsSupplierService.GetAllLgsSupplierNames(), chkAutoCompleationSupplier.Checked);
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
                else { txtTax1No.Enabled = false; txtTax1No.Text = string.Empty; }
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

        private LgsSupplier FillSupplier()
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

                existingLgsSupplier.SupplierCode = txtSupplierCode.Text.Trim();
                existingLgsSupplier.SupplierName = txtSupplierName.Text.Trim();
                existingLgsSupplier.Remark = txtRemark.Text.Trim();
                existingLgsSupplier.ReferenceNo = txtReferenceNo.Text.Trim();
                existingLgsSupplier.RepresentativeName = txtRepresentative.Text.Trim();
                existingLgsSupplier.RepresentativeNICNo = txtRepresentativeNicNo.Text.Trim();
                existingLgsSupplier.BillingAddress1 = txtBillingAddress1.Text.Trim();
                existingLgsSupplier.BillingAddress2 = txtBillingAddress2.Text.Trim();
                existingLgsSupplier.BillingAddress3 = txtBillingAddress3.Text.Trim();
                existingLgsSupplier.PostalCode = "";
                existingLgsSupplier.BillingTelephone = txtBillingTelephone.Text.Trim();
                existingLgsSupplier.BillingMobile = txtBillingMobile.Text.Trim();
                existingLgsSupplier.BillingFax = txtBillingFax.Text.Trim();
                existingLgsSupplier.Email = txtEmail.Text.Trim();
                existingLgsSupplier.ContactPersonName = txtContactPerson.Text.Trim();
                existingLgsSupplier.DeliveryAddress1 = txtDeliveryAddress1.Text.Trim();
                existingLgsSupplier.DeliveryAddress2 = txtDeliveryAddress2.Text.Trim();
                existingLgsSupplier.DeliveryAddress3 = txtDeliveryAddress3.Text.Trim();
                existingLgsSupplier.DeliveryTelephone = txtDeliveryTelephone.Text.Trim();
                existingLgsSupplier.DeliveryMobile = txtDeliveryMobile.Text.Trim();
                existingLgsSupplier.DeliveryFax = txtDeliveryFax.Text.Trim();
                existingLgsSupplier.ReferenceSerial = txtReferenceSerialNo.Text.Trim();
                existingLgsSupplier.OrderCircle = Common.ConvertStringToInt(txtOrderCircle.Text.Trim());

                existingTax = taxService.GetTaxByName(lblTax1.Text.Trim());
                if (chkTax1.Checked) { existingLgsSupplier.TaxID1 = existingTax.TaxID; }
                else { existingLgsSupplier.TaxID1 = 0; }

                existingTax = taxService.GetTaxByName(lblTax2.Text.Trim());
                if (chkTax2.Checked) { existingLgsSupplier.TaxID2 = existingTax.TaxID; }
                else { existingLgsSupplier.TaxID2 = 0; }

                existingTax = taxService.GetTaxByName(lblTax3.Text.Trim());
                if (chkTax3.Checked) { existingLgsSupplier.TaxID3 = existingTax.TaxID; }
                else { existingLgsSupplier.TaxID3 = 0; }

                existingTax = taxService.GetTaxByName(lblTax4.Text.Trim());
                if (chkTax4.Checked) { existingLgsSupplier.TaxID4 = existingTax.TaxID; }
                else { existingLgsSupplier.TaxID4 = 0; }

                existingTax = taxService.GetTaxByName(lblTax5.Text.Trim());
                if (chkTax5.Checked) { existingLgsSupplier.TaxID5 = existingTax.TaxID; }
                else { existingLgsSupplier.TaxID5 = 0; }

                existingLgsSupplier.TaxNo1 = txtTax1No.Text.Trim();
                existingLgsSupplier.TaxNo2 = txtTax2No.Text.Trim();
                existingLgsSupplier.TaxNo3 = txtTax3No.Text.Trim();
                existingLgsSupplier.TaxNo4 = txtTax4No.Text.Trim();
                existingLgsSupplier.TaxNo5 = txtTax5No.Text.Trim();

                existingLgsSupplier.CreditLimit = Common.ConvertStringToDecimalCurrency(txtCreditLimit.Text.Trim());
                existingLgsSupplier.CreditPeriod = Common.ConvertStringToInt(txtCreditPeriod.Text.Trim());
                existingLgsSupplier.ChequeLimit = Common.ConvertStringToDecimalCurrency(txtChequeLimit.Text.Trim());
                existingLgsSupplier.ChequePeriod = Common.ConvertStringToInt(txtChequePeriod.Text.Trim());

                paymentMethod = paymentMethodService.GetPaymentMethodsByName(cmbPaymentMethod.Text.Trim());
                if (paymentMethod != null) { existingLgsSupplier.PaymentMethod = paymentMethod.PaymentMethodID; }
                else existingLgsSupplier.PaymentMethod = 0;

                //Read Supplier Title
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.TitleType).ToString(), cmbTitle.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingLgsSupplier.SupplierTitle = existingReferenceType.LookupKey;
                }
                else
                {
                    existingLgsSupplier.SupplierTitle = 0;
                }

                //Read Gender Type
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.GenderType).ToString(), cmbGender.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingLgsSupplier.Gender = existingReferenceType.LookupKey;
                }
                else
                {
                    existingLgsSupplier.Gender = 0;
                }

                //Read Supplier Type
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.LogisticSupplierType).ToString(), cmbSupplierType.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingLgsSupplier.SupplierType = existingReferenceType.LookupKey;
                }
                else
                {
                    existingLgsSupplier.SupplierType = 0;
                }

                //Read Supplier Group ID
                existingSupplierGroup = supplierGroupService.GetSupplierGroupsByName(cmbSupplierGroup.Text.Trim());
                if (existingSupplierGroup != null)
                {
                    existingLgsSupplier.SupplierGroupID = existingSupplierGroup.SupplierGroupID;
                }
                else
                {
                    existingLgsSupplier.SupplierGroupID = 0;
                }

                //Read Ledger ID
                existingLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtLedgerCode.Text.Trim());
                if (existingLedgerAccount != null)
                {
                    existingLgsSupplier.LedgerID = existingLedgerAccount.AccLedgerAccountID;
                }
                else
                {
                    existingLgsSupplier.LedgerID = 0;
                }

                //Read Other Ledegr ID
                existingOtherLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(txtOtherLedgerCode.Text.Trim());
                if (existingOtherLedgerAccount != null)
                {
                    existingLgsSupplier.OtherLedgerID = existingOtherLedgerAccount.AccLedgerAccountID;
                }
                else
                {
                    existingLgsSupplier.OtherLedgerID = 0;
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
                existingLgsSupplier.SupplierImage = pic;
                #endregion

                #region Supplier Property

                existingLgsSupplierProperty.LgsSupplierID = existingLgsSupplier.LgsSupplierID;
                existingLgsSupplierProperty.SupplierCode = existingLgsSupplier.SupplierCode.Trim();

                //Company Details
                existingLgsSupplierProperty.DealingPeriod = Common.ConvertStringToLong(txtDealingPeriod.Text.ToString());
                existingLgsSupplierProperty.YearOfBusinessCommencement = dtpYearOfBusinessCommencement.Value.Year;
                existingLgsSupplierProperty.NoOfEmployees = Common.ConvertStringToInt(txtNoOfEmployees.Text.ToString().Trim());
                existingLgsSupplierProperty.NoOfMachines = Common.ConvertStringToInt(txtNoOfMachines.Text.ToString().Trim());
                existingLgsSupplierProperty.PercentageOfWHT = Common.ConvertStringToInt(txtPercentageOfWht.Text.ToString().Trim());
                existingLgsSupplierProperty.IncomeTaxFileNo = txtIncomeTaxFileNo.Text.Trim();
                existingLgsSupplierProperty.RemarkByPurchasingDepartment = txtPurchasingDepartmentRemark.Text.Trim();
                existingLgsSupplierProperty.RemarkByAccountDepartment = txtAccountsDepartmentRemarks.Text.Trim();

                ImageConverter imageConverter = new ImageConverter();

                existingLgsSupplierProperty.IsVatRegistrationCopy = chkVatRegistrationCopy.Checked;
                byte[] vatRegCopy = (byte[])imageConverter.ConvertTo(imgVatRegCopy, typeof(byte[]));
                existingLgsSupplierProperty.VatRegistrationCopy = vatRegCopy;

                existingLgsSupplierProperty.IsPassportPhotos = chkPassportPhotos.Checked;
                byte[] passportPhoto = (byte[])imageConverter.ConvertTo(imgPassportPhoto, typeof(byte[]));
                existingLgsSupplierProperty.PassportPhotos = passportPhoto;

                existingLgsSupplierProperty.IsCDDCertification = chkCddCertification.Checked;
                byte[] ccdCerticate = (byte[])imageConverter.ConvertTo(imgccdCertification, typeof(byte[]));
                existingLgsSupplierProperty.CDDCertificate = ccdCerticate;

                existingLgsSupplierProperty.IsBrandCertification = chkBrandCertification.Checked;
                byte[] brandCerticate = (byte[])imageConverter.ConvertTo(imgbrandCertification, typeof(byte[]));
                existingLgsSupplierProperty.BrandCertificate = brandCerticate;

                existingLgsSupplierProperty.IsRefeneceDocumentCopy = chkReferenceDocumentCopy.Checked;
                byte[] refDoc = (byte[])imageConverter.ConvertTo(imgreferenceDocumentCopy, typeof(byte[]));
                existingLgsSupplierProperty.RefeneceDocumentCopy = refDoc;

                existingLgsSupplierProperty.IsLicenceToImport = chkLicenseToImport.Checked;
                byte[] licence = (byte[])imageConverter.ConvertTo(imglicenceToImport, typeof(byte[]));
                existingLgsSupplierProperty.LicenceToImport = licence;

                existingLgsSupplierProperty.IsLabCertification = chkLabCertification.Checked;
                byte[] labCertificate = (byte[])imageConverter.ConvertTo(imglabCertification, typeof(byte[]));
                existingLgsSupplierProperty.LabCertificate = labCertificate;

                existingLgsSupplierProperty.IsBRCCopy = chkBrCopy.Checked;
                byte[] brCopy = (byte[])imageConverter.ConvertTo(imgbrCopy, typeof(byte[]));
                existingLgsSupplierProperty.BRCCopy = brCopy;

                existingLgsSupplierProperty.IsDistributorOwnerCopy = chkDistributorOwnerCopy.Checked;
                byte[] distributerOwner = (byte[])imageConverter.ConvertTo(imgdistributorOwnerCopy, typeof(byte[]));
                existingLgsSupplierProperty.DistributorOwnerCopy = distributerOwner;

                existingLgsSupplierProperty.IsQCCertification = chkQcCertification.Checked;
                byte[] qcCertificate = (byte[])imageConverter.ConvertTo(imgqcCertification, typeof(byte[]));
                existingLgsSupplierProperty.QCCertificate = qcCertificate;

                existingLgsSupplierProperty.IsSLSISOCertiification = chkSLSCertification.Checked;
                byte[] slsIso = (byte[])imageConverter.ConvertTo(imgslsIsoCertification, typeof(byte[]));
                existingLgsSupplierProperty.SLSISOCertiificate = slsIso;

                //Collector 1
                existingLgsSupplierProperty.PaymentCollector1Name = txtCollector1Name.Text.Trim();
                existingLgsSupplierProperty.PaymentCollector1Designation = txtCollector1Designation.Text.Trim();
                existingLgsSupplierProperty.PaymentCollector1ReferenceNo = txtCollector1ReferenceNo.Text.Trim();
                existingLgsSupplierProperty.PaymentCollector1TelephoneNo = txtCollector1Telephone.Text.Trim();

                stream = new MemoryStream();
                pbCollector1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                pic = stream.ToArray();
                existingLgsSupplierProperty.PaymentCollector1Image = pic;

                //Collector 2
                existingLgsSupplierProperty.PaymentCollector2Name = txtCollector2Name.Text.Trim();
                existingLgsSupplierProperty.PaymentCollector2Designation = txtCollector2Designation.Text.Trim();
                existingLgsSupplierProperty.PaymentCollector2ReferenceNo = txtCollector2ReferenceNo.Text.Trim();
                existingLgsSupplierProperty.PaymentCollector2TelephoneNo = txtCollector2Telephone.Text.Trim();

                stream = new MemoryStream();
                pbCollector2.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                pic = stream.ToArray();
                existingLgsSupplierProperty.PaymentCollector2Image = pic;

                //Introducer 1
                existingLgsSupplierProperty.IntroducedPerson1Name = txtPerson1Name.Text.Trim();
                existingLgsSupplierProperty.IntroducedPerson1Address1 = txtPerson1Address1.Text.Trim();
                existingLgsSupplierProperty.IntroducedPerson1Address2 = txtPerson1Address2.Text.Trim();
                existingLgsSupplierProperty.IntroducedPerson1Address3 = txtPerson1Address3.Text.Trim();
                existingLgsSupplierProperty.IntroducedPerson1TelephoneNo = txtPerson1Telephone.Text.Trim();

                //Introducer 2
                existingLgsSupplierProperty.IntroducedPerson2Name = txtPerson2Name.Text.Trim();
                existingLgsSupplierProperty.IntroducedPerson2Address1 = txtPerson2Address1.Text.Trim();
                existingLgsSupplierProperty.IntroducedPerson2Address2 = txtPerson2Address2.Text.Trim();
                existingLgsSupplierProperty.IntroducedPerson2Address3 = txtPerson2Address3.Text.Trim();
                existingLgsSupplierProperty.IntroducedPerson2TelephoneNo = txtPerson2Telephone.Text.Trim();
                #endregion

                existingLgsSupplier.LgsSupplierProperty = existingLgsSupplierProperty;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            return existingLgsSupplier;
        }

        #endregion

        private void txtOtherLedgerCode_Layout(object sender, LayoutEventArgs e)
        {

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

        private void pbDocument_DoubleClick(object sender, EventArgs e)
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
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();

                    existingLgsSupplier = lgsSupplierService.GetLgsSupplierByName(txtSupplierName.Text.Trim());

                    if (existingLgsSupplier != null)
                    {
                        LoadSupplier(existingLgsSupplier);

                        Common.EnableButton(true, btnSave, btnDelete);
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
