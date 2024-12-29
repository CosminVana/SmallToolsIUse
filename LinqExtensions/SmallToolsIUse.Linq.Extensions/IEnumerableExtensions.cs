using SmallToolsIUse.Linq.Extensions.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SmallToolsIUse.Linq.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Checks if the provided IEnumerable instance is null or empty
        /// </summary>
        /// <param name="enumerable">IEnumerable instance to check</param>
        /// <returns>A boolean value indicating if the collection is null or empty</returns>
        public static bool IsNullOrEmpty(this IEnumerable enumerable) => enumerable == null || !enumerable.GetEnumerator().MoveNext();

        /// <summary>
        /// Will return all the elements which exist in source IEnumerable, but not in destination IEnumerable, based on the key selector
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="keySelector">The key which should be used to compare the values of the two IEnumerable</param>
        /// <returns></returns>
        public static IEnumerable<TCollection> NotIn<TCollection, TKey>(this IEnumerable<TCollection> source,
            IEnumerable<TCollection> destination, Func<TCollection, TKey> keySelector) where TCollection : class
        {
            IEnumerable<TCollection> notIn = from s in source
                                             join d in destination
                                                 on keySelector(s) equals keySelector(d) into g
                                             from element in g.DefaultIfEmpty()
                                             where element == null
                                             select s;

            return notIn;
        }

        /// <summary>
        /// Will return all the elements which exist in both the source and destination IEnumerable, based on the key selector
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="keySelector">The key which should be used to compare the values of the two IEnumerable</param>
        public static IEnumerable<TCollection> In<TCollection, TKey>(this IEnumerable<TCollection> source,
            IEnumerable<TCollection> destination, Func<TCollection, TKey> keySelector) where TCollection : class
        {
            IEnumerable<TCollection> inDestination = from s in source
                                                     join d in destination
                                                         on keySelector(s) equals keySelector(d)
                                                     select s;

            return inDestination;
        }

        /// <summary>
        /// Will return all the elements which exist in source IEnumerable, but not in destination IEnumerable, based on the key selector
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="sourceKeySelector">The key which should be used for the source IEnumerable</param>
        /// <param name="destKeySelector">The key which should be used for the destination IEnumerable</param>
        /// <returns></returns>
        public static IEnumerable<TSourceCollection> In<TSourceCollection, TDestinationCollection, TKey>(this IEnumerable<TSourceCollection> source,
            IEnumerable<TDestinationCollection> destination, Func<TSourceCollection, TKey> sourceKeySelector, Func<TDestinationCollection, TKey> destKeySelector) where TSourceCollection : class
        {
            IEnumerable<TSourceCollection> In = from s in source
                                                join d in destination
                                                    on sourceKeySelector(s) equals destKeySelector(d)
                                                select s;

            return In;
        }

        /// <summary>
        /// Will return all the elements which exist in both the source and destination IEnumerable, based on the key selector for each collection
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="sourceKeySelector">The key which should be used for the source IEnumerable</param>
        /// <param name="destKeySelector">The key which should be used for the destination IEnumerable</param>
        /// <returns></returns>
        public static IEnumerable<TSourceCollection> NotIn<TSourceCollection, TDestinationCollection, TKey>(this IEnumerable<TSourceCollection> source,
            IEnumerable<TDestinationCollection> destination, Func<TSourceCollection, TKey> sourceKeySelector, Func<TDestinationCollection, TKey> destKeySelector) where TSourceCollection : class
        {
            IEnumerable<TSourceCollection> notIn = from s in source
                                                   join d in destination
                                                       on sourceKeySelector(s) equals destKeySelector(d) into g
                                                   from element in g.DefaultIfEmpty()
                                                   where element == null
                                                   select s;

            return notIn;
        }

        /// <summary>
        /// Will return all the elements which exist in both the source and destination IEnumerable, based on the key selector for each collection, but which are different based on the comparison predicate
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="keySelector">The key which should be used to compare the values of the two IEnumerable</param>
        /// <param name="comparisonPredicate">A predicate which decides if the 2 items are different</param>
        /// <returns></returns>
        public static IEnumerable<EnumerableItemDiff<TCollection>> ModifiedIn<TCollection, TKey>(this IEnumerable<TCollection> source, IEnumerable<TCollection> destination, Func<TCollection, TKey> keySelector, Func<TCollection, TCollection, bool> comparisonPredicate)
        {
            return from s in source
                   join d in destination
                       on keySelector(s) equals keySelector(d)
                   where comparisonPredicate(s, d)
                   select new EnumerableItemDiff<TCollection>
                   {
                       Source = s,
                       Destination = d
                   };
        }

        /// <summary>
        /// Will return all the elements which exist in both the source and destination IEnumerable, based on the key selector for each collection, but which are different based on the comparison predicate
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="sourceKeySelector">The key which should be used for the source IEnumerable</param>
        /// <param name="destKeySelector">The key which should be used for the destination IEnumerable</param>
        /// <returns></returns>
        /// <param name="comparisonPredicate">A predicate which decides if the 2 items are different</param>
        /// <returns></returns>
        public static IEnumerable<EnumerableItemDiff<TSourceCollection, TDestinationCollection>> ModifiedIn<TSourceCollection, TDestinationCollection, TKey>(this IEnumerable<TSourceCollection> source, IEnumerable<TDestinationCollection> destination, Func<TSourceCollection, TKey> sourceKeySelector, Func<TDestinationCollection, TKey> destKeySelector, Func<TSourceCollection, TDestinationCollection, bool> comparisonPredicate)
        {
            return from s in source
                   join d in destination
                       on sourceKeySelector(s) equals destKeySelector(d)
                   where comparisonPredicate(s, d)
                   select new EnumerableItemDiff<TSourceCollection, TDestinationCollection>
                   {
                       Source = s,
                       Destination = d
                   };
        }
    }
}
