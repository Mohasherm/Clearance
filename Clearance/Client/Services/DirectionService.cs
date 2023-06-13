using Clearance.Shared;
using System.Net.Http.Json;

namespace Clearance.Client.Services
{
    public class DirectionService
    {
        private readonly HttpClient httpClient;

        public DirectionService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<DirectionDTO>?> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<DirectionDTO>>("api/Direction/GetAll");
        }
         public async Task<List<DirectionDTO>?> Search(string? Name)
        {
            return await httpClient.GetFromJsonAsync<List<DirectionDTO>>($"api/Direction/Search/{Name}");
        }

        public async Task<DirectionDTO?> GetById(int Id)
        {
            var response = await httpClient.GetAsync($"api/Direction/GetById/{Id}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<DirectionDTO>();
            }
            else
            {
                return null;
            }
        }


        public async Task<bool> Insert(DirectionDTO directionDTO)
        {
            var response = await httpClient.PostAsJsonAsync("api/Direction/Post/", directionDTO);
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

        public async Task<bool> Update(DirectionDTO directionDTO, int Id)
        {
            var response = await httpClient.PutAsJsonAsync($"api/Direction/Put/{Id}", directionDTO);

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
            var response = await httpClient.DeleteAsync($"api/Direction/Delete/{Id}");

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
