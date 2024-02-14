using Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Windows
{
    static class Program
    {
        public static FrmSplash splash = null;
        public static MdiMainRibbon mdi = null;
        public static FrmLoginUser login = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            // Validate Date Time Format
            //if (!Common.ValidateMachineDateTimeFromat())
            //{ return; }

            Common.LoggedPcName = (Environment.MachineName.Length > 50 ? Environment.MachineName .Substring(0,50): Environment.MachineName);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmSplash());

        }

        public static void ShowMdi(string ver) 
        {
           
            mdi = new MdiMainRibbon();
            mdi.ShowInTaskbar = true;
            mdi.Show();
            ShowLogin();
        }
        public static void ShowLogin()
        {
            
            login = new FrmLoginUser(mdi);
            login.MdiParent = mdi;
            login.Show();
        }
        
        public static MdiMainRibbon getmdi()
        {
            return mdi;
        }

       
    }
}
