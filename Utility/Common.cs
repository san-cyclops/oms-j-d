using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

using System.Data.Entity;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using System.Globalization;
using System.Configuration;

//using 

namespace Utility
{
    public static class Common
    {
        /// <summary>
        /// Pravin
        /// </summary>
        
        // Data Structure for Report Fileds, Filed Names and Data types
        public struct ReportDataStruct
        {
            public string ReportField; // Ex :- FieldString1 (Report value field)
            public string ReportFieldName; // Ex :- Document No (Report text field)
            public object ReportDataType; // Ex :- string (Report value field data type)
            public string DbColumnName; // Ex :- DocumentNo (Column name of the data base table)
            public string DbJoinColumnName; // Ex :- DocumentNo (Join Column name of the data base table)
            public object DbJoinTableName; // Ex :- DocumentNo (Joined Table name of the data base table)
            public object ValueDataType; // Ex :- string (C# data type)
            public bool IsSelectionField;  // Ex :- DocumentNo (Fields to be loaded into field selection grid in report generater form )
            public bool IsConditionField;  // Ex :- DocumentNo (Fields to be loaded into field selection grid in report generater form )
            public bool IsGroupBy; // Ex :- Supplier Code (Fields to be loaded into Group by grid in report generater form )
            public bool IsColumnTotal;// Ex :- Net Amount (Fields to be loaded into Column total grid in report generater form )
            public bool IsRowTotal;// Ex :- Net Amount (Fields to be loaded into Raw total grid in report generater form )            
            public bool IsJoinField; // Ex :- SupplierID
            public bool IsConditionNameJoined; // Ex :- (Fields to be loaded into conditions using Join Column Name of the data base table)
            public bool IsResultGroupBy;  // Ex :- DocumentNo (Selected Groupby field for Report)
            public bool IsRecordFilterByGivenOption; // Ex :- (allow to enter field for record searching or not)
            public bool IsManualRecordFilter; // Ex :- (Allow to select records based on manual entered record )
            public bool IsResultOrderBy;  // Ex :- DocumentNo (Selected Orderby field for Report)
            public bool IsMandatoryField; // Ex :- Codes of master data (Department Code of departments)
            public bool IsUntickedField; // Ex :- User entered Fields/default untick fields (Stock Entry field of Product Master)
        }

        // Data structure for report conditions
        public struct ReportConditionsDataStruct
        {
            public ReportDataStruct ReportDataStruct;
            public string ConditionFrom;
            public string ConditionTo;
        }

        // class for CheckedListBox selection
        public class CheckedListBoxSelection
        {
            public string Value;
            public CheckState isChecked;
        }

        public struct ModuleTypes
        {
            public static bool InventoryAndSales;
            public static bool PointOfSales;
            public static bool Manufacture;
            public static bool HirePurchase;
            public static bool CustomerRelationshipModule;
            public static bool GiftVouchers;
            public static bool Accounts;
            public static bool NonTrading;
            public static bool HrManagement;
            public static bool HospitalManagement;
            public static bool HotelManagement;

            //public static bool FixedAsset;
        }

        #region Pre Initialization Based On Client
        public static int GiftVoucherSerialLength = 8;
        public static int GiftVoucherCoupanSerialLength = 8;

        public static byte decimalPointsCurrency = 6;
        public static byte decimalPointsQty = 2;
        public static int decimalPointsNumeric = 2;

        #endregion
        
        public static int CharacterLengthChequeNo = 6;

        public static bool tStatus = false;
        public static bool isHeadOffice;

        public static int GroupOfCompanyID;
        public static int SystemProductID;
        public static string LoggedUser;
        public static long LoggedUserId;
        public static int LoggedLocationID;
        public static string LoggedLocationCode;
        public static string LoggedLocationName;
        public static int LoggedCompanyID;
        public static string Version = "V 1.0.0.0";
        public static string LoggedCompanyName;
        public static string LoggedCompanyAddress;
        public static int EntryLevel;
        public static string LoggedPcName;
        public static long UserGroupID; 

        public static string AuthorName = "Cynex Soft";
        public static string AuthorAddress = "#";
        public static decimal ExchangeRate = 0;

        
       

        #region ClearForm

        public static void ClearForm(Control control)
        {
            
            string a;
                foreach (Control c in control.Controls)
                {

                    if (!c.HasChildren)
                    {
                       
                        if (c.Tag == null || c.Tag.ToString().Equals("3") || c.Tag.ToString().Trim().Equals(string.Empty))
                        {

                            if (c is TextBox)
                            {
                               

                                    ((TextBox)c).Clear();
                            }

                            if (c is CheckBox)
                            {
                               

                                    ((CheckBox)c).Checked = false;
                            }

                            if (c is RadioButton)
                            {
                                

                                    ((RadioButton)c).Checked = false;
                            }

                            if (c is ComboBox)
                            {
                                
                                {
                                    if (!((ComboBox)c).SelectedIndex.Equals(-1))
                                        ((ComboBox)c).SelectedIndex = 0;
                                    //if (!((ComboBox)c).Text.Trim().Equals(string.Empty))
                                    ((ComboBox)c).Text = string.Empty;


                                }
                            }

                            if (c is DataGridView)
                            {


                                ((DataGridView)c).DataSource = null;
                                ((DataGridView)c).Refresh();
                            }
                        }
                    }
                    else
                        ClearForm(c);
                    
                }
           
        }

