using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using System.Transactions;

using Domain;
using Utility;
using Service;
using System.Threading;
using System.Drawing.Printing;
using System.Globalization;

namespace UI.Windows
{
    /// <summary>
    /// Invoice
    /// Design By - C.S.Malluwawadu
    /// Developed By - C.S.Malluwawadu
    /// Date - 14/10/2013
    /// </summary>
    /// 

    public partial class FrmSplash : Form
    {
        int progressSize = 0;
        int progressWidth = 400;
        int conResult = 0;

        
        public FrmSplash()
        {
            InitializeComponent();            
            timer1.Start();
        }

        private void FrmSplash_Load(object sender, EventArgs e)
        {
           
        }

        private void DisplayProductDetails()
        {
            SystemConfig systemConfig = new SystemConfig();
            CommonService commonDetails = new CommonService();
            systemConfig = commonDetails.GetSystemInfo(1);

            if (systemConfig != null)
            {
                lblProductName.Text = systemConfig.ProductName;
                lblLicensedTo.Text = "Licensed To " + systemConfig.LicensedTo;
                lblVersion.Text = "Version " + systemConfig.Version;
            }
        }

        private void SetSystemFeatures()
        {
            SystemFeature systemFeature = new SystemFeature();
            CommonService commonDetails = new CommonService();
            systemFeature = commonDetails.GetSystemFeatures(1);

            if (systemFeature != null)
            {
                Common.EntryLevel = systemFeature.EntryLevel;
            }
        }

        private void SetModuleFeatures()
        {
            GroupOfCompany groupOfCompany = new GroupOfCompany();
            GroupOfCompanyService groupOfCompanyService = new GroupOfCompanyService();
           // groupOfCompany = groupOfCompanyService.GetGroupOfCompany();

            //if (groupOfCompany != null)
            {
                Common.SystemProductID = 1; // groupOfCompany.GroupOfCompanyID;
                Common.GroupOfCompanyID = 1; // groupOfCompany.GroupOfCompanyID;
                Common.SetModuleFeature(Common.SystemProductID);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SplashService dbstate = new SplashService();
            int lable = (int)((progressSize / progressWidth) * 100);

            if (progressSize >= progressWidth)
            {
                timer1.Stop();
                SetModuleFeatures();
                this.Hide();
                if (Program.getmdi() == null)
                {
                    Program.ShowMdi(lblVersion.Text.Trim());
                    //dbstate.ReadSysConfig();
                }
                else
                {
                    Program.getmdi().Focus();
                }
            }
            progressSize += 8;
            pnlProgressBar.Width = progressSize;

            DisplayProductDetails();
            SetSystemFeatures();
            

            if (progressSize > 60 && progressSize < 130)
            {
                timer1.Interval = 35;
                lblDatabaseStatus.Text = "Checking Database Connectivity...";
                lblDatabaseStatus.Refresh();
                lblComponent.Refresh();
               
                
            }
            if (progressSize > 100 && progressSize < 200)
            {
                timer1.Interval = 1;
                lblComponent.Text = "Initializing components...";
            }
            if (progressSize > 330)
            {
                lblDatabaseStatus.Text = "Database Connectivity Succeeded.";
                lblComponent.Text = "Components Load Successfully.";
                lblDatabaseStatus.Refresh();
                lblComponent.Refresh();
            }
        }
    }
}
