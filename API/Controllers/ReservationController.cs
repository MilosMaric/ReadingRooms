using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Services.Implementations;
using API.Services.Interfaces;
using DAL.DTOs;

namespace API.Controllers
{
    public class ReservationController : ApiController
    {
        private IReservationService reservationService = new ReservationServiceImpl();

        // GET api/<controller>
        public List<ReservationDTO> Get()
        {
            return reservationService.GetAll();
        }

        // GET api/<controller>/5
        public ReservationDTO Get(int id)
        {
            return reservationService.GetById(id);
        }

        // POST api/<controller>
        public ReservationDTO Post([FromBody]ReservationDTO dto)
        {
            ReservationDTO retVal = reservationService.Add(dto);
            return retVal;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]ReservationDTO dto)
        {
            reservationService.Update(id, dto);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            reservationService.Delete(id);
        }
    }
}