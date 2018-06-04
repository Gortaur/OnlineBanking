using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OnlineBanking.Web.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public async Task AuthorizeWithoutCredentials_NotAuthorized()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:4444/exchangeRates");
            Assert.True(!response.IsSuccessStatusCode);
        }
        [Test]
        public async Task AuthorizeWithWrongCredentials_NotAuthorized()
        {
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes("1user:84103f56b825f0b37c52d8e7296e1c7e8da39362469b96bd3741c3036f663d91");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var response = await client.GetAsync("http://localhost:4444/exchangeRates");
            Assert.True(!response.IsSuccessStatusCode);
        }
        [Test]
        public async Task AuthorizeWithCorrectCredentials_Ok()
        {
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes("user:84103f56b825f0b37c52d8e7296e1c7e8da39362469b96bd3741c3036f663d91");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var response = await client.GetAsync("http://localhost:4444/exchangeRates");
            Assert.True(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task RetrieveUnExistingUser_Null()
        {
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes("user:84103f56b825f0b37c52d8e7296e1c7e8da39362469b96bd3741c3036f663d91");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var response = await client.GetAsync("http://localhost:4444/user?login=1");
            Assert.True(response.StatusCode == HttpStatusCode.NoContent);
        }
        [Test]
        public async Task RetrieveExistingUser_NotNull()
        {
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes("user:84103f56b825f0b37c52d8e7296e1c7e8da39362469b96bd3741c3036f663d91");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var response = await client.GetAsync("http://localhost:4444/user?login=user");
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async Task GetExchangeRates_Ok()
        {
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes("user:84103f56b825f0b37c52d8e7296e1c7e8da39362469b96bd3741c3036f663d91");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var response = await client.GetAsync("http://localhost:4444/exchangeRates");
            Assert.True(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task CorrectTransferMoney_Ok()
        {
            var json =
                "{\"sourceCard\": \"1111222233334444\",\"destination\": \"9999888877776666\",\"transactionSum\": 0,\"date\": \"2018-06-04T17:41:09.105Z\",\"transactionType\": 1,\"transactionDestinationRole\": 0}";
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes("user:84103f56b825f0b37c52d8e7296e1c7e8da39362469b96bd3741c3036f663d91");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            HttpContent contentPost = new StringContent(/*JsonConvert.SerializeObject(transaction)*/json, Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync("http://localhost:4444/user/transfer", contentPost);
            Assert.True(response.IsSuccessStatusCode);
        }
        [Test]
        public async Task NotCorrectTransferMoney_InternalServerError()
        {
            var json =
                "{\"sourceCard\": \"2111222233334444\",\"destination\": \"9999888877776666\",\"transactionSum\": 0,\"date\": \"2018-06-04T17:41:09.105Z\",\"transactionType\": 1,\"transactionDestinationRole\": 0}";
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes("user:84103f56b825f0b37c52d8e7296e1c7e8da39362469b96bd3741c3036f663d91");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            HttpContent contentPost = new StringContent(/*JsonConvert.SerializeObject(transaction)*/json, Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync("http://localhost:4444/user/transfer", contentPost);
            Assert.True(response.StatusCode == HttpStatusCode.InternalServerError);
        }
        [Test]
        public async Task CorrectReplenishMoney_Ok()
        {
            var json =
                "{\"sourceCard\": \"1111222233334444\",\"destination\": \"+380971518658\",\"transactionSum\": 0,\"date\": \"2018-06-04T17:41:09.105Z\",\"transactionType\": 0,\"transactionDestinationRole\": 0}";
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes("user:84103f56b825f0b37c52d8e7296e1c7e8da39362469b96bd3741c3036f663d91");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            HttpContent contentPost = new StringContent(json, Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync("http://localhost:4444/user/replenish", contentPost);
            Assert.True(response.IsSuccessStatusCode);
        }
        [Test]
        public async Task NotCorrectReplenish_InternalServerError()
        {
            var json =
                "{\"sourceCard\": \"2111222233334444\",\"destination\": \"+380971518658\",\"transactionSum\": 0,\"date\": \"2018-06-04T17:41:09.105Z\",\"transactionType\": 0,\"transactionDestinationRole\": 0}";
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes("user:84103f56b825f0b37c52d8e7296e1c7e8da39362469b96bd3741c3036f663d91");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            HttpContent contentPost = new StringContent(/*JsonConvert.SerializeObject(transaction)*/json, Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync("http://localhost:4444/user/replenish", contentPost);
            Assert.True(response.StatusCode == HttpStatusCode.InternalServerError);
        }
    }
}
