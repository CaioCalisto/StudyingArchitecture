using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vehicle.Register.Domain.UnitTest.Entities
{
    [TestClass]
    public class VehicleTypeUnitTest
    {
        [TestMethod]
        public void Create_NewVehicleType_VehicleTypeHasName()
        {
            Domain.Entities.VehicleType vType = new Domain.Entities.VehicleType("Cabriolet");
            Assert.AreEqual("Cabriolet", vType.Name);
        }
    }
}
