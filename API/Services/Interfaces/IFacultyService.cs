using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs;

namespace API.Services.Interfaces
{
    interface IFacultyService
    {
        FacultyDTO Add(FacultyDTO faculty);
        FacultyDTO Update(long id, FacultyDTO faculty);
        Boolean Delete(long id);
        List<FacultyDTO> GetAll();
        FacultyDTO GetById(long id);

        List<ReadingRoomDTO> GetReadingRooms(long facId);
        List<UserDTO> GetStudents(long facId);
        List<BlogDTO> GetBlogs(long facId);
    }
}
