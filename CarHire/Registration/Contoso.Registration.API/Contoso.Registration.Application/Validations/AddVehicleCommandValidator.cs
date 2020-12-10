// <copyright file="AddVehicleCommandValidator.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using Contoso.Registration.Application.Commands;
using Contoso.Registration.Domain.Aggregate;
using FluentValidation;

namespace Contoso.Registration.Application.Validations
{
    /// <summary>
    /// Validations for AddVehicleCommand.
    /// </summary>
    public class AddVehicleCommandValidator : AbstractValidator<AddVehicleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddVehicleCommandValidator"/> class.
        /// </summary>
        public AddVehicleCommandValidator()
        {
            this.RuleFor(command => command.Brand).NotEmpty();
            this.RuleFor(command => command.Name).NotEmpty();
            this.RuleFor(command => command.Passengers).NotEqual(0);
            this.RuleFor(command => command.Doors).NotEqual(0);
            this.RuleFor(command => command.Emission).NotEqual(0);
            this.RuleFor(command => command.Consume).NotEqual(0);
            this.RuleFor(command => command.Category).Must(this.BeValidCategory).WithMessage("'Category' must be valid.");
            this.RuleFor(command => command.Transmission).Must(this.BeValidTransmission).WithMessage("'Transmission' must be valid.");
        }

        private bool BeValidTransmission(string transmission)
        {
            if (string.IsNullOrEmpty(transmission) || !Enum.TryParse(transmission.ToUpper(), out Transmission vehicleTransmission))
            {
                return false;
            }

            return true;
        }

        private bool BeValidCategory(string category)
        {
            if (string.IsNullOrEmpty(category) || !Enum.TryParse(category.ToUpper(), out Category vehicleCategory))
            {
                return false;
            }

            return true;
        }
    }
}
