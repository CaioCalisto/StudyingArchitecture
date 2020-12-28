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
            context.Services.AddSingleton<IRegistrationAPI>(new Mock<IRegistrationAPI>().Object);

            string expected = "<h1>Vehicles</h1><br /><div></div><br /><button id=\"getMoreBtn\">Get more 10</button><br /><ul></ul>";
            context.RenderComponent<Vehicles>().MarkupMatches(expected);
        }

        /// <summary>
        /// Unit test for Vehicle Page.
        /// </summary>
        [TestMethod]
        public void VehiclesPage_clickIngetMoreBtn_ListShouldContainElements()
        {
            Bunit.TestContext context = new Bunit.TestContext();
            Mock<IRegistrationAPI> apiMock = new Mock<IRegistrationAPI>();
            apiMock.Setup(a => a.GetVehiclesAsync()).Returns(Task.FromResult(this.GetVehicles()));
            context.Services.AddSingleton<IRegistrationAPI>(apiMock.Object);
            IRenderedComponent<Vehicles> page = context.RenderComponent<Vehicles>();
            page.FindAll("button").GetElementById("getMoreBtn").Click(new MouseEventArgs());

            string expected = "<h1>Vehicles</h1><br /><div></div><br /><button id=\"getMoreBtn\">Get more 10</button><br /><ul><li>Ferrari F50 - Sport</li><li>Ford Focus - Standard</li></ul>";
            page.MarkupMatches(expected);
        }

        /// <summary>
        /// Unit test for Vehicle Page.
        /// </summary>
        [TestMethod]
        public void VehiclesPage_clickIngetMoreBtn_ShouldGetErrorWhenCallIsRefused()
        {
            string errorMessage = "No connection could be made because the target machine actively refused it.";
            Bunit.TestContext context = new Bunit.TestContext();
            Mock<IRegistrationAPI> apiMock = new Mock<IRegistrationAPI>();
            apiMock.Setup(a => a.GetVehiclesAsync()).Throws(new System.Exception(errorMessage));
            context.Services.AddSingleton<IRegistrationAPI>(apiMock.Object);
            IRenderedComponent<Vehicles> page = context.RenderComponent<Vehicles>();
            page.FindAll("button").GetElementById("getMoreBtn").Click();

            string expected = $"<h1>Vehicles</h1><br /><div>{errorMessage}</div><br /><button id=\"getMoreBtn\">Get more 10</button><br /><ul></ul>";
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
