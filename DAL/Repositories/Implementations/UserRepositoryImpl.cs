using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Helpers;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class UserRepositoryImpl : IUserRepository
    {
        private ReadingRoomsEntities ctx;

        public USER Add(USER entity)
        {
            return null;
        }

        public USER Update(USER entity)
        {
            return null;
        }

        public void Delete(long id)
        {
            
        }

        public USER GetById(long id)
        {
            USER user = null;

            using (ctx = new ReadingRoomsEntities())
            {
                user = ctx.USERs
                    .Where(u => u.USR_ID == id)
                    .FirstOrDefault();
            }

            return user;
        }

        public List<USER> GetAll()
        {
            return null;
        }

        public USER CheckCredentials(string username, string password)
        {
            USER user = null;

            using (ctx = new ReadingRoomsEntities())
            {
                user = ctx.USERs
                    .Where(u => u.USR_USERNAME.Equals(username) && u.USR_PASS.Equals(password))
                    .FirstOrDefault();
            }

            return user;
        }
    }
}
