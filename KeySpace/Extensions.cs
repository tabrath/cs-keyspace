using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeySpace
{
    public static class Extensions
    {
        public static Key[] SortByDistance(this IEnumerable<Key> toSort, Key center)
        {
            var sorted = toSort.ToList();
            sorted.Sort((a,b) => center.Distance(a).CompareTo(center.Distance(b)));
            return sorted.ToArray();
        }
    }
}
