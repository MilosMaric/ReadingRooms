using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class ReadingRoomStatusDTO
    {
        public ReadingRoomDTO ReadingRoom { get; set; }

        public long FacultyId { get; set; }
        public string FacultyName { get; set; }

        public long UniversityId { get; set; }
        public string UniversityName { get; set; }

        public int FreeSeats { get; set; }
    }
}
