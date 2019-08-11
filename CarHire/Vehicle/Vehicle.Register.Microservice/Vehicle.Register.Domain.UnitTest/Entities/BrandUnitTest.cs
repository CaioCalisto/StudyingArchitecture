using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vehicle.Register.Domain.UnitTest.Entities
{
    [TestClass]
    public class BrandUnitTest
    {
        [TestMethod]
        public void Create_NewBrand_BrandHasName()
        {
            Domain.Entities.Brand brand = new Domain.Entities.Brand("BMW");
            Assert.AreEqual("BMW", brand.Name);
        }
    }
}
