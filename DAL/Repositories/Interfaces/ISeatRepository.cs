using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ISeatRepository : IGenericRepository<SEAT>
    {
        bool IsFree(long id, DateTime ETA, DateTime ETD);
        List<SEAT> GetForReadingRoom(long rroomId);
        long getNextId();
    }
}
