using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class ListHepler<T>
    {
        public ListHepler()
        {
            
        }

        public static List<T> CutListByIndex(List<T> list, int start, int end,  ref decimal totalrows)
        {
            totalrows = list.Count();
            List<T> result = list.Skip(start).Take(end-start).ToList();
            return result;
        }
    }
}
