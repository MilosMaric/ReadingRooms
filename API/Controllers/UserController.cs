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
        private IReservationService resService = new ReservationServiceImpl();

        // GET api/<controller>
        public List<UserDTO> Get()
        {
            return userService.GetAll();
        }

        // GET api/<controller>/5
        public UserDTO Get(int id)
        {
            return userService.GetById(id);
        }

        [GET("api/user/{userId}/reservations")]
        public List<ReservationDTO> GetUserReservations(int userId)
        {
            UserDTO user = userService.GetById(userId);
            List<ReservationDTO> retVal = null;

            if (user.Role.Equals(AppConstants.STUDENT))
            { 
                retVal = resService.GetStudentsReservations(userId);
            }

            return retVal;
        }
        
        // POST api/<controller>
        public UserDTO Post([FromBody]UserDTO dto)
        {
            UserDTO retVal = userService.Add(dto);
            return retVal;
        }
        
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]UserDTO userDTO)
        {
            userService.Update(id, userDTO);
        }

        // DELETE api/<controller>/5
        [JWTAuthorize(ForbiddenRole = AppConstants.STUDENT)]
        public void Delete(int id)
        {
            userService.Delete(id);
        }
    }
}