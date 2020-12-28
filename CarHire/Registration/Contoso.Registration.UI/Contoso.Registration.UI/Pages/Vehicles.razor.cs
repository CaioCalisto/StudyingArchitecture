﻿// <copyright file="Vehicles.razor.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contoso.Registration.Services.Api;
using Contoso.Registration.Services.Api.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Contoso.Registration.UI.Pages
{
    /// <summary>
    /// Vehicles page.
    /// </summary>
    public partial class Vehicles : ComponentBase
    {
        protected IEnumerable<Vehicle> vehicles = new List<Vehicle>();
        protected string error = string.Empty;

        [Inject]
        private IRegistrationAPI RegistrationApi { get; set; }

        private async Task GetMoreVehicles(MouseEventArgs args)
        {
            try
            {
                this.vehicles = await this.RegistrationApi.GetVehiclesAsync();
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
            }
        }
    }
}