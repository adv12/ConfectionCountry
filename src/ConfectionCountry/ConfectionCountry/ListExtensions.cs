// Copyright (c) 2019 Andrew Vardeman.  Published under the MIT license.
// See license.txt in the ConfectionCountry distribution or repository for the
// full text of the license.

using System;
using System.Collections.Generic;

namespace ConfectionCountry
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random random = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int indexToSwap = random.Next(0, i + 1);
                T itemI = list[i];
                list[i] = list[indexToSwap];
                list[indexToSwap] = itemI;
            }
        }
    }
}
