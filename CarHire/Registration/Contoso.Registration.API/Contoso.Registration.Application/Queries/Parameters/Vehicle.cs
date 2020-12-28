// <copyright file="Vehicle.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contoso.Registration.Domain.Aggregate;

namespace Contoso.Registration.Application.Queries.Parameters
{
    /// <summary>
    /// Parameter Vehicle.
    /// </summary>
    public class Vehicle : IValidatableObject
    {
        /// <summary>
        /// Gets or Sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Brand.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or Sets Category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or Sets How many doors.
        /// </summary>
        public int? Doors { get; set; }

        /// <summary>
        /// Gets or Sets How many passengers.
        /// </summary>
        public int? Passengers { get; set; }

        /// <summary>
        /// Gets or Sets Gear transmission.
        /// </summary>
        public string Transmission { get; set; }

        /// <summary>
        /// Gets or Sets Miles/Galon consume.
        /// </summary>
        public int? Consume { get; set; }

        /// <summary>
        /// Gets or Sets g/km CO2.
        /// </summary>
        public int? Emission { get; set; }

        /// <summary>
        /// Validate object.
        /// </summary>
        /// <param name="validationContext">Context.</param>
        /// <returns>Results.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (!string.IsNullOrEmpty(this.Category) && !Enum.TryParse(this.Category.ToUpper(), out Category vehicleCategory))
            {
                results.Add(new ValidationResult("'Category' must be valid."));
            }

            if (!string.IsNullOrEmpty(this.Transmission) && !Enum.TryParse(this.Transmission.ToUpper(), out Transmission vehicleTransmission))
            {
                results.Add(new ValidationResult("'Transmission' must be valid."));
            }

            return results;
        }
    }
}
