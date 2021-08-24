// <copyright file="RegistrationApi.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Contoso.Registration.Services.Api.Models;
using Newtonsoft.Json;

namespace Contoso.Registration.Services.Api
{
    /// <summary>
    /// Registration Api.
    /// </summary>
    public class RegistrationAPI : IRegistrationAPI
    {
        private const string GetVehicleUri = "/api/v1/vehicles/query";
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationAPI"/> class.
        /// </summary>
        /// <param name="httpClient">HttpClient.</param>
        public RegistrationAPI(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            using (HttpResponseMessage response = await this.httpClient.GetAsync(GetVehicleUri))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Vehicle>>(jsonResponse);
                }
            }

            return vehicles;
        }
    }
}