using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;

namespace UI.Windows
{
    public partial class FrmBaseMasterForm : Form
    {
        private static FrmBaseMasterForm baseform;

        public FrmBaseMasterForm()
        {
            InitializeComponent();
        }

        public static FrmBaseMasterForm GetBaseForm
        {
            get { return baseform; }
            set { baseform = value; }
        }

        private void FrmBaseMasterForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           
            CloseForm();
        }
        
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            Clear();
        }

        public virtual void ClearForm()
        {
            Common.ClearForm(this);
            errorProvider.Clear();
            InitializeForm();
            
        }

        public virtual void CloseForm()
        {
            if (Toast.Show("Do you want to Close this form?",Toast.messageType.Question,Toast.messageAction.General).Equals(DialogResult.Yes))
            {
                this.Close();
                this.Dispose();
                baseform = null;
            }
        }

        public virtual void FormLoad()
        {
            InitializeForm();
        }

        public virtual void Save() { }

        public virtual void Delete() { }

        public virtual void Clear() { }

        public virtual void View() { }

        public virtual void Help() { }

        public virtual void Print() { }

        public virtual void InitializeForm() { }

        private void FrmBaseMasterForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                CloseForm();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            View();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

      
          

       
       

    }
}
