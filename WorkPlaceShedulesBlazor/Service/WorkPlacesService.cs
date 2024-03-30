using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WorkPlaceShedulesBlazor.DTO;
using WorkPlaceShedulesBlazor.Interface;
using WorkPlaceShedulesBlazor.Storage;

namespace WorkPlaceShedulesBlazor.Service
{
    public class WorkPlacesService : IWorkPlacesService
    {
        private HttpClient _httpClient;
        private readonly AutenticationExtension _authService;

        public WorkPlacesService(HttpClient http, AutenticationExtension authService)
        {
            _httpClient = http;
            _authService = authService;
        }


        public async Task<WorkPlacesDTO> FindWorkPlaces(int id)
        {
            _httpClient = await getToken(_httpClient);

            var result = await _httpClient.GetFromJsonAsync<WorkPlacesDTO>("api/WorkPlaces/" + id.ToString());
            if (result!.WorkPlaceCode != "")
            {
                return result;
            }

            throw new Exception("Error al obtener los espacios de trabajo");
        }

        public async Task<List<WorkPlacesDTO>> GetWorkPlaces()
        {
            try
            {
                _httpClient = await getToken(_httpClient);

                var result = await _httpClient.GetFromJsonAsync<List<WorkPlacesDTO>>("api/WorkPlaces");

                return result;
            }
            catch (Exception ex)
            {
            }
            return new List<WorkPlacesDTO>();

        }

        public async Task<int> SaveWorkPlaces(WorkPlacesDTO workPlace)
        {

            _httpClient = await getToken(_httpClient);

            var result = await _httpClient.PostAsJsonAsync("api/WorkPlaces", workPlace);
            var response = await result.Content.ReadFromJsonAsync<WorkPlacesDTO>();
            if (response!.WorkPlaceCode != "")
            {
                return 1;
            }

            return 0;
        }

        public async Task<int> UpdateWorkPlaces(WorkPlacesDTO WorkPlace)
        {
            _httpClient = await getToken(_httpClient);

            var result = await _httpClient.PutAsJsonAsync("api/WorkPlaces/UpdateWorkPlaces", WorkPlace);
            var response = await result.Content.ReadFromJsonAsync<WorkPlacesDTO>();
            if (response!.WorkPlaceCode != "")
            {
                return 1;
            }

            return 0;
        }


        public async Task<bool> DeleteWorkPlaces(int id)
        {
            _httpClient = await getToken(_httpClient);

            var result = await _httpClient.DeleteAsync("api/WorkPlaces/DeleteWorkPlaces?id=" + id.ToString());
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
