using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Report;
using Report.Com;
using Report.GV;
using Report.Inventory.Transactions.Reports;
using Report.CRM;
using Service;
using Domain;
using UI.Windows;
using UI.Windows.Reports;
using Utility;
using System.Collections;
using Report.Inventory;
using System.Reflection;
using Report.GUI;
using Report.CRM.Reports;
using System.Threading;
using DGVPrinterHelper;


namespace Report.GUI

{
    public partial class FrmOrderSystemFilterReport2 : FrmBaseReportsForm
    {
        public FrmOrderSystemFilterReport2()
        {
            InitializeComponent();
        }

        private void searchToolBar1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void FrmOrderSystemFilterReport_Load(object sender, EventArgs e)
        {

        }

        public override void FormLoad()
        {
            try
            {
               
 

                //LoadSearchCodes();

                base.FormLoad();

            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        public override void Clear()
        {
            
            dgvFilter.DataSource = null;
            dgvFilter.Rows.Clear();
            dgvFilter.Refresh();
            base.Clear();
           
        }

        public override void InitializeForm()
        {
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

            bindingSource1.DataSource =  loyaltyCustomerService.GetActiveOrdersFilterGrid();

            dgvFilter.DataSource = bindingSource1;
            searchToolBar1.SetColumns(dgvFilter.Columns);
            base.InitializeForm();
        }

        private void searchToolBar1_Search(object sender, ADGV.SearchToolBarSearchEventArgs e)
        {
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = dgvFilter.CurrentCell.ColumnIndex + 1 >= dgvFilter.ColumnCount;
                bool endrow = dgvFilter.CurrentCell.RowIndex + 1 >= dgvFilter.RowCount;

                if (endcol && endrow)
                {
                    startColumn = dgvFilter.CurrentCell.ColumnIndex;
                    startRow = dgvFilter.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : dgvFilter.CurrentCell.ColumnIndex + 1;
                    startRow = dgvFilter.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = dgvFilter.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);

            if (c != null)
                dgvFilter.CurrentCell = c;
        }

        private void dgvFilter_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Filter = dgvFilter.FilterString;
            textBox_filter.Text = bindingSource1.Filter;
             
        }

        private void dgvFilter_SortStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Sort = dgvFilter.SortString;
            textBox_sort.Text = bindingSource1.Sort;
        }

        private void btnView_Click(object sender, EventArgs e)
        {

            DGVPrinter printer = new DGVPrinter();

            printer.Title = "J & D Group";

            printer.SubTitle = "Sales Order Report";

            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit |

                                          StringFormatFlags.NoClip;

            printer.PageNumbers = true;

            printer.PageNumberInHeader = false;

            printer.PorportionalColumns = true;
 
            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.Footer = "J & D Group";

            printer.FooterSpacing = 15;



            printer.PrintDataGridView(dgvFilter);
        }
 

    }
}
