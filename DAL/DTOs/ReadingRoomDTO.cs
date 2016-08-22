using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class ReadingRoomDTO
    {
        public long Id { get; set; }        
        public string Name { get; set; }
        public int Dimension { get; set; }
        public DateTime WorkingTimeFrom { get; set; }
        public DateTime WorkingTimeTo { get; set; }
        public bool ChecksIndex { get; set; }
        public DateTime ChecksIndexFrom { get; set; }
        public DateTime ChecksIndexTo { get; set; }

        //Faculty info
        public long FacultyId { get; set; }
        public string FacultyName { get; set; }

        //University info
        public long UniversityId { get; set; }
        public string UniversityName { get; set; }
        
        //Google Maps info
        public decimal Latidude { get; set; }
        public decimal Longitude { get; set; }
    }
}
