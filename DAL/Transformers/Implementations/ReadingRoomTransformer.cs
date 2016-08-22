using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs;
using DAL.Helpers;

namespace DAL.Transformers
{
    public class ReadingRoomTransformer : ITransformer<READING_ROOM, ReadingRoomDTO>
    {
        public ReadingRoomDTO TransformToDTO(READING_ROOM entry)
        {
            ReadingRoomDTO dto = null;

            if (CheckHelper.IsFilled(entry))
            {
                dto = new ReadingRoomDTO()
                {
                    Id = entry.RROOM_ID,
                    Name = entry.RROOM_NAME,
                    Dimension = entry.RROOM_DIM,
                    WorkingTimeFrom = entry.RROOM_WTF,
                    WorkingTimeTo = entry.RROOM_WTT,
                    ChecksIndex = entry.RROOM_IDX,
                    ChecksIndexFrom = (DateTime)entry.RROOM_IDXF,
                    ChecksIndexTo = (DateTime)entry.RROOM_IDXT,
                    Longitude = (decimal)entry.RROOM_LON,
                    Latidude = (decimal)entry.RROOM_LAT,
                    FacultyId = 0,
                    UniversityId = 0
                };

                if (CheckHelper.IsFilled(entry.FACULTY))
                {
                    dto.FacultyId = (long)entry.FAC_ID;
                    dto.FacultyName = entry.FACULTY.FAC_NAME;
                }

                if (CheckHelper.IsFilled(entry.UNIVERSITY))
                {
                    dto.FacultyId = (long)entry.UNI_ID;
                    dto.FacultyName = entry.UNIVERSITY.UNI_NAME;
                }
            }

            return dto;
        }

        public List<ReadingRoomDTO> TransformToDTO(List<READING_ROOM> entries)
        {
            List<ReadingRoomDTO> dtos = null;
            ReadingRoomDTO dto;

            if (CheckHelper.IsFilled(entries))
            {
                foreach (READING_ROOM rroom in entries)
                {
                    dto = TransformToDTO(rroom);
                    dtos.Add(dto);
                }
            }

            return dtos;
        }

        public READING_ROOM TransformFromDTO(long id, ReadingRoomDTO dto)
        {
            READING_ROOM rroom = new READING_ROOM() 
            {
                FAC_ID = dto.FacultyId,
                RROOM_DIM = dto.Dimension,
                RROOM_ID = id,
                RROOM_IDX = dto.ChecksIndex,
                RROOM_IDXF = dto.ChecksIndexFrom,
                RROOM_IDXT = dto.ChecksIndexTo,
                RROOM_LAT = dto.Latidude,
                RROOM_LON = dto.Longitude,
                RROOM_NAME = dto.Name,
                RROOM_WTF = dto.WorkingTimeFrom,
                RROOM_WTT = dto.WorkingTimeTo,
                UNI_ID = dto.UniversityId
            };
            
            return rroom;
        }
    }
}
