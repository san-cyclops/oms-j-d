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
    public partial class FrmBaseReportsForm : Form
    {
        private static FrmBaseReportsForm basetransactionform;

        public FrmBaseReportsForm()
        {
            InitializeComponent();
        }

        public static FrmBaseReportsForm GetBaseTransactionForm
        {
            get { return basetransactionform; }
            set { basetransactionform = value; }
        }

       

    

        public virtual void ClearForm()
        {
            Common.ClearForm(this);
            //errorProvider.Clear();
            InitializeForm();

        }

        public virtual void CloseForm()
        {
            if (Toast.Show("Do you want to Close this form?", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.Yes))
            {
                this.Close();
                this.Dispose();
                basetransactionform = null;
            }
        }

        public virtual void FormLoad()
        {
            InitializeForm();
        }

        public virtual void Save() { }

        public virtual void Pause() { }


        public virtual void Delete() { }

        public virtual void Clear() { }

        public virtual void View() { }

        public virtual void Help() { }

        public virtual void Print() { }

        public virtual void InitializeForm() { }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            View();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            Clear();
        }

        private void FrmBaseTransactionForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                CloseForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void FrmBaseTransactionForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

     

         











    }
}
