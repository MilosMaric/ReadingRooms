using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<USER>
    {
        USER CheckCredentials(string username, string password);
        List<USER> GetForFaculty(long facultyId);
    }
}
