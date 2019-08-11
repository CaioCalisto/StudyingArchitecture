using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vehicle.Register.Domain.UnitTest.Entities
{
    [TestClass]
    public class BrandUnitTest
    {
        [TestMethod]
        public void Create_NewBrand_BrandHasId()
        {
            Domain.Entities.Brand brand = new Domain.Entities.Brand(1, "BMW");
            Assert.AreEqual(1, brand.BrandId);
        }

        [TestMethod]
        public void Create_NewBrand_BrandHasName()
        {
            Domain.Entities.Brand brand = new Domain.Entities.Brand(1, "BMW");
            Assert.AreEqual("BMW", brand.Name);
        }
    }
}
