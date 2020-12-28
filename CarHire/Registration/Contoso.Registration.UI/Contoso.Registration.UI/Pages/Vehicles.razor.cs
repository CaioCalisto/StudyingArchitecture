// <copyright file="Vehicles.razor.cs" company="CaioCesarCalisto">
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
        private IEnumerable<Vehicle> vehicles = new List<Vehicle>();

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
                Console.WriteLine(ex.Message);
            }
        }
    }
}