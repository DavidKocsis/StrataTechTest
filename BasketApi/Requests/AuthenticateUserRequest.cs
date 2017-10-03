using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketApi.Requests
{
    public class AuthenticateUserRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}