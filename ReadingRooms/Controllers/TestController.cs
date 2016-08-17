using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;

namespace ReadingRooms.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public string Add()
        {
            IUniversityRepository repo = new UniversityRepositoryImpl();
            UNIVERSITY uni = new UNIVERSITY()
            {
                UNI_CITY = "Novi Sad",
                UNI_NAME = "Univerzitet u Novom Sadu"
            };

            repo.Add(uni);
            return "Ok";
        }

    }
}
