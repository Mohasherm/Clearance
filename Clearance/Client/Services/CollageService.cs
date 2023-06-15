using Clearance.Client.Authentication;
using Clearance.Shared;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Clearance.Client.Services
{
    public class CollageService
    {
        private readonly HttpClient httpClient;

        public CollageService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<CollageDTO>?> GetAll()
        {
            //var token = await tokenService.GetToken();

            //if (token != null && token.Expiration > DateTime.Now)
            //{
            //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            //}
            return await httpClient.GetFromJsonAsync<List<CollageDTO>>("api/Collage/GetAll");
        }
        public async Task<List<CollageDTO>?> Search(string? Name)
        {
            //var token = await tokenService.GetToken();

            //if (token != null && token.Expiration > DateTime.Now)
            //{
            //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            //}
            return await httpClient.GetFromJsonAsync<List<CollageDTO>>($"api/Collage/Search/{Name}");
        }

        public async Task<CollageDTO?> GetById(int Id)
        {
            //var token = await tokenService.GetToken();

            //if (token != null && token.Expiration > DateTime.Now)
            //{
            //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            //}
            var response = await httpClient.GetAsync($"api/Collage/GetById/{Id}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<CollageDTO>();
            }
            else
            {
                return null;
            }
        }


        public async Task<bool> Insert(CollageDTO collageDTO)
        {
            //var token = await tokenService.GetToken();

            //if (token != null && token.Expiration > DateTime.Now)
            //{
            //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            //}

            var response = await httpClient.PostAsJsonAsync("api/Collage/Post/", collageDTO);
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

        public async Task<bool> Update(CollageDTO collageDTO, int Id)
        {
            //var token = await tokenService.GetToken();

            //if (token != null && token.Expiration > DateTime.Now)
            //{
            //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            //}
            var response = await httpClient.PutAsJsonAsync($"api/Collage/Put/{Id}", collageDTO);

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
            //var token = await tokenService.GetToken();

            //if (token != null && token.Expiration > DateTime.Now)
            //{
            //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            //}
            var response = await httpClient.DeleteAsync($"api/Collage/Delete/{Id}");

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
