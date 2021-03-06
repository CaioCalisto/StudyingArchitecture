﻿// <copyright file="IRepository.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace Contoso.Registration.Domain.Common
{
    /// <summary>
    /// Repository interface.
    /// </summary>
    /// <typeparam name="T">AggregateRoot type.</typeparam>
    public interface IRepository<T>
        where T : IAggregateRoot
    {
    }
}
