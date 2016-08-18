using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using API.Constants;
using API.Services.Implementations;
using Newtonsoft.Json;

namespace API.Attributes
{
    public class JWTAuthorize : AuthorizeAttribute
    {
        public string Role { get; set; }
        public string ForbiddenRole { get; set; }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool isAuthorized;
            string token;
            UserServiceImpl service;
            long id = -1;
            Dictionary<string, object> values;

            service = new UserServiceImpl();
            
            token = actionContext.Request.Headers.Authorization.Scheme;
            string payload = JWT.JsonWebToken.Decode(token, AppConstants.JWT_SECRET_KEY);           

            try
            {
                values = JsonConvert.DeserializeObject<Dictionary<string, object>>(payload);
                id = Convert.ToInt64(values["Id"]);    
                isAuthorized = service.isAuthorized(id, Role, ForbiddenRole);
            }
            catch (Exception)
            {
                isAuthorized = false;
            }            

            return isAuthorized;
        }
    }
}