using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class JWTDTO
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string Role { get; set; }
    }
}
