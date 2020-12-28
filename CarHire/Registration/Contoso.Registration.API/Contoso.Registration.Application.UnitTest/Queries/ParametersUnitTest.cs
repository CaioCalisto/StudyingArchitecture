// <copyright file="ParametersUnitTest.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Contoso.Registration.Application.Queries
{
    /// <summary>
    /// Test for parameters.
    /// </summary>
    [TestClass]
    public class ParametersUnitTest
    {
        /// <summary>
        /// Test for parameters.
        /// </summary>
        [TestMethod]
        public void Validate_WrongTransmission_ReturnError()
        {
            Parameters.Vehicle vehicle = new Parameters.Vehicle()
            {
                Transmission = "NA",
                Category = "standard",
            };
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(vehicle, null, null);
            Validator.TryValidateObject(vehicle, context, results);

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("'Transmission' must be valid.", results.First().ErrorMessage);
        }

        /// <summary>
        /// Test for parameters.
        /// </summary>
        [TestMethod]
        public void Validate_WrongCategory_ReturnError()
        {
            Parameters.Vehicle vehicle = new Parameters.Vehicle()
            {
                Transmission = "manual",
                Category = "NA",
            };
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(vehicle, null, null);
            Validator.TryValidateObject(vehicle, context, results);

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("'Category' must be valid.", results.First().ErrorMessage);
        }
    }
}
