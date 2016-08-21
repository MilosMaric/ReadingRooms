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
    public class FacultyRepositoryImpl : IFacultyRepository
    {
        private ReadingRoomsEntities ctx;

        public FACULTY Add(FACULTY entity)
        {
            FACULTY insertedFaculty = null;

            if (CheckHelper.IsFilled(entity))
            {
                using (ctx = new ReadingRoomsEntities())
                {
                    entity.FAC_ID = 1;

                    FACULTY maxFaculty = ctx.FACULTies.OrderByDescending(f => f.FAC_ID).FirstOrDefault();
                    if (CheckHelper.IsFilled(maxFaculty))
                    {
                        entity.FAC_ID = maxFaculty.FAC_ID + 1;
                    }

                    insertedFaculty = ctx.FACULTies.Add(entity);
                    ctx.SaveChanges();
                }
            }

            return insertedFaculty;
        }

        public FACULTY Update(FACULTY entity)
        {
            FACULTY updatedFaculty = null;

            using (ctx = new ReadingRoomsEntities())
            {
                updatedFaculty = ctx.FACULTies.Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
            }

            return updatedFaculty;
        }

        public void Delete(long id)
        {
            using (ctx = new ReadingRoomsEntities())
            {
                FACULTY entity = ctx.FACULTies.Where(f => f.FAC_ID == id).FirstOrDefault();
                if (CheckHelper.IsFilled(entity))
                {
                    ctx.FACULTies.Remove(entity);
                    ctx.SaveChanges();
                }
            }
        }

        public FACULTY GetById(long id)
        {
            FACULTY faculty = null;

            using (ctx = new ReadingRoomsEntities())
            {
                if (id > 0)
                {
                    faculty = ctx.FACULTies
                        .Where(f => f.FAC_ID == id)
                        .Include(f => f.UNIVERSITY)
                        .FirstOrDefault();
                }
            }

            return faculty;
        }

        public List<FACULTY> GetAll()
        {
            List<FACULTY> faculties = null;

            using (ctx = new ReadingRoomsEntities())
            {
                faculties = ctx.FACULTies
                    .Where(f => f.FAC_ID > 0)
                    .Include(f => f.UNIVERSITY)
                    .ToList();
            }

            return faculties;
        }

        public List<FACULTY> GetForUniversity(long uniId)
        {
            List<FACULTY> faculties = null;

            using (ctx = new ReadingRoomsEntities())
            {
                var collection = ctx.FACULTies
                    .Where(f => f.UNI_ID == uniId)
                    .Include(f => f.UNIVERSITY);
                if (CheckHelper.IsFilled(collection))
                {
                    faculties = collection.ToList();
                }
            }

            return faculties;
        }
    }
}
