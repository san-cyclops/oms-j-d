using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Data;
using System.Data;
using Utility;
using EntityFramework.Extensions;
using System.Transactions;
using System.Data.Common;
using System.Data.Entity;
using MoreLinq;
using System.Collections;
using EntityFramework.Extensions;
using MoreLinq;
using System.Data.Sql;
using System.Data.SqlClient;

using System.IO;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System.Threading;
using System.Configuration;

namespace Service
{
    public class GivenDateStockService
    {
        SqlConnection mscn = new SqlConnection(ConfigurationManager.ConnectionStrings["SysConn"].ConnectionString);
        ERPDbContext context = new ERPDbContext();

        private string sqlStatement;

        public string SqlStatement
        {
            get { return sqlStatement; }
            set { sqlStatement = value; }
        }

        private bool isExsists;

        public bool IsExsists
        {
            get { return isExsists; }
            set { isExsists = value; }
        }

        private DataSet dsGetGivenDateStock = new DataSet();

        public DataSet DsGetGivenDateStock
        {
            get { return dsGetGivenDateStock; }
            set { dsGetGivenDateStock = value; }
        }

        private DataSet dsGetDataTables = new DataSet();

        public DataSet DsGetDataTables
        {
            get { return dsGetDataTables; }
            set { dsGetDataTables = value; }
        }

