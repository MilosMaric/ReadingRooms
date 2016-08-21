using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs;

namespace DAL.Repositories.Interfaces
{
    public interface IReservationRepository : IGenericRepository<RESERVATION>
    {
        List<RESERVATION> GetStudentsReservations(long studentId);
        List<RESERVATION> GetReservationsForReadingRoom(long rroomId);
    }
}
