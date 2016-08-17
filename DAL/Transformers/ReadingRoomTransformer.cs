using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs;

namespace DAL.Transformers
{
    public class ReadingRoomTransformer : ITransformer<READING_ROOM, ReadingRoomDTO>
    {
        public ReadingRoomDTO TransformToDTO(READING_ROOM entry)
        {
            return null;
        }

        public List<ReadingRoomDTO> TransformToDTO(List<READING_ROOM> entries)
        {
            return null;
        }

        public READING_ROOM TransformFromDTO(long id, ReadingRoomDTO dto)
        {
            return null;
        }
    }
}
