// <copyright file="NavMenuUnitTest.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Linq;
using AngleSharp.Dom;
using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Contoso.Registration.UI.Shared
{
    /// <summary>
    /// Test for nav menu.
    /// </summary>
    [TestClass]
    public class NavMenuUnitTest
    {
        private Bunit.TestContext TestContext;

        [TestInitialize]
        public void Setup() => TestContext = new Bunit.TestContext();

        [TestCleanup]
        public void TearDown() => TestContext?.Dispose();

        /// <summary>
        /// Navmenu test.
        /// </summary>
        [TestMethod]
        public void NavMenu_MenuHomeExists()
        {
            // Arrange
            IRenderedComponent<NavMenu> navMenu = TestContext.RenderComponent<NavMenu>();
            IRefreshableElementCollection<AngleSharp.Dom.IElement> menus = navMenu.FindAll("li");

            // Assert            
            menus.Any<IElement>(a => a.TextContent.Contains("Home"));
        }

        /// <summary>
        /// Navmenu test.
        /// </summary>
        [TestMethod]
        public void NavMenu_MenuVehiclesExists()
        {
            // Arrange
            IRenderedComponent<NavMenu> navMenu = TestContext.RenderComponent<NavMenu>();
            IRefreshableElementCollection<IElement> menus = navMenu.FindAll("li");

            // Assert
            menus.Any<IElement>(a => a.TextContent.Contains("Vehicles"));            
        }

        /// <summary>
        /// Navmenu test.
        /// </summary>
        [TestMethod]
        public void NavMenu_RegistrationMenuExists()
        {
            // Arrange
            IRenderedComponent<NavMenu> navMenu = TestContext.RenderComponent<NavMenu>();
            IRefreshableElementCollection<IElement> menus = navMenu.FindAll("a");

            // Assert
            menus.Any<IElement>(a => a.TextContent.Contains("Registration"));
        }

        /// <summary>
        /// Navmenu test.
        /// </summary>
        [TestMethod]
        public void ToggleNavMenu_Click_ShouldNavigateToPage()
        {

        }
    }
}
