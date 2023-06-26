using Clearance.Client.Authentication;
using Clearance.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Clearance.Client.Services
{
    public class CollageDirectionService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private readonly NavigationManager navigationManager;

        public CollageDirectionService(HttpClient httpClient, ITokenService tokenService,NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
            this.navigationManager = navigationManager;
        }

        public async Task<List<CollageDirectionDTO>?> GetAll(int CollageId)
        {
            var token = await tokenService.GetToken();

            if (token != null && token.Expiration > DateTime.UtcNow)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            }
            else
            {
                navigationManager.NavigateTo("login", true);
            }
            return await httpClient.GetFromJsonAsync<List<CollageDirectionDTO>>($"api/CollageDirection/GetAll/{CollageId}");
        }

        public async Task<CollageDirectionDTO?> GetById(int Id)
        {
            var token = await tokenService.GetToken();

            if (token != null && token.Expiration > DateTime.UtcNow)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            }
            else
            {
                navigationManager.NavigateTo("login", true);
            }
            var response = await httpClient.GetAsync($"api/CollageDirection/GetById/{Id}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<CollageDirectionDTO>();
            }
            else
            {
                return null;
            }
        }


        public async Task<bool> Insert(CollageDirectionDTO collageDirectionDTO)
        {
            var token = await tokenService.GetToken();

            if (token != null && token.Expiration > DateTime.UtcNow)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            }
            else
            {
                navigationManager.NavigateTo("login", true);
            }

            var response = await httpClient.PostAsJsonAsync("api/CollageDirection/Post/", collageDirectionDTO);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return false;
            }
            else
                return false;
        }

        public async Task<bool> Delete(int Id)
        {
            var token = await tokenService.GetToken();

            if (token != null && token.Expiration > DateTime.UtcNow)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            }
            else
            {
                navigationManager.NavigateTo("login", true);
            }
            var response = await httpClient.DeleteAsync($"api/CollageDirection/Delete/{Id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return false;
            }
            else
                return false;
        }
    }
}
