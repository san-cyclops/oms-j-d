using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Domain;
using Service;
using Utility;

namespace UI.Windows
{
    /// <summary>
    /// Numbers Generation
    /// Card Number - length=11, prefix=Location Code, prefix length=3, IsCardNoOtherPrefix=True
    /// Serial Number - length=8, prefix=AA(AA,AB,AC.......), prefix length=2, IsSerialOtherPrefix=False
    ///                 Update column 'SerialPrefix' in table 'CardGenerationSetting' with last prefix generted.
    /// Encoded Number - length=11, prefix=LocationPrefix+0, prefix length=3, IsEncodeOtherPrefix=True                  
    /// </summary>
    public partial class FrmCardNoGeneration : UI.Windows.FrmBaseMasterForm
    {
        private LoyaltyCardGenerationHeader existingCardGenerationHeader;
        private LoyaltyCardGenerationDetail existingCardGenerationDetail;
        List<LoyaltyCardGenerationDetailTemp> loyaltyCardGenerationDetailTemps = new List<LoyaltyCardGenerationDetailTemp>();
        private CardGenerationSetting cardGenerationSetting = new CardGenerationSetting();
        private Location existingLocation;
        private CardMaster existingCardMaster;

        public FrmCardNoGeneration()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidateControls())
                {
                    return;
                }

                dgvDisplay.DataSource = null;
                dgvDisplay.DataSource = GenerateCardNOs(txtCardPrefix.Text.ToString(), Common.ConvertStringToInt(txtCardLength.Text.ToString()), Common.ConvertStringToInt(txtCardStartingNo.Text.ToString()), txtSerialPrefix.Text.ToString(), Common.ConvertStringToInt(txtSerialLength.Text.ToString()), Common.ConvertStringToInt(txtSerialStartingNo.Text.ToString()), txtEncodePrefix.Text.ToString(), Common.ConvertStringToInt(txtEncodeLength.Text.ToString()), Common.ConvertStringToInt(txtEncodeStartingNo.Text.ToString()), Common.ConvertStringToInt(txtQty.Text.ToString()));

                if (loyaltyCardGenerationDetailTemps.Any(cn => cn.IsSerialExists.Equals(true)))
                {
                    Toast.Show("Some Serial numbers are already exists. Please check the serial numbers before you save new card numbers.", Toast.messageType.Information, Toast.messageAction.General);
                    btnSave.Enabled = false;
                    gbSerial.Enabled = true;
                    return;
                }
                else
                {
                    btnSave.Enabled = true;
                    gbSerial.Enabled = false;
                    txtCode.Enabled = false;
                    txtName.Enabled = false;
                    txtQty.Enabled = false;
                    btnGenerate.Enabled = false;
                    cmbLocation.Enabled = false;
                }
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private List<LoyaltyCardGenerationDetailTemp> GenerateCardNOs(string Cardprefix, int CardLength, int CardstartingNo, string Serialprefix, int SerialLength, int SerialstartingNo, string Encodeprefix, int EncodeLength, int EncodestartingNo, int qty)
        {
            LoyaltyCardGeneratrionService loyaltyCardGeneratrionService = new LoyaltyCardGeneratrionService();
            //int No = CardstartingNo;
            loyaltyCardGenerationDetailTemps.Clear();
            for (int i = 1; i < qty + 1; i++)
            {
                //if (chkActiveEncode.Checked)
                //{
                    string serialNo = GetCodeFormat("SN", cardGenerationSetting);
                    var tc = new LoyaltyCardGenerationDetailTemp()
                    {
                        CardNo = GetCodeFormat("CN", cardGenerationSetting),
                        SerialNo = serialNo,
                        EncodeNo = GetCodeFormat("EN", cardGenerationSetting),
                        CardStartingNo = cardGenerationSetting.CardStartingNo,
                        SerialStartingNo = cardGenerationSetting.SerialStartingNo,
                        EncodeStartingNo = cardGenerationSetting.EncodeStartingNo,
                        IsSerialExists = loyaltyCardGeneratrionService.GetLoyaltyCardGenerationDetailBySerialNo(serialNo) == null ? false : true
                    };
                    loyaltyCardGenerationDetailTemps.Add(tc);
                //}
                //else
                //{
                //    string serialNo = GetCodeFormat("SN", cardGenerationSetting);
                //    var tc = new LoyaltyCardGenerationDetailTemp()
                //    {
                //        CardNo = GetCodeFormat("CN", cardGenerationSetting),
                //        SerialNo = serialNo,
                //        EncodeNo = "",
                //        CardStartingNo = cardGenerationSetting.CardStartingNo,
                //        SerialStartingNo = cardGenerationSetting.SerialStartingNo,
                //        EncodeStartingNo = cardGenerationSetting.EncodeStartingNo,
                //        IsSerialExists = loyaltyCardGeneratrionService.GetLoyaltyCardGenerationDetailBySerialNo(serialNo) == null ? false : true
                //    };
                //    loyaltyCardGenerationDetailTemps.Add(tc);

                //}

                //No = No + 1;
                //CardstartingNo = CardstartingNo + 1;
                //SerialstartingNo = SerialstartingNo + 1;
                //EncodestartingNo = EncodestartingNo + 1;
                cardGenerationSetting.CardStartingNo++;
                cardGenerationSetting.SerialStartingNo++;
                cardGenerationSetting.EncodeStartingNo++;
            }
            return loyaltyCardGenerationDetailTemps;
        }

