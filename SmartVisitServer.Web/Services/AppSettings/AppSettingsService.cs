using Application.Features.AppSettings.Commands.Create;
using Application.Features.AppSettings.Commands.Delete;
using Application.Features.AppSettings.Commands.Update;
using Application.Features.AppSettings.Queries.GetAll;
using Application.Features.AppSettings.Queries.GetById;
using Newtonsoft.Json;
using System.Text;

namespace SmartVisitServer.Web.Services.AppSettings
{
    public class AppSettingsService : IAppSettingsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl = "http://localhost:5011/api/AppSettings";
        //http://localhost:5011/api/AppSettings/GetAll

        public AppSettingsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CreateAppSettingCommandResponse> AppSettingCreateAsync(CreateAppSettingCommandRequest request)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/Create";
            var jsonContent = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<CreateAppSettingCommandResponse>(responseData);

            return pagedResponse!;
        }

        //http://localhost:5011/api/AppSettings/Delete?Id=2bda8663-fefa-4c09-ad95-040a42251bf4 
        public async Task<DeleteAppSettingCommandResponse> AppSettingDeleteAsync(Guid id)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/Delete/{id}"; 
            var response = await client.DeleteAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri silinirken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var deletedResponse = JsonConvert.DeserializeObject<DeleteAppSettingCommandResponse>(responseData);
            return deletedResponse!;
        }

        public async Task<UpdateAppSettingsCommandResponse> AppSettingUpdateAsync(UpdateAppSettingsCommandRequest request)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/Update";  
            var jsonContent = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(apiUrl, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri güncellenirken hata oluştu: {response.StatusCode}, {errorContent}");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var updatedResponse = JsonConvert.DeserializeObject<UpdateAppSettingsCommandResponse>(responseData);

            return updatedResponse!;
        }

        public async Task<AppSettingGetAllQueryResponse> GetAllAppSettingGetAllQueryAsync()
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/GetAll";
            var response = await client.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<AppSettingGetAllQueryResponse>(responseData);
            return pagedResponse!;
        }        
        public async Task<AppSettingGetByIdQueryResponse> GetAppSettingGetByIdAsync(Guid id)
        {
            var client = _httpClientFactory.CreateClient("SmartVisit");
            var apiUrl = $"{_apiUrl}/Id? Id={id}";
            var response = await client.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API'den veri alınırken hata oluştu: {response.StatusCode}, {errorContent}");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<AppSettingGetByIdQueryResponse>(responseData);
            return pagedResponse!;
        }
    }
}
