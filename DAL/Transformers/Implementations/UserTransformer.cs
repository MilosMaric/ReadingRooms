using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs;
using DAL.Helpers;

namespace DAL.Transformers.Implementations
{
    public class UserTransformer : ITransformer<USER, UserDTO>
    {
        public UserDTO TransformToDTO(USER entry)
        {
            UserDTO retVal = null;

            if (CheckHelper.IsFilled(entry) && CheckHelper.IsFilled(entry.FACULTY))
            {
                retVal = new UserDTO()
                {
                    Id = entry.USR_ID,
                    Username = entry.USR_USERNAME,
                    Email = entry.USR_EMAIL,
                    Index = entry.USR_IDX,
                    Name = entry.USR_NAME,
                    LastName = entry.USR_LNAME,
                    Role = entry.USR_ROLE,
                    FacultyId = (long)entry.FAC_ID,
                    FacultyName = entry.FACULTY.FAC_NAME,
                    Password = entry.USR_PASS
                };
            }

            return retVal;
        }

        public List<UserDTO> TransformToDTO(List<USER> entries)
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            UserDTO dto;

            if (CheckHelper.IsFilled(entries))
            {
                foreach (USER entry in entries)
                {
                    if (CheckHelper.IsFilled(entry))
                    {
                        dto = TransformToDTO(entry);
                        userDTOs.Add(dto);
                    }
                }
            }

            return userDTOs;
        }

        public USER TransformFromDTO(long id, UserDTO dto)
        {
            USER retVal = new USER();

            if (CheckHelper.IsFilled(dto))
            {
                retVal.USR_ID = id;
                retVal.USR_USERNAME = dto.Username;
                retVal.USR_EMAIL = dto.Email;
                retVal.USR_NAME = dto.Name;
                retVal.USR_LNAME = dto.LastName;
                retVal.USR_IDX = dto.Index;
                retVal.USR_ROLE = dto.Role;
                retVal.FAC_ID = dto.FacultyId;
                retVal.USR_PASS = dto.Password;
            }
            else
            {
                retVal = null;
            }

            return retVal;
        }
    }
}
