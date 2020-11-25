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

namespace Contoso.Registration.UI.Pages
{
    /// <summary>
    /// Page vehicles.
    /// </summary>
    [TestClass]
    public class VehiclesUnitTest
    {
        /// <summary>
        /// Unit test for Vehicle Page.
        /// </summary>
        [TestMethod]
        public void VehiclesPage_FirstLoad_LoadEmptyList()
        {
            Bunit.TestContext context = new Bunit.TestContext();
            context.Services.AddSingleton<IRegistrationAPI>(this.GetApiMock().Object);

            string expected = "<h1>Vehicles</h1><br /><button id=\"getMoreBtn\">Get more 10</button><br /><ul></ul>";
            this.GetVehiclePageComponent(context).MarkupMatches(expected);
        }

        /// <summary>
        /// Unit test for Vehicle Page.
        /// </summary>
        [TestMethod]
        public void VehiclesPage_clickIngetMoreBtn_ListShouldContainElements()
        {
            Bunit.TestContext context = new Bunit.TestContext();
            Mock<IRegistrationAPI> apiMock = this.GetApiMock();
            apiMock.Setup(a => a.GetVehiclesAsync()).Returns(Task.FromResult(this.GetVehicles()));
            context.Services.AddSingleton<IRegistrationAPI>(apiMock.Object);
            IRenderedComponent<Vehicles> page = this.GetVehiclePageComponent(context);
            IElement button = page.FindAll("button").GetElementById("getMoreBtn");

            button.Click(new MouseEventArgs());

            string expected = "<h1>Vehicles</h1><br /><button id=\"getMoreBtn\">Get more 10</button><br /><ul><li>Ferrari F50 - Sport</li><li>Ford Focus - Standard</li></ul>";
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

        private IRenderedComponent<Vehicles> GetVehiclePageComponent(Bunit.TestContext context) => context.RenderComponent<Vehicles>();

        private Mock<IRegistrationAPI> GetApiMock() => new Mock<IRegistrationAPI>();
    }
}
