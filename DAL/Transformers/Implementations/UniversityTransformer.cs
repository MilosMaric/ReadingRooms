using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs;
using DAL.Helpers;

namespace DAL.Transformers
{
    public class UniversityTransformer : ITransformer<UNIVERSITY, UniversityDTO>
    {
        public UniversityDTO TransformToDTO(UNIVERSITY entry)
        {
            UniversityDTO retVal = null;

            if (CheckHelper.IsFilled(entry))
            {
                retVal = new UniversityDTO()
                {
                    Id = entry.UNI_ID,
                    Name = entry.UNI_NAME,
                    City = entry.UNI_CITY
                };
            }

            return retVal;
        }

        public List<UniversityDTO> TransformToDTO(List<UNIVERSITY> entries)
        {
            List<UniversityDTO> uniDTOs = new List<UniversityDTO>();
            UniversityDTO dto;

            if (CheckHelper.IsFilled(entries))
            {
                foreach (UNIVERSITY entry in entries)
                {
                    if (CheckHelper.IsFilled(entry))
                    {
                        dto = TransformToDTO(entry);
                        uniDTOs.Add(dto);
                    }
                }
            }

            return uniDTOs;
        }

        public UNIVERSITY TransformFromDTO(long id, UniversityDTO dto)
        {
            UNIVERSITY retVal = new UNIVERSITY();

            if (CheckHelper.IsFilled(dto))
            {
                retVal.UNI_ID = id;
                retVal.UNI_NAME = dto.Name;
                retVal.UNI_CITY = dto.City;
            }
            else
            {
                retVal = null;
            }

            return retVal;
        }
    }
}
