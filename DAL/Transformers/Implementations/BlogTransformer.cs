using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTOs;

namespace DAL.Transformers.Implementations
{
    public class BlogTransformer : ITransformer<BLOG, BlogDTO>
    {
        public BlogDTO TransformToDTO(BLOG entry)
        {
            return null;
        }

        public List<BlogDTO> TransformToDTO(List<BLOG> entries)
        {
            return null;
        }

        public BLOG TransformFromDTO(long id, BlogDTO dto)
        {
            return null;
        }
    }
}
