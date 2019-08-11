using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vehicle.Register.Domain.UnitTest.Entities
{
    [TestClass]
    public class VehicleTypeUnitTest
    {
        [TestMethod]
        public void Create_NewVehicleType_VehicleTypeHasId()
        {
            Domain.Entities.VehicleType vType = new Domain.Entities.VehicleType(1, "Cabriolet");
            Assert.AreEqual(1, vType.VehicleTypeId);
        }

        [TestMethod]
        public void Create_NewVehicleType_VehicleTypeHasName()
        {
            Domain.Entities.VehicleType vType = new Domain.Entities.VehicleType(1, "Cabriolet");
            Assert.AreEqual("Cabriolet", vType.Name);
        }
    }
}
