using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;
using System.Reflection;
using Utility;

namespace UI.Windows
{
    public partial class FrmLoyaltyCustomerGrid : Form
    {
        string nicNo;
        string customerCode;
        string cardNo;
        bool isLostAndRenew;

        public FrmLoyaltyCustomerGrid()
        {
            InitializeComponent();
        }

        public FrmLoyaltyCustomerGrid(List<LoyaltyCustomer> loyaltyCustomerList)
        {
            InitializeComponent();
            dgvLoyalityCustomerGrid.AutoGenerateColumns = false;
            dgvLoyalityCustomerGrid.DataSource = loyaltyCustomerList;
            dgvLoyalityCustomerGrid.Refresh();
        }

        public FrmLoyaltyCustomerGrid(List<LoyaltyCustomer> loyaltyCustomerList, bool isLostRenew)
        {
            InitializeComponent();
            dgvLoyalityCustomerGrid.AutoGenerateColumns = false;
            dgvLoyalityCustomerGrid.DataSource = loyaltyCustomerList;
            dgvLoyalityCustomerGrid.Refresh();
            isLostAndRenew = isLostRenew;
        }

        private void CloseForm()
        {
            this.Close();
            this.Dispose();
        }

        private void dgvLoyalityCustomerGrid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvLoyalityCustomerGrid.CurrentCell.RowIndex >= 0)
                {
                    if (dgvLoyalityCustomerGrid["NicNo", dgvLoyalityCustomerGrid.CurrentCell.RowIndex].Value == null) { nicNo = string.Empty; }
                    else { nicNo = dgvLoyalityCustomerGrid["NicNo", dgvLoyalityCustomerGrid.CurrentCell.RowIndex].Value.ToString().Trim(); }

                    if (dgvLoyalityCustomerGrid["CardNo", dgvLoyalityCustomerGrid.CurrentCell.RowIndex].Value == null) { cardNo = string.Empty; }
                    else { cardNo = dgvLoyalityCustomerGrid["CardNo", dgvLoyalityCustomerGrid.CurrentCell.RowIndex].Value.ToString().Trim(); }

                    if (dgvLoyalityCustomerGrid["CustomerCode", dgvLoyalityCustomerGrid.CurrentCell.RowIndex].Value == null) { customerCode = string.Empty; }
                    else { customerCode = dgvLoyalityCustomerGrid["CustomerCode", dgvLoyalityCustomerGrid.CurrentCell.RowIndex].Value.ToString().Trim(); }

                    CloseForm();
                   
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void FrmLoyaltyCustomerGrid_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (isLostAndRenew)
                {
                    FrmLostAndRenew frmLostAndRenew = new FrmLostAndRenew();
                    frmLostAndRenew.SetCustomerCode(customerCode);
                }
                else
                {
                    FrmLoyaltyCustomer frmLoyaltyCustomer = new FrmLoyaltyCustomer();
                    frmLoyaltyCustomer.SetCustomerCode(customerCode);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvLoyalityCustomerGrid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    nicNo = string.Empty;
                    cardNo = string.Empty;
                    customerCode = string.Empty;

                    CloseForm();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
