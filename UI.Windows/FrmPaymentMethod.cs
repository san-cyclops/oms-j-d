using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Domain;
using Utility;
using Service;
using System.Reflection;
using Report.Com;

namespace UI.Windows
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public partial class FrmPaymentMethod : UI.Windows.FrmBaseMasterForm
    {
        private PaymentMethod existingPaymentMethod;
        AutoCompleteStringCollection autoCompletePaymentMethodCode;
        AutoCompleteStringCollection autoCompletePaymentMethodName;
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        public FrmPaymentMethod()
        {
            InitializeComponent();
        }

        private void FrmPaymentMethod_Load(object sender, EventArgs e)
        {
            
        }

        #region Button Click Events....

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Common.EnableButton(false, btnNew, btnDelete);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                
                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    PaymentMethodService PaymentMethodService = new PaymentMethodService();
                    txtPaymentMethodCode.Text = PaymentMethodService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength);
                    Common.EnableTextBox(false, txtPaymentMethodCode);
                    txtPaymentMethodName.Focus();
                }
                else
                {
                    Common.EnableTextBox(true, txtPaymentMethodCode);
                    txtPaymentMethodCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region Override Methods....

        public override void FormLoad()
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                chkAutoCompleationPaymentMethodCode.Checked = true;

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
                PaymentMethod paymentMethod = new PaymentMethod();
                PaymentMethodService paymentMethodService = new PaymentMethodService();

                List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
                paymentMethods = paymentMethodService.GetAllPaymentMethods();

                ////auto complete Payment Method Code
                autoCompletePaymentMethodCode = new AutoCompleteStringCollection();
                for (int i = 0; i < paymentMethods.Count; i++)
                {
                    autoCompletePaymentMethodCode.Add(paymentMethods[i].PaymentMethodCode.Trim());
                }
                Common.SetAutoComplete(txtPaymentMethodCode, autoCompletePaymentMethodCode, chkAutoCompleationPaymentMethodCode.Checked);
                ////

                ////auto complete Payment Method name
                autoCompletePaymentMethodName = new AutoCompleteStringCollection();
                for (int i = 0; i < paymentMethods.Count; i++)
                {
                    autoCompletePaymentMethodName.Add(paymentMethods[i].PaymentMethodName.Trim());
                }
                Common.SetAutoComplete(txtPaymentMethodName, autoCompletePaymentMethodName, chkAutoCompleationPaymentMethodCode.Checked);
                ////

                Common.EnableButton(true, btnNew);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                Common.EnableTextBox(true, txtPaymentMethodCode);
                Common.ClearTextBox(txtPaymentMethodCode);

                ActiveControl = txtPaymentMethodCode;
                txtPaymentMethodCode.Focus();
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
                if (ValidateControls().Equals(false))
                {
                    return;
                }

                PaymentMethodService paymentMethodService = new PaymentMethodService();
                bool isNew = false;
                existingPaymentMethod = paymentMethodService.GetPaymentMethodsByCode(txtPaymentMethodCode.Text.Trim());

                if (existingPaymentMethod == null || existingPaymentMethod.PaymentMethodID == 0)
                {
                    existingPaymentMethod = new PaymentMethod();
                    isNew = true;
                }

                existingPaymentMethod.PaymentMethodCode = txtPaymentMethodCode.Text.Trim();
                existingPaymentMethod.PaymentMethodName = txtPaymentMethodName.Text.Trim();
                existingPaymentMethod.CommissionRate = decimal.Parse(txtCommisionRate.Text.Trim());

                if (existingPaymentMethod.PaymentMethodID== 0)
                {
                    if ((Toast.Show("Payment Method - " + existingPaymentMethod.PaymentMethodCode + " - " + existingPaymentMethod.PaymentMethodName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    paymentMethodService.AddPaymentMethod(existingPaymentMethod);

                    if ((Toast.Show("Payment Method - " + existingPaymentMethod.PaymentMethodCode + " - " + existingPaymentMethod.PaymentMethodName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        if ((Toast.Show("Payment Method - " + existingPaymentMethod.PaymentMethodCode + " - " + existingPaymentMethod.PaymentMethodName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingPaymentMethod.PaymentMethodCode + " - " + existingPaymentMethod.PaymentMethodName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("Payment Method - " + existingPaymentMethod.PaymentMethodCode + " - " + existingPaymentMethod.PaymentMethodName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    paymentMethodService.UpdatePaymentMethod(existingPaymentMethod);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    Toast.Show("Payment Method - " + existingPaymentMethod.PaymentMethodCode + " - " + existingPaymentMethod.PaymentMethodName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }
                txtPaymentMethodCode.Focus();
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
                if (Toast.Show("Payment Method - " + existingPaymentMethod.PaymentMethodCode + " - " + existingPaymentMethod.PaymentMethodName+ "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                {
                    return;
                }

                PaymentMethodService paymentMethodService = new PaymentMethodService();
                existingPaymentMethod = paymentMethodService.GetPaymentMethodsByCode(txtPaymentMethodCode.Text.Trim());

                if (existingPaymentMethod != null && existingPaymentMethod.PaymentMethodID != 0)
                {
                    paymentMethodService.DeletePaymentMethod(existingPaymentMethod);

                    Toast.Show("Payment Method - " + existingPaymentMethod.PaymentMethodCode + " - " + existingPaymentMethod.PaymentMethodName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    ClearForm();
                    txtPaymentMethodCode.Focus();
                }
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
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPaymentMethod");
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

        private void txtPaymentMethodCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    PaymentMethodService paymentMethodService = new PaymentMethodService();
                    DataView dvAllReferenceData = new DataView(paymentMethodService.GetAllActivePaymentMethodsDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtPaymentMethodCode);
                        txtPaymentMethodCode_Leave(this, e);
                    }
                }
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtPaymentMethodName.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPaymentMethodCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPaymentMethodCode.Text.Trim() != string.Empty)
                {
                    PaymentMethodService paymentMethodService = new PaymentMethodService();
                    existingPaymentMethod = paymentMethodService.GetPaymentMethodsByCode(txtPaymentMethodCode.Text.Trim());

                    if (existingPaymentMethod != null)
                    {
                        txtPaymentMethodCode.Text = existingPaymentMethod.PaymentMethodCode.Trim();
                        txtPaymentMethodName.Text = existingPaymentMethod.PaymentMethodName.Trim();
                        txtCommisionRate.Text = existingPaymentMethod.CommissionRate.ToString();
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        txtPaymentMethodName.Focus();
                    }
                    else
                    {
                        if (chkAutoClear.Checked)
                        {
                            Common.ClearTextBox(txtPaymentMethodName, txtCommisionRate);
                        }
                        if (btnNew.Enabled)
                        {
                            if (Toast.Show("Payment Method - " + txtPaymentMethodCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                            {
                                btnNew.PerformClick();
                            }
                        }
                    }
                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtPaymentMethodCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPaymentMethodName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    PaymentMethodService paymentMethodService = new PaymentMethodService();
                    DataView dvAllReferenceData = new DataView(paymentMethodService.GetAllActivePaymentMethodsDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtPaymentMethodCode);
                        txtPaymentMethodCode_Leave(this, e);
                    }
                }
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtCommisionRate.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPaymentMethodName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                {
                    return;
                }
                if (txtPaymentMethodName.Text.Trim() != string.Empty)
                {
                    PaymentMethodService paymentMethodService = new PaymentMethodService();
                    existingPaymentMethod = paymentMethodService.GetPaymentMethodsByName(txtPaymentMethodName.Text.Trim());

                    if (existingPaymentMethod != null)
                    {
                        txtPaymentMethodCode.Text = existingPaymentMethod.PaymentMethodCode.Trim();
                        txtPaymentMethodName.Text = existingPaymentMethod.PaymentMethodName.Trim();
                        txtCommisionRate.Text = existingPaymentMethod.CommissionRate.ToString();
                        Common.EnableButton(true, btnSave, btnDelete);
                        Common.EnableButton(false, btnNew);
                        txtCommisionRate.Focus();
                    }
                    else
                    {
                        if (btnNew.Enabled)
                        {
                            if (Toast.Show("Payment Method - " + txtPaymentMethodCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                            {
                                btnNew.PerformClick();
                            }
                        }
                    }
                    txtCommisionRate.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCommisionRate_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtCommisionRate_Leave(object sender, EventArgs e)
        {

        }

        #endregion
        
        #region Methods....

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtPaymentMethodCode, txtPaymentMethodName);
        }

        private void chkAutoCompleationPaymentMethod_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Common.SetAutoComplete(txtPaymentMethodCode, autoCompletePaymentMethodCode, chkAutoCompleationPaymentMethodCode.Checked);
                Common.SetAutoComplete(txtPaymentMethodName, autoCompletePaymentMethodName, chkAutoCompleationPaymentMethodCode.Checked);
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

        #endregion
    }
}
