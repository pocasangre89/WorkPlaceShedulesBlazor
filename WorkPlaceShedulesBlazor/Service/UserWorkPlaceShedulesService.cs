using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using WorkPlaceShedulesBlazor.DTO;
using WorkPlaceShedulesBlazor.Interface;
using WorkPlaceShedulesBlazor.Storage;

namespace WorkPlaceShedulesBlazor.Service
{
    public class UserWorkPlaceShedulesService : IUserWorkPlaceShedulesService
    {
        private HttpClient _httpClient;
        private readonly AutenticationExtension _authService;

        public UserWorkPlaceShedulesService(HttpClient http, AutenticationExtension authService)
        {
            _httpClient = http;
            _authService = authService;
        }

        //public async Task<UsersDTO> FindUsers(int id)
        //{
        //    _httpClient = await getToken(_httpClient);

        //    var result = await _httpClient.GetFromJsonAsync<UsersDTO>("api/Users/" + id.ToString());
        //    if (result!.UserCode != "")
        //    {
        //        return result;
        //    }

        //    throw new Exception("Error al obtener los usuarios");
        //}

        public async Task<List<UserWorkPlaceShedulesDTO>> GetUserWorkPlaceShedules()
        {
            try
            {
                _httpClient = await getToken(_httpClient);

                var result = await _httpClient.GetFromJsonAsync<List<UserWorkPlaceShedulesDTO>>("api/UsersWorkPlaceShedules");

                return result;
            }
            catch (Exception ex)
            {
            }
            return new List<UserWorkPlaceShedulesDTO>();

        }

        public async Task<int> SaveUserWorkPlaceShedules(UserWorkPlaceShedulesDTO userWorkPlaceShedules)
        {

            _httpClient = await getToken(_httpClient);
            
            userWorkPlaceShedules.UserId = await getUserId();
            var result = await _httpClient.PostAsJsonAsync("api/UsersWorkPlaceShedules", userWorkPlaceShedules);
            var response = await result.Content.ReadFromJsonAsync<int>();
            if (response != 0)
            {
                return 1;
            }

            return 0;
        }

        //public async Task<int> UpdateUsers(UsersDTO users)
        //{
        //    _httpClient = await getToken(_httpClient);

        //    var result = await _httpClient.PutAsJsonAsync("api/Users/UpdateUsuario", users);
        //    var response = await result.Content.ReadFromJsonAsync<UsersDTO>();
        //    if (response!.UserCode != "")
        //    {
        //        return 1;
        //    }

        //    return 0;
        //}


        //public async Task<bool> DeleteUsers(int id)
        //{
        //    _httpClient = await getToken(_httpClient);

        //    var result = await _httpClient.DeleteAsync("api/Users/DeleteUsuario?id=" + id.ToString());
        //    var response = await result.Content.ReadFromJsonAsync<int>();
        //    if (response == 1)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

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

        private async Task<int> getUserId()
        {

            AuthenticationState UserData = await _authService.GetAuthenticationStateAsync();
            if (UserData != null)
            {

                string respData = UserData.User.Claims.FirstOrDefault(c => c.Type == "Token").Value.ToString();


                var handler = new JwtSecurityTokenHandler();
                var tokenSplits = respData.Split('.');
                JwtSecurityToken gg = handler.ReadJwtToken(respData);
                string aaaa = gg.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
                try
                {
                    
                        return int.Parse(aaaa.ToString());
                    
                }
                catch(Exception ex)
                {
                    return 0;
                }
            }
            return 0;
        }
    }
    public class UsuarioDTO
    {
        public string Email { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public long Nbf { get; set; }
        public long Exp { get; set; }
        public long Iat { get; set; }
        public string Iss { get; set; }
        public string Aud { get; set; }
    }
}
