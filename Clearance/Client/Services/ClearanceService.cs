﻿using Clearance.Client.Authentication;
using Clearance.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Clearance.Client.Services
{
    public class ClearanceService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private readonly NavigationManager navigationManager;

        public ClearanceService(HttpClient httpClient, ITokenService tokenService,NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
            this.navigationManager = navigationManager;
        }

        public async Task<List<ClearanceDTO>?> GetAll()
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
            return await httpClient.GetFromJsonAsync<List<ClearanceDTO>>("api/Clearance/GetAll");
        }


        public async Task<List<ClearanceDTO>?> GetAllByUserId(Guid Id)
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
            return await httpClient.GetFromJsonAsync<List<ClearanceDTO>>($"api/Clearance/GetAllByUserId/{Id}");
        }
        public async Task<List<ClearanceDirectionsDTO>?> GetAllByDirection(Guid Id)
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
            return await httpClient.GetFromJsonAsync<List<ClearanceDirectionsDTO>>($"api/Clearance/GetAllByDirection/{Id}");
        }
        public async Task<List<ClearanceDirectionsDTO>?> GetByclId(int Id)
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
            return await httpClient.GetFromJsonAsync<List<ClearanceDirectionsDTO>>($"api/Clearance/GetByclId/{Id}");
        }

        public async Task<List<ClearanceDirectionsDTO>?> GetAllByDirectionState(Guid Id, bool state)
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
            return await httpClient.GetFromJsonAsync<List<ClearanceDirectionsDTO>>($"api/Clearance/GetAllByDirectionState/{Id}/{state}");
        }

        public async Task<ClearanceDTO?> GetById(int Id)
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

        public async Task<ClearanceDirectionsDTO?> GetByDirectionId(int Id)
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
            var response = await httpClient.GetAsync($"api/Clearance/GetByDirectionId/{Id}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<ClearanceDirectionsDTO>();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ClearanceDTO>?> Search(string Name)
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
            return await httpClient.GetFromJsonAsync<List<ClearanceDTO>>($"api/Clearance/Search/{Name}");
        }

        public async Task<List<ClearanceDTO>?> Search(Guid Id, string Name)
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
            return await httpClient.GetFromJsonAsync<List<ClearanceDTO>>($"api/Clearance/Search/{Id}/{Name}");
        }

        public async Task<List<ClearanceDirectionsDTO>?> SearchbyDirection(Guid Id, string Name)
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
            return await httpClient.GetFromJsonAsync<List<ClearanceDirectionsDTO>>($"api/Clearance/SearchbyDirection/{Id}/{Name}");
        }
        public async Task<List<ClearanceDirectionsDTO>?> SearchbyDirectionState(Guid Id, string Name, bool state)
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
            return await httpClient.GetFromJsonAsync<List<ClearanceDirectionsDTO>>($"api/Clearance/SearchbyDirectionState/{Id}/{Name}/{state}");
        }


        public async Task<bool> Insert(ClearanceDTO clearanceDTO)
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
            else
            {
                navigationManager.NavigateTo("login", true);
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

        public async Task<bool> RenewOrder(ClearanceDTO clearanceDTO, int Id)
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
            var response = await httpClient.PutAsJsonAsync($"api/Clearance/RenewOrder/{Id}", clearanceDTO);

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

        public async Task<bool> UpdateDirection(ClearanceDirectionsDTO clearanceDirectionsDTO, int Id,Guid userId)
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
            var response = await httpClient.PutAsJsonAsync($"api/Clearance/PutDirection/{Id}/{userId}", clearanceDirectionsDTO);

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
