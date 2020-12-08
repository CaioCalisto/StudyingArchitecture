// <copyright file="ExternalServices.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using Contoso.Registration.Infrastructure.Messaging;

namespace Contoso.Registration.FunctionalTest.Services
{
    internal static class ExternalServices
    {
        public static IMessageBus MessageBus { get; set; }
    }
}
