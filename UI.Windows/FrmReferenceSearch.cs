using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;
using System.Reflection;

namespace UI.Windows
{
    /// <summary>
    /// Process - Search all types of master file details
    /// Developed by asanka
    /// </summary>
    public partial class FrmReferenceSearch : Form
    {
        #region Public Properties
        /// <summary>
        /// Parent form of serach form instant.
        /// </summary>
        public String ParentOfSearch { get; set; }

        /// <summary>
        /// Caption to be printed on search form.
        /// </summary>
        public String FormCaption { get; set; }

        /// <summary>
        /// Text enterd from parent control
        /// </summary>
        public String SearchText { get; set; }

        /// <summary>
        /// DataView for sorting and filtering 
        /// </summary>
        public DataView DvResults {  get ; set; }

        /// <summary>
        /// Control to be focussed from search form
        /// </summary>
        public Control FocusControl { get; set; }
        
        #endregion

        // String for Dataview filter Expression
        private string filterExpression = string.Empty;


        public FrmReferenceSearch()
        {
            InitializeComponent();
        }

        #region Form Events
        /// <summary>
        /// Set form Title
        /// Load provided text into search text box
        /// Set Search options
        /// Arrange Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmReferenceSearch_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = FormCaption.Trim();
                
                dgvSearch.DataSource = DvResults;
                this.cmbSearchOption.SelectedIndexChanged -= new System.EventHandler(this.cmbSearchOption_SelectedIndexChanged);
                SetSearchOptions();
                this.cmbSearchOption.SelectedIndexChanged += new System.EventHandler(this.cmbSearchOption_SelectedIndexChanged);

                this.cmbOperation.SelectedIndexChanged -= new System.EventHandler(this.cmbOperation_SelectedIndexChanged);
                SetOperands();
                this.cmbOperation.SelectedIndexChanged += new System.EventHandler(this.cmbOperation_SelectedIndexChanged);
                DisplayResult();

                ArrangeControlsOnLoad();

                cmbOperation.Text = "Start with";
                
                this.ActiveControl = txtSearch1;
                txtSearch1.Focus();
                txtSearch1.Text = SearchText.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// Search data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter)) { DisplayResult(); }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSearchOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SetOperands();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        /// <summary>
        /// Enable search fields dipending on selected oparand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbOperation.SelectedIndex < 0)
                { return; }

                if (string.Equals(cmbOperation.SelectedValue.ToString().Trim(), "><"))
                {
                    txtSearch2.Visible = true;
                }
                else
                {
                    txtSearch2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// Call txtSearch1_KeyDown function on txtSearch1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                txtSearch1_KeyDown(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                DvResults.RowFilter = "";
                DvResults.Sort = "";
                dgvSearch.DataSource = DvResults;
                cmbSearchOption.SelectedIndex = 0;
                cmbOperation.SelectedIndex = 0;
                txtSearch1.Text = string.Empty;

                txtSearch2.Text = string.Empty;
                txtSearch2.Visible = false;
                //cmbSearchOption.Focus();
                txtSearch1.Focus();
                cmbOperation.Text = "Start with";
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvSearch_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (FocusControl != null)
                {
                    FocusControl.Text = dgvSearch.SelectedRows[0].Cells[0].Value.ToString().Trim();
                    FocusControl.Focus();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);            
            }
        }

