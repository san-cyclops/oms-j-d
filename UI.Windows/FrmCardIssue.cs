using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Domain;
using Service;
using Utility;

namespace UI.Windows
{
    public partial class FrmCardIssue : UI.Windows.FrmBaseTransactionForm
    {
        //private List<LoyaltyCardGenerationDetailTemp> loyaltyCardGenerationDetailTemps = new List<LoyaltyCardGenerationDetailTemp>();
        List<LoyaltyCardGenerationDetail> loyaltyCardGenerationDetailList = new List<LoyaltyCardGenerationDetail>();
        private Employee existingEmployee;
        //private CardMaster existingCardMaster;
        private LocationService locationService;
        private LoyaltyCardIssueHeader existingIssueMaster;
        private LoyaltyCardIssueDetail existingIssueDetail;
        private Location existingLocation;
        private int documentID;// = 28;

        public FrmCardIssue()
        {
            InitializeComponent();
        }

        private void FrmCardIssue_Load(object sender, EventArgs e)
        {
            chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;

            dgvDisplay.AutoGenerateColumns = false;

            base.FormLoad();
            txtReferenceNo.Focus();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public override void InitializeForm()
        {
            try
            {
       
                base.InitializeForm();

                chkAutoCompleationCardRange.Checked = true;
                chkAutoCompleationEmployee.Checked = true;
                chkAutoCompleationDocNo.Checked = true;
                
                CardMasterService cardMasterService = new CardMasterService();
                //Common.SetAutoComplete(txtCardTypeCode, cardMasterService.GetAllCardCodes(), chkAutoCompleationCard.Checked);
                //Common.SetAutoComplete(txtCardTypeName, cardMasterService.GetAllCardNames(), chkAutoCompleationCard.Checked);

                // Load Locations            
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
                cmbLocation.SelectedValue = Common.LoggedLocationID;

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;

                 txtDocNo.Text = GetDocumentNo(true);
                 txtReferenceNo.Focus();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }


        private string GetDocumentNo(bool isTemporytNo)
        {

            try
            {
                LoyaltyCardIssueService loyaltyCardIssueService = new LoyaltyCardIssueService();
                LocationService locationService = new LocationService();
                return loyaltyCardIssueService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }

        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCardNoFrom.Text.Trim()) && string.IsNullOrEmpty(txtCardNoTo.Text.Trim()))
                {
                    LoadCardNOs(txtCardNoFrom.Text.Trim(), txtCardNoTo.Text.Trim());
                    UpdatedgvDisplay();
                    return;
                }

                LoyaltyCardGeneratrionService loyaltyCardGeneratrionService = new LoyaltyCardGeneratrionService();
                if (loyaltyCardGeneratrionService.GetUnIssuedLoyaltyCardGenerationDetailByCardNo(txtCardNoFrom.Text.Trim()) == null)
                {
                    Toast.Show("From Card No -" + txtCardNoFrom.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
                
                if (loyaltyCardGeneratrionService.GetUnIssuedLoyaltyCardGenerationDetailByCardNo(txtCardNoTo.Text.Trim()) == null)
                {
                    Toast.Show("To Card No -" + txtCardNoTo.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }

                LoadCardNOs(txtCardNoFrom.Text.Trim(), txtCardNoTo.Text.Trim());
                UpdatedgvDisplay();                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name,Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private List<LoyaltyCardGenerationDetail> LoadCardNOs(string codeFrom, string codeTo)
        {            
            LoyaltyCardGeneratrionService loyaltyCardGeneratrionService = new LoyaltyCardGeneratrionService();            
            loyaltyCardGenerationDetailList = loyaltyCardGeneratrionService.GetPendigLoyaltyCardList(codeFrom, codeTo);
            return loyaltyCardGenerationDetailList;
        }

        private void UpdatedgvDisplay()
        {
            dgvDisplay.DataSource = null;
            dgvDisplay.DataSource = loyaltyCardGenerationDetailList.OrderBy(cd => cd.CardNo).ToList();
        }

        //private List<LoyaltyCardGenerationDetailTemp> GenerateCardNOs(string prefix, int bookCodeLength, int startingNo,int qty)
        //{
        //    //InvGiftVoucherMasterDetailTemp invGiftVoucherMasterDetailTemp = new InvGiftVoucherMasterDetailTemp();

        //    int No = startingNo;
        //    loyaltyCardGenerationDetailTemps.Clear();
        //    for (int i = 1; i < qty + 1; i++)
        //    {
        //        var tc = new LoyaltyCardGenerationDetailTemp()
        //                     {
        //                         SerialNo = GetCodeFormat(prefix, bookCodeLength, No),
        //                     };
        //        loyaltyCardGenerationDetailTemps.Add(tc);
        //        No = No + 1;
        //    }
        //    return loyaltyCardGenerationDetailTemps;
        //}

        private string GetCodeFormat(string prefix, int length, int startNo)
        {
                string Format = "";

                if (length > 0)
                {
                    length = (length - prefix.Length);
                }

                if (!string.IsNullOrEmpty(length.ToString()))
                {
                    Format = String.Format("{0}{1," + length + ":D" + length + "} ", prefix, startNo);
                }
                return Format;
        }

        private bool IsValidateControls()
        {
            try
            {
                //LoyaltyCardGeneratrionValidator cardGeneratrionValidator = new LoyaltyCardGeneratrionValidator();
                bool isValidated = false;
                //if (
                    
                //{
                //    //string text = lblSerialLength.Text.Replace(lblSerialLength.Text.Substring((lblSerialLength.Text.Length - 1), 1), "");

                //    Toast.Show("Not Validate Length", Toast.messageType.Information, Toast.messageAction.Length);
                //    isValidated = false;
                //}
                //else
                //{
                //    isValidated = true;
                //}

                //return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtPrefix, txtLength, txtQty,txtStartingNo,txtCardTypeCode,txtCardTypeName,txtEmployeeCode,txtEmployeeName);
                //return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbLocation);



                return isValidated;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override void Pause()
        {

            if ((Toast.Show("Card Issue No  - " + txtDocNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                if (SaveDocument(0, txtDocNo.Text.Trim()).Equals(true))
                {
                    Toast.Show("Card Issue No  - " + txtDocNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    ClearForm();
                }
                else
                {
                    Toast.Show("Card Issue No  - " + txtDocNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }

        }

        public override void Save()
        {

            if ((Toast.Show("Card Issue No  - " + txtDocNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                bool saveDocument = SaveDocument(1, txtDocNo.Text.Trim());
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Card Issue No  - " + txtDocNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    ClearForm();
                }
                else
                {
                    Toast.Show("Card Issue No  - " + txtDocNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }

        }

        public override void ClearForm()
        {
            try
            {
                loyaltyCardGenerationDetailList.Clear();
                UpdatedgvDisplay();
                base.ClearForm();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool SaveDocument(int documentStatus, string documentNo)
        {
            try
            {

                existingIssueMaster = new LoyaltyCardIssueHeader();
                LoyaltyCardIssueService loyaltyCardIssueService = new LoyaltyCardIssueService();
                EmployeeService employeeService = new EmployeeService();
                LocationService locationService = new LocationService();
                existingEmployee = employeeService.GetEmployeesByCode(txtEmployeeCode.Text.Trim());

                existingLocation = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                existingIssueMaster = loyaltyCardIssueService.getIssueHeaderByDocNo(documentID, txtDocNo.Text.Trim(), existingLocation.LocationID);

                CardMasterService cardMasterService = new CardMasterService();
                //existingCardMaster = cardMasterService.GetCardMasterByCode(txtCardTypeCode.Text.Trim());

                if (existingIssueMaster == null)
                { existingIssueMaster = new LoyaltyCardIssueHeader(); }

                if (documentStatus.Equals(1)) // update paused document
                {
                    documentNo = GetDocumentNo(false);
                    txtDocNo.Text = documentNo;
                }

                existingIssueMaster.DocumentNo = txtDocNo.Text.Trim();
                existingIssueMaster.ReferenceNo = txtRemark.Text.Trim();
                existingIssueMaster.Remark = txtRemark.Text.Trim();
                existingIssueMaster.IssueDate = dtpIssueDate.Value;
                //existingIssueMaster.CardPrefix = txtPrefix.Text.Trim();
                //existingIssueMaster.CardLength = Common.ConvertStringToInt(txtLength.Text.Trim());
                //existingIssueMaster.CardFormat = txtFormat.Text.Trim();
                //existingIssueMaster.StartingNo = Common.ConvertStringToInt(txtStartingNo.Text.Trim());
                existingIssueMaster.ToLocationId = existingLocation.LocationID;
                existingIssueMaster.EmployeeId = existingEmployee.EmployeeID;
                //existingIssueMaster.CardTypeId = existingCardMaster.CardMasterID;

                //if (loyaltyCardGenerationDetailTemps == null)
                //{ loyaltyCardGenerationDetailTemps = new List<LoyaltyCardGenerationDetailTemp>(); }

                return loyaltyCardIssueService.Save(existingIssueMaster, loyaltyCardGenerationDetailList);
               
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return false;

            }


        }
                       

        private bool IsLocationExistsByName(string locationName)
        {
            try
            {
                bool recodFound = false;
                LocationService locationService = new LocationService();
                Location location = new Location();
                location = locationService.GetLocationsByName(locationName);
                if (location != null)
                {
                    recodFound = true;
                    cmbLocation.Text = location.LocationName;
                }
                else
                {
                    recodFound = false;
                    Common.ClearComboBox(cmbLocation);
                    Toast.Show(lblLocation.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                }

                return recodFound;
            }
            catch (Exception)
            {

                throw;
            }
        }

       

        #region Key Down Events

        private void txtDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbLocation);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name,Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e,txtCardNoFrom);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void dtpIssueDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e,txtRemark);
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
                Common.SetFocus(e, cmbLocation);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

       

        

        

        

        #endregion
        

        private void txtEmployeeCode_KeyDown(object sender, KeyEventArgs e)
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

        

        private void txtEmployeeName_KeyDown(object sender, KeyEventArgs e)
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

        private void txtEmployeeCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeCode.Text.Trim() != string.Empty)
                {
                    EmployeeService employeeService = new EmployeeService();
                    Employee employee = new Employee();
                    employee =employeeService.GetEmployeesByCode(txtEmployeeCode.Text.Trim());
                    if (employee == null)
                    {
                        Toast.Show("Employee Code - " + txtEmployeeCode.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }
                    else
                    {
                        txtEmployeeCode.Text = employee.EmployeeCode;
                        txtEmployeeName.Text = employee.EmployeeName;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDocNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtDocNo.Text != string.Empty)
                {
                    LoyaltyCardIssueService loyaltyCardIssueService = new LoyaltyCardIssueService();
                    existingIssueMaster = loyaltyCardIssueService.GetIssueHeaderByDocNo(txtDocNo.Text.Trim());

                    if (existingIssueMaster != null)
                    {
                        txtDocNo.Text = existingIssueMaster.DocumentNo.Trim();
                        txtRemark.Text = existingIssueMaster.ReferenceNo.Trim();
                        txtRemark.Text = existingIssueMaster.Remark.Trim();
                        dtpIssueDate.Value = existingIssueMaster.IssueDate;
                        //txtPrefix.Text  =   existingIssueMaster.CardPrefix.Trim();
                        //txtFormat.Text  =   existingIssueMaster.CardFormat.Trim();
                        //txtLength.Text  =   existingIssueMaster.CardLength.ToString();
                        //txtStartingNo.Text = existingIssueMaster.StartingNo.ToString();

                        EmployeeService employeeService = new EmployeeService();
                        LocationService locationService = new LocationService();

                        existingEmployee = employeeService.GetEmployeesByID(existingIssueMaster.EmployeeId);

                        existingLocation = locationService.GetLocationsByID(existingIssueMaster.ToLocationId);

                        //CardMasterService cardMasterService = new CardMasterService();
                        //existingCardMaster = cardMasterService.GetCardMasterById(existingIssueMaster.CardTypeId);

                        txtEmployeeCode.Text = existingEmployee.EmployeeCode;
                        txtEmployeeName.Text = existingEmployee.EmployeeName;
                        cmbLocation.Text = existingLocation.LocationName;
                        //txtCardTypeCode.Text = existingCardMaster.CardCode;
                        //txtCardTypeName.Text = existingCardMaster.CardName;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }                

        private void chkAutoCompleationDocNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoyaltyCardIssueService loyaltyCardIssueService = new LoyaltyCardIssueService();
                Common.SetAutoComplete(txtDocNo, loyaltyCardIssueService.GetAllDocNos(), chkAutoCompleationDocNo.Checked);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationEmployee_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                EmployeeService employeeService = new EmployeeService();
                Common.SetAutoComplete(txtEmployeeCode, employeeService.GetAllEmployeeCodes(),chkAutoCompleationEmployee.Checked);
                Common.SetAutoComplete(txtEmployeeName, employeeService.GetAllEmployeeNames(),chkAutoCompleationEmployee.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }        

        private void chkAutoCompleationCardRange_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoyaltyCardGeneratrionService loyaltyCardGeneratrionService = new LoyaltyCardGeneratrionService();
                Common.SetAutoComplete(txtCardNoFrom, loyaltyCardGeneratrionService.GetPendigLoyaltyCardNos(), chkAutoCompleationCardRange.Checked);
                Common.SetAutoComplete(txtCardNoTo, loyaltyCardGeneratrionService.GetPendigLoyaltyCardNos(), chkAutoCompleationCardRange.Checked);                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCardNoFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e,txtCardNoTo);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCardNoFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCardNoFrom.Text.Trim()))
                {
                    LoyaltyCardGeneratrionService loyaltyCardGeneratrionService = new LoyaltyCardGeneratrionService();
                    if (loyaltyCardGeneratrionService.GetUnIssuedLoyaltyCardGenerationDetailByCardNo(txtCardNoFrom.Text.Trim()) == null)
                    {
                        Toast.Show("Card No From -" + txtCardNoFrom.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtCardNoFrom.Focus();
                        return;
                    }                    
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCardNoTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                btnLoad.PerformClick();               
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCardNoTo_Leave(object sender, EventArgs e)
        {
            try
            {               
                if (!string.IsNullOrEmpty(txtCardNoTo.Text.Trim()))
                {
                    LoyaltyCardGeneratrionService loyaltyCardGeneratrionService = new LoyaltyCardGeneratrionService();
                    if (loyaltyCardGeneratrionService.GetUnIssuedLoyaltyCardGenerationDetailByCardNo(txtCardNoTo.Text.Trim()) == null)
                    {
                        Toast.Show("Card No To -" + txtCardNoTo.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtCardNoTo.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmployeeName_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeName.Text.Trim() != string.Empty)
                {
                    EmployeeService employeeService = new EmployeeService();
                    Employee employee = new Employee();
                    employee = employeeService.GetEmployeesByName(txtEmployeeName.Text.Trim());
                    if (employee == null)
                    {
                        Toast.Show("Employee Name - " + txtEmployeeName.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }
                    else
                    {
                        txtEmployeeCode.Text = employee.EmployeeCode;
                        txtEmployeeName.Text = employee.EmployeeName;
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
