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
            this.CreateMap<Model.Vehicle, Domain.Aggregate.Vehicle>();
        }
    }
}
