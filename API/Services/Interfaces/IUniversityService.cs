using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.DTOs;

namespace API.Services.Interfaces
{
    interface IUniversityService
    {
        UniversityDTO Add(UniversityDTO uni);
        UniversityDTO Update(long id, UniversityDTO uni);
        Boolean Delete(long id);
        List<UniversityDTO> GetAll();
        UniversityDTO GetById(long id);

        List<FacultyDTO> GetFaculties(long uniId);
        List<ReadingRoomDTO> GetReadingRooms(long uniId);
    }
}
