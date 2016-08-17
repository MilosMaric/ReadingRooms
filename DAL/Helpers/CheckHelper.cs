using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers
{
    public static class CheckHelper
    {
        public static bool IsFilled(Object obj) 
        {
            bool isFilled = false;

            if (obj != null)
            {
                isFilled = true;
            }

            return isFilled; 
        }

        public static bool IsFilled(ICollection<Object> collection)
        {
            bool isFilled = false;

            if (collection != null && collection.Count > 0)
            {
                isFilled = true;
            }

            return isFilled;
        }
    }
}
