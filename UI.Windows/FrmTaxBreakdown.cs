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
using System.Linq;
using System.IO;
using System.Reflection;

namespace UI.Windows
{
    public partial class FrmTaxBreakdown : Form
    {


        public FrmTaxBreakdown(long vendorID, int vendorType, decimal amountToTax)
        {
            InitializeComponent();

            decimal tax1 = 0;
            decimal tax2 = 0;
            decimal tax3 = 0;
            decimal tax4 = 0;
            decimal tax5 = 0;
            decimal taxValue = 0;

            CommonService commonService = new Service.CommonService();

            //if (vendorType == 1){ txtTax.Text = Common.ConvertDecimalToStringCurrency(commonService.CalculateTax(true, amountToTax, vendorID, out tax1, out tax2, out tax3, out tax4, out tax5)); } // Supplier Related
            //if (vendorType == 2) { txtTax.Text = Common.ConvertDecimalToStringCurrency(commonService.CalculateTax(false, amountToTax, vendorID, out tax1, out tax2, out tax3, out tax4, out tax5)); } // Customer Related
            //if (vendorType == 3) { txtTax.Text = Common.ConvertDecimalToStringCurrency(commonService.CalculateTax(false, amountToTax, vendorID, out tax1, out tax2, out tax3, out tax4, out tax5)); } // Lgs Supplier Related

            txtTax.Text = Common.ConvertDecimalToStringCurrency(commonService.CalculateTax(vendorType, amountToTax, vendorID, out tax1, out tax2, out tax3, out tax4, out tax5));


            DataTable dt_Page = new DataTable();

            dt_Page.Columns.Add("Tax Description", typeof(string));
            dt_Page.Columns.Add("Rate (%)", typeof(string));
            dt_Page.Columns.Add("Effective Rate (%)", typeof(string));
            dt_Page.Columns.Add("Value", typeof(string));


            DataRow dr;

            DataTable dt = new DataTable();
            if (vendorType == 1) { dt = commonService.GetAllTaxesRelatedToSupplier(vendorID); } // Supplier Related
            if (vendorType == 2) { dt = commonService.GetAllTaxesRelatedToCustomer(vendorID); } // Customer Related
            if (vendorType == 3) { dt = commonService.GetAllTaxesRelatedToLgsSupplier(vendorID); } // LgsSupplier Related

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0) { taxValue = tax1; }
                if (i == 1) { taxValue = tax2; }
                if (i == 2) { taxValue = tax3; }
                if (i == 3) { taxValue = tax4; }
                if (i == 4) { taxValue = tax5; }

                dr = dt_Page.NewRow();
                dr["Tax Description"] = dt.Rows[i]["TaxName"].ToString();
                dr["Rate (%)"] = dt.Rows[i]["TaxPercentage"].ToString();
                dr["Effective Rate (%)"] = dt.Rows[i]["EffectivePercentage"].ToString();
                dr["Value"] = Common.ConvertDecimalToStringCurrency(taxValue);

                dt_Page.Rows.Add(dr);                

            }

            
            dataGridView1.DataSource = null;


            dataGridView1.DataSource = dt_Page;
            DataGridViewColumn column0 = dataGridView1.Columns[0];
            column0.Width = 120;            
            DataGridViewColumn column1 = dataGridView1.Columns[1];
            column1.Width = 65;
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            column2.Width = 65;
            DataGridViewColumn column3 = dataGridView1.Columns[3];
            column3.Width = 100;

            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Refresh();
        }

        

        private void FrmTaxBreakdown_Load(object sender, EventArgs e)
        {
            
        }

        private void FrmTaxBreakdown_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
