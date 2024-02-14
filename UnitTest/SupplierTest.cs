using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using Domain;

namespace UnitTest
{
    [TestClass]
    public class SupplierTest
    {
        [TestMethod]
        public void TestAdd()
        {
            SupplierService supplierService = new SupplierService();

            Supplier supplier = new Supplier();

            int preCount = supplierService.GetAllSuppliers().Count;

            supplier.SupplierCode = "CODE1";
            supplier.SupplierName = "DESCRIPTION1";
            supplier.SupplierGroup.SupplierGroupCode = "REMARK1";
            supplier.BillingTelephone = "";

            supplierService.AddSupplier(supplier);

            int postCount = supplierService.GetAllSuppliers().Count;

            Assert.AreEqual(preCount + 1, postCount, "Record added successfully.");
        }
    }
}
