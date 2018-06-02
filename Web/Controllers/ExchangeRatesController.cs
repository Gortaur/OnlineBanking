using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Web.ApplicationCore.Services;
using Web.FIlters;

namespace Web.Controllers
{
    [AuthenticationFilter]
    [RoutePrefix("exchangeRates")]
    public class ExchangeRatesController : ApiController
    {
        public ExchangeRatesController()
        {
            _exchangeRatesService = new ExchangeRatesService();
        }
        [HttpGet,Route]
        public async Task<IHttpActionResult> GetExchangeRates()
        {
            var result = await _exchangeRatesService.GetNbuExchangeRates();
            if (result == null)
            {
                return new StatusCodeResult(HttpStatusCode.NoContent,this);
            }
            return Ok(result);
        }

        private readonly ExchangeRatesService _exchangeRatesService;
    }
}