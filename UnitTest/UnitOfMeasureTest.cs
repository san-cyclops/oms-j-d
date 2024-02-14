using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using Domain;

namespace UnitTest
{
    [TestClass]
    public class UnitOfMeasureTest
    {
        [TestMethod]
        public void TestAdd()
        {
            UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();

            UnitOfMeasure unitOfMeasure = new UnitOfMeasure();

            int preCount = unitOfMeasureService.GetAllUnitOfMeasures().Count;

            unitOfMeasure.UnitOfMeasureCode = "CODE1";
            unitOfMeasure.UnitOfMeasureName = "DESCRIPTION1";
            unitOfMeasure.Remark = "REMARK1";

            unitOfMeasureService.AddUnitOfMeasure(unitOfMeasure);

            int postCount = unitOfMeasureService.GetAllUnitOfMeasures().Count;

            Assert.AreEqual(preCount + 1, postCount, "Record added successfully.");
        }

        [TestMethod]
        public void TestUpdate()
        {
            UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();

            UnitOfMeasure unitOfMeasure = new UnitOfMeasure();

            UnitOfMeasure unitOfMeasureBeforeUpdate = unitOfMeasureService.GetUnitOfMeasureByCode("CODE1");
            long unitOfMeasureBeforeUpdateId = unitOfMeasureBeforeUpdate.UnitOfMeasureID;

            unitOfMeasureBeforeUpdate.UnitOfMeasureCode = "CODE2";
            unitOfMeasureBeforeUpdate.UnitOfMeasureName = "DESCRIPTION2";

            unitOfMeasureService.UpdateUnitOfMeasure(unitOfMeasureBeforeUpdate);

            UnitOfMeasure unitOfMeasureAfterUpdate = unitOfMeasureService.GetUnitOfMeasureById(unitOfMeasureBeforeUpdateId);

            Assert.AreEqual("CODE2", unitOfMeasureAfterUpdate.UnitOfMeasureCode);
            Assert.AreEqual("DESCRIPTION2", unitOfMeasureAfterUpdate.UnitOfMeasureName);
        }

        [TestMethod]
        public void TestGetNewCode()
        {
            
        }
    }
}
