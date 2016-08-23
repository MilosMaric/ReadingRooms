using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs;

namespace API.Services.Interfaces
{
    interface IUserService
    {
        UserDTO Add(UserDTO user);
        UserDTO Update(long id, UserDTO user);
        Boolean Delete(long id);
        List<UserDTO> GetAll();
        UserDTO GetById(long id);
                
        List<PostDTO> GetPosts(long userId);           
        List<ThreadDTO> GetThreads(long userId);
        List<ReportDTO> GetReports(long userId);

        JWTDTO GetJWT(LoginDTO credentials);
        bool isAuthorized(long id, string role, string forbiddenRole);
        UserDTO GetLoggedUser(string jwt);
    }
}
