using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs;
using DAL.Helpers;

namespace DAL.Transformers
{
    public class FacultyTransformer : ITransformer<FACULTY, FacultyDTO>
    {
        public FacultyDTO TransformToDTO(FACULTY entry)
        {
            FacultyDTO retVal = null;

            if (CheckHelper.IsFilled(entry) && CheckHelper.IsFilled(entry.UNIVERSITY))
            {
                retVal = new FacultyDTO() 
                {
                    Id = entry.FAC_ID,
                    Name = entry.FAC_NAME,
                    Email = entry.FAC_EMAIL,
                    Address = entry.FAC_ADDR,
                    Abberation = entry.FAC_ABBR,
                    Telephone = entry.FAC_TEL,
                    WebSite = entry.FAC_WEB,
                    UniversityId = entry.UNI_ID,
                    UniversityName = entry.UNIVERSITY.UNI_NAME
                };
            }

            return retVal;
        }

        public List<FacultyDTO> TransformToDTO(List<FACULTY> entries)
        {
            List<FacultyDTO> facultyDTOs = new List<FacultyDTO>();
            FacultyDTO dto;

            if (CheckHelper.IsFilled(entries))
            {
                foreach (FACULTY entry in entries)
                {
                    if (CheckHelper.IsFilled(entry))
                    {
                        dto = TransformToDTO(entry);
                        facultyDTOs.Add(dto);
                    }
                }
            }

            return facultyDTOs;
        }

        public FACULTY TransformFromDTO(long id, FacultyDTO dto)
        {
            FACULTY retVal = new FACULTY();

            if (CheckHelper.IsFilled(dto))
            {
                retVal.FAC_ID = id;
                retVal.FAC_ABBR = dto.Abberation;
                retVal.FAC_ADDR = dto.Address;
                retVal.FAC_EMAIL = dto.Email;
                retVal.FAC_NAME = dto.Name;
                retVal.FAC_TEL = dto.Telephone;
                retVal.FAC_WEB = dto.WebSite;
                retVal.UNI_ID = dto.UniversityId;
            }
            else
            {
                retVal = null;
            }

            return retVal;
        }
    }
}
