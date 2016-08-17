using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs;

namespace DAL.Transformers.Implementations
{
    public class UserTransformer : ITransformer<USER, UserDTO>
    {
        public UserDTO TransformToDTO(USER entry)
        {
            return null;
        }

        public List<UserDTO> TransformToDTO(List<USER> entries)
        {
            return null;
        }

        public USER TransformFromDTO(long id, UserDTO dto)
        {
            return null;
        }
    }
}
