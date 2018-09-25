using System.Collections.Generic;

namespace AspNetCoreDemoApp.Utils
{
    public class ListUtils
    {
        /// <summary>
        /// Checks if the given list is null or empty.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <typeparam name="T">The type of the list.</typeparam>
        /// <returns>True if the list either null or empty, false otherwise.</returns>
        public static bool IsListNullOrEmpty<T>(IList<T> list)
        {
            return list == null || list.Count == 0;
        }

        /// <summary>
        /// Performs an intersection between two lists of the same type.
        /// Note that the result will maintain the same order as the first list.
        /// </summary>
        /// <param name="firstList">The first list to intersect. The order will match this list.</param>
        /// <param name="secondList">The second list to use to intersect.</param>
        /// <typeparam name="T">The type of the two lists.</typeparam>
        /// <returns>The intersection of the two lists.</returns>
        public static IList<T> IntersectWith<T>(IList<T> firstList, IList<T> secondList)
        {
            IList<T> result = new List<T>();

            foreach (T item in firstList)
            {
                if (secondList.Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}