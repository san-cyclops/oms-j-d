using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Domain;
using Service;
using Utility;

namespace UI.Windows
{
    public partial class FrmLinkedAccount : UI.Windows.FrmBaseMasterForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        List<AccTransactionTypeDetail> accTransactionTypeDetailsList = new List<AccTransactionTypeDetail>();

        AccLedgerAccount existingAccLedgerAccount;
        private ReferenceType existingReferenceType;


        int documentID = 0;

        public FrmLinkedAccount()
        {
            InitializeComponent();
        }

        #region Form Events
        #endregion

        #region Methods

        public override void InitializeForm()
        {
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                Common.SetAutoComplete(txtLedgerCode, accLedgerAccountService.GetPettyCashLedgerCodes(), chkAutoCompleationLedgerAccount.Checked);
                Common.SetAutoComplete(txtLedgerName, accLedgerAccountService.GetPettyCashLedgerNames(), chkAutoCompleationLedgerAccount.Checked);


                Common.EnableButton(false, btnView, btnSave, btnDelete);
                Common.EnableComboBox(true, cmbTransaction);
                Common.EnableComboBox(false,cmbDrCrType);
                Common.EnableTextBox(false,txtLedgerCode,txtLedgerName,txtLedgerPercentage);

                Common.ClearTextBox(txtLedgerCode, txtLedgerName, txtLedgerPercentage);
                Common.ClearComboBox(cmbTransaction,cmbDefinition,cmbDrCrType);

                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                existingAccLedgerAccount = null;
                existingReferenceType = null;

                ActiveControl = cmbTransaction;
                cmbTransaction.Focus();
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
                //chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                this.Text = autoGenerateInfo.FormText;

                dgvLinkedAccount.AutoGenerateColumns = false;

                documentID = autoGenerateInfo.DocumentID;

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);

