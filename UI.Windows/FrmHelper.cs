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
    public partial class FrmHelper : UI.Windows.FrmBaseMasterForm
    {
        public FrmHelper()
        {
            InitializeComponent();
        }

        private void FrmHelper_Load(object sender, EventArgs e)
        {

        }

        public override void View()
        {
            try
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmHelper");
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