        public static void ClearForm(Control control, Control excludeControl)
        {

            
            foreach (Control c in control.Controls)
            {

                if (!c.HasChildren)
                {
                    if( c != excludeControl)
                    {
                    if ((c.Tag == null || c.Tag.ToString().Equals("3") || c.Tag.ToString().Trim().Equals(string.Empty)))
                    {

                        if (c is TextBox)
                        {


                            ((TextBox)c).Clear();
                        }

                        if (c is CheckBox)
                        {


                            ((CheckBox)c).Checked = false;
                        }

                        if (c is RadioButton)
                        {


                            ((RadioButton)c).Checked = false;
                        }

                        if (c is ComboBox)
                        {

                            {
                                if (!((ComboBox)c).SelectedIndex.Equals(-1))
                                    ((ComboBox)c).SelectedIndex = 0;
                                ((ComboBox)c).Text = string.Empty;


                            }
                        }

                        if (c is DataGridView)
                        {


                            ((DataGridView)c).DataSource = null;
                            ((DataGridView)c).Refresh();
                        }
                    }

                    }
                    
                }
                
                else
                    ClearForm(c);

            }

        }

        #endregion

        #region ClearTextBox

        public static void ClearTextBox(params TextBox[] textBox)
        {
            foreach (TextBox TBox in textBox)
                TBox.Text = string.Empty;
        }

        #endregion

        #region ClearComboBox

        public static void ClearComboBox(params ComboBox[] comboBox)
        {
            foreach (ComboBox CBox in comboBox)
                if (CBox.DropDownStyle.Equals(ComboBoxStyle.DropDownList))
                {
                    CBox.SelectedIndex = -1;
                }
                else
                {
                    CBox.Text = string.Empty;
                }
        }

        #endregion

        #region ClearCheckBox

        public static void ClearCheckBox(params CheckBox[] checkBox)
        {
            foreach (CheckBox ChBox in checkBox)
                ChBox.Checked = false;
        }

        #endregion

        #region EnableButton

        public static void EnableButton(bool enable,params Button[] button)
        {

            foreach (Button b in button)
                b.Enabled = enable;
        }

        #endregion

        #region EnableTextBox

        public static void EnableTextBox(bool enable, params TextBox[] textBox)
        {
            foreach (TextBox t in textBox)
                t.Enabled = enable;
        }

        #endregion

        #region SetZeroToTextBox

        public static void SetZeroToTextBox(params TextBox[] textBox)
        {
            foreach (TextBox t in textBox)
                t.Text = "0";
        }

        #endregion

        #region EnableComboBox

        public static void EnableComboBox(bool enable, params ComboBox[] comboBox)
        {
            foreach (ComboBox c in comboBox)
                c.Enabled = enable;
        }

        #endregion

        #region TextBoxReadOnly

        public static void ReadOnlyTextBox(bool readOnly, params TextBox[] textBox)
        {
            foreach (TextBox t in textBox)
                t.ReadOnly = readOnly;
        }

        #endregion

        #region EnableCheckBox

        public static void EnableCheckBox(bool enable, params CheckBox[] checkox)
        {
            foreach (CheckBox c in checkox)
                c.Enabled = enable;
        }

        #endregion

        #region Load All User Groups into combo box
        /// <summary>
        /// Load All User Groups into combo box
        /// </summary>

        public static void LoadAllUserGroups<U>(ComboBox cmbUserGroup, List<U> userGroups)
        {
            cmbUserGroup.DataSource = userGroups;
            cmbUserGroup.DisplayMember = "UserGroupName";
            cmbUserGroup.ValueMember = "UserGroupID";
            cmbUserGroup.SelectedIndex = -1;
        }


        #endregion

        #region Load All User Accounts into combo box
        /// <summary>
        /// Load All User Accounts into combo box
        /// </summary>

        public static void LoadAllUserAccounts<U>(ComboBox cmbUser, List<U> userAccounts)
        {
            cmbUser.DataSource = userAccounts;
            cmbUser.DisplayMember = "UserName";
            cmbUser.ValueMember = "UserMasterID";
            cmbUser.SelectedIndex = -1;
        }


        #endregion

        #region Set read only property of Data grid view columns
        /// <summary>
        /// Enable or disable data grid view read only columns
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="dataGridViewColumns"></param>
        /// <param name="readOnly"></param>
        public static void SetDataGridviewColumnsReadOnly(bool readOnly, DataGridView dataGridView, params DataGridViewColumn[] dataGridViewColumns)
        {
            foreach (DataGridViewBand band in dataGridView.Columns)
            {
                if (dataGridViewColumns.Contains(band))
                { band.ReadOnly = readOnly; }
                else
                { band.ReadOnly = !readOnly; }
            }
        }

        #endregion

        #region SetAutoComplete

