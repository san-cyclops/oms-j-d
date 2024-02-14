using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Utility;

namespace UI.Windows
{
    public partial class FrmColour : UI.Windows.FrmBaseMasterForm
    {
        public FrmColour()
        {
            InitializeComponent();
        }

        private void FrmColour_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                ColorSelection();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void ColorSelection()
        {
            try
            {
                colorDialog.ShowDialog();
                picSampleColour.BackColor = colorDialog.Color;
                lblClickMe.BackColor = colorDialog.Color;
                txtStandardColourCode.Text = string.Format("0x{0:X8}", Color.FromArgb(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B).ToArgb());
                txtStandardColourName.Text = colorDialog.Color.Name;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
    }
}
