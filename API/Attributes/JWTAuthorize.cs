using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using API.Constants;
using API.Services.Implementations;
using DAL.Helpers;
using Newtonsoft.Json;

namespace API.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class JWTAuthorize : AuthorizeAttribute
    {
        public string Role { get; set; }
        public string ForbiddenRole { get; set; }
        public bool Anonymous { get; set; }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool isAuthorized = false;
            string token;
            UserServiceImpl service;
            long id = -1;
            Dictionary<string, object> values;

            service = new UserServiceImpl();
            var authorization = actionContext.Request.Headers.Authorization;


            if (CheckHelper.IsFilled(authorization) && CheckHelper.IsFilled(authorization.Scheme))
            {
                token = authorization.Scheme;
                try
                {
                    string payload = JWT.JsonWebToken.Decode(token, AppConstants.JWT_SECRET_KEY);
                    values = JsonConvert.DeserializeObject<Dictionary<string, object>>(payload);
                    id = Convert.ToInt64(values["Id"]);
                    isAuthorized = service.isAuthorized(id, Role, ForbiddenRole);
                }
                catch (Exception) 
                {
                    isAuthorized = Anonymous;
                }
            }
            else 
            {
                isAuthorized = Anonymous;
            }

            return isAuthorized;
        }
    }
}