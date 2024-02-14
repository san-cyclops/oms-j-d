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

namespace UI.Windows
{
    public partial class FrmSalesPerson : UI.Windows.FrmBaseMasterForm
    {
        /// <summary>
        /// Sanjeewa
        /// </summary>

        InvSalesPerson existingInvSalesman;
        CommissionSchema existingCommisionSchema;
        string imagename;
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;
        private ReferenceType existingReferenceType;

        public FrmSalesPerson()
        {
            InitializeComponent();
        }

        public override void InitializeForm()
        {
            try
            {
                InvSalesPerson invSalesman = new InvSalesPerson();
                InvSalesPersonService invSalesmanService = new InvSalesPersonService();
                CommissionSchemaService commisionSchemaService = new CommissionSchemaService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();

                Common.SetAutoComplete(txtSalesPersonCode, invSalesmanService.GetAllInvSalesPersonCodes(), chkAutoCompleationSalesPerson.Checked);
                Common.SetAutoComplete(txtSalesPersonName, invSalesmanService.GetAllInvSalesPersonNames(), chkAutoCompleationSalesPerson.Checked);
                Common.SetAutoComplete(txtCommtionCode, commisionSchemaService.GetAllCommissionSchemas(), chkAutoCompleationCommSchema.Checked);
                Common.SetAutoComplete(txtCommtionName, commisionSchemaService.GetAllCommissionSchemaNames(), chkAutoCompleationCommSchema.Checked);

                SetAutoBindRecords(cmbGender, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.GenderType).ToString()));
                cmbType.SelectedIndex = -1;

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtSalesPersonCode);
                Common.ClearTextBox(txtSalesPersonCode, txtSalesPersonName, txtAddress1, txtAddress2, txtAddress3, txtEmail, txtMobile, txtReferenceNo, txtRemark, txtTelephone);
                pbSalesPerson.Image = UI.Windows.Properties.Resources.Default_Salesman;
                tabSalesPerson.SelectedTab = tbpGeneral;

