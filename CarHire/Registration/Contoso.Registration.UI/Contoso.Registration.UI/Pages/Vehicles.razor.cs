// <copyright file="Vehicles.razor.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Components;

namespace Contoso.Registration.UI.Pages
{
    /// <summary>
    /// Vehicles page.
    /// </summary>
    public partial class Vehicles : ComponentBase
    {
        private bool collapse1Visible = false;
        private bool collapse2Visible = false;
        private int saveCount = 0;

        private void UpdateSaveCount(int value)
        {
            saveCount = value;
        }
    }
}