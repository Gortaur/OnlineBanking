using System.Net.Http;

namespace OnlineBankingPrism.RequestManager
{
    public static class HttpClientInstance
    {
        public static HttpClient Client
        {
            get
            {
                _restClient = _restClient ?? new HttpClient();
                return _restClient;
            }
            set => _restClient = value;
        }

        private static HttpClient _restClient;
    }
}
