using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OnlineBankingPrism.Constants;
using OnlineBankingPrism.SharedEntities.Entities;

namespace OnlineBankingPrism.RequestManager
{
    public static class RequestManager
    {
        public static async Task<ApplicationUser> GetAuthenticatedUser(String user)
        {
            var url = RequestUrls.GetUserUrl + $"?login={user}";
            var response = await HttpClientInstance.Client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var applicationUser = JsonConvert.DeserializeObject<ApplicationUser>(content);
            return applicationUser;
        }
        public static async Task<Boolean> PostTransfer(Transaction transaction)
        {
            HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(transaction), Encoding.UTF8,
                "application/json");
            var response = await HttpClientInstance.Client.PostAsync(RequestUrls.GetTransferUrl, contentPost);
            return response.IsSuccessStatusCode;
        }
        public static async Task<Boolean> PostReplenishment(Transaction transaction)
        {
            HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(transaction), Encoding.UTF8,
                "application/json");
            var response = await HttpClientInstance.Client.PostAsync(RequestUrls.GetReplenishUrl, contentPost);
            return response.IsSuccessStatusCode;
        }
        public static async Task<List<NbuExchangeRates>> GetExchangeRates()
        {
            var response = await HttpClientInstance.Client.GetAsync(RequestUrls.ExchangeRatesUrl).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<NbuExchangeRates>>(content);
            return result;
        }
    }
}