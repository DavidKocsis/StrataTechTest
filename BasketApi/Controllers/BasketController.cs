using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BasketApi.Requests;
using StrataBasket;
using StrataServices.Contracts;

namespace BasketApi.Controllers
{
    public class BasketController : ApiController
    {
        private readonly IBasket _basket;

        public BasketController(IBasket basket)
        {
            _basket = basket;
        }

        [HttpPost]
        [Route("api/{basket}/authenticate")]
        public IHttpActionResult Authenticate([FromBody]AuthenticateUserRequest request)
        {
            try
            {
                return Ok(_basket.AuthenticateUser(request.Username,request.Password));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/{basket}/confirm/{userName}")]
        public IHttpActionResult Get(string username)
        {
            try
            {
                return Ok(_basket.GenerateOrder(username));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/{basket}/add/{username}")]
        public HttpResponseMessage Add(string username, ShoppingCartItem item)
        {
            try
            {
                item.Customer = username;
                _basket.Add(item);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
               return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
