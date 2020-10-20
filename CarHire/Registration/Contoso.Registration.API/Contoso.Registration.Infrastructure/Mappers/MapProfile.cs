// <copyright file="MapProfile.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using AutoMapper;

namespace Contoso.Registration.Infrastructure.Mappers
{
    /// <summary>
    /// Mapper DB x Domain.
    /// </summary>
    public class MapProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapProfile"/> class.
        /// </summary>
        public MapProfile()
        {
            this.CreateMap<Model.Vehicle, Domain.Aggregate.Vehicle>()
                .ForMember(v => v.Category, opt => opt.MapFrom(src => this.MapCategory(src.Category)))
                .ForMember(v => v.Transmission, opt => opt.MapFrom(src => this.MapTransmission(src.Transmission)));
        }

        private Domain.Aggregate.Category MapCategory(string category)
        {
            Domain.Aggregate.Category vehicleCategory;
            if (!Enum.TryParse(category.ToUpper(), out vehicleCategory))
            {
                throw new ArgumentException($"Category {category} doest not exists");
            }

            return vehicleCategory;
        }

        private Domain.Aggregate.Transmission MapTransmission(string transmission)
        {
            Domain.Aggregate.Transmission vehicleTransmission;
            if (!Enum.TryParse(transmission.ToUpper(), out vehicleTransmission))
            {
                throw new ArgumentException($"Category {vehicleTransmission} doest not exists");
            }

            return vehicleTransmission;
        }
    }
}
