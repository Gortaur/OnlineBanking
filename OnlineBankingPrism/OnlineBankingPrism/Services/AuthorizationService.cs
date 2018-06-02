using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OnlineBanking.Security;
using OnlineBankingPrism.Constants;
using OnlineBankingPrism.RequestManager;

namespace OnlineBankingPrism.Services
{
    internal class AuthorizationService
    {
        public static async Task<Boolean> Authorize(string login, string password)
        {
            HttpClientInstance.Client = new HttpClient();
            var encodedPassword = PasswordEncoder.GetHash(password);
            var byteArray = Encoding.ASCII.GetBytes($"{login}:{encodedPassword}");
            HttpClientInstance.Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            return await SetCurrentUser(login);
        }

        private static async Task<Boolean> SetCurrentUser(string login)
        {
            SharedApplicationData.CurrentUser =
                await RequestManager.RequestManager.GetAuthenticatedUser(login);
            SharedApplicationData.ExchangeRates = await RequestManager.RequestManager.GetExchangeRates();
            return SharedApplicationData.CurrentUser != null;
        }
    }
}
