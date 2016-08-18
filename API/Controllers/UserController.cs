using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Attributes;
using API.Constants;
using API.Services.Implementations;
using API.Services.Interfaces;
using AttributeRouting.Web.Http;
using DAL.DTOs;

namespace API.Controllers
{
    public class UserController : ApiController
    {
        private IUserService userService = new UserServiceImpl(); 

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }
        
        // POST api/<controller>
        [JWTAuthorize(Role = AppConstants.ADMINISTRATOR)]
        public void Post([FromBody]UserDTO dto)
        {
            var a = "asd";
        }
        
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
            var a = "asd";
        }

        // DELETE api/<controller>/5
        [JWTAuthorize(ForbiddenRole = AppConstants.STUDENT)]
        public void Delete(int id)
        {
            var a = "asd";
        }
    }
}