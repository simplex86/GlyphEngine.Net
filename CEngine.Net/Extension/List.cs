using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    public static class ListExtension
    {
        public static T Get<T>(this List<T> self, int index)
        {
            if (index >= 0) return self[index];
            return self[self.Count + index];
        }
    }
}
