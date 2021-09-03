// <copyright file="VehiclesUnitTest.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Bunit;
using Contoso.Registration.Services.Api;
using Contoso.Registration.Services.Api.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Contoso.Registration.UI.Components.Vehicles
{
    /// <summary>
    /// Page vehicles.
    /// </summary>
    [TestClass]
    public class VehicleListUnitTest
    {
        private Bunit.TestContext TestContext;

        [TestInitialize]
        public void Setup() => TestContext = new Bunit.TestContext();

        [TestCleanup]
        public void TearDown() => TestContext?.Dispose();

        /// <summary>
        /// Unit test for Vehicle Page.
        /// </summary>
        [TestMethod]
        public void VehiclesPage_FirstLoad_LoadEmptyList()
        {
            // Arrange
            string expected = "<h1>Vehicles</h1><br /><div></div><br /><button id=\"getMoreBtn\">Get more 10</button><br /><ul></ul>";

            // Act
            TestContext.Services.AddSingleton<IRegistrationAPI>(new Mock<IRegistrationAPI>().Object);

            // Assert
            TestContext.RenderComponent<VehicleList>().MarkupMatches(expected);
        }

        /// <summary>
        /// Unit test for Vehicle Page.
        /// </summary>
        [TestMethod]
        public void VehiclesPage_clickIngetMoreBtn_ListShouldContainElements()
        {
            // Arrange
            Mock<IRegistrationAPI> apiMock = new Mock<IRegistrationAPI>();
            apiMock.Setup(a => a.GetVehiclesAsync()).Returns(Task.FromResult(this.GetVehicles()));
            TestContext.Services.AddSingleton<IRegistrationAPI>(apiMock.Object);
            string expected = "<h1>Vehicles</h1><br /><div></div><br /><button id=\"getMoreBtn\">Get more 10</button><br /><ul><li>Ferrari F50 - Sport</li><li>Ford Focus - Standard</li></ul>";
            IRenderedComponent<VehicleList> page = TestContext.RenderComponent<VehicleList>();

            // Act            
            page.FindAll("button").GetElementById("getMoreBtn").Click(new MouseEventArgs());

            // Assert            
            page.MarkupMatches(expected);
        }

        /// <summary>
        /// Unit test for Vehicle Page.
        /// </summary>
        [TestMethod]
        public void VehiclesPage_clickIngetMoreBtn_ShouldGetErrorWhenCallIsRefused()
        {
            // Arrange
            string errorMessage = "No connection could be made because the target machine actively refused it.";
            string expected = $"<h1>Vehicles</h1><br /><div>{errorMessage}</div><br /><button id=\"getMoreBtn\">Get more 10</button><br /><ul></ul>";
            Mock<IRegistrationAPI> apiMock = new Mock<IRegistrationAPI>();
            apiMock.Setup(a => a.GetVehiclesAsync()).Throws(new System.Exception(errorMessage));
            TestContext.Services.AddSingleton<IRegistrationAPI>(apiMock.Object);
            IRenderedComponent<VehicleList> page = TestContext.RenderComponent<VehicleList>();

            // Act            
            page.FindAll("button").GetElementById("getMoreBtn").Click();
            
            // Assert            
            page.MarkupMatches(expected);
        }

        private IEnumerable<Vehicle> GetVehicles()
        {
            return new List<Vehicle>()
            {
                new Vehicle() { Brand = "Ferrari", Name = "F50", Category = "Sport" },
                new Vehicle() { Brand = "Ford", Name = "Focus", Category = "Standard" },
            };
        }
    }
}