        public static void SetAutoComplete<T>(TextBox textBox, List<T> list)
        {
            AutoCompleteStringCollection AutoCompleteCode = new AutoCompleteStringCollection();

            textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;

            string a;
//            public static T[] ConvertListToArray<T>(List<T> list) {
            int count = list.Count;
            T[] array = new T[count];
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
                //return array;
                a = array[i].ToString();
                
                MessageBox.Show(a);
                AutoCompleteCode.Add(list[i].ToString());
                a = list[i].ToString();
            }
            for (int i = 0; i < list.Count; i++)
            {
                a = list[i].ToString();
            }
            foreach (var val in list)
            {
                a = val.ToString();
            }

            textBox.AutoCompleteCustomSource = AutoCompleteCode;
        }

        public static void SetAutoComplete(TextBox textBox, AutoCompleteStringCollection autoCompleteCode,bool isComplete)
        {
            if (isComplete)
            {
                textBox.AutoCompleteCustomSource = null;
                textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox.AutoCompleteCustomSource = autoCompleteCode;
            }
            else
            {
                textBox.AutoCompleteMode = AutoCompleteMode.None;
                textBox.AutoCompleteSource = AutoCompleteSource.None;
                textBox.AutoCompleteCustomSource = null;
            }
        }


        public static void SetAutoComplete(TextBox textBox, string[] stringCollection, bool isComplete)
        {
            if (isComplete)
            {
                AutoCompleteStringCollection autoCompleteCode = new AutoCompleteStringCollection();
                autoCompleteCode.AddRange(stringCollection);
                textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox.AutoCompleteCustomSource = autoCompleteCode;
            }
            else
            {
                textBox.AutoCompleteMode = AutoCompleteMode.None;
                textBox.AutoCompleteSource = AutoCompleteSource.None;
                textBox.AutoCompleteCustomSource = null;
            }
        }

        #endregion

        #region SetAutoBindRecords
        public static void SetAutoBindRecords(ComboBox comboBox, string[] stringCollection)
        {
            AutoCompleteStringCollection autoCompleteCode = new AutoCompleteStringCollection();
            autoCompleteCode.AddRange(stringCollection);
            comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBox.AutoCompleteCustomSource = autoCompleteCode;
            comboBox.DataSource = autoCompleteCode;
            comboBox.SelectedIndex = -1;
        }

        public static void SetAutoBindRecords(ComboBox comboBox, string[] stringCollection, bool isComplete)
        {
            if (isComplete)
            {
                AutoCompleteStringCollection autoCompleteCode = new AutoCompleteStringCollection();
                autoCompleteCode.AddRange(stringCollection);
                comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox.AutoCompleteCustomSource = autoCompleteCode;
                comboBox.DataSource = autoCompleteCode;
                comboBox.SelectedIndex = -1;
            }
            else
            {
                comboBox.AutoCompleteMode = AutoCompleteMode.None;
                comboBox.AutoCompleteSource = AutoCompleteSource.None;
                comboBox.AutoCompleteCustomSource = null;
            }
        }
        
        public static void SetAutoBindRecords(ComboBox comboBox, string[] stringCollection, string displayMember, string valueMember, bool isComplete)
        {
            if (isComplete)
            {
                AutoCompleteStringCollection autoCompleteCode = new AutoCompleteStringCollection();
                autoCompleteCode.AddRange(stringCollection);
                comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox.AutoCompleteCustomSource = autoCompleteCode;
                comboBox.DisplayMember = displayMember;
                comboBox.ValueMember = valueMember;
                comboBox.DataSource = autoCompleteCode;
                comboBox.SelectedIndex = -1;
            }
            else
            {
                comboBox.AutoCompleteMode = AutoCompleteMode.None;
                comboBox.AutoCompleteSource = AutoCompleteSource.None;
                comboBox.AutoCompleteCustomSource = null;
            }
        }
        #endregion

        #region GetAutoGenetateInfo


            #endregion

        #region SetFocus
            /// <summary>
        /// Setfucs Controls 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="ctrl"></param>

        public static void SetFocus(KeyEventArgs e, Control ctrl)
       {
            if (e.KeyCode == Keys.Enter)
            {
                ctrl.Focus();
                
            }
        }
        
