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

namespace UI.Windows
{
    public partial class FrmExchangeRate :Form
    {
        /// <summary>
        /// Sanjeewa
        /// </summary>
        InvSize existingInvSize;
        AutoCompleteStringCollection autoCompleteCode;
        public FrmExchangeRate()
        {
            InitializeComponent();
        }

        

        private void FrmSize_Load(object sender, EventArgs e)
        {
            try
            {
                txtExchangeRate.Text = AutoGenerateInfoService.GetExchangeRate().ToString();
               
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                InvSizeService invSizeService = new InvSizeService();
                bool isNew = false;

              
                if ((Toast.Show("Exchange Rate .", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                {
                    return;
                }
                invSizeService.AddInvSizes(Common.ConvertStringToDecimalCurrency(txtExchangeRate.Text.Trim()));

                txtExchangeRate.Text = AutoGenerateInfoService.GetExchangeRate().ToString();

                Toast.Show("Need To Restart System.", Toast.messageType.Information, Toast.messageAction.General, "Exit");
                this.Close();
                Application.Exit();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

     
 
        

       

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

 
    }
}