                this.ActiveControl = txtSalesPersonCode;
                txtSalesPersonCode.Focus();
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
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                InitializeForm();
                ActiveControl = txtSalesPersonCode;
                txtSalesPersonCode.Focus();

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

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtSalesPersonCode, txtSalesPersonName, txtReferenceNo, txtMobile, txtAddress1, txtAddress2);
        }

        private bool ValidateComboBoxes() 
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbGender, cmbType);
        }

        private InvSalesPerson FillSalesman()
        {
            try
            {
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                CommissionSchemaService commisionSchemaService = new CommissionSchemaService();
                existingInvSalesman.SalesPersonCode = txtSalesPersonCode.Text.Trim();
                existingInvSalesman.SalesPersonName = txtSalesPersonName.Text.Trim();
                existingInvSalesman.SalesPersonType = cmbType.Text.Trim();
                existingInvSalesman.ReferenceNo = txtReferenceNo.Text.Trim();
                existingInvSalesman.Address1 = txtAddress1.Text.Trim();
                existingInvSalesman.Address2 = txtAddress2.Text.Trim();
                existingInvSalesman.Address3 = txtAddress3.Text.Trim();
                existingInvSalesman.Email = txtEmail.Text.Trim();
                existingInvSalesman.Telephone = txtTelephone.Text.Trim();
                existingInvSalesman.Mobile = txtMobile.Text.Trim();
                existingInvSalesman.Remark = txtRemark.Text.Trim();

                existingCommisionSchema = commisionSchemaService.GetCommissionSchemaByCode(txtCommtionCode.Text.Trim());
                if (existingCommisionSchema != null)
                {
                    existingInvSalesman.CommissionSchemaID = existingCommisionSchema.CommissionSchemaID;
                }
                else
                {
                    existingInvSalesman.CommissionSchemaID = 0;
                }

                //Read Gender Type
                existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.GenderType).ToString(), cmbGender.Text.Trim());
                if (existingReferenceType != null)
                {
                    existingInvSalesman.Gender = existingReferenceType.LookupKey;
                }
                else
                {
                    existingInvSalesman.Gender = 0;
                }
                

                MemoryStream stream = new MemoryStream();
                pbSalesPerson.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] pic = stream.ToArray();
                existingInvSalesman.Image = pic;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            return existingInvSalesman;
        }

        public override void Save()
        {
            try
            {

                if (ValidateControls() == false) return;
                if (ValidateComboBoxes() == false) return;

                InvSalesPersonService invSalesmanService = new InvSalesPersonService();
                bool isNew = false;
                existingInvSalesman = invSalesmanService.GetInvSalesPersonByCode(txtSalesPersonCode.Text.Trim());

                if ( existingInvSalesman== null || existingInvSalesman.InvSalesPersonID == 0)
                {
                    existingInvSalesman = new InvSalesPerson();
                    isNew = true;
                }

                FillSalesman();

                if (existingInvSalesman.InvSalesPersonID == 0)
                {
                    if ((Toast.Show("Salesman  - " + existingInvSalesman.SalesPersonCode + " - " + existingInvSalesman.SalesPersonName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    invSalesmanService.AddInvSalesPerson(existingInvSalesman);

                    if ((Toast.Show("Salesman  - " + existingInvSalesman.SalesPersonCode + " - " + existingInvSalesman.SalesPersonName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        if ((Toast.Show("Salesman  - " + existingInvSalesman.SalesPersonCode + " - " + existingInvSalesman.SalesPersonName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingInvSalesman.SalesPersonCode + " - " + existingInvSalesman.SalesPersonName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("Salesman  - " + existingInvSalesman.SalesPersonCode + " - " + existingInvSalesman.SalesPersonName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    invSalesmanService.UpdateInvSalesPerson(existingInvSalesman);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    Toast.Show("Salesman  - " + existingInvSalesman.SalesPersonCode + " - " + existingInvSalesman.SalesPersonName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }
                txtSalesPersonCode.Focus();

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
                if (Toast.Show("Salesman  - " + existingInvSalesman.SalesPersonCode + " - " + existingInvSalesman.SalesPersonName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                    return;

                InvSalesPersonService invSalesmanService = new InvSalesPersonService();
                existingInvSalesman = invSalesmanService.GetInvSalesPersonByCode(txtSalesPersonCode.Text.Trim());

                if (existingInvSalesman != null && existingInvSalesman.InvSalesPersonID != 0)
                {
                    existingInvSalesman.IsDelete = true;
                    invSalesmanService.UpdateInvSalesPerson(existingInvSalesman);
                    ClearForm();
                    Toast.Show("Salesman  - " + existingInvSalesman.SalesPersonCode + " - " + existingInvSalesman.SalesPersonName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    txtSalesPersonCode.Focus();
                }
                else
                    Toast.Show("Salesman  - " + existingInvSalesman.SalesPersonCode + " - " + existingInvSalesman.SalesPersonName + "", Toast.messageType.Warning, Toast.messageAction.NotExists);


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
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSalesPerson");
                InvReportGenerator invReportGenerator = new InvReportGenerator();
                invReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesPersonCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtSalesPersonName);

                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    DataView dvAllReferenceData = new DataView(invSalesPersonService.GetAllActiveSalesPersonsDataTable());
                    if (dvAllReferenceData.Count > 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtSalesPersonCode);
                        txtSalesPersonCode_Leave(this, e);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesPersonCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSalesPersonCode.Text.Trim() != string.Empty)
                {
                    InvSalesPersonService invSalesmanService = new InvSalesPersonService();

                    existingInvSalesman = invSalesmanService.GetInvSalesPersonByCode(txtSalesPersonCode.Text.Trim());

                    if (existingInvSalesman != null)
                    {
                        LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();

                        txtSalesPersonCode.Text = existingInvSalesman.SalesPersonCode;
                        txtSalesPersonName.Text = existingInvSalesman.SalesPersonName;

                        //Read Gender Type
                        existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.GenderType).ToString(), existingInvSalesman.Gender);
                        if (existingReferenceType != null)
                        {
                            cmbGender.Text = existingReferenceType.LookupValue;
                        }
                        else
                        {
                            cmbGender.SelectedIndex = -1;
                        }

                        cmbType.Text = existingInvSalesman.SalesPersonType;
                        txtReferenceNo.Text = existingInvSalesman.ReferenceNo;
                        txtAddress1.Text = existingInvSalesman.Address1;
                        txtAddress2.Text = existingInvSalesman.Address2;
                        txtAddress3.Text = existingInvSalesman.Address3;
                        txtEmail.Text = existingInvSalesman.Email;
                        txtTelephone.Text = existingInvSalesman.Telephone;
                        txtMobile.Text = existingInvSalesman.Mobile;
                        txtRemark.Text = existingInvSalesman.Remark;

                        CommissionSchema commissionSchema = new CommissionSchema();
                        CommissionSchemaService commissionSchemaService = new CommissionSchemaService();
                        commissionSchema = commissionSchemaService.GetCommissionSchemaByID(existingInvSalesman.CommissionSchemaID);

                        if (commissionSchema != null)
                        {
                            txtCommtionCode.Text = commissionSchema.CommissionSchemaCode;
                            txtCommtionName.Text = commissionSchema.CommissionSchemaName;
                        }

                        if (existingInvSalesman.Image != null)
                        {
                            MemoryStream ms = new MemoryStream((byte[])existingInvSalesman.Image);
                            pbSalesPerson.Image = Image.FromStream(ms);
                            pbSalesPerson.SizeMode = PictureBoxSizeMode.StretchImage;
                            pbSalesPerson.Refresh();
                        }
                        else
                        {
                            pbSalesPerson.Image = UI.Windows.Properties.Resources.Default_Salesman;
                            pbSalesPerson.Refresh();
                        }
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                    }
                    else
                    {
                        if (btnNew.Enabled)
                        {
                            if (Toast.Show("Salesman  - " + txtSalesPersonCode.Text.Trim() + " ", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                            {
                                btnNew.PerformClick();
                                txtSalesPersonName.Focus();
                            }
                        }
                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtSalesPersonCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex,MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Common.EnableButton(false, btnNew, btnDelete);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    InvSalesPersonService invSalesmanService = new InvSalesPersonService();
                    txtSalesPersonCode.Text = invSalesmanService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength);
                    Common.EnableTextBox(false, txtSalesPersonCode);
                    txtSalesPersonName.Focus();

                }
                else
                {
                    Common.EnableTextBox(true, txtSalesPersonCode);
                    txtSalesPersonCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesPersonName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabSalesPerson, tbpGeneral);
                Common.SetFocus(e, cmbGender);

                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    DataView dvAllReferenceData = new DataView(invSalesPersonService.GetAllActiveSalesPersonsDataTable());
                    if (dvAllReferenceData.Count > 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtSalesPersonCode);
                        txtSalesPersonCode_Leave(this, e);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

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

        private void cmbGender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtReferenceNo);
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
                Common.SetFocus(e, cmbType);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabSalesPerson, tbpContactDetails);
                Common.SetFocus(e, txtAddress1);
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
                    pbSalesPerson.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbSalesPerson.Image = (Image)newimg;
                }
                fldlg = null;
            }

            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                MessageBox.Show(ae.Message.ToString());
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tabSalesPerson, tbpFinancial);
                Common.SetFocus(e, txtCommtionCode);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtAddress2);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtAddress3);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAddress3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtEmail);
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
                Common.SetFocus(e, txtTelephone);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTelephone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtMobile);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCommtionCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtRemark);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCommtionCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCommtionCode.Text.Trim() != string.Empty)
                {
                    CommissionSchemaService commisionSchemaService = new CommissionSchemaService();
                    existingCommisionSchema = commisionSchemaService.GetCommissionSchemaByCode(txtCommtionCode.Text.Trim());
                    if (existingCommisionSchema != null)
                    {
                        txtCommtionCode.Text = existingCommisionSchema.CommissionSchemaCode;
                        txtCommtionName.Text = existingCommisionSchema.CommissionSchemaName;
                        txtRemark.Focus();
                    }
                    else
                    {
                        Toast.Show("Salesman  - " + txtSalesPersonCode.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists).Equals(DialogResult.Yes);
                        txtCommtionCode.Text = string.Empty;
                        txtCommtionCode.Focus();
                    }
                }

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
                pbSalesPerson.Image = UI.Windows.Properties.Resources.Default_Salesman;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationSalesPerson_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvSalesPersonService invSalesmanService = new InvSalesPersonService();

                Common.SetAutoComplete(txtSalesPersonCode, invSalesmanService.GetAllInvSalesPersonCodes(), chkAutoCompleationSalesPerson.Checked);
                Common.SetAutoComplete(txtSalesPersonName, invSalesmanService.GetAllInvSalesPersonNames(), chkAutoCompleationSalesPerson.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationCommSchema_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CommissionSchemaService commisionSchemaService = new CommissionSchemaService();

                Common.SetAutoComplete(txtCommtionCode, commisionSchemaService.GetAllCommissionSchemas(), chkAutoCompleationCommSchema.Checked);
                Common.SetAutoComplete(txtCommtionName, commisionSchemaService.GetAllCommissionSchemaNames(), chkAutoCompleationCommSchema.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesPersonName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                {
                    return;
                }
                if (txtSalesPersonName.Text.Trim() != string.Empty)
                {
                    InvSalesPersonService invSalesmanService = new InvSalesPersonService();

                    existingInvSalesman = invSalesmanService.GetInvSalesPersonByName(txtSalesPersonName.Text.Trim()); 

                    if (existingInvSalesman != null)
                    {
                        LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();

                        txtSalesPersonCode.Text = existingInvSalesman.SalesPersonCode;
                        txtSalesPersonName.Text = existingInvSalesman.SalesPersonName;

                        //Read Gender Type
                        existingReferenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.GenderType).ToString(), existingInvSalesman.Gender);
                        if (existingReferenceType != null)
                        {
                            cmbGender.Text = existingReferenceType.LookupValue;
                        }
                        else
                        {
                            cmbGender.SelectedIndex = -1;
                        }

                        cmbType.Text = existingInvSalesman.SalesPersonType;
                        txtReferenceNo.Text = existingInvSalesman.ReferenceNo;
                        txtAddress1.Text = existingInvSalesman.Address1;
                        txtAddress2.Text = existingInvSalesman.Address2;
                        txtAddress3.Text = existingInvSalesman.Address3;
                        txtEmail.Text = existingInvSalesman.Email;
                        txtTelephone.Text = existingInvSalesman.Telephone;
                        txtMobile.Text = existingInvSalesman.Mobile;
                        txtRemark.Text = existingInvSalesman.Remark;

                        CommissionSchema commissionSchema = new CommissionSchema();
                        CommissionSchemaService commissionSchemaService = new CommissionSchemaService();
                        commissionSchema = commissionSchemaService.GetCommissionSchemaByID(existingInvSalesman.CommissionSchemaID);

                        if (commissionSchema != null)
                        {
                            txtCommtionCode.Text = commissionSchema.CommissionSchemaCode;
                            txtCommtionName.Text = commissionSchema.CommissionSchemaName;
                        }

                        if (existingInvSalesman.Image != null)
                        {
                            MemoryStream ms = new MemoryStream((byte[])existingInvSalesman.Image);
                            pbSalesPerson.Image = Image.FromStream(ms);
                            pbSalesPerson.SizeMode = PictureBoxSizeMode.StretchImage;
                            pbSalesPerson.Refresh();
                        }
                        else
                        {
                            pbSalesPerson.Image = UI.Windows.Properties.Resources.Default_Salesman;
                            pbSalesPerson.Refresh();
                        }
                        
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                    }
                    else
                    {
                        Toast.Show("" + this.Text + " - " + txtSalesPersonName.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtSalesPersonName.Focus();
                        return;
                    }
                }
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
                Validater.ValidateEmailAddress(this, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(this, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(this, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