        public static void SetFocus(KeyEventArgs e, Control tabctrl,TabPage tabPage)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tabctrl.Focus();
                if (tabctrl is TabControl)
                {
                    ((TabControl)tabctrl).SelectedTab = tabPage;

                }
                
            }
        }

       

        public static void SetFocus(Form selectedForm, Control ctrl)
        {
            try
            {
                selectedForm.Controls[ctrl.Name].Focus();
            }
            catch (Exception)
            {
                throw;
            }
        }     

        #endregion

        #region Allow Delete

        /// <summary>
        /// Allow to delete grid view items(rows) only on 'F2' key down of grid view
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool AllowDeleteGridRow(KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F2))
            { return true; }
            else
            { return false; }
        }

        #endregion

        #region ConvertStringToInt

        public static int ConvertStringToInt(string Value)
        {
            try
            {
                return (Value.Trim() != string.Empty ? int.Parse(Value) : 0);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #endregion

        #region ConvertStringToBool

        public static bool ConvertStringToBool(string Value)
        {
            try
            {
                return (Value.Trim() != string.Empty ? bool.Parse(Value) : false);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region ConvertStringToDecimal

        public static decimal ConvertStringToDecimal(string Value)
        {
            try
            {
                if (Value == null)
                    Value = "0";
                return (Value.Trim() != string.Empty ? Convert.ToDecimal(Value) : 0);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        
        #endregion

        #region ConvertDecimalToStringCurrency

        public static string ConvertDecimalToStringCurrency(decimal Value)
        {
            try
            {
                if (Value == null)
                    Value = 0;

                return String.Format("{0:N" + Math.Abs(decimalPointsCurrency) + "}", Value);
            }
            catch (Exception ex)
            {
                return String.Format("{0:N" + Math.Abs(decimalPointsCurrency) + "}", 0);
            }
        }

        #endregion

        #region ConvertDecimalToStringQty

        public static string ConvertDecimalToStringQty(decimal Value)
        {
            try
            {
                if (Value == null)
                    Value = 0;

                return String.Format("{0:N" + Math.Abs(decimalPointsQty) + "}", Value);
            }
            catch (Exception ex)
            {
                return String.Format("{0:N" + Math.Abs(decimalPointsQty) + "}", 0);
            }
        }

        #endregion

        #region ConvertDecimalToDecimalQty

        public static decimal ConvertDecimalToDecimalQty(decimal Value)
        {
            try
            {
                if (Value == null)
                    Value = 0;

                return Math.Round(Value, Math.Abs(decimalPointsQty));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #endregion

        #region ConvertStringToDecimalCurrency

        public static decimal ConvertStringToDecimalCurrency(string Value)
        {
            try
            {
                if (Value == null)
                    Value = "0";
                return Math.Round((Value.Trim() != string.Empty ? Convert.ToDecimal(Value) : 0), decimalPointsCurrency);
                
                
            }
            catch (Exception ex)
            {
                return 0;

            }
        }

       

        #endregion

        #region ConvertStringToDecimalQty
        public static decimal ConvertStringToDecimalQty(string Value)
        {
            try
            {
                if (Value == null)
                    Value = "0";
                return Math.Round((Value.Trim() != string.Empty ? Convert.ToDecimal(Value) : 0), decimalPointsQty);


            }
            catch (Exception ex)
            {
                return 0;

            }
        }
        #endregion

        #region ConvertStringToLong

        public static long ConvertStringToLong(string Value)
        {
            try
            {

                return (long)(Value.Trim() != string.Empty ? Convert.ToDouble(Value) : 0);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #endregion

        #region ConvertStringToDateTime

        public static DateTime ConvertStringToDateTime(string Value)
        {
            try
            {

                return (DateTime)(Value.Trim() != string.Empty ? Convert.ToDateTime(Value) : DateTime.Now);
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }


        
        #endregion

        #region ConvertStringToDate

        public static DateTime ConvertStringToDate(string Value)
        {
            try
            {

                return (DateTime)(Value.Trim() != string.Empty ? Convert.ToDateTime(Value).Date : DateTime.Now);
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }

        public static DateTime ConvertDateTimeToDate(DateTime Value)
        {
            try
            {

                return (DateTime)(Convert.ToDateTime(Value).Date );
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }

        #endregion

        #region FormatDate

        public static DateTime FormatDate(DateTime Value)
        {
            try
            {
                return DateTime.Parse(string.Format("{0:dd/MM/yyyy}", Value));
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }

        public static DateTime FormatDateTime(DateTime Value)
        {
            try
            {
                return DateTime.Parse(string.Format("{0:dd/MM/yyyy hh:mm:ss}", Value));
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }

        #endregion

        #region GetDate

        public static DateTime GetSystemDate()
        {
            return Common.FormatDate(DateTime.Now);
        }

        #endregion

        #region GetDateWithTime

        public static DateTime GetSystemDateWithTime()
        {
            return Common.FormatDateTime(DateTime.Now);
        }


        #endregion

        #region ValidateDate

        public static bool ValidateDate(DateTime docDate)
        {
            //docDate = Common.FormatDate(docDate);
            DateTime dtNow = Common.FormatDate(DateTime.Now);

            if (docDate < dtNow) { return false; }
            else { return true; }
        }

        #endregion

        #region Display String Format
        public static string ConvertStringToDisplayFormat(string displayString)
        {
            string formattedDisplayString = "";
            if (displayString.EndsWith("*"))
            { formattedDisplayString = displayString.Replace(displayString.Substring((displayString.Length - 1), 1), ""); }
            else
            { formattedDisplayString = displayString; }

            return formattedDisplayString;
        }
        #endregion

        //#region ConvertStringToTime

        //public static DateTime ConvertStringToTime(string Value)
        //{
        //    try
        //    {

        //        return (DateTime)(Value.Trim() != string.Empty ? Convert.ToDateTime(Value).TimeOfDay : DateTime.Now);
        //    }
        //    catch (Exception ex)
        //    {
        //        return DateTime.Now;
        //    }
        //}

        //#endregion

        #region SetMenu
        public static void SetMenu(Form form, Form mdiForm)
        {

            try
            {
                bool isChild = false;

                foreach (Form f in mdiForm.MdiChildren)
                {
                    if (f.Name.Equals(form.Name))
                    {
                        isChild = true;
                        break;
                    }
                }

                if (isChild)
                {

                    form.Focus();
                }
                else
                {

                    form.MdiParent = mdiForm;
                    form.Show();
                    form.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), "", Logger.logtype.ErrorLog, Common.LoggedLocationID);
               
            }
        }

        public static void SetMenu1(Telerik.WinControls.UI.RadForm form, Form mdiForm)
        {

            try
            {
                bool isChild = false;

                foreach (Form f in mdiForm.MdiChildren)
                {
                    if (f.Name.Equals(form.Name))
                    {
                        isChild = true;
                        break;
                    }
                }

                if (isChild)
                {

                    form.Focus();
                }
                else
                {

                    form.MdiParent = mdiForm;
                    form.Show();
                    form.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), "", Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }
        #endregion

        #region LINQ To DataTable
        /// <summary>
        /// Convert LINQ query to DataTable 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varlist"></param>
        /// <returns></returns>
        
        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
         
            DataTable dtReturn = new DataTable();
            // column names 
             PropertyInfo[] oProps = null;

             if (varlist == null)
             { return dtReturn;  }

             foreach (T rec in varlist)
             {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                            { colType = colType.GetGenericArguments()[0]; }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                    
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                       dr[pi.Name] = pi.GetValue(rec, null) == null ?DBNull.Value :pi.GetValue
                       (rec,null);
                }

                dtReturn.Rows.Add(dr);
             }
             return dtReturn;
        }

       
        #endregion

        //private DataSet LINQToDataSet(var myDB, IQueryable item)
        //{
        //    SqlCommand cmd = myDB.GetCommand(item) as SqlCommand;

        //    DataTable oDataTable = new DataTable();
        //    SqlDataAdapter oDataAdapter = new SqlDataAdapter(cmd);
        //    oDataAdapter.Fill(oDataTable);

        //    DataSet oDataSet = new DataSet();
        //    oDataSet.Tables.Add(oDataTable);
        //    return oDataSet;
        //}


        /// <summary>
        /// Convert IQueryable query to object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static List<T> ConvertQueryToList<T>(IQueryable<T> query)
        {
            return query.ToList();
        }

        #region Load Payment Methods into combo box
        /// <summary>
        /// Load payment methods into given Combo box control
        /// </summary>
        /// <param name="cmbPaymentMethod"></param>
        public static void LoadPaymentMethods <T>(ComboBox cmbPaymentMethod, List<T> paymentMethods)
        {            
            cmbPaymentMethod.DataSource = paymentMethods;
            cmbPaymentMethod.DisplayMember = "PaymentMethodName";
            cmbPaymentMethod.ValueMember = "PaymentMethodID";
            
            cmbPaymentMethod.SelectedIndex = -1;
        }

        #endregion

        #region Load All Locations into combo box
        /// <summary>
        /// Load All Locations into combo box
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmbLocation"></param>
        /// <param name="locations"></param>
        /// 
        public static void LoadLocations <T>(ComboBox cmbLocation, List<T> locations)
        {
            cmbLocation.DataSource = locations;
            cmbLocation.DisplayMember = "LocationName";
            cmbLocation.ValueMember = "LocationID";
            cmbLocation.SelectedIndex = -1;
        }
        #endregion

        #region Load All Unit Of Measures into combo box
        /// <summary>
        /// Load All Unit Of Measures into combo box
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmbLocation"></param>
        /// <param name="unitOfMeasures"></param>
        public static void LoadUnitOfMeasures<T>(ComboBox cmbUnitOfMeasure, List<T> unitOfMeasures)
        {
            cmbUnitOfMeasure.DataSource = unitOfMeasures;
            cmbUnitOfMeasure.DisplayMember = "UnitOfMeasureName";
            cmbUnitOfMeasure.ValueMember = "UnitOfMeasureID";
            cmbUnitOfMeasure.SelectedIndex = -1;
        }

        #endregion

        #region Load All Banks into combo box
        /// <summary>
        /// Load All Banks into combo box
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmbBank"></param>
        /// <param name="banks"></param>
        public static void LoadAllBanks<T>(ComboBox cmbBank, List<T> banks)
        {
            cmbBank.DataSource = banks;
            cmbBank.DisplayMember = "BankName";
            cmbBank.ValueMember = "BankID";
            cmbBank.SelectedIndex = -1;
        }

        #endregion

        #region Load All Cost centers into combo box
        /// <summary>
        /// Load All Cost centers in to combo box
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmbCostCenter"></param>
        /// <param name="costCenters"></param>
        public static void LoadCostCenters<T>(ComboBox cmbCostCenter, List<T> costCenters)
        {
            cmbCostCenter.DataSource = costCenters;
            cmbCostCenter.DisplayMember = "CostCentreName";
            cmbCostCenter.ValueMember = "CostCentreID";
            cmbCostCenter.SelectedIndex = -1;
        }

        #endregion

        #region Load into combo box
        public static void LoadLoyltyTypes<T>(ComboBox cmbType, List<T> types) 
        {
            cmbType.DataSource = types;
            cmbType.DisplayMember = "LookupValue";
            cmbType.ValueMember = "LookupKey";
            cmbType.SelectedIndex = -1;
        }


        public static void LoadCardTypes<T>(ComboBox cmbLocation, List<T> cardTypes) 
        {
            cmbLocation.DataSource = cardTypes;
            cmbLocation.DisplayMember = "CardName";
            cmbLocation.ValueMember = "CardMasterID"; 
            cmbLocation.SelectedIndex = -1;
        }

        public static void LoadLocationsToCheckList<T>(CheckedListBox chkListLocation, List<T> locations)
        {
            chkListLocation.DataSource = locations;
            chkListLocation.DisplayMember = "LocationName";
            chkListLocation.ValueMember = "LocationID";
        }

        public static void LoadLocationCodes<T>(ComboBox cmbLocation, List<T> locations)
        {
            cmbLocation.DataSource = locations;
            cmbLocation.DisplayMember = "LocationCode";
            cmbLocation.ValueMember = "LocationID";
        }

        public static void LoadTransferTypes<T>(ComboBox cmbTransferTypes, List<T> transferTypes)
        {
            cmbTransferTypes.DataSource = transferTypes;
            cmbTransferTypes.DisplayMember = "TransferType";
            cmbTransferTypes.ValueMember = "InvTransferTypeID";
            cmbTransferTypes.SelectedIndex = -1;
        }

        public static void LoadPOSDocuments<T>(ComboBox cmbReturnDocument, List<T> returnDocuments)
        {
            cmbReturnDocument.DataSource = returnDocuments;
            cmbReturnDocument.DisplayMember = "Receipt";
            cmbReturnDocument.ValueMember = "TransactionDetId";
            cmbReturnDocument.SelectedIndex = -1;
        }

        public static void LoadLgsTransferTypes<T>(ComboBox cmbTransferTypes, List<T> transferTypes)
        {
            cmbTransferTypes.DataSource = transferTypes;
            cmbTransferTypes.DisplayMember = "TransferType";
            cmbTransferTypes.ValueMember = "LgsTransferTypeID";
            cmbTransferTypes.SelectedIndex = -1;
        }

        public static void LoadInvReturnTypes<T>(ComboBox cmbTransferTypes, List<T> returnTypes) 
        {
            cmbTransferTypes.DataSource = returnTypes;
            cmbTransferTypes.DisplayMember = "ReturnType";
            cmbTransferTypes.ValueMember = "InvReturnTypeID"; 
            cmbTransferTypes.SelectedIndex = -1;
        }

        public static void LoadLgsReturnTypes<T>(ComboBox cmbTransferTypes, List<T> returnTypes) 
        {
            cmbTransferTypes.DataSource = returnTypes;
            cmbTransferTypes.DisplayMember = "ReturnType";
            cmbTransferTypes.ValueMember = "LgsReturnTypeID"; 
            cmbTransferTypes.SelectedIndex = -1;
        }

        public static void LoadEmployeeDesignations<T>(ComboBox cmbDesigTypes, List<T> designations) 
        {
            cmbDesigTypes.DataSource = designations;
            cmbDesigTypes.DisplayMember = "Designation";
            cmbDesigTypes.ValueMember = "EmployeeDesignationTypeID";
            cmbDesigTypes.SelectedIndex = -1;
        }
        
        public static void LoadDepartmentNames<T>(ComboBox cmbDepartment, List<T> DepartmentName)
        {
            cmbDepartment.DataSource = DepartmentName;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "InvDepartmentID";
            cmbDepartment.SelectedIndex = -1;
        }

        public static void LoadTOGDocuments<T>(ComboBox cmbTOGDocument, List<T> returnDocuments)
        {
            cmbTOGDocument.DataSource = returnDocuments;
            cmbTOGDocument.DisplayMember = "DocumentNo";
            cmbTOGDocument.ValueMember = "DocumentID";
            cmbTOGDocument.SelectedIndex = -1;
        }

        public static void LoadDamageType<T>(ComboBox cmbDamage, List<T> DamageName)
        {
            cmbDamage.DataSource = DamageName;
            cmbDamage.DisplayMember = "DamageType";
            cmbDamage.ValueMember = "InvDamageTypeID";
            cmbDamage.SelectedIndex = -1;
        }
        #endregion

        public static void LoadItemsToCheckListt<T>(CheckedListBox checkedListBox, List<CheckedListBoxSelection> items, string displayMember, string valueMember)
        {
            checkedListBox.DataSource = items.OrderBy(v=> v.Value).Select(v => v.Value.Trim()).ToList();
            checkedListBox.DisplayMember = displayMember;
            checkedListBox.ValueMember = valueMember;
        }

        public static void LoadItemsToCheckList(CheckedListBox checkedListBox, string[] items, string displayMember, string valueMember)
        {
            checkedListBox.DataSource = items;
            checkedListBox.DisplayMember = displayMember;
            checkedListBox.ValueMember = valueMember;
        }

        public static void LoadStockTakingLayer<T>(ComboBox cmbLayer, List<T> locations)
        {
            cmbLayer.DataSource = locations;
            cmbLayer.DisplayMember = "FormText";
            cmbLayer.ValueMember = "AutoGenerateInfoID";
            cmbLayer.SelectedIndex = -1;
        } 
        
        #region GetSummaryAmount

        public static decimal GetSummaryAmount<T>(this IEnumerable<T> source, Func<T, bool> predicate, Func<T, decimal> valueSelector)
        {
            //return source.Where(predicate)
            //             .Sum(valueSelector);

            return source.Sum(valueSelector);
        }

        public static decimal GetSummaryAmount<T>(this IEnumerable<T> source, Func<T, decimal> valueSelector)
        {
            //return source.Where(predicate)
            //             .Sum(valueSelector);

            return source.Sum(valueSelector);
        }

        public static int GetTotalCount<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            //return source.Where(predicate)
            //             .Sum(valueSelector);

            return source.Count();
        }

        public static int GetTotalCount<T>(this IEnumerable<T> source)
        {
            //return source.Where(predicate)
            //             .Sum(valueSelector);

            return source.Count();
        }
        #endregion

        #region GetTotalAmount

        public static decimal GetTotalAmount(params decimal[] value)
        {
            return value.Sum(x => x);
        }

        #endregion

        #region GetDiscountAmount

        public static decimal GetDiscountAmount(bool isPercentage, decimal amountToDiscount, decimal discountValue)
        {
            if (discountValue > 0)
            {
                if (isPercentage)
                {
                    return (amountToDiscount * discountValue) / 100;
                }
                else
                {
                    return discountValue;
                }
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region Get percentage value
        /// <summary>
        /// Remove character '%'  from given string and return percentage value
        /// </summary>
        /// <param name="percentageString"></param>
        /// <returns></returns>
        public static decimal GetPercentageValue(string percentageString)
        {
            decimal percentageValue = 0;
            if (string.Equals("%", percentageString.Substring(percentageString.Length - 1, 1)))
            {
                return percentageValue = Decimal.Parse(percentageString.Remove(percentageString.Length - 1));
            }
            else
            {
                return percentageValue = Decimal.Parse(percentageString);            
            }
            
        }

        #endregion

        #region Set Form Opacity

        public static void SetFormOpacity(Form form, bool isActive)
        {
            if (isActive)
            { form.Opacity = 1; }
            else
            { form.Opacity = 0.9; }
        }

        #endregion

        #region Load All Promotion Types into combo box
        /// <summary>
        /// Load All Promotion Types into combo box
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmbBank"></param>
        /// <param name="banks"></param>
        public static void LoadAllPromotionTypes<T>(ComboBox cmbPromotionType, List<T> promotionTypes)
        {
            cmbPromotionType.DataSource = promotionTypes;
            cmbPromotionType.DisplayMember = "PromotionTypeName";
            cmbPromotionType.ValueMember = "InvPromotionTypeID";
            cmbPromotionType.SelectedIndex = -1;
        }
        #endregion

        #region Get Selection Criteria For Gift Vouchers
        public static string[] GetGiftVoucherSelectionCriteria(int selectionCriteria)
        {
            string[] giftVoucherSelectionCriteria = new string[] { };
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            if (selectionCriteria == 0)
            {
                dictionary[1] = "Base On Voucher Quantity";
                dictionary[2] = "Base On Voucher Serial Range";
                dictionary[3] = "Base On Voucher No Range";
            }
            else
            {
                dictionary[1] = "Base On Voucher Quantity";
                dictionary[2] = "Base On Voucher Serial Range";
            }

            List<KeyValuePair<int, string>> list = dictionary.ToList();
            int count = 0;
            giftVoucherSelectionCriteria = new string[list.Count];
            foreach (KeyValuePair<int, string> pair in list)
            {
                giftVoucherSelectionCriteria[count] = pair.Value;
                count++;
            }
            return giftVoucherSelectionCriteria;
        }
        #endregion

        #region Copy File 
        public static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
        {
            bool ret = false;
            try
            {
                SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
                DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                        Directory.CreateDirectory(DestinationPath);

                    foreach (string fls in Directory.GetFiles(SourcePath))
                    {
                        FileInfo flinfo = new FileInfo(fls);
                        flinfo.CopyTo(DestinationPath + flinfo.Name, overwriteexisting);
                    }
                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        if (CopyDirectory(drs, DestinationPath + drinfo.Name, overwriteexisting) == false)
                            ret = false;
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }

        public static bool CopyDirectoryToTargetServer(string SourcePath, string DestinationPath, bool overwriteexisting)
        {
            bool ret = false;
            try
            {
                SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
                DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                        Directory.CreateDirectory(DestinationPath);

                    foreach (string fls in Directory.GetFiles(SourcePath))
                    {
                        FileInfo flinfo = new FileInfo(fls);
                        flinfo.CopyTo(DestinationPath + flinfo.Name, overwriteexisting);
                        //Delete file after copy to destination for prevent duplicate
                        //if (System.IO.File.Exists(@fls))
                        //{
                        //    System.IO.File.Delete(@fls);
                        //}
                    }
                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        if (CopyDirectory(drs, DestinationPath + drinfo.Name, overwriteexisting) == false)
                            ret = false;
                    }
                }
                ret = true;




            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }

        public static bool CheckTargetIPStatus(String LocIP)
        {
            try
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();

                // Use the default Ttl value which is 128,
                // but change the fragmentation behavior.
                options.DontFragment = true;
                bool ping = false;
                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 500;
                //check slave server 1
                if (LocIP.Trim() != string.Empty)
                {

                    PingReply reply = pingSender.Send(LocIP, timeout, buffer, options);
                    if (reply.Status == IPStatus.Success)
                    {

                        //Console.WriteLine("Address: {0}", reply.Address.ToString());
                        //Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                        //Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                        //Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                        //Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);

                        //lblServer1Status.Text = LocDescription + " Server On Line";
                        //lblServer1Status.BackColor = Color.Silver;
                        ping = true;
                    }
                    else
                    {
                        //isTargetsvr1Online = false;
                        //lblServer1Status.Text = LocDescription + " Server Is Off Line";
                        //lblServer1Status.BackColor = Color.Red;
                        ping = false;
                    }
                }
                return ping;
            }
            catch (System.Net.NetworkInformation.PingException)
            {
                //return ("Failed: Host unknown.");
                return false;
            }

        }
        
        #endregion

        #region "getobject filled object with property reconized"

        public static List<T> ConvertTo<T>(DataTable datatable) where T : new()
        {
            List<T> Temp = new List<T>();
            try
            {
                List<string> columnsNames = new List<string>();
                foreach (DataColumn DataColumn in datatable.Columns)
                    columnsNames.Add(DataColumn.ColumnName);
                Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => getObject<T>(row, columnsNames));
                return Temp;
            }
            catch
            {
                return Temp;
            }

        }
        public static T getObject<T>(DataRow row, List<string> columnsName) where T : new()
        {
            T obj = new T();
            try
            {
                string columnname = "";
                string value = "";
                PropertyInfo[] Properties;
                Properties = typeof(T).GetProperties();
                //Properties = typeof(T).GetProperties().Where ( P=> !P.GetGetMethod().IsVirtual).ToArray();
                foreach (PropertyInfo objProperty in Properties)
                {
                    Type propertyType = objProperty.PropertyType;
                    columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnname))
                    {
                        value = row[columnname].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                            }
                            else
                            {
                                value = row[columnname].ToString().Replace("%", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                            }

                        }
                    }
                    if (propertyType.IsGenericType)
                    {
                        propertyType = Nullable.GetUnderlyingType(propertyType);
                    }
                }
                return obj;
            }
            catch
            {
                return obj;
            }
        }

        #endregion

        #region validate Machine date time format
        public static bool ValidateMachineDateTimeFromat()
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            DateTimeFormatInfo dtfi = ci.DateTimeFormat;
            string[] SystemDateTimePatterns = new string[250];
            int i = 0;

            foreach (string name in dtfi.GetAllDateTimePatterns('d'))
            {
                SystemDateTimePatterns[i] = name;
                i++;
            }

            string[] myDateTimeFormat = { "dd/MM/yyyy" };
            if (!myDateTimeFormat[0].Equals(SystemDateTimePatterns[0]))
            {
                MessageBox.Show("Your Machine DateTime Format is: " + SystemDateTimePatterns[0] + ".\n" + "Required DateTime Format is: dd/MM/yyyy. \nPlease Change the Datetime Format.", "System Datetime Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }      
        #endregion


        public static bool CopyDirectoryAll(DirectoryInfo source, DirectoryInfo destination)
        {
            try
            {
                if (!destination.Exists)
                {
                    destination.Create();
                }
                
                // Process subdirectories.
                DirectoryInfo[] dirs = source.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    // Get destination directory.
                    string destinationDir = Path.Combine(destination.FullName, dir.Name);

                    DirectoryInfo destinationX = new DirectoryInfo(destinationDir);
 
                    if (!destinationX.Exists)
                    {
                        destinationX.Create();
                    }

                    // Call CopyDirectory() recursively.
                    //CopyDirectoryAll(dir, new DirectoryInfo(destinationDir));

                    // Copy all files.
                    FileInfo[] files = dir.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        file.CopyTo(Path.Combine(destinationX.FullName,
                            file.Name));
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
 
            }
 
        }

        //public static DataTable ExecuteSqlQuery(string sqlQuery)
        //{
        //    DataTable dtQueryResult = new DataTable(); 
        //    SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SysConn"].ConnectionString);
        //    sqlConn.Open();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = sqlConn;
        //    cmd.CommandText = sqlQuery.ToString();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandTimeout = 300;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.Fill(dtQueryResult);

        //    return dtQueryResult;
        //}

        #region SetModuleFeatures
        public static void SetModuleFeature(int systemProductId)
        {
            switch (systemProductId)
            {
                case 1: // Company :1
                    ModuleTypes.InventoryAndSales = true;
                    ModuleTypes.PointOfSales = true;
                    ModuleTypes.Manufacture = false;
                    ModuleTypes.HirePurchase = false;
                    ModuleTypes.CustomerRelationshipModule = true;
                    ModuleTypes.GiftVouchers = true;
                    ModuleTypes.Accounts = true;
                    ModuleTypes.NonTrading = true;
                    ModuleTypes.HrManagement = false;
                    ModuleTypes.HospitalManagement = false;
                    ModuleTypes.HotelManagement = false; 
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
