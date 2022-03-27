﻿using System;
using System.Collections.Generic;

namespace UniCorn.Utils
{
    public static class ListExtension
    {
        public static T GetLastOrDefault<T>(this List<T> list)
        {
            if (list == null)
            {
                throw new NullReferenceException();
            }

            return list.Count == 0 ? default : list[^1];
        }
    }
}
