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
        /// <summary>
        /// Navmenu test.
        /// </summary>
        [TestMethod]
        public void NavMenu_MenuHomeExists()
        {
            Bunit.TestContext context = new Bunit.TestContext();
            IRenderedComponent<NavMenu> navMenu = context.RenderComponent<NavMenu>();
            IRefreshableElementCollection<AngleSharp.Dom.IElement> menus = navMenu.FindAll("li");

            menus.Any<IElement>(a => a.TextContent.Contains("Home"));
        }

        /// <summary>
        /// Navmenu test.
        /// </summary>
        [TestMethod]
        public void NavMenu_MenuVehiclesExists()
        {
            Bunit.TestContext context = new Bunit.TestContext();
            IRenderedComponent<NavMenu> navMenu = context.RenderComponent<NavMenu>();
            IRefreshableElementCollection<IElement> menus = navMenu.FindAll("li");

            menus.Any<IElement>(a => a.TextContent.Contains("Vehicles"));
        }

        /// <summary>
        /// Navmenu test.
        /// </summary>
        [TestMethod]
        public void NavMenu_RegistrationMenuExists()
        {
            Bunit.TestContext context = new Bunit.TestContext();
            IRenderedComponent<NavMenu> navMenu = context.RenderComponent<NavMenu>();
            IRefreshableElementCollection<IElement> menus = navMenu.FindAll("a");

            menus.Any<IElement>(a => a.TextContent.Contains("Registration"));
        }

        /// <summary>
        /// Navmenu test.
        /// </summary>
        [TestMethod]
        public void ToggleNavMenu_Click_ShouldOpenMenu()
        {

        }
    }
}
