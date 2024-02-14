using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Report
{
    public partial class FrmReportViewer : Form
    {
        public FrmReportViewer()
        {
            InitializeComponent();
        }

        private void FrmReportViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void FrmReportViewer_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.P)
            //{
            //    crRptViewer.PrintReport();
            //}
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.P)
            {
                crRptViewer.PrintReport();
            }

        }

        private void FrmReportViewer_Load(object sender, EventArgs e)
        {
            this.ActiveControl = button1;
            button1.Focus();
        }

        private void crRptViewer_Click(object sender, EventArgs e)
        {
            this.ActiveControl = button1;
            button1.Focus();

            //this.ActiveControl = crRptViewer;
            //crRptViewer.Focus();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P)
            {
                crRptViewer.PrintReport();
            }
        }

        private void crRptViewer_Load(object sender, EventArgs e)
        {
            this.ActiveControl = crRptViewer;
            crRptViewer.Focus();
        }

    }
}
