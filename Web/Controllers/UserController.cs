using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using OnlineBankingPrism.SharedEntities.Entities;
using Web.ApplicationCore.Services;
using Web.FIlters;

namespace Web.Controllers
{
    [AuthenticationFilter]
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        public UserController()
        {
            _userService = new UserService();
        }

        [HttpGet, Route]
        public async Task<IHttpActionResult> GetUser(String login)
        {
            var user = await _userService.GetUser(login);
            if (user == null)
            {
                return new StatusCodeResult(HttpStatusCode.NoContent,this);
            }

            return Ok(user);
        }

        [HttpPost, Route("replenish")]
        public async Task<IHttpActionResult> ReplenishMoney(Transaction transaction)
        {
            if (await _userService.ReplenishMoney(transaction))
            {
                return Ok(true);
            }

            return InternalServerError();
        }
        [HttpPost, Route("transfer")]
        public async Task<IHttpActionResult> TransferMoney(Transaction transaction)
        {
            if (await _userService.TransferMoney(transaction))
            {
                return Ok(true);
            }

            return InternalServerError();
        }

        private readonly UserService _userService;
    }
}