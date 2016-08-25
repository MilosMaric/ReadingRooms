using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Helpers;
using DAL.Repositories.Interfaces;
using System.Data.Entity;

namespace DAL.Repositories.Implementations
{
    public class UserRepositoryImpl : IUserRepository
    {
        private ReadingRoomsEntities ctx;

        public USER Add(USER entity)
        {
            USER insertedUser = null;

            if (CheckHelper.IsFilled(entity))
            {
                using (ctx = new ReadingRoomsEntities())
                {
                    entity.USR_ID = 1;

                    USER maxUser = ctx.USERs.OrderByDescending(f => f.USR_ID).FirstOrDefault();
                    if (CheckHelper.IsFilled(maxUser))
                    {
                        entity.USR_ID = maxUser.USR_ID + 1;
                    }

                    insertedUser = ctx.USERs.Add(entity);
                    ctx.SaveChanges();
                }
            }

            return insertedUser;
        }

        public USER Update(USER entity)
        {
            USER updatedUser = null;

            using (ctx = new ReadingRoomsEntities())
            {
                updatedUser = ctx.USERs.Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
            }

            return updatedUser;
        }

        public void Delete(long id)
        {
            using (ctx = new ReadingRoomsEntities())
            {
                USER entity = ctx.USERs.Where(f => f.USR_ID == id).FirstOrDefault();
                if (CheckHelper.IsFilled(entity))
                {
                    ctx.USERs.Remove(entity);
                    ctx.SaveChanges();
                }
            }
        }

        public USER GetById(long id)
        {
            USER user = null;

            using (ctx = new ReadingRoomsEntities())
            {
                user = ctx.USERs
                    .Where(u => u.USR_ID == id)
                    .Include(f => f.FACULTY)
                    .FirstOrDefault();
            }

            return user;
        }

        public List<USER> GetAll()
        {
            List<USER> users = null;

            using (ctx = new ReadingRoomsEntities())
            {
                users = ctx.USERs
                    .Include(f => f.FACULTY)
                    .OrderBy(u => u.USR_ID)
                    .ToList();
                    
            }

            return users;
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


        public List<USER> GetForFaculty(long facultyId)
        {
            List<USER> users = null;

            using (ctx = new ReadingRoomsEntities())
            {
                users = ctx.USERs
                    .Where(u => u.FAC_ID == facultyId)
                    .Include(f => f.FACULTY)
                    .ToList();

            }

            return users;
        }
    }
}
