using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;

using Domain;
using Data;
using System.Data;
using Utility;
using System.Data.Entity;
using EntityFramework.Extensions;
using MoreLinq;

namespace Service
{
    /// <summary>
    /// Developed by - C.S.Malluwawadu
    /// </summary>
    public class InvPromotionMasterService
    {
        ERPDbContext context = new ERPDbContext();

        #region GetNewCode....

        public string GetNewCode(string preFix, int suffix, int codeLength)
        {
            string GetNewCode = string.Empty;
            GetNewCode = context.InvPromotionMasters.Max(a => a.PromotionCode);
            if (GetNewCode != null)
            {
                GetNewCode = GetNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
            }
            else
            {
                GetNewCode = "0";
            }
            GetNewCode = (int.Parse(GetNewCode) + 1).ToString();
            GetNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + GetNewCode.Length))) + GetNewCode;
            return GetNewCode;
        }

        #endregion


        public InvPromotionMaster GetPromotionByCode(string PromotionCode)
        {
            return context.InvPromotionMasters.Where(a => a.PromotionCode.Equals(PromotionCode) && a.IsDelete.Equals(false)).FirstOrDefault();
        }


        public void AddPromotion(InvPromotionMaster Promotion)
        {
            context.InvPromotionMasters.Add(Promotion);
            context.SaveChanges();       

        }


        public void UpdatePromotion(InvPromotionMaster existingPromotion)
        {
            existingPromotion.ModifiedUser = Common.LoggedUser;
            existingPromotion.ModifiedDate = Common.GetSystemDateWithTime(); 
            this.context.Entry(existingPromotion).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void UpdatePromotion(int Edatatransfer, int datatransfer)
        {
            context.InvPromotionMasters.Update(ps => ps.DataTransfer.Equals(Edatatransfer), ps => new InvPromotionMaster { DataTransfer = datatransfer });
        }
        public void UpdateInvPromotionDetailsSubTotal(int Edatatransfer, int datatransfer)
        {
            context.InvPromotionDetailsSubTotal.Update(r => r.DataTransfer.Equals(Edatatransfer), r => new InvPromotionDetailsSubTotal { DataTransfer = datatransfer });
        }

        public void UpdateInvPromotionDetailsSubCategoryDis(int Edatatransfer, int datatransfer)
        {
            context.InvPromotionDetailsSubCategoryDis.Update(ps => ps.DataTransfer.Equals(Edatatransfer), ps => new InvPromotionDetailsSubCategoryDis { DataTransfer = datatransfer });
        }

        public void UpdateInvPromotionDetailsProductDis(int Edatatransfer, int datatransfer)
        {
            context.InvPromotionDetailsProductDis.Update(ps => ps.DataTransfer.Equals(Edatatransfer), ps => new InvPromotionDetailsProductDis { DataTransfer = datatransfer });
        }

        public void UpdateInvPromotionDetailsGetYDetails(int Edatatransfer, int datatransfer)
        {
            context.InvPromotionDetailsGetYDetails.Update(ps => ps.DataTransfer.Equals(Edatatransfer), ps => new InvPromotionDetailsGetYDetails { DataTransfer = datatransfer });
        }

        public void UpdateInvPromotionDetailsDepartmentDis(int Edatatransfer, int datatransfer)
        {
            context.InvPromotionDetailsDepartmentDis.Update(ps => ps.DataTransfer.Equals(Edatatransfer), ps => new InvPromotionDetailsDepartmentDis { DataTransfer = datatransfer });
        }

        public void UpdateInvPromotionDetailsCategoryDis(int Edatatransfer, int datatransfer)
        {
            context.InvPromotionDetailsCategoryDis.Update(ps => ps.DataTransfer.Equals(Edatatransfer), ps => new InvPromotionDetailsCategoryDis { DataTransfer = datatransfer });
        }

        public void UpdateInvPromotionDetailsBuyXSubCategory(int Edatatransfer, int datatransfer)
        {
            context.InvPromotionDetailsBuyXSubCategory.Update(ps => ps.DataTransfer.Equals(Edatatransfer), ps => new InvPromotionDetailsBuyXSubCategory { DataTransfer = datatransfer });
        }

        public void UpdateInvPromotionDetailsBuyXProduct(int Edatatransfer, int datatransfer)
        {
            context.InvPromotionDetailsBuyXProduct.Update(ps => ps.DataTransfer.Equals(Edatatransfer), ps => new InvPromotionDetailsBuyXProduct { DataTransfer = datatransfer });
        }

        public void UpdateInvPromotionDetailsBuyXCategory(int Edatatransfer, int datatransfer)
        {
            context.InvPromotionDetailsBuyXCategory.Update(x => x.DataTransfer.Equals(Edatatransfer), x => new InvPromotionDetailsBuyXCategory { DataTransfer = datatransfer });
        }

        public void UpdateInvPromotionDetailsBuyXDepartment(int Edatatransfer, int datatransfer)
        {
            context.InvPromotionDetailsBuyXDepartment.Update(x => x.DataTransfer.Equals(Edatatransfer), x => new InvPromotionDetailsBuyXDepartment{ DataTransfer = datatransfer });
        }
 

        public void UpdateInvPromotionDetailsAllowTypes(int Edatatransfer, int datatransfer)
        {
            context.InvPromotionDetailsAllowTypes.Update(ps => ps.DataTransfer.Equals(Edatatransfer), ps => new InvPromotionDetailsAllowTypes { DataTransfer = datatransfer });
        }

        public void UpdateInvPromotionDetailsAllowLocations(int Edatatransfer, int datatransfer)
        {
            context.InvPromotionDetailsAllowLocations.Update(a => a.DataTransfer.Equals(Edatatransfer), a => new InvPromotionDetailsAllowLocations { DataTransfer = datatransfer });
        }

        public void UpdateInvPromotionDetailsBuyXSubCategory2(int Edatatransfer, int datatransfer)
        {
            context.InvPromotionDetailsBuyXSubCategory2.Update(a => a.DataTransfer.Equals(Edatatransfer), a => new InvPromotionDetailsBuyXSubCategory2 { DataTransfer = datatransfer });
        }

        public void UpdateInvPromotionDetailsSubCategory2Dis(int Edatatransfer, int datatransfer)
        {
            context.InvPromotionDetailsSubCategory2Dis.Update(a => a.DataTransfer.Equals(Edatatransfer), a => new InvPromotionDetailsSubCategory2Dis { DataTransfer = datatransfer });
        }

        public void UpdateInvPromotionType(int Edatatransfer, int datatransfer)
        {
            context.InvPromotionTypes.Update(a => a.DataTransfer.Equals(Edatatransfer), a => new InvPromotionType { DataTransfer = datatransfer });
        }


        public DataTable GetAllPromotionDataTable()
        {
            DataTable tblPromotion = new DataTable();
            var query = from d in context.InvPromotionMasters where d.IsDelete == false select new { d.InvPromotionMasterID, d.CompanyID, d.LocationID, d.CostCentreID, d.PromotionCode, d.PromotionName, d.IsAutoApply, d.PromotionTypeID, d.StartDate, d.EndDate, d.IsMonday, d.IsTuesday, d.IsWednesday, d.IsThuresday,d.IsFriday,d.IsSaturday,d.IsSunday,d.IsMondayTime,d.IsTuesdayTime,d.IsWednesdayTime,d.IsThuresdayTime,d.IsFridayTime,d.IsSaturdayTime,d.IsSundayTime,d.MondayStartTime,d.MondayEndTime,d.TuesdayStartTime,d.TuesdayEndTime,d.WednesdayStartTime,d.WednesdayEndTime,d.ThuresdayStartTime,d.ThuresdayEndTime,d.FridayStartTime,d.FridayEndTime,d.SaturdayStartTime,d.SaturdayEndTime,d.SundayStartTime,d.SundayEndTime,d.PaymentMethodID,d.IsProvider,d.IsAllLocations,d.IsAllType,d.IsValueRange,d.MinimumValue,d.MaximumValue,d.DiscountValue,d.DiscountPercentage,d.Points,d.Remark,d.DisplayMessage,d.CashierMessage,d.IsDelete};
            return tblPromotion = Common.LINQToDataTable(query);

        }


        public DataTable GetAllDTPromotionDataTable()
        {
            //DataTable tblPromotion = new DataTable();
            //var query = (from d in context.InvPromotionMasters select new { d});
            //return tblPromotion = query.ToDataTable();

            //context.Configuration.LazyLoadingEnabled = false;
            //return context.InvPromotionMasters.SqlQuery("select InvPromotionMasterID , CompanyID , LocationID , CostCentreID , PromotionCode , PromotionName , IsAutoApply , PromotionTypeID , StartDate , EndDate , IsMonday , IsTuesday , IsWednesday , IsThuresday , IsFriday , IsSaturday , IsSunday , IsMondayTime , IsTuesdayTime , IsWednesdayTime , IsThuresdayTime , IsFridayTime , IsSaturdayTime , IsSundayTime , MondayStartTime , MondayEndTime , TuesdayStartTime , TuesdayEndTime , WednesdayStartTime , WednesdayEndTime , ThuresdayStartTime , ThuresdayEndTime , FridayStartTime , FridayEndTime , SaturdayStartTime , SaturdayEndTime , SundayStartTime , SundayEndTime , PaymentMethodID , IsProvider , IsAllLocations , IsAllType , IsValueRange , MinimumValue , MaximumValue , DiscountValue , DiscountPercentage , Points , Remark , DisplayMessage , CashierMessage , IsDelete , GroupOfCompanyID , CreatedUser , CreatedDate , ModifiedUser , ModifiedDate , DataTransfer from InvPromotionMaster where datatransfer=1").ToDataTable();
            
            DataTable dt = new DataTable();
            var query = from d in context.InvPromotionMasters where d.DataTransfer.Equals(1) select new { d.InvPromotionMasterID, d.CompanyID, d.LocationID, d.CostCentreID, d.PromotionCode, d.PromotionName, d.IsAutoApply, d.PromotionTypeID, d.StartDate, d.EndDate, d.IsMonday, d.IsTuesday, d.IsWednesday, d.IsThuresday, d.IsFriday, d.IsSaturday, d.IsSunday, d.IsMondayTime, d.IsTuesdayTime, d.IsWednesdayTime, d.IsThuresdayTime, d.IsFridayTime, d.IsSaturdayTime, d.IsSundayTime, d.MondayStartTime, d.MondayEndTime, d.TuesdayStartTime, d.TuesdayEndTime, d.WednesdayStartTime, d.WednesdayEndTime, d.ThuresdayStartTime, d.ThuresdayEndTime, d.FridayStartTime, d.FridayEndTime, d.SaturdayStartTime, d.SaturdayEndTime, d.SundayStartTime, d.SundayEndTime, d.PaymentMethodID, d.IsProvider, d.IsAllLocations, d.IsAllType, d.IsValueRange, d.MinimumValue, d.MaximumValue, d.DiscountValue, d.DiscountPercentage, d.Points, d.Remark, d.DisplayMessage, d.CashierMessage, d.IsDelete, d.GroupOfCompanyID, d.CreatedUser, d.CreatedDate, d.ModifiedUser, d.ModifiedDate, d.DataTransfer,d.IsRaffle,d.IsIncreseQty};
            return dt = query.ToDataTable();


        }

        public DataTable GetAllDTInvPromotionDetailsSubTotal()
        {
            var query = from d in context.InvPromotionDetailsSubTotal where d.DataTransfer.Equals(1) select new { d.InvPromotionDetailsSubTotalID, d.InvPromotionMasterID, d.CompanyID, d.LocationID, d.Points, d.DiscountPercentage, d.DiscountAmount, d.GroupOfCompanyID, d.CreatedUser, d.CreatedDate, d.ModifiedUser, d.ModifiedDate, d.DataTransfer };
            return query.ToDataTable();
        }

        public DataTable GetAllDTInvPromotionDetailsGetYDetails()
        {
            var query = from d in context.InvPromotionDetailsGetYDetails
                        where d.DataTransfer.Equals(1)
                        select new
                        {
                            d.InvPromotionDetailsGetYDetailsID,
                            d.InvPromotionMasterID,
                            d.CompanyID,
                            d.LocationID,
                            d.GetProductID,
                            d.GetUnitOfMeasureID,
                            d.GetRate,
                            d.GetQty,
                            d.GetPoints,
                            d.GetDiscountPercentage,
                            d.GetDiscountAmount,
                            d.GroupOfCompanyID,
                            d.CreatedUser,
                            d.CreatedDate,
                            d.ModifiedUser,
                            d.ModifiedDate,
                            d.DataTransfer
                        };
            return query.ToDataTable();
        }

        public DataTable GetAllDTInvPromotionDetailsSubCategoryDis()
        {
            var query = from d in context.InvPromotionDetailsSubCategoryDis where d.DataTransfer.Equals(1) select new { d.InvPromotionDetailsSubCategoryDisID, d.InvPromotionMasterID, d.CompanyID, d.LocationID, d.SubCategoryID, d.Qty, d.Points, d.DiscountPercentage, d.DiscountAmount, d.GroupOfCompanyID, d.CreatedUser, d.CreatedDate, d.ModifiedUser, d.ModifiedDate, d.DataTransfer };
            return query.ToDataTable();
        }

        public DataTable GetAllDTInvPromotionDetailsProductDis()
        {
            var query = from d in context.InvPromotionDetailsProductDis where d.DataTransfer.Equals(1) select new { d.InvPromotionDetailsProductDisID, d.InvPromotionMasterID, d.CompanyID, d.LocationID, d.ProductID, d.UnitOfMeasureID, d.Rate, d.Qty, d.Points, d.DiscountPercentage, d.DiscountAmount, d.GroupOfCompanyID, d.CreatedUser, d.CreatedDate, d.ModifiedUser, d.ModifiedDate, d.DataTransfer };
            return query.ToDataTable();
        }

        //public DataTable GetAllDTInvPromotionDetailsGetYDetails()
        //{
        //    var query = from d in context.InvPromotionDetailsGetYDetails where d.DataTransfer.Equals(1) select new { d.InvPromotionDetailsGetYDetailsID, d.InvPromotionMasterID, d.CompanyID, d.LocationID, d.GetProductID, d.GetUnitOfMeasureID, d.GetRate, d.GetQty, d.GetPoints, d.GetDiscountPercentage, d.GetDiscountAmount, d.GroupOfCompanyID, d.CreatedUser, d.CreatedDate, d.ModifiedUser, d.ModifiedDate, d.DataTransfer };
        //    return query.ToDataTable();
        //}

        public DataTable GetAllDTInvPromotionDetailsDepartmentDis()
        {
            var query = from d in context.InvPromotionDetailsDepartmentDis where d.DataTransfer.Equals(1) select new { d.InvPromotionDetailsDepartmentDisID, d.InvPromotionMasterID, d.CompanyID, d.LocationID, d.DepartmentID, d.Qty, d.Points, d.DiscountPercentage, d.DiscountAmount, d.GroupOfCompanyID, d.CreatedUser, d.CreatedDate, d.ModifiedUser, d.ModifiedDate, d.DataTransfer };
            return query.ToDataTable();
        }

        public DataTable GetAllDTInvPromotionDetailsCategoryDis()
        {
            var query = from d in context.InvPromotionDetailsCategoryDis
                        where d.DataTransfer.Equals(1)
                        select new
                        {
                            d.InvPromotionDetailsCategoryDisID,
                            d.InvPromotionMasterID,
                            d.CompanyID,
                            d.LocationID,
                            d.CategoryID,
                            d.Qty,
                            d.Points,
                            d.DiscountPercentage,
                            d.DiscountAmount,
                            d.GroupOfCompanyID,
                            d.CreatedUser,
                            d.CreatedDate,
                            d.ModifiedUser,
                            d.ModifiedDate,
                            d.DataTransfer
                        };
            return query.ToDataTable();
        }


        public DataTable GetAllDTInvPromotionDetailsBuyXSubCategory()
        {
            var query = from d in context.InvPromotionDetailsBuyXSubCategory
                        where d.DataTransfer.Equals(1)
                        select new
                        {
                            d.InvPromotionDetailsBuyXSubCategoryID,
                            d.InvPromotionMasterID,
                            d.CompanyID,
                            d.LocationID,
                            d.BuySubCategoryID,
                            d.BuyQty,
                            d.GroupOfCompanyID,
                            d.CreatedUser,
                            d.CreatedDate,
                            d.ModifiedUser,
                            d.ModifiedDate,
                            d.DataTransfer
                        };
            return query.ToDataTable();
        }


        public DataTable GetAllDTInvPromotionDetailsBuyXProduct()
        {
            var query = from d in context.InvPromotionDetailsBuyXProduct
                        where d.DataTransfer.Equals(1)
                        select new
                        {
                            d.InvPromotionDetailsBuyXProductID,
                            d.InvPromotionMasterID,
                            d.CompanyID,
                            d.LocationID,
                            d.BuyProductID,
                            d.BuyUnitOfMeasureID,
                            d.BuyRate,
                            d.BuyQty,
                            d.GroupOfCompanyID,
                            d.CreatedUser,
                            d.CreatedDate,
                            d.ModifiedUser,
                            d.ModifiedDate,
                            d.DataTransfer
                        };
            return query.ToDataTable();
        }

        public DataTable GetAllDTInvPromotionDetailsBuyXDepartment()
        {
            var query = from d in context.InvPromotionDetailsBuyXDepartment
                        where d.DataTransfer.Equals(1)
                        select new
                        {
                            d.InvPromotionDetailsBuyXDepartmentID,
                            d.InvPromotionMasterID,
                            d.CompanyID,
                            d.LocationID,
                            d.BuyDepartmentID,
                            d.BuyQty,
                            d.GroupOfCompanyID,
                            d.CreatedUser,
                            d.CreatedDate,
                            d.ModifiedUser,
                            d.ModifiedDate,
                            d.DataTransfer
                        };
            return query.ToDataTable();
        }

        public DataTable GetAllDTInvPromotionDetailsBuyXCategory()
        {
            var query = from d in context.InvPromotionDetailsBuyXCategory
                        where d.DataTransfer.Equals(1)
                        select new
                        {
                            d.InvPromotionDetailsBuyXCategoryID,
                            d.InvPromotionMasterID,
                            d.CompanyID,
                            d.LocationID,
                            d.BuyCategoryID,
                            d.BuyQty,
                            d.GroupOfCompanyID,
                            d.CreatedUser,
                            d.CreatedDate,
                            d.ModifiedUser,
                            d.ModifiedDate,
                            d.DataTransfer
                        };
            return query.ToDataTable();
        }

        public DataTable GetAllDTInvPromotionDetailsAllowTypes()
        {
            var query = from d in context.InvPromotionDetailsAllowTypes
                        where d.DataTransfer.Equals(1)
                        select new
                        {
                            d.InvPromotionDetailsAllowTypesID,
                            d.InvPromotionMasterID,
                            d.CompanyID,
                            d.LocationID,
                            d.TypeID,
                            d.GroupOfCompanyID,
                            d.CreatedUser,
                            d.CreatedDate,
                            d.ModifiedUser,
                            d.ModifiedDate,
                            d.DataTransfer
                        };
            return query.ToDataTable();
        }

        public DataTable GetAllDTInvPromotionDetailsAllowLocations()
        {
            var query = from d in context.InvPromotionDetailsAllowLocations
                        where d.DataTransfer.Equals(1)
                        select new
                        {
                            d.InvPromotionDetailsAllowLocationsID,
                            d.InvPromotionMasterID,
                            d.CompanyID,
                            d.LocationID,
                            d.PromotionLocationID,
                            d.GroupOfCompanyID,
                            d.CreatedUser,
                            d.CreatedDate,
                            d.ModifiedUser,
                            d.ModifiedDate,
                            d.DataTransfer
                        };
            return query.ToDataTable();
        }

        public DataTable GetAllDTInvPromotionDetailsBuyXSubCategory2s()
        {
            var query = from d in context.InvPromotionDetailsBuyXSubCategory2
                        where d.DataTransfer.Equals(1)
                        select new
                        {
                            d.InvPromotionDetailsBuyXSubCategory2ID, 
                            d.InvPromotionMasterID, 
                            d.CompanyID,
                            d.LocationID,
                            d.BuySubCategory2ID, 
                            d.BuyQty, 
                            d.GroupOfCompanyID, 
                            d.CreatedUser,
                            d.CreatedDate, 
                            d.ModifiedUser, 
                            d.ModifiedDate, 
                            d.DataTransfer  
                        };
            return query.ToDataTable();
        }

        public DataTable GetAllDTInvPromotionType()
        {
            var query = from d in context.InvPromotionTypes
                        where d.DataTransfer.Equals(1)
                        select new
                        {
                            d.InvPromotionTypeID,
                            d.PromotionTypeCode,
                            d.PromotionTypeName,
                            d.Remark,
                            d.IsDelete,
                            d.GroupOfCompanyID,
                            d.CreatedUser,
                            d.CreatedDate,
                            d.ModifiedUser,
                            d.ModifiedDate,
                            d.DataTransfer
                        };
            return query.ToDataTable();
        }

        public DataTable GetAllDTInvPromotionDetailsSubCategory2Dis()
        {
            var query = from d in context.InvPromotionDetailsSubCategory2Dis
                        where d.DataTransfer.Equals(1)
                        select new
                        {
                            d.InvPromotionDetailsSubCategory2DisID,
                            d.InvPromotionMasterID,
                            d.CompanyID,
                            d.LocationID,
                            d.SubCategory2ID,
                            d.Qty,
                            d.Points,
                            d.DiscountPercentage,
                            d.DiscountAmount,
                            d.GroupOfCompanyID,
                            d.CreatedUser,
                            d.CreatedDate,
                            d.ModifiedUser,
                            d.ModifiedDate,
                            d.DataTransfer
                        };
            return query.ToDataTable();
        }

        public List<InvPromotionDetailsBuyXProduct> getUpdatePromotionDetailBuyXTemp(List<InvPromotionDetailsBuyXProduct> invPromotionBuyXTempDetailsList, InvPromotionDetailsBuyXProduct invBuyXPromotionDetail)
        {
            InvPromotionDetailsBuyXProduct invPromotionBuyXTemp = new InvPromotionDetailsBuyXProduct();

            invPromotionBuyXTemp = invPromotionBuyXTempDetailsList.Where(p => p.BuyProductID == invBuyXPromotionDetail.BuyProductID).FirstOrDefault();

            if (invPromotionBuyXTemp != null)
            {
                invPromotionBuyXTempDetailsList.Remove(invPromotionBuyXTemp);
            }

            invPromotionBuyXTempDetailsList.Add(invBuyXPromotionDetail);            

            return invPromotionBuyXTempDetailsList.ToList();
        }

        public List<InvPromotionDetailsGetYDetails> getUpdatePromotionDetailGetYTemp(List<InvPromotionDetailsGetYDetails> invPromotionGetYTempDetailsList, InvPromotionDetailsGetYDetails invGetYPromotionDetail)
        {
            InvPromotionDetailsGetYDetails invPromotionGetYTemp = new InvPromotionDetailsGetYDetails();

            invPromotionGetYTemp = invPromotionGetYTempDetailsList.Where(p => p.GetProductID == invGetYPromotionDetail.GetProductID).FirstOrDefault();

            if (invPromotionGetYTemp != null)
            {
                invPromotionGetYTempDetailsList.Remove(invPromotionGetYTemp);
            }

           invPromotionGetYTempDetailsList.Add(invGetYPromotionDetail);

           return invPromotionGetYTempDetailsList.ToList();
        }

        public List<InvPromotionDetailsProductDis> getUpdatePromotionDetailProductDiscountTemp(List<InvPromotionDetailsProductDis> invPromotionProductDiscountTempDetailsList, InvPromotionDetailsProductDis invProductDiscountPromotionDetail)
        {
            InvPromotionDetailsProductDis invPromotionProductDiscountTemp = new InvPromotionDetailsProductDis();

            invPromotionProductDiscountTemp = invPromotionProductDiscountTempDetailsList.Where(p => p.ProductID == invProductDiscountPromotionDetail.ProductID).FirstOrDefault();

            if (invPromotionProductDiscountTemp != null)
            {
                invPromotionProductDiscountTempDetailsList.Remove(invPromotionProductDiscountTemp);
            }

            invPromotionProductDiscountTempDetailsList.Add(invProductDiscountPromotionDetail);

            return invPromotionProductDiscountTempDetailsList.ToList();
        }

        public List<InvPromotionDetailsDepartmentDis> getUpdatePromotionDetailDepartmentTemp(List<InvPromotionDetailsDepartmentDis> invPromotionDepartmentTempDetailsList, InvPromotionDetailsDepartmentDis invDepartmentPromotionDetail)
        {
            InvPromotionDetailsDepartmentDis invPromotionDepartmentTemp = new InvPromotionDetailsDepartmentDis();

            invPromotionDepartmentTemp = invPromotionDepartmentTempDetailsList.Where(p => p.DepartmentID == invDepartmentPromotionDetail.DepartmentID).FirstOrDefault();

            if (invPromotionDepartmentTemp != null)
            {
                invPromotionDepartmentTempDetailsList.Remove(invPromotionDepartmentTemp);
            }

            invPromotionDepartmentTempDetailsList.Add(invDepartmentPromotionDetail);

            return invPromotionDepartmentTempDetailsList.ToList();
        }

        public List<InvPromotionDetailsBuyXDepartment> getUpdatePromotionDetailBuyXDepartmentTemp(List<InvPromotionDetailsBuyXDepartment> invPromotionDepartmentTempDetailsList, InvPromotionDetailsBuyXDepartment invDepartmentBuyXPromotionDetail)
        {
            InvPromotionDetailsBuyXDepartment invPromotionDepartmentBuyXTemp = new InvPromotionDetailsBuyXDepartment();

            invPromotionDepartmentBuyXTemp = invPromotionDepartmentTempDetailsList.Where(p => p.BuyDepartmentID == invDepartmentBuyXPromotionDetail.BuyDepartmentID).FirstOrDefault();

            if (invPromotionDepartmentBuyXTemp != null)
            {
                invPromotionDepartmentTempDetailsList.Remove(invPromotionDepartmentBuyXTemp);
            }

            invPromotionDepartmentTempDetailsList.Add(invDepartmentBuyXPromotionDetail);

            return invPromotionDepartmentTempDetailsList.ToList();
        }

        public List<InvPromotionDetailsCategoryDis> getUpdatePromotionDetailCategoryTemp(List<InvPromotionDetailsCategoryDis> invPromotionCategoryTempDetailsList, InvPromotionDetailsCategoryDis invCategoryPromotionDetail)
        {
            InvPromotionDetailsCategoryDis invPromotionCategoryTemp = new InvPromotionDetailsCategoryDis();

            invPromotionCategoryTemp = invPromotionCategoryTempDetailsList.Where(p => p.CategoryID == invCategoryPromotionDetail.CategoryID).FirstOrDefault();

            if (invPromotionCategoryTemp != null)
            {
                invPromotionCategoryTempDetailsList.Remove(invPromotionCategoryTemp);
            }

            invPromotionCategoryTempDetailsList.Add(invCategoryPromotionDetail);

            return invPromotionCategoryTempDetailsList.ToList();
        }

        public List<InvPromotionDetailsBuyXCategory> getUpdatePromotionBuyXDetailCategoryTemp(List<InvPromotionDetailsBuyXCategory> invPromotionCategoryBuyXTempDetailsList, InvPromotionDetailsBuyXCategory invCategoryBuyXPromotionDetail)
        {
            InvPromotionDetailsBuyXCategory invPromotionBuyXCategoryTemp = new InvPromotionDetailsBuyXCategory();

            invPromotionBuyXCategoryTemp = invPromotionCategoryBuyXTempDetailsList.Where(p => p.BuyCategoryID == invCategoryBuyXPromotionDetail.BuyCategoryID).FirstOrDefault();

            if (invPromotionBuyXCategoryTemp != null)
            {
                invPromotionCategoryBuyXTempDetailsList.Remove(invPromotionBuyXCategoryTemp);
            }

            invPromotionCategoryBuyXTempDetailsList.Add(invCategoryBuyXPromotionDetail);

            return invPromotionCategoryBuyXTempDetailsList.ToList();
        }



        public List<InvPromotionDetailsSubCategoryDis> getUpdatePromotionDetailSubCategoryTemp(List<InvPromotionDetailsSubCategoryDis> invPromotionSubCategoryTempDetailsList, InvPromotionDetailsSubCategoryDis invSubCategoryPromotionDetail)
        {
            InvPromotionDetailsSubCategoryDis invPromotionSubCategoryTemp = new InvPromotionDetailsSubCategoryDis();

            invPromotionSubCategoryTemp = invPromotionSubCategoryTempDetailsList.Where(p => p.SubCategoryID == invSubCategoryPromotionDetail.SubCategoryID).FirstOrDefault();

            if (invPromotionSubCategoryTemp != null)
            {
                invPromotionSubCategoryTempDetailsList.Remove(invPromotionSubCategoryTemp);
            }

            invPromotionSubCategoryTempDetailsList.Add(invSubCategoryPromotionDetail);

            return invPromotionSubCategoryTempDetailsList.ToList();
        }


        public List<InvPromotionDetailsSubCategory2Dis> getUpdatePromotionDetailSubCategory2Temp(List<InvPromotionDetailsSubCategory2Dis> invPromotionSubCategory2TempDetailsList, InvPromotionDetailsSubCategory2Dis invSubCategory2PromotionDetail)
        {
            InvPromotionDetailsSubCategory2Dis invPromotionSubCategory2Temp = new InvPromotionDetailsSubCategory2Dis();

            invPromotionSubCategory2Temp = invPromotionSubCategory2TempDetailsList.Where(p => p.SubCategory2ID == invSubCategory2PromotionDetail.SubCategory2ID).FirstOrDefault();

            if (invPromotionSubCategory2Temp != null)
            {
                invPromotionSubCategory2TempDetailsList.Remove(invPromotionSubCategory2Temp);
            }

            invPromotionSubCategory2TempDetailsList.Add(invSubCategory2PromotionDetail);

            return invPromotionSubCategory2TempDetailsList.ToList();
        }


        public List<InvPromotionDetailsBuyXSubCategory> getUpdatePromotionBuyXDetailSubCategoryTemp(List<InvPromotionDetailsBuyXSubCategory> invPromotionSubCategoryBuyXTempDetailsList, InvPromotionDetailsBuyXSubCategory invSubCategoryBuyXPromotionDetail)
        {
            InvPromotionDetailsBuyXSubCategory invPromotionBuyXSubCategoryTemp = new InvPromotionDetailsBuyXSubCategory();

            invPromotionBuyXSubCategoryTemp = invPromotionSubCategoryBuyXTempDetailsList.Where(p => p.BuySubCategoryID == invSubCategoryBuyXPromotionDetail.BuySubCategoryID).FirstOrDefault();

            if (invPromotionBuyXSubCategoryTemp != null)
            {
                invPromotionSubCategoryBuyXTempDetailsList.Remove(invPromotionBuyXSubCategoryTemp);
            }

            invPromotionSubCategoryBuyXTempDetailsList.Add(invSubCategoryBuyXPromotionDetail);

            return invPromotionSubCategoryBuyXTempDetailsList.ToList();
        }

        public List<InvPromotionDetailsBuyXSubCategory2> getUpdatePromotionBuyXDetailSubCategory2Temp(List<InvPromotionDetailsBuyXSubCategory2> invPromotionSubCategory2BuyXTempDetailsList, InvPromotionDetailsBuyXSubCategory2 invSubCategory2BuyXPromotionDetail)
        {
            InvPromotionDetailsBuyXSubCategory2 invPromotionBuyXSubCategory2Temp = new InvPromotionDetailsBuyXSubCategory2();

            invPromotionBuyXSubCategory2Temp = invPromotionSubCategory2BuyXTempDetailsList.Where(p => p.BuySubCategory2ID == invSubCategory2BuyXPromotionDetail.BuySubCategory2ID).FirstOrDefault();

            if (invPromotionBuyXSubCategory2Temp != null)
            {
                invPromotionSubCategory2BuyXTempDetailsList.Remove(invPromotionBuyXSubCategory2Temp);
            }

            invPromotionSubCategory2BuyXTempDetailsList.Add(invSubCategory2BuyXPromotionDetail);

            return invPromotionSubCategory2BuyXTempDetailsList.ToList();
        }

               

        public List<InvPromotionType> GetAllPromotionTypes()
        {
            return context.InvPromotionTypes.Where(c => c.IsDelete == false).ToList();

        }

        public InvPromotionMaster getPromotionHeaderByPromotionCode(string promotionCode, int locationID)
        {
            return context.InvPromotionMasters.Where(ph => ph.PromotionCode.Equals(promotionCode) && ph.IsDelete.Equals(false) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public InvPromotionMaster getPromotionHeaderByPromotionName(string promotionName, int locationID)
        {
            return context.InvPromotionMasters.Where(ph => ph.PromotionName.Equals(promotionName) && ph.IsDelete.Equals(false) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public List<InvPromotionDetailsBuyXProduct> getBuyXProductDetail(InvPromotionMaster invPromotionMaster)
        {
            InvPromotionDetailsBuyXProduct invPromotionDetailsBuyXProduct;

            List<InvPromotionDetailsBuyXProduct> invPromotionDetailsBuyXProductList = new List<InvPromotionDetailsBuyXProduct>();


            var buyXProductDetails = (from pm in context.InvPromotionMasters
                                      join pd in context.InvPromotionDetailsBuyXProduct on pm.InvPromotionMasterID equals pd.InvPromotionMasterID
                                      join dc in context.InvProductMasters on pd.BuyProductID equals dc.InvProductMasterID
                                      join uc in context.UnitOfMeasures on pd.BuyUnitOfMeasureID equals uc.UnitOfMeasureID
                                   where pm.LocationID.Equals(invPromotionMaster.LocationID) && pm.CompanyID.Equals(invPromotionMaster.CompanyID)
                                   && pm.InvPromotionMasterID.Equals(invPromotionMaster.InvPromotionMasterID)
                                   select new
                                   {
                                       pd.BuyProductID,
                                       dc.ProductCode,
                                       dc.ProductName,
                                       uc.UnitOfMeasureName,
                                       pd.BuyQty,
                                       pd.BuyUnitOfMeasureID,
                                       pd.BuyRate

                                   }).ToArray();

            foreach (var tempProduct in buyXProductDetails)
            {

                invPromotionDetailsBuyXProduct = new InvPromotionDetailsBuyXProduct();


                invPromotionDetailsBuyXProduct.BuyProductCode = tempProduct.ProductCode;
                invPromotionDetailsBuyXProduct.BuyProductID = tempProduct.BuyProductID;
                invPromotionDetailsBuyXProduct.BuyProductName = tempProduct.ProductName;
                invPromotionDetailsBuyXProduct.BuyQty = tempProduct.BuyQty;
                invPromotionDetailsBuyXProduct.BuyRate = tempProduct.BuyRate;
                invPromotionDetailsBuyXProduct.BuyUnitOfMeasureID = tempProduct.BuyUnitOfMeasureID;
                invPromotionDetailsBuyXProduct.UnitOfMeasure = tempProduct.UnitOfMeasureName;

                invPromotionDetailsBuyXProductList.Add(invPromotionDetailsBuyXProduct);

            }


            return invPromotionDetailsBuyXProductList.OrderBy(pd => pd.BuyProductID).ToList();
        }


        public List<InvPromotionDetailsGetYDetails> getGetYProductDetail(InvPromotionMaster invPromotionMaster)
        {
            InvPromotionDetailsGetYDetails invPromotionDetailsGetYProduct;

            List<InvPromotionDetailsGetYDetails> invPromotionDetailsGetYProductList = new List<InvPromotionDetailsGetYDetails>();


            var getYProductDetails = (from pm in context.InvPromotionMasters
                                      join pd in context.InvPromotionDetailsGetYDetails on pm.InvPromotionMasterID equals pd.InvPromotionMasterID
                                      join dc in context.InvProductMasters on pd.GetProductID equals dc.InvProductMasterID
                                      join uc in context.UnitOfMeasures on pd.GetUnitOfMeasureID equals uc.UnitOfMeasureID
                                      where pm.LocationID.Equals(invPromotionMaster.LocationID) && pm.CompanyID.Equals(invPromotionMaster.CompanyID)
                                      && pm.InvPromotionMasterID.Equals(invPromotionMaster.InvPromotionMasterID)
                                      select new
                                      {
                                          pd.GetProductID,
                                          dc.ProductCode,
                                          dc.ProductName,
                                          uc.UnitOfMeasureName,
                                          pd.GetQty,
                                          pd.GetUnitOfMeasureID,
                                          pd.GetRate,
                                          pd.GetDiscountPercentage,
                                          pd.GetDiscountAmount,
                                          pd.GetPoints

                                      }).ToArray();

            foreach (var tempProduct in getYProductDetails)
            {

                invPromotionDetailsGetYProduct = new InvPromotionDetailsGetYDetails();

                invPromotionDetailsGetYProduct.ProductCode = tempProduct.ProductCode;
                invPromotionDetailsGetYProduct.GetProductID = tempProduct.GetProductID;
                invPromotionDetailsGetYProduct.ProductName = tempProduct.ProductName;
                invPromotionDetailsGetYProduct.GetQty = tempProduct.GetQty;
                invPromotionDetailsGetYProduct.GetRate = tempProduct.GetRate;
                invPromotionDetailsGetYProduct.GetUnitOfMeasureID = tempProduct.GetUnitOfMeasureID;
                invPromotionDetailsGetYProduct.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                invPromotionDetailsGetYProduct.GetPoints = tempProduct.GetPoints;
                invPromotionDetailsGetYProduct.GetDiscountAmount = tempProduct.GetDiscountAmount;
                invPromotionDetailsGetYProduct.GetDiscountPercentage = tempProduct.GetDiscountPercentage;

                invPromotionDetailsGetYProductList.Add(invPromotionDetailsGetYProduct);

            }


            return invPromotionDetailsGetYProductList.OrderBy(pd => pd.GetProductID).ToList();
        }

        public List<InvPromotionDetailsBuyXDepartment> BuyDepartmentDetail(InvPromotionMaster invPromotionMaster)
        {
            InvPromotionDetailsBuyXDepartment invPromotionDetailsBuyDepartment;

            List<InvPromotionDetailsBuyXDepartment> invPromotionDetailsBuyDepartmentList = new List<InvPromotionDetailsBuyXDepartment>();


            var buyDepartmentDetails = (from pm in context.InvPromotionMasters
                                      join pd in context.InvPromotionDetailsBuyXDepartment on pm.InvPromotionMasterID equals pd.InvPromotionMasterID
                                      join dc in context.InvDepartments on pd.BuyDepartmentID equals dc.InvDepartmentID
                                      where pm.LocationID.Equals(invPromotionMaster.LocationID) && pm.CompanyID.Equals(invPromotionMaster.CompanyID)
                                      && pm.InvPromotionMasterID.Equals(invPromotionMaster.InvPromotionMasterID)
                                      select new
                                      {
                                          pd.BuyDepartmentID,
                                          pd.BuyQty,
                                          dc.DepartmentCode,
                                          dc.DepartmentName

                                      }).ToArray();

            foreach (var tempProduct in buyDepartmentDetails)
            {

                invPromotionDetailsBuyDepartment = new InvPromotionDetailsBuyXDepartment();

                invPromotionDetailsBuyDepartment.BuyDepartmentID = tempProduct.BuyDepartmentID;
                invPromotionDetailsBuyDepartment.DepartmentCode = tempProduct.DepartmentCode;
                invPromotionDetailsBuyDepartment.DepartmentName = tempProduct.DepartmentName;
                invPromotionDetailsBuyDepartment.BuyQty = tempProduct.BuyQty;

                invPromotionDetailsBuyDepartmentList.Add(invPromotionDetailsBuyDepartment);

            }


            return invPromotionDetailsBuyDepartmentList.OrderBy(pd => pd.BuyDepartmentID).ToList();
        }


        public List<InvPromotionDetailsBuyXCategory> BuyCategoryDetail(InvPromotionMaster invPromotionMaster)
        {
            InvPromotionDetailsBuyXCategory invPromotionDetailsBuyCategory;

            List<InvPromotionDetailsBuyXCategory> invPromotionDetailsBuyCategoryList = new List<InvPromotionDetailsBuyXCategory>();


            var buyCategoryDetails = (from pm in context.InvPromotionMasters
                                        join pd in context.InvPromotionDetailsBuyXCategory on pm.InvPromotionMasterID equals pd.InvPromotionMasterID
                                        join dc in context.InvCategories on pd.BuyCategoryID equals dc.InvCategoryID
                                        where pm.LocationID.Equals(invPromotionMaster.LocationID) && pm.CompanyID.Equals(invPromotionMaster.CompanyID)
                                        && pm.InvPromotionMasterID.Equals(invPromotionMaster.InvPromotionMasterID)
                                        select new
                                        {
                                            pd.BuyCategoryID,
                                            pd.BuyQty,
                                            dc.CategoryCode,
                                            dc.CategoryName

                                        }).ToArray();

            foreach (var tempProduct in buyCategoryDetails)
            {

                invPromotionDetailsBuyCategory = new InvPromotionDetailsBuyXCategory();

                invPromotionDetailsBuyCategory.BuyCategoryID = tempProduct.BuyCategoryID;
                invPromotionDetailsBuyCategory.CategoryCode = tempProduct.CategoryCode;
                invPromotionDetailsBuyCategory.CategoryName = tempProduct.CategoryName;
                invPromotionDetailsBuyCategory.BuyQty = tempProduct.BuyQty;

                invPromotionDetailsBuyCategoryList.Add(invPromotionDetailsBuyCategory);

            }


            return invPromotionDetailsBuyCategoryList.OrderBy(pd => pd.BuyCategoryID).ToList();
        }

        public List<InvPromotionDetailsBuyXSubCategory> BuySubCategoryDetail(InvPromotionMaster invPromotionMaster)
        {
            InvPromotionDetailsBuyXSubCategory invPromotionDetailsBuySubCategory;

            List<InvPromotionDetailsBuyXSubCategory> invPromotionDetailsBuySubCategoryList = new List<InvPromotionDetailsBuyXSubCategory>();


            var buySubCategoryDetails = (from pm in context.InvPromotionMasters
                                      join pd in context.InvPromotionDetailsBuyXSubCategory on pm.InvPromotionMasterID equals pd.InvPromotionMasterID
                                      join dc in context.InvSubCategories on pd.BuySubCategoryID equals dc.InvSubCategoryID
                                      where pm.LocationID.Equals(invPromotionMaster.LocationID) && pm.CompanyID.Equals(invPromotionMaster.CompanyID)
                                      && pm.InvPromotionMasterID.Equals(invPromotionMaster.InvPromotionMasterID)
                                      select new
                                      {
                                          pd.BuySubCategoryID,
                                          pd.BuyQty,
                                          dc.SubCategoryCode,
                                          dc.SubCategoryName

                                      }).ToArray();

            foreach (var tempProduct in buySubCategoryDetails)
            {

                invPromotionDetailsBuySubCategory = new InvPromotionDetailsBuyXSubCategory();

                invPromotionDetailsBuySubCategory.BuySubCategoryID = tempProduct.BuySubCategoryID;
                invPromotionDetailsBuySubCategory.SubCategoryCode = tempProduct.SubCategoryCode;
                invPromotionDetailsBuySubCategory.SubCategoryName = tempProduct.SubCategoryName;
                invPromotionDetailsBuySubCategory.BuyQty = tempProduct.BuyQty;

                invPromotionDetailsBuySubCategoryList.Add(invPromotionDetailsBuySubCategory);

            }


            return invPromotionDetailsBuySubCategoryList.OrderBy(pd => pd.BuySubCategoryID).ToList();
        }

        public List<InvPromotionDetailsBuyXSubCategory2> BuySubCategory2Detail(InvPromotionMaster invPromotionMaster)
        {
            InvPromotionDetailsBuyXSubCategory2 invPromotionDetailsBuySubCategory2;

            List<InvPromotionDetailsBuyXSubCategory2> invPromotionDetailsBuySubCategory2List = new List<InvPromotionDetailsBuyXSubCategory2>();


            var buySubCategoryDetails = (from pm in context.InvPromotionMasters
                                         join pd in context.InvPromotionDetailsBuyXSubCategory2 on pm.InvPromotionMasterID equals pd.InvPromotionMasterID
                                         join dc in context.InvSubCategories2 on pd.BuySubCategory2ID equals dc.InvSubCategory2ID
                                         where pm.LocationID.Equals(invPromotionMaster.LocationID) && pm.CompanyID.Equals(invPromotionMaster.CompanyID)
                                         && pm.InvPromotionMasterID.Equals(invPromotionMaster.InvPromotionMasterID)
                                         select new
                                         {
                                             pd.BuySubCategory2ID,
                                             pd.BuyQty,
                                             dc.SubCategory2Code,
                                             dc.SubCategory2Name

                                         }).ToArray();

            foreach (var tempProduct in buySubCategoryDetails)
            {

                invPromotionDetailsBuySubCategory2 = new InvPromotionDetailsBuyXSubCategory2();

                invPromotionDetailsBuySubCategory2.BuySubCategory2ID = tempProduct.BuySubCategory2ID;
                invPromotionDetailsBuySubCategory2.SubCategory2Code = tempProduct.SubCategory2Code;
                invPromotionDetailsBuySubCategory2.SubCategory2Name = tempProduct.SubCategory2Name;
                invPromotionDetailsBuySubCategory2.BuyQty = tempProduct.BuyQty;

                invPromotionDetailsBuySubCategory2List.Add(invPromotionDetailsBuySubCategory2);

            }


            return invPromotionDetailsBuySubCategory2List.OrderBy(pd => pd.BuySubCategory2ID).ToList();
        }



        public List<InvPromotionDetailsGetYDetails> GetSubCategory2Detail(InvPromotionMaster invPromotionMaster)
        {
            InvPromotionDetailsGetYDetails invPromotionDetailsGetYProduct;

            List<InvPromotionDetailsGetYDetails> invPromotionDetailsGetYProductList = new List<InvPromotionDetailsGetYDetails>();


            var getYProductDetails = (from pm in context.InvPromotionMasters
                                      join pd in context.InvPromotionDetailsGetYDetails on pm.InvPromotionMasterID equals pd.InvPromotionMasterID
                                      join dc in context.InvSubCategories2 on pd.GetProductID equals dc.InvSubCategory2ID
                                      where pm.LocationID.Equals(invPromotionMaster.LocationID) && pm.CompanyID.Equals(invPromotionMaster.CompanyID)
                                      && pm.InvPromotionMasterID.Equals(invPromotionMaster.InvPromotionMasterID)
                                      select new
                                      {
                                          pd.GetProductID,
                                          dc.SubCategory2Code,
                                          dc.SubCategory2Name,
                                          pd.GetQty,
                                          pd.GetUnitOfMeasureID,
                                          pd.GetRate,
                                          pd.GetDiscountPercentage,
                                          pd.GetDiscountAmount,
                                          pd.GetPoints

                                      }).ToArray();

            foreach (var tempProduct in getYProductDetails)
            {

                invPromotionDetailsGetYProduct = new InvPromotionDetailsGetYDetails();

                invPromotionDetailsGetYProduct.ProductCode = tempProduct.SubCategory2Code;
                invPromotionDetailsGetYProduct.GetProductID = tempProduct.GetProductID;
                invPromotionDetailsGetYProduct.ProductName = tempProduct.SubCategory2Name;
                invPromotionDetailsGetYProduct.GetQty = tempProduct.GetQty;
                invPromotionDetailsGetYProduct.GetRate = tempProduct.GetRate;
                invPromotionDetailsGetYProduct.GetUnitOfMeasureID = tempProduct.GetUnitOfMeasureID;
                invPromotionDetailsGetYProduct.UnitOfMeasure = "";
                invPromotionDetailsGetYProduct.GetPoints = tempProduct.GetPoints;
                invPromotionDetailsGetYProduct.GetDiscountAmount = tempProduct.GetDiscountAmount;
                invPromotionDetailsGetYProduct.GetDiscountPercentage = tempProduct.GetDiscountPercentage;

                invPromotionDetailsGetYProductList.Add(invPromotionDetailsGetYProduct);

            }


            return invPromotionDetailsGetYProductList.OrderBy(pd => pd.GetProductID).ToList();
        }

        


        public List<InvPromotionDetailsProductDis> getProductDisDetail(InvPromotionMaster invPromotionMaster)
        {
            InvPromotionDetailsProductDis invPromotionDetailsProductDis;

            List<InvPromotionDetailsProductDis> invPromotionDetailsProductDisList = new List<InvPromotionDetailsProductDis>();


            var productDisDetails = (from pm in context.InvPromotionMasters
                                      join pd in context.InvPromotionDetailsProductDis on pm.InvPromotionMasterID equals pd.InvPromotionMasterID
                                      join dc in context.InvProductMasters on pd.ProductID equals dc.InvProductMasterID
                                      join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                      where pm.LocationID.Equals(invPromotionMaster.LocationID) && pm.CompanyID.Equals(invPromotionMaster.CompanyID)
                                      && pm.InvPromotionMasterID.Equals(invPromotionMaster.InvPromotionMasterID)
                                      select new
                                      {
                                          pd.ProductID,
                                          dc.ProductCode,
                                          dc.ProductName,
                                          uc.UnitOfMeasureName,
                                          pd.Qty,
                                          pd.UnitOfMeasureID,
                                          pd.Rate,
                                          pd.DiscountAmount,
                                          pd.DiscountPercentage,
                                          pd.Points

                                      }).ToArray();

            foreach (var tempProduct in productDisDetails)
            {

                invPromotionDetailsProductDis = new InvPromotionDetailsProductDis();


                invPromotionDetailsProductDis.ProductCode = tempProduct.ProductCode;
                invPromotionDetailsProductDis.ProductID = tempProduct.ProductID;
                invPromotionDetailsProductDis.ProductName = tempProduct.ProductName;
                invPromotionDetailsProductDis.Qty = tempProduct.Qty;
                invPromotionDetailsProductDis.Rate = tempProduct.Rate;
                invPromotionDetailsProductDis.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                invPromotionDetailsProductDis.UnitOfMeasure = tempProduct.UnitOfMeasureName;

                invPromotionDetailsProductDisList.Add(invPromotionDetailsProductDis);

            }


            return invPromotionDetailsProductDisList.OrderBy(pd => pd.ProductID).ToList();
        }


        public List<InvPromotionDetailsDepartmentDis> getGetDepartmentDisDetail(InvPromotionMaster invPromotionMaster)
        {
            InvPromotionDetailsDepartmentDis invPromotionDetailsDepartmentDiscount;

            List<InvPromotionDetailsDepartmentDis> invPromotionDetailsGetDepartmentDisList = new List<InvPromotionDetailsDepartmentDis>();


            var getDepartmentDisDetails = (from pm in context.InvPromotionMasters
                                      join pd in context.InvPromotionDetailsDepartmentDis on pm.InvPromotionMasterID equals pd.InvPromotionMasterID
                                      join dc in context.InvDepartments on pd.DepartmentID equals dc.InvDepartmentID
                                      where pm.LocationID.Equals(invPromotionMaster.LocationID) && pm.CompanyID.Equals(invPromotionMaster.CompanyID)
                                      && pm.InvPromotionMasterID.Equals(invPromotionMaster.InvPromotionMasterID)
                                      select new
                                      {
                                          pd.DepartmentID,
                                          dc.DepartmentCode,
                                          dc.DepartmentName,
                                          pd.Qty,
                                          pd.DiscountPercentage,
                                          pd.DiscountAmount,
                                          pd.Points

                                      }).ToArray();

            foreach (var tempProduct in getDepartmentDisDetails)
            {

                invPromotionDetailsDepartmentDiscount = new InvPromotionDetailsDepartmentDis();

                invPromotionDetailsDepartmentDiscount.DepartmentCode = tempProduct.DepartmentCode;
                invPromotionDetailsDepartmentDiscount.DepartmentID = tempProduct.DepartmentID;
                invPromotionDetailsDepartmentDiscount.DepartmentName = tempProduct.DepartmentName;
                invPromotionDetailsDepartmentDiscount.Qty = tempProduct.Qty;
                invPromotionDetailsDepartmentDiscount.Points = tempProduct.Points;
                invPromotionDetailsDepartmentDiscount.DiscountAmount = tempProduct.DiscountAmount;
                invPromotionDetailsDepartmentDiscount.DiscountPercentage = tempProduct.DiscountPercentage;

                invPromotionDetailsGetDepartmentDisList.Add(invPromotionDetailsDepartmentDiscount);

            }


            return invPromotionDetailsGetDepartmentDisList.OrderBy(pd => pd.DepartmentID).ToList();
        }

        public List<InvPromotionDetailsCategoryDis> getGetCategoryDisDetail(InvPromotionMaster invPromotionMaster)
        {
            InvPromotionDetailsCategoryDis invPromotionDetailsCategoryDiscount;

            List<InvPromotionDetailsCategoryDis> invPromotionDetailsGetCategoryDisList = new List<InvPromotionDetailsCategoryDis>();


            var getCategoryDisDetails = (from pm in context.InvPromotionMasters
                                           join pd in context.InvPromotionDetailsCategoryDis on pm.InvPromotionMasterID equals pd.InvPromotionMasterID
                                           join dc in context.InvCategories on pd.CategoryID equals dc.InvCategoryID
                                           where pm.LocationID.Equals(invPromotionMaster.LocationID) && pm.CompanyID.Equals(invPromotionMaster.CompanyID)
                                           && pm.InvPromotionMasterID.Equals(invPromotionMaster.InvPromotionMasterID)
                                           select new
                                           {
                                               pd.CategoryID,
                                               dc.CategoryCode,
                                               dc.CategoryName,
                                               pd.Qty,
                                               pd.DiscountPercentage,
                                               pd.DiscountAmount,
                                               pd.Points

                                           }).ToArray();

            foreach (var tempProduct in getCategoryDisDetails)
            {

                invPromotionDetailsCategoryDiscount = new InvPromotionDetailsCategoryDis();

                invPromotionDetailsCategoryDiscount.CategoryCode = tempProduct.CategoryCode;
                invPromotionDetailsCategoryDiscount.CategoryID = tempProduct.CategoryID;
                invPromotionDetailsCategoryDiscount.CategoryName = tempProduct.CategoryName;
                invPromotionDetailsCategoryDiscount.Qty = tempProduct.Qty;
                invPromotionDetailsCategoryDiscount.Points = tempProduct.Points;
                invPromotionDetailsCategoryDiscount.DiscountAmount = tempProduct.DiscountAmount;
                invPromotionDetailsCategoryDiscount.DiscountPercentage = tempProduct.DiscountPercentage;

                invPromotionDetailsGetCategoryDisList.Add(invPromotionDetailsCategoryDiscount);

            }


            return invPromotionDetailsGetCategoryDisList.OrderBy(pd => pd.CategoryID).ToList();
        }


        public List<InvPromotionDetailsSubCategoryDis> getGetSubCategoryDisDetail(InvPromotionMaster invPromotionMaster)
        {
            InvPromotionDetailsSubCategoryDis invPromotionDetailsSubCategoryDiscount;

            List<InvPromotionDetailsSubCategoryDis> invPromotionDetailsGetSubCategoryDisList = new List<InvPromotionDetailsSubCategoryDis>();


            var getSubCategoryDisDetails = (from pm in context.InvPromotionMasters
                                            join pd in context.InvPromotionDetailsSubCategoryDis on pm.InvPromotionMasterID equals pd.InvPromotionMasterID
                                            join dc in context.InvSubCategories on pd.SubCategoryID equals dc.InvSubCategoryID
                                         where pm.LocationID.Equals(invPromotionMaster.LocationID) && pm.CompanyID.Equals(invPromotionMaster.CompanyID)
                                         && pm.InvPromotionMasterID.Equals(invPromotionMaster.InvPromotionMasterID)
                                         select new
                                         {
                                             pd.SubCategoryID,
                                             dc.SubCategoryCode,
                                             dc.SubCategoryName,
                                             pd.Qty,
                                             pd.DiscountPercentage,
                                             pd.DiscountAmount,
                                             pd.Points

                                         }).ToArray();

            foreach (var tempProduct in getSubCategoryDisDetails)
            {

                invPromotionDetailsSubCategoryDiscount = new InvPromotionDetailsSubCategoryDis();

                invPromotionDetailsSubCategoryDiscount.SubCategoryCode = tempProduct.SubCategoryCode;
                invPromotionDetailsSubCategoryDiscount.SubCategoryID = tempProduct.SubCategoryID;
                invPromotionDetailsSubCategoryDiscount.SubCategoryName = tempProduct.SubCategoryName;
                invPromotionDetailsSubCategoryDiscount.Qty = tempProduct.Qty;
                invPromotionDetailsSubCategoryDiscount.Points = tempProduct.Points;
                invPromotionDetailsSubCategoryDiscount.DiscountAmount = tempProduct.DiscountAmount;
                invPromotionDetailsSubCategoryDiscount.DiscountPercentage = tempProduct.DiscountPercentage;

                invPromotionDetailsGetSubCategoryDisList.Add(invPromotionDetailsSubCategoryDiscount);

            }


            return invPromotionDetailsGetSubCategoryDisList.OrderBy(pd => pd.SubCategoryID).ToList();
        }


        public List<InvPromotionDetailsSubCategory2Dis> getGetSubCategory2DisDetail(InvPromotionMaster invPromotionMaster)
        {
            InvPromotionDetailsSubCategory2Dis invPromotionDetailsSubCategory2Discount;

            List<InvPromotionDetailsSubCategory2Dis> invPromotionDetailsGetSubCategory2DisList = new List<InvPromotionDetailsSubCategory2Dis>();


            var getSubCategoryDisDetails = (from pm in context.InvPromotionMasters
                                            join pd in context.InvPromotionDetailsSubCategory2Dis on pm.InvPromotionMasterID equals pd.InvPromotionMasterID
                                            join dc in context.InvSubCategories2 on pd.SubCategory2ID equals dc.InvSubCategory2ID
                                            where pm.LocationID.Equals(invPromotionMaster.LocationID) && pm.CompanyID.Equals(invPromotionMaster.CompanyID)
                                            && pm.InvPromotionMasterID.Equals(invPromotionMaster.InvPromotionMasterID)
                                            select new
                                            {
                                                pd.SubCategory2ID,
                                                dc.SubCategory2Code,
                                                dc.SubCategory2Name,
                                                pd.Qty,
                                                pd.DiscountPercentage,
                                                pd.DiscountAmount,
                                                pd.Points

                                            }).ToArray();

            foreach (var tempProduct in getSubCategoryDisDetails)
            {

                invPromotionDetailsSubCategory2Discount = new InvPromotionDetailsSubCategory2Dis();

                invPromotionDetailsSubCategory2Discount.SubCategory2Code = tempProduct.SubCategory2Code;
                invPromotionDetailsSubCategory2Discount.SubCategory2ID = tempProduct.SubCategory2ID;
                invPromotionDetailsSubCategory2Discount.SubCategory2Name = tempProduct.SubCategory2Name;
                invPromotionDetailsSubCategory2Discount.Qty = tempProduct.Qty;
                invPromotionDetailsSubCategory2Discount.Points = tempProduct.Points;
                invPromotionDetailsSubCategory2Discount.DiscountAmount = tempProduct.DiscountAmount;
                invPromotionDetailsSubCategory2Discount.DiscountPercentage = tempProduct.DiscountPercentage;

                invPromotionDetailsGetSubCategory2DisList.Add(invPromotionDetailsSubCategory2Discount);

            }


            return invPromotionDetailsGetSubCategory2DisList.OrderBy(pd => pd.SubCategory2ID).ToList();
        }

        public List<InvPromotionDetailsAllowLocations> getPromotionDetailLocations(InvPromotionMaster invPromotionMaster, List<InvPromotionDetailsAllowLocations> invPromotionAllowLocationList)
        {
            InvPromotionDetailsAllowLocations invPromotionDetailsAllowLocations;
            List<InvPromotionDetailsAllowLocations> invPromotionAllowLocationListtmp = new List<InvPromotionDetailsAllowLocations>();

            //List<InvPromotionDetailsAllowLocations> invPromotionDetailsAllowLocationsList = new List<InvPromotionDetailsAllowLocations>();
            List<Location> location=new List<Location>();

            location=context.Locations.Where(l=> l.IsDelete==false).ToList();

            invPromotionAllowLocationList = context.InvPromotionDetailsAllowLocations.Where(p => p.InvPromotionMasterID == invPromotionMaster.InvPromotionMasterID && p.PromotionLocationID!=0).ToList();
            foreach (Location loc in location)
            {
                invPromotionDetailsAllowLocations = new InvPromotionDetailsAllowLocations();
                invPromotionDetailsAllowLocations.LocationID = loc.LocationID;
                invPromotionDetailsAllowLocations.LocationName = loc.LocationName;
                invPromotionDetailsAllowLocations.InvPromotionMasterID = invPromotionMaster.InvPromotionMasterID;

                foreach (InvPromotionDetailsAllowLocations promoloc in invPromotionAllowLocationList)
                {
                    
                    if (promoloc.PromotionLocationID == loc.LocationID)
                    {
                        if (invPromotionDetailsAllowLocations.IsSelect!=true)
                            invPromotionDetailsAllowLocations.IsSelect = true;

                    }
                    else
                    {
                        if (invPromotionDetailsAllowLocations.IsSelect != true)
                            invPromotionDetailsAllowLocations.IsSelect = false;
                    }

                }

                invPromotionAllowLocationListtmp.Add(invPromotionDetailsAllowLocations);

            }

            //var getPromotionDetailLocations =  (from context.InvPromotionDetailsAllowLocations 
            //                                   where InvPromotionMasterID==invPromotionMaster.InvPromotionMasterID );

            //foreach (var tempProduct in getPromotionDetailLocations)
            //{

            //    invPromotionDetailsAllowLocations = new InvPromotionDetailsAllowLocations();
            //    invPromotionDetailsAllowLocations.LocationID = tempProduct.LocationID;
            //    invPromotionDetailsAllowLocations.LocationName = tempProduct.LocationName;

            //    invPromotionDetailsAllowLocations.IsSelect = tempProduct.IsSelect;
            //    invPromotionAllowLocationList.Add(invPromotionDetailsAllowLocations);

            //}


            return invPromotionAllowLocationListtmp.OrderBy(pd => pd.LocationID).ToList();
        }

        public string[] GetAllInvPromotionCodes()
        {
            List<string> PromotionList = context.InvPromotionMasters.Where(s => s.IsDelete.Equals(false)).Select(s => s.PromotionCode).ToList();
            return PromotionList.ToArray();

        }

        public string[] GetAllInvPromotionNames()
        {
            List<string> PromotionList = context.InvPromotionMasters.Where(s => s.IsDelete.Equals(false)).Select(s => s.PromotionName).ToList();
            return PromotionList.ToArray();

        }
    }
}
