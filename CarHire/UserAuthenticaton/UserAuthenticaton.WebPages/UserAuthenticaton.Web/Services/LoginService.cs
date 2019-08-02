using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UserAuthenticaton.Web.Configurations;
using UserAuthenticaton.Web.Models;

namespace UserAuthenticaton.Web.Services
{
    public class LoginService : ILoginService
    {
        private ApiConfig apiConfig;

        public LoginService(IOptions<ApiConfig> apiConfig)
        {
            this.apiConfig = apiConfig.Value ?? throw new ArgumentException("There is no apiConfig");
        }

        public async Task<AuthenticationResponse> LogIn(EndUser endUser)
        {
            string endPoint = $"/api/v1.0/Login/";
            AuthenticationResponse authenticationResponse = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.apiConfig.AuthenticatioApi);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync(endPoint, endUser);
                if (response.IsSuccessStatusCode)
                {
                    authenticationResponse = await response.Content.ReadAsAsync<AuthenticationResponse>();
                }
                return authenticationResponse;
            }
        }
    }
}
