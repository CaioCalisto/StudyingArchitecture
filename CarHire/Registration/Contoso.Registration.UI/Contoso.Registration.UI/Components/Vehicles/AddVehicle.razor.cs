// <copyright file="AddVehicle.razor.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Contoso.Registration.Services.Api.Commands;

namespace Contoso.Registration.UI.Components.Vehicles
{
    /// <summary>
    /// Vehicles List Component.
    /// </summary>
    public partial class AddVehicle : ComponentBase
    {
        private AddVehicleCommand addVehicleCommand = new AddVehicleCommand();
        private IEnumerable<string> categories;

        [Parameter]
        public int SaveCount { get; set; }

        [Parameter]
        public EventCallback<int> SaveCountChanged { get; set; }

        protected override void OnInitialized()
        {
            categories = new List<string>()
            {
                "Sport",
                "Standard",
                "Coupé"
            };
        }

        private async Task HandleValidSubmit()
        {
            SaveCount++;
            await SaveCountChanged.InvokeAsync(SaveCount);
        }
    }
}