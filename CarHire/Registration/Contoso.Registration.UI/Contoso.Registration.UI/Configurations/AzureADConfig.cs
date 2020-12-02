// <copyright file="AzureADConfig.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

namespace Contoso.Registration.UI.Configurations
{
    internal class AzureADConfig
    {
        public string ClientId { get; set; }

        public string RegistrationApiSecret { get; set; }

        public string Instance { get; set; }

        public string TenantId { get; set; }

        public string ResourceId { get; set; }
    }
}
