// <copyright file="MapProfile.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using AutoMapper;

namespace Contoso.Registration.Application.Mappers
{
    /// <summary>
    /// Map models.
    /// </summary>
    public class MapProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapProfile"/> class.
        /// </summary>
        public MapProfile()
        {
            this.CreateMap<Domain.Aggregate.Vehicle, Model.Vehicle>()
                .ForMember(v => v.Category, opt => opt.MapFrom(src => src.Category.ToString()))
                .ForMember(v => v.Transmission, opt => opt.MapFrom(src => src.Transmission.ToString()));

            this.CreateMap<Infrastructure.Model.Vehicle, Model.Vehicle>();
        }
    }
}
