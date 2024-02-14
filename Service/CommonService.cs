using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain;
using Data;
using System.Reflection;
using Utility;
using System.Data.SqlClient;
using EntityFramework.Extensions;
using System.Data.Common;
using System.Configuration;


namespace Service
{
    public class CommonService
    {
        ERPDbContext context = new ERPDbContext();


        decimal tax1Value = 0;
        decimal tax2Value = 0;
        decimal tax3Value = 0;
        decimal tax4Value = 0;
        decimal tax5Value = 0;
        
        # region Methods

        public static List<Tax> GetAllTaxes()
        {
            ERPDbContext context = new ERPDbContext();
            return context.Taxes.Where(t => t.IsDelete.Equals(false)).ToList();
        }
               

        public DataTable GetAllTaxesRelatedToSupplier(long supplieID)
        {

            DataTable dt = new DataTable();

            var query = (from t in context.Taxes
                         //from s in context.Suppliers
                         //where (new int[] { s.TaxID1, s.TaxID2, s.TaxID3, s.TaxID4, s.TaxID5 }).Contains(t.TaxID) && s.SupplierID == supplieID
                         select new
                         {

                             t.TaxID,
                             t.TaxCode,
                             t.TaxName,
                             t.TaxPercentage,
                             t.EffectivePercentage,
                             t.Tax1,
                             t.Tax2,
                             t.Tax3,
                             t.Tax4,
                             t.Tax5,
                             t.LedgerID
                         });
            return dt = Common.LINQToDataTable(query);

        }

        public DataTable GetAllTaxesRelatedToLgsSupplier(long supplieID)
        {

            DataTable dt = new DataTable();

            var query = (from t in context.Taxes
                         //from s in context.LgsSuppliers
                         //where (new int[] { s.TaxID1, s.TaxID2, s.TaxID3, s.TaxID4, s.TaxID5 }).Contains(t.TaxID) && s.LgsSupplierID == supplieID
                         select new
                         {
                             t.TaxID,
                             t.TaxCode,
                             t.TaxName,
                             t.TaxPercentage,
                             t.EffectivePercentage,
                             t.Tax1,
                             t.Tax2,
                             t.Tax3,
                             t.Tax4,
                             t.Tax5,
                             t.LedgerID
                         });
            return dt = Common.LINQToDataTable(query);

        }


        public DataTable GetAllTaxesRelatedToCustomer(long customerID)
        {

            DataTable dt = new DataTable();

            var query = (from t in context.Taxes
                         //from c in context.Customers
                         //where (new int[] { c.TaxID1, c.TaxID2, c.TaxID3, c.TaxID4, c.TaxID5 }).Contains(t.TaxID) && c.CustomerID == customerID
                         select new
                         {
                             t.TaxID,
                             t.TaxCode,
                             t.TaxName,
                             t.TaxPercentage,
                             t.EffectivePercentage,
                             t.Tax1,
                             t.Tax2,
                             t.Tax3,
                             t.Tax4,
                             t.Tax5,
                             t.LedgerID
                         });
            return dt = Common.LINQToDataTable(query);

        }


        #endregion

        # region CalculateTax

        public decimal CalculateTax(int vendorType, decimal amountToTax, long vendorID, out decimal tax1, out decimal tax2, out decimal tax3, out decimal tax4, out decimal tax5)
        
        {
            
            tax1Value = 0;
            tax2Value = 0;
            tax3Value = 0;
            tax4Value = 0;
            tax5Value = 0;

            if (vendorType == 1)
            {
                DataTable dt = new DataTable();
                dt = GetAllTaxesRelatedToSupplier(vendorID);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //if (i == 0) // Tax1 
                    //{
                    //    tax1Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                    //}
                    //else if (i == 1) // Tax2
                    //{
                    //    tax2Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                    //}
                    //else if (i == 2) // Tax3
                    //{
                    //    tax3Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                    //}
                    //else if (i == 3) // Tax4
                    //{
                    //    tax4Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                    //}
                    //else if (i == 4) // Tax5
                    //{
                    //    tax5Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                    //}
                    //else // More than 5
                    //{

                    //}    

                    Supplier s = new Supplier();
                    SupplierService ss = new SupplierService();

                    s = ss.GetSupplierByID(vendorID);

                    if (i==0) // Tax1 
                    {
                        if (s.TaxID1 == 1)
                        {
                            tax1Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        }
                    }
                    else if (i == 1) // Tax2
                    {
                        if (s.TaxID2 == 2)
                        {
                            tax2Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        }
                    }
                    else if (i == 2) // Tax3
                    {
                        if (s.TaxID3 == 3)
                        {
                            tax3Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        }
                    }
                    else if (i == 3) // Tax4
                    {
                        if (s.TaxID4 == 4)
                        {
                            tax4Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        }
                    }
                    else if (i == 4) // Tax5
                    {
                        if (s.TaxID5 == 5)
                        {
                            tax5Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        }
                    }
                    else // More than 5
                    {

                    }                    
                   
                }

            }
            else if (vendorType == 2)
            {
                DataTable dt = new DataTable();
                dt = GetAllTaxesRelatedToCustomer(vendorID);

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    Customer s = new Customer();
                    CustomerService ss = new CustomerService();

                    s = ss.GetCustomersById(vendorID);

                    if (i == 0) // Tax1
                    {
                        if (s.TaxID1 == 1)
                        {
                            tax1Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        }
                    }
                    else if (i == 1) // Tax2
                    {
                        if (s.TaxID2 == 2)
                        {
                            tax2Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        }
                    }
                    else if (i == 2) // Tax3
                    {
                        if (s.TaxID3 == 3)
                        {
                            tax3Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        }
                    }
                    else if (i == 3) // Tax4
                    {
                        if (s.TaxID4 == 4)
                        {
                            tax4Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        }
                    }
                    else if (i == 4) // Tax5
                    {
                        if (s.TaxID5 == 5)
                        {
                            tax5Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        }
                    }
                    else // More than 5
                    {

                    }

                }
            }

