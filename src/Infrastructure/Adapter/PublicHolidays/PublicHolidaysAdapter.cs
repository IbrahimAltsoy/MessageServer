using Application.Services.PublicHolidaysService;
using Core.Application.Pipelines.Caching;
using Domain.AdapterModels;
using Newtonsoft.Json;

namespace Infrastructure.Adapter.PublicHolidays;

public class PublicHolidaysAdapter : IPublicHolidaysService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PublicHolidaysAdapter(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ResultPublicHolidays> PublicHolidaysAsync()
    {
        try
        {
            //HttpClient httpClient = new HttpClient();
            //string apiUrl = "https://api.ubilisim.com/resmitatiller/";
            //HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            var client = _httpClientFactory.CreateClient("PublicHolidays");
            var response = await client.GetAsync("");

            string jsonResponse = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(jsonResponse);
            var data = JsonConvert.DeserializeObject<ResultPublicHolidays>(jsonResponse);
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}