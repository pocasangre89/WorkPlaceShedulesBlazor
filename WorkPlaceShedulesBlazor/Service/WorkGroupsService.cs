using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WorkPlaceShedulesBlazor.DTO;
using WorkPlaceShedulesBlazor.Interface;
using WorkPlaceShedulesBlazor.Storage;

namespace WorkGroupshedulesBlazor.Service
{
    public class WorkGroupsService : IWorkGroupsService
    {
        private HttpClient _httpClient;
        private readonly AutenticationExtension _authService;

        public WorkGroupsService(HttpClient http, AutenticationExtension authService)
        {
            _httpClient = http;
            _authService = authService;
        }


        public async Task<WorkGroupsDTO> FindWorkGroups(int id)
        {
            _httpClient = await getToken(_httpClient);

            var result = await _httpClient.GetFromJsonAsync<WorkGroupsDTO>("api/WorkGroups/" + id.ToString());
            if (result!.GroupName != "")
            {
                return result;
            }

            throw new Exception("Error al obtener los espacios de trabajo");
        }

        public async Task<List<WorkGroupsDTO>> GetWorkGroups()
        {
            try
            {
                _httpClient = await getToken(_httpClient);

                var result = await _httpClient.GetFromJsonAsync<List<WorkGroupsDTO>>("api/WorkGroups");

                return result;
            }
            catch (Exception ex)
            {
                string aa = ex.ToString();
                aa = "";
            }
            return new List<WorkGroupsDTO>();

        }

        public async Task<int> SaveWorkGroups(WorkGroupsDTO workGroup)
        {

            _httpClient = await getToken(_httpClient);

            var result = await _httpClient.PostAsJsonAsync("api/WorkGroups", workGroup);
            var response = await result.Content.ReadFromJsonAsync<WorkGroupsDTO>();
            if (response!.GroupName != "")
            {
                return 1;
            }

            return 0;
        }

        public async Task<int> UpdateWorkGroups(WorkGroupsDTO workGroup)
        {
            _httpClient = await getToken(_httpClient);

            var result = await _httpClient.PutAsJsonAsync("api/WorkGroups/UpdateWorkGroups", workGroup);
            var response = await result.Content.ReadFromJsonAsync<WorkGroupsDTO>();
            if (response!.GroupName != "")
            {
                return 1;
            }

            return 0;
        }


        public async Task<bool> DeleteWorkGroups(int id)
        {
            _httpClient = await getToken(_httpClient);

            var result = await _httpClient.DeleteAsync("api/WorkGroups/DeleteWorkGroups?id=" + id.ToString());
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
