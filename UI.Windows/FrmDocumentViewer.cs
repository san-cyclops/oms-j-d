using Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Windows
{
    public partial class FrmDocumentViewer : Form
    {
        Image backgroundImage;

        public FrmDocumentViewer()
        {
            InitializeComponent();
        }

        public FrmDocumentViewer(Image img)
        {
            InitializeComponent();
            pbPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            backgroundImage = img;
            pbPicture.Image = img;
        }

        protected override void OnMouseWheel(MouseEventArgs me) 
        {
            try
            {
            if (pbPicture.Image != null)
            {
                if (me.Delta > 0)
                {
                    if ((pbPicture.Width < (15 * this.Width)) && (pbPicture.Height < (15 * this.Height)))
                    {
                        pbPicture.Width = (int)(pbPicture.Width * 1.25);
                        pbPicture.Height = (int)(pbPicture.Height * 1.25);
                    }
                }
                else
                {
                    if ((pbPicture.Width > (this.Width / 15)) && (pbPicture.Height > (this.Height / 15)))
                    {
                        pbPicture.Width = (int)(pbPicture.Width / 1.25);
                        pbPicture.Height = (int)(pbPicture.Height / 1.25);
                    }
                }
            }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void FrmPictureView_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            //this.BackgroundImage = bckGround;
        }



    }

}
