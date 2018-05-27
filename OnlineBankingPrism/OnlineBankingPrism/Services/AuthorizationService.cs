using System;
using System.Net;
using System.Net.Http;
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
            var encodedPassword = PasswordEncoder.GetHash(password);
            HttpClientHandler handler = new HttpClientHandler
            {
                Credentials = new NetworkCredential(login,encodedPassword),
            };
            HttpClientInstance.Client = new HttpClient(handler);
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
