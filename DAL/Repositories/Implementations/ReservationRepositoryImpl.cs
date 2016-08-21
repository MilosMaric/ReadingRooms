using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Helpers;
using DAL.Repositories.Interfaces;
using System.Data.Entity;
using System.Data;

namespace DAL.Repositories.Implementations
{
    public class ReservationRepositoryImpl : IReservationRepository
    {
        private ReadingRoomsEntities ctx;

        public RESERVATION Add(RESERVATION entity)
        {
            RESERVATION insertedReservation = null;

            if (CheckHelper.IsFilled(entity))
            {
                using (ctx = new ReadingRoomsEntities())
                {
                    entity.RES_ID = 1;

                    RESERVATION maxReservation = ctx.RESERVATIONs.OrderByDescending(s => s.RES_ID).FirstOrDefault();
                    if (CheckHelper.IsFilled(maxReservation))
                    {
                        entity.RES_ID = maxReservation.RES_ID + 1;
                    }

                    insertedReservation = ctx.RESERVATIONs.Add(entity);
                    ctx.SaveChanges();
                }
            }

            return insertedReservation;
        }

        public RESERVATION Update(RESERVATION entity)
        {
            RESERVATION updatedReservation = null;

            using (ctx = new ReadingRoomsEntities())
            {
                updatedReservation = ctx.RESERVATIONs.Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
            }

            return updatedReservation;
        }

        public void Delete(long id)
        {
            using (ctx = new ReadingRoomsEntities())
            {
                RESERVATION entity = ctx.RESERVATIONs.Where(s => s.RES_ID == id).FirstOrDefault();
                if (CheckHelper.IsFilled(entity))
                {
                    ctx.RESERVATIONs.Remove(entity);
                    ctx.SaveChanges();
                }
            }
        }

        public RESERVATION GetById(long id)
        {
            RESERVATION reservation = null;

            using (ctx = new ReadingRoomsEntities())
            {
                if (id > 0)
                {
                    reservation = ctx.RESERVATIONs
                        .Where(s => s.RES_ID == id)
                        .Include(s => s.SEATs)
                        .Include(s => s.USER)
                        .FirstOrDefault();
                }
            }

            return reservation;
        }

        public List<RESERVATION> GetAll()
        {
            List<RESERVATION> reservations = null;

            using (ctx = new ReadingRoomsEntities())
            {
                reservations = ctx.RESERVATIONs
                    .Where(s => s.RES_ID > 0)
                    .Include(s => s.SEATs)
                    .Include(s => s.USER)
                    .ToList();
            }

            return reservations;
        }

        public List<RESERVATION> GetStudentsReservations(long studentId)
        {
            List<RESERVATION> reservations = null;

            using (ctx = new ReadingRoomsEntities())
            {
                reservations = ctx.RESERVATIONs
                    .Where(s => s.USR_ID == studentId)
                    .Include(s => s.SEATs)
                    .Include(s => s.USER)
                    .ToList();
            }

            return reservations;
        }


        public List<RESERVATION> GetReservationsForReadingRoom(long rroomId)
        {
            List<RESERVATION> reservations = null;

            using (ctx = new ReadingRoomsEntities())
            {
                reservations = ctx.RESERVATIONs
                    .Include(s => s.SEATs)
                    .Include(s => s.USER)
                    .Where(s => s.SEATs.
                        All(s1 => s1.RROOM_ID == rroomId))
                    .ToList();
            }

            return reservations;
        }
    }
}
