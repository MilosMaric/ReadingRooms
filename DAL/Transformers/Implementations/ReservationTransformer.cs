using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs;
using DAL.Helpers;

namespace DAL.Transformers.Implementations
{
    public class ReservationTransformer : ITransformer<RESERVATION, ReservationDTO>
    {
        public ReservationDTO TransformToDTO(RESERVATION entry)
        {
            ReservationDTO dto = null;

            if (CheckHelper.IsFilled(entry))
            { 
                dto = new ReservationDTO()
                {
                    Id = entry.RES_ID,
                    ETA = entry.RES_ETA,
                    ETD = entry.RES_ETD,
                    User = new UserDTO() { Id = entry.USR_ID }
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
                USR_ID = dto.User.Id,
                RES_ETA = dto.ETA,
                RES_ETD = dto.ETD
            };
        }
    }
}
