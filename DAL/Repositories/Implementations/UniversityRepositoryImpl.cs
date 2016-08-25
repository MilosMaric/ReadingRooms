using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Helpers;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class UniversityRepositoryImpl: IUniversityRepository
    {
        private ReadingRoomsEntities ctx;

        public UNIVERSITY Add(UNIVERSITY entity)
        {
            UNIVERSITY insertedUni = null;

            if (CheckHelper.IsFilled(entity))
            {                
                using (ctx = new ReadingRoomsEntities())
                {
                    entity.UNI_ID = 1;

                    UNIVERSITY maxUni = ctx.UNIVERSITies.OrderByDescending(u => u.UNI_ID).FirstOrDefault();
                    if (CheckHelper.IsFilled(maxUni)) 
                    {
                        entity.UNI_ID = maxUni.UNI_ID + 1;
                    }

                    insertedUni = ctx.UNIVERSITies.Add(entity);
                    ctx.SaveChanges();
                }
            }

            return insertedUni;
        }

        public UNIVERSITY Update(UNIVERSITY entity)
        {
            UNIVERSITY updatedUni = null;

            using (ctx = new ReadingRoomsEntities())
            {
                updatedUni = ctx.UNIVERSITies.Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
            }

            return updatedUni;
        }

        public void Delete(long id)
        {
            using (ctx = new ReadingRoomsEntities())
            {
                UNIVERSITY entity = ctx.UNIVERSITies.Where(u => u.UNI_ID == id).FirstOrDefault();
                if (CheckHelper.IsFilled(entity))
                {
                    ctx.UNIVERSITies.Remove(entity);
                    ctx.SaveChanges();
                }
            }
        }

        public UNIVERSITY GetById(long id)
        {
            UNIVERSITY uni = null;

            using(ctx = new ReadingRoomsEntities())
            {
                if (id > 0)
                {
                    uni = ctx.UNIVERSITies
                        .Where(u => u.UNI_ID == id)
                        .FirstOrDefault();
                }
            }

            return uni;
        }

        public List<UNIVERSITY> GetAll()
        {
            List<UNIVERSITY> unis = null;

            using (ctx = new ReadingRoomsEntities())
            {
                unis = ctx.UNIVERSITies                    
                    .Where(u => u.UNI_ID > 0)
                    .OrderBy(u => u.UNI_ID)
                    .ToList();
            }

            return unis;
        }
    }
}
