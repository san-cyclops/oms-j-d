using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Domain;
using Report;
using Report.Inventory;
using Report.Inventory.Reference.Reports;
using Utility;
using Service;
using System.Linq;
using System.IO;
namespace UI.Windows
{
    public partial class FrmLogisticTransactionSearch : Form
    {
        int requestInboxCount;
        string mrnDocumentNo;
        bool inBoxClicked;
        string formText;

        public FrmLogisticTransactionSearch()
        {
            InitializeComponent();
        }

        private void FrmLogisticTransactionSearch_Load(object sender, EventArgs e)
        {
            try
            {
                dgvMain.AutoGenerateColumns = false;
                dgvViewedRequestions.AutoGenerateColumns = false;
                dgvPendingPO.AutoGenerateColumns = false;

                lblDate.Text = "Date : " + DateTime.Now.ToShortDateString();
                lblUser.Text = "Logged User : " + Common.LoggedUser.Trim();
                timer1.Start();

                inBoxClicked = true;
                formText = this.Text;
                LoadMaterialRequestionsInbox();
                LoadViewedMaterialRequestionsInbox();
                LoadPendingPurchaseOrders();
                RefreshCounts();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void LoadMaterialRequestionsInbox() 
        {
            List<LgsMaterialRequestHeader> lgsMaterialRequestHeaderList = new List<LgsMaterialRequestHeader>();
            LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();

            lgsMaterialRequestHeaderList = lgsMaterialRequestService.GetMaterialRequestionsInbox();
            dgvMain.DataSource = null;
            dgvMain.DataSource = lgsMaterialRequestHeaderList;
            dgvMain.Refresh();
        }

        public void LoadViewedMaterialRequestionsInbox() 
        {
            List<LgsMaterialRequestHeader> lgsMaterialRequestHeaderList = new List<LgsMaterialRequestHeader>();
            LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();

            lgsMaterialRequestHeaderList = lgsMaterialRequestService.GetViewedMaterialRequestions();
            dgvViewedRequestions.DataSource = null;
            dgvViewedRequestions.DataSource = lgsMaterialRequestHeaderList;
            dgvViewedRequestions.Refresh();
        }

        public void LoadPendingPurchaseOrders()
        {
            List<LgsPurchaseOrderHeader> lgsPurchaseOrderHeaderList = new List<LgsPurchaseOrderHeader>();
            LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();

            lgsPurchaseOrderHeaderList = lgsMaterialRequestService.GetPendingPurchaseOrders();
            dgvPendingPO.DataSource = null;
            dgvPendingPO.DataSource = lgsPurchaseOrderHeaderList;
            dgvPendingPO.Refresh();
        }

        public void RefreshCounts()
        {
            LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();
            lblInbox.Text = "("+lgsMaterialRequestService.RequestInboxCount().ToString()+")";
            lblOutbox.Text = "(" + lgsMaterialRequestService.RequestOutboxCount().ToString() + ")";
            lblPending.Text = "(" + lgsMaterialRequestService.PendingRequestCount().ToString() + ")";
            lblViewedRequestions.Text = "(" + lgsMaterialRequestService.ViewedRequestCount().ToString() + ")";
            lblPendingPO.Text = "(" + lgsMaterialRequestService.PendingPurchaseOrderCount().ToString() + ")";
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Toast.Show("Do you want to Close this form?", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.Yes))
                {
                    this.Close();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
               Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnPendingPurchaseOrder_Click(object sender, EventArgs e)
        {
            try
            {
                inBoxClicked = false;
                this.Text = formText + " " + "(" + btnPendingPurchaseOrder.Text + ")";
                tabSummery.SelectedTab = tbpPendingPO;
                LoadPendingPurchaseOrders();
                RefreshCounts();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnPurchaseOrder_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLogisticPurchaseOrder frmLogisticPurchaseOrder = new FrmLogisticPurchaseOrder();
                frmLogisticPurchaseOrder.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnRequestionInbox_Click(object sender, EventArgs e)
        {
            try
            {
                inBoxClicked = true ;
                this.Text = formText + " " + "(" + btnRequestionInbox.Text + ")";
                //if (tabSummery.SelectedIndex != 1)
                //{
                //    tabSummery.SelectedTab = tbpMain;
                //}
                tabSummery.SelectedTab = tbpMain;
                LoadMaterialRequestionsInbox();
                RefreshCounts();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvMain_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (inBoxClicked)
                {
                    if (dgvMain["DocumentNo", dgvMain.CurrentCell.RowIndex].Value != null)
                    {
                        mrnDocumentNo = dgvMain["DocumentNo", dgvMain.CurrentCell.RowIndex].Value.ToString();

                        LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();
                        lgsMaterialRequestService.UpdateViewedMrnDocuments(mrnDocumentNo);

                        RefreshCounts();

                        FrmMaterialAllocationNote frmMaterialAllocationNote = new FrmMaterialAllocationNote(mrnDocumentNo);
                        frmMaterialAllocationNote.ShowDialog();

                        LoadMaterialRequestionsInbox();
                        RefreshCounts();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnRequestionOutbox_Click(object sender, EventArgs e)
        {
            try
            {
                inBoxClicked = false;
                this.Text = formText + " " + "(" + btnRequestionOutbox.Text + ")";
                tabSummery.SelectedTab = tbpMain;
                LoadMaterialRequestionsOutbox();
                RefreshCounts();
            }
            catch (Exception ex)
            {
               Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void LoadMaterialRequestionsOutbox()
        {
            List<LgsMaterialRequestHeader> lgsMaterialRequestHeaderList = new List<LgsMaterialRequestHeader>();
            LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();

            lgsMaterialRequestHeaderList = lgsMaterialRequestService.GetMaterialRequestionsOutbox();
            dgvMain.DataSource = null;
            dgvMain.DataSource = lgsMaterialRequestHeaderList;
            dgvMain.Refresh();
        }

        private void btnPendingRequestion_Click(object sender, EventArgs e)
        {
            try
            {
                inBoxClicked = true;
                this.Text = formText + " " + "(" + btnPendingRequestion.Text + ")";
                tabSummery.SelectedTab = tbpMain;
                LoadPendingRequestions();
                RefreshCounts();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void LoadPendingRequestions()  
        {
            List<LgsMaterialRequestHeader> lgsMaterialRequestHeaderList = new List<LgsMaterialRequestHeader>();
            LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();

            lgsMaterialRequestHeaderList = lgsMaterialRequestService.GetPendingRequestions();
            dgvMain.DataSource = null;
            dgvMain.DataSource = lgsMaterialRequestHeaderList;
            dgvMain.Refresh();
        }

        private void btnExpiredRequestion_Click(object sender, EventArgs e)
        {
            try
            {
                inBoxClicked = false;
                this.Text = formText + " " + "(" + btnExpiredRequestion.Text + ")";
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnGoodsReceivedNote_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLogisticGoodsReceivedNote frmLogisticGoodsReceivedNote = new FrmLogisticGoodsReceivedNote();
                frmLogisticGoodsReceivedNote.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnTransferOfGoodsNote_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLogisticTransferOfGoodsNote frmLogisticTransferOfGoodsNote = new FrmLogisticTransferOfGoodsNote();
                frmLogisticTransferOfGoodsNote.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnMaterialRequestNote_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMaterialRequestNote frmMaterialRequestNote = new FrmMaterialRequestNote();
                frmMaterialRequestNote.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnMaterialAllocationNote_Click(object sender, EventArgs e)
        {
            FrmMaterialAllocationNote frmMaterialAllocationNote = new FrmMaterialAllocationNote();
            frmMaterialAllocationNote.ShowDialog();
        }

        private void dgvViewedRequestions_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvViewedRequestions["DocumentNoViewed", dgvViewedRequestions.CurrentCell.RowIndex].Value != null)
                {
                    mrnDocumentNo = dgvViewedRequestions["DocumentNoViewed", dgvViewedRequestions.CurrentCell.RowIndex].Value.ToString();
                    FrmMaterialAllocationNote frmMaterialAllocationNote = new FrmMaterialAllocationNote(mrnDocumentNo);
                    frmMaterialAllocationNote.ShowDialog();

                    LoadViewedMaterialRequestionsInbox();
                    RefreshCounts();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnViewedRequestions_Click(object sender, EventArgs e)
        {
            try
            {
                inBoxClicked = false;
                this.Text = formText + " " + "(" + btnViewedRequestions.Text + ")";
                tabSummery.SelectedTab = tbpViewedRequestions;
                LoadViewedMaterialRequestionsInbox();
                RefreshCounts();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnPurchaseReturnNote_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLogisticPurchaseReturnNote frmLogisticPurchaseReturnNote = new FrmLogisticPurchaseReturnNote();
                frmLogisticPurchaseReturnNote.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLogisticDepartment frmLogisticDepartment = new FrmLogisticDepartment();
                frmLogisticDepartment.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLogisticCategory frmLogisticCategory = new FrmLogisticCategory();
                frmLogisticCategory.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSubCategory_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLogisticSubCategory frmLogisticSubCategory = new FrmLogisticSubCategory();
                frmLogisticSubCategory.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSubCategory2_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLogisticSubCategory2 frmLogisticSubCategory2 = new FrmLogisticSubCategory2();
                frmLogisticSubCategory2.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnProductMaster_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLogisticProduct frmLogisticProduct = new FrmLogisticProduct();
                frmLogisticProduct.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLogisticSupplier frmLogisticSupplier = new FrmLogisticSupplier();
                frmLogisticSupplier.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToShortTimeString();
        }

    }
}
