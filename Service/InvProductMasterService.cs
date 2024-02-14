using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using Domain;
using Data;
using System.Data;
using Utility;
using System.Data.Entity;
using EntityFramework.Extensions;
using System.Data.Entity.Core.Objects;
using MoreLinq;
using EntityFramework.Extensions;

namespace Service
{
    public class InvProductMasterService
    {
        ERPDbContext context = new ERPDbContext();
        InvProductMaster invProductMaster = new InvProductMaster();


        #region methods


        /// Save new Product

        public void AddProduct(InvProductMaster invProductMaster)
        {

            context.InvProductMasters.Add(invProductMaster);
            context.SaveChanges();
        }


        /// Update existing Product

        public void UpdateProduct(InvProductMaster invProductMaster)
        {
            invProductMaster.ModifiedUser = Common.LoggedUser;
            invProductMaster.ModifiedDate = Common.GetSystemDateWithTime();
            invProductMaster.DataTransfer = 0;
            this.context.Entry(invProductMaster).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void DeleteProduct(InvProductMaster invProductMaster) 
        {
            invProductMaster.IsDelete = true;
            this.context.Entry(invProductMaster).State = EntityState.Modified;
            this.context.SaveChanges();
        }


        // data transfer
        public void UpdateProduct(int datatransfer)
        {
            context.InvProductMasters.Update(ps => ps.DataTransfer.Equals(1), ps => new InvProductMaster { DataTransfer = datatransfer });
        }
        public void UpdateProductDtSelect(int datatransfer)
        {
            var qry = (from d in context.InvProductMasters where d.DataTransfer.Equals(0) select d).Take(100).ToArray();

            foreach (var temp in qry)
            {
                context.InvProductMasters.Update(d => d.InvProductMasterID.Equals(temp.InvProductMasterID), d => new InvProductMaster { DataTransfer = 1 });
            }
        }
        //-----
        public void UpdateInvProductBatchNoExpiaryDetail(int datatransfer)
        {
            context.InvProductBatchNoExpiaryDetails.Update(ps => ps.DataTransfer.Equals(1), ps => new InvProductBatchNoExpiaryDetail { DataTransfer = datatransfer });
        }

        public void UpdateInvProductBatchNoExpiaryDetailDTSelect(int datatransfer)
        {
            var qry = (from d in context.InvProductBatchNoExpiaryDetails where d.DataTransfer.Equals(0) select d).Take(300).ToArray();

            foreach (var temp in qry)
            {
                context.InvProductBatchNoExpiaryDetails.Update(d => d.InvProductBatchNoExpiaryDetailID.Equals(temp.InvProductBatchNoExpiaryDetailID), d => new InvProductBatchNoExpiaryDetail { DataTransfer = 1 });
            }
        }


        public void UpdateProductSupplierLink(int datatransfer)
        {
            context.InvProductSupplierLinks.Update(ps => ps.DataTransfer.Equals(1), ps => new InvProductSupplierLink{ DataTransfer = datatransfer });
        }
        public void UpdateProductSupplierLinkDTSelect(int datatransfer)
        {
            var qry = (from d in context.InvProductSupplierLinks where d.DataTransfer.Equals(0) select d).Take(5).ToArray();

            foreach (var temp in qry)
            {
                context.InvProductSupplierLinks.Update(d => d.InvProductSupplierLinkID.Equals(temp.InvProductSupplierLinkID), d => new InvProductSupplierLink { DataTransfer = 1 });
            }
        }

        
        public void UpdateProductSerialNo(int datatransfer)
        {
            context.InvProductSerialNos.Update(ps => ps.DataTransfer.Equals(1), ps => new InvProductSerialNo { DataTransfer = datatransfer });
        }
        public void UpdateProductSerialNoDTSelect(int datatransfer)
        {
            var qry = (from d in context.InvProductSerialNos where d.DataTransfer.Equals(0) select d).Take(5).ToArray();

            foreach (var temp in qry)
            {
                context.InvProductSerialNos.Update(d => d.InvProductSerialNoID.Equals(temp.InvProductSerialNoID), d => new InvProductSerialNo { DataTransfer = 1 });
            }
        }

        public void UpdateProductStockMaster(int datatransfer)
        {
            context.InvProductStockMasters.Update(ps => ps.DataTransfer.Equals(1) , ps => new InvProductStockMaster{ DataTransfer = datatransfer });
        }

        public void UpdateProductStockMasterDTSelect(int datatransfer)
        {
            var qry = (from d in context.InvProductStockMasters where d.DataTransfer.Equals(0) select d).Take(5).ToArray();

            foreach (var temp in qry)
            {
                context.InvProductStockMasters.Update(d => d.InvProductStockMasterID.Equals(temp.InvProductStockMasterID), d => new InvProductStockMaster { DataTransfer = 1 });
            }
        }

        public void UpdateProductExtendedProperty(int datatransfer)
        {
            context.InvProductExtendedProperties.Update(ps => ps.DataTransfer.Equals(1), ps => new InvProductExtendedProperty { DataTransfer = datatransfer });
        }

        public void UpdateProductExtendedPropertyDTSelect()
        {
            var qry = (from d in context.InvProductExtendedProperties where d.DataTransfer.Equals(0) select d).Take(5).ToArray();

            foreach (var temp in qry)
            {
                context.InvProductExtendedProperties.Update(d => d.InvProductExtendedPropertyID.Equals(temp.InvProductExtendedPropertyID), d => new InvProductExtendedProperty { DataTransfer = 1 });
            }
        }

        public void UpdateProductExtendedPropertyValue(int datatransfer)
        {
            context.InvProductExtendedPropertyValues.Update(ps => ps.DataTransfer.Equals(1), ps => new InvProductExtendedPropertyValue { DataTransfer = datatransfer });
        }
        public void UpdateProductExtendedPropertyValueDTSelect(int datatransfer)
        {
            var qry = (from d in context.InvProductExtendedPropertyValues where d.DataTransfer.Equals(0) select d).Take(5).ToArray();

            foreach (var temp in qry)
            {
                context.InvProductExtendedPropertyValues.Update(d => d.InvProductExtendedPropertyValueID.Equals(temp.InvProductExtendedPropertyValueID), d => new InvProductExtendedPropertyValue { DataTransfer = datatransfer });
            }
        }
     
        public void UpdateProductExtendedValue(int datatransfer)
        {
            context.InvProductExtendedValues.Update(ps => ps.DataTransfer.Equals(1), ps => new InvProductExtendedValue { DataTransfer = datatransfer });
        }
        public void UpdateProductExtendedValueDTSelect(int datatransfer)
        {
            var qry = (from d in context.InvProductExtendedValues where d.DataTransfer.Equals(0) select d).Take(5).ToArray();

            foreach (var temp in qry)
            {
                context.InvProductExtendedValues.Update(d => d.InvProductExtendedPropertyID.Equals(temp.InvProductExtendedPropertyID), d => new InvProductExtendedValue { DataTransfer = datatransfer });
            }
        }

        public void UpdateProductUnitConversions(int datatransfer)
        {
            context.InvProductUnitConversions.Update(ps => ps.DataTransfer.Equals(1), ps => new InvProductUnitConversion { DataTransfer = datatransfer });
        }


        public void UpdateProductUnitConversionsDTSelect(int datatransfer)
        {
            var qry = (from d in context.InvProductUnitConversions where d.DataTransfer.Equals(0) select d).Take(5).ToArray();

            foreach (var temp in qry)
            {
                context.InvProductUnitConversions.Update(d => d.InvProductUnitConversionID.Equals(temp.InvProductUnitConversionID), d => new InvProductUnitConversion { DataTransfer = datatransfer });
            }
        }

        public void UpdateProductCodeDependancy(int Edatatransfer,int datatransfer)
        {
            context.ProductCodeDependancies.Update(ps => ps.DataTransfer.Equals(Edatatransfer), ps => new ProductCodeDependancy {DataTransfer = datatransfer });
        }

        /// Search Product by code

        public InvProductMaster GetProductsByRefCodes(string RefCode)
        {
            return context.InvProductMasters.Where(c => c.ProductCode == RefCode || c.BarCode == RefCode || c.ReferenceCode1 == RefCode || c.ReferenceCode2 == RefCode || c.ReferenceCode3 == RefCode && c.IsDelete == false).FirstOrDefault();
        }

        public InvProductMaster GetProductsByRefCodesForBarcodePrint(string RefCode)
        {
            InvProductMaster tempInvProductMaster = context.InvProductMasters.Where(c => c.ProductCode == RefCode || c.BarCode == RefCode || c.ReferenceCode1 == RefCode || c.ReferenceCode2 == RefCode || c.ReferenceCode3 == RefCode && c.IsDelete == false).FirstOrDefault();
            if (tempInvProductMaster == null)
            {
                long lgRefCode = Common.ConvertStringToLong(RefCode);
                var tempQry = context.InvProductBatchNoExpiaryDetails.Where(c => c.BarCode == lgRefCode && c.IsDelete == false).FirstOrDefault();
                if (tempQry != null)
                {tempInvProductMaster = this.GetProductDetailsByID(tempQry.ProductID);}
            }
            return tempInvProductMaster;
        }

        public decimal GetProductsQtyByProductCodes(long ProductID, int LocaId)
        {
            decimal sumqtyx = 0;
            var sumqty = (from t in
                             (from t in context.InvProductBatchNoExpiaryDetails
                              where
                                t.ProductID == ProductID &&
                                t.LocationID == LocaId
                              select new
                              {
                                  t.BalanceQty,
                                  Dummy = "x"
                              })
                         group t by new { t.Dummy } into g
                         select new
                         {
                             Column1 = (System.Decimal)g.Sum(p => p.BalanceQty)
                         }).ToArray();
           

            foreach (var temp in sumqty)
            {
                sumqtyx = temp.Column1;
            }

            return sumqtyx;
     
        }

        public InvProductMaster GetProductsByCode(string ProductCode)
        {
            return context.InvProductMasters.Where(c => c.ProductCode == ProductCode  && c.IsDelete == false).FirstOrDefault();
        }

        public InvProductMaster GetProductsByBarCode(string ProductCode)
        {
            //return context.InvProductMasters.Where(c => c.ProductCode == ProductCode && c.IsDelete == false).FirstOrDefault();

            return context.InvProductMasters.Where(b => b.IsDelete.Equals(false))
                                                            .DefaultIfEmpty()
                                                            .Join(context.InvProductBatchNoExpiaryDetails,
                                                            b => b.InvProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b)
                                                            .FirstOrDefault();
        }

        //public InvProductMaster GetProductsByID(int productID)
        //{
        //    return context.InvProductMasters.Where(c => c.InvProductMasterID == productID && c.IsDelete == false).FirstOrDefault();
        //}

        public InvProductMaster GetProductDetailsByID(long productID)
        {
            return context.InvProductMasters.Where(c => c.InvProductMasterID == productID && c.IsDelete == false).FirstOrDefault();
        }

        public InvProductMaster GetProductsByCodeBySupplier(string ProductCode, long supplierID)
        {
            InvProductMaster invProductMaster = new InvProductMaster();
            return context.InvProductMasters.Where(c => c.ProductCode == ProductCode && c.SupplierID.Equals(supplierID) && c.IsDelete == false).FirstOrDefault();
        }
        
        ///  Search Product by name

        public InvProductMaster GetProductsByName(string productName)
        {
            return context.InvProductMasters.Where(c => c.ProductName == productName && c.IsDelete == false).FirstOrDefault();
        }

        public InvProductMaster GetProductsByNameBySupplier(string productName, long supplierID)
        {
            return context.InvProductMasters.Where(c => c.ProductName == productName && c.SupplierID.Equals(supplierID) && c.IsDelete == false).FirstOrDefault();
        }

        /// Get all Product details 

        public List<InvProductMaster> GetAllProducts()
        {
            return context.InvProductMasters.Where(c => c.IsDelete == false).ToList();


        }

        public DataTable GetAllProductsDataTable()
        {
            DataTable dt = new DataTable();
            var query = from d in context.InvProductMasters where d.IsDelete.Equals(false) && d.DataTransfer.Equals(0) select new { d.InvProductMasterID, d.ProductCode, d.BarCode, d.ReferenceCode1, d.ReferenceCode2, d.ProductName, d.NameOnInvoice, d.DepartmentID, d.CategoryID, d.SubCategoryID, d.SubCategory2ID, d.SupplierID, d.UnitOfMeasureID, d.PackSize, d.ProductImage, ValuationMethod = d.CostingMethod, d.CostPrice, d.AverageCost, d.SellingPrice, WholeSalePrice = d.WholesalePrice, d.MinimumPrice, d.FixedDiscount, d.MaximumDiscount, d.MaximumPrice, d.FixedDiscountPercentage, d.MaximumDiscountPercentage, d.ReOrderLevel, d.ReOrderQty, d.ReOrderPeriod, d.IsActive, d.IsBatch, d.IsPromotion, d.IsBundle, d.IsFreeIssue, IsDryage = d.IsDrayage, Dryage = d.DrayagePercentage, d.IsExpiry, d.IsConsignment, d.IsCountable, d.IsDCS, d.DcsID, d.IsTax, d.IsSerial, d.IsDelete, d.GroupOfCompanyID, d.CreatedUser, d.CreatedDate, d.ModifiedUser, d.ModifiedDate, DataTransfer = d.DataTransfer };
            return dt = query.ToDataTable();
        }
        // data Transfer
        //InvProductMaster
        public DataTable GetAllDTProductsDataTable()
        {
            DataTable dt = new DataTable();
            var query = (from d in context.InvProductMasters where d.DataTransfer.Equals(1) select new
            {
                d.InvProductMasterID,
                d.ProductCode,
                d.BarCode,
                d.ReferenceCode1,
                d.ReferenceCode2,
                d.ProductName,
                d.NameOnInvoice,
                d.DepartmentID,
                d.CategoryID,
                d.SubCategoryID,
                d.SubCategory2ID,
                d.SupplierID,
                d.UnitOfMeasureID,
                d.PackSize,
                d.ProductImage,
                d.CostingMethod,
                d.CostPrice,
                d.AverageCost,
                d.SellingPrice,
                d.WholesalePrice,
                d.MinimumPrice,
                d.FixedDiscount,
                d.MaximumDiscount,
                d.MaximumPrice,
                d.FixedDiscountPercentage,
                d.MaximumDiscountPercentage,
                d.ReOrderLevel,
                d.ReOrderQty,
                d.ReOrderPeriod,
                d.IsActive,
                d.IsBatch,
                d.IsPromotion,
                d.IsBundle,
                d.IsFreeIssue,
                d.IsDrayage,
                d.DrayagePercentage,
                d.IsExpiry,
                d.IsConsignment,
                d.IsCountable,
                d.IsDCS,
                d.DcsID,
                d.IsTax,
                d.IsSerial,
                d.IsDelete,
                d.PackSizeUnitOfMeasureID,
                d.Margin,
                d.WholesaleMargin,
                d.FixedGP,
                d.PurchaseLedgerID,
                d.SalesLedgerID,
                d.OtherPurchaseLedgerID,
                d.OtherSalesLedgerID,
                d.GroupOfCompanyID,
                d.CreatedUser,
                d.CreatedDate,
                d.ModifiedUser,
                d.ModifiedDate,
                d.DataTransfer,
                d.OrderPrice,
                d.ReferenceCode3
            });
            return dt = query.ToDataTable();

            //context.Configuration.LazyLoadingEnabled = false;
            //return context.InvProductMasters.SqlQuery("select InvProductMasterID,ProductCode,BarCode,ReferenceCode1,ReferenceCode2,ProductName,NameOnInvoice,DepartmentID,CategoryID,SubCategoryID ,SubCategory2ID,SupplierID,UnitOfMeasureID,PackSize,ProductImage,CostingMethod,CostPrice,AverageCost,SellingPrice,WholesalePrice ,MinimumPrice,FixedDiscount,MaximumDiscount,MaximumPrice,FixedDiscountPercentage,MaximumDiscountPercentage,ReOrderLevel,ReOrderQty ,ReOrderPeriod,IsActive,IsBatch,IsPromotion,IsBundle,IsFreeIssue,IsDrayage,DrayagePercentage,IsExpiry,IsConsignment,IsCountable ,IsDCS,DcsID,IsTax,IsSerial ,IsDelete ,PackSizeUnitOfMeasureID ,Margin ,WholesaleMargin ,FixedGP ,PurchaseLedgerID ,SalesLedgerID ,OtherPurchaseLedgerID ,OtherSalesLedgerID ,GroupOfCompanyID ,CreatedUser ,CreatedDate ,ModifiedUser ,ModifiedDate ,DataTransfer from InvProductMaster").ToDataTable();
        }
        //InvProductBatchNoExpiaryDetail
        public DataTable GetAllDTProductsBatchExDataTable()
        {
            DataTable dt = new DataTable();
            var query = (from d in context.InvProductBatchNoExpiaryDetails
                         where d.DataTransfer.Equals(1) 
                         select new 
                         { 
                             d.InvProductBatchNoExpiaryDetailID ,
                             d.ProductBatchNoExpiaryDetailID ,d.CompanyID ,
                             d.LocationID ,
                             d.CostCentreID ,
                             d.ReferenceDocumentDocumentID ,
                             d.ReferenceDocumentID ,
                             d.ProductID ,
                             d.BarCode ,
                             d.BatchNo ,d.ExpiryDate ,
                             d.LineNo ,
                             d.Qty ,d.UnitOfMeasureID ,d.BalanceQty ,d.CostPrice ,d.SellingPrice ,
                             d.SupplierID ,d.IsDelete ,d.GroupOfCompanyID ,d.CreatedUser ,d.CreatedDate ,
                             d.ModifiedUser ,d.ModifiedDate ,DataTransfer = d.DataTransfer
                         });
            return dt = query.ToDataTable();
            //context.Configuration.LazyLoadingEnabled = false;
            //return context.InvProductBatchNoExpiaryDetails.SqlQuery("select InvProductBatchNoExpiaryDetailID ,ProductBatchNoExpiaryDetailID ,CompanyID ,LocationID ,CostCentreID ,ReferenceDocumentDocumentID ,ReferenceDocumentID ,ProductID ,BarCode ,BatchNo ,ExpiryDate ,[LineNo] ,Qty ,UnitOfMeasureID ,BalanceQty ,CostPrice ,SellingPrice ,SupplierID ,IsDelete ,GroupOfCompanyID ,CreatedUser ,CreatedDate ,ModifiedUser ,ModifiedDate ,DataTransfer from InvProductBatchNoExpiaryDetail").ToDataTable();

        }
        //InvProductSupplierLink
        public DataTable GetAllDTProductSupplierLinkDataTable()
        {
            DataTable dt = new DataTable();
            var query = (from d in context.InvProductSupplierLinks where d.DataTransfer.Equals(1) select new 
            {
                d.InvProductSupplierLinkID,
                d.CompanyID,
                d.LocationID,
                d.CostCentreID,
                d.ProductID,
                d.SupplierID,
                d.DocumentID,
                d.ReferenceDocumentID,
                d.ReferenceDocumentNo,
                d.DocumentDate,
                d.CostPrice,
                d.FixedGP,
                d.DocumentStatus,
                d.IsDelete,
                d.GroupOfCompanyID,
                d.CreatedUser,
                d.CreatedDate,
                d.ModifiedUser,
                d.ModifiedDate,
                DataTransfer = d.DataTransfer
            });
            return dt = query.ToDataTable();
            //context.Configuration.LazyLoadingEnabled = false;
            //return context.InvProductSupplierLinks.SqlQuery("select InvProductSupplierLinkID ,CompanyID ,LocationID ,CostCentreID ,ProductID ,SupplierID ,DocumentID ,ReferenceDocumentID ,ReferenceDocumentNo ,DocumentDate ,CostPrice ,FixedGP ,DocumentStatus ,IsDelete ,GroupOfCompanyID ,CreatedUser ,CreatedDate ,ModifiedUser ,ModifiedDate ,DataTransfer from InvProductSupplierLink").ToDataTable();
        }
        //InvProductSerialNo
        public DataTable GetAllDTInvProductSerialNoDataTable()
        {
            DataTable dt = new DataTable();
            var query = (from d in context.InvProductSerialNos where d.DataTransfer.Equals(1) select new 
            {
                d.InvProductSerialNoID,
                d.ProductSerialNoID,
                d.GroupOfCompanyID,
                d.CompanyID,
                d.LocationID,
                d.CostCentreID,
                d.ProductID,
                d.BatchNo,
                d.UnitOfMeasureID,
                d.ExpiryDate,
                d.SerialNo,
                d.SerialNoStatus,
                d.DocumentStatus,
                d.CreatedUser,
                d.CreatedDate,
                d.ModifiedUser,
                d.ModifiedDate,
                DataTransfer = d.DataTransfer
            });
            return dt = query.ToDataTable();
            //context.Configuration.LazyLoadingEnabled = false;
            //return context.InvProductSerialNos.SqlQuery("select InvProductSerialNoID, ProductSerialNoID ,GroupOfCompanyID ,CompanyID ,LocationID ,CostCentreID ,ProductID ,BatchNo ,UnitOfMeasureID ,ExpiryDate ,SerialNo ,SerialNoStatus ,DocumentStatus ,CreatedUser ,CreatedDate ,ModifiedUser ,ModifiedDate ,DataTransfer from InvProductSerialNo").ToDataTable();
        }
        //InvProductStockMaster
        public DataTable GetAllDTInvProductStockMasterDataTable()
        {
            DataTable dt = new DataTable();
            var query = (from d in context.InvProductStockMasters where d.DataTransfer.Equals(1) select new
            
            {
                d.InvProductStockMasterID,
                d.CompanyID,
                d.LocationID,
                d.CostCentreID,
                d.ProductID,
                d.Stock,
                d.CostPrice,
                d.SellingPrice,
                d.MinimumPrice,
                d.ReOrderLevel,
                d.ReOrderQuantity,
                d.ReOrderPeriod,
                d.IsDelete,
                d.GroupOfCompanyID,
                d.CreatedUser,
                d.CreatedDate,
                d.ModifiedUser,
                d.ModifiedDate,
                DataTransfer = d.DataTransfer
            });
            return dt = query.ToDataTable();
            //context.Configuration.LazyLoadingEnabled = false;
            //return context.InvProductStockMasters.SqlQuery("select InvProductStockMasterID ,CompanyID ,LocationID ,CostCentreID ,ProductID ,Stock ,CostPrice ,SellingPrice ,MinimumPrice ,ReOrderLevel ,ReOrderQuantity ,ReOrderPeriod ,IsDelete ,GroupOfCompanyID ,CreatedUser ,CreatedDate ,ModifiedUser ,ModifiedDate ,DataTransfer  from InvProductStockMaster").ToDataTable();
        }
        //InvProductExtendedProperty
        public DataTable GetAllDTInvProductExtendedPropertyDataTable()
        {
            DataTable dt = new DataTable();
            var query = (from d in context.InvProductExtendedProperties where d.DataTransfer.Equals(1) 
                         select new
            {
                d.InvProductExtendedPropertyID,
                d.ExtendedPropertyCode,
                d.ExtendedPropertyName,
                d.DataType,
                d.Parent,
                d.IsAllLocations,
                d.IsRequired,
                d.IsDelete,
                d.GroupOfCompanyID,
                d.CreatedUser,
                d.CreatedDate,
                d.ModifiedUser,
                d.ModifiedDate,
                DataTransfer = d.DataTransfer
            });
            return dt = query.ToDataTable();
            //context.Configuration.LazyLoadingEnabled = false;
            //return context.InvProductExtendedProperties.SqlQuery("select InvProductExtendedPropertyID ,ExtendedPropertyCode ,ExtendedPropertyName ,DataType ,Parent ,IsAllLocations ,IsRequired ,IsDelete ,GroupOfCompanyID ,CreatedUser ,CreatedDate ,ModifiedUser ,ModifiedDate ,DataTransfer from InvProductExtendedProperty").ToDataTable();
        }
        //InvProductExtendedPropertyValue
        public DataTable GetAllDTInvProductExtendedPropertyValueDataTable()
        {
            DataTable dt = new DataTable();
            var query = (from d in context.InvProductExtendedPropertyValues where d.DataTransfer.Equals(1) select new 
            {
                d.InvProductExtendedPropertyValueID,
                d.ProductID,
                d.InvProductExtendedPropertyID,
                d.InvProductExtendedValueID,
                d.IsDelete,
                d.GroupOfCompanyID,
                d.CreatedUser,
                d.CreatedDate,
                d.ModifiedUser,
                d.ModifiedDate,
                DataTransfer = d.DataTransfer

            });
            return dt = query.ToDataTable();
            //context.Configuration.LazyLoadingEnabled = false;
            //return context.InvProductExtendedPropertyValues.SqlQuery("select InvProductExtendedPropertyValueID ,ProductID ,InvProductExtendedPropertyID ,InvProductExtendedValueID ,IsDelete ,GroupOfCompanyID ,CreatedUser ,CreatedDate ,ModifiedUser ,ModifiedDate ,DataTransfer ,InvProductMaster_InvProductMasterID from InvProductExtendedPropertyValue").ToDataTable();
        }
        //InvProductExtendedValue
        public DataTable GetAllDTInvProductExtendedValueDataTable()
        {
            DataTable dt = new DataTable();
            var query = (from d in context.InvProductExtendedValues
                         where d.DataTransfer.Equals(1)
                         select new 
            {
                d.InvProductExtendedValueID,
                d.InvProductExtendedPropertyID,
                d.ValueData,
                d.ParentValueData,
                d.IsDelete,
                d.GroupOfCompanyID,
                d.CreatedUser,
                d.CreatedDate,
                d.ModifiedUser,
                d.ModifiedDate,
                DataTransfer = d.DataTransfer
            });
            return dt = query.ToDataTable();
            //context.Configuration.LazyLoadingEnabled = false;
            //return context.InvProductExtendedValues.SqlQuery("select InvProductExtendedValueID ,InvProductExtendedPropertyID ,ValueData ,ParentValueData ,IsDelete ,GroupOfCompanyID ,CreatedUser ,CreatedDate ,ModifiedUser ,ModifiedDate ,DataTransfer from InvProductExtendedValue").ToDataTable();
        }
        //InvProductUnitConversion
        public DataTable GetAllDTInvProductUnitConversionDataTable()
        {
            DataTable dt = new DataTable();
            var query = (from d in context.InvProductUnitConversions where d.DataTransfer.Equals(1) select new 
            { 
                d.InvProductUnitConversionID,
                d.ProductID ,
                d.Description ,
                d.UnitOfMeasureID ,
                d.ConvertFactor ,
                d.CostPrice ,
                d.SellingPrice ,
                d.MinimumPrice ,
                d.IsDelete ,
                d.LineNo ,
                d.GroupOfCompanyID,
                d.CreatedUser,
                d.CreatedDate,
                d.ModifiedUser,
                d.ModifiedDate,
                DataTransfer = d.DataTransfer

            });
            return dt = query.ToDataTable();
            //context.Configuration.LazyLoadingEnabled = false;
            //return context.InvProductUnitConversions.SqlQuery("select InvProductUnitConversionID ,ProductID ,Description ,UnitOfMeasureID ,ConvertFactor ,CostPrice ,SellingPrice ,MinimumPrice ,IsDelete ,[LineNo] ,GroupOfCompanyID ,CreatedUser ,CreatedDate ,ModifiedUser ,ModifiedDate ,DataTransfer  from InvProductUnitConversion").ToDataTable();
        }

        //ProductCodeDependancy
        public DataTable GetAllDTProductCodeDependancyDataTable()
        {
            DataTable dt = new DataTable();
            var query = (from d in context.ProductCodeDependancies
                         where d.DataTransfer.Equals(1)
                         select new
                         {
                             d.ProductCodeDependancyID ,
                             d.FormName ,
                             d.DependOnDepartment ,
                             d.DependOnCategory ,
                             d.DependOnSubCategory ,
                             d.DependOnSubCategory2,
                             d.GroupOfCompanyID,
                             d.CreatedUser,
                             d.CreatedDate,
                             d.ModifiedUser,
                             d.ModifiedDate,
                             DataTransfer = d.DataTransfer
                          });
            return dt = query.ToDataTable();
            //context.Configuration.LazyLoadingEnabled = false;
            //return context.InvProductUnitConversions.SqlQuery("select InvProductUnitConversionID ,ProductID ,Description ,UnitOfMeasureID ,ConvertFactor ,CostPrice ,SellingPrice ,MinimumPrice ,IsDelete ,[LineNo] ,GroupOfCompanyID ,CreatedUser ,CreatedDate ,ModifiedUser ,ModifiedDate ,DataTransfer  from InvProductUnitConversion").ToDataTable();
        }

        public DataTable GetProductsDataTable() 
        {
            DataTable dt = new DataTable();
            var query = from pm in context.InvProductMasters
                        join s in context.Suppliers on pm.SupplierID equals s.SupplierID
                        join dep in context.InvDepartments on pm.DepartmentID equals dep.InvDepartmentID
                        join cat in context.InvCategories on pm.CategoryID equals cat.InvCategoryID
                        join scat in context.InvSubCategories on pm.SubCategoryID equals scat.InvSubCategoryID
                        join scat2 in context.InvSubCategories2 on pm.SubCategory2ID equals scat2.InvSubCategory2ID
                        where pm.IsDelete.Equals(false)
                        select new
                        {
                            pm.ProductCode,
                            pm.ProductName,
                            pm.NameOnInvoice,
                            Department = dep.DepartmentCode,
                            Lifestyle = cat.CategoryCode,
                            Product = scat.SubCategoryCode,
                            Brand = scat2.SubCategory2Code,
                            pm.BarCode,
                            pm.ReferenceCode1,
                            pm.ReferenceCode2,
                            s.SupplierCode,
                            pm.CostPrice,
                            pm.AverageCost,
                            pm.SellingPrice,
                            WholeSalePrice = pm.WholesalePrice,
                            pm.ReOrderLevel,
                            pm.ReOrderQty,
                            pm.ReOrderPeriod,
                        };
            return dt = Common.LINQToDataTable(query);
        }

        public DataTable GetProductsDataTableForTransactions(long supplierID) 
        {
            var query = from pm in context.InvProductMasters
                        join s in context.Suppliers on pm.SupplierID equals s.SupplierID
                        join dep in context.InvDepartments on pm.DepartmentID equals dep.InvDepartmentID
                        join cat in context.InvCategories on pm.CategoryID equals cat.InvCategoryID
                        join scat in context.InvSubCategories on pm.SubCategoryID equals scat.InvSubCategoryID
                        join scat2 in context.InvSubCategories2 on pm.SubCategory2ID equals scat2.InvSubCategory2ID
                        where pm.IsDelete.Equals(false) //&&
                        //s.SupplierID == supplierID
                        select new
                        {
                            pm.ProductCode,
                            pm.ProductName,
                            pm.NameOnInvoice,
                            Department = dep.DepartmentCode,
                            Lifestyle = cat.CategoryCode,
                            Product = scat.SubCategoryCode,
                            Brand = scat2.SubCategory2Code,
                            s.SupplierCode,
                            pm.CostPrice,
                            pm.AverageCost,
                            pm.SellingPrice,
                            WholeSalePrice = pm.WholesalePrice,
                            pm.ReOrderLevel,
                            pm.ReOrderQty,
                            pm.ReOrderPeriod,
                        };
            return query.ToDataTable();
        }

        public DataTable GetProductsDataTableForTransactions()
        {
            var query = from pm in context.InvProductMasters
                        join s in context.Suppliers on pm.SupplierID equals s.SupplierID
                        join dep in context.InvDepartments on pm.DepartmentID equals dep.InvDepartmentID
                        join cat in context.InvCategories on pm.CategoryID equals cat.InvCategoryID
                        join scat in context.InvSubCategories on pm.SubCategoryID equals scat.InvSubCategoryID
                        join scat2 in context.InvSubCategories2 on pm.SubCategory2ID equals scat2.InvSubCategory2ID
                        where pm.IsDelete.Equals(false)
                        select new
                        {
                            pm.ProductCode,
                            pm.ProductName,
                            pm.NameOnInvoice,
                            Department = dep.DepartmentCode,
                            Lifestyle = cat.CategoryCode,
                            Product = scat.SubCategoryCode,
                            Brand = scat2.SubCategory2Code,
                            s.SupplierCode,
                            pm.CostPrice,
                            pm.AverageCost,
                            pm.SellingPrice,
                            WholeSalePrice = pm.WholesalePrice,
                            pm.ReOrderLevel,
                            pm.ReOrderQty,
                            pm.ReOrderPeriod,
                        };
            return query.ToDataTable();
        }

        public DataTable GetProductDataTableForBatchTransaction()
        {
            var qry = (from bd in context.InvProductBatchNoExpiaryDetails
                       join pm in context.InvProductMasters on bd.ProductID equals pm.InvProductMasterID
                       join s in context.Suppliers on pm.SupplierID equals s.SupplierID
                       join dep in context.InvDepartments on pm.DepartmentID equals dep.InvDepartmentID
                       join cat in context.InvCategories on pm.CategoryID equals cat.InvCategoryID
                       join scat in context.InvSubCategories on pm.SubCategoryID equals scat.InvSubCategoryID
                       join scat2 in context.InvSubCategories2 on pm.SubCategory2ID equals scat2.InvSubCategory2ID
                       where pm.IsDelete.Equals(false)
                       group new { bd, pm, s } by new
                       {
                           pm.ProductCode,
                           pm.ProductName,
                           pm.NameOnInvoice,
                           //dep.DepartmentCode,
                           //cat.CategoryCode,
                           //scat.SubCategoryCode,
                           //scat2.SubCategory2Code,
                           s.SupplierCode,
                           pm.CostPrice,
                           pm.AverageCost,
                           pm.SellingPrice,
                           WholeSalePrice = pm.WholesalePrice,
                           pm.ReOrderLevel,
                           pm.ReOrderQty,
                           pm.ReOrderPeriod,
                       } into grp

                       select new
                       {
                           ProductCode = grp.Key.ProductCode,
                           ProductName = grp.Key.ProductName,
                           NameOnInvoice = grp.Key.NameOnInvoice,
                           //dep.DepartmentCode,
                           //cat.CategoryCode,
                           //scat.SubCategoryCode,
                           //scat2.SubCategory2Code,
                           SupplierCode = grp.Key.SupplierCode,
                           CostPrice = grp.Key.CostPrice,
                           AverageCost = grp.Key.AverageCost,
                           SellingPrice = grp.Key.SellingPrice,
                           WholeSalePrice = grp.Key.WholeSalePrice,
                           ReOrderLevel = grp.Key.ReOrderLevel,
                           ReOrderQty = grp.Key.ReOrderQty,
                           ReOrderPeriod = grp.Key.ReOrderPeriod,
                       });
            return qry.ToDataTable();
        }

        public DataTable GetAllProductRelatedToBatchNo(long[] arrDept,long[] arrCategory, long[] arrSubcategory,long[] arrSubCategory2,string[] arrBatchNo,long[] arrLocation)
        {
            var query = (from p in context.InvProductMasters  
                         join pb in context.InvProductBatchNoExpiaryDetails on p.InvProductMasterID equals pb.ProductID
                         join lc in context.Locations on pb.LocationID equals lc.LocationID
                         join um in context.UnitOfMeasures on pb.UnitOfMeasureID equals um.UnitOfMeasureID
                         where (arrDept).Contains(p.DepartmentID)
                         || (arrCategory).Contains(p.CategoryID)
                         || (arrSubcategory).Contains(p.SubCategoryID)
                         || (arrSubCategory2).Contains(p.SubCategory2ID)
                         || (arrBatchNo).Contains(pb.BatchNo)
                         || (arrLocation).Contains(lc.LocationID)
                         // where arrDept.Any(dept => dept.Equals(p.DepartmentID)) 
                         //&& arrCategory.Any(category => category.Equals(p.CategoryID))
                         //&& arrSubcategory.Any(subcategory =>subcategory.Equals(p.SubCategoryID)) && arrSubCategory2.Any(subcategory2 => subcategory2.Equals(p.SubCategory2ID))
                         //&& arrBatchNo.Any(batchno => batchno.Equals(pb.BatchNo))
                         //&& (arrCategory).Contains(p.CategoryID) && (arrSubcategory).Contains(p.SubCategoryID) && (arrSubCategory2).Contains(p.SubCategory2ID) && (arrBatchNo).Contains(pb.BatchNo)
                         select new
                         {
                             pb.LocationID,
                             lc.LocationName,
                             pb.BatchNo,
                             pb.ProductID,
                             p.ProductCode,
                             p.ProductName,
                             pb.Qty,
                             pb.UnitOfMeasureID
                             ,um.UnitOfMeasureName,
                             pb.CostPrice,
                             pb.SellingPrice,
                             NewSellingPrice=0,
                             NewCostPrice=0
                         });
            return query.ToDataTable();
        }

        public List<PriceChangeTemp> GetAllProductRelatedToBatchNoList(long[] arrDept, long[] arrCategory, long[] arrSubcategory, long[] arrSubCategory2, string[] arrBatchNo, long[] arrLocation, long[] arrSupplier)
        {
            PriceChangeTemp priceChangeTemp = new PriceChangeTemp();
            List<PriceChangeTemp> priceChangeLst = new List<PriceChangeTemp>();
            long lineNo = 0;
            var query = (from p in context.InvProductMasters
                         join pb in context.InvProductBatchNoExpiaryDetails on p.InvProductMasterID equals pb.ProductID
                         join lc in context.Locations on pb.LocationID equals lc.LocationID
                         join um in context.UnitOfMeasures on pb.UnitOfMeasureID equals um.UnitOfMeasureID
                         where (arrDept).Contains(p.DepartmentID)
                         || (arrCategory).Contains(p.CategoryID)
                         || (arrSubcategory).Contains(p.SubCategoryID)
                         || (arrSubCategory2).Contains(p.SubCategory2ID)
                         || (arrBatchNo).Contains(pb.BatchNo)
                         || (arrSupplier).Contains(p.SupplierID)
                         && (arrLocation).Contains(lc.LocationID)
                         orderby p.InvProductMasterID
                         // where arrDept.Any(dept => dept.Equals(p.DepartmentID)) 
                         //&& arrCategory.Any(category => category.Equals(p.CategoryID))
                         //&& arrSubcategory.Any(subcategory =>subcategory.Equals(p.SubCategoryID)) && arrSubCategory2.Any(subcategory2 => subcategory2.Equals(p.SubCategory2ID))
                         //&& arrBatchNo.Any(batchno => batchno.Equals(pb.BatchNo))
                         //&& (arrCategory).Contains(p.CategoryID) && (arrSubcategory).Contains(p.SubCategoryID) && (arrSubCategory2).Contains(p.SubCategory2ID) && (arrBatchNo).Contains(pb.BatchNo)
                         select new
                         {
                             pb.LocationID,
                             lc.LocationName,
                             pb.BatchNo,
                             pb.ProductID,
                             p.ProductCode,
                             p.ProductName,
                             pb.Qty,
                             pb.UnitOfMeasureID,
                             p.MaximumPrice,
                             um.UnitOfMeasureName,
                             pb.CostPrice,
                             pb.SellingPrice,
                             NewSellingPrice = 0,
                             NewCostPrice = 0
                         }).ToArray();

            foreach (var tmpPriceChange in query)
            {
                priceChangeTemp = new PriceChangeTemp();
                priceChangeTemp.ProductCode = tmpPriceChange.ProductCode;
                priceChangeTemp.ProductID = tmpPriceChange.ProductID;
                priceChangeTemp.ProductName = tmpPriceChange.ProductName;
                priceChangeTemp.Qty = tmpPriceChange.Qty;
                priceChangeTemp.SellingPrice = tmpPriceChange.SellingPrice;
                priceChangeTemp.UnitOfMeasureID = tmpPriceChange.UnitOfMeasureID;
                priceChangeTemp.UnitOfMeasureName = tmpPriceChange.UnitOfMeasureName;
                priceChangeTemp.NewSellingPrice = tmpPriceChange.NewSellingPrice;
                priceChangeTemp.NewCostPrice = tmpPriceChange.NewCostPrice;
                priceChangeTemp.MRP = tmpPriceChange.MaximumPrice;
                priceChangeTemp.LocationName = tmpPriceChange.LocationName;
                priceChangeTemp.LocationId = tmpPriceChange.LocationID;
                priceChangeTemp.BatchNo = tmpPriceChange.BatchNo;
                priceChangeTemp.CostPrice = tmpPriceChange.CostPrice;
               
                lineNo=   lineNo + 1;
                priceChangeTemp.LineNo = lineNo;
                
                priceChangeLst.Add(priceChangeTemp);
            }


            return priceChangeLst.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<string> GetAllProductsArray()
        {
            List<string> list = context.InvProductMasters.Where(c => c.IsDelete == false).Select(c => c.ProductCode).ToList();
            return list;


        }

        public DataTable GetAllProductDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.InvProductMasters.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (string)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsManualRecordFilter)
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @0 ", reportConditionsDataStruct.ConditionFrom.Trim()); }
                    else
                    {query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim());}
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                {query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()));}

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim()));}

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "InvDepartmentID":
                                query = (from qr in query
                                         join jt in context.InvDepartments.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.DepartmentID equals jt.InvDepartmentID
                                         select qr
                                        );
                                break;
                            case "InvCategoryID":
                                 query = (from qr in query
                                         join jt in context.InvCategories.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.CategoryID equals jt.InvCategoryID
                                         select qr
                                        );
                                break;
                            case "InvSubCategoryID":
                                query = (from qr in query
                                         join jt in context.InvSubCategories.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.SubCategoryID equals jt.InvSubCategoryID
                                         select qr
                                        );
                                break;
                            case "InvSubCategory2ID":
                                 query = (from qr in query
                                         join jt in context.InvSubCategories2.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.SubCategory2ID equals jt.InvSubCategory2ID
                                         select qr
                                        );
                                break;
                            default:
                                query = query
                                 .Where("" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                break;
                        }
                    }
                    else
                    {
                        query =
                            query.Where(
                                "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " +
                                "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                                long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                    }
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
            }

            // Set fields to be selected
            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {
                querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", ");
            }

            // Remove last two charators (", ")
            querrySelect.Remove(querrySelect.Length - 2, 2);

            //DataTable tp = query.AsQueryable().ToDataTable();

            //var query1 = query.AsEnumerable();
            

            var queryResult = (
                               from pm in query
                               join dm in context.InvDepartments on pm.DepartmentID equals dm.InvDepartmentID
                               join cm in context.InvCategories on pm.CategoryID equals cm.InvCategoryID
                               join sm in context.InvSubCategories on pm.SubCategoryID equals sm.InvSubCategoryID
                               join sc in context.InvSubCategories2 on pm.SubCategory2ID equals sc.InvSubCategory2ID
                               select new
                               {
                                   FieldString1 = dm.DepartmentCode + " " + dm.DepartmentName,
                                   FieldString2 = cm.CategoryCode + " " + cm.CategoryName,
                                   FieldString3 = sm.SubCategoryCode + " " + sm.SubCategoryName,
                                   FieldString4 = sc.SubCategory2Code + " " + sc.SubCategory2Name,
                                   FieldString5 = pm.ProductCode,
                                   FieldString6 = pm.ProductName,
                                   FieldString7 = pm.NameOnInvoice,
                                   FieldString8 = pm.BarCode,
                                   FieldString9 = pm.ReOrderQty,
                                   FieldString10 = pm.CostPrice,
                                   FieldString11 = pm.SellingPrice,
                                   FieldString12 = pm.FixedDiscount,
                                   FieldString13 = pm.IsBatch,
                                   FieldString14 = pm.IsPromotion,
                                   FieldString15 = "..................."
                               }); //.ToArray();

            DataTable dtQueryResult = new DataTable();
            StringBuilder sbOrderByColumns = new StringBuilder();

            if (reportOrderByDataStructList.Any(g => g.IsResultOrderBy))
            {
                foreach (var item in reportOrderByDataStructList.Where(g => g.IsResultOrderBy.Equals(true)))
                { sbOrderByColumns.Append("" + item.ReportField + " , "); }

                sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                dtQueryResult = queryResult.OrderBy(sbOrderByColumns.ToString())
                                           .ToDataTable();
            }
            else
            { dtQueryResult = queryResult.ToDataTable(); }

            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {
                if (dtQueryResult.Columns.Contains(reportDataStruct.ReportField))
                {
                    if (!reportDataStruct.IsSelectionField)
                    { dtQueryResult.Columns.Remove(reportDataStruct.ReportField); }
                }
            }

            return dtQueryResult; //return queryResult.ToDataTable();
        }

        public DataTable GetAllProductDataTable()
        {
            //DataTable tblProductMasters = new DataTable();

            var query = (from pm in context.InvProductMasters
                      join dm in context.InvDepartments on pm.DepartmentID equals dm.InvDepartmentID
                      join cm in context.InvCategories on pm.CategoryID equals cm.InvCategoryID
                      join sm in context.InvSubCategories on pm.SubCategoryID equals sm.InvSubCategoryID
                      join sc in context.InvSubCategories2 on pm.SubCategory2ID equals sc.InvSubCategory2ID 
                      select new
                      {
                          FieldString1 = pm.ProductCode,
                          FieldString2 = pm.ProductName,
                          FieldString3 = pm.NameOnInvoice,
                          FieldString4 = dm.DepartmentCode + dm.DepartmentName,
                          FieldString5 = cm.CategoryCode + cm.CategoryName,
                          FieldString6 = sm.SubCategoryCode + sm.SubCategoryName,
                          FieldString7 = sc.SubCategory2Code + sc.SubCategory2Name,
                          FieldString8 = pm.BarCode,
                          FieldString9 = pm.ReOrderQty,
                          FieldString10 = pm.CostPrice,
                          FieldString11 = pm.SellingPrice,
                          FieldString12 = pm.FixedDiscount,
                          FieldString13 = pm.IsBatch,
                          FieldString14 = pm.IsPromotion
                      }).ToArray();

            //var data2 = (from c in sq
            //             select new
            //             {
            //                 FieldString1 = c.FieldString1.ToString(),
            //                 FieldString2 = c.FieldString2.ToString(),
            //                 FieldString3 = c.FieldString3.ToString(),
            //                 FieldString4 = c.FieldString4.ToString(),
            //                 FieldString5 = c.FieldString5.ToString(),
            //                 FieldString6 = c.FieldString6.ToString(),
            //                 FieldString7 = c.FieldString7.ToString(),
            //                 FieldString8 = c.FieldString8.ToString(),
            //                 FieldString9 = c.FieldString9.ToString(),
            //                 FieldString10 = c.FieldString10.ToString(),
            //                 FieldString11 = c.FieldString11.ToString(),
            //                 FieldString12 = c.FieldString12.ToString(),
            //                 FieldString13 = c.FieldString13.ToString(),
            //                 FieldString14 = c.FieldString14.ToString()

            //             });


            //return tblProductMasters = Common.LINQToDataTable(data2);
            return query.AsEnumerable().ToDataTable();
        }

        /// <summary>
        /// Get selection data for Report Generator Combo boxes
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct)
        {
            IQueryable qryResult = context.InvProductMasters
                                             .Where("IsDelete == @0 ", false);
                            //.OrderBy("" + reportDataStruct.DbColumnName.Trim() + "")
                            //.Select(reportDataStruct.DbColumnName.Trim());

            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "InvDepartmentID":
                    qryResult = qryResult.Join(context.InvDepartments, "DepartmentID",
                                               reportDataStruct.DbColumnName.Trim(),
                                               "new(inner.DepartmentCode, inner.DepartmentName)")
                                         .GroupBy("new(DepartmentCode, DepartmentName)",
                                                  "new(DepartmentCode, DepartmentName)")
                                         .OrderBy("Key.DepartmentName")
                                         .Select("Key.DepartmentName");
                    break;
                case "InvCategoryID":
                    qryResult = qryResult.Join(context.InvCategories, "CategoryID", reportDataStruct.DbColumnName.Trim(), "new(inner.CategoryCode, inner.CategoryName)")
                                .GroupBy("new(CategoryCode, CategoryName)", "new(CategoryCode, CategoryName)")
                                .OrderBy("Key.CategoryName")
                                .Select("Key.CategoryName");
                    break;
                case "InvSubCategoryID":
                    qryResult = qryResult.Join(context.InvSubCategories, "SubCategoryID", reportDataStruct.DbColumnName.Trim(), "new(inner.SubCategoryCode, inner.SubCategoryName)")
                                .GroupBy("new(SubCategoryCode, SubCategoryName)", "new(SubCategoryCode, SubCategoryName)")
                                .OrderBy("Key.SubCategoryName")
                                .Select("Key.SubCategoryName");
                    break;
                case "InvSubCategory2ID":
                    qryResult = qryResult.Join(context.InvSubCategories2, "SubCategory2ID", reportDataStruct.DbColumnName.Trim(), "new(inner.SubCategory2Code, inner.SubCategory2Name)")
                                .GroupBy("new(SubCategory2Code, SubCategory2Name)", "new(SubCategory2Code, SubCategory2Name)")
                                .OrderBy("Key.SubCategory2Name")
                                .Select("Key.SubCategory2Name");
                    break;
                default:
                    qryResult = qryResult.OrderBy(reportDataStruct.DbColumnName).Select(reportDataStruct.DbColumnName.Trim());
                    break;
            }

            // change this code to get rid of the foreach loop
            ArrayList selectionDataList = new ArrayList();
            if (reportDataStruct.ValueDataType.Equals(typeof(bool)))
            {
                foreach (var item in qryResult)
                {
                    if (item.Equals(true))
                    { selectionDataList.Add("Yes"); }
                    else
                    { selectionDataList.Add("No"); }
                }
            }
            else if (reportDataStruct.ValueDataType.Equals(typeof(DateTime)))
            {
                foreach (var item in qryResult)
                {
                    DateTime tdnew = new DateTime();
                    tdnew = Convert.ToDateTime(item.ToString());
                    selectionDataList.Add(tdnew.ToShortDateString());
                }
            }
            else
            {
                foreach (var item in qryResult)
                { selectionDataList.Add(item.ToString()); }
            }
            return selectionDataList;
        }

       #region GetNewCode

        public string GetDirectNewCode(string formName) 
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = autoGenerateInfo.Prefix;
            int codeLength = autoGenerateInfo.CodeLength;

            getNewCode = context.InvProductMasters.Max(c => c.ProductCode);
            
            if (getNewCode != null)
                getNewCode = getNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
            else
                getNewCode = "0";

            getNewCode = (int.Parse(getNewCode) + 1).ToString();
            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
            return getNewCode;
        }

        #endregion


        public string GetNewCode(string formName, string productCode, long departmentID, long categoryID, long subCategoryID) 
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = productCode;
            int codeLength = autoGenerateInfo.CodeLength;

            int prefixLength = Convert.ToInt32(productCode.Length);
            int length = codeLength - prefixLength;

            //getNewCode = context.InvProductMasters.Where(pm => pm.DepartmentID.Equals(departmentID) && pm.CategoryID.Equals(categoryID) && pm.SubCategoryID.Equals(subCategoryID)).Max(c => EntityFunctions.Right(c.ProductCode, length));
            getNewCode = context.InvProductMasters.Where(pm => pm.DepartmentID.Equals(departmentID) && pm.CategoryID.Equals(categoryID) && pm.SubCategoryID.Equals(subCategoryID)).Max(c => ((int)c.ProductCode.Length == 12 ? (c.ProductCode.Substring(0, 8) + "0" + c.ProductCode.Substring(c.ProductCode.Length - 4, 4)) : c.ProductCode).Substring(((int)c.ProductCode.Length == 12 ? (c.ProductCode.Substring(0, 8) + "0" + c.ProductCode.Substring(c.ProductCode.Length - 4, 4)) : c.ProductCode).Length - 5, 5));

            if (getNewCode == null)
            {
                getNewCode = "0";
            }

            getNewCode = (int.Parse(getNewCode) + 1).ToString();
            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
            return getNewCode;
        }


        public string[] GetAllProductCodes()
        {
            List<string> ProductCodeList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.ProductCode).ToList();
            return ProductCodeList.ToArray();

        }

        public string[] GetAllProductCodes(long DeptId)
        {
            List<string> ProductCodeList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false) && c.DepartmentID.Equals(DeptId)).Select(c => c.ProductCode).ToList();
            return ProductCodeList.ToArray();
        }

        public string[] GetAllProductCodes(long DeptId,Decimal SellingPrice)
        {
            List<string> ProductCodeList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false) && c.DepartmentID.Equals(DeptId) && c.SellingPrice.Equals(SellingPrice)).Select(c => c.ProductCode).ToList();
            return ProductCodeList.ToArray();
        }

        public int GetAllProductCodesCount(long DeptId, Decimal SellingPrice)
        {
            return context.InvProductMasters.Where(c => c.IsDelete.Equals(false) && c.DepartmentID.Equals(DeptId) && c.SellingPrice.Equals(SellingPrice)).Select(c => c.ProductCode).Count();
        }

        public InvProductMaster GetProductCode(long DeptId, Decimal SellingPrice)
        {
            return context.InvProductMasters.Where(c => c.IsDelete.Equals(false) && c.DepartmentID.Equals(DeptId) && c.SellingPrice.Equals(SellingPrice)).FirstOrDefault();
        }

        public string[] GetAllProductNames()
        {
            List<string> ProductNameList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.ProductName).ToList();
            return ProductNameList.ToArray();
        }

        public string[] GetAllProductCodesBatch()
        {
            List<string> ProductCodeList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.ProductCode).ToList();
            return ProductCodeList.ToArray();

        }

        public string[] GetAllProductNamesBatch()
        {
            List<string> ProductNameList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.ProductName).ToList();
            return ProductNameList.ToArray();

        }


        public string[] GetAllProductCodesBySupplier(long supplierID)
        {
            List<string> ProductCodeList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false) && c.SupplierID.Equals(supplierID)).Select(c => c.ProductCode).ToList();
            return ProductCodeList.ToArray();

        }

        public string[] GetAllProductNamesBySupplier(long supplierID)
        {
            List<string> ProductNameList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false) && c.SupplierID.Equals(supplierID)).Select(c => c.ProductName).ToList();
            return ProductNameList.ToArray();

        }

        public List<InvProductUnitConversion> GetUpdateProductUnitConversionTemp(List<InvProductUnitConversion> invProductUnitConversionList, InvProductUnitConversion invProductUnitConversionPrm, InvProductMaster invProductMaster)
        {
            InvProductUnitConversion invProductUnitConversion = new InvProductUnitConversion();

            invProductUnitConversion = invProductUnitConversionList.Where(p => p.ProductID == invProductUnitConversionPrm.ProductID && p.UnitOfMeasureID == invProductUnitConversionPrm.UnitOfMeasureID).FirstOrDefault();

            if (invProductUnitConversion == null)
            {
                if (invProductUnitConversionList.Count.Equals(0))
                    invProductUnitConversionPrm.LineNo = 1;
                else
                    invProductUnitConversionPrm.LineNo = invProductUnitConversionList.Max(s => s.LineNo) + 1;
            }
            else
            {
                invProductUnitConversionList.Remove(invProductUnitConversion);
                invProductUnitConversionPrm.LineNo = invProductUnitConversion.LineNo;
            }

            invProductUnitConversionList.Add(invProductUnitConversionPrm);

            return invProductUnitConversionList.OrderBy(pd => pd.LineNo).ToList();
        }


        public List<InvProductLink> GetUpdateProductLinkTemp(List<InvProductLink> invProductLinkList, InvProductLink invProductLinkPrm, InvProductMaster invProductMaster)
        {
            InvProductLink invProductLink = new InvProductLink();

            invProductLink = invProductLinkList.Where(p => p.ProductID == invProductLinkPrm.ProductID && p.ProductLinkCode == invProductLinkPrm.ProductLinkCode).FirstOrDefault();

            if (invProductLink == null)
            {
                //if (invProductLinkList.Count.Equals(0))
                //    invProductLinkPrm.LineNo = 1;
                //else
                //    invProductLinkPrm.LineNo = invProductLinkList.Max(s => s.LineNo) + 1;
            }
            else
            {
                invProductLinkList.Remove(invProductLink);
                //invProductLinkPrm.LineNo = invProductLink.LineNo;
            }

            invProductLinkList.Add(invProductLinkPrm);

            return invProductLinkList.ToList();
        }

        //public List<InvProductUnitConversion> GetUpdateProductUnitConversionTemp(List<InvProductUnitConversion> invProductUnitConversionList, InvProductUnitConversion invProductUnitConversionPrm, InvProductMaster invProductMaster)
        //{
        //    InvProductUnitConversion invProductUnitConversion = new InvProductUnitConversion();

        //    invProductUnitConversion = invProductUnitConversionList.Where(p => p.ProductID == invProductUnitConversionPrm.ProductID && p.UnitOfMeasureID == invProductUnitConversionPrm.UnitOfMeasureID).FirstOrDefault();

        //    if (invProductUnitConversion == null || invProductUnitConversion.LineNo.Equals(0))
        //    {
        //        if (invProductUnitConversionList.Count.Equals(0))
        //            invProductUnitConversionPrm.LineNo = 1;
        //        else
        //            invProductUnitConversionPrm.LineNo = invProductUnitConversionList.Max(s => s.LineNo) + 1;
        //    }
        //    else
        //    {
        //        invProductUnitConversionList.Remove(invProductUnitConversion);
        //        invProductUnitConversionPrm.LineNo = invProductUnitConversion.LineNo;
        //    }

        //    invProductUnitConversionList.Add(invProductUnitConversionPrm);

        //    return invProductUnitConversionList.OrderBy(pd => pd.LineNo).ToList();
        //}

        public List<InvProductSupplierLink> GetUpdateProductSupplierLinkTemp(List<InvProductSupplierLink> invProductSupplierLinkList, InvProductSupplierLink invProductSupplierLinkPrn, InvProductMaster invProductMaster)
        {
            InvProductSupplierLink invProductSupplierLink = new InvProductSupplierLink();

            invProductSupplierLink = invProductSupplierLinkList.Where(p => p.ProductID == invProductSupplierLinkPrn.ProductID && p.SupplierCode == invProductSupplierLinkPrn.SupplierCode).FirstOrDefault();

            if (invProductSupplierLink != null)
            {
                invProductSupplierLinkList.Remove(invProductSupplierLink);
            }

            invProductSupplierLinkList.Add(invProductSupplierLinkPrn);

            return invProductSupplierLinkList.ToList();
        }

        public string GetProductCodeFromBatchBarcode(long barcode)
        {
            string productCode = string.Empty;
            InvProductBatchNoExpiaryDetail invProductBatchNoExpiaryDetail = new InvProductBatchNoExpiaryDetail();
            InvBatchNoExpiaryDetailService invBatchNoExpiaryDetailService = new InvBatchNoExpiaryDetailService();

            invProductBatchNoExpiaryDetail = invBatchNoExpiaryDetailService.GetBatchNoExpiaryDetailByBarcodeForProductMaster(barcode);

            if (invProductBatchNoExpiaryDetail != null)
            {
                productCode = this.GetProductDetailsByID(invProductBatchNoExpiaryDetail.ProductID).ProductCode.ToString();
            }

            return productCode;
        }

        public List<InvProductMaster> GetAllProductsFromPatternNumber(long valueID)
        {
            List<InvProductMaster>rtnList=new List<InvProductMaster>();
            InvProductExtendedPropertyValue invProductExtendedPropertyValue = new InvProductExtendedPropertyValue();
            InvProductExtendedPropertyValueService invProductExtendedPropertyValueService = new InvProductExtendedPropertyValueService();

            var qry = (from epv in context.InvProductExtendedPropertyValues
                       where epv.InvProductExtendedValueID == valueID
                       select new
                       {
                           epv.ProductID
                       }).ToArray();

            foreach (var temp in qry)
            {
                InvProductMaster invProductMaster = new InvProductMaster();
                invProductMaster = this.GetProductDetailsByID(temp.ProductID);

                rtnList.Add(invProductMaster);
            }

            return rtnList;
        }


        public DataTable GetProductsSearchAccordingToCombination(long departmentID, long CategoryID, long SubCategoryID, long subCategory2ID, long SupplierID, decimal costPrice, decimal sellingPrise)
        {
            var qry = (from pm in context.InvProductMasters
                       join dep in context.InvDepartments on pm.DepartmentID equals dep.InvDepartmentID
                       join cat in context.InvCategories on pm.CategoryID equals cat.InvCategoryID
                       join scat in context.InvSubCategories on pm.SubCategoryID equals scat.InvSubCategoryID
                       join scat2 in context.InvSubCategories2 on pm.SubCategory2ID equals scat2.InvSubCategory2ID
                       join s in context.Suppliers on pm.SupplierID equals s.SupplierID
                       select new
                       {
                           pm.InvProductMasterID,
                           pm.ProductCode,
                           pm.ProductName,
                           dep.DepartmentName,
                           pm.DepartmentID,
                           cat.CategoryName,
                           pm.CategoryID,
                           scat.SubCategoryName,
                           pm.SubCategoryID,
                           scat2.SubCategory2Name,
                           pm.SubCategory2ID,
                           s.SupplierName,
                           pm.SupplierID,
                           pm.CostPrice,
                           pm.SellingPrice
                       }).ToArray();



            if (departmentID != 0)
            {
                qry = (from q in qry
                       where q.DepartmentID == departmentID
                       select new
                       {
                           q.InvProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice
                       }).ToArray();

            }
            if (CategoryID != 0)
            {
                qry = (from q in qry
                       where q.CategoryID == CategoryID
                       select new
                       {
                           q.InvProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice
                       }).ToArray();
            }
            if (SubCategoryID != 0)
            {
                qry = (from q in qry
                       where q.SubCategoryID == SubCategoryID
                       select new
                       {
                           q.InvProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice
                       }).ToArray();
            }
            if (subCategory2ID != 0)
            {
                qry = (from q in qry
                       where q.SubCategory2ID == subCategory2ID
                       select new
                       {
                           q.InvProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice
                       }).ToArray();
            }
            if (SupplierID != 0)
            {
                qry = (from q in qry
                       where q.SupplierID == SupplierID
                       select new
                       {
                           q.InvProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice
                       }).ToArray();
            }
            if (costPrice != 0)
            {
                qry = (from q in qry
                       where q.CostPrice == costPrice
                       select new
                       {
                           q.InvProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice
                       }).ToArray();
            }
            if (sellingPrise != 0)
            {
                qry = (from q in qry
                       where q.SellingPrice == sellingPrise
                       select new
                       {
                           q.InvProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice
                       }).ToArray();
            }

            var rtnqry = (from q in qry
                          select new
                          {
                              q.ProductCode,
                              q.ProductName,
                              Department = q.DepartmentName,
                              Lifestyle = q.CategoryName,
                              Product = q.SubCategoryName,
                              Brand = q.SubCategory2Name,
                              q.SupplierName,
                              q.CostPrice,
                              q.SellingPrice
                          }).ToArray();

            return rtnqry.ToDataTable();
        }

        public DataTable GetProductsSearchAccordingToCombination(long departmentID, long CategoryID, long SubCategoryID, long subCategory2ID, long SupplierID, decimal costPrice, decimal sellingPrise, List<InvProductExtendedPropertyValue> invProductExtendedPropertyValue) 
        {
            var listPrm = invProductExtendedPropertyValue.ToArray();
            var tableToList = (from pepv in context.InvProductExtendedPropertyValues
                               join pep in context.InvProductExtendedProperties on pepv.InvProductExtendedPropertyID equals pep.InvProductExtendedPropertyID
                               join epv in context.InvProductExtendedValues on pepv.InvProductExtendedValueID equals epv.InvProductExtendedValueID
                               select new
                               {
                                   pepv.InvProductExtendedValueID,
                                   pepv.ProductID,
                                   pep.ExtendedPropertyName,
                                   epv.ValueData
                               }).AsNoTracking().ToList();

            var extendedQryList = (from lst in tableToList
                                   join q in listPrm on lst.InvProductExtendedValueID equals q.InvProductExtendedValueID
                                   where lst.InvProductExtendedValueID == q.InvProductExtendedValueID
                                   select new
                                   {
                                       lst.ProductID,
                                       lst.ExtendedPropertyName,
                                       lst.ValueData
                                   }).ToList();

            var qryList = (from pm in context.InvProductMasters
                           join dep in context.InvDepartments on pm.DepartmentID equals dep.InvDepartmentID
                           join cat in context.InvCategories on pm.CategoryID equals cat.InvCategoryID
                           join scat in context.InvSubCategories on pm.SubCategoryID equals scat.InvSubCategoryID
                           join scat2 in context.InvSubCategories2 on pm.SubCategory2ID equals scat2.InvSubCategory2ID
                           join s in context.Suppliers on pm.SupplierID equals s.SupplierID
                           select new
                           {
                               pm.InvProductMasterID,
                               pm.ProductCode,
                               pm.ProductName,
                               dep.DepartmentName,
                               pm.DepartmentID,
                               cat.CategoryName,
                               pm.CategoryID,
                               scat.SubCategoryName,
                               pm.SubCategoryID,
                               scat2.SubCategory2Name,
                               pm.SubCategory2ID,
                               s.SupplierName,
                               pm.SupplierID,
                               pm.CostPrice,
                               pm.SellingPrice
                           }).AsNoTracking().ToList();

            var qry = (from q in qryList
                       join eq in extendedQryList on q.InvProductMasterID equals eq.ProductID
                       select new
                       {
                           q.InvProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           eq.ExtendedPropertyName,
                           eq.ValueData
                       }).ToArray();



            if (departmentID != 0)
            {
                qry = (from q in qry
                       where q.DepartmentID == departmentID
                       select new
                       {
                           q.InvProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ExtendedPropertyName,
                           q.ValueData
                       }).ToArray();

            }
            if (CategoryID != 0)
            {
                qry = (from q in qry
                       where q.CategoryID == CategoryID
                       select new
                       {
                           q.InvProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ExtendedPropertyName,
                           q.ValueData
                       }).ToArray();
            }
            if (SubCategoryID != 0)
            {
                qry = (from q in qry
                       where q.SubCategoryID == SubCategoryID
                       select new
                       {
                           q.InvProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ExtendedPropertyName,
                           q.ValueData
                       }).ToArray();
            }
            if (subCategory2ID != 0)
            {
                qry = (from q in qry
                       where q.SubCategory2ID == subCategory2ID
                       select new
                       {
                           q.InvProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ExtendedPropertyName,
                           q.ValueData
                       }).ToArray();
            }
            if (SupplierID != 0)
            {
                qry = (from q in qry
                       where q.SupplierID == SupplierID
                       select new
                       {
                           q.InvProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ExtendedPropertyName,
                           q.ValueData
                       }).ToArray();
            }
            if (costPrice != 0)
            {
                qry = (from q in qry
                       where q.CostPrice == costPrice
                       select new
                       {
                           q.InvProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ExtendedPropertyName,
                           q.ValueData
                       }).ToArray();
            }
            if (sellingPrise != 0)
            {
                qry = (from q in qry
                       where q.SellingPrice == sellingPrise
                       select new
                       {
                           q.InvProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ExtendedPropertyName,
                           q.ValueData
                       }).ToArray();
            }

            var rtnqry = (from q in qry
                          select new
                          {
                              q.ProductCode,
                              q.ProductName,
                              Department = q.DepartmentName,
                              Lifestyle = q.CategoryName,
                              Product = q.SubCategoryName,
                              Brand = q.SubCategory2Name,
                              q.SupplierName,
                              q.CostPrice,
                              q.SellingPrice,
                              q.ExtendedPropertyName,
                              q.ValueData
                          }).ToArray();

            return rtnqry.ToDataTable();
        }


        public List<InvProductProperty> GetSearchProducts(long departmentID, long CategoryID, long SubCategoryID, long subCategory2ID, long SupplierID, decimal costPrice, decimal sellingPrice,
                                                          string productFeature, string country, string cut, string sleeve, string heel, string embelishment, string fit, string length, string material, string txture, string neck,
                                                          string collar, string size, string colour, string patternNo, string Brand, string shop)
        {
            List<InvProductProperty> rtnList = new List<InvProductProperty>();

            var qry = (from pm in context.InvProductMasters
                       join pp in context.InvProductProperties on pm.InvProductMasterID equals pp.ProductID
                       select new
                       {
                           pm.ProductCode,
                           pm.ProductName,
                           pm.DepartmentID,
                           pm.CategoryID,
                           pm.SubCategoryID,
                           pm.SubCategory2ID,
                           pm.SupplierID,
                           pm.CostPrice,
                           pm.SellingPrice,
                           pp.ProductFeature,
                           pp.Country,
                           pp.Cut,
                           pp.Sleeve,
                           pp.Heel,
                           pp.Embelishment,
                           pp.Fit,
                           pp.Length,
                           pp.Material,
                           pp.Texture,
                           pp.Neck,
                           pp.Collar,
                           pp.Size,
                           pp.Colour,
                           pp.PatternNo,
                           pp.Brand,
                           pp.Shop
                       }).ToArray();
            
            #region departmentID
            if (departmentID != 0)
            {
                qry = (from q in qry
                       where q.DepartmentID == departmentID
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region CategoryID
            if (CategoryID != 0)
            {
                qry = (from q in qry
                       where q.CategoryID == CategoryID
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();

            }
            #endregion

            #region SubCategoryID
            if (SubCategoryID != 0)
            {

                qry = (from q in qry
                       where q.SubCategoryID == SubCategoryID
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();

            }
            #endregion

            #region SubCategory2ID
            if (subCategory2ID != 0)
            {
                qry = (from q in qry
                       where q.SubCategory2ID == subCategory2ID
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region SupplierID
            if (SupplierID != 0)
            {
                qry = (from q in qry
                       where q.SupplierID == SupplierID
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Cost Price
            if (costPrice != 0)
            {
                qry = (from q in qry
                       where q.CostPrice == costPrice
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Selling Price
            if (sellingPrice != 0)
            {
                qry = (from q in qry
                       where q.SellingPrice == sellingPrice
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Product Feature
            if (!string.IsNullOrEmpty(productFeature))
            {
                qry = (from q in qry
                       where q.ProductFeature == productFeature
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Country
            if (!string.IsNullOrEmpty(country))
            {
                qry = (from q in qry
                       where q.Country == country
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Cut
            if (!string.IsNullOrEmpty(cut))
            {
                qry = (from q in qry
                       where q.Cut == cut
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Sleeve
            if (!string.IsNullOrEmpty(sleeve))
            {
                qry = (from q in qry
                       where q.Sleeve == sleeve
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Heel
            if (!string.IsNullOrEmpty(heel))
            {
                qry = (from q in qry
                       where q.Heel == heel
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Embleshment
            if (!string.IsNullOrEmpty(embelishment))
            {
                qry = (from q in qry
                       where q.Embelishment == embelishment
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Fit
            if (!string.IsNullOrEmpty(fit))
            {
                qry = (from q in qry
                       where q.Fit == fit
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Length
            if (!string.IsNullOrEmpty(length))
            {
                qry = (from q in qry
                       where q.Length == length
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Material
            if (!string.IsNullOrEmpty(material))
            {
                qry = (from q in qry
                       where q.Material == material
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Texture
            if (!string.IsNullOrEmpty(txture))
            {
                qry = (from q in qry
                       where q.Texture == txture
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Neck
            if (!string.IsNullOrEmpty(neck))
            {
                qry = (from q in qry
                       where q.Neck == neck
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Collar
            if (!string.IsNullOrEmpty(collar))
            {
                qry = (from q in qry
                       where q.Collar == collar
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Size
            if (!string.IsNullOrEmpty(size))
            {
                qry = (from q in qry
                       where q.Size == size
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Colour
            if (!string.IsNullOrEmpty(colour))
            {
                qry = (from q in qry
                       where q.Colour == colour
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Pattern No
            if (!string.IsNullOrEmpty(patternNo))
            {
                qry = (from q in qry
                       where q.PatternNo == patternNo
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Brand
            if (!string.IsNullOrEmpty(Brand))
            {
                qry = (from q in qry
                       where q.Brand == Brand
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Shop
            if (!string.IsNullOrEmpty(shop))
            {
                qry = (from q in qry
                       where q.Shop == shop
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            foreach (var item in qry)
            {
                InvProductProperty invProductProperty = new InvProductProperty();

                invProductProperty.ProductCode = item.ProductCode;
                invProductProperty.ProductName = item.ProductName;

                rtnList.Add(invProductProperty);
            }

            return rtnList;
        }



        public DataTable GetSearchProductsDatatable(long departmentID, long CategoryID, long SubCategoryID, long subCategory2ID, long SupplierID, decimal costPrice, decimal sellingPrice,
                                                          string productFeature, string country, string cut, string sleeve, string heel, string embelishment, string fit, string length, string material, string txture, string neck,
                                                          string collar, string size, string colour, string patternNo, string Brand, string shop)
        {
            List<InvProductProperty> rtnList = new List<InvProductProperty>();

            var qry = (from pm in context.InvProductMasters
                       join pp in context.InvProductProperties on pm.InvProductMasterID equals pp.ProductID
                       select new
                       {
                           pm.ProductCode,
                           pm.ProductName,
                           pm.DepartmentID,
                           pm.CategoryID,
                           pm.SubCategoryID,
                           pm.SubCategory2ID,
                           pm.SupplierID,
                           pm.CostPrice,
                           pm.SellingPrice,
                           pp.ProductFeature,
                           pp.Country,
                           pp.Cut,
                           pp.Sleeve,
                           pp.Heel,
                           pp.Embelishment,
                           pp.Fit,
                           pp.Length,
                           pp.Material,
                           pp.Texture,
                           pp.Neck,
                           pp.Collar,
                           pp.Size,
                           pp.Colour,
                           pp.PatternNo,
                           pp.Brand,
                           pp.Shop
                       }).ToArray();

            #region departmentID
            if (departmentID != 0)
            {
                qry = (from q in qry
                       where q.DepartmentID == departmentID
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region CategoryID
            if (CategoryID != 0)
            {
                qry = (from q in qry
                       where q.CategoryID == CategoryID
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();

            }
            #endregion

            #region SubCategoryID
            if (SubCategoryID != 0)
            {

                qry = (from q in qry
                       where q.SubCategoryID == SubCategoryID
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();

            }
            #endregion

            #region SubCategory2ID
            if (subCategory2ID != 0)
            {
                qry = (from q in qry
                       where q.SubCategory2ID == subCategory2ID
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region SupplierID
            if (SupplierID != 0)
            {
                qry = (from q in qry
                       where q.SupplierID == SupplierID
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Cost Price
            if (costPrice != 0)
            {
                qry = (from q in qry
                       where q.CostPrice == costPrice
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Selling Price
            if (sellingPrice != 0)
            {
                qry = (from q in qry
                       where q.SellingPrice == sellingPrice
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Product Feature
            if (!string.IsNullOrEmpty(productFeature))
            {
                qry = (from q in qry
                       where q.ProductFeature == productFeature
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Country
            if (!string.IsNullOrEmpty(country))
            {
                qry = (from q in qry
                       where q.Country == country
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Cut
            if (!string.IsNullOrEmpty(cut))
            {
                qry = (from q in qry
                       where q.Cut == cut
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Sleeve
            if (!string.IsNullOrEmpty(sleeve))
            {
                qry = (from q in qry
                       where q.Sleeve == sleeve
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Heel
            if (!string.IsNullOrEmpty(heel))
            {
                qry = (from q in qry
                       where q.Heel == heel
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Embleshment
            if (!string.IsNullOrEmpty(embelishment))
            {
                qry = (from q in qry
                       where q.Embelishment == embelishment
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Fit
            if (!string.IsNullOrEmpty(fit))
            {
                qry = (from q in qry
                       where q.Fit == fit
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Length
            if (!string.IsNullOrEmpty(length))
            {
                qry = (from q in qry
                       where q.Length == length
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Material
            if (!string.IsNullOrEmpty(material))
            {
                qry = (from q in qry
                       where q.Material == material
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Texture
            if (!string.IsNullOrEmpty(txture))
            {
                qry = (from q in qry
                       where q.Texture == txture
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Neck
            if (!string.IsNullOrEmpty(neck))
            {
                qry = (from q in qry
                       where q.Neck == neck
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Collar
            if (!string.IsNullOrEmpty(collar))
            {
                qry = (from q in qry
                       where q.Collar == collar
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Size
            if (!string.IsNullOrEmpty(size))
            {
                qry = (from q in qry
                       where q.Size == size
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Colour
            if (!string.IsNullOrEmpty(colour))
            {
                qry = (from q in qry
                       where q.Colour == colour
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Pattern No
            if (!string.IsNullOrEmpty(patternNo))
            {
                qry = (from q in qry
                       where q.PatternNo == patternNo
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Brand
            if (!string.IsNullOrEmpty(Brand))
            {
                qry = (from q in qry
                       where q.Brand == Brand
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            #region Shop
            if (!string.IsNullOrEmpty(shop))
            {
                qry = (from q in qry
                       where q.Shop == shop
                       select new
                       {
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentID,
                           q.CategoryID,
                           q.SubCategoryID,
                           q.SubCategory2ID,
                           q.SupplierID,
                           q.CostPrice,
                           q.SellingPrice,
                           q.ProductFeature,
                           q.Country,
                           q.Cut,
                           q.Sleeve,
                           q.Heel,
                           q.Embelishment,
                           q.Fit,
                           q.Length,
                           q.Material,
                           q.Texture,
                           q.Neck,
                           q.Collar,
                           q.Size,
                           q.Colour,
                           q.PatternNo,
                           q.Brand,
                           q.Shop
                       }).ToArray();
            }
            #endregion

            var rtnQry = (from q in qry
                   join dep in context.InvDepartments on q.DepartmentID equals dep.InvDepartmentID
                   join cat in context.InvCategories on q.CategoryID equals cat.InvCategoryID
                   join scat in context.InvSubCategories on q.SubCategoryID equals scat.InvSubCategoryID
                   join scat2 in context.InvSubCategories2 on q.SubCategory2ID equals scat2.InvSubCategory2ID
                   join s in context.Suppliers on q.SupplierID equals s.SupplierID
                   select new
                   {
                       ItemCode = q.ProductCode,
                       ItemName = q.ProductName,
                       dep.DepartmentName,
                       Lifestyle = cat.CategoryName,
                       Product = scat.SubCategoryName,
                       Brand = scat2.SubCategory2Name,
                       s.SupplierName,
                       q.CostPrice,
                       q.SellingPrice,
                       q.ProductFeature,
                       q.Country,
                       q.Cut,
                       q.Sleeve,
                       q.Heel,
                       q.Embelishment,
                       q.Fit,
                       q.Length,
                       q.Material,
                       q.Texture,
                       q.Neck,
                       q.Collar,
                       q.Size,
                       q.Colour,
                       q.PatternNo,
                       q.Shop
                   }).ToArray();

            return rtnQry.ToDataTable();
        }


        #endregion

    }
}
