using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs;
using DAL.Helpers;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;

namespace DAL.Transformers.Implementations
{
    public class SeatTransformer : ITransformer<SEAT, SeatDTO>
    {
        public SeatDTO TransformToDTO(SEAT entry)
        {
            return new SeatDTO()
            {
                Id = entry.SEAT_ID,
                ReadingRoomId = entry.RROOM_ID,
                Label = entry.SEAT_LABEL,
                Position = entry.SEAT_POSITION,
            };
        }

        public List<SeatDTO> TransformToDTO(List<SEAT> entries)
        {
            List<SeatDTO> dtos = null;
            if (CheckHelper.IsFilled(entries))
            {
                dtos = new List<SeatDTO>();
                foreach (SEAT entry in entries)
                {
                    dtos.Add(TransformToDTO(entry));
                }
            }

            return dtos;
        }

        public SEAT TransformFromDTO(long id, SeatDTO dto)
        {
            return new SEAT()
            {
                SEAT_ID = id,
                RROOM_ID = dto.ReadingRoomId,
                SEAT_LABEL = dto.Label,
                SEAT_POSITION = dto.Position
            };
        }
    }
}
