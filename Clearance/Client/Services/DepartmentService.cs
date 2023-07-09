using Clearance.Client.Authentication;
using Clearance.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Clearance.Client.Services
{
    public class DepartmentService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private readonly NavigationManager navigationManager;

        public DepartmentService(HttpClient httpClient, ITokenService tokenService, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
            this.navigationManager = navigationManager;
        }

        public async Task<List<DepartmentDTO>?> GetAll(int collageId)
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
            return await httpClient.GetFromJsonAsync<List<DepartmentDTO>>($"api/Department/GetAll/{collageId}");
        }
        
        public async Task<List<DepartmentDTO>?> Search(string? Name)
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
            return await httpClient.GetFromJsonAsync<List<DepartmentDTO>>($"api/Department/Search/{Name}");
        }

        public async Task<DepartmentDTO?> GetById(int Id)
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
            var response = await httpClient.GetAsync($"api/Department/GetById/{Id}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<DepartmentDTO>();
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Insert(DepartmentDTO departmentDTO)
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

            var response = await httpClient.PostAsJsonAsync("api/Department/Post/", departmentDTO);
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

        public async Task<bool> Update(DepartmentDTO departmentDTO, int Id)
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
            var response = await httpClient.PutAsJsonAsync($"api/Department/Put/{Id}", departmentDTO);

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
            var response = await httpClient.DeleteAsync($"api/Department/Delete/{Id}");

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
