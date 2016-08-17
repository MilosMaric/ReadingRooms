using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL;
using DAL.DTOs;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using AttributeRouting;
using AttributeRouting.Web.Http;
using API.Services.Implementations;
using API.Services.Interfaces;

namespace API.Controllers
{
    public class UniController : ApiController
    {
        private IUniversityService uniService = new UniversityServiceImpl();

        // GET api/<controller>
        public List<UniversityDTO> Get()
        {
            return uniService.GetAll();
        }

        // GET api/<controller>/5
        public UniversityDTO Get(int id)
        {
            return uniService.GetById(id);
        }

        [GET("api/uni/{uniId}/faculties")]
        public List<FacultyDTO> GetFaculties(int uniId)
        {
            return null;
        }

        [GET("api/uni/{uniId}/rroom")]
        public List<ReadingRoomDTO> GetUniReadingRoom(int uniId)
        {
            return null;
        }

        // POST api/<controller>
        public UniversityDTO Post([FromBody] UniversityDTO uniDTO)
        {
            UniversityDTO retVal = uniService.Add(uniDTO);
            return retVal;
        }

        // PUT api/<controller>/5
        public UniversityDTO Put(int id, [FromBody]UniversityDTO uniDTO)
        {
            UniversityDTO retVal = uniService.Update(id, uniDTO);
            return retVal;
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {            
            uniService.Delete(id);
        }
    }
}