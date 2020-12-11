// <copyright file="AddVehicleCommandValidatorUnitTest.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Linq;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Contoso.Registration.Application.Validations
{
    /// <summary>
    /// Unit test for validations.
    /// </summary>
    [TestClass]
    public class AddVehicleCommandValidatorUnitTest
    {
        /// <summary>
        /// Test for validations.
        /// </summary>
        [TestMethod]
        public void Validate_BrandIsEmpty_ShouldBeInvalid()
        {
            AddVehicleCommandValidator validator = new AddVehicleCommandValidator();
            var command = this.GetCommand();
            command.Brand = string.Empty;
            ValidationResult result = validator.Validate(command);

            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("'Brand' must not be empty.", result.Errors.First().ErrorMessage);
        }

        /// <summary>
        /// Test for validations.
        /// </summary>
        [TestMethod]
        public void Validate_NameIsEmpty_ShouldBeInvalid()
        {
            AddVehicleCommandValidator validator = new AddVehicleCommandValidator();
            var command = this.GetCommand();
            command.Name = string.Empty;
            ValidationResult result = validator.Validate(command);

            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("'Name' must not be empty.", result.Errors.First().ErrorMessage);
        }

        /// <summary>
        /// Test for validations.
        /// </summary>
        [TestMethod]
        public void Validate_InvalidCategory_ShouldBeInvalid()
        {
            AddVehicleCommandValidator validator = new AddVehicleCommandValidator();
            var command = this.GetCommand();
            command.Category = string.Empty;
            ValidationResult result = validator.Validate(command);

            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("'Category' must be valid.", result.Errors.First().ErrorMessage);
        }

        /// <summary>
        /// Test for validations.
        /// </summary>
        [TestMethod]
        public void Validate_InvalidTransmission_ShouldBeInvalid()
        {
            AddVehicleCommandValidator validator = new AddVehicleCommandValidator();
            var command = this.GetCommand();
            command.Transmission = string.Empty;
            ValidationResult result = validator.Validate(command);

            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("'Transmission' must be valid.", result.Errors.First().ErrorMessage);
        }

        /// <summary>
        /// Test for validations.
        /// </summary>
        [TestMethod]
        public void Validate_InvalidDoors_ShouldBeInvalid()
        {
            AddVehicleCommandValidator validator = new AddVehicleCommandValidator();
            var command = this.GetCommand();
            command.Doors = 0;
            ValidationResult result = validator.Validate(command);

            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("'Doors' must not be equal to '0'.", result.Errors.First().ErrorMessage);
        }

        /// <summary>
        /// Test for validations.
        /// </summary>
        [TestMethod]
        public void Validate_InvalidEmission_ShouldBeInvalid()
        {
            AddVehicleCommandValidator validator = new AddVehicleCommandValidator();
            var command = this.GetCommand();
            command.Emission = 0;
            ValidationResult result = validator.Validate(command);

            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("'Emission' must not be equal to '0'.", result.Errors.First().ErrorMessage);
        }

        /// <summary>
        /// Test for validations.
        /// </summary>
        [TestMethod]
        public void Validate_InvalidPassengers_ShouldBeInvalid()
        {
            AddVehicleCommandValidator validator = new AddVehicleCommandValidator();
            var command = this.GetCommand();
            command.Passengers = 0;
            ValidationResult result = validator.Validate(command);

            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("'Passengers' must not be equal to '0'.", result.Errors.First().ErrorMessage);
        }

        /// <summary>
        /// Test for validations.
        /// </summary>
        [TestMethod]
        public void Validate_InvalidConsume_ShouldBeInvalid()
        {
            AddVehicleCommandValidator validator = new AddVehicleCommandValidator();
            var command = this.GetCommand();
            command.Consume = 0;
            ValidationResult result = validator.Validate(command);

            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("'Consume' must not be equal to '0'.", result.Errors.First().ErrorMessage);
        }

        private Commands.AddVehicleCommand GetCommand() =>
            new Commands.AddVehicleCommand()
            {
                Brand = "Fiesta",
                Name = "Ford",
                Category = "standard",
                Doors = 4,
                Consume = 20,
                Emission = 10,
                Passengers = 4,
                Transmission = "manual",
            };
    }
}
