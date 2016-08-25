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
using DAL.Helpers;

namespace API.Controllers
{
    public class LoginController : ApiController
    {
        private IUserService userService = new UserServiceImpl();

        // GET api/<controller>
        public UserDTO Get()
        {
            UserDTO user = null;
            string token;

            var auth = Request.Headers.Authorization;

            if (CheckHelper.IsFilled(auth) && CheckHelper.IsFilled(auth.Scheme))
            {
                token = auth.Scheme;
                user = userService.GetLoggedUser(token);
            }

            return user;
        }

        // POST api/<controller>
        public JWTDTO Post([FromBody]LoginDTO credentials)
        {
            JWTDTO jwt;

            jwt = userService.GetJWT(credentials);

            return jwt;
        }
    }
}