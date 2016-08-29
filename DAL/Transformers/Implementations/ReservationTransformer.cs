using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs;
using DAL.Helpers;
using DAL.Repositories.Implementations;

namespace DAL.Transformers.Implementations
{
    public class ReservationTransformer : ITransformer<RESERVATION, ReservationDTO>
    {
        public ReservationDTO TransformToDTO(RESERVATION entry)
        {
            ReservationDTO dto = null;
            READING_ROOM rroom = null;
            ReadingRoomRepositoryImpl repo = new ReadingRoomRepositoryImpl();

            if (CheckHelper.IsFilled(entry.SEATs))
            {
                rroom = repo.GetById(entry.SEATs.ToList()[0].RROOM_ID);
            }


            if (CheckHelper.IsFilled(entry))
            { 
                dto = new ReservationDTO()
                {
                    Id = entry.RES_ID,
                    ETA = entry.RES_ETA,
                    ETD = entry.RES_ETD,
                    UserId = entry.USR_ID,
                    ReadingRoom = new ReadingRoomTransformer().TransformToDTO(rroom)
                };
            }

            return dto;
        }

        public List<ReservationDTO> TransformToDTO(List<RESERVATION> entries)
        {
            List<ReservationDTO> reservations = null;

            if (CheckHelper.IsFilled(entries))
            {
                reservations = new List<ReservationDTO>();
                foreach (RESERVATION entry in entries)
                {
                    reservations.Add(TransformToDTO(entry));
                }
            }

            return reservations;
        }

        public RESERVATION TransformFromDTO(long id, ReservationDTO dto)
        {
            return new RESERVATION()
            {
                RES_ID = id,
                USR_ID = dto.UserId,
                RES_ETA = dto.ETA,
                RES_ETD = dto.ETD
            };
        }
    }
}
