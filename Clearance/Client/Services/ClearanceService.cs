using Clearance.Client.Authentication;
using Clearance.Shared;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Clearance.Client.Services
{
    public class ClearanceService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public ClearanceService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<List<ClearanceDTO>?> GetAll()
        {
            var token = await tokenService.GetToken();
            if (token != null && token.Expiration > DateTime.UtcNow)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            }
            return await httpClient.GetFromJsonAsync<List<ClearanceDTO>>("api/Clearance/GetAll");
        }

        public async Task<List<ClearanceDTO>?> GetAllByState(string State)
        {
            var token = await tokenService.GetToken();
            if (token != null && token.Expiration > DateTime.UtcNow)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            }
            return await httpClient.GetFromJsonAsync<List<ClearanceDTO>>($"api/Clearance/GetAllByState/{State}");
        }
        public async Task<List<ClearanceDTO>?> GetAllByUserId(Guid Id)
        {
            var token = await tokenService.GetToken();
            if (token != null && token.Expiration > DateTime.UtcNow)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            }
            return await httpClient.GetFromJsonAsync<List<ClearanceDTO>>($"api/Clearance/GetAllByUserId/{Id}");
        }

        public async Task<ClearanceDTO?> GetById(int Id)
        {
            var token = await tokenService.GetToken();

            if (token != null && token.Expiration > DateTime.UtcNow)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            }
            var response = await httpClient.GetAsync($"api/Clearance/GetById/{Id}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<ClearanceDTO>();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ClearanceDTO>?> Search(string? Name)
        {
            var token = await tokenService.GetToken();

            if (token != null && token.Expiration > DateTime.UtcNow)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            }
            return await httpClient.GetFromJsonAsync<List<ClearanceDTO>>($"api/Clearance/Search/{Name}");
        }

         public async Task<List<ClearanceDTO>?> Search(Guid Id,string? Name)
        {
            var token = await tokenService.GetToken();

            if (token != null && token.Expiration > DateTime.UtcNow)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            }
            return await httpClient.GetFromJsonAsync<List<ClearanceDTO>>($"api/Clearance/Search/{Id}/{Name}");
        }


        public async Task<bool> Insert(ClearanceDTO clearanceDTO)
        {
            var token = await tokenService.GetToken();

            if (token != null && token.Expiration > DateTime.UtcNow)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            }

            var response = await httpClient.PostAsJsonAsync("api/Clearance/Post/", clearanceDTO);
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

        public async Task<bool> Update(ClearanceDTO clearanceDTO, int Id)
        {
            var token = await tokenService.GetToken();

            if (token != null && token.Expiration > DateTime.UtcNow)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            }
            var response = await httpClient.PutAsJsonAsync($"api/Clearance/Put/{Id}", clearanceDTO);

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
            var response = await httpClient.DeleteAsync($"api/Clearance/Delete/{Id}");

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
