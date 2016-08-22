using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs;

namespace API.Services.Interfaces
{
    public interface IReadingRoomService
    {
        ReadingRoomDTO Add(ReadingRoomDTO rroom);
        ReadingRoomDTO Update(long id, ReadingRoomDTO rroom);
        Boolean Delete(long id);
        List<ReadingRoomDTO> GetAll();
        ReadingRoomDTO GetById(long id);

        List<SeatDTO> GetSittingSchema(long rroomID);
        void AddSeats(List<SeatDTO> schedule);
        void UpdateSeats(List<SeatDTO> schedule);
    }
}
