using System;
using System.Collections.Generic;

namespace HCEngine.DI
{
    public static class DIExtension
    {
        /// <summary>
        /// Get all the values that is not null.
        /// </summary>
        public static IEnumerable<TSource> NonNull<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("The source has a null value!..");

            foreach (TSource obj in source)
            {
                if (obj != null && !obj.Equals(null)) yield return obj;
            }
        }
    }
}
