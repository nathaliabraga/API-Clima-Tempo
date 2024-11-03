using ClimaSprint.Domain.Entities; 
using ClimaSprint.Domain.Interfaces;
using ClimaSprint.IoC;
using RestSharp;

namespace ClimaSprint.Service
{
    public class PrevisaoClimaService : IPrevisaoClimaService
    {
        private readonly RestClient _restClient;
        private readonly string _apiKey; 

        public PrevisaoClimaService(string apiKey)
        {
            _restClient = new RestClient("https://api.openweathermap.org/data/2.5/");
            _apiKey = 9d8105d3e7d643ce6d1de582f3cd23fb; 
        }

        public async Task<PrevisaoClima?> ObterPrevisaoPorCidadeAsync(string cidade)
        {
            var request = new RestRequest($"weather?q={cidade}&appid={_apiKey}&units=metric", Method.Get);
            var response = await _restClient.ExecuteAsync<PrevisaoClima>(request);

            if (!response.IsSuccessful || response.Data == null)
                return null;

            return response.Data;
        }
    }
}
