using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class SeatDTO
    {
        public long Id { get; set; }
        public long ReadingRoomId { get; set; }
        public int Position { get; set; }
        public string Label { get; set; }
        public bool IsFree { get; set; }
    }
}
