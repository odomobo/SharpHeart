﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dragonfly.Engine
{
    internal static class Extensions
    {
        public static T GetOrDefault<T>(this T[] array, int index) where T : struct
        {
            if (index < array.Length)
                return array[index];
            else
                return default;
        }

        public static void QuickRemoveAt<T>(this List<T> list, int index)
        {
            list[index] = list[^1];
            list.RemoveAt(list.Count-1);
        }
    }
}
