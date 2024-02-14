using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using CrystalDecisions.CrystalReports.Engine;
using Domain;
using Report.Inventory;
using Utility;
using Service;
using System.Reflection;
using Report;
using Report.Inventory.Reference.Reports;
//
namespace UI.Windows
{
    /// <summary>
    /// Developed by - asanka
    /// </summary>
    public partial class FrmDepartment : UI.Windows.FrmBaseMasterForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;
        private ErrorMessage errorMessage = new ErrorMessage();
        private InvDepartment existingDeparment;
        private AutoCompleteStringCollection autoCompleteDepartmentCode = new AutoCompleteStringCollection();
        private AutoCompleteStringCollection autoCompleteDepartmentName = new AutoCompleteStringCollection();
        private bool isDependCategory = false;
        

        public FrmDepartment()
        {
            InitializeComponent();
        }


        #region Form Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmDepartment_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name,
                                Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void FormLoad()
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;

                //By Pravin  // For Dependency
                isDependCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend;
                this.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).FormText.Trim();
                lblDepartmentCode.Text = this.Text + " " + lblDepartmentCode.Text;
                lblDepartmentName.Text = this.Text + " " + lblDepartmentName.Text;

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);


                base.FormLoad();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name,
                                Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    txtDepartmentCode.Text = invDepartmentService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix,AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength, isDependCategory);
                    Common.EnableTextBox(false, txtDepartmentCode);
                    txtDepartmentName.Focus();
                }
                else
                {
                    Common.EnableTextBox(true, txtDepartmentCode);
                    txtDepartmentCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    DataView dvAllReferenceData = new DataView(invDepartmentService.GetAllActiveInvDepartmentsDataTable(isDependCategory));
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtDepartmentCode);
                        txtDepartmentCode_Leave(this, e);
                    }
                }

                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtDepartmentName.Focus();
                txtDepartmentName.SelectionStart = txtDepartmentName.Text.Length;
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
                if (string.IsNullOrEmpty(txtDepartmentCode.Text.Trim()))
                {
                    return;
                }

                InvDepartmentService invDepartmentService = new InvDepartmentService();
                existingDeparment = invDepartmentService.GetInvDepartmentsByCode(txtDepartmentCode.Text.Trim(),
                                                                                 isDependCategory);

                if (existingDeparment != null)
                {
                    txtDepartmentCode.Text = existingDeparment.DepartmentCode.Trim();
                    txtDepartmentName.Text = existingDeparment.DepartmentName.Trim();
                    txtRemark.Text = existingDeparment.Remark.Trim();
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                    Common.EnableButton(false, btnNew);
                }
                else
                {
                    if (chkAutoClear.Checked)
                    {
                        Common.ClearTextBox(txtDepartmentName, txtRemark);
                    }

                    if (btnNew.Enabled)
                    {
                        if (
                            Toast.Show("" + this.Text + " - " + txtDepartmentCode.Text.Trim() + "",
                                       Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(
                                           DialogResult.Yes))
                        {
                            btnNew.PerformClick();
                        }
                    }
                }

                if (btnSave.Enabled)
                {
                    Common.EnableTextBox(false, txtDepartmentCode);
                }

                txtDepartmentName.Focus();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name,
                                Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    DataView dvAllReferenceData = new DataView(invDepartmentService.GetAllActiveInvDepartmentsDataTable(isDependCategory));
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtDepartmentCode);
                        txtDepartmentCode_Leave(this, e);
                    }
                }
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtRemark.Focus();
                txtRemark.SelectionStart = txtRemark.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name,
                                Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(txtDepartmentName.Text.Trim()))
                {
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    InvDepartment existingInvDepartment = new InvDepartment();
                    existingInvDepartment = invDepartmentService.GetInvDepartmentsByName(txtDepartmentName.Text.Trim(),
                                                                                         isDependCategory);

                    if (existingInvDepartment != null)
                    {
                        txtDepartmentCode.Text = existingInvDepartment.DepartmentCode;
                        txtDepartmentName.Text = existingInvDepartment.DepartmentName;
                        txtRemark.Text = existingInvDepartment.Remark;

                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        Common.EnableTextBox(false, txtDepartmentCode);
                    }
                    else
                    {
                        Toast.Show("" + this.Text + " - " + txtDepartmentName.Text.Trim() + "",
                                   Toast.messageType.Information, Toast.messageAction.NotExists);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name,
                                Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        ///  Set Auto complete values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAutoCompleationDepartment_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Common.SetAutoComplete(txtDepartmentCode, autoCompleteDepartmentCode,
                                       chkAutoCompleationDepartment.Checked);
                Common.SetAutoComplete(txtDepartmentName, autoCompleteDepartmentName,
                                       chkAutoCompleationDepartment.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name,
                                Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Setup GUI contols (Set visibility, data binding, auto loading)
        /// </summary>
        public override void InitializeForm()
        {
            try
            {
                InvDepartmentService invDepartmentService = new InvDepartmentService();
                autoCompleteDepartmentCode.Clear();
                autoCompleteDepartmentCode.AddRange(invDepartmentService.GetInvDepartmentCodes(isDependCategory));
                Common.SetAutoComplete(txtDepartmentCode, autoCompleteDepartmentCode,
                                       chkAutoCompleationDepartment.Checked);
                autoCompleteDepartmentName.Clear();
                autoCompleteDepartmentName.AddRange(invDepartmentService.GetInvDepartmentNames(isDependCategory));
                Common.SetAutoComplete(txtDepartmentName, autoCompleteDepartmentName,
                                       chkAutoCompleationDepartment.Checked);

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete, btnView);
                Common.EnableTextBox(true, txtDepartmentCode);
                Common.ClearTextBox(txtDepartmentCode);

                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                ActiveControl = txtDepartmentCode;
                txtDepartmentCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name,
                                Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// Save new Department or Update existing department
        /// </summary>
        public override void Save()
        {
            try
            {
                if (!ValidateControls())
                {
                    return;
                }

                InvDepartmentService invDepartmentService = new InvDepartmentService();
                bool isNew = false;
                existingDeparment = invDepartmentService.GetInvDepartmentsByCode(txtDepartmentCode.Text.Trim(),
                                                                                 isDependCategory);

                if (existingDeparment == null || existingDeparment.InvDepartmentID == 0)
                {
                    existingDeparment = new InvDepartment();
                }

                existingDeparment.DepartmentCode = txtDepartmentCode.Text.Trim();
                existingDeparment.DepartmentName = txtDepartmentName.Text.Trim();
                existingDeparment.Remark = txtRemark.Text.Trim();
                existingDeparment.IsDelete = false;

                if (existingDeparment.InvDepartmentID.Equals(1) && isDependCategory.Equals(false))
                    return;

                if (existingDeparment.InvDepartmentID.Equals(0))
                {
                    if ((Toast.Show("" + this.Text + " - " + existingDeparment.DepartmentCode + " - " + existingDeparment.DepartmentName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    // Create new Department
                    invDepartmentService.AddInvDepartment(existingDeparment);

                    if ((Toast.Show("" + this.Text + " - " + existingDeparment.DepartmentCode + " - " + existingDeparment.DepartmentName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        if ((Toast.Show("" + this.Text + " - " + existingDeparment.DepartmentCode + " - " +existingDeparment.DepartmentName + "", Toast.messageType.Question,Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingDeparment.DepartmentCode + " - " + existingDeparment.DepartmentName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("" + this.Text + " - " + existingDeparment.DepartmentCode + " - " + existingDeparment.DepartmentName + "", Toast.messageType.Question,Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    // Update Department deatils
                    invDepartmentService.UpdateInvDepartment(existingDeparment);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }

                    // Display updated iformation 
                    Toast.Show("" + this.Text + " - " + existingDeparment.DepartmentCode + " - " +existingDeparment.DepartmentName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }

                txtDepartmentCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name,Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// Delete Department( Change Delete status of relevent Department)
        /// </summary>
        public override void Delete()
        {
            try
            {
                InvDepartmentService invDepartmentService = new InvDepartmentService();
                existingDeparment = invDepartmentService.GetInvDepartmentsByCode(txtDepartmentCode.Text.Trim(),
                                                                                 isDependCategory);

                if (
                    Toast.Show(
                        "" + this.Text + " - " + existingDeparment.DepartmentCode + " - " +
                        existingDeparment.DepartmentName + "", Toast.messageType.Question, Toast.messageAction.Delete).
                        Equals(DialogResult.No))
                {
                    return;
                }

                if (existingDeparment != null && existingDeparment.InvDepartmentID != 0)
                {
                    existingDeparment.IsDelete = true;
                    invDepartmentService.UpdateInvDepartment(existingDeparment);
                    MessageBox.Show(
                        "" + this.Text + " - " + existingDeparment.DepartmentCode.Trim() + " successfully deleted",
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    InitializeForm();
                    txtDepartmentCode.Focus();
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name,
                                Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void View()
        {
            try
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment");
                InvReportGenerator invReportGenerator = new InvReportGenerator();
                invReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void CloseForm()
        {
            base.CloseForm();
        }

        /// <summary>
        /// Validate form controls
        /// </summary>
        /// <returns></returns>
        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDepartmentCode,
                                             txtDepartmentName);
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

        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                //FrmReportViewer reportViewer = new FrmReportViewer();
                //Cursor.Current = Cursors.WaitCursor;
                //existingDeparment = new InvDepartment();
                //InvDepartmentService invDepartmentService = new InvDepartmentService();

                //CryRptDeparment rptDeparment = new CryRptDeparment();

                //rptDeparment.SummaryInfo.ReportTitle = "Department Details";
                //rptDeparment.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

                //rptDeparment.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
                //rptDeparment.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                //rptDeparment.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                //rptDeparment.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                //rptDeparment.DataDefinition.FormulaFields["isOrg"].Text = "" + 1 + "";
                //rptDeparment.DataDefinition.FormulaFields["DateFrom"].Text = "'" + "01/01/2013" + "'";
                //rptDeparment.DataDefinition.FormulaFields["DateTo"].Text = "'" + "01/01/2013" + "'";
                //rptDeparment.DataDefinition.FormulaFields["ValueFrom"].Text = "'" + 01000045128 + "'";
                //rptDeparment.DataDefinition.FormulaFields["ValueTo"].Text = "'" + 01000045158 + "'";
                //rptDeparment.Section1.SectionFormat.EnableSuppress = false;
                //rptDeparment.Section2.SectionFormat.EnableSuppress = false;
                //rptDeparment.Section3.SectionFormat.EnableSuppress = false;
                //rptDeparment.Section4.SectionFormat.EnableSuppress = false;
                //rptDeparment.Section5.SectionFormat.EnableSuppress = false;



                //DataTable dt = new DataTable();
                //dt = invDepartmentService.GetAllActiveInvDepartmentsDataTable(true);

                //rptDeparment.SetDataSource(dt);

                //reportViewer.crRptViewer.ReportSource = rptDeparment;
                //reportViewer.WindowState = FormWindowState.Maximized;
                //reportViewer.Show();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }


     

    }
}


 

