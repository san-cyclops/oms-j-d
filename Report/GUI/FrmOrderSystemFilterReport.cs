﻿using System;
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
using System.Threading;
using DGVPrinterHelper;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using Telerik.WinControls.UI.Export.ExcelML; 


namespace Report.GUI

{
    public partial class FrmOrderSystemFilterReport : FrmBaseReportsForm2 
    {
        //class ViewDefinitionInfo
        //{
        //    public List<string> Columns;
        //    public IGridViewDefinition ViewDefinition;
        //    public int RowHeight = 30;
        //    public int HeaderHeight = 30;
        //}

        //ViewDefinitionInfo tableViewInfo;
        //ViewDefinitionInfo htmlViewInfo;
        //ViewDefinitionInfo columnGroupViewInfo;
        //ViewDefinitionInfo currentViewInfo;
        public FrmOrderSystemFilterReport()
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
                this.dgvFilter.AllowAddNewRow = false;
                this.dgvFilter.EnableFiltering = true;
                this.dgvFilter.AllowEditRow = false;
                this.dgvFilter.ShowFilteringRow = false;
                this.dgvFilter.ShowHeaderCellButtons = true;
                this.dgvFilter.ShowGroupedColumns = true;
                this.dgvFilter.AutoExpandGroups = true;
                this.dgvFilter.EnableAlternatingRowColor = true;
                this.dgvFilter.CellFormatting += new CellFormattingEventHandler(dgvFilter_CellFormatting);
                this.dgvFilter.PrintCellFormatting += new PrintCellFormattingEventHandler(dgvFilter_PrintCellFormatting);


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
        private void InitializePrintDocument()
        {
            this.radPrintDocument1.LeftFooter = "Page [Page #] of [Total Pages]";
            this.radPrintDocument1.LeftHeader = "[Time Printed]";
            this.radPrintDocument1.MiddleFooter = "***";
            this.radPrintDocument1.MiddleHeader = "J & D Group";
            this.radPrintDocument1.RightFooter = "Printed by: '"+ Common.LoggedUser +"'";
            this.radPrintDocument1.RightHeader = DateTime.Now.ToShortDateString();
        }
        public override void InitializeForm()
        {
            InitializePrintDocument();
            //this.dgvFilter.GroupDescriptors.Add(new Telerik.WinControls.Data.GroupDescriptor("Title Desc"));
      

            
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

            bindingSource1.DataSource = loyaltyCustomerService.GetActiveOrdersFilterGrid();

            dgvFilter.DataSource = bindingSource1;

            //this.dgvFilter.Columns["OrderNo"].Width = 80;
            //this.dgvFilter.Columns["ReferenceNo"].Width = 80;
            //this.dgvFilter.Columns["DocumentDate"].Width = 120;
            //this.dgvFilter.Columns["DeliverDate"].Width = 80;
            //this.dgvFilter.Columns["Company"].Width = 80;
            //this.dgvFilter.Columns["Pending"].Width = 70;
 

            

            base.InitializeForm();
        }

        private void searchToolBar1_Search(object sender, ADGV.SearchToolBarSearchEventArgs e)
        {
           
        }

        private void dgvFilter_FilterStringChanged(object sender, EventArgs e)
        {
             
             
        }

        private void dgvFilter_SortStringChanged(object sender, EventArgs e)
        {
             
        }

        private void btnView_Click(object sender, EventArgs e)
        {

            this.dgvFilter.Print(true, this.radPrintDocument1);
        }

        private void dgvFilter_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {

        }

        private void dgvFilter_PrintCellFormatting(object sender, Telerik.WinControls.UI.PrintCellFormattingEventArgs e)
        {

        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            //RadPrintPreviewDialog dialog = new RadPrintPreviewDialog(); 
            //dialog.ThemeName = this.dgvFilter.ThemeName; 
            //dialog.Document = this.radPrintDocument1; 
            //dialog.StartPosition = FormStartPosition.CenterScreen; 
            //dialog.ShowDialog();
            dgvFilter.PrintPreview();
        }

        private void dgvFilter_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if ((string)e.RowElement.RowInfo.Cells["ActivePending"].Value == "Active")
            {
                e.RowElement.DrawFill = true;
                e.RowElement.BackColor = Color.Aqua;
            }
            else if ((string)e.RowElement.RowInfo.Cells["ActivePending"].Value == "Pending")
            {
                e.RowElement.DrawFill = true;
                e.RowElement.BackColor = Color.White;
            }
            else if ((string)e.RowElement.RowInfo.Cells["ActivePending"].Value == "ReOrder")
            {
                e.RowElement.DrawFill = true;
                e.RowElement.BackColor = Color.Red;
            }
            else
            {
                e.RowElement.DrawFill = true;
                e.RowElement.BackColor = Color.LimeGreen;

            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog1 = new SaveFileDialog();
            saveDialog1.Filter = "Excel (*.xls)|*.xls";

            if (saveDialog1.ShowDialog(this) == DialogResult.OK)
            {



                Thread thread2 = new Thread(new ParameterizedThreadStart(RunExportToExcelML));
                thread2.Start(saveDialog1.FileName);
            }

        }

        private void RunExportToExcelML(object fileName)
        {
            try
            {
                ExportToExcelML exporter = new ExportToExcelML(this.dgvFilter);
                exporter.ExportVisualSettings = true;
                exporter.SheetMaxRows = ExcelMaxRows._1048576;

                exporter.ExcelCellFormatting += new ExcelCellFormattingEventHandler(exporter_ExcelCellFormatting);
                exporter.ExcelTableCreated += new ExcelTableCreatedEventHandler(exporter_ExcelTableCreated);

                exporter.RunExport(fileName.ToString());

                string text = "Export finished successfully!";
                if (this.InvokeRequired)
                {
                    this.Invoke(new EventHandler(delegate
                    {
                        RadMessageBox.Show(this, text);
                    }));
                }
                else
                {
                    RadMessageBox.Show(this, text);
                }
            }
            catch (System.IO.IOException ex)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new EventHandler(delegate
                    {
                        RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                    }));
                }
                else
                {
                    RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }

        }
        void exporter_ExcelCellFormatting(object sender, ExcelCellFormattingEventArgs e)
        {

            if (e.GridRowInfoType == typeof(GridViewDataRowInfo))
            {
                //update progress bar
                //int position = (int)(100 * (double)e.GridRowIndex / (double)(this.dgvFilter.RowCount - 1));
                //do some formatting
                //if (e.GridColumnIndex == 3 && (int)e.ExcelCellElement.Data.DataItem < 200)
                // {
                e.ExcelStyleElement.InteriorStyle.Color = Color.LightBlue;
                //}
            }
        }
        void exporter_ExcelTableCreated(object sender, ExcelTableCreatedEventArgs e)
        {
            if (e.SheetIndex == 0) //add header row only for the first excel sheet 
            {
                SingleStyleElement style = ((ExportToExcelML)sender).AddCustomExcelRow(e.ExcelTableElement, 48, " Active Pending ");
                style.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
                style.AlignmentElement.VerticalAlignment = VerticalAlignmentType.Center;

                style.InteriorStyle.Pattern = InteriorPatternType.Solid;
                style.InteriorStyle.Color = Color.Red;
                style.FontStyle.Color = Color.White;
                style.FontStyle.Bold = true;
                style.FontStyle.Size = 26;
            }
        }
         
    }
}
