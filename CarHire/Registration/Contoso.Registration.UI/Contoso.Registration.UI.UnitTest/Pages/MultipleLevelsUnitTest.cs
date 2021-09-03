// <copyright file="MultipleLevelTest.cs" company="CaioCesarCalisto">
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
    /// Page MultiLevels.
    /// </summary>
    [TestClass]
    public class MultipleLevelUnitTest
    {
        private Bunit.TestContext TestContext;

        [TestInitialize]
        private void Setup() => TestContext = new Bunit.TestContext();

        [TestCleanup]
        private void TearDown() => TestContext?.Dispose();

        /// <summary>
        /// Unit test for MultiLevels Page.
        /// </summary>
        [TestMethod]
        public void MultipleLevels_EditTextBox_ShouldPassBrandToChildComponent()
        {
            // Arrange
            IRenderedComponent<MultipleLevelsExample> page = TestContext.RenderComponent<MultipleLevelsExample>();
            string expected = "";

            // Act
            page.Find("TextEdit").TextContent = "Ford Focus";

            // Assert
            page.MarkupMatches(expected);
        }       
    }
}