        public bool View(int typeId, int locationId, DateTime givenDate, long fromId, long toId, long uniqueId, string fromCode, string toCode)
        {

            {

                var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CompanyId", Value=Common.LoggedCompanyID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@SelectedLocationID", Value=locationId},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@GivenDate", Value=givenDate},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@TypeId", Value=typeId},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@FromId", Value=fromId},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ToId", Value=toId}, 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@FromCode", Value=fromCode}, 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ToCode", Value=toCode}, 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@UserId",Value=Common.LoggedUserId}, 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@UniqueId", Value=uniqueId},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CreatedUser", Value=Common.LoggedUser}
                    };


                if (CommonService.ExecuteStoredProcedure("spCalculateGivenDateStock", parameter))
                {

                    return true;
                }
                else
                { return false; }

            }
        }

        
        public DataTable GetGivenDateStock(int locationId, string fromCode, string toCode)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = from d in context.InvTmpProductStockDetails
                            join lc in context.Locations on d.LocationID equals lc.LocationID
                            where d.UserID.Equals(Common.LoggedUserId) && d.LocationID.Equals(locationId) && d.ProductCode.CompareTo(fromCode) >= 0 && d.ProductCode.CompareTo(toCode) <= 0
                            select new
                            {
                                d.ProductID,
                                d.ProductCode,
                                d.ProductName,
                                d.DepartmentID,
                                d.CategoryID,
                                d.SubCategoryID,
                                d.SubCategory2ID,
                                d.CostPrice,
                                d.SellingPrice,
                                d.StockQty,
                                d.LocationID,
                                lc.LocationCode,
                                lc.LocationName
                            };
                return query.ToDataTable();
            }

        }

        public DataTable GetGivenDateStockAllLocation(string fromCode, string toCode)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = from d in context.InvTmpProductStockDetails.AsNoTracking()
                            join lc in context.Locations on d.LocationID equals lc.LocationID
                            where d.UserID.Equals(Common.LoggedUserId) && d.ProductCode.CompareTo(fromCode) >= 0 && d.ProductCode.CompareTo(toCode) <= 0
                            select new
                            {
                                d.ProductID,
                                d.ProductCode,
                                d.ProductName,
                                d.DepartmentID,
                                d.CategoryID,
                                d.SubCategoryID,
                                d.SubCategory2ID,
                                d.CostPrice,
                                d.SellingPrice,
                                d.StockQty,
                                d.LocationID,
                                lc.LocationCode,
                                lc.LocationName
                            };
                return query.ToDataTable();
            }

        }

        public DataTable GetGivenDateStockDepartmentWise(int locationId, string fromCode, string toCode)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = from d in context.InvTmpProductStockDetails
                            join lc in context.Locations on d.LocationID equals lc.LocationID
                            join de in context.InvDepartments on d.DepartmentID equals de.InvDepartmentID
                            where d.UserID.Equals(Common.LoggedUserId) && d.LocationID.Equals(locationId) && de.DepartmentCode.CompareTo(fromCode) >= 0 && de.DepartmentCode.CompareTo(toCode) <= 0
                            select new
                            {
                                d.ProductID,
                                d.ProductCode,
                                d.ProductName,
                                d.DepartmentID,
                                d.CategoryID,
                                d.SubCategoryID,
                                d.SubCategory2ID,
                                d.CostPrice,
                                d.SellingPrice,
                                d.StockQty,
                                d.LocationID,
                                lc.LocationCode,
                                lc.LocationName,
                                de.DepartmentCode,
                                de.DepartmentName
                            };
                return query.ToDataTable();
            }

        }

        public DataTable GetGivenDateStockDepartmentWiseAllLocation(string fromCode, string toCode)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = from d in context.InvTmpProductStockDetails
                            join lc in context.Locations on d.LocationID equals lc.LocationID
                            join de in context.InvDepartments on d.DepartmentID equals de.InvDepartmentID
                            where d.UserID.Equals(Common.LoggedUserId) && de.DepartmentCode.CompareTo(fromCode) >= 0 && de.DepartmentCode.CompareTo(toCode) <= 0
                            select new
                            {
                                d.ProductID,
                                d.ProductCode,
                                d.ProductName,
                                d.DepartmentID,
                                d.CategoryID,
                                d.SubCategoryID,
                                d.SubCategory2ID,
                                d.CostPrice,
                                d.SellingPrice,
                                d.StockQty,
                                d.LocationID,
                                lc.LocationCode,
                                lc.LocationName,
                                de.DepartmentCode,
                                de.DepartmentName
                            };
                return query.ToDataTable();
            }

        }

        public DataTable GetGivenDateStockCategoryWise(int locationId, string fromCode, string toCode)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = from d in context.InvTmpProductStockDetails
                            join lc in context.Locations on d.LocationID equals lc.LocationID
                            join de in context.InvCategories on d.CategoryID equals de.InvCategoryID
                            where d.UserID.Equals(Common.LoggedUserId) && d.LocationID.Equals(locationId) && de.CategoryCode.CompareTo(fromCode) >= 0 && de.CategoryCode.CompareTo(toCode) <= 0
                            select new
                            {
                                d.ProductID,
                                d.ProductCode,
                                d.ProductName,
                                d.DepartmentID,
                                d.CategoryID,
                                d.SubCategoryID,
                                d.SubCategory2ID,
                                d.CostPrice,
                                d.SellingPrice,
                                d.StockQty,
                                d.LocationID,
                                lc.LocationCode,
                                lc.LocationName,
                                de.CategoryCode,
                                de.CategoryName
                            };
                return query.ToDataTable();
            }

        }

        public DataTable GetGivenDateStockCategoryWiseAllLocation(string fromCode, string toCode)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = from d in context.InvTmpProductStockDetails
                            join lc in context.Locations on d.LocationID equals lc.LocationID
                            join de in context.InvCategories on d.CategoryID equals de.InvCategoryID
                            where d.UserID.Equals(Common.LoggedUserId) && de.CategoryCode.CompareTo(fromCode) >= 0 && de.CategoryCode.CompareTo(toCode) <= 0
                            select new
                            {
                                d.ProductID,
                                d.ProductCode,
                                d.ProductName,
                                d.DepartmentID,
                                d.CategoryID,
                                d.SubCategoryID,
                                d.SubCategory2ID,
                                d.CostPrice,
                                d.SellingPrice,
                                d.StockQty,
                                d.LocationID,
                                lc.LocationCode,
                                lc.LocationName,
                                de.CategoryCode,
                                de.CategoryName
                            };
                return query.ToDataTable();
            }

        }

        public DataTable GetGivenDateStockSubCategoryWise(int locationId, string fromCode, string toCode)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = from d in context.InvTmpProductStockDetails
                            join lc in context.Locations on d.LocationID equals lc.LocationID
                            join de in context.InvSubCategories on d.SubCategoryID equals de.InvSubCategoryID
                            where d.UserID.Equals(Common.LoggedUserId) && d.LocationID.Equals(locationId) && de.SubCategoryCode.CompareTo(fromCode) >= 0 && de.SubCategoryCode.CompareTo(toCode) <= 0
                            select new
                            {
                                d.ProductID,
                                d.ProductCode,
                                d.ProductName,
                                d.DepartmentID,
                                d.CategoryID,
                                d.SubCategoryID,
                                d.SubCategory2ID,
                                d.CostPrice,
                                d.SellingPrice,
                                d.StockQty,
                                d.LocationID,
                                lc.LocationCode,
                                lc.LocationName,
                                de.SubCategoryCode,
                                de.SubCategoryName
                            };
                return query.ToDataTable();
            }

        }

        public DataTable GetGivenDateStockSubCategoryWiseAllLocation(string fromCode, string toCode)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = from d in context.InvTmpProductStockDetails
                            join lc in context.Locations on d.LocationID equals lc.LocationID
                            join de in context.InvSubCategories on d.SubCategoryID equals de.InvSubCategoryID
                            where d.UserID.Equals(Common.LoggedUserId) && de.SubCategoryCode.CompareTo(fromCode) >= 0 && de.SubCategoryCode.CompareTo(toCode) <= 0
                            select new
                            {
                                d.ProductID,
                                d.ProductCode,
                                d.ProductName,
                                d.DepartmentID,
                                d.CategoryID,
                                d.SubCategoryID,
                                d.SubCategory2ID,
                                d.CostPrice,
                                d.SellingPrice,
                                d.StockQty,
                                d.LocationID,
                                lc.LocationCode,
                                lc.LocationName,
                                de.SubCategoryCode,
                                de.SubCategoryName
                            };
                return query.ToDataTable();
            }

        }

        public DataTable GetGivenDateStockSupplierWise(int locationId, string fromCode, string toCode)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = from d in context.InvTmpProductStockDetails
                            join lc in context.Locations on d.LocationID equals lc.LocationID
                            join de in context.Suppliers on d.SupplierID equals de.SupplierID
                            where d.UserID.Equals(Common.LoggedUserId) && d.LocationID.Equals(locationId) && de.SupplierCode.CompareTo(fromCode) >= 0 && de.SupplierCode.CompareTo(toCode) <= 0
                            select new
                            {
                                d.ProductID,
                                d.ProductCode,
                                d.ProductName,
                                d.DepartmentID,
                                d.SupplierID,
                                d.CategoryID,
                                d.SubCategoryID,
                                d.SubCategory2ID,
                                d.CostPrice,
                                d.SellingPrice,
                                d.StockQty,
                                d.LocationID,
                                lc.LocationCode,
                                lc.LocationName,
                                de.SupplierCode,
                                de.SupplierName
                            };
                return query.ToDataTable();
            }

        }

        public DataTable GetGivenDateStockSupplierWiseAllLocation(string fromCode, string toCode)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = from d in context.InvTmpProductStockDetails
                            join lc in context.Locations on d.LocationID equals lc.LocationID
                            join de in context.Suppliers on d.SupplierID equals de.SupplierID
                            where d.UserID.Equals(Common.LoggedUserId) && de.SupplierCode.CompareTo(fromCode) >= 0 && de.SupplierCode.CompareTo(toCode) <= 0
                            select new
                            {
                                d.ProductID,
                                d.ProductCode,
                                d.ProductName,
                                d.DepartmentID,
                                d.SupplierID,
                                d.CategoryID,
                                d.SubCategoryID,
                                d.SubCategory2ID,
                                d.CostPrice,
                                d.SellingPrice,
                                d.StockQty,
                                d.LocationID,
                                lc.LocationCode,
                                lc.LocationName,
                                de.SupplierCode,
                                de.SupplierName
                            };
                return query.ToDataTable();
            }

        }
    }
}
