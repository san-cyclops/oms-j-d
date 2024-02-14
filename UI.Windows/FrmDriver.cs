using Domain;
using Report.Com;
using Service;
using Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace UI.Windows
{
    public partial class FrmDriver : UI.Windows.FrmBaseMasterForm
    {
        public FrmDriver()
        {
            InitializeComponent();
        }

        private void FrmDriver_Load(object sender, EventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        public override void View()
        {
            try
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDriver");
                ComReportGenerator comReportGenerator = new ComReportGenerator();
                comReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
    }
}
