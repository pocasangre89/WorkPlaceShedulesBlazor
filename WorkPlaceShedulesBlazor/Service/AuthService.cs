using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using WorkPlaceShedulesBlazor.DTO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace WorkPlaceShedulesBlazor.Service
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public string AccessToken { get; private set; }

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TokenDTO> LoginAsync(string _email, string _password)
        {
            // Realizar la lógica de inicio de sesión y obtener el token
            // Por ejemplo:

            var loginModel = new { email = _email, password = _password };
            var content = new StringContent(JsonSerializer.Serialize(loginModel), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, _httpClient.BaseAddress + "api/login");
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            //response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<TokenDTO>();

                if(responseContent!.result != null)
                {
                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                    // Deserializar el token JWT
                    var jwtToken = tokenHandler.ReadJwtToken(responseContent!.result);

                    // Obtener los claims del token
                    var claims = jwtToken.Claims;
                    string Rol = claims.FirstOrDefault(c => c.Type == "role").Value.ToString();

                    responseContent.Rol = Rol;

                }


                return responseContent;
            }
            else
            {
                return new TokenDTO();
            }
        }
    }
}
