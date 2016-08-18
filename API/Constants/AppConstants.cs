using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Constants
{
    public class AppConstants
    {
        //user roles
        public const string ADMINISTRATOR = "Administrator";
        public const string MANAGER = "Manager";
        public const string STUDENT = "Student";

        //string for JWT encryption
        public const string JWT_SECRET_KEY = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
    }
}