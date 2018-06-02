using System;

namespace OnlineBankingPrism.Constants
{
    public static class RequestUrls
    {
        public const String BaseUrl = "http://192.168.0.101:4444";
        public static String ExchangeRatesUrl = $"{BaseUrl}/exchangeRates";
        public static String GetUserUrl = $"{BaseUrl}/user";
        public static String GetTransferUrl = $"{BaseUrl}/user/transfer";
        public static String GetReplenishUrl = $"{BaseUrl}/user/replenish";
    }
}