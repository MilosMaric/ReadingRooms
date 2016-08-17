using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Transformers
{
    public interface ITransformer<T, DTO>
    {
        DTO TransformToDTO(T entry);
        List<DTO> TransformToDTO(List<T> entries);
        T TransformFromDTO(long id, DTO dto);
    }
}
