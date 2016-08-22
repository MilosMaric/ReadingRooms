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
    public class ReadingRoomRepositoryImpl : IReadingRoomRepository
    {
        private ReadingRoomsEntities ctx;

        public READING_ROOM Add(READING_ROOM entity)
        {
            READING_ROOM insertedReadingRoom = null;

            if (CheckHelper.IsFilled(entity))
            {
                using (ctx = new ReadingRoomsEntities())
                {
                    entity.RROOM_ID = 1;

                    READING_ROOM maxReadingRoom = ctx.READING_ROOM.OrderByDescending(f => f.RROOM_ID).FirstOrDefault();
                    if (CheckHelper.IsFilled(maxReadingRoom))
                    {
                        entity.RROOM_ID = maxReadingRoom.RROOM_ID + 1;
                    }

                    insertedReadingRoom = ctx.READING_ROOM.Add(entity);
                    ctx.SaveChanges();
                }
            }

            return insertedReadingRoom;
        }

        public READING_ROOM Update(READING_ROOM entity)
        {
            READING_ROOM updatedReadingRoom = null;

            using (ctx = new ReadingRoomsEntities())
            {
                updatedReadingRoom = ctx.READING_ROOM.Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
            }

            return updatedReadingRoom;
        }

        public void Delete(long id)
        {
            using (ctx = new ReadingRoomsEntities())
            {
                READING_ROOM entity = ctx.READING_ROOM.Where(f => f.RROOM_ID == id).FirstOrDefault();
                if (CheckHelper.IsFilled(entity))
                {
                    ctx.READING_ROOM.Remove(entity);
                    ctx.SaveChanges();
                }
            }
        }

        public READING_ROOM GetById(long id)
        {
            READING_ROOM faculty = null;

            using (ctx = new ReadingRoomsEntities())
            {
                if (id > 0)
                {
                    faculty = ctx.READING_ROOM
                        .Where(rr => rr.RROOM_ID == id)
                        .Include(rr => rr.UNIVERSITY)
                        .Include(rr => rr.FACULTY)
                        .FirstOrDefault();
                }
            }

            return faculty;
        }

        public List<READING_ROOM> GetAll()
        {
            List<READING_ROOM> rrooms = null;

            using (ctx = new ReadingRoomsEntities())
            {
                rrooms = ctx.READING_ROOM
                    .Where(rr => rr.RROOM_ID > 0)
                    .Include(rr => rr.UNIVERSITY)
                    .Include(rr => rr.FACULTY)
                    .ToList();
            }

            return rrooms;
        }

        public List<READING_ROOM> GetRRoomsForFaculty(long facId)
        {
            List<READING_ROOM> rrooms = null;

            using (ctx = new ReadingRoomsEntities())
            {
                rrooms = ctx.READING_ROOM
                    .Where(rr => rr.RROOM_ID == facId)
                    .Include(rr => rr.UNIVERSITY)
                    .Include(rr => rr.FACULTY)
                    .ToList();
            }

            return rrooms;
        }
    }
}