        private bool IsValidateControls()
        {
            LoyaltyCardGeneratrionValidator cardGeneratrionValidator = new LoyaltyCardGeneratrionValidator();
            bool isValidated = false;
            if (!cardGeneratrionValidator.ValidateLength(Common.ConvertStringToInt(txtCardLength.Text.Trim()), string.Concat(txtSerialPrefix.Text.Trim(), txtCardStartingNo.Text.Trim())))
            {
                //string text = lblSerialLength.Text.Replace(lblSerialLength.Text.Substring((lblSerialLength.Text.Length - 1), 1), "");

                Toast.Show("Not Validate Length", Toast.messageType.Information, Toast.messageAction.Length);
                isValidated = false;
            }
            else
            { isValidated = true; }

            return isValidated;
        }

        public override void FormLoad()
        {
            try
            {
                InitializeForm();

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
                CardMasterService cardMasterService = new CardMasterService();
                Common.ClearForm(this);
                Common.SetAutoComplete(txtCode,cardMasterService.GetAllCardCodes(),chkAutoCompleationCard.Checked);
                Common.SetAutoComplete(txtName, cardMasterService.GetAllCardNames(), chkAutoCompleationCard.Checked);

                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
                cmbLocation.SelectedValue = Common.LoggedLocationID;

                Common.EnableComboBox(true, cmbLocation);
                Common.EnableButton(true, btnDelete, btnGenerate);
                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtCode,txtName, txtQty);
                
                LoadCardGenerationSetting();
                txtCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadCardGenerationSetting()
        {
            try
            {
                LoyaltyCardGeneratrionService loyaltyCardGeneratrionService = new LoyaltyCardGeneratrionService();
                cardGenerationSetting = loyaltyCardGeneratrionService.GetCardGenerationSetting();
                LocationService locationService = new LocationService();
                existingLocation = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                this.txtSerialPrefix.TextChanged -= new System.EventHandler(this.txtSerialPrefix_TextChanged);
                this.txtSerialLength.TextChanged -= new System.EventHandler(this.txtSerialLength_TextChanged);
                this.txtSerialStartingNo.TextChanged -= new System.EventHandler(this.txtSerialStartingNo_TextChanged);
                this.txtEncodePrefix.TextChanged -= new System.EventHandler(this.txtEncodePrefix_TextChanged);
                this.txtEncodeLength.TextChanged -= new System.EventHandler(this.txtEncodeLength_TextChanged);
                this.txtEncodeStartingNo.TextChanged -= new System.EventHandler(this.txtEncodeStartingNo_TextChanged);

                txtCardLength.Text = cardGenerationSetting.CardLength.ToString().Trim();
                txtCardStartingNo.Text = cardGenerationSetting.CardStartingNo.ToString().Trim();

                txtSerialLength.Text = cardGenerationSetting.SerialLength.ToString().Trim();
                txtSerialStartingNo.Text = cardGenerationSetting.SerialStartingNo.ToString().Trim();

                txtEncodeLength.Text = cardGenerationSetting.EncodeLength.ToString().Trim();
                txtEncodeStartingNo.Text = cardGenerationSetting.EncodeStartingNo.ToString().Trim();


                if (cardGenerationSetting.IsCardNoOtherPrefix)
                {txtCardPrefix.Text = existingLocation.LocationCode.Trim();
                }
                else
                { txtCardPrefix.Text = cardGenerationSetting.CardNoPrefix;
                }

                if (cardGenerationSetting.IsSerialOtherPrefix)
                { txtSerialPrefix.Text = "";
                }
                else
                { txtSerialPrefix.Text = cardGenerationSetting.SerialPrefix.Trim();                            
                }
                
                if (cardGenerationSetting.IsEncodeOtherPrefix)
                { txtEncodePrefix.Text = existingLocation.LocationPrefixCode.Trim().Length < 3 ? existingLocation.LocationPrefixCode.Trim().PadRight(3, '0') : existingLocation.LocationPrefixCode.Trim();
                }
                else
                { txtEncodePrefix.Text = cardGenerationSetting.EncodePrefix;
                }

                // CN-Card No, SN-Serial No, EN-Encoded No 
                txtCardFormat.Text = GetCodeFormat("CN", cardGenerationSetting);//.IsCardNoOtherPrefix ? string.Empty: txtCardPrefix.Text.Trim(), Common.ConvertStringToInt(txtCardLength.Text),
                              //Common.ConvertStringToInt(txtCardStartingNo.Text));

                txtSerialFormat.Text = GetCodeFormat("SN", cardGenerationSetting);//.IsSerialOtherPrefix ? string.Empty : txtSerialPrefix.Text.Trim(), Common.ConvertStringToInt(txtSerialLength.Text),
                              //Common.ConvertStringToInt(txtSerialStartingNo.Text));

                txtEncodeFormat.Text = GetCodeFormat("EN", cardGenerationSetting);//.IsEncodeOtherPrefix ? string.Empty : txtEncodePrefix.Text.Trim(), Common.ConvertStringToInt(txtEncodeLength.Text),
                                           //Common.ConvertStringToInt(txtEncodeStartingNo.Text));

                gbEncodeSetting.Enabled = false;
                //chkActiveEncode.Checked = false;

                this.txtSerialPrefix.TextChanged += new System.EventHandler(this.txtSerialPrefix_TextChanged);
                this.txtSerialLength.TextChanged += new System.EventHandler(this.txtSerialLength_TextChanged);
                this.txtSerialStartingNo.TextChanged += new System.EventHandler(this.txtSerialStartingNo_TextChanged);
                this.txtEncodePrefix.TextChanged += new System.EventHandler(this.txtEncodePrefix_TextChanged);
                this.txtEncodeLength.TextChanged += new System.EventHandler(this.txtEncodeLength_TextChanged);
                this.txtEncodeStartingNo.TextChanged += new System.EventHandler(this.txtEncodeStartingNo_TextChanged);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeType" : 'CN'-Card No, 'SN'-Serial No, 'EN'-Encoded No> </param>
        /// <param name="cardGenerationSetting"></param>
        /// <returns></returns>
        private string GetCodeFormat(string codeType, CardGenerationSetting cardGenerationSetting)//string prefix, int length, int startNo)
        {
            string Format = string.Empty;
            int suffix=0;
            LocationService locationService = new LocationService();                
            existingLocation = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

            switch (codeType)
            {
                case "CN":
                    if (cardGenerationSetting.CardLength <= 0)
                    { 
                        Toast.Show("Card number length ", Toast.messageType.Information, Toast.messageAction.NotFound);
                        break;
                    }

                    if (cardGenerationSetting.IsCardNoOtherPrefix)
                    {
                        suffix = (cardGenerationSetting.CardLength - existingLocation.LocationCode.Trim().Length); 
                        //Format = String.Format("{0}{1," + suffix + ":D" + suffix + "} ", existingLocation.LocationCode, cardGenerationSetting.CardStartingNo);
                        Format = String.Format("{0}{1}", existingLocation.LocationCode.Trim(), cardGenerationSetting.CardStartingNo.ToString().PadLeft(suffix, '0'));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(cardGenerationSetting.CardNoPrefix.Trim()))
                        {
                            Toast.Show("Card number prefix ", Toast.messageType.Information, Toast.messageAction.NotFound);
                            break;
                        }
                        else
                        {
                            txtCardPrefix.Text = cardGenerationSetting.CardNoPrefix.Trim();
                            suffix = (cardGenerationSetting.CardLength - cardGenerationSetting.CardNoPrefix.Trim().Length); 
                            //Format = String.Format("{0}{1," + suffix + ":D" + suffix + "} ", cardGenerationSetting.CardNoPrefix, cardGenerationSetting.CardStartingNo);
                            Format = String.Format("{0}{1}", cardGenerationSetting.CardNoPrefix.Trim(), cardGenerationSetting.CardStartingNo.ToString().PadLeft(suffix, '0'));
                        }
                    }
                break;

                case "SN":
                   if (cardGenerationSetting.SerialLength <= 0)
                    { 
                        Toast.Show("Serial number length ", Toast.messageType.Information, Toast.messageAction.NotFound);
                        break;
                    }

                    if (cardGenerationSetting.IsSerialOtherPrefix)
                    {
                        string prefix = string.Empty;

                        suffix = (cardGenerationSetting.SerialLength - prefix.Length);
                        Format = String.Format("{0}{1}", prefix, cardGenerationSetting.SerialStartingNo.ToString().PadLeft(suffix, '0'));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(cardGenerationSetting.SerialPrefix.Trim()))
                        {
                            Toast.Show("Serial number prefix ", Toast.messageType.Information, Toast.messageAction.NotFound);
                            break;
                        }
                        else
                        {
                            suffix = (cardGenerationSetting.SerialLength - cardGenerationSetting.SerialPrefix.Trim().Length);
                            //string st = string.Empty.PadLeft(suffix, '9');
                            if (cardGenerationSetting.SerialStartingNo.Equals(int.Parse(string.Empty.PadLeft(suffix, '9'))+1))
                            {
                                char firstChar = char.Parse(cardGenerationSetting.SerialPrefix.Trim().Substring(0, 1)); 
                                char secondChar = char.Parse(cardGenerationSetting.SerialPrefix.Trim().Substring(1, 1));

                                // review this section
                                if (secondChar < 'Z')
                                {
                                    secondChar++;
                                    cardGenerationSetting.SerialPrefix = firstChar.ToString() + secondChar.ToString();
                                }
                                else
                                {
                                    if (firstChar < 'Z')
                                    {
                                        firstChar++;
                                        secondChar = 'A';
                                        cardGenerationSetting.SerialPrefix = firstChar.ToString() + secondChar.ToString();
                                    }
                                    else
                                    {
                                        Toast.Show("Maximum number of Serial Numbers has been reached", Toast.messageType.Information, Toast.messageAction.General);
                                       // and return    
                                        break;
                                    }
                                }

                                cardGenerationSetting.SerialStartingNo = 1;
                            }

                            //cardGenerationSetting.SerialPrefix = prefix.Trim();
                            //txtSerialPrefix.Text = cardGenerationSetting.SerialPrefix.Trim();
                            
                            Format = String.Format("{0}{1}", cardGenerationSetting.SerialPrefix.Trim(), cardGenerationSetting.SerialStartingNo.ToString().PadLeft(suffix, '0'));
                        }
                    }

                break;

                case "EN":
                    if (cardGenerationSetting.EncodeLength <= 0)
                    {
                        Toast.Show("Encode number length ", Toast.messageType.Information, Toast.messageAction.NotFound);
                        break;
                    }

                    if (cardGenerationSetting.IsEncodeOtherPrefix)
                    {
                        string prefix = existingLocation.LocationPrefixCode.Trim().Length < 3 ? existingLocation.LocationPrefixCode.Trim().PadRight(3, '0') : existingLocation.LocationPrefixCode.Trim();
                        suffix = (cardGenerationSetting.EncodeLength - prefix.Trim().Length);
                        Format = String.Format("{0}{1}", prefix.Trim(), cardGenerationSetting.EncodeStartingNo.ToString().PadLeft(suffix,'0'));                    
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(cardGenerationSetting.CardNoPrefix.Trim()))
                        {
                            Toast.Show("Card number prefix ", Toast.messageType.Information, Toast.messageAction.NotFound);
                            break;
                        }
                        else
                        {
                            suffix = (cardGenerationSetting.CardLength - cardGenerationSetting.CardNoPrefix.Trim().Length); 
                            Format = String.Format("{0}{1}", cardGenerationSetting.CardNoPrefix.Trim(), cardGenerationSetting.EncodeStartingNo.ToString().PadLeft(suffix, '0'));
                        }
                    }

                break;
                default:
                    break;
            }

            return Format;
        }

