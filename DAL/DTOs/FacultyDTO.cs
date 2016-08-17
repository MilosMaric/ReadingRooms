using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class FacultyDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Abberation { get; set; }
        public string Address { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }

        //FK
        public long UniversityId { get; set; }

        //Semantic field
        public string UniversityName { get; set; }
    }
}
