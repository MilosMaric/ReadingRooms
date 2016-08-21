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
    public class SeatRepositoryImpl : ISeatRepository
    {
        private ReadingRoomsEntities ctx;

        public SEAT Add(SEAT entity)
        {
            SEAT insertedSeat = null;

            if (CheckHelper.IsFilled(entity))
            {
                using (ctx = new ReadingRoomsEntities())
                {
                    entity.SEAT_ID = 1;

                    SEAT maxSeat = ctx.SEATs.OrderByDescending(s => s.SEAT_ID).FirstOrDefault();
                    if (CheckHelper.IsFilled(maxSeat))
                    {
                        entity.SEAT_ID = maxSeat.SEAT_ID + 1;
                    }

                    insertedSeat = ctx.SEATs.Add(entity);
                    ctx.SaveChanges();
                }
            }

            return insertedSeat;
        }

        public SEAT Update(SEAT entity)
        {
            SEAT updatedSeat = null;

            using (ctx = new ReadingRoomsEntities())
            {
                updatedSeat = ctx.SEATs.Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
            }

            return updatedSeat;
        }

        public void Delete(long id)
        {
            using (ctx = new ReadingRoomsEntities())
            {
                SEAT entity = ctx.SEATs.Where(s => s.SEAT_ID == id).FirstOrDefault();
                if (CheckHelper.IsFilled(entity))
                {
                    ctx.SEATs.Remove(entity);
                    ctx.SaveChanges();
                }
            }
        }

        public SEAT GetById(long id)
        {
            SEAT seat = null;

            using (ctx = new ReadingRoomsEntities())
            {
                if (id > 0)
                {
                    seat = ctx.SEATs
                        .Where(s => s.SEAT_ID == id)
                        .Include(s => s.RESERVATIONs)
                        .FirstOrDefault();
                }
            }

            return seat;
        }

        public List<SEAT> GetAll()
        {
            List<SEAT> seats = null;

            using (ctx = new ReadingRoomsEntities())
            {
                seats = ctx.SEATs
                    .Where(s => s.SEAT_ID > 0)
                    .Include(s => s.RESERVATIONs)
                    .ToList();
            }

            return seats;
        }

        public bool IsFree(long id, DateTime ETA, DateTime ETD)
        {
            bool isFree = false;

            if(id > 0 && CheckHelper.IsFilled(ETA) && CheckHelper.IsFilled(ETD))
            {
                using (ctx = new ReadingRoomsEntities())
                {
                    isFree = GetById(id).RESERVATIONs
                        .Any(r => (r.RES_ETA < ETA && r.RES_ETD > ETA) || 
                            (r.RES_ETA < ETD && r.RES_ETD > ETD));
                }
            }

            return isFree;
        }
    }
}
