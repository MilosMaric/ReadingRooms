using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs;

namespace API.Services.Interfaces
{
    interface IReservationService
    {
        ReservationDTO Add(ReservationDTO res);
        ReservationDTO Update(long id, ReservationDTO res);
        Boolean Delete(long id);
        List<ReservationDTO> GetAll();
        ReservationDTO GetById(long id);

        List<ReservationDTO> GetStudentsReservations(long id);
        List<ReservationDTO> GetReservationsForReadingRoom(long readingRoomId);

        ReservationDTO FinalizeDTOTransformation(ReservationDTO dto);
        List<ReservationDTO> FinalizeDTOTransformation(List<ReservationDTO> dto);
    }
}
