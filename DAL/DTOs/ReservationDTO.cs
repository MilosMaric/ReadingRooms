using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class ReservationDTO
    {
        public long Id { get; set; }
        public DateTime ETA { get; set; }
        public DateTime ETD { get; set; }
        public UserDTO User { get; set; }
        public List<SeatDTO> Seats { get; set; }
    }
}
