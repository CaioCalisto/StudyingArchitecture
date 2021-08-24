// <copyright file="AddVehicle.razor.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Contoso.Registration.UI.Components
{
    /// <summary>
    /// Vehicles List Component.
    /// </summary>
    public partial class AddVehicle : ComponentBase
    {
        protected IEnumerable<string> categories = new List<string>()
        {
            "Sport",
            "Standard",
            "Coupé"
        };

        private async Task AddVehicleBtn()
        {

        }
    }
}