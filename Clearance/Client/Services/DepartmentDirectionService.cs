using Clearance.Client.Authentication;
using Clearance.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Clearance.Client.Services
{
    public class DepartmentDirectionService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private readonly NavigationManager navigationManager;

        public DepartmentDirectionService(HttpClient httpClient, ITokenService tokenService,NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
            this.navigationManager = navigationManager;
        }

        public async Task<List<DepartmentDirectionDTO>?> GetAll(int DepId)
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
            return await httpClient.GetFromJsonAsync<List<DepartmentDirectionDTO>>($"api/CollageDirection/GetAll/{DepId}");
        }

        public async Task<DepartmentDirectionDTO?> GetById(int Id)
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

                return await response.Content.ReadFromJsonAsync<DepartmentDirectionDTO>();
            }
            else
            {
                return null;
            }
        }

         public async Task<int?> GetCollageIdByDepartment(int depId)
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
            var response = await httpClient.GetAsync($"api/CollageDirection/GetCollageIdByDepartment/{depId}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<int>();
            }
            else
            {
                return null;
            }
        }


        public async Task<bool> Insert(DepartmentDirectionDTO collageDirectionDTO)
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