        private void dgvSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                if (FocusControl != null)
                {
                    FocusControl.Text = dgvSearch.SelectedRows[0].Cells[0].Value.ToString().Trim();
                    FocusControl.Focus();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set visibility and posotions of controls
        /// </summary>
        private void ArrangeControlsOnLoad()
        {
            try
            {
                txtSearch2.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// Load serch options(data grid view header columns) into cmbSearchOption.
        /// </summary>
        private void SetSearchOptions()
        {
            try
            {
                if (DvResults.Table.Rows.Count > 0)
                {
                    DataTable dtSearchOption = new DataTable();
                    dtSearchOption.Columns.Add("GridIndex", typeof(int));
                    dtSearchOption.Columns.Add("HeaderColumn", typeof(string));

                    for (int i = 0; i < DvResults.Table.Columns.Count; i++)
                    {
                        dtSearchOption.Rows.Add(i, DvResults.Table.Columns[i].ColumnName.Trim());
                    }

                    cmbSearchOption.DataSource = dtSearchOption;
                    cmbSearchOption.ValueMember = "GridIndex";
                    cmbSearchOption.DisplayMember = "HeaderColumn";
                    cmbSearchOption.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// Load DataView data into GridView
        /// </summary>
        private void DisplayResult()
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch1.Text.Trim()))
                {
                    dgvSearch.DataSource = DvResults;
                }
                else
                {
                    DvResults.RowFilter = ArrangeRawFilter().Trim();
                    DvResults.Sort = cmbSearchOption.Text.Trim() + " ASC";
                    dgvSearch.DataSource = DvResults;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// Arrange Raw Filter for DataView
        /// </summary>
        /// <returns></returns>
        private string ArrangeRawFilter()
        {
            string dataTypeFormat1 = string.Empty;
            string dataTypeFormat2 = string.Empty;

            if (dgvSearch.Columns[int.Parse(cmbSearchOption.SelectedValue.ToString())].ValueType.Equals(typeof(System.Int16)) ||
             dgvSearch.Columns[int.Parse(cmbSearchOption.SelectedValue.ToString())].ValueType.Equals(typeof(System.Int32)) ||
             dgvSearch.Columns[int.Parse(cmbSearchOption.SelectedValue.ToString())].ValueType.Equals(typeof(System.Int64)) ||
             dgvSearch.Columns[int.Parse(cmbSearchOption.SelectedValue.ToString())].ValueType.Equals(typeof(System.Decimal)) ||
             dgvSearch.Columns[int.Parse(cmbSearchOption.SelectedValue.ToString())].ValueType.Equals(typeof(System.Double)))
            {
                dataTypeFormat1 = txtSearch1.Text.Trim();
                dataTypeFormat2 = txtSearch2.Text.Trim();
            }
            else if (dgvSearch.Columns[int.Parse(cmbSearchOption.SelectedValue.ToString())].ValueType.Equals(typeof(System.String)))
            {
                dataTypeFormat1 = "'" + txtSearch1.Text.Trim() + "'";
            }
            else if (dgvSearch.Columns[int.Parse(cmbSearchOption.SelectedValue.ToString())].ValueType.Equals(typeof(System.DateTime)))
            {
                dataTypeFormat1 = "#" + txtSearch1.Text.Trim() + "#";
                dataTypeFormat2 = "#" + txtSearch2.Text.Trim() + "#";
            }

            switch (cmbOperation.SelectedValue.ToString().Trim())
            {
                case "=":  // Equal
                    filterExpression = String.Format("[{0}] = {1}", cmbSearchOption.Text.Trim().ToString().Trim(), dataTypeFormat1);
                    break;
                case "<>": // Not equal
                    filterExpression = String.Format("[{0}] <> {1}", cmbSearchOption.Text.Trim().ToString().Trim(), dataTypeFormat1);
                    break;
                case ">":  // Greater than
                    filterExpression = String.Format("[{0}] > {1}", cmbSearchOption.Text.Trim().ToString().Trim(), dataTypeFormat1);
                    break;
                case "<":  // Less than
                    filterExpression = String.Format("[{0}] < {1}", cmbSearchOption.Text.Trim().ToString().Trim(), dataTypeFormat1);
                    break;
                case "><": // Between
                    filterExpression = String.Format("[{0}] > {1} AND [{0}] < {2}", cmbSearchOption.Text.Trim().ToString().Trim(), dataTypeFormat1, dataTypeFormat2);
                    break;
                case "_%": // Start with
                    dataTypeFormat1 = dataTypeFormat1.Remove(0, 1);
                    dataTypeFormat1 = dataTypeFormat1.Remove(dataTypeFormat1.Length - 1, 1);
                    filterExpression = String.Format("[{0}] LIKE '{1}%'", cmbSearchOption.Text.Trim().ToString().Trim(), EscapeLikeValue(dataTypeFormat1));
                    break;
                case "%_": // End with
                    dataTypeFormat1 = dataTypeFormat1.Remove(0, 1);
                    dataTypeFormat1 = dataTypeFormat1.Remove(dataTypeFormat1.Length - 1, 1);
                    filterExpression = String.Format("[{0}] LIKE '%{1}'", cmbSearchOption.Text.Trim().ToString().Trim(), EscapeLikeValue(dataTypeFormat1));
                    break;
                case "%%": // Contains
                    dataTypeFormat1 = dataTypeFormat1.Remove(0, 1);
                    dataTypeFormat1 = dataTypeFormat1.Remove(dataTypeFormat1.Length - 1, 1);
                    filterExpression = String.Format("[{0}] LIKE '%{1}%'", cmbSearchOption.Text.Trim().ToString().Trim(), EscapeLikeValue(dataTypeFormat1));
                    break;
                default:
                    break;
            }
            return filterExpression;
        }

        /// <summary>
        ///  Escapes a text value for usage in a 'LIKE' clause
        /// </summary>
        /// <param name="valueWithoutWildcards"></param>
        /// <returns></returns>
        public static string EscapeLikeValue(string valueWithoutWildcards)
        {
            StringBuilder tmpStringBuilder = new StringBuilder();
            for (int i = 0; i < valueWithoutWildcards.Length; i++)
            {
                char c = valueWithoutWildcards[i];
                if (c == '*' || c == '%' || c == '[' || c == ']')
                    tmpStringBuilder.Append("[").Append(c).Append("]");
                else if (c == '\'')
                    tmpStringBuilder.Append("''");
                else
                    tmpStringBuilder.Append(c);
            }
            return tmpStringBuilder.ToString();
        }


        /// <summary>
        /// Set oparands depending on selected search option
        /// </summary>
        private void SetOperands()
        {
            try
            {
                cmbOperation.DataSource = null;
                DataTable dtOparands = new DataTable();
                dtOparands.Columns.Add("Operand", typeof(string));
                dtOparands.Columns.Add("DisplayText", typeof(string));

                if (dgvSearch.Columns[int.Parse(cmbSearchOption.SelectedValue.ToString())].ValueType.Equals(typeof(System.Int16)) ||
                     dgvSearch.Columns[int.Parse(cmbSearchOption.SelectedValue.ToString())].ValueType.Equals(typeof(System.Int32)) ||
                     dgvSearch.Columns[int.Parse(cmbSearchOption.SelectedValue.ToString())].ValueType.Equals(typeof(System.Int64)) ||
                     dgvSearch.Columns[int.Parse(cmbSearchOption.SelectedValue.ToString())].ValueType.Equals(typeof(System.Decimal)) ||
                     dgvSearch.Columns[int.Parse(cmbSearchOption.SelectedValue.ToString())].ValueType.Equals(typeof(System.Double)))
                {
                    dtOparands.Rows.Add("=", "Equal");
                    dtOparands.Rows.Add("<>", "Not equal");
                    dtOparands.Rows.Add(">", "Greater than");
                    dtOparands.Rows.Add("<", "Less than");
                    dtOparands.Rows.Add("><", "Between");
                }
                else if (dgvSearch.Columns[int.Parse(cmbSearchOption.SelectedValue.ToString())].ValueType.Equals(typeof(System.String)))
                {
                    dtOparands.Rows.Add("=", "Equal");
                    dtOparands.Rows.Add("<>", "Not equal");
                    dtOparands.Rows.Add("_%", "Start with");
                    dtOparands.Rows.Add("%_ ", "End with");
                    dtOparands.Rows.Add("%%", "Contains");

                }
                else if (dgvSearch.Columns[int.Parse(cmbSearchOption.SelectedValue.ToString())].ValueType.Equals(typeof(System.DateTime)))
                {
                    dtOparands.Rows.Add("=", "Equal");
                    dtOparands.Rows.Add("<>", "Not equal");
                    dtOparands.Rows.Add(">", "Greater than");
                    dtOparands.Rows.Add("<", "Less than");
                    dtOparands.Rows.Add("><", "Between");
                }

                cmbOperation.DataSource = dtOparands;
                cmbOperation.ValueMember = "Operand";
                cmbOperation.DisplayMember = "DisplayText";
                cmbOperation.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        /// <summary>
        /// Set opacity(1.0) when activating from
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmReferenceSearch_Activated(object sender, EventArgs e)
        {
            try
            {
                this.Opacity = 1.0;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// Set opacity(0.9) when deactivating from
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmReferenceSearch_Deactivate(object sender, EventArgs e)
        {
            try
            {
                this.Opacity = 0.9;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void txtSearch1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch1.Text.Trim())) { btnReset.PerformClick(); }
                DisplayResult();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        
        

        






    }
}
