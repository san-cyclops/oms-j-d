using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Service;
using Utility;
using Domain;

namespace UI.Windows
{
    public partial class FrmCardAllocation : UI.Windows.FrmBaseMasterForm
    {

        int documentID ;
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        LoyaltyCardAllocation existingLoyaltyCardAllocation;
        List<LoyaltyCardAllocation> loyaltyCardAllocations = new List<LoyaltyCardAllocation>();
        LoyaltyCustomer existingLoyaltyCustomer;
        public FrmCardAllocation()
        {
            InitializeComponent();
        }

        public override void InitializeForm()
        {
            try
            {
                CardMasterService cardMasterService = new CardMasterService();

                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);


                Common.ClearForm(this);


            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void FrmCardAllocation_Load(object sender, EventArgs e)
        {
            try
            {           
                AddCustomCoulmnstoGridCardType();
                base.FormLoad();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void ClearForm()
        {
            try
            {

                dgvItemDetails.DataSource = null;
                do
                {
                    foreach (DataGridViewRow row in dgvItemDetails.Rows)
                    {
                        try
                        {
                            dgvItemDetails.Rows.Remove(row);
                        }
                        catch (Exception) { }
                    }
                } while (dgvItemDetails.Rows.Count > 1);
                base.ClearForm();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void AddCustomCoulmnstoGridCardType()
        {
            try
            {
                CardMasterService cardMasterService = new CardMasterService();
                dgvItemDetails.AllowUserToAddRows = true;
                dgvItemDetails.Columns.Remove("CardType");
                DataGridViewComboBoxColumn comboboxColumnParentValues = new DataGridViewComboBoxColumn();
                comboboxColumnParentValues.DataSource = cardMasterService.GetAllCardMasters();
                comboboxColumnParentValues.DataPropertyName = "CardType";
                comboboxColumnParentValues.Name = "CardType";
                comboboxColumnParentValues.DisplayMember = "CardName";
                comboboxColumnParentValues.ValueMember = "CardMasterId";
                comboboxColumnParentValues.HeaderText = "Card Type";
                comboboxColumnParentValues.MaxDropDownItems = 20;
                comboboxColumnParentValues.FlatStyle = FlatStyle.Flat;
                dgvItemDetails.Columns.Insert(0, comboboxColumnParentValues);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvItemDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try 
	        {	        
	
                int colIndex;
                int rowIndex;

                TextBox txtBox = e.Control as TextBox;
                if (dgvItemDetails.CurrentCell.ColumnIndex == 0)
                {
              
                }

                else if (dgvItemDetails.CurrentCell.ColumnIndex == 1)
                {
                    colIndex = dgvItemDetails.CurrentCell.ColumnIndex;
                    rowIndex = dgvItemDetails.CurrentCell.RowIndex;
                    AutoCompleteStringCollection CustomSource = new AutoCompleteStringCollection();
                    if (e.Control is TextBox)
                    {
                        if (txtBox != null)
                        {
                            txtBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            txtBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            txtBox.AutoCompleteCustomSource = null;
                            LoyaltyCardGeneratrionService loyaltyCardGeneratrionService =new LoyaltyCardGeneratrionService();
                            txtBox.CharacterCasing = CharacterCasing.Upper;
                            txtBox.AutoCompleteCustomSource.AddRange(loyaltyCardGeneratrionService.GetAllCardNos());
                            txtBox.TextChanged += new EventHandler(ItemTxtBox_TextChanged);


                        }
                    }
                }
                else if(dgvItemDetails.CurrentCell.ColumnIndex == 2)
                {
                    colIndex = dgvItemDetails.CurrentCell.ColumnIndex;
                    rowIndex = dgvItemDetails.CurrentCell.RowIndex;
                    AutoCompleteStringCollection CustomSource = new AutoCompleteStringCollection();
                    if (e.Control is TextBox)
                    {
                        if (txtBox != null)
                        {
                            txtBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            txtBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            txtBox.AutoCompleteCustomSource = null;
                            LoyaltyCustomerService LoyaltyCustomerService =new LoyaltyCustomerService();
                            txtBox.CharacterCasing = CharacterCasing.Upper;
                            txtBox.AutoCompleteCustomSource.AddRange(LoyaltyCustomerService.GetAllLoyaltyCustomerCodes());
                        }
                    }
   
                }
                else if (dgvItemDetails.CurrentCell.ColumnIndex == 3)
                {
                    colIndex = dgvItemDetails.CurrentCell.ColumnIndex;
                    rowIndex = dgvItemDetails.CurrentCell.RowIndex;
                    AutoCompleteStringCollection CustomSource = new AutoCompleteStringCollection();
                    if (e.Control is TextBox)
                    {
                        if (txtBox != null)
                        {
                            txtBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            txtBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            txtBox.AutoCompleteCustomSource = null;
                            LoyaltyCustomerService LoyaltyCustomerService = new LoyaltyCustomerService();

                            txtBox.CharacterCasing = CharacterCasing.Upper;
                            txtBox.AutoCompleteCustomSource.AddRange(LoyaltyCustomerService.GetAllLoyaltyCustomerNames());
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }


        void ItemTxtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {

            if ((sender as TextBox).Text != null && (sender as TextBox).Text.Trim() != "")
            {
                //MessageBox.Show((sender as TextBox).Text);
                  LoyaltyCustomerService lgsCustomerervice = new LoyaltyCustomerService();
                  LoyaltyCardGeneratrionService loyaltyCardGeneratrionService = new LoyaltyCardGeneratrionService();
                  LoyaltyCardGenerationDetail loyaltyCardGenerationDetail = new LoyaltyCardGenerationDetail();
                  existingLoyaltyCustomer = lgsCustomerervice.GetLoyaltyCustomerByCardNo((sender as TextBox).Text);

                  if (existingLoyaltyCustomer != null)
                  {
                      dgvItemDetails["CustomerCode", dgvItemDetails.CurrentCell.RowIndex].Value = existingLoyaltyCustomer.CustomerCode;
                      dgvItemDetails["CustomerName", dgvItemDetails.CurrentCell.RowIndex].Value = existingLoyaltyCustomer.CustomerName;
                  }
                  loyaltyCardGenerationDetail= loyaltyCardGeneratrionService.GetLoyaltyCardGenerationDetailByCardNo((sender as TextBox).Text);
                  if (loyaltyCardGenerationDetail != null)
                  {
                      dgvItemDetails["SerialNo", dgvItemDetails.CurrentCell.RowIndex].Value = loyaltyCardGenerationDetail.SerialNo;
                      dgvItemDetails["EncodeNo", dgvItemDetails.CurrentCell.RowIndex].Value = loyaltyCardGenerationDetail.EncodeNo;
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
                if (!ValidateControls()) { return; }
                LoyaltyCardAllocationService loyaltyCardAllocationService = new LoyaltyCardAllocationService();
                LoyaltyCustomerService LoyaltyCustomerService = new LoyaltyCustomerService();
                bool isNew = false;

                existingLoyaltyCardAllocation = new LoyaltyCardAllocation();
                if (dgvItemDetails.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvItemDetails.Rows)
                    {

                        if (dgvItemDetails["CardType", row.Index].Value != null &&
                            !dgvItemDetails["CardType", row.Index].Value.ToString().Trim().Equals(string.Empty) && Common.ConvertStringToBool(dgvItemDetails["Assign", row.Index].Value.ToString()) == true)
                        {
                            existingLoyaltyCardAllocation.CardTypeId =Common.ConvertStringToLong(dgvItemDetails["CardType", row.Index].Value.ToString());
                            existingLoyaltyCardAllocation.SerialNo = dgvItemDetails["SerialNo", row.Index].Value.ToString();
                            existingLoyaltyCardAllocation.CardNo = dgvItemDetails["CardNo", row.Index].Value.ToString();
                            existingLoyaltyCardAllocation.EncodeNo = dgvItemDetails["EncodeNo", row.Index].Value.ToString();
                            existingLoyaltyCustomer = LoyaltyCustomerService.GetLoyaltyCustomerByCode(dgvItemDetails["CustomerCode", row.Index].Value.ToString());
                            if (existingLoyaltyCustomer != null)
                                existingLoyaltyCardAllocation.CustomerId = existingLoyaltyCustomer.LoyaltyCustomerID;
                             
                            existingLoyaltyCardAllocation.Assign =Common.ConvertStringToBool(dgvItemDetails["Assign", row.Index].Value.ToString());
                            existingLoyaltyCardAllocation.IsDelete = false;
        
                            if (existingLoyaltyCardAllocation.LoyaltyCardAllocationID.Equals(0))
                            {
                                loyaltyCardAllocationService.AddLoyaltyCardAllocation(existingLoyaltyCardAllocation);
                            }
                            else
                            {
                                loyaltyCardAllocationService.UpdateLoyaltyCardAllocation(existingLoyaltyCardAllocation);
                            }

                        }
                    }


                }

                ClearForm();
                InitializeForm();

                Toast.Show(this.Text + " - " + existingLoyaltyCardAllocation.LoyaltyCardAllocationID + " ", Toast.messageType.Information, Toast.messageAction.Save);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateControls()
        {
            //return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, SerialNo, txtName, txtLength, txtFormat, txtStartingNo, txtQty);
            return true;
        }

        public void FormLoad(object sender, EventArgs e)
        {
            try
            {

                dgvItemDetails.AutoGenerateColumns = false;
                chkAutoCompleationCustomer.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                dgvItemDetails.AutoGenerateColumns = false;
                AddCustomCoulmnstoGridCardType();
                base.FormLoad();
                btnSave.Enabled = true;
                dgvItemDetails.CellEnter += new DataGridViewCellEventHandler(dgvItemDetails_CellEnter);

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }    
        }


        void dgvItemDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                DataGridView dg = (DataGridView)sender;

                if (dg.CurrentCell.EditType == typeof(DataGridViewComboBoxEditingControl))
                {
                    SendKeys.Send("{F4}");
                }
             }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
    }
}