            else if (vendorType == 3)
            {
                DataTable dt = new DataTable();
                dt = GetAllTaxesRelatedToLgsSupplier(vendorID);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    LgsSupplier s = new LgsSupplier();
                    LgsSupplierService ss = new LgsSupplierService();

                    s = ss.GetLgsSupplierByID(vendorID);

                    if (i == 0) // Tax1
                    {
                        if (s.TaxID1 == 1)
                        {
                            tax1Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        }
                    }
                    else if (i == 1) // Tax2
                    {
                        if (s.TaxID2 == 2)
                        {
                            tax2Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        }
                    }
                    else if (i == 2) // Tax3
                    {
                        if (s.TaxID3 == 3)
                        {
                            tax3Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        }
                    }
                    else if (i == 3) // Tax4
                    {
                        if (s.TaxID4 == 4)
                        {
                            tax4Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        }
                    }
                    else if (i == 4) // Tax5
                    {
                        if (s.TaxID5 == 5)
                        {
                            tax5Value = calculateIndividualTaxValue(amountToTax, dt.Rows[i]);
                        }
                    }
                    else // More than 5
                    {

                    }

                }
            }
            tax1 = tax1Value;
            tax2 = tax2Value;
            tax3 = tax3Value;
            tax4 = tax4Value;
            tax5 = tax5Value;
            return (tax1Value + tax2Value + tax3Value + tax4Value + tax5Value);
        }

        public decimal calculateIndividualTaxValue(decimal amountToTax, DataRow drTax)
        {
           
            decimal tax=0;
            decimal calValue = amountToTax;

            bool tax1 = Common.ConvertStringToBool(drTax["Tax1"].ToString());
            bool tax2 = Common.ConvertStringToBool(drTax["Tax2"].ToString());
            bool tax3 = Common.ConvertStringToBool(drTax["Tax3"].ToString());
            bool tax4 = Common.ConvertStringToBool(drTax["Tax4"].ToString());
            bool tax5 = Common.ConvertStringToBool(drTax["Tax5"].ToString());
            decimal effectiveRate = Common.ConvertStringToDecimalCurrency(drTax["EffectivePercentage"].ToString());


            if (tax1 == true)
            {
                calValue = calValue + tax1Value;
            }
            if (tax2 == true)
            {
                calValue = calValue + tax2Value;
            }
            if (tax3 == true)
            {
                calValue = calValue + tax3Value;
            }
            if (tax4 == true)
            {
                calValue = calValue + tax4Value;
            }
            if (tax5 == true)
            {
                calValue = calValue + tax5Value;
            }

            tax = Common.ConvertStringToDecimalCurrency((calValue * effectiveRate / 100).ToString());

            return tax;
        }

        # endregion


        public static bool GetConnectionProperties(out string serverName, out string databaseName, out string userName, out string password)
        {
            serverName = string.Empty;
            databaseName = string.Empty;
            userName = string.Empty;
            password = string.Empty;

            using (ERPDbContext DataContext = new ERPDbContext())
            {
                using (DataContext.Database.Connection)
                {
                    serverName = DataContext.Database.Connection.DataSource;
                    databaseName = DataContext.Database.Connection.Database;
                    userName = DataContext.Database.Connection.ConnectionString.Substring(DataContext.Database.Connection.ConnectionString.IndexOf("User ID="), DataContext.Database.Connection.ConnectionString.Length - DataContext.Database.Connection.ConnectionString.IndexOf("User ID="));
                    userName = userName.Substring(userName.IndexOf("=") + 1, userName.IndexOf(";") - userName.IndexOf("=") - 1);
                    password = DataContext.Database.Connection.ConnectionString.Substring(DataContext.Database.Connection.ConnectionString.IndexOf("Password="), DataContext.Database.Connection.ConnectionString.Length - DataContext.Database.Connection.ConnectionString.IndexOf("Password="));
                    password = password.Substring(password.IndexOf("=") + 1, password.IndexOf(";") - password.IndexOf("=") - 1);
                }
            }
            return true;

        }

        public static bool ExecuteStoredProcedure(string storedProcedure, object[] parameter)
        {

            int result;
            using (ERPDbContext DataContext = new ERPDbContext())
            {
                using (DataContext.Database.Connection)
                {
                    try
                    {

                        DataContext.Database.Connection.Open();
                        IDbCommand cmd = DataContext.Database.Connection.CreateCommand();
                        cmd.CommandText = storedProcedure;
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SqlParameter param in parameter)
                        {
                            cmd.Parameters.Add(param);
                        }

                        cmd.CommandTimeout = 0;
                        result = cmd.ExecuteNonQuery();
                        
                        return true;
                    }

                    catch (Exception ex)
                    {
                        return false;
                    }
                    finally
                    {
                        DataContext.Database.Connection.Close();
                    }
                }
            }
        }


        public static bool ExecuteStoredProcedure(string storedProcedure)
        {
            int result;
            using (ERPDbContext DataContext = new ERPDbContext())
            {
                using (DataContext.Database.Connection)
                {
                    try
                    {
                        DataContext.Database.Connection.Open();
                        IDbCommand cmd = DataContext.Database.Connection.CreateCommand();
                        cmd.CommandText = storedProcedure;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        result = cmd.ExecuteNonQuery();
                        return true;
                    }

                    catch (Exception ex)
                    {
                        return false;
                    }
                    finally
                    {
                        DataContext.Database.Connection.Close();
                    }
                }
            }
        }

        public static string GetConnString()
        {

          
            ERPDbContext DataContext = new ERPDbContext();
           
                        
            return DataContext.Database.Connection.ConnectionString.ToString().Trim();
                    
                

        }


        public static SqlParameter AddSqlParameter(string Name, object Value)
        {
            
                SqlParameter parm = new SqlParameter();
                parm.ParameterName = Name;
                parm.Value = Value;

                return parm;
           


        }

       

        public static string GetConnection()
        {

            ERPDbContext context = new Data.ERPDbContext();
            //System.Data.EntityClient.EntityConnection c = (System.Data.EntityClient.EntityConnection)context.Database.Connection;
            System.Data.Entity.Core.EntityClient.EntityConnection c = (System.Data.Entity.Core.EntityClient.EntityConnection)context.Database.Connection;
            return c.StoreConnection.ConnectionString;

        }
       
        public static void BulkInsert(DataTable dataTable, string destinationTableName)
        {

            ERPDbContext context = new ERPDbContext();
            var entityConn = context.Database.Connection as System.Data.Entity.Core.EntityClient.EntityConnection;
            var dbConn = context.Database.Connection as SqlConnection;

            SqlBulkCopy bulkInsert = new SqlBulkCopy(dbConn);
            bulkInsert.DestinationTableName = destinationTableName;
            dbConn.Open();
            bulkInsert.WriteToServer(dataTable);
            dbConn.Close();
        }

        public static void BulkInsert(DataTable dataTable, string destinationTableName, string mergeSql)
        {
            string sql="";
            ERPDbContext context = new ERPDbContext();
            var entityConn = context.Database.Connection as System.Data.Entity.Core.EntityClient.EntityConnection;
            var dbConn = context.Database.Connection as SqlConnection;

            SqlBulkCopy bulkInsert = new SqlBulkCopy(dbConn);
            dataTable.TableName = "#TEMP";
            bulkInsert.DestinationTableName = "#TEMP";
            dbConn.Open();
            //sql = "SELECT TOP 0 * INTO #TEMP from " + destinationTableName;
            //SqlCommand cmd = new SqlCommand(sql, dbConn);
            //cmd.ExecuteNonQuery();

            SqlCommand cmd = new SqlCommand(CreateTABLEPablo("#TEMP", dataTable), dbConn);
            cmd.ExecuteNonQuery();

            cmd.CommandText = "truncate table #TEMP";
            cmd.ExecuteNonQuery();
            //cmd.CommandText = "ALTER TABLE #TEMP DROP CONSTRAINT #TEMP_id;";
            //cmd.ExecuteNonQuery();
            

            bulkInsert.WriteToServer(dataTable);
            cmd.CommandText = mergeSql;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "drop table #TEMP";
            cmd.ExecuteNonQuery();

            dbConn.Close();
        }

        public static string MergeSqlTo(DataTable dt, string tablName)
        {
            string mergeSql = "";
            mergeSql = " SET IDENTITY_INSERT [dbo]." + tablName + " ON  merge INTO " + tablName + " as Target using #TEMP as Source on Target.";
            mergeSql = mergeSql + "["+ dt.Columns[0].ColumnName.ToString() + "]" + "=Source.[" + dt.Columns[0].ColumnName.ToString() + "] when matched then ";
            mergeSql = mergeSql + "update set ";
            foreach (DataColumn DataColumn in dt.Columns)
            {
                if (DataColumn.ColumnName.ToString() != dt.Columns[0].ColumnName.ToString())
                {
                    mergeSql = mergeSql + "Target.[" + DataColumn.ColumnName.ToString() + "] =Source.[" + DataColumn.ColumnName.ToString() + "], ";
                }
            }

            mergeSql = mergeSql.Remove(mergeSql.Length - 2, 2);

            mergeSql = mergeSql + " when not matched then insert (";
            foreach (DataColumn DataColumn in dt.Columns)
            {
                
                mergeSql = mergeSql + " [" + DataColumn.ColumnName.ToString() + "], ";
                
            }
            mergeSql = mergeSql.Remove(mergeSql.Length - 2, 2);
            mergeSql = mergeSql + ") values (";
            foreach (DataColumn DataColumn in dt.Columns)
            {
                mergeSql = mergeSql + "Source.[" + DataColumn.ColumnName.ToString() + "], ";
            
            }
            mergeSql = mergeSql.Remove(mergeSql.Length - 2, 2);
            mergeSql = mergeSql + ");"+ " SET IDENTITY_INSERT [dbo]." + tablName + " OFF ";

            //mergeSql = "merge INTO dbo.InvDepartment as Target using #temp as Source " +
            //                  "on Target.DepartmentCode=Source.DepartmentCode " +
            //                "when matched then " +
            //                "update set Target.DepartmentName=Source.DepartmentName " +
            //                "when not matched then " +
            //                "insert (DepartmentCode ,DepartmentName ,Remark ,IsDelete ,GroupOfCompanyID ,CreatedUser ,CreatedDate ,ModifiedUser , ModifiedDate ,DataTransfer)" +
            //                   "      values (Source.DepartmentCode ," +
            //                   "     Source.DepartmentName ," +
            //                   "     Source.Remark ,Source.IsDelete ," +
            //                   "     Source.GroupOfCompanyID ,Source.CreatedUser ," +
            //                    "    Source.ModifiedDate ,Source.DataTransfer);";
            return mergeSql;

        }

        public static string MergeSqlToForTrans(DataTable dt, string tablName)
        {
            string mergeSql = "";
            mergeSql = " merge INTO " + tablName + " as Target using #TEMP as Source on Target.";
            mergeSql = mergeSql + "[" + dt.Columns[0].ColumnName.ToString() + "]" + "=Source.[" + dt.Columns[0].ColumnName.ToString() + "] when matched then ";
            mergeSql = mergeSql + "update set ";
            foreach (DataColumn DataColumn in dt.Columns)
            {
                if (DataColumn.ColumnName.ToString() != dt.Columns[0].ColumnName.ToString())
                {
                    mergeSql = mergeSql + "Target.[" + DataColumn.ColumnName.ToString() + "] =Source.[" + DataColumn.ColumnName.ToString() + "], ";
                }
            }

            mergeSql = mergeSql.Remove(mergeSql.Length - 2, 2);

            mergeSql = mergeSql + " when not matched then insert (";
            foreach (DataColumn DataColumn in dt.Columns)
            {
                if (DataColumn.ColumnName.ToString() != dt.Columns[0].ColumnName.ToString())
                {
                    mergeSql = mergeSql + " [" + DataColumn.ColumnName.ToString() + "], ";
                }

            }
            mergeSql = mergeSql.Remove(mergeSql.Length - 2, 2);
            mergeSql = mergeSql + ") values (";
            foreach (DataColumn DataColumn in dt.Columns)
            {
                if (DataColumn.ColumnName.ToString() != dt.Columns[0].ColumnName.ToString())
                {
                    mergeSql = mergeSql + "Source.[" + DataColumn.ColumnName.ToString() + "], ";
                }

            }
            mergeSql = mergeSql.Remove(mergeSql.Length - 2, 2);
            mergeSql = mergeSql + ");";

            //mergeSql = "merge INTO dbo.InvDepartment as Target using #temp as Source " +
            //                  "on Target.DepartmentCode=Source.DepartmentCode " +
            //                "when matched then " +
            //                "update set Target.DepartmentName=Source.DepartmentName " +
            //                "when not matched then " +
            //                "insert (DepartmentCode ,DepartmentName ,Remark ,IsDelete ,GroupOfCompanyID ,CreatedUser ,CreatedDate ,ModifiedUser , ModifiedDate ,DataTransfer)" +
            //                   "      values (Source.DepartmentCode ," +
            //                   "     Source.DepartmentName ," +
            //                   "     Source.Remark ,Source.IsDelete ," +
            //                   "     Source.GroupOfCompanyID ,Source.CreatedUser ," +
            //                    "    Source.ModifiedDate ,Source.DataTransfer);";
            return mergeSql;

        }

        public static void DataRowSqlTo(DataTable dt, string tablName)
        {

            foreach (DataRow row in dt.Rows)
            {
                //foreach (DataColumn c in dt.Columns)
                //{
                    //if (r[c] != null)
                    //{
                    //    Console.WriteLine(r[c]);
                    //}
                    string mergeSql = "";
                    //mergeSql = "INSERT INTO " + tablName + " ( ";
                    //foreach (DataColumn c in dt.Columns)
                    //{
                    //    mergeSql = mergeSql + " [" + c.ColumnName.ToString() + "], ";
                    //}

                    //mergeSql = mergeSql.Remove(mergeSql.Length - 2, 2);

                    //mergeSql = mergeSql + " ) VALUES( ";
                    //foreach (DataColumn c in dt.Columns)
                    //{
                    //    mergeSql = mergeSql + "" + row[c.ColumnName.ToString()] + ", ";
                    //}
                    //mergeSql = mergeSql.Remove(mergeSql.Length - 2, 2);
   
                    //mergeSql = mergeSql + ")";
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Clone();
                    dtNew.Rows.Add(row.ItemArray);
                    CommonService.DataRowInsert(mergeSql, tablName, row[tablName + "ID"].ToString(), dtNew);

                //}
            }
          


        }

        public static void DataRowInsert(string strsql, string tablName, string tableField,DataTable dt)
        {
            string sql = "";
            ERPDbContext context = new ERPDbContext();
            var entityConn = context.Database.Connection as System.Data.Entity.Core.EntityClient.EntityConnection;
            var dbConn = context.Database.Connection as SqlConnection;
 
            dbConn.Open();

            sql = "DELETE FROM " + tablName + " WHERE " + tablName + "ID =" + tableField;
            SqlCommand cmd = new SqlCommand(sql, dbConn);
            cmd.ExecuteNonQuery();

            //SqlBulkCopy bulkInsert = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["SysConn"].ConnectionString, SqlBulkCopyOptions.KeepIdentity & SqlBulkCopyOptions.KeepNulls);
            using (var bulkInsert = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["SysConn"].ConnectionString, SqlBulkCopyOptions.KeepIdentity & SqlBulkCopyOptions.KeepNulls))
            {
                bulkInsert.BatchSize = dt.Rows.Count;
                bulkInsert.DestinationTableName = tablName;
                bulkInsert.ColumnMappings.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        bulkInsert.ColumnMappings.Add(c.ColumnName.ToString(), c.ColumnName.ToString());
                    }
                }
                bulkInsert.WriteToServer(dt);
            }


            dbConn.Close(); 
        }

        private static string CreateTABLEPablo(string tableName, System.Data.DataTable table)
        {
            string sqlsc;
            
               sqlsc = "CREATE TABLE " + tableName + "(";
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    sqlsc += "\n" + "[" + table.Columns[i].ColumnName + "]";
                    if (table.Columns[i].DataType.ToString().Contains("System.Int32"))
                        sqlsc += " int ";
                    else if (table.Columns[i].DataType.ToString().Contains("System.Int64"))
                        sqlsc += " BIGINT ";
                    else if (table.Columns[i].DataType.ToString().Contains("System.DateTime"))
                        sqlsc += " datetime ";
                    else if (table.Columns[i].DataType.ToString().Contains("System.String"))
                        sqlsc += " nvarchar(MAX) ";
                    else if (table.Columns[i].DataType.ToString().Contains("System.Single"))
                        sqlsc += " single ";
                    else if (table.Columns[i].DataType.ToString().Contains("System.Double"))
                        sqlsc += " double ";
                    else if (table.Columns[i].DataType.ToString().Contains("System.Boolean"))
                        sqlsc += " bit ";
                    else if (table.Columns[i].DataType.ToString().Contains("System.Decimal"))
                        sqlsc += " decimal(18, 6) ";
                    else if (table.Columns[i].DataType.ToString().Contains("System.Byte"))
                        sqlsc += " varbinary(MAX)";
                    else
                        sqlsc += " nvarchar(MAX) ";        
                    


                   // if (table.Columns[i].AutoIncrement)
                   //     sqlsc += " IDENTITY(" + table.Columns[i].AutoIncrementSeed.ToString() + "," + table.Columns[i].AutoIncrementStep.ToString() + ") ";
                    if (!table.Columns[i].AllowDBNull)
                        sqlsc += " NOT NULL ";
                    sqlsc += ",";
                }

                //string pks = "\nCONSTRAINT PK_" + tableName + " PRIMARY KEY (";
                //for (int i = 0; i < table.PrimaryKey.Length; i++)
                //{
                //    pks += table.PrimaryKey[i].ColumnName + ",";
                //}
                //pks = pks.Substring(0, pks.Length - 1) + ")";

                //sqlsc += pks;

           
            return sqlsc + ")";
        }

        public static void DeleteData(string TableName,long paramField)
        {
            var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@TableName",Value = TableName},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ColumName", Value = TableName},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@paramField", Value=paramField},
                    };

            CommonService.ExecuteStoredProcedure("spDataTransferDelete", parameter);

        }

        #region Select Data with comparision
        public static IEnumerable<T> Except<T, TKey>(IEnumerable<T> items, IEnumerable<T> other, Func<T, TKey> getKey)
        {
            return from item in items
                   join otherItem in other on getKey(item)
                   equals getKey(otherItem) into tempItems
                   from temp in tempItems.DefaultIfEmpty()
                   where ReferenceEquals(null, temp) || temp.Equals(default(T))
                   select item;
        }
        #endregion

        public List<InvProductSerialNoTemp> UpdateInvSerialNoTemp(List<InvProductSerialNoTemp> invProductSerialTempList, InvProductSerialNoTemp invProductSerial)
        {
            if (invProductSerialTempList.Count.Equals(0) || invProductSerialTempList.Equals(null))
            {
                invProductSerial.LineNo = 1;
            }
            else
            {
                invProductSerial.LineNo = invProductSerialTempList.Where(sl => sl.ProductID.Equals(invProductSerial.ProductID) && sl.UnitOfMeasureID.Equals(invProductSerial.UnitOfMeasureID)).Max(sl => sl.LineNo + 1);
            }

            invProductSerialTempList.Add(invProductSerial);

            return invProductSerialTempList.ToList(); ;
        }

        public List<PriceChangeTemp> UpdatePriceChangeTemp(List<PriceChangeTemp> PriceChangeTempList, PriceChangeTemp priceChangeTemp)
        {
            if (PriceChangeTempList.Count.Equals(0) || PriceChangeTempList.Equals(null))
            {
                priceChangeTemp.LineNo = 1;
            }
            else
            {
                priceChangeTemp.LineNo = PriceChangeTempList.Where(sl => sl.ProductID.Equals(priceChangeTemp.ProductID) && sl.UnitOfMeasureID.Equals(priceChangeTemp.UnitOfMeasureID)).Max(sl => sl.LineNo + 1);
            }

            PriceChangeTempList.Add(priceChangeTemp);

            return PriceChangeTempList.ToList(); ;
        }

        public List<LgsProductSerialNoTemp> UpdateLgsSerialNoTemp(List<LgsProductSerialNoTemp> lgsProductSerialTempList, LgsProductSerialNoTemp lgsProductSerial)
        {
            if (lgsProductSerialTempList.Count.Equals(0) || lgsProductSerialTempList.Equals(null))
            {
                lgsProductSerial.LineNo = 1;
            }
            else
            {
                lgsProductSerial.LineNo = lgsProductSerialTempList.Where(sl => sl.ProductID.Equals(lgsProductSerial.ProductID) && sl.UnitOfMeasureID.Equals(lgsProductSerial.UnitOfMeasureID)).Max(sl => sl.LineNo + 1);
            }

            lgsProductSerialTempList.Add(lgsProductSerial);

            return lgsProductSerialTempList.ToList(); ;
        }

        public InvProductBatchNoExpiaryDetail CheckInvBatchNumber(string batchNo, long productID, long baseUnitID)
        {
            //return context.InvProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo) && a.ProductID.Equals(productID) && a.LocationID.Equals(locationID) && a.UnitOfMeasureID.Equals(baseUnitID) && a.BalanceQty > 0).FirstOrDefault();
            return context.InvProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo) && a.ProductID.Equals(productID) && a.UnitOfMeasureID.Equals(baseUnitID)).FirstOrDefault();
        }

        public InvProductBatchNoExpiaryDetail CheckInvBatchNumber(string batchNo, long productID, int locationID, long baseUnitID)
        {
            //return context.InvProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo) && a.ProductID.Equals(productID) && a.LocationID.Equals(locationID) && a.UnitOfMeasureID.Equals(baseUnitID) && a.BalanceQty > 0).FirstOrDefault();
            return context.InvProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo) && a.ProductID.Equals(productID) && a.LocationID.Equals(locationID) && a.UnitOfMeasureID.Equals(baseUnitID)).FirstOrDefault();
        }

        public LgsProductBatchNoExpiaryDetail CheckLgsBatchNumber(string batchNo, long productID, int locationID, long baseUnitID) 
        {
            //return context.LgsProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo) && a.ProductID.Equals(productID) && a.LocationID.Equals(locationID) && a.UnitOfMeasureID.Equals(baseUnitID) && a.BalanceQty > 0).FirstOrDefault();
            return context.LgsProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo) && a.ProductID.Equals(productID) && a.LocationID.Equals(locationID) && a.UnitOfMeasureID.Equals(baseUnitID)).FirstOrDefault();
        }

        public InvProductMaster CheckInvProductCode(long productID, long baseUnitID)
        {
            return context.InvProductMasters.Where(a => a.InvProductMasterID.Equals(productID) && a.UnitOfMeasureID.Equals(baseUnitID)).FirstOrDefault();
        }
 
        public InvProductBatchNoExpiaryDetail CheckInvBatchNumber(string batchNo)
        {
            return context.InvProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo)).FirstOrDefault();
        }

        public LgsProductBatchNoExpiaryDetail CheckLgsBatchNumber(string batchNo) 
        {
            return context.LgsProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo)).FirstOrDefault();
        }

        public List<LgsProductStockMaster> GetLgsStockDetailsToStockGrid(LgsProductMaster lgsProductMaster)
        {
            LgsProductStockMaster lgsProductStockMaster;

            List<LgsProductStockMaster> lgsProductStockMasterList = new List<LgsProductStockMaster>();

            var stock = (from pm in context.LgsProductStockMasters
                         join lo in context.Locations on pm.LocationID equals lo.LocationID
                         where pm.ProductID.Equals(lgsProductMaster.LgsProductMasterID)
                         select new
                         {
                             lo.LocationID,
                             LocationName = lo.LocationName,
                             ReOrderQuantity = pm.ReOrderQuantity,
                             ReOrderLevel = pm.ReOrderLevel,
                             Stock = pm.Stock
                         }).ToArray().OrderBy(l=>l.LocationID);

            foreach (var tempProduct in stock)
            {

                lgsProductStockMaster = new LgsProductStockMaster();

                lgsProductStockMaster.LocationName = tempProduct.LocationName;
                lgsProductStockMaster.ReOrderQuantity = tempProduct.ReOrderQuantity;
                lgsProductStockMaster.ReOrderLevel = tempProduct.ReOrderLevel;
                lgsProductStockMaster.Stock = tempProduct.Stock;

                lgsProductStockMasterList.Add(lgsProductStockMaster);
            }
            return lgsProductStockMasterList.OrderBy(pd => pd.LocationID).ToList();
        }

        public List<LgsProductBatchNoExpiaryDetail> GetLgsBatchStockDetailsToStockGrid(long productID, int locationId) 
        {
            LgsProductBatchNoExpiaryDetail lgsProductBatchNoExpiaryDetail;

            List<LgsProductBatchNoExpiaryDetail> lgsProductBatchStockList = new List<LgsProductBatchNoExpiaryDetail>();

            var stock = (from pm in context.LgsProductBatchNoExpiaryDetails
                         where pm.ProductID.Equals(productID) && pm.LocationID.Equals(locationId) && pm.BalanceQty > 0
                         select new
                         {
                             pm.BatchNo,
                             Stock = pm.BalanceQty,
                             pm.LgsProductBatchNoExpiaryDetailID
                         }).ToArray().OrderBy(l => l.BatchNo);

            foreach (var tempProduct in stock)
            {

                lgsProductBatchNoExpiaryDetail = new LgsProductBatchNoExpiaryDetail();
                lgsProductBatchNoExpiaryDetail.BatchNo = tempProduct.BatchNo;
                lgsProductBatchNoExpiaryDetail.BalanceQty = tempProduct.Stock;
                lgsProductBatchNoExpiaryDetail.LgsProductBatchNoExpiaryDetailID = tempProduct.LgsProductBatchNoExpiaryDetailID;

                lgsProductBatchStockList.Add(lgsProductBatchNoExpiaryDetail);
            }
            return lgsProductBatchStockList.OrderBy(pd => pd.BatchNo).ToList();
        }

        public List<InvProductStockMaster> GetInvStockDetailsToStockGrid(InvProductMaster invProductMaster)
        {
            InvProductStockMaster invProductStockMaster;

            List<InvProductStockMaster> invProductStockMasterList = new List<InvProductStockMaster>();

            var stock = (from pm in context.InvProductStockMasters
                         join lo in context.Locations on pm.LocationID equals lo.LocationID
                         where pm.ProductID.Equals(invProductMaster.InvProductMasterID)
                         select new
                         {
                             lo.LocationID,
                             LocationName = lo.LocationName,
                             ReOrderQuantity = pm.ReOrderQuantity,
                             ReOrderLevel = pm.ReOrderLevel,
                             Stock = pm.Stock
                         }).ToArray().OrderBy(l=>l.LocationID);

            foreach (var tempProduct in stock)
            {

                invProductStockMaster = new InvProductStockMaster();

                invProductStockMaster.LocationName = tempProduct.LocationName;
                invProductStockMaster.ReOrderQuantity = tempProduct.ReOrderQuantity;
                invProductStockMaster.ReOrderLevel = tempProduct.ReOrderLevel;
                invProductStockMaster.Stock = tempProduct.Stock;

                invProductStockMasterList.Add(invProductStockMaster);
            }
            return invProductStockMasterList.OrderBy(pd => pd.LocationID).ToList();
        }

        public List<InvProductBatchNoExpiaryDetail> GetInvBatchStockDetailsToStockGrid(long productID, int locationId)
        {
            InvProductBatchNoExpiaryDetail InvProductBatchNoExpiaryDetail;

            List<InvProductBatchNoExpiaryDetail> InvProductBatchStockList = new List<InvProductBatchNoExpiaryDetail>();

            var stock = (from pm in context.InvProductBatchNoExpiaryDetails
                         where pm.ProductID.Equals(productID) && pm.LocationID.Equals(locationId) && pm.BalanceQty > 0
                         select new
                         {
                             pm.BatchNo,
                             Stock = pm.BalanceQty
                         }).ToArray().OrderBy(l => l.BatchNo);

            foreach (var tempProduct in stock)
            {

                InvProductBatchNoExpiaryDetail = new InvProductBatchNoExpiaryDetail();
                InvProductBatchNoExpiaryDetail.BatchNo = tempProduct.BatchNo;
                InvProductBatchNoExpiaryDetail.BalanceQty = tempProduct.Stock;

                InvProductBatchStockList.Add(InvProductBatchNoExpiaryDetail);
            }
            return InvProductBatchStockList.OrderBy(pd => pd.BatchNo).ToList();
        }

        public bool ValidateCurrentStock(decimal orderQty, InvProductMaster invProductMaster, int locationId)
        {
            decimal currentStock = 0;
            var getCurrentStock = (from psm in context.InvProductStockMasters
                                   where psm.ProductID.Equals(invProductMaster.InvProductMasterID) && psm.LocationID.Equals(locationId)
                                   select new
                                   {
                                       psm.Stock
                                   }).ToArray();

            foreach (var temp in getCurrentStock)
            {
                currentStock = temp.Stock;
            }

            if (orderQty > currentStock)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        public bool ValidateCurrentStock(decimal orderQty, LgsProductMaster lgsProductMaster, int locationId)
        {
            decimal currentStock = 0;
            var getCurrentStock = (from psm in context.LgsProductStockMasters
                                   where psm.ProductID.Equals(lgsProductMaster.LgsProductMasterID) && psm.LocationID.Equals(locationId)
                                   select new
                                   {
                                       psm.Stock
                                   }).ToArray();

            foreach (var temp in getCurrentStock)
            {
                currentStock = temp.Stock;
            }

            if (orderQty > currentStock)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidateBatchStock(decimal orderQty, LgsProductMaster lgsProductMaster, int locationId, string batchNo, decimal convertFactor)  
        {
            decimal currentbatchStock = 0;
            var getCurrentStock = (from bd in context.LgsProductBatchNoExpiaryDetails
                                   where bd.ProductID.Equals(lgsProductMaster.LgsProductMasterID) && bd.LocationID.Equals(locationId) && bd.BatchNo.Equals(batchNo)
                                   select new
                                   {
                                       bd.BalanceQty
                                   }).ToArray();

            foreach (var temp in getCurrentStock)
            {
                currentbatchStock = temp.BalanceQty;
            }

            if (orderQty > currentbatchStock)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidateBatchStockForLgsTOG(decimal orderQty, LgsProductMaster lgsProductMaster, int locationId, string batchNo, decimal convertFactor, long batchExpiaryDetailID)
        { 
            decimal currentbatchStock = 0;
            var getCurrentStock = (from bd in context.LgsProductBatchNoExpiaryDetails
                                   where bd.ProductID.Equals(lgsProductMaster.LgsProductMasterID) && bd.LocationID.Equals(locationId) && bd.BatchNo.Equals(batchNo)
                                   && bd.LgsProductBatchNoExpiaryDetailID.Equals(batchExpiaryDetailID)
                                   select new
                                   {
                                       bd.BalanceQty
                                   }).ToArray();

            foreach (var temp in getCurrentStock)
            {
                currentbatchStock = temp.BalanceQty;
            }

            if (orderQty > currentbatchStock)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidateBatchStock(decimal orderQty, InvProductMaster invProductMaster, int locationId, string batchNo, decimal convertFactor)
        {
            decimal currentbatchStock = 0;
            var getCurrentStock = (from bd in context.InvProductBatchNoExpiaryDetails
                                   where bd.ProductID.Equals(invProductMaster.InvProductMasterID) && bd.LocationID.Equals(locationId) && bd.BatchNo.Equals(batchNo) && bd.UnitOfMeasureID.Equals(invProductMaster.UnitOfMeasureID)
                                   select new
                                   {
                                       bd.BalanceQty
                                   }).ToArray();

            foreach (var temp in getCurrentStock)
            {
                currentbatchStock = temp.BalanceQty;
            }

            if ((orderQty * convertFactor) > currentbatchStock)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static UserPrivileges GetUserPrivilegesByUserIDandLocation(long userID, long locationID, long transactionID)
        {
            ERPDbContext context = new Data.ERPDbContext();
            UserPrivileges userPrivileges =null;

            var userPrivilegesDetails = (from tg in context.UserPrivileges
                                         join pl in context.UserPrivilegesLocations on tg.UserMasterID equals pl.UserMasterID
                                         where tg.UserMasterID == userID && pl.LocationID == locationID && tg.FormID == transactionID && pl.IsSelect == true
                                         select new
                                         {
                                             tg.UserMasterID,
                                             tg.IsAccess,
                                             tg.IsModify,
                                             tg.IsSave,
                                             tg.IsPause,
                                             tg.IsView,
                                             tg.UserPrivilegesID

                                         }).ToArray();

            var x = userPrivilegesDetails.Count();
            foreach (var tempPrivileges in userPrivilegesDetails)
            {
                userPrivileges = new UserPrivileges();
                userPrivileges.UserMasterID = tempPrivileges.UserMasterID;
                userPrivileges.IsAccess = tempPrivileges.IsAccess;
                userPrivileges.IsPause = tempPrivileges.IsPause;
                userPrivileges.IsSave = tempPrivileges.IsSave;
                userPrivileges.IsModify = tempPrivileges.IsModify;
                userPrivileges.IsView = tempPrivileges.IsView;
                userPrivileges.UserPrivilegesID = tempPrivileges.UserPrivilegesID;

            }

            return userPrivileges;
        }


        public static UserPrivileges GetUserPrivilegesByUserIDLocationAndPassword(long userID, long locationID, string password)
        {
            ERPDbContext context = new Data.ERPDbContext();
            UserPrivileges userPrivileges = null;

            var userPrivilegesDetails = (from up in context.UserMaster
                                         join tg in context.UserPrivileges on up.UserMasterID equals tg.UserMasterID
                                         join pl in context.UserPrivilegesLocations on up.UserMasterID equals pl.UserMasterID
                                         where up.UserMasterID == userID && pl.LocationID == locationID
                                         && up.Password == password
                                         select new
                                         {
                                             up.UserMasterID,
                                             up.UserGroupID,
                                             up.UserName,
                                             up.UserDescription,
                                             up.Password,
                                             up.IsUserCantChangePassword,
                                             up.IsActive,
                                             up.IsUserMustChangePassword,
                                             tg.IsAccess,
                                             tg.IsModify,
                                             tg.IsSave,
                                             tg.IsPause,
                                             tg.IsView,
                                             tg.UserPrivilegesID

                                         }).ToArray();

            
                foreach (var tempPrivileges in userPrivilegesDetails)
                {
                    userPrivileges = new UserPrivileges();

                    userPrivileges.UserMasterID = tempPrivileges.UserMasterID;
                    userPrivileges.UserPassword = tempPrivileges.Password;
                    userPrivileges.UserDescription = tempPrivileges.UserDescription;
                    userPrivileges.IsAccess = tempPrivileges.IsAccess;
                    userPrivileges.IsPause = tempPrivileges.IsPause;
                    userPrivileges.IsSave = tempPrivileges.IsSave;
                    userPrivileges.IsModify = tempPrivileges.IsModify;
                    userPrivileges.IsView = tempPrivileges.IsView;
                    userPrivileges.UserPrivilegesID = tempPrivileges.UserPrivilegesID;

                }

                return userPrivileges;
           
        }

        public List<UserPrivileges> GetAccessPrivilegesByUserIDandLocation(long userID, int locationID)
        {
            ERPDbContext context = new Data.ERPDbContext();
            UserPrivileges userPrivilegesInfo;

            List<UserPrivileges> userPrivilegesList = new List<UserPrivileges>();

            var userPrivilegesDetails = (from up in context.UserPrivileges
                                         join tg in context.TransactionRights on up.TransactionRightsID equals tg.TransactionRightsID
                                         join pl in context.UserPrivilegesLocations on up.UserMasterID equals pl.UserMasterID
                                         where up.UserMasterID == userID && pl.LocationID == locationID && up.IsAccess == true
                                         select new
                                         {
                                             up.UserMasterID,
                                             up.IsAccess,
                                             up.IsModify,
                                             up.IsSave,
                                             up.IsPause,
                                             up.IsView,
                                             up.UserPrivilegesID,
                                             tg.TransactionRightsID,
                                             tg.TransactionCode


                                         }).ToArray();

            foreach (var tempPrivileges in userPrivilegesDetails)
            {
                userPrivilegesInfo = new UserPrivileges();

                userPrivilegesInfo.UserMasterID = tempPrivileges.UserMasterID;
                userPrivilegesInfo.IsAccess = tempPrivileges.IsAccess;
                userPrivilegesInfo.IsPause = tempPrivileges.IsPause;
                userPrivilegesInfo.IsSave = tempPrivileges.IsSave;
                userPrivilegesInfo.IsModify = tempPrivileges.IsModify;
                userPrivilegesInfo.IsView = tempPrivileges.IsView;
                userPrivilegesInfo.UserPrivilegesID = tempPrivileges.UserPrivilegesID;
                userPrivilegesInfo.TransactionRightsID = tempPrivileges.TransactionRightsID;
                userPrivilegesInfo.TransactionCode = tempPrivileges.TransactionCode;

                userPrivilegesList.Add(userPrivilegesInfo);

            }
            return userPrivilegesList.OrderBy(pd => pd.TransactionRightsID).ToList();
        }

        public SystemConfig GetSystemInfo(int productId)
        {
              return context.SystemConfig.Where(a => a.ProductID.Equals(productId)).FirstOrDefault();
        }

        public SystemFeature GetSystemFeatures(int productId) 
        {
            return context.SystemFeatures.Where(a => a.ProductID.Equals(productId)).FirstOrDefault();
        }

        public string GetCostingMethodByLocation(int locationID) 
        {
            string costingMethod = "";
            Location location = new Location();
            LocationService locationService = new LocationService();
            location = locationService.GetLocationsByID(locationID);

            if (location != null)
            {
                costingMethod = location.CostingMethod;
            }

            return costingMethod;
        }

        public static DataTable ExecuteSqlQuery(string sqlQuery)
        {
            DataTable dtQueryResult = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SysConn"].ConnectionString);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConn;
            cmd.CommandText = sqlQuery.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtQueryResult);

            return dtQueryResult;
        }

        public static bool ExecuteSqlstatement(string sqlstatement)
        {

            int result;
            using (ERPDbContext DataContext = new ERPDbContext())
            {
                using (DataContext.Database.Connection)
                {
                    try
                    {

                        DataContext.Database.Connection.Open();
                        IDbCommand cmd = DataContext.Database.Connection.CreateCommand();
                        cmd.CommandText = sqlstatement;
                        cmd.CommandType = CommandType.Text;

                        cmd.CommandTimeout = 0;
                        result = cmd.ExecuteNonQuery();
                        return true;
                    }

                    catch (Exception ex)
                    {
                        return false;
                    }
                    finally
                    {
                        DataContext.Database.Connection.Close();
                    }
                }
            }
        }

        //public static bool ExecuteSqlQuery(string sqlQuery)
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
    }
}
