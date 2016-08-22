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
    public class RroomController : ApiController
    {
        IReadingRoomService rroomService = new ReadingRoomServiceImpl();

        // GET api/<controller>
        public List<ReadingRoomDTO> Get()
        {
            return rroomService.GetAll();
        }

        // GET api/<controller>/5
        public ReadingRoomDTO Get(int id)
        {
            return rroomService.GetById(id);
        }

        [GET("api/faculty/{rroomId}/schema")]
        public List<SeatDTO> GetFacStudents(int rroomId)
        {
            return rroomService.GetSittingSchema(rroomId);
        }

        // POST api/<controller>
        [JWTAuthorize(ForbiddenRole = AppConstants.STUDENT)]
        public ReadingRoomDTO Post([FromBody]ReadingRoomDTO rroom)
        {
            ReadingRoomDTO retVal = rroomService.Add(rroom);
            return retVal;
        }

        // PUT api/<controller>/5
        [JWTAuthorize(ForbiddenRole = AppConstants.STUDENT)]
        public void Put(int id, [FromBody]ReadingRoomDTO rroom)
        {
            rroomService.Update(id, rroom);
        }

        // DELETE api/<controller>/5
        [JWTAuthorize(ForbiddenRole = AppConstants.STUDENT)]
        public void Delete(int id)
        {
            rroomService.Delete(id);
        }
    }
}