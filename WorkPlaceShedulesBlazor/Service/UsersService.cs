using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WorkPlaceShedulesBlazor.DTO;
using WorkPlaceShedulesBlazor.Interface;
using WorkPlaceShedulesBlazor.Storage;

namespace WorkPlaceShedulesBlazor.Service
{
    public class UsersService : IUsersService
    {
        private HttpClient _httpClient;
        private readonly AutenticationExtension _authService;

        public UsersService(HttpClient http, AutenticationExtension authService)
        {
            _httpClient = http;
            _authService = authService;
        }


        public async Task<UsersDTO> FindUsers(int id)
        {
            _httpClient = await getToken(_httpClient);

            var result = await _httpClient.GetFromJsonAsync<UsersDTO>("api/Users/" + id.ToString());
            if (result!.UserCode != "")
            {
                return result;
            }

            throw new Exception("Error al obtener los usuarios");
        }

        public async Task<List<UsersDTO>> GetUsers()
        {
            try
            {
                _httpClient = await getToken(_httpClient);

                var result = await _httpClient.GetFromJsonAsync<List<UsersDTO>>("api/Users");

                return result;
            }
            catch (Exception ex)
            {
            }
            return new List<UsersDTO>();

        }

        public async Task<int> SaveUsers(UsersDTO users)
        {

            _httpClient = await getToken(_httpClient);

            var result = await _httpClient.PostAsJsonAsync("api/Users", users);
            var response = await result.Content.ReadFromJsonAsync<UsersDTO>();
            if (response!.UserCode != "")
            {
                return 1;
            }

            return 0;
        }

        public async Task<int> UpdateUsers(UsersDTO users)
        {
            _httpClient = await getToken(_httpClient);

            var result = await _httpClient.PutAsJsonAsync("api/Users/UpdateUsuario", users);
            var response = await result.Content.ReadFromJsonAsync<UsersDTO>();
            if (response!.UserCode != "")
            {
                return 1;
            }

            return 0;
        }


        public async Task<bool> DeleteUsers(int id)
        {
            _httpClient = await getToken(_httpClient);

            var result = await _httpClient.DeleteAsync("api/Users/DeleteUsuario?id=" + id.ToString());
            var response = await result.Content.ReadFromJsonAsync<int>();
            if (response == 1)
            {
                return true;
            }

            return false;
        }

        private async Task<HttpClient> getToken(HttpClient _httpClient)
        {

            AuthenticationState token = await _authService.GetAuthenticationStateAsync();
            if (token != null)
            {

                string tok = token.User.Claims.FirstOrDefault(c => c.Type == "Token").Value.ToString();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tok);
            }
            return _httpClient;
        }
    }
}
