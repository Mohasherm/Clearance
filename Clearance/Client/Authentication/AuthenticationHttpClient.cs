using Clearance.Shared;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Clearance.Client.Authentication
{
    public class AuthenticationHttpClient
    {
        private readonly ILogger<AuthenticationHttpClient> logger;
        private readonly HttpClient http;
        private readonly ITokenService tokenService;
        private readonly CustomAuthenticationStateProvider myAuthenticationStateProvider;

        public AuthenticationHttpClient(ILogger<AuthenticationHttpClient> logger,
            HttpClient http,
            ITokenService tokenService,
            CustomAuthenticationStateProvider myAuthenticationStateProvider)
        {
            this.logger = logger;
            this.http = http;
            this.tokenService = tokenService;
            this.myAuthenticationStateProvider = myAuthenticationStateProvider;
        }

        public async Task<UserRegisterResultDTO> RegisterUser(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                //var token = await tokenService.GetToken();

                //if (token != null && token.Expiration > DateTime.Now)
                //{
                //    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
                //}

                var response = await http.PostAsJsonAsync("api/User/register", userRegisterDTO);
                var result = await response.Content.ReadFromJsonAsync<UserRegisterResultDTO>();

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return new UserRegisterResultDTO
                {
                    Succeeded = false,
                    Errors = new List<string>()
                    {
                        "Sorry, we were unable to register you at this time. Please try again shortly."
                    }
                };
            }
        }

        public async Task<UserLoginResultDTO> LoginUser(UserLoginDTO userLoginDTO)
        {
            try
            {
                var response = await http.PostAsJsonAsync("api/User/login", userLoginDTO);
                var result = await response.Content.ReadFromJsonAsync<UserLoginResultDTO>();
                await tokenService.SetToken(result.Token);
                myAuthenticationStateProvider.StateChanged();
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return new UserLoginResultDTO
                {
                    Succeeded = false,
                    Message = "Sorry, we were unable to log you in at this time. Please try again shortly."
                };
            }
        }

        public async Task LogoutUser()
        {
            await tokenService.RemoveToken();
            myAuthenticationStateProvider.StateChanged();
        }


        public async Task<bool> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            //var token = await tokenService.GetToken();

            //if (token != null && token.Expiration > DateTime.Now)
            //{
            //    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
            //}

            var response = await http.PostAsJsonAsync("api/User/ChangePassword", changePasswordDTO);
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

        public async Task<UserDTO?> GetUser(string Email)
        {
            var response = await http.GetAsync($"api/User/GetUser/{Email}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<UserDTO>();
            }
            else
            {
                return null;
            }
        }
        public async Task<UserDTO?> GetUserById(string Id)
        {
            var response = await http.GetAsync($"api/User/GetUserById/{new Guid(Id)}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<UserDTO>();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<UserDTO>?> GetUsers()
        {
            var response = await http.GetAsync($"api/User/GetUsers");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<List<UserDTO>>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IList<string>?> GetRoleForUser(Guid Id)
        {
            var response = await http.GetAsync($"api/User/GetRoleForUser/{Id}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent ||
                    response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<IList<string>>();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<UserDTO>?> GetUserRoles(string RoleName)
        {

            var response = await http.GetAsync($"api/User/GetUserRoles/{RoleName}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<List<UserDTO>>();
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> SetRole(UserRolesDTO userRolesDTO)
        {
            var response = await http.PostAsJsonAsync("api/User/SetUserRole", userRolesDTO);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
            else
                return false;
        }

        public async Task<bool> Update(UserDTO userDTO, Guid Id)
        {
            if (userDTO.DirectionName is null)
            {
                userDTO.DirectionName = string.Empty;
            }
            var response = await http.PutAsJsonAsync($"api/User/PutUser/{Id}", userDTO);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return false;
            }
            else
                return false;
        }

        public async Task<bool> Delete(Guid Id)
        {
            var response = await http.DeleteAsync($"api/User/Delete/{Id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
            else
                return false;
        }

        public async Task<List<UserDTO>?> Search(string? Name)
        {
            return await http.GetFromJsonAsync<List<UserDTO>>($"api/User/Search/{Name}");
        }
    }
}
