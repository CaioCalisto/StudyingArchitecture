using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vehicle.Register.Domain.UnitTest.Aggregates
{
    [TestClass]
    public class VehicleUnitTest
    {
        [TestMethod]
        public void Create_NewCar_VehicleHasName()
        {
            Domain.Aggregates.Vehicle vehicle = new Domain.Aggregates.Vehicle("118");

            Assert.AreEqual("118", vehicle.Name);
        }
        
        [TestMethod]
        public void Create_NewCar_VehicleHasVehicleTypeName()
        {
            Domain.Entities.VehicleType vType = new Domain.Entities.VehicleType("Cabriolet");
            Domain.Aggregates.Vehicle vehicle = new Domain.Aggregates.Vehicle("118", vType);

            Assert.AreEqual("Cabriolet", vehicle.VehicleType.Name);
        }

        [TestMethod]
        public void Create_NoVehicleType_VehicleHasVehicleTypeNull()
        {
            Domain.Aggregates.Vehicle vehicle = new Domain.Aggregates.Vehicle("118");

            Assert.IsNull(vehicle.VehicleType);
        }

        [TestMethod]
        public void Create_NewCar_VehicleHasBrand()
        {
            Domain.Entities.VehicleType vType = new Domain.Entities.VehicleType("Cabriolet");
            Domain.Entities.Brand brand = new Domain.Entities.Brand("BMW");
            Domain.Aggregates.Vehicle vehicle = new Domain.Aggregates.Vehicle("118", vType, brand);

            Assert.AreEqual("BMW", vehicle.Brand.Name);
        }

        [TestMethod]
        public void SetName_VehicleHasName_NameIsChanged()
        {
            Domain.Aggregates.Vehicle vehicle = new Domain.Aggregates.Vehicle("118");
            vehicle.SetName("320");
            Assert.AreEqual("320", vehicle.Name);
        }

        [TestMethod]
        public void SetBrand_VehicleDoesNotContainBrand_VehicleHasBrandName()
        {
            Domain.Aggregates.Vehicle vehicle = new Domain.Aggregates.Vehicle("118");
            Domain.Entities.Brand brand = new Domain.Entities.Brand("BMW");
            vehicle.SetBrand(brand);

            Assert.AreEqual("BMW", vehicle.Brand.Name);
        }

        [TestMethod]
        public void SetVehicleType_VehicleDoesNotContainType_VehicleHasTypeName()
        {
            Domain.Aggregates.Vehicle vehicle = new Domain.Aggregates.Vehicle("118");
            Domain.Entities.VehicleType vType = new Domain.Entities.VehicleType("Cabriolet");
            vehicle.SetVehicleType(vType);

            Assert.AreEqual("Cabriolet", vehicle.VehicleType.Name);
        }
    }
}
