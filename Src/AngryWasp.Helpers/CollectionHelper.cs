using System.Linq;
using System.Collections.Generic;

namespace AngryWasp.Helpers
{
    public static class CollectionHelper
    {
        public static List<T> Join<T>(this List<T> value, IEnumerable<T> toAdd)
        {
            value.AddRange(toAdd);
            return value;
        }
    }
}