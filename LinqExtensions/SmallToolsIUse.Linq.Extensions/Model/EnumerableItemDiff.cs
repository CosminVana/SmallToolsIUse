namespace SmallToolsIUse.Linq.Extensions.Model
{
    public class EnumerableItemDiff<T>
    {
        /// <summary>
        /// Element instance in source collection
        /// </summary>
        public T Source { get; set; }

        /// <summary>
        /// Element instance in destination collection
        /// </summary>
        public T Destination { get; set; }
    }

    public class EnumerableItemDiff<TSource, TDestination>
    {
        /// <summary>
        /// Element instance in source collection
        /// </summary>
        public TSource Source { get; set; }

        /// <summary>
        /// Element instance in destination collection
        /// </summary>
        public TDestination Destination { get; set; }
    }
}
