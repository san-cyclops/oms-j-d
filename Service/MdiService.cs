using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MdiService
    {

        public DataTable GetPurchaseOrderDataTable()
        {

            InvPurchaseOrderService purchaseOrderService = new InvPurchaseOrderService();
            return purchaseOrderService.GetAllPurchaseOrderDataTable();

        }


        //public DataTable GetLoyaltyCustomerDataTable()
        //{

        //    CustomerService customerService = new CustomerService();
        //    return  customerService.GetLoyaltyCustomerDataTable();

        //}
        //public DataTable GetCustomerHistoryDataTable()
        //{
        //    CustomerService customerService = new CustomerService();
        //    return customerService.GetCustomerHistoryDataTable();
        //}
        //public DataTable GetAllSupplierDataTable()
        //{
        //    //SupplierService supplierService = new SupplierService();
        //    //return supplierService.GetAllSupplierDataTable();
        //}
        public DataTable GetAllLocationDataTable()
        {
            LocationService locationService = new LocationService();
            return locationService.GetAllLocationDataTable();
        }
        public DataTable GetAllDepartmentWiseCategoryDataTable()
        {
            InvCategoryService invCategoryService = new InvCategoryService();
            return invCategoryService.GetAllDepartmentWiseCategoryDataTable();
        }
        public DataTable GetAllCategoryDataTable()
        {
            InvCategoryService invCategoryService = new InvCategoryService();
            return invCategoryService.GetAllCategoryDataTable();
        }
        public DataTable GetAllCategoryWiseSubCategoryDataTable()
        {
            InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
            return invSubCategoryService.GetAllCategoryWiseSubCategoryDataTable();
        }
        public DataTable GetAllSubCategoryDataTable()
        {
            InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
            return invSubCategoryService.GetAllSubCategoryDataTable();
        }

        public DataTable GetAllSubCategoryWiseSub2CategoryDataTable()
        {
            InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
            return invSubCategory2Service.GetAllSubCategoryWiseSub2CategoryDataTable();
        }
        public DataTable GetAllSub2CategoryDataTable()
        {
            InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
            return invSubCategory2Service.GetAllSub2CategoryDataTable();
        }
        public DataTable GetAllProductDataTable()
        {
            InvProductMasterService invProductMasterService = new InvProductMasterService();
            return invProductMasterService.GetAllProductDataTable();
        }
        //public DataTable GetPurchaseDataTable()
        //{
        //    InvPurchaseService invPurchaseService = new InvPurchaseService();
        //    return invPurchaseService.GetPurchaseDataTable();
        //}
       

        #region Logistic
        public DataTable GetAllLgsSupplierDataTable()
        {
            LgsSupplierService lgsSupplierService = new LgsSupplierService();
            return lgsSupplierService.GetAllLgsSupplierDataTable();
        }

        public DataTable GetAllLgsDepartmentWiseCategoryDataTable()
        {
            LgsCategoryService lgsCategoryService = new LgsCategoryService();
            return lgsCategoryService.GetAllLgsDepartmentWiseCategoryDataTable();
        }
        public DataTable GetAllLgsCategoryDataTable()
        {
            LgsCategoryService lgsCategoryService = new LgsCategoryService();
            return lgsCategoryService.GetAllLgsCategoryDataTable();
        }

        public DataTable GetAllLgsCategoryWiseSubCategoryDataTable()
        {
            LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
            return lgsSubCategoryService.GetAllLgsCategoryWiseSubCategoryDataTable();
        }
        public DataTable GetAllLgsSubCategoryDataTable()
        {
            LgsSubCategoryService lgsSubCategoryService = new LgsSubCategoryService();
            return lgsSubCategoryService.GetAllLgsSubCategoryDataTable();
        }

        public DataTable GetAllLgsSubCategoryWiseSub2CategoryDataTable()
        {
            LgsSubCategory2Service lgsSubCategory2Service = new LgsSubCategory2Service();
            return lgsSubCategory2Service.GetAllLgsSubCategoryWiseSub2CategoryDataTable();
        }
        public DataTable GetAllLgsSub2CategoryDataTable()
        {
            LgsSubCategory2Service lgsSubCategory2Service = new LgsSubCategory2Service();
            return lgsSubCategory2Service.GetAllLgsSub2CategoryDataTable();
        }


        #endregion
    }
}
