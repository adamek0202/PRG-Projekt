using System;
using System.Collections.Generic;

namespace Pokladna
{
    internal static class ListExtensions
    {
        public static IEnumerable<List<T>> Chunk<T>(this List<T> source, int chunkSize)
        {
            for (int i = 0; i < source.Count; i += chunkSize)
            {
                yield return source.GetRange(i, Math.Min(chunkSize, source.Count - i));
            }
        }
    }
}
