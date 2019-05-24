using System.Collections.Generic;
using System.Linq;
using UpdateCollection.Contracts;

namespace UpdateCollection
{
    public static class CollectionExtensions
    {
        public static IUsingSource<TDestination, TSource> UsingSource<TDestination, TSource>(
            this IEnumerable<TDestination> target,
            IEnumerable<TSource> source
        ) 
            where TDestination : class
        {
            return new CollectionUpdater<TDestination, TSource>(target.ToList(), source);
        }
    }
}