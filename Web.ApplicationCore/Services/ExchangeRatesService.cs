using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OnlineBankingPrism.SharedEntities.Entities;

namespace Web.ApplicationCore.Services
{
    public class ExchangeRatesService
    {
        public async Task<List<NbuExchangeRates>> GetNbuExchangeRates()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json");
            var message = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<NbuExchangeRates>>(message);
            return result;
        }
    }
}
