using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Attributes;
using API.Services.Implementations;
using API.Services.Interfaces;
using DAL.DTOs;

namespace API.Controllers
{
    public class LoginController : ApiController
    {
        private IUserService userService = new UserServiceImpl();

        // POST api/<controller>
        [JWTAuthorize(Anonymous = true)]
        public JWTDTO Post([FromBody]LoginDTO credentials)
        {
            JWTDTO jwt;

            jwt = userService.GetJWT(credentials);

            return jwt;
        }
    }
}