        public override void ClearForm()
        {
            try
            {
                dgvDisplay.DataSource = null;
                if (dgvDisplay.Rows.Count > 1)
                {
                    foreach (DataGridViewRow dr in dgvDisplay.Rows)
                        dgvDisplay.Rows.Remove(dr);
                }

                gbSerial.Enabled = true;
                base.ClearForm();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private bool ValidateControls()
        {
            return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtCode, txtName, txtCardLength, txtCardFormat, txtCardStartingNo, txtQty);
        }

        public override void Save()
        {
            try
            {
                if (loyaltyCardGenerationDetailTemps.Any(cn => cn.IsSerialExists.Equals(true)))
                {
                    Toast.Show("Some Serail numbers are already exists. Please check the serial numbers before you save new card numbers.", Toast.messageType.Information, Toast.messageAction.General);
                    return;
                }

                if ((Toast.Show("Generated Cards", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.No)))
                {
                    return;
                }

                if (!ValidateControls()) { return; }
                LoyaltyCardGeneratrionService loyaltyCardGeneratrionService = new LoyaltyCardGeneratrionService();

                bool isNew = false;

                FillLoyaltyCardGeneration();

                loyaltyCardGeneratrionService.Save(existingCardGenerationHeader, loyaltyCardGenerationDetailTemps);

                ClearForm();
                InitializeForm();

                if (Toast.Show(this.Text + " - " + existingCardGenerationHeader.CardPrefix + " - " + existingCardGenerationHeader.CardStartingNo + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.No))
                {
                    btnClose.PerformClick();
                }
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private LoyaltyCardGenerationHeader FillLoyaltyCardGeneration()
        {
            try
            {
                #region Header
                existingCardGenerationHeader = new LoyaltyCardGenerationHeader();
                LocationService locationService = new LocationService();
                CardMasterService cardMasterService = new CardMasterService();

                existingCardGenerationHeader.CardPrefix = txtCardPrefix.Text.Trim();
                existingCardGenerationHeader.CardLength = Common.ConvertStringToInt(txtCardLength.Text.Trim());
                existingCardGenerationHeader.CardStartingNo = Common.ConvertStringToInt(txtCardStartingNo.Text.Trim());
                existingCardGenerationHeader.SerialPrefix = txtSerialPrefix.Text.Trim();
                existingCardGenerationHeader.SerialLength = Common.ConvertStringToInt(txtSerialLength.Text.Trim());
                existingCardGenerationHeader.SerialStartingNo = Common.ConvertStringToInt(txtSerialStartingNo.Text.Trim());
                existingCardGenerationHeader.EncodePrefix = txtEncodePrefix.Text.Trim();
                existingCardGenerationHeader.EncodeLength = Common.ConvertStringToInt(txtEncodeLength.Text.Trim());
                existingCardGenerationHeader.EncodeStartingNo = Common.ConvertStringToInt(txtEncodeStartingNo.Text.Trim());

                existingCardGenerationHeader.GeneratedDate = dtpDate.Value;
                existingCardGenerationHeader.IsDelete = false;

                existingLocation = locationService.GetLocationsByName(cmbLocation.Text.Trim());
                if (existingLocation != null)
                    existingCardGenerationHeader.LocationID = existingLocation.LocationID;


                existingCardMaster = cardMasterService.GetCardMasterByCode(txtCode.Text.Trim());
                if (existingCardMaster != null)
                    existingCardGenerationHeader.CardMasterID = existingCardMaster.CardMasterID;


                //existingCardGenerationHeader.StartingNo = Common.ConvertStringToInt(txtCardStartingNo.Text.Trim());
                #endregion

                #region Details


                //List<LoyaltyCardGenerationDetailTemp> loyaltyCardGenerationDetailsTemp = GenerateCardNOs(txtSerialPrefix.Text.ToString(),
                //                                    Common.ConvertStringToInt(txtCardLength.Text.ToString()),
                //                                    Common.ConvertStringToInt(txtCardStartingNo.Text.ToString()),
                //                                    Common.ConvertStringToInt(txtQty.Text.ToString()));

                //foreach (var loyaltyCardGenerationDetailTemp in loyaltyCardGenerationDetailsTemp)
                //{
                //    var tc = new LoyaltyCardGenerationDetailTemp
                //    {
                //        SerialNo = loyaltyCardGenerationDetailTemp.SerialNo,
                //    };
                //    loyaltyCardGenerationDetailsTemp.Add(tc);
                //}

            
                #endregion

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            return existingCardGenerationHeader;
        }

        public override void Delete()
        {
            try
            {
                if (Toast.Show("Card Format  - " + txtCardFormat.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                    return;

                LoyaltyCardGenerationDetail loyaltyCardGenerationDetail = new LoyaltyCardGenerationDetail();
                //existingCardGenerationDetail = loyaltyCardGenerationDetail.GeneratedDate Date(txtCustomerCode.Text.Trim());

                //if (existingLgsCustomer != null && existingLgsCustomer.LoyaltyCustomerID != 0)
                //{
                //    existingLgsCustomer.IsDelete = true;
                //    lgsCustomerervice.UpdateLgsCustomer(existingLgsCustomer);
                //    ClearForm();
                //    Toast.Show("Loyalty Customer  - " + existingLgsCustomer.CustomerCode + " - " + existingLgsCustomer.CustomerName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                //    txtCustomerCode.Focus();
                //}
                //else
                //    Toast.Show("Loyalty Customer  - " + existingLgsCustomer.CustomerCode + " - " + existingLgsCustomer.CustomerName + "", Toast.messageType.Warning, Toast.messageAction.NotExists);


            }
            catch (Exception ex)
            {
                    Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
  
        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtName);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtQty);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {
            try
            {
                CardMasterService cardMasterService = new CardMasterService();
                existingCardMaster = cardMasterService.GetCardMasterByCode(txtCode.Text.Trim());

                if (existingCardMaster != null)
                {
                    txtName.Text = existingCardMaster.CardName.Trim();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }            
            
        }

        private void txtName_Leave(object sender, EventArgs e)
        {

            try
            {
                CardMasterService cardMasterService = new CardMasterService();

                existingCardMaster = cardMasterService.GetCardMasterByName(txtName.Text.Trim());

                if (existingCardMaster != null)
                {
                    txtCode.Text = existingCardMaster.CardCode.Trim();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }
        
        private void txtPrefix_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                txtCardFormat.Text = GetCodeFormat("SN", cardGenerationSetting);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
               
  
        }
       
                
        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    if (cmbLocation.Text != null)
                    {
                        LocationService locationService = new LocationService();
                        existingLocation =
                            locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                        if (existingLocation != null)
                            txtCardPrefix.Text = existingLocation.LocationCode.Trim().Substring(0, 2);

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            
        }

        private void txtSerialPrefix_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtSerialLength);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEncodePrefix_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtEncodeLength);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
                        
        }
        
        private void txtSerialPrefix_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtSerialFormat.Text = GetCodeFormat("SN", cardGenerationSetting);
             
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSerialLength_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtSerialFormat.Text = GetCodeFormat("SN", cardGenerationSetting);
             
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSerialStartingNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtSerialFormat.Text = GetCodeFormat("SN", cardGenerationSetting);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSerialLength_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtSerialStartingNo);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSerialStartingNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (chkActiveEncode.Checked)
                //{
                //    Common.SetFocus(e, txtEncodePrefix);
                //}
                //else
                //{
                    Common.SetFocus(e, txtCode);
                //}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEncodePrefix_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtEncodeFormat.Text = GetCodeFormat("EN", cardGenerationSetting);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }             
        }

        private void txtEncodeLength_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtEncodeStartingNo);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEncodeLength_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtEncodeFormat.Text = GetCodeFormat("EN", cardGenerationSetting);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }   
        }

        private void txtEncodeStartingNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtCode);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEncodeStartingNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtEncodeFormat.Text = GetCodeFormat("EN", cardGenerationSetting);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }   
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    btnGenerate.PerformClick();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvDisplay_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex <= 0)
                { return; }

                //e.CellStyle.BackColor = Color.FromArgb(int.Parse(((DataRowView)dgvDisplay.Rows[e.RowIndex].DataBoundItem).Row[4].ToString()));
                //e.CellStyle.BackColor = dgvDisplay.Rows[e.RowIndex].Cells[3].Value.Equals(true) ? Color.Pink : Color.White;
                dgvDisplay.Rows[e.RowIndex].DefaultCellStyle.BackColor = dgvDisplay.Rows[e.RowIndex].Cells[3].Value.Equals(true) ? Color.Yellow : Color.White;
                

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        

    }
}
