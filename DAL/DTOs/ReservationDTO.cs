﻿using System;
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
        public long UserId { get; set; }
        public ReadingRoomDTO ReadingRoom { get; set; }
    }
}
