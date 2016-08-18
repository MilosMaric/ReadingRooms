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
    public class FacultyController : ApiController
    {
        private IFacultyService facultyService = new FacultyServiceImpl();

        // GET api/<controller>
        public List<FacultyDTO> Get()
        {
            return facultyService.GetAll();
        }

        // GET api/<controller>/5
        public FacultyDTO Get(int id)
        {
            return facultyService.GetById(id);
        }

        [GET("api/faculty/{facId}/rrooms")]
        public List<ReadingRoomDTO> GetFacReadingRooms(int facId)
        {
            return facultyService.GetReadingRooms(facId);
        }

        [GET("api/faculty/{facId}/blogs")]
        public List<BlogDTO> GetFacBlogs(int facId)
        {
            return facultyService.GetBlogs(facId);
        }

        [GET("api/faculty/{facId}/students")]
        public List<UserDTO> GetFacStudents(int facId)
        {
            return facultyService.GetStudents(facId);
        }

        // POST api/<controller>
        public FacultyDTO Post([FromBody] FacultyDTO facDTO)
        {
            FacultyDTO retVal = facultyService.Add(facDTO);
            return retVal;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] FacultyDTO facDTO)
        {
            facultyService.Update(id, facDTO);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            facultyService.Delete(id);
        }
    }
}