                GetAllTransactions();

                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                Common.SetAutoBindRecords(cmbDefinition, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.TransactionDefinition).ToString()));
                Common.SetAutoBindRecords(cmbDrCrType, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.EntryDrCr).ToString()));

                // Read Ledger Accounts
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                Common.SetAutoComplete(txtLedgerCode, accLedgerAccountService.GetLedgerCodes(), chkAutoCompleationLedgerAccount.Checked);
                Common.SetAutoComplete(txtLedgerName, accLedgerAccountService.GetLedgerNames(), chkAutoCompleationLedgerAccount.Checked);

                base.FormLoad();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void GetAllTransactions()
        {
            Common.SetAutoBindRecords(cmbTransaction, AutoGenerateInfoService.GetTransactionFormTexts());
        }

        public override void Save()
        {
            try
            {
                if (!ValidateControls()) { return; }

                AccTransactionTypeDetail accTransactionTypeDetail = new AccTransactionTypeDetail();
                AccTransactionTypeService accTransactionTypeService = new AccTransactionTypeService();

                bool isNew = false;
                //accTransactionTypeDetail = accTransactionTypeService.GetAllAccTransactionTypeDetailsByTransactionIDDefinition(txtSupplierCode.Text.Trim());

                //if (existingSupplier == null || existingSupplier.SupplierID == 0)
                //{
                //    existingSupplier = new Supplier();
                //    existingSupplierProperty = new SupplierProperty();
                //    isNew = true;
                //}
                //else
                //{
                //    existingSupplierProperty = existingSupplier.SupplierProperty;
                //    //supplierPropertyService.GetSupplierPropertyByID(existingSupplier.SupplierID);
                //}

                //FillSupplier();

                //if (existingSupplier.SupplierID == 0)
                //{
                //    if ((Toast.Show(this.Text + " - " + existingSupplier.SupplierCode + " - " + existingSupplier.SupplierName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                //    {
                //        return;
                //    }

                //    supplierService.AddSupplier(existingSupplier);

                //    if ((Toast.Show(this.Text + " - " + existingSupplier.SupplierCode + " - " + existingSupplier.SupplierName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
                //    {
                //        if (chkAutoClear.Checked)
                //        {
                //            ClearForm();
                //        }
                //        btnNew.Enabled = true;
                //        btnNew.PerformClick();
                //    }
                //    else
                //    {
                //        if (chkAutoClear.Checked)
                //        {
                //            ClearForm();
                //        }
                //        else
                //        {
                //            InitializeForm();
                //        }
                //    }
                //}
                //else
                //{
                //    if (isNew)
                //    {
                //        if ((Toast.Show(this.Text + " - " + existingSupplier.SupplierCode + " - " + existingSupplier.SupplierName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                //        {
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingSupplier.SupplierCode + " - " + existingSupplier.SupplierName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                //        if ((Toast.Show(this.Text + " - " + existingSupplier.SupplierCode + " - " + existingSupplier.SupplierName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                //        {
                //            return;
                //        }
                //    }
                //    supplierService.UpdateSupplier(existingSupplier);
                //    if (chkAutoClear.Checked)
                //    {
                //        ClearForm();
                //    }
                //    else
                //    {
                //        InitializeForm();
                //    }
                //    Toast.Show(this.Text + " - " + existingSupplier.SupplierCode + " - " + existingSupplier.SupplierName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                //}
                //txtSupplierCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateControls()
        {
            return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, cmbTransaction,cmbDefinition,txtLedgerCode,txtLedgerName,cmbDrCrType,txtLedgerPercentage);
        }

        private void LoadLeaders(bool isCode, string strLedger)
        {
            try
            {
                existingAccLedgerAccount = new AccLedgerAccount();

                if (strLedger.Equals(string.Empty))
                {return;}

                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();

                if (isCode)
                {
                    existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByCode(strLedger);
                    if (isCode && strLedger.Equals(string.Empty))
                    {
                        txtLedgerName.Focus();
                        return;
                    }
                }
                else
                { existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByName(strLedger); }

                if (existingAccLedgerAccount != null)
                {
                    if (isCode)
                    {
                        txtLedgerName.Text = existingAccLedgerAccount.LedgerName;
                    }
                    else
                    {
                        txtLedgerCode.Text = existingAccLedgerAccount.LedgerCode;
                    }

                    txtLedgerPercentage.Focus();
                }
                else
                {
                    Toast.Show("Ledger - " + strLedger + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }
        #endregion

        private void chkAutoCompleationLedgerAccount_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                Common.SetAutoComplete(txtLedgerCode, accLedgerAccountService.GetLedgerCodes(), chkAutoCompleationLedgerAccount.Checked);
                Common.SetAutoComplete(txtLedgerName, accLedgerAccountService.GetLedgerNames(), chkAutoCompleationLedgerAccount.Checked);
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
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtLedgerCode.Text.Trim().Equals(string.Empty))
                    {
                        txtLedgerName.Enabled = true;
                        txtLedgerName.Focus();
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
                LoadLeaders(true, txtLedgerCode.Text.Trim());
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
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtLedgerName.Text.Trim().Equals(string.Empty))
                    {
                        cmbDrCrType.Enabled = true;
                        cmbDrCrType.Focus();
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
                LoadLeaders(false, txtLedgerName.Text.Trim());
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLedgerPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimalCurrency(txtLedgerPercentage.Text.Trim()) > 0)
                    {
                        AccTransactionTypeDetail accTransactionTypeDetail = new AccTransactionTypeDetail();
                        AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                        LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                        AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();

                        autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(cmbTransaction.Text.Trim());
                        if (autoGenerateInfo != null) { accTransactionTypeDetail.AccTransactionTypeHeaderID = autoGenerateInfo.AutoGenerateInfoID; }
                        else accTransactionTypeDetail.AccTransactionTypeHeaderID = 0;

                        //Read Transaction Definition
                        existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.TitleType).ToString(), cmbDefinition.Text.Trim());
                        if (existingReferenceType != null)
                        {
                            accTransactionTypeDetail.TransactionDefinition = existingReferenceType.LookupKey;
                        }
                        else
                        {
                            accTransactionTypeDetail.TransactionDefinition = 0;
                        }

                        accTransactionTypeDetail.AccLedgerAccountID = accLedgerAccountService.GetAccLedgerAccountByCode(txtLedgerCode.Text.Trim()).AccLedgerAccountID;
                        accTransactionTypeDetail.LedgerCode = txtLedgerCode.Text.Trim();
                        accTransactionTypeDetail.LedgerName = txtLedgerName.Text.Trim();

                        //Read Dr / Cr Type
                        existingReferenceType = lookUpReferenceService.GetLookUpReferenceByValue(((int)LookUpReference.TitleType).ToString(), cmbDrCrType.Text.Trim());
                        if (existingReferenceType != null)
                        {
                            accTransactionTypeDetail.DrCr = existingReferenceType.LookupKey;
                        }
                        else
                        {
                            accTransactionTypeDetail.DrCr = 0;
                        }

                        accTransactionTypeDetail.LedgerPercentage = Common.ConvertStringToDecimalCurrency(txtLedgerPercentage.Text.ToString());

                        if (accTransactionTypeDetailsList == null)
                        {accTransactionTypeDetailsList = new List<AccTransactionTypeDetail>();}

                        //OtherExpenseTemp otherExpenceTempRemove = new OtherExpenseTemp();

                        //otherExpenceTempRemove = otherExpenceTempList.Where(p => p.AccLedgerAccountID.Equals(otherExpenceTemp.AccLedgerAccountID)).FirstOrDefault();

                        //if (otherExpenceTempRemove != null)
                        //    otherExpenceTempList.Remove(otherExpenceTempRemove);

                        accTransactionTypeDetailsList.Add(accTransactionTypeDetail);
                        dgvLinkedAccount.DataSource = null;
                        dgvLinkedAccount.DataSource = accTransactionTypeDetailsList;
                        dgvLinkedAccount.Refresh();

                        if (accTransactionTypeDetailsList.Count > 0)
                        {Common.EnableTextBox(true,txtLedgerCode);}

                        //txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(otherExpenceTempList.Sum(p => p.ExpenseAmount));
                        //GetSummarizeSubFigures();

                        Common.ClearTextBox(txtLedgerCode, txtLedgerName, txtLedgerPercentage);
                        Common.ClearComboBox(cmbDrCrType);
                        txtLedgerCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbDefinition_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!cmbDefinition.Text.Trim().Equals(string.Empty))
                    {
                        txtLedgerCode.Enabled = true;
                        txtLedgerCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbTransaction_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!cmbTransaction.Text.Trim().Equals(string.Empty))
                    {
                        cmbDefinition.Enabled = true;
                        cmbDefinition.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbDrCrType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!cmbDrCrType.Text.Trim().Equals(string.Empty))
                    {
                        txtLedgerPercentage.Enabled = true;
                        txtLedgerPercentage.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
    }
}
