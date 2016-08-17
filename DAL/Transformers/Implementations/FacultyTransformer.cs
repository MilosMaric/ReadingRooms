using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs;

namespace DAL.Transformers
{
    public class FacultyTransformer : ITransformer<FACULTY, FacultyDTO>
    {
        public FacultyDTO TransformToDTO(FACULTY entry)
        {
            return null;
        }

        public List<FacultyDTO> TransformToDTO(List<FACULTY> entries)
        {
            return null;
        }

        public FACULTY TransformFromDTO(long id, FacultyDTO dto)
        {
            return null;
        }
    }